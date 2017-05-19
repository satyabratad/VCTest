<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="ProfileDisabled.aspx.vb" Inherits="Bill2PayAdmin45.ProfileDisabled" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Profile Search :: Bill2Pay Administration</title>
    <link rel="stylesheet" type="text/css" href="../css/main.css" media="all" />
    <link rel="stylesheet" type="text/css" href="../css/content_normal.css" title="normal" />
    <link rel="stylesheet" type="text/css" href="../Content/themes/base/minified/jquery.ui.all.min.css" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:ToolkitScriptManager ID="ScriptManager1" runat="server" />
        <div class="top_header_main"></div>
        <custom:menu ID="menu" runat="server" />
        <div class="top_header_shadow"></div>
      
        <div class="content">
            <custom:logout ID="logout" runat="server" />
            <div class="header">Profile Results</div>
            <div class="header_border"></div>
           
            <!--Start Main Content-->
            <div class="main_content">
              <p>The profile has been disabled.</p> 
                
                 <img src="/images/spacer.gif" alt="spacer" height="200" />  
            </div>
        </div> 
        <custom:timeout ID="timeoutControl" runat="server" />

    </form>      
    <div class="clear"></div> 
    <div class="footer_shadow"><custom:footer ID="Footer" runat="server" /></div>
</body>
</html>
