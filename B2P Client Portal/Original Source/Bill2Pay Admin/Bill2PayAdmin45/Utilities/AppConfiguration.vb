'==========================================================================
' File:     AppConfiguration.vb
' Date:     06.10.2016
' Author:   Scott Leonard
'  
' Summary:  Provides strongly typed properties for the various 
'           application settings in the web.config file.
'
' TODO:     
'
'==========================================================================
Imports System
Imports System.Configuration


Public NotInheritable Class AppConfiguration

#Region " ::: Properties ::: "

    Public Shared ReadOnly Property ClientLogoPath As String
        Get
            Return ConfigurationManager.AppSettings("ClientLogoPath")
        End Get
    End Property

    Public Shared ReadOnly Property DebugLogging As Boolean
        Get
            Return Convert.ToBoolean(ConfigurationManager.AppSettings("DebugLogging"))
        End Get
    End Property

    Public Shared ReadOnly Property DomainWhitelist As String
        Get
            Return ConfigurationManager.AppSettings("DomainWhitelist")
        End Get
    End Property

    Public Shared ReadOnly Property TestMode As Boolean
        Get
            Return Convert.ToBoolean(ConfigurationManager.AppSettings("TestMode"))
        End Get
    End Property

#End Region

End Class
