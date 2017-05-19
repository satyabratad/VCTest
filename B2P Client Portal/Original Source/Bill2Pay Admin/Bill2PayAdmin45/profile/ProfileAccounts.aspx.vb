'Imports Microsoft.Security.Application '*** used in antixss library
Imports PeterBlum.DES
Imports System.Threading
Imports System.Globalization

Partial Public Class ProfileAccounts
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            ddlProductList.DataSource = B2P.Profile.Product.ListProducts(CStr(Session("AccountClientCode")))
            ddlProductList.DataBind()

        End If
    End Sub

    Protected Sub ddlProductList_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlProductList.SelectedIndexChanged
        txtAccountNumber1.Text = ""
        txtAccountNumber2.Text = ""
        txtAccountNumber3.Text = ""

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
    Private Sub btnSubmit_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnSubmit.Click
        If Global.PeterBlum.DES.Web.WebControls.WebFormDirector.Current.IsValid Then
            addAccount()
        End If
    End Sub
    Private Sub addAccount()
        Try
            Dim acctObj As New B2P.Profile.Account
            acctObj.ClientCode = CStr(Session("AccountClientCode"))
            acctObj.ProductName = baseclass.SafeEncode(ddlProductList.SelectedItem.Text)
            acctObj.AccountNumber1 = HttpUtility.HtmlEncode(txtAccountNumber1.Text)
            If Trim(txtAccountNumber2.Text) <> "" Then
                acctObj.AccountNumber2 = HttpUtility.HtmlEncode(txtAccountNumber2.Text)
            Else
                acctObj.AccountNumber2 = ""
            End If
            If Trim(txtAccountNumber3.Text) <> "" Then
                acctObj.AccountNumber3 = HttpUtility.HtmlEncode(txtAccountNumber3.Text)
            Else
                acctObj.AccountNumber3 = ""
            End If
            acctObj.NickName = ""


            Dim addResults As B2P.Profile.Accounts.AddAccountResults
            addResults = B2P.Profile.Accounts.AddAccount(acctObj, Convert.ToInt32(Session("Profile_ID").ToString), Convert.ToInt32(Session("SecurityID")))

            Select Case addResults.Result
                Case B2P.Profile.Accounts.AddAccountResults.Results.AccountLimitReached
                    lblProductTextNumberError.Text = "Add Account Results"
                    lblLookupMessageError.Text = "You have reached the maximum number of accounts."
                    modalLookupError.Show()
                Case B2P.Profile.Accounts.AddAccountResults.Results.DuplicateAccount
                    lblProductTextNumberError.Text = "Add Account Results"
                    lblLookupMessageError.Text = "That account already exists on a profile in our system."
                    modalLookupError.Show()
                Case B2P.Profile.Accounts.AddAccountResults.Results.DuplicateNickName
                    lblProductTextNumberError.Text = "Add Account Results"
                    lblLookupMessageError.Text = "An account already exists on your profile with that nickname."
                    modalLookupError.Show()
                Case B2P.Profile.Accounts.AddAccountResults.Results.Failed
                    lblProductTextNumberError.Text = "Add Account Results"
                    lblLookupMessageError.Text = "There was a problem adding your account. Please try again."
                    modalLookupError.Show()
                Case B2P.Profile.Accounts.AddAccountResults.Results.InvalidAccount
                    lblProductTextNumberError.Text = "Add Account Results"
                    lblLookupMessageError.Text = "Invalid account."
                    modalLookupError.Show()
                Case B2P.Profile.Accounts.AddAccountResults.Results.Success
                    'txtAccountNumber1.Text = ""
                    'txtAccountNumber2.Text = ""
                    'txtAccountNumber3.Text = ""
                    'pnlAccount1.Visible = False
                    'pnlAccount2.Visible = False
                    'pnlAccount3.Visible = False
                    'ddlProductList.SelectedValue = ""
                    'pnlButton.Visible = False
                    'pnlHeaders.Visible = False
                    'getAccountList()
                    Response.Redirect("profiledetail.aspx", False)

            End Select


        Catch ex As Exception
            B2P.Common.Logging.LogError("PMS", "Error adding account " & ex.ToString, B2P.Common.Logging.NotifySupport.No)
            Response.Redirect("/errors/")
            HttpContext.Current.ApplicationInstance.CompleteRequest()
        End Try
    End Sub


    Private Sub BuildForm()
        Try
            Dim productSelected As String = Replace(HttpUtility.HtmlEncode(ddlProductList.SelectedValue), "&amp;", "&")
            Dim CurrentCategory As New B2P.Objects.Product(CStr(Session("AccountClientCode")), productSelected, B2P.Common.Enumerations.TransactionSources.NotSpecified)
            With CurrentCategory.WebOptions
                If .AccountIDField1.Enabled = True Then
                    lblAccountNumber1.Text = HttpUtility.HtmlEncode(.AccountIDField1.Label)
                    txtAccountNumber1.MaxLength = .AccountIDField1.MaximumLength
                    'lblAccountDescription1.Text = HttpUtility.HtmlEncode(.AccountIDField1.Description)                
                    pnlAccount1.Visible = True
                    SetValidatorProperties(txtAccountNumber1_FilteredTextBoxExtender, regAccountNumber1, reqAccountNumber1, .AccountIDField1)
                Else
                    pnlAccount1.Visible = False
                End If

                If .AccountIDField2.Enabled = True Then
                    lblAccountNumber2.Text = HttpUtility.HtmlEncode(.AccountIDField2.Label)
                    txtAccountNumber2.MaxLength = .AccountIDField2.MaximumLength
                    'lblAccountDescription2.Text = HttpUtility.HtmlEncode(.AccountIDField2.Description)               
                    pnlAccount2.Visible = True
                    SetValidatorProperties(txtAccountNumber2_FilteredTextBoxExtender, regAccountNumber2, reqAccountNumber2, .AccountIDField2)
                Else
                    pnlAccount2.Visible = False

                End If

                If .AccountIDField3.Enabled = True Then
                    lblAccountNumber3.Text = HttpUtility.HtmlEncode(.AccountIDField3.Label)
                    txtAccountNumber3.MaxLength = .AccountIDField3.MaximumLength
                    'lblAccountDescription3.Text = HttpUtility.HtmlEncode(.AccountIDField3.Description)
                    pnlAccount3.Visible = True
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

    
    Private Sub getData()

        Try
            pnlHeaders.Visible = True

            Dim z As New B2P.Objects.Product(CStr(Session("AccountClientCode")), ddlProductList.SelectedValue, B2P.Common.Enumerations.TransactionSources.NotSpecified)
            If z.AmountDueSource = B2P.Common.Enumerations.AmountDueSources.Lookup Or z.AmountDueSource = B2P.Common.Enumerations.AmountDueSources.Table Then
                pnlLookupButton.Visible = True
                pnlButton.Visible = False
            Else
                pnlLookupButton.Visible = False
                pnlButton.Visible = True
            End If
            BuildForm()

        Catch ex As Exception
            B2P.Common.Logging.LogError("B2P", "Error during product lookup " & ex.ToString, B2P.Common.Logging.NotifySupport.No)
            Response.Redirect("/errors/")
        End Try


        'Dim CurrentCategory As New B2P.Objects.Product(MySession.ClientSession.ClientCode, ddlProductList.SelectedValue, B2P.Common.Enumerations.TransactionSources.NotSpecified)


        'B2PSession.CurrentCategory = CurrentCategory
        'B2PSession.PaymentFormData = Nothing
        'B2PSession.PaymentFormData = Nothing
        'B2PSession.LookupProduct = ddlCategories.SelectedValue
        'Response.Redirect("/payment/product_detail")
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

  

    Private Sub btnLookup_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnLookup.Click
        If Global.PeterBlum.DES.Web.WebControls.WebFormDirector.Current.IsValid Then

            Try
                Dim x As New B2P.ClientInterface.Manager.ClientInterface() ' B2P.ClientInterface.Custom.AccountSearch
                Dim y As New B2P.ClientInterface.Manager.ClientInterfaceWS.SearchResults
                Dim sp As New B2P.ClientInterface.Manager.ClientInterfaceWS.SearchParameters
                Dim productSelected As String = Replace(HttpUtility.HtmlEncode(ddlProductList.SelectedValue), "&amp;", "&")

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

                sp.ClientCode = CStr(Session("AccountClientCode"))
                sp.ProductName = productSelected

                y = B2P.ClientInterface.Manager.ClientInterface.GetClientData(sp)
                Session("ClientData") = y

                Select Case y.SearchResult
                    Case B2P.ClientInterface.Manager.ClientInterfaceWS.StatusCodes.Success
                        Select Case y.PaymentStatus
                            Case B2P.ClientInterface.Manager.ClientInterfaceWS.PaymentStatusCodes.Allowed, B2P.ClientInterface.Manager.ClientInterfaceWS.PaymentStatusCodes.NotEditable, B2P.ClientInterface.Manager.ClientInterfaceWS.PaymentStatusCodes.MinimumPaymentRequired
                                lblProductTextNumber.Text = "Product Lookup"
                                'lblProductNumberLookup.Text = textnumber.ToString
                                lblLookupMessage.Text = "Account found. Please review the following information: "
                                'ckUseAddress.Checked = True
                                grdLookup.DataSource = y.SupplementalInformation.ClientInfo
                                grdLookup.DataBind()

                                modalLookup.Show()
                            Case B2P.ClientInterface.Manager.ClientInterfaceWS.PaymentStatusCodes.NotAllowed
                                lblProductTextNumber.Text = "Product Lookup"
                                'lblProductNumberLookup.Text = textnumber.ToString
                                lblLookupMessage.Text = "Account found. Note: An online payment is not allowed for this account. Please review the following information: "
                                'ckUseAddress.Checked = True
                                grdLookup.DataSource = y.SupplementalInformation.ClientInfo
                                grdLookup.DataBind()

                                modalLookup.Show()
                        End Select

                    Case B2P.ClientInterface.Manager.ClientInterfaceWS.StatusCodes.ErrorOccurred
                        lblProductTextNumberError.Text = "Product Lookup"
                        'lblProductNumberLookupError.Text = textnumber.ToString
                        lblLookupMessageError.Text = "Message: Oops, an error has ocurred during the account lookup."

                        modalLookupError.Show()

                    Case B2P.ClientInterface.Manager.ClientInterfaceWS.StatusCodes.LookupNotEnabled
                        lblProductTextNumberError.Text = "Product Lookup"
                        'lblProductNumberLookupError.Text = textnumber.ToString
                        lblLookupMessageError.Text = "Message: Lookup is not enabled for this account."

                        modalLookupError.Show()

                    Case B2P.ClientInterface.Manager.ClientInterfaceWS.StatusCodes.NotFound
                        lblProductTextNumberError.Text = "Product Lookup"
                        'lblProductNumberLookupError.Text = textnumber.ToString
                        lblLookupMessageError.Text = "Message: This account was not found. Please check the account number and try again."

                        modalLookupError.Show()

                End Select
            Catch te As ThreadAbortException

            Catch ex As Exception
                B2P.Common.Logging.LogError("PMS", "Error account lookup" & ex.ToString, B2P.Common.Logging.NotifySupport.No)
                Response.Redirect("/errors/")
                HttpContext.Current.ApplicationInstance.CompleteRequest()
            End Try
        End If
    End Sub

    Private Sub btnLookupOK_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnLookupOK.Click
        If Global.PeterBlum.DES.Web.WebControls.WebFormDirector.Current.IsValid Then
            addAccount()

        End If
    End Sub
End Class