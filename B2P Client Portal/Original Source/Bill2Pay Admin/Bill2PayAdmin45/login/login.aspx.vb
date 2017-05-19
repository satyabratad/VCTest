Imports System.Configuration.ConfigurationManager
Imports B2P.Integration.TriPOS


Public Class login
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Session.Abandon()
            litYear.Text = Now.Year
        End If

        Session("AdminLoginID") = ""
        Session("SecurityLevel") = ""
        Session("LoggedIn") = ""
        Session("ID") = ""

        txtLoginID.Focus()
    End Sub

    Protected Sub btnLogIn_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnLogin.Click
        Dim errMsg As String = String.Empty
        Dim Session_id As Guid = Guid.NewGuid()
        Session("SessionID") = Session_id.ToString()

        Try
            Dim x As New B2PAdminBLL.Security(AppSettings("ConnectionString"))
            x.UserLogin = HttpUtility.HtmlEncode(txtLoginID.Text)
            x.Password = HttpUtility.HtmlEncode(txtPassword.Text)
            Select Case x.ValidateAdminLogin
                Case B2PAdminBLL.Security.AdminLoginStatus.Success
                    Session("AdminLoginID") = txtLoginID.Text
                    ' createa a new GUID and save into the session
                    Dim guid__1 As String = Guid.NewGuid().ToString()
                    Session("AuthToken") = guid__1
                    ' now create a new cookie with this guid value
                    Response.Cookies.Add(New HttpCookie("AuthToken", guid__1))

                    Session("RoleInfo") = x.RoleObject
                    Session("SecurityID") = x.Security_ID
                    BLL.SessionManager.SecurityID = x.Security_ID
                    Session("SecurityLevel") = x.SecurityLevel
                    Session("AdminLoginID") = txtLoginID.Text
                    Session("AdminEmailAddress") = x.EmailAddress.Trim
                    Session("ClientCode") = x.ClientCode.Trim
                    B2P.Integration.TriPOS.BLL.SessionManager.ClientCode = Session("ClientCode")
                    Session("PaySiteURL") = AppSettings("PaySiteURL")
                    Session("AssignedOffice") = x.AssignedOffices
                    System.Web.Security.FormsAuthentication.RedirectFromLoginPage("admin", False)
                    Session("Authenticated") = "TRUE"
                    If CDate(x.PasswordExpiration) > CDate(Now()) Then
                        Session("Password") = x.EncryptPassword(x.Password)
                        Dim UserSecurity As B2PAdminBLL.Role = CType(Session("RoleInfo"), B2PAdminBLL.Role)

                        If UserSecurity.UserRole.ToUpper.Contains("CALL") Then
                            Response.Redirect("/payment/callcenter.aspx", False)
                        ElseIf UserSecurity.UserRole.ToUpper.Contains("CLERK") Then
                            Response.Redirect("/payment/newcounter.aspx", False)
                        Else
                            Response.Redirect("/search/", False)
                        End If

                    Else
                        Session("PWResetUserName") = x.UserLogin
                        Session("Password") = x.EncryptPassword(x.Password)
                        Response.Redirect("/login/pwexpired.aspx", False)
                    End If
                Case B2PAdminBLL.Security.AdminLoginStatus.Failed
                    lblMessage.Text = "Error: Either the Login ID or Password is invalid. Please try again."
                    lblMessage.Visible = True
                Case B2PAdminBLL.Security.AdminLoginStatus.ExessiveAttempts
                    lblMessage.Text = "Error: Too many incorrect attempts: Your account has been locked out. Please contact your administrator to reset your password."
                    lblMessage.Visible = True
                Case B2PAdminBLL.Security.AdminLoginStatus.Inactive
                    lblMessage.Text = "Error: Your login information is not active. Please contact your administrator."
                    lblMessage.Visible = True
            End Select
        Catch ex As Exception
            ' Build the error message
            errMsg = baseclass.BuildErrorMessage(Request.Url.Host, Request.Url.AbsolutePath, _
                                               ex.Source, ex.TargetSite.DeclaringType.Name, _
                                               ex.TargetSite.Name, ex.Message)

            B2P.Common.Logging.LogError("B2P Admin Site --> " & Request.Url.AbsoluteUri & ".", errMsg, B2P.Common.Logging.NotifySupport.No)
            Response.Redirect("/Error/", False)
            HttpContext.Current.ApplicationInstance.CompleteRequest()
        End Try

    End Sub

End Class