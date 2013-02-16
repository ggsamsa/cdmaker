using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace FabricantesLib
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IFabricanteC" in both code and config file together.
    [ServiceContract]
    public interface IFabricanteC
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
