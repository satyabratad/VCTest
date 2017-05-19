
Imports System.Configuration.ConfigurationManager
Imports Telerik.Web.UI
Imports System.Threading
Imports System.Drawing
Imports B2P.Common.ExtensionMethods


Public Class pin_pad1
    Inherits baseclass
#Region " Enums and Structures "
    Private Enum CardTypes
        Visa
        VisaDebit
        PINDebit
        MasterCard
        Amex
        Discover
    End Enum

    Private Enum EntryModes
        Keyed
        SwipedPINPad
        Token
    End Enum

    Private Structure CCPayment
        Dim PIN As String
        Dim MaskedCCNumber As String
        Dim BIN_SwipedCard As String
        Dim BIN_CreditCard As String
    End Structure

#End Region
    Dim UserSecurity As B2PAdminBLL.Role
    Dim x As New B2PAdminBLL.Security(AppSettings("ConnectionString"))
    Dim dsOffice As New DataSet
    Dim y As New B2P.Web.Counter.PaymentSupport
    'Dim z As B2P.ClientOptions.Product
    Dim z As B2P.Objects.Product
    Public tiListItems As B2P.Payment.PaymentBase.TransactionItems
    Dim listProducts As List(Of String)
    Dim acct As New B2P.Common.BlockedAccounts.BlockedAccountResults
    Dim sc As B2P.ShoppingCart.Cart
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        UserSecurity = CType(Session("RoleInfo"), B2PAdminBLL.Role)
        If Me.txtTrack1.Value <> "" Then   'card was swiped and has a value. make button green for clerk, and disable the cc validator due to masked value
            Me.btnSubmit.BackColor = Drawing.Color.Lime
            Me.ccValidator.Enabled = False
        Else
            Me.btnSubmit.BackColor = Drawing.Color.White
            Me.ccValidator.Enabled = True
        End If

        If Me.txtSwipeStatus.Value.Length > 0 Then
            Me.lblStatus.Text = Me.txtSwipeStatus.Value
        End If
        If Not IsPostBack Then
            Session("ClickedButton") = False
            Session("CardType") = Nothing
            Session("NonTaxItemsAmount") = 0
            Session("TaxItemsAmount") = 0
            Session("NonTaxAuth") = Nothing
            ClearCCFields()
            Session("ShoppingCart") = Nothing
            ddlProduct1.Focus()
            loadData()
            loadProducts()
            tbCreditCardNumber.Attributes.Add("onkeyup", "DetectKeyedCC(event);")
        End If
        AddHandler cvRoutingNumber.ServerCondition, AddressOf RoutingNumberValidation
        If Not UserSecurity.MakeACCPayment Then
            Response.Redirect("/error/default.aspx?type=security")
        End If
    End Sub

    Protected Sub ddlAUClient_SelectedIndexChanged(ByVal o As Object, ByVal e As Global.Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles ddlAUClient.SelectedIndexChanged
        loadOffices()
        ddlProduct1.Items.Clear()
        ClearCCFields()
        clearProducts()
        loadProducts()
    End Sub

    Protected Sub RadioButtonList1_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles RadioButtonList1.SelectedIndexChanged

        If Me.RadioButtonList1.SelectedValue = "CC" Then
            Me.pnlACH.Visible = False
            Me.pnlCreditCard.Visible = True
            loadCountry()
            loadExpirationDate()
            tbBankAccountNumber.Text = ""
            tbBankRoutingNumber.Text = ""
            'tbFirstName.Text = ""
            'tbLastName.Text = ""
            Page.Validate()
        Else
            Me.pnlCreditCard.Visible = False
            Me.pnlACH.Visible = True
            tbCreditCardNumber.Text = ""
            tbSecurityCode.Text = ""
            'tbFirstName.Text = ""
            'tbLastName.Text = ""
            BuildACH()
            Page.Validate()
        End If

    End Sub

    Protected Sub ddlCountry_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlCountry.SelectedIndexChanged
        SetCountrySpecs()
    End Sub
    Protected Sub ddlProduct1_SelectedIndexChanged(ByVal o As Object, ByVal e As Global.Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles ddlProduct1.SelectedIndexChanged
        Try

            Dim product1Selected As String = Replace(HttpUtility.HtmlEncode(ddlProduct1.SelectedValue), "&amp;", "&")

          
            If product1Selected <> "" Then

                Session("BlockedAccount") = Nothing

                ddlProduct1.SelectedValue = product1Selected
                CartButtons1.Visible = True
                Dim z As New B2P.Objects.Product(CStr(Session("UserClientCode")), product1Selected, B2P.Common.Enumerations.TransactionSources.CallCenter)
                Dim CurrentCategory As New B2P.Objects.Product(CStr(Session("UserClientCode")), ddlProduct1.SelectedValue, B2P.Common.Enumerations.TransactionSources.Counter)

                '*** added isnothing check - tmp 4/17/2014
                If Not IsNothing(CurrentCategory.WebOptions) Then
                    If CurrentCategory.WebOptions.AccountIDField1.Enabled = True Then
                        TextBoxWatermarkExtender1.WatermarkText = CurrentCategory.WebOptions.AccountIDField1.Label
                        txtProduct1Input.MaxLength = CurrentCategory.WebOptions.AccountIDField1.MaximumLength
                    End If
                End If
                If z.IsLookupEnabled Then
                    btnLookup1.Enabled = True
                    btnLookup1.ImageUrl = "/images/lookup.jpg"

                Else
                    btnCart1.Enabled = True
                    btnCart1.ImageUrl = "/images/cart.jpg"
                    btnLookup1.Enabled = False
                    btnLookup1.ImageUrl = "/images/lookupdisabled.jpg"
                End If


                With CurrentCategory.WebOptions
                    If .AccountIDField2.Enabled = True Then
                        TextBoxWatermarkExtender11.WatermarkText = .AccountIDField2.Label
                        txtProduct1aInput.Enabled = True
                        txtProduct1aInput.ReadOnly = False
                        txtProduct1aInput.MaxLength = .AccountIDField2.MaximumLength
                    Else
                        txtProduct1aInput.ReadOnly = True
                        txtProduct1aInput.Enabled = False
                        TextBoxWatermarkExtender11.WatermarkText = "Not required"
                    End If

                    If .AccountIDField3.Enabled = True Then
                        TextBoxWatermarkExtender12.WatermarkText = .AccountIDField3.Label
                        txtProduct1bInput.Enabled = True
                        txtProduct1bInput.ReadOnly = False
                        txtProduct1bInput.MaxLength = .AccountIDField3.MaximumLength
                    Else
                        txtProduct1bInput.ReadOnly = True
                        txtProduct1bInput.Enabled = False
                        TextBoxWatermarkExtender12.WatermarkText = "Not required"
                    End If
                End With

                SetFocus(txtProduct1Input)
            End If
        Catch ex As Exception
            'Response.Write(ex.Message)
            Response.Redirect("/error/default.aspx?type=system", False)
        End Try
    End Sub
    Protected Sub ddlProduct2_SelectedIndexChanged(ByVal o As Object, ByVal e As Global.Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles ddlProduct2.SelectedIndexChanged
        productSelect(2)
    End Sub
    Protected Sub ddlProduct3_SelectedIndexChanged(ByVal o As Object, ByVal e As Global.Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles ddlProduct3.SelectedIndexChanged
        productSelect(3)
    End Sub
    Protected Sub ddlProduct4_SelectedIndexChanged(ByVal o As Object, ByVal e As Global.Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles ddlProduct4.SelectedIndexChanged
        productSelect(4)
    End Sub
    Protected Sub ddlProduct5_SelectedIndexChanged(ByVal o As Object, ByVal e As Global.Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles ddlProduct5.SelectedIndexChanged
        productSelect(5)
    End Sub
    Private Sub btnCart1_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnCart1.Click
        checkBlock(1)
    End Sub
    Private Sub btnCart2_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnCart2.Click
        'addCart(2)
        checkBlock(2)
    End Sub
    Private Sub btnCart3_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnCart3.Click
        ' addCart(3)
        checkBlock(3)
    End Sub
    Private Sub btnCart4_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnCart4.Click
        'addCart(4)
        checkBlock(4)
    End Sub
    Private Sub btnCart5_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnCart5.Click
        'addCart(5)
        checkBlock(5)
    End Sub
    Private Sub btnRemove2_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnRemove2.Click
        removeProducts(2)
    End Sub
    Private Sub btnRemove3_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnRemove3.Click
        removeProducts(3)
    End Sub
    Private Sub btnRemove4_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnRemove4.Click
        removeProducts(4)
    End Sub
    Private Sub btnRemove5_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnRemove5.Click
        removeProducts(5)
    End Sub
    Private Sub ddlBankAccountType_SelectedIndexChanged(sender As Object, e As RadComboBoxSelectedIndexChangedEventArgs) Handles ddlBankAccountType.SelectedIndexChanged
        Select Case Me.ddlBankAccountType.SelectedValue
            Case "CommChecking"
                Me.lblCommercialMessage.Visible = True
            Case "CommSavings"
                Me.lblCommercialMessage.Visible = True
            Case Else
                Me.lblCommercialMessage.Visible = False
        End Select
        Me.ddlBankAccountType.Focus()
    End Sub
    Private Sub btnStartReader_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnStartReader.Click
        Dim HideCVVZip As Boolean = (txtSwiped.Value.ToLower <> "swiped")

        Me.txtZip.Visible = HideCVVZip
        Me.tbSecurityCode.Visible = HideCVVZip
        Me.lblCVVCode.Visible = HideCVVZip
        Me.lblBillingZip.Visible = HideCVVZip

        reqZip.Enabled = False
        reqCVV.Enabled = False
        reqFirstName.Enabled = False
    End Sub
    Protected Sub btnSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSubmit.Click
        Try
            sc = getItems()
            Session("ShoppingCart") = sc
            tiListItems = sc.GetCartItems(B2P.Payment.FeeCalculation.PaymentTypes.BankAccount, B2P.ShoppingCart.Cart.CartItemsTypes.All)
            If RadioButtonList1.SelectedValue = "CC" Then
                payCreditCard(sc)
            Else
                If CBool(Session("ClickedButton")) = False Then
                    Dim bapr As B2P.Payment.BankAccountPayment.BankAccountPaymentResults = payBankAccount(sc.GetCartItems(B2P.Payment.FeeCalculation.PaymentTypes.BankAccount, B2P.ShoppingCart.Cart.CartItemsTypes.All))

                    Select Case bapr.Result
                        Case B2P.Payment.BankAccountPayment.BankAccountPaymentResults.Results.Success
                            Session("ClickedButton") = True
                            btnSubmit.Enabled = False
                            Dim b As New B2P.ClientInterface.Manager.ClientInterface.ServiceInformation
                            Dim x As New B2P.ClientInterface.Manager.ClientInterface
                            Dim y As New B2P.ClientInterface.Manager.ClientInterfaceWS.PostBackResult
                            Dim c As B2P.ClientInterface.Manager.ClientInterfaceWS.PostbackInformation
                            Try


                                For Each ti As B2P.Payment.PaymentBase.TransactionItems.LineItem In tiListItems
                                    b = B2P.ClientInterface.Manager.ClientInterface.GetServiceURL(CStr(Session("UserClientCode")), ti.ProductName)

                                    If b.PostPaymentOption = B2P.Common.Enumerations.PostPaymentOptions.CreditCard Or b.PostPaymentOption = B2P.Common.Enumerations.PostPaymentOptions.Both Then
                                        'If b.PostPaymentOption = B2P.ClientOptions.Product.PostPaymentOptions.CreditCard Or b.PostPaymentOption = B2P.ClientOptions.Product.PostPaymentOptions.Both Then

                                        c = B2P.ClientInterface.Manager.Utility.GetPostBackInformation()

                                        c.AccountNumber1 = ti.AccountNumber1
                                        If txtProduct1aInput.Text.Trim <> "" Then
                                            c.AccountNumber2 = ti.AccountNumber2
                                        Else
                                            c.AccountNumber2 = ""
                                        End If
                                        If txtProduct1bInput.Text.Trim <> "" Then
                                            c.AccountNumber3 = ti.AccountNumber3
                                        Else
                                            c.AccountNumber3 = ""
                                        End If

                                        c.ConfirmationNumber = CStr(Session("ConfirmationNumber"))
                                        c.Amount = ti.Amount
                                        c.AuthorizationCode = CStr(Session("AuthorizationCode"))
                                        c.PaymentType = B2P.ClientInterface.Manager.ClientInterfaceWS.PaymentTypes.eCheck
                                        c.ClientCode = CStr(Session("UserClientCode"))
                                        c.ProductName = ti.ProductName
                                        c.PaymentAccountNumber = Right(HttpUtility.HtmlEncode(tbBankAccountNumber.Text), 4)
                                        c.Comments = Left(HttpUtility.HtmlEncode(txtNotes.Text), 200)
                                        c.Demographics.Address1.Value = ""
                                        c.PassThrough.Field1 = ""
                                        c.PaymentDate = Now()


                                        y = x.UpdateClientSystem(c)
                                        Session("PostBackMessage") = Nothing
                                        Select Case y.Result
                                            Case B2P.ClientInterface.Manager.ClientInterfaceWS.Results.Success
                                                Session("PostBackMessage") = "Success"
                                            Case B2P.ClientInterface.Manager.ClientInterfaceWS.Results.Failed
                                                Session("PostBackMessage") = y.Message
                                            Case B2P.ClientInterface.Manager.ClientInterfaceWS.Results.NotApplicable
                                                Session("PostBackMessage") = Nothing
                                        End Select

                                    End If

                                Next
                            Catch te As ThreadAbortException
                            Catch ex As Exception
                                Session("PostBackMessage") = "A portion of the postback process has failed."
                                B2P.Common.Logging.LogError("B2P Admin", "Error during postback " & ex.ToString, B2P.Common.Logging.NotifySupport.No)
                            End Try
                            x = Nothing
                            y = Nothing
                            c = Nothing
                            Response.Redirect("/payment/pinPadReturn.aspx")
                        Case Else
                            Session("ConfirmationNumber") = ""
                            lblMessageTitle.Text = "Payment Processing Error"
                            lblMessage.Text = "Message: Bank account payment was not processed."
                            mdlPopup.Show()
                    End Select
                Else
                    Response.Redirect("/payment/pinPadReturn.aspx")
                End If
            End If

        Catch te As ThreadAbortException
        Catch ex As Exception
            B2P.Common.Logging.LogError("B2P Admin", "Payment Processing Error: " & ex.ToString, B2P.Common.Logging.NotifySupport.No)
            lblMessageTitle.Text = "Payment Processing Error"
            lblMessage.Text = "Message: Payment was not processed."
            mdlPopup.Show()
        End Try
    End Sub


    Private Sub btnClearAll_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClearAll.Click
        Response.Redirect("/payment/pin-pad.aspx")
    End Sub

    Private Sub productSelect(ByVal productNumber As Integer)
        Dim productDropdown As Global.Telerik.Web.UI.RadComboBox = CType(FindControl(String.Format("ddlProduct{0}", productNumber)), Global.Telerik.Web.UI.RadComboBox)
        Dim buttonRemove As ImageButton = CType(FindControl(String.Format("btnRemove{0}", productNumber)), ImageButton)
        Dim buttonLookup As ImageButton = CType(FindControl(String.Format("btnLookup{0}", productNumber)), ImageButton)
        Dim buttonCart As ImageButton = CType(FindControl(String.Format("btnCart{0}", productNumber)), ImageButton)
        Dim productInput As TextBox = CType(FindControl(String.Format("txtProduct{0}Input", productNumber)), TextBox)
        Dim productInputa As TextBox = CType(FindControl(String.Format("txtProduct{0}aInput", productNumber)), TextBox)
        Dim productInputb As TextBox = CType(FindControl(String.Format("txtProduct{0}bInput", productNumber)), TextBox)
        Dim watermarkExtender As AjaxControlToolkit.TextBoxWatermarkExtender = CType(FindControl(String.Format("TextBoxWatermarkExtenderProduct{0}", productNumber)), AjaxControlToolkit.TextBoxWatermarkExtender)
        Dim watermarkExtendera As AjaxControlToolkit.TextBoxWatermarkExtender = CType(FindControl(String.Format("TextBoxWatermarkExtenderProduct{0}a", productNumber)), AjaxControlToolkit.TextBoxWatermarkExtender)
        Dim watermarkExtenderb As AjaxControlToolkit.TextBoxWatermarkExtender = CType(FindControl(String.Format("TextBoxWatermarkExtenderProduct{0}b", productNumber)), AjaxControlToolkit.TextBoxWatermarkExtender)

        If productDropdown.SelectedValue <> "" Then
            Dim myProduct As String = Replace(HttpUtility.HtmlEncode(productDropdown.SelectedValue), "&amp;", "&")

            Session("BlockedAccount") = Nothing

            With buttonRemove
                .Enabled = True
                .ImageUrl = "/images/remove.jpg"
            End With
            With buttonLookup
                Dim z As New B2P.Objects.Product(Session("UserClientCode").ToString, myProduct, B2P.Common.Enumerations.TransactionSources.Counter)
                Dim CurrentCategory As New B2P.Objects.Product(CStr(Session("UserClientCode")), myProduct, B2P.Common.Enumerations.TransactionSources.Counter)

                If CurrentCategory.WebOptions.AccountIDField1.Enabled = True Then
                    watermarkExtender.WatermarkText = CurrentCategory.WebOptions.AccountIDField1.Label
                    productInput.MaxLength = CurrentCategory.WebOptions.AccountIDField1.MaximumLength
                End If
                If z.IsLookupEnabled Then
                    .Enabled = True
                    .ImageUrl = "/images/lookup.jpg"
                Else
                    With buttonCart
                        .Enabled = True
                        .ImageUrl = "/images/cart.jpg"
                    End With
                    .Enabled = False
                    .ImageUrl = "/images/lookupdisabled.jpg"
                End If

                With CurrentCategory.WebOptions
                    If .AccountIDField2.Enabled = True Then
                        watermarkExtendera.WatermarkText = .AccountIDField2.Label
                        productInputa.Enabled = True
                        productInputa.ReadOnly = False
                        productInputa.MaxLength = .AccountIDField2.MaximumLength
                    Else
                        productInputa.ReadOnly = True
                        productInputa.Enabled = False
                        watermarkExtendera.WatermarkText = "Not required"
                    End If

                    If .AccountIDField3.Enabled = True Then
                        watermarkExtenderb.WatermarkText = .AccountIDField3.Label
                        productInputb.Enabled = True
                        productInputb.ReadOnly = False
                        productInputb.MaxLength = .AccountIDField3.MaximumLength
                    Else
                        productInputb.ReadOnly = True
                        productInputb.Enabled = False
                        watermarkExtenderb.WatermarkText = "Not required"
                    End If
                End With

            End With
            SetFocus(productInput)
        End If
    End Sub

    Private Sub loadOffices()
        Session("UserClientCode") = ddlAUClient.SelectedItem.Text
        x.ClientCode = ddlAUClient.SelectedItem.Text
        'x.Role = UserSecurity.UserRole
        x.UserLogin = CStr(Session("AdminLoginID"))
        ddlOffice.Items.Clear()
        dsOffice = x.LoadAvailableOffices

        Select Case dsOffice.Tables(0).Rows.Count
            Case 0
                ddlOffice.Visible = False
            Case 1
                ddlOffice.Visible = True
                ddlOffice.DataSource = dsOffice.Tables(0)
                ddlOffice.DataBind()
            Case Else
                ddlOffice.Visible = True
                ddlOffice.Items.Add(New RadComboBoxItem("Select Office", ""))
                ddlOffice.SelectedValue = ""
                ddlOffice.DataSource = dsOffice.Tables(0)
                ddlOffice.DataBind()
                ddlOffice.SortItems()
                If Not IsNothing(Session("SelectedOffice")) Then
                    ddlOffice.SelectedValue = Session("SelectedOffice")
                End If
        End Select

    End Sub

    Private Sub loadProducts()
        Try
            ddlProduct1.Items.Clear()


            ddlProduct1.Items.Add(New RadComboBoxItem("Select Product", ""))
            ddlProduct1.DataSource = B2P.Objects.Product.ListProducts(Session("UserClientCode").ToString, B2P.Common.Enumerations.TransactionSources.Counter)
            ddlProduct1.DataBind()


            'Set default product
            Dim a As B2P.Objects.Client = B2P.Objects.Client.GetClient(Session("UserClientCode").ToString)
            Dim lstItem As Global.Telerik.Web.UI.RadComboBoxItem


            lstItem = ddlProduct1.Items.FindItemByText(Trim(a.DefaultProductName))
            If Not IsNothing(lstItem) Then
                ddlProduct1.SelectedValue = Trim(a.DefaultProductName)
                btnCart1.ImageUrl = "/images/cart.jpg"
                btnCart1.Enabled = True
                Dim z As New B2P.Objects.Product(Session("UserClientCode").ToString, Replace(HttpUtility.HtmlEncode(ddlProduct1.SelectedValue), "&amp;", "&"), B2P.Common.Enumerations.TransactionSources.CallCenter)
                Dim CurrentCategory As New B2P.Objects.Product(CStr(Session("UserClientCode")), ddlProduct1.SelectedValue, B2P.Common.Enumerations.TransactionSources.Counter)

                '*** added isnothing check - tmp 4/17/2014
                If Not IsNothing(CurrentCategory.WebOptions) Then
                    If CurrentCategory.WebOptions.AccountIDField1.Enabled = True Then
                        TextBoxWatermarkExtender1.WatermarkText = CurrentCategory.WebOptions.AccountIDField1.Label
                        txtProduct1Input.MaxLength = CurrentCategory.WebOptions.AccountIDField1.MaximumLength
                    End If
                End If
                If z.IsLookupEnabled Then
                    btnLookup1.Enabled = True
                    btnLookup1.ImageUrl = "/images/lookup.jpg"

                    'With CurrentCategory.WebOptions
                    '    If .AccountIDField2.Enabled = True Then
                    '        TextBoxWatermarkExtender11.WatermarkText = .AccountIDField2.Label
                    '        txtProduct1aInput.Enabled = True
                    '        txtProduct1aInput.ReadOnly = False
                    '        txtProduct1aInput.MaxLength = .AccountIDField2.MaximumLength
                    '    Else
                    '        txtProduct1aInput.ReadOnly = True
                    '        txtProduct1aInput.Enabled = False
                    '        TextBoxWatermarkExtender11.WatermarkText = "Not required"
                    '    End If

                    '    If .AccountIDField3.Enabled = True Then
                    '        TextBoxWatermarkExtender12.WatermarkText = .AccountIDField3.Label
                    '        txtProduct1bInput.Enabled = True
                    '        txtProduct1bInput.ReadOnly = False
                    '        txtProduct1bInput.MaxLength = .AccountIDField3.MaximumLength
                    '    Else
                    '        txtProduct1bInput.ReadOnly = True
                    '        txtProduct1bInput.Enabled = False
                    '        TextBoxWatermarkExtender12.WatermarkText = "Not required"
                    '    End If
                    'End With
                Else
                    btnCart1.Enabled = True
                    btnCart1.ImageUrl = "/images/cart.jpg"
                    btnLookup1.Enabled = False
                    btnLookup1.ImageUrl = "/images/lookupdisabled.jpg"
                End If


                With CurrentCategory.WebOptions
                    If .AccountIDField2.Enabled = True Then
                        TextBoxWatermarkExtender11.WatermarkText = .AccountIDField2.Label
                        txtProduct1aInput.Enabled = True
                        txtProduct1aInput.ReadOnly = False
                        txtProduct1aInput.MaxLength = .AccountIDField2.MaximumLength
                    Else
                        txtProduct1aInput.ReadOnly = True
                        txtProduct1aInput.Enabled = False
                        TextBoxWatermarkExtender11.WatermarkText = "Not required"
                    End If

                    If .AccountIDField3.Enabled = True Then
                        TextBoxWatermarkExtender12.WatermarkText = .AccountIDField3.Label
                        txtProduct1bInput.Enabled = True
                        txtProduct1bInput.ReadOnly = False
                        txtProduct1bInput.MaxLength = .AccountIDField3.MaximumLength
                    Else
                        txtProduct1bInput.ReadOnly = True
                        txtProduct1bInput.Enabled = False
                        TextBoxWatermarkExtender12.WatermarkText = "Not required"
                    End If
                End With

            End If
        Catch ex As Exception
            lblMessageTitle.Text = "Error"
            lblMessage.Text = "Message: An unexpected error has occurred."
            mdlPopup.Show()
        End Try
    End Sub

    Protected Sub clearProducts()
        If pnlProduct2.Visible = True Then
            ddlProduct2.Items.Clear()
            txtProduct2Input.Text = ""
            txtProduct2aInput.Text = ""
            txtProduct2aInput.Enabled = False
            txtProduct2bInput.Text = ""
            txtProduct2bInput.Enabled = False
            TextBoxWatermarkExtenderProduct2.WatermarkText = "Account number 1"
            TextBoxWatermarkExtenderProduct2a.WatermarkText = "Not required"
            TextBoxWatermarkExtenderProduct2b.WatermarkText = "Not required"
            txtProduct2Amt.Text = ""
            pnlProduct2.Visible = False
        End If
        If pnlProduct3.Visible = True Then
            ddlProduct3.Items.Clear()
            txtProduct3Input.Text = ""
            txtProduct3aInput.Text = ""
            txtProduct3bInput.Text = ""
            txtProduct3aInput.Enabled = False
            txtProduct3bInput.Enabled = False
            TextBoxWatermarkExtenderProduct3.WatermarkText = "Account number 1"
            TextBoxWatermarkExtenderProduct3a.WatermarkText = "Not required"
            TextBoxWatermarkExtenderProduct3b.WatermarkText = "Not required"
            txtProduct3Amt.Text = ""
            pnlProduct3.Visible = False
        End If
        If pnlProduct4.Visible = True Then
            ddlProduct4.Items.Clear()
            txtProduct4Input.Text = ""
            txtProduct4aInput.Text = ""
            txtProduct4bInput.Text = ""
            txtProduct4aInput.Enabled = False
            txtProduct4bInput.Enabled = False
            TextBoxWatermarkExtenderProduct4.WatermarkText = "Account number 1"
            TextBoxWatermarkExtenderProduct4a.WatermarkText = "Not required"
            TextBoxWatermarkExtenderProduct4b.WatermarkText = "Not required"
            txtProduct4Amt.Text = ""
            pnlProduct4.Visible = False
        End If
        If pnlProduct5.Visible = True Then
            ddlProduct5.Items.Clear()
            txtProduct5Input.Text = ""
            txtProduct5aInput.Text = ""
            txtProduct5bInput.Text = ""
            txtProduct5aInput.Enabled = False
            txtProduct5bInput.Enabled = False
            TextBoxWatermarkExtenderProduct5.WatermarkText = "Account number 1"
            TextBoxWatermarkExtenderProduct5a.WatermarkText = "Not required"
            TextBoxWatermarkExtenderProduct5b.WatermarkText = "Not required"
            txtProduct5Amt.Text = ""
            pnlProduct5.Visible = False
        End If
        ddlProduct1.Enabled = True
        With txtProduct1Input
            .Text = ""
            .Enabled = True
            '.CssClass = "inputtext"
        End With
        With txtProduct1aInput
            .Text = ""
            .Enabled = False
            ' .CssClass = "inputtext"
        End With
        With txtProduct1bInput
            .Text = ""
            .Enabled = False
            '.CssClass = "inputtext"
        End With
        With txtProduct1Amt
            .Text = ""
            .Enabled = True
            ' .CssClass = "inputtext"
        End With
        TextBoxWatermarkExtender1.WatermarkText = "Account number 1"
        TextBoxWatermarkExtender11.WatermarkText = "Not required"
        TextBoxWatermarkExtender12.WatermarkText = "Not required"
        lblSubTotal.Text = "Total Amount: $0.00"

    End Sub

    Private Sub SetCountrySpecs()
        txtZip.Text = ""
        Select Case ddlCountry.SelectedValue
            Case "US", ""
                Me.regZip.Enabled = True
                Me.lblBillingZip.Text = "Zip"
                Me.txtZip.MaxLength = 5
                Me.FilteredTextBoxExtender9.FilterType = AjaxControlToolkit.FilterTypes.Numbers
                Me.regZip.Expression = "^[a-zA-Z0-9]{5,9}$"
            Case "CA"
                Me.lblBillingZip.Text = "Postal Code"
                Me.txtZip.MaxLength = 7
                Me.FilteredTextBoxExtender9.FilterType = AjaxControlToolkit.FilterTypes.LowercaseLetters Or AjaxControlToolkit.FilterTypes.Numbers Or AjaxControlToolkit.FilterTypes.UppercaseLetters Or AjaxControlToolkit.FilterTypes.Custom
                Me.FilteredTextBoxExtender9.ValidChars = " "
                Me.regZip.Expression = "^[a-zA-Z][0-9][a-zA-Z]\s?[0-9][a-zA-Z][0-9]$"
            Case Else
                Me.lblBillingZip.Text = "Postal Code"
                Me.txtZip.MaxLength = 7
                Me.FilteredTextBoxExtender9.FilterType = AjaxControlToolkit.FilterTypes.LowercaseLetters Or AjaxControlToolkit.FilterTypes.Numbers Or AjaxControlToolkit.FilterTypes.UppercaseLetters Or AjaxControlToolkit.FilterTypes.Custom
                Me.FilteredTextBoxExtender9.ValidChars = " "
                Me.regZip.Enabled = False
        End Select
    End Sub

    Private Sub loadData()

        x.ClientCode = CStr(Session("ClientCode"))
        x.Role = UserSecurity.UserRole
        x.UserLogin = CStr(Session("AdminLoginID"))

        Dim ds As DataSet = x.LoadDropDownBoxes
        Session("UserClientCode") = Session("ClientCode")
        If CStr(Session("ClientCode")) = "B2P" Then
            ddlAUClient.DataSource = ds.Tables(0)
            ddlAUClient.DataTextField = ds.Tables(0).Columns("ClientCode").ColumnName.ToString()
            ddlAUClient.DataBind()
            ddlAUClient.Visible = True
            ddlAUClient.Items.FindItemByText("B2P").Selected = True
        Else
            ddlAUClient.Visible = False
        End If
        dsOffice = x.ListCounterOffices
        Select Case dsOffice.Tables(0).Rows.Count
            Case 0
                ddlOffice.Visible = False
            Case 1
                ddlOffice.Visible = True
                ddlOffice.DataSource = dsOffice.Tables(0)
                ddlOffice.DataBind()
            Case Else
                ddlOffice.Visible = True
                ddlOffice.Items.Add(New RadComboBoxItem("Select Office", ""))
                ddlOffice.SelectedValue = ""
                ddlOffice.DataSource = dsOffice.Tables(0)
                ddlOffice.DataBind()
                ddlOffice.SortItems()
                If Not IsNothing(Session("SelectedOffice")) Then
                    ddlOffice.SelectedValue = Session("SelectedOffice")
                End If
        End Select

    End Sub

    Private Sub checkBlock(ByVal productNumber As Integer)
        Dim productDropdown As Global.Telerik.Web.UI.RadComboBox = CType(FindControl(String.Format("ddlProduct{0}", productNumber)), Global.Telerik.Web.UI.RadComboBox)
        'Dim productDropdownNext As Global.Telerik.Web.UI.RadComboBox = CType(FindControl(String.Format("ddlProduct{0}", (productNumber + 1))), Global.Telerik.Web.UI.RadComboBox)
        Dim productInput As TextBox = CType(FindControl(String.Format("txtProduct{0}Input", productNumber)), TextBox)
        Dim productInputa As TextBox = CType(FindControl(String.Format("txtProduct{0}aInput", productNumber)), TextBox)
        Dim productInputb As TextBox = CType(FindControl(String.Format("txtProduct{0}bInput", productNumber)), TextBox)
        Dim productAmount As TextBox = CType(FindControl(String.Format("txtProduct{0}Amt", productNumber)), TextBox)
        Dim buttonLookup As ImageButton = CType(FindControl(String.Format("btnLookup{0}", productNumber)), ImageButton)

        If productDropdown.SelectedValue <> "" And productInput.Text <> "" And productAmount.Text <> "" Then
            'if not lookup  check block account
            If buttonLookup.Enabled = False Then
                If checkBlockedAccount(CStr(Session("UserClientCode")), productDropdown.SelectedValue, productInput.Text, productInputa.Text, productInputb.Text, RadioButtonList1.SelectedValue) Then
                    lblProductNumber.Text = productNumber.ToString()
                    'pnlBlock.Visible = True
                    lblBlockBy2.Text = "Blocked By: " & acct.BlockUser
                    lblBlockDate2.Text = "Blocked Date: " & acct.BlockDate
                    lblBlockComment2.Text = "Block Comment: " & acct.BlockComment
                    ckBlockOverride.Checked = False

                    modalBlock.Show()
                Else
                    addCart(productNumber)
                End If

            Else
                addCart(productNumber)
            End If
        End If

    End Sub

    Private Sub addCart(ByVal productNumber As Integer)
        Dim productDropdown As Global.Telerik.Web.UI.RadComboBox = CType(FindControl(String.Format("ddlProduct{0}", productNumber)), Global.Telerik.Web.UI.RadComboBox)
        Dim productDropdownNext As Global.Telerik.Web.UI.RadComboBox = CType(FindControl(String.Format("ddlProduct{0}", (productNumber + 1))), Global.Telerik.Web.UI.RadComboBox)
        Dim productInput As TextBox = CType(FindControl(String.Format("txtProduct{0}Input", productNumber)), TextBox)
        Dim productInputa As TextBox = CType(FindControl(String.Format("txtProduct{0}aInput", productNumber)), TextBox)
        Dim productInputb As TextBox = CType(FindControl(String.Format("txtProduct{0}bInput", productNumber)), TextBox)
        Dim productAmount As TextBox = CType(FindControl(String.Format("txtProduct{0}Amt", productNumber)), TextBox)
        Dim buttonRemove As ImageButton = CType(FindControl(String.Format("btnRemove{0}", productNumber)), ImageButton)
        Dim buttonLookup As ImageButton = CType(FindControl(String.Format("btnLookup{0}", productNumber)), ImageButton)
        Dim buttonCart As ImageButton = CType(FindControl(String.Format("btnCart{0}", productNumber)), ImageButton)
        Dim pnlProductNext As Panel = CType(FindControl(String.Format("pnlProduct{0}", (productNumber + 1))), Panel)
        Dim a As New B2P.Web.Counter.PaymentSupport
        Dim ItemCount As Integer = a.GetAllowedItemCount(CStr(Session("UserClientCode")), B2P.Web.Counter.PaymentSupport.Sources.Counter)

        If productDropdown.SelectedValue <> "" And productInput.Text <> "" And productAmount.Text <> "" Then
            With productInput
                '.CssClass = "inputText"
                .Enabled = False
            End With

            sc = getItems()
            If Not IsNothing(sc) Then
                If productNumber <> 5 Then
                    If productNumber <> ItemCount Then
                        pnlProductNext.Visible = True
                        productDropdownNext.Items.Clear()
                        productDropdownNext.Enabled = True
                        productDropdownNext.Items.Add(New RadComboBoxItem("Select Product", ""))
                        productDropdownNext.DataSource = B2P.Objects.Product.ListProducts(Session("UserClientCode").ToString, B2P.Common.Enumerations.TransactionSources.Counter)
                        productDropdownNext.DataBind()
                    End If

                End If
                With buttonCart
                    .ImageUrl = "/images/cartdisabled.jpg"
                    .Enabled = False
                End With

                With productAmount
                    '.CssClass = "disabled"
                    .Enabled = False
                End With
                With productDropdown
                    .Enabled = False
                    '.CssClass = "disabled"
                End With
                With buttonLookup
                    .Enabled = False
                    .ImageUrl = "/images/lookupdisabled.jpg"
                End With
                If productNumber <> 1 Then
                    With buttonRemove
                        .Enabled = True
                        .ImageUrl = "/images/remove.jpg"
                    End With
                End If
                With productInputa
                    .Enabled = False
                End With

                With productInputb
                    .Enabled = False
                End With

                If sc.eCheckStatus <> B2P.Common.Enumerations.ReturnCodes.Success And sc.CreditCardStatus <> B2P.Common.Enumerations.ReturnCodes.Success Then
                    pnlPaymentInformation.Visible = False
                    lblMessageTitle.Text = "Invalid Product and/or Payment Selection"
                    lblMessage.Text = "Message: Amount not within valid range."
                    btnSubmit.Enabled = False
                    mdlPopup.Show()
                Else
                    pnlPaymentInformation.Visible = True
                    If sc.AcceptECheck Then
                        RadioButtonList1.Items.FindByValue("ACH").Enabled = True
                        lblACHTotal.Text = String.Format("{0:c} (Fee: ${1:#,###.00})", sc.ECheckFee + sc.TotalAmount, sc.ECheckFee)
                        If Not (sc.AcceptAmex Or sc.AcceptDiscover Or sc.AcceptMasterCard Or sc.AcceptVisa) Then
                            RadioButtonList1.SelectedValue = "ACH"
                            pnlACH.Visible = True
                            BuildACH()
                        End If
                    Else
                        RadioButtonList1.Items.FindByValue("ACH").Enabled = False
                        pnlACH.Visible = False
                    End If

                    If sc.AcceptAmex Or sc.AcceptDiscover Or sc.AcceptMasterCard Or sc.AcceptVisa Then
                        RadioButtonList1.Items.FindByValue("CC").Enabled = True

                        If RadioButtonList1.Items.FindByValue("ACH").Enabled = True And RadioButtonList1.SelectedValue = "ACH" Then
                            pnlACH.Visible = True
                            pnlCreditCard.Visible = False
                        Else
                            pnlCreditCard.Visible = True
                            RadioButtonList1.SelectedValue = "CC"
                            pnlACH.Visible = False
                        End If

                        If sc.AcceptCreditCards = True Then
                            lblTotalCredit.Text = String.Format("{0:c} (Fee: ${1:#,###.00})", sc.CreditCardFee + sc.TotalAmount, sc.CreditCardFee)
                            txtAllowCredit.Value = "T"
                        Else
                            lblTotalCredit.Text = "Not Available"
                            txtAllowCredit.Value = "F"
                        End If

                        If sc.AcceptPinDebit = True Then
                            'pnlDebitCard.Visible = True
                            lblTotalDebit.Text = String.Format("{0:c} (Fee: ${1:#,###.00})", sc.PinDebitFee + sc.TotalAmount, sc.PinDebitFee)
                            txtAllowDebit.Value = "T"
                        Else
                            'pnlDebitCard.Visible = False
                            lblTotalDebit.Text = "Not Available"
                            txtAllowDebit.Value = "F"
                        End If
                        If Me.txtTrack1.Value <> "" Then   'card was swiped and has a value. make button green for clerk, and disable the cc validator due to masked value
                            Me.btnSubmit.BackColor = Drawing.Color.Lime
                            Me.ccValidator.Enabled = False
                        Else
                            Me.btnSubmit.BackColor = Drawing.Color.White
                            Me.ccValidator.Enabled = True
                        End If

                        If Me.txtSwipeStatus.Value.Length > 0 Then
                            Me.lblStatus.Text = Me.txtSwipeStatus.Value
                        End If
                        LoadCountry()
                        LoadExpirationDate()
                    Else
                        RadioButtonList1.Items.FindByValue("CC").Enabled = False
                        pnlCreditCard.Visible = False
                        If RadioButtonList1.Items.FindByValue("ACH").Enabled = False Then
                            pnlPaymentInformation.Visible = False
                        End If
                    End If
                    'end if goes here
                End If
            Else
                lblMessageTitle.Text = "Invalid Product and/or Payment Selection"
                btnSubmit.Enabled = False
                lblMessage.Text = "Message: Transaction was not processed."
                mdlPopup.Show()
            End If
        End If

    End Sub

    Private Function getItems() As B2P.ShoppingCart.Cart
        Try
            Dim strProduct As String
            Dim lis As New B2P.Payment.PaymentBase.TransactionItems

            If txtProduct1Input.Text.Trim <> "" And txtProduct1Input.Enabled = False Then
                strProduct = Replace(HttpUtility.HtmlEncode(ddlProduct1.SelectedItem.Text), "&amp;", "&")
                lis.Add(HttpUtility.HtmlEncode(txtProduct1Input.Text.Trim), HttpUtility.HtmlEncode(txtProduct1aInput.Text.Trim), HttpUtility.HtmlEncode(txtProduct1bInput.Text.Trim), _
                       strProduct, CDec(HttpUtility.HtmlEncode(txtProduct1Amt.Text)), 0, 0)

            End If

            If txtProduct2Input.Text.Trim <> "" And txtProduct2Input.Enabled = False Then
                strProduct = Replace(HttpUtility.HtmlEncode(ddlProduct2.SelectedItem.Text), "&amp;", "&")
                lis.Add(HttpUtility.HtmlEncode(txtProduct2Input.Text.Trim), HttpUtility.HtmlEncode(txtProduct2aInput.Text.Trim), HttpUtility.HtmlEncode(txtProduct2bInput.Text.Trim), _
                   strProduct, CDec(HttpUtility.HtmlEncode(txtProduct2Amt.Text)), 0, 0)
            End If

            If txtProduct3Input.Text.Trim <> "" And txtProduct3Input.Enabled = False Then
                strProduct = Replace(HttpUtility.HtmlEncode(ddlProduct3.SelectedItem.Text), "&amp;", "&")
                lis.Add(HttpUtility.HtmlEncode(txtProduct3Input.Text.Trim), HttpUtility.HtmlEncode(txtProduct3aInput.Text.Trim), HttpUtility.HtmlEncode(txtProduct3bInput.Text.Trim), _
                   strProduct, CDec(HttpUtility.HtmlEncode(txtProduct3Amt.Text)), 0, 0)
            End If

            If txtProduct4Input.Text.Trim <> "" And txtProduct4Input.Enabled = False Then
                strProduct = Replace(HttpUtility.HtmlEncode(ddlProduct4.SelectedItem.Text), "&amp;", "&")
                lis.Add(HttpUtility.HtmlEncode(txtProduct4Input.Text.Trim), HttpUtility.HtmlEncode(txtProduct4aInput.Text.Trim), HttpUtility.HtmlEncode(txtProduct4bInput.Text.Trim), _
                   strProduct, CDec(HttpUtility.HtmlEncode(txtProduct4Amt.Text)), 0, 0)
            End If

            If txtProduct5Input.Text.Trim <> "" And txtProduct5Input.Enabled = False Then
                strProduct = Replace(HttpUtility.HtmlEncode(ddlProduct5.SelectedItem.Text), "&amp;", "&")
                lis.Add(HttpUtility.HtmlEncode(txtProduct5Input.Text.Trim), HttpUtility.HtmlEncode(txtProduct5aInput.Text.Trim), HttpUtility.HtmlEncode(txtProduct5bInput.Text.Trim), _
                   strProduct, CDec(HttpUtility.HtmlEncode(txtProduct5Amt.Text)), 0, 0)
            End If
            Dim sc As B2P.ShoppingCart.Cart = New B2P.ShoppingCart.Cart(CStr(Session("UserClientCode")), B2P.Common.Enumerations.TransactionSources.Counter, lis)
            lblSubTotal.Text = String.Format("Sub Total:  ${0:#,###.00}", sc.TotalAmount)

            Return sc
        Catch ex As Exception
            lblMessage.Text = "INVALID TRANSACTION"
            lblMessage.ForeColor = Drawing.Color.Red
        End Try
    End Function

    Private Function checkBlockedAccount(clientCode As String, product As String, acct1 As String, acct2 As String, acct3 As String, payType As String) As Boolean
        Dim IsBlocked As Boolean = False
        Dim SearchResults As New B2P.Common.BlockedAccounts.BlockedAccountResults

        SearchResults = B2P.Common.BlockedAccounts.CheckForBlockedAccount(clientCode, product, acct1, acct2, acct3)

        If SearchResults.IsAccountBlocked Then
            If payType = "CC" And SearchResults.CreditCardBlocked Then
                acct = SearchResults
                Session("BlockedAccount") = acct
                IsBlocked = True
            ElseIf payType = "ACH" And SearchResults.ACHBlocked Then
                acct = SearchResults
                Session("BlockedAccount") = acct
                IsBlocked = True
            End If
        Else
            Session("BlockedAccount") = Nothing
        End If

        Return IsBlocked

    End Function

    Private Sub removeProducts(ByVal productNumber As Integer)
        Dim productDropdown As Global.Telerik.Web.UI.RadComboBox = CType(FindControl(String.Format("ddlProduct{0}", productNumber)), Global.Telerik.Web.UI.RadComboBox)
        Dim productInput As TextBox = CType(FindControl(String.Format("txtProduct{0}Input", productNumber)), TextBox)
        Dim productInputa As TextBox = CType(FindControl(String.Format("txtProduct{0}aInput", productNumber)), TextBox)
        Dim productInputb As TextBox = CType(FindControl(String.Format("txtProduct{0}bInput", productNumber)), TextBox)
        Dim productAmount As TextBox = CType(FindControl(String.Format("txtProduct{0}Amt", productNumber)), TextBox)
        Dim buttonRemove As ImageButton = CType(FindControl(String.Format("btnRemove{0}", productNumber)), ImageButton)
        Dim buttonLookup As ImageButton = CType(FindControl(String.Format("btnLookup{0}", productNumber)), ImageButton)
        Dim buttonCart As ImageButton = CType(FindControl(String.Format("btnCart{0}", productNumber)), ImageButton)
        Dim watermarkExtender As AjaxControlToolkit.TextBoxWatermarkExtender = CType(FindControl(String.Format("TextBoxWatermarkExtenderProduct{0}", productNumber)), AjaxControlToolkit.TextBoxWatermarkExtender)
        Dim watermarkExtendera As AjaxControlToolkit.TextBoxWatermarkExtender = CType(FindControl(String.Format("TextBoxWatermarkExtenderProduct{0}a", productNumber)), AjaxControlToolkit.TextBoxWatermarkExtender)
        Dim watermarkExtenderb As AjaxControlToolkit.TextBoxWatermarkExtender = CType(FindControl(String.Format("TextBoxWatermarkExtenderProduct{0}b", productNumber)), AjaxControlToolkit.TextBoxWatermarkExtender)


        With productDropdown
            .SelectedValue = ""
            .Enabled = True
            '.CssClass = "inputtext"
        End With

        With productInput
            .Text = ""
            .Enabled = True
            '.CssClass = "inputtext"
        End With

        With productInputa
            .Text = ""
            .Enabled = False
        End With

        With productInputb
            .Text = ""
            .Enabled = False
        End With

        With productAmount
            .Text = ""
            .Enabled = True
            '.CssClass = "inputtext"
        End With

        With buttonRemove
            .Enabled = True
            .ImageUrl = "/images/removedisabled.jpg"
        End With
        With buttonCart
            .Enabled = False
            .ImageUrl = "/images/cartdisabled.jpg"
        End With

        With buttonLookup
            .Enabled = False
            .ImageUrl = "/images/lookupdisabled.jpg"
        End With
        watermarkExtender.WatermarkText = "Account Number 1"
        watermarkExtendera.WatermarkText = "Not required"
        watermarkExtenderb.WatermarkText = "Not required"


        sc = getItems()


        If sc.eCheckStatus <> B2P.Common.Enumerations.ReturnCodes.Success And sc.CreditCardStatus <> B2P.Common.Enumerations.ReturnCodes.Success Then
            pnlPaymentInformation.Visible = False
            lblMessageTitle.Text = "Invalid Product and/or Payment Selection"
            lblMessage.Text = "Message: Amount not within valid range."
            btnSubmit.Enabled = False
            mdlPopup.Show()
        Else
            pnlPaymentInformation.Visible = True

            If sc.AcceptECheck Then
                lblACHTotal.Text = String.Format("{0:c} (Fee: ${1:#,###.00})", sc.ECheckFee + sc.TotalAmount, sc.ECheckFee)
            End If

            If sc.AcceptAmex Or sc.AcceptDiscover Or sc.AcceptMasterCard Or sc.AcceptVisa Then
                If sc.AcceptCreditCards = True Then
                    lblTotalCredit.Text = String.Format("{0:c} (Fee: ${1:#,###.00})", sc.CreditCardFee + sc.TotalAmount, sc.CreditCardFee)
                End If
                If sc.AcceptPinDebit = True Then
                    'pnlDebitCard.Visible = True
                    lblTotalDebit.Text = String.Format("{0:c} (Fee: ${1:#,###.00})", sc.PinDebitFee + sc.TotalAmount, sc.PinDebitFee)
                End If
            End If

        End If
    End Sub

    Private Function payBankAccount(ByVal tiListItems As B2P.Payment.PaymentBase.TransactionItems) As B2P.Payment.BankAccountPayment.BankAccountPaymentResults
        Try
            Session("CardType") = Nothing
            Dim x As New B2P.Payment.BankAccountPayment(Session("UserClientCode").ToString)
            x.UserComments = Left(HttpUtility.HtmlEncode(txtNotes.Text), 200)
            If ddlOffice.SelectedValue = "" Then
                x.Office_ID = 0
            Else
                x.Office_ID = CInt(HttpUtility.HtmlEncode(ddlOffice.SelectedValue))
                'Keep current selected office stored for users to make another payment with stored office selection
                Session("SelectedOffice") = (HttpUtility.HtmlEncode(ddlOffice.SelectedValue))
            End If
            x.ClientCode = Session("UserClientCode").ToString
            x.Security_ID = CInt(Session("SecurityID"))
            x.AllowConfirmationEmails = True
            x.VendorReferenceCode = ""
            x.PaymentSource = B2P.Common.Enumerations.TransactionSources.Counter
            If ddlOffice.SelectedValue = "" Then
                x.Office_ID = 0
            Else
                x.Office_ID = CInt(HttpUtility.HtmlEncode(ddlOffice.SelectedValue))
                'Keep current selected office stored for users to make another payment with stored office selection
                Session("SelectedOffice") = (HttpUtility.HtmlEncode(ddlOffice.SelectedValue))
            End If

            Dim ba As New B2P.Common.Objects.BankAccount
            ba.Owner.FirstName = HttpUtility.HtmlEncode(tbFirstName.Text)
            ba.Owner.LastName = HttpUtility.HtmlEncode(tbLastName.Text)
            ba.Owner.Address1 = "NOT PROVIDED"
            ba.Owner.Address2 = ""
            ba.Owner.City = "NOT PROVIDED"
            ba.Owner.State = "XX"
            ba.Owner.ZipCode = HttpUtility.HtmlEncode(txtZip.Text)
            If tbEmailAddress.Text <> "" Then
                ba.Owner.EMailAddress = HttpUtility.HtmlEncode(tbEmailAddress.Text.Trim)
            Else
                ba.Owner.EMailAddress = "null@cybersource.com"
            End If

            If txtPhone.Text.Trim <> "" Then
                ba.Owner.PhoneNumber = txtPhone.Text.Trim
            Else
                ba.Owner.PhoneNumber = ""
            End If
            ba.BankAccountNumber = HttpUtility.HtmlEncode(tbBankAccountNumber.Text)
            ba.BankRoutingNumber = HttpUtility.HtmlEncode(tbBankRoutingNumber.Text)

            Select Case ddlBankAccountType.SelectedValue
                Case "Checking"
                    ba.BankAccountType = B2P.Common.Objects.BankAccount.BankAccountTypes.Personal_Checking
                    Session("OriginatorID") = ""
                Case "Savings"
                    ba.BankAccountType = B2P.Common.Objects.BankAccount.BankAccountTypes.Personal_Savings
                    Session("OriginatorID") = ""
                Case "CommChecking"
                    ba.BankAccountType = B2P.Common.Objects.BankAccount.BankAccountTypes.Commercial_Checking
                    Session("OriginatorID") = B2P.Payment.BankAccountPayment.GetCompanyID(CStr(Session("UserClientCode")))
                Case "CommSavings"
                    ba.BankAccountType = B2P.Common.Objects.BankAccount.BankAccountTypes.Commercial_Savings
                    Session("OriginatorID") = B2P.Payment.BankAccountPayment.GetCompanyID(CStr(Session("UserClientCode")))
            End Select

            x.Items = tiListItems
            If Page.IsValid = True Then
                Dim bapr As B2P.Payment.BankAccountPayment.BankAccountPaymentResults = x.PayByBankAccount(ba)
                If bapr.Result = B2P.Payment.BankAccountPayment.BankAccountPaymentResults.Results.Success Then
                    Session("TaxItemsAmount") = 0
                    Session("TaxFee") = 0
                    Session("NonTaxItemsAmount") = x.Items.TotalAmount
                    Session("NonTaxFee") = x.Items.TotalFee
                    Session("NonTaxConfirm") = bapr.ConfirmationNumber
                End If
                Return bapr
            End If

        Catch te As ThreadAbortException
        Catch ex As Exception
            B2P.Common.Logging.LogError("PMS", "Error during ach payment " & ex.ToString, B2P.Common.Logging.NotifySupport.No)
            Response.Redirect("/errors/", False)
            HttpContext.Current.ApplicationInstance.CompleteRequest()
        End Try
    End Function

    Private Sub BuildACH()
        ddlBankAccountType.DataSourceID = "XmlDataSource1"
        ddlBankAccountType.DataBind()
        ddlBankAccountType.SortItems()

    End Sub

    Private Sub LoadExpirationDate()
        With ddlMonth
            .Items.Clear()
            .Items.Add(New ListItem("01", "01"))
            .Items.Add(New ListItem("02", "02"))
            .Items.Add(New ListItem("03", "03"))
            .Items.Add(New ListItem("04", "04"))
            .Items.Add(New ListItem("05", "05"))
            .Items.Add(New ListItem("06", "06"))
            .Items.Add(New ListItem("07", "07"))
            .Items.Add(New ListItem("08", "08"))
            .Items.Add(New ListItem("09", "09"))
            .Items.Add(New ListItem("10", "10"))
            .Items.Add(New ListItem("11", "11"))
            .Items.Add(New ListItem("12", "12"))
            .SelectedItem.Text = "01"
        End With
        LoadYear()

    End Sub

    Private Sub LoadYear()

        With ddlYear
            .Items.Clear()
            Dim i, x As Integer
            x = Year(Today())

            For i = x To x + 10
                .Items.Add(New ListItem(CStr(i)))
            Next
            ddlMonth.Focus()
        End With


    End Sub

    Private Sub LoadCountry()
        With ddlCountry
            .Items.Clear()
            .DataSource = B2P.Payment.PaymentBase.ListCountries
            .DataTextField = "CountryName"
            .DataValueField = "CountryCode"
            .DataBind()
        End With

    End Sub

    Private Function CheckCardType(ByVal cart As B2P.ShoppingCart.Cart) As Boolean
        Dim CardType As CardTypes

        If Not txtPINEPB.Value Is Nothing AndAlso txtPINEPB.Value.Trim.Length > 0 Then
            CardType = CardTypes.PINDebit
            Session("CardIssuer") = "PIN Debit"
        Else
            Dim BIN As String = String.Empty

            If Not MaskCCNumber(tbCreditCardNumber.Text.Trim) Is Nothing AndAlso MaskCCNumber(tbCreditCardNumber.Text.Trim).Length > 0 Then
                BIN = tbCreditCardNumber.Text.Trim.Substring(0, 6)
            End If

            Dim CardName As String = B2P.Common.Objects.CreditCard.GetCreditCardType(BIN)

            Select Case CardName.ToLower
                Case "mastercard"
                    CardType = CardTypes.MasterCard
                    Session("CardIssuer") = "MasterCard"
                Case "visa"
                    'If B2P.Payment.FeeCalculation.GetCardType(BIN) = B2P.Payment.FeeCalculation.PaymentTypes.DebitCard Then
                    '    CardType = CardTypes.VisaDebit
                    'Else
                    CardType = CardTypes.Visa
                    'End If
                    Session("CardIssuer") = "Visa"
                Case "discover"
                    CardType = CardTypes.Discover
                    Session("CardIssuer") = "Discover"
                Case "american express"
                    CardType = CardTypes.Amex
                    Session("CardIssuer") = "American Express"
                Case Else
                    Session("CardIssuer") = ""
                    B2P.Common.Logging.LogError("ServerInterface.GetCardType2", String.Format("Invalid response: {0}", CardType), B2P.Common.Logging.NotifySupport.Yes)
                    Throw New ApplicationException(String.Format("Invalid response: {0}", CardType))
            End Select
        End If

        Select Case CardType
            Case CardTypes.Visa
                Return cart.AcceptVisa
            Case CardTypes.VisaDebit
                Return cart.AcceptPinDebit
            Case CardTypes.PINDebit
                Return cart.AcceptPinDebit
            Case CardTypes.MasterCard
                Return cart.AcceptMasterCard
            Case CardTypes.Discover
                Return cart.AcceptDiscover
            Case CardTypes.Amex
                Return cart.AcceptAmex
        End Select

    End Function

    Private Function MaskCCNumber(ByVal creditCardNumber As String) As String
        Dim tFirst6 As String = ""
        Dim tLast4 As String = ""
        Dim tMask As String = "*"
        Dim iPadValue As Int32 = 0
        Dim tResult As String = ""

        If creditCardNumber.Trim.Length < 10 Then
            Return creditCardNumber
        Else
            tFirst6 = creditCardNumber.Substring(0, 6)
            tLast4 = Right(creditCardNumber, 4)
            iPadValue = creditCardNumber.Trim.Length - 10
            tMask = tMask.PadRight(iPadValue, "*"c)

            tResult = String.Format("{0}{1}{2}", tFirst6, tMask, tLast4)

            Return tResult
        End If

    End Function


    Private Sub ClearCCFields()
        txtZip.Text = ""
        tbCreditCardNumber.Text = ""
        tbSecurityCode.Text = ""
        tbFirstName.Text = ""
        tbLastName.Text = ""
        LoadExpirationDate()
        LoadCountry()
        txtPINEPB.Value = ""
        txtPINKSN.Value = ""
        txtTrack1.Value = ""
        txtTrack2Data.Value = ""
        txtTrackKSN.Value = ""
        txtSwiped.Value = ""
        txtPrintData.Value = ""
        txtPrintStatus.Value = ""

        Me.lblCVVCode.Visible = True
        Me.tbSecurityCode.Visible = True

        Me.btnSubmit.BackColor = Drawing.Color.White
        Me.ccValidator.Enabled = True
        lblStatus.Text = "Click button to scan card or enter manually."

    End Sub

    Private Function GetCardType(ByVal ccpmt As CCPayment) As B2P.Payment.FeeCalculation.PaymentTypes

        If Not ccpmt.PIN Is Nothing AndAlso ccpmt.PIN <> "" Then
            Return B2P.Payment.FeeCalculation.PaymentTypes.PinDebit
        End If

        Dim BIN As String
        If Session("Swiped") Then
            BIN = ccpmt.BIN_SwipedCard
        Else
            BIN = ccpmt.BIN_CreditCard
        End If

        Return B2P.Payment.FeeCalculation.GetCardType(BIN)

    End Function

    Private Sub SetKeyedCreditCardFields(ByRef cc As B2P.Common.Objects.CreditCard)
        With cc
            .CreditCardNumber = HttpUtility.HtmlEncode(tbCreditCardNumber.Text.Trim)
            .SecurityCode = HttpUtility.HtmlEncode(tbSecurityCode.Text)
            .ExpirationMonth = HttpUtility.HtmlEncode(ddlMonth.SelectedItem.Text)
            .ExpirationYear = HttpUtility.HtmlEncode(ddlYear.SelectedItem.Text)
            .Owner.FirstName = HttpUtility.HtmlEncode(tbFirstName.Text.Truncate(20))
            .Owner.LastName = HttpUtility.HtmlEncode(tbLastName.Text.Truncate(30))
            .Owner.CountryCode = HttpUtility.HtmlEncode(ddlCountry.SelectedValue)
            .Owner.Address1 = "NOT PROVIDED"
            .Owner.Address2 = ""
            .Owner.City = "NOT PROVIDED"
            Select Case ddlCountry.SelectedValue
                Case "US"
                    .Owner.State = "FL"
                Case "CA"
                    .Owner.State = "AB"
                Case Else
                    .Owner.State = "FL"
            End Select
            .Owner.ZipCode = HttpUtility.HtmlEncode(txtZip.Text)
            If tbEmailAddress.Text <> "" Then
                .Owner.EMailAddress = HttpUtility.HtmlEncode(tbEmailAddress.Text.Trim)
            Else
                .Owner.EMailAddress = "null@cybersource.com"
            End If
            If txtPhone.Text.Trim <> "" Then
                .Owner.PhoneNumber = txtPhone.Text.Trim
            Else
                .Owner.PhoneNumber = ""
            End If
        End With
        Session("CreditCard") = cc
    End Sub


    Private Sub payCreditCard(ByVal sc As B2P.ShoppingCart.Cart)
        Dim pmtEntryMode As EntryModes
        Dim ccpmt As New CCPayment
        Dim IsPinDebit As Boolean = False 'Pin debit transactions will ignore tax/nontax shopping carts and process all together

        If txtSwiped.Value.Trim.Length > 0 And txtTrack2Data.Value.Trim.Length > 0 Then
            Session("Swiped") = True
            pmtEntryMode = EntryModes.SwipedPINPad
            With ccpmt
                .BIN_SwipedCard = HttpUtility.HtmlEncode(tbCreditCardNumber.Text.Trim.Substring(0, 6))

                If txtPINEPB.Value.Trim.Length > 0 Then  'pin debit card 
                    .PIN = HttpUtility.HtmlEncode(txtPINEPB.Value.Trim)
                    IsPinDebit = True
                    ' sc.ContainsNonTaxItems = True   'override default behavior as pin debit will be processed as nontax only
                    ' sc.ContainsTaxItems = False

                End If
            End With
            'If txtCreditDebit.Value.Trim.ToLower = "credit" Then
            '    If reqZip.Enabled = False And reqCVV.Enabled = False Then
            'reqZip.Enabled = True
            'reqCVV.Enabled = True
            reqFirstName.Enabled = True
            PeterBlum.DES.Globals.WebFormDirector.Validate()
            'PeterBlum.DES.Globals.Page.Validate()
            'End If
            '    End If
        Else
            Session("Swiped") = False
            ' If reqZip.Enabled = False And reqCVV.Enabled = False Then
            reqZip.Enabled = True
            reqCVV.Enabled = True
            reqFirstName.Enabled = True
            PeterBlum.DES.Globals.WebFormDirector.Validate()
            'PeterBlum.DES.Globals.Page.Validate()
            'End If
            ccpmt.BIN_CreditCard = HttpUtility.HtmlEncode(tbCreditCardNumber.Text.Trim.Substring(0, 6))
            If ValidateKeyedFields() = False Then
                Return
            End If
            pmtEntryMode = EntryModes.Keyed
            txtPINEPB.Value = ""
            ccpmt.PIN = ""
        End If

        'If PeterBlum.DES.Globals.Page.IsValid Then
        If PeterBlum.DES.Globals.WebFormDirector.IsValid Then
            Dim ccpr As New B2P.Payment.CreditCardPaymentBase.CreditCardPaymentResults
            Dim ccpr2 As New B2P.Payment.CreditCardPaymentBase.CreditCardPaymentResults
            Dim PaymentToken As String

            Try
                If CheckCardType(sc) = False Then
                    Session("ClickedButton") = False
                    Session("ConfirmationNumber") = ""
                    ClearCCFields()
                    lblMessageTitle.Text = "Payment Processing Error"
                    lblMessage.Text = "Message: Card type not available for this transaction."
                    mdlPopup.Show()
                Else
                    Dim pmt As New B2P.Payment.CreditCardPayment(Session("UserClientCode").ToString)
                    Dim cardType As B2P.Payment.FeeCalculation.PaymentTypes = GetCardType(ccpmt)
                    Session("cardType") = cardType

                    With pmt
                        .UserComments = Left(HttpUtility.HtmlEncode(txtNotes.Text), 200)
                        If ddlOffice.SelectedValue = "" Then
                            .Office_ID = 0
                        Else
                            .Office_ID = CInt(HttpUtility.HtmlEncode(ddlOffice.SelectedValue))
                            'Keep current selected office stored for users to make another payment with stored office selection
                            Session("SelectedOffice") = (HttpUtility.HtmlEncode(ddlOffice.SelectedValue))
                        End If
                        .ClientCode = Session("UserClientCode").ToString
                        .Security_ID = CInt(Session("SecurityID"))
                        .AllowConfirmationEmails = True
                        .PaymentSource = B2P.Common.Enumerations.TransactionSources.Counter

                    End With

                    'Begin handling of the transaction info
                    Dim TaxBatch_ID As Integer

                    If sc.ContainsNonTaxItems Or IsPinDebit Then

                        If IsPinDebit = True Then
                            pmt.Items = sc.GetCartItems(B2P.Payment.FeeCalculation.PaymentTypes.PinDebit, B2P.ShoppingCart.Cart.CartItemsTypes.All)
                        Else
                            pmt.Items = sc.GetCartItems(cardType, B2P.ShoppingCart.Cart.CartItemsTypes.Nontax)
                        End If

                        Select Case pmtEntryMode
                            Case EntryModes.Keyed    'The card number was keyed
                                Dim Card As New B2P.Common.Objects.CreditCard
                                SetKeyedCreditCardFields(Card)
                                ccpr = pmt.PayByCreditCard(Card)
                            Case EntryModes.SwipedPINPad   'Card is swiped
                                Dim Card As New B2P.Common.Objects.SwipedCreditCard
                                SetSwipedCreditCardFields(Card)
                                ccpr = pmt.PayBySwipedCreditCard(Card)
                        End Select
                        Session("NonTaxItemsAmount") = pmt.Items.TotalAmount
                        Session("NonTaxFee") = pmt.Items.TotalFee

                        If ccpr.Result = B2P.Payment.CreditCardPayment.CreditCardPaymentResults.Results.Success Then
                            PaymentToken = ccpr.PaymentToken
                            Session("NonTaxConfirm") = ccpr.ConfirmationNumber
                            Session("NonTaxAuth") = ccpr.AuthorizationCode
                            Session("ClickedButton") = True
                           
                            btnSubmit.Enabled = False
                        Else
                            Session("ClickedButton") = False
                            Session("ConfirmationNumber") = ""
                            ClearCCFields()
                            lblMessageTitle.Text = "Payment Processing Error"
                            lblMessage.Text = "Message: " & ccpr.Message '"Credit card payment was not processed."
                            mdlPopup.Show()
                            Return

                        End If
                    End If

                    If sc.ContainsTaxItems And IsPinDebit = False Then

                        If Not PaymentToken Is Nothing AndAlso pmtEntryMode <> EntryModes.Keyed Then  'If token has value then nontax payment has been processed. Use token for this one
                            pmtEntryMode = EntryModes.Token
                            pmt.Items = sc.GetCartItems(B2P.Payment.FeeCalculation.PaymentTypes.CreditCard, B2P.ShoppingCart.Cart.CartItemsTypes.Tax)
                        Else
                            pmt.Items = sc.GetCartItems(cardType, B2P.ShoppingCart.Cart.CartItemsTypes.Tax)
                        End If

                        Session("TaxItemsAmount") = pmt.Items.TotalAmount
                        Session("TaxFee") = pmt.Items.TotalFee
                        Select Case pmtEntryMode
                            Case EntryModes.Keyed   'The card number was keyed
                                Dim card As New B2P.Common.Objects.CreditCard
                                SetKeyedCreditCardFields(card)
                                ccpr2 = pmt.PayByCreditCard(card)
                            Case EntryModes.SwipedPINPad   'Card is swiped
                                Dim card As New B2P.Common.Objects.SwipedCreditCard
                                SetSwipedCreditCardFields(card)
                                ccpr2 = pmt.PayBySwipedCreditCard(card)
                            Case EntryModes.Token   'Token set
                                Dim card As New B2P.Common.Objects.TokenCreditCard
                                SetTokenFields(card)
                                card.PaymentToken = PaymentToken
                                ccpr2 = pmt.PayByToken(card)
                        End Select

                        If ccpr2.Result = B2P.Payment.CreditCardPaymentBase.CreditCardPaymentResults.Results.Success Then
                            PaymentToken = ccpr2.PaymentToken
                            Session("TaxAuth") = ccpr2.AuthorizationCode
                            TaxBatch_ID = Convert.ToInt32(ccpr2.ConfirmationNumber)
                            Session("TaxConfirm") = ccpr2.ConfirmationNumber
                            Session("ClickedButton") = True
                            btnSubmit.Enabled = False
                            If pmtEntryMode <> EntryModes.Keyed And tbEmailAddress.Text <> "" Then
                                SendEmail()
                            End If
                            Response.Redirect("pinPadReturn.aspx", False)
                            HttpContext.Current.ApplicationInstance.CompleteRequest()
                        Else
                            Session("ClickedButton") = False
                            Session("ConfirmationNumber") = ""
                            ClearCCFields()
                            lblMessageTitle.Text = "Payment Processing Error"
                            lblMessage.Text = "Message: " & ccpr2.Message '"Credit card payment was not processed."
                            mdlPopup.Show()
                        End If
                    Else
                        If pmtEntryMode = EntryModes.SwipedPINPad And tbEmailAddress.Text <> "" Then
                            SendEmail()
                        End If
                        Response.Redirect("pinPadReturn.aspx", False)
                        HttpContext.Current.ApplicationInstance.CompleteRequest()
                    End If
                End If
            Catch ex As Exception
                B2P.Common.Logging.LogError("PinPad", "Error during payment " & ex.ToString, B2P.Common.Logging.NotifySupport.No)
                lblMessageTitle.Text = "Payment Processing Error"
                lblMessage.Text = "Message: Payment was not processed."
                mdlPopup.Show()
            End Try
        End If
    End Sub

    Private Function ValidateKeyedFields() As Boolean
        Dim ErrorMessage As String = ""

        If B2P.Common.Objects.CreditCard.ValidateCreditCardNumber(tbCreditCardNumber.Text) = False Then
            ErrorMessage = "Invalid Credit Card Number"
        End If

        'Validate expiration date
        Dim ccDate As String = String.Format("{0}/{1}", ddlMonth.SelectedItem.Text, ddlYear.SelectedItem.Text)
        If CDate(ccDate) < CDate((String.Format("{0}/{1}", DateTime.Now.Month, DateTime.Now.Year))) AndAlso ErrorMessage.Length = 0 Then
            ErrorMessage = "Expiration Date is invalid or card has expired"
        End If

        'Dim TempDate As Date
        'If Date.TryParse(String.Format("{0}/1/{1}", Me.ddlMonth.Text, Me.ddlYear.Text), TempDate) = False AndAlso ErrorMessage.Length = 0 Then
        '    ErrorMessage = "Invalid Expiration Date"
        'End If

        'If TempDate.AddMonths(1) < Now Then
        '    ErrorMessage = "Expiration Date is invalid or card has expired"
        'End If

        If ErrorMessage.Length > 0 Then
            lblMessageTitle.Text = "Payment Processing Error"
            lblMessage.Text = ErrorMessage
            mdlPopup.Show()
        Else
            Return True
        End If

    End Function

    Private Sub SetSwipedCreditCardFields(ByRef cc As B2P.Common.Objects.SwipedCreditCard)

        With cc.Owner
            .FirstName = HttpUtility.HtmlEncode(tbFirstName.Text.Truncate(20))
            .LastName = HttpUtility.HtmlEncode(tbLastName.Text.Truncate(30))
            .Address1 = "NOT PROVIDED"
            .Address2 = ""
            .City = "NOT PROVIDED"
            Select Case ddlCountry.SelectedValue
                Case "US"
                    .State = "FL"
                Case "CA"
                    .State = "AB"
                Case Else
                    .State = "FL"
            End Select
            .ZipCode = ""
            If txtPhone.Text.Trim <> "" Then
                .PhoneNumber = txtPhone.Text.Trim
            Else
                .PhoneNumber = ""
            End If
            If tbEmailAddress.Text <> "" Then
                .EMailAddress = HttpUtility.HtmlEncode(tbEmailAddress.Text.Trim)
            Else
                .EMailAddress = "null@cybersource.com"
            End If
            .CountryCode = HttpUtility.HtmlEncode(ddlCountry.SelectedValue)
        End With

        If txtPINEPB.Value.Trim.Length = 0 Then 'not pin debit
            cc.Owner.ZipCode = HttpUtility.HtmlEncode(txtZip.Text)
        End If

        With cc
            .EncryptedTrack2Data = HttpUtility.HtmlEncode(txtTrack2Data.Value.Trim)
            .TrackKSN = HttpUtility.HtmlEncode(txtTrackKSN.Value.Trim)
            .MagnePrintData = HttpUtility.HtmlEncode(txtPrintData.Value.Trim)
            .MagnePrintStatus = HttpUtility.HtmlEncode(txtPrintStatus.Value.Trim)
            .MaskedCCNumber = HttpUtility.HtmlEncode(MaskCCNumber(tbCreditCardNumber.Text.Trim))
            .BINNumber = HttpUtility.HtmlEncode(tbCreditCardNumber.Text.Trim.Substring(0, 6))
            .ExpirationMonth = HttpUtility.HtmlEncode(ddlMonth.SelectedItem.Text)
            .ExpirationYear = HttpUtility.HtmlEncode(ddlYear.SelectedItem.Text)
            If txtPINEPB.Value.Trim.Length > 0 Then
                .PIN = HttpUtility.HtmlEncode(txtPINEPB.Value.Trim)
                .PINKSN = HttpUtility.HtmlEncode(txtPINKSN.Value.Trim)
            End If
        End With
        Session("SwipedCreditCard") = cc
    End Sub

    Private Sub showLookup(ByVal textnumber As Integer)

        Try

            Dim x As New B2P.ClientInterface.Manager.ClientInterface() ' B2P.ClientInterface.Custom.AccountSearch
            Dim y As New B2P.ClientInterface.Manager.ClientInterfaceWS.SearchResults
            Dim sp As New B2P.ClientInterface.Manager.ClientInterfaceWS.SearchParameters
            Dim productInput As TextBox = CType(FindControl(String.Format("txtProduct{0}Input", textnumber)), TextBox)
            Dim productInputa As TextBox = CType(FindControl(String.Format("txtProduct{0}aInput", textnumber)), TextBox)
            Dim productInputb As TextBox = CType(FindControl(String.Format("txtProduct{0}bInput", textnumber)), TextBox)
            Dim productDropdown As Global.Telerik.Web.UI.RadComboBox = CType(FindControl(String.Format("ddlProduct{0}", textnumber)), Global.Telerik.Web.UI.RadComboBox)
            Dim productAmount As TextBox = CType(FindControl(String.Format("txtProduct{0}Amt", textnumber)), TextBox)
            Dim buttonCart As ImageButton = CType(FindControl(String.Format("btnCart{0}", textnumber)), ImageButton)
            Dim buttonLookup As ImageButton = CType(FindControl(String.Format("btnLookup{0}", textnumber)), ImageButton)

            sp.AccountNumber1 = HttpUtility.HtmlEncode(Trim(productInput.Text))
            If Trim(productInputa.Text) <> "" Then
                sp.AccountNumber2 = HttpUtility.HtmlEncode(Trim(productInputa.Text))
            Else
                sp.AccountNumber2 = ""
            End If
            If Trim(productInputb.Text) <> "" Then
                sp.AccountNumber3 = HttpUtility.HtmlEncode(Trim(productInputb.Text))
            Else
                sp.AccountNumber3 = ""
            End If


            sp.ClientCode = CStr(Session("UserClientCode"))
            sp.ProductName = HttpUtility.HtmlEncode(productDropdown.SelectedValue)

            y = B2P.ClientInterface.Manager.ClientInterface.GetClientData(sp)
            Session("ClientData") = y

            Select Case y.SearchResult
                Case B2P.ClientInterface.Manager.ClientInterfaceWS.StatusCodes.Success
                    Select Case y.PaymentStatus
                        Case B2P.ClientInterface.Manager.ClientInterfaceWS.PaymentStatusCodes.Allowed, B2P.ClientInterface.Manager.ClientInterfaceWS.PaymentStatusCodes.NotEditable, B2P.ClientInterface.Manager.ClientInterfaceWS.PaymentStatusCodes.MinimumPaymentRequired
                            lblProductTextNumber.Text = String.Format("Product {0} Lookup", textnumber)
                            lblProductNumberLookup.Text = textnumber.ToString
                            lblLookupMessage.Text = "Account found. Please review the following information: "
                            ckUseAddress.Checked = True
                            grdLookup.DataSource = y.SupplementalInformation.ClientInfo
                            grdLookup.DataBind()

                            buttonCart.Enabled = True
                            buttonCart.ImageUrl = "/images/cart.jpg"

                            'new block code
                            If checkBlockedAccount(sp.ClientCode, sp.ProductName, sp.AccountNumber1, sp.AccountNumber2, sp.AccountNumber3, RadioButtonList1.SelectedValue) Then
                                pnlBlock.Visible = True
                                ckOverrideBlock.Checked = False
                                lblBlockBy.Text = "Blocked By: " & acct.BlockUser
                                lblBlockDate.Text = "Blocked Date: " & acct.BlockDate
                                lblBlockComment.Text = "Block Comment: " & acct.BlockComment
                            Else
                                pnlBlock.Visible = False
                            End If

                            modalLookup.Show()
                        Case B2P.ClientInterface.Manager.ClientInterfaceWS.PaymentStatusCodes.NotAllowed
                            lblProductTextNumber.Text = String.Format("Product {0} Lookup", textnumber)
                            lblProductNumberLookup.Text = textnumber.ToString
                            lblLookupMessage.Text = "Account found. Note: An online payment is not allowed for this account. Please review the following information: "
                            ckUseAddress.Checked = True
                            grdLookup.DataSource = y.SupplementalInformation.ClientInfo
                            grdLookup.DataBind()

                            buttonCart.Enabled = True
                            buttonCart.ImageUrl = "/images/cart.jpg"

                            modalLookup.Show()
                    End Select

                Case B2P.ClientInterface.Manager.ClientInterfaceWS.StatusCodes.ErrorOccurred
                    lblProductTextNumberError.Text = String.Format("Product {0} Lookup", textnumber)
                    lblProductNumberLookupError.Text = textnumber.ToString
                    lblLookupMessageError.Text = "Message: Oops, an error has occurred during the account lookup."

                    modalLookupError.Show()

                Case B2P.ClientInterface.Manager.ClientInterfaceWS.StatusCodes.LookupNotEnabled
                    lblProductTextNumberError.Text = String.Format("Product {0} Lookup", textnumber)
                    lblProductNumberLookupError.Text = textnumber.ToString
                    lblLookupMessageError.Text = "Message: Lookup is not enabled for this account."

                    modalLookupError.Show()

                Case B2P.ClientInterface.Manager.ClientInterfaceWS.StatusCodes.NotFound
                    lblProductTextNumberError.Text = String.Format("Product {0} Lookup", textnumber)
                    lblProductNumberLookupError.Text = textnumber.ToString
                    lblLookupMessageError.Text = "Message: This account was not found. Please check the account number and try again."

                    modalLookupError.Show()

            End Select
            buttonLookup.Enabled = True
            buttonLookup.ImageUrl = "/images/lookup.jpg"
        Catch ex As Exception
            B2P.Common.Logging.LogError("B2P Admin", "Error during lookup " & ex.ToString, B2P.Common.Logging.NotifySupport.No)
            Response.Redirect("/errors/")
            HttpContext.Current.ApplicationInstance.CompleteRequest()
        End Try
    End Sub
    Private Sub btnLookup1_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnLookup1.Click
        showLookup(1)
    End Sub
    Private Sub btnLookup2_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnLookup2.Click
        showLookup(2)
    End Sub
    Private Sub btnLookup3_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnLookup3.Click
        showLookup(3)
    End Sub
    Private Sub btnLookup4_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnLookup4.Click
        showLookup(4)
    End Sub
    Private Sub btnLookup5_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnLookup5.Click
        showLookup(5)
    End Sub

    Protected Sub btnLookupCancel_Click(sender As Object, e As EventArgs) Handles btnLookupCancel.Click
        Dim productNumber As String = lblProductNumberLookup.Text

        If productNumber = 1 Then
            Response.Redirect("/payment/pin-pad.aspx")
        Else
            removeProducts(productNumber)
        End If

    End Sub

    Private Sub btnLookupOK_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnLookupOK.Click
        Dim productNumber As String = lblProductNumberLookup.Text
        Dim productAmount As TextBox = CType(FindControl(String.Format("txtProduct{0}Amt", productNumber)), TextBox)
        Dim buttonCart As ImageButton = CType(FindControl(String.Format("btnCart{0}", productNumber)), ImageButton)

        Dim y As New B2P.ClientInterface.Manager.ClientInterfaceWS.SearchResults
        y = CType(Session("ClientData"), B2P.ClientInterface.Manager.ClientInterfaceWS.SearchResults)
        productAmount.Text = HttpUtility.HtmlEncode(y.AmountDue.ToString("####.00"))
        hidAssembly.Value = HttpUtility.HtmlEncode(y.AssemblyInformation)

        If Not IsNothing(Session("BlockedAccount")) Then
            acct = CType(Session("BlockedAccount"), B2P.Common.BlockedAccounts.BlockedAccountResults)
            If acct.IsAccountBlocked Then
                If ckOverrideBlock.Checked Then
                    addCart(productNumber)
                    If ckUseAddress.Checked Then

                        If RadioButtonList1.SelectedValue = "CC" Then
                            tbFirstName.Text = HttpUtility.HtmlEncode(y.Demographics.FirstName.Value)
                            tbLastName.Text = HttpUtility.HtmlEncode(y.Demographics.LastName.Value)
                        Else
                            tbFirstName.Text = HttpUtility.HtmlEncode(y.Demographics.FirstName.Value)
                            tbLastName.Text = HttpUtility.HtmlEncode(y.Demographics.LastName.Value)
                        End If

                        txtZip.Text = HttpUtility.HtmlEncode(y.Demographics.ZipCode.Value)
                        txtPhone.Text = HttpUtility.HtmlEncode(y.Demographics.HomePhone.Value)

                    End If
                    'buttonCart.Enabled = True
                    'buttonCart.ImageUrl = "/images/cart.jpg"
                Else
                    'clear boxes?
                    If productNumber = 1 Then
                        Response.Redirect("/payment/newCounter.aspx")
                    Else
                        removeProducts(productNumber)
                    End If
                End If
            Else

                If ckUseAddress.Checked Then
                    If RadioButtonList1.SelectedValue = "CC" Then
                        tbFirstName.Text = HttpUtility.HtmlEncode(y.Demographics.FirstName.Value)
                        tbLastName.Text = HttpUtility.HtmlEncode(y.Demographics.LastName.Value)
                    Else
                        tbFirstName.Text = HttpUtility.HtmlEncode(y.Demographics.FirstName.Value)
                        tbLastName.Text = HttpUtility.HtmlEncode(y.Demographics.LastName.Value)
                    End If

                    txtZip.Text = HttpUtility.HtmlEncode(y.Demographics.ZipCode.Value)
                    txtPhone.Text = HttpUtility.HtmlEncode(y.Demographics.HomePhone.Value)
                End If
                'buttonCart.Enabled = True
                'buttonCart.ImageUrl = "/images/cart.jpg"
            End If
        Else
            addCart(productNumber)
            If ckUseAddress.Checked Then
                If RadioButtonList1.SelectedValue = "CC" Then
                    tbFirstName.Text = HttpUtility.HtmlEncode(y.Demographics.FirstName.Value)
                    tbLastName.Text = HttpUtility.HtmlEncode(y.Demographics.LastName.Value)
                Else
                    tbFirstName.Text = HttpUtility.HtmlEncode(y.Demographics.FirstName.Value)
                    tbLastName.Text = HttpUtility.HtmlEncode(y.Demographics.LastName.Value)
                End If
                txtZip.Text = HttpUtility.HtmlEncode(y.Demographics.ZipCode.Value)
                txtPhone.Text = HttpUtility.HtmlEncode(y.Demographics.HomePhone.Value)
            End If
            buttonCart.Enabled = True
            buttonCart.ImageUrl = "/images/cart.jpg"
        End If


        'If ckUseAddress.Checked Then
        '    If RadioButtonList1.SelectedValue = "CC" Then
        '        tbFirstName.Text = HttpUtility.HtmlEncode(y.Demographics.FirstName.Value)
        '        tbLastName.Text = HttpUtility.HtmlEncode(y.Demographics.LastName.Value)
        '    Else
        '        tbBankAccountFirstName.Text = HttpUtility.HtmlEncode(y.Demographics.FirstName.Value)
        '        tbBankAccountLastName.Text = HttpUtility.HtmlEncode(y.Demographics.LastName.Value)
        '    End If

        '    txtAddress.Text = HttpUtility.HtmlEncode(y.Demographics.Address1.Value)
        '    txtAddress2.Text = HttpUtility.HtmlEncode(y.Demographics.Address2.Value)
        '    txtCity.Text = HttpUtility.HtmlEncode(y.Demographics.City.Value)
        '    ddlState.SelectedValue = HttpUtility.HtmlEncode(y.Demographics.State.Value)
        '    txtZip.Text = HttpUtility.HtmlEncode(y.Demographics.ZipCode.Value)
        '    tbHomePhone.Text = HttpUtility.HtmlEncode(y.Demographics.HomePhone.Value)

        '    tbEmailAddress.Text = HttpUtility.HtmlEncode(y.Demographics.EmailAddress.Value)
        'End If

        'buttonCart.Enabled = True
        'buttonCart.ImageUrl = "/images/cart.jpg"
    End Sub
    Private Sub SetTokenFields(ByRef cc As B2P.Common.Objects.TokenCreditCard)
        With cc
            .Owner.FirstName = HttpUtility.HtmlEncode(tbFirstName.Text.Truncate(20))
            .Owner.LastName = HttpUtility.HtmlEncode(tbLastName.Text.Truncate(30))
            .Owner.Address1 = "NOT PROVIDED"
            .Owner.Address2 = ""
            .Owner.City = "NOT PROVIDED"
            Select Case ddlCountry.SelectedValue
                Case "US"
                    .Owner.State = "FL"
                Case "CA"
                    .Owner.State = "AB"
                Case Else
                    .Owner.State = "FL"
            End Select
            .Owner.ZipCode = HttpUtility.HtmlEncode(txtZip.Text)
            .Owner.EMailAddress = HttpUtility.HtmlEncode(tbEmailAddress.Text)
            .Owner.CountryCode = HttpUtility.HtmlEncode(ddlCountry.SelectedValue)
            If txtPhone.Text.Trim <> "" Then
                .Owner.PhoneNumber = txtPhone.Text.Trim
            Else
                .Owner.PhoneNumber = ""
            End If
            If B2PSession.CreditCard Is Nothing Then
                Dim scc As B2P.Common.Objects.SwipedCreditCard = CType(B2PSession.SwipedCreditCard, B2P.Common.Objects.SwipedCreditCard)

                .BINNumber = scc.BINNumber
                .ExpirationMonth = scc.ExpirationMonth
                .ExpirationYear = scc.ExpirationYear
                .MaskedCCNumber = scc.MaskedCCNumber
            Else
                Dim savedCC As B2P.Common.Objects.CreditCard = CType(B2PSession.CreditCard, B2P.Common.Objects.CreditCard)

                .BINNumber = savedCC.BINNumber
                .ExpirationMonth = savedCC.ExpirationMonth
                .ExpirationYear = savedCC.ExpirationYear
            End If
        End With
    End Sub

    Private Sub btnBlockOverride_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBlockOverride.Click
        Dim buttonCart As ImageButton = CType(FindControl(String.Format("btnCart{0}", CType(lblProductNumber.Text, Integer))), ImageButton)

        If Not IsNothing(Session("BlockedAccount")) Then
            acct = CType(Session("BlockedAccount"), B2P.Common.BlockedAccounts.BlockedAccountResults)
            If acct.IsAccountBlocked Then
                If ckBlockOverride.Checked Then
                    addCart(CType(lblProductNumber.Text, Integer))
                Else
                    With buttonCart
                        .ImageUrl = "/images/cart.jpg"
                        .Enabled = True
                    End With
                End If
            End If
        End If
    End Sub

    Protected Sub RoutingNumberValidation(ByVal sourceCondition As PeterBlum.DES.IBaseCondition, ByVal args As PeterBlum.DES.IConditionEventArgs)

        Dim vArgs As PeterBlum.DES.Web.WebControls.ConditionTwoFieldEventArgs = CType(args, PeterBlum.DES.Web.WebControls.ConditionTwoFieldEventArgs)

        If vArgs.ControlToEvaluate Is Nothing Then
            vArgs.CannotEvaluate = True
        Else
            Dim routingNumber As String = vArgs.Value.ToString
            If String.IsNullOrEmpty(routingNumber) = True Then
                vArgs.Validator.ErrorMessage = ""
                vArgs.IsMatch = False
                Exit Sub
            Else
                Dim x As New B2P.Profile.BankAccount
                Select Case x.ValidateBankRoutingNumber(tbBankRoutingNumber.Text, B2P.Common.Objects.BankAccount.RoutingNumberValidationMode.FederalReserveLookup)
                    Case B2P.Common.Objects.BankAccount.ValidationStatus.Invalid
                        vArgs.IsMatch = False

                    Case Else
                        vArgs.IsMatch = True
                End Select
            End If


        End If

    End Sub

    Protected Sub SendEmail()
        Dim TotalAmount As Decimal
        Dim sc As B2P.ShoppingCart.Cart = CType(Session("ShoppingCart"), B2P.ShoppingCart.Cart)
        Select Case CType(Session("cardType"), B2P.Payment.FeeCalculation.PaymentTypes)
            Case B2P.Payment.FeeCalculation.PaymentTypes.CreditCard
                TotalAmount = sc.CreditCardFee + sc.TotalAmount
            Case B2P.Payment.FeeCalculation.PaymentTypes.DebitCard
                TotalAmount = sc.DebitFee + sc.TotalAmount
            Case B2P.Payment.FeeCalculation.PaymentTypes.PinDebit
                TotalAmount = sc.PinDebitFee + sc.TotalAmount
        End Select

        Dim tCardIssuer As String = ""
        Dim tCreditCardNumber As String = ""
        Dim tFirstName As String = ""
        Dim tLastName As String = ""
        If Session("Swiped") Then
            Dim cc As New B2P.Common.Objects.SwipedCreditCard
            cc = Session("SwipedCreditCard")
            With cc
                tCardIssuer = Session("CardIssuer")
                tCreditCardNumber = .MaskedCCNumber
                tFirstName = .Owner.FirstName
                tLastName = .Owner.LastName
            End With
        Else
            Dim cc As New B2P.Common.Objects.CreditCard
            cc = CType(Session("CreditCard"), B2P.Common.Objects.CreditCard)
            With cc
                tCardIssuer = .CardIssuer
                tCreditCardNumber = .CreditCardNumber
                tFirstName = .Owner.FirstName
                tLastName = .Owner.LastName
            End With
        End If

        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("<HTML><BODY>")
        sb.AppendLine("<style> type=""text/css""")
        sb.AppendLine("body {font: 100% 'Segoe UI', Arial, sans-serif;	color:#444444;	margin:5px;	background-color:white;}")
        sb.AppendLine("tr{	font: 100% 'Segoe UI', Arial, sans-serif;}")
        sb.AppendLine("td{	font: 100% 'Segoe UI', Arial, sans-serif;}")
        sb.AppendLine(".Bold {font-weight: bold;}")
        sb.AppendLine(".ClientName{font: 100% 'Segoe UI', Arial, sans-serif; font-size:medium; font-weight:bold;}")
        sb.AppendLine(".right{	font: 100% 'Segoe UI', Arial, sans-serif; text-align:right; }")
        sb.AppendLine(".ConfTable{width:450px;}")
        sb.AppendLine("</style>")
        sb.Append(String.Format("<p class=""ClientName"">{0}</p>", Session("ClientName")))

        sb.AppendLine("<Table class=""ConfTable"">")
        sb.AppendLine(String.Format("<TR><TD>Transaction Date:</TD><TD class=""right"">{0:MM/dd/yyyy}</TD></TR>", Now))
        sb.AppendLine(String.Format("<TR><TD>Name:</TD><TD class=""right"">{0} - {1}</TD></TR>", tFirstName, tLastName))
        sb.AppendLine(String.Format("<TR><TD>Payment Type:</TD><TD class=""right"">{0} - {1}</TD></TR>", tCardIssuer, tCreditCardNumber))
       

        sb.AppendLine("<TR><TD>&nbsp</TD></TR>")
        If B2PSession.TaxItemsAmount > 0 Then
            sb.AppendLine("<TR><TD class=""bold"">Tax Items:</TD></TR>")
            sb.AppendLine(String.Format("<TR><TD>Confirmation Number:</TD><TD class=""right"">{0}</TD></TR>", HttpUtility.HtmlEncode(Session("TaxConfirm").TrimStart(CChar("0")))))
            sb.AppendLine(String.Format("<TR><TD>Authorization Code:</TD><TD class=""right"">{0}</TD></TR>", HttpUtility.HtmlEncode(Session("TaxAuth"))))
            sb.AppendLine(String.Format("<TR><TD>Tax Items Amount:</TD><TD class=""right"">{0:c}</TD></TR>", Session("TaxItemsAmount")))
            sb.AppendLine(String.Format("<TR><TD>Convenience Fee:</TD><TD class=""right"">{0:c}</TD></TR>", Session("TaxFee")))
            sb.AppendLine("<TR><TD>&nbsp</TD></TR>")
        End If

        If B2PSession.NonTaxItemsAmount > 0 Then
            sb.AppendLine("<TR><TD class=""bold"">Non-Tax Items:</TD></TR>")
            sb.AppendLine(String.Format("<TR><TD>Confirmation Number:</TD><TD class=""right"">{0}</TD></TR>", HttpUtility.HtmlEncode(Session("NonTaxConfirm").TrimStart(CChar("0")))))
            sb.AppendLine(String.Format("<TR><TD>Authorization Code:</TD><TD class=""right"">{0}</TD></TR>", HttpUtility.HtmlEncode(Session("NonTaxAuth"))))
            sb.AppendLine(String.Format("<TR><TD>Tax Items Amount:</TD><TD class=""right"">{0:c}</TD></TR>", Session("NonTaxItemsAmount")))
            sb.AppendLine(String.Format("<TR><TD>Convenience Fee:</TD><TD class=""right"">{0:c}</TD></TR>", Session("NonTaxFee")))
            sb.AppendLine("<TR><TD>&nbsp</TD></TR>")
        End If

        sb.AppendLine(String.Format("<TR><TD class=""bold"">Total Amount:</TD><TD class=""bold""  style=""text-align:right;"">{0:c}</TD></TR>", TotalAmount))
        sb.AppendLine("<TR><TD>&nbsp</TD></TR>")

        sb.AppendLine(String.Format("<TR><TD colspan=2>{0}</TD></TR>", B2P.Objects.Client.GetClientMessage("Manatron", Session("UserClientCode"))))
        sb.AppendLine("</TABLE></BODY></HTML>")

        Dim em As New B2P.Common.Email.EmailMessage
        em.RecipientAddress = HttpUtility.HtmlEncode(tbEmailAddress.Text)
        em.SenderAddress = "Payments@Bill2Pay.com"
        em.Subject = String.Format("{0} Payment Receipt", Session("ClientName"))
        em.HTMLBody = sb.ToString

        If B2P.Common.Email.SendMail(em) Then
            'Me.litEmailResult.Text = "Email sent successfully"
            Dim test As String = "xyz"
        Else
            'Me.litEmailResult.Text = "Unable to send email at this time"
            Dim test As String = "xyz"
        End If




    End Sub
End Class