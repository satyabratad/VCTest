Imports System

Namespace B2P.PaymentLanding.Express.Web

    Public Class Header : Inherits System.Web.UI.UserControl

        Private Sub Header_Load(sender As Object, e As EventArgs) Handles Me.Load
            If Not IsPostBack Then
                If BLL.SessionManager.ClientCode <> "" Then
                    imgClientLogo.ImageUrl = "/img/ClientImages/" & BLL.SessionManager.ClientCode & ".jpg"
                Else
                    imgClientLogo.ImageUrl = "/img/B2P-Logo4.png"
                End If

                imgClientLogo.Attributes("onerror") = "this.src='/img/B2P-Logo4.png';"
            End If
        End Sub
    End Class

End Namespace