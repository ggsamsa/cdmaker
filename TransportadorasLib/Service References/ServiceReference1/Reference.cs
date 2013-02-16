﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.296
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TransportadorasLib.ServiceReference1 {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ServiceReference1.IBackofficeService")]
    public interface IBackofficeService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IBackofficeService/getTracksFromLastfm", ReplyAction="http://tempuri.org/IBackofficeService/getTracksFromLastfmResponse")]
        string[] getTracksFromLastfm(string artist);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IBackofficeService/getSimilarNamed", ReplyAction="http://tempuri.org/IBackofficeService/getSimilarNamedResponse")]
        string[] getSimilarNamed(string artist);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IBackofficeService/setArtistToOrder", ReplyAction="http://tempuri.org/IBackofficeService/setArtistToOrderResponse")]
        void setArtistToOrder(string artist);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IBackofficeService/addTrackToOrder", ReplyAction="http://tempuri.org/IBackofficeService/addTrackToOrderResponse")]
        void addTrackToOrder(string title);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IBackofficeService/getTotalPrice", ReplyAction="http://tempuri.org/IBackofficeService/getTotalPriceResponse")]
        float getTotalPrice();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IBackofficeService/getTracksFromOrder", ReplyAction="http://tempuri.org/IBackofficeService/getTracksFromOrderResponse")]
        string[] getTracksFromOrder();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IBackofficeService/setAddress", ReplyAction="http://tempuri.org/IBackofficeService/setAddressResponse")]
        void setAddress(string address);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IBackofficeService/commitOrder", ReplyAction="http://tempuri.org/IBackofficeService/commitOrderResponse")]
        void commitOrder();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IBackofficeService/setOrderStatus", ReplyAction="http://tempuri.org/IBackofficeService/setOrderStatusResponse")]
        void setOrderStatus(int idFab, string status);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IBackofficeService/setClientUsername", ReplyAction="http://tempuri.org/IBackofficeService/setClientUsernameResponse")]
        void setClientUsername(string username);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IBackofficeService/getOrdersStatus", ReplyAction="http://tempuri.org/IBackofficeService/getOrdersStatusResponse")]
        string[] getOrdersStatus(string username);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IBackofficeServiceChannel : TransportadorasLib.ServiceReference1.IBackofficeService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class BackofficeServiceClient : System.ServiceModel.ClientBase<TransportadorasLib.ServiceReference1.IBackofficeService>, TransportadorasLib.ServiceReference1.IBackofficeService {
        
        public BackofficeServiceClient() {
        }
        
        public BackofficeServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public BackofficeServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public BackofficeServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public BackofficeServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public string[] getTracksFromLastfm(string artist) {
            return base.Channel.getTracksFromLastfm(artist);
        }
        
        public string[] getSimilarNamed(string artist) {
            return base.Channel.getSimilarNamed(artist);
        }
        
        public void setArtistToOrder(string artist) {
            base.Channel.setArtistToOrder(artist);
        }
        
        public void addTrackToOrder(string title) {
            base.Channel.addTrackToOrder(title);
        }
        
        public float getTotalPrice() {
            return base.Channel.getTotalPrice();
        }
        
        public string[] getTracksFromOrder() {
            return base.Channel.getTracksFromOrder();
        }
        
        public void setAddress(string address) {
            base.Channel.setAddress(address);
        }
        
        public void commitOrder() {
            base.Channel.commitOrder();
        }
        
        public void setOrderStatus(int idFab, string status) {
            base.Channel.setOrderStatus(idFab, status);
        }
        
        public void setClientUsername(string username) {
            base.Channel.setClientUsername(username);
        }
        
        public string[] getOrdersStatus(string username) {
            return base.Channel.getOrdersStatus(username);
        }
    }
}
