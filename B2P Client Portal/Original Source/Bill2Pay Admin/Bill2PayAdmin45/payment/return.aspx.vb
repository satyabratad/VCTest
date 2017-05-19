Imports System.Configuration.ConfigurationManager

Partial Public Class _return
    Inherits baseclass
    
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Session("ConfirmationNumber") = "227"
        Try
            If Not IsPostBack Then


                If CStr(Session("ConfirmationNumber")) <> "" Then
                    pnlNoResults.Visible = False
                    pnlResults.Visible = True

                    Dim x As New B2P.Web.Counter.TransactionInformation(CInt(Session("ConfirmationNumber")), CStr(Session("AdminLoginID")), CStr(Session("ClientCode")), "counter")
                    'Dim x As New B2P.Web.Counter.TransactionInformation(10693, CStr(Session("AdminLoginID")), CStr(Session("ClientCode")), "counter")
                    Dim y As New B2PAdminBLL.Security(AppSettings("ConnectionString"))
                    y.ClientCode = CStr(Session("UserClientCode"))
                    y.OfficeID = x.Office_ID
                    y.GetOffice()

                    litOfficeName.Text = Trim(y.OfficeName)
                    litConfirmationNumber.Text = CStr(Session("ConfirmationNumber")).TrimStart(CChar("0"))

                    If x.Result = B2P.Web.Counter.TransactionInformation.Results.Success Then


                        Dim status As B2P.Web.Counter.TransactionInformation.TransactionStatus = x.Status
                        Select Case status
                            Case B2P.Web.Counter.TransactionInformation.TransactionStatus.ACHReturn
                                litStatus.Text = "E-Check Return"
                            Case B2P.Web.Counter.TransactionInformation.TransactionStatus.Credited
                                litStatus.Text = "Refund"
                            Case B2P.Web.Counter.TransactionInformation.TransactionStatus.Pending
                                litStatus.Text = "Approved"
                            Case B2P.Web.Counter.TransactionInformation.TransactionStatus.Processed
                                litStatus.Text = "Approved"
                            Case B2P.Web.Counter.TransactionInformation.TransactionStatus.Voided
                                litStatus.Text = "Voided"
                            Case B2P.Web.Counter.TransactionInformation.TransactionStatus.Unknown
                                litStatus.Text = "Unknown"
                        End Select

                        litClientName.Text = x.ClientName
                        litClientName2.Text = B2P.Objects.Client.GetClientMessage("Manatron", Session("UserClientCode")) 'x.ClientName & " thanks you for your business."
                        Dim ClientOptions As B2P.Objects.Client = B2P.Objects.Client.GetClient(CStr(Session("UserClientCode")))
                        litDate.Text = CStr(x.TransactionDate) & " " & ClientOptions.TimeZoneSuffix
                        litPhone.Text = x.PayeePhone
                        litNotes.Text = x.UserComments
                        litClientMessage.Text = x.ClientMessage
                        litSystemMessage.Text = x.SystemMessage
                       
                        If (Not Session("PostBackMessage") Is Nothing) Then
                            If Session("PostBackMessage").ToString = "Success" Then
                                litPostBackMessage.Text = String.Format("<br />The account update to {0} was successful.", x.ClientName)
                            Else
                                litPostBackMessage.Text = String.Format("<br />{0}", Session("PostBackMessage"))
                            End If

                        End If

                        If CStr(Session("OriginatorID")) <> "" Then
                            pnlCommercial.Visible = True
                            litOriginatorID.Text = CStr(Session("OriginatorID"))
                        Else
                            pnlCommercial.Visible = False
                        End If
                        Dim test As String = x.SystemMessage


                        For Each item As B2P.Payment.PaymentBase.TransactionItems.LineItem In x.Items
                            litItems.Text = String.Format("{0}{1}{2}", litItems.Text, (String.Format("{0} - {1} - {2:$#,###.00}", item.ProductName, item.AccountNumber1, item.Amount)), "<br />")
                        Next


                        Select Case x.PaymentType
                            Case B2P.Web.Counter.TransactionInformation.PaymentTypes.BankAccount
                                litType.Text = "Bank Account - " & x.BankAccountNumber
                            Case B2P.Web.Counter.TransactionInformation.PaymentTypes.CreditCard
                                litType.Text = String.Format("{0} - {1}", x.CardIssuer, x.CreditCardNumber)

                        End Select
                        litName.Text = String.Format("{0} {1}", x.PayeeFirstName, x.PayeeLastName)
                        litFee.Text = String.Format("{0:c}", x.Items.TotalFee)
                        litTotal.Text = String.Format("{0:c}", (x.Items.TotalFee + x.Items.TotalAmount))
                    Else
                        pnlNoResults.Visible = True
                        pnlResults.Visible = False
                    End If

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