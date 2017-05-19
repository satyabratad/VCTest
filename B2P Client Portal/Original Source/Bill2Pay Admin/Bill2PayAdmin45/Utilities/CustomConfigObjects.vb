'==========================================================================
' File:     CustomConfigObjects.vb
' Date:     07.14.2016
' Author:   Scott Leonard
'  
' Summary:  EMV transaction swiper process configuration info:

'           Client item attributes:
'               code:
'                   Must be the same as the ClientCode in the database.
'               useBinLookup:
'                   Whether the client uses a BIN lookup to determine the 
'                   card type used.
'               useSingleFee:
'                   Whether the client uses a single fee for different
'                   types of transactions (i.e., Credit, PIN Debit, etc).
'
'          
'           New attributes should be added to the lender configuration
'           class in the form of a property.
'
' TODO:     
'
'==========================================================================
Imports System.Configuration

''' <summary>
''' Represents the custom configuration section within the configuration file.
''' </summary>
Public Class ClientSection : Inherits ConfigurationSection

    <ConfigurationProperty("clients", IsRequired:=True, IsDefaultCollection:=True)>
    Public Property Clients() As ClientCollection
        Get
            Return DirectCast(Me("clients"), ClientCollection)
        End Get
        Set(ByVal value As ClientCollection)
            Me("clients") = value
        End Set
    End Property

End Class

''' <summary>
''' Represents the collection of Client elements within the configuration file.
''' </summary>
Public Class ClientCollection : Inherits ConfigurationElementCollection

    Public Sub Add(ByVal client As Client)
        MyBase.BaseAdd(client)
    End Sub

    Public Overrides ReadOnly Property CollectionType() As ConfigurationElementCollectionType
        Get
            Return ConfigurationElementCollectionType.BasicMap
        End Get
    End Property

    Protected Overloads Overrides Function CreateNewElement() As System.Configuration.ConfigurationElement
        Return New Client()
    End Function

    Protected Overrides ReadOnly Property ElementName() As String
        Get
            Return "client"
        End Get
    End Property

    Protected Overrides Function GetElementKey(ByVal element As System.Configuration.ConfigurationElement) As Object
        Return DirectCast(element, Client).Code
    End Function

    Public Overloads ReadOnly Property Item(ByVal elementKey As Object) As Client
        Get
            Return DirectCast(BaseGet(elementKey), Client)
        End Get
    End Property

End Class

''' <summary>
''' Represents a Client configuration element within the configuration file.
''' </summary>
Public Class Client : Inherits ConfigurationElement

    <ConfigurationProperty("code", IsRequired:=True, IsKey:=True)>
    Public Property Code() As String
        Get
            Return DirectCast(Me("code"), String)
        End Get
        Set(ByVal value As String)
            Me("code") = value
        End Set
    End Property

    <ConfigurationProperty("useBinLookup", IsRequired:=True)>
    Public Property UseBinLookup() As Boolean
        Get
            Return DirectCast(Me("useBinLookup"), Boolean)
        End Get
        Set(ByVal value As Boolean)
            Me("useBinLookup") = value
        End Set
    End Property

    <ConfigurationProperty("useSingleFee", IsRequired:=True)>
    Public Property UseSingleFee() As Boolean
        Get
            Return DirectCast(Me("useSingleFee"), Boolean)
        End Get
        Set(ByVal value As Boolean)
            Me("useSingleFee") = value
        End Set
    End Property

    '<ConfigurationProperty("someAttribute", IsRequired:=True)> _
    'Public Property SomeAttribute() As SomeType
    '    Get
    '        Return DirectCast(Me("someAttribute"), SomeType)
    '    End Get
    '    Set(ByVal value As SomeType)
    '        Me("someAttribute") = value
    '    End Set
    'End Property

End Class