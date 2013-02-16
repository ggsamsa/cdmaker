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
    public class TransportadoraX : Transportadora, ITransportadoraX
    {
        //preco do transporte por KM
        private double price;

        public TransportadoraX()
        {
            name = "X";
            address = "Porto, Portugal";
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
