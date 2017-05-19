<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="ReportViewer.aspx.vb" Inherits="Bill2PayAdmin45.ReportViewer" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Reports :: Bill2Pay Administration</title>
    <link rel="stylesheet" type="text/css" href="../css/main.css" media="all" />
    <link rel="stylesheet" type="text/css" href="../css/content_normal.css" title="normal" />
    <link rel="stylesheet" type="text/css" href="../Content/themes/base/minified/jquery.ui.all.min.css" />   
    
    <script language="javascript" type="text/javascript">

        function openNewWin(url) {
            var x = window.open(url, 'mynewwin', 'resizable=yes,toolbar=no,scrollbars=yes,menubar=no');
            x.focus();
        }

</script>
    
    
    
</head>
<body>
    <form id="form1" runat="server">
        <div class="top_header_main"></div>
        <custom:menu ID="menu" runat="server" />
        <div class="top_header_shadow"></div>
          
        <div class="content">
           <custom:logout ID="logout" runat="server" />
           <div class="header"><asp:Label ID="lblTitle" runat="server"></asp:Label></div>
           <div class="header_border"></div>
           
           <asp:ToolkitScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="true" />
           <des:PageManager ID="PageManager1" runat="server" AJAXFramework="MicrosoftAJAX" />
           
           <!--Start Main Content-->
           <div class="main_content">
     
                <asp:Panel ID="pnlClientCode" runat="server" Visible="false">
                    Client Code:
                    <telerik:RadComboBox ID="ddlAUClient" OnSelectedIndexChanged="ddlAUClient_SelectedIndexChanged" AppendDataBoundItems="true" Runat="server" Skin="Windows7" tabindex="1" AutoPostBack="true" CausesValidation="false">
                        <Items>
                            <telerik:RadComboBoxItem Text="Select One" Value="" Font-Italic="true" />
                        </Items>
                  </telerik:RadComboBox>
                </asp:Panel>                 
                <br />
                
                <fieldset>
                    <legend>Report Selection</legend>
                    <br />
                    
                    <table>
                        <tr>
                            <td style="width:30%" valign="top">
                                <p class="padding" style="font-style:italic; padding-bottom:5px; border-bottom: dotted 1px black;">1. Select Report &#8659</p><br />
                                <telerik:RadListBox runat="server" ID="RadListBox1" Width="250px" Height="200px" 
                                                    CssClass="padding" AutoPostBack="true" DataTextField="Name" DataValueField="ReportID"                                                    
                                                    OnSelectedIndexChanged="RadListBox1_SelectedIndexChanged">
                                </telerik:RadListBox>
                            </td>
                            <td valign="top" style="width:40%">
                                <p class="padding" style="font-style:italic; padding-bottom:5px; border-bottom: dotted 1px black;">2. Enter Parameters &#8659</p><br />
                                <asp:Panel ID="ParametersPanel" runat="server"></asp:Panel>
                            </td>
                            <td valign="top" style="width:20%">
                                <p class="padding" style="font-style:italic; padding-bottom:5px; border-bottom: dotted 1px black;">3. Create Report &#8659</p><br />
                                <span style="padding-left:40px;">
                                    <des:ImageButton id="btnSubmit" tooltip="Generate Report" imageurl="/images/btnGenerateReport.jpg" runat="server" text="Generate Report"
                                        OnClick="btnSubmit_Click" />
                                </span>
                            </td>
                        </tr>
                    </table>
                    
                    <br /> 
                </fieldset>                
           </div>
        </div>
        <custom:timeout runat="server" ID="timeout" />
    </form>
</body>
</html>
