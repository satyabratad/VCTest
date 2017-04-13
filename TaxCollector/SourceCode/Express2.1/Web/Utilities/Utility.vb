'Imports Microsoft-.Security.Application
Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Text
Imports System.Text.RegularExpressions

Namespace B2P.PaymentLanding.Express.Web

    Public Class Utility

#Region " ::: Methods ::: "
        ''' <summary>
        ''' Builds the min/max amounts on the bank account form.
        ''' </summary>
        Public Shared Function BuildACHAmount() As String
            Dim achAmt As String = String.Empty
            Dim retVal As String = String.Empty

            If BLL.SessionManager.CurrentCategory IsNot Nothing Then
                retVal = "var achMinMaxAmounts = '"

                If BLL.SessionManager.LookupAmountMinimum = True Then
                    If BLL.SessionManager.LookupAmount > BLL.SessionManager.CurrentCategory.PaymentInformation.ACH.MaximumAmount Then
                        achAmt &= BLL.SessionManager.CurrentCategory.PaymentInformation.ACH.MaximumAmount & "|" & BLL.SessionManager.CurrentCategory.PaymentInformation.ACH.MaximumAmount
                    Else
                        achAmt &= BLL.SessionManager.LookupAmount & "|" & BLL.SessionManager.CurrentCategory.PaymentInformation.ACH.MaximumAmount
                    End If

                Else
                    achAmt &= BLL.SessionManager.CurrentCategory.PaymentInformation.ACH.MinimumAmount & "|" & BLL.SessionManager.CurrentCategory.PaymentInformation.ACH.MaximumAmount
                End If

                retVal &= achAmt & "';"

            End If

            Return retVal

        End Function
        ''' <summary>
        ''' Builds the min/max amounts on the credit card form.
        ''' </summary>
        Public Shared Function BuildCCAmount() As String
            Dim ccAmt As String = String.Empty
            Dim retVal As String = String.Empty

            If BLL.SessionManager.CurrentCategory IsNot Nothing Then
                retVal = "var ccMinMaxAmounts = '"

                If BLL.SessionManager.LookupAmountMinimum = True Then
                    If BLL.SessionManager.LookupAmount > BLL.SessionManager.CurrentCategory.PaymentInformation.Creditcard.MaximumAmount Then
                        ccAmt &= BLL.SessionManager.CurrentCategory.PaymentInformation.Creditcard.MaximumAmount & "|" & BLL.SessionManager.CurrentCategory.PaymentInformation.Creditcard.MaximumAmount
                    Else
                        ccAmt &= BLL.SessionManager.LookupAmount & "|" & BLL.SessionManager.CurrentCategory.PaymentInformation.Creditcard.MaximumAmount
                    End If

                Else
                    ccAmt &= BLL.SessionManager.CurrentCategory.PaymentInformation.Creditcard.MinimumAmount & "|" & BLL.SessionManager.CurrentCategory.PaymentInformation.Creditcard.MaximumAmount
                End If

                retVal &= ccAmt & "';"

            End If

            Return retVal

        End Function

        ''' <summary>
        ''' Builds the accepted card images on the credit card form.
        ''' </summary>

        Public Shared Function BuildAllowedCardImages() As String
            Dim retVal As String = String.Empty

            If BLL.SessionManager.CurrentCategory IsNot Nothing Then
                If BLL.SessionManager.CurrentCategory.PaymentInformation.Creditcard.AcceptVisa Then
                    retVal &= "<img id=""imgVisa"" src=""/img/icons/visa_small.png"" title=""Visa"" alt=""Visa"" /> "
                End If

                If BLL.SessionManager.CurrentCategory.PaymentInformation.Creditcard.AcceptMasterCard Then
                    retVal &= "<img id=""imgMasterCard"" src=""/img/icons/mastercard_small.png"" title=""MasterCard"" alt=""MasterCard"" /> "
                End If

                If BLL.SessionManager.CurrentCategory.PaymentInformation.Creditcard.AcceptDiscover Then
                    retVal &= "<img id=""imgDiscover"" src=""/img/icons/discover_small.png"" title=""Discover"" alt=""Discover"" /> "
                End If

                If BLL.SessionManager.CurrentCategory.PaymentInformation.Creditcard.AcceptAmericanExpress Then
                    retVal &= "<img id=""imgAmex"" src=""/img/icons/amex_small.png"" title=""American Express"" alt=""American Express"" /> "
                End If
            End If

            Return retVal.Trim()
        End Function

        ''' <summary>
        ''' Builds the card type pattern used to validate accepted credit card types.
        ''' </summary>

        Public Shared Function BuildAllowedCardsPattern() As String
            Dim cards As String = String.Empty
            Dim retVal As String = String.Empty

            If BLL.SessionManager.CurrentCategory IsNot Nothing Then
                If BLL.SessionManager.CurrentCategory.PaymentInformation.CreditCardAccepted Then
                    retVal = "var validCardPattern = /^("

                    If BLL.SessionManager.CurrentCategory.PaymentInformation.Creditcard.AcceptAmericanExpress Then
                        cards &= "amex|"
                    End If

                    If BLL.SessionManager.CurrentCategory.PaymentInformation.Creditcard.AcceptDiscover Then
                        cards &= "discover|"
                    End If

                    If BLL.SessionManager.CurrentCategory.PaymentInformation.Creditcard.AcceptMasterCard Then
                        cards &= "mastercard|"
                    End If

                    If BLL.SessionManager.CurrentCategory.PaymentInformation.Creditcard.AcceptVisa Then
                        cards &= "visa|"
                    End If

                    retVal &= cards.TrimEnd("|"c) & ")$/i;"
                End If
            End If

            Return retVal
        End Function

        ''' <summary>
        ''' Builds an error message from a thrown exception.
        ''' </summary>
        ''' <param name="host">The website host URL address.</param>
        ''' <param name="page">The website page where the exception ocurred.</param>
        ''' <param name="source">The name of the application or the object that caused the exception.</param>
        ''' <param name="type">The declaring type that raised the exception.</param>
        ''' <param name="method">The calling method that raised the exception.</param>
        ''' <param name="message">The message that describes the current exception.</param>
        ''' <returns>String.</returns>
        Public Shared Function BuildErrorMessage(ByVal host As String, ByVal page As String, ByVal source As String, ByVal type As String, ByVal method As String, ByVal message As String) As String
            Dim sb As New StringBuilder
            Dim retVal As String = String.Empty

            Try
                sb.AppendLine(message)
                sb.AppendLine()
                sb.AppendLine("Host URL: " & host)
                sb.AppendLine("Offending Page: " & page)
                sb.AppendLine("Application Source: " & source)
                sb.AppendLine("Declaring Type: " & type)
                sb.AppendLine("Calling Method: " & method)
                sb.AppendLine()

                retVal = sb.ToString

            Finally
                ' Clean up a bit
                If sb IsNot Nothing Then
                    sb = Nothing
                End If
            End Try

            Return retVal
        End Function

        ''' <summary>
        ''' Builds the min/max amounts on the credit card form.
        ''' </summary>
        Public Shared Function BuildClientEmailRequirement() As String
            Dim client As B2P.Objects.Client = B2P.Objects.Client.GetClient(BLL.SessionManager.ClientCode)
            Dim retVal As String = String.Empty
            retVal = "var RequireEmail = '"

            If client.RequireBillingEmail Then
                retVal &= True & "';"
            Else
                retVal &= False & "';"
            End If
            Return retVal

        End Function

        ''' <summary>
        ''' Get's the date portion of today's date.
        ''' </summary>
        ''' <returns>String.</returns>
        ''' <remarks>Just for testing webmethods in the page.</remarks>
        Public Shared Function GetDate() As String
            Return "Today's date is " & DateTime.Now.ToString("MM/dd/yyyy")
        End Function

        ''' <summary>
        ''' Get's the payment card type based on its BIN number.
        ''' </summary>
        ''' <param name="bin"></param>
        ''' <returns>String.</returns>
        Public Shared Function GetPaymentCardType(ByVal bin As String) As String
            Dim reader As SqlDataReader = Nothing
            Dim retVal As String = String.Empty

            Try
                ' Create the connection
                Using conn As New SqlConnection(B2P.Common.Configuration.ConnectionString)

                    ' Create the command
                    Using cmd As New SqlCommand
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.CommandText = "ap_CheckBINNumber"
                        cmd.CommandTimeout = 300
                        cmd.Connection = conn

                        ' Assign the param values
                        cmd.Parameters.Add("@BINNumber", SqlDbType.VarChar, 6).Value = bin
                        cmd.Parameters.Add("@CardType", SqlDbType.Char, 1).Direction = ParameterDirection.Output

                        ' Open the database connection
                        conn.Open()

                        ' Execute the command
                        cmd.ExecuteNonQuery()

                        ' Get the return value
                        retVal = cmd.Parameters("@CardType").Value.ToString
                    End Using

                End Using

            Catch ex As Exception
                ' Do something

            Finally
                ' Clean up a bit
                If reader IsNot Nothing Then
                    If Not reader.IsClosed Then
                        reader.Close()
                    End If

                    reader = Nothing
                End If
            End Try

            Return retVal
        End Function

        ''' <summary>
        ''' An inline If function that does not evaluate both true and false parts before the expression.
        ''' </summary>
        ''' <typeparam name="T">Type.</typeparam>
        ''' <param name="expression">A boolean expression to be evalutaed.</param>
        ''' <param name="truePart">The type returned if the expression evaluates to true.</param>
        ''' <param name="falsePart">The type returned if the expression evaluates to false.</param>
        ''' <returns>Type.</returns>
        Public Shared Function IIf(Of T)(ByVal expression As Boolean, ByVal truePart As T, ByVal falsePart As T) As T
            If expression Then
                Return truePart
            Else
                Return falsePart
            End If
        End Function

        ''' <summary>
        ''' An inline assignment helper function that sets a target type to a specified value type.
        ''' </summary>
        ''' <typeparam name="T">Type.</typeparam>
        ''' <param name="target">The type to be assigned a value.</param>
        ''' <param name="value">The value to assign to the target type.</param>
        ''' <returns>Type.</returns>
        Public Shared Function InlineAssignHelper(Of T)(ByRef target As T, ByVal value As T) As T
            target = value

            Return value
        End Function

        ''' <summary>
        ''' Indicates whether the specified String object is a null reference or an Empty string.
        ''' </summary>
        ''' <param name="value">The string to check.</param>
        ''' <returns>Boolean.</returns>
        ''' <remarks>
        ''' Replaces .Net's flawed String.IsNullOrEmpty function.
        ''' </remarks>
        Public Shared Function IsNullOrEmpty(ByVal value As String) As Boolean
            Return (value Is Nothing OrElse value.Trim.Length = 0)
        End Function

        ''' <summary>
        ''' Checks to see if a field is valid.
        ''' </summary>
        ''' <param name="fieldItem">The item to be validated.</param>
        ''' <param name="validationType">The validation type to be applied to the field item.</param>
        ''' <returns>Boolean.</returns>
        ''' <remarks>
        ''' These regex patterns can be placed in the configuration file so that they can be 
        ''' modified as needed without having to recompile the appliction.
        ''' </remarks>
        Public Shared Function IsValidField(ByVal fieldItem As String, ByVal validationType As FieldValidationType) As Boolean
            Dim dt As DateTime
            Dim dateParts As String()
            Dim month As Int32
            Dim year As Int32

            Select Case validationType
                Case FieldValidationType.AccountNumber
                    Return Regex.IsMatch(fieldItem, "^\d{10}$")

                Case FieldValidationType.BankAccount
                    Return Regex.IsMatch(fieldItem, "^\d{1,17}$")

                Case FieldValidationType.CreditCard
                    Return Regex.IsMatch(fieldItem.Replace(" ", ""),
                                 "^((4\d{3})|(2[2-7]\d{2})|(5[1-5]\d{2})|(6011))-?\d{4}-?\d{4}-?\d{4}|3[4,7]\d{13}$")

                Case FieldValidationType.Currency
                    Return Regex.IsMatch(fieldItem, "^\$?(\d{1,3}(\,\d{3})*|(\d+))(\.\d{2})?$")

                Case FieldValidationType.CVV
                    Return Regex.IsMatch(fieldItem, "^[0-9]{3,4}$")

                Case FieldValidationType.Date
                    If Regex.IsMatch(fieldItem, "^(0?[1-9]|1[012])/(0?[1-9]|[12]\d|3[01])/((19|2\d)\d\d)$") AndAlso DateTime.TryParse(fieldItem, dt) Then 'IsDate(fieldItem) Then
                        Return True
                    End If

                Case FieldValidationType.Email
                    ' Per RFC specs, e-mail address local and domain parts have no leading, trailing, or
                    ' consecutive dots and top-level domain has two to six letters.
                    Return Regex.IsMatch(fieldItem, "^[\w%+-]+(?:\.[\w%+-]+)*@(?:[A-Z0-9-]+\.)+[A-Z]{2,6}$", RegexOptions.IgnoreCase)


                Case FieldValidationType.ExpirationDate
                    If Regex.IsMatch(fieldItem, "^(0[1-9]|1[012])(\s/\s|/)((19|2\d)?\d\d)$") Then
                        dateParts = fieldItem.Replace(" ", "").Split("/"c)

                        month = Convert.ToInt32(dateParts(0).Trim)
                        year = Convert.ToInt32(dateParts(1).Trim)

                        ' Make sure the expiration date > today
                        If DateTime.Today.Date < New Date(year, month, 1).AddMonths(1) AndAlso year <= DateTime.Today.Year + 10 Then
                            Return True
                        Else
                            Return False
                        End If
                    End If

                Case FieldValidationType.Integer
                    Return Regex.IsMatch(fieldItem, "^[\-]?\d+$")

                Case FieldValidationType.IpAddress
                    Return Regex.IsMatch(fieldItem, "^\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}$")

                Case FieldValidationType.Name
                    Return Regex.IsMatch(fieldItem, "^[a-z0-9\s\.\-\,\']+$", RegexOptions.IgnoreCase)

                Case FieldValidationType.NonEmpty
                    Return Regex.IsMatch(fieldItem, "^\w+", RegexOptions.IgnoreCase)

                Case FieldValidationType.Numeric
                    Return Regex.IsMatch(fieldItem, "^[\-]?\d+(.\d+)?$")

                Case FieldValidationType.PhoneFax
                    Return Regex.IsMatch(fieldItem,
                                 "^(\((?<area>\d{3})\)|(?<area>\d{3}))[\-\s]?(?<exch>\d{3})[\-\s]?(?<num>\d{4})$")

                Case FieldValidationType.PositiveInteger
                    Return Regex.IsMatch(fieldItem, "^\d+$")

                Case FieldValidationType.RoutingNumber
                    Return Regex.IsMatch(fieldItem,
                                 "^((0[0-9])|(1[0-2])|(2[1-9])|(3[0-2])|(6[1-9])|(7[0-2])|80)([0-9]{7})$")

                Case FieldValidationType.SSN
                    ' First 3 digits are called the area number and cannot be 000, 666, or between 900 and 999.
                    ' Digits 4 and 5 are called the group number and range from 01 to 99.
                    ' Last 4 digits are serial numbers from 0001 to 9999.
                    ' SSNs 219-09-9999 and 078-05-1120 are invalid due to SSA fubar.
                    Return Regex.IsMatch(fieldItem,
                                 "^(?!219-09-9999|078-05-1120)(?!000|666)[0-8][0-9]{2}[\-\s]?(?!00)[0-9]{2}[\-\s]?(?!0000)[0-9]{4}$")

                Case FieldValidationType.StateAbbreviation
                    Return Regex.IsMatch(fieldItem.ToUpper,
                                 "^(AL|AK|AZ|AR|CA|CO|CT|DE|DC|FL|GA|HI|ID|IL|IN|IA|KS|KY|LA|ME|MD|MA|MI|MN|MS|MO|MT|NE|NV|NH|NJ|NM|NY|NC|ND|OH|OK|OR|PA|RI|SC|SD|TN|TX|UT|VT|VA|WA|WV|WI|WY|AB|BC|MB|NB|NL|NS|NT|NU|ON|PE|QC|SK|YT)$",
                                 RegexOptions.IgnoreCase)

                Case FieldValidationType.TrueFalse
                    Return Regex.IsMatch(fieldItem, "^(T(RUE)?|F(ALSE)?)$", RegexOptions.IgnoreCase)

                Case FieldValidationType.URL
                    Return Regex.IsMatch(fieldItem,
                                 "^(http|https)://[\w\-_]+(\.[\w\-_]+)+([\w\-\.,@?^=%&:/~\+#]*[\w\-\@?^=%&/~\+#])?",
                                 RegexOptions.IgnoreCase)

                Case FieldValidationType.YesNo
                    Return Regex.IsMatch(fieldItem, "^(Y(ES)?|N(O)?)$", RegexOptions.IgnoreCase)

                Case FieldValidationType.ZipCodeCanada
                    ' CAN postal codes do not include the letters D, F, I, O, Q or U, and the first position also does not use the letters W or Z
                    ' See https://en.wikipedia.org/wiki/Postal_codes_in_Canada#Components_of_a_postal_code
                    Return Regex.IsMatch(fieldItem, "^(?!.*[DFIOQU])[A-VXY]\d[A-Z]\s?\d[A-Z]\d$", RegexOptions.IgnoreCase)

                Case FieldValidationType.ZipCodeInternational
                    Return Regex.IsMatch(fieldItem, "^[a-z0-9]+[ \-]?[a-z0-9]+$")

                Case FieldValidationType.ZipCodeUnitedStates
                    ' Matches Zip or Zip+4
                    Return Regex.IsMatch(fieldItem, "^\d{5}(?:\-\d{4})?$")

                Case Else
                    Return False
            End Select

            ' Got to here --> something's wrong
            Return False
        End Function


        ''' <summary>
        ''' Gets an enumeration member based on it's corresponding integer value.
        ''' </summary>
        ''' <typeparam name="T">Type.</typeparam>
        ''' <param name="number">Enumeration integer value.</param>
        ''' <returns>Type.</returns>
        Public Shared Function NumberToEnum(Of T)(ByVal number As Int32) As T
            Return DirectCast([Enum].ToObject(GetType(T), number), T)
        End Function

        ''' <summary>
        ''' 
        ''' </summary>
        ''' <param name="TextToEncode"></param>
        ''' <returns>String.</returns>
        Public Shared Function SafeEncode(ByVal textToEncode As String) As String
            Dim retVal As String = textToEncode.Trim

            If retVal.Length <> 0 Then
                'retVal = Replace(Encoder.HtmlEncode(TextToEncode), "&amp;", "&")    ' Stop using functions from Microsoft.VisualBasic
                retVal = Microsoft.Security.Application.Encoder.HtmlEncode(textToEncode).Replace("&amp;", "&")

                'retVal = Replace(retVal, "&#39;", "'")    ' Stop using functions from Microsoft.VisualBasic
                retVal = retVal.Replace("&#39;", "'")
            End If

            Return retVal
        End Function

        ''' <summary>
        ''' setting Contact Info flag from bread crumb menu
        ''' If Demographics or HomePhone either are enabled, then customer will see this Contact Info page
        ''' If neither are enabled Contact Info page will not display 
        ''' </summary>
        ''' <param name="z"></param>

        Public Shared Sub SetBreadCrumbContactInfo(z As Objects.Product)

            If z.WebOptions.Demographics = Objects.WebConfiguration.OptionalFields.NotUsed And z.WebOptions.HomePhone = Objects.WebConfiguration.OptionalFields.NotUsed Then
                BLL.SessionManager.IsContactInfoRequired = False
            Else
                BLL.SessionManager.IsContactInfoRequired = True
            End If

        End Sub
#End Region

    End Class

End Namespace