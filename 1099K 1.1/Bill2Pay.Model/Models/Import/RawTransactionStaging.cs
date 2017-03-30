using Bill2Pay.ExceptionLogger;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Bill2Pay.Model
{
    /// <summary>
    /// Temporary storage of raw transaction, before type custing.
    /// </summary>
    public class RawTransactionStaging
    {
        /// <summary>
        /// Database identity key
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Payee Account Number
        /// </summary>
        [MaxLength(20)]
        public string PayeeAccountNumber { get; set; }

        /// <summary>
        /// Transaction Type
        /// </summary>
        [MaxLength(3)]
        public string TransactionType { get; set; }

        /// <summary>
        /// Transaction amount
        /// </summary>
        [MaxLength(20)]
        public string TransactionAmount { get; set; }

        /// <summary>
        /// Transaction date
        /// </summary>
        [MaxLength(32)]
        public string TransactionDate { get; set; }

        /// <summary>
        /// Clear all record
        /// </summary>
        public static void Clear()
        {
            using(var dbContext = new ApplicationDbContext())
            {
                var result = dbContext.Database
                .ExecuteSqlCommand("PreImportDataProcessing");
            }
        }

        /// <summary>
        /// Asynchronus Bulk Addition
        /// </summary>
        public static void AddBulkAsync()
        {
            using (var dbContext = new ApplicationDbContext())
            {
                dbContext.RawTransactionStaging.AddRange(list);
                dbContext.SaveChanges();
            }
        }

        static List<RawTransactionStaging> list = null;

        /// <summary>
        /// List of entity
        /// </summary>
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

        /// <summary>
        /// Tabular form of entity
        /// </summary>
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

        /// <summary>
        /// Database timeout 
        /// </summary>
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

        /// <summary>
        /// Bulk Addition
        /// </summary>
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

        /// <summary>
        /// Post Import data processing
        /// </summary>
        /// <param name="year">Year</param>
        /// <param name="userId">User</param>
        /// <param name="fileName">CSV file name</param>
        /// <param name="totalCount">total record count</param>
        /// <param name="payerId">Payer</param>
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
