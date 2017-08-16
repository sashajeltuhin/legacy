using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Apprenda.Services.Logging;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace InterestService
{
  
    public class AccountAnalyzer : IAccountAnalyzer
    {
        private static readonly ILogger Log = LogManager.Instance().GetLogger(typeof(AccountAnalyzer));

        public IEnumerable<MonthlyInterest> GetAccountData(string accountID, string accountBalance)
        {
            
            double[] arr = new double[12] { 0.01, 0.013, 0.013, 0.013, 0.013, 0.011, 0.013, 0.02, 0.01, 0.0129, 0.0131, 0.011 };
            List<MonthlyInterest> list = new List<MonthlyInterest>();
            try
            {
                double balance = double.Parse(accountBalance);
                double cum = balance;
                for (int i = 0; i < arr.Length; i++)
                {
                    MonthlyInterest mi = new MonthlyInterest();
                    mi.Month = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(i + 1);
                    mi.Rate = arr[i];
                    cum += cum * arr[i];
                    mi.Total = cum;
                    list.Add(mi);
                }
                Log.Log(string.Format("Account {0} successfully processed", accountID), LogLevel.Info);
            }
            catch(Exception ex)
            {
                Log.Log(string.Format("Error analyzing the account {0}. {1}", accountID, ex.Message), LogLevel.Error);
            }
            return list;
        }
    }
}
