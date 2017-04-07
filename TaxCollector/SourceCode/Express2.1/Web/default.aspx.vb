Imports System
Imports System.IO
Imports B2P.PaymentLanding.Express

Namespace B2P.PaymentLanding.Express.Web
    Public Class _default
        Inherits System.Web.UI.Page

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            If Not IsPostBack Then
                Session.Clear()
                Dim errMsg As String = String.Empty
                Dim clientCode As String = String.Empty
                Try
                    BLL.SessionManager.ClientFAQ = "/faq/faq.html"
                    ' Make sure there is a client code passed over via the query string parameter

                    Dim client As String = Page.RouteData.Values("client")

                    If Not IsNothing(client) Then
                        clientCode = Utility.SafeEncode(Page.RouteData.Values("client"))
                        BLL.SessionManager.ClientCode = clientCode
                    Else
                        Session.Clear()
                        psmErrorMessage.ToggleStatusMessage(GetGlobalResourceObject("WebResources", "ErrMsgMissingClient").ToString(), StatusMessageType.Danger, True, True)
                        Exit Sub
                    End If

                    Dim y As B2P.Objects.Client = B2P.Objects.Client.GetClient(BLL.SessionManager.ClientCode)
                    If y.Found = False Then
                        Session.Clear()
                        psmErrorMessage.ToggleStatusMessage(GetGlobalResourceObject("WebResources", "ErrMsgMissingClient").ToString(), StatusMessageType.Danger, True, True)
                        Exit Sub
                    End If

                    ' Set the session variables
                    BLL.SessionManager.IsInitialized = True
                    'BLL.SessionManager.SystemMessage = B2P.Common.Utility.GetSystemMessage(BLL.SessionManager.ClientCode.ToString)
                    BLL.SessionManager.CategoryList = B2P.Objects.Product.ListProducts(BLL.SessionManager.ClientCode, B2P.Common.Enumerations.TransactionSources.Web)
                    BLL.SessionManager.Client = y
                    BLL.SessionManager.IsSSOProduct = False
                    BLL.SessionManager.VendorReferenceCode = ""

                    ' Check to see if custom CSS file exists for client
                    If File.Exists(Server.MapPath("/Css/ClientCSS/" & BLL.SessionManager.ClientCode & ".css")) Then
                        BLL.SessionManager.ClientCSS = "/Css/ClientCSS/" & BLL.SessionManager.ClientCode & ".css"
                    Else
                        BLL.SessionManager.ClientCSS = "/Css/app.css"
                    End If

                    ' Set the CSS link to the appropriate file name
                    lnkCSS.Attributes("href") = BLL.SessionManager.ClientCSS

                    ' Check for custom FAQ page
                    If File.Exists(Server.MapPath("/faq/ClientFAQ/" & BLL.SessionManager.ClientCode & ".html")) Then
                        BLL.SessionManager.ClientFAQ = "/faq/ClientFAQ/" & BLL.SessionManager.ClientCode & ".html"
                    Else
                        BLL.SessionManager.ClientFAQ = "/faq/faq.html"
                    End If

                    Response.Redirect("/pay/Temp.aspx", False)
                Catch ex As Exception
                    ' Build the error message
                    errMsg = Utility.BuildErrorMessage(Request.Url.Host, Request.Url.AbsolutePath,
                                                   ex.Source, ex.TargetSite.DeclaringType.Name,
                                                   ex.TargetSite.Name, ex.Message)

                    B2P.Common.Logging.LogError("Express Payment --> " & Request.Url.AbsoluteUri & ".", errMsg, B2P.Common.Logging.NotifySupport.No)
                    Response.Redirect("/Errors/Error.aspx", False)
                    HttpContext.Current.ApplicationInstance.CompleteRequest()
                End Try


            End If


        End Sub




    End Class
End Namespace