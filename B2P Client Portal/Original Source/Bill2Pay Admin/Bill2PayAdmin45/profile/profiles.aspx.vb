Imports System.Configuration.ConfigurationManager
Imports Telerik.Web.UI
Imports System.Threading

Partial Public Class Profiles1
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
            Response.Redirect("/error/default.aspx?type=security")
        End If
        If Not IsPostBack Then
            LoadData()
            FillForm()
        End If
    End Sub
#End Region

#Region " Control Events "
    Private Sub ddlAUClient_SelectedIndexChanged(ByVal sender As Object, ByVal e As Global.Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles ddlAUClient.SelectedIndexChanged
        Session("ProfileSearch") = Nothing
        RadGrid1.Rebind()
    End Sub

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnSearch.Click
        Try
            Session("ProfileSearch") = Nothing
            CheckForm()
            'RadGrid1.Rebind()
            RadGrid1.DataBind()
        Catch te As ThreadAbortException

        Catch ex As Exception
            B2P.Common.Logging.LogError("B2P Admin", "Error during loading profiles " & ex.ToString, B2P.Common.Logging.NotifySupport.No)
            Response.Redirect("/error/")
            HttpContext.Current.ApplicationInstance.CompleteRequest()
        End Try
    End Sub

    Private Sub btnClear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClear.Click
        Session("ProfileSearch") = Nothing
        Response.Redirect("/profile/profiles.aspx")
    End Sub
#End Region

#Region " Grid Events "
    Private Sub RadGrid1_CustomAggregate(ByVal sender As Object, ByVal e As Global.Telerik.Web.UI.GridCustomAggregateEventArgs) Handles RadGrid1.CustomAggregate

        e.Result = ViewState("distinctCount").ToString

    End Sub

    Private Sub RadGrid1_ItemCommand(ByVal source As Object, ByVal e As Global.Telerik.Web.UI.GridCommandEventArgs) Handles RadGrid1.ItemCommand
        If TypeOf e.Item Is GridDataItem Then
            Dim dataItem As GridDataItem = CType(e.Item, GridDataItem)

            Session("Profile_ID") = Trim(dataItem("column1a").Text)
            If CStr(Session("ClientCode")) = "B2P" Then
                If ddlAUClient.SelectedItem.Text = "All Clients" Then
                    Session("UserClientCode") = "B2P"
                Else
                    Session("UserClientCode") = ddlAUClient.SelectedItem.Text
                End If
            Else
                Session("UserClientCode") = CStr(Session("ClientCode"))
            End If

            Select Case e.CommandName
                Case "viewProfileDetail"
                    Response.Redirect("/profile/profiledetail.aspx?source=profile")
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

        'Dim x As B2P.Profile.User

        'If TypeOf e.Item Is GridDataItem Then
        'Dim dataItem As GridDataItem = CType(e.Item, GridDataItem)

        'x = New B2P.Profile.User
        'Dim s As String = dataItem("column1a").Text
        'x = B2P.Profile.ProfileManagement.GetUserByProfile_ID(Convert.ToInt32(dataItem("column1a").Text))

        'dataItem("column7").Text = x.Status.ToString
        'dataItem("column1a").Visible = False  'hide the Profile_ID column

        'End If
    End Sub

    Private Sub RadGrid1_NeedDataSource(ByVal source As Object, ByVal e As Global.Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles RadGrid1.NeedDataSource
        If Not IsNothing(Session("ProfileSearch")) Then
            LoadDataSet()
            'Else
            '    CheckForm()
        End If
    End Sub
#End Region

#Region " Custom Methods "
    Private Sub CheckForm()
        Dim hasData As Boolean = False
        If ckAllProfiles.Checked = False Then

            If txtAccountNumber.Text <> "" Then hasData = True
            If txtUserName.Text <> "" Then hasData = True
            If txtEmailAddress.Text <> "" Then hasData = True
            If txtLastName.Text <> "" Then hasData = True
            If txtCCLast4.Text <> "" Then hasData = True
            If txtECheck.Text <> "" Then hasData = True

            If hasData = False Then
                lblMessage.Text = "You must enter data into at least one parameter - select the checkbox to display all profiles."
            Else
                lblMessage.Text = ""
                LoadDataSet()
            End If
        Else
            LoadDataSet()
        End If
    End Sub

    Private Sub CheckSession()
        If Session("SessionID") Is Nothing Then
            Response.Redirect("/welcome")
        End If
    End Sub

    Private Sub FillForm()
        Try

            If Not IsNothing(Session("ProfileSearch")) Then
                Dim savedSearch As B2P.Admin.Profile.ProfileSearch.SearchCriteria
                'Dim savedSearch As B2PAdminBLL.ProfileSearch.SearchCriteria
                savedSearch = CType(Session("ProfileSearch"), B2P.Admin.Profile.ProfileSearch.SearchCriteria)

                If Not IsNothing(savedSearch.UserName) Then
                    txtUserName.Text = HttpUtility.HtmlEncode(savedSearch.UserName)
                End If
                If Not IsNothing(savedSearch.LastName) Then
                    txtLastName.Text = HttpUtility.HtmlEncode(savedSearch.LastName)
                End If
                If Not IsNothing(savedSearch.EmailAddress) Then
                    txtEmailAddress.Text = HttpUtility.HtmlEncode(savedSearch.EmailAddress)
                End If

                If Not IsNothing(savedSearch.AccountNumber) Then
                    txtAccountNumber.Text = HttpUtility.HtmlEncode(savedSearch.AccountNumber)
                End If
                If Not IsNothing(savedSearch.CreditCardLast4) Then
                    txtCCLast4.Text = HttpUtility.HtmlEncode(savedSearch.CreditCardLast4)
                End If
                If Not IsNothing(savedSearch.BankAccountLast4) Then
                    txtECheck.Text = HttpUtility.HtmlEncode(savedSearch.BankAccountLast4)
                End If

            End If
        Catch te As ThreadAbortException

        Catch ex As Exception
            B2P.Common.Logging.LogError("B2P Admin", "Error during filling form for profiles " & ex.ToString, B2P.Common.Logging.NotifySupport.No)
            Response.Redirect("/error/")
            HttpContext.Current.ApplicationInstance.CompleteRequest()
        End Try
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
            B2P.Common.Logging.LogError("B2P Admin", "Error during loading profiles " & ex.ToString, B2P.Common.Logging.NotifySupport.No)
            Response.Redirect("/error/")
            HttpContext.Current.ApplicationInstance.CompleteRequest()
        End Try
    End Sub

    Private Sub LoadDataSet()
        Try

            Dim Criteria As New B2P.Admin.Profile.ProfileSearch.SearchCriteria
            Dim y As New B2P.Admin.Profile.ProfileSearch()

            If Not IsNothing(Session("ProfileSearch")) Then
                Dim savedSearch As B2P.Admin.Profile.ProfileSearch.SearchCriteria
                savedSearch = CType(Session("ProfileSearch"), B2P.Admin.Profile.ProfileSearch.SearchCriteria)
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
                    .UserName = Utility.SafeEncode(txtUserName.Text.Trim)
                    .LastName = Utility.SafeEncode(txtLastName.Text.Trim)
                    .EmailAddress = Utility.SafeEncode(txtEmailAddress.Text.Trim)
                    .AccountNumber = Utility.SafeEncode(txtAccountNumber.Text.Trim)
                    .CreditCardLast4 = Utility.SafeEncode(txtCCLast4.Text.Trim)
                    .BankAccountLast4 = Utility.SafeEncode(txtECheck.Text.Trim)
                End With

                Session("ProfileSearch") = Criteria
                ds = y.SearchProfiles(Criteria)
            End If

            RadGrid1.DataSource = ds

            ViewState("distinctCount") = ds.Tables(0).DefaultView.ToTable(True, "username").Rows.Count

            'If ds.Tables(0).Rows.Count > 2000 Then
            '    lblMessage.Text = "Your search returned more than 2,000 profiles. Please narrow your search criteria."
            'Else
            lblMessage.Text = ""
            If Session("ClientCode").ToString <> "B2P" Then
                RadGrid1.Columns(2).Visible = False
            End If
            'End If
        Catch te As ThreadAbortException

        Catch ex As Exception
            B2P.Common.Logging.LogError("B2P Admin", "Error during loading profiles " & ex.ToString, B2P.Common.Logging.NotifySupport.No)
            Response.Redirect("/error/")
            HttpContext.Current.ApplicationInstance.CompleteRequest()
        End Try
    End Sub

   
#End Region

    
End Class