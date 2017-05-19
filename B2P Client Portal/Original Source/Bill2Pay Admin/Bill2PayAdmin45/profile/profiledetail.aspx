<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="ProfileDetail.aspx.vb" Inherits="Bill2PayAdmin45.ProfileDetail1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Profile Details :: Bill2Pay Administration</title>
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
            <div class="header">Profile Details</div>
            <div style="padding-left:10px"><asp:HyperLink ID="hypBack" runat="server" NavigateUrl="/profile/profiles.aspx" Text="&lt;&lt; Back to search results" /> </div>
            <div class="header_border"></div>
            <asp:ToolkitScriptManager ID="ScriptManager1" runat="server" />
       
            <!--Start Main Content-->
            <div class="main_content">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <asp:ValidationSummary id="ValidationSummary1" runat="server"  HeaderText="There's an error in the information you entered. Please correct the following errors and continue."
                            ScrollIntoView="Top" CssClass="ErrorSummary" DisplayMode="bulletList">
	                    </asp:ValidationSummary>  <br />  
                        <span style="font-style:italic; padding-left:20px;"><asp:Label ID="lblMessage" runat="server" Visible="true" />
                         <asp:Label ID="lblMessage2" Font-Italic="true" runat="server" Font-Bold="true"  /></span>
                        <div id="ProfileDetail">
                        <div style="padding-left:10px;"><asp:ValidationSummary id="ValidationSummary2"  runat="server" CssClass="ErrorSummary"  HeaderText="There's an error in the information you entered. Please correct the following errors and continue."
			                HyperLinkToFields="True" ScrollIntoView="Top"  DisplayMode="bulletList">
	                        </asp:ValidationSummary>
	                    </div>
                            <table cellpadding="2" width="100%">
                                <tr>
                                    <td valign="top">
                                        <fieldset style="padding-top:0px;margin-top:0px;">
                                            <legend>Profile Information</legend>
                                            <table cellpadding="2" cellspacing="8">
                                                <tr>
                                                    <td>Client Name:</td><td>&nbsp;<asp:Label ID="lblClientName" runat="server" /></td>
                                                </tr>
                                                <tr>
                                                    <td>Profile Id:</td><td>&nbsp;<asp:Label ID="lblProfileID" runat="server" /></td>
                                                </tr>
                                                <tr>
                                                    <td>Username:</td><td>&nbsp;<asp:Label ID="lblUsername" runat="server" /></td>
                                                </tr>                                             
                                                <tr>
                                                    <td>First Name:</td>
                                                    <td>
                                                        <asp:TextBox ID="txtFirstName" runat="server" CssClass="inputText" Columns="30"></asp:TextBox>
                                                        <asp:FilteredTextBoxExtender ID="tbFirstName_FilteredTextBoxExtender1" 
                                                            runat="server" TargetControlID="txtFirstName" FilterType="Custom, LowercaseLetters, UppercaseLetters, Numbers" ValidChars=" ,.-'" >
                                                        </asp:FilteredTextBoxExtender>
                          
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1"  ValidationGroup="Profile" ForeColor="Red"
                                                            runat="server" ControlToValidate="txtFirstName" ErrorMessage="First name is required" 
                                                        SetFocusOnError="True" ToolTip="Tool TIp" Display="Dynamic" Text="*"></asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Last Name:</td>
                                                    <td>
                                                        <asp:TextBox ID="txtLastName" runat="server" CssClass="inputText" Columns="30"></asp:TextBox>
                                                        <asp:FilteredTextBoxExtender ID="tbLastName_FilteredTextBoxExtender1" 
                                                                runat="server" TargetControlID="txtLastName" FilterType="Custom, LowercaseLetters, UppercaseLetters, Numbers" ValidChars=" ,.-'" >
                                                        </asp:FilteredTextBoxExtender>
                      
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2"   ValidationGroup="Profile" ForeColor="Red"
                                                                runat="server" ControlToValidate="txtLastName" ErrorMessage="Last name is required" 
                                                                SetFocusOnError="True" ToolTip="Tool TIp" Display="Dynamic" Text="*"></asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Email:</td>
                                                    <td>
                                                        <asp:TextBox ID="txtEmailAddress" runat="server" CssClass="inputText" Columns="40"></asp:TextBox>
                                                         <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" 
                                                            runat="server" filtertype="Numbers,UpperCaseLetters,LowerCaseLetters,Custom" TargetControlID="txtEmailAddress" 
                                                            ValidChars="-@._">
                                                        </asp:FilteredTextBoxExtender>
                                                        <asp:RegularExpressionValidator ID="regEmail" runat="server" ValidationGroup="Profile" ForeColor="Red"
                                                            ControlToValidate="txtEmailAddress" Display="Dynamic" ErrorMessage="Invalid Email Address" ToolTip="Invalid Email Address" 
                                                            SetFocusOnError="True" ValidationExpression="<$|^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,6}$">*</asp:RegularExpressionValidator>
                                                         <asp:RequiredFieldValidator ID="RequiredFieldValidator3"   ValidationGroup="Profile" ForeColor="Red"
                                                                runat="server" ControlToValidate="txtEmailAddress" ErrorMessage="Email address is required" 
                                                                SetFocusOnError="True" ToolTip="Tool TIp" Display="Dynamic" Text="*"></asp:RequiredFieldValidator>    
                                                    </td>
                                                </tr>
                                                <asp:Panel ID="pnlPendingEmail" runat="server">
                                                    <tr>
                                                        <td>Pending Email:</td>
                                                        <td>&nbsp;<asp:Label ID="lblPendingEmail" runat="server" />
                                                            <asp:LinkButton ID="lnkResendValidationEmail" runat="server" Text="Resend validation email" />
                                                        </td>
                                                    </tr>
                                                </asp:Panel>
                                                <asp:Panel ID="pnlCellPhone" runat="server">
                                                    <tr>
                                                        <td>Cell:</td>
                                                        <td>&nbsp;<asp:Label ID="lblCellPhone" runat="server" />
                                                                <asp:LinkButton ID="lnkChangeCellPhone" runat="server" Text="Change" />
                                                                &nbsp;<asp:LinkButton ID="lnkDeleteCellPhone" runat="server" Text="Delete" />
                                                               
                                                        </td>
                                                    </tr>
                                                </asp:Panel>
                                                <asp:Panel ID="pnlPendingCellPhone" runat="server">
                                                    <tr>
                                                        <td>Pending Cell:</td>
                                                        <td>&nbsp;<asp:Label ID="lblPendingCellPhone" runat="server" />
                                                             <asp:LinkButton ID="lnkEnterValidationCode" runat="server" Text="Enter validation code" />
                                                        </td>
                                                    </tr>
                                                </asp:Panel>
                                                <tr>
                                                    <td>Status:</td><td>&nbsp;<asp:Label ID="lblStatus" runat="server" />
                                                        <asp:LinkButton ID="lnkResendActivationEmail" runat="server" Text="Resend activation email" />
                                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2" align="center">&nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2" align="center">
                                                        <des:ImageButton ID="btnUpdateProfile" ValidationGroup="Profile" AlternateText="Update Profile" ImageUrl="/images/btnUpdateProfile.jpg"  runat="server"  />
                                                    </td>
                                                </tr>
                                            </table>                                        
                                        </fieldset>
                                    </td>
                                    <td valign="top">
                                        <fieldset style="padding-top:0px;margin-top:0px;">
                                            <legend>Default Billing Address</legend>
                                        <table cellpadding="2" cellspacing="8">
                                            <tr>
                                                <td>Country</td>
                                                <td>
                                                 <telerik:RadComboBox ID="ddlCountry" runat="server" ToolTip="Select Country"  CausesValidation="false"
                                                        OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged" AutoPostBack="true" >
                                                </telerik:RadComboBox>    
                                                    
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Address1:</td>
                                                <td>
                                                    
                                                    <asp:TextBox ID="txtAddress1" runat="server" Width="254px" MaxLength="40" CssClass="inputText" Text=""></asp:TextBox>
                                                        <asp:FilteredTextBoxExtender ID="TextBox1_FilteredTextBoxExtender" 
                                                            runat="server" Enabled="True" TargetControlID="txtAddress1" FilterType="Custom, LowercaseLetters, UppercaseLetters, Numbers" ValidChars=" ,.&-'">
                                                        </asp:FilteredTextBoxExtender>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ValidationGroup="Address" ForeColor="Red"
                                                                runat="server" ControlToValidate="txtAddress1" ErrorMessage="Address is required" 
                                                                SetFocusOnError="True" ToolTip="Tool TIp" Display="Dynamic" Text="*"></asp:RequiredFieldValidator>   
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Address2:</td>
                                                <td>
                                                    <asp:TextBox ID="txtAddress2" runat="server" CssClass="inputText" Columns="30"></asp:TextBox>
                                                     <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" 
                                                        runat="server" Enabled="True" TargetControlID="txtAddress2" FilterType="Custom, LowercaseLetters, UppercaseLetters, Numbers" ValidChars=" ,.&-'">
                                                     </asp:FilteredTextBoxExtender>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>City:</td>
                                                <td>
                                                    <asp:TextBox ID="txtCity" runat="server" CssClass="inputText" Columns="30"></asp:TextBox>
                                                     <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" 
                                                        runat="server" Enabled="True" TargetControlID="txtCity" FilterType="Custom, LowercaseLetters, UppercaseLetters, Numbers" ValidChars=" ,.&-'">
                                                     </asp:FilteredTextBoxExtender>
                                                     <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ValidationGroup="Address" ForeColor="Red"
                                                                runat="server" ControlToValidate="txtCity" ErrorMessage="City is required" 
                                                                SetFocusOnError="True" ToolTip="Tool TIp" Display="Dynamic" Text="*"></asp:RequiredFieldValidator>   
                                                </td>
                                            </tr>
                                            <asp:Panel ID="pnlState" runat="server">
                                            <tr>
                                                <td><asp:Label ID="lblBillingState" runat="server" Text="State"></asp:Label>:</td>
                                                <td> <telerik:RadComboBox borderColor="blue" ID="ddlState" CausesValidation="false" runat="server"  Sort="Ascending" DataTextField="name" DataValueField="abbreviation"
                                                    TabIndex="5"    />
                                                    
                                                     <asp:XmlDataSource runat="server" ID="XmlDataSource2" DataFile="~/resource/usStates.xml" />
                                                </td>
                                            </tr>
                                            </asp:Panel>
                                            <tr>
                                                <td><asp:Label id="lblBillingZip" runat="server" Text="Zip"></asp:Label>:</td>
                                                <td>
                                                    <asp:TextBox ID="txtZip" runat="server" Width="70px" MaxLength="5" CssClass="inputText"></asp:TextBox>
                                                <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender9" 
                                                    runat="server" Enabled="True" TargetControlID="txtZip" FilterType="Numbers">
                                                </asp:FilteredTextBoxExtender>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server"  ForeColor="Red"
                                                ControlToValidate="txtZip" Display="Dynamic"  Text="*"  ValidationGroup="Address"
                                                ErrorMessage="Not a valid zip code" SetFocusOnError="True" ValidationExpression="^[a-zA-Z0-9]{5,9}$"></asp:RegularExpressionValidator>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ErrorMessage="Zip is required" ControlToValidate="txtZip" ForeColor="Red"
                                                etFocusOnError="True" ToolTip="Tool TIp" Text="*" ValidationGroup="Address"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2" align="center">&nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td colspan="2" align="center"><des:ImageButton AlternateText="Update Address" ImageUrl="/images/btnUpdateAddress.jpg" ValidationGroup="Address"  ID="btnUpdateAddress" runat="server"  /></td>
                                            </tr>
                                        </table>                                        
                                        </fieldset>
                                    </td>
                                   
                                    <td valign="top">
                                        <fieldset style="padding-top:0px;margin-top:0px;">
                                            <legend>Profile Activities</legend>
                                            <div style="padding:20px; padding-top:10px">
                                                <des:ImageButton AlternateText="Disable Profile" ImageUrl="/images/btnDisableProfile.jpg" ID="btnDisableProfile" runat="server" CausesValidation="False" /><br /><br />
                                                <des:ImageButton  AlternateText="Email UserName" ImageUrl="/images/btnEmailUsername.jpg" ID="btnSendUsername" runat="server" CausesValidation="False"  /><br /><br />
                                                <des:ImageButton AlternateText="Reset Password" ImageUrl="/images/btnResetPassword.jpg" ID="btnResetPwd" runat="server" CausesValidation="False" /><br /><br />
                                                <des:ImageButton AlternateText="Send Email" ImageUrl="/images/btnSendEmail.jpg" ID="btnEmail" runat="server" CausesValidation="False" /><br /><br />
                                                <%--<des:Button ID="btnChangeEmail" runat="server" CausesValidation="False" Text="Change Email" /><br /><br />--%>
                                                <%--<des:Button ID="btnCancelTxtPmt" Enabled="false" runat="server" CausesValidation="False" Text="Cancel Text Payments" />--%>                          
                                            </div>
                                        </fieldset>
                                        <br />
                                       
                                    </td>
                                </tr>  
                                <tr>
                                     <td>
                                        <fieldset style="padding-top:0px;margin-top:0px;">
                                            <legend>Notifications</legend>
                                            <table cellpadding="2" cellspacing="8">
                                                <tr>
                                                    <td></td>
                                                    <td align="center">Email</td>
                                                    <td align="center">Text</td> 
                                                </tr>
                                                <asp:Panel ID="pnlPaymentReminders" runat="server">
                                                <tr>
                                                    <td style="width:50%;">Due Date Reminders</td>
                                                    <td align="center"><asp:CheckBox ID="ckPaymentAlertsEmail" runat="server" ToolTip="Payment reminders via email" AutoPostBack="true" /></td>
                                                    <td align="center"><asp:CheckBox ID="ckPaymentAlertsText" runat="server" ToolTip="Payment reminders via text" AutoPostBack="true"  /> </td>                                                   
                                                </tr>
                                                </asp:Panel>
                                                    
                                                
                                                <tr>
                                                    <td>Profile Alerts</td>
                                                    <td align="center"><asp:CheckBox ID="ckProfileAlertsEmail" runat="server" ToolTip="Profile alerts via email" AutoPostBack="true" /></td>
                                                    <td align="center"><asp:CheckBox ID="ckProfileAlertsText" runat="server" ToolTip="Profile alerts via text" AutoPostBack="true"   />   </td>
                                                </tr>
                                                <tr>
                                                    <td>System Alerts</td>
                                                    <td align="center"><asp:CheckBox ID="ckSystemAlertsEmail" Checked="true" runat="server" ToolTip="System alerts via email" Enabled="false"  /></td>
                                                    <td align="center"><asp:CheckBox ID="ckSystemAlertsText" runat="server" ToolTip="System alerts via text"  AutoPostBack="true" />    </td>
                                                </tr>
                                            </table>
                                        </fieldset>
                                    </td>
                                    <td></td>
                                    <td></td>
                                </tr>   
                            </table>
                          
                            
                            <asp:confirmbuttonextender ID="ConfirmButtonExtender1" runat="server" TargetControlID="btnResetPwd" DisplayModalPopupID="ModalPopupExtender1"  />
                           
                            
                            <asp:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="btnResetPwd" PopupControlID="Panel1" OkControlID="ButtonOk" CancelControlID="ButtonCancel" 
                                BackgroundCssClass="modalBackground" />
                            <asp:Panel ID="Panel1" runat="server" style="display:none; width:250px; background-color:White; border-width:2px; border-color:Black; border-style:solid; padding:20px;">
                                <div style="font-family:Arial; font-size:10pt;">
                                    Password reset will be generated and emailed to the address on file with this profile.
                                    Do you wish to continue?
                                </div>
                                <br /><br />
                
                                <div style="text-align:center; font-family:Arial; font-size:10pt;">
                                    
                                    <asp:Button ID="ButtonOk" runat="server" Text="OK" CssClass="buttons" />
                                    <asp:Button ID="ButtonCancel" runat="server" Text="Cancel" CssClass="buttons" />
                                </div>
                            </asp:Panel>
            
                            <asp:confirmbuttonextender ID="ConfirmButtonExtender2" runat="server" TargetControlID="btnDisableProfile" DisplayModalPopupID="ModalPopupExtender4"  />
                          
                            
                            <asp:ModalPopupExtender ID="ModalPopupExtender4" runat="server" TargetControlID="btnDisableProfile" PopupControlID="Panel2" OkControlID="ButtonOk4" 
                                CancelControlID="ButtonCancel4" BackgroundCssClass="modalBackground" />
                            <asp:Panel ID="Panel2" runat="server" style="display:none; width:250px; background-color:White; border-width:2px; border-color:Black; border-style:solid; padding:20px;">
                                <div style="font-family:Arial; font-size:10pt;">
                                    The profile will be disabled. The user will not be able to login, and all related payment
                                    information will be removed. Do you wish to continue?
                                </div>
                                <br /><br />
                                
                                <div style="text-align:center; font-family:Arial; font-size:10pt;">
                                    <asp:Button ID="ButtonOk4" runat="server" Text="OK" CssClass="buttons" />
                                    <asp:Button ID="ButtonCancel4" runat="server" Text="Cancel" CssClass="buttons" />
                                </div>
                            </asp:Panel>

                            <asp:ModalPopupExtender ID="CellPhoneModalExtender" runat="server" TargetControlID="lnkChangeCellPhone" PopupControlID="pnlCellPhoneUpdate"  
                                 BackgroundCssClass="modalBackground" />
                            
                            <asp:Panel ID="pnlCellPhoneUpdate" runat="server" style="display:none; width:375px; background-color:White; border-width:2px; border-color:Black; border-style:solid; padding:20px;">
                                <fieldset style="width:350px;">
                                        <legend>New cell phone</legend>
                                        <div style="padding:15px;">
                                           
                                                    <asp:TextBox ID="txtCellPhone" MaxLength="10" Width="250px" runat="server" CssClass="inputText" />
                                                        <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" 
                                                                                runat="server" filtertype="Numbers" TargetControlID="txtCellPhone">
                                                                            </asp:FilteredTextBoxExtender>
                                                       <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ValidationGroup="ProfileCellPhone" 
                                                           ControlToValidate="txtCellPhone" Display="Dynamic" Text="Invalid" ToolTip="Invalid" 
                                                           ValidationExpression="^\D?(\d{3})\D?\D?(\d{3})\D?(\d{4})$"></asp:RegularExpressionValidator>
                                                       <asp:RequiredFieldValidator ID="RequiredFieldValidator9" ValidationGroup="ProfileCellPhone"
                                                           runat="server" ControlToValidate="txtCellPhone" Text="Required" 
                                                              SetFocusOnError="True" ToolTip="Tool TIp" Display="Dynamic"></asp:RequiredFieldValidator>   
                                                    
                                        </div>
                                    </fieldset>
                                
                                <div style="text-align:center; font-family:Arial; font-size:10pt;">
                                    <asp:Button ID="btnSubmitCellPhone" runat="server" Text="Update Cell Phone" CssClass="buttons" CausesValidation="true" ValidationGroup="ProfileCellPhone" />
                                    <asp:Button ID="btnCancelCellPhone" runat="server" Text="Cancel" CssClass="buttons" CausesValidation="false" ValidationGroup="ProfileCellPhone"  />
                                </div>
                            </asp:Panel>


                            <asp:ModalPopupExtender ID="ValidationCodeModal" runat="server" TargetControlID="lnkEnterValidationCode" PopupControlID="pnlValidationCode"  
                                 BackgroundCssClass="modalBackground" />
                            
                            <asp:Panel ID="pnlValidationCode" runat="server" style="display:none; width:375px; background-color:White; border-width:2px; border-color:Black; border-style:solid; padding:20px;">
                                <fieldset style="width:350px;">
                                        <legend>Validation Code</legend>
                                        <div style="padding:15px;">
                                           
                                                    <asp:TextBox ID="txtValidationCode" MaxLength="7" Width="200px" runat="server" CssClass="inputText" />
                                                        <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender8" 
                                                                                runat="server" filtertype="UppercaseLetters, Lowercaseletters, Numbers" TargetControlID="txtValidationCode">
                                                                            </asp:FilteredTextBoxExtender>
                                                      
                                                       <asp:RequiredFieldValidator ID="RequiredFieldValidator10" ValidationGroup="ProfileCellPhoneValidation"
                                                           runat="server" ControlToValidate="txtValidationCode" Text="Required" 
                                                              SetFocusOnError="True" ToolTip="Tool TIp" Display="Dynamic"></asp:RequiredFieldValidator>   
                                                    
                                        </div>
                                    </fieldset>
                                
                                <div style="text-align:center; font-family:Arial; font-size:10pt;">
                                    <asp:Button ID="btnSubmitValidationCode" runat="server" Text="Update Cell Phone" CssClass="buttons" CausesValidation="true" ValidationGroup="ProfileCellPhoneValidation" />
                                    <asp:Button ID="btnCancelValidationCode" runat="server" Text="Cancel" CssClass="buttons" CausesValidation="false" ValidationGroup="ProfileCellPhoneValidation"  />
                                </div>
                            </asp:Panel>

                            <!-- Start Resend Activation Email -->
                            <asp:confirmbuttonextender ID="ConfirmButtonExtender3" runat="server" TargetControlID="lnkResendActivationEmail" DisplayModalPopupID="ResendActivationModal"  />
                           
                            <asp:ModalPopupExtender ID="ResendActivationModal" runat="server" TargetControlID="lnkResendActivationEmail" PopupControlID="pnlResendActivationEmail" OkControlID="btnResendActivation" CancelControlID="btnCancelActivation" 
                                BackgroundCssClass="modalBackground" />
                            <asp:Panel ID="pnlResendActivationEmail" runat="server" style="display:none; width:250px; background-color:White; border-width:2px; border-color:Black; border-style:solid; padding:20px;">
                                <div style="font-family:Arial; font-size:10pt;">
                                    Activation email will be generated and emailed to the address on file with this profile.
                                    Do you wish to continue?
                                </div>
                                <br /><br />
                
                                <div style="text-align:center; font-family:Arial; font-size:10pt;">
                                    
                                    <asp:Button ID="btnResendActivation" runat="server" Text="OK" CssClass="buttons" />
                                    <asp:Button ID="btnCancelActivation" runat="server" Text="Cancel" CssClass="buttons" />
                                </div>
                            </asp:Panel>
                            <!-- End Resend Activation Email -->

                            <!-- Start Resend Validation Email -->
                            <asp:confirmbuttonextender ID="ConfirmButtonExtender5" runat="server" TargetControlID="lnkResendValidationEmail" DisplayModalPopupID="ResendValidationModal"  />
                           
                            <asp:ModalPopupExtender ID="ResendValidationModal" runat="server" TargetControlID="lnkResendValidationEmail" PopupControlID="pnlResendValidationEmail" 
                                OkControlID="btnResendValidationEmail" CancelControlID="btnCancelValidationEmail" BackgroundCssClass="modalBackground" />
                            <asp:Panel ID="pnlResendValidationEmail" runat="server" style="display:none; width:250px; background-color:White; border-width:2px; border-color:Black; border-style:solid; padding:20px;">
                                <div style="font-family:Arial; font-size:10pt;">
                                    Validation email will be generated and emailed to the address on file with this profile.
                                    Do you wish to continue?
                                </div>
                                <br /><br />
                
                                <div style="text-align:center; font-family:Arial; font-size:10pt;">
                                    
                                    <asp:Button ID="btnResendValidationEmail" runat="server" Text="OK" CssClass="buttons" />
                                    <asp:Button ID="btnCancelValidationEmail" runat="server" Text="Cancel" CssClass="buttons" />
                                </div>
                            </asp:Panel>
                            <!-- End Resend Validation Email -->


                            <!-- Start Delete Cell Phone -->
                            <asp:confirmbuttonextender ID="ConfirmButtonExtender4" runat="server" TargetControlID="lnkDeleteCellPhone" DisplayModalPopupID="DeleteCellPhoneModal"  />
                           
                            <asp:ModalPopupExtender ID="DeleteCellPhoneModal" runat="server" TargetControlID="lnkDeleteCellPhone" PopupControlID="pnlDeleteCellPhone" OkControlID="btnDeleteCellPhone" CancelControlID="btnCancelDeleteCellPhone" 
                                BackgroundCssClass="modalBackground" />
                            <asp:Panel ID="pnlDeleteCellPhone" runat="server" style="display:none; width:250px; background-color:White; border-width:2px; border-color:Black; border-style:solid; padding:20px;">
                                <div style="font-family:Arial; font-size:10pt;">
                                    User's cell phone will be deleted. Do you wish to continue?
                                </div>
                                <br /><br />
                
                                <div style="text-align:center; font-family:Arial; font-size:10pt;">
                                    
                                    <asp:Button ID="btnDeleteCellPhone" runat="server" Text="OK" CssClass="buttons" />
                                    <asp:Button ID="btnCancelDeleteCellPhone" runat="server" Text="Cancel" CssClass="buttons" />
                                </div>
                            </asp:Panel>
                            <!-- End Delete Cell Phone -->

                                    
                                                         
                            <asp:ModalPopupExtender ID="modalEmail" runat="server" TargetControlID="btnEmail" PopupControlID="pnlEmail" BackgroundCssClass="modalBackground"
                                 CancelControlID="ButtonCancelEmail" />
                            <asp:Panel ID="pnlEmail" runat="server" style="display:none; width:450px; background-color:White; border-width:2px; border-color:Black; border-style:solid; padding:20px;">
                                <fieldset style="width:400px;">
                                        <legend>Send Email</legend>
                                        <div style="padding:8px;">
                                            <table>
                                                <tr>
                                                    <td>From:</td>
                                                    <td><asp:Literal ID="litFromEmail" runat="server" /> </td>
                                                </tr>
                                                <tr>
                                                    <td>To:</td>
                                                    <td><asp:TextBox ID="txtTo" MaxLength="100" Width="250px" runat="server" CssClass="inputText" />
                                                        <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" 
                                                                                runat="server" filtertype="Numbers,UpperCaseLetters,LowerCaseLetters,Custom" TargetControlID="txtTo" 
                                                                                ValidChars="-@._">
                                                                            </asp:FilteredTextBoxExtender>
                                                       <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ValidationGroup="Profile"
                                                           ControlToValidate="txtTo" Display="Dynamic" ErrorMessage="Invalid Email Address" ToolTip="Invalid" 
                                                           SetFocusOnError="True" ValidationExpression="<$|^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,6}$">*</asp:RegularExpressionValidator>
                                                       <asp:RequiredFieldValidator ID="RequiredFieldValidator6"   ValidationGroup="ProfileEmail"
                                                           runat="server" ControlToValidate="txtTo" Text="Required" 
                                                              SetFocusOnError="True" ToolTip="Tool TIp" Display="Dynamic"></asp:RequiredFieldValidator>   
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Subject:</td>
                                                    <td><asp:TextBox ID="txtSubject" MaxLength="50" Width="250px" runat="server" CssClass="inputText" />
                                                     <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" 
                                                    runat="server" TargetControlID="txtSubject" FilterType="Custom, LowercaseLetters, UppercaseLetters, Numbers" ValidChars=" .,'" >
                                                </asp:FilteredTextBoxExtender>
                                                     <asp:RequiredFieldValidator ID="RequiredFieldValidator7"  ValidationGroup="ProfileEmail"
                                                                                runat="server" ControlToValidate="txtSubject" text="Required" 
                                                                            SetFocusOnError="True" ToolTip="Tool TIp" Display="Dynamic" ></asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td valign="top">Message:
                                    
                                                    </td>
                                                    <td valign="top"><span style="max-height:100px;min-height:100px;max-width:400px;min-width:400px;">
                                                        <asp:TextBox ID="txtMessage" TextMode="MultiLine" Width="300px" ValidationGroup="EmailValidation" runat="server" />&nbsp;<des:RequiredTextValidator ID="RequiredTextValidator2" ControlIDToEvaluate="txtMessage" InAJAXUpdate="true" ErrorMessage="* E-Mail Message is required." runat="server" Group="EmailValidation" /></span>
                                                     <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" 
                                                    runat="server" TargetControlID="txtMessage" FilterType="Custom, LowercaseLetters, UppercaseLetters, Numbers" ValidChars=" .,'$!?@" >
                                                </asp:FilteredTextBoxExtender>
                                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator8"  ValidationGroup="ProfileEmail"
                                                                                runat="server" ControlToValidate="txtMessage" Text="Required" 
                                                                            SetFocusOnError="True" ToolTip="Tool TIp" Display="Dynamic"></asp:RequiredFieldValidator>
                                                <br />
                                                        <des:TextCounter ID="txtCounter" TextBoxControlID="txtMessage" Maximum="2048" AboveMaximumMessage="Only 2048 characters are allowed in the Message field." runat="server" />
                                    
                                                    </td>
                                                </tr>
                            
                                            </table>
                                        </div>
                                    </fieldset>
                                                               
                                <br /><br />
                                
                                <div style="text-align:center; font-family:Arial; font-size:10pt;">
                                    <des:Button ID="ButtonOkEmail" runat="server" Text="Send Email" ValidationGroup="ProfileEmail" CausesValidation="true" CssClass="buttons" />
                                    <des:Button ID="ButtonCancelEmail" runat="server" Text="Cancel" CssClass="buttons" />
                                </div>
                            </asp:Panel>
                                
                            <fieldset style="padding-left:0px;margin-left:0px;">
                                <legend>Accounts</legend>
                               
                                <div style="padding:20px;">
                                    <asp:Panel ID="pnlNoAccounts" runat="server" Visible="false">
                                        <a href="/profile/profileaccounts.aspx" title="Add an account" >Click here to add an account</a><br /><br />
                                    </asp:Panel>
                                    <asp:Panel ID="pnlAccounts" runat="server">
                                    <p>Select arrow in the far left column to view future automated payments for each account.<br /><br /></p>
                                    <telerik:RadGrid ID="RadGrid1" runat="server" OnItemCreated="RadGrid1_ItemCreated" MasterTableView-CommandItemDisplay="top"
                                        AllowAutomaticDeletes="true" OnDeleteCommand="RadGrid1_DeleteCommand" ShowHeader="true"
                                         OnInsertCommand="RadGrid1_InsertCommand" pagesize="3" AllowPaging="true" MasterTableView-ShowHeadersWhenNoRecords="true"
                                        AutoGenerateColumns="False" GridLines="None" Skin="WebBlue"  ExportSettings-ExportOnlyData="true"
                                        ShowFooter="True" ExportSettings-OpenInNewWindow="true">                                        
                                        <MasterTableView DataKeyNames="Account_ID" CommandItemDisplay="Top" Name="ParentGrid">                                           
                                            <CommandItemSettings 
                                                     ShowAddNewRecordButton="true"
                                                    ShowRefreshButton="false"
                                                    AddNewRecordText="Add new account"
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
                                            <DetailTables>
                                                <telerik:GridTableView DataKeyNames="AccountPayment_ID" HorizontalAlign="Left" Name="ChildGrid" Width="98%"> 
                                                    <Columns>
                                                        <telerik:GridBoundColumn HeaderText="Payment Type" HeaderButtonType="TextButton" UniqueName="PaymentType" 
                                                            DataField="Type">
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn HeaderText="Payment Method" HeaderButtonType="TextButton"
                                                            DataField="WalletItem.Description">
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn HeaderText="Payment Date" HeaderButtonType="TextButton" UniqueName="PaymentDate"
                                                            DataField="PaymentEvent.NextPaymentDate">
                                                        </telerik:GridBoundColumn>                                                        
                                                        <telerik:GridBoundColumn SortExpression="Amount" HeaderText="Amount" HeaderButtonType="TextButton"
                                                            DataField="PaymentEvent.Amount" UniqueName="Amount" DataFormatString="{0:C}">
                                                        </telerik:GridBoundColumn>
                                                    </Columns>
                                                </telerik:GridTableView>
                                            </DetailTables>

                                            <Columns>   
                                            <telerik:GridButtonColumn CommandName="Delete" Text="Delete" 
                                                UniqueName= "DeleteColumn" ConfirmText="Are you sure you want to delete this account?" /> 
                                                <telerik:GridButtonColumn CommandName="viewProfileAccountDetail" Text="View" 
                                                    UniqueName="column0">
                                                </telerik:GridButtonColumn>               
                                                <telerik:GridBoundColumn DataField="ProductName" HeaderText="Product" 
                                                    UniqueName="column1" HtmlEncode="true">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="AccountNumber1" HeaderText="Account Number" 
                                                    UniqueName="column2" HtmlEncode="true">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="AccountNumber2" HeaderText="Account2" 
                                                    UniqueName="column3" HtmlEncode="true">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="AccountNumber3" HeaderText="Account3" 
                                                    UniqueName="column4" HtmlEncode="true">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="NickName" HeaderText="Description" 
                                                    UniqueName="column5" HtmlEncode="true">
                                                </telerik:GridBoundColumn>   
                                                <telerik:GridBoundColumn DataField="AmountDue" HeaderText="Amount Due"
                                                    UniqueName="AmountDue" HtmlEncode="true">
                                                </telerik:GridBoundColumn>
                                                
                                            </Columns>
                                        </MasterTableView>
                                    </telerik:RadGrid>
                                    </asp:Panel>
                                </div>
                            </fieldset>
                            <br />
                                
                            <fieldset style="padding-left:0px;margin-left:0px;">
                                <legend>eWallet</legend>
                                <div style="padding:20px;">
                                    <telerik:RadGrid ID="RadGrid2" runat="server" OnItemCreated="RadGrid2_ItemCreated" pagesize="3"
                                        AutoGenerateColumns="False" GridLines="None" Skin="WebBlue"  ExportSettings-ExportOnlyData="true"
                                        ShowFooter="True"  ExportSettings-OpenInNewWindow="true" AllowPaging="true">
                                        <MasterTableView DataKeyNames="PaymentMethod_ID" CommandItemDisplay="Top">
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
                                                <telerik:GridBoundColumn DataField="FullName" HeaderText="Name" 
                                                    UniqueName="column1" HtmlEncode="true">
                                                </telerik:GridBoundColumn>          
                                                <telerik:GridBoundColumn DataField="Description" HeaderText="Type" 
                                                    UniqueName="column2" HtmlEncode="true">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="LastFour" HeaderText="Account Number" 
                                                    UniqueName="column3" HtmlEncode="true">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="ExpirationDate" HeaderText="Expiration" 
                                                    UniqueName="column4" HtmlEncode="true">
                                                </telerik:GridBoundColumn>
                                            </Columns>
                                        </MasterTableView>
                                    </telerik:RadGrid>
                                </div>
                            </fieldset>
                            <br />
                            
                            
                            
                            <fieldset style="padding-left:0px;margin-left:0px;">
                                <legend>Profile Activity</legend>
                                <div style="padding:20px;">
                                    <telerik:RadGrid ID="rdActivity" runat="server" OnItemCreated="rdActivity_ItemCreated" pagesize="10"
                                        AutoGenerateColumns="False" GridLines="None" Skin="WebBlue"  ExportSettings-ExportOnlyData="true"
                                        ShowFooter="True" ExportSettings-OpenInNewWindow="true" AllowPaging="true">
                                        <MasterTableView CommandItemDisplay="Top">
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
                                                <telerik:GridBoundColumn DataField="ActivityDate" HeaderText="Date" 
                                                    UniqueName="column1" HtmlEncode="true">
                                                </telerik:GridBoundColumn>          
                                                <telerik:GridBoundColumn DataField="Description" HeaderText="Activity" 
                                                    UniqueName="column2" HtmlEncode="true">
                                                </telerik:GridBoundColumn>
                                                 <telerik:GridBoundColumn DataField="UserName" HeaderText="User Name" 
                                                    UniqueName="column3" HtmlEncode="true">
                                                </telerik:GridBoundColumn>
                                              
                                            </Columns>
                                        </MasterTableView>
                                    </telerik:RadGrid>
                                </div>
                            </fieldset>
                            <br />
                            
                            
                            
                            
                            
                            
                            
                                
<%--                            <asp:Panel ID="pnlTextPay" runat="server" Visible="false">
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
                            </asp:Panel>--%>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            <br />
            
            <asp:UpdateProgress ID="UpdateProgress1" runat="server">
      <ProgressTemplate>
            <div id="progressBackgroundFilter">
                
            </div>
            <div id="processMessage"><img src="/images/loader.gif" style="border:none; vertical-align:middle; background-color:White;" alt="Page loader" />&nbsp;&nbsp; Processing ...<br />
                 
            </div>
      </ProgressTemplate>
    </asp:UpdateProgress>
            </div>
    
        </div>
        <custom:timeout ID="timeoutControl" runat="server" />
    </form>
      
    <div class="clear"></div> 
    <div class="footer_shadow"><custom:footer ID="Footer" runat="server" /></div>

    
</body>
</html>
