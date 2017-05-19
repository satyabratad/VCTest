Imports System.Configuration.ConfigurationManager
Public Class pinPadReturn
    Inherits baseclass
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
       Try
            If Not IsPostBack Then

                Session("PaymentProcessed") = True

                Dim sc As B2P.ShoppingCart.Cart = CType(Session("ShoppingCart"), B2P.ShoppingCart.Cart)

                If sc.TotalAmount > 0 Then

                    pnlNoResults.Visible = False
                    pnlResults.Visible = True

                    litDate.Text = B2P.Payment.Utility.GetClientTime(Session("ClientCode"))

                    If Not IsNothing(Session("CardType")) Then
                        If Session("TaxItemsAmount") > 0 Then
                            litTaxConfirm.Text = HttpUtility.HtmlEncode(Session("TaxConfirm").TrimStart(CChar("0")))
                            litTaxAuth.Text = HttpUtility.HtmlEncode(Session("TaxAuth"))
                            pnlTax.Visible = True
                            litTaxAmt.Text = String.Format("{0:c}", Session("TaxItemsAmount"))
                            litTaxFee.Text = String.Format("{0:c}", Session("TaxFee"))
                        End If
                        If Session("NonTaxItemsAmount") > 0 Then
                            pnlNonTax.Visible = True
                            litNonTaxAmt.Text = String.Format("{0:c}", Session("NonTaxItemsAmount"))
                            litNonTaxConfirm.Text = HttpUtility.HtmlEncode(Session("NonTaxConfirm").TrimStart(CChar("0")))
                            If Not IsNothing(Session("NonTaxAuth")) Then
                                litNonTaxAuth.Text = HttpUtility.HtmlEncode(Session("NonTaxAuth"))
                            Else
                                litNonTaxAuth.Text = "N/A"
                            End If

                            litNonTaxFee.Text = String.Format("{0:c}", Session("NonTaxFee"))
                        End If
                        Select Case Session("cardType")
                            Case B2P.Payment.FeeCalculation.PaymentTypes.CreditCard
                                litTotal.Text = String.Format("{0:c}", sc.CreditCardFee + sc.TotalAmount)
                                'litFee.Text = String.Format("{0:c}", CDec(sc.CreditCardFee))
                            Case B2P.Payment.FeeCalculation.PaymentTypes.DebitCard
                                litTotal.Text = String.Format("{0:c}", sc.DebitFee + sc.TotalAmount)
                                'litFee.Text = String.Format("{0:c}", CDec(sc.DebitFee))
                            Case B2P.Payment.FeeCalculation.PaymentTypes.PinDebit
                                litTotal.Text = String.Format("{0:c}", sc.PinDebitFee + sc.TotalAmount)
                                'litFee.Text = String.Format("{0:c}", CDec(sc.DebitFee))
                                pnlSignature.Visible = False

                        End Select
                        Dim tCardIssuer As String = ""
                        Dim tCreditCardNumber As String = ""
                        Dim tFirstName As String = ""
                        Dim tLastName As String = ""

                        If Session("Swiped") Then
                            Dim cc As New B2P.Common.Objects.SwipedCreditCard
                            cc = Session("SwipedCreditCard")
                            With cc
                                tCardIssuer = Session("CardIssuer")
                                tCreditCardNumber = .MaskedCCNumber
                                tFirstName = .Owner.FirstName
                                tLastName = .Owner.LastName
                            End With
                        Else
                            Dim cc As New B2P.Common.Objects.CreditCard
                            cc = CType(Session("CreditCard"), B2P.Common.Objects.CreditCard)
                            With cc
                                tCardIssuer = .CardIssuer
                                tCreditCardNumber = .CreditCardNumber
                                tFirstName = .Owner.FirstName
                                tLastName = .Owner.LastName
                            End With
                        End If

                        litType.Text = String.Format("{0} - {1}", tCardIssuer, tCreditCardNumber)
                        litName.Text = String.Format("{0} {1}", tFirstName, tLastName)
                    Else

                        Dim x As New B2P.Web.Counter.TransactionInformation(CInt(Session("NonTaxConfirm")), CStr(Session("AdminLoginID")), CStr(Session("ClientCode")), "counter")
                        If Session("NonTaxItemsAmount") > 0 Then
                            pnlNonTax.Visible = True
                            litNonTaxAmt.Text = String.Format("{0:c}", Session("NonTaxItemsAmount"))
                            litNonTaxConfirm.Text = HttpUtility.HtmlEncode(Session("NonTaxConfirm").TrimStart(CChar("0")))
                            If Not IsNothing(Session("NonTaxAuth")) Then
                                litNonTaxAuth.Text = HttpUtility.HtmlEncode(Session("NonTaxAuth"))
                            Else
                                pnlAuthCode.Visible = False
                            End If
                            litNonTaxFee.Text = String.Format("{0:c}", Session("NonTaxFee"))
                            litTotal.Text = String.Format("{0:c}", Session("NonTaxItemsAmount") + Session("NonTaxFee"))
                        End If
                        litType.Text = "Bank Account - " & x.BankAccountNumber
                        litName.Text = String.Format("{0} {1}", x.PayeeFirstName, x.PayeeLastName)
                        If CStr(Session("OriginatorID")) <> "" Then
                            pnlCommercial.Visible = True
                            litOriginatorID.Text = CStr(Session("OriginatorID"))
                        Else
                            pnlCommercial.Visible = False
                        End If
                    End If


                    litClientName.Text = Session("ClientName")
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