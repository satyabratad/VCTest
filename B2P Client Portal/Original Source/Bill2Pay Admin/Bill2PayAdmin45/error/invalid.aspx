<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="invalid.aspx.vb" Inherits="Bill2PayAdmin45.invalid" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Error :: Bill2Pay Administration</title>
    <link rel="stylesheet" type="text/css" href="../css/main.css" media="all" />
    <link rel="stylesheet" type="text/css" href="../css/content_normal.css" title="normal" />
    <link rel="stylesheet" type="text/css" href="../Content/themes/base/minified/jquery.ui.all.min.css" />
</head>

<body>
    <form id="form1" runat="server" >
        <asp:ToolkitScriptManager ID="ScriptManager1" runat="server" />
        <div class="top_header_main"></div>
        <custom:menu ID="menu" runat="server" />
        <div class="top_header_shadow"></div>
  
        <div class="content">
            <custom:logout ID="logout" runat="server" />
            <div class="header">Error - <asp:Literal ID="litErrorType" runat="server"></asp:Literal></div>
            <div class="header_border"></div>

            <!--StartContent-->
            <div class="main_content">
                <p><asp:Literal ID="litErrorMessage" runat="server"></asp:Literal></p>
                <img src="/images/spacer.gif" alt="spacer" height="200" />       
            </div>
            <!--EndContent-->
            
            <custom:timeout ID="timeoutControl" runat="server" />
        </div>
  
    </form>
      
    <div class="clear"></div> 
    <div class="footer_shadow"><custom:footer ID="Footer" runat="server" /></div>
</body>
</html>
