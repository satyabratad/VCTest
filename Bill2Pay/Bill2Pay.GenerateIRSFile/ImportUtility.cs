using Bill2Pay.Model;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.IO;
using System.Data;
using Bill2Pay.ExceptionLogger;
using ICSharpCode.SharpZipLib.Zip;
using ICSharpCode.SharpZipLib.Core;
using System.Web.Hosting;
using System.Transactions;
using System.Data.Entity.Core;


namespace Bill2Pay.GenerateIRSFile
{
    public class ImportUtility
    {
        int year;
        string fileName;
        long userId;

        public void ProcessInputFileAsync(int year, string fileName, long UserId)
        {
            this.year = year;
            this.fileName = fileName;
            this.userId = UserId;

            //Task.Factory.StartNew(this.ProcessInputFile);
            HostingEnvironment.QueueBackgroundWorkItem(clt => ProcessInputFile());
        }

        private void ProcessInputFile()
        {
            try
            {
                Logger.LogInstance.LogDebug("Import Started Path:{0}", fileName);
                var random = DateTime.Now.Ticks;
                ExtractZip(fileName, random);
                ReadCSV(fileName, random);
                ExecutePostImportDataProcessing(this.year, this.userId);
            }
            catch (EntityException ex)
            {
                Logger.LogInstance.LogError(ex.Message);
            }
            catch (Exception ex)
            {
                Logger.LogInstance.LogError(ex.StackTrace);
            }
        }

        private void ExtractZip(string fileName, long random)
        {
            var outPath = Path.Combine(Path.GetDirectoryName(fileName), random.ToString());
            ExtractZipFile(fileName, "", outPath);
            Logger.LogInstance.LogDebug("File UnZipped Path:{0}", outPath);
        }

        private void ExtractZipFile(string archiveFilenameIn, string password, string outFolder)
        {
            ZipFile zf = null;
            try
            {
                FileStream fs = File.OpenRead(archiveFilenameIn);
                zf = new ZipFile(fs);
                if (!String.IsNullOrEmpty(password))
                {
                    zf.Password = password;     // AES encrypted entries are handled automatically
                }
                foreach (ZipEntry zipEntry in zf)
                {
                    if (!zipEntry.IsFile)
                    {
                        continue;           // Ignore directories
                    }
                    String entryFileName = zipEntry.Name;
                    // to remove the folder from the entry:- entryFileName = Path.GetFileName(entryFileName);
                    // Optionally match entrynames against a selection list here to skip as desired.
                    // The unpacked length is available in the zipEntry.Size property.

                    byte[] buffer = new byte[4096];     // 4K is optimum
                    Stream zipStream = zf.GetInputStream(zipEntry);

                    // Manipulate the output filename here as desired.
                    String fullZipToPath = Path.Combine(outFolder, entryFileName);
                    string directoryName = Path.GetDirectoryName(fullZipToPath);
                    if (directoryName.Length > 0)
                        Directory.CreateDirectory(directoryName);

                    // Unzip file in buffered chunks. This is just as fast as unpacking to a buffer the full size
                    // of the file, but does not waste memory.
                    // The "using" will close the stream even if an exception occurs.
                    using (FileStream streamWriter = File.Create(fullZipToPath))
                    {
                        StreamUtils.Copy(zipStream, streamWriter, buffer);
                    }
                }
            }
            finally
            {
                if (zf != null)
                {
                    zf.IsStreamOwner = true; // Makes close also shut the underlying stream
                    zf.Close(); // Ensure we release resources
                }
            }
        }

        private void ExecutePostImportDataProcessing(int year, long UserId)
        {
            Logger.LogInstance.LogDebug("PostImportDataProcessing Starts year:'{0}' User:'{1}'", year, UserId);
            RawTransactionStaging.ExecutePostImportDataProcessing(year, UserId);
            Logger.LogInstance.LogDebug("PostImportDataProcessing Ends");
        }

        string[] line_records;
        private void ReadCSV(string filename, long random)
        {
            List<KeyValueCollection> result = new List<KeyValueCollection>();
            var outPath = Path.Combine(Path.GetDirectoryName(fileName), random.ToString());
            if (!Directory.Exists(outPath))
            {
                throw new DirectoryNotFoundException(outPath);
            }
            foreach (var file in Directory.GetFiles(outPath, "*.csv"))
            {
                filename = file;

                Logger.LogInstance.LogDebug("DB Import Started:{0}", filename);

                RawTransactionStaging.Clear();
                Logger.LogInstance.LogDebug("DB Old Staging data cleared");

                using (StreamReader sr = new StreamReader(filename))
                {
                    int iteration = 0;
                    while (sr.Peek() >= 0)
                    {
                        var fileLine = sr.ReadLine();
                        iteration++;
                        AddItemToStagingTable(fileLine, iteration);

                        if ((iteration % 5000) == 0)
                        {
                            RawTransactionStaging.AddBulk();
                            RawTransactionStaging.StagingTable.Rows.Clear();
                            Logger.LogInstance.LogDebug("DB Import Iteration:{0}", iteration);
                        }
                    }

                    RawTransactionStaging.AddBulk();
                    RawTransactionStaging.StagingTable.Rows.Clear();

                    // TODO REMOVE
                    //RawTransactionStaging.StagingList.Clear();

                    //while (sr.Peek() >= 0)
                    //{
                    //    var fileLine = sr.ReadLine();
                    //    iteration++;
                    //    AddItemToStaging(fileLine, iteration);

                    //    if ((iteration % 5000) == 0)
                    //    {
                    //        RawTransactionStaging.AddBulkAsync();
                    //        RawTransactionStaging.StagingList.Clear();
                    //        Logger.LogInstance.LogDebug("DB Import Iteration:{0}", iteration);
                    //    }

                    //}

                    //RawTransactionStaging.AddBulkAsync();
                    //RawTransactionStaging.StagingList.Clear();
                }

                Logger.LogInstance.LogDebug("DB Import Completed");
            }
        }

        private void AddItemToStagingTable(string fileLine, int iteration)
        {
            var line = string.Format("{0},{1}", iteration, fileLine);
            line_records = line.Split(',');

            if (line_records.Length != 13) // TODO FOR OTHER FIELDS
            {
                throw new Exception("Import File not in  correct format");
            }

            RawTransactionStaging.StagingTable.Rows.Add(line_records);
        }

        private void AddItemToStaging(string fileLine, int Id)
        {
            line_records = fileLine.Split(',');

            if (line_records.Length != 12) // TODO FOR OTHER FIELDS
            {
                throw new Exception("Import File not in  correct format");
            }

            RawTransactionStaging data = new RawTransactionStaging();
            data.Id = Id;
            data.PayeeAccountNumber = line_records[0];
            data.TransactionType = line_records[1];
            data.TransactionAmount = line_records[2];
            data.TransactionDate = line_records[3];
            //data.PayeeFirstName = line_records[4];
            //data.PayeeSecondName = line_records[5];
            //data.PayeeMailingAddress = line_records[6];
            //data.PayeeCity = line_records[7];
            //data.PayeeState = line_records[8];
            //data.PayeeZIP = line_records[9];
            //data.MCC = line_records[11];

            //data.FilerIndicatorType = line_records[];
            //data.PaymentIndicatorType = line_records[];
            //data.TINType = line_records[];
            //data.PayeeTIN = line_records[];
            //data.PayeeOfficeCode = line_records[];
            //data.CardPresentTransactions = line_records[];
            //data.FederalIncomeTaxWithheld = line_records[];
            //data.StateIncomeTaxWithheld = line_records[];

            RawTransactionStaging.StagingList.Add(data);
        }


    }

    public class KeyValue
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }

    public class KeyValueCollection : List<KeyValue>, ICloneable
    {
        public int ID { get; set; }

        public string this[string key]
        {
            get
            {
                var temp = this.FirstOrDefault(p => p.Key.Equals(key));
                if (temp == null)
                {
                    return string.Empty;
                }

                return temp.Value;
            }
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
