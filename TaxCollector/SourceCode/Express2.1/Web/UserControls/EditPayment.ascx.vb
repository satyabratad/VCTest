Imports System

Namespace B2P.PaymentLanding.Express.Web

    Public Class EditPayment : Inherits System.Web.UI.UserControl

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            If Not IsPostBack Then
                If Not BLL.SessionManager.CreditCard Is Nothing Then
                    If Not BLL.SessionManager.ContactInfo Is Nothing Then
                        pnlZip.Visible = True
                        Select Case BLL.SessionManager.CreditCard.Owner.CountryCode
                            Case "US"
                                lblBillingZipCaption.Text = GetGlobalResourceObject("WebResources", "lblBillingZip").ToString()
                            Case "CA"
                                lblBillingZipCaption.Text = GetGlobalResourceObject("WebResources", "lblBillingPostalCode").ToString()
                            Case Else
                                lblBillingZipCaption.Text = GetGlobalResourceObject("WebResources", "lblBillingZip").ToString()
                        End Select
                    Else
                        pnlZip.Visible = False
                    End If
                End If


                Select Case BLL.SessionManager.PaymentType
                    Case Common.Enumerations.PaymentTypes.BankAccount
                        ' Show the payment method info
                        lblPaymentMethod.Text = BLL.SessionManager.BankAccount.BankAccountType.ToString.Replace("_", " ") & " " & "*" & Right(BLL.SessionManager.BankAccount.BankAccountNumber, BLL.SessionManager.BankAccount.BankAccountNumber.Length - 1)
                        pnlExpDate.Visible = False
                        pnlZip.Visible = False
                    Case Common.Enumerations.PaymentTypes.CreditCard
                        ' Show the payment method info
                        lblPaymentMethod.Text = BLL.SessionManager.CreditCard.CardIssuer & " " & Right(BLL.SessionManager.CreditCard.CreditCardNumber, 6)
                        ' Set Exp Date
                        pnlExpDate.Visible = True
                        lblExpDate.Text = BLL.SessionManager.CreditCard.ExpirationMonth + "/" + BLL.SessionManager.CreditCard.ExpirationYear.Substring(2)
                        'set zip code
                        pnlZip.Visible = True
                        lblBillZip.Text = BLL.SessionManager.CreditCard.Owner.ZipCode
                End Select

            End If
        End Sub

        Protected Sub lnkEdit_Click(sender As Object, e As EventArgs) Handles lnkEdit.Click
            Response.Redirect("/pay/Payment.aspx", False)
        End Sub
    End Class

End Namespace