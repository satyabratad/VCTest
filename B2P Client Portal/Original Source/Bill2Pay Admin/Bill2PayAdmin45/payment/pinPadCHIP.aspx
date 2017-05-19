<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="pinPadCHIP.aspx.vb" Inherits="Bill2PayAdmin45.pinPadCHIP" %>
<%@ Register TagPrefix="tripos" TagName="PaymentStatusMessage" Src="~/UserControls/StatusMessage.ascx" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">

<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Make a Payment :: Bill2Pay Administration</title>
    <link rel="stylesheet" type="text/css" href="../css/main.css" media="all" />
    <link rel="stylesheet" type="text/css" href="../css/content_normal.css" title="normal" />
    <link rel="stylesheet" type="text/css" href="../Content/themes/base/minified/jquery.ui.all.min.css" />
    
</head>


<body>

    <script lang="JavaScript" type="text/javascript">


        function LimtCharacters(txtMsg, CharLength, indicator) {
            chars = txtMsg.value.length;
            document.getElementById(indicator).innerHTML = CharLength - chars;
            if (chars > CharLength) {
                txtMsg.value = txtMsg.value.substring(0, CharLength);
            }
        }
       
    </script>
    
    <form id="IPAD_FRM" runat="server" autocomplete="off">
      <div class="top_header_main"></div>
      <custom:menu ID="menu" runat="server" />
      <div class="top_header_shadow"></div>
  
      <div class="content">
        <custom:logout ID="logout" runat="server" />
        <div class="header">Make a Counter Payment</div>
        <div class="header_border"></div>   
    
        <asp:ToolkitScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="true" />
        <des:PageManager id="PageManager2" runat="server" AJAXFramework="MicrosoftAJAX" ChangeStyleOnControlsWithError="True"></des:PageManager>
  
        <!--StartContent-->
        <div class="main_content">

            <tripos:PaymentStatusMessage id="psmErrorMessage" runat="server" />

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
                        <des:RequiredTextValidator ID="RequiredFieldValidator8" ControlIDToEvaluate="ddlOffice"                                                  
                                                    runat="server" 
                                                    text="*" 
                                                    ErrorMessage="Office is required"></des:RequiredTextValidator>
                    </div>    
                  
        <div class="clear"></div> 
                    
 
                    <div id="ProductTypes" style="padding-bottom:5px; margin-top:5px;">
                       
                        <fieldset>
                            <legend>Product Information</legend>
                            <div id="Products" style="padding-top:5px; padding-bottom:5px;">            
                                <asp:UpdatePanel id="updProduct1" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:Panel ID="pnlProduct1" runat="server" Visible="true" DefaultButton="btnCart1">
                                            <table style="margin-left:20px; width:98%;"  >
                                                <tr style="height:25px; vertical-align: bottom;">
                                                    <td>
                                                        <telerik:RadComboBox ID="ddlProduct1" runat="server"  onSelectedIndexChanged="ddlProduct1_SelectedIndexChanged" 
                                                        AutoPostBack="true" CausesValidation="false" AppendDataBoundItems="true">                               
                                                        </telerik:RadComboBox>
                                                     </td>
                                                     <td style="width:170px; text-align:left;">
                                                        <asp:TextBox id="txtProduct1Input" runat="server" Width="150px" MaxLength="40" Height="20px"></asp:TextBox>
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
                                                        <asp:TextBox id="txtProduct1aInput" runat="server" Width="150px"  MaxLength="40" ReadOnly="true" Enabled="false" Height="20px"></asp:TextBox>
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
                                                         <asp:TextBox id="txtProduct1bInput" runat="server" Width="150px" MaxLength="40" ReadOnly="true" Enabled="false" Height="20px"></asp:TextBox>
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
                                                         <asp:TextBox id="txtProduct1Amt" runat="server" Width="100px" MaxLength="10" Height="20px"></asp:TextBox>
                                                         <asp:TextBoxWatermarkExtender 
                                                           ID="TextBoxWatermarkExtender2" 
                                                           runat="server" 
                                                           Enabled="True" 
                                                           TargetControlID="txtProduct1Amt" 
                                                           WatermarkCssClass="watermarked" 
                                                           WatermarkText="Amount"> 
                                                         </asp:TextBoxWatermarkExtender>
                                                          <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" 
                                                            runat="server" Enabled="True" TargetControlID="txtProduct1Amt" FilterType="Custom, Numbers" ValidChars=".">
                                                        </asp:FilteredTextBoxExtender>
                                                         <des:RequiredTextValidator ID="reqAmount1" runat="server" ControlIDToEvaluate="txtProduct1Amt"  Group="Product1" ErrorMessage="*" />
                                                     </td>
                                                     <asp:Panel id="CartButtons1" runat="server" Visible="true">
                                                         <td align="right"><des:ImageButton ID="btnCart1" ImageUrl="/images/cart.jpg" CssClass="noborder" runat="server" ToolTip="Add to Cart" Enabled="false" CausesValidation="true" ValidationGroup="Product1" /></td>
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
                                                        <asp:TextBox id="txtProduct2Input" runat="server" Width="150px"  MaxLength="40" Height="20px"></asp:TextBox>
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
                                                        <asp:TextBox id="txtProduct2aInput" runat="server" Width="150px" MaxLength="40" ReadOnly="true" Enabled="false" Height="20px"></asp:TextBox>
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
                                                        <asp:TextBox id="txtProduct2bInput" runat="server" Width="150px" MaxLength="40" ReadOnly="true" Enabled="false" Height="20px"></asp:TextBox>
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
                                                    <td>$&nbsp;<asp:TextBox id="txtProduct2Amt" runat="server" Width="100px" Height="20px"></asp:TextBox>
                                                        <asp:TextBoxWatermarkExtender 
                                                           ID="TextBoxWatermarkExtender7" 
                                                           runat="server" 
                                                           Enabled="True" 
                                                           TargetControlID="txtProduct2Amt" 
                                                           WatermarkCssClass="watermarked" 
                                                           WatermarkText="Amount"> 
                                                        </asp:TextBoxWatermarkExtender>
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
                                                    <td style="width:170px;">
                                                        <asp:TextBox id="txtProduct3Input" runat="server" Width="150px" MaxLength="40" Height="20px"></asp:TextBox>
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
                                                    <td style="width:170px;">
                                                        <asp:TextBox id="txtProduct3aInput" runat="server" Width="150px" MaxLength="40" ReadOnly="true" Enabled="false" Height="20px"></asp:TextBox>
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
                                                    <td style="width:170px;"><asp:TextBox id="txtProduct3bInput" runat="server" Width="150px" Height="20px"  MaxLength="40" ReadOnly="true" Enabled="false"></asp:TextBox>
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
                                                    <td>$&nbsp;<asp:TextBox id="txtProduct3Amt" runat="server" Width="100px" Height="20px"></asp:TextBox>
                                                        <asp:TextBoxWatermarkExtender 
                                                           ID="TextBoxWatermarkExtender8" 
                                                           runat="server" 
                                                           Enabled="True" 
                                                           TargetControlID="txtProduct3Amt" 
                                                           WatermarkCssClass="watermarked" 
                                                           WatermarkText="Amount"> 
                                                        </asp:TextBoxWatermarkExtender>
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
                                                        <asp:TextBox id="txtProduct4Input" runat="server" Width="150px" MaxLength="40" Height="20px"></asp:TextBox>
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
                                                        <asp:TextBox id="txtProduct4aInput" runat="server" Width="150px" MaxLength="40" ReadOnly="true" Enabled="false" Height="20px"></asp:TextBox>
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
                                                        <asp:TextBox id="txtProduct4bInput" runat="server" Width="150px" MaxLength="40" ReadOnly="true" Enabled="false" Height="20px"></asp:TextBox>
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
                                                        <asp:TextBox id="txtProduct4Amt" runat="server" Width="100px" Height="20px"></asp:TextBox>
                                                        <asp:TextBoxWatermarkExtender 
                                                            ID="TextBoxWatermarkExtender9" 
                                                            runat="server" 
                                                            Enabled="True" 
                                                            TargetControlID="txtProduct4Amt" 
                                                            WatermarkCssClass="watermarked" 
                                                            WatermarkText="Amount"/> 
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
                                                        <asp:TextBox id="txtProduct5Input" runat="server" Width="150px"  MaxLength="40" Height="20px"></asp:TextBox>
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
                                                        <asp:TextBox id="txtProduct5aInput" runat="server" Width="150px" MaxLength="40" ReadOnly="true" Enabled="false" Height="20px"></asp:TextBox>
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
                                                        <asp:TextBox id="txtProduct5bInput" runat="server" Width="150px"  MaxLength="40" ReadOnly="true" Enabled="false" Height="20px"></asp:TextBox>
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
                                                        <asp:TextBox id="txtProduct5Amt" runat="server" Width="100px" Height="20px"></asp:TextBox>
                                                        <asp:TextBoxWatermarkExtender 
                                                            ID="TextBoxWatermarkExtender10" 
                                                            runat="server" 
                                                            Enabled="True" 
                                                            TargetControlID="txtProduct5Amt" 
                                                            WatermarkCssClass="watermarked" 
                                                            WatermarkText="Amount"> 
                                                        </asp:TextBoxWatermarkExtender>
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
                                                <td style="text-align:right;"><asp:label ID="lblSubTotal" runat="server" Text="Sub Total: $0.00" Font-Bold="true" /></td>
                                                
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
                                        
                                    </Triggers> 
                                </asp:UpdatePanel>           
                            </div>    
                        </fieldset>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
            
            <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="always">
                <ContentTemplate>    
                    <asp:Panel ID="pnlPaymentInformation" runat="server" Visible="false">
                       
		            <fieldset>
		                <legend>Payment Information</legend>
                        <br />
                         <p style="padding-left:10px;">Please enter email address for payment receipt and any notes before continuing to the payment screen.</p>
                        <br />
                       
                            <table style="width:95%; padding-left:10px;">
                            <tr>
                   
                                <td style="vertical-align:top;">Email Address:</td>
                                <td style="vertical-align:top;">
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
                                <td style="vertical-align:top;">Notes:</td>
                    
                                <td>
                                     <asp:TextBox ID="txtNotes" TextMode="MultiLine" runat="server" Rows="2" Columns="5" Width="400px" Height="60px" ToolTip="Enter payment notes" onkeyup="LimtCharacters(this,140,'lblcount');" />
                            <br>
                            <label id="lblcount" style="background-color:#E2EEF1;font-weight:bold;">200</label>
                            characters left
                                        <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" 
                                        runat="server" Enabled="True" TargetControlID="txtNotes" FilterType="Custom, LowercaseLetters, UppercaseLetters, Numbers" ValidChars="!,.$@-' ">
                                    </asp:FilteredTextBoxExtender>
                                </td>   
                            </tr>
                                 </table>
                        <span style="padding-left:10px;">
                            <asp:LinkButton ID="btnContinue" runat="server" text="Continue to Payment Screen >>" ToolTip="Continue" Width="200px" />
                        </span>
                    <br />
                    <br />
                    <br />
                    </fieldset>
                   
                  
                    </asp:Panel>
                </ContentTemplate>
                <Triggers>
                       
                        <asp:AsyncPostBackTrigger ControlID="ddlAUClient"  EventName="SelectedIndexChanged" />                        
                        <asp:AsyncPostBackTrigger ControlID="btnLookupOK"  EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="btnLookupCancel"  EventName="Click" />
                       
                </Triggers> 
            </asp:UpdatePanel> 
 
 <asp:UpdatePanel ID="UpdatePanel5" runat="server" UpdateMode="Conditional">
    <Triggers>
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
                         BackgroundCssClass="modalBackground"></asp:ModalPopupExtender>
                    <asp:Panel ID="pnlPayBlock" runat="server" style="display:none; width:450px; background-color:White; border-width:2px; border-color:#336699; border-style:solid; padding:10px;" SkinID="PopUpPanel">
                        <asp:UpdatePanel ID="UpdatePanel11" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <div style="padding-left: 5px;">
                                    <asp:Label ID="lblProductNumber" runat="server" Visible="false" />
                                    <asp:Label ID="Label1" runat="server" Text="This account is blocked from this payment method. Click below to continue." Font-Bold="true"></asp:Label><br />
                                    <br />
                                    <asp:CheckBox ID="ckBlockOverrideCC" runat="server" Checked="false" Text=" I agree to override the credit card block status." />
                                    
                                    <br />
                                    <asp:CheckBox ID="ckBlockOverrideACH" runat="server" Checked="false" Text=" I agree to override the bank account block status." />
                                    
                                    <br />

                                </div>
                                <div style="padding-left: 8px; padding-bottom: 10px; padding-top: 10px;">
                                    <asp:Label ID="lblBlockBy2" runat="server" /><br />
                                    <asp:Label ID="lblBlockDate2" runat="server" /><br />
                                    <asp:Label ID="lblBlockComment2" runat="server" /><br />
                                </div>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btnCart1" EventName="Click" />
                                <asp:AsyncPostBackTrigger ControlID="btnCart2" EventName="Click" />
                                <asp:AsyncPostBackTrigger ControlID="btnCart3" EventName="Click" />
                                <asp:AsyncPostBackTrigger ControlID="btnCart4" EventName="Click" />
                                <asp:AsyncPostBackTrigger ControlID="btnCart5" EventName="Click" />
                            </Triggers>
                        </asp:UpdatePanel>
                        
                        <div style="text-align: right; width: 100%; margin-top: 5px;">
                            <asp:Button ID="btnBlockOverride" runat="server" Text=" Continue " CssClass="buttons" Height="25px" CausesValidation="false" />
                           
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
                            <asp:Label ID="lblBlockedDescriptionCreditCard" runat="server" Font-Bold="true"></asp:Label><br /><br />
                                <asp:CheckBox ID="ckOverrideBlockCC" runat="server" checked="false" Visible="false" Text=" I agree to override the block status." />  <br />
                            <asp:Label ID="lblBlockedDescriptionACH" runat="server" Font-Bold="true"></asp:Label><br /><br />                            
                                <asp:CheckBox ID="ckOverrideBlockACH" runat="server" checked="false" Visible="false" Text=" I agree to override the block status." />  <br />
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
               <asp:Button ID="btnLookupOK" runat="server" Text="Continue"  CausesValidation="false" Width="100px" Height="25px" />
                <asp:Button ID="btnLookupCancel" runat="server" Text="Cancel"  CausesValidation="false" Width="100px" Height="25px"  />
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


        <br /><br />
  

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
 
 <asp:PlaceHolder ID="PlaceHolder1" runat="server">
            <script type="text/javascript" language="javascript">
                
            </script>

           
        </asp:PlaceHolder> 
 
   
  </div>
        

        <div id="dvObjectHolder">  </div>
        <br /><br />
         
         <custom:timeout ID="timeoutControl" runat="server" />



    </form>
      
    <div class="clear"></div> 
    <div class="footer_shadow">
        <custom:footer ID="Footer" runat="server" />
    </div>

</body> 
</html> 
 
 