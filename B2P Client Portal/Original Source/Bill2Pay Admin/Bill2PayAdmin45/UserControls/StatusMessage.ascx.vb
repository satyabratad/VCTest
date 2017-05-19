
Public Class StatusMessage : Inherits System.Web.UI.UserControl

#Region " ::: Control Event Handlers ::: "

        Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
            pnlStatusMessage.Attributes.Add("role", "alert")
        End Sub

#End Region

#Region "::: Methods :::"

    ''' <summary>
    ''' Updates a user bound message.
    ''' </summary>
    Public Sub ToggleStatusMessage(ByVal msg As String, ByVal msgType As StatusMessageType, ByVal showMsg As Boolean, ByVal showIcon As Boolean)
        ToggleStatusMessage(msg, msgType, StatusMessageSize.Normal, showMsg, showIcon)
    End Sub

    ''' <summary>
    ''' Updates a user bound message.
    ''' </summary>
    Public Sub ToggleStatusMessage(ByVal msg As String, ByVal msgType As StatusMessageType, ByVal msgSize As StatusMessageSize, ByVal showMsg As Boolean, ByVal showIcon As Boolean)
            Dim size As String = String.Empty
            Dim icon As String = String.Empty
            Dim data As String = String.Empty

            Select Case msgSize
                Case StatusMessageSize.ExtraSmall
                    size = " alert-xs"

                Case StatusMessageSize.Small
                    size = " alert-sm"
            End Select

            Select Case msgType
                Case StatusMessageType.Danger
                    pnlStatusMessage.CssClass = "alert alert-danger" & size
                    icon = "<div id=""imgStatusMsgIcon"" class=""fa fa-exclamation-circle fa-2x status-msg-icon""></div>"

                Case StatusMessageType.Info
                    pnlStatusMessage.CssClass = "alert alert-info" & size
                    icon = "<div id=""imgStatusMsgIcon"" class=""fa fa-info-circle fa-2x status-msg-icon""></div>"

                Case StatusMessageType.Success
                    pnlStatusMessage.CssClass = "alert alert-success" & size
                    icon = "<div id=""imgStatusMsgIcon"" class=""fa fa-check-circle-o fa-2x status-msg-icon""></div>"

                Case StatusMessageType.Warning
                    pnlStatusMessage.CssClass = "alert alert-warning" & size
                    icon = "<div id=""imgStatusMsgIcon"" class=""fa fa-exclamation-triangle fa-2x status-msg-icon""></div>"

                Case StatusMessageType.None
                    pnlStatusMessage.CssClass = String.Empty
                    icon = "<div id=""imgStatusMsgIcon""></div>"

                Case Else
                    pnlStatusMessage.CssClass = Nothing
            End Select

            If showIcon Then
                data = icon & "<div id=""txtStatusMsg"" class=""status-msg-text"">" & msg & "</div>"
            Else
                data = msg
            End If

            litStatusMessage.Text = data
            litStatusMessage.Visible = showMsg
            pnlStatusMessage.Visible = showMsg
        End Sub

#End Region

    End Class

