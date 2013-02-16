using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading;

namespace TransportadorasLib
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class TransportadoraZ : Transportadora, ITransportadoraZ
    {
        private double price;
        private int nrVehiclesAvailable = 1;

        public TransportadoraZ()
        {
            name = "Z";
            address = "Rua da Graça, 31, 7090-244, Viana do Alentejo, Portugal";
            if (!pkgSenderActive)
            {
                Thread t = new Thread(packageSender);
                t.Start();
                pkgSenderActive = true;
            }
        }

        public string getAddress()
        {
            return address;
        }
    }
}
