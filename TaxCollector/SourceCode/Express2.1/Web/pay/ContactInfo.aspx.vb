Imports System
Imports System.Globalization
Imports System.IO
Imports System.Xml

Namespace B2P.PaymentLanding.Express.Web

    Public Class ContactInfo : Inherits SiteBasePage

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            If Not IsPostBack Then
                BindCountries()
                BindState()
                RegisterClientJs()
                PopulateContactInfo()
            End If
        End Sub
        Protected Sub ddlContactCountry_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlContactCountry.SelectedIndexChanged
            BindState()
        End Sub
        Protected Sub btnContinue_Click(sender As Object, e As EventArgs) Handles btnContinue.Click
            SetContactInfo()
            Response.Redirect("/pay/payment.aspx", False)
        End Sub
#Region " ::: Methods ::: "
        ''' <summary>
        ''' Validate if this page need to be rendered
        ''' </summary>
        Private Function IsContactInfoNeedToBePopulated() As Boolean

        End Function
        ''' <summary>
        ''' Get Contact Info
        ''' </summary>
        Private Sub PopulateContactInfo()
            If Not BLL.SessionManager.ContactInfo Is Nothing Then
                With BLL.SessionManager.ContactInfo
                    txtContactName.Text = .ContactName
                    txtContactAddress1.Text = .Address1
                    txtContactAddress2.Text = .Address2
                    ddlContactCountry.SelectedValue = .Country
                    ddlContactState.SelectedValue = .State
                    txtContactCity.Text = .City
                    txtContactZip.Text = .Zip
                    txtContactPhone.Text = .Phone
                End With
            End If
        End Sub
        Private Sub SetContactInfo()
            Dim contactInfo As New B2P.Cart.ContactInfo()
            contactInfo.ContactName = txtContactName.Text
            contactInfo.Address1 = txtContactAddress1.Text
            contactInfo.Address2 = txtContactAddress2.Text
            contactInfo.Country = ddlContactCountry.SelectedValue
            contactInfo.State = ddlContactState.SelectedValue
            contactInfo.City = txtContactCity.Text
            contactInfo.Zip = txtContactZip.Text
            contactInfo.Phone = txtContactPhone.Text
            BLL.SessionManager.ContactInfo = contactInfo
        End Sub
        ''' <summary>
        ''' Adds client side javascript to the various server controls.
        ''' </summary>
        Private Sub RegisterClientJs()
            btnContinue.Attributes.Add("onClick", "return validateForm()")
        End Sub
        ''' <summary>
        ''' Bind State from XML
        ''' </summary>
        Private Sub BindState()
            Dim StateAbbr As String = ddlContactCountry.SelectedValue
            If Not BLL.SessionManager.ContactInfo Is Nothing Then
                StateAbbr = BLL.SessionManager.ContactInfo.Country
            End If
            If String.IsNullOrEmpty(StateAbbr) Then Exit Sub
            ddlContactState.Items.Clear()
            ddlContactState.Items.Add(New ListItem("--Select--", ""))
            If StateAbbr.Equals("US") Then
                If ConfigurationManager.AppSettings("UnitedStates") IsNot Nothing Then
                    Dim UnitedStatesPath = ConfigurationManager.AppSettings("UnitedStates")

                    If Not UnitedStatesPath.StartsWith("~/") Then
                        UnitedStatesPath = String.Format("~/{0}", UnitedStatesPath)
                    End If
                    UnitedStatesPath = Server.MapPath(UnitedStatesPath)


                    Dim _USStates = ReadStateXML(UnitedStatesPath)
                    For Each item As KeyValuePair(Of String, String) In _USStates
                        Dim element = New ListItem(item.Value, item.Key)
                        element.Attributes("OptionGroup") = "USA"
                        ddlContactState.Items.Add(element)
                    Next

                End If
            ElseIf StateAbbr.Equals("CA") Then
                If ConfigurationManager.AppSettings("CanadianProvinces") IsNot Nothing Then
                    Dim CanadianProvincessPath = ConfigurationManager.AppSettings("CanadianProvinces")

                    If Not CanadianProvincessPath.StartsWith("~/") Then
                        CanadianProvincessPath = String.Format("~/{0}", CanadianProvincessPath)
                    End If
                    CanadianProvincessPath = Server.MapPath(CanadianProvincessPath)

                    Dim _USStates = ReadStateXML(CanadianProvincessPath)
                    For Each item As KeyValuePair(Of String, String) In _USStates
                        Dim element = New ListItem(item.Value, item.Key)
                        element.Attributes("OptionGroup") = "Canada"
                        ddlContactState.Items.Add(element)
                    Next
                End If
            ElseIf StateAbbr.Equals("OT") Then
                ddlContactState.Items.Add("Öther")
            End If
        End Sub
        ''' <summary>
        ''' Bind Countries
        ''' </summary>
        Private Sub BindCountries()
            ddlContactCountry.Items.Clear()
            ddlContactCountry.Items.Add(New ListItem("--Select--", ""))

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
                    ddlContactCountry.Items.Add(element)
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