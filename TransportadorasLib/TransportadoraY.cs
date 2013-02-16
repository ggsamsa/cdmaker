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
    public class TransportadoraY : Transportadora, ITransportadoraY
    {
        private double price;
        private int nrVehiclesAvailable = 1;

        public TransportadoraY()
        {
            name = "Y";
            address = "Rua Doutor Manuel Cardona, 6, 5000, Vila Real, Portugal";
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
