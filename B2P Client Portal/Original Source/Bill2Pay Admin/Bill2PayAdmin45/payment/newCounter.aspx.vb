Imports System.Configuration.ConfigurationManager
Imports Telerik.Web.UI
Imports System.Threading
Imports System.Drawing

Partial Public Class newCounter
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

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        UserSecurity = CType(Session("RoleInfo"), B2PAdminBLL.Role)
        If Not IsPostBack Then
            Session("ClickedButton") = False
            ddlProduct1.Focus()
            loadData()
            loadProducts()
            Session("tiListItems") = Nothing
            Session("TempFee") = 0
        End If

        If Not UserSecurity.MakeACCPayment Then
            Response.Redirect("/error/default.aspx?type=security")
        End If
    End Sub

    Protected Sub ddlBankAccountType_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ddlBankAccountType.SelectedIndexChanged
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

    Private Sub BuildACH()
        ddlBankAccountType.DataSourceID = "XmlDataSource1"
        ddlBankAccountType.DataBind()
        ddlBankAccountType.SortItems()

    End Sub

    Private Sub loadData()

        loadStates()
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

        Dim a As New B2P.Web.Counter.PaymentSupport
        a.GetAcceptedPaymentTypes(CStr(Session("ClientCode")), B2P.Web.Counter.PaymentSupport.Sources.Counter)
        If a.AcceptCreditCards Then
            RadioButtonList1.Items.FindByValue("CC").Enabled = True
            RadioButtonList1.SelectedValue = "CC"
            pnlCreditCard.Visible = True
            loadCountry()
            loadExpirationDate()
        Else
            RadioButtonList1.Items.FindByValue("CC").Enabled = False
            pnlCreditCard.Visible = False
        End If
        If a.AcceptACH Then
            RadioButtonList1.Items.FindByValue("ACH").Enabled = True
            If Not a.AcceptCreditCards Then
                RadioButtonList1.SelectedValue = "ACH"
                pnlACH.Visible = True
                BuildACH()
            End If
        Else
            RadioButtonList1.Items.FindByValue("ACH").Enabled = False
            pnlACH.Visible = False
        End If

    End Sub

    Private Sub loadCountry()
        ddlCountry.Items.Clear()
        ddlCountry.DataSource = B2P.Payment.PaymentBase.ListCountries
        ddlCountry.DataTextField = "CountryName"
        ddlCountry.DataValueField = "CountryCode"
        ddlCountry.DataBind()
    End Sub

    Protected Sub RadioButtonList1_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles RadioButtonList1.SelectedIndexChanged

        If Me.RadioButtonList1.SelectedValue = "CC" Then
            Me.pnlACH.Visible = False
            Me.pnlCreditCard.Visible = True
            loadCountry()
            loadExpirationDate()
            tbBankAccountNumber.Text = ""
            tbBankRoutingNumber.Text = ""
            tbBankAccountFirstName.Text = ""
            tbBankAccountLastName.Text = ""
            btnSubmit.Enabled = False
        Else
            Me.pnlCreditCard.Visible = False
            Me.pnlACH.Visible = True
            tbCreditCardNumber.Text = ""
            tbSecurityCode.Text = ""
            tbFirstName.Text = ""
            tbLastName.Text = ""
            btnSubmit.Enabled = False

            BuildACH()
        End If
        ddlProduct1.Items.Clear()

        clearProducts()


        loadProducts()
    End Sub

    Private Sub btnClearAll_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClearAll.Click
        Response.Redirect("/payment/newcounter.aspx")
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
        lblAmount.Text = "Total Amount: $0.00"
        lblSubTotal.Text = "Sub Total Amount: $0.00"
        Session("tiListItems") = Nothing
    End Sub

    Protected Sub ddlAUClient_SelectedIndexChanged(ByVal o As Object, ByVal e As Global.Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles ddlAUClient.SelectedIndexChanged
        Dim a As New B2P.Web.Counter.PaymentSupport
        a.GetAcceptedPaymentTypes(CStr(ddlAUClient.SelectedItem.Text), B2P.Web.Counter.PaymentSupport.Sources.Counter)

        If a.AcceptCreditCards Then
            RadioButtonList1.Items.FindByValue("CC").Enabled = True
            RadioButtonList1.SelectedValue = "CC"
            pnlCreditCard.Visible = True
            loadCountry()
            loadExpirationDate()
        Else
            RadioButtonList1.Items.FindByValue("CC").Enabled = False
            pnlCreditCard.Visible = False
        End If
        If a.AcceptACH Then
            RadioButtonList1.Items.FindByValue("ACH").Enabled = True
            If Not a.AcceptCreditCards Then
                RadioButtonList1.SelectedValue = "ACH"
                pnlACH.Visible = True
            End If
        Else
            RadioButtonList1.Items.FindByValue("ACH").Enabled = False
            pnlACH.Visible = False
        End If

        loadOffices()
        ddlProduct1.Items.Clear()
        clearProducts()
        loadProducts()
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

            If RadioButtonList1.SelectedValue = "CC" Then
                listProducts = y.GetSourceProductList(CStr(Session("UserClientCode")), B2P.Web.Counter.PaymentSupport.PaymentTypes.CreditCard, B2P.Web.Counter.PaymentSupport.Sources.Counter)
            Else
                listProducts = y.GetSourceProductList(CStr(Session("UserClientCode")), B2P.Web.Counter.PaymentSupport.PaymentTypes.BankAccount, B2P.Web.Counter.PaymentSupport.Sources.Counter)
            End If
            ddlProduct1.Items.Add(New RadComboBoxItem("Select Product", ""))
            ddlProduct1.DataSource = listProducts
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
            btnSubmit.Enabled = False
            mdlPopup.Show()
        End Try
    End Sub

    Private Sub loadStates()
        'Load states
        ddlState.Items.Clear()

        ddlState.DataSourceID = "XmlDataSource2"
        ddlState.DataBind()
        ddlState.SortItems()
        ddlState.SelectedValue = "FL"


    End Sub

    Protected Sub ddlProduct1_SelectedIndexChanged(ByVal o As Object, ByVal e As Global.Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles ddlProduct1.SelectedIndexChanged
        Try

            Dim product1Selected As String = Replace(HttpUtility.HtmlEncode(ddlProduct1.SelectedValue), "&amp;", "&")

            Dim listProducts As List(Of String)
            If RadioButtonList1.SelectedValue = "CC" Then
                listProducts = y.GetSourceProductList(CStr(Session("UserClientCode")), B2P.Web.Counter.PaymentSupport.PaymentTypes.CreditCard, product1Selected, B2P.Web.Counter.PaymentSupport.Sources.Counter)
            Else
                listProducts = y.GetSourceProductList(CStr(Session("UserClientCode")), B2P.Web.Counter.PaymentSupport.PaymentTypes.BankAccount, product1Selected, B2P.Web.Counter.PaymentSupport.Sources.Counter)
            End If
            ddlProduct1.Items.Clear()
            ddlProduct1.DataSource = listProducts
            ddlProduct1.DataBind()
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

                SetFocus(txtProduct1Input)
            End If
        Catch ex As Exception
            'Response.Write(ex.Message)
            Response.Redirect("/error/default.aspx?type=system", False)
        End Try
    End Sub

    Private Sub loadExpirationDate()
        ddlMonth2.Items.Clear()
        ddlMonth2.Items.Add(New ListItem("01", "01"))
        ddlMonth2.Items.Add(New ListItem("02", "02"))
        ddlMonth2.Items.Add(New ListItem("03", "03"))
        ddlMonth2.Items.Add(New ListItem("04", "04"))
        ddlMonth2.Items.Add(New ListItem("05", "05"))
        ddlMonth2.Items.Add(New ListItem("06", "06"))
        ddlMonth2.Items.Add(New ListItem("07", "07"))
        ddlMonth2.Items.Add(New ListItem("08", "08"))
        ddlMonth2.Items.Add(New ListItem("09", "09"))
        ddlMonth2.Items.Add(New ListItem("10", "10"))
        ddlMonth2.Items.Add(New ListItem("11", "11"))
        ddlMonth2.Items.Add(New ListItem("12", "12"))

        loadYear()
    End Sub

    Private Sub loadYear()
        ddlYear2.Items.Clear()

        Dim i, x As Integer
        x = Year(Today())

        For i = x To x + 10
            ddlYear2.Items.Add(New ListItem(CStr(i)))
        Next
        'End If
        Me.ddlMonth2.Focus()

    End Sub

    '*** Telerik control replace with standard .NET control
    'Private Sub loadExpirationDate()
    '    ddlMonth.Items.Clear()
    '    ddlMonth.Items.Add(New RadComboBoxItem("01", "01"))
    '    ddlMonth.Items.Add(New RadComboBoxItem("02", "02"))
    '    ddlMonth.Items.Add(New RadComboBoxItem("03", "03"))
    '    ddlMonth.Items.Add(New RadComboBoxItem("04", "04"))
    '    ddlMonth.Items.Add(New RadComboBoxItem("05", "05"))
    '    ddlMonth.Items.Add(New RadComboBoxItem("06", "06"))
    '    ddlMonth.Items.Add(New RadComboBoxItem("07", "07"))
    '    ddlMonth.Items.Add(New RadComboBoxItem("08", "08"))
    '    ddlMonth.Items.Add(New RadComboBoxItem("09", "09"))
    '    ddlMonth.Items.Add(New RadComboBoxItem("10", "10"))
    '    ddlMonth.Items.Add(New RadComboBoxItem("11", "11"))
    '    ddlMonth.Items.Add(New RadComboBoxItem("12", "12"))
    '    ddlMonth.SelectedItem.Text = "01"

    '    loadYear()

    'End Sub

    'Private Sub loadYear()
    '    ddlYear.Items.Clear()
    '    'If CInt(ddlMonth.SelectedItem.Text) < CInt(Date.Now.Month) Then
    '    '    ddlYear.Items.Clear()
    '    '    Dim i, x As Integer
    '    '    x = Year(Today()) + 1

    '    '    For i = x To x + 10
    '    '        ddlYear.Items.Add(New RadComboBoxItem(CStr(i)))
    '    '    Next
    '    'Else
    '    ddlYear.Items.Clear()
    '    Dim i, x As Integer
    '    x = Year(Today())

    '    For i = x To x + 10
    '        ddlYear.Items.Add(New RadComboBoxItem(CStr(i)))
    '    Next
    '    'End If
    '    Me.ddlMonth.Focus()
    'End Sub

    Private Function getItems() As B2P.Payment.PaymentBase.TransactionItems
        Try
            Dim strProduct As String
            Dim ti As New B2P.Payment.PaymentBase.TransactionItems
            If txtProduct1Input.Text.Trim <> "" And txtProduct1Input.Enabled = False Then
                strProduct = Replace(HttpUtility.HtmlEncode(ddlProduct1.SelectedItem.Text), "&amp;", "&")
                ti.Add(HttpUtility.HtmlEncode(txtProduct1Input.Text.Trim), HttpUtility.HtmlEncode(txtProduct1aInput.Text.Trim), HttpUtility.HtmlEncode(txtProduct1bInput.Text.Trim), strProduct, CDec(HttpUtility.HtmlEncode(txtProduct1Amt.Text)), 0, 0)
            End If

            If txtProduct2Input.Text.Trim <> "" And txtProduct2Input.Enabled = False Then
                strProduct = Replace(HttpUtility.HtmlEncode(ddlProduct2.SelectedItem.Text), "&amp;", "&")
                ti.Add(HttpUtility.HtmlEncode(txtProduct2Input.Text.Trim), HttpUtility.HtmlEncode(txtProduct2aInput.Text.Trim), HttpUtility.HtmlEncode(txtProduct2bInput.Text.Trim), strProduct, CDec(HttpUtility.HtmlEncode(txtProduct2Amt.Text)), 0, 0)
            End If

            If txtProduct3Input.Text.Trim <> "" And txtProduct3Input.Enabled = False Then
                strProduct = Replace(HttpUtility.HtmlEncode(ddlProduct3.SelectedItem.Text), "&amp;", "&")
                ti.Add(HttpUtility.HtmlEncode(txtProduct3Input.Text.Trim), HttpUtility.HtmlEncode(txtProduct3aInput.Text.Trim), HttpUtility.HtmlEncode(txtProduct3bInput.Text.Trim), strProduct, CDec(HttpUtility.HtmlEncode(txtProduct3Amt.Text)), 0, 0)
            End If

            If txtProduct4Input.Text.Trim <> "" And txtProduct4Input.Enabled = False Then
                strProduct = Replace(HttpUtility.HtmlEncode(ddlProduct4.SelectedItem.Text), "&amp;", "&")
                ti.Add(HttpUtility.HtmlEncode(txtProduct4Input.Text.Trim), HttpUtility.HtmlEncode(txtProduct4aInput.Text.Trim), HttpUtility.HtmlEncode(txtProduct4bInput.Text.Trim), strProduct, CDec(HttpUtility.HtmlEncode(txtProduct4Amt.Text)), 0, 0)
            End If

            If txtProduct5Input.Text.Trim <> "" And txtProduct5Input.Enabled = False Then
                strProduct = Replace(HttpUtility.HtmlEncode(ddlProduct5.SelectedItem.Text), "&amp;", "&")
                ti.Add(HttpUtility.HtmlEncode(txtProduct5Input.Text.Trim), HttpUtility.HtmlEncode(txtProduct5aInput.Text.Trim), HttpUtility.HtmlEncode(txtProduct5bInput.Text.Trim), strProduct, CDec(HttpUtility.HtmlEncode(txtProduct5Amt.Text)), 0, 0)
            End If
            Return ti
        Catch ex As Exception
            lblMessage.Text = "INVALID TRANSACTION"
            lblMessage.ForeColor = Drawing.Color.Red
        End Try
    End Function

    Private Function validatePayment(ByVal tiItems As B2P.Payment.PaymentBase.TransactionItems, ByVal buttonType As String) As B2P.Web.Counter.PaymentSupport.ValidateTransactionReturn

        Dim hasCreditCard As String = ""
        If tbCreditCardNumber.Text <> "" Then
            hasCreditCard = HttpUtility.HtmlEncode(tbCreditCardNumber.Text)
        Else
            hasCreditCard = ""
        End If

        Dim hasBankAccount As String = ""
        If tbBankAccountNumber.Text <> "" Then
            hasBankAccount = HttpUtility.HtmlEncode(tbBankAccountNumber.Text)
        Else
            hasBankAccount = ""
        End If


        Dim vtr As B2P.Web.Counter.PaymentSupport.ValidateTransactionReturn
        Try
            If RadioButtonList1.SelectedValue = "CC" Then
                If hasCreditCard <> "" Then
                    vtr = y.ValidateTransaction(CStr(Session("UserClientCode")), tiItems, B2P.Web.Counter.PaymentSupport.PaymentTypes.CreditCard, tbCreditCardNumber.Text, B2P.Web.Counter.PaymentSupport.Sources.Counter)
                Else
                    btnSubmit.Enabled = False
                    btnSubmit.ImageUrl = "/images/btnSubmitPaymentDisabled.jpg"
                    vtr = y.ValidateTransaction(CStr(Session("UserClientCode")), tiItems, B2P.Web.Counter.PaymentSupport.PaymentTypes.CreditCard, B2P.Web.Counter.PaymentSupport.Sources.Counter)
                End If

            Else
                If hasBankAccount <> "" Then
                    btnSubmit.Enabled = True
                    btnSubmit.ImageUrl = "/images/btnSubmitPayment.jpg"
                Else
                    btnSubmit.Enabled = False
                    btnSubmit.ImageUrl = "/images/btnSubmitPaymentDisabled.jpg"
                End If

                Dim i As Integer
                Dim hasProducts As Boolean = False
                For i = 1 To 5
                    Dim productInput As TextBox = CType(FindControl(String.Format("txtProduct{0}Input", i)), TextBox)
                    If productInput.Text <> "" Then
                        hasProducts = True
                    End If
                Next
                If hasProducts = True Then
                    vtr = y.ValidateTransaction(CStr(Session("UserClientCode")), tiItems, B2P.Web.Counter.PaymentSupport.PaymentTypes.BankAccount, B2P.Web.Counter.PaymentSupport.Sources.Counter)
                Else
                    vtr.Result = B2P.Web.Counter.PaymentSupport.ValidateTransactionReturn.Results.ErrorOccurred
                End If

                'vtr = y.ValidateTransaction(CStr(Session("UserClientCode")), tiItems, B2P.Web.Counter.PaymentSupport.PaymentTypes.BankAccount, "")
            End If

            lblAmount.Text = "INVALID TRANSACTION"
            lblAmount.ForeColor = Drawing.Color.Red

            Select Case vtr.Result
                Case B2P.Web.Counter.PaymentSupport.ValidateTransactionReturn.Results.Valid


                    Dim total As Double = CDbl(tiItems.TotalAmount + vtr.ConvenienceFee)
                    lblAmount.Text = String.Format("Total:  ${0:#,###.00} (Fee: ${1:#,###.00})", total, vtr.ConvenienceFee)
                    lblSubTotal.Text = String.Format("Sub Total:  ${0:#,###.00}", total - vtr.ConvenienceFee)
                    hidAmount.Value = CStr(tiItems.TotalAmount)
                    If buttonType <> "submit" Then
                        Session("TempFee") = vtr.ConvenienceFee
                    End If
                    lblAmount.ForeColor = Drawing.Color.Black
                    If hasCreditCard <> "" Or hasBankAccount <> "" Then
                        btnSubmit.Enabled = True
                        btnSubmit.ImageUrl = "/images/btnSubmitPayment.jpg"
                    End If
                ' For debugging
                'lblMessage.Text = vtr.ConvenienceFee & " " & CDec(Session("TempFee")) & " " & tbCreditCardNumber.Text
                'mdlPopup.Show()

                Case B2P.Web.Counter.PaymentSupport.ValidateTransactionReturn.Results.AmountNotInFeeRange
                    lblMessageTitle.Text = "Invalid Product and/or Payment Selection"
                    lblMessage.Text = "Message: Amount not within fee range."
                    btnSubmit.Enabled = False
                    mdlPopup.Show()
                Case B2P.Web.Counter.PaymentSupport.ValidateTransactionReturn.Results.CardNotAccepted
                    lblMessageTitle.Text = "Invalid Product and/or Payment Selection"
                    btnSubmit.Enabled = False
                    lblMessage.Text = "Message: Card not accepted for this transaction."
                    mdlPopup.Show()
                Case B2P.Web.Counter.PaymentSupport.ValidateTransactionReturn.Results.ErrorCalculatingFee
                    lblMessageTitle.Text = "Invalid Product and/or Payment Selection"
                    btnSubmit.Enabled = False
                    lblMessage.Text = "Message: Fee calculation error."
                    mdlPopup.Show()
                Case B2P.Web.Counter.PaymentSupport.ValidateTransactionReturn.Results.ErrorOccurred
                    lblMessageTitle.Text = "Invalid Product and/or Payment Selection"
                    btnSubmit.Enabled = False
                    lblMessage.Text = "Message: Transaction was not processed."
                    mdlPopup.Show()
                Case B2P.Web.Counter.PaymentSupport.ValidateTransactionReturn.Results.MultipleFees
                    lblMessageTitle.Text = "Invalid Product and/or Payment Selection"
                    btnSubmit.Enabled = False
                    lblMessage.Text = "Message: Not a valid transaction."
                    mdlPopup.Show()
                Case B2P.Web.Counter.PaymentSupport.ValidateTransactionReturn.Results.ProductAmountLimitExceeded
                    lblMessageTitle.Text = "Invalid Product and/or Payment Selection"
                    btnSubmit.Enabled = False
                    lblMessage.Text = "Message: Amount exceeds maximum payment amount."
                    mdlPopup.Show()
                Case B2P.Web.Counter.PaymentSupport.ValidateTransactionReturn.Results.MinimumPaymentRequired
                    lblMessageTitle.Text = "Invalid Product and/or Payment Selection"
                    btnSubmit.Enabled = False
                    lblMessage.Text = "Message: Amount does not meet the minimum required."
                    mdlPopup.Show()
                Case Else
                    btnSubmit.Enabled = False
                    lblMessage.Text = "Message: Not a valid transaction."
                    mdlPopup.Show()
            End Select
        Catch ex As Exception
            btnSubmit.Enabled = False
            lblMessage.Text = "Message: Not a valid transaction."
            mdlPopup.Show()
        End Try
        Return vtr

    End Function

    'Protected Sub ddlMonth_SelectedIndexChanged(ByVal o As Object, ByVal e As Global.Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles ddlMonth.SelectedIndexChanged
    'loadYear()
    'End Sub

    Protected Sub btnSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSubmit.Click

        Try
            tiListItems = CType(Session("tiListItems"), B2P.Payment.PaymentBase.TransactionItems)

            Dim vtr As B2P.Web.Counter.PaymentSupport.ValidateTransactionReturn
            vtr = validatePayment(CType(Session("tiListItems"), B2P.Payment.PaymentBase.TransactionItems), "submit")

            Select Case vtr.Result

                Case B2P.Web.Counter.PaymentSupport.ValidateTransactionReturn.Results.Valid

                    If vtr.ConvenienceFee = CDec(Session("TempFee")) Then
                        If RadioButtonList1.SelectedValue = "CC" Then
                            Dim ccnumber As String = ""
                            If CardData.Value.Length > 0 Then

                                Dim y As New B2P.Web.Counter.CardSwipe.CardDecryption ' B2P.Web.Counter.CardSwipe.MagensaWSWrapper
                                Dim ci As New B2P.Web.Counter.CardSwipe.CreditCardInfo

                                ci = y.DecryptCard(CardData.Value)

                                If ci.Success = True Then
                                    ccnumber = ci.CreditCardNumber
                                    CardData.Value = ""
                                Else
                                    lblMessageTitle.Text = "Credit Card Error"
                                    lblMessage.Text = "Message: Error reading swiped card. Please try again."
                                    mdlPopup.Show()
                                    CardData.Value = ""
                                    tbCreditCardNumber.Text = ""
                                    tbFirstName.Text = ""
                                    tbLastName.Text = ""
                                    tbSecurityCode.Text = ""
                                    Return

                                End If

                            Else
                                ccnumber = HttpUtility.HtmlEncode(tbCreditCardNumber.Text)
                            End If

                            'validate expiration date
                            Dim ccDate As String = String.Format("{0}/{1}", ddlMonth2.SelectedItem.Text, ddlYear2.SelectedItem.Text)
                            If CDate(ccDate) < CDate((String.Format("{0}/{1}", DateTime.Now.Month, DateTime.Now.Year))) Then
                                lblMessageTitle.Text = "Invalid Payment Selection"
                                lblMessage.Text = "Message: Invalid credit card expiration date."
                                mdlPopup.Show()

                            Else
                                If CBool(Session("ClickedButton")) = False Then

                                    Dim ccpr As B2P.Payment.CreditCardPayment.CreditCardPaymentResults
                                    ccpr = payCreditCard(tiListItems, ccnumber)

                                    Select Case ccpr.Result
                                        Case B2P.Payment.CreditCardPayment.CreditCardPaymentResults.Results.Success
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
                                                        If ti.AccountNumber2.Trim <> "" Then
                                                            c.AccountNumber2 = ti.AccountNumber2
                                                        Else
                                                            c.AccountNumber2 = ""
                                                        End If
                                                        If ti.AccountNumber3.Trim <> "" Then
                                                            c.AccountNumber3 = ti.AccountNumber3
                                                        Else
                                                            c.AccountNumber3 = ""
                                                        End If

                                                        c.ConfirmationNumber = CStr(Session("ConfirmationNumber"))
                                                        c.Amount = ti.Amount
                                                        c.AuthorizationCode = CStr(Session("AuthorizationCode"))
                                                        c.PaymentType = B2P.ClientInterface.Manager.ClientInterfaceWS.PaymentTypes.CreditCard
                                                        c.ClientCode = CStr(Session("UserClientCode"))
                                                        c.ProductName = ti.ProductName
                                                        c.PaymentAccountNumber = Right(HttpUtility.HtmlEncode(tbCreditCardNumber.Text), 4)
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
                                            Response.Redirect("/payment/return.aspx")
                                        Case B2P.Payment.CreditCardPayment.CreditCardPaymentResults.Results.Failed
                                            Session("ClickedButton") = False
                                            Session("ConfirmationNumber") = ""
                                            Session("AuthorizationCode") = ""
                                            clearCCFields()
                                            lblMessageTitle.Text = "Payment Processing Error"
                                            lblMessage.Text = "Message: " & ccpr.Message '"Credit card payment was declined."
                                            mdlPopup.Show()

                                        Case Else
                                            Session("ClickedButton") = False
                                            Session("ConfirmationNumber") = ""
                                            Session("AuthorizationCode") = ""
                                            clearCCFields()
                                            lblMessageTitle.Text = "Payment Processing Error"
                                            lblMessage.Text = "Message: " & ccpr.Message '"Credit card payment was not processed."
                                            mdlPopup.Show()
                                    End Select
                                Else
                                    Response.Redirect("/payment/return.aspx")
                                End If
                            End If

                        Else
                            If CBool(Session("ClickedButton")) = False Then
                                Dim bapr As B2P.Payment.BankAccountPayment.BankAccountPaymentResults
                                bapr = payBankAccount(CType(Session("tiListItems"), B2P.Payment.PaymentBase.TransactionItems))
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
                                        Response.Redirect("/payment/return.aspx")
                                    Case Else
                                        Session("ConfirmationNumber") = ""
                                        lblMessageTitle.Text = "Payment Processing Error"
                                        lblMessage.Text = "Message: Bank account payment was not processed."
                                        mdlPopup.Show()
                                End Select
                            Else
                                Response.Redirect("/payment/return.aspx")
                            End If
                        End If
                    Else
                        lblMessageTitle.Text = "Calculation Change"
                        lblMessage.Text = "Message: The total amount and/or fee does not match the original transaction. Recalculate transaction."
                        btnSubmit.Enabled = False
                        mdlPopup.Show()

                    End If
                Case Else
                    lblMessage.Text = "Message: Not a valid transaction."
                    mdlPopup.Show()
            End Select
        Catch te As ThreadAbortException
        Catch ex As Exception
            B2P.Common.Logging.LogError("B2P Admin", "Payment Processing Error: " & ex.ToString, B2P.Common.Logging.NotifySupport.No)
            lblMessageTitle.Text = "Payment Processing Error"
            lblMessage.Text = "Message: Payment was not processed."
            mdlPopup.Show()
        End Try
    End Sub

    Private Function payCreditCard(ByVal tiListItems As B2P.Payment.PaymentBase.TransactionItems, ByVal creditCardNumber As String) As B2P.Payment.CreditCardPayment.CreditCardPaymentResults

        Dim x As New B2P.Payment.CreditCardPayment(CStr(Session("ClientCode")))
        x.PaymentSource = B2P.Common.Enumerations.TransactionSources.Counter
        x.UserComments = Left(HttpUtility.HtmlEncode(txtNotes.Text), 200)
        If ddlOffice.SelectedValue = "" Then
            x.Office_ID = 0
        Else
            x.Office_ID = CInt(HttpUtility.HtmlEncode(ddlOffice.SelectedValue))
            'Keep current selected office stored for users to make another payment with stored office selection
            Session("SelectedOffice") = (HttpUtility.HtmlEncode(ddlOffice.SelectedValue))
        End If
        x.ClientCode = CStr(Session("UserClientCode"))
        x.Security_ID = CInt(Session("SecurityID"))
        x.Items = tiListItems
        x.AllowConfirmationEmails = True

        Dim cc As New B2P.Common.Objects.CreditCard


        cc.CreditCardNumber = creditCardNumber
        cc.SecurityCode = HttpUtility.HtmlEncode(tbSecurityCode.Text)

        cc.ExpirationMonth = HttpUtility.HtmlEncode(ddlMonth2.SelectedItem.Text)
        cc.ExpirationYear = HttpUtility.HtmlEncode(ddlYear2.SelectedItem.Text)
        cc.Owner.FirstName = HttpUtility.HtmlEncode(tbFirstName.Text)
        cc.Owner.LastName = HttpUtility.HtmlEncode(tbLastName.Text)
        cc.Owner.CountryCode = HttpUtility.HtmlEncode(ddlCountry.SelectedValue)
        If txtAddress.Text <> "" Then
            cc.Owner.Address1 = HttpUtility.HtmlEncode(txtAddress.Text)
        Else
            cc.Owner.Address1 = "NOT PROVIDED"
        End If
        cc.Owner.Address2 = HttpUtility.HtmlEncode(txtAddress2.Text)

        If txtCity.Text <> "" Then
            cc.Owner.City = HttpUtility.HtmlEncode(txtCity.Text)
        Else
            cc.Owner.City = "NOT PROVIDED"
        End If

        If ddlState.SelectedValue <> "" Then
            cc.Owner.State = HttpUtility.HtmlEncode(ddlState.SelectedValue)
        Else
            cc.Owner.State = "FL"
        End If

        cc.Owner.ZipCode = txtZip.Text
        If tbEmailAddress.Text <> "" Then
            cc.Owner.EMailAddress = HttpUtility.HtmlEncode(tbEmailAddress.Text)
        Else
            cc.Owner.EMailAddress = "null@cybersource.com"
        End If

        cc.Owner.PhoneNumber = HttpUtility.HtmlEncode(tbHomePhone.Text)


        Dim ccpr As B2P.Payment.CreditCardPayment.CreditCardPaymentResults = x.PayByCreditCard(cc)
        Session("ConfirmationNumber") = ccpr.ConfirmationNumber
        Session("AuthorizationCode") = ccpr.AuthorizationCode

        Return ccpr

    End Function

    Private Function payBankAccount(ByVal tiListItems As B2P.Payment.PaymentBase.TransactionItems) As B2P.Payment.BankAccountPayment.BankAccountPaymentResults
        Dim x As New B2P.Payment.BankAccountPayment(CStr(Session("UserClientCode")))

        Dim ba As New B2P.Common.Objects.BankAccount
        x.PaymentSource = B2P.Common.Enumerations.TransactionSources.Counter
        x.UserComments = Left(HttpUtility.HtmlEncode(txtNotes.Text), 200)
        x.Security_ID = CInt(Session("SecurityID"))
        x.AllowConfirmationEmails = True
        ba.Owner.FirstName = HttpUtility.HtmlEncode(tbBankAccountFirstName.Text)
        ba.Owner.LastName = HttpUtility.HtmlEncode(tbBankAccountLastName.Text)
        If txtAddress.Text <> "" Then
            ba.Owner.Address1 = HttpUtility.HtmlEncode(txtAddress.Text)
        Else
            ba.Owner.Address1 = "NOT PROVIDED"
        End If
        ba.Owner.Address2 = HttpUtility.HtmlEncode(txtAddress2.Text)

        If txtCity.Text <> "" Then
            ba.Owner.City = HttpUtility.HtmlEncode(txtCity.Text)
        Else
            ba.Owner.City = "NOT PROVIDED"
        End If

        If ddlState.SelectedValue <> "" Then
            ba.Owner.State = HttpUtility.HtmlEncode(ddlState.SelectedValue)
        Else
            ba.Owner.State = "XX"
        End If

        ba.Owner.ZipCode = HttpUtility.HtmlEncode(txtZip.Text)
        If tbEmailAddress.Text <> "" Then
            ba.Owner.EMailAddress = HttpUtility.HtmlEncode(tbEmailAddress.Text)
        Else
            ba.Owner.EMailAddress = "null@cybersource.com"
        End If

        ba.Owner.PhoneNumber = HttpUtility.HtmlEncode(tbHomePhone.Text)
        If ddlOffice.SelectedValue = "" Then
            x.Office_ID = 0
        Else
            x.Office_ID = CInt(HttpUtility.HtmlEncode(ddlOffice.SelectedValue))
            'Keep current selected office stored for users to make another payment with stored office selection
            Session("SelectedOffice") = (HttpUtility.HtmlEncode(ddlOffice.SelectedValue))
        End If

        x.ClientCode = CStr(Session("UserClientCode"))
        x.Items = tiListItems


        ba.BankAccountNumber = HttpUtility.HtmlEncode(tbBankAccountNumber.Text)
        Select Case ba.ValidateBankRoutingNumber(HttpUtility.HtmlEncode(tbBankRoutingNumber.Text), B2P.Common.Objects.BankAccount.RoutingNumberValidationMode.FederalReserveLookup)
            Case B2P.Common.Objects.BankAccount.ValidationStatus.Invalid
                Me.RegularExpressionValidator4.IsValid = False
            Case Else
                ba.BankRoutingNumber = HttpUtility.HtmlEncode(tbBankRoutingNumber.Text)

        End Select



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
        If Page.IsValid = True Then
            Dim bapr As B2P.Payment.BankAccountPayment.BankAccountPaymentResults = x.PayByBankAccount(ba)
            Session("ConfirmationNumber") = bapr.ConfirmationNumber
            Return bapr
        End If


    End Function

    Private Sub btnResetCCFields_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnResetCCFields.Click
        clearCCFields()
    End Sub

    Private Sub clearCCFields()
        CardData.Value = ""
        tbCreditCardNumber.Text = ""
        tbSecurityCode.Text = ""
        tbFirstName.Text = ""
        tbLastName.Text = ""
        loadExpirationDate()
    End Sub

    Private Sub loadProvince()
        ddlState.Items.Clear()
        ddlState.Items.Add(New RadComboBoxItem("AB - Alberta", "AB"))
        ddlState.Items.Add(New RadComboBoxItem("BC - British Columbia", "BC"))
        ddlState.Items.Add(New RadComboBoxItem("MB - Manitoba", "MB"))
        ddlState.Items.Add(New RadComboBoxItem("NB - New Brunswick", "NB"))
        ddlState.Items.Add(New RadComboBoxItem("NL - Newfoundland And Labrador", "NL"))
        ddlState.Items.Add(New RadComboBoxItem("NS - Nova Scotia", "NS"))
        ddlState.Items.Add(New RadComboBoxItem("NT - Northwest Territories", "NT"))
        ddlState.Items.Add(New RadComboBoxItem("NU - Nunavut", "NU"))
        ddlState.Items.Add(New RadComboBoxItem("ON - Ontario", "ON"))
        ddlState.Items.Add(New RadComboBoxItem("PE - Prince Edward Island", "PE"))
        ddlState.Items.Add(New RadComboBoxItem("QC - Quebec", "QC"))
        ddlState.Items.Add(New RadComboBoxItem("SK - Saskatchewan", "SK"))
        ddlState.Items.Add(New RadComboBoxItem("YT - Yukon", "YT"))
    End Sub

    Private Sub setCountrySpecs()

        Select Case Me.ddlCountry.SelectedValue
            Case "US"
                Me.pnlState.Visible = True
                loadStates()
                Me.lblBillingZip.Text = "Zip"
                Me.lblBillingState.Text = "State"
                Me.txtZip.MaxLength = 5
                Me.FilteredTextBoxExtender9.FilterType = AjaxControlToolkit.FilterTypes.Numbers
                Me.RegularExpressionValidator3.ValidationExpression = "^[a-zA-Z0-9]{5,9}$"
            Case "CA"
                Me.pnlState.Visible = True
                Me.lblBillingState.Text = "Province"
                loadProvince()
                Me.lblBillingZip.Text = "Postal Code"
                Me.txtZip.MaxLength = 7
                Me.FilteredTextBoxExtender9.FilterType = AjaxControlToolkit.FilterTypes.LowercaseLetters Or AjaxControlToolkit.FilterTypes.Numbers Or AjaxControlToolkit.FilterTypes.UppercaseLetters Or AjaxControlToolkit.FilterTypes.Custom
                Me.FilteredTextBoxExtender9.ValidChars = " "
                Me.RegularExpressionValidator3.ValidationExpression = "^[a-zA-Z][0-9][a-zA-Z]\s?[0-9][a-zA-Z][0-9]$"
            Case Else
                Me.pnlState.Visible = False
                Me.lblBillingZip.Text = "Postal Code"
                Me.txtZip.MaxLength = 7
                Me.FilteredTextBoxExtender9.FilterType = AjaxControlToolkit.FilterTypes.LowercaseLetters Or AjaxControlToolkit.FilterTypes.Numbers Or AjaxControlToolkit.FilterTypes.UppercaseLetters Or AjaxControlToolkit.FilterTypes.Custom
                Me.FilteredTextBoxExtender9.ValidChars = " "
                Me.RegularExpressionValidator3.Enabled = False

        End Select
    End Sub

    Protected Sub ddlCountry_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlCountry.SelectedIndexChanged
        clearAddressFields()
        setCountrySpecs()
    End Sub

    Private Sub clearAddressFields()
        Me.txtAddress.Text = ""
        Me.txtAddress2.Text = ""
        Me.txtCity.Text = ""
        Me.txtZip.Text = ""
    End Sub

    Private Sub btnReCalculate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReCalculate.Click
        If CDbl(hidAmount.Value) > 0 Then

            validatePayment(CType(Session("tiListItems"), B2P.Payment.PaymentBase.TransactionItems), "calc")
        Else
            lblMessage.Text = "Message: Please add a product to the cart before calculating."
            mdlPopup.Show()
        End If

    End Sub

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

    Private Sub btnCart1_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnCart1.Click
        'addCart(1)
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
            Session("tiListItems") = getItems()

            Dim vtr As B2P.Web.Counter.PaymentSupport.ValidateTransactionReturn
            vtr = validatePayment(CType(Session("tiListItems"), B2P.Payment.PaymentBase.TransactionItems), "calc")
            If vtr.Result = B2P.Web.Counter.PaymentSupport.ValidateTransactionReturn.Results.Valid Then
                If RadioButtonList1.SelectedValue = "CC" Then
                    listProducts = y.GetSourceProductList(CStr(Session("UserClientCode")), B2P.Web.Counter.PaymentSupport.PaymentTypes.CreditCard, productDropdown.SelectedValue, B2P.Web.Counter.PaymentSupport.Sources.Counter)
                Else
                    listProducts = y.GetSourceProductList(CStr(Session("UserClientCode")), B2P.Web.Counter.PaymentSupport.PaymentTypes.BankAccount, productDropdown.SelectedValue, B2P.Web.Counter.PaymentSupport.Sources.Counter)
                End If
                If productNumber <> 5 Then
                    If productNumber <> ItemCount Then
                        pnlProductNext.Visible = True
                        productDropdownNext.Items.Clear()
                        productDropdownNext.Enabled = True
                        productDropdownNext.Items.Add(New RadComboBoxItem("Select Product", ""))
                        productDropdownNext.DataSource = listProducts
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

            Else

                With productInput
                    ' .CssClass = "inputtext"
                    .Enabled = True
                End With
                With buttonCart
                    .ImageUrl = "/images/cart.jpg"
                    .Enabled = True
                End With

            End If
        End If

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

        Session("tiListItems") = getItems()
        validatePayment(CType(Session("tiListItems"), B2P.Payment.PaymentBase.TransactionItems), "calc")


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
            Response.Redirect("/payment/newCounter.aspx")
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
                    If ckUseAddress.Checked Then
                        If RadioButtonList1.SelectedValue = "CC" Then
                            tbFirstName.Text = HttpUtility.HtmlEncode(y.Demographics.FirstName.Value)
                            tbLastName.Text = HttpUtility.HtmlEncode(y.Demographics.LastName.Value)
                        Else
                            tbBankAccountFirstName.Text = HttpUtility.HtmlEncode(y.Demographics.FirstName.Value)
                            tbBankAccountLastName.Text = HttpUtility.HtmlEncode(y.Demographics.LastName.Value)
                        End If

                        txtAddress.Text = HttpUtility.HtmlEncode(y.Demographics.Address1.Value)
                        txtAddress2.Text = HttpUtility.HtmlEncode(y.Demographics.Address2.Value)
                        txtCity.Text = HttpUtility.HtmlEncode(y.Demographics.City.Value)
                        ddlState.SelectedValue = HttpUtility.HtmlEncode(y.Demographics.State.Value)
                        txtZip.Text = HttpUtility.HtmlEncode(y.Demographics.ZipCode.Value)
                        tbHomePhone.Text = HttpUtility.HtmlEncode(y.Demographics.HomePhone.Value)

                        tbEmailAddress.Text = HttpUtility.HtmlEncode(y.Demographics.EmailAddress.Value)
                    End If
                    buttonCart.Enabled = True
                    buttonCart.ImageUrl = "/images/cart.jpg"
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
                        tbBankAccountFirstName.Text = HttpUtility.HtmlEncode(y.Demographics.FirstName.Value)
                        tbBankAccountLastName.Text = HttpUtility.HtmlEncode(y.Demographics.LastName.Value)
                    End If

                    txtAddress.Text = HttpUtility.HtmlEncode(y.Demographics.Address1.Value)
                    txtAddress2.Text = HttpUtility.HtmlEncode(y.Demographics.Address2.Value)
                    txtCity.Text = HttpUtility.HtmlEncode(y.Demographics.City.Value)
                    ddlState.SelectedValue = HttpUtility.HtmlEncode(y.Demographics.State.Value)
                    txtZip.Text = HttpUtility.HtmlEncode(y.Demographics.ZipCode.Value)
                    tbHomePhone.Text = HttpUtility.HtmlEncode(y.Demographics.HomePhone.Value)

                    tbEmailAddress.Text = HttpUtility.HtmlEncode(y.Demographics.EmailAddress.Value)
                End If
                buttonCart.Enabled = True
                buttonCart.ImageUrl = "/images/cart.jpg"
            End If
        Else
            If ckUseAddress.Checked Then
                If RadioButtonList1.SelectedValue = "CC" Then
                    tbFirstName.Text = HttpUtility.HtmlEncode(y.Demographics.FirstName.Value)
                    tbLastName.Text = HttpUtility.HtmlEncode(y.Demographics.LastName.Value)
                Else
                    tbBankAccountFirstName.Text = HttpUtility.HtmlEncode(y.Demographics.FirstName.Value)
                    tbBankAccountLastName.Text = HttpUtility.HtmlEncode(y.Demographics.LastName.Value)
                End If

                txtAddress.Text = HttpUtility.HtmlEncode(y.Demographics.Address1.Value)
                txtAddress2.Text = HttpUtility.HtmlEncode(y.Demographics.Address2.Value)
                txtCity.Text = HttpUtility.HtmlEncode(y.Demographics.City.Value)
                ddlState.SelectedValue = HttpUtility.HtmlEncode(y.Demographics.State.Value)
                txtZip.Text = HttpUtility.HtmlEncode(y.Demographics.ZipCode.Value)
                tbHomePhone.Text = HttpUtility.HtmlEncode(y.Demographics.HomePhone.Value)

                tbEmailAddress.Text = HttpUtility.HtmlEncode(y.Demographics.EmailAddress.Value)
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

  
    Protected Sub ckValidator_ServerValidate(source As Object, args As ServerValidateEventArgs) Handles ckValidator.ServerValidate
        If ckNacha.Checked Then
            args.IsValid = True
        Else
            args.IsValid = False
        End If
    End Sub
End Class