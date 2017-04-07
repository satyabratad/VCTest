Imports System
Imports System.Configuration.ConfigurationManager
Imports System.Web

Namespace B2P.PaymentLanding.Express.Web

    ''' <summary>
    ''' An HttpModule which redirects traffic to HTTPS based on configuration settings.
    ''' </summary>
    Public Class HttpsRedirector : Implements IHttpModule

#Region " ::: Member Variables ::: "

        Private m_context As HttpApplication

#End Region

#Region " ::: Constructors ::: "

        '
        ' TODO: Add constructor logic here
        '
        Public Sub New()
        End Sub

#End Region

#Region " ::: IHttpModule Members ::: "

        ''' <summary>
        ''' Initializes a module and prepares it to handle requests.
        ''' </summary>
        ''' <param name="context">
        ''' An <see cref="T:System.Web.HttpApplication"/> that provides access to the methods, 
        ''' properties, and events common to all application objects within an ASP.NET application.
        ''' </param>
        Public Sub Init(ByVal context As HttpApplication) Implements IHttpModule.Init
            m_context = context

            AddHandler m_context.BeginRequest, New EventHandler(AddressOf context_BeginRequest)
        End Sub

        ''' <summary>
        ''' Disposes of the resources (other than memory) used by the module that 
        ''' implements <see langword="IHttpModule."/>
        ''' </summary>
        Public Sub Dispose() Implements IHttpModule.Dispose
            ' Was getting "Event handlers can only be bound to HttpApplication events during IHttpModule initialization" error in IIS7+
            ' http://stackoverflow.com/questions/6877045/event-handlers-can-only-be-bound-to-httpapplication-events-during-ihttpmodule-in
            'RemoveHandler m_context.BeginRequest, New EventHandler(AddressOf context_BeginRequest)

            m_context.Dispose()
        End Sub

#End Region

        ''' <summary>
        ''' Handles the BeginRequest event of the current Http Context.
        ''' </summary>
        Private Sub context_BeginRequest(ByVal sender As Object, ByVal e As EventArgs)
            Dim useSSL As Boolean
            Dim result As String = String.Empty

            If Utility.InlineAssignHelper(result, AppSettings("RequireSSL").ToString) IsNot Nothing Then
                If result.ToUpper() = "TRUE" OrElse result.ToUpper() = "YES" Then
                    useSSL = True
                End If
            End If

            If useSSL Then
                EnforceSSL()
            End If
        End Sub

        ''' <summary>
        ''' Enforces a redirection to HTTPS if the current connection is using HTTP (port 80).
        ''' </summary>
        Private Sub EnforceSSL()
            Dim uri As UriBuilder = Nothing

            '::: See if the app is being run on the localhost ::: 
            If m_context.Request.ServerVariables("SERVER_NAME").ToLower() <> "localhost" Then

                If m_context.Request.ServerVariables("SERVER_PORT") = "80" Then
                    uri = New UriBuilder(m_context.Request.Url)

                    uri.Scheme = "https"
                    uri.Port = 443

                    m_context.Response.Redirect(uri.ToString())
                End If

            End If
        End Sub

    End Class

End Namespace