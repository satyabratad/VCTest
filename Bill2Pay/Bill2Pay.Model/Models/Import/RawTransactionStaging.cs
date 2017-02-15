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

namespace Bill2Pay.Model
{
    public class RawTransactionStaging : RawTransaction
    {
        [Key]
        public int Id { get; set; }

        public static void Clear()
        {
            //var existing = ApplicationDbContext.Instence.RawTransactionStaging.AsEnumerable<RawTransactionStaging>();
            //ApplicationDbContext.Instence.RawTransactionStaging.RemoveRange(existing);
            //ApplicationDbContext.Instence.SaveChanges();
            var result = ApplicationDbContext.Instence.Database
                .ExecuteSqlCommand("PreImportDataProcessing");

        }

        public static void ExecutePostImportDataProcessing(int year, string path, long userId)
        {
            var yearParam = new SqlParameter("@YEAR", year);
            var fileParam = new SqlParameter("@FileName", path);
            var userParam = new SqlParameter("@UserId", userId);

            var result = ApplicationDbContext.Instence.Database
                .ExecuteSqlCommand("PostImportDataProcessing @YEAR, @FileName, @UserId", yearParam, fileParam, userParam);
               
        }

        public static void AddBulkAsync()
        {
            //using(ApplicationDbContext context = new Model.ApplicationDbContext())
            //{
            //    context.RawTransactionStaging.AddRange(list);
            //    context.SaveChanges();
            //}
            if (ApplicationDbContext.Instence.Database.Connection.State != ConnectionState.Open)
            {
                ApplicationDbContext.Instence.Database.Connection.Open();
            }
            ApplicationDbContext.Instence.RawTransactionStaging.AddRange(list);
            ApplicationDbContext.Instence.SaveChanges();
        }

        static List<RawTransactionStaging> list = null;
        public static List<RawTransactionStaging> StagingList
        {
            get
            {
                if(list == null)
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
                if(dt == null)
                {
                    dt = new DataTable();
                    dt.Columns.Add("Id");
                    dt.Columns.Add("PayeeAccountNumber");
                    dt.Columns.Add("TransactionType");
                    dt.Columns.Add("TransactionAmount");
                    dt.Columns.Add("TransactionDate");

                    dt.Columns.Add("PayeeFirstName");
                    dt.Columns.Add("PayeeSecondName");
                    dt.Columns.Add("PayeeMailingAddress");
                    dt.Columns.Add("PayeeCity");
                    dt.Columns.Add("PayeeState");
                    dt.Columns.Add("PayeeZIP");
                    

                    dt.Columns.Add("FilerIndicatorType");

                    dt.Columns.Add("MCC");

                    dt.Columns.Add("PaymentIndicatorType");
                    dt.Columns.Add("TINType");
                    dt.Columns.Add("PayeeTIN");
                    dt.Columns.Add("PayeeOfficeCode");
                    dt.Columns.Add("CardPresentTransactions");
                    dt.Columns.Add("FederalIncomeTaxWithheld");
                    dt.Columns.Add("StateIncomeTaxWithheld");

                }

                return dt;
            }
        }

        public static void AddBulk()
        {
            using (SqlConnection dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                dbConnection.Open();
                using (SqlBulkCopy s = new SqlBulkCopy(dbConnection))
                {
                    s.DestinationTableName = "dbo.RawTransactionStagings";
                    foreach (var column in dt.Columns)
                        s.ColumnMappings.Add(column.ToString(), column.ToString());
                    s.WriteToServer(dt);
                }
            }
        }
    }
}
