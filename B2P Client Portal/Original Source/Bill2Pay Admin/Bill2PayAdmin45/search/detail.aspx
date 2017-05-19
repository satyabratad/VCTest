<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="detail.aspx.vb" Inherits="Bill2PayAdmin45.detail" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Transaction Detail :: Bill2Pay Administration</title>
    <link rel="stylesheet" type="text/css" href="../css/main.css" media="all" />
    <link rel="stylesheet" type="text/css" href="../css/content_normal.css" title="normal" />
    <link rel="stylesheet" type="text/css" href="../Content/themes/base/minified/jquery.ui.all.min.css" />
</head>

<body>
    <script type="text/javascript" lang="javascript">
        function Clickheretoprint() {
            var disp_setting = "toolbar=yes,location=no,directories=yes,menubar=yes,";
            disp_setting += "scrollbars=yes, left=100, top=25";
            var content_vlue = document.getElementById("print_content").innerHTML;

            var docprint = window.open("", "", disp_setting);
            docprint.document.open();
            docprint.document.write('<html><head><title>Payment Receipt</title>');
            docprint.document.write('<link rel="stylesheet" href="../css/print.css">');
            docprint.document.write('</head><body onLoad="self.print()">');
            docprint.document.write('<div id="pageheader"> ');
            docprint.document.write('Payment Receipt</div>');
            docprint.document.write(content_vlue);
            docprint.document.write('</body><p style="padding-left:310px;"><a href="javascript:self.close();"  >Close</a></p></html>');
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
            <div class="header">Search - Transaction Detail</div>
            <div class="header_border"></div>
            <asp:ToolkitScriptManager ID="ScriptManager1" runat="server" />

            <!--StartContent-->
            <div class="main_content">
                <a href="/search/"><< Back to search results</a>
                
                <asp:Panel ID="pnlResults" runat="server">
                    <table style="width:95%; padding:4px; margin-left:5px;">
                        <tr>
                            <td style="width:50%;">
                                <fieldset>
                                    <legend>Payment Receipt - <a href="javascript:Clickheretoprint()" class="print">Click here to print</a></legend>
                                    <br />    
                                    <div id="print_content">
                                        <table style="margin-left:10px; width:90%; border-spacing: 4px">
                                            <tr>
                                                <td style="width:35%; color:#336699; ">Confirmation Number:</td>
                                                <td style="color:#336699; width:85%;"><asp:Literal ID="litConfirmationNumber" runat="server"></asp:Literal></td>
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
                                                <td style="vertical-align: top">Product Detail:</td>
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
                                                <td style="vertical-align:top">Notes:</td>
                                                <td>
                                                    <asp:textbox ID="txtNotes" runat="server" Width="300" TextMode="MultiLine" Wrap="true" ReadOnly="true" BorderWidth="0" BorderStyle="none" style="overflow:hidden;"></asp:textbox>

                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                    <br />
                                </fieldset>         
                            </td>
         
                            <asp:UpdatePanel id="UpdatePanel1" runat="server">
                                <contenttemplate>
                                    <td style="width:50%; vertical-align:top">
                                        <fieldset>
                                            <legend>Transaction Activities</legend>
                                            <br />
                                            <span class="padding">
                                                <asp:Button ID="btnVoid" Visible="true" Text="Void" runat="server" Enabled="false" CausesValidation="false" CssClass="buttons" />
                                                <asp:Button ID="btnReturn" Visible="true" Text="Refund" runat="server" Enabled="false" CausesValidation="false" CssClass="buttons" />
                                                <asp:Button id="btnNotes" Visible="true" Text="Add Note" Enabled="true" runat="server" CausesValidation="false" CssClass="buttons" />
                                                <asp:Button id="btnEmail" Visible="true" Text="Email Confirmation" Enabled="false" runat="server" CausesValidation="false" CssClass="buttons" />
                                                <br /><br />
                                                <span style="font-style:italic; padding-left:20px; color:Blue;"><asp:Literal ID="litMessage" runat="server"></asp:Literal></span>
                                            </span>
                                            <br /><br />            
                                        </fieldset> 
          
                                        <asp:ConfirmButtonExtender ID="ConfirmButtonExtender1" runat="server" TargetControlID="btnReturn" ConfirmText="" DisplayModalPopupID="ModalPopupExtender1"  />
                                        <br />
                                        <asp:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="btnReturn" PopupControlID="PNL" OkControlID="ButtonOk" CancelControlID="ButtonCancel" BackgroundCssClass="modalBackground" />
                                        
                                        <asp:Panel ID="PNL" runat="server" style="display:none; width:250px; background-color:White; border-width:2px; border-color:Black; border-style:solid; padding:20px;">
                                            <div style="font-family:Arial; font-size:12pt;">Are you sure you want to refund this transaction?</div>
                                            <br /><br />
                                            <div style="text-align:right; font-family:Arial; font-size:12pt;">
                                                <asp:Button ID="ButtonOk" runat="server" Text="OK" CausesValidation="false" CssClass="buttons" />
                                                <asp:Button ID="ButtonCancel" runat="server" Text="Cancel"  CausesValidation="false" CssClass="buttons" />
                                            </div>
                                        </asp:Panel> 
           
                                        <asp:ConfirmButtonExtender ID="ConfirmButtonExtender2" runat="server" TargetControlID="btnVoid" ConfirmText="" DisplayModalPopupID="ModalPopupExtender2"  />
                                        <br />
                                        <asp:ModalPopupExtender ID="ModalPopupExtender2" runat="server" TargetControlID="btnVoid" PopupControlID="PNL2" OkControlID="ButtonOk2" CancelControlID="ButtonCancel2" BackgroundCssClass="modalBackground" />
                                        
                                        <asp:Panel ID="PNL2" runat="server" style="display:none; width:250px; background-color:White; border-width:2px; border-color:Black; border-style:solid; padding:20px;">
                                            <div style="font-family:Arial; font-size:12pt;">Are you sure you want to void this 
                                                transaction?</div>
                                            <br /><br />
                                            <div style="text-align:right; font-family:Arial; font-size:12pt;">
                                                <asp:Button ID="ButtonOk2" runat="server" Text="OK" CausesValidation="false" CssClass="buttons" />
                                                <asp:Button ID="ButtonCancel2" runat="server" Text="Cancel" CausesValidation="false" CssClass="buttons" />
                                            </div>
                                        </asp:Panel>          
                                        <br />
          
                                        <asp:ModalPopupExtender ID="ModalPopupExtender3" runat="server" TargetControlID="btnNotes" PopupControlID="PNL3" CancelControlID="ButtonCancel3" BackgroundCssClass="modalBackground" />
                                        <asp:Panel ID="PNL3" runat="server" style="display:none; width:400px; background-color:White; border-width:2px; border-color:Black; border-style:solid; padding:20px;">
                                            <div style="font-family:Arial; font-size:11pt;">Add Note Description:</div>                            
                                            <asp:TextBox ID="txtNotesAdd" runat="server" TextMode="MultiLine" Width="350" Rows="5" MaxLength="500"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtNotesAdd" ValidationGroup="Notes" ErrorMessage="Required"/>
                                            <asp:FilteredTextBoxExtender ID="txtNotesAdd_FilteredTextBoxExtender1" runat="server"
                                                TargetControlID="txtNotesAdd" FilterType="Custom, LowercaseLetters, UppercaseLetters, Numbers"
                                                ValidChars=" ,.'$#!()">
                                            </asp:FilteredTextBoxExtender>
                                            <div style="text-align:left; font-family:Arial; font-size:12pt; padding-top:5px;">
                                                <asp:Button ID="ButtonOk3" runat="server" Text="OK" CausesValidation="true" ValidationGroup="Notes" CssClass="buttons" />
                                                <asp:Button ID="ButtonCancel3" runat="server" Text="Cancel"  CausesValidation="false" CssClass="buttons"  />
                                            </div>
                                        </asp:Panel>  
                  
                                        <asp:ModalPopupExtender ID="ModalPopupExtender4" runat="server" TargetControlID="btnEmail" PopupControlID="PNL4" CancelControlID="ButtonCancel4" BackgroundCssClass="modalBackground" />
                                        <asp:Panel ID="PNL4" runat="server" style="display:none; width:400px; background-color:White; border-width:2px; border-color:Black; border-style:solid; padding:20px;">
                                            <div style="font-family:Arial; font-size:11pt;">Email Address:</div>
                                            <asp:TextBox ID="txtEmail" runat="server" Width="300"  MaxLength="50"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="reqEmail" runat="server" ControlToValidate="txtEmail" ValidationGroup="Email" ErrorMessage="Required"/>
                                            <asp:RegularExpressionValidator ID="regEmail" runat="server" ControlToValidate="txtEmail" Display="Dynamic" 
                                                ErrorMessage="Invalid Email Address" ToolTip="Invalid Email Address" SetFocusOnError="True" 
                                                 ValidationGroup="Email"
                                                ValidationExpression="<$|^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,6}$"></asp:RegularExpressionValidator>
                                            <asp:FilteredTextBoxExtender ID="tbEmailAddress_FilteredTextBoxExtender" 
                                                runat="server" filtertype="Numbers,UpperCaseLetters,LowerCaseLetters,Custom" TargetControlID="txtEmail" 
                                                ValidChars="-@._">
                                            </asp:FilteredTextBoxExtender>
                                            <div style="text-align:left; font-family:Arial; font-size:12pt; padding-top:5px;">
                                                <asp:Button ID="ButtonOk4" runat="server" Text="Send Email" CausesValidation="true" ValidationGroup="Email" CssClass="buttons" />
                                                <asp:Button ID="ButtonCancel4" runat="server" Text="Cancel" CausesValidation="false" CssClass="buttons" />
                                            </div>
                                        </asp:Panel>              
                                        
                                        <fieldset>
                                            <legend>Additional Detail</legend>
                                            <br />
                                            <div id="Additional Detail" class="padding">
                                                Payment Taken By:&nbsp;<asp:Literal ID="litCreateUser" runat="server"></asp:Literal><br />
                                                Source:&nbsp;<asp:Literal ID="litSource" runat="server"></asp:Literal><br />
                                                <span style="padding-bottom:10px;"><asp:HyperLink ID="lnkAdditionalLink" runat="server" Target="_blank" Visible="false" /></span>
                                                <span style="padding-bottom:10px;"><asp:HyperLink ID="lnkProfileDetail" runat="server" Visible="false" Text="View Profile" /></span>
                                            </div>
                                            <br />            
                                        </fieldset>          
                                    </td>
                                </contenttemplate>
                            </asp:UpdatePanel>
                        </tr>
                        <tr>
                            <td></td>
                            <td></td>
                        </tr>
                    </table>

                    <asp:Panel ID="pnlNoDetail" runat="server" Visible="false">
                        <fieldset>
                            <legend>No Additional Details</legend>
                            <br />
                            <p class="padding">No additional details available for this transaction.</p>
                            <br />
                        </fieldset>
                    </asp:Panel>

                    <asp:Panel ID="pnlNotes" runat="server" Visible="true">
                        <fieldset>
                            <legend>Notes</legend>
                            <br />
                            <div>
                                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CssClass="padding" Width="90%" BorderStyle="Solid" BorderColor="Gray">
                                    <HeaderStyle BackColor="#DAE2E8" Font-Bold="True" Font-Size="Small" 
                                        Font-Italic="False" />
                    
                                    <Columns>
                                        <asp:BoundField AccessibleHeaderText="Note Description" DataField="Comments" 
                                            HeaderText="Note Description"  />
                                        <asp:BoundField AccessibleHeaderText="Date Created" DataField="CommentDate" 
                                            HeaderText="Date Created"  />
                                        <asp:BoundField AccessibleHeaderText="Created By" DataField="UserName" 
                                            HeaderText="Created By" />
                                    </Columns>
                    
                                </asp:GridView>
                            </div>
                            <br />
                        </fieldset>
                    </asp:Panel>
    
                    <asp:Panel ID="pnlACHDetails" runat="server" Visible="false">
                        <fieldset>
                            <legend>ACH Details</legend>
                            <br />
                            <table style="border:solid 1px gray; margin-left:20px; width:90%; border-spacing:0px">
                                <tr style="background-color:#DAE2E8;">
                                    <td>B2P Process Date</td>
                                    <td>Bank Account Number</td>
                                    <td>Routing Number</td>
                                    <td>Account Type</td>                   
                                </tr>
                                <tr>
                                    <td><asp:Literal ID="litACHDetailsProcessDate" runat="server" Text=""></asp:Literal></td>
                                    <td><asp:Literal ID="litACHDetailsBankAccountNumber" runat="server" Text="***234"></asp:Literal></td>
                                    <td><asp:Literal ID="litACHDetailsBankRoutingNumber" runat="server" Text="063000047"></asp:Literal></td>
                                    <td><asp:Literal ID="litACHDetailsBankAccountType" runat="server" Text="Personal Checking"></asp:Literal></td>                    
                                </tr>
                            </table>
                            <br />
                        </fieldset>
                    </asp:Panel>
    
                    <asp:Panel ID="pnlCCDetails" runat="server" Visible="false">
                        <fieldset>
                            <legend>Credit Card Details</legend>
                            <br />
                            <table style="border:solid 1px gray; margin-left:20px; width:90%; border-spacing:0px">
                                <tr style="background-color:#DAE2E8;">
                                    <td>B2P Process Date</td>
                                    <td>Authorization Code</td>
                                    <td>First 6 Card Numbers</td>
                                </tr>
                                <tr>
                                    <td><asp:Literal ID="litCCDetailsProcessDate" runat="server" Text="151"></asp:Literal></td>
                                    <td><asp:Literal ID="litCCDetailsAuthoCode" runat="server" Text="***111"></asp:Literal></td>
                                    <td><asp:Literal ID="litCCDetailsFirst6" runat="server" Text=""></asp:Literal></td>
                                </tr>
                            </table>
                            <br />
                        </fieldset>
                    </asp:Panel>
    
                    <asp:Panel ID="pnlACHReturn" Visible="false" runat="server">
                        <fieldset>
                            <legend>ACH Return Details</legend>
                            <br />
                            <table style="border:solid 1px gray; margin-left:20px; width:90%; border-spacing:0px;">
                                <tr style="background-color:#DAE2E8;">
                                    <td>Return Code</td>
                                    <td>Return Description</td>
                                    <td>Original Trace</td>
                                    <td>Return Trace</td>
                                    <td>Return Date</td>
                                </tr>
                                <tr>
                                    <td><asp:Literal ID="litACHReturnCode" runat="server" Text="123"></asp:Literal></td>
                                    <td><asp:Literal ID="litACHReturnDesc" runat="server" Text="Closed Account"></asp:Literal></td>
                                    <td><asp:Literal ID="litACHOriginalTrace" runat="server" Text="12344444423"></asp:Literal></td>
                                    <td><asp:Literal ID="litACHReturnTrace" runat="server" Text="102110000"></asp:Literal></td>
                                    <td><asp:Literal ID="litACHReturnDate" runat="server" Text=""></asp:Literal></td>
                                </tr>
                            </table><br />
                        </fieldset>
                    </asp:Panel>
    
                    <asp:Panel ID="pnlCredit" Visible="false" runat="server">
                        <fieldset>
                            <legend>Refund Details</legend>
                            <br />
                           <table style="border:solid 1px gray; margin-left:20px; width:90%; border-spacing:0px;">
                                <tr style="background-color:#DAE2E8;">
                                    <td>Refund Date</td>
                                    <td>Clear Credit Card</td>
                                    <td>Amount</td>
                                    <td>B2P Process Date</td>
                                    <td>Refunded By</td>
                                </tr>
                                <tr>
                                    <td><asp:Literal ID="litRefundCreditDate" runat="server" Text="01/15/2010"></asp:Literal></td>
                                    <td><asp:Literal ID="litRefundCreditCard" runat="server" Text="*465444"></asp:Literal></td>
                                    <td><asp:Literal ID="litRefundAmount" runat="server" Text="$125.44"></asp:Literal></td>
                                    <td><asp:Literal ID="litRefundProcessDate" runat="server" Text="01/25/2010"></asp:Literal></td>
                                    <td><asp:Literal ID="litRefundBy" runat="server" Text=""></asp:Literal></td>
                                </tr>
                            </table><br />
                        </fieldset>
                    </asp:Panel>
    
    
                    <asp:Panel ID="pnlVoid" Visible="false" runat="server">
                        <fieldset>
                            <legend>Voided Details</legend>
                            <br />
                           <table style="border:solid 1px gray; margin-left:20px; width:90%; border-spacing:0px;">
                                <tr style="background-color:#DAE2E8;">
                                    <td>Voided Date</td>
                                    <td>Voided By</td>
                                </tr>
                                <tr>
                                    <td><asp:Literal ID="litVoidDate" runat="server"></asp:Literal></td>
                                    <td><asp:Literal ID="litVoidBy" runat="server"></asp:Literal></td>
                                </tr>
                            </table>
                            <br />
                        </fieldset>    
                    </asp:Panel>
    
                    <asp:Panel ID="pnlFailed" Visible="false" runat="server">
                        <fieldset>
                            <legend>Failed Transaction Details</legend>
                            <br />
                            <table style="border:solid 1px gray; margin-left:20px; width:90%; border-spacing:0px;">
                                <tr style="background-color:#DAE2E8;">
                                    <td>Error Description</td>
                                    <td>AVS Code</td>
                                    <td>Billing Zip</td>
                                    <td>Item Authorization</td>
                                    <td>Fee Authorization</td>
                                    <td>Reverse Status</td>
                                </tr>
                                <tr>
                                    <td><asp:Literal ID="litFailDesc" runat="server"></asp:Literal></td>
                                    <td><asp:Literal ID="litFailAVS" runat="server"></asp:Literal></td>
                                    <td><asp:Literal ID="litFailZip" runat="server"></asp:Literal></td>
                                    <td><asp:Literal ID="litFailItemAuth" runat="server"></asp:Literal></td>
                                    <td><asp:Literal ID="litFailFeeAuth" runat="server"></asp:Literal></td>
                                    <td><asp:Literal ID="litReverseStatus" runat="server"></asp:Literal></td>
                                </tr>
                            </table>
                            <br />
                        </fieldset>    
                    </asp:Panel>
    
                    <asp:Panel ID="pnlUserFields" runat="server">
                        <fieldset>
                            <legend>Additional Transaction Details</legend>
                            <br />
                            <table style="border:none; margin-left:20px; width:90%; border-spacing:0px;">
                                <tr>
                                    <td><asp:Literal ID="litPayeeAddress" runat="server"></asp:Literal></td>
                                </tr>
                                <tr>
                                    <td><asp:Literal ID="litDemoAddress1" runat="server" Visible="false"></asp:Literal>
                                    <asp:Literal ID="litDemoAddress2" runat="server" Visible="false"></asp:Literal>
                                    <asp:Literal ID="litDemoCityStateZip" runat="server" Visible="false"></asp:Literal>
                                    <asp:Literal ID="litDemoHomePhone" runat="server" Visible="false"></asp:Literal>
                                    <asp:Literal ID="litDemoCellPhone" runat="server" Visible="false"></asp:Literal>
                                    <asp:Literal ID="litDemoWorkPhone" runat="server" Visible="false"></asp:Literal>
                                    <asp:Literal ID="litDemoEmailAddress" runat="server" Visible="false"></asp:Literal></td>
                                </tr>
                                <tr>
                                    <td><asp:Literal ID="litUserField1" runat="server" Visible="false"></asp:Literal>
                                    <asp:Literal ID="litUserField2" runat="server" Visible="false"></asp:Literal>
                                    <asp:Literal ID="litUserField3" runat="server" Visible="false"></asp:Literal>
                                    <asp:Literal ID="litUserField4" runat="server" Visible="false"></asp:Literal>
                                    <asp:Literal ID="litUserField5" runat="server" Visible="false"></asp:Literal></td>                    
                                </tr>
                            </table>
                            <br />
                        </fieldset>   
                    </asp:Panel>    
                </asp:Panel>
    
                <asp:Panel ID="pnlNoResults" runat="server"  Visible="false">
                    <fieldset>
                        <legend>No Results</legend>
                        <br />
                        <p class="padding">Oops, there are no transaction details to display.</p>
                        <br />
                    </fieldset>
                </asp:Panel>
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
