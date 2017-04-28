Imports System

Namespace B2P.PaymentLanding.Express.Web

    Public Class EditContactInfo : Inherits System.Web.UI.UserControl
#Region "::: Control Event Handlers :::"
        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            If Not BLL.SessionManager.IsContactInfoRequired Then
                pnlEditContactInfo.Visible = False
                Exit Sub
            End If
            If Not IsPostBack Then
                If Not BLL.SessionManager.ContactInfo Is Nothing Then
                    lblContactInfoName.Text = BLL.SessionManager.ContactInfo.UserField1
                    lblContactAddress.Text = GetContactAddress()
                End If
            End If
        End Sub
        Protected Sub lnkEdit_Click(sender As Object, e As EventArgs) Handles lnkEdit.Click
            Response.Redirect("/pay/ContactInfo.aspx", False)
        End Sub
#End Region

#Region "::: Methods :::"
        Private Function GetContactAddress() As String
            Dim propertyAddress As StringBuilder = New StringBuilder()
            If Not BLL.SessionManager.ContactInfo Is Nothing Then
                If Not String.IsNullOrEmpty(BLL.SessionManager.ContactInfo.Address1) Then
                    propertyAddress.AppendFormat("{0}, ", BLL.SessionManager.ContactInfo.Address1)
                End If
                If Not String.IsNullOrEmpty(BLL.SessionManager.ContactInfo.Address2) Then
                    propertyAddress.AppendFormat("{0}, ", BLL.SessionManager.ContactInfo.Address2)
                End If
                If Not String.IsNullOrEmpty(BLL.SessionManager.ContactInfo.UserField2) Then
                    propertyAddress.AppendFormat("{0}, ", BLL.SessionManager.ContactInfo.UserField2)
                End If
                If Not String.IsNullOrEmpty(BLL.SessionManager.ContactInfo.City) Then
                    propertyAddress.AppendFormat("{0}, ", BLL.SessionManager.ContactInfo.City)
                End If
                If Not String.IsNullOrEmpty(BLL.SessionManager.ContactInfo.State) Then
                    propertyAddress.AppendFormat("{0} ", BLL.SessionManager.ContactInfo.State)
                End If
                If Not String.IsNullOrEmpty(BLL.SessionManager.ContactInfo.Zip) Then
                    propertyAddress.AppendFormat("{0}, ", BLL.SessionManager.ContactInfo.Zip)
                End If
            End If
            Return propertyAddress.ToString().Trim().TrimEnd(",")
        End Function
#End Region

    End Class

End Namespace