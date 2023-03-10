//------------------------------------------------------------------------------
// <auto-generated>
//     Ten kod został wygenerowany przez narzędzie.
//     Wersja wykonawcza:4.0.30319.42000
//
//     Zmiany w tym pliku mogą spowodować nieprawidłowe zachowanie i zostaną utracone, jeśli
//     kod zostanie ponownie wygenerowany.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BillsServiceClient.BillsService {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="BillsService.IBillsService")]
    public interface IBillsService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IBillsService/Hello", ReplyAction="http://tempuri.org/IBillsService/HelloResponse")]
        string Hello();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IBillsService/Hello", ReplyAction="http://tempuri.org/IBillsService/HelloResponse")]
        System.Threading.Tasks.Task<string> HelloAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IBillsService/GetBills", ReplyAction="http://tempuri.org/IBillsService/GetBillsResponse")]
        string GetBills(int userId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IBillsService/GetBills", ReplyAction="http://tempuri.org/IBillsService/GetBillsResponse")]
        System.Threading.Tasks.Task<string> GetBillsAsync(int userId);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IBillsServiceChannel : IBillsService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class BillsServiceClient : System.ServiceModel.ClientBase<IBillsService>, IBillsService {
        
        public BillsServiceClient() {
        }
        
        public BillsServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public BillsServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public BillsServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public BillsServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public string Hello() {
            return base.Channel.Hello();
        }
        
        public System.Threading.Tasks.Task<string> HelloAsync() {
            return base.Channel.HelloAsync();
        }
        
        public string GetBills(int userId) {
            return base.Channel.GetBills(userId);
        }
        
        public System.Threading.Tasks.Task<string> GetBillsAsync(int userId) {
            return base.Channel.GetBillsAsync(userId);
        }
    }
}
