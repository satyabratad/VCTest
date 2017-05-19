Imports System.Configuration.ConfigurationManager
Imports Telerik.Web.UI
Imports System.Threading
Imports System.Drawing
Imports B2P.Common
Imports System.Xml

Public Class AddBlock
    Inherits baseclass

    Dim UserSecurity As B2PAdminBLL.Role
    Dim x As New B2PAdminBLL.Security(AppSettings("ConnectionString"))
    Dim acct As New B2PAdminBLL.BlockAccount.Account
    Dim profileAccount As New B2PAdminBLL.BlockAccount.ProfileAccount
    Dim ds As New DataSet

#Region " Page Procedures "
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
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        UserSecurity = CType(Session("RoleInfo"), B2PAdminBLL.Role)

        If Not UserSecurity.BlockAccount Then
            Response.Redirect("/error/default.aspx?type=security")
        End If
        If Not IsPostBack Then
            loadProducts()
            LoadData()
        End If
        'DebugCoding()

    End Sub

#End Region

    Private Sub DebugCoding()
        Dim bk As New B2P.Common.BlockedAccounts.BlockedAccountResults
        bk = B2P.Common.BlockedAccounts.CheckForBlockedAccount("aa1", "Tax Payment", "32154", "", "")

    End Sub


    Private Sub LoadData()
        Try
            UserSecurity = CType(Session("RoleInfo"), B2PAdminBLL.Role)
            x.ClientCode = CStr(Session("ClientCode"))
            x.Role = UserSecurity.UserRole
            x.UserLogin = CStr(Session("AdminLoginID"))

            ds = x.LoadDropDownBoxes
            Session("UserClientCode") = Session("ClientCode")
            If CStr(Session("ClientCode")) = "B2P" Then
                ddlAUClient.DataSource = ds.Tables(0)
                ddlAUClient.DataTextField = ds.Tables(0).Columns("ClientCode").ColumnName.ToString()
                ddlAUClient.DataBind()
                pnlClient.Visible = True
            Else
                pnlClient.Visible = False
            End If
            ds = Nothing
        Catch te As ThreadAbortException

        Catch ex As Exception
            B2P.Common.Logging.LogError("B2P Admin", "Error during loading data " & ex.ToString, B2P.Common.Logging.NotifySupport.No)
            Response.Redirect("/error/")
            HttpContext.Current.ApplicationInstance.CompleteRequest()
        End Try
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks>Get AmountDueSourc - Lookup flag This would be on web page - Copied from Profile system</remarks>
    Private Sub getData()
        Try
            pnlHeaders.Visible = True

            Dim z As New B2P.Objects.Product(CStr(Session("UserClientCode")), ddlProductList.SelectedValue, B2P.Common.Enumerations.TransactionSources.NotSpecified)
            If z.AmountDueSource = B2P.Common.Enumerations.AmountDueSources.Lookup Or z.AmountDueSource = B2P.Common.Enumerations.AmountDueSources.Table Then
                pnlLookupButton.Visible = True
                pnlClear.Visible = True
                pnlButton.Visible = False
                pnlReEnterAccount.Visible = False
                pnlBlockDetail.Visible = False
                BuildLookupForm()
            Else
                pnlLookupButton.Visible = False
                pnlButton.Visible = True
                pnlClear.Visible = True
                pnlReEnterAccount.Visible = True
                pnlBlockDetail.Visible = False
                BuildForm()
            End If

        Catch ex As Exception
            B2P.Common.Logging.LogError("B2P", "Error during product lookup " & ex.ToString, B2P.Common.Logging.NotifySupport.No)
            Response.Redirect("/errors/")
        End Try

    End Sub

    Private Sub BuildLookupForm()
        Try
            Dim productSelected As String = Replace(HttpUtility.HtmlEncode(ddlProductList.SelectedValue), "&amp;", "&")
            Dim CurrentCategory As New B2P.Objects.Product(CStr(Session("UserClientCode")), productSelected, B2P.Common.Enumerations.TransactionSources.NotSpecified)
            With CurrentCategory.WebOptions
                If .AccountIDField1.Enabled = True Then
                    lblAccountNumber1.Text = HttpUtility.HtmlEncode(.AccountIDField1.Label)
                    txtAccountNumber1.MaxLength = .AccountIDField1.MaximumLength
                    lblAccountNumber1d.Text = HttpUtility.HtmlEncode(.AccountIDField1.Label)
                    'lblAccountDescription1.Text = HttpUtility.HtmlEncode(.AccountIDField1.Description)                
                    pnlAccount1.Visible = True
                    pnlAccount1d.Visible = True
                    SetValidatorProperties(txtAccountNumber1_FilteredTextBoxExtender, regAccountNumber1, reqAccountNumber1, .AccountIDField1)
                Else
                    pnlAccount1.Visible = False
                End If

                If .AccountIDField2.Enabled = True Then
                    lblAccountNumber2.Text = HttpUtility.HtmlEncode(.AccountIDField2.Label)
                    txtAccountNumber2.MaxLength = .AccountIDField2.MaximumLength
                    lblAccountNumber2d.Text = HttpUtility.HtmlEncode(.AccountIDField2.Label)
                    'lblAccountDescription2.Text = HttpUtility.HtmlEncode(.AccountIDField2.Description)               
                    pnlAccount2.Visible = True
                    pnlAccount2d.Visible = True
                    SetValidatorProperties(txtAccountNumber2_FilteredTextBoxExtender, regAccountNumber2, reqAccountNumber2, .AccountIDField2)
                Else
                    pnlAccount2.Visible = False

                End If

                If .AccountIDField3.Enabled = True Then
                    lblAccountNumber3.Text = HttpUtility.HtmlEncode(.AccountIDField3.Label)
                    txtAccountNumber3.MaxLength = .AccountIDField3.MaximumLength
                    lblAccountNumber3d.Text = HttpUtility.HtmlEncode(.AccountIDField3.Label)
                    'lblAccountDescription3.Text = HttpUtility.HtmlEncode(.AccountIDField3.Description)
                    pnlAccount3.Visible = True
                    pnlAccount3d.Visible = True
                    SetValidatorProperties(txtAccountNumber3_FilteredTextBoxExtender, regAccountNumber3, reqAccountNumber3, .AccountIDField3)
                Else
                    pnlAccount3.Visible = False
                End If


            End With
        Catch ex As Exception
            B2P.Common.Logging.LogError("B2P", "Error during product lookup " & ex.ToString, B2P.Common.Logging.NotifySupport.No)
            Response.Redirect("/errors/")
        End Try

    End Sub

    Private Sub BuildForm()
        Try
            Dim productSelected As String = Replace(HttpUtility.HtmlEncode(ddlProductList.SelectedValue), "&amp;", "&")
            Dim CurrentCategory As New B2P.Objects.Product(CStr(Session("UserClientCode")), productSelected, B2P.Common.Enumerations.TransactionSources.NotSpecified)

            pnlAccount1.Visible = False
            pnlAccount1r.Visible = False
            pnlAccount1d.Visible = False

            With CurrentCategory.WebOptions
                If .AccountIDField1.Enabled = True Then
                    lblAccountNumber1.Text = HttpUtility.HtmlEncode(.AccountIDField1.Label)
                    lblAccountNumber1r.Text = HttpUtility.HtmlEncode(.AccountIDField1.Label)
                    lblAccountNumber1d.Text = HttpUtility.HtmlEncode(.AccountIDField1.Label)
                    txtAccountNumber1.MaxLength = .AccountIDField1.MaximumLength
                    txtAccountNumber1r.MaxLength = .AccountIDField1.MaximumLength
                    pnlAccount1.Visible = True
                    pnlAccount1r.Visible = True
                    pnlAccount1d.Visible = True
                    SetValidatorProperties(txtAccountNumber1_FilteredTextBoxExtender, regAccountNumber1, reqAccountNumber1, .AccountIDField1)
                    SetValidatorProperties(txtAccountNumber1r_FilteredTextBoxExtender, regAccountNumber1r, reqAccountNumber1r, .AccountIDField1)
                End If

                If .AccountIDField2.Enabled = True Then
                    lblAccountNumber2.Text = HttpUtility.HtmlEncode(.AccountIDField2.Label)
                    lblAccountNumber2r.Text = HttpUtility.HtmlEncode(.AccountIDField2.Label)
                    lblAccountNumber2d.Text = HttpUtility.HtmlEncode(.AccountIDField2.Label)
                    txtAccountNumber2.MaxLength = .AccountIDField2.MaximumLength
                    txtAccountNumber2r.MaxLength = .AccountIDField2.MaximumLength
                    pnlAccount2.Visible = True
                    pnlAccount2r.Visible = True
                    pnlAccount2d.Visible = True
                    SetValidatorProperties(txtAccountNumber2_FilteredTextBoxExtender, regAccountNumber2, reqAccountNumber2, .AccountIDField2)
                    SetValidatorProperties(txtAccountNumber2r_FilteredTextBoxExtender, regAccountNumber2r, reqAccountNumber2r, .AccountIDField2)

                End If

                If .AccountIDField3.Enabled = True Then
                    lblAccountNumber3.Text = HttpUtility.HtmlEncode(.AccountIDField3.Label)
                    lblAccountNumber3r.Text = HttpUtility.HtmlEncode(.AccountIDField3.Label)
                    lblAccountNumber3d.Text = HttpUtility.HtmlEncode(.AccountIDField3.Label)
                    txtAccountNumber3.MaxLength = .AccountIDField3.MaximumLength
                    txtAccountNumber3r.MaxLength = .AccountIDField3.MaximumLength
                    pnlAccount3.Visible = True
                    pnlAccount3r.Visible = True
                    pnlAccount3d.Visible = True
                    SetValidatorProperties(txtAccountNumber3_FilteredTextBoxExtender, regAccountNumber3, reqAccountNumber3, .AccountIDField3)
                    SetValidatorProperties(txtAccountNumber3r_FilteredTextBoxExtender, regAccountNumber3r, reqAccountNumber3r, .AccountIDField3)
                End If


            End With
        Catch ex As Exception
            B2P.Common.Logging.LogError("B2P", "Error during product lookup ReEntery " & ex.ToString, B2P.Common.Logging.NotifySupport.No)
            Response.Redirect("/errors/")
        End Try

    End Sub

    Private Sub SetValidatorProperties(ByVal filter As AjaxControlToolkit.FilteredTextBoxExtender, ByVal validator As Global.PeterBlum.DES.Web.WebControls.RegexValidator, ByVal requiredValidator As Global.PeterBlum.DES.Web.WebControls.RequiredTextValidator, ByVal customField As B2P.Objects.WebConfiguration.CustomField)

        filter.Enabled = True
        validator.Enabled = True
        requiredValidator.Enabled = True

        Dim MinimumLength As Integer = 0
        If customField.Required = True Then
            MinimumLength = 1
        End If

        Select Case customField.DataType
            Case B2P.Objects.WebConfiguration.CustomField.FieldDataType.All
                filter.FilterType = AjaxControlToolkit.FilterTypes.LowercaseLetters Or AjaxControlToolkit.FilterTypes.Numbers Or AjaxControlToolkit.FilterTypes.UppercaseLetters Or AjaxControlToolkit.FilterTypes.Custom
                filter.ValidChars = " -'&,./"
                validator.Expression = String.Format("^(.{{1,{0}}})$", customField.MaximumLength)
                validator.ErrorMessage = String.Format("{0} format is invalid", customField.Label)
                validator.ToolTip = validator.ErrorMessage
                If customField.Required = True Then
                    requiredValidator.ToolTip = String.Format("{0} is required", customField.Label)
                    'requiredValidator.ErrorMessage = String.Format("{0} is required", customField.Label)
                    requiredValidator.ErrorMessage = "Required"
                    requiredValidator.Enabled = True
                Else
                    requiredValidator.Enabled = False
                End If


            Case B2P.Objects.WebConfiguration.CustomField.FieldDataType.Alphanumeric
                filter.FilterType = AjaxControlToolkit.FilterTypes.LowercaseLetters Or AjaxControlToolkit.FilterTypes.Numbers Or AjaxControlToolkit.FilterTypes.UppercaseLetters
                validator.Expression = String.Format("^[a-zA-Z0-9]{{{0},{1}}}$", MinimumLength, customField.MaximumLength)
                validator.ErrorMessage = String.Format("{0} format is invalid", customField.Label)
                validator.ToolTip = validator.ErrorMessage
                If customField.Required = True Then
                    requiredValidator.ToolTip = String.Format("{0} is required", customField.Label)
                    requiredValidator.ErrorMessage = "Required" 'String.Format("{0} is required", customField.Label)
                    requiredValidator.Enabled = True
                Else
                    requiredValidator.Enabled = False
                End If
                filter.Enabled = True
            Case B2P.Objects.WebConfiguration.CustomField.FieldDataType.Custom
            Case B2P.Objects.WebConfiguration.CustomField.FieldDataType.Numeric
                filter.FilterType = AjaxControlToolkit.FilterTypes.Numbers
                validator.ErrorMessage = String.Format("{0} format is invalid", customField.Label)
                validator.ToolTip = validator.ErrorMessage
                ' to test zipcode - validator.ValidationExpression = "^\d{5}(-\d{4})?$"
                validator.Expression = String.Format("^[0-9]{{{0},{1}}}$", MinimumLength, customField.MaximumLength)
                If customField.Required = True Then
                    requiredValidator.ToolTip = String.Format("{0} is required", customField.Label)
                    requiredValidator.ErrorMessage = "Required" 'String.Format("{0} is required", customField.Label)
                    requiredValidator.Enabled = True
                Else
                    requiredValidator.Enabled = False
                End If
                filter.Enabled = True
        End Select

    End Sub

    Private Sub loadProducts()
        Try

            ddlProductList.Items.Clear()
            ddlProductList.Items.Add(New RadComboBoxItem("Select Product", ""))
            'ddlProductList.DataSource = B2P.Profile.Product.ListProducts(CStr(Session("UserClientCode")))
            ddlProductList.DataSource = B2P.Objects.Product.ListProducts(CStr(Session("UserClientCode")), Enumerations.TransactionSources.NotSpecified)
            ddlProductList.DataBind()

        Catch ex As Exception
            'lblMessageTitle.Text = "Error"
            'lblMessage.Text = "Message: An unexpected error has occurred."
            'btnSubmit.Enabled = False
            'mdlPopup.Show()
        End Try
    End Sub

    Private Sub ClearScreen()

        pnlHeaders.Visible = False
        pnlAccount1.Visible = False
        pnlAccount1r.Visible = False
        pnlAccount2.Visible = False
        pnlAccount2r.Visible = False
        pnlAccount3.Visible = False
        pnlAccount3r.Visible = False
        pnlReEnterAccount.Visible = False
        pnlLookupButton.Visible = False
        pnlButton.Visible = False
        pnlClear.Visible = False

    End Sub

    Private Function ValidateLookup() As Boolean
        Dim x As New B2P.ClientInterface.Manager.ClientInterface() ' B2P.ClientInterface.Custom.AccountSearch
        Dim sr As New B2P.ClientInterface.Manager.ClientInterfaceWS.SearchResults
        Dim sp As New B2P.ClientInterface.Manager.ClientInterfaceWS.SearchParameters
        Dim productSelected As String = HttpUtility.HtmlEncode(ddlProductList.SelectedValue)

        Dim LookupSuccessful As Boolean = False

        sp.ClientCode = Session("UserClientCode")
        sp.ProductName = productSelected
        sp.AccountNumber1 = HttpUtility.HtmlEncode(Trim(txtAccountNumber1.Text))

        If Trim(txtAccountNumber2.Text) <> "" Then
            sp.AccountNumber2 = HttpUtility.HtmlEncode(Trim(txtAccountNumber2.Text))
        Else
            sp.AccountNumber2 = ""
        End If
        If Trim(txtAccountNumber3.Text) <> "" Then
            sp.AccountNumber3 = HttpUtility.HtmlEncode(Trim(txtAccountNumber3.Text))
        Else
            sp.AccountNumber3 = ""
        End If

        sr = B2P.ClientInterface.Manager.ClientInterface.GetClientData(sp)
        Session("ClientData") = sr

        Select Case sr.SearchResult
            Case B2P.ClientInterface.Manager.ClientInterfaceWS.StatusCodes.Success
                acct.ClientCode = sp.ClientCode
                acct.ProductName = sp.ProductName
                acct.AccountNumber1 = sp.AccountNumber1
                acct.AccountNumber2 = sp.AccountNumber2
                acct.AccountNumber3 = sp.AccountNumber3
                acct.FirstName = sr.Demographics.FirstName.Value.ToString()
                acct.LastName = sr.Demographics.LastName.Value.ToString()
                LookupSuccessful = True

            Case B2P.ClientInterface.Manager.ClientInterfaceWS.StatusCodes.ErrorOccurred
                lblMsg.Text = "Message: Oops, an error has occurred during the account lookup."
                lblMsg.ForeColor = Color.Red
                lblMsg.Font.Italic = True
                pnlMessages.Visible = True
                LookupSuccessful = False

            Case B2P.ClientInterface.Manager.ClientInterfaceWS.StatusCodes.LookupNotEnabled
                lblMsg.Text = "Message: Lookup is not enabled for this account."
                lblMsg.ForeColor = Color.Red
                lblMsg.Font.Italic = True
                pnlMessages.Visible = True
                LookupSuccessful = False

            Case B2P.ClientInterface.Manager.ClientInterfaceWS.StatusCodes.NotFound
                lblMsg.Text = "Message: This account was not found. Please check the account number and try again."
                lblMsg.ForeColor = Color.Red
                lblMsg.Font.Italic = True
                pnlMessages.Visible = True
                LookupSuccessful = False
        End Select

        Return LookupSuccessful
    End Function

    Private Function CheckAccountBlocked() As Boolean
        Dim IsBlocked As Boolean = False
        Dim productSelected As String = HttpUtility.HtmlEncode(ddlProductList.SelectedValue)
        Dim SearchResults As New B2PAdminBLL.BlockAccount.Account

        Dim x As New B2PAdminBLL.BlockAccount.SearchCriteria
        x.ClientCode = Session("UserClientCode")
        x.ProductName = productSelected
        x.AccountNumber1 = HttpUtility.HtmlEncode(Trim(txtAccountNumber1.Text))

        If Trim(txtAccountNumber2.Text) <> "" Then
            x.AccountNumber2 = HttpUtility.HtmlEncode(Trim(txtAccountNumber2.Text))
        Else
            x.AccountNumber2 = ""
        End If
        If Trim(txtAccountNumber3.Text) <> "" Then
            x.AccountNumber3 = HttpUtility.HtmlEncode(Trim(txtAccountNumber3.Text))
        Else
            x.AccountNumber3 = ""
        End If

        SearchResults = B2PAdminBLL.BlockAccount.SearchBlockedAccount(x)

        If SearchResults.IsAccountBlocked Then
            acct = SearchResults
            Session("Block_ID") = SearchResults.BlockAccount_ID
            IsBlocked = True
        End If

        Return IsBlocked
    End Function

    Private Function CheckProfileAccount() As Boolean
        Dim HasProfile As Boolean = False
        Dim productSelected As String = HttpUtility.HtmlEncode(ddlProductList.SelectedValue)
        Dim SearchResults As New B2PAdminBLL.BlockAccount.ProfileAccount

        Session("BlockProfile") = Nothing

        Dim x As New B2PAdminBLL.BlockAccount.SearchCriteria
        x.ClientCode = Session("UserClientCode")
        x.ProductName = productSelected
        x.AccountNumber1 = HttpUtility.HtmlEncode(Trim(txtAccountNumber1.Text))

        If Trim(txtAccountNumber2.Text) <> "" Then
            x.AccountNumber2 = HttpUtility.HtmlEncode(Trim(txtAccountNumber2.Text))
        Else
            x.AccountNumber2 = ""
        End If
        If Trim(txtAccountNumber3.Text) <> "" Then
            x.AccountNumber3 = HttpUtility.HtmlEncode(Trim(txtAccountNumber3.Text))
        Else
            x.AccountNumber3 = ""
        End If

        SearchResults = B2PAdminBLL.BlockAccount.CheckProfileAccount(x)

        If SearchResults.Profile_ID > 0 Then
            profileAccount = SearchResults
            Session("BlockProfile") = profileAccount
            HasProfile = True
        End If

        Return HasProfile
    End Function

    Private Sub PreLoadBlockDetails()
        lblProduct.Text = acct.ProductName
        txtAccountNumber1d.Text = acct.AccountNumber1
        txtAccountNumber2d.Text = acct.AccountNumber2
        txtAccountNumber3d.Text = acct.AccountNumber3

        If Not IsNothing(acct.FirstName) Then
            txtFirstName.Text = acct.FirstName.ToString()
            txtLastName.Text = acct.LastName.ToString()
        ElseIf Not IsNothing(profileAccount.FirstName) Then
            txtFirstName.Text = profileAccount.FirstName
            txtLastName.Text = profileAccount.LastName
        End If

        If Not IsNothing(profileAccount.EmailAddress) Then
            If profileAccount.EmailAddress.Length > 0 Then
                txtEmailAddress.Text = profileAccount.EmailAddress
                cbSendEmail.Checked = True
            Else
                txtEmailAddress.Text = Nothing
                cbSendEmail.Checked = False
            End If
        Else
            txtEmailAddress.Text = Nothing
            cbSendEmail.Checked = False
        End If


        If profileAccount.Profile_ID > 0 Then
            Session("Profile_ID") = profileAccount.Profile_ID
            pnlProfile.Visible = True
        End If
        If profileAccount.AccountPayment_ID > 0 Then
            If profileAccount.PaymentType = "C" Then
                lblAutoPay.Text = "This account is setup for automated payment by Credit/Debit card. " + vbCrLf + "Blocking this account for Credit/Debit card will cancel all future payments."
            Else
                lblAutoPay.Text = "This account is setup for automated payment by eCheck." + vbCrLf + "Blocking this account for eCheck will cancel all future payments."
            End If
            lblAutoPay.ForeColor = Color.Red
        Else
            lblAutoPay.Text = ""
        End If
    End Sub

    Private Sub ResetScreen()
        txtAccountNumber1.Text = ""
        txtAccountNumber1r.Text = ""
        txtAccountNumber2.Text = ""
        txtAccountNumber2r.Text = ""
        txtAccountNumber3.Text = ""
        txtAccountNumber3r.Text = ""
        lblMsg.Text = ""
        pnlMessages.Visible = False
        pnlBlockDetail.Visible = False
        pnlProduct.Visible = True
    End Sub

    Private Sub SaveBlock()

        Dim ba As New B2PAdminBLL.BlockAccount.Account
        Dim pa As New B2PAdminBLL.BlockAccount.ProfileAccount

        If Not IsNothing(Session("BlockProfile")) Then
            pa = Session("BlockProfile")
        End If
        If cbSendEmail.Checked Then
            ba.EmailAddress = HttpUtility.HtmlEncode(txtEmailAddress.Text)
        End If

        ba.ClientCode = CStr(Session("UserClientCode"))
        ba.ProductName = lblProduct.Text
        ba.AccountNumber1 = HttpUtility.HtmlEncode(Trim(txtAccountNumber1d.Text))
        ba.AccountNumber2 = HttpUtility.HtmlEncode(Trim(txtAccountNumber2d.Text))
        ba.AccountNumber3 = HttpUtility.HtmlEncode(Trim(txtAccountNumber3d.Text))
        ba.FirstName = HttpUtility.HtmlEncode(Trim(txtFirstName.Text))
        ba.LastName = HttpUtility.HtmlEncode(Trim(txtLastName.Text))
        ba.EmailAddress = HttpUtility.HtmlEncode(Trim(txtEmailAddress.Text))
        ba.BlockComments = HttpUtility.HtmlEncode(Trim(txtComments.Text))
        ba.BlockBy = Convert.ToInt32(Session("SecurityID"))
        ba.BlockACH = cbEcheck.Checked
        ba.BlockCreditCard = cbCreditCard.Checked

        If B2PAdminBLL.BlockAccount.SaveAccountBlock(ba, pa, cbSendEmail.Checked) Then
            If cbSendEmail.Checked And pa.Profile_ID > 0 Then
                SendText()
            End If
            ResetScreen()
            pnlMessages.Visible = True
            lblMsg.Text = String.Format("Account {0} has been successfully Blocked", txtAccountNumber1d.Text)
            lblMsg.ForeColor = Color.Blue
            lblMsg.Font.Italic = True

        Else
            'we had an error
            pnlMessageDetail.Visible = True
            lblMessageDetail.Text = "An error occurred during Save. Block not saved!"
            lblMessageDetail.ForeColor = Color.Red
            lblMessageDetail.Font.Italic = True
        End If

    End Sub

    Private Sub SendText()
        Dim errMsg As String = String.Empty
        Dim ClientOptions As B2P.Objects.Client = B2P.Objects.Client.GetClient(Session("UserClientCode"))
        Dim x As B2P.Profile.User = New B2P.Profile.User
        profileAccount = CType(Session("BlockProfile"), B2PAdminBLL.BlockAccount.ProfileAccount)
        x = B2P.Profile.ProfileManagement.GetUserByProfile_ID(profileAccount.Profile_ID)

        Try

            If ClientOptions.TextNotify And x.CellPhoneStatus = B2P.Profile.User.EmailPhoneStatus.Validated Then
                Dim ntfOption As B2P.Profile.Notification = B2P.Profile.Notifications.GetNotificationOptions(profileAccount.Profile_ID)
                If ntfOption.SystemText Then
                    Dim ct As New B2P.Common.Messaging.CustomerText("BlockAccount", Session("UserClientCode"))
                    'ct.textMessage.AddParameter(ClientOptions.ClientName, "ClientName")
                    ct.textMessage.AddParameter(ddlProductList.SelectedValue, "ProductName")
                    If cbEcheck.Checked And cbCreditCard.Checked Then
                        ct.textMessage.AddParameter("eCheck and Credit/Debit Card", "PaymentMethod")
                    ElseIf cbEcheck.Checked Then
                        ct.textMessage.AddParameter("eCheck", "PaymentMethod")
                    ElseIf cbCreditCard.Checked Then
                        ct.textMessage.AddParameter("Credit/Debit Card", "PaymentMethod")
                    End If

                    ct.textMessage.AddParameter(ClientOptions.ContactPhone, "ClientPhone")
                    ct.textMessage.AddParameter(String.Format("{0} {1} {2}", Trim(txtAccountNumber1.Text), Trim(txtAccountNumber2.Text), Trim(txtAccountNumber3.Text)), "ACCOUNTNUMBER")
                    ct.textMessage.PhoneNumber = x.CellPhoneNumber
                    ct.Reference_ID = x.Profile_ID
                    ct.ReferenceType = 10
                    ct.SendText()
                End If

            End If
        Catch ex As Exception
            ' Build the error message
            errMsg = Utility.BuildErrorMessage(Request.Url.Host, Request.Url.AbsolutePath,
                                               ex.Source, ex.TargetSite.DeclaringType.Name,
                                               ex.TargetSite.Name, ex.Message)

            B2P.Common.Logging.LogError("B2P Admin --> " & Request.Url.AbsoluteUri & ".", errMsg, B2P.Common.Logging.NotifySupport.No)
            Response.Redirect("/Error", False)
            HttpContext.Current.ApplicationInstance.CompleteRequest()
        End Try

    End Sub

#Region "Events"
    Private Sub ddlAUClient_SelectedIndexChanged(ByVal sender As Object, ByVal e As Global.Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles ddlAUClient.SelectedIndexChanged
        Session("UserClientCode") = ddlAUClient.SelectedItem.Text
        ClearScreen()
        loadProducts()
    End Sub

    Protected Sub ddlProductList_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlProductList.SelectedIndexChanged
        txtAccountNumber1.Text = ""
        txtAccountNumber1r.Text = ""
        txtAccountNumber2.Text = ""
        txtAccountNumber2r.Text = ""
        txtAccountNumber3.Text = ""
        txtAccountNumber3r.Text = ""
        txtAccountNumber1d.Text = ""
        txtAccountNumber2d.Text = ""
        txtAccountNumber3d.Text = ""

        If ddlProductList.SelectedValue <> "" Then
            getData()

        Else
            pnlHeaders.Visible = False
            pnlButton.Visible = False
            pnlLookupButton.Visible = False
            pnlAccount1.Visible = False
            pnlAccount2.Visible = False
            pnlAccount3.Visible = False
        End If

    End Sub

    Protected Sub btnLookup_Click(sender As Object, e As ImageClickEventArgs) Handles btnLookup.Click

        If Global.PeterBlum.DES.Web.WebControls.WebFormDirector.Current.IsValid Then

            Try
                If ValidateLookup() Then
                    If CheckAccountBlocked() Then
                        'navigate to detail page
                        Response.Redirect("/BlockAccount/BlockDetail.aspx?source=add")
                    Else
                        CheckProfileAccount()
                        'lock product and account fields, hide buttons
                        pnlProduct.Visible = False
                        ddlAUClient.Enabled = False
                        'Display BlockAccount panel
                        pnlBlockDetail.Visible = True
                        cbCreditCard.Checked = False
                        cbEcheck.Checked = False
                        PreLoadBlockDetails()
                    End If
                End If

            Catch te As ThreadAbortException

            Catch ex As Exception
                B2P.Common.Logging.LogError("B2PAdmin.BlockAccount", "Error account lookup" & ex.ToString, B2P.Common.Logging.NotifySupport.No)
                Response.Redirect("/errors/")
                HttpContext.Current.ApplicationInstance.CompleteRequest()
            End Try
        End If
    End Sub

    Protected Sub btnSubmit_Click(sender As Object, e As ImageClickEventArgs) Handles btnSubmit.Click
        If Global.PeterBlum.DES.Web.WebControls.WebFormDirector.Current.IsValid Then

            Try
                If CheckAccountBlocked() Then
                    'navigate to detail page
                    Response.Redirect("/BlockAccount/BlockDetail.aspx?source=add")
                Else
                    acct.ProductName = HttpUtility.HtmlEncode(ddlProductList.SelectedValue)
                    acct.AccountNumber1 = HttpUtility.HtmlEncode(Trim(txtAccountNumber1.Text))
                    acct.AccountNumber2 = HttpUtility.HtmlEncode(Trim(txtAccountNumber2.Text))
                    acct.AccountNumber3 = HttpUtility.HtmlEncode(Trim(txtAccountNumber3.Text))
                    CheckProfileAccount()
                    'lock product and account fields, hide buttons
                    pnlProduct.Visible = False
                    ddlAUClient.Enabled = False
                    'Display BlockAccount panel
                    pnlBlockDetail.Visible = True
                    PreLoadBlockDetails()
                End If

            Catch te As ThreadAbortException

            Catch ex As Exception
                B2P.Common.Logging.LogError("B2PAdmin.BlockAccount.Submit", ex.ToString, B2P.Common.Logging.NotifySupport.No)
                Response.Redirect("/errors/")
                HttpContext.Current.ApplicationInstance.CompleteRequest()
            End Try
        End If
    End Sub

    Protected Sub btnClear_Click(sender As Object, e As ImageClickEventArgs) Handles btnClear.Click
        txtAccountNumber1.Text = ""
        txtAccountNumber1r.Text = ""
        txtAccountNumber2.Text = ""
        txtAccountNumber2r.Text = ""
        txtAccountNumber3.Text = ""
        txtAccountNumber3r.Text = ""
        lblMsg.Text = ""
        pnlMessages.Visible = False
    End Sub

    Protected Sub lnkBtnProfile_Click(sender As Object, e As EventArgs) Handles lnkBtnProfile.Click
        Response.Redirect("/profile/profiledetail.aspx?source=profile")
    End Sub

    Protected Sub btnCancel_CLick(sender As Object, e As ImageClickEventArgs) Handles btnCancel.Click
        ResetScreen()
    End Sub

    Protected Sub btnSubmitBlock_Click(sender As Object, e As ImageClickEventArgs) Handles btnSubmitBlock.Click
        SaveBlock()
    End Sub

#End Region
End Class