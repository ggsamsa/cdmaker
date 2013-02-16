using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.ServiceModel.Description;
using BackofficeServerLib;

namespace BackofficeServer
{
    class Program
    {
        static void Main(string[] args)
        {
            Uri addr = new Uri("http://localhost:2100/");
            ServiceHost selfHost = new ServiceHost(typeof(BackofficeService), addr);

            try
            {
                selfHost.AddServiceEndpoint(typeof(IBackofficeService), new WSHttpBinding(), "BackofficeService");

                ServiceMetadataBehavior smb = new ServiceMetadataBehavior();
                smb.HttpGetEnabled = true;
                selfHost.Description.Behaviors.Add(smb);

                selfHost.Open();

                Console.WriteLine("Service started");
                Console.WriteLine("Press ENTER to terminate the Backoffice.");
                Console.WriteLine();
                Console.ReadLine();

                selfHost.Close();
            }
            catch (CommunicationException ex)
            {
                Console.WriteLine("Exception: {0}", ex.Message);
                selfHost.Abort();
            }
        }
    }
}
