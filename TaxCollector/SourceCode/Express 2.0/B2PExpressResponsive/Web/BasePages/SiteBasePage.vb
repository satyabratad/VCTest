Imports System
Imports System.Web
Imports System.Web.UI
Imports B2P.PaymentLanding.Express

Namespace B2P.PaymentLanding.Express.Web

    Public Class SiteBasePage : Inherits System.Web.UI.Page

        Public Sub New()
        End Sub

        Protected Overrides Sub OnInit(e As System.EventArgs)
            MyBase.OnInit(e)

        End Sub

        Private Sub Page_Init(sender As Object, e As System.EventArgs) Handles Me.Init
            Response.Cache.SetCacheability(HttpCacheability.NoCache)
            Response.Cache.SetExpires(DateTime.UtcNow.AddHours(-1))
            Response.Cache.SetNoStore()
        End Sub

        Private Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
            If Not BLL.SessionManager.IsInitialized Then
                Response.Redirect("/Errors/SessionExpired.aspx")
            Else
                Me.Title = BLL.SessionManager.Client.ClientName
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


End Namespace