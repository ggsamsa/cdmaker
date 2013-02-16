using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using TransportadorasLib.ServiceReference1;

namespace TransportadorasLib
{
    public class Transportadora
    {
        //preco do transporte por KM
        private double price;

        public string address;

        private int nrVehiclesAvailable = 1;

        //Fila de Encomendas a enviar
        private Queue<Package> packages = new Queue<Package>();
        public bool pkgSenderActive = false;
        private List<Package> sentPackages = new List<Package>();

        private int pk_ids = 0;

        public string name;

        public LogWriter writer = LogWriter.Instance;

        public void sendPackage(string fabAddress, string clientAddress, int idInFab)
        {
            writer.WriteToLog(this.name + " | New request to deliver received");
            Package package = new Package();
            GeoCoder gc = new GeoCoder();
            GeoPoint fabGp;
            GeoPoint clientGp;
            GeoPoint myGp;

            myGp = gc.GetGeoPointFromAddress(this.address);
            fabGp = gc.GetGeoPointFromAddress(fabAddress);
            clientGp = gc.GetGeoPointFromAddress(clientAddress);
            pk_ids += 1;
            package.pk_id = pk_ids;
            package.fabAddress = fabAddress;
            package.clientAddress = clientAddress;
            package.status = "pre-deliver";
            writer.WriteToLog(this.name + " | 0");

            package.distance1 = gc.GetDrivingDistance(myGp, fabGp);
            System.Threading.Thread.Sleep(1000);
            package.distance2 = gc.GetDrivingDistance(fabGp, clientGp);
            System.Threading.Thread.Sleep(1000);
            package.distance3 = gc.GetDrivingDistance(clientGp, myGp);
        
            packages.Enqueue(package);
            package.idInFab = idInFab;

            writer.WriteToLog(this.name + " | New package to be delivered. ID given: " + package.pk_id);
            writer.WriteToLog(this.name + " | From address: " + package.fabAddress);
            writer.WriteToLog(this.name + " | To address: " + package.clientAddress);
            writer.WriteToLog(this.name + " | Item ID in Fabricante: " + package.idInFab);
        }

        //thread que verifica constantemente se há encomendas por tratar. se houver carros disponiveis, sao tratadas
        public void packageSender()
        {
            while (true)
            {
                if (packages.Count > 0 && nrVehiclesAvailable > 0)
                {
                    Package package = packages.Dequeue();

                    package.status = "pre-deliver";

                    writer.WriteToLog(this.name + " | A car left to send package ID " + package.pk_id);
                    writer.WriteToLog(this.name + " | Distance to Fabricante: " + package.distance1);
                    nrVehiclesAvailable -= 1;
                    writer.WriteToLog(this.name + " | Number of vehicles available: " + nrVehiclesAvailable);
                    
                    int time = (int)package.distance1 * 250;
                    System.Threading.Thread.Sleep(time);

                    writer.WriteToLog(this.name + " | The car car arrived at Fabricante and picked up package ID: " + package.pk_id);
                    writer.WriteToLog(this.name + " | Distance to client: " + package.distance2);
                    BackofficeServiceClient boClient = new BackofficeServiceClient();
                    boClient.setOrderStatus(package.idInFab, "Delivering");

                    package.status = "delivering";
                    
                    time = (int)package.distance2 * 250;
                    System.Threading.Thread.Sleep(time);

                    writer.WriteToLog(this.name + " | Package ID " + package.pk_id + " delivered");
                    writer.WriteToLog(this.name + " | Distance home: " + package.distance3);
                    boClient.setOrderStatus(package.idInFab, "Delivered");

                    package.status = "post-deliver";
                    sentPackages.Add(package);

                    time = (int)package.distance3 * 250;
                    System.Threading.Thread.Sleep(time);

                    writer.WriteToLog(this.name + " | The car that picked up package " + package.pk_id + " came back");
                    nrVehiclesAvailable += 1;
                    writer.WriteToLog(this.name + " | Number of vehicles available: " + nrVehiclesAvailable);
                }
            }
        }

        public float getBudget(string fabAddress, string clientAddress)
        {
            price = CostManager.Instance.getCostPerKm();

            GeoCoder geocoder = new GeoCoder();

            GeoPoint fabGp = new GeoPoint();
            GeoPoint clientGp = new GeoPoint();
            GeoPoint myGp = new GeoPoint();

            fabGp = geocoder.GetGeoPointFromAddress(fabAddress);
            clientGp = geocoder.GetGeoPointFromAddress(clientAddress);
            myGp = geocoder.GetGeoPointFromAddress(address);

            double distance = 0;
            distance = geocoder.GetDrivingDistance(myGp, fabGp);

            distance += geocoder.GetDrivingDistance(fabGp, clientGp);
            float budg = (float)distance / 1000 * (float)price;
            writer.WriteToLog(this.name + " | Backoffice requested a budget for the following trip:"); 
            writer.WriteToLog(this.name + " | " + fabAddress + " - " + clientAddress);
            writer.WriteToLog(this.name + " | My reply was: " + budg.ToString());
            return budg;
        }
     }

    /**
     * Estrutura da encomenda que vai conter o CD.
     * Tem 4 estados.
     * "Queued"
     * Transportadora - Fabricante = "pre-deliver"
     * Fabricante - Cliente = "delivering"
     * Cliente - Transportadora - "post-deliver" - dado util para a transportadora
     * 
     * TODO: depois da simulacao estar a funcionar correctamente com timers, tentar fazer com que
     * a chegada do carro da transportadora ao fabricante coincida com o fim da gravaçáo do CD.
     * TODO: informar o backoffice do estado das packages
     * */
    public class Package
    {
        public int pk_id;
        public int idInFab;
        public string fabAddress;
        public string clientAddress;
        public string status;
        public double distance1;
        public double distance2;
        public double distance3;
    }

}


