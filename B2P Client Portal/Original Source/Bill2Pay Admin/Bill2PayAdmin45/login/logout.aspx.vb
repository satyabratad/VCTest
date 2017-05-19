Public Class logout
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Request.Cookies("ASP.NET_SessionId") IsNot Nothing Then
            Response.Cookies("ASP.NET_SessionId").Value = String.Empty
            Response.Cookies("ASP.NET_SessionId").Expires = DateTime.Now.AddMonths(-20)
        End If

        If Request.Cookies("AuthToken") IsNot Nothing Then
            Response.Cookies("AuthToken").Value = String.Empty
            Response.Cookies("AuthToken").Expires = DateTime.Now.AddMonths(-20)
        End If

        Session.Abandon()
        Response.Redirect("/welcome")
    End Sub

End Class