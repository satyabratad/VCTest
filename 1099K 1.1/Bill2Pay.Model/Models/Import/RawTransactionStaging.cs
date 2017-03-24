using Bill2Pay.ExceptionLogger;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Bill2Pay.Model
{
    public class RawTransactionStaging
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(20)]
        public string PayeeAccountNumber { get; set; }

        [MaxLength(3)]
        public string TransactionType { get; set; }

        [MaxLength(20)]
        public string TransactionAmount { get; set; }

        [MaxLength(32)]
        public string TransactionDate { get; set; }

        

        public static void Clear()
        {
            using(var dbContext = new ApplicationDbContext())
            {
                var result = dbContext.Database
                .ExecuteSqlCommand("PreImportDataProcessing");
            }
        }



        public static void AddBulkAsync()
        {
            using (var dbContext = new ApplicationDbContext())
            {
                dbContext.RawTransactionStaging.AddRange(list);
                dbContext.SaveChanges();
            }
        }

        static List<RawTransactionStaging> list = null;
        public static List<RawTransactionStaging> StagingList
        {
            get
            {
                if (list == null)
                {
                    list = new List<RawTransactionStaging>();
                }

                return list;
            }
        }

        static DataTable dt;
        public static DataTable StagingTable
        {
            get
            {
                if (dt == null)
                {
                    dt = new DataTable();
                    dt.Columns.Add("Id");
                    dt.Columns.Add("PayeeAccountNumber");
                    dt.Columns.Add("TransactionType");
                    dt.Columns.Add("TransactionAmount");
                    dt.Columns.Add("TransactionDate");
                }

                return dt;
            }
        }

        public static int? ProcessTimeOut
        {
            get
            {
                var timeout = 5 * 60;// Five Minute
                if (ConfigurationManager.AppSettings["ProcessTimeOut"] != null)
                {
                    timeout = Convert.ToInt32(ConfigurationManager.AppSettings["ProcessTimeOut"]) * 60;
                }
                return timeout;
            }
        }

        public static void AddBulk()
        {
            using (SqlConnection dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                dbConnection.Open();
                using (SqlBulkCopy s = new SqlBulkCopy(dbConnection))
                {
                    s.DestinationTableName = "RawTransactionStagings";
                    foreach (var column in dt.Columns)
                        s.ColumnMappings.Add(column.ToString(), column.ToString());
                    s.WriteToServer(dt);
                }
            }
        }

        public static void ExecutePostImportDataProcessing(int year, long userId, string fileName, int totalCount,int payerId)
        {
            try
            {
                using (var dbContext = new ApplicationDbContext())
                {
                    var yearParam = new SqlParameter("@YEAR", year);
                    var userParam = new SqlParameter("@UserId", userId);
                    var totalParam = new SqlParameter("@TotalCount", totalCount);
                    var fileParam = new SqlParameter("@FileName", fileName);
                    var payerParam = new SqlParameter("@PayerId", payerId);

                    dbContext.Database.CommandTimeout = RawTransactionStaging.ProcessTimeOut;

                    var result = dbContext.Database
                        .ExecuteSqlCommand("PostImportDataProcessing @YEAR, @UserId, @TotalCount, @FileName,@PayerId", yearParam, userParam, totalParam, fileParam, payerParam);
                }
            }
            catch (Exception ex)
            {
                Logger.LogInstance.LogError(ex.Message);
            }

        }
    }
}
