using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BackofficeServerLib.ServiceReference1;
using BackofficeServerLib.ServiceReference2;
using BackofficeServerLib.ServiceReference3;
using BackofficeServerLib.ServiceReference4;
using BackofficeServerLib.ServiceReference5;
using BackofficeServerLib.ServiceReference6;
using System.IO;
using System.Timers;
using System.Threading;

namespace BackofficeServerLib
{
    public class ProcessOrder
    {
        private Order order;
        LogWriter writer = LogWriter.Instance;

        public ProcessOrder(Order mOrder)
        {
            order = mOrder;
            order.id = MySingleton.Instance.getNextOrderId();
            order.status = "Processing";
            MySingleton.Instance.addOrder(order);
            Thread t = new Thread(statusChangeChecker);
            t.Start();
        }

        public void statusChangeChecker()
        {
            string last = order.status;
            while (true)
            {
                if (order.status != last)
                {
                    writer.WriteToLog("Status of order " + order.id + " changed to '" + order.status + "'");
                    if(order.status == "Recorded")
                    {
                        submitOrderToTransportadora();
                    }
                    last = order.status;
                }
                System.Threading.Thread.Sleep(1000);
            }
        }

        public void process()
        {
            writer.WriteToLog("------------------------------");
            writer.WriteToLog("Processing order ID " + order.id + " for client " + order.clientUsername + " on address " + order.getAddress());
            
            List<Fabricante> fabricantes = new List<Fabricante>();
            List<Transportadora> transps = new List<Transportadora>();

            FabricanteAClient fabA = new FabricanteAClient();

            Fabricante f = new Fabricante();
            f.id = "A";
            f.address = fabA.getAddress();
            f.budget = fabA.getBudget();
            fabricantes.Add(f);

            FabricanteBClient fabB = new FabricanteBClient();
            
            Fabricante f1 = new Fabricante();
            f1.id = "B";
            f1.address = fabB.getAddress();
            f1.budget = fabB.getBudget();
            fabricantes.Add(f1);

            FabricanteCClient fabC = new FabricanteCClient();
            Fabricante f2 = new Fabricante();
            f2.id = "C";
            f2.address = fabC.getAddress();
            f2.budget = fabC.getBudget();
            fabricantes.Add(f2);

            TransportadoraXClient transX = new TransportadoraXClient();
            Transportadora t = new Transportadora();
            t.id = "X";
            t.address = transX.getAddress();
            transps.Add(t);

            TransportadoraYClient transY = new TransportadoraYClient();
            Transportadora t1 = new Transportadora();
            t.id = "Y";
            t1.address = transY.getAddress();
            transps.Add(t1);

            TransportadoraZClient transZ = new TransportadoraZClient();
            Transportadora t2 = new Transportadora();
            t.id = "Z";
            t2.address = transZ.getAddress();
            transps.Add(t2);

            Tuple<TransportadoraXClient, TransportadoraYClient, TransportadoraZClient> transpClients = new Tuple<TransportadoraXClient, TransportadoraYClient, TransportadoraZClient>
                (transX, transY, transZ);

            MyPair mypair = new MyPair();

            order.mypair = choosePair(fabricantes, transpClients);

            submitOrderToFabricante();
        }

        private void submitOrderToTransportadora()
        {
            switch (order.mypair.transp.id)
            {
                case "X":
                    TransportadoraXClient transpX = new TransportadoraXClient();
                    transpX.sendPackage(order.mypair.fabricante.address, order.getAddress(), order.idInFab);
                    break;
                case "Y":
                    TransportadoraYClient transpY = new TransportadoraYClient();
                    transpY.sendPackage(order.mypair.fabricante.address, order.getAddress(), order.idInFab);
                    break;
                case "Z":
                    TransportadoraZClient transpZ = new TransportadoraZClient();
                    transpZ.sendPackage(order.mypair.fabricante.address, order.getAddress(), order.idInFab);
                    break;
            }
            writer.WriteToLog("");
            writer.WriteToLog("Requested Transportadora " + order.mypair.transp.id + " to send CD with order ID " + order.id);
        }

        //envia a encomenda para o fabricante escolhido
        private void submitOrderToFabricante()
        {
            MySingleton st = MySingleton.Instance;
            int tmpId;
            switch (order.mypair.fabricante.id)
            {
                case "A":
                    FabricanteAClient fabA = new FabricanteAClient();
                    tmpId = fabA.recordCd(order.getArtist(), order.getTracks().ToArray());
                    st.setIdInFabricante(order.id, tmpId);
                    order.fabricante = "A";
                    break;
                case "B":
                    FabricanteBClient fabB = new FabricanteBClient();
                    tmpId = fabB.recordCd(order.getArtist(), order.getTracks().ToArray());
                    st.setIdInFabricante(order.id, tmpId);
                    order.fabricante = "B";
                    break;
                case "C":
                    FabricanteCClient fabC = new FabricanteCClient();
                    tmpId = fabC.recordCd(order.getArtist(), order.getTracks().ToArray());
                    st.setIdInFabricante(order.id, tmpId);
                    order.fabricante = "C";
                    break;
            }

            st.setOrderStatus(order.idInFab, "Sent to Fabricante");
            writer.WriteToLog("");
            writer.WriteToLog("Order ID " + order.id + " sent to record at Fabricante " + 
                order.fabricante + " and was given this CD ID: " + order.idInFab);
        }

        /**
         *  Escolhe o melhor par <Fabricante, Transportadora> com base nos parametros do enunciado.
         *  TODO: por vezes a google responde com valores negativos devido ao excesso de pedidos/seg. Corrigir com timers
         **/
        private MyPair choosePair(List<Fabricante> fabricantes, Tuple<TransportadoraXClient, TransportadoraYClient, TransportadoraZClient> transpClients)
        {
            writer.WriteToLog("Choosing best Transportadora/Fabricante combination");

            TransportadoraXClient transX = new TransportadoraXClient();
            TransportadoraYClient transY = new TransportadoraYClient();
            TransportadoraZClient transZ = new TransportadoraZClient();

            MyPair mypair = new MyPair();
            GeoCoder geocoder = new GeoCoder();
            GeoPoint clientGp = new GeoPoint();
            clientGp = geocoder.GetGeoPointFromAddress(order.getAddress());

            GeoPoint transGp = new GeoPoint();
            GeoPoint fabGp = new GeoPoint();

            Transportadora myT = new Transportadora();

            double bestDistance = 0;
            double bestFootprint = 0;
            double bestBudg = 0;

            double low = 0;

            foreach(Fabricante fab in fabricantes)
            {
                System.Threading.Thread.Sleep(2000);
                double budg = transX.getBudget(fab.address, order.getAddress());
                transGp = geocoder.GetGeoPointFromAddress(transX.getAddress());
                fabGp = geocoder.GetGeoPointFromAddress(fab.address);

                double distance = geocoder.GetDrivingDistance(transGp, fabGp, clientGp);

                double footprint = distance * 0.26 / 1000;
                budg += fab.budget;
                budg += footprint;
                
                if (low == 0 || budg < low)
                {
                    low = budg;
                    myT.budget = low;
                    myT.id = "X";
                    mypair.transp = myT;
                    mypair.fabricante = fab;
                    bestDistance = distance;
                    bestFootprint = footprint;
                    bestBudg = budg;
                }
                writer.WriteToLog("\t--");
                writer.WriteToLog("X - " + fab.id + "\tdistancia: " + distance.ToString() + " footprint: " + footprint.ToString() + " budget: " + budg.ToString());
                writer.WriteToLog(transX.getAddress() + " - " + fab.address);
            }

            foreach (Fabricante fab in fabricantes)
            {
                System.Threading.Thread.Sleep(2000);
                double budg = transY.getBudget(fab.address, order.getAddress());
                transGp = geocoder.GetGeoPointFromAddress(transY.getAddress());
                fabGp = geocoder.GetGeoPointFromAddress(fab.address);

                double distance = geocoder.GetDrivingDistance(transGp, fabGp, clientGp);

                double footprint = distance * 0.26 / 1000;
                budg += fab.budget;
                budg += footprint;

                if (budg < low)
                {
                    low = budg;
                    myT.budget = low;
                    myT.id = "Y";
                    mypair.transp = myT;
                    mypair.fabricante = fab;
                    bestDistance = distance;
                    bestFootprint = footprint;
                    bestBudg = budg;
                }
                writer.WriteToLog("\t--");
                writer.WriteToLog("Y - " + fab.id + "\tdistancia: " + distance.ToString() + " footprint: " + footprint.ToString() + " budget: " + budg.ToString());
                writer.WriteToLog(transY.getAddress() + " - " + fab.address);
            }

            foreach (Fabricante fab in fabricantes)
            {
                System.Threading.Thread.Sleep(2000);
                double budg = transZ.getBudget(fab.address, order.getAddress());
                transGp = geocoder.GetGeoPointFromAddress(transZ.getAddress());
                fabGp = geocoder.GetGeoPointFromAddress(fab.address);

                double distance = geocoder.GetDrivingDistance(transGp, fabGp, clientGp);

                double footprint = distance * 0.26 / 1000;
                budg += fab.budget;
                budg += footprint;
                if (budg < low)
                {
                    low = budg;
                    myT.budget = low;
                    myT.id = "Z";
                    mypair.transp = myT;
                    mypair.fabricante = fab;
                    bestDistance = distance;
                    bestFootprint = footprint;
                    bestBudg = budg;
                }
                writer.WriteToLog("\t--");
                writer.WriteToLog("Z - " + fab.id + "\tdistancia: " + distance.ToString() + " footprint: " + footprint.ToString() + " budget: " + budg.ToString());
                writer.WriteToLog(transZ.getAddress() + " - " + fab.address);
            }
            writer.WriteToLog("\t--");
            writer.WriteToLog("Best pair: ");
            writer.WriteToLog("\t" + mypair.transp.id + " - " + mypair.fabricante.id);
            writer.WriteToLog("\tdistancia: " + bestDistance + " footprint: " + bestFootprint);
            writer.WriteToLog("\tbudget: " + bestBudg);
            
            return mypair;
        }
    }

    public class Fabricante
    {
        public string id; //"A", "B", "C"
        public string address {get; set;}
        public double budget {get; set;}
    }

    public class Transportadora
    {
        public string id; //"X", "Y", "Z"
        public string address {get; set;}
        public double budget { get; set; }
    }

    public class MyPair
    {
        public Fabricante fabricante { get; set; }
        public Transportadora transp { get; set; }
    }

    /*
     * O CD é representado pelas classes "Order" no backoffice, "Cd" nos fabricantes, "Package" nas transportadoras
     * */
    public class Order
    {
        public string fabricante;
        public int idInFab; //identificador do Cd no fabricante onde vai, foi ou está a ser gravado
        private string artist;
        private List<string> tracks = new List<string>();
        private double price_track = 0.9;
        private string address = "";
        public string clientUsername = "";
        public string status;
        public int id;

        public MyPair mypair;

        public Order()
        {
        }

        public void setArtist(string sArtist)
        {
            artist = sArtist;
        }

        public void addTrack(string title)
        {
            tracks.Add(title);
        }

        public void setPricePerTrack(float pptrack)
        {
            price_track = pptrack;
        }

        public string getArtist()
        {
            return artist;
        }

        public List<string> getTracks()
        {
            return tracks;
        }

        public double getTotalPrice()
        {
            double totalPrice = 0;
            
            foreach (string track in tracks)
            {
                totalPrice += price_track;
            }

            return totalPrice;
        }

        public void setAddress(string mAddress)
        {
            address = mAddress;
        }

        public string getAddress()
        {
            return address;
        }
    }
}
