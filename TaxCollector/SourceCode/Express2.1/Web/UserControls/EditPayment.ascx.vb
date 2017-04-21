Imports System

Namespace B2P.PaymentLanding.Express.Web

    Public Class EditPayment : Inherits System.Web.UI.UserControl

        Private Sub Footer_Load(sender As Object, e As EventArgs) Handles Me.Load
            If Not IsPostBack Then
                litYear.Text = Now.Year
                If BLL.SessionManager.Client IsNot Nothing Then
                    If BLL.SessionManager.Client.Website.Contains("http") Then
                        litLink1.Text = String.Format("<a href={0} target=""_blank"">{1}</a>", BLL.SessionManager.Client.Website, BLL.SessionManager.Client.ClientName)
                    Else
                        litLink1.Text = String.Format("<a href=http://{0} target=""_blank"">{1}</a>", BLL.SessionManager.Client.Website, BLL.SessionManager.Client.ClientName)
                    End If
                    litBill2Pay.Text = String.Format("<a href={0} target=""_blank"">{1}</a>", "http://www.bill2pay.com", "www.Bill2Pay.com")
                Else
                    litLink1.Visible = False
                    litBill2Pay.Visible = False
                End If


            End If
        End Sub
    End Class

End Namespace