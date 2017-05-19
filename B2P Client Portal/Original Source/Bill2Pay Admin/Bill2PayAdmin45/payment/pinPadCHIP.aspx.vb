Imports System.Configuration.ConfigurationManager
Imports B2P.Integration.TriPOS
Imports Telerik.Web.UI
Public Class pinPadCHIP
    Inherits baseclass

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
        Dim configSection As ClientSection = DirectCast(GetSection("emvPinPads"), ClientSection)

        If Not IsPostBack Then
            Session("ClickedButton") = False
            Session("CardType") = Nothing
            Session("NonTaxItemsAmount") = 0
            Session("TaxItemsAmount") = 0
            Session("NonTaxAuth") = Nothing
            Session("tiListItems") = Nothing
            BLL.SessionManager.ACHBlocked = False
            BLL.SessionManager.CreditCardBlocked = False
            BLL.SessionManager.PaymentMade = False
            BLL.SessionManager.Cart = Nothing
            BLL.SessionManager.UserComments = Nothing
            BLL.SessionManager.CustomerEmail = Nothing
            BLL.SessionManager.BankAccount = Nothing
            Session("ShoppingCart") = Nothing
            BLL.SessionManager.CreditCard = Nothing


            ' Pull these values from the emvPinPads config section in the web.config file
            If configSection.Clients.Item(BLL.SessionManager.ClientCode.ToUpper) IsNot Nothing Then
                BLL.SessionManager.UseBinLookup = configSection.Clients.Item(BLL.SessionManager.ClientCode.ToUpper).UseBinLookup
                BLL.SessionManager.UseSingleFee = configSection.Clients.Item(BLL.SessionManager.ClientCode.ToUpper).UseSingleFee

                ' Got here -- initialize
                BLL.SessionManager.IsInitialized = True
            Else
                psmErrorMessage.ToggleStatusMessage("Client configuration error.", StatusMessageType.Danger, True, True)
            End If

            ddlProduct1.Focus()
            loadData()
            loadProducts()

        End If

        If Not UserSecurity.MakeACCPayment Then
            Response.Redirect("/error/default.aspx?type=security")
        End If
    End Sub

    Protected Sub ddlAUClient_SelectedIndexChanged(ByVal o As Object, ByVal e As Global.Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles ddlAUClient.SelectedIndexChanged
        loadOffices()
        ddlProduct1.Items.Clear()
        clearProducts()
        loadProducts()
    End Sub

    Protected Sub ddlProduct1_SelectedIndexChanged(ByVal o As Object, ByVal e As Global.Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles ddlProduct1.SelectedIndexChanged
        Try

            Dim product1Selected As String = Replace(Web.HttpUtility.HtmlEncode(ddlProduct1.SelectedValue), "&amp;", "&")


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


    Private Sub btnClearAll_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClearAll.Click
        Response.Redirect("/payment/pinPadCHIP.aspx")
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
            Dim myProduct As String = Replace(Web.HttpUtility.HtmlEncode(productDropdown.SelectedValue), "&amp;", "&")

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
        BLL.SessionManager.ClientCode = ddlAUClient.SelectedItem.Text
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
                Dim z As New B2P.Objects.Product(Session("UserClientCode").ToString, Replace(Web.HttpUtility.HtmlEncode(ddlProduct1.SelectedValue), "&amp;", "&"), B2P.Common.Enumerations.TransactionSources.CallCenter)
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
                If checkBlockedAccount(CStr(Session("UserClientCode")), productDropdown.SelectedValue, productInput.Text, productInputa.Text, productInputb.Text) Then


                    lblProductNumber.Text = productNumber.ToString()
                    'pnlBlock.Visible = True
                    lblBlockBy2.Text = "Blocked By: " & acct.BlockUser
                    lblBlockDate2.Text = "Blocked Date: " & acct.BlockDate
                    lblBlockComment2.Text = "Block Comment: " & acct.BlockComment
                    ckBlockOverrideACH.Checked = False
                    ckBlockOverrideCC.Checked = False

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

                If sc.CreditCardStatus <> B2P.Common.Enumerations.ReturnCodes.Success Or Not sc.AcceptCreditCards Then
                    pnlPaymentInformation.Visible = False
                    lblMessageTitle.Text = "Invalid Product and/or Payment Selection"
                    lblMessage.Text = "Message: Amount not within valid range."

                    mdlPopup.Show()
                Else
                    pnlPaymentInformation.Visible = True
                End If

            Else
                lblMessageTitle.Text = "Invalid Product and/or Payment Selection"
                'Disable button here
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
                strProduct = Replace(Web.HttpUtility.HtmlEncode(ddlProduct1.SelectedItem.Text), "&amp;", "&")
                lis.Add(Web.HttpUtility.HtmlEncode(txtProduct1Input.Text.Trim), Web.HttpUtility.HtmlEncode(txtProduct1aInput.Text.Trim), Web.HttpUtility.HtmlEncode(txtProduct1bInput.Text.Trim),
                       strProduct, CDec(Web.HttpUtility.HtmlEncode(txtProduct1Amt.Text)), 0, 0)

            End If

            If txtProduct2Input.Text.Trim <> "" And txtProduct2Input.Enabled = False Then
                strProduct = Replace(Web.HttpUtility.HtmlEncode(ddlProduct2.SelectedItem.Text), "&amp;", "&")
                lis.Add(Web.HttpUtility.HtmlEncode(txtProduct2Input.Text.Trim), Web.HttpUtility.HtmlEncode(txtProduct2aInput.Text.Trim), Web.HttpUtility.HtmlEncode(txtProduct2bInput.Text.Trim),
                   strProduct, CDec(Web.HttpUtility.HtmlEncode(txtProduct2Amt.Text)), 0, 0)
            End If

            If txtProduct3Input.Text.Trim <> "" And txtProduct3Input.Enabled = False Then
                strProduct = Replace(Web.HttpUtility.HtmlEncode(ddlProduct3.SelectedItem.Text), "&amp;", "&")
                lis.Add(Web.HttpUtility.HtmlEncode(txtProduct3Input.Text.Trim), Web.HttpUtility.HtmlEncode(txtProduct3aInput.Text.Trim), Web.HttpUtility.HtmlEncode(txtProduct3bInput.Text.Trim),
                   strProduct, CDec(Web.HttpUtility.HtmlEncode(txtProduct3Amt.Text)), 0, 0)
            End If

            If txtProduct4Input.Text.Trim <> "" And txtProduct4Input.Enabled = False Then
                strProduct = Replace(Web.HttpUtility.HtmlEncode(ddlProduct4.SelectedItem.Text), "&amp;", "&")
                lis.Add(Web.HttpUtility.HtmlEncode(txtProduct4Input.Text.Trim), Web.HttpUtility.HtmlEncode(txtProduct4aInput.Text.Trim), Web.HttpUtility.HtmlEncode(txtProduct4bInput.Text.Trim),
                   strProduct, CDec(Web.HttpUtility.HtmlEncode(txtProduct4Amt.Text)), 0, 0)
            End If

            If txtProduct5Input.Text.Trim <> "" And txtProduct5Input.Enabled = False Then
                strProduct = Replace(Web.HttpUtility.HtmlEncode(ddlProduct5.SelectedItem.Text), "&amp;", "&")
                lis.Add(Web.HttpUtility.HtmlEncode(txtProduct5Input.Text.Trim), Web.HttpUtility.HtmlEncode(txtProduct5aInput.Text.Trim), Web.HttpUtility.HtmlEncode(txtProduct5bInput.Text.Trim),
                   strProduct, CDec(Web.HttpUtility.HtmlEncode(txtProduct5Amt.Text)), 0, 0)
            End If
            Dim sc As B2P.ShoppingCart.Cart = New B2P.ShoppingCart.Cart(CStr(Session("UserClientCode")), B2P.Common.Enumerations.TransactionSources.Counter, lis)
            lblSubTotal.Text = String.Format("Sub Total:  ${0:#,###.00}", sc.TotalAmount)

            Session("tiListItems") = lis
            Return sc
        Catch ex As Exception
            lblMessage.Text = "INVALID TRANSACTION"
            lblMessage.ForeColor = Drawing.Color.Red
        End Try
    End Function

    Private Function checkBlockedAccount(clientCode As String, product As String, acct1 As String, acct2 As String, acct3 As String) As Boolean
        Dim IsBlocked As Boolean = False
        Dim SearchResults As New B2P.Common.BlockedAccounts.BlockedAccountResults

        SearchResults = B2P.Common.BlockedAccounts.CheckForBlockedAccount(clientCode, product, acct1, acct2, acct3)

        If SearchResults.IsAccountBlocked Then
            acct = SearchResults
            Session("BlockedAccount") = acct
            IsBlocked = True
            If SearchResults.CreditCardBlocked Then
                ckBlockOverrideCC.Visible = True
                BLL.SessionManager.CreditCardBlocked = True
            Else
                ckBlockOverrideCC.Visible = False
                BLL.SessionManager.CreditCardBlocked = False
            End If
            If SearchResults.ACHBlocked Then
                ckBlockOverrideACH.Visible = True
                BLL.SessionManager.ACHBlocked = True
            Else
                ckBlockOverrideACH.Visible = False
                BLL.SessionManager.ACHBlocked = False
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
            'Disable button here
            mdlPopup.Show()
        Else
            pnlPaymentInformation.Visible = True
        End If
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

            sp.AccountNumber1 = Web.HttpUtility.HtmlEncode(Trim(productInput.Text))
            If Trim(productInputa.Text) <> "" Then
                sp.AccountNumber2 = Web.HttpUtility.HtmlEncode(Trim(productInputa.Text))
            Else
                sp.AccountNumber2 = ""
            End If
            If Trim(productInputb.Text) <> "" Then
                sp.AccountNumber3 = Web.HttpUtility.HtmlEncode(Trim(productInputb.Text))
            Else
                sp.AccountNumber3 = ""
            End If


            sp.ClientCode = CStr(Session("UserClientCode"))
            sp.ProductName = Web.HttpUtility.HtmlEncode(productDropdown.SelectedValue)

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
                            If checkBlockedAccount(sp.ClientCode, sp.ProductName, sp.AccountNumber1, sp.AccountNumber2, sp.AccountNumber3) Then
                                pnlBlock.Visible = True
                                If BLL.SessionManager.CreditCardBlocked Then
                                    lblBlockedDescriptionCreditCard.Text = "This account is blocked from credit card payments. Click below to continue."
                                    ckOverrideBlockCC.Visible = True
                                End If
                                If BLL.SessionManager.ACHBlocked Then
                                    lblBlockedDescriptionACH.Text = "<br />This account is blocked from bank account payments. Click below to continue."
                                    ckOverrideBlockACH.Visible = True
                                End If
                                ckOverrideBlockACH.Checked = False
                                ckOverrideBlockCC.Checked = False
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
            Response.Redirect("/payment/pinPadCHIP.aspx")
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

        If Not IsNothing(Session("BlockedAccount")) Then
            acct = CType(Session("BlockedAccount"), B2P.Common.BlockedAccounts.BlockedAccountResults)
            If acct.IsAccountBlocked Then
                If ckOverrideBlockACH.Checked Or ckOverrideBlockCC.Checked Then
                    If ckOverrideBlockCC.Checked Then
                        BLL.SessionManager.CreditCardBlocked = False
                    End If
                    If ckOverrideBlockACH.Checked Then
                        BLL.SessionManager.ACHBlocked = False
                    End If
                    buttonCart.Enabled = True
                    buttonCart.ImageUrl = "/images/cart.jpg"
                    'Else
                    'clear boxes?
                    'If productNumber = 1 Then
                    '    Response.Redirect("/payment/pinPadCHIP.aspx")
                    'Else
                    '    removeProducts(productNumber)
                    'End If
                End If
            Else

                buttonCart.Enabled = True
                buttonCart.ImageUrl = "/images/cart.jpg"
            End If
        Else

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


    Private Sub btnBlockOverride_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBlockOverride.Click
        Dim buttonCart As ImageButton = CType(FindControl(String.Format("btnCart{0}", CType(lblProductNumber.Text, Integer))), ImageButton)

        If Not IsNothing(Session("BlockedAccount")) Then
            acct = CType(Session("BlockedAccount"), B2P.Common.BlockedAccounts.BlockedAccountResults)
            If acct.IsAccountBlocked Then
                If ckBlockOverrideCC.Visible AndAlso ckBlockOverrideCC.Checked Then
                    BLL.SessionManager.CreditCardBlocked = False
                    addCart(CType(lblProductNumber.Text, Integer))
                ElseIf ckBlockOverrideCC.Visible AndAlso Not ckBlockOverrideCC.Checked Then
                    BLL.SessionManager.CreditCardBlocked = True
                    addCart(CType(lblProductNumber.Text, Integer))
                End If

                If ckBlockOverrideACH.Visible AndAlso ckBlockOverrideACH.Checked Then
                    BLL.SessionManager.ACHBlocked = False
                    addCart(CType(lblProductNumber.Text, Integer))
                ElseIf ckBlockOverrideACH.Visible AndAlso Not ckOverrideBlockACH.Checked Then
                    BLL.SessionManager.ACHBlocked = True
                    addCart(CType(lblProductNumber.Text, Integer))
                End If

                If Not ckBlockOverrideACH.Checked AndAlso Not ckOverrideBlockCC.Checked Then
                    With buttonCart
                        .ImageUrl = "/images/cart.jpg"
                        .Enabled = True
                    End With
                End If
            End If
        End If
    End Sub

    Private Sub btnContinue_Click(sender As Object, e As EventArgs) Handles btnContinue.Click
        Dim token As String = Replace(Guid.NewGuid().ToString, "-", "")

        sc = getItems()
        Session("ShoppingCart") = sc
        BLL.SessionManager.Cart = sc
        If tbEmailAddress.Text <> "" Then
            BLL.SessionManager.CustomerEmail = SafeEncode(tbEmailAddress.Text.Trim)
        End If
        If txtNotes.Text <> "" Then
            BLL.SessionManager.UserComments = Left(SafeEncode(txtNotes.Text), 200)
        End If
        'Store office ID
        If ddlOffice.SelectedValue <> "" Then
            BLL.SessionManager.OfficeID = ddlOffice.SelectedValue
        Else
            BLL.SessionManager.OfficeID = 0
        End If


        Response.Redirect("/payment/default.aspx?token=" & token)
    End Sub


End Class