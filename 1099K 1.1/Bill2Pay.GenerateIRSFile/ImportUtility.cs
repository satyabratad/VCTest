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
        int payerId;

        public void ProcessInputFileAsync(int year, string fileName, long UserId,int PayerId)
        {
            this.year = year;
            this.fileName = fileName;
            this.userId = UserId;
            this.payerId = PayerId;

            HostingEnvironment.QueueBackgroundWorkItem(clt => ProcessInputFile());
        }

        private void ProcessInputFile()
        {
            try
            {
                Logger.LogInstance.LogInfo("Import Started Path:{0}", fileName);
                var random = DateTime.Now.Ticks;
                ExtractZip(fileName, random);
                ProcessCSV(fileName, random);

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
            Logger.LogInstance.LogInfo("File UnZipped Path:{0}", outPath);
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

        string[] line_records;
        private void ProcessCSV(string filename, long random)
        {
            var outPath = Path.Combine(Path.GetDirectoryName(fileName), random.ToString());
            if (!Directory.Exists(outPath))
            {
                throw new DirectoryNotFoundException(outPath);
            }
            foreach (var file in Directory.GetFiles(outPath, "*.csv"))
            {
                filename = file;

                Logger.LogInstance.LogInfo("DB Import Started:{0}", filename);

                RawTransactionStaging.Clear();
                Logger.LogInstance.LogInfo("DB Old Staging data cleared");

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

                    var fName = Path.GetFileName(filename);
                    ExecutePostImportDataProcessing(this.year, this.userId, fName, iteration,this.payerId);
                }

                Logger.LogInstance.LogInfo("DB Import Completed");
            }
        }

        public static void CreateZip(string rootpath)
        {
            //TODO
        }

        private void ExecutePostImportDataProcessing(int year, long userId, string fileName, int totalCount,int payerId)
        {
            Logger.LogInstance.LogInfo("PostImportDataProcessing Starts year:'{0}' User:'{1}' FileName: '{2}' Total Count: {3}", year, userId, fileName, totalCount);
            RawTransactionStaging.ExecutePostImportDataProcessing(year, userId, fileName, totalCount, payerId);
            Logger.LogInstance.LogInfo("PostImportDataProcessing Ends");
        }

        private void AddItemToStagingTable(string fileLine, int iteration)
        {
            var line = string.Format("{0},{1}", iteration, fileLine);
            line_records = line.Split(',');

            if (line_records.Length != 5)
            {
                throw new Exception("Import File not in  correct format");
            }

            RawTransactionStaging.StagingTable.Rows.Add(line_records);
        }
    }
}
