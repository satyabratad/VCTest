<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="return.aspx.vb" Inherits="Bill2PayAdmin45._return" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
  <head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Payment Receipt :: Bill2Pay Administration</title>
    <link rel="stylesheet" type="text/css" href="../css/main.css" media="all" />
    <link rel="stylesheet" type="text/css" href="../css/content_normal.css" title="normal" />
    <link rel="stylesheet" type="text/css" href="../Content/themes/base/minified/jquery.ui.all.min.css" />
    
</head>
<body>
<script type="text/javascript" language="javascript">

    function Clickheretoprint(type) {
        var disp_setting = "toolbar=yes,location=no,directories=yes,menubar=yes,";
        disp_setting += "scrollbars=yes, left=100, top=25";
        var content_vlue = document.getElementById("print_content").innerHTML;

        var docprint = window.open("", "", disp_setting);
        docprint.document.open();
        docprint.document.write('<html><head><title>Payment Receipt</title>');
        if (type == "Normal") {
            docprint.document.write('<link rel="stylesheet" href="../css/print.css">')
        }
        else {
            docprint.document.write('<link rel="stylesheet" href="../css/printReceipt.css">');
        }
        docprint.document.write('</head><body onLoad="self.print()">');
        docprint.document.write('<div id="pageheader"> ');
        docprint.document.write('Payment Receipt</div>');
        docprint.document.write(content_vlue);
        docprint.document.write('</body><p style="padding-left:0px;"><a href="javascript:self.close();"  >Close</a></p></html>');
        docprint.document.close();
        docprint.focus();
    }
        

 </script>    
 <form id="form1" runat="server">

  <div class="top_header_main"></div>
  <custom:menu ID="menu" runat="server" />

  <div class="top_header_shadow"></div>
  
  <div class="content">
   <custom:logout ID="logout" runat="server" />
    <div class="header">Make a Payment - Receipt</div>
    <div class="header_border"></div>
   
    
    
    <asp:ToolkitScriptManager ID="ScriptManager1" runat="server" />

  
   
      
    
    
    <!--StartContent-->
    <div class="main_content">
    <fieldset style="width:450px;">
    <asp:Panel ID="pnlResults" runat="server">
        <legend>Payment Receipt - <a href="javascript:Clickheretoprint('Normal')"> Print Full Page</a>&nbsp;&nbsp;&nbsp;<a href="javascript:Clickheretoprint('Receipt')">Print Receipt</a></legend>
        <br />    
    <div id="print_content">
        <p style="font-size:medium; margin-left:25px; font-weight:bold;"></p>
         <table  style="margin-left:10px;" cellspacing="4" class="ConfTable">
           
            <tr>
                <td style="color:#336699; ">Confirmation Number:</td>
                <td style="color:#336699;"><asp:Literal ID="litConfirmationNumber" runat="server"></asp:Literal></td>
            </tr>
             <tr>
                <td>Payment For:</td>
                <td><asp:Literal ID="litClientName" runat="server"></asp:Literal></td>
            </tr>
            <tr>
                <td>Office:</td>
                <td><asp:Literal ID="litOfficeName" runat="server"></asp:Literal></td>
            </tr>
            <tr>
                <td>Status:</td>
                <td><asp:literal ID="litStatus" runat="server"></asp:literal></td>
            </tr>
            <tr>
                <td>Transaction Date:</td>
                <td><asp:literal ID="litDate" runat="server"></asp:literal></td>
            </tr>
            <tr>
                <td>Payment Type:</td>
                <td><asp:literal ID="litType" runat="server"></asp:literal></td>
            </tr>
            <tr>
                <td valign="top">Product Detail:</td>
                <td><asp:Literal ID="litItems" runat="server"></asp:Literal></td>
            </tr>
            <tr>
                <td>Convenience Fee:</td>
                <td><asp:Literal ID="litFee" runat="server"></asp:Literal></td>
            </tr>
            <tr>
                <td>Total Payment Amount:</td>
                <td><asp:Literal ID="litTotal" runat="server"></asp:Literal></td>
            </tr>
            <tr>
                <td>Name:</td>
                <td><asp:Literal ID="litName" runat="server"></asp:Literal></td>
            </tr>
            <tr>
                <td>Phone:</td>
                <td><asp:Literal ID="litPhone" runat="server"></asp:Literal></td>
            </tr>
            <tr>
                <td>Notes:</td>
                <td><asp:Literal ID="litNotes" runat="server"></asp:Literal></td>
            </tr>
            
            <tr>
                <td valign="top">Message:</td>
                <td><asp:Literal ID="litClientMessage" runat="server"></asp:Literal>
                    <asp:Literal ID="litPostBackMessage" runat="server"></asp:Literal>
                </td>
            </tr>
            
            <asp:Panel ID="pnlCommercial" runat="server" Visible="false">
                <tr>
                    <td>Originator ID:</td>
                    <td><asp:Literal ID="litOriginatorID" runat="server"></asp:Literal></td>
                </tr>
            </asp:Panel>
            <tr>
               
                <td colspan="2">
                    <table width="85%" cellspacing="0" style="margin-top:30px;">
                        <tr>
                            <td style="border-top:solid 1px black;">Signature</td>
                            <td>&nbsp;</td>
                            <td style="border-top:solid 1px black;">Date</td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <div id="SystemMessage" style="font-style:italic; padding-top:20px; font-size:smaller;">
                        <asp:Literal ID="litClientName2" runat="server"></asp:Literal><br />
                        <asp:Literal ID="litSystemMessage" runat="server" ></asp:Literal>

                    </div></td>
            </tr>
         </table>
    </div>
    <br />
    
    </asp:Panel>
    
    <asp:Panel ID="pnlNoResults" runat="server"  Visible="false">
    <legend>No Results</legend>
        <br />
        <p class="padding">Oops, there are no payment results to display.</p>
        <br />
    </asp:Panel>
  </fieldset>  
  
    
  
 
            
            
   </div>
    <!--EndContent-->
  
 
 
 
   <br />
  </div>
        <custom:timeout ID="timeoutControl" runat="server" />
  
</form>
      
      <div class="clear"></div> 
      <div class="footer_shadow">
        <custom:footer ID="Footer" runat="server" />
      </div>
</body> 
</html> 