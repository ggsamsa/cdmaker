﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.296
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BackofficeServerLib.ServiceReference2 {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ServiceReference2.IFabricanteB")]
    public interface IFabricanteB {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IFabricanteB/getAddress", ReplyAction="http://tempuri.org/IFabricanteB/getAddressResponse")]
        string getAddress();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IFabricanteB/getBudget", ReplyAction="http://tempuri.org/IFabricanteB/getBudgetResponse")]
        double getBudget();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IFabricanteB/recordCd", ReplyAction="http://tempuri.org/IFabricanteB/recordCdResponse")]
        int recordCd(string artist, string[] tracks);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IFabricanteB/getCdStatus", ReplyAction="http://tempuri.org/IFabricanteB/getCdStatusResponse")]
        string getCdStatus(int id);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IFabricanteBChannel : BackofficeServerLib.ServiceReference2.IFabricanteB, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class FabricanteBClient : System.ServiceModel.ClientBase<BackofficeServerLib.ServiceReference2.IFabricanteB>, BackofficeServerLib.ServiceReference2.IFabricanteB {
        
        public FabricanteBClient() {
        }
        
        public FabricanteBClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public FabricanteBClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public FabricanteBClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public FabricanteBClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
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
