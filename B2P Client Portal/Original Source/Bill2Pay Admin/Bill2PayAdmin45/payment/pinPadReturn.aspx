<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="pinPadReturn.aspx.vb" Inherits="Bill2PayAdmin45.pinPadReturn" %>

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
                <legend>
                    Payment Details -  <a href="javascript:Clickheretoprint('Normal')"> Print Full Page</a>&nbsp;&nbsp;&nbsp;<a href="javascript:Clickheretoprint('Receipt')">Print Receipt</a>
                </legend>
                
                <asp:Panel ID="pnlResults" runat="server">
                                        <br />    
                    <div id="print_content">
                        <p style="font-size:medium; margin-left:0px; font-weight:bold;"><asp:Literal ID="litClientName" runat="server"></asp:Literal></p>
                       <table style="margin-left:10px;" cellspacing="4" class="ConfTable" >
                            <tr>
                                <td>Transaction Date:</td>
                                <td style="text-align:right"><asp:literal ID="litDate" runat="server"></asp:literal></td>
                            </tr>
                            <tr>
                                <td>Name:</td>
                                <td style="text-align:right"><asp:Literal ID="litName" runat="server"></asp:Literal></td>
                            </tr>  
                            <tr>
                                <td>Payment Type:</td>
                                <td style="text-align:right"><asp:literal ID="litType" runat="server"></asp:literal></td>
                            </tr>
                           <asp:Panel ID="pnlCommercial" runat="server" Visible="false">
                            <tr>
                                <td>Originator ID:</td>
                                <td style="text-align:right"><asp:Literal ID="litOriginatorID" runat="server"></asp:Literal></td>
                            </tr>
                            </asp:Panel>
                            <tr>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                               
                            </tr>  
                        </table>
                    
                      <table style="margin-left:10px;" cellspacing="4"  class="ConfTable">
                            <asp:Panel ID="pnlTax" runat="server" Visible="false">
                                <tr>
                                    <td><b>Tax Items:</b></td>
                                    <td>&nbsp;</td>
                                </tr>
                                
                                <tr>
                                    <td>Confirmation Number:</td>
                                    <td style="text-align:right"><asp:literal ID="litTaxConfirm" runat="server"></asp:literal></td>
                                </tr>
                               
                                    <tr>
                                        <td>Authorization Code:</td>
                                        <td style="text-align:right"><asp:literal ID="litTaxAuth" runat="server"></asp:literal></td>
                                    </tr>
                                
                                 <tr>
                                    <td>Tax Items Amount:</td>
                                    <td style="text-align:right"><asp:literal ID="litTaxAmt" runat="server"></asp:literal></td>
                                </tr>
                                <tr>
                                    <td>Convenience Fee:</td>
                                    <td style="text-align:right"><asp:literal ID="litTaxFee" runat="server"></asp:literal></td>
                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                </tr>            
                            </asp:Panel>
                        
                            <asp:Panel ID="pnlNonTax" runat="server" Visible="false">
                                <tr>
                                    <td><b>Non-Tax Items:</b></td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td>Confirmation Number:</td>
                                    <td style="text-align:right"><asp:literal ID="litNonTaxConfirm" runat="server"></asp:literal></td>
                                </tr>
                                 <asp:panel id="pnlAuthCode" runat="server">
                                  <tr>
                                    <td>Authorization Code:</td>
                                    <td style="text-align:right"><asp:literal ID="litNonTaxAuth" runat="server"></asp:literal></td>
                                  </tr>
                                </asp:panel>
                                 <tr>
                                    <td>Non-Tax Items Amount:</td>
                                    <td style="text-align:right"><asp:literal ID="litNonTaxAmt" runat="server"></asp:literal></td>
                                </tr>
                                <tr>
                                    <td>Convenience Fee:</td>
                                    <td style="text-align:right"><asp:literal ID="litNonTaxFee" runat="server"></asp:literal></td>
                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                </tr>
                            </asp:Panel>
            
           
                            <tr>
                                <td><b>Total Payment Amount:</b></td>
                                <td style="text-align:right"><b><asp:Literal ID="litTotal" runat="server" ></asp:Literal></b></td>
                            </tr>
                            <tr>               
                                <td colspan="2">
                                    <br />
                                    <asp:Panel ID="pnlSignature" runat="server"> 
                                        <table style="margin-top:30px; width:100%; padding:0px">
                                            <tr>
                                                <td style="border-top:solid 1px black;">Signature</td>
                                                <td>&nbsp;</td>
                                                <td style="border-top:solid 1px black;">Date</td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </td>
                            </tr>            
                            <tr>
                                <td colspan="2">
                                    <div id="SystemMessage" style="font-style:italic; padding-top:20px; font-size:smaller;">
                                        <asp:Literal ID="litClientMessage" runat="server" ></asp:Literal>
                                    </div>
                                </td>
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