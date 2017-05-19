Imports System
Imports System.Web
Imports B2P.Integration.TriPOS

'Namespace B2P.PaymentLanding.TriPOS.Web

Public Class SiteBasePage : Inherits System.Web.UI.Page

        Public Sub New()
        End Sub

        Protected Overrides Sub OnInit(e As System.EventArgs)
            MyBase.OnInit(e)

            '' It appears from testing that the Request and Response both share the 
            '' same cookie collection. If I set a cookie myself in the Reponse, it is 
            '' also immediately visible to the Request collection. This just means that 
            '' since the ASP.Net_SessionID is set in the Session HTTPModule (which 
            '' has already run), that we can't use our own code to see if the cookie was 
            '' actually sent by the agent with the request using the collection. Check if 
            '' the given page supports session or not (this tested as a reliable indicator 
            '' if EnableSessionState is true), should not care about a page that does 
            '' not need session.
            'If Context.Session IsNot Nothing Then

            '    ' Tested and the IsNewSession is more advanced then simply checking if 
            '    ' a cookie is present. It does take into account a session timeout, because 
            '    ' I tested a timeout and it did show as a new session.
            '    If Session.IsNewSession Then

            '        ' If it says it is a new session, but an existing cookie exists, then it must 
            '        ' have timed out -- can't use the cookie collection because even on first 
            '        ' request it already contains the cookie (request and response
            '        ' seem to share the collection).
            '        Dim szCookieHeader As String = Request.Headers("Cookie")

            '        If (szCookieHeader IsNot Nothing) AndAlso (szCookieHeader.IndexOf("ASP.NET_SessionId") >= 0) Then
            '            Response.Redirect("/Errors/SessionExpired.aspx")
            '        End If

            '    End If

            'End If
        End Sub

        Private Sub Page_Init(sender As Object, e As System.EventArgs) Handles Me.Init
            Response.Cache.SetCacheability(HttpCacheability.NoCache)
            Response.Cache.SetExpires(DateTime.UtcNow.AddHours(-1))
            Response.Cache.SetNoStore()
        End Sub

        Private Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
            If Not BLL.SessionManager.IsInitialized Then
            Response.Redirect("/login/login.aspx")
        Else
            Me.Title = "Make a Payment :: Bill2Pay Administration"
        End If
        End Sub

        ''' <summary>
        ''' Checks if the session has been initialized.
        ''' </summary>
        Public Sub CheckSession()
            Me.CheckSession(True)
        End Sub

        ''' <summary>
        ''' Checks if the session has been initialized.
        ''' </summary>
        ''' <param name="endResponse">
        ''' Indicates whether execution of the current page should terminate.
        ''' </param>
        ''' <remarks>If not initialized, redirects to a session expired page.</remarks>
        Public Sub CheckSession(ByVal endResponse As Boolean)
            If Not BLL.SessionManager.IsInitialized Then
                Response.Redirect("/Errors/SessionExpired.aspx", endResponse)
            End If
        End Sub

    End Class

'End Namespace