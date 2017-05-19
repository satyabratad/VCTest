Imports System.Configuration.ConfigurationManager
Imports B2P.Integration.TriPOS
Imports B2P.Integration.TriPOS.Entities

Public Class PinPadReturnChip
    Inherits baseclass

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then

                Session("PaymentProcessed") = True
                Dim x As New B2PAdminBLL.TransactionInformation(CInt(BLL.SessionManager.ConfirmationNumber), CStr(Session("AdminLoginID")), CStr(Session("ClientCode")))

                Dim sc As B2P.ShoppingCart.Cart = BLL.SessionManager.Cart
                If sc.TotalAmount > 0 Then

                    pnlNoResults.Visible = False
                    pnlResults.Visible = True

                    litDate.Text = CStr(x.TransactionDate) & " " & B2P.Objects.Client.GetClient(Session("ClientCode")).TimeZoneSuffix
                    pnlNonTax.Visible = True
                    litNonTaxAmt.Text = String.Format("{0:c}", sc.TotalAmount)
                        litNonTaxConfirm.Text = BLL.SessionManager.ConfirmationNumber.TrimStart(CChar("0"))
                        litNonTaxFee.Text = String.Format("{0:c}", sc.ECheckFee)
                        litTotal.Text = String.Format("{0:c}", sc.TotalAmount + sc.ECheckFee)

                        litType.Text = "Bank Account - " & x.BankAccountNumber
                        litName.Text = String.Format("{0} {1}", x.PayeeFirstName, x.PayeeLastName)
                        If CStr(Session("OriginatorID")) <> "" Then
                            pnlCommercial.Visible = True
                            litOriginatorID.Text = CStr(Session("OriginatorID"))
                        Else
                            pnlCommercial.Visible = False
                        End If


                    litClientName.Text = BLL.SessionManager.ClientName
                    litClientMessage.Text = B2P.Objects.Client.GetClientMessage("Manatron", Session("UserClientCode"))
                Else
                    pnlNoResults.Visible = True
                    pnlResults.Visible = False
                End If
            End If

        Catch ex As Exception
            pnlNoResults.Visible = True
            pnlResults.Visible = False
        End Try
    End Sub

End Class