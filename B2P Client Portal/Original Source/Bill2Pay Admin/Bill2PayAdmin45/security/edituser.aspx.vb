Imports System.Configuration.ConfigurationManager
Partial Public Class edituser
    Inherits baseclass
    Dim x As New B2PAdminBLL.Security(AppSettings("ConnectionString"))
    Dim UserSecurity As B2PAdminBLL.Role
    Dim firstName, lastName, emailAddress As String
    Private Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        ViewStateUserKey = Context.Session.SessionID
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        UserSecurity = CType(Session("RoleInfo"), B2PAdminBLL.Role)
        If CStr(Session("editUserID")) = "" Then
            Response.Redirect("/security/modifyuser.aspx")
        End If
        If Not UserSecurity.SecurityUserMgmt Then
            Response.Redirect("/error/default.aspx?type=security")
        End If
        If Not IsPostBack Then
            loadData()
        End If

    End Sub
    Protected Sub loadData()
        x.ClientCode = CStr(Session("UserClientCode"))
        litUserLogin.Text = Trim(CStr(Session("editUserID")))
        x.UserName = Trim(CStr(Session("editUserID")))

        x.GetUser()
        txtAUFirstName.Text = Trim(HttpUtility.HtmlEncode(x.UserFirstName))
        txtAULastName.Text = Trim(HttpUtility.HtmlEncode(x.UserLastName))
        txtAUEMailAddress.Text = Trim(HttpUtility.HtmlEncode(x.EmailAddress))
        litLoginAttempts.Text = HttpUtility.HtmlEncode(CStr(x.LoginAttempt))

        ddlStatus.DataSourceID = "XmlDataSource1"
        ddlStatus.DataBind()
        ddlStatus.SelectedValue = HttpUtility.HtmlEncode(x.Status)

        
        ddlAURoles.Items.Clear()
        x.SecurityLevel = CInt(Session("SecurityLevel"))
        Dim dsRoles As DataSet = x.ListRoles
        ddlAURoles.DataSource = dsRoles.Tables(0)
        ddlAURoles.DataTextField = dsRoles.Tables(0).Columns("Role").ColumnName.ToString()
        ddlAURoles.DataBind()
        ddlAURoles.Items.FindItemByText(x.Role).Selected = True

        x.UserLogin = CStr(Session("AdminLoginID"))
        Dim dsOffice As DataSet = x.LoadAvailableOffices
        Dim dsNewOffice As DataSet = x.LoadAvailableOffices

        If dsOffice.Tables(0).Rows.Count > 0 Then
            Dim dsAssigned As DataSet = x.UserAssignedOffices
            For Each row As DataRow In dsOffice.Tables(0).Rows
                Dim filter As String = String.Format("Office_Id = '{0}'", row("Office_Id"))
                Dim rows As DataRow() = dsAssigned.Tables(0).[Select](filter)
                If rows.Length <> 0 Then
                    Dim deleteRows As DataRow() = dsNewOffice.Tables(0).[Select](filter)
                    dsNewOffice.Tables(0).Rows.Remove(deleteRows(0))
                Else
                    'dsNewOffice.Tables(0).Rows.Add(row)

                End If
            Next


            pnlOffice.Visible = True
            RadListBoxSource.DataSource = dsNewOffice.Tables(0)
            RadListBoxSource.DataBind()


            RadListBoxDestination.DataSource = dsAssigned.Tables(0)
            RadListBoxDestination.DataBind()
        Else
            pnlOffice.Visible = False
        End If

    End Sub

    Protected Sub btnChgPassword_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnChgPassword.Click
        Try

            x.NewPassword = HttpUtility.HtmlEncode(txtAUPassword.Text)
            x.UserLogin = Trim(CStr(Session("editUserID")))
            If x.ResetAdminPassword Then
                pnlPasswordResults.Visible = True
                pnlChgPassword.Visible = False
                lblPWStatus.Text = "Password reset"
                lblPWStatus.ForeColor = Drawing.Color.Green
            Else
                pnlPasswordResults.Visible = True
                pnlChgPassword.Visible = True
                lblPWStatus.Text = "Password not reset"
                lblPWStatus.Text = x.PasswordError
                lblPWStatus.ForeColor = Drawing.Color.Red
            End If
        Catch ex As Exception
            pnlPasswordResults.Visible = True
            lblPWStatus.Text = "Password not reset"
            lblPWStatus.ForeColor = Drawing.Color.Red
        End Try
    End Sub

    Protected Sub btnSubmit_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSubmit.Click
        If Page.IsValid = True Then
            btnSubmit.Enabled = False

            Try
                Dim x As New B2PAdminBLL.Security(AppSettings("ConnectionString"))

                x.UserLogin = Trim(CStr(Session("editUserID")))
                x.ClientCode = CStr(Session("UserClientCode"))
                x.UserFirstName = HttpUtility.HtmlEncode(txtAUFirstName.Text)
                x.UserLastName = HttpUtility.HtmlEncode(txtAULastName.Text)
                x.EmailAddress = HttpUtility.HtmlEncode(txtAUEMailAddress.Text)
                x.Role = HttpUtility.HtmlEncode(ddlAURoles.SelectedItem.Text)
                x.Status = HttpUtility.HtmlEncode(ddlStatus.SelectedItem.Text)


                Dim sOfficeList As String = String.Empty
                For i = 0 To RadListBoxDestination.Items.Count - 1
                    sOfficeList = String.Format("{0}{1},", sOfficeList, HttpUtility.HtmlEncode(RadListBoxDestination.Items(i).Value))
                Next

                If sOfficeList <> "" Then
                    x.AssignedOffices = sOfficeList
                End If

                If x.EditUser() Then
                    lblAUStatus.Text = "User information updated"
                    lblAUStatus.ForeColor = Drawing.Color.Green
                    pnlModifyUser.Visible = False
                    pnlResults.Visible = True
                Else
                    btnSubmit.Enabled = True
                    pnlResults.Visible = False
                    pnlModifyUser.Visible = True
                    lblAUStatus.ForeColor = Drawing.Color.Red
                End If

                lblAUStatus.Text = HttpUtility.HtmlEncode(x.StatusMessage)

            Catch ex As Exception

            End Try
        End If
    End Sub

    Protected Sub btnUnlock_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnUnlock.Click
        Try
            x.ClientCode = CStr(Session("UserClientCode"))
            x.UserName = Trim(CStr(Session("editUserID")))
            x.GetUser()

            firstName = Trim(HttpUtility.HtmlEncode(x.UserFirstName))
            lastName = Trim(HttpUtility.HtmlEncode(x.UserLastName))
            emailAddress = Trim(HttpUtility.HtmlEncode(x.EmailAddress))
            x.UserLogin = Trim(CStr(Session("editUserID")))
            x.LoginAttempt = 0

            Dim sOfficeList As String = String.Empty
            For i = 0 To RadListBoxDestination.Items.Count - 1
                sOfficeList = String.Format("{0}{1},", sOfficeList, HttpUtility.HtmlEncode(RadListBoxDestination.Items(i).Value))
            Next

            If sOfficeList <> "" Then
                x.AssignedOffices = sOfficeList
            End If

            If x.EditUser() Then
                litLoginAttempts.Text = CStr(0)
                pnlPasswordResults.Visible = True
                lblPWStatus.Text = "Login attempts reset"
                lblPWStatus.ForeColor = Drawing.Color.Green
            Else
                pnlPasswordResults.Visible = True
                lblPWStatus.Text = "Login attempts not reset"
                lblPWStatus.ForeColor = Drawing.Color.Red
            End If
        Catch ex As Exception
            pnlPasswordResults.Visible = True
            lblPWStatus.Text = "Login attempts not reset"
        End Try
    End Sub

    Protected Sub lnkViewUsers_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkViewUsers.Click
        Response.Redirect("/security/modifyuser.aspx")
    End Sub
End Class