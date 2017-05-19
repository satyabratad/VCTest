Imports System.Configuration.ConfigurationManager
Imports Telerik.Web.UI
Imports System.Threading
Imports System.Drawing
Imports B2P.Common
Imports System.Xml

Public Class SearchBlock
    Inherits baseclass

    Dim UserSecurity As B2PAdminBLL.Role
    Dim x As New B2PAdminBLL.Security(AppSettings("ConnectionString"))
    Dim ba As New B2PAdminBLL.BlockAccount.Account
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
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        UserSecurity = CType(Session("RoleInfo"), B2PAdminBLL.Role)

        If Not UserSecurity.BlockAccount Then
            Response.Redirect("/error/default.aspx?type=security")
        End If
        If Not IsPostBack Then
            'Session("BlockCriteria") = Nothing
            LoadData()
            loadProducts()
            FillForm()
        End If

    End Sub

    Private Sub CheckSession()
        If Session("SessionID") Is Nothing Then
            Response.Redirect("/welcome")
        End If
    End Sub

    Private Sub LoadData()
        Try
            ddlStatus.DataSourceID = "xmlStatus"
            ddlStatus.DataBind()
            ddlStatus.SortItems()

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

    Private Sub FillForm()
        Try

            If Not IsNothing(Session("BlockCriteria")) Then
                Dim savedSearch As B2PAdminBLL.BlockAccount.SearchCriteria = CType(Session("BlockCriteria"), B2PAdminBLL.BlockAccount.SearchCriteria)

                If Not IsNothing(savedSearch.ClientCode) Then
                    ddlAUClient.SelectedValue = HttpUtility.HtmlEncode(savedSearch.ClientCode)
                End If
                If Not IsNothing(savedSearch.ProductName) Then
                    ddlProductList.SelectedValue = HttpUtility.HtmlEncode(savedSearch.ProductName)
                End If
                If Not IsNothing(savedSearch.AccountNumber1) Then
                    txtProduct1Input.Text = HttpUtility.HtmlEncode(savedSearch.AccountNumber1)
                End If
                If Not IsNothing(savedSearch.AccountNumber2) Then
                    txtProduct1aInput.Text = HttpUtility.HtmlEncode(savedSearch.AccountNumber2)
                End If
                If Not IsNothing(savedSearch.AccountNumber3) Then
                    txtProduct1bInput.Text = HttpUtility.HtmlEncode(savedSearch.AccountNumber3)
                End If
                If Not IsNothing(savedSearch.BlockStatus) Then
                    ddlStatus.SelectedValue = HttpUtility.HtmlEncode(savedSearch.BlockStatus)
                End If
                If Not IsNothing(savedSearch.DateRangeFrom) Then
                    txtStart.Text = HttpUtility.HtmlEncode(savedSearch.DateRangeFrom)
                End If
                If Not IsNothing(savedSearch.DateRangeTo) Then
                    txtEnd.Text = HttpUtility.HtmlEncode(savedSearch.DateRangeTo)
                End If

            End If
        Catch te As ThreadAbortException

        Catch ex As Exception
            B2P.Common.Logging.LogError("B2P Admin", "Error during filling form for search Block " & ex.ToString, B2P.Common.Logging.NotifySupport.No)
            Response.Redirect("/error/")
            HttpContext.Current.ApplicationInstance.CompleteRequest()
        End Try
    End Sub

    'Private Sub ClearScreen()

    '    'pnlHeaders.Visible = False
    '    'pnlAccount1.Visible = False
    '    'pnlAccount1r.Visible = False
    '    'pnlAccount2.Visible = False
    '    'pnlAccount2r.Visible = False
    '    'pnlAccount3.Visible = False
    '    'pnlAccount3r.Visible = False
    '    'pnlReEnterAccount.Visible = False
    '    'pnlLookupButton.Visible = False
    '    'pnlButton.Visible = False
    '    'pnlClear.Visible = False

    'End Sub

    Private Sub loadProducts()
        Try
            ddlProductList.Items.Clear()
            ddlProductList.Items.Add(New RadComboBoxItem("All Products", ""))
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

    Private Sub LoadDataSet()
        Try
            Dim Criteria As New B2PAdminBLL.BlockAccount.SearchCriteria
            Dim y As New B2PAdminBLL.BlockAccount

            If Not IsNothing(Session("BlockCriteria")) Then
                Dim savedSearch As B2PAdminBLL.BlockAccount.SearchCriteria = CType(Session("BlockCriteria"), B2PAdminBLL.BlockAccount.SearchCriteria)
                ds = B2PAdminBLL.BlockAccount.SearchBlockedAccounts(savedSearch)
            Else
                If CStr(Session("ClientCode")) = "B2P" Then
                    If ddlAUClient.SelectedItem.Text = "All Clients" Then
                        Criteria.ClientCode = "B2P"
                    Else
                        Criteria.ClientCode = HttpUtility.HtmlEncode(ddlAUClient.SelectedItem.Text)
                    End If
                Else
                    Criteria.ClientCode = CStr(Session("ClientCode"))
                End If

                If txtStart.Text <> "" Then
                    Criteria.DateRangeFrom = CType(HttpUtility.HtmlEncode(txtStart.Text), Date?)
                End If
                If txtEnd.Text <> "" Then
                    Criteria.DateRangeTo = CType(HttpUtility.HtmlEncode(txtEnd.Text), Date?)
                End If

                If ddlProductList.SelectedItem.Text = "All Products" Then
                    Criteria.ProductName = Nothing
                Else
                    Criteria.ProductName = HttpUtility.HtmlEncode(ddlProductList.SelectedItem.Text)
                End If

                If HttpUtility.HtmlEncode(txtProduct1Input.Text).Length > 0 Then
                    Criteria.AccountNumber1 = HttpUtility.HtmlEncode(txtProduct1Input.Text)
                Else
                    Criteria.AccountNumber1 = Nothing
                End If
                If HttpUtility.HtmlEncode(txtProduct1aInput.Text).Length > 0 Then
                    Criteria.AccountNumber2 = HttpUtility.HtmlEncode(txtProduct1aInput.Text)
                Else
                    Criteria.AccountNumber2 = Nothing
                End If
                If HttpUtility.HtmlEncode(txtProduct1bInput.Text).Length > 0 Then
                    Criteria.AccountNumber3 = HttpUtility.HtmlEncode(txtProduct1bInput.Text)
                Else
                    Criteria.AccountNumber3 = Nothing
                End If
                Criteria.BlockStatus = HttpUtility.HtmlEncode(ddlStatus.SelectedItem.Text)

                Session("BlockCriteria") = Criteria
                ds = B2PAdminBLL.BlockAccount.SearchBlockedAccounts(Criteria)
            End If

            RadGrid1.DataSource = ds

        Catch ex As Exception
            Session("BlockCriteria") = Nothing
            lblMessage.Text = "An unexpected error has occurred. Please review the search parameters and try again."
        End Try
    End Sub

#Region "Events"
    Private Sub ddlAUClient_SelectedIndexChanged(ByVal sender As Object, ByVal e As Global.Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles ddlAUClient.SelectedIndexChanged
        Session("UserClientCode") = ddlAUClient.SelectedItem.Text
        Session("BlockCriteria") = Nothing
        RadGrid1.Rebind()
        'ClearScreen()
        loadProducts()
    End Sub

    Protected Sub ddlProductList_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlProductList.SelectedIndexChanged
        Try
            Dim product1Selected As String = Replace(HttpUtility.HtmlEncode(ddlProductList.SelectedValue), "&amp;", "&")

            loadProducts()

            If product1Selected <> "" Then
                ddlProductList.SelectedValue = product1Selected

                Dim z As New B2P.Objects.Product(CStr(Session("UserClientCode")), product1Selected, B2P.Common.Enumerations.TransactionSources.NotSpecified)
                If z.WebOptions.AccountIDField1.Enabled = True Then
                    TextBoxWatermarkExtender1.WatermarkText = z.WebOptions.AccountIDField1.Label
                    txtProduct1Input.MaxLength = z.WebOptions.AccountIDField1.MaximumLength
                End If

                With z.WebOptions
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

                'If z.IsLookupEnabled Then

                '    With z.WebOptions
                '        If .AccountIDField2.Enabled = True Then
                '            TextBoxWatermarkExtender11.WatermarkText = .AccountIDField2.Label
                '            txtProduct1aInput.Enabled = True
                '            txtProduct1aInput.ReadOnly = False
                '            txtProduct1aInput.MaxLength = .AccountIDField2.MaximumLength
                '        Else
                '            txtProduct1aInput.ReadOnly = True
                '            txtProduct1aInput.Enabled = False
                '            TextBoxWatermarkExtender11.WatermarkText = "Not required"
                '        End If

                '        If .AccountIDField3.Enabled = True Then
                '            TextBoxWatermarkExtender12.WatermarkText = .AccountIDField3.Label
                '            txtProduct1bInput.Enabled = True
                '            txtProduct1bInput.ReadOnly = False
                '            txtProduct1bInput.MaxLength = .AccountIDField3.MaximumLength
                '        Else
                '            txtProduct1bInput.ReadOnly = True
                '            txtProduct1bInput.Enabled = False
                '            TextBoxWatermarkExtender12.WatermarkText = "Not required"
                '        End If
                '    End With
                'Else
                '    TextBoxWatermarkExtender11.WatermarkText = "Account Number 2"
                '    TextBoxWatermarkExtender12.WatermarkText = "Account Number 3"
                '    txtProduct1aInput.Enabled = True
                '    txtProduct1aInput.ReadOnly = False
                '    txtProduct1bInput.Enabled = True
                '    txtProduct1bInput.ReadOnly = False
                'End If

                SetFocus(txtProduct1Input)
            Else
                TextBoxWatermarkExtender11.WatermarkText = "Account Number 2"
                TextBoxWatermarkExtender12.WatermarkText = "Account Number 3"
                txtProduct1aInput.Enabled = True
                txtProduct1aInput.ReadOnly = False
                txtProduct1bInput.Enabled = True
                txtProduct1bInput.ReadOnly = False

                SetFocus(txtProduct1Input)
            End If
        Catch ex As Exception
            'Response.Write(ex.Message)
            Response.Redirect("/error/default.aspx?type=system", False)
        End Try

    End Sub

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSearch.Click
        Session("BlockCriteria") = Nothing
        LoadDataSet()
        'RadGrid1.Rebind()
        RadGrid1.DataBind()
    End Sub

    Protected Sub btnClear_Click(sender As Object, e As ImageClickEventArgs) Handles btnClear.Click
        Session("BlockCriteria") = Nothing
        Response.Redirect("/BlockAccount/SearchBlock.aspx")
    End Sub
#End Region

#Region "Grid Events"
    Protected Sub RadGrid1_ItemCreated(ByVal sender As Object, ByVal e As GridItemEventArgs) Handles RadGrid1.ItemCreated

        If TypeOf e.Item Is GridCommandItem Then
            Dim btncmd As Button
            btncmd = TryCast(TryCast(e.Item, GridCommandItem).FindControl("ExportToExcelButton"), Button)
            ScriptManager1.RegisterPostBackControl(btncmd)
            btncmd = TryCast(TryCast(e.Item, GridCommandItem).FindControl("ExportToCSVButton"), Button)
            ScriptManager1.RegisterPostBackControl(btncmd)
        End If
    End Sub

    Private Sub RadGrid1_NeedDataSource(ByVal source As Object, ByVal e As Global.Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles RadGrid1.NeedDataSource
        If Not IsNothing(Session("BlockCriteria")) Then
            LoadDataSet()
        End If
    End Sub

    Private Sub RadGrid1_ItemCommand(ByVal source As Object, ByVal e As Global.Telerik.Web.UI.GridCommandEventArgs) Handles RadGrid1.ItemCommand
        If TypeOf e.Item Is GridDataItem Then
            Dim dataItem As GridDataItem = CType(e.Item, GridDataItem)

            Session("Block_ID") = Trim(dataItem("column12").Text)
            If CStr(Session("ClientCode")) = "B2P" Then
                If ddlAUClient.SelectedItem.Text = "All Clients" Then
                    Session("UserClientCode") = "B2P"
                Else
                    Session("UserClientCode") = HttpUtility.HtmlEncode(ddlAUClient.SelectedItem.Text)
                End If
            Else
                Session("UserClientCode") = CStr(Session("ClientCode"))
            End If

            Select Case e.CommandName
                Case "viewBlock"
                    Response.Redirect("/BlockAccount/BlockDetail.aspx?source=edit")
                Case "ReleaseBlock"
                    Response.Redirect("/BlockAccount/BlockDetail.aspx?source=unblock")
                Case Else
            End Select

        End If
    End Sub

#End Region
End Class