﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.296
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BackofficeServerLib.ServiceReference6 {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ServiceReference6.ITransportadoraZ")]
    public interface ITransportadoraZ {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITransportadoraZ/getBudget", ReplyAction="http://tempuri.org/ITransportadoraZ/getBudgetResponse")]
        float getBudget(string fabAddress, string clientAddress);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITransportadoraZ/getAddress", ReplyAction="http://tempuri.org/ITransportadoraZ/getAddressResponse")]
        string getAddress();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITransportadoraZ/sendPackage", ReplyAction="http://tempuri.org/ITransportadoraZ/sendPackageResponse")]
        void sendPackage(string fabAddress, string clientAddress, int idInFab);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ITransportadoraZChannel : BackofficeServerLib.ServiceReference6.ITransportadoraZ, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class TransportadoraZClient : System.ServiceModel.ClientBase<BackofficeServerLib.ServiceReference6.ITransportadoraZ>, BackofficeServerLib.ServiceReference6.ITransportadoraZ {
        
        public TransportadoraZClient() {
        }
        
        public TransportadoraZClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public TransportadoraZClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public TransportadoraZClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public TransportadoraZClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public float getBudget(string fabAddress, string clientAddress) {
            return base.Channel.getBudget(fabAddress, clientAddress);
        }
        
        public string getAddress() {
            return base.Channel.getAddress();
        }
        
        public void sendPackage(string fabAddress, string clientAddress, int idInFab) {
            base.Channel.sendPackage(fabAddress, clientAddress, idInFab);
        }
    }
}
