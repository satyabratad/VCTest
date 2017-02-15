using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Resources;
using System.Globalization;
using System.Collections;
using System.IO;
using Newtonsoft.Json;
using Bill2Pay.GenerateIRSFile.Model;
using System.Web.Hosting;
using Bill2Pay.Model;

namespace Bill2Pay.GenerateIRSFile
{
    public class GenerateTaxFile
    {
        List<Records> records;
        StringBuilder fileData= new StringBuilder();
        bool testFileIndicator = false;
        int recordSequenceNumber = 1;
        ApplicationDbContext dbContext = null;
        List<ImportDetail> detailTableData = null;
        ImportSummary summaryTableData= null;
        string amount = string.Empty;
        decimal januaryAmount, februaryAmount, marchAmount, aprilAmount, mayAmount, juneAmount, julyAmount, augustAmount, septemberAmount, octoberAmount, novemberAmount, decemberAmount = 0;
        decimal grossAmount, cnpTransactionAmount, federalWithHoldingAmount, stateWithHolding, localWithHolding=0;
        int numberofPayee = 0;
        int paymentYear = 0;
        long userId = 0;

        
        //SubmissionDetail submissionDetail = new SubmissionDetail();
        //SubmissionStatus submissionStatus = new SubmissionStatus();
        SubmissionSummary submissionSummary = new SubmissionSummary();
        //PSEMaster pseMaster = new PSEMaster();

        public GenerateTaxFile(bool testFile, int year, long userId)
        {
            testFileIndicator = testFile;
            paymentYear = year;

             dbContext= new ApplicationDbContext();
            //var submissionDetails = dbContext.SubmissionDetails.ToList();

            //var tableData = from SubmissionDetail in dbContext.SubmissionDetails
            //                join SubmissionSummary in dbContext.SubmissionSummary on SubmissionDetail.SubmissionSummary.Id equals SubmissionSummary.Id
            //                select new { detail = SubmissionDetail, summary = SubmissionSummary };

            detailTableData = dbContext.ImportDetails.ToList();
            summaryTableData = dbContext.ImportSummary.OrderByDescending(x=>x.Id).First();
            numberofPayee = detailTableData.Count();
        } 

        private void GenerateTRecord()
        {
            Records tRecords = records.FirstOrDefault(x => x.RecordType == "T");

            foreach(Field item in tRecords.Fields)
            {
                switch(item.Name.ToUpper())
                {
                    case "TEST FILE INDICATOR":
                        if (testFileIndicator)
                            fileData.Append(GetFieldValue(item, Model.ValueType.Defalut));
                        else
                            fileData.Append(GetFieldValue(item, Model.ValueType.Alternate));
                        break;
                    case "RECORD SEQUENCE NUMBER":
                        item.Default = recordSequenceNumber.ToString();
                        fileData.Append(GetFieldValue(item));
                        recordSequenceNumber++;
                        break;
                    case "TOTAL NUMBER OF PAYEES":
                        item.Default= numberofPayee.ToString();
                        fileData.Append(GetFieldValue(item));
                        break;
                    default:
                        fileData.Append(GetFieldValue(item));
                        break;
                }
            }
        }
        private void GenerateARecord()
        {
            Records tRecords = records.FirstOrDefault(x => x.RecordType == "A");

            foreach (Field item in tRecords.Fields)
            {
                switch (item.Name.ToUpper())
                {
                    case "RECORD SEQUENCE NUMBER":
                        item.Default = recordSequenceNumber.ToString();
                        fileData.Append(GetFieldValue(item));
                        recordSequenceNumber++;
                        break;
                    case "COMBINED FEDERAL/STATE FILING PROGRAM":
                        fileData.Append(GetFieldValue(item,Model.ValueType.Alternate));
                        break;
                    default:
                        fileData.Append(GetFieldValue(item));
                        break;
                }
            }
        }
        private void GenerateBRecord()
        {
            foreach (var data in detailTableData)
            {
                Records bRecords = records.FirstOrDefault(x => x.RecordType == "B");
                foreach (Field item in bRecords.Fields)
                {
                    switch (item.Name.ToUpper())
                    {
                        case "PAYMENT YEAR":
                            fileData.Append(GetFieldValue(item));
                            break;
                        case "RECORD SEQUENCE NUMBER":
                            item.Default = recordSequenceNumber.ToString();
                            fileData.Append(GetFieldValue(item));
                            recordSequenceNumber++;
                            break;
                        case "PAYMENT AMOUNT 1":
                            amount= GetFieldValue(item, dataValue: data);
                            grossAmount = grossAmount + Convert.ToDecimal(amount);
                            fileData.Append(FormatAmount(amount, item));
                            break;
                        case "PAYMENT AMOUNT 2":
                            amount = GetFieldValue(item, dataValue: data);
                            cnpTransactionAmount = cnpTransactionAmount+ Convert.ToDecimal(amount);
                            fileData.Append(FormatAmount(amount, item));
                            break;
                        case "PAYMENT AMOUNT 3":
                            fileData.Append(GetFieldValue(item));
                            break;
                        case "PAYMENT AMOUNT 4":
                            amount = GetFieldValue(item, dataValue: data);
                            federalWithHoldingAmount = federalWithHoldingAmount + Convert.ToDecimal(amount);
                            fileData.Append(FormatAmount(amount, item));
                            break;
                        case "PAYMENT AMOUNT 5":
                            amount = GetFieldValue(item, dataValue: data);
                            januaryAmount = januaryAmount + Convert.ToDecimal(amount);
                            fileData.Append(FormatAmount(amount, item));
                            break;
                        case "PAYMENT AMOUNT 6":
                            amount = GetFieldValue(item, dataValue: data);
                            februaryAmount = februaryAmount + Convert.ToDecimal(amount);
                            fileData.Append(FormatAmount(amount, item));
                            break;
                        case "PAYMENT AMOUNT 7":
                            amount = GetFieldValue(item, dataValue: data);
                            marchAmount = marchAmount + Convert.ToDecimal(amount);
                            fileData.Append(FormatAmount(amount, item));
                            break;
                        case "PAYMENT AMOUNT 8":
                            amount = GetFieldValue(item, dataValue: data);
                            aprilAmount = aprilAmount + Convert.ToDecimal(amount);
                            fileData.Append(FormatAmount(amount, item));
                            break;
                        case "PAYMENT AMOUNT 9":
                            amount = GetFieldValue(item, dataValue: data);
                            mayAmount = mayAmount + Convert.ToDecimal(amount);
                            fileData.Append(FormatAmount(amount, item));
                            break;
                        case "PAYMENT AMOUNT A":
                            amount = GetFieldValue(item, dataValue: data);
                            juneAmount= juneAmount+ Convert.ToDecimal(amount);
                            fileData.Append(FormatAmount(amount, item));
                            break;
                        case "PAYMENT AMOUNT B":
                            amount = GetFieldValue(item, dataValue: data);
                            julyAmount = julyAmount+ Convert.ToDecimal(amount);
                            fileData.Append(FormatAmount(amount, item));
                            break;
                        case "PAYMENT AMOUNT C":                        
                            amount = GetFieldValue(item, dataValue: data);
                            augustAmount = augustAmount + Convert.ToDecimal(amount);
                            fileData.Append(FormatAmount(amount, item));
                            break;
                        case "PAYMENT AMOUNT D":
                            amount = GetFieldValue(item, dataValue: data);
                            septemberAmount  = septemberAmount+ Convert.ToDecimal(amount);
                            fileData.Append(FormatAmount(amount, item));
                            break;
                        case "PAYMENT AMOUNT E":
                            amount = GetFieldValue(item, dataValue: data);
                            octoberAmount = octoberAmount + Convert.ToDecimal(amount);
                            fileData.Append(FormatAmount(amount, item));
                            break;
                        case "PAYMENT AMOUNT F":
                            amount = GetFieldValue(item, dataValue: data);
                            novemberAmount = novemberAmount + Convert.ToDecimal(amount);
                            fileData.Append(FormatAmount(amount, item));
                            break;
                        case "PAYMENT AMOUNT G":
                            amount = GetFieldValue(item, dataValue: data);
                            decemberAmount = decemberAmount + Convert.ToDecimal(amount);
                            fileData.Append(FormatAmount(amount, item));
                            break;
                        case "STATE INCOME TAX WITHHELD":
                            amount = GetFieldValue(item, dataValue: data);
                            stateWithHolding = stateWithHolding + Convert.ToDecimal(amount);
                            fileData.Append(FormatAmount(amount, item));
                            break;
                        case "LOCAL INCOME TAX WITHHELD":
                            amount = GetFieldValue(item, dataValue: data);
                            localWithHolding = localWithHolding + Convert.ToDecimal(amount);
                            fileData.Append(FormatAmount(amount, item));
                            break;
                        default:
                            fileData.Append(GetFieldValue(item, dataValue: data));
                            break;
                    }
                }
            }
        }
        private void GenerateCRecord()
        {
            Records tRecords = records.FirstOrDefault(x => x.RecordType == "C");

            foreach (Field item in tRecords.Fields)
            {
                switch (item.Name.ToUpper())
                {
                    case "RECORD SEQUENCE NUMBER":
                        item.Default = recordSequenceNumber.ToString();
                        fileData.Append(GetFieldValue(item));
                        recordSequenceNumber++;
                        break;
                    case "NUMBER OF PAYEES":
                        item.Default = numberofPayee.ToString();
                        fileData.Append(GetFieldValue(item));
                        break;
                    case "CONTROL TOTAL 1":
                        item.Default = grossAmount.ToString();
                        amount = GetFieldValue(item);
                        fileData.Append(FormatAmount(amount, item));
                        break;
                    case "CONTROL TOTAL 2":
                        item.Default = cnpTransactionAmount.ToString();
                        amount = GetFieldValue(item);
                        fileData.Append(FormatAmount(amount, item));
                        break;
                    case "CONTROL TOTAL 3":
                        fileData.Append(GetFieldValue(item));
                        break;
                    case "CONTROL TOTAL 4":
                        item.Default = federalWithHoldingAmount.ToString();
                        amount = GetFieldValue(item);
                        fileData.Append(FormatAmount(amount, item));
                        break;
                    case "CONTROL TOTAL 5":
                        item.Default = januaryAmount.ToString();
                        amount = GetFieldValue(item);
                        fileData.Append(FormatAmount(amount, item));
                        break;
                    case "CONTROL TOTAL 6":
                        item.Default = februaryAmount.ToString();
                        amount = GetFieldValue(item);
                        fileData.Append(FormatAmount(amount, item));
                        break;
                    case "CONTROL TOTAL 7":
                        item.Default = marchAmount.ToString();
                        amount = GetFieldValue(item);
                        fileData.Append(FormatAmount(amount, item));
                        break;
                    case "CONTROL TOTAL 8":
                        item.Default = aprilAmount.ToString();
                        amount = GetFieldValue(item);
                        fileData.Append(FormatAmount(amount, item));
                        break;
                    case "CONTROL TOTAL 9":
                        item.Default = mayAmount.ToString();
                        amount = GetFieldValue(item);
                        fileData.Append(FormatAmount(amount, item));
                        break;
                    case "CONTROL TOTAL A":
                        item.Default = juneAmount.ToString();
                        amount = GetFieldValue(item);
                        fileData.Append(FormatAmount(amount, item));
                        break;
                    case "CONTROL TOTAL B":
                        item.Default = julyAmount.ToString();
                        amount = GetFieldValue(item);
                        fileData.Append(FormatAmount(amount, item));
                        break;
                    case "CONTROL TOTAL C":
                        item.Default = augustAmount.ToString();
                        amount = GetFieldValue(item);
                        fileData.Append(FormatAmount(amount, item));
                        break;
                    case "CONTROL TOTAL D":
                        item.Default = septemberAmount.ToString();
                        amount = GetFieldValue(item);
                        fileData.Append(FormatAmount(amount, item));
                        break;
                    case "CONTROL TOTAL E":
                        item.Default = octoberAmount.ToString();
                        amount = GetFieldValue(item);
                        fileData.Append(FormatAmount(amount, item));
                        break;
                    case "CONTROL TOTAL F":
                        item.Default = novemberAmount.ToString();
                        amount = GetFieldValue(item);
                        fileData.Append(FormatAmount(amount, item));
                        break;
                    case "CONTROL TOTAL G":
                        item.Default = decemberAmount.ToString();
                        amount = GetFieldValue(item);
                        fileData.Append(FormatAmount(amount, item));
                        break;
                    default:
                        fileData.Append(GetFieldValue(item));
                        break;
                }
            }
        }
        private void GenerateKRecord()
        {
            Records tRecords = records.FirstOrDefault(x => x.RecordType == "K");

            foreach (Field item in tRecords.Fields)
            {
                switch (item.Name.ToUpper())
                {
                    case "RECORD SEQUENCE NUMBER":
                        item.Default = recordSequenceNumber.ToString();
                        fileData.Append(GetFieldValue(item));
                        recordSequenceNumber++;
                        break;
                    case "NUMBER OF PAYEES":
                        item.Default = numberofPayee.ToString();
                        fileData.Append(GetFieldValue(item));
                        break;
                    case "CONTROL TOTAL 1":
                        item.Default = grossAmount.ToString();
                        amount = GetFieldValue(item);
                        fileData.Append(FormatAmount(amount, item));
                        break;
                    case "CONTROL TOTAL 2":
                        item.Default = cnpTransactionAmount.ToString();
                        amount = GetFieldValue(item);
                        fileData.Append(FormatAmount(amount, item));
                        break;
                    case "CONTROL TOTAL 3":
                        fileData.Append(GetFieldValue(item));
                        break;
                    case "CONTROL TOTAL 4":
                        item.Default = federalWithHoldingAmount.ToString();
                        amount = GetFieldValue(item);
                        fileData.Append(FormatAmount(amount, item));
                        break;
                    case "CONTROL TOTAL 5":
                        item.Default = januaryAmount.ToString();
                        amount = GetFieldValue(item);
                        fileData.Append(FormatAmount(amount, item));
                        break;
                    case "CONTROL TOTAL 6":
                        item.Default = februaryAmount.ToString();
                        amount = GetFieldValue(item);
                        fileData.Append(FormatAmount(amount, item));
                        break;
                    case "CONTROL TOTAL 7":
                        item.Default = marchAmount.ToString();
                        amount = GetFieldValue(item);
                        fileData.Append(FormatAmount(amount, item));
                        break;
                    case "CONTROL TOTAL 8":
                        item.Default = aprilAmount.ToString();
                        amount = GetFieldValue(item);
                        fileData.Append(FormatAmount(amount, item));
                        break;
                    case "CONTROL TOTAL 9":
                        item.Default = mayAmount.ToString();
                        amount = GetFieldValue(item);
                        fileData.Append(FormatAmount(amount, item));
                        break;
                    case "CONTROL TOTAL A":
                        item.Default = juneAmount.ToString();
                        amount = GetFieldValue(item);
                        fileData.Append(FormatAmount(amount, item));
                        break;
                    case "CONTROL TOTAL B":
                        item.Default = julyAmount.ToString();
                        amount = GetFieldValue(item);
                        fileData.Append(FormatAmount(amount, item));
                        break;
                    case "CONTROL TOTAL C":
                        item.Default = augustAmount.ToString();
                        amount = GetFieldValue(item);
                        fileData.Append(FormatAmount(amount, item));
                        break;
                    case "CONTROL TOTAL D":
                        item.Default = septemberAmount.ToString();
                        amount = GetFieldValue(item);
                        fileData.Append(FormatAmount(amount, item));
                        break;
                    case "CONTROL TOTAL E":
                        item.Default = octoberAmount.ToString();
                        amount = GetFieldValue(item);
                        fileData.Append(FormatAmount(amount, item));
                        break;
                    case "CONTROL TOTAL F":
                        item.Default = novemberAmount.ToString();
                        amount = GetFieldValue(item);
                        fileData.Append(FormatAmount(amount, item));
                        break;
                    case "CONTROL TOTAL G":
                        item.Default = decemberAmount.ToString();
                        amount = GetFieldValue(item);
                        fileData.Append(FormatAmount(amount, item));
                        break;
                    case "STATE INCOME TAX WITHHELD TOTAL":
                        item.Default = stateWithHolding.ToString();
                        amount = GetFieldValue(item);
                        fileData.Append(FormatAmount(amount, item));
                        break;
                    case "LOCAL INCOME TAX WITHHELD TOTAL":
                        item.Default = localWithHolding.ToString();
                        amount = GetFieldValue(item);
                        fileData.Append(FormatAmount(amount, item));
                        break;
                    default:
                        fileData.Append(GetFieldValue(item));
                        break;
                }
            }
        }
        private void GenerateFRecord()
        {
            Records tRecords = records.FirstOrDefault(x => x.RecordType == "F");

            foreach (Field item in tRecords.Fields)
            {
                switch (item.Name.ToUpper())
                {
                    case "RECORD SEQUENCE NUMBER":
                        item.Default = recordSequenceNumber.ToString();
                        fileData.Append(GetFieldValue(item));
                        recordSequenceNumber++;
                        break;
                    case "TOTAL NUMBER OF PAYEES":
                        item.Default = numberofPayee.ToString();
                        fileData.Append(GetFieldValue(item));
                        break;
                    default:
                        fileData.Append(GetFieldValue(item));
                        break;
                }
            }
        }
        private string GetFieldValue(Field field, Model.ValueType valueType = Model.ValueType.Defalut, ImportDetail dataValue = null)
        {
            string value = string.Empty;
            if (!string.IsNullOrEmpty(field.Data))
            {
                if (dataValue == null)
                {
                    System.Reflection.PropertyInfo pi = summaryTableData.GetType().GetProperty(field.Data.Trim());
                    if (pi != null)
                    {
                        value = (pi.GetValue(summaryTableData, null)).ToString();
                    }
                    else
                    {
                        pi = detailTableData.GetType().GetProperty(field.Data.Trim());
                        value =(pi.GetValue(detailTableData, null)).ToString();
                    }
                }
                else
                {
                    System.Reflection.PropertyInfo pi = dataValue.GetType().GetProperty(field.Data.Trim());
                    if (pi != null)
                    {
                        if (pi.GetValue(dataValue, null) != null)
                            value = (pi.GetValue(dataValue, null)).ToString();
                        else
                        {
                            if (field.Type == FieldType.Alphanumeric)
                                value = "";
                            else
                                value = "0";
                        }
                    }
                }
            }
            else if (field.Default.Equals("\n"))
                value = field.Default.Replace("\n", Environment.NewLine);
            else
            {
                if (valueType == Model.ValueType.Defalut)
                    value = field.Default.Trim();
                else
                    value = field.Alternate.Trim();
            }
            return field.PadValue(value);
        }
        private string FormatAmount(string value,Field field)
        {
            value = value.Replace(".", "").Replace(",", "").Replace("$", "");
            return field.PadValue(value);
        }

private void PopulateSubmissionSummary()
        {
            
            submissionSummary.PaymentYear = 1234;
            //submissionSummary.CreatedUser = 1;


            dbContext.SubmissionSummary.Add(submissionSummary);
            //dbContext.SaveChanges();
            
            
        }       
        public void ReadFromSchemaFile()
        {
            //TODO : Read from config file;
            //var path = string.Format(@"{0}\App_Data", HostingEnvironment.ApplicationPhysicalPath);
            var json = File.ReadAllText(@"C:\B2P\Bill2Pay.GenerateIRSFile\IRSFileFields.json");
            
            records= JsonConvert.DeserializeObject<List<Records>>(json);

            GenerateETaxFile();

            #region "commented"
            //var result = (Records)JsonConvert.DeserializeObject(json, typeof(Records));

    //        Records []x= new Records[2];

    //Model.Field fl = new Model.Field();
    //        x[0] = new Records();
    //        x[0].Fields = new List<Model.Field>();
                
    //        fl.Name = "Name1";
    //        x[0].Fields.Add(fl);
    //        fl.Name = "Name2";
    //        x[0].Fields.Add(fl);
    //        fl.Name = "Name3";
    //        x[0].Fields.Add(fl);

    //        x[1] = new Records();
    //        x[1].Fields = new List<Model.Field>();
    //        fl.Name = "Name4";
    //        x[1].Fields.Add(fl);
    //        fl.Name = "Name5";
    //        x[1].Fields.Add(fl);
    //        fl.Name = "Name6";
    //        x[1].Fields.Add(fl);

    //        var result = JsonConvert.SerializeObject(x);
            #endregion
        }
        private void GenerateETaxFile()
        {
            GenerateTRecord();
            GenerateARecord();
            GenerateBRecord();
            GenerateCRecord();
            GenerateKRecord();
            GenerateFRecord();
        }
    }
}
