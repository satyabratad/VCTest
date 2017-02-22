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
        #region "Variables"
        List<Records> records;
        StringBuilder fileData = new StringBuilder();
        bool testFileIndicator = false;
        int recordSequenceNumber = 1;
        ApplicationDbContext dbContext = null;
        List<ImportDetail> detailTableData = null;
        ImportSummary summaryTableData = null;
        string amount = string.Empty;
        decimal januaryAmount, februaryAmount, marchAmount, aprilAmount, mayAmount, juneAmount, julyAmount, augustAmount, septemberAmount, octoberAmount, novemberAmount, decemberAmount = 0;
        decimal grossAmount, cnpTransactionAmount, federalWithHoldingAmount, stateWithHolding, localWithHolding = 0;
        int numberofPayee = 0;
        int paymentYear = 0;
        long userId = 0;
        bool reSubmission = false;
        PSEMaster pseMaster = null;
        #endregion

        public GenerateTaxFile(bool testFile, int year, long user, List<string> selectedAccountNo, bool correction = false)
        {
            testFileIndicator = testFile;
            paymentYear = year;
            userId = user;
            reSubmission = correction;

            dbContext = new ApplicationDbContext();
            pseMaster = new PSEMaster();

            summaryTableData = dbContext.ImportSummary.OrderByDescending(x => x.Id).First();
            

            detailTableData = dbContext.ImportDetails
            .Join(dbContext.ImportSummary, d => d.ImportSummaryId, s => s.Id, (d, s) => new { detail = d, summary = s })
            .Where(x => selectedAccountNo.Contains(x.detail.AccountNo) && x.summary.PaymentYear == year && x.detail.IsActive==true && x.summary.IsActive==true) 
            .Select(x=>x.detail).ToList();

            numberofPayee = detailTableData.Count();
        }
        int pseMasterId = 0;
        private void GenerateTRecord()
        {
            Records tRecords = records.FirstOrDefault(x => x.RecordType == "T");

            foreach (Field item in tRecords.Fields)
            {
                switch (item.Name.ToUpper())
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
                        item.Default = numberofPayee.ToString();
                        fileData.Append(GetFieldValue(item));
                        break;
                    default:
                        fileData.Append(GetFieldValue(item));
                        break;
                }
            }
            SavePSEMaster(tRecords);
        }
        private void GenerateARecord()
        {
            Records aRecords = records.FirstOrDefault(x => x.RecordType == "A");

            foreach (Field item in aRecords.Fields)
            {
                switch (item.Name.ToUpper())
                {
                    case "RECORD SEQUENCE NUMBER":
                        item.Default = recordSequenceNumber.ToString();
                        fileData.Append(GetFieldValue(item));
                        recordSequenceNumber++;
                        break;
                    case "COMBINED FEDERAL/STATE FILING PROGRAM":
                        fileData.Append(GetFieldValue(item, Model.ValueType.Alternate));
                        break;
                    default:
                        fileData.Append(GetFieldValue(item));
                        break;
                }
            }
            SavePSEMaster(aRecords);
            dbContext.PSEMaster.Add(pseMaster);
            dbContext.SaveChanges();
            pseMasterId = pseMaster.Id;

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
                            amount = GetFieldValue(item, dataValue: data);
                            grossAmount = grossAmount + Convert.ToDecimal(amount);
                            fileData.Append(FormatAmount(amount, item));
                            break;
                        case "PAYMENT AMOUNT 2":
                            amount = GetFieldValue(item, dataValue: data);
                            cnpTransactionAmount = cnpTransactionAmount + Convert.ToDecimal(amount);
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
                            juneAmount = juneAmount + Convert.ToDecimal(amount);
                            fileData.Append(FormatAmount(amount, item));
                            break;
                        case "PAYMENT AMOUNT B":
                            amount = GetFieldValue(item, dataValue: data);
                            julyAmount = julyAmount + Convert.ToDecimal(amount);
                            fileData.Append(FormatAmount(amount, item));
                            break;
                        case "PAYMENT AMOUNT C":
                            amount = GetFieldValue(item, dataValue: data);
                            augustAmount = augustAmount + Convert.ToDecimal(amount);
                            fileData.Append(FormatAmount(amount, item));
                            break;
                        case "PAYMENT AMOUNT D":
                            amount = GetFieldValue(item, dataValue: data);
                            septemberAmount = septemberAmount + Convert.ToDecimal(amount);
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
                        value = (pi.GetValue(detailTableData, null)).ToString();
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
        private string FormatAmount(string value, Field field)
        {
            value = value.Replace(".", "").Replace(",", "").Replace("$", "");
            return field.PadValue(value);
        }
        private int SaveSubmissionSummary()
        {
            var submissionSummary = new SubmissionSummary();

            submissionSummary.PaymentYear = paymentYear;
            submissionSummary.SubmissionDate = DateTime.Now;
            submissionSummary.UserId = userId;
            submissionSummary.DateAdded = DateTime.Now;
            submissionSummary.IsActive = true;

            dbContext.SubmissionSummary.Add(submissionSummary);
            dbContext.SaveChanges();
            return submissionSummary.Id;
        }
        private void SaveSubmissionDetails()
        {
            int submissionSummaryId = SaveSubmissionSummary();

            foreach (var item in detailTableData)
            {
                var submissionDetails = new SubmissionDetail();

                submissionDetails.AccountNo = item.AccountNo;
                submissionDetails.SubmissionId = submissionSummaryId;
                submissionDetails.TINType = item.TINType;
                submissionDetails.TIN = item.TIN;
                submissionDetails.PayerOfficeCode = item.PayerOfficeCode;
                submissionDetails.GrossAmount = item.GrossAmount;
                submissionDetails.CNPTransactionAmount = item.CNPTransactionAmount;
                submissionDetails.FederalWithHoldingAmount = item.FederalWithHoldingAmount;
                submissionDetails.JanuaryAmount = item.JanuaryAmount;
                submissionDetails.FebruaryAmount = item.FebruaryAmount;
                submissionDetails.MarchAmount = item.MarchAmount;
                submissionDetails.AprilAmount = item.AprilAmount;
                submissionDetails.MayAmount = item.MayAmount;
                submissionDetails.JuneAmount = item.JuneAmount;
                submissionDetails.JulyAmount = item.JulyAmount;
                submissionDetails.AugustAmount = item.AugustAmount;
                submissionDetails.SeptemberAmount = item.SeptemberAmount;
                submissionDetails.OctoberAmount = item.OctoberAmount;
                submissionDetails.NovemberAmount = item.NovemberAmount;
                submissionDetails.DecemberAmount = item.DecemberAmount;
                submissionDetails.ForeignCountryIndicator = item.ForeignCountryIndicator;
                submissionDetails.FirstPayeeName = item.FirstPayeeName;
                submissionDetails.SecondPayeeName = item.SecondPayeeName;
                submissionDetails.PayeeMailingAddress = item.PayeeMailingAddress;
                submissionDetails.PayeeCity = item.PayeeCity;
                submissionDetails.PayeeState = item.PayeeState;
                submissionDetails.PayeeZipCode = item.PayeeZipCode;
                submissionDetails.SecondTINNoticed = item.SecondTINNoticed;
                submissionDetails.FillerIndicatorType = item.FillerIndicatorType;
                submissionDetails.PaymentIndicatorType = item.PaymentIndicatorType;
                submissionDetails.TransactionCount = item.TransactionCount;
                submissionDetails.PSEMasterId = pseMasterId;
                submissionDetails.MerchantCategoryCode = item.MerchantCategoryCode;
                submissionDetails.SpecialDataEntry = item.SpecialDataEntry;
                submissionDetails.StateWithHolding = item.StateWithHolding;
                submissionDetails.LocalWithHolding = item.LocalWithHolding;
                submissionDetails.CFSF = item.CFSF;
                submissionDetails.DateAdded = DateTime.Now;

                submissionDetails.SubmissionType = (!reSubmission) ? 1 : 2;

                dbContext.SubmissionDetails.Add(submissionDetails);
                item.SubmissionSummaryId = submissionSummaryId;
                item.PSEMasterId = pseMasterId;
                submissionDetails.IsActive = true;
                SaveSubmissionStatus(item.AccountNo, reSubmission ? (int)RecordStatus.ReSubmitted : (int)RecordStatus.Submitted);
                dbContext.SaveChanges();
            }
        }
        private void SaveSubmissionStatus(string accountNo, int statusId)
        {
            var submissionStatus = new SubmissionStatus();
            var previousData = dbContext.SubmissionStatus.Where(x=> x.AccountNumber.Equals(accountNo) && x.PaymentYear.Equals(paymentYear) && x.IsActive==true).ToList();

            if(previousData!=null)
            {
                foreach (var item in previousData)
                {
                    item.IsActive = false;
                }
            }

            submissionStatus.PaymentYear = paymentYear;
            submissionStatus.AccountNumber = accountNo;
            submissionStatus.ProcessingDate = DateTime.Now;
            submissionStatus.StatusId = statusId;
            submissionStatus.DateAdded = DateTime.Now;
            submissionStatus.IsActive = true;

            dbContext.SubmissionStatus.Add(submissionStatus);
        }
        private void SavePSEMaster(Records records)
        {
            foreach (Field item in records.Fields)
            {
                switch (item.Name.ToUpper())
                {
                    case "TRANSMITTER’S TIN":
                        pseMaster.TransmitterTIN = item.Default;
                        break;
                    case "TRANSMITTER CONTROL CODE":
                        pseMaster.TransmitterControlCode = item.Default;
                        break;
                    case "TEST FILE INDICATOR":
                        pseMaster.TestFileIndicator = testFileIndicator ? "T" : " ";
                        break;
                    case "FOREIGN ENTITY INDICATOR":
                        pseMaster.TransmitterForeignEntityIndicator = item.Default;
                        break;
                    case "TRANSMITTER NAME":
                        pseMaster.TransmitterName = item.Default;
                        break;
                    case "TRANSMITTER NAME (CONTINUATION)":
                        pseMaster.TransmitterNameContinued = item.Default;
                        break;
                    case "COMPANY NAME":
                        pseMaster.CompanyName = item.Default;
                        break;
                    case "COMPANY NAME (CONTINUATION)":
                        pseMaster.CompanyNameContinued = item.Default;
                        break;
                    case "COMPANY MAILING ADDRESS":
                        pseMaster.CompanyMailingAddress = item.Default;
                        break;
                    case "COMPANY CITY":
                        pseMaster.CompanyCity = item.Default;
                        break;
                    case "COMPANY STATE":
                        pseMaster.CompanyState = item.Default;
                        break;
                    case "COMPANY ZIP CODE":
                        pseMaster.CompanyZIP = item.Default;
                        break;
                    case "TOTAL NUMBER OF PAYEES":
                        pseMaster.TotalNumberofPayees = numberofPayee;
                        break;
                    case "CONTACT NAME":
                        pseMaster.ContactName = item.Default;
                        break;
                    case "CONTACT TELEPHONE NUMBER & EXTENSION":
                        pseMaster.ContactTelephoneNumber = item.Default;
                        break;
                    case "CONTACT EMAIL ADDRESS":
                        pseMaster.ContactEmailAddress = item.Default;
                        break;
                    case "VENDOR INDICATOR":
                        pseMaster.VendorIndicator = item.Default;
                        break;
                    case "VENDOR NAME":
                        pseMaster.VendorName = item.Default;
                        break;
                    case "VENDOR MAILING ADDRESS":
                        pseMaster.VendorMailingAddress = item.Default;
                        break;
                    case "VENDOR CITY":
                        pseMaster.VendorCity = item.Default;
                        break;
                    case "VENDOR STATE":
                        pseMaster.VendorState = item.Default;
                        break;
                    case "VENDOR ZIP CODE":
                        pseMaster.VendorZIP = item.Default;
                        break;
                    case "VENDOR CONTACT NAME":
                        pseMaster.VendorContactName = item.Default;
                        break;
                    case "VENDOR CONTACT TELEPHONE NUMBER & EXTENSION":
                        pseMaster.VendorContactTelephoneNumber = item.Default;
                        break;
                    case "VENDOR FOREIGN ENTITY INDICATOR":
                        pseMaster.VendorForeignEntityIndicator = item.Default;
                        break;
                    case "COMBINED FEDERAL/STATE FILING PROGRAM":
                        pseMaster.CFSF = item.Default;
                        break;
                    case "PAYER’S TAXPAYER IDENTIFICATION NUMBER (TIN)":
                        pseMaster.PayerTIN = item.Default;
                        break;
                    case "PAYER NAME CONTROL":
                        pseMaster.PayerNameControl = item.Default;
                        break;
                    case "LAST FILING INDICATOR":
                        pseMaster.LastFilingIndicator = item.Default;
                        break;
                    case "TYPE OF RETURN":
                        pseMaster.ReturnType = item.Default;
                        break;
                    case "FIRST PAYER NAME LINE":
                        pseMaster.FirstPayerName = item.Default;
                        break;
                    case "SECOND PAYER NAME LINE":
                        pseMaster.SecondPayerName = item.Default;
                        break;
                    case "TRANSFER AGENT INDICATOR":
                        pseMaster.TransferAgentIndicator = item.Default;
                        break;
                    case "PAYER SHIPPING ADDRESS":
                        pseMaster.PayerShippingAddress = item.Default;
                        break;
                    case "PAYER CITY":
                        pseMaster.PayerCity = item.Default;
                        break;
                    case "PAYER STATE":
                        pseMaster.PayerState = item.Default;
                        break;
                    case "PAYER ZIP CODE":
                        pseMaster.PayerZIP = item.Default;
                        break;
                    case "PAYER’S TELEPHONE NUMBER AND EXTENSION":
                        pseMaster.PayerTelephoneNumber = item.Default;
                        break;
                    default:
                        break;
                        
                }
                pseMaster.DateAdded = DateTime.Now;
            }
        }
        public void ReadFromSchemaFile()
        {
            string path = string.Empty;
            var jsonpath = string.Format(@"{0}App_Data\IRSFileFields.json", HostingEnvironment.ApplicationPhysicalPath);
            if (testFileIndicator)
                path = string.Format(@"{0}App_Data\Download\Irs\IRSInputFile_Test.txt", HostingEnvironment.ApplicationPhysicalPath);
            else
                path = string.Format(@"{0}App_Data\Download\Irs\IRSInputFile.txt", HostingEnvironment.ApplicationPhysicalPath);

            var json = File.ReadAllText(jsonpath);


            records = JsonConvert.DeserializeObject<List<Records>>(json);

            GenerateETaxFile();

            // Delete the file if it exists.
            if (File.Exists(path))
            {
                File.Delete(path);
            }

            // Create the file.
            using (FileStream fs = File.Create(path))
            {
                Byte[] info = new UTF8Encoding(true).GetBytes(fileData.ToString());
                fs.Write(info, 0, info.Length);
                if (!testFileIndicator)
                    SaveSubmissionDetails();
            }

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

    public enum RecordStatus
    {
        NotSubmitted = 1,
        Submitted = 2,
        CorrectionRequired = 3,
        CorrectionUploaded = 4,
        ReSubmitted = 5
    }
}
