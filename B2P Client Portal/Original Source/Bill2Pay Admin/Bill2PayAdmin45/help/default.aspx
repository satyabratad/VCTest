<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="default.aspx.vb" Inherits="Bill2PayAdmin45._default2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Profile :: Bill2Pay Administration</title>
    <link rel="stylesheet" type="text/css" href="../css/main.css" media="all" />
    <link rel="stylesheet" type="text/css" href="../css/content_normal.css" title="normal" />
    <link rel="stylesheet" type="text/css" href="../Content/themes/base/minified/jquery.ui.all.min.css" />
</head>

<body>
    <form id="form1" runat="server">
        <div class="top_header_main"></div>
        <custom:menu ID="menu" runat="server" />
        <div class="top_header_shadow"></div>
  
        <div class="content">
            <custom:logout ID="logout" runat="server" />
            <div class="header">Help - Contact Us</div>
            <div class="header_border"></div>
    
            <asp:ToolkitScriptManager ID="ScriptManager1" runat="server" />

            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
          
                    <!--StartContent-->
                    <div class="main_content">    
                        <p>Please e-mail <a href="mailto:help@bill2pay.com">help@bill2pay.com</a> or call 727.524.3511 ext. 0 if you have any payments questions or need help with the system.</p>
                        <p>&nbsp;</p>
                        <%--<p><a href="/resource/jre-6u21-windows-i586-s.exe">Download the latest Java version</a></p>--%>
                        <img src="/images/spacer.gif" height="200" alt="" />            
                    </div>
                    <!--EndContent-->
  
  
                </ContentTemplate>
                <Triggers></Triggers>
            </asp:UpdatePanel>  
 
            <custom:timeout ID="timeoutControl" runat="server" />
        </div>
    </form>
      
    <div class="clear"></div> 
    <div class="footer_shadow"><custom:footer ID="Footer" runat="server" /></div>
</body> 
</html>
