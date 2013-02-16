using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.Threading;

namespace FabricantesLib
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class FabricanteA : Fabricante, IFabricanteA
    {
        public string address = "Rua dos Bombeiros Voluntários 17, 4820-142 Braga, Portugal";
 
        public FabricanteA()
        {
            name = "A";
            if (!isCdDequeuerActive)
            {
                isCdDequeuerActive = true;
                Thread t = new Thread(cdDequeuer);
                t.Start();
            }
        }

        public string getAddress()
        {
            return address;
        }
    }

    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class FabricanteB : Fabricante, IFabricanteB
    {
        public string address = "Rua Ferreira Lapa, 2715-311 Lisboa, Portugal";
        
        public FabricanteB()
        {
            name = "B";
            if (!isCdDequeuerActive)
            {
                isCdDequeuerActive = true;
                Thread t = new Thread(cdDequeuer);
                t.Start();
            }
        }

        public string getAddress()
        {
            return address;
        }
    }

    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class FabricanteC : Fabricante, IFabricanteC
    {
        public string address = "Avenida do Mar, zambujeira do mar, 7630-786, portugal";
        
        public FabricanteC()
        {
            name = "C";
            if (!isCdDequeuerActive)
            {
                isCdDequeuerActive = true;
                Thread t = new Thread(cdDequeuer);
                t.Start();
            }
        }

        public string getAddress()
        {
            return address;
        }
    }

}
