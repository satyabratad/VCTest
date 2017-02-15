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
                .ExecuteSqlCommand("PostImportDataProcessing @YEAR @FileName @UserId", yearParam, fileParam, userParam);
               
        }

        public static void AddBulkAsync()
        {
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
    }
}
