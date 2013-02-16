using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace TransportadorasLib
{
    [ServiceContract]
    public interface ITransportadoraZ
    {
        [OperationContract]
        float getBudget(string fabAddress, string clientAddress);
        [OperationContract]
        string getAddress();
        [OperationContract]
        void sendPackage(string fabAddress, string clientAddress, int idInFab);
    }
}
