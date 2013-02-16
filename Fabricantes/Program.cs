using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FabricantesLib;
using System.ServiceModel;
using System.ServiceModel.Description;

namespace Fabricantes
{
    class Program
    {
        static void Main(string[] args)
        {
            Uri addrA = new Uri("http://localhost:2101/");
            ServiceHost selfHostA = new ServiceHost(typeof(FabricanteA), addrA);
            Uri addrB = new Uri("http://localhost:2102/");
            ServiceHost selfHostB = new ServiceHost(typeof(FabricanteB), addrB);
            Uri addrC = new Uri("http://localhost:2103/");
            ServiceHost selfHostC = new ServiceHost(typeof(FabricanteC), addrC);

            try
            {
                selfHostA.AddServiceEndpoint(typeof(IFabricanteA), new WSHttpBinding(), "FabricanteA");
                selfHostB.AddServiceEndpoint(typeof(IFabricanteB), new WSHttpBinding(), "FabricanteB");
                selfHostC.AddServiceEndpoint(typeof(IFabricanteC), new WSHttpBinding(), "FabricanteC");

                ServiceMetadataBehavior smb = new ServiceMetadataBehavior();
                smb.HttpGetEnabled = true;
                
                selfHostA.Description.Behaviors.Add(smb);
                selfHostB.Description.Behaviors.Add(smb);
                selfHostC.Description.Behaviors.Add(smb);

                selfHostA.Open();
                Console.WriteLine("Fabricante A started");
                selfHostB.Open();
                Console.WriteLine("Fabricante B started");
                selfHostC.Open();
                Console.WriteLine("Fabricante C started");
                
                Console.WriteLine("Press ENTER to terminate Fabricantes services.");
                Console.WriteLine();
                Console.ReadLine();

                selfHostA.Close();
                selfHostB.Close();
                selfHostC.Close();
            }
            catch (CommunicationException ex)
            {
                Console.WriteLine("Exception: {0}", ex.Message);
                selfHostA.Abort();
                selfHostB.Abort();
                selfHostC.Abort();
            }
        }
    }
}
