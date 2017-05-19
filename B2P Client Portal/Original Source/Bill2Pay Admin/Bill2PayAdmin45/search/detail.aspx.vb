Imports System.Configuration.ConfigurationManager
Imports System.Web.HttpUtility

Public Class detail
    Inherits System.Web.UI.Page

    Dim UserSecurity As B2PAdminBLL.Role

    Private Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        ViewStateUserKey = Context.Session.SessionID
        CheckSession()
        Dim Meta1, Meta2, Meta3, Meta4 As New HtmlMeta
        Meta1.HttpEquiv = "Expires"
        Meta1.Content = "0"

        Meta2.HttpEquiv = "Cache-Control"
        Meta2.Content = "no-cache"

        Meta3.HttpEquiv = "Pragma"
        Meta3.Content = "no-cache"

        Meta4.HttpEquiv = "Refresh"
        Meta4.Content = Convert.ToString((Session.Timeout * 60) + 10) & "; url=/welcome"


        Page.Header.Controls.Add(Meta1)
        Page.Header.Controls.Add(Meta2)
        Page.Header.Controls.Add(Meta3)
        'Page.Header.Controls.Add(Meta4)
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                getTransaction()
            End If

        Catch ex As Exception
            pnlNoResults.Visible = True
            pnlResults.Visible = False
        End Try
    End Sub

    Private Sub CheckSession()
        If Session("SessionID") Is Nothing Then
            Response.Redirect("/welcome")
        End If
    End Sub

    Private Sub getTransaction()
        ViewState("EmailAddress") = ""
        If CStr(Session("ConfirmationNumber")) <> "" Then
            pnlNoResults.Visible = False
            pnlResults.Visible = True

            Dim x As New B2P.Web.Counter.TransactionInformation(CInt(Session("ConfirmationNumber")), CStr(Session("AdminLoginID")), CStr(Session("ClientCode")), "counter")

            Dim y As New B2PAdminBLL.Security(AppSettings("ConnectionString"))
            y.ClientCode = CStr(Session("UserClientCode"))
            y.OfficeID = x.Office_ID
            y.GetOffice()

            y.TransactionId = CInt(Session("ConfirmationNumber"))
            Dim dsNotes As DataSet = y.GetNotes()
            GridView1.DataSource = dsNotes
            GridView1.DataBind()

            litOfficeName.Text = Trim(y.OfficeName)
            litConfirmationNumber.Text = CStr(Session("ConfirmationNumber"))

            If x.Result = B2P.Web.Counter.TransactionInformation.Results.Success Then
                Dim status As B2P.Web.Counter.TransactionInformation.TransactionStatus = x.Status
                Select Case status
                    Case B2P.Web.Counter.TransactionInformation.TransactionStatus.ACHReturn
                        litStatus.Text = "E-Check Return"
                    Case B2P.Web.Counter.TransactionInformation.TransactionStatus.Credited
                        litStatus.Text = "Refund"
                    Case B2P.Web.Counter.TransactionInformation.TransactionStatus.Pending
                        litStatus.Text = "Approved"
                    Case B2P.Web.Counter.TransactionInformation.TransactionStatus.Processed
                        litStatus.Text = "Approved"
                    Case B2P.Web.Counter.TransactionInformation.TransactionStatus.Voided
                        litStatus.Text = "Voided"
                    Case B2P.Web.Counter.TransactionInformation.TransactionStatus.Unknown
                        pnlNoResults.Visible = True
                        pnlResults.Visible = False
                        Exit Sub
                    Case B2P.Web.Counter.TransactionInformation.TransactionStatus.CreditCardFailure
                        litStatus.Text = "Declined"
                End Select

                litCreateUser.Text = HttpUtility.HtmlEncode(x.CreateUserName)
                litSource.Text = HttpUtility.HtmlEncode(x.Source)
                If x.ApplicationDetails.Found Then
                    Dim url As String = x.ApplicationDetails.URL
                    Dim order_ID As String = Mid(url, InStr(url, "=") + 1)
                    Dim redirectURL As String = Left(url, InStr(url, "="))

                    lnkAdditionalLink.Visible = True
                    lnkAdditionalLink.Text = x.ApplicationDetails.Name
                    lnkAdditionalLink.NavigateUrl = redirectURL & B2P.Common.Encryption.EncryptForURL(String.Format("{0}|{1}", Now.AddMinutes(20), order_ID))
                End If

                If x.Profile_ID > 0 Then
                    Session("Profile_ID") = x.Profile_ID
                    lnkProfileDetail.Visible = True
                    If UCase(x.Source) = "TEXTPAY" Then
                        lnkProfileDetail.NavigateUrl = "/textpay/textpaydetail.aspx"
                    Else
                        lnkProfileDetail.NavigateUrl = "/profile/profiledetail.aspx?source=transaction"
                    End If

                Else
                    lnkProfileDetail.Visible = False
                End If

                litClientName.Text = x.ClientName 'HttpUtility.HtmlEncode(x.ClientName)
                litDate.Text = HttpUtility.HtmlEncode(CStr(x.TransactionDate)) & " " & B2P.Objects.Client.GetClient(x.ClientCode).TimeZoneSuffix
                litPhone.Text = HttpUtility.HtmlEncode(x.PayeePhone)
                txtNotes.Text = baseclass.SafeEncode(x.UserComments)
                ViewState("ClientCode") = x.ClientCode
                ViewState("EmailAddress") = x.EmailAddress
                ViewState("TransactionDate") = x.TransactionDate

                Session("MyItems") = x.Items
                litItems.Text = ""
                Dim amount As Double

                For Each item As B2P.Payment.PaymentBase.TransactionItems.LineItem In x.Items
                    amount = CDbl(HttpUtility.HtmlEncode(CStr(item.Amount)))
                    litItems.Text = String.Format("{0}{1}<br />", litItems.Text, (String.Format("{0} - {1} {2} {3} - {4:$#,###.00}", HttpUtility.HtmlEncode(item.ProductName), HttpUtility.HtmlEncode(item.AccountNumber1), HttpUtility.HtmlEncode(item.AccountNumber2), HttpUtility.HtmlEncode(item.AccountNumber3), amount)))
                Next

                Select Case x.PaymentType
                    Case B2P.Web.Counter.TransactionInformation.PaymentTypes.BankAccount
                        litType.Text = "Bank Account - " & HttpUtility.HtmlEncode(x.BankAccountNumber)
                        ViewState("PaymentType") = 1
                    Case B2P.Web.Counter.TransactionInformation.PaymentTypes.CreditCard
                        litType.Text = String.Format("{0} - {1} - {2}", HttpUtility.HtmlEncode(x.CardIssuer), HttpUtility.HtmlEncode(x.CreditCardNumber), HttpUtility.HtmlEncode(x.CardNetwork))
                        ViewState("PaymentType") = 0
                    Case Else
                        litType.Text = "Other"

                End Select
                litName.Text = String.Format("{0} {1}", HttpUtility.HtmlEncode(x.PayeeFirstName), HttpUtility.HtmlEncode(x.PayeeLastName))
                Dim total As Double
                Dim fee As Double = CDbl(HttpUtility.HtmlEncode(CStr(x.Items.TotalFee)))
                'litFee.Text = String.Format("{0:c}", x.Items.TotalFee)
                litFee.Text = String.Format("{0:c}", fee)

                total = x.Items.TotalFee + x.Items.TotalAmount
                'litTotal.Text = String.Format("{0:c}", (x.Items.TotalFee + x.Items.TotalAmount))
                litTotal.Text = String.Format("{0:c}", total)
            Else
                pnlNoResults.Visible = True
                pnlResults.Visible = False
            End If
            getDetails()
        Else
            pnlNoResults.Visible = True
            pnlResults.Visible = False
        End If
    End Sub

    Private Sub getDetails()
        Try
            UserSecurity = CType(Session("RoleInfo"), B2PAdminBLL.Role)

            Dim x As New B2PAdminBLL.TransactionSearch.SearchDetails(CInt(Session("ConfirmationNumber")), CStr(Session("AdminLoginID")), CStr(Session("ClientCode")))
            Session("Type") = ""
            Select Case UCase(x.SupplementalDetail)
                Case "CREDIT"
                    pnlCredit.Visible = True
                    Dim cred As New B2PAdminBLL.TransactionSearch.SearchDetails.CreditDetails
                    cred = x.CreditDetail
                    litRefundCreditDate.Text = HttpUtility.HtmlEncode(cred.CreditDate)
                    litRefundCreditCard.Text = HttpUtility.HtmlEncode(cred.ClearCreditCardNumber)
                    litRefundAmount.Text = HttpUtility.HtmlEncode(cred.Amount)
                    litRefundProcessDate.Text = HttpUtility.HtmlEncode(cred.ProcessDate)
                    litRefundBy.Text = HttpUtility.HtmlEncode(cred.UserName)
                    btnEmail.Enabled = False
                Case "VOID"
                    pnlVoid.Visible = True
                    Dim void As New B2PAdminBLL.TransactionSearch.SearchDetails.VoidDetails
                    void = x.VoidDetail
                    litVoidDate.Text = HttpUtility.HtmlEncode(void.VoidDate)
                    litVoidBy.Text = HttpUtility.HtmlEncode(void.VoidUser)
                    btnEmail.Enabled = False
                Case "ACH RETURN"
                    pnlACHReturn.Visible = True
                    Dim achRet As New B2PAdminBLL.TransactionSearch.SearchDetails.ACHReturnDetails
                    achRet = x.ReturnDetail
                    litACHReturnCode.Text = HttpUtility.HtmlEncode(achRet.ReasonCode)
                    litACHOriginalTrace.Text = HttpUtility.HtmlEncode(achRet.OriginalTraceNumber)
                    litACHReturnTrace.Text = HttpUtility.HtmlEncode(achRet.ReturnTrace)
                    litACHReturnDesc.Text = HttpUtility.HtmlEncode(achRet.FriendlyReason)
                    litACHReturnDate.Text = HttpUtility.HtmlEncode(achRet.ReturnDate)
                    btnEmail.Enabled = False
                Case "ACH DETAIL"
                    Session("Type") = "ACH"
                    pnlACHDetails.Visible = True
                    Dim achDet As B2PAdminBLL.TransactionSearch.SearchDetails.ACHDetails = x.ACHDetail
                    If Trim(achDet.AllowVoid) <> "" And UserSecurity.SearchScreenVoid Then
                        btnVoid.Enabled = True
                    Else
                        btnVoid.Enabled = False
                    End If
                    litACHDetailsBankAccountNumber.Text = HttpUtility.HtmlEncode(achDet.ClearBankAccountNumber)
                    litACHDetailsBankAccountType.Text = HttpUtility.HtmlEncode(achDet.AccountType)
                    litACHDetailsBankRoutingNumber.Text = HttpUtility.HtmlEncode(achDet.BankRoutingNumber)
                    litACHDetailsProcessDate.Text = HttpUtility.HtmlEncode(achDet.ProcessDate)
                    litPayeeAddress.Text = String.Format("Billing Address: {0}<br /><br />", HttpUtility.HtmlEncode(Trim(achDet.PayeeAddress)))
                    If Trim(achDet.Address1) <> "" Then
                        litDemoAddress1.Visible = True
                        litDemoAddress1.Text = String.Format("Address: {0}<br />", HttpUtility.HtmlEncode(achDet.Address1))
                    End If
                    If Trim(achDet.Address2) <> "" Then
                        litDemoAddress2.Visible = True
                        litDemoAddress2.Text = String.Format("Address 2: {0}<br />", HttpUtility.HtmlEncode(achDet.Address2))
                    End If
                    If Trim(achDet.City) <> "" Or Trim(achDet.State) <> "" Or Trim(achDet.Zip) <> "" Then
                        litDemoCityStateZip.Visible = True
                        litDemoCityStateZip.Text = String.Format("City, State, Zip: {0}, {1}, {2}<br />", HttpUtility.HtmlEncode(achDet.City), HttpUtility.HtmlEncode(achDet.State), HttpUtility.HtmlEncode(achDet.Zip))
                    End If
                    If Trim(achDet.CellPhone) <> "" Then
                        litDemoCellPhone.Visible = True
                        litDemoCellPhone.Text = String.Format("Cell Phone: {0}<br />", HttpUtility.HtmlEncode(achDet.CellPhone))
                    End If
                    If Trim(achDet.HomePhone) <> "" Then
                        litDemoHomePhone.Visible = True
                        litDemoHomePhone.Text = String.Format("Home Phone: {0}<br />", HttpUtility.HtmlEncode(achDet.HomePhone))
                    End If
                    If Trim(achDet.WorkPhone) <> "" Then
                        litDemoWorkPhone.Visible = True
                        litDemoWorkPhone.Text = String.Format("Home Phone: {0}<br />", HttpUtility.HtmlEncode(achDet.WorkPhone))
                    End If
                    If Trim(achDet.Email) <> "" Then
                        litDemoEmailAddress.Visible = True
                        litDemoEmailAddress.Text = String.Format("Email Address: {0}<br />", HttpUtility.HtmlEncode(achDet.Email))
                    End If
                    If Trim(achDet.UserField1) <> "" Then
                        litUserField1.Visible = True
                        litUserField1.Text = String.Format("Field 1: {0}<br />", HttpUtility.HtmlEncode(achDet.UserField1))
                    End If
                    If Trim(achDet.UserField2) <> "" Then
                        litUserField2.Visible = True
                        litUserField2.Text = String.Format("Field 2: {0}<br />", HttpUtility.HtmlEncode(achDet.UserField2))
                    End If
                    If Trim(achDet.UserField3) <> "" Then
                        litUserField3.Visible = True
                        litUserField3.Text = String.Format("Field 3: {0}<br />", HttpUtility.HtmlEncode(achDet.UserField3))
                    End If
                    If Trim(achDet.UserField4) <> "" Then
                        litUserField4.Visible = True
                        litUserField4.Text = String.Format("Field 4: {0}<br />", HttpUtility.HtmlEncode(achDet.UserField4))
                    End If
                    If Trim(achDet.UserField5) <> "" Then
                        litUserField5.Visible = True
                        litUserField5.Text = String.Format("Field 5: {0}<br />", HttpUtility.HtmlEncode(achDet.UserField5))
                    End If
                    btnEmail.Enabled = True

                Case "CC DETAIL"
                    Session("Type") = "CC"
                    pnlCCDetails.Visible = True
                    Dim ccDet As B2PAdminBLL.TransactionSearch.SearchDetails.CCDetails = x.CCDetail
                    If Trim(ccDet.AllowVoid) <> "" And UserSecurity.SearchScreenVoid Then
                        btnVoid.Enabled = True
                    End If
                    If Trim(ccDet.AllowCredit) <> "" And UserSecurity.SearchScreenCredit Then
                        btnReturn.Enabled = True
                    End If
                    litCCDetailsAuthoCode.Text = HttpUtility.HtmlEncode(ccDet.AuthCode)
                    litCCDetailsProcessDate.Text = HttpUtility.HtmlEncode(ccDet.ProcessDate)
                    litCCDetailsFirst6.Text = HttpUtility.HtmlEncode(ccDet.FirstSix)
                    litPayeeAddress.Text = String.Format("Billing Address: {0}<br /><br />", HttpUtility.HtmlEncode(Trim(ccDet.PayeeAddress)))
                    If Trim(ccDet.Address1) <> "" Then
                        litDemoAddress1.Visible = True
                        litDemoAddress1.Text = String.Format("Address: {0}<br />", HttpUtility.HtmlEncode(ccDet.Address1))
                    End If
                    If Trim(ccDet.Address2) <> "" Then
                        litDemoAddress2.Visible = True
                        litDemoAddress2.Text = String.Format("Address 2: {0}<br />", HttpUtility.HtmlEncode(ccDet.Address2))
                    End If

                    If Trim(ccDet.City) <> "" Or Trim(ccDet.State) <> "" Or Trim(ccDet.Zip) <> "" Then
                        litDemoCityStateZip.Visible = True
                        litDemoCityStateZip.Text = String.Format("City, State, Zip: {0}, {1}, {2}<br />", HttpUtility.HtmlEncode(ccDet.City), HttpUtility.HtmlEncode(ccDet.State), HttpUtility.HtmlEncode(ccDet.Zip))
                    End If
                    If Trim(ccDet.CellPhone) <> "" Then
                        litDemoCellPhone.Visible = True
                        litDemoCellPhone.Text = String.Format("Cell Phone: {0}<br />", HttpUtility.HtmlEncode(ccDet.CellPhone))
                    End If
                    If Trim(ccDet.HomePhone) <> "" Then
                        litDemoHomePhone.Visible = True
                        litDemoHomePhone.Text = String.Format("Home Phone: {0}<br />", HttpUtility.HtmlEncode(ccDet.HomePhone))
                    End If
                    If Trim(ccDet.WorkPhone) <> "" Then
                        litDemoWorkPhone.Visible = True
                        litDemoWorkPhone.Text = String.Format("Home Phone: {0}<br />", HttpUtility.HtmlEncode(ccDet.WorkPhone))
                    End If
                    If Trim(ccDet.Email) <> "" Then
                        litDemoEmailAddress.Visible = True
                        litDemoEmailAddress.Text = String.Format("Email Address: {0}<br />", HttpUtility.HtmlEncode(ccDet.Email))
                    End If
                    If Trim(ccDet.UserField1) <> "" Then
                        litUserField1.Visible = True
                        litUserField1.Text = String.Format("Field 1: {0}<br />", HttpUtility.HtmlEncode(ccDet.UserField1))
                    End If
                    If Trim(ccDet.UserField2) <> "" Then
                        litUserField2.Visible = True
                        litUserField2.Text = String.Format("Field 2: {0}<br />", HttpUtility.HtmlEncode(ccDet.UserField2))
                    End If
                    If Trim(ccDet.UserField3) <> "" Then
                        litUserField3.Visible = True
                        litUserField3.Text = String.Format("Field 3: {0}<br />", HttpUtility.HtmlEncode(ccDet.UserField3))
                    End If
                    If Trim(ccDet.UserField4) <> "" Then
                        litUserField4.Visible = True
                        litUserField4.Text = String.Format("Field 4: {0}<br />", HttpUtility.HtmlEncode(ccDet.UserField4))
                    End If
                    If Trim(ccDet.UserField5) <> "" Then
                        litUserField5.Visible = True
                        litUserField5.Text = String.Format("Field 5: {0}<br />", HttpUtility.HtmlEncode(ccDet.UserField5))
                    End If
                    btnEmail.Enabled = True
                Case "CC FAILED"
                    pnlFailed.Visible = True
                    Dim ccFail As New B2PAdminBLL.TransactionSearch.SearchDetails.FailedCCDetails
                    ccFail = x.FailedCCDetail
                    litFailAVS.Text = HttpUtility.HtmlEncode(ccFail.AVSCode)
                    litFailDesc.Text = HttpUtility.HtmlEncode(ccFail.ErrorMessage)
                    litFailZip.Text = HttpUtility.HtmlEncode(ccFail.BillZip)
                    litFailItemAuth.Text = HttpUtility.HtmlEncode(ccFail.ItemAuthCode)
                    litFailFeeAuth.Text = HttpUtility.HtmlEncode(ccFail.FeeAuthCode)
                    If ccFail.ItemAuthCode <> "" Then
                        If ccFail.ItemReverseStatus = "0" Then
                            litReverseStatus.Text = "Unable to reverse"
                        Else
                            litReverseStatus.Text = "Reversed"
                        End If
                    Else
                        litReverseStatus.Text = ""
                    End If
                    btnEmail.Enabled = False

                Case "NORMAL"
                    pnlNoDetail.Visible = True

                Case Else
                    pnlNoDetail.Visible = True
            End Select
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

#Region " Form Events "
    Private Sub btnVoid_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnVoid.Click
        Dim rPost As New B2P.Payment.Reversal

        'Dim x As New B2PAdminBLL.Transactions(AppSettings("ConnectionString"))
        'Dim result As String
        If CStr(Session("Type")) = "ACH" Then
            If rPost.VoidACHTransaction(CInt(Session("ConfirmationNumber")), Session("SecurityID")) Then
                btnVoid.Enabled = False
                pnlNoDetail.Visible = False
                pnlACHDetails.Visible = False
                litMessage.Text = "Void processed"
                getTransaction()
            Else
                litMessage.Text = "Void not processed"
            End If
            'result = CStr(x.VoidACHTransaction(CInt(Session("ConfirmationNumber")), CStr(Session("AdminLoginID"))))
            'Select Case result
            '    Case CStr(-1), CStr(-2), CStr(-3), CStr(-4)
            '        litMessage.Text = "Void not processed"
            '    Case Else
            '        btnVoid.Enabled = False
            '        pnlNoDetail.Visible = False
            '        pnlACHDetails.Visible = False
            '        litMessage.Text = "Void processed"
            '        getTransaction()
            'End Select
        Else
            If rPost.VoidCCTransaction(CInt(Session("ConfirmationNumber")), Session("SecurityID")) Then
                btnVoid.Enabled = False
                pnlCCDetails.Visible = False
                pnlNoDetail.Visible = False
                litMessage.Text = "Void processed"
                getTransaction()
            Else
                litMessage.Text = "Void not processed"
            End If
            'result = CStr(x.VoidCCTransaction(CInt(Session("ConfirmationNumber")), CStr(Session("AdminLoginID"))))
            'Select Case result.ToUpper()
            '    Case "ALREADY CREDITTED", "INVALID TRANSACTION TYPE"
            '        litMessage.Text = "Void not processed"
            '    Case Else
            '        btnVoid.Enabled = False
            '        pnlCCDetails.Visible = False
            '        pnlNoDetail.Visible = False
            '        litMessage.Text = "Void processed"
            '        getTransaction()
            'End Select
        End If
    End Sub

    Private Sub btnReturn_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReturn.Click
        Dim rCredit As New B2P.Payment.Reversal
        Dim x As New B2P.Payment.Reversal.CreditResponse
        'Dim x As New B2PAdminBLL.Transactions(AppSettings("ConnectionString"))
        'Dim result As String = String.Empty
       
        x = rCredit.CreditCCTransaction(CInt(Session("ConfirmationNumber")), Session("SecurityID"))
        Select Case x.Result
            Case B2P.Payment.Reversal.CreditResponse.Results.AlreadyCredited
                litMessage.Text = "Alread credited"
            Case B2P.Payment.Reversal.CreditResponse.Results.Success
                btnReturn.Enabled = False
                pnlNoDetail.Visible = False
                pnlCCDetails.Visible = False
                pnlACHDetails.Visible = False
                litMessage.Text = "Refund processed"
                getTransaction()
                'success 
            Case B2P.Payment.Reversal.CreditResponse.Results.Error
                litMessage.Text = "Refund not processed"
            Case B2P.Payment.Reversal.CreditResponse.Results.InvalidTransactionType
                litMessage.Text = "Invalid transaction type"
            Case Else
                litMessage.Text = "Refund not processed"
        End Select
        'Select Case result.ToUpper()
        '    Case "ALREADY CREDITTED", "INVALID TRANSACTION TYPE"
        '        litMessage.Text = "Refund not processed"
        '    Case Else
        '        btnReturn.Enabled = False
        '        pnlNoDetail.Visible = False
        '        pnlCCDetails.Visible = False
        '        pnlACHDetails.Visible = False
        '        litMessage.Text = "Refund processed"
        '        getTransaction()
        '        'success 
        'End Select
    End Sub

    Protected Sub ButtonOk3_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonOk3.Click

        If txtNotesAdd.Text <> "" Then
            Dim y As New B2PAdminBLL.Security(AppSettings("ConnectionString"))
            y.TransactionId = CInt(Session("ConfirmationNumber"))
            y.Comments = HttpUtility.HtmlEncode(txtNotesAdd.Text)
            y.UserName = CStr(Session("AdminLoginID"))
            y.AddNote()
            txtNotesAdd.Text = ""
            'getTransaction()
            Response.Redirect("detail.aspx", False)
        End If
    End Sub

    Private Sub ButtonOk4_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonOk4.Click
        Dim x As New B2PAdminBLL.EmailConfirmations
        Dim PmtType As Integer = CInt(ViewState("PaymentType"))
        x.ConfirmationNumber = CInt(Session("ConfirmationNumber"))
        x.ClientCode = ViewState("ClientCode").ToString
        x.EmailAddress = txtEmail.Text
        x.TransDate = CDate(ViewState("TransactionDate"))
        x.LineItems = CType(Session("MyItems"), B2P.Payment.PaymentBase.TransactionItems)


        If x.SendConfirmation(CType(PmtType, B2P.Common.Enumerations.PaymentTypes)) Then
            litMessage.Text = "Confirmation email was successful."
        Else
            litMessage.Text = "Confirmation email NOT successful."
        End If
    End Sub

    Private Sub ModalPopupExtender4_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles ModalPopupExtender4.Load
        If Not IsPostBack Then
            txtEmail.Text = ViewState("EmailAddress").ToString
        End If

    End Sub

#End Region

End Class