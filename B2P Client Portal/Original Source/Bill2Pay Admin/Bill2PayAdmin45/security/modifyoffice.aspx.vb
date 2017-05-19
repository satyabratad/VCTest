Imports System.Configuration.ConfigurationManager

Partial Public Class modifyoffice
    Inherits baseclass

    Dim x As New B2PAdminBLL.Security(AppSettings("ConnectionString"))
    Dim UserSecurity As B2PAdminBLL.Role

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        UserSecurity = CType(Session("RoleInfo"), B2PAdminBLL.Role)

        If Not UserSecurity.SecurityOfficeMgmt Then
            Response.Redirect("/error/default.aspx?type=security")
        End If

        If Not IsPostBack Then
            UserSecurity = CType(Session("RoleInfo"), B2PAdminBLL.Role)
            If CStr(Session("ClientCode")) = "B2P" Then
                pnlClientCode.Visible = True
                x.ClientCode = "B2P"
                x.Role = UserSecurity.UserRole

                Dim ds As DataSet = x.LoadDropDownBoxes
                ddlAUClient.DataSource = ds.Tables(0)
                ddlAUClient.DataTextField = ds.Tables(0).Columns("ClientCode").ColumnName.ToString()
                ddlAUClient.DataBind()
                If CStr(Session("UserClientCode")) <> "" Then
                    ddlAUClient.Items.FindItemByText(CStr(Session("UserClientCode"))).Selected = True
                Else
                    ddlAUClient.Items.FindItemByText("B2P").Selected = True
                End If

                'ddlAUClient.SelectedItem.Text = "B2P"
                If CStr(Session("UserClientCode")) <> "" Then
                    changeClient(CStr(Session("UserClientCode")))
                Else
                    changeClient("B2P")
                End If
            Else
                x.ClientCode = CStr(Session("ClientCode"))
                'x.Role = UserSecurity.UserRole
                x.UserLogin = CStr(Session("AdminLoginID"))

                RadGrid1.DataSource = x.ListOffices
                RadGrid1.DataBind()
                pnlClientCode.Visible = False
            End If

                x = Nothing
            End If


    End Sub

    Protected Sub ddlAUClient_SelectedIndexChanged(ByVal o As Object, ByVal e As Global.Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles ddlAUClient.SelectedIndexChanged
        changeClient(HttpUtility.HtmlEncode(ddlAUClient.SelectedItem.Text))
        RadGrid1.DataBind()

    End Sub
    Private Sub changeClient(ByVal clientCode As String)
        x.ClientCode = clientCode
        x.UserLogin = CStr(Session("AdminLoginID"))
        RadGrid1.DataSource = x.ListOffices

    End Sub

    Private Sub RadGrid1_ItemCommand(ByVal source As Object, ByVal e As Global.Telerik.Web.UI.GridCommandEventArgs) Handles RadGrid1.ItemCommand
        Select Case e.CommandName
            Case "viewOffice"
                Session("editOfficeID") = Trim(e.Item.Cells(3).Text)
                If CStr(Session("ClientCode")) = "B2P" Then
                    Session("UserClientCode") = HttpUtility.HtmlEncode(ddlAUClient.SelectedItem.Text)
                Else
                    Session("UserClientCode") = CStr(Session("ClientCode"))
                End If
                Response.Redirect("/security/editoffice.aspx")
            Case Else

        End Select


    End Sub

    Private Sub RadGrid1_NeedDataSource(ByVal source As Object, ByVal e As Global.Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles RadGrid1.NeedDataSource
        If CStr(Session("ClientCode")) = "B2P" Then
            changeClient(HttpUtility.HtmlEncode(ddlAUClient.SelectedItem.Text))
        Else
            changeClient(CStr(Session("ClientCode")))
        End If

    End Sub
End Class

