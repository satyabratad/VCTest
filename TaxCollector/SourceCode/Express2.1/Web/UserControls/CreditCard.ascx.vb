Imports System
Imports System.Configuration.ConfigurationManager
Imports System.Data
Imports System.Globalization
Imports System.IO
Imports System.Web
Imports System.Web.UI.WebControls
Imports System.Xml

Namespace B2P.PaymentLanding.Express.Web

    Public Class CreditCard : Inherits System.Web.UI.UserControl

#Region "::: Properties :::"

        Public ReadOnly Property CreditCardNumber() As String
            Get
                Return Utility.SafeEncode(txtCreditCardNumber.Text.Trim.Replace(" ", ""))
            End Get
        End Property

        Public ReadOnly Property CVV() As String
            Get
                Return Utility.SafeEncode(txtCCV.Text.Trim)
            End Get
        End Property

        Public ReadOnly Property ExpirationDate() As String
            Get
                Return Utility.SafeEncode(txtExpireDate.Text.Trim.Replace(" ", ""))
            End Get
        End Property

        'Public Property FirstName() As String
        '    Get
        '        Return Utility.SafeEncode(txtCreditFirstName.Text.Trim)
        '    End Get
        '    Set(value As String)
        '        txtCreditFirstName.Text = value.Trim
        '    End Set
        'End Property

        'Public Property LastName() As String
        '    Get
        '        Return Utility.SafeEncode(txtCreditLastName.Text.Trim)
        '    End Get
        '    Set(value As String)
        '        txtCreditLastName.Text = value.Trim
        '    End Set
        'End Property
        Public Property NameonCard() As String
            Get
                Return Utility.SafeEncode(txtNameonCard.Text.Trim)
            End Get
            Set(value As String)
                txtNameonCard.Text = value.Trim
            End Set
        End Property

#End Region

#Region "::: Control Event Handlers :::"

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            ' Add client side javascript attributes to the various form controls
            RegisterClientJs()

            ' Initialize a few things
            If Not Me.IsPostBack Then
                ' Set accepted card the images
                litCardImages.Text = Utility.BuildAllowedCardImages()


                BindCountries()
                Me.txtBillingZip.Text = BLL.SessionManager.ContactInfo.Zip
                Me.ddlCountry.SelectedValue = BLL.SessionManager.ContactInfo.UserField2
            End If
        End Sub


#End Region

#Region "::: Methods :::"

        ''' <summary>
        ''' Clears the values of this UserControl.
        ''' </summary>
        Public Sub Clear()
            'txtCreditFirstName.Text = String.Empty
            'txtCreditLastName.Text = String.Empty

            txtCreditCardNumber.Text = String.Empty
            txtExpireDate.Text = String.Empty
            txtCCV.Text = String.Empty
        End Sub


        ''' <summary>
        ''' Adds client side javascript to validate the form.
        ''' </summary>
        Private Sub RegisterClientJs()
            'txtCreditFirstName.Attributes.Add("onkeypress", "return restrictInput(event, restrictionTypes.AlphaNumericAndExtraChars)")
            'txtCreditFirstName.Attributes.Add("onpaste", "return reformatInput(this, restrictionTypes.AlphaNumericAndExtraChars)")

            'txtCreditLastName.Attributes.Add("onkeypress", "return restrictInput(event, restrictionTypes.AlphaNumericAndExtraChars)")
            'txtCreditLastName.Attributes.Add("onpaste", "return reformatInput(this, restrictionTypes.AlphaNumericAndExtraChars)")

            txtNameonCard.Attributes.Add("onkeypress", "return restrictInput(event, restrictionTypes.AlphaNumericAndExtraChars)")
            txtNameonCard.Attributes.Add("onpaste", "return reformatInput(this, restrictionTypes.AlphaNumericAndExtraChars)")


        End Sub

        Private Sub BindCountries()
            ddlCountry.Items.Clear()
            ddlCountry.Items.Add(New ListItem("--Select--", ""))

            If ConfigurationManager.AppSettings("Countries") IsNot Nothing Then
                Dim countryPath = ConfigurationManager.AppSettings("Countries")

                If Not countryPath.StartsWith("~/") Then
                    countryPath = String.Format("~/{0}", countryPath)
                End If
                countryPath = Server.MapPath(countryPath)


                Dim countries = ReadStateXML(countryPath)
                For Each item As KeyValuePair(Of String, String) In countries
                    Dim element = New ListItem(item.Value, item.Key)
                    element.Attributes("OptionGroup") = "Country"
                    ddlCountry.Items.Add(element)
                Next
            End If
        End Sub

        Private Function ReadStateXML(path As String) As IEnumerable(Of KeyValuePair(Of String, String))

            Dim _Values As List(Of KeyValuePair(Of String, String)) =
            New List(Of KeyValuePair(Of String, String))

            If (IO.File.Exists(path)) Then
                Dim document As XmlReader = New XmlTextReader(path)
                While (document.Read())
                    Dim type = document.NodeType
                    If (type = XmlNodeType.Element) Then
                        If (document.Name = "state" Or document.Name = "country") Then
                            Dim key = document.GetAttribute("abbreviation")
                            Dim value = document.GetAttribute("name")
                            Dim element = New KeyValuePair(Of String, String)(key, value)

                            _Values.Add(element)
                        End If
                    End If
                End While
            Else
                Throw New FileNotFoundException(String.Format(CultureInfo.InvariantCulture, "{0} Not Found", path))
            End If

            Return _Values
        End Function
#End Region

    End Class

End Namespace