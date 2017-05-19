<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="textpaydetail.aspx.vb" Inherits="Bill2PayAdmin45.textpaydetail" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>TextPay Details :: Bill2Pay Administration</title>
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
            <div class="header">TextPay Details</div>
            <div style="padding-left:10px"><asp:HyperLink ID="hypBack" runat="server" NavigateUrl="/textpay/textpay.aspx" Text="&lt;&lt; Back to search results" /> </div>
            <div class="header_border"></div>
       
            <!--Start Main Content-->
            <div class="main_content">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <span style="font-style:italic;"><asp:Label ID="lblMessage" runat="server" Visible="true" /></span>
                        <div id="ProfileDetail">
                            <table cellpadding="2" width="100%">
                                <tr>
                                    <td valign="top">
                                        <table cellpadding="2">
                                            <tr>
                                                <td>Client Name</td><td>&nbsp;<asp:Label ID="lblClientName" runat="server" /></td>
                                            </tr>
                                            <tr>
                                                <td>Profile Id:</td><td>&nbsp;<asp:Label ID="lblProfileID" runat="server" /></td>
                                            </tr>
                                            <tr>
                                                <td>Username:</td><td>&nbsp;<asp:Label ID="lblUsername" runat="server" /></td>
                                            </tr>
                                            <tr>
                                                <td>Name:</td><td>&nbsp;<asp:Label ID="lblName" runat="server" /></td>
                                            </tr>
                                            <tr>
                                                <td>Email:</td><td>&nbsp;<asp:Label ID="lblEmail" runat="server" /></td>
                                            </tr>
                                            <tr>
                                                <td>Status:</td><td>&nbsp;<asp:Label ID="lblStatus" runat="server" /></td>
                                            </tr>
                                            <tr>
                                                <td>Phone Number:</td><td>&nbsp;<asp:Label ID="lblPhoneNumber" runat="server" /></td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td valign="top" align="center">
                                        <fieldset style="padding-top:0px;margin-top:0px;">
                                            <legend>Profile Activities</legend>
                                            <div style="padding:20px;">
                                                <des:Button ID="btnResetPwd" runat="server" CausesValidation="False" Text="Reset Password" />&nbsp;
                                                <des:Button ID="btnSendActivation" runat="server" CausesValidation="False" Text="Send Activation" />&nbsp;
                                                <des:Button ID="btnChangeEmail" runat="server" CausesValidation="False" Text="Change Email" />&nbsp;
                                                <des:Button ID="btnDisableProfile" runat="server" CausesValidation="False" Text="Disable Profile" />&nbsp;
                                                <des:Button ID="btnCancelTxtPmt" Enabled="false" runat="server" CausesValidation="False" Text="Cancel Text Payments" />&nbsp;                            
                                            </div>
                                        </fieldset>
                                        <br />
                                        <asp:Label ID="lblMessage2" Font-Italic="true" runat="server" />
                                    </td>
                                </tr>     
                            </table>
                            <br />
                            
                            <asp:confirmbuttonextender ID="ConfirmButtonExtender1" runat="server" TargetControlID="btnResetPwd" ConfirmText="This is a test" DisplayModalPopupID="ModalPopupExtender1"  />
                            <br />
                            
                            <asp:modalpopupextender ID="ModalPopupExtender1" runat="server" TargetControlID="btnResetPwd" PopupControlID="Panel1" OkControlID="ButtonOk" CancelControlID="ButtonCancel" BackgroundCssClass="modalBackground" />
                            <asp:Panel ID="Panel1" runat="server" style="display:none; width:250px; background-color:White; border-width:2px; border-color:Black; border-style:solid; padding:20px;">
                                <div style="font-family:Arial; font-size:10pt;">
                                    Password reset will be generated and emailed to the address on file with this profile.
                                    Do you wish to continue?
                                </div>
                                <br /><br />
                
                                <div style="text-align:center; font-family:Arial; font-size:10pt;">
                                    <asp:Button ID="ButtonOk" runat="server" Text="OK" />
                                    <asp:Button ID="ButtonCancel" runat="server" Text="Cancel" />
                                </div>
                            </asp:Panel>
            
                            <asp:confirmbuttonextender ID="ConfirmButtonExtender2" runat="server" TargetControlID="btnSendActivation" ConfirmText="This is a test" DisplayModalPopupID="ModalPopupExtender2"  />
                            <br />
                            
                            <asp:modalpopupextender ID="ModalPopupExtender2" runat="server" TargetControlID="btnSendActivation" PopupControlID="Panel2" OkControlID="ButtonOk2" CancelControlID="ButtonCancel2" BackgroundCssClass="modalBackground" />
                            <asp:Panel ID="Panel2" runat="server" style="display:none; width:250px; background-color:White; border-width:2px; border-color:Black; border-style:solid; padding:20px;">
                                <div style="font-family:Arial; font-size:10pt;">
                                    A new activation link will be generated and emailed to the address on file with this profile.
                                    Do you wish to continue?
                                </div>
                                <br /><br />
                                
                                <div style="text-align:center; font-family:Arial; font-size:10pt;">
                                    <asp:Button ID="ButtonOk2" runat="server" Text="OK" />
                                    <asp:Button ID="ButtonCancel2" runat="server" Text="Cancel" />
                                </div>
                            </asp:Panel>
                            
                            <asp:confirmbuttonextender ID="ConfirmButtonExtender3" runat="server" TargetControlID="btnChangeEmail" ConfirmText="This is a test" DisplayModalPopupID="ModalPopupExtender3"  />
                            <br />
                            
                            <asp:modalpopupextender ID="ModalPopupExtender3" runat="server" TargetControlID="btnChangeEmail" PopupControlID="Panel3" CancelControlID="ButtonCancel3"  BackgroundCssClass="modalBackground" />
                            <asp:Panel ID="Panel3" runat="server" style="display:none; width:650px; background-color:White; border-width:2px; border-color:Black; border-style:solid; padding:20px;">
                                <div style="font-family:Arial; font-size:10pt;text-align:center;">
                                    <table>
                                        <tr>
                                            <td colspan="2"><asp:Label ID="lblError" Font-Italic="true" runat="server" ForeColor="#FF0000" /></td>
                                        </tr>
                                        <tr>
                                            <td style="width:170px; text-align:left">Enter new email address:</td>
                                            <td style="text-align:left">
                                                <des:TextBox ID="txtEmail" Width="300px" MaxLength="50" ValidationGroup="Email" CssClass="inputText" runat="server" />
                                                <des:EmailAddressValidator ID="EmailAddressValidator" IgnoreBlankText="false" ControlIDToEvaluate="txtEmail" ErrorMessage="* Invalid Email Address" Group="Email" runat="server">
                                                    <ErrorFormatterContainer>
                                                        <des:TextErrorFormatter Font-Size="9pt" Display="Static" />
                                                    </ErrorFormatterContainer>
                                                </des:EmailAddressValidator>                                
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">Re-type email address:</td>
                                            <td align="left">
                                                <des:TextBox ID="txtConfirmEmail" Width="300px" ValidationGroup="Email" MaxLength="50" CssClass="inputText" runat="server" />
                                                <des:CompareTwoFieldsValidator ID="CompareTwoFieldsValidator1" ControlIDToEvaluate="txtConfirmEmail" SecondControlIDToEvaluate="txtEmail" runat="server" ErrorMessage="* Emails must match" Group="Email">
                                                    <ErrorFormatterContainer>
                                                        <des:TextErrorFormatter Font-Size="9pt" Display="Static" />
                                                    </ErrorFormatterContainer>
                                                </des:CompareTwoFieldsValidator>
                                            </td>
                                        </tr>
                                    </table>                     
                                    <br />
                                    
                                    <p>Do you wish to continue to change user's email address?</p>
                                    <br />
                                    
                                    <p>
                                        <des:Button ID="ButtonOk3" runat="server" Group="Email" Text="OK" />
                                        <asp:Button ID="ButtonCancel3" runat="server" CausesValidation="false" Text="Cancel" />
                                    </p>                             
                                </div>
                            </asp:Panel>
            
                            <asp:confirmbuttonextender ID="ConfirmButtonExtender4" runat="server" TargetControlID="btnDisableProfile" ConfirmText="This is a test" DisplayModalPopupID="ModalPopupExtender4"  />
                            <br />
                            
                            <asp:modalpopupextender ID="ModalPopupExtender4" runat="server" TargetControlID="btnDisableProfile" PopupControlID="Panel4" OkControlID="ButtonOk4" CancelControlID="ButtonCancel4" BackgroundCssClass="modalBackground" />
                            <asp:Panel ID="Panel4" runat="server" style="display:none; width:250px; background-color:White; border-width:2px; border-color:Black; border-style:solid; padding:20px;">
                                <div style="font-family:Arial; font-size:10pt;">
                                    The profile will be disabled. The user will not be able to login, and all related payment
                                    information will be removed. Do you wish to continue?
                                </div>
                                <br /><br />
                                
                                <div style="text-align:center; font-family:Arial; font-size:10pt;">
                                    <asp:Button ID="ButtonOk4" runat="server" Text="OK" />
                                    <asp:Button ID="ButtonCancel4" runat="server" Text="Cancel" />
                                </div>
                            </asp:Panel>
                            
                            <asp:confirmbuttonextender ID="ConfirmButtonExtender5" runat="server" TargetControlID="btnCancelTxtPmt" ConfirmText="This is a test" DisplayModalPopupID="ModalPopupExtender5"  />
                            <br />
                            
                            <asp:modalpopupextender ID="ModalPopupExtender5" runat="server" TargetControlID="btnCancelTxtPmt" PopupControlID="Panel5" OkControlID="ButtonOk5" CancelControlID="ButtonCancel5" BackgroundCssClass="modalBackground" />
                            <asp:Panel ID="Panel5" runat="server" style="display:none; width:250px; background-color:White; border-width:2px; border-color:Black; border-style:solid; padding:20px;">
                                <div style="font-family:Arial; font-size:10pt;">
                                    All future text payments will be cancelled, and user will need to re-enroll in order to 
                                    restart text payments. Do you wish to continue?
                                </div>
                                <br /><br />
                                <div style="text-align:center; font-family:Arial; font-size:10pt;">
                                    <asp:Button ID="ButtonOk5" runat="server" Text="OK" />
                                    <asp:Button ID="ButtonCancel5" runat="server" Text="Cancel" />
                                </div>
                            </asp:Panel>   
                                
                            <fieldset style="padding-left:0px;margin-left:0px;">
                                <legend>Accounts</legend>
                                <div style="padding:20px;">
                                    <telerik:RadGrid ID="RadGrid1" runat="server" OnItemCreated="RadGrid1_ItemCreated"
                                        AutoGenerateColumns="False" GridLines="None" Skin="WebBlue"  ExportSettings-ExportOnlyData="true"
                                        ShowFooter="True" AllowSorting="True" ExportSettings-OpenInNewWindow="true">
                                        <MasterTableView DataKeyNames="Account_ID" CommandItemDisplay="Top">
                                            <CommandItemSettings  
                                                    ShowRefreshButton="false"
                                                    ShowAddNewRecordButton="false"
                                                    ShowExportToWordButton="false"
                                                    ShowExportToExcelButton="true" 
                                                    ShowExportToCsvButton="true"
                                                    ShowExportToPdfButton="false"/>
                                            <RowIndicatorColumn>
                                                <HeaderStyle Width="20px"></HeaderStyle>
                                            </RowIndicatorColumn>

                                            <ExpandCollapseColumn>
                                                <HeaderStyle Width="20px"></HeaderStyle>
                                            </ExpandCollapseColumn>
                                            <Columns>                    
                                                <telerik:GridBoundColumn DataField="ProductName" 
                                                    HeaderText="Product" UniqueName="column1" HtmlEncode="true">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="AccountNumber1" 
                                                    HeaderText="Account Number" UniqueName="column2" HtmlEncode="true">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="NickName" HeaderText="Description" 
                                                    UniqueName="column3" HtmlEncode="true">
                                                </telerik:GridBoundColumn>                    
                                                <telerik:GridButtonColumn CommandName="viewTextPayDetail" Text="Text Pay" 
                                                    UniqueName="column4">
                                                </telerik:GridButtonColumn>
                                            </Columns>
                                        </MasterTableView>
                                    </telerik:RadGrid>
                                </div>
                            </fieldset>
                            <br />
                                
                            <asp:Panel ID="pnlTextPay" runat="server" Visible="false">
                                <fieldset style="padding-left:0px;margin-left:0px;">
                                    <legend>Text Pay</legend>
                                    <div style="padding:20px;">
                                        <table cellpadding="5" cellspacing="2">
                                            <tr>
                                                <td>Enrolled in Text Pay:</td>
                                                <td>&nbsp;<asp:Literal ID="ltlEnrollTxtPay" runat="server" Text="Yes" /></td>
                                                <td>Phone Number Activated:</td>
                                                <td>&nbsp;<asp:Label ID="lblPhActivated" runat="server" /></td>
                                            </tr>
                                            <tr>
                                                <td>Payment Method:</td>
                                                <td>&nbsp;<asp:Label ID="lblPaymentMethod" runat="server" />&nbsp;&nbsp;</td>
                                                <td><asp:Label ID="lblOtherInfoTitle" runat="server" /></td>
                                                <td>&nbsp;<asp:Label ID="lblOtherInfoData" runat="server" /></td>
                                            </tr>
                                            <tr>
                                                <td>Payment Date:</td>
                                                <td>&nbsp;<asp:Label ID="lblPaymentDate" runat="server" />&nbsp;&nbsp;</td>
                                                <td>Payment Amount:</td>
                                                <td>&nbsp;<asp:Label ID="lblPaymentAmount" runat="server" /></td>
                                            </tr>
                                            <tr>
                                                <td>Next Payment Date:</td>
                                                <td>&nbsp;<asp:Label ID="lblNextPaymentDate" runat="server" />&nbsp;&nbsp;</td>
                                                <td>Last Text Prenotice Date:</td>
                                                <td>&nbsp;<asp:Label ID="lblPrenotice" runat="server" /></td>
                                            </tr>
                                            <tr>
                                                <td>PIN:</td>
                                                <td colspan="3">&nbsp;<asp:Label ID="lblPIN" runat="server" /></td>                                    
                                            </tr>
                                        </table>
                                    </div>
                                </fieldset>
                            </asp:Panel>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            <br />
            </div>
    
        </div>
        <custom:timeout ID="timeoutControl" runat="server" />
    </form>
      
    <div class="clear"></div> 
    <div class="footer_shadow"><custom:footer ID="Footer" runat="server" /></div>
</body>
</html>
