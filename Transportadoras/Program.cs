using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TransportadorasLib;
using System.ServiceModel;
using System.ServiceModel.Description;

namespace Transportadoras
{
    class Program
    {
        static void Main(string[] args)
        {
            Uri addrX = new Uri("http://localhost:2105/");
            ServiceHost selfHostX = new ServiceHost(typeof(TransportadoraX), addrX);
            Uri addrY = new Uri("http://localhost:2106/");
            ServiceHost selfHostY = new ServiceHost(typeof(TransportadoraY), addrY);
            Uri addrZ = new Uri("http://localhost:2107/");
            ServiceHost selfHostZ = new ServiceHost(typeof(TransportadoraZ), addrZ);

            try
            {
                selfHostX.AddServiceEndpoint(typeof(ITransportadoraX), new WSHttpBinding(), "TransportadoraX");
                selfHostY.AddServiceEndpoint(typeof(ITransportadoraY), new WSHttpBinding(), "TransportadoraY");
                selfHostZ.AddServiceEndpoint(typeof(ITransportadoraZ), new WSHttpBinding(), "TransportadoraZ");

                ServiceMetadataBehavior smb = new ServiceMetadataBehavior();
                smb.HttpGetEnabled = true;

                selfHostX.Description.Behaviors.Add(smb);
                selfHostY.Description.Behaviors.Add(smb);
                selfHostZ.Description.Behaviors.Add(smb);

                selfHostX.Open();
                Console.WriteLine("Transportadora X started");
                selfHostY.Open();
                Console.WriteLine("Transportadora Y started");
                selfHostZ.Open();
                Console.WriteLine("Transportadora Z started");

                Console.WriteLine("Press ENTER to terminate Transportadoras services.");
                Console.WriteLine();
                Console.ReadLine();

                selfHostX.Close();
                selfHostY.Close();
                selfHostZ.Close();
            }
            catch (CommunicationException ex)
            {
                Console.WriteLine("Exception: {0}", ex.Message);
                selfHostX.Abort();
                selfHostY.Abort();
                selfHostZ.Abort();
            }
        }
    }
}
