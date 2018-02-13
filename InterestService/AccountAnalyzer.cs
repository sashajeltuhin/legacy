using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Apprenda.Services.Logging;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Data.Common;
using System.Configuration;

namespace AccountService
{
  
    public class AccountAnalyzer : IAccountAnalyzer
    {
        private static readonly ILogger Log = LogManager.Instance().GetLogger(typeof(AccountAnalyzer));

        public IEnumerable<MonthlyInterest> GetAccountData(string accountID, string accountBalance)
        {
            
            double[] arr = LoadData(3); //new double[12] { 0.01, 0.013, 0.013, 0.013, 0.013, 0.011, 0.013, 0.02, 0.01, 0.0129, 0.0131, 0.011 };
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

        public IEnumerable<ClaimsData> GetClaimsData(string accountID)
        {

            double[] totalClaim = LoadData(1);//new double[12] { 350, 245, 70, 0, 0, 156, 0, 876, 0, 240, 432, 0 };
            double[] pocket = LoadData(2);//new double[12] { 80, 190, 70, 0, 0, 25, 0, 453, 0, 45, 187, 0 };
            List<ClaimsData> list = new List<ClaimsData>();
            try
            {
                for (int i = 0; i < totalClaim.Length; i++)
                {
                    ClaimsData data = new ClaimsData();
                    data.Month = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(i + 1);
                    data.Total = totalClaim[i];
                    data.Pocket = pocket[i];
                    list.Add(data);
                }
                Log.Log(string.Format("Claims for account {0} successfully processed", accountID), LogLevel.Info);
            }
            catch (Exception ex)
            {
                Log.Log(string.Format("Error getting claims for the account {0}. {1}", accountID, ex.Message), LogLevel.Error);
            }
            return list;
        }

        private double [] LoadData(int type)
        {
           
            double[] data = new double[12];
            string[] fields = new string[12] {"jan", "feb", "mar", "apr", "may", "jun", "jul", "aug", "sep", "oct", "nov", "dec" };
            ConnectionStringSettings connectionSetting = ConfigurationManager.ConnectionStrings["AccountDB"];
            if (connectionSetting == null)
            {
                throw new Exception("Connection string AccountDB is not found in the configuration file");
            }
            try
            {
                DbProviderFactory providerFactory =
           DbProviderFactories.GetFactory(connectionSetting.ProviderName);
                using (DbConnection conn = providerFactory.CreateConnection())
                {
                    conn.ConnectionString = connectionSetting.ConnectionString;
                    DbCommand command = conn.CreateCommand();
                    command.CommandText =
                   "select " + string.Join(",", fields) + $" from [AccountData] where [tupleType] = {type}";
                    Log.Log(string.Format("Executing query with connection string {0}. {1}", command.CommandText, connectionSetting.ConnectionString), LogLevel.Info);
                    conn.Open();
                    using (DbDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            for (int i = 0; i < 12; i++)
                            {
                                data[i] = (double)reader[fields[i]];                            
                            }
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                Log.Log(string.Format("Issues loading data for account type {0}. Reason: {1}", type, ex), LogLevel.Error);
            }
            return data;
        }
    }
}
