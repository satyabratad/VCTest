<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="newCounter.aspx.vb" Inherits="Bill2PayAdmin45.newCounter" %>

<%--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">--%>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">

<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Make a Payment :: Bill2Pay Administration</title>
    <link rel="stylesheet" type="text/css" href="../css/main.css" media="all" />
    <link rel="stylesheet" type="text/css" href="../css/content_normal.css" title="normal" />
    <link rel="stylesheet" type="text/css" href="../Content/themes/base/minified/jquery.ui.all.min.css" />
    <script language="JavaScript" type="text/javascript" src="../Magensa/mtjmsr.js"></script>
</head>

<body>

    <script language="JavaScript" type="text/javascript">

        var isDone = false;
        // timeout after 30 seconds and ReportIncorrectJavaVersion (plugin is not installed or applet was not run)
        setTimeout('ReportIncorrectJavaVersion()', 30000);

        function ReportIncorrectJavaVersion() {
            //if (!isDone) {
            //    isDone = true;
                //alert("You do NOT have the Java Plugin 1.6 or higher installed.");
            //}
        }

        function ReportCorrectJavaVersion() {
            //if (!isDone) {
            //    isDone = true;
                //alert("You have the Java Plugin 1.6 or higher installed.");
            //}
        }

        function getDevConStatus() {
            //GetDevConStatus();
            //setTimeout('getDevConStatus()', 2000);
        }

        //setTimeout('getDevConStatus()', 2000);


        function GetTrackDataDisable() {
            //DisableGetTrackData();
        }
        //setTimeout('GetTrackDataEnable()', 2000);


        function GetTrackDataEnable() {
            //EnableGetTrackData();
        }
        //setTimeout('GetTrackDataEnable()', 2500);

        function LimtCharacters(txtMsg, CharLength, indicator) {
            chars = txtMsg.value.length;
            document.getElementById(indicator).innerHTML = CharLength - chars;
            if (chars > CharLength) {
                txtMsg.value = txtMsg.value.substring(0, CharLength);
            }
        }
    </script>
    
    <form id="form1" runat="server" defaultbutton="btnSubmit" autocomplete="off">
      <div class="top_header_main"></div>
      <custom:menu ID="menu" runat="server" />
      <div class="top_header_shadow"></div>
  
      <div class="content">
        <custom:logout ID="logout" runat="server" />
        <div class="header">Make a Counter Payment</div>
        <div class="header_border"></div>   
    
        <asp:ToolkitScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="true" />
          
        <!--StartContent-->
        <div class="main_content">

            <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <div id="ClientOfficeSelect" style="float:left; padding-right:5px;">
                        <telerik:RadComboBox ID="ddlAUClient"   
                                             Runat="server" 
                                             Skin="Windows7" 
                                             tabindex="1" 
                                             OnSelectedIndexChanged="ddlAUClient_SelectedIndexChanged" 
                                             AutoPostBack="true" 
                                             CausesValidation="false" 
                                             Visible="false"/>
                        &nbsp;
                        <telerik:RadComboBox ID="ddlOffice"  
                                             runat="server" 
                                             ToolTip="Select Office" 
                                             Visible="false" 
                                             DataValueField="Office_ID" 
                                             DataTextField="Name" 
                                             AppendDataBoundItems="true" 
                                             CausesValidation="false"/>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" 
                                                    InitialValue="Select Office" 
                                                    ControlToValidate="ddlOffice" 
                                                    runat="server" 
                                                    text="*" 
                                                    ErrorMessage="Office is required"></asp:RequiredFieldValidator>
                    </div>    
                    
                    <div id="PaymentType" style="float:left; padding-right:5px; background:none;">
                        <asp:RadioButtonList ID="RadioButtonList1" CssClass="noborder" runat="server" RepeatDirection="horizontal" OnSelectedIndexChanged="RadioButtonList1_SelectedIndexChanged" AutoPostBack="true" CausesValidation="false" >
                            <asp:ListItem Text="Credit Card" Value="CC" Selected="True" Enabled="false" ></asp:ListItem>
                            <asp:ListItem Text="Bank Account" Value="ACH" Enabled="false"></asp:ListItem>
                        </asp:RadioButtonList>
                    </div>       
        
                    <div class="clear"></div>    
 
                    <div id="ProductTypes" style="padding-bottom:5px; margin-top:15px;">
                        <div style="padding-left:10px;">
                            <asp:ValidationSummary id="ValidationSummary1"  runat="server" CssClass="ErrorSummary"  HeaderText="There's an error in the information you entered. Please correct the following errors and continue."
			                    HyperLinkToFields="True" ScrollIntoView="Top"  DisplayMode="bulletList">
	                        </asp:ValidationSummary>
                        </div>
                        <fieldset>
                            <legend>Product Information</legend>
                            <div id="Products" style="padding-top:5px; padding-bottom:5px;">            
                                <asp:UpdatePanel id="updProduct1" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:Panel ID="pnlProduct1" runat="server" Visible="true" DefaultButton="btnCart1">
                                            <table width="98%" style="margin-left:20px;"  >
                                                <tr style="height:25px; vertical-align: bottom;">
                                                    <td>
                                                        <telerik:RadComboBox ID="ddlProduct1" runat="server"  onSelectedIndexChanged="ddlProduct1_SelectedIndexChanged" 
                                                        AutoPostBack="true" CausesValidation="false" AppendDataBoundItems="true">                               
                                                        </telerik:RadComboBox>
                                                     </td>
                                                     <td style="width:170px; text-align:left;">
                                                        <asp:TextBox id="txtProduct1Input" runat="server" Width="150px" MaxLength="40"></asp:TextBox>
                                                        <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" Enabled="True" TargetControlID="txtProduct1Input" 
                                                             FilterType="Custom, LowercaseLetters, UppercaseLetters, Numbers" ValidChars=" ,.&-'">
                                                        </asp:FilteredTextBoxExtender>
                                
                                                        <asp:TextBoxWatermarkExtender 
                                                           ID="TextBoxWatermarkExtender1" 
                                                           runat="server" 
                                                           Enabled="True" 
                                                           TargetControlID="txtProduct1Input" 
                                                           WatermarkCssClass="watermarked" 
                                                           WatermarkText="Account Number 1"> 
                                                        </asp:TextBoxWatermarkExtender>
                                                        <des:RequiredTextValidator ID="reqProduct1" runat="server" ControlIDToEvaluate="txtProduct1Input"  Group="Product1" ErrorMessage="*" />
                                                     </td>                             
                                                     <td style="width:170px;">
                                                        <asp:TextBox id="txtProduct1aInput" runat="server" Width="150px"  MaxLength="40" ReadOnly="true" Enabled="false"></asp:TextBox>
                                                        <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender10" runat="server" Enabled="True" TargetControlID="txtProduct1aInput" 
                                                                        FilterType="Custom, LowercaseLetters, UppercaseLetters, Numbers" ValidChars=" ,.&-'#_/">
                                                        </asp:FilteredTextBoxExtender>
                                
                                                        <asp:TextBoxWatermarkExtender 
                                                           ID="TextBoxWatermarkExtender11" 
                                                           runat="server" 
                                                           Enabled="True" 
                                                           TargetControlID="txtProduct1aInput" 
                                                           WatermarkCssClass="watermarked" 
                                                           WatermarkText="Not required"> 
                                                        </asp:TextBoxWatermarkExtender>                                
                                                     </td>                             
                                                     <td style="width:170px;">
                                                         <asp:TextBox id="txtProduct1bInput" runat="server" Width="150px" MaxLength="40" ReadOnly="true" Enabled="false"></asp:TextBox>
                                                         <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender11" runat="server" Enabled="True" TargetControlID="txtProduct1bInput" 
                                                                    FilterType="Custom, LowercaseLetters, UppercaseLetters, Numbers" ValidChars=" ,.&-'#_/">
                                                         </asp:FilteredTextBoxExtender>
                                
                                                        <asp:TextBoxWatermarkExtender 
                                                           ID="TextBoxWatermarkExtender12" 
                                                           runat="server" 
                                                           Enabled="True" 
                                                           TargetControlID="txtProduct1bInput" 
                                                           WatermarkCssClass="watermarked" 
                                                           WatermarkText="Not required"> 
                                                        </asp:TextBoxWatermarkExtender>
                                
                                                     </td>
                                                     <td>
                                                         $&nbsp;
                                                         <asp:TextBox id="txtProduct1Amt" runat="server" Width="100px" MaxLength="10"></asp:TextBox>
                                                         <asp:TextBoxWatermarkExtender 
                                                           ID="TextBoxWatermarkExtender2" 
                                                           runat="server" 
                                                           Enabled="True" 
                                                           TargetControlID="txtProduct1Amt" 
                                                           WatermarkCssClass="watermarked" 
                                                           WatermarkText="Amount"> 
                                                         </asp:TextBoxWatermarkExtender>
                                                          <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender20" 
                                                            runat="server" Enabled="True" TargetControlID="txtProduct1Amt" FilterType="Custom, Numbers" ValidChars=".">
                                                        </asp:FilteredTextBoxExtender>
                                                         <des:RequiredTextValidator ID="reqAmount1" runat="server" ControlIDToEvaluate="txtProduct1Amt"  Group="Product1" ErrorMessage="*" />
                                                     </td>
                                                     <asp:Panel id="CartButtons1" runat="server" Visible="true">
                                                         <td align="right"><des:ImageButton ID="btnCart1" ImageUrl="/images/cartdisabled.jpg" CssClass="noborder" runat="server" ToolTip="Add to Cart" Enabled="false" CausesValidation="true" ValidationGroup="Product1" /></td>
                                                         <td align="right"><des:ImageButton ID="btnClearAll" ImageUrl="/images/clearall.jpg" runat="server" ToolTip="Clear All Products" CausesValidation="false" /></td>
                                                         <td align="right"><des:ImageButton ID="btnLookup1" ImageUrl="/images/lookupdisabled.jpg" ToolTip="Lookup Account" Enabled="false" runat="server" CausesValidation="false" /></td>
                                                     </asp:Panel>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                    </ContentTemplate>

                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="btnCart1"  EventName="Click" />
                                        <asp:AsyncPostBackTrigger ControlID="btnClearAll"  EventName="Click" />
                                        <asp:AsyncPostBackTrigger ControlID="btnLookupOK"  EventName="Click" />
                                        <asp:AsyncPostBackTrigger ControlID="btnLookupCancel"  EventName="Click" />
                                        <asp:AsyncPostBackTrigger ControlID="btnBlockOverride"  EventName="Click" />
                                    </Triggers>                  
                                </asp:UpdatePanel>    
              
                                <asp:UpdatePanel id="updProduct2" runat="server" UpdateMode="Conditional" RenderMode="Inline">
                                    <ContentTemplate>
                                        <asp:Panel ID="pnlProduct2" runat="server" Visible="false" DefaultButton="btnCart2">
                                            <table width="98%" style="margin-left:20px;"  >
                                                <tr style="height:25px;">                            
                                                    <td> 
                                                        <telerik:RadComboBox ID="ddlProduct2" runat="server"  
                                                            onSelectedIndexChanged="ddlProduct2_SelectedIndexChanged" 
                                                            AutoPostBack="true" 
                                                            CausesValidation="false" 
                                                            AppendDataBoundItems="true"/>
                                                    </td>
                                                    <td style="width:170px; text-align:left;">
                                                        <asp:TextBox id="txtProduct2Input" runat="server" Width="150px"  MaxLength="40"></asp:TextBox>
                                                        <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" 
                                                                runat="server" 
                                                                Enabled="True" 
                                                                TargetControlID="txtProduct2Input" 
                                                                FilterType="Custom, LowercaseLetters, UppercaseLetters, Numbers" 
                                                                ValidChars=" ,.&-'"/>
                                                        <asp:TextBoxWatermarkExtender 
                                                               ID="TextBoxWatermarkExtenderProduct2" 
                                                               runat="server" 
                                                               Enabled="True" 
                                                               TargetControlID="txtProduct2Input" 
                                                               WatermarkCssClass="watermarked" 
                                                               WatermarkText="Account Number 1"/> 
                                                        <des:RequiredTextValidator ID="reqProduct2" runat="server" ControlIDToEvaluate="txtProduct2Input"  Group="Product2" ErrorMessage="*" />
                                                    </td>                                    
                                                    <td style="width:170px; text-align:left;">
                                                        <asp:TextBox id="txtProduct2aInput" runat="server" Width="150px" MaxLength="40" ReadOnly="true" Enabled="false"></asp:TextBox>
                                                        <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender12" 
                                                                runat="server" 
                                                                Enabled="True" 
                                                                TargetControlID="txtProduct2aInput" 
                                                                FilterType="Custom, LowercaseLetters, UppercaseLetters, Numbers" 
                                                                ValidChars=" ,.&-'#_/"/>                                
                                                        <asp:TextBoxWatermarkExtender 
                                                               ID="TextBoxWatermarkExtenderProduct2a" 
                                                               runat="server" 
                                                               Enabled="True" 
                                                               TargetControlID="txtProduct2aInput" 
                                                               WatermarkCssClass="watermarked" 
                                                               WatermarkText="Not required"/>                                
                                                    </td>                             
                                                    <td style="width:170px; text-align:left;">
                                                        <asp:TextBox id="txtProduct2bInput" runat="server" Width="150px" MaxLength="40" ReadOnly="true" Enabled="false"></asp:TextBox>
                                                        <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender13" 
                                                                runat="server" 
                                                                Enabled="True" 
                                                                TargetControlID="txtProduct2bInput" 
                                                                FilterType="Custom, LowercaseLetters, UppercaseLetters, Numbers" ValidChars=" ,.&-'#_/"/>
                                
                                                        <asp:TextBoxWatermarkExtender 
                                                               ID="TextBoxWatermarkExtenderProduct2b" 
                                                               runat="server" 
                                                               Enabled="True" 
                                                               TargetControlID="txtProduct2bInput" 
                                                               WatermarkCssClass="watermarked" 
                                                               WatermarkText="Not required"> 
                                                        </asp:TextBoxWatermarkExtender>                                
                                                    </td>                                    
                                                    <td>$&nbsp;<asp:TextBox id="txtProduct2Amt" runat="server" Width="100px"></asp:TextBox>
                                                        <asp:TextBoxWatermarkExtender 
                                                           ID="TextBoxWatermarkExtender7" 
                                                           runat="server" 
                                                           Enabled="True" 
                                                           TargetControlID="txtProduct2Amt" 
                                                           WatermarkCssClass="watermarked" 
                                                           WatermarkText="Amount"> 
                                                        </asp:TextBoxWatermarkExtender>
                                                         <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender21" 
                                                            runat="server" Enabled="True" TargetControlID="txtProduct2Amt" FilterType="Custom, Numbers" ValidChars=".">
                                                        </asp:FilteredTextBoxExtender>
                                                        <des:RequiredTextValidator ID="reqAmount2" runat="server" ControlIDToEvaluate="txtProduct2Amt"  Group="Product2" ErrorMessage="*" />
                                                    </td>                                    
                                                    <td align="right"><des:ImageButton ID="btnCart2" ImageUrl="/images/cartdisabled.jpg" ToolTip="Add to Cart" runat="server" enabled="false" ValidationGroup="Product2" CausesValidation="true"  /></td>
                                                    <td align="right"><des:ImageButton ID="btnRemove2" ImageUrl="/images/removedisabled.jpg" ToolTip="Remove from Cart" runat="server" enabled="false" CausesValidation="false" /></td>
                                                    <td align="right"><des:ImageButton ID="btnLookup2" ImageUrl="/images/lookupdisabled.jpg" ToolTip="Lookup Account" Enabled="false" runat="server" CausesValidation="false" /></td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                    </ContentTemplate>
                
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="btnCart1"  EventName="Click" />
                                        <asp:AsyncPostBackTrigger ControlID="btnClearAll"  EventName="Click" />
                                        <asp:AsyncPostBackTrigger ControlID="btnCart2"  EventName="Click" />
                                        <asp:AsyncPostBackTrigger ControlID="btnRemove2"  EventName="Click" />
                                        <asp:AsyncPostBackTrigger ControlID="btnLookupOK"  EventName="Click" />
                                        <asp:AsyncPostBackTrigger ControlID="btnLookupCancel"  EventName="Click" />
                                        <asp:AsyncPostBackTrigger ControlID="btnBlockOverride"  EventName="Click" />
                                    </Triggers>                  
                                </asp:UpdatePanel>
              
                                <asp:UpdatePanel id="updProduct3" runat="server" UpdateMode="Conditional" RenderMode="Inline">
                                    <ContentTemplate>
                                        <asp:Panel ID="pnlProduct3" runat="server" Visible="false" DefaultButton="btnCart3"> 
                                            <table width="98%" style="margin-left:20px;"  >
                                                <tr style="height:25px;">
                                                    <td> 
                                                        <telerik:RadComboBox ID="ddlProduct3" runat="server" onSelectedIndexChanged="ddlProduct3_SelectedIndexChanged" 
                                                                AutoPostBack="true" CausesValidation="false" AppendDataBoundItems="true"/>
                                                    </td>
                                                    <td style="width:170px; text-align:left;">
                                                        <asp:TextBox id="txtProduct3Input" runat="server" Width="150px" MaxLength="40"></asp:TextBox>
                                                        <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" 
                                                            runat="server" Enabled="True" TargetControlID="txtProduct3Input" FilterType="Custom, LowercaseLetters, UppercaseLetters, Numbers" ValidChars=" ,.&-'">
                                                        </asp:FilteredTextBoxExtender>
                                                        <asp:TextBoxWatermarkExtender 
                                                           ID="TextBoxWatermarkExtenderProduct3" 
                                                           runat="server" 
                                                           Enabled="True" 
                                                           TargetControlID="txtProduct3Input" 
                                                           WatermarkCssClass="watermarked" 
                                                           WatermarkText="Account Number 1"> 
                                                        </asp:TextBoxWatermarkExtender>
                                                        <des:RequiredTextValidator ID="reqProduct3" runat="server" ControlIDToEvaluate="txtProduct3Input"  Group="Product3" ErrorMessage="*" />
                                                    </td>                    
                                                    <td style="width:170px; text-align:left;">
                                                        <asp:TextBox id="txtProduct3aInput" runat="server" Width="150px" MaxLength="40" ReadOnly="true" Enabled="false"></asp:TextBox>
                                                        <asp:FilteredTextBoxExtender 
                                                                ID="FilteredTextBoxExtender14" 
                                                                runat="server" 
                                                                Enabled="True" 
                                                                TargetControlID="txtProduct3aInput" 
                                                                FilterType="Custom, LowercaseLetters, UppercaseLetters, Numbers" 
                                                                ValidChars=" ,.&-'#_/">
                                                        </asp:FilteredTextBoxExtender>                                
                                                        <asp:TextBoxWatermarkExtender 
                                                               ID="TextBoxWatermarkExtenderProduct3a" 
                                                               runat="server" 
                                                               Enabled="True" 
                                                               TargetControlID="txtProduct3aInput" 
                                                               WatermarkCssClass="watermarked" 
                                                               WatermarkText="Not required"> 
                                                        </asp:TextBoxWatermarkExtender>                                
                                                    </td>                             
                                                    <td style="width:170px; text-align:left;"><asp:TextBox id="txtProduct3bInput" runat="server" Width="150px"  MaxLength="40" ReadOnly="true" Enabled="false"></asp:TextBox>
                                                        <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender15" 
                                                            runat="server" Enabled="True" TargetControlID="txtProduct3bInput" FilterType="Custom, LowercaseLetters, UppercaseLetters, Numbers" ValidChars=" ,.&-'#_/">
                                                        </asp:FilteredTextBoxExtender>
                                
                                                        <asp:TextBoxWatermarkExtender 
                                                           ID="TextBoxWatermarkExtenderProduct3b" 
                                                           runat="server" 
                                                           Enabled="True" 
                                                           TargetControlID="txtProduct3bInput" 
                                                           WatermarkCssClass="watermarked" 
                                                           WatermarkText="Not required"> 
                                                        </asp:TextBoxWatermarkExtender>
                                
                                                    </td>                    
                                                    <td>$&nbsp;<asp:TextBox id="txtProduct3Amt" runat="server" Width="100px"></asp:TextBox>
                                                        <asp:TextBoxWatermarkExtender 
                                                           ID="TextBoxWatermarkExtender8" 
                                                           runat="server" 
                                                           Enabled="True" 
                                                           TargetControlID="txtProduct3Amt" 
                                                           WatermarkCssClass="watermarked" 
                                                           WatermarkText="Amount"> 
                                                        </asp:TextBoxWatermarkExtender>
                                                         <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender22" 
                                                            runat="server" Enabled="True" TargetControlID="txtProduct3Amt" FilterType="Custom, Numbers" ValidChars=".">
                                                        </asp:FilteredTextBoxExtender>
                                                        <des:RequiredTextValidator ID="reqAmount3" runat="server" ControlIDToEvaluate="txtProduct3Amt"  Group="Product3" ErrorMessage="*" />
                                                    </td>                   
                                                    <td align="right"><des:ImageButton ID="btnCart3" ImageUrl="/images/cartdisabled.jpg" ValidationGroup="Product3" ToolTip="Add to Cart" Enabled="false" runat="server" CausesValidation="true" /></td>
                                                    <td align="right"><des:ImageButton ID="btnRemove3" ImageUrl="/images/removedisabled.jpg" ToolTip="Remove from Cart" Enabled="false" runat="server" CausesValidation="false" /></td>
                                                    <td align="right"><des:ImageButton ID="btnLookup3" ImageUrl="/images/lookupdisabled.jpg" ToolTip="Lookup Account" Enabled="false" runat="server" CausesValidation="false" /></td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="btnCart2"  EventName="Click" />
                                        <asp:AsyncPostBackTrigger ControlID="btnRemove2"  EventName="Click" />
                                        <asp:AsyncPostBackTrigger ControlID="btnCart3"  EventName="Click" />
                                        <asp:AsyncPostBackTrigger ControlID="btnRemove3"  EventName="Click" />
                                        <asp:AsyncPostBackTrigger ControlID="btnLookupOK"  EventName="Click" />
                                        <asp:AsyncPostBackTrigger ControlID="btnLookupCancel"  EventName="Click" />
                                        <asp:AsyncPostBackTrigger ControlID="btnBlockOverride"  EventName="Click" />
                                    </Triggers>                  
                                </asp:UpdatePanel>
                            
                                <asp:UpdatePanel id="updProduct4" runat="server" UpdateMode="Conditional" RenderMode="Inline">
                                    <ContentTemplate> 
                                        <asp:Panel ID="pnlProduct4" runat="server" Visible="false" DefaultButton="btnCart4">
                                            <table width="98%" style="margin-left:20px;"  >
                                                <tr style="height:25px;">
                                                    <td> 
                                                        <telerik:RadComboBox ID="ddlProduct4" runat="server" onSelectedIndexChanged="ddlProduct4_SelectedIndexChanged" 
                                                            AutoPostBack="true" CausesValidation="false" AppendDataBoundItems="true">
                                                        </telerik:RadComboBox>
                                                    </td>
                                                    <td style="width:170px;">
                                                        <asp:TextBox id="txtProduct4Input" runat="server" Width="150px" MaxLength="40"></asp:TextBox>
                                                        <asp:FilteredTextBoxExtender 
                                                            ID="FilteredTextBoxExtender7" 
                                                            runat="server" 
                                                            Enabled="True" 
                                                            TargetControlID="txtProduct4Input" 
                                                            FilterType="Custom, LowercaseLetters, UppercaseLetters, Numbers" 
                                                            ValidChars=" ,.&-'"/>
                                                        <asp:TextBoxWatermarkExtender 
                                                            ID="TextBoxWatermarkExtenderProduct4" 
                                                            runat="server" 
                                                            Enabled="True" 
                                                            TargetControlID="txtProduct4Input" 
                                                            WatermarkCssClass="watermarked" 
                                                            WatermarkText="Enter Account Number Here"/> 
                                                        <des:RequiredTextValidator 
                                                            ID="reqProduct4" 
                                                            runat="server" 
                                                            ControlIDToEvaluate="txtProduct4Input"  
                                                            Group="Product4" 
                                                            ErrorMessage="*" />                        
                                                    </td>
                                                    <td style="width:170px;">
                                                        <asp:TextBox id="txtProduct4aInput" runat="server" Width="150px" MaxLength="40" ReadOnly="true" Enabled="false"></asp:TextBox>
                                                        <asp:FilteredTextBoxExtender 
                                                            ID="FilteredTextBoxExtender16" 
                                                            runat="server" 
                                                            Enabled="True" 
                                                            TargetControlID="txtProduct4aInput" 
                                                            FilterType="Custom, LowercaseLetters, UppercaseLetters, Numbers" 
                                                            ValidChars=" ,.&-'#_/"/>                                
                                                        <asp:TextBoxWatermarkExtender 
                                                            ID="TextBoxWatermarkExtenderProduct4a" 
                                                            runat="server" 
                                                            Enabled="True" 
                                                            TargetControlID="txtProduct4aInput" 
                                                            WatermarkCssClass="watermarked" 
                                                            WatermarkText="Not required"/>                                 
                                                    </td>                             
                                                    <td style="width:170px;">
                                                        <asp:TextBox id="txtProduct4bInput" runat="server" Width="150px" MaxLength="40" ReadOnly="true" Enabled="false"></asp:TextBox>
                                                        <asp:FilteredTextBoxExtender 
                                                            ID="FilteredTextBoxExtender17" 
                                                            runat="server" 
                                                            Enabled="True" 
                                                            TargetControlID="txtProduct4bInput" 
                                                            FilterType="Custom, LowercaseLetters, UppercaseLetters, Numbers" 
                                                            ValidChars=" ,.&-'#_/">
                                                        </asp:FilteredTextBoxExtender>                                
                                                        <asp:TextBoxWatermarkExtender 
                                                            ID="TextBoxWatermarkExtenderProduct4b" 
                                                            runat="server" 
                                                            Enabled="True" 
                                                            TargetControlID="txtProduct4bInput" 
                                                            WatermarkCssClass="watermarked" 
                                                            WatermarkText="Not required"> 
                                                        </asp:TextBoxWatermarkExtender>                                
                                                    </td>
                                                    <td>$&nbsp;
                                                        <asp:TextBox id="txtProduct4Amt" runat="server" Width="100px"></asp:TextBox>
                                                        <asp:TextBoxWatermarkExtender 
                                                            ID="TextBoxWatermarkExtender9" 
                                                            runat="server" 
                                                            Enabled="True" 
                                                            TargetControlID="txtProduct4Amt" 
                                                            WatermarkCssClass="watermarked" 
                                                            WatermarkText="Amount"/> 
                                                         <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender23" 
                                                            runat="server" Enabled="True" TargetControlID="txtProduct4Amt" FilterType="Custom, Numbers" ValidChars=".">
                                                        </asp:FilteredTextBoxExtender>
                                                        <des:RequiredTextValidator 
                                                            ID="reqAmount4" 
                                                            runat="server" 
                                                            ControlIDToEvaluate="txtProduct4Amt"  
                                                            Group="Product4" 
                                                            ErrorMessage="*" />
                                                    </td>
                                                    <td align="right"><des:ImageButton ID="btnCart4" ImageUrl="/images/cartdisabled.jpg" ToolTip="Add to Cart" Enabled="false" runat="server" CausesValidation="true" ValidationGroup="Product4" /></td>
                                                    <td align="right"><des:ImageButton ID="btnRemove4" ImageUrl="/images/removedisabled.jpg" ToolTip="Remove from Cart" Enabled="false" runat="server" CausesValidation="false" /></td>
                                                    <td align="right"><des:ImageButton ID="btnLookup4" ImageUrl="/images/lookupdisabled.jpg" ToolTip="Lookup Account" Enabled="false" runat="server" CausesValidation="false" /></td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="btnCart3"  EventName="Click" />
                                        <asp:AsyncPostBackTrigger ControlID="btnRemove3"  EventName="Click" />
                                        <asp:AsyncPostBackTrigger ControlID="btnCart4"  EventName="Click" />
                                        <asp:AsyncPostBackTrigger ControlID="btnRemove4"  EventName="Click" />
                                        <asp:AsyncPostBackTrigger ControlID="btnLookupOK"  EventName="Click" />
                                        <asp:AsyncPostBackTrigger ControlID="btnLookupCancel"  EventName="Click" />
                                        <asp:AsyncPostBackTrigger ControlID="btnBlockOverride"  EventName="Click" />
                                    </Triggers>                  
                                </asp:UpdatePanel>
                            
                                <asp:UpdatePanel id="updProduct5" runat="server" UpdateMode="Conditional" RenderMode="Inline">
                                    <ContentTemplate>   
                                        <asp:Panel ID="pnlProduct5" runat="server" Visible="false" DefaultButton="btnCart5">
                                            <table width="98%" style="margin-left:20px;"  >
                                                <tr style="height:25px;">
                                                    <td> 
                                                        <telerik:RadComboBox 
                                                            ID="ddlProduct5" 
                                                            runat="server" 
                                                            onSelectedIndexChanged="ddlProduct5_SelectedIndexChanged" 
                                                            AutoPostBack="true"
                                                            CausesValidation="false" 
                                                            AppendDataBoundItems="true">                         
                                                        </telerik:RadComboBox>
                                                    </td>
                                                    <td style="width:170px;">
                                                        <asp:TextBox id="txtProduct5Input" runat="server" Width="150px"  MaxLength="40"></asp:TextBox>
                                                        <asp:FilteredTextBoxExtender 
                                                            ID="FilteredTextBoxExtender8" 
                                                            runat="server" 
                                                            Enabled="True" 
                                                            TargetControlID="txtProduct5Input" 
                                                            FilterType="Custom, LowercaseLetters, UppercaseLetters, Numbers" 
                                                            ValidChars=" ,.&-'"/>
                                                        <asp:TextBoxWatermarkExtender 
                                                            ID="TextBoxWatermarkExtenderProduct5" 
                                                            runat="server" 
                                                            Enabled="True" 
                                                            TargetControlID="txtProduct5Input" 
                                                            WatermarkCssClass="watermarked" 
                                                            WatermarkText="Enter Account Number Here"/> 
                                                        <des:RequiredTextValidator 
                                                            ID="reqProduct5" 
                                                            runat="server" 
                                                            ControlIDToEvaluate="txtProduct5Input"  
                                                            Group="Product5" 
                                                            ErrorMessage="*" />
                                                    </td>                    
                                                    <td style="width:170px;">
                                                        <asp:TextBox id="txtProduct5aInput" runat="server" Width="150px" MaxLength="40" ReadOnly="true" Enabled="false"></asp:TextBox>
                                                        <asp:FilteredTextBoxExtender 
                                                            ID="FilteredTextBoxExtender18" 
                                                            runat="server" 
                                                            Enabled="True" 
                                                            TargetControlID="txtProduct5aInput" 
                                                            FilterType="Custom, LowercaseLetters, UppercaseLetters, Numbers" 
                                                            ValidChars=" ,.&-'#_/"/>                                
                                                        <asp:TextBoxWatermarkExtender 
                                                            ID="TextBoxWatermarkExtenderProduct5a" 
                                                            runat="server" 
                                                            Enabled="True" 
                                                            TargetControlID="txtProduct5aInput" 
                                                            WatermarkCssClass="watermarked" 
                                                            WatermarkText="Not required"/>                                 
                                                    </td>                             
                                                    <td style="width:170px;">
                                                        <asp:TextBox id="txtProduct5bInput" runat="server" Width="150px"  MaxLength="40" ReadOnly="true" Enabled="false"></asp:TextBox>
                                                        <asp:FilteredTextBoxExtender  
                                                            ID="FilteredTextBoxExtender19" 
                                                            runat="server" 
                                                            Enabled="True" 
                                                            TargetControlID="txtProduct5bInput" 
                                                            FilterType="Custom, LowercaseLetters, UppercaseLetters, Numbers" 
                                                            ValidChars=" ,.&-'#_/"/>
                                                        <asp:TextBoxWatermarkExtender 
                                                            ID="TextBoxWatermarkExtenderProduct5b" 
                                                            runat="server" 
                                                            Enabled="True" 
                                                            TargetControlID="txtProduct5bInput" 
                                                            WatermarkCssClass="watermarked" 
                                                            WatermarkText="Not required"/>                                 
                                                    </td>
                                                    <td>$&nbsp;
                                                        <asp:TextBox id="txtProduct5Amt" runat="server" Width="100px"></asp:TextBox>
                                                        <asp:TextBoxWatermarkExtender 
                                                            ID="TextBoxWatermarkExtender10" 
                                                            runat="server" 
                                                            Enabled="True" 
                                                            TargetControlID="txtProduct5Amt" 
                                                            WatermarkCssClass="watermarked" 
                                                            WatermarkText="Amount"> 
                                                        </asp:TextBoxWatermarkExtender>
                                                         <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender24" 
                                                            runat="server" Enabled="True" TargetControlID="txtProduct5Amt" FilterType="Custom, Numbers" ValidChars=".">
                                                        </asp:FilteredTextBoxExtender>
                                                        <des:RequiredTextValidator 
                                                            ID="reqAmount5" 
                                                            runat="server" 
                                                            ControlIDToEvaluate="txtProduct5Amt"  
                                                            Group="Product5" 
                                                            ErrorMessage="*" />
                                                    </td>                    
                                                    <td align="right"><des:ImageButton ID="btnCart5" ImageUrl="/images/cartdisabled.jpg" ToolTip="Add to Cart" Enabled="false" runat="server" CausesValidation="true" ValidationGroup="Product5" /></td>
                                                    <td align="right"><des:ImageButton ID="btnRemove5" ImageUrl="/images/removedisabled.jpg" ToolTip="Remove from Cart" Enabled="false" runat="server" CausesValidation="false" /></td>
                                                    <td align="right"><des:ImageButton ID="btnLookup5" ImageUrl="/images/lookupdisabled.jpg" ToolTip="Lookup Account" Enabled="false" runat="server" CausesValidation="false" /></td>
                                                </tr>
                                            </table>
                                        </asp:Panel>                
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="btnCart4"  EventName="Click" />
                                        <asp:AsyncPostBackTrigger ControlID="btnRemove4"  EventName="Click" />
                                        <asp:AsyncPostBackTrigger ControlID="btnCart5"  EventName="Click" />
                                        <asp:AsyncPostBackTrigger ControlID="btnRemove5"  EventName="Click" />
                                        <asp:AsyncPostBackTrigger ControlID="btnLookupOK"  EventName="Click" />
                                        <asp:AsyncPostBackTrigger ControlID="btnLookupCancel"  EventName="Click" />
                                        <asp:AsyncPostBackTrigger ControlID="btnBlockOverride"  EventName="Click" />
                                    </Triggers>                  
                                </asp:UpdatePanel>

                                <asp:UpdatePanel id="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>                     
                                        <table width="95%" style="margin-left:20px; background-color:#ECF0F0;">
                                            <tr>
                                                <td style="text-align:right; width:70%;"><asp:label ID="lblSubTotal" runat="server" Text="Sub Total: $0.00" Font-Bold="true" /></td>
                                                <td style="text-align:right; width:30%;"><asp:label ID="lblAmount" runat="server" Text="Total Amount: $0.00" Font-Bold="true" />
                                                <asp:HiddenField ID="hidAmount" runat="server" Value="0" /></td>
                                             </tr>                   
                                        </table> 
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="btnCart1"  EventName="Click" />
                                        <asp:AsyncPostBackTrigger ControlID="btnCart2"  EventName="Click" />
                                        <asp:AsyncPostBackTrigger ControlID="btnCart3"  EventName="Click" />
                                        <asp:AsyncPostBackTrigger ControlID="btnCart4"  EventName="Click" />
                                        <asp:AsyncPostBackTrigger ControlID="btnCart5"  EventName="Click" />
                                        <asp:AsyncPostBackTrigger ControlID="btnRemove2"  EventName="Click" />
                                        <asp:AsyncPostBackTrigger ControlID="btnRemove3"  EventName="Click" />
                                        <asp:AsyncPostBackTrigger ControlID="btnRemove4"  EventName="Click" />
                                        <asp:AsyncPostBackTrigger ControlID="btnRemove5"  EventName="Click" />
                                        <asp:AsyncPostBackTrigger ControlID="btnRecalculate"  EventName="Click" />
                                        <asp:AsyncPostBackTrigger ControlID="btnBlockOverride"  EventName="Click" />
                                    </Triggers> 
                                </asp:UpdatePanel>           
                            </div>    
                        </fieldset>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>

            <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                <ContentTemplate>     
		            <fieldset>
		                <legend>Payment Information</legend>
		                <asp:Panel ID="pnlCreditCard" runat="server" Visible="true" DefaultButton="btnSubmit" >
		                    <asp:HiddenField ID="CardData" runat="server"  />
                                <table style="width:90%; margin-left:20px; padding-top:10px;">
                                    <tr style="height:25px;">
                                        <td class="style11">Credit Card Number</td>
                                        <td class="style7">
                                            <asp:TextBox ID="tbCreditCardNumber" runat="server" MaxLength="20" Width="254px" CssClass="inputText" autocomplete="off"></asp:TextBox>
                                            <asp:FilteredTextBoxExtender 
                                                ID="tbCreditCardNumber_FilteredTextBoxExtender" 
                                                runat="server" 
                                                TargetControlID="tbCreditCardNumber" 
                                                FilterType="Custom, Numbers" 
                                                ValidChars="*"/>
                                            <asp:RegularExpressionValidator 
                                                ID="regexTextBox1"  ForeColor="Red"
                                                ControlToValidate="tbCreditCardNumber" 
                                                runat="server" 
                                                ValidationExpression="^[\s\S]{13,18}$" 
                                                Text="*"
                                                ErrorMessage="Credit card number is invalid" />
                                            <asp:RequiredFieldValidator 
                                                ID="rqCreditCardNumber" 
                                                runat="server"  ForeColor="Red"
                                                ControlToValidate="tbCreditCardNumber" 
                                                ErrorMessage="Credit card number is required" 
                                                SetFocusOnError="True" 
                                                ToolTip="Tool Tip" 
                                                Display="Dynamic" 
                                                Text="*"/>
                                        </td>
                                        <td class="style12">Exp. Date</td>
                                        <td class="style13">    
<%--                                            <telerik:RadComboBox ID="ddlMonth" runat="server" CausesValidation="false" Width="50px"></telerik:RadComboBox>
                                            <telerik:RadComboBox ID="ddlYear" runat="server" CausesValidation="False" Width="60px"></telerik:RadComboBox>&nbsp;--%>
                                            <asp:DropDownList ID="ddlMonth2" runat="server" CausesValidation="false" Width="50px"></asp:DropDownList>&nbsp;
                                            <asp:DropDownList ID="ddlYear2" runat="server" CausesValidation="False" Width="60px"></asp:DropDownList>&nbsp;
                                            <asp:LinkButton TabIndex="-2"  UseSubmitBehavior="false" CausesValidation="false"  ID="btnResetCCFields" runat="server" Font-Size="Small" Text="Reset CC Fields"  />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style11">CVV Code</td>
                                        <td class="style7">
                                            <asp:TextBox ID="tbSecurityCode" runat="server" MaxLength="4" Width="54px" CssClass="inputText" autocomplete="off"></asp:TextBox>
                                            <asp:FilteredTextBoxExtender 
                                                ID="tbSecurityCode_FilteredTextBoxExtender1" 
                                                runat="server" 
                                                TargetControlID="tbSecurityCode" 
                                                FilterType="Numbers"/>
                                            <asp:RegularExpressionValidator 
                                                ID="RegularExpressionValidator2" 
                                                runat="server"  ForeColor="Red"
                                                ControlToValidate="tbSecurityCode" 
                                                Display="Dynamic"  
                                                Text="*"
                                                ErrorMessage="Not a valid security code" 
                                                SetFocusOnError="True" 
                                                ValidationExpression="^[a-zA-Z0-9]{3,4}$"/>
                                            <asp:RequiredFieldValidator 
                                                ID="RequiredFieldValidator3" 
                                                runat="server"  ForeColor="Red"
                                                ControlToValidate="tbSecurityCode" 
                                                ErrorMessage="Security code is required" 
                                                SetFocusOnError="True" 
                                                ToolTip="Tool TIp" 
                                                Display="Dynamic" 
                                                Text="*"/>                            
                                        </td>
                                        <td>Country</td>
                                        <td>
                                            <telerik:RadComboBox 
                                                ID="ddlCountry" 
                                                runat="server" 
                                                ToolTip="Select Country"  
                                                CausesValidation="false"
                                                OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged" 
                                                AutoPostBack="true" >
                                            </telerik:RadComboBox>                                          
                                        </td>
                                    </tr>                    
                                    <tr>
                                        <td class="style11">First Name</td>
                                        <td class="style7">
                                            <asp:TextBox ID="tbFirstName" runat="server" MaxLength="20" Width="254px" CssClass="inputText"></asp:TextBox>
                                            <asp:FilteredTextBoxExtender 
                                                ID="tbFirstName_FilteredTextBoxExtender1" 
                                                runat="server" 
                                                TargetControlID="tbFirstName" 
                                                FilterType="Custom, LowercaseLetters, UppercaseLetters" 
                                                ValidChars=" " />
                                            <asp:RequiredFieldValidator 
                                                ID="RequiredFieldValidator1" 
                                                runat="server"  ForeColor="Red"
                                                ControlToValidate="tbFirstName" 
                                                ErrorMessage="First name is required" 
                                                SetFocusOnError="True" 
                                                ToolTip="Tool TIp" 
                                                Display="Dynamic" 
                                                Text="*"/>                            
                                        </td>
                                        <td class="style12">Last Name</td>
                                        <td class="style13">
                                            <asp:TextBox ID="tbLastName" runat="server" MaxLength="30" Width="254px" CssClass="inputText"></asp:TextBox>
                                            <asp:FilteredTextBoxExtender 
                                                ID="tbLastName_FilteredTextBoxExtender1" 
                                                runat="server" 
                                                TargetControlID="tbLastName" 
                                                FilterType="Custom, LowercaseLetters, UppercaseLetters" 
                                                ValidChars=" " />
                                            <asp:RequiredFieldValidator 
                                                ID="RequiredFieldValidator2" 
                                                runat="server"  ForeColor="Red"
                                                ControlToValidate="tbLastName" 
                                                ErrorMessage="Last name is required" 
                                                SetFocusOnError="True" 
                                                ToolTip="Tool TIp" 
                                                Display="Dynamic" 
                                                Text="*"/>                            
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
            
                            <asp:Panel ID="pnlACH" runat="server" Visible="false" DefaultButton="btnSubmit">
                                <asp:label 
                                    ID="lblCommercialMessage" 
                                    runat="server" 
                                    text="Note: please confirm with your bank that debits are allowed from the Company ID displayed on the confirmation page." 
                                    ForeColor="gray" 
                                    Font-Italic="true" 
                                    Font-Size="Small" 
                                    Visible="false" 
                                    CssClass="padding">
                                </asp:label>
                                <table style="width:90%; margin-left:20px;">            
                                    <tr style="height:25px;">
                                        <td class="style11">Account Type</td>
                                        <td class="style7">
                                            <telerik:RadComboBox  ID="ddlBankAccountType" runat="server" AutoPostBack="true" CausesValidation="false"
                                            EnableViewState="true"  DataTextField="name" DataValueField="value">
                                            </telerik:RadComboBox>
                                            <asp:XmlDataSource ID="XmlDataSource1" runat="server" DataFile="~/resource/bankAccountType.xml" />
                                           </td>
                                        <td>&nbsp;</td>
                                        <td><asp:CheckBox ID="ckNacha" runat="server" Text=" NACHA form on file" />
                                            <asp:CustomValidator ForeColor="Red" ID="ckValidator" runat="server" ErrorMessage="NACHA agreement is required" text="Required" Display="Dynamic" onservervalidate="ckValidator_ServerValidate"></asp:CustomValidator>                                            
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style11">Bank Routing No.</td>
                                        <td class="style7">
                                        <asp:TextBox ID="tbBankRoutingNumber" runat="server" MaxLength="9" Width="254px" CssClass="inputText"></asp:TextBox>
                                                            <asp:FilteredTextBoxExtender ID="tbBankRoutingNumber_FilteredTextBoxExtender2" 
                                                runat="server" TargetControlID="tbBankRoutingNumber" FilterType="Numbers" >
                                            </asp:FilteredTextBoxExtender>
                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" 
                                                ControlToValidate="tbBankRoutingNumber" Display="Dynamic"  Text="*" ForeColor="Red"
                                                ErrorMessage="Not a valid routing number" SetFocusOnError="True" ValidationExpression="^((0[0-9])|(1[0-2])|(2[1-9])|(3[0-2])|(6[1-9])|(7[0-2])|80)([0-9]{7})$"></asp:RegularExpressionValidator>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5"  ForeColor="Red"
                                                runat="server" ControlToValidate="tbBankRoutingNumber" ErrorMessage="Routing number is required" 
                                                SetFocusOnError="True" ToolTip="Tool TIp" Display="Dynamic" Text="*"></asp:RequiredFieldValidator>
                            
                            
                                               </td>
                                        <td class="style12">Bank Account No.</td>
                                        <td class="style13">
                                            <asp:TextBox ID="tbBankAccountNumber" runat="server" MaxLength="17" Width="254px" CssClass="inputText" autocomplete="off"></asp:TextBox>
                                                            <asp:FilteredTextBoxExtender ID="tbBankAccountNumber_FilteredTextBoxExtender1" 
                                                runat="server" TargetControlID="tbBankAccountNumber" FilterType="Numbers">
                                            </asp:FilteredTextBoxExtender>
                                                             <asp:RequiredFieldValidator ID="RequiredFieldValidator4"  ForeColor="Red"
                                                runat="server" ControlToValidate="tbBankAccountNumber" ErrorMessage="Bank account number is required" 
                                                SetFocusOnError="True" ToolTip="Tool TIp" Display="Dynamic" Text="*"></asp:RequiredFieldValidator>
                            
                                               </td>
                                    </tr>                   
                                    <tr>
                                        <td class="style11">First Name</td>
                                        <td class="style7">
                                            <asp:TextBox ID="tbBankAccountFirstName" runat="server" MaxLength="20" Width="254px" CssClass="inputText"></asp:TextBox>
                                                            <asp:FilteredTextBoxExtender ID="tbBankAccountFirstName_FilteredTextBoxExtender3" 
                                                runat="server" TargetControlID="tbBankAccountFirstName" FilterType="Custom, LowercaseLetters, UppercaseLetters" 
                                                 ValidChars=" ,'&">
                                            </asp:FilteredTextBoxExtender>
                      
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6"  ForeColor="Red"
                                                runat="server" ControlToValidate="tbBankAccountFirstName" ErrorMessage="First name on bank account is required" 
                                                SetFocusOnError="True" ToolTip="Tool TIp" Display="Dynamic"  Text="*"></asp:RequiredFieldValidator>
                                        </td>
                        
                                        <td class="style12">Last Name</td>
                                        <td class="style13">
                                            <asp:TextBox ID="tbBankAccountLastName" runat="server" MaxLength="30" Width="254px" CssClass="inputText"></asp:TextBox>
                                                            <asp:FilteredTextBoxExtender ID="tbBankAccountLastName_FilteredTextBoxExtender3" 
                                                runat="server" TargetControlID="tbBankAccountLastName" FilterType="Custom, LowercaseLetters, UppercaseLetters" 
                                                 ValidChars=" ,'&">
                                            </asp:FilteredTextBoxExtender>
                      
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7"  ForeColor="Red"
                                                runat="server" ControlToValidate="tbBankAccountLastName" ErrorMessage="Name on bank account is required" 
                                                SetFocusOnError="True" ToolTip="Tool TIp" Display="Dynamic" Text="*"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>     
                                </table>                  
                            </asp:Panel>
    
            
            
            <table style="width:90%; margin-left:20px;">
                <tr>
                    <td class="style11"><asp:Label id="lblBillingZip" runat="server" Text="Zip"></asp:Label></td>
                    <td class="style7">
                        <asp:TextBox ID="txtZip" runat="server" Width="70px" MaxLength="5" CssClass="inputText"></asp:TextBox>
                        <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender9" 
                            runat="server" Enabled="True" TargetControlID="txtZip" FilterType="Numbers">
                        </asp:FilteredTextBoxExtender>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server"  ForeColor="Red"
                        ControlToValidate="txtZip" Display="Dynamic"  Text="*"
                        ErrorMessage="Not a valid zip code" SetFocusOnError="True" ValidationExpression="^[a-zA-Z0-9]{5,9}$"></asp:RegularExpressionValidator>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ErrorMessage="Zip is required" ControlToValidate="txtZip"
                        etFocusOnError="True" ToolTip="Tool TIp" Text="*"></asp:RequiredFieldValidator>
                    </td>
                     
                </tr>
               <tr>
                    <td class="style11">Address 1</td>
                    <td class="style7">
                        <asp:TextBox ID="txtAddress" runat="server" Width="254px" MaxLength="40" CssClass="inputText" Text=""></asp:TextBox>
                        <asp:FilteredTextBoxExtender ID="TextBox1_FilteredTextBoxExtender" 
                            runat="server" Enabled="True" TargetControlID="txtAddress" FilterType="Custom, LowercaseLetters, UppercaseLetters, Numbers" ValidChars=" ,.&-'">
                        </asp:FilteredTextBoxExtender>
                       
                    </td>
                     <td class="style12">Email Address</td>
                    <td class="style13">
                        <asp:TextBox ID="tbEmailAddress" runat="server" MaxLength="100" Width="254px" CssClass="inputText"></asp:TextBox>
                    <asp:FilteredTextBoxExtender ID="tbEmailAddress_FilteredTextBoxExtender" 
                        runat="server" filtertype="Numbers,UpperCaseLetters,LowerCaseLetters,Custom" TargetControlID="tbEmailAddress" 
                        ValidChars="-@._">
                    </asp:FilteredTextBoxExtender>
                     
                    <asp:RegularExpressionValidator ID="regEmail" runat="server"  ForeColor="Red"
                        ControlToValidate="tbEmailAddress" Display="Dynamic" ErrorMessage="Invalid Email Address" ToolTip="Invalid Email Address" 
                        SetFocusOnError="True"
                        ValidationExpression="<$|^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,6}$">*</asp:RegularExpressionValidator>
                    </td>
                   
                </tr>
                
                <tr >
                    <td class="style11">Address 2</td>
                    <td class="style7">
                        <asp:TextBox ID="txtAddress2" runat="server" Width="254px" MaxLength="40" CssClass="inputText"></asp:TextBox>
                        <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" 
                            runat="server" Enabled="True" TargetControlID="txtAddress2" FilterType="Custom, LowercaseLetters, UppercaseLetters, Numbers" ValidChars=" ,.&-'">
                        </asp:FilteredTextBoxExtender>
                    </td>
                     <td class="style12">Phone Number</td>
                    <td class="style13">
                         <asp:TextBox ID="tbHomePhone" runat="server" MaxLength="10" Width="254px" CssClass="inputText"></asp:TextBox>
                    <asp:FilteredTextBoxExtender ID="tbHomePhone_FilteredTextBoxExtender" 
                    runat="server" filtertype="Numbers" TargetControlID="tbHomePhone" >
                    </asp:FilteredTextBoxExtender>
                    
                    <asp:RegularExpressionValidator ID="regHomePhone" runat="server"  ForeColor="Red"
                    ControlToValidate="tbHomePhone" Display="Dynamic" ErrorMessage="Invalid Phone Number" ToolTip="Invalid Home Phone"
                    SetFocusOnError="True" ValidationExpression="(([2-9]{1})([0-9]{2})([0-9]{3})([0-9]{4}))$">*</asp:RegularExpressionValidator>
                    </td>
                   
                </tr>
                
                <tr valign="top">
                    <td class="style11">City</td>
                    <td class="style7">
                        <asp:TextBox ID="txtCity" runat="server" Width="254px" MaxLength="40" CssClass="inputText" ></asp:TextBox>
                        <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" 
                            runat="server" Enabled="True" TargetControlID="txtCity" FilterType="Custom, LowercaseLetters, UppercaseLetters, Numbers" ValidChars=" ,.&-'">
                        </asp:FilteredTextBoxExtender>
                    </td>
                    <td></td>
                    <td></td>
                </tr>
                <asp:Panel ID="pnlState" runat="server">
                <tr valign="top" style="height:25px;">
                    <td class="style11"><asp:Label ID="lblBillingState" runat="server" Text="State"></asp:Label></td>
                    <td class="style7">
                       <telerik:RadComboBox borderColor="blue" ID="ddlState" CausesValidation="false" runat="server"  Sort="Ascending" DataTextField="name" DataValueField="abbreviation"
                                TabIndex="5"    />
                        &nbsp;
                                    
                        <asp:XmlDataSource runat="server" ID="XmlDataSource2" DataFile="~/resource/usStates.xml" />
                    </td>
                    
                    <td class="style12" rowspan="2" valign="top">Notes</td>
                    <td class="style13" rowspan="2">
                        

                        <asp:TextBox ID="txtNotes" TextMode="MultiLine" runat="server" Columns="30" Rows="4" ToolTip="Enter payment notes"
                           onkeyup="LimtCharacters(this,140,'lblcount');" />
                            <br>
                            <label id="lblcount" style="background-color:#E2EEF1;font-weight:bold;">200</label>
                            characters left
                            <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" 
                            runat="server" Enabled="True" TargetControlID="txtNotes" FilterType="Custom, LowercaseLetters, UppercaseLetters, Numbers" ValidChars="!,.$@-' ">
                        </asp:FilteredTextBoxExtender>
                    </td>
                    
                  </tr>
                </asp:Panel>
               
                            <tr><td colspan="4">&nbsp;</td></tr>
                        </table>
                    </fieldset>
                    <br />
                    <asp:HiddenField ID="hidAssembly" runat="server" />
                </ContentTemplate>
                <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="RadioButtonList1"  EventName="SelectedIndexChanged" />
                        <asp:AsyncPostBackTrigger ControlID="ddlAUClient"  EventName="SelectedIndexChanged" />
                        <asp:AsyncPostBackTrigger ControlID="btnReCalculate"  EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="btnSubmit"  EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="btnLookupOK"  EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="btnLookupCancel"  EventName="Click" />
                </Triggers> 
            </asp:UpdatePanel> 
 
 <asp:UpdatePanel ID="UpdatePanel5" runat="server" UpdateMode="Conditional">
    <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnRecalculate"  EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btnSubmit"  EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btnCart1"  EventName="Click" />
    </Triggers>
    <ContentTemplate>
    <asp:Button id="btnShowPopup" runat="server" style="display:none" />
            <asp:ModalPopupExtender ID="mdlPopup" runat="server" TargetControlID="btnShowPopup" PopupControlID="pnlMessage"
                CancelControlID="btnClose" BackgroundCssClass="modalBackground"></asp:ModalPopupExtender>
            <asp:Panel ID="pnlMessage" runat="server" style="display:none; width:350px; background-color:White; border-width:2px; border-color:#336699; border-style:solid; padding:10px;" SkinID="PopUpPanel">
                <asp:UpdatePanel ID="upLookup" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:Label ID="lblMessageTitle" runat="server" Text="Invalid Payment/Product Selection"></asp:Label>
                            <div id="Message" style="padding-left:10px;">
                                <asp:Label ID="lblMessage" runat="server" CssClass="ErrorSummary"></asp:Label>
                            </div>
                    </ContentTemplate>
                </asp:UpdatePanel>            
                <div style="text-align: right; width: 100%; margin-top: 5px;">
                    <asp:Button ID="btnClose" runat="server" Text="Close" Width="50px" CausesValidation="false" />
                </div>
            </asp:Panel>
    </ContentTemplate>
 </asp:UpdatePanel>

             <asp:UpdatePanel ID="UpdatePanel10" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:Button id="btnShowBlock" runat="server" style="display:none" />            
                    <asp:ModalPopupExtender ID="modalBlock" runat="server" TargetControlID="btnShowBlock" PopupControlID="pnlPayBlock"
                        CancelControlID="btnBlockCancel" BackgroundCssClass="modalBackground"></asp:ModalPopupExtender>
                    <asp:Panel ID="pnlPayBlock" runat="server" style="display:none; width:450px; background-color:White; border-width:2px; border-color:#336699; border-style:solid; padding:10px;" SkinID="PopUpPanel">
                        <asp:UpdatePanel ID="UpdatePanel11" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <div style="padding-left: 5px;">
                                    <asp:Label ID="lblProductNumber" runat="server" Visible="false" />
                                    <asp:Label ID="Label3" runat="server" Text="This account is blocked from this payment method. Click below to continue." Font-Bold="true"></asp:Label><br />
                                    <br />
                                    <asp:CheckBox ID="ckBlockOverride" runat="server" Checked="false" />
                                    I agree to override the block status.
                                    <br />
                                </div>
                                <div style="padding-left: 8px; padding-bottom: 10px; padding-top: 10px;">
                                    <asp:Label ID="lblBlockBy2" runat="server" /><br />
                                    <asp:Label ID="lblBlockDate2" runat="server" /><br />
                                    <asp:Label ID="lblBlockComment2" runat="server" /><br />
                                </div>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btnRecalculate"  EventName="Click" />
                                <asp:AsyncPostBackTrigger ControlID="btnSubmit"  EventName="Click" />
                                
                                <asp:AsyncPostBackTrigger ControlID="btnCart1" EventName="Click" />
                                <asp:AsyncPostBackTrigger ControlID="btnCart2" EventName="Click" />
                                <asp:AsyncPostBackTrigger ControlID="btnCart3" EventName="Click" />
                                <asp:AsyncPostBackTrigger ControlID="btnCart4" EventName="Click" />
                                <asp:AsyncPostBackTrigger ControlID="btnCart5" EventName="Click" />
                            </Triggers>
                        </asp:UpdatePanel>
                        
                        <div style="text-align: right; width: 100%; margin-top: 5px;">
                            <asp:Button ID="btnBlockOverride" runat="server" Text=" Yes, Override Block " Height="25px" CausesValidation="false" />
                            <asp:Button ID="btnBlockCancel" runat="server" Text=" No, Cancel Transaction " Height="25px" CausesValidation="false" />
                        </div> 
                    </asp:Panel>
                </ContentTemplate>
             </asp:UpdatePanel>
 
 <asp:UpdatePanel ID="UpdatePanel6" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
        <asp:Button id="btnShowLookup" runat="server" style="display:none" />
        <asp:ModalPopupExtender ID="modalLookup" runat="server" TargetControlID="btnShowLookup" PopupControlID="pnlLookup"
                BackgroundCssClass="modalBackground"></asp:ModalPopupExtender>
        <asp:Panel ID="pnlLookup" runat="server" style="display:none; width:450px; 
                background-color:White; border-width:2px; border-color:#336699; border-style:solid; padding:10px;" SkinID="PopUpPanel">
            <asp:UpdatePanel ID="UpdatePanel7" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:Label ID="lblProductTextNumber" runat="server" Text="" CssClass="popupTitle" /><asp:Label ID="lblProductNumberLookup" runat="server" Visible="false" />
                    <div style="padding-left:8px; padding-bottom:10px; padding-top:10px;"><asp:Label ID="lblLookupMessage" runat="server" /></div>
                    <div style="padding-left:5px;"><asp:CheckBox ID="ckUseAddress" runat="server" Text="Use this address for billing information" Checked="true" /><br /></div>
                    <asp:GridView ID="grdLookup" runat="server" AutoGenerateColumns="true" GridLines="None" BorderStyle="none"  ShowHeader="false" cellspacing="10"></asp:GridView><br />
                    <asp:Panel ID="pnlBlock" runat="server" Visible="false">
                        <div style="padding-left:8px; padding-bottom:10px; padding-top:10px;"><asp:Label ID="lblBlockMsg" runat="server"></asp:Label></div>
                        <div style="padding-left:5px;"> 
                            <asp:Label ID="Label1" runat="server" Text="This account is blocked from this payment method. Click below to continue." Font-Bold="true"></asp:Label><br />
                            <asp:CheckBox ID="ckOverrideBlock" runat="server" checked="false" /> I agree to override the block status. <br />
                        </div>                        
                        <div style="padding-left:8px; padding-bottom:10px; padding-top:10px;">
                            <asp:Label ID="lblBlockBy" runat="server" /><br />
                            <asp:Label ID="lblBlockDate" runat="server" /><br />
                            <asp:Label ID="lblBlockComment" runat="server" /><br />
                        </div>
                    </asp:Panel>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnLookup1"  EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="btnLookup2"  EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="btnLookup3"  EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="btnLookup4"  EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="btnLookup5"  EventName="Click" />
                </Triggers>
            </asp:UpdatePanel>         
                            
            <div style="text-align: right; width: 100%; margin-top: 5px;">
                <asp:Button ID="btnLookupOK" runat="server" Text="Continue"  CausesValidation="false" />
                <asp:Button ID="btnLookupCancel" runat="server" Text="Cancel"  CausesValidation="false" />
            </div> 
        </asp:Panel>
    </ContentTemplate>
 </asp:UpdatePanel>
 
 
 <asp:UpdatePanel ID="UpdatePanel8" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
    <asp:Button id="btnShowError" runat="server" style="display:none" />
            <asp:ModalPopupExtender ID="modalLookupError" runat="server" TargetControlID="btnShowError" PopupControlID="pnlLookupError"
                CancelControlID="btnLookupError"   BackgroundCssClass="modalBackground"></asp:ModalPopupExtender>
            <asp:Panel ID="pnlLookupError" runat="server" style="display:none; width:350px; background-color:White; border-width:2px; border-color:#336699; border-style:solid; padding:10px;" SkinID="PopUpPanel">
                <asp:UpdatePanel ID="UpdatePanel9" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:Label ID="lblProductTextNumberError" runat="server" Text="" CssClass="popupTitle" /><asp:Label ID="lblProductNumberLookupError" runat="server" Visible="false" /><br />
                        <asp:Label ID="lblLookupMessageError" runat="server" />
                    </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnLookup1"  EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="btnLookup2"  EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="btnLookup3"  EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="btnLookup4"  EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="btnLookup5"  EventName="Click" />
                   
                        </Triggers>
                </asp:UpdatePanel>        
                        
                <div style="text-align: right; width: 100%; margin-top: 5px;">
                    <asp:Button ID="btnLookupError" runat="server" Text="Close"  CausesValidation="false" />
                </div> 
            </asp:Panel>
    </ContentTemplate>
 </asp:UpdatePanel>

 <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
        <Triggers>
            
            <asp:AsyncPostBackTrigger ControlID="btnReCalculate"  EventName="Click" />
        
        </Triggers>
          <ContentTemplate>     
       
            <des:ImageButton ID="btnReCalculate" ImageUrl="/images/btnRecalculate.jpg" runat="server" CausesValidation="false" ToolTip="Recalculate Payment" Text="Recalculate" Enabled="true" CssClass="submit" />       
            <des:Imagebutton ID="btnSubmit"  ImageUrl="/images/btnSubmitPaymentDisabled.jpg" runat="server" ToolTip="Submit Payment" text="Submit Payment" Enabled="false" CssClass="submit" OnClick="btnSubmit_Click" DisableOnSubmit="true" />

        <br /><br />
    </ContentTemplate>
</asp:UpdatePanel>
    

  </div>
  <!--EndContent-->
  
   
        <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                      <ProgressTemplate>
                            <div id="progressBackgroundFilter">
                                
                            </div>
                            <div id="processMessage"> Processing ...<br />
                                 
                            </div>
                      </ProgressTemplate>
        </asp:UpdateProgress>
 
 
 
   
  </div>
  
  
        <div id="dvObjectHolder">  </div>
        <script language="JavaScript" type="text/javascript">
            if (window.navigator.appName.toLowerCase().indexOf("netscape") != -1) { // set object for Netscape:
                document.getElementById('dvObjectHolder').innerHTML =
                "<applet type=\"application/x-java-applet;version=1.5\"" +
       		    "codebase =\"/magensa/\"" +
		        "archive = \"/magensa/JMTCardReader.jar\"" +
		        "code=\"JMTCardReader.class\"" +
		        "name=\"JMSR\"" +
   		        "scriptable=\"true\"" +
  		        "style=\"visibility:hidden;\"" +
		        "mayscript=\"mayscript\"" +
		        "pluginspage=\"http://java.com/en/download/index.jsp\"" +
                ">" +
                "<param name=\"cache_option\" value=\"No\">" +
		        "<param name=\"classloader_cache\" value=\"true\">" +
          	    "</applet>";
            }
            else if (window.navigator.appName.toLowerCase().indexOf('internet explorer') != -1) { //set object for IE
                document.getElementById('dvObjectHolder').innerHTML =
                "<object type=\"application/x-java-applet;version=1.5\"" +
       		    "codebase =\"/magensa/\"" +
		        "archive = \"/magensa/JMTCardReader.jar\"" +
		        "code=\"JMTCardReader.class\"" +
		        "name=\"JMSR\"" +
                "height=\"0\" width=\"0\" >" +
                "<param name=\"mayscript\" value=\"true\">" +
    		    "<param name=\"classloader_cache\" value=\"true\">" +
	            "<param name=\"cache_option\" value=\"No\">" +
    		    " </object>"
            }
        </script>
  
        <custom:timeout ID="timeoutControl" runat="server" />
    </form>
      
    <div class="clear"></div> 
    <div class="footer_shadow">
        <custom:footer ID="Footer" runat="server" />
    </div>

</body> 
</html> 
 
 
