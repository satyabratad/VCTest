Imports System.Configuration.ConfigurationManager
Imports Telerik.Web.UI

Partial Public Class textpay
    Inherits baseclass

    Dim UserSecurity As B2PAdminBLL.Role
    Dim x As New B2PAdminBLL.Security(AppSettings("ConnectionString"))
    Dim ds As New DataSet

#Region " Page Events "
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

        If Not UserSecurity.SearchScreen Then
            Response.Redirect("/error/security")
        End If
        If Not IsPostBack Then
            LoadData()
            FillForm()
        End If
    End Sub
#End Region

#Region " Control Events "
    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnSearch.Click
        Session("TextSearch") = Nothing
        checkForm()
        RadGrid1.DataBind()
    End Sub

    Private Sub btnClear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClear.Click
        Session("TextSearch") = Nothing
        Response.Redirect("/textpay/textpay.aspx")
    End Sub

    Private Sub RadGrid1_ItemCommand(ByVal source As Object, ByVal e As Global.Telerik.Web.UI.GridCommandEventArgs) Handles RadGrid1.ItemCommand
        If TypeOf e.Item Is GridDataItem Then
            Dim dataItem As GridDataItem = CType(e.Item, GridDataItem)

            Select Case e.CommandName
                Case "viewProfileDetail"
                    Session("Profile_ID") = Trim(dataItem("column2").Text)
                    If CStr(Session("ClientCode")) = "B2P" Then
                        If ddlAUClient.SelectedItem.Text = "All Clients" Then
                            Session("UserClientCode") = "B2P"
                        Else
                            Session("UserClientCode") = ddlAUClient.SelectedItem.Text
                        End If
                    Else
                        Session("UserClientCode") = CStr(Session("ClientCode"))
                    End If
                    Response.Redirect("textpaydetail.aspx")
                Case Else

            End Select
        End If

    End Sub

    Protected Sub RadGrid1_ItemCreated(ByVal sender As Object, ByVal e As GridItemEventArgs) Handles RadGrid1.ItemCreated

        If TypeOf e.Item Is GridCommandItem Then
            Dim btncmd As Button
            btncmd = TryCast(TryCast(e.Item, GridCommandItem).FindControl("ExportToExcelButton"), Button)
            ScriptManager1.RegisterPostBackControl(btncmd)
            btncmd = TryCast(TryCast(e.Item, GridCommandItem).FindControl("ExportToCSVButton"), Button)
            ScriptManager1.RegisterPostBackControl(btncmd)
        End If
    End Sub

    Private Sub RadGrid1_ItemDataBound(ByVal sender As Object, ByVal e As GridItemEventArgs) Handles RadGrid1.ItemDataBound
        Dim x As B2P.Customer.User

        If TypeOf e.Item Is GridDataItem Then
            Dim dataItem As GridDataItem = CType(e.Item, GridDataItem)

            x = New B2P.Customer.User
            x = B2P.Customer.ProfileManagement.GetUserByProfile_ID(Convert.ToInt32(dataItem("column2").Text))

            dataItem("column7").Text = x.Status.ToString

        End If
    End Sub

    Private Sub RadGrid1_NeedDataSource(ByVal source As Object, ByVal e As Global.Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles RadGrid1.NeedDataSource
        If Not IsNothing(Session("TextSearch")) Then
            LoadDataSet()
        Else
            CheckForm()
        End If
    End Sub
#End Region

#Region " Custom Methods "
    Private Sub CheckForm()
        Dim hasData As Boolean = False

        If txtAccountNumber.Text <> "" Then hasData = True
        If txtUserName.Text <> "" Then hasData = True
        If txtEmailAddress.Text <> "" Then hasData = True
        If txtPhoneNumber.Text <> "" Then hasData = True

        If hasData = False Then
            lblMessage.Text = "You must enter data into at least one parameter."
        Else
            lblMessage.Text = ""
            LoadDataSet()
        End If
    End Sub

    Private Sub CheckSession()
        If Session("SessionID") Is Nothing Then
            Response.Redirect("/welcome")
        End If
    End Sub

    Private Sub FillForm()
        If Not IsNothing(Session("TextSearch")) Then
            Dim savedSearch As B2PAdminBLL.ProfileSearch.SearchCriteria
            savedSearch = CType(Session("TextSearch"), B2PAdminBLL.ProfileSearch.SearchCriteria)

            If Not IsNothing(savedSearch.UserName) Then
                txtUserName.Text = savedSearch.UserName
            End If
            If Not IsNothing(savedSearch.EmailAddress) Then
                txtEmailAddress.Text = savedSearch.EmailAddress
            End If
            If Not IsNothing(savedSearch.AccountNumber) Then
                txtAccountNumber.Text = savedSearch.AccountNumber
            End If
            If Not IsNothing(savedSearch.PhoneNumber) Then
                txtPhoneNumber.Text = Mid(savedSearch.PhoneNumber, 2)
            End If

        End If

    End Sub

    Private Sub LoadData()
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
    End Sub

    Private Sub LoadDataSet()
        Try

            Dim Criteria As New B2PAdminBLL.ProfileSearch.SearchCriteria
            Dim y As New B2PAdminBLL.ProfileSearch()

            If Not IsNothing(Session("TextSearch")) Then
                Dim savedSearch As B2PAdminBLL.ProfileSearch.SearchCriteria
                savedSearch = CType(Session("TextSearch"), B2PAdminBLL.ProfileSearch.SearchCriteria)
                ds = y.SearchProfiles(savedSearch)
            Else
                Criteria.UserName = CStr(Session("AdminLoginID"))
                If CStr(Session("ClientCode")) = "B2P" Then
                    If ddlAUClient.SelectedItem.Text = "All Clients" Then
                        Criteria.ClientCode = "B2P"
                    Else
                        Criteria.ClientCode = ddlAUClient.SelectedItem.Text
                    End If
                Else
                    Criteria.ClientCode = CStr(Session("ClientCode"))
                End If
                With Criteria
                    .AccountNumber = HttpUtility.HtmlEncode(txtAccountNumber.Text.Trim)
                    .UserName = HttpUtility.HtmlEncode(txtUserName.Text.Trim)
                    .EmailAddress = HttpUtility.HtmlEncode(txtEmailAddress.Text.Trim)
                    .PhoneNumber = "1" & HttpUtility.HtmlEncode(txtPhoneNumber.Text.Trim)

                End With
                Session("TextSearch") = Criteria
                ds = y.SearchProfiles(Criteria)
            End If

            RadGrid1.DataSource = ds


            If ds.Tables(0).Rows.Count > 2000 Then
                lblMessage.Text = "Your search returned more than 2,000 profiles. Please narrow your search criteria."
            Else
                lblMessage.Text = ""
            End If
        Catch ex As Exception
            Session("TextSearch") = Nothing
            lblMessage.Text = "An unexpected error has occurred. Please review the search parameters and try again."
        End Try
    End Sub
#End Region

End Class