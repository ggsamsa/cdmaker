﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.296
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BackofficeServerLib.ServiceReference5 {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ServiceReference5.ITransportadoraY")]
    public interface ITransportadoraY {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITransportadoraY/getBudget", ReplyAction="http://tempuri.org/ITransportadoraY/getBudgetResponse")]
        float getBudget(string fabAddress, string clientAddress);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITransportadoraY/getAddress", ReplyAction="http://tempuri.org/ITransportadoraY/getAddressResponse")]
        string getAddress();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITransportadoraY/sendPackage", ReplyAction="http://tempuri.org/ITransportadoraY/sendPackageResponse")]
        void sendPackage(string fabAddress, string clientAddress, int idInFab);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ITransportadoraYChannel : BackofficeServerLib.ServiceReference5.ITransportadoraY, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class TransportadoraYClient : System.ServiceModel.ClientBase<BackofficeServerLib.ServiceReference5.ITransportadoraY>, BackofficeServerLib.ServiceReference5.ITransportadoraY {
        
        public TransportadoraYClient() {
        }
        
        public TransportadoraYClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public TransportadoraYClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public TransportadoraYClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public TransportadoraYClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
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
