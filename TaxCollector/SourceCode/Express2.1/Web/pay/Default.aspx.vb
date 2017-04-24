Imports System

Namespace B2P.PaymentLanding.Express.Web

    Public Class _paydefault : Inherits SiteBasePage

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

            lnkCSS.Href = BLL.SessionManager.ClientCSS
            BLL.SessionManager.PaymentMade = False
            BLL.SessionManager.CreditCard = Nothing
            BLL.SessionManager.BankAccount = Nothing

            ' Add client side javascript attributes to the various form controls
            RegisterClientJs()

            If Not IsPostBack Then
                If BLL.SessionManager.IsSSOProduct = False Then
                    Dim y As B2P.Objects.Client = B2P.Objects.Client.GetClient(BLL.SessionManager.ClientCode.ToString)

                    lblClientMessage.Text = B2P.Objects.Client.GetClientMessage("Welcome", BLL.SessionManager.ClientCode.ToString)
                    ddlCategories.DataSource = BLL.SessionManager.CategoryList
                    ddlCategories.DataBind()


                    If ddlCategories.Items.Count = 1 Then
                        pnlProducts.Visible = False
                    End If

                        If y.DefaultProductName <> "" Then
                        ddlCategories.SelectedValue = y.DefaultProductName.Trim

                    End If

                    ' Check for session values; populate fields if not blank
                    If BLL.SessionManager.ProductName <> "" Then
                        ddlCategories.SelectedValue = BLL.SessionManager.ProductName
                        txtLookupAccount1.Text = BLL.SessionManager.AccountNumber1
                        txtLookupAccount2.Text = BLL.SessionManager.AccountNumber2
                        txtLookupAccount3.Text = BLL.SessionManager.AccountNumber3


                    End If


                    BLL.SessionManager.LookupAmount = Nothing
                    BLL.SessionManager.LookupAmountMinimum = False
                    BLL.SessionManager.LookupAmountEditable = True
                    getProductLookup()


                Else
                    Response.Redirect("/errors/")
                End If

            End If
        End Sub

        Protected Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
            BLL.SessionManager.LookupData = Nothing
            txtLookupAccount1.Text = ""
            txtLookupAccount2.Text = ""
            txtLookupAccount3.Text = ""
            grdLookup.DataSource = Nothing
            btnLookup.Visible = True
            lblLookupHeader.Text = ""
            divLookupAlert.Visible = False

        End Sub

        Private Sub btnLookup_Click(sender As Object, e As EventArgs) Handles btnLookup.Click
            Dim x As New B2P.ClientInterface.Manager.ClientInterface()
            Dim y As New B2P.ClientInterface.Manager.ClientInterfaceWS.SearchResults
            Dim sp As New B2P.ClientInterface.Manager.ClientInterfaceWS.SearchParameters
            Dim errMsg As String = String.Empty
            Try
                sp.AccountNumber1 = HttpUtility.HtmlEncode(txtLookupAccount1.Text)

                If txtLookupAccount2.Text.Trim <> "" Then
                    sp.AccountNumber2 = HttpUtility.HtmlEncode(txtLookupAccount2.Text)
                Else
                    sp.AccountNumber2 = ""
                End If

                If txtLookupAccount3.Text.Trim <> "" Then
                    sp.AccountNumber3 = HttpUtility.HtmlEncode(txtLookupAccount3.Text)
                Else
                    sp.AccountNumber3 = ""
                End If

                sp.ClientCode = BLL.SessionManager.ClientCode
                sp.ProductName = ddlCategories.SelectedValue

                y = B2P.ClientInterface.Manager.ClientInterface.GetClientData(sp)
                BLL.SessionManager.LookupData = Nothing
                BLL.SessionManager.AmountEditable = True

                Select Case y.SearchResult
                    Case B2P.ClientInterface.Manager.ClientInterfaceWS.StatusCodes.Success
                        divLookupAlert.Visible = True

                        BLL.SessionManager.LookupData = y
                        Dim dt As DataTable = y.SupplementalInformation.ClientInfo
                        grdLookup.DataSource = dt
                        ' Get the address and name to display on the confirmation page
                        BLL.SessionManager.ServiceAddress = Utility.SafeEncode(y.Demographics.Address1.Value)


                        BLL.SessionManager.NameOnLookupAccount = Utility.SafeEncode(y.Demographics.FirstName.Value & " " & y.Demographics.LastName.Value)
                        grdLookup.DataBind()
                        btnLookupGo.Visible = True
                        btnClear.Visible = True
                        btnLookupOK.Visible = False
                        BLL.SessionManager.PaymentStatusCode = y.PaymentStatus
                        Select Case y.PaymentStatus
                            Case B2P.ClientInterface.Manager.ClientInterfaceWS.PaymentStatusCodes.Allowed
                                'Check for blocked payment methods
                                Dim bk As New B2P.Common.BlockedAccounts.BlockedAccountResults
                                bk = B2P.Common.BlockedAccounts.CheckForBlockedAccount(BLL.SessionManager.ClientCode, ddlCategories.SelectedValue, Utility.SafeEncode(txtLookupAccount1.Text.Trim),
                                                                                  Utility.SafeEncode(txtLookupAccount2.Text.Trim), Utility.SafeEncode(txtLookupAccount3.Text.Trim))
                                If bk.IsAccountBlocked Then
                                    If bk.ACHBlocked = True AndAlso bk.CreditCardBlocked = True Then
                                        divLookupAlertError.Attributes.Add("class", "alert alert-danger")
                                        lblLookupHeaderError.Text = "We are not able to accept any online payments for this account. Please call Customer Service at " & Utility.SafeEncode(BLL.SessionManager.Client.ContactPhone) & "."
                                        ' Show the modal
                                        Page.ClientScript.RegisterStartupScript(Me.GetType, "Show", "$(document).ready(function() { $('#pnlLookupError').modal({show: 'true', backdrop: 'static', keyboard: false}); });", True)
                                        Exit Sub
                                    End If

                                    If bk.ACHBlocked = True Then
                                        BLL.SessionManager.BlockedACH = True
                                    Else
                                        BLL.SessionManager.BlockedACH = False
                                    End If

                                    If bk.CreditCardBlocked = True Then
                                        BLL.SessionManager.BlockedCC = True
                                    Else
                                        BLL.SessionManager.BlockedCC = False
                                    End If
                                Else
                                    BLL.SessionManager.BlockedCC = False
                                    BLL.SessionManager.BlockedACH = False
                                End If

                                lblLookupHeader.Text = "Account found. Please see information below."
                                divLookupAlert.Attributes.Add("class", "alert alert-success")
                                BLL.SessionManager.LookupAmount = y.AmountDue
                                ctlPropertyAddress.Amount = y.AmountDue
                                BLL.SessionManager.LookupProduct = ddlCategories.SelectedValue
                                BLL.SessionManager.AccountNumber1 = Utility.SafeEncode(txtLookupAccount1.Text)
                                BLL.SessionManager.AccountNumber2 = Utility.SafeEncode(txtLookupAccount2.Text)
                                BLL.SessionManager.AccountNumber3 = Utility.SafeEncode(txtLookupAccount3.Text)


                                If y.ClientMessage <> "" Then
                                    lblLookupHeader.Text = lblLookupHeader.Text & " Please note: " & y.ClientMessage
                                End If
                                ' Show the modal
                                Page.ClientScript.RegisterStartupScript(Me.GetType, "Show", "$(document).ready(function() { $('#pnlLookupResults').modal({show: 'true', backdrop: 'static', keyboard: false}); });", True)

                            Case B2P.ClientInterface.Manager.ClientInterfaceWS.PaymentStatusCodes.NotAllowed
                                divLookupAlert.Visible = True
                                lblLookupHeader.Text = "Account found. Please see information below."
                                divLookupAlert.Attributes.Add("class", "alert alert-success")
                                lblLookupHeader.Text = y.ClientMessage
                                grdLookup.DataSource = y.SupplementalInformation.ClientInfo
                                grdLookup.DataBind()
                                BLL.SessionManager.AccountNumber1 = Utility.SafeEncode(txtLookupAccount1.Text)
                                BLL.SessionManager.AccountNumber2 = Utility.SafeEncode(txtLookupAccount2.Text)
                                BLL.SessionManager.AccountNumber3 = Utility.SafeEncode(txtLookupAccount3.Text)
                                btnLookupGo.Visible = False
                                btnClear.Visible = False
                                btnLookupOK.Visible = True
                                ' Show the modal
                                Page.ClientScript.RegisterStartupScript(Me.GetType, "Show", "$(document).ready(function() { $('#pnlLookupResults').modal({show: 'true', backdrop: 'static', keyboard: false}); });", True)

                            Case B2P.ClientInterface.Manager.ClientInterfaceWS.PaymentStatusCodes.MinimumPaymentRequired
                                divLookupAlert.Visible = True
                                'Check for blocked payment methods
                                Dim bk As New B2P.Common.BlockedAccounts.BlockedAccountResults
                                bk = B2P.Common.BlockedAccounts.CheckForBlockedAccount(BLL.SessionManager.ClientCode, ddlCategories.SelectedValue, Utility.SafeEncode(txtLookupAccount1.Text.Trim),
                                                                                  Utility.SafeEncode(txtLookupAccount2.Text.Trim), Utility.SafeEncode(txtLookupAccount3.Text.Trim))
                                If bk.IsAccountBlocked Then
                                    If bk.ACHBlocked = True AndAlso bk.CreditCardBlocked = True Then
                                        lblLookupHeaderError.Text = "Payments Not Accepted."
                                        divLookupAlertError.Attributes.Add("class", "alert alert-danger")
                                        lblLookupHeaderError.Text = lblLookupHeader.Text & "We are not able to accept any online payments for this account. Please call Customer Service at " & Utility.SafeEncode(BLL.SessionManager.Client.ContactPhone) & "."
                                        ' Show the modal
                                        Page.ClientScript.RegisterStartupScript(Me.GetType, "Show", "$(document).ready(function() { $('#pnlLookupError').modal({show: 'true', backdrop: 'static', keyboard: false}); });", True)
                                        Exit Sub
                                    End If

                                    If bk.ACHBlocked = True Then
                                        BLL.SessionManager.BlockedACH = True
                                    Else
                                        BLL.SessionManager.BlockedACH = False
                                    End If

                                    If bk.CreditCardBlocked = True Then
                                        BLL.SessionManager.BlockedCC = True
                                    Else
                                        BLL.SessionManager.BlockedCC = False
                                    End If
                                Else
                                    BLL.SessionManager.BlockedCC = False
                                    BLL.SessionManager.BlockedACH = False
                                End If
                                lblLookupHeader.Text = "Account found. Please see information below."
                                divLookupAlert.Attributes.Add("class", "alert alert-success")
                                BLL.SessionManager.LookupAmount = y.AmountDue
                                BLL.SessionManager.LookupAmountMinimum = True
                                BLL.SessionManager.LookupProduct = ddlCategories.SelectedValue
                                If y.ClientMessage <> "" Then
                                    lblLookupHeader.Text = lblLookupHeader.Text & "Please note: " & y.ClientMessage
                                End If
                                BLL.SessionManager.AccountNumber1 = Utility.SafeEncode(txtLookupAccount1.Text)
                                BLL.SessionManager.AccountNumber2 = Utility.SafeEncode(txtLookupAccount2.Text)
                                BLL.SessionManager.AccountNumber3 = Utility.SafeEncode(txtLookupAccount3.Text)

                                ' Show the modal
                                Page.ClientScript.RegisterStartupScript(Me.GetType, "Show", "$(document).ready(function() { $('#pnlLookupResults').modal({show: 'true', backdrop: 'static', keyboard: false}); });", True)

                            Case B2P.ClientInterface.Manager.ClientInterfaceWS.PaymentStatusCodes.NotEditable
                                divLookupAlert.Visible = True
                                'Check for blocked payment methods
                                Dim bk As New B2P.Common.BlockedAccounts.BlockedAccountResults
                                bk = B2P.Common.BlockedAccounts.CheckForBlockedAccount(BLL.SessionManager.ClientCode, ddlCategories.SelectedValue, Utility.SafeEncode(txtLookupAccount1.Text.Trim),
                                                                                  Utility.SafeEncode(txtLookupAccount2.Text.Trim), Utility.SafeEncode(txtLookupAccount3.Text.Trim))
                                If bk.IsAccountBlocked Then
                                    If bk.ACHBlocked = True AndAlso bk.CreditCardBlocked = True Then
                                        lblLookupHeaderError.Text = "Payments Not Accepted."
                                        divLookupAlertError.Attributes.Add("class", "alert alert-danger")
                                        lblLookupHeaderError.Text = lblLookupHeader.Text & "We are not able to accept any online payments for this account. Please call Customer Service at " & Utility.SafeEncode(BLL.SessionManager.Client.ContactPhone) & "."
                                        ' Show the modal
                                        Page.ClientScript.RegisterStartupScript(Me.GetType, "Show", "$(document).ready(function() { $('#pnlLookupError').modal({show: 'true', backdrop: 'static', keyboard: false}); });", True)
                                        Exit Sub
                                    End If

                                    If bk.ACHBlocked = True Then
                                        BLL.SessionManager.BlockedACH = True
                                    Else
                                        BLL.SessionManager.BlockedACH = False
                                    End If

                                    If bk.CreditCardBlocked = True Then
                                        BLL.SessionManager.BlockedCC = True
                                    Else
                                        BLL.SessionManager.BlockedCC = False
                                    End If
                                Else
                                    BLL.SessionManager.BlockedCC = False
                                    BLL.SessionManager.BlockedACH = False
                                End If
                                lblLookupHeader.Text = "Account found. Please see information below."
                                divLookupAlert.Attributes.Add("class", "alert alert-success")
                                BLL.SessionManager.LookupAmountEditable = False
                                BLL.SessionManager.LookupAmount = y.AmountDue
                                BLL.SessionManager.LookupProduct = ddlCategories.SelectedValue
                                If y.ClientMessage <> "" Then
                                    lblLookupHeader.Text = lblLookupHeader.Text & "Please note: " & y.ClientMessage
                                End If
                                BLL.SessionManager.AccountNumber1 = Utility.SafeEncode(txtLookupAccount1.Text)
                                BLL.SessionManager.AccountNumber2 = Utility.SafeEncode(txtLookupAccount2.Text)
                                BLL.SessionManager.AccountNumber3 = Utility.SafeEncode(txtLookupAccount3.Text)
                                ' Show the modal
                                Page.ClientScript.RegisterStartupScript(Me.GetType, "Show", "$(document).ready(function() { $('#pnlLookupResults').modal({show: 'true', backdrop: 'static', keyboard: false}); });", True)
                        End Select

                    Case B2P.ClientInterface.Manager.ClientInterfaceWS.StatusCodes.ErrorOccurred
                        divLookupAlertError.Attributes.Add("class", "alert alert-danger")
                        lblLookupHeaderError.Text = "An unexpected error has occurred during the account lookup."
                        ' Show the modal
                        Page.ClientScript.RegisterStartupScript(Me.GetType, "Show", "$(document).ready(function() { $('#pnlLookupError').modal({show: 'true', backdrop: 'static', keyboard: false}); });", True)
                    Case B2P.ClientInterface.Manager.ClientInterfaceWS.StatusCodes.LookupNotEnabled
                        divLookupAlertError.Attributes.Add("class", "alert alert-danger")
                        lblLookupHeaderError.Text = "Lookup is not enabled for this account."
                        ' Show the modal
                        Page.ClientScript.RegisterStartupScript(Me.GetType, "Show", "$(document).ready(function() { $('#pnlLookupError').modal({show: 'true', backdrop: 'static', keyboard: false}); });", True)
                    Case B2P.ClientInterface.Manager.ClientInterfaceWS.StatusCodes.NotFound
                        divLookupAlertError.Attributes.Add("class", "alert alert-danger")
                        lblLookupHeaderError.Text = "This account was not found. Please check the account number and try again."
                        ' Show the modal
                        Page.ClientScript.RegisterStartupScript(Me.GetType, "Show", "$(document).ready(function() { $('#pnlLookupError').modal({show: 'true', backdrop: 'static', keyboard: false}); });", True)

                End Select

                Dim z As New B2P.Objects.Product(BLL.SessionManager.ClientCode, ddlCategories.SelectedValue, B2P.Common.Enumerations.TransactionSources.Web)

                If z.CollectAddress Then
                    Dim _client As B2P.Objects.Client = B2P.Objects.Client.GetClient(BLL.SessionManager.ClientCode.ToString)

                    ctlPropertyAddress.Address1 = _client.Address1
                    ctlPropertyAddress.Address2 = _client.Address2
                    ctlPropertyAddress.City = _client.City
                    ctlPropertyAddress.StateValue = _client.State
                    ctlPropertyAddress.Zip = _client.ZipCode
                End If

            Catch ex As Exception
                ' Build the error message
                errMsg = Utility.BuildErrorMessage(Request.Url.Host, Request.Url.AbsolutePath,
                                                   ex.Source, ex.TargetSite.DeclaringType.Name,
                                                   ex.TargetSite.Name, ex.Message)

                B2P.Common.Logging.LogError("Express Payment --> " & Request.Url.AbsoluteUri & ".", errMsg, B2P.Common.Logging.NotifySupport.No)
                Response.Redirect("/Errors/Error.aspx", False)
                HttpContext.Current.ApplicationInstance.CompleteRequest()
            End Try
        End Sub


        Private Sub BuildForm()
            Dim errMsg As String = String.Empty
            Try
                Dim productSelected As String = Utility.SafeEncode(ddlCategories.SelectedValue)
                Dim CurrentCategory As New B2P.Objects.Product(BLL.SessionManager.ClientCode.ToString, productSelected, B2P.Common.Enumerations.TransactionSources.Web)
                With CurrentCategory.WebOptions
                    If .AccountIDField1.Enabled = True Then
                        lblAccountNumber1.Text = Utility.SafeEncode(.AccountIDField1.Label)
                        txtLookupAccount1.MaxLength = .AccountIDField1.MaximumLength
                        pnlAccount1.Visible = True
                        SetValidatorProperties(txtLookupAccount1, .AccountIDField1, "hdAccount1")
                    Else
                        pnlAccount1.Visible = False
                    End If

                    If .AccountIDField2.Enabled = True Then
                        lblAccountNumber2.Text = Utility.SafeEncode(.AccountIDField2.Label)
                        txtLookupAccount2.MaxLength = .AccountIDField2.MaximumLength
                        pnlAccount2.Visible = True
                        hdAccount2.Value = "required"
                        SetValidatorProperties(txtLookupAccount2, .AccountIDField2, "hdAccount2")
                    Else
                        pnlAccount2.Visible = False
                        hdAccount2.Value = ""
                    End If

                    If .AccountIDField3.Enabled = True Then
                        lblAccountNumber3.Text = Utility.SafeEncode(.AccountIDField3.Label)
                        txtLookupAccount3.MaxLength = .AccountIDField3.MaximumLength
                        pnlAccount3.Visible = True
                        SetValidatorProperties(txtLookupAccount3, .AccountIDField3, "hdAccount3")
                    Else
                        pnlAccount3.Visible = False
                    End If


                End With
            Catch ex As Exception
                ' Build the error message
                errMsg = Utility.BuildErrorMessage(Request.Url.Host, Request.Url.AbsolutePath,
                                                   ex.Source, ex.TargetSite.DeclaringType.Name,
                                                   ex.TargetSite.Name, ex.Message)

                B2P.Common.Logging.LogError("Express Payment --> " & Request.Url.AbsoluteUri & ".", errMsg, B2P.Common.Logging.NotifySupport.No)
                Response.Redirect("/Errors/Error.aspx", False)
                HttpContext.Current.ApplicationInstance.CompleteRequest()
            End Try

        End Sub

        Private Sub ddlCategories_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCategories.SelectedIndexChanged
            'SelectedProduct = ddlCategories.SelectedValue
            ClearFields()
            getProductLookup()
        End Sub
        Private Sub ClearFields()
            lblAccountNumber1.Text = String.Empty
            lblAccountNumber2.Text = String.Empty
            lblAccountNumber3.Text = String.Empty
            txtLookupAccount1.Text = String.Empty
            txtLookupAccount2.Text = String.Empty
            txtLookupAccount3.Text = String.Empty



        End Sub
        Private Sub getProductLookup()
            Dim errMsg As String = String.Empty
            Try
                'Show/Hide Contact Info
                'Dim z As New B2P.Objects.Product(BLL.SessionManager.ClientCode, IIf(SelectedProduct Is Nothing, ddlCategories.SelectedValue, SelectedProduct), B2P.Common.Enumerations.TransactionSources.Web)
                Dim z As New B2P.Objects.Product(BLL.SessionManager.ClientCode, ddlCategories.SelectedValue, B2P.Common.Enumerations.TransactionSources.Web)
                Utility.SetBreadCrumbContactInfo("BreadCrumbMenu")
                'End Show/Hide Contact Info

                If z.AmountDueSource = B2P.Common.Enumerations.AmountDueSources.Lookup Or z.AmountDueSource = B2P.Common.Enumerations.AmountDueSources.Table Then
                    'pnlLookupAccount.Visible = True
                    BuildForm()
                    btnLookup.Enabled = True
                    btnLookup.Visible = True
                    btnAddtoCart.Visible = False
                    txtLookupAccount1.Focus()
                    ctlPropertyAddress.Visible = False
                Else
                    btnAddtoCart.Visible = True
                    btnLookup.Visible = False
                    BLL.SessionManager.LookupData = Nothing
                    BLL.SessionManager.LookupAmount = Nothing
                    BLL.SessionManager.LookupAmountMinimum = False
                    BLL.SessionManager.LookupAmountEditable = True
                    BLL.SessionManager.LookupProduct = Nothing
                    'txtLookupAccount1.Text = ""
                    If z.CollectAddress Then
                        ctlPropertyAddress.AddressVisible = True
                    Else
                        ctlPropertyAddress.AddressVisible = False
                    End If
                    grdLookup.DataSource = Nothing
                    BuildForm()
                End If


                If BLL.SessionManager.ManageCart.ShowCart = True Then
                    pnlCart.Visible = True
                    pnlContent.Visible = False
                    pnlEditLookupItem.Visible = False
                    If z.AmountDueSource = B2P.Common.Enumerations.AmountDueSources.Lookup Or z.AmountDueSource = B2P.Common.Enumerations.AmountDueSources.Table Then
                        BLL.SessionManager.ClientType = B2P.Cart.EClientType.Lookup
                    Else
                        BLL.SessionManager.ClientType = B2P.Cart.EClientType.NonLookup
                    End If

                    ctlCartGrid.PopulateGrid("ctlCartGrid")
                Else
                    If BLL.SessionManager.ManageCart.EditItemIndex > -1 Then
                        pnlCart.Visible = False
                        pnlContent.Visible = False
                        pnlEditLookupItem.Visible = True
                        Dim _SelectedItem = BLL.SessionManager.ManageCart.Cart.FirstOrDefault(Function(p) p.Index = BLL.SessionManager.ManageCart.EditItemIndex)
                        If Not _SelectedItem Is Nothing Then
                            ctlEditLookupItem.SelectedItem = _SelectedItem
                        End If
                    Else
                        pnlCart.Visible = False
                        pnlContent.Visible = True
                        pnlEditLookupItem.Visible = False
                    End If
                End If


            Catch ex As Exception
                ' Build the error message
                errMsg = Utility.BuildErrorMessage(Request.Url.Host, Request.Url.AbsolutePath,
                                                   ex.Source, ex.TargetSite.DeclaringType.Name,
                                                   ex.TargetSite.Name, ex.Message)

                B2P.Common.Logging.LogError("Express Payment --> " & Request.Url.AbsoluteUri & ".", errMsg, B2P.Common.Logging.NotifySupport.No)
                Response.Redirect("/Errors/Error.aspx", False)
                HttpContext.Current.ApplicationInstance.CompleteRequest()
            End Try


        End Sub
        '' setting Contact Info flag from bread crumb menu
        '' If Demographics or HomePhone either are enabled, then customer will see this Contact Info page
        '' If neither are enabled Contact Info page will not display
        'Private Sub SetBreadCrumbContactInfo(z As Objects.Product)

        '    If z.WebOptions.Demographics = Objects.WebConfiguration.OptionalFields.NotUsed And z.WebOptions.HomePhone = Objects.WebConfiguration.OptionalFields.NotUsed Then
        '        BLL.SessionManager.IsContactInfoRequired = False
        '    Else
        '        BLL.SessionManager.IsContactInfoRequired = True
        '    End If

        'End Sub

        ''' <summary>
        ''' Adds client side javascript to the various server controls.
        ''' </summary>
        Private Sub RegisterClientJs()
            btnLookup.Attributes.Add("onClick", "return validateForm()")
            btnSubmit.Attributes.Add("onClick", "return validateForm()")
        End Sub

        Private Sub SetValidatorProperties(ByVal lookupTextBox As TextBox, ByVal customField As B2P.Objects.WebConfiguration.CustomField, ByVal hiddenField As String)

            Dim MinimumLength As Integer = 0
            Dim myControl1 As HiddenField = FindControl(hiddenField)

            If customField.Required = True Then
                MinimumLength = 1
            End If

            If customField.Required AndAlso (Not myControl1 Is Nothing) Then
                myControl1.Value = "required"
            End If

            Select Case customField.DataType
                Case B2P.Objects.WebConfiguration.CustomField.FieldDataType.All
                    lookupTextBox.Attributes.Add("onkeypress", "return restrictInput(event, restrictionTypes.AlphaNumericAndExtraChars)")
                    lookupTextBox.Attributes.Add("onpaste", "return reformatInput(this, restrictionTypes.AlphaNumericAndExtraChars)")


                Case B2P.Objects.WebConfiguration.CustomField.FieldDataType.Alphanumeric
                    lookupTextBox.Attributes.Add("onkeypress", "return restrictInput(event, restrictionTypes.AlphaNumeric)")
                    lookupTextBox.Attributes.Add("onpaste", "return reformatInput(this, restrictionTypes.AlphaNumeric)")

                Case B2P.Objects.WebConfiguration.CustomField.FieldDataType.Custom

                Case B2P.Objects.WebConfiguration.CustomField.FieldDataType.Numeric
                    lookupTextBox.Attributes.Add("onkeypress", "return restrictInput(event, restrictionTypes.NumbersOnly)")
                    lookupTextBox.Attributes.Add("onpaste", "return reformatInput(this, restrictionTypes.NumbersOnly)")
            End Select

        End Sub

        Protected Sub btnLookupGo_Click(sender As Object, e As EventArgs) Handles btnLookupGo.Click
            Dim CurrentCategory As New B2P.Objects.Product(BLL.SessionManager.ClientCode.ToString, ddlCategories.SelectedValue, B2P.Common.Enumerations.TransactionSources.Web)
            Dim z As New B2P.Payment.FeeDesciptions(BLL.SessionManager.ClientCode.ToString, ddlCategories.SelectedValue, B2P.Common.Enumerations.TransactionSources.Web)

            BLL.SessionManager.CreditFeeDescription = z.CreditCardFeeDescription
            BLL.SessionManager.AchFeeDescription = z.BankAccountFeeDescription

            BLL.SessionManager.CurrentCategory = CurrentCategory
            BLL.SessionManager.ProductName = ddlCategories.SelectedValue
            AddtoCart()
        End Sub

        Private Sub btnSubmit_Click(sender As Object, e As EventArgs) Handles btnSubmit.Click
            Dim CurrentCategory As New B2P.Objects.Product(BLL.SessionManager.ClientCode.ToString, ddlCategories.SelectedValue, B2P.Common.Enumerations.TransactionSources.Web)
            Dim z As New B2P.Payment.FeeDesciptions(BLL.SessionManager.ClientCode.ToString, ddlCategories.SelectedValue, B2P.Common.Enumerations.TransactionSources.Web)

            BLL.SessionManager.CreditFeeDescription = z.CreditCardFeeDescription
            BLL.SessionManager.AchFeeDescription = z.BankAccountFeeDescription

            BLL.SessionManager.CurrentCategory = CurrentCategory
            BLL.SessionManager.ProductName = ddlCategories.SelectedValue
            BLL.SessionManager.AccountNumber1 = Utility.SafeEncode(txtLookupAccount1.Text)
            BLL.SessionManager.AccountNumber2 = Utility.SafeEncode(txtLookupAccount2.Text)
            BLL.SessionManager.AccountNumber3 = Utility.SafeEncode(txtLookupAccount3.Text)

            If BLL.SessionManager.IsContactInfoRequired Then
                Response.Redirect("/pay/ContactInfo.aspx", False)
            Else
                Response.Redirect("/pay/payment.aspx", False)
            End If

        End Sub

        Protected Sub btnAddtoCart_Click(sender As Object, e As EventArgs) Handles btnAddtoCart.Click
            AddtoCart()
        End Sub

        Private Sub AddtoCart()

            Dim cart As New B2P.Cart.Cart
            cart.Item = ddlCategories.SelectedValue
            cart.AccountIdFields = New List(Of B2P.Cart.AccountIdField)
            If pnlAccount1.Visible = True Then
                Dim acc1 As New B2P.Cart.AccountIdField(lblAccountNumber1.Text, Utility.SafeEncode(txtLookupAccount1.Text))
                cart.AccountIdFields.Add(acc1)
            End If
            If pnlAccount2.Visible = True Then
                Dim acc2 As New B2P.Cart.AccountIdField(lblAccountNumber2.Text, Utility.SafeEncode(txtLookupAccount2.Text))
                cart.AccountIdFields.Add(acc2)
            End If
            If pnlAccount3.Visible = True Then
                Dim acc3 As New B2P.Cart.AccountIdField(lblAccountNumber3.Text, Utility.SafeEncode(txtLookupAccount3.Text))
                cart.AccountIdFields.Add(acc3)

            End If



            cart.Amount = ctlPropertyAddress.Amount
            cart.AmountDue = ctlPropertyAddress.Amount
            Dim propAddr As New B2P.Cart.PropertyAddress
            propAddr.Address1 = Utility.SafeEncode(ctlPropertyAddress.Address1)
            propAddr.Address2 = Utility.SafeEncode(ctlPropertyAddress.Address2)
            propAddr.City = Utility.SafeEncode(ctlPropertyAddress.City)
            If Not String.IsNullOrEmpty(ctlPropertyAddress.StateValue) Then
                propAddr.State = Utility.SafeEncode(ctlPropertyAddress.StateText)
            End If

            propAddr.Zip = Utility.SafeEncode(ctlPropertyAddress.Zip)

            cart.PropertyAddress = propAddr

            If BLL.SessionManager.ManageCart.AddToCart(cart) Then
                BLL.SessionManager.ManageCart.ShowCart = True
                BLL.SessionManager.ManageCart.EditItemIndex = -1
                Response.Redirect("~/pay/")
            Else
                Page.ClientScript.RegisterStartupScript(Me.GetType, "Show", "$(document).ready(function() { $('#pnlValidationDuplicate').modal({show: 'true', backdrop: 'static', keyboard: false}); });", True)
            End If
        End Sub

        Protected Sub btnAddMoreItem_Click(sender As Object, e As EventArgs) Handles btnAddMoreItem.Click
            BLL.SessionManager.ManageCart.ShowCart = False
            BLL.SessionManager.ManageCart.EditItemIndex = -1

            Response.Redirect("~/pay/")
        End Sub

    End Class
End Namespace
