using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace FabricantesLib
{
    [ServiceContract]
    public interface IFabricanteB
    {
        [OperationContract]
        string getAddress();
        [OperationContract]
        double getBudget();
        [OperationContract]
        int recordCd(string artist, List<string> tracks);
        [OperationContract]
        string getCdStatus(int id);
    }
}
