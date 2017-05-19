Imports System.Configuration.ConfigurationManager

Public Class pwexpired
    Inherits System.Web.UI.Page

    Private Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        ViewStateUserKey = Context.Session.SessionID
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        txtAUPassword.Focus()
        litYear.Text = Now.Year
    End Sub

    Protected Sub btnSubmit_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSubmit.Click
        Try
            Dim x As New B2PAdminBLL.Security(AppSettings("ConnectionString"))
            x.CurrentPassword = x.DecryptPassword(CStr(Session("Password")))
            x.NewPassword = HttpUtility.HtmlEncode(txtAUPassword.Text)
            x.UserLogin = CStr(Session("PWResetUserName"))

            If x.ResetOwnPassword Then
                Session("AdminLoginID") = CStr(Session("PWResetUserName"))
                ' createa a new GUID and save into the session
                Dim guid__1 As String = Guid.NewGuid().ToString()
                Session("AuthToken") = guid__1
                ' now create a new cookie with this guid value
                Response.Cookies.Add(New HttpCookie("AuthToken", guid__1))

                Session("PWResetUserName") = ""
                Session("Password") = x.EncryptPassword(x.NewPassword)
                Dim UserSecurity As B2PAdminBLL.Role = CType(Session("RoleInfo"), B2PAdminBLL.Role)
                If UserSecurity.UserRole.ToUpper.Contains("CALL") Then
                    Response.Redirect("/payment/callcenter.aspx")
                ElseIf UserSecurity.UserRole.ToUpper.Contains("CLERK") Then
                    Response.Redirect("/payment/newcounter.aspx")
                Else
                    Response.Redirect("/search/")
                End If
            Else
                'lblMessage.Text = "New password must be different than the current password."
                lblMessage.Text = x.PasswordError
                lblMessage.Visible = True
            End If
        Catch ex As Exception
            lblMessage.Text = ex.Message
            lblMessage.Visible = True
        End Try
    End Sub

End Class