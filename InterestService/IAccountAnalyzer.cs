using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace InterestService
{
    [ServiceContract]
    public interface IAccountAnalyzer
    {
        [OperationContract]
        [WebInvoke(Method = "GET",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat =WebMessageFormat.Json,
            UriTemplate = "GetAccountData/{accountID}/{accountBalance}")]
        IEnumerable<MonthlyInterest> GetAccountData(string accountID, string accountBalance);

        [OperationContract]
        [WebInvoke(Method = "GET",
                   RequestFormat = WebMessageFormat.Json,
                   ResponseFormat = WebMessageFormat.Json,
                   UriTemplate = "GetAccountData/{accountID}")]
        IEnumerable<ClaimsData> GetClaimsData(string accountID);
    }


    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    // You can add XSD files into the project. After building the project, you can directly use the data types defined there, with the namespace "InterestService.ContractType".
    [DataContract]
    public class MonthlyInterest
    {
        double total = 0;
        double rate = 0;
        string month = string.Empty;

        [DataMember]
        public string Month
        {
            get { return month; }
            set { month = value; }
        }

        [DataMember]
        public double Total { get => total; set => total = value; }

        [DataMember]
        public double Rate { get => rate; set => rate = value; }
    }


    [DataContract]
    public class ClaimsData
    {
        private double total = 0;
        private double pocket = 0;
        string month = string.Empty;

        [DataMember]
        public string Month
        {
            get { return month; }
            set { month = value; }
        }

        [DataMember]
        public double Total
        {
            get { return total; }
            set { total = value; }
        }

        [DataMember]
        public double Pocket
        {
            get { return pocket; }
            set { pocket = value; }
        }
    }
}
