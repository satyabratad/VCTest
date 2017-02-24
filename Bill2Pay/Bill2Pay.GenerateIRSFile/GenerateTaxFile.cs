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
        List<CFSFStates> cfsfStates;
        List<PayerDetail> payerDetails = null;
        TransmitterDetail transmitterDetails = null;
        StringBuilder fileData = new StringBuilder();
        bool testFileIndicator = false;
        int recordSequenceNumber = 1;
        ApplicationDbContext dbContext = null;
        List<ImportDetail> importDetaiils = null;
        ImportSummary importSummaries = null;
        string amount = string.Empty;
        decimal januaryAmount, februaryAmount, marchAmount, aprilAmount, mayAmount, juneAmount, julyAmount, augustAmount, septemberAmount, octoberAmount, novemberAmount, decemberAmount = 0;
        decimal grossAmount, cnpTransactionAmount, federalWithHoldingAmount, stateWithHolding, localWithHolding = 0;
        int numberofPayee = 0;
        int paymentYear = 0;
        long userId = 0;
        bool reSubmission = false;
        PSEDetails pseDetails = null;
        List<string> selectedAccounts = null;
        string currentAccountNumber = string.Empty;
        int currentPayer = 0;
        List<string> payerIds;
        int pseMasterId = 0;
        int totalNumberofPayee = 0;
        #endregion
        public GenerateTaxFile(bool testFile, int year, long user, List<string> selectedAccountNo, List<string> payerId, bool correction = false)
        {
            testFileIndicator = testFile;
            paymentYear = year;
            userId = user;
            reSubmission = correction;
            selectedAccounts = selectedAccountNo;
            payerIds = payerId;

            dbContext = new ApplicationDbContext();
            pseDetails = new PSEDetails();

            importSummaries = dbContext.ImportSummary.OrderByDescending(x => x.Id).Where(x => x.IsActive == true).FirstOrDefault();

            transmitterDetails = dbContext.TransmitterDetails.FirstOrDefault(x => x.IsActive == true);

            payerDetails = dbContext.PayerDetails.Where(x => payerIds.Contains(x.Id.ToString()) && x.IsActive == true).ToList();

        }

        
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
                        item.Default = selectedAccounts.Count.ToString();
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
            string cfsf = string.Empty;
            foreach (var data in payerDetails)
            {
                importDetaiils = dbContext.ImportDetails
                .Join(dbContext.ImportSummary, d => d.ImportSummaryId, s => s.Id, (d, s) => new { detail = d, summary = s })
                .Join(dbContext.MerchantDetails, d => d.detail.MerchantId, m => m.Id, (d, m) => new { d.detail, d.summary, merchant = m })
                .Where(x => selectedAccounts.Contains(x.detail.AccountNo) && x.summary.PaymentYear == paymentYear && x.detail.IsActive == true && x.summary.IsActive == true && x.merchant.PayerId.Equals(data.Id))
                .Select(x => x.detail).ToList();

                currentPayer = data.Id;
                numberofPayee = importDetaiils.Count();
                totalNumberofPayee = totalNumberofPayee + numberofPayee;

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
                            fileData.Append(GetFieldValue(item));
                            cfsf = GetFieldValue(item);
                            break;
                        default:
                            fileData.Append(GetFieldValue(item));
                            break;
                    }
                }
                SavePSEMaster(aRecords);
                dbContext.PSEMaster.Add(pseDetails);
                dbContext.SaveChanges();
                pseMasterId = pseDetails.Id;

                GenerateBRecord();
                GenerateCRecord();
                if (!string.IsNullOrEmpty(cfsf.Trim()))
                    GenerateKRecord();

            }




        }
        private void GenerateBRecord()
        {
            januaryAmount= februaryAmount= marchAmount= aprilAmount= mayAmount= juneAmount= julyAmount= augustAmount= septemberAmount= octoberAmount= novemberAmount= decemberAmount = 0;
            grossAmount= cnpTransactionAmount= federalWithHoldingAmount= stateWithHolding= localWithHolding = 0;

            foreach (var data in importDetaiils)
            {
                currentAccountNumber = data.AccountNo;
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
                            amount = GetFieldValue(item);
                            grossAmount = grossAmount + Convert.ToDecimal(amount);
                            fileData.Append(FormatAmount(amount, item));
                            break;
                        case "PAYMENT AMOUNT 2":
                            amount = GetFieldValue(item);
                            cnpTransactionAmount = cnpTransactionAmount + Convert.ToDecimal(amount);
                            fileData.Append(FormatAmount(amount, item));
                            break;
                        case "PAYMENT AMOUNT 3":
                            fileData.Append(GetFieldValue(item));
                            break;
                        case "PAYMENT AMOUNT 4":
                            amount = GetFieldValue(item);
                            federalWithHoldingAmount = federalWithHoldingAmount + Convert.ToDecimal(amount);
                            fileData.Append(FormatAmount(amount, item));
                            break;
                        case "PAYMENT AMOUNT 5":
                            amount = GetFieldValue(item);
                            januaryAmount = januaryAmount + Convert.ToDecimal(amount);
                            fileData.Append(FormatAmount(amount, item));
                            break;
                        case "PAYMENT AMOUNT 6":
                            amount = GetFieldValue(item);
                            februaryAmount = februaryAmount + Convert.ToDecimal(amount);
                            fileData.Append(FormatAmount(amount, item));
                            break;
                        case "PAYMENT AMOUNT 7":
                            amount = GetFieldValue(item);
                            marchAmount = marchAmount + Convert.ToDecimal(amount);
                            fileData.Append(FormatAmount(amount, item));
                            break;
                        case "PAYMENT AMOUNT 8":
                            amount = GetFieldValue(item);
                            aprilAmount = aprilAmount + Convert.ToDecimal(amount);
                            fileData.Append(FormatAmount(amount, item));
                            break;
                        case "PAYMENT AMOUNT 9":
                            amount = GetFieldValue(item);
                            mayAmount = mayAmount + Convert.ToDecimal(amount);
                            fileData.Append(FormatAmount(amount, item));
                            break;
                        case "PAYMENT AMOUNT A":
                            amount = GetFieldValue(item);
                            juneAmount = juneAmount + Convert.ToDecimal(amount);
                            fileData.Append(FormatAmount(amount, item));
                            break;
                        case "PAYMENT AMOUNT B":
                            amount = GetFieldValue(item);
                            julyAmount = julyAmount + Convert.ToDecimal(amount);
                            fileData.Append(FormatAmount(amount, item));
                            break;
                        case "PAYMENT AMOUNT C":
                            amount = GetFieldValue(item);
                            augustAmount = augustAmount + Convert.ToDecimal(amount);
                            fileData.Append(FormatAmount(amount, item));
                            break;
                        case "PAYMENT AMOUNT D":
                            amount = GetFieldValue(item);
                            septemberAmount = septemberAmount + Convert.ToDecimal(amount);
                            fileData.Append(FormatAmount(amount, item));
                            break;
                        case "PAYMENT AMOUNT E":
                            amount = GetFieldValue(item);
                            octoberAmount = octoberAmount + Convert.ToDecimal(amount);
                            fileData.Append(FormatAmount(amount, item));
                            break;
                        case "PAYMENT AMOUNT F":
                            amount = GetFieldValue(item);
                            novemberAmount = novemberAmount + Convert.ToDecimal(amount);
                            fileData.Append(FormatAmount(amount, item));
                            break;
                        case "PAYMENT AMOUNT G":
                            amount = GetFieldValue(item);
                            decemberAmount = decemberAmount + Convert.ToDecimal(amount);
                            fileData.Append(FormatAmount(amount, item));
                            break;
                        case "STATE INCOME TAX WITHHELD":
                            amount = GetFieldValue(item);
                            stateWithHolding = stateWithHolding + Convert.ToDecimal(amount);
                            fileData.Append(FormatAmount(amount, item));
                            break;
                        case "LOCAL INCOME TAX WITHHELD":
                            amount = GetFieldValue(item);
                            localWithHolding = localWithHolding + Convert.ToDecimal(amount);
                            fileData.Append(FormatAmount(amount, item));
                            break;
                        default:
                            fileData.Append(GetFieldValue(item));
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

            var states = cfsfStates.Select(x => x.State).ToList();

            var stateWieSummary = importDetaiils.Join(cfsfStates, d=> d.PayeeState, s=> s.State, (d,s)=> new {details=d, state=s } )
                .Where(x => x.details.IsActive == true)
                .Where(xy => selectedAccounts.Contains(xy.details.AccountNo))
                .Where(y => states.Contains(y.details.PayeeState))
                .GroupBy(x => x.state.Code)
                .Select(s => new
                {
                    state = s.Key,
                    count = s.Count(),
                    jan = s.Sum(w => w.details.JanuaryAmount),
                    feb = s.Sum(w => w.details.FebruaryAmount),
                    mar = s.Sum(w => w.details.MarchAmount),
                    apr = s.Sum(w => w.details.AprilAmount),
                    may = s.Sum(w => w.details.MayAmount),
                    jun = s.Sum(w => w.details.JuneAmount),
                    jul = s.Sum(w => w.details.JulyAmount),
                    aug = s.Sum(w => w.details.AugustAmount),
                    sep = s.Sum(w => w.details.SeptemberAmount),
                    oct = s.Sum(w => w.details.OctoberAmount),
                    nov = s.Sum(w => w.details.NovemberAmount),
                    dec = s.Sum(w => w.details.DecemberAmount),
                    stateWH = s.Sum(w => w.details.StateWithHolding),
                    localWH = s.Sum(w => w.details.LocalWithHolding),
                    gross = s.Sum(w => w.details.GrossAmount),
                    cnp = s.Sum(w => w.details.CNPTransactionAmount),
                    fWH = s.Sum(w => w.details.FederalWithHoldingAmount)
                }).ToList();
            //var stateWieSummary = dbContext.ImportDetails.Where(x => x.IsActive == true)
            //    .Where(xy => selectedAccounts.Contains(xy.AccountNo))
            //    .Where(y => states.Contains(y.PayeeState))
            //    .GroupBy(x => x.PayeeState)
            //    .Select(s => new
            //    {
            //        state = s.Key,
            //        count = s.Count(),
            //        jan = s.Sum(w => w.JanuaryAmount),
            //        feb = s.Sum(w => w.FebruaryAmount),
            //        mar = s.Sum(w => w.MarchAmount),
            //        apr = s.Sum(w => w.AprilAmount),
            //        may = s.Sum(w => w.MayAmount),
            //        jun = s.Sum(w => w.JuneAmount),
            //        jul = s.Sum(w => w.JulyAmount),
            //        aug = s.Sum(w => w.AugustAmount),
            //        sep = s.Sum(w => w.SeptemberAmount),
            //        oct = s.Sum(w => w.OctoberAmount),
            //        nov = s.Sum(w => w.NovemberAmount),
            //        dec = s.Sum(w => w.DecemberAmount),
            //        stateWH = s.Sum(w => w.StateWithHolding),
            //        localWH = s.Sum(w => w.LocalWithHolding),
            //        gross = s.Sum(w => w.GrossAmount),
            //        cnp = s.Sum(w => w.CNPTransactionAmount),
            //        fWH = s.Sum(w => w.FederalWithHoldingAmount)
            //    }).ToList();

            foreach (var data in stateWieSummary)
            {
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
                            item.Default = data.count.ToString();
                            fileData.Append(GetFieldValue(item));
                            break;
                        case "CONTROL TOTAL 1":
                            item.Default = data.gross.ToString();
                            amount = GetFieldValue(item);
                            fileData.Append(FormatAmount(amount, item));
                            break;
                        case "CONTROL TOTAL 2":
                            item.Default = data.cnp.ToString();
                            amount = GetFieldValue(item);
                            fileData.Append(FormatAmount(amount, item));
                            break;
                        case "CONTROL TOTAL 3":
                            fileData.Append(GetFieldValue(item));
                            break;
                        case "CONTROL TOTAL 4":
                            item.Default = data.fWH.ToString();
                            amount = GetFieldValue(item);
                            fileData.Append(FormatAmount(amount, item));
                            break;
                        case "CONTROL TOTAL 5":
                            item.Default = data.jan.ToString();
                            amount = GetFieldValue(item);
                            fileData.Append(FormatAmount(amount, item));
                            break;
                        case "CONTROL TOTAL 6":
                            item.Default = data.feb.ToString();
                            amount = GetFieldValue(item);
                            fileData.Append(FormatAmount(amount, item));
                            break;
                        case "CONTROL TOTAL 7":
                            item.Default = data.mar.ToString();
                            amount = GetFieldValue(item);
                            fileData.Append(FormatAmount(amount, item));
                            break;
                        case "CONTROL TOTAL 8":
                            item.Default = data.apr.ToString();
                            amount = GetFieldValue(item);
                            fileData.Append(FormatAmount(amount, item));
                            break;
                        case "CONTROL TOTAL 9":
                            item.Default = data.may.ToString();
                            amount = GetFieldValue(item);
                            fileData.Append(FormatAmount(amount, item));
                            break;
                        case "CONTROL TOTAL A":
                            item.Default = data.jun.ToString();
                            amount = GetFieldValue(item);
                            fileData.Append(FormatAmount(amount, item));
                            break;
                        case "CONTROL TOTAL B":
                            item.Default = data.jul.ToString();
                            amount = GetFieldValue(item);
                            fileData.Append(FormatAmount(amount, item));
                            break;
                        case "CONTROL TOTAL C":
                            item.Default = data.aug.ToString();
                            amount = GetFieldValue(item);
                            fileData.Append(FormatAmount(amount, item));
                            break;
                        case "CONTROL TOTAL D":
                            item.Default = data.sep.ToString();
                            amount = GetFieldValue(item);
                            fileData.Append(FormatAmount(amount, item));
                            break;
                        case "CONTROL TOTAL E":
                            item.Default = data.oct.ToString();
                            amount = GetFieldValue(item);
                            fileData.Append(FormatAmount(amount, item));
                            break;
                        case "CONTROL TOTAL F":
                            item.Default = data.nov.ToString();
                            amount = GetFieldValue(item);
                            fileData.Append(FormatAmount(amount, item));
                            break;
                        case "CONTROL TOTAL G":
                            item.Default = data.dec.ToString();
                            amount = GetFieldValue(item);
                            fileData.Append(FormatAmount(amount, item));
                            break;
                        case "STATE INCOME TAX WITHHELD TOTAL":
                            item.Default = data.stateWH.ToString();
                            amount = GetFieldValue(item);
                            fileData.Append(FormatAmount(amount, item));
                            break;
                        case "LOCAL INCOME TAX WITHHELD TOTAL":
                            item.Default = data.localWH.ToString();
                            amount = GetFieldValue(item);
                            fileData.Append(FormatAmount(amount, item));
                            break;
                        case "COMBINED FEDERAL/STATE CODE":
                            item.Default = data.state;
                            fileData.Append(GetFieldValue(item));
                            break;
                        default:
                            fileData.Append(GetFieldValue(item));
                            break;
                    }
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
                        item.Default = totalNumberofPayee.ToString();
                        fileData.Append(GetFieldValue(item));
                        break;
                    default:
                        fileData.Append(GetFieldValue(item));
                        break;
                }
            }
        }
        private string GetFieldValues(Field field, Model.ValueType valueType = Model.ValueType.Defalut, ImportDetail dataValue = null)
        {
            string value = string.Empty;
            if (!string.IsNullOrEmpty(field.Data))
            {
                if (dataValue == null)
                {
                    System.Reflection.PropertyInfo pi = importSummaries.GetType().GetProperty(field.Data.Trim());
                    if (pi != null)
                    {
                        value = (pi.GetValue(importSummaries, null)).ToString();
                    }
                    else
                    {
                        pi = importDetaiils.GetType().GetProperty(field.Data.Trim());
                        value = (pi.GetValue(importDetaiils, null)).ToString();
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
        private string GetFieldValue(Field field, Model.ValueType valueType = Model.ValueType.Defalut)
        {
            string value = string.Empty;
            System.Reflection.PropertyInfo pi = null;

            if (!string.IsNullOrEmpty(field.Table) && !string.IsNullOrEmpty(field.Data))
            {
                switch (field.Table.ToUpper())
                {
                    case "IMPORTDETAILS":
                        if (!string.IsNullOrEmpty(currentAccountNumber))
                        {
                            var importDetail = importDetaiils.FirstOrDefault(x => x.AccountNo.Equals(currentAccountNumber));
                            pi = importDetail.GetType().GetProperty(field.Data.Trim());
                            value = Convert.ToString((pi.GetValue(importDetail, null)));
                        }
                        break;
                    case "IMPORTSUMMARIES":
                        pi = importSummaries.GetType().GetProperty(field.Data.Trim());
                        value = Convert.ToString((pi.GetValue(importSummaries, null)));
                        break;
                    case "PAYERDETAILS":
                        var payer = payerDetails.FirstOrDefault(x => x.Id.Equals(currentPayer));
                        pi = payer.GetType().GetProperty(field.Data.Trim());
                        value = Convert.ToString((pi.GetValue(payer, null)));
                        break;
                    case "TRANSMITTERDETAILS":
                        pi = transmitterDetails.GetType().GetProperty(field.Data.Trim());
                        value = Convert.ToString(pi.GetValue(transmitterDetails, null));
                        break;
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

            foreach (var item in importDetaiils)
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
                submissionDetails.PseId = pseMasterId;
                submissionDetails.MerchantCategoryCode = item.MerchantCategoryCode;
                submissionDetails.SpecialDataEntry = item.SpecialDataEntry;
                submissionDetails.StateWithHolding = item.StateWithHolding;
                submissionDetails.LocalWithHolding = item.LocalWithHolding;
                submissionDetails.CFSF = item.CFSF;
                submissionDetails.DateAdded = DateTime.Now;

                submissionDetails.SubmissionType = (!reSubmission) ? 1 : 2;

                dbContext.SubmissionDetails.Add(submissionDetails);
                item.SubmissionSummaryId = submissionSummaryId;
                //item.PseId = pseMasterId;
                submissionDetails.IsActive = true;
                SaveSubmissionStatus(item.AccountNo, reSubmission ? (int)RecordStatus.ReSubmitted : (int)RecordStatus.Submitted);
                dbContext.SaveChanges();
            }
        }
        private void SaveSubmissionStatus(string accountNo, int statusId)
        {
            var submissionStatus = new SubmissionStatus();
            var previousData = dbContext.SubmissionStatus.Where(x => x.AccountNumber.Equals(accountNo) && x.PaymentYear.Equals(paymentYear) && x.IsActive == true).ToList();

            if (previousData != null)
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
                        pseDetails.TransmitterTIN = item.Default;
                        break;
                    case "TRANSMITTER CONTROL CODE":
                        pseDetails.TransmitterControlCode = item.Default;
                        break;
                    case "TEST FILE INDICATOR":
                        pseDetails.TestFileIndicator = testFileIndicator ? "T" : " ";
                        break;
                    case "FOREIGN ENTITY INDICATOR":
                        pseDetails.TransmitterForeignEntityIndicator = item.Default;
                        break;
                    case "TRANSMITTER NAME":
                        pseDetails.TransmitterName = item.Default;
                        break;
                    case "TRANSMITTER NAME (CONTINUATION)":
                        pseDetails.TransmitterNameContinued = item.Default;
                        break;
                    case "COMPANY NAME":
                        pseDetails.CompanyName = item.Default;
                        break;
                    case "COMPANY NAME (CONTINUATION)":
                        pseDetails.CompanyNameContinued = item.Default;
                        break;
                    case "COMPANY MAILING ADDRESS":
                        pseDetails.CompanyMailingAddress = item.Default;
                        break;
                    case "COMPANY CITY":
                        pseDetails.CompanyCity = item.Default;
                        break;
                    case "COMPANY STATE":
                        pseDetails.CompanyState = item.Default;
                        break;
                    case "COMPANY ZIP CODE":
                        pseDetails.CompanyZIP = item.Default;
                        break;
                    case "TOTAL NUMBER OF PAYEES":
                        pseDetails.TotalNumberofPayees = numberofPayee;
                        break;
                    case "CONTACT NAME":
                        pseDetails.ContactName = item.Default;
                        break;
                    case "CONTACT TELEPHONE NUMBER & EXTENSION":
                        pseDetails.ContactTelephoneNumber = item.Default;
                        break;
                    case "CONTACT EMAIL ADDRESS":
                        pseDetails.ContactEmailAddress = item.Default;
                        break;
                    case "VENDOR INDICATOR":
                        pseDetails.VendorIndicator = item.Default;
                        break;
                    case "VENDOR NAME":
                        pseDetails.VendorName = item.Default;
                        break;
                    case "VENDOR MAILING ADDRESS":
                        pseDetails.VendorMailingAddress = item.Default;
                        break;
                    case "VENDOR CITY":
                        pseDetails.VendorCity = item.Default;
                        break;
                    case "VENDOR STATE":
                        pseDetails.VendorState = item.Default;
                        break;
                    case "VENDOR ZIP CODE":
                        pseDetails.VendorZIP = item.Default;
                        break;
                    case "VENDOR CONTACT NAME":
                        pseDetails.VendorContactName = item.Default;
                        break;
                    case "VENDOR CONTACT TELEPHONE NUMBER & EXTENSION":
                        pseDetails.VendorContactTelephoneNumber = item.Default;
                        break;
                    case "VENDOR FOREIGN ENTITY INDICATOR":
                        pseDetails.VendorForeignEntityIndicator = item.Default;
                        break;
                    case "COMBINED FEDERAL/STATE FILING PROGRAM":
                        pseDetails.CFSF = item.Default;
                        break;
                    case "PAYER’S TAXPAYER IDENTIFICATION NUMBER (TIN)":
                        pseDetails.PayerTIN = item.Default;
                        break;
                    case "PAYER NAME CONTROL":
                        pseDetails.PayerNameControl = item.Default;
                        break;
                    case "LAST FILING INDICATOR":
                        pseDetails.LastFilingIndicator = item.Default;
                        break;
                    case "TYPE OF RETURN":
                        pseDetails.ReturnType = item.Default;
                        break;
                    case "FIRST PAYER NAME LINE":
                        pseDetails.FirstPayerName = item.Default;
                        break;
                    case "SECOND PAYER NAME LINE":
                        pseDetails.SecondPayerName = item.Default;
                        break;
                    case "TRANSFER AGENT INDICATOR":
                        pseDetails.TransferAgentIndicator = item.Default;
                        break;
                    case "PAYER SHIPPING ADDRESS":
                        pseDetails.PayerShippingAddress = item.Default;
                        break;
                    case "PAYER CITY":
                        pseDetails.PayerCity = item.Default;
                        break;
                    case "PAYER STATE":
                        pseDetails.PayerState = item.Default;
                        break;
                    case "PAYER ZIP CODE":
                        pseDetails.PayerZIP = item.Default;
                        break;
                    case "PAYER’S TELEPHONE NUMBER AND EXTENSION":
                        pseDetails.PayerTelephoneNumber = item.Default;
                        break;
                    default:
                        break;

                }
                pseDetails.DateAdded = DateTime.Now;
            }
        }
        public void ReadFromSchemaFile()
        {
            string path = string.Empty;
            var jsonPath = string.Format(@"{0}App_Data\IRSFileFields.json", HostingEnvironment.ApplicationPhysicalPath);
            var cfsfPath = string.Format(@"{0}App_Data\CFSFStates.json", HostingEnvironment.ApplicationPhysicalPath);
            if (testFileIndicator)
                path = string.Format(@"{0}App_Data\Download\Irs\IRSInputFile_Test.txt", HostingEnvironment.ApplicationPhysicalPath);
            else
                path = string.Format(@"{0}App_Data\Download\Irs\IRSInputFile.txt", HostingEnvironment.ApplicationPhysicalPath);

            var json = File.ReadAllText(jsonPath);
            var cfsfJson = File.ReadAllText(cfsfPath);



            records = JsonConvert.DeserializeObject<List<Records>>(json);
            cfsfStates = JsonConvert.DeserializeObject<List<CFSFStates>>(cfsfJson);

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
            //GenerateBRecord();
            //GenerateCRecord();
            //GenerateKRecord();
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
