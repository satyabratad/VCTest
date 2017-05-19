Imports System.Configuration.ConfigurationManager
Imports System.Data
Imports PeterBlum.DES
Imports PeterBlum.DES.Web
Imports PeterBlum.DES.Web.WebControls

Partial Public Class adduser
    Inherits baseclass
    Dim x As New B2PAdminBLL.Security(AppSettings("ConnectionString"))
    Dim dsOffice As New DataSet
    Dim UserSecurity As New B2PAdminBLL.Role
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        UserSecurity = CType(Session("RoleInfo"), B2PAdminBLL.Role)
        'AddHandler valColorErr.ServerCondition, AddressOf OfficeDataCheck
        If Not UserSecurity.SecurityUserMgmt Then
            Response.Redirect("/error/default.aspx?type=security")
        End If
        If Not IsPostBack Then

            x.ClientCode = CStr(Session("ClientCode"))
            x.UserLogin = CStr(Session("AdminLoginID"))

            dsOffice = x.LoadAvailableOffices
            If dsOffice.Tables(0).Rows.Count > 0 Then
                pnlOffice.Visible = True
                RadListBoxSource.DataSource = dsOffice.Tables(0)
                RadListBoxSource.DataBind()
                'RadListBoxDestination.Items.Insert(0, New Global.Telerik.Web.UI.RadListBoxItem("-- Select Item --", "-1"))

            Else
                pnlOffice.Visible = False
            End If

            UserSecurity = CType(Session("RoleInfo"), B2PAdminBLL.Role)
            x.Role = UserSecurity.UserRole
            x.SecurityLevel = CInt(Session("SecurityLevel"))
            ddlAURoles.Items.Clear()

            Dim dsRoles As DataSet = x.ListRoles
            ddlAURoles.DataSource = dsRoles.Tables(0)
            ddlAURoles.DataTextField = dsRoles.Tables(0).Columns("Role").ColumnName.ToString()
            ddlAURoles.DataBind()

            Dim ds As DataSet = x.LoadDropDownBoxes

            If CStr(Session("ClientCode")) = "B2P" Then
                ddlAUClient.DataSource = ds.Tables(0)
                ddlAUClient.DataTextField = ds.Tables(0).Columns("ClientCode").ColumnName.ToString()
                ddlAUClient.DataBind()
                ddlAUClient.Visible = True
                lblAUClient.Visible = False
            Else
                desValidator1.Enabled = False
                lblAUClient.Text = CStr(Session("ClientCode"))
                lblAUClient.Visible = True
                ddlAUClient.Visible = False
            End If

        End If


    End Sub
    
    Protected Sub btnSubmit_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSubmit.Click
        If Page.IsValid = True Then
            btnSubmit.Enabled = False

            Try
                Dim x As New B2PAdminBLL.Security(AppSettings("ConnectionString"))

                x.UserLogin = HttpUtility.HtmlEncode(txtAUUserLogin.Text)
                x.UserFirstName = HttpUtility.HtmlEncode(txtAUFirstName.Text)
                x.UserLastName = HttpUtility.HtmlEncode(txtAULastName.Text)
                x.EmailAddress = HttpUtility.HtmlEncode(txtAUEMailAddress.Text)
                x.Password = HttpUtility.HtmlEncode(txtAUPassword.Text)
                x.Role = ddlAURoles.SelectedItem.Text
                x.CreatedBy = CStr(Session("AdminLoginID"))
                If CStr(Session("ClientCode")) = "B2P" Then
                    x.ClientCode = HttpUtility.HtmlEncode(ddlAUClient.Text)
                Else
                    x.ClientCode = HttpUtility.HtmlEncode(lblAUClient.Text)
                End If

                Dim sOfficeList As String = String.Empty
                For i = 0 To RadListBoxDestination.Items.Count - 1
                    sOfficeList = String.Format("{0}{1},", sOfficeList, RadListBoxDestination.Items(i).Value)
                Next

                If sOfficeList <> "" Then
                    x.AssignedOffices = sOfficeList

                    'Else

                    '    Page.Validate()
                    '    Exit Sub
                End If
                x.CreateAccount()


                If x.StatusMessage = "Account created" Then
                    lblAUStatus.ForeColor = Drawing.Color.Green
                    pnlAddUser.Visible = False
                    pnlResults.Visible = True
                Else
                    btnSubmit.Enabled = True
                    pnlResults.Visible = False
                    pnlAddUser.Visible = True
                    lblAUStatus.ForeColor = Drawing.Color.Red
                End If

                lblAUStatus.Text = x.StatusMessage

            Catch ex As Exception

            End Try
        End If
    End Sub
    Protected Sub OfficeDataCheck(ByVal sourceCondition As IBaseCondition, ByVal args As IConditionEventArgs)
        Dim vCondition As CustomCondition = CType(sourceCondition, CustomCondition)
        Dim vArgs As ConditionTwoFieldEventArgs = CType(args, ConditionTwoFieldEventArgs)
        ' cannot evaluate when blank or ControlToEvaluate is unassigned
        'If ((vArgs.Value = "") Or (vArgs.ControlToEvaluate Is Nothing)) Then
        'vArgs.CannotEvaluate = True
        'Else ' evaluate

        If RadListBoxDestination.Items.Count = 0 Then
            vArgs.IsMatch = False
        Else
            vArgs.IsMatch = True
        End If
        'End If
    End Sub


    Protected Sub ddlAUClient_SelectedIndexChanged(ByVal o As Object, ByVal e As Global.Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles ddlAUClient.SelectedIndexChanged
        x.ClientCode = ddlAUClient.SelectedItem.Text
        UserSecurity = CType(Session("RoleInfo"), B2PAdminBLL.Role)
        x.Role = UserSecurity.UserRole
        x.UserLogin = CStr(Session("AdminLoginID"))
        x.SecurityLevel = CInt(Session("SecurityLevel"))
        ddlAURoles.Items.Clear()
        Dim dsRoles As DataSet = x.ListRoles
        ddlAURoles.DataSource = dsRoles.Tables(0)
        ddlAURoles.DataTextField = dsRoles.Tables(0).Columns("Role").ColumnName.ToString()
        ddlAURoles.DataBind()

        dsOffice = x.LoadAvailableOffices
        If dsOffice.Tables(0).Rows.Count > 0 Then
            pnlOffice.Visible = True
            RadListBoxSource.DataSource = dsOffice.Tables(0)
            RadListBoxSource.DataBind()
        Else
            pnlOffice.Visible = False
        End If
    End Sub

    Protected Sub lnkAddUser_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkAddUser.Click
        pnlAddUser.Visible = True
        pnlResults.Visible = False
        lblAUStatus.Text = ""
        lblAUStatus.Visible = False

    End Sub

    Protected Sub lnkViewUsers_Click(ByVal sender As Object, ByVal e As EventArgs) Handles lnkViewUsers.Click
        Response.Redirect("/security/modifyuser.aspx")
    End Sub
End Class