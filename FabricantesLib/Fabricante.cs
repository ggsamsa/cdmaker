using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using FabricantesLib.ServiceReference1;

namespace FabricantesLib
{
    public class Fabricante
    {
        //Fila de CDs a gravar
        private Queue<Cd> cdsToBurn = new Queue<Cd>();
        
        //List de CDs gravados
        private List<Cd> recordedCds = new List<Cd>();

        //Qtd de gravadores de CDs disponivel
        private int availableBurners = 1;

        public bool isCdDequeuerActive = false;

        //o Fabricante tem os seus proprios identificadores para os cds
        private int cdIds = 0;

        public string name;

        public LogWriter writer = LogWriter.Instance;

        /**
         * método que é executado em thread paralela, encarregue de "gravar" CDs.
         * como os serviços dos fabricantes têm apenas uma instância, utilizei a variável isCdDequeuerActive
         * para garantir tb apenas uma thread cdDequeuer.
         * */
        public void cdDequeuer()
        {
            while (true)
            {
                if (cdsToBurn.Count > 0 && availableBurners > 0)
                {
                    Cd cd = cdsToBurn.Dequeue();
                    writer.WriteToLog(this.name + " | Id of CD dequeued to record: " + cd.id);
                    availableBurners -= 1;
                    writer.WriteToLog(this.name + " | Number of available CD recorders: " + availableBurners);
                    setOrderStatus(cd.id, "Being recorded");

                    System.Threading.Thread.Sleep(60000);

                    writer.WriteToLog(this.name + " | Id of CD successfully recorded: " + cd.id);
                    recordedCds.Add(cd);
              
                    setOrderStatus(cd.id, "Recorded");

                    availableBurners += 1;
                    writer.WriteToLog(this.name + " | Number of available CD recorders: " + availableBurners);
                }
            }
        }

        //Introduz um CD novo no sistema e devolve ao Backoffice o identificador atribuido
        public int recordCd(string artist, List<string> tracks)
        {
            writer.WriteToLog(this.name + " | ------------------------------"); 
            Cd cd = new Cd();
            cdIds += 1;
            cd.id = cdIds;
            cd.artist = artist;
            cd.tracks = tracks;

            cdsToBurn.Enqueue(cd);
            writer.WriteToLog(this.name + " | New CD from backoffice. ID given: " + cd.id);
            writer.WriteToLog(this.name + " | DETAILS");
            writer.WriteToLog(this.name + " | \t Artist:");
            writer.WriteToLog(this.name + " | \t|\t" + cd.artist);
            writer.WriteToLog(this.name + " | \t Tracks:");

            foreach (String t in cd.tracks)
            {
                writer.WriteToLog(this.name + " | \t|\t" + t);
            }
            writer.WriteToLog(this.name + " | Queued");
            return cd.id;
        }

        /**
         * Altera no backoffice o estado de uma encomenda.
         * */
        private void setOrderStatus(int id, string status)
        {
            BackofficeServiceClient client = new BackofficeServiceClient();

            client.setOrderStatus(id, status);
        }

        public double getBudget()
        {
            BudgetManager budget = BudgetManager.Instance;
            double tmp = budget.getBudget();
            writer.WriteToLog(this.name + " | Backoffice requested a budget. My Answer: " + tmp.ToString());
            return tmp;
        }

        //devolve ao backoffice o estado de um CD
        public string getCdStatus(int id)
        {
            string status = "";
            int i = cdsToBurn.Count;
            while (i > 0)
            {
                Cd cd = cdsToBurn.Dequeue();
                if (cd.id == id)
                {
                    status = cd.status;

                }
                cdsToBurn.Enqueue(cd);
                i--;
            }

            if (status.Length > 0)
            {
                return status;
            }

            foreach (Cd c in recordedCds)
            {
                if (c.id == id)
                {
                    return c.status;
                }
            }
            return "inexistent";
        }
    }

    public class Cd
    {
        public Cd() { }
        public int id; //este identificador não é o do backoffice ou os das transportadoras
        public string artist;
        public List<string> tracks;
        public string status;
    }
}

