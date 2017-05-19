Imports System.Configuration.ConfigurationManager
Imports Telerik.Web.UI

Public Class _default5
    Inherits baseclass

    Dim UserSecurity As B2PAdminBLL.Role
    Dim x As New B2PAdminBLL.Security(AppSettings("ConnectionString"))
    Dim ds As New DataSet

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
        UserSecurity = CType(Session("RoleInfo"), B2PAdminBLL.Role)
        PeterBlum.DES.Globals.WebFormDirector.Browser.SupportsFilterStyles = False
        If Not UserSecurity.SearchScreen Then
            Response.Redirect("/error/default.aspx?type=security")
        End If
        If Not IsPostBack Then
            'Session("SearchCriteria") = Nothing
            loadData()
            fillForm()
        End If
    End Sub

    Private Sub CheckSession()
        If Session("SessionID") Is Nothing Then
            Response.Redirect("/welcome")
        End If
    End Sub

    Private Sub loadData()
        ddlPaymentType.DataSourceID = "xmlPaymentType"
        ddlPaymentType.DataBind()
        ddlPaymentType.SortItems()

        ddlSource.DataSourceID = "xmlSource"
        ddlSource.DataBind()
        ddlSource.SortItems()

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

        Dim ProductList As New List(Of String)
        ProductList.Add("All")
        ProductList.AddRange(B2P.Objects.Product.ListProducts(CStr(Session("ClientCode")), B2P.Common.Enumerations.TransactionSources.NotSpecified))
        ddlProduct.DataSource = ProductList
        ddlProduct.DataBind()

    End Sub

    Private Sub fillForm()
        If Not IsNothing(Session("SearchCriteria")) Then
            Dim savedSearch As B2PAdminBLL.TransactionSearch.SearchCriteria = CType(Session("SearchCriteria"), B2PAdminBLL.TransactionSearch.SearchCriteria)

            If Not IsNothing(savedSearch.AccountNumber) Then
                txtAccountNumber.Text = HttpUtility.HtmlEncode(savedSearch.AccountNumber)
            End If
            If Not IsNothing(savedSearch.Amount) Then
                txtPaymentAmount.Text = HttpUtility.HtmlEncode(CStr(savedSearch.Amount))
            End If
            If Not IsNothing(savedSearch.BankAccountLast4) Then
                txtECheck.Text = HttpUtility.HtmlEncode(savedSearch.BankAccountLast4)
            End If
            If savedSearch.Confirmation <> 0 Then
                txtConfirmation.Text = HttpUtility.HtmlEncode(CStr(savedSearch.Confirmation))
            End If
            If Not IsNothing(savedSearch.CreditCardFirst6) Then
                txtCCFirst6.Text = HttpUtility.HtmlEncode(savedSearch.CreditCardFirst6)
            End If
            If Not IsNothing(savedSearch.CreditCardLast4) Then
                txtCCLast4.Text = HttpUtility.HtmlEncode(savedSearch.CreditCardLast4)
            End If
            If Not IsNothing(savedSearch.DateRangeFrom) Then
                txtStart.Text = HttpUtility.HtmlEncode(CStr(savedSearch.DateRangeFrom))
            End If
            If Not IsNothing(savedSearch.DateRangeTo) Then
                txtEnd.Text = HttpUtility.HtmlEncode(CStr(savedSearch.DateRangeTo))
            End If
            If Not IsNothing(savedSearch.FirstName) Then
                txtFirstName.Text = HttpUtility.HtmlEncode(savedSearch.FirstName)
            End If
            If Not IsNothing(savedSearch.LastName) Then
                txtLastName.Text = HttpUtility.HtmlEncode(savedSearch.LastName)
            End If
            If Not IsNothing(savedSearch.TotalAmount) Then
                txtTotalAmount.Text = HttpUtility.HtmlEncode(CStr(savedSearch.TotalAmount))
            End If
            If savedSearch.Source <> 0 Then
                ddlSource.SelectedValue = HttpUtility.HtmlEncode(CStr(savedSearch.Source))
            End If
            If Not IsNothing(savedSearch.PaymentType) Then
                ddlPaymentType.SelectedValue = HttpUtility.HtmlEncode(savedSearch.PaymentType)
            End If
        End If

    End Sub

    Private Sub checkForm()
        Dim dateRequired As Boolean = True
        Dim hasData As Boolean = False
        Dim hasValidDate As Boolean

        If txtStart.Text <> "" And txtEnd.Text <> "" Then
            hasValidDate = True
        Else
            hasValidDate = False
        End If

        If txtConfirmation.Text <> "" Then
            dateRequired = False
            hasData = True
        End If

        If txtAccountNumber.Text <> "" Then
            dateRequired = False
            hasData = True
        End If

        If txtCCFirst6.Text <> "" Then
            dateRequired = False
            hasData = True
        End If

        If txtCCLast4.Text <> "" Then
            dateRequired = False
            hasData = True
        End If

        If txtFirstName.Text <> "" Then
            dateRequired = False
            hasData = True
        End If

        If txtLastName.Text <> "" Then
            dateRequired = False
            hasData = True
        End If

        If txtECheck.Text <> "" Then
            dateRequired = False
            hasData = True
        End If

        If txtPaymentAmount.Text <> "" Then
            hasData = True
        End If

        If txtTotalAmount.Text <> "" Then
            hasData = True
        End If

        If ddlSource.SelectedValue <> "" Then
            hasData = True
        End If

        If ddlPaymentType.SelectedValue <> "AT" Then
            hasData = True
        End If

        If ddlProduct.SelectedValue <> "All" Then
            hasData = True
        End If


        If hasValidDate = True And hasData = False Then
            lblMessage.Text = "Please enter another search parameter."

            'RadGrid1.DataSource = Nothing
            'RadGrid1.DataBind()
        ElseIf dateRequired And hasValidDate = False Then
            lblMessage.Text = "A valid date range is required."

            'RadGrid1.DataSource = Nothing
            'RadGrid1.DataBind()
        Else
            lblMessage.Text = ""
            loadDataSet()

        End If
    End Sub

    Private Sub loadDataSet()
        Try

            Dim Criteria As New B2PAdminBLL.TransactionSearch.SearchCriteria
            Dim y As New B2PAdminBLL.TransactionSearch()

            If Not IsNothing(Session("SearchCriteria")) Then
                Dim savedSearch As B2PAdminBLL.TransactionSearch.SearchCriteria = CType(Session("SearchCriteria"), B2PAdminBLL.TransactionSearch.SearchCriteria)
                ds = y.SearchTransactions(savedSearch)
            Else
                Criteria.UserName = CStr(Session("AdminLoginID"))
                If CStr(Session("ClientCode")) = "B2P" Then
                    If ddlAUClient.SelectedItem.Text = "All Clients" Then
                        Criteria.ClientCode = "B2P"
                    Else
                        Criteria.ClientCode = HttpUtility.HtmlEncode(ddlAUClient.SelectedItem.Text)
                    End If

                Else
                    Criteria.ClientCode = CStr(Session("ClientCode"))
                End If

                'lblMessage.Text = ""
                If txtStart.Text <> "" Then
                    Criteria.DateRangeFrom = CType(HttpUtility.HtmlEncode(txtStart.Text), Date?)
                End If
                If txtEnd.Text <> "" Then
                    Criteria.DateRangeTo = CType(HttpUtility.HtmlEncode(txtEnd.Text), Date?)
                End If

                Criteria.AccountNumber = HttpUtility.HtmlEncode(txtAccountNumber.Text)
                If txtPaymentAmount.Text <> "" Then
                    Criteria.Amount = CType(HttpUtility.HtmlEncode(txtPaymentAmount.Text), Double?)
                End If

                Criteria.BankAccountLast4 = HttpUtility.HtmlEncode(txtECheck.Text)
                If txtConfirmation.Text <> "" Then
                    Criteria.Confirmation = CInt(HttpUtility.HtmlEncode(txtConfirmation.Text))
                End If

                Criteria.CreditCardFirst6 = HttpUtility.HtmlEncode(txtCCFirst6.Text)
                Criteria.CreditCardLast4 = HttpUtility.HtmlEncode(txtCCLast4.Text)
                Criteria.FirstName = HttpUtility.HtmlEncode(txtFirstName.Text)
                Criteria.LastName = HttpUtility.HtmlEncode(txtLastName.Text)
                Criteria.PaymentType = HttpUtility.HtmlEncode(ddlPaymentType.SelectedValue)
                If ddlSource.SelectedValue <> "" Then
                    Criteria.Source = CInt(HttpUtility.HtmlEncode(ddlSource.SelectedValue))
                End If

                If txtTotalAmount.Text <> "" Then
                    Criteria.TotalAmount = CType(HttpUtility.HtmlEncode(txtTotalAmount.Text), Double?)
                End If
                If ddlProduct.SelectedItem.Text <> "All" Then
                    Criteria.ProductName = HttpUtility.HtmlEncode(ddlProduct.SelectedItem.Text).Replace("&amp;", "&")
                End If
                Criteria.NotesOnly = ckNotes.Checked

                Session("SearchCriteria") = Criteria
                ds = y.SearchTransactions(Criteria)
            End If

            RadGrid1.DataSource = ds


            If ds.Tables(0).Rows.Count > 2000 Then
                lblMessage.Text = "Your search returned more than 2,000 transactions. Please narrow your search criteria."
            Else
                lblMessage.Text = ""
            End If
        Catch ex As Exception
            Session("SearchCriteria") = Nothing
            lblMessage.Text = "An unexpected error has occurred. Please review the search parameters and try again."
        End Try
    End Sub

#Region " Form Events "
    Private Sub ddlAUClient_SelectedIndexChanged(ByVal o As Object, ByVal e As Global.Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles ddlAUClient.SelectedIndexChanged
        Dim ProductList As New List(Of String)
        ProductList.Add("All")
        If ddlAUClient.SelectedValue = "B2P" Then
            ProductList.AddRange(B2P.Objects.Product.ListProducts("B2P", B2P.Common.Enumerations.TransactionSources.NotSpecified))
        Else
            ProductList.AddRange(B2P.Objects.Product.ListProducts(HttpUtility.HtmlEncode(ddlAUClient.SelectedItem.Text), B2P.Common.Enumerations.TransactionSources.NotSpecified))
        End If

        ddlProduct.DataSource = ProductList
        ddlProduct.DataBind()
    End Sub

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSearch.Click
        Session("SearchCriteria") = Nothing
        checkForm()
        RadGrid1.DataBind()
    End Sub

    Private Sub btnClear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClear.Click
        Session("SearchCriteria") = Nothing
        Response.Redirect("/search/")
    End Sub

#End Region

#Region " RadGrid Events "
    Private Sub RadGrid1_ItemCommand(ByVal source As Object, ByVal e As Global.Telerik.Web.UI.GridCommandEventArgs) Handles RadGrid1.ItemCommand
        Select Case e.CommandName
            Case "viewTransaction"
                Session("ConfirmationNumber") = Trim(e.Item.Cells(3).Text)
                If CStr(Session("ClientCode")) = "B2P" Then
                    If ddlAUClient.SelectedItem.Text = "All Clients" Then
                        Session("UserClientCode") = "B2P"
                    Else
                        Session("UserClientCode") = HttpUtility.HtmlEncode(ddlAUClient.SelectedItem.Text)
                    End If
                Else
                    Session("UserClientCode") = CStr(Session("ClientCode"))
                End If
                Response.Redirect("/search/detail.aspx")
            Case Else

        End Select
    End Sub

    Private Sub RadGrid1_NeedDataSource(ByVal source As Object, ByVal e As Global.Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles RadGrid1.NeedDataSource
        If Not IsNothing(Session("SearchCriteria")) Then
            loadDataSet()
            'Else
            '    checkForm()
        End If
    End Sub

    Protected Sub RadGrid1_ItemCreated(ByVal sender As Object, ByVal e As GridItemEventArgs)

        If TypeOf e.Item Is GridCommandItem Then
            Dim btncmd As Button = TryCast(TryCast(e.Item, GridCommandItem).FindControl("ExportToExcelButton"), Button)
            ScriptManager1.RegisterPostBackControl(btncmd)
            btncmd = TryCast(TryCast(e.Item, GridCommandItem).FindControl("ExportToCSVButton"), Button)
            ScriptManager1.RegisterPostBackControl(btncmd)
            'btncmd = TryCast(TryCast(e.Item, GridCommandItem).FindControl("ExportToPDFButton"), Button)
            'ScriptManager1.RegisterPostBackControl(btncmd)
        End If
    End Sub

#End Region
End Class