﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.296
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BackofficeServerLib.ServiceReference1 {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ServiceReference1.IFabricanteA")]
    public interface IFabricanteA {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IFabricanteA/getAddress", ReplyAction="http://tempuri.org/IFabricanteA/getAddressResponse")]
        string getAddress();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IFabricanteA/getBudget", ReplyAction="http://tempuri.org/IFabricanteA/getBudgetResponse")]
        double getBudget();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IFabricanteA/recordCd", ReplyAction="http://tempuri.org/IFabricanteA/recordCdResponse")]
        int recordCd(string artist, string[] tracks);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IFabricanteA/getCdStatus", ReplyAction="http://tempuri.org/IFabricanteA/getCdStatusResponse")]
        string getCdStatus(int id);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IFabricanteAChannel : BackofficeServerLib.ServiceReference1.IFabricanteA, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class FabricanteAClient : System.ServiceModel.ClientBase<BackofficeServerLib.ServiceReference1.IFabricanteA>, BackofficeServerLib.ServiceReference1.IFabricanteA {
        
        public FabricanteAClient() {
        }
        
        public FabricanteAClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public FabricanteAClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public FabricanteAClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public FabricanteAClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public string getAddress() {
            return base.Channel.getAddress();
        }
        
        public double getBudget() {
            return base.Channel.getBudget();
        }
        
        public int recordCd(string artist, string[] tracks) {
            return base.Channel.recordCd(artist, tracks);
        }
        
        public string getCdStatus(int id) {
            return base.Channel.getCdStatus(id);
        }
    }
}
