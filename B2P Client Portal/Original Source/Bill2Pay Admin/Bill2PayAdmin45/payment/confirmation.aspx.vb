Imports System
Imports System.Configuration
Imports System.Data
Imports System.Drawing
Imports System.IO
Imports System.Text
Imports System.Web
Imports System.Web.Services
Imports Newtonsoft.Json
Imports B2P.Integration.TriPOS
Public Class confirmation
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim errMsg As String = String.Empty

        Try
            If Not Me.IsPostBack Then
                ' Check to see if user entered customer's email address
                If BLL.SessionManager.CustomerEmail <> "" AndAlso BLL.SessionManager.TransactionReceipt.ReceiptType = Entities.ReceiptType.Approved Then
                    hidEmailAddress.Value = BLL.SessionManager.CustomerEmail
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "sendReceipt", "sendReceipt();", True)
                End If
                BuildClientLogo()

                litMerchantHeaderInfo.Text = BuildMerchantHeader()

                BuildTransInfo()

                BuildEmvInfo()

                If BLL.SessionManager.TransactionReceipt.ReceiptType = Entities.ReceiptType.Approved Then
                    DetermineSignature(BLL.SessionManager.TransactionReceipt)
                    pnlSignatureInfo.Visible = True
                Else
                    pnlSignatureInfo.Visible = False
                End If
            End If

        Catch ex As Exception
            ' Build the error message
            errMsg = Utility.BuildErrorMessage(HttpContext.Current.Request.Url.Host, HttpContext.Current.Request.Url.AbsolutePath,
                                                   ex.Source, ex.TargetSite.DeclaringType.Name,
                                                   ex.TargetSite.Name, ex.Message)

            B2P.Common.Logging.LogError("B2P Admin --> " & HttpContext.Current.Request.Url.AbsoluteUri & ".", errMsg, B2P.Common.Logging.NotifySupport.No)
            HttpContext.Current.Response.Redirect("/Error", False)
            HttpContext.Current.ApplicationInstance.CompleteRequest()
        End Try
    End Sub

    ''' <summary>
    ''' Builds the source of the client logo for the receipt.
    ''' </summary>
    ''' <remarks>
    ''' Tries to find the logo locally first, then tries to retrieve it from the database.
    ''' </remarks>
    Private Sub BuildClientLogo()
        Dim logoPath As String = Request.PhysicalApplicationPath & ConfigurationManager.AppSettings("ClientLogoPath").Trim
        Dim logoFile As String = String.Empty
        Dim image As Byte() = Nothing
        Dim errMsg As String = String.Empty

        Try
            ' Look for a local PNG, JPG or GIF image file based on the client code
            Select Case True
                Case File.Exists(logoPath & BLL.SessionManager.ClientCode & ".png")
                    logoFile = BLL.SessionManager.ClientCode & ".png"
                Case File.Exists(logoPath & BLL.SessionManager.ClientCode & ".jpg")
                    logoFile = BLL.SessionManager.ClientCode & ".jpg"
                Case File.Exists(logoPath & BLL.SessionManager.ClientCode & ".gif")
                    logoFile = BLL.SessionManager.ClientCode & ".gif"
            End Select

            ' See if we have a local image file resource
            If Not Utility.IsNullOrEmpty(logoFile) Then
                imgClientLogo.ImageUrl = "/" & ConfigurationManager.AppSettings("ClientLogoPath").Trim.Replace("\", "/") & logoFile
            Else
                ' Try to grab the image from the database
                image = GetClientLogo()

                If image IsNot Nothing Then
                    imgClientLogo.ImageUrl = "data:image/png;base64," & Convert.ToBase64String(image)
                Else
                    ' Hide the image
                    imgClientLogo.Visible = False
                End If
            End If

        Catch ex As Exception
            ' Build the error message
            errMsg = Utility.BuildErrorMessage(HttpContext.Current.Request.Url.Host, HttpContext.Current.Request.Url.AbsolutePath,
                                                   ex.Source, ex.TargetSite.DeclaringType.Name,
                                                   ex.TargetSite.Name, ex.Message)

            B2P.Common.Logging.LogError("B2P Admin --> " & HttpContext.Current.Request.Url.AbsoluteUri & ".", errMsg, B2P.Common.Logging.NotifySupport.Yes)
            HttpContext.Current.Response.Redirect("/Error", False)
            HttpContext.Current.ApplicationInstance.CompleteRequest()
        End Try
    End Sub

    Private Sub BuildEmvInfo()
        Dim tag As Entities.ApiTag = Nothing
        Dim sb As StringBuilder = Nothing

        If BLL.SessionManager.TransactionReceipt.EmvVerified Then
            If Not Utility.IsNullOrEmpty(BLL.SessionManager.TransactionReceipt.EmvApplicationPreferredName) Then
                litCardAndPaymentType.Text = BLL.SessionManager.TransactionReceipt.EmvApplicationPreferredName.ToUpper
            Else
                litCardAndPaymentType.Text = BLL.SessionManager.TransactionReceipt.EmvApplicationLabel.ToUpper
            End If

            litEmvAppId.Text = BLL.SessionManager.TransactionReceipt.EmvApplicationID

            pnlEmvAppId.Visible = True

            litEmvCrypto.Text = BLL.SessionManager.TransactionReceipt.EmvCryptogram
            pnlEmvCrypto.Visible = True

            ' Get the EMV tags
            If BLL.SessionManager.TransactionReceipt.EmvTags IsNot Nothing AndAlso BLL.SessionManager.TransactionReceipt.EmvTags.Count > 0 Then
                sb = New StringBuilder

                For Each tag In BLL.SessionManager.TransactionReceipt.EmvTags
                    sb.Append(tag.Key & ": " & tag.Value & "<br />")
                Next tag

                litEmvTags.Text = sb.ToString
                pnlEmvTags.Visible = True
            Else
                pnlEmvTags.Visible = False
            End If
        Else
            pnlEmvAppId.Visible = False
            pnlEmvCrypto.Visible = False
            pnlEmvTags.Visible = False
        End If
    End Sub

    Private Function BuildMerchantHeader() As String
        Dim sb As New StringBuilder
        Dim responseDate As String = BLL.SessionManager.TransactionReceipt.TransactionDateTime

        sb.Append(BLL.SessionManager.TransactionReceipt.MerchantName & "<br />")
        sb.Append(BLL.SessionManager.TransactionReceipt.MerchantAddress1 & "<br />")

        If Not Utility.IsNullOrEmpty(BLL.SessionManager.TransactionReceipt.MerchantAddress2) Then
            sb.Append(BLL.SessionManager.TransactionReceipt.MerchantAddress2 & "<br />")
        End If

        sb.Append(BLL.SessionManager.TransactionReceipt.MerchantCity & ", ")
        sb.Append(BLL.SessionManager.TransactionReceipt.MerchantState & " ")
        sb.Append(BLL.SessionManager.TransactionReceipt.MerchantZip & "<br />")

        If Not Utility.IsNullOrEmpty(BLL.SessionManager.TransactionReceipt.MerchantPhone) Then
            sb.Append(BLL.SessionManager.TransactionReceipt.MerchantPhone & "<br />")
        End If

        ' Remove the TZ offset so that the server doesn't mess up the time when converting
        If responseDate.IndexOf("-"c) > -1 Then
            ' Response transaction date in ISO-8601 format
            sb.Append(Convert.ToDateTime(responseDate.Substring(0, responseDate.LastIndexOf("-"))).ToString("MMM dd, yyyy    hh:mmtt") & " " &
                      B2P.Objects.Client.GetClient(BLL.SessionManager.ClientCode).TimeZoneSuffix)
        Else
            ' Just pulling from the transaction table
            sb.Append(Convert.ToDateTime(responseDate).ToString("MMM dd, yyyy    hh:mmtt") & " " &
                      B2P.Objects.Client.GetClient(BLL.SessionManager.ClientCode).TimeZoneSuffix)
        End If

        'sb.Append(Convert.ToDateTime(BLL.SessionManager.TransactionReceipt.TransactionDateTime).ToString("MMM dd, yyyy    hh:mmtt") & " " & _
        '          B2P.Objects.Client.GetClient(BLL.SessionManager.ClientCode).TimeZoneSuffix)

        Return sb.ToString.ToUpper

    End Function

    Private Sub BuildTransInfo()
        litCardAndPaymentType.Text = (BLL.SessionManager.TransactionReceipt.CardType & " " & BLL.SessionManager.TransactionReceipt.PaymentType).ToUpper
        litMechantLocationCode.Text = BLL.SessionManager.TransactionReceipt.MerchantLocationCode
        litCardHolderName.Text = BLL.SessionManager.TransactionReceipt.CardHolderName.ToUpper
        litReferenceNumber.Text = BLL.SessionManager.TransactionReceipt.ReferenceNumber.ToUpper
        litAccountNumber.Text = BLL.SessionManager.TransactionReceipt.AccountNumber.ToUpper
        litCardType.Text = BLL.SessionManager.TransactionReceipt.CardType.ToUpper
        litEntryMode.Text = BLL.SessionManager.TransactionReceipt.EntryMode.ToUpper
        litPayType.Text = BLL.SessionManager.TransactionReceipt.PaymentType.ToUpper
        litAmount.Text = BLL.SessionManager.TransactionReceipt.SubTotal.ToString("C2")
        litConvenienceFee.Text = BLL.SessionManager.TransactionReceipt.ConvenienceFee.ToString("C2")
        litTotalAmount.Text = BLL.SessionManager.TransactionReceipt.TransactionAmount.ToString("C2")
        litApprovalCode.Text = BLL.SessionManager.TransactionReceipt.TransactionApprovalNumber.ToUpper

        If BLL.SessionManager.TransactionReceipt.EntryMode.ToUpper <> "MANUAL" Then
            litHostCode.Text = BLL.SessionManager.TransactionReceipt.HostResponseCode.ToUpper
            litTransID.Text = BLL.SessionManager.TransactionReceipt.TransactionID.ToUpper
        Else
            pnlHostCodes.Visible = False
        End If

        Select Case BLL.SessionManager.TransactionReceipt.ReceiptType
            Case Entities.ReceiptType.Approved
                litTransStatus.Text = Entities.ReceiptType.Approved.ToString.ToUpper
                pnlApprovalCode.Visible = True
                litSalutation.Text = BLL.SessionManager.TransactionReceipt.Salutation.ToUpper
            Case Entities.ReceiptType.Declined
                litTransStatus.Text = Entities.ReceiptType.Declined.ToString.ToUpper
                pnlApprovalCode.Visible = False
                litSalutation.Text = GetGlobalResourceObject("WebResources", "SalutationDeclined").ToString.ToUpper
            Case Else
                litSalutation.Text = GetGlobalResourceObject("WebResources", "SalutationDeclined").ToString.ToUpper
        End Select
    End Sub

    ''' <summary>
    ''' Determines whether or not to display signature, display a blank
    ''' signature line, or display nothing at all.
    ''' </summary>
    ''' <param name="receipt">The response object</param>
    Private Sub DetermineSignature(receipt As Entities.Receipt)

        If Not Utility.IsNullOrEmpty(receipt.SignatureStatusCode) Then

            Select Case receipt.SignatureStatusCode
                Case "SignatureRequired"
                    ' Signature required, check for signature data
                    If receipt.SignatureData IsNot Nothing Then
                        Me.DisplaySignature(receipt)
                    Else
                        ' Signature is required but the response contains no signature data, include blank signature line on receipt 
                        Me.DisplaySignatureLine()
                    End If

                Case "SignaturePresent"
                    ' Signature present, display it
                    Me.DisplaySignature(receipt)

                Case "SignatureRequiredCancelledByCardholder", "SignatureRequiredNotSupportedByPinPad", "SignatureRequiredPinPadError"
                    ' Signature required, cancelled by cardholder
                    ' Signature required, not supported by PIN pad
                    ' Signature required, PIN pad error
                    Me.DisplaySignatureLine()

                Case "Unknown", "SignatureNotRequiredByThresholdAmount", "SignatureNotRequiredByPaymentType", "SignatureNotRequiredByTransactionType"
                    ' Unknown/error, do not display signature or signature line
                    ' Signature not required by threshold amount
                    ' Signature not required by payment type
                    ' Signature not required by transaction type
                    imgSignature.Visible = False

                    ' Check for PIN
                    If receipt.PinVerified Then
                        litSignatureNotNeeded.Text = "PIN USED - SIGNATURE NOT REQUIRED"
                    End If

                    ' Check for EMV
                    If receipt.EmvVerified Then
                        litSignatureNotNeeded.Text = "PIN VERIFIED - SIGNATURE NOT REQUIRED"
                    End If

                Case Else
                    ' Me.labelException.Text = SignatureStatusCodeNotExpectedValue
            End Select

        Else
            ' No signature came back
        End If
    End Sub

    ''' <summary>
    ''' Displays signature image on receipt.
    ''' </summary>
    ''' <param name="receipt">The response object</param>
    Private Sub DisplaySignature(receipt As Entities.Receipt)
        Dim signature = New Entities.Imaging.Signature
        Dim signatureImage As Bitmap = Nothing
        Dim stream As System.IO.MemoryStream = Nothing
        Dim byteImage As Byte() = Nothing
        Dim errMsg As String = String.Empty

        Try
            ' Make sure we have a receipt to get a signature from
            If receipt IsNot Nothing Then
                signature.SetFormat(receipt.SignatureFormat)
                signature.SetData(receipt.SignatureData)

                ' Get the signature image
                signatureImage = signature.GetSignatureBitmap(10)

                ' See if there is anything to process
                If signatureImage IsNot Nothing Then
                    stream = New System.IO.MemoryStream
                    signatureImage.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg)

                    ' Convert the image stream to byte array
                    byteImage = stream.ToArray()

                    ' Set the image source
                    imgSignature.Src = "data:image/png;base64," & Convert.ToBase64String(byteImage)
                End If
            End If

        Catch ex As Exception
            ' Build the error message
            errMsg = Utility.BuildErrorMessage(HttpContext.Current.Request.Url.Host, HttpContext.Current.Request.Url.AbsolutePath,
                                                   ex.Source, ex.TargetSite.DeclaringType.Name,
                                                   ex.TargetSite.Name, ex.Message)

            B2P.Common.Logging.LogError("B2P Admin --> " & HttpContext.Current.Request.Url.AbsoluteUri & ".", errMsg, B2P.Common.Logging.NotifySupport.No)
            HttpContext.Current.Response.Redirect("/Error", False)
            HttpContext.Current.ApplicationInstance.CompleteRequest()

        Finally
            ' Clean up a bit
            If byteImage IsNot Nothing Then
                byteImage = Nothing
            End If

            If stream IsNot Nothing Then
                stream = Nothing
            End If

            If signatureImage IsNot Nothing Then
                signatureImage = Nothing
            End If

            If signature IsNot Nothing Then
                signature = Nothing
            End If
        End Try
    End Sub

    ''' <summary>
    ''' Displays blank signature line on receipt.
    ''' </summary>
    Private Sub DisplaySignatureLine()
        imgSignature.Src = "/img/SignatureLine.png"
    End Sub

    ''' <summary>
    ''' Gets the client logo from the database.
    ''' </summary>
    ''' <returns>A Byte array.</returns>
    Private Function GetClientLogo() As Byte()
        Dim retVal As Byte() = Nothing
        Dim errMsg As String = String.Empty

        Try
            ' Create the connection
            Using conn As New SqlClient.SqlConnection(B2P.Common.Configuration.ConnectionString)

                ' Create the command
                Using cmd As New SqlClient.SqlCommand
                    cmd.CommandText = "ap_GetClientLogo"
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.Connection = conn

                    ' Assign the param values
                    cmd.Parameters.Add("@ClientCode", SqlDbType.VarChar, 10).Value = BLL.SessionManager.ClientCode
                    cmd.Parameters.Add("@Logo", SqlDbType.VarBinary, -1).Direction = ParameterDirection.Output

                    ' Open the database connection
                    conn.Open()

                    ' Execute the query
                    cmd.ExecuteNonQuery()

                    ' Get the return value -- make sure the data is not NULL
                    If cmd.Parameters("@Logo").Value IsNot Nothing AndAlso cmd.Parameters("@Logo").Value IsNot System.DBNull.Value Then
                        retVal = DirectCast(cmd.Parameters("@Logo").Value, Byte())
                    End If

                End Using

            End Using

        Catch ex As Exception
            ' Build the error message
            errMsg = Utility.BuildErrorMessage(HttpContext.Current.Request.Url.Host, HttpContext.Current.Request.Url.AbsolutePath,
                                                   ex.Source, ex.TargetSite.DeclaringType.Name,
                                                   ex.TargetSite.Name, ex.Message)

            B2P.Common.Logging.LogError("B2P Admin --> " & HttpContext.Current.Request.Url.AbsoluteUri & ".", errMsg, B2P.Common.Logging.NotifySupport.Yes)
            HttpContext.Current.Response.Redirect("/Error", False)
            HttpContext.Current.ApplicationInstance.CompleteRequest()
        End Try

        Return retVal
    End Function

    <WebMethod>
    Public Shared Function SendReceipt(ByVal email As String, ByVal contents As String) As Int32
        Dim message As New B2P.Common.Email.EmailMessage
        Dim retVal As Int32

        message.RecipientAddress = email.Trim '"scott.leonard@bill2pay.com" email.Trim
        message.SenderAddress = "Payments@Bill2Pay.com"
        message.Subject = String.Format("{0} Payment Receipt", BLL.SessionManager.ClientName)
        message.HTMLBody = contents.Trim

        If B2P.Common.Email.SendMail(message) Then
            retVal = 1
            'Else
            '    Me.litEmailResult.Text = "Unable to send email at this time"
        End If

        Return retVal
    End Function

End Class