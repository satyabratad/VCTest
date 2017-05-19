<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="ProfileAccounts.aspx.vb" Inherits="Bill2PayAdmin45.ProfileAccounts" %>

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
    <form id="form1" runat="server" >
        <asp:ToolkitScriptManager ID="ScriptManager1" runat="server" />
        <div class="top_header_main"></div>
        <custom:menu ID="menu" runat="server" />
        <div class="top_header_shadow"></div>
      
        <div class="content">
            <custom:logout ID="logout" runat="server" />
            <div class="header">Profile Accounts</div>
             <div style="padding-left:10px"><asp:HyperLink ID="hypBack" runat="server" NavigateUrl="ProfileDetail.aspx" Text="&lt;&lt; Back to profile" /> </div>
            <div class="header_border"></div>
           
            <!--Start Main Content-->
            <div class="main_content">
               
                 <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
      <ContentTemplate> 
      
      <asp:Panel ID="pnlAddAccount" runat="server">
     
      <asp:Label ID="lblErrorMessage" runat="server" ForeColor="Red"></asp:Label>
      
       
              <table width="95%" cellspacing="5" cellpadding="5">  
                    <tr>
                        <td colspan="2"><h4>Step 1: Choose Product</h4></td>
                        <asp:Panel ID="pnlHeaders" runat="server" Visible="false">
                            <td><h4>Step 2: Enter Details</h4></td>
                            <td><h4>Step 3: Add Account</h4></td>
                        </asp:Panel>
                    </tr>                      
                    <tr style="height:25px;">
                        <td style="width:5%;" valign="top"><b>Product: </b></td>
                        <td valign="top" align="left" style="width:15%;">
                            <div id="divState" class="noborder">
                                <asp:DropDownList ID="ddlProductList" runat="server" OnSelectedIndexChanged="ddlProductList_SelectedIndexChanged" AutoPostBack="true"
                                    EnableViewState="true" CausesValidation="false" AppendDataBoundItems="true">
                                    <asp:ListItem Text="Select Product" Value="" /> 
                                </asp:DropDownList>
                                <des:RequiredTextValidator ID="reqProducts" runat="server" ErrorMessage="Required" ControlIDToEvaluate="ddlProductList">
 		                            <ErrorFormatterContainer>
                                        <des:TextErrorFormatter Display="Dynamic" />
                                    </ErrorFormatterContainer>
 		                         </des:RequiredTextValidator>
                            </div>
                        </td>
                        <td style="width:50%;">
                        <asp:Panel ID="pnlAccount1" runat="server" Visible="false">
                        <table style="width:100%;">
                            <tr>
                                <td style="width:25%;">
                                    <asp:Label ID="lblAccountNumber1" runat="server" Text="Label"></asp:Label>
                                </td>
                                <td>
                                <des:RegexValidator ID="regAccountNumber1" runat="server" ControlIDToEvaluate="txtAccountNumber1">
                                     <ErrorFormatterContainer>
                                            <des:TextErrorFormatter Display="Dynamic" />
                                        </ErrorFormatterContainer>
                                    </des:RegexValidator>
                                    <des:RequiredTextValidator ID="reqAccountNumber1" runat="server" ControlIDToEvaluate="txtAccountNumber1">
                                     <ErrorFormatterContainer>
                                            <des:TextErrorFormatter Display="Dynamic" />
                                        </ErrorFormatterContainer>
                                     </des:RequiredTextValidator>
                                    <div id="divAccount1">
                                        <des:TextBox ID="txtAccountNumber1" runat="server" MaxLength="10" Width="180px"></des:TextBox>
                                        <asp:FilteredTextBoxExtender ID="txtAccountNumber1_FilteredTextBoxExtender" 
                                            runat="server" TargetControlID="txtAccountNumber1" Enabled="false" FilterType="Custom, LowercaseLetters, UppercaseLetters, Numbers" ValidChars=" ,.&-'">
                                        </asp:FilteredTextBoxExtender>
                                    </div>
                                  </td>     
                            </tr>
                        </table>
                        </asp:Panel>
                        <asp:Panel ID="pnlAccount2" runat="server" Visible="false">
                            <table style="width:100%;">
                                <tr>
                                    <td style="width:25%;">
                                        <asp:Label ID="lblAccountNumber2" runat="server" Text="Label"></asp:Label>
                                    </td>
                                    <td>
                                         <des:RegexValidator ID="regAccountNumber2" runat="server" ControlIDToEvaluate="txtAccountNumber2">
                                         <ErrorFormatterContainer>
                                            <des:TextErrorFormatter Display="Dynamic" />
                                        </ErrorFormatterContainer>
                                        </des:RegexValidator>
                                        <des:RequiredTextValidator ID="reqAccountNumber2" runat="server" ControlIDToEvaluate="txtAccountNumber2">
                                         <ErrorFormatterContainer>
                                            <des:TextErrorFormatter Display="Dynamic" />
                                        </ErrorFormatterContainer>
                                        </des:RequiredTextValidator>
                                        
                                        <div id="divAccount2">
                                            <des:TextBox ID="txtAccountNumber2" runat="server" MaxLength="10" Width="180px"></des:TextBox>
                                            <asp:FilteredTextBoxExtender ID="txtAccountNumber2_FilteredTextBoxExtender" 
                                                runat="server" TargetControlID="txtAccountNumber2" Enabled="false" FilterType="Custom, LowercaseLetters, UppercaseLetters, Numbers" ValidChars=" ,.&-'">
                                            </asp:FilteredTextBoxExtender>
                                        </div>
                                        </td>
                                </tr>
                            </table>
                        </asp:Panel>
                        <asp:Panel ID="pnlAccount3" runat="server" Visible="false">
                            <table style="width:100%;">
                                <tr>
                                    <td style="width:25%;">
                                        <asp:Label ID="lblAccountNumber3" runat="server" Text="Label"></asp:Label>
                                    </td>
                                    <td>
                                        <des:RegexValidator ID="regAccountNumber3" runat="server" ControlIDToEvaluate="txtAccountNumber3">
                                         <ErrorFormatterContainer>
                                            <des:TextErrorFormatter Display="Dynamic" />
                                        </ErrorFormatterContainer>
                                        </des:RegexValidator>
                                        <des:RequiredTextValidator ID="reqAccountNumber3" runat="server" ControlIDToEvaluate="txtAccountNumber3">
                                         <ErrorFormatterContainer>
                                            <des:TextErrorFormatter Display="Dynamic" />
                                        </ErrorFormatterContainer>
                                        </des:RequiredTextValidator>
                                        <div id="divAccount3">
                                            <des:TextBox ID="txtAccountNumber3" runat="server" MaxLength="10" Width="180px"></des:TextBox>
                                            <asp:FilteredTextBoxExtender ID="txtAccountNumber3_FilteredTextBoxExtender" 
                                                runat="server" TargetControlID="txtAccountNumber3" Enabled="false" FilterType="Custom, LowercaseLetters, UppercaseLetters, Numbers" ValidChars=" ,.&-'">
                                            </asp:FilteredTextBoxExtender>
                                        </div>
                                        </td>   
                                </tr>
                            </table>
                        </asp:Panel>
                        
                        </td>
                        <td align="left">
                            <asp:Panel ID="pnlLookupButton" runat="server" Visible="false">
                                <des:ImageButton ImageUrl="/images/btnLookupAccount.jpg" ID="btnLookup" runat="server" ToolTip="Lookup Account" CssClass="formButton" />
                            </asp:Panel>
                            <asp:Panel ID="pnlButton" runat="server" Visible="false">
                                <des:ImageButton ImageUrl="/images/btnAddAccount.jpg" ID="btnSubmit" runat="server" ToolTip="Add Account" CssClass="formButton" />
                            </asp:Panel>    
                        </td>
                    </tr>
                    
                    
                </table>
                
           
<br /><br />
     <br /><br />
     </asp:Panel>
     
  </ContentTemplate>
   <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnLookupOK"  EventName="Click" />
                        </Triggers> 
  </asp:UpdatePanel>   

<asp:UpdatePanel ID="UpdatePanel6" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
    <asp:Button id="btnShowLookup" runat="server" style="display:none;" />
            <asp:ModalPopupExtender ID="modalLookup" runat="server" TargetControlID="btnShowLookup" PopupControlID="pnlLookup"
                CancelControlID="btnLookupCancel" BackgroundCssClass="modalBackground"></asp:ModalPopupExtender>
            <asp:Panel ID="pnlLookup" runat="server" style="display:none; width:450px; 
                background-color:White; border-width:2px; border-color:#336699; border-style:solid; padding:10px;" SkinID="PopUpPanel">
                <asp:UpdatePanel ID="UpdatePanel7" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:Label ID="lblProductTextNumber" runat="server" Text="" CssClass="popupTitle" /><asp:Label ID="lblProductNumberLookup" runat="server" Visible="false" />
                        <div style="padding-left:8px; padding-bottom:10px; padding-top:10px;"><asp:Label ID="lblLookupMessage" runat="server" /></div>
                        <div style="padding-left:5px;"><br /></div>
                        
                        <asp:GridView ID="grdLookup" runat="server" AutoGenerateColumns="true" GridLines="None" BorderStyle="none"  ShowHeader="false" cellspacing="10"></asp:GridView><br />
                        
                 </ContentTemplate>
                         <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnLookup"  EventName="Click" />
                        </Triggers>
                </asp:UpdatePanel>  
                            <div style="text-align: right; width: 100%; margin-top: 5px;">
                                <asp:Button ID="btnLookupOK" runat="server" Text="Yes, this is correct"  CssClass="modalButton" CausesValidation="false" />
                                <asp:Button ID="btnLookupCancel" runat="server" Text="No, this is not correct"  CssClass="modalButton" CausesValidation="false" />
                            </div> 
            </asp:Panel>
    </ContentTemplate>
    
     </asp:UpdatePanel>
 
 
 <asp:UpdatePanel ID="UpdatePanel8" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
    <asp:Button id="btnShowError" runat="server" style="display:none;" />
            <asp:ModalPopupExtender ID="modalLookupError" runat="server" TargetControlID="btnShowError" PopupControlID="pnlLookupError"
                CancelControlID="btnLookupError"   BackgroundCssClass="modalBackground"></asp:ModalPopupExtender>
            <asp:Panel ID="pnlLookupError" runat="server" style="display:none; width:350px; background-color:White; border-width:2px; border-color:#336699; border-style:solid; padding:10px;" SkinID="PopUpPanel">
                <asp:UpdatePanel ID="UpdatePanel9" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:Label ID="lblProductTextNumberError" runat="server" Text="" CssClass="popupTitle" /><asp:Label ID="lblProductNumberLookupError" runat="server" Visible="false" /><br />
                        <br /><asp:Label ID="lblLookupMessageError" runat="server" />
                    </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnSubmit"  EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="btnLookup"  EventName="Click" />
                           <asp:AsyncPostBackTrigger ControlID="btnLookupOK"  EventName="Click" />
                        </Triggers>
                </asp:UpdatePanel>        
                        
                <div style="text-align: right; width: 100%; margin-top: 0px;">
                    <asp:Button ID="btnLookupError" runat="server" Text="Close" CssClass="modalButton" CausesValidation="false" />
                </div> 
            </asp:Panel>
    </ContentTemplate>
 </asp:UpdatePanel>
     
     
          
     
     
    <asp:Panel ID="pnlUpdateBankResults" runat="server" Visible="false">
        <p>Your profile has been updated.</p>
        <p><asp:label ID="lblAltRoutingNumber" runat="server" text="Please note: An alternate routing number has been found while adding your payment method. This alternate routing number has been saved with your payment method." Visible="false"></asp:label></p>
         Click <a href="/profile/main.aspx">here</a> to return your account.
    </asp:Panel>
    
     
    </div>
     
     
    </div>
    
  
               
           
        <custom:timeout ID="timeoutControl" runat="server" />
        
<asp:UpdateProgress ID="UpdateProgress1" runat="server">
      <ProgressTemplate>
            <div id="progressBackgroundFilter">
                
            </div>
            <div id="processMessage"><img src="/images/loader.gif" style="border:none; vertical-align:middle; background-color:White;" alt="Page loader" />&nbsp;&nbsp; Processing ...<br />
                 
            </div>
      </ProgressTemplate>
    </asp:UpdateProgress>
    </form>      
    <div class="clear"></div> 
    <div class="footer_shadow"><custom:footer ID="Footer" runat="server" /></div>
</body>
</html>
