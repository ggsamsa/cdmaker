using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace BackofficeServerLib
{
    [ServiceContract]
    public interface IBackofficeService
    {
        [OperationContract]
        string[] getTracksFromLastfm(string artist);
        [OperationContract]
        string[] getSimilarNamed(string artist);
        [OperationContract]
        void setArtistToOrder(string artist);
        [OperationContract]
        void addTrackToOrder(string title);
        [OperationContract]
        float getTotalPrice();
        [OperationContract]
        List<string> getTracksFromOrder();
        [OperationContract]
        void setAddress(string address);
        [OperationContract]
        void commitOrder();
        [OperationContract]
        void setOrderStatus(int idFab, string status);
        [OperationContract]
        void setClientUsername(string username);
        [OperationContract]
        List<string> getOrdersStatus(string username);
     }
}
