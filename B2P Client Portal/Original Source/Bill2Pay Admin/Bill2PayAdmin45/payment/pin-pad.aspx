<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="pin-pad.aspx.vb" Inherits="Bill2PayAdmin45.pin_pad1" %>

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
    
    <form id="IPAD_FRM" runat="server" defaultbutton="btnSubmit" autocomplete="off">
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
                                                          <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" 
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
                         <div style="padding-left:10px;">
                            <des:ValidationSummary id="ValidationSummary1"  runat="server" CssClass="ErrorSummary"  HeaderText="There's an error in the information you entered. Please correct the following errors and continue."
			                    HyperLinkToFields="True" ScrollIntoView="Top"  DisplayMode="bulletList">
	                        </des:ValidationSummary>
                        </div>
                         <div id="PaymentType" style="float:left; padding:5px; background:none;">
                        <asp:RadioButtonList ID="RadioButtonList1" CssClass="noborder" runat="server" RepeatDirection="horizontal" 
                            CellSpacing="10" OnSelectedIndexChanged="RadioButtonList1_SelectedIndexChanged" AutoPostBack="true" CausesValidation="true" >
                            <asp:ListItem Text=" Credit Card" Value="CC" Selected="True" Enabled="false" ></asp:ListItem>
                            <asp:ListItem Text=" Bank Account" Value="ACH" Enabled="false"></asp:ListItem>
                        </asp:RadioButtonList>  
                       </div>    
                    
                    <div class="clear"></div> 
                        <asp:Panel ID="pnlCreditCard" runat="server" Visible="true" DefaultButton="btnSubmit">

                            <div style="padding-left:10px;">
                                <asp:ImageButton ImageUrl="/images/btnSwipe.jpg" ID="btnStartReader" runat="server" ToolTip="Click to Swipe Card" CssClass="submit" CausesValidation="false" OnClientClick="ProcessCard()"  />
		                 
                                <span style="padding-left:10px; vertical-align:top;">
		                            <b>Reader status:</b>&nbsp;&nbsp;<asp:Label ID="lblStatus" runat="server" ForeColor="#FF0000" Text="Click button to scan card or enter manually." Font-Bold="true" />
		                        </span>
                            </div>
		                  
		                    <asp:HiddenField ID="txtSwiped" runat="server"  />
		                    <asp:HiddenField ID="txtTrack1" runat="server" />
		                    <asp:HiddenField ID="txtTrack2Data" runat="server" />
		                    <asp:HiddenField ID="txtTrackKSN" runat="server" />
		                    <asp:HiddenField ID="txtCreditDebit" runat="server" />
		                    <asp:HiddenField ID="txtPINKSN" runat="server" />
		                    <asp:HiddenField ID="txtPINEPB" runat="server" />
		                    <asp:HiddenField ID="txtPrintData" runat="server" />
		                    <asp:HiddenField ID="txtPrintStatus" runat="server" />
		                    <asp:HiddenField ID="txtAllowCredit" runat="server" />
		                    <asp:HiddenField ID="txtAllowDebit" runat="server" />
		                    <asp:HiddenField ID="txtSwipeStatus" runat="server" />
		                    <br />
                      
                            <div style="padding-left:20px; padding-top:5px; padding-bottom:5px;">
		                        <table style="width: 90%; background-color:#cdd4db;">
		                           	    
		                            <tr>	       
		                                <td class="style11">
                                            <b>Credit Card Total:</b>
		                                </td>
                                        <td class="style7">
                                            <asp:Label ID="lblTotalCredit" runat="server" Font-Bold="true"></asp:Label>
                                        </td>
		                                <asp:Panel ID="pnlDebitCard" runat="server" visible="true">
		                                    <td class="style11">
                                                <b>PIN Debit Total:</b>
		                                    </td>
                                            <td style="text-align: left;">
                                                <asp:Label ID="lblTotalDebit" runat="server" Font-Bold="true"></asp:Label>
                                            </td>
		                                </asp:Panel>
		                            </tr>  
		                           
		                        </table>		
		                    </div>
                            <table style="width:90%; margin-left:20px;">                
                                <tr> 
                                    <td class="style11">Credit Card Number</td>
                                        <td class="style7">      
                                        <asp:TextBox ID="tbCreditCardNumber" runat="server" MaxLength="20" Width="200px" CssClass="inputText" autocomplete="off"></asp:TextBox>
                                        <asp:filteredtextboxextender ID="tbCreditCardNumber_FilteredTextBoxExtender" runat="server" TargetControlID="tbCreditCardNumber" FilterType="Custom, Numbers" ValidChars="*"/>      
                                        <des:requiredtextvalidator 
                                            ID="reqCreditCardNumber" 
                                            runat="server" 
                                            ControlIDToEvaluate="tbCreditCardNumber" 
                                            SummaryErrorMessage="Credit card number is required" 
                                            SetFocusOnError="True" 
                                            Display="Dynamic"   
                                            ErrorMessage="*"/>
                                         <des:RegexValidator 
                                                ID="ccValidator"  
                                                ControlIDToEvaluate="tbCreditCardNumber" 
                                                runat="server" 
                                                Expression="^[\s\S]{13,18}$" 
                                                Text="*"
                                                SummaryErrorMessage="Credit card number is invalid"
                                                ErrorMessage="*"
                                                 />                           
                                         
                                    </td>  
                                    <td class="style11"><asp:Label id="lblCVVCode" runat="server" Text="CVV Code"></asp:Label></td> 
                                    <td>                      
                                        <asp:TextBox ID="tbSecurityCode" runat="server" MaxLength="4" Width="54px" CssClass="inputText" autocomplete="off"></asp:TextBox>
                                            <asp:FilteredTextBoxExtender 
                                                ID="tbSecurityCode_FilteredTextBoxExtender1" 
                                                runat="server" TargetControlID="tbSecurityCode" FilterType="Numbers">
                            
                                        </asp:FilteredTextBoxExtender>
                                        <des:RegexValidator ID="regCVV" runat="server" 
                                                ControlIDToEvaluate="tbSecurityCode" Display="Dynamic"  ErrorMessage="*"
                                            SummaryErrorMessage="Not a valid security code" SetFocusOnError="True"  Expression="^[a-zA-Z0-9]{3,4}$"></des:RegexValidator>
                                                        <des:RequiredTextValidator ID="reqCVV" 
                                            runat="server" ControlIDToEvaluate="tbSecurityCode" SummaryErrorMessage="Security code is required" 
                                            SetFocusOnError="True" Display="Dynamic"  ErrorMessage="*"></des:RequiredTextValidator>
                                    </td>      
                   </tr>
                   <tr>      
                       <td class="style11">Expiration Date</td>                 
                       <td class="style7">
                            <asp:DropDownList ID="ddlMonth" runat="server" CausesValidation="false" Width="50px"></asp:DropDownList>&nbsp;                            
                            <asp:DropDownList ID="ddlYear" runat="server" CausesValidation="false" AutoPostBack="false" Width="60px"></asp:DropDownList>&nbsp;                                        
           				 
					  </td>
                        <td class="style11">Country</td>
                       <td>
                            <asp:DropDownList ID="ddlCountry" runat="server" ToolTip="Select Country" Width="210px" CausesValidation="false"
                                OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged" AutoPostBack="true" >
                            </asp:DropDownList>                                         
                        </td>
                    </tr>                    
                  
                 </table>
          
      </asp:Panel>
            
                            <asp:Panel ID="pnlACH" runat="server" Visible="false" DefaultButton="btnSubmit">
                                <div style="padding-left:20px; padding-top:5px; padding-bottom:5px;">
		                        <table style="width: 90%; background-color:#cdd4db;">
		                           	    
		                            <tr>	       
		                                <td class="style11">
                                            <b>ACH Total:</b>
		                                </td>
                                        <td style="text-align:left;">
                                            <asp:Label ID="lblACHTotal" runat="server" Font-Bold="true"></asp:Label>
                                        </td>		                              
		                            </tr>  
		                           
		                        </table>		
		                    </div>
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
                                        <td>Account Type</td>
                                        <td class="style7">
                                            <telerik:RadComboBox  ID="ddlBankAccountType" runat="server" AutoPostBack="true" CausesValidation="false"
                                            EnableViewState="true"  DataTextField="name" DataValueField="value">
                                            </telerik:RadComboBox>
                                            <asp:XmlDataSource ID="XmlDataSource1" runat="server" DataFile="~/resource/bankAccountType.xml" />
                                           </td>
                                        <td>&nbsp;</td>
                                        <td><asp:CheckBox ID="ckNacha" runat="server" Text=" NACHA form on file" />
                                            <des:CheckStateValidator runat="server" ControlIDToEvaluate="ckNacha" ErrorMessage="*"  SummaryErrorMessage="NACHA agreement is required"></des:CheckStateValidator>
                                        </td>
                                    </tr>
                                  
                                    <tr>
                                       <td class="style11">Bank Routing No.</td>
                                        <td class="style7">
                                           
                                        <asp:TextBox ID="tbBankRoutingNumber" runat="server" MaxLength="9" Width="200px" CssClass="inputText"></asp:TextBox>
                                                            <asp:FilteredTextBoxExtender ID="tbBankRoutingNumber_FilteredTextBoxExtender2" 
                                                runat="server" TargetControlID="tbBankRoutingNumber" FilterType="Numbers" >
                                            </asp:FilteredTextBoxExtender>
                                                             <des:CustomValidator ID="cvRoutingNumber" ErrorMessage="*" SummaryErrorMessage="Invalid routing number" runat="server" ControlIDToEvaluate="tbBankRoutingNumber" 
                                                        EventsThatValidate="All" InAJAXUpdate="true" OverrideClientSideEvaluation="CallbackOrHide"  ReportErrorsAfter="EachEdit" OnServerCondition="RoutingNumberValidation">
                                                  <ErrorFormatterContainer>
                                                                        <des:TextErrorFormatter Display="Dynamic" />
                                                                    </ErrorFormatterContainer>
                                                    </des:CustomValidator> 
                                                            <des:RequiredTextValidator ID="RequiredFieldValidator5" ErrorMessage="*"
                                                runat="server"  ControlIDToEvaluate="tbBankRoutingNumber"  SummaryErrorMessage="Routing number is required" 
                                                SetFocusOnError="True"  Display="Dynamic" Text="*"></des:RequiredTextValidator>
                            
                            
                                               </td>
                                        <td class="style12">Bank Account No.</td>
                                        <td class="style13">
                                            <asp:TextBox ID="tbBankAccountNumber" runat="server" MaxLength="17" Width="200px" CssClass="inputText" autocomplete="off"></asp:TextBox>
                                                            <asp:FilteredTextBoxExtender ID="tbBankAccountNumber_FilteredTextBoxExtender1" 
                                                runat="server" TargetControlID="tbBankAccountNumber" FilterType="Numbers">
                                            </asp:FilteredTextBoxExtender>
                                                             <des:RequiredTextValidator ID="RequiredFieldValidator4" ErrorMessage="*"
                                                runat="server"  ControlIDToEvaluate="tbBankAccountNumber"  SummaryErrorMessage="Bank account number is required" 
                                                SetFocusOnError="True" Display="Dynamic" Text="*"></des:RequiredTextValidator>
                            
                                               </td>
                                    </tr>   
                                </table>                  
                            </asp:Panel>

                         <table style="width:90%; margin-left:20px;">      
                              <tr>
                                        <td class="style11">First Name</td>
                                        <td class="style7">
                                            <asp:TextBox ID="tbFirstName" runat="server" MaxLength="20" Width="200px" CssClass="inputText"></asp:TextBox>
                                                            <asp:FilteredTextBoxExtender ID="tbBankAccountFirstName_FilteredTextBoxExtender3" 
                                                runat="server" TargetControlID="tbFirstName" FilterType="Custom, LowercaseLetters, UppercaseLetters, Numbers" 
                                                 ValidChars=" ,'&">
                                            </asp:FilteredTextBoxExtender>
                      
                                                            <des:RequiredTextValidator ID="reqFirstName" ErrorMessage="*"
                                                runat="server"  ControlIDToEvaluate="tbFirstName"  SummaryErrorMessage="First name is required" 
                                                SetFocusOnError="True" Display="Dynamic"  Text="*"></des:RequiredTextValidator>
                                        </td>
                        
                                       <td class="style12">Last Name</td>
                                       <td>
                                            <asp:TextBox ID="tbLastName" runat="server" MaxLength="30" Width="200px" CssClass="inputText"></asp:TextBox>
                                                            <asp:FilteredTextBoxExtender ID="tbBankAccountLastName_FilteredTextBoxExtender3" 
                                                runat="server" TargetControlID="tbLastName" FilterType="Custom, LowercaseLetters, UppercaseLetters" 
                                                 ValidChars=" ,'&">
                                            </asp:FilteredTextBoxExtender>
                      
                                                            <des:RequiredTextValidator ID="RequiredFieldValidator7" ErrorMessage="*"
                                                runat="server"  ControlIDToEvaluate="tbLastName"  SummaryErrorMessage="Last name is required" 
                                                SetFocusOnError="True"  Display="Dynamic" Text="*"></des:RequiredTextValidator>
                                        </td>
                                    </tr>   
                             
                                        <tr>
                                             <td class="style11"><asp:Label id="lblBillingZip" runat="server" Text="Zip"></asp:Label></td>
                                            <td class="style7">
                                                <asp:TextBox ID="txtZip" runat="server" Width="70px" MaxLength="5" CssClass="inputText"></asp:TextBox>
                                                <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender9" 
                                                    runat="server" Enabled="True" TargetControlID="txtZip" FilterType="Numbers">
                                                </asp:FilteredTextBoxExtender>
                                                <des:RegexValidator ID="regZip" runat="server" 
                                                 ControlIDToEvaluate="txtZip" Display="Dynamic"  ErrorMessage="*"
                                                 SummaryErrorMessage="Not a valid zip code" SetFocusOnError="True"  Expression="^[a-zA-Z0-9]{5,9}$"></des:RegexValidator>
                                                <des:RequiredTextValidator ID="reqZip" runat="server" SummaryErrorMessage="Zip is required" ControlIDToEvaluate="txtZip"
                                                etFocusOnError="True" ErrorMessage="*"></des:RequiredTextValidator>
                                            </td>
                                            <td class="style11">Phone Number</td>
                                             <td>
                                                <asp:TextBox ID="txtPhone" runat="server" MaxLength="14" Width="140px" CssClass="inputText"></asp:TextBox>
                                                     <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" 
                                                            runat="server" TargetControlID="txtPhone" FilterType="Numbers">
                                                        </asp:FilteredTextBoxExtender>                          
                                                    
                                                        <des:RegexValidator ID="regHomePhone" runat="server" ErrorMessage="*"
                                                        ControlIDToEvaluate="txtPhone" Display="Dynamic"  SummaryErrorMessage="Invalid Phone Number" 
                                                        SetFocusOnError="True"  Expression="(([2-9]{1})([0-9]{2})([0-9]{3})([0-9]{4}))$"></des:RegexValidator>
                             
                                            </td>
                                        </tr>
                                  
                            <tr>
                   
                                <td class="style11" style="vertical-align:top;">Email Address</td>
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
                                   
                
                        </table>
                        <span style="padding-left:20px;">
                            <des:Button ID="btnSubmit" Width="130px" runat="server" ToolTip="Submit Payment" text=" Submit Payment > " OnClick="btnSubmit_Click" Enabled="true" DisableOnSubmit="true" /></span>
                        <br /><br />

                    </fieldset>
                    <br />
                    <asp:HiddenField ID="hidAssembly" runat="server" />
                    </asp:Panel>
                </ContentTemplate>
                <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="RadioButtonList1"  EventName="SelectedIndexChanged" />
                        <asp:AsyncPostBackTrigger ControlID="ddlAUClient"  EventName="SelectedIndexChanged" />
                        <asp:AsyncPostBackTrigger ControlID="btnSubmit"  EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="btnLookupOK"  EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="btnLookupCancel"  EventName="Click" />
                       
                </Triggers> 
            </asp:UpdatePanel> 
 
 <asp:UpdatePanel ID="UpdatePanel5" runat="server" UpdateMode="Conditional">
    <Triggers>
            
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
                               
                                <asp:AsyncPostBackTrigger ControlID="btnSubmit"  EventName="Click" />
                                <asp:AsyncPostBackTrigger ControlID="btnCart1" EventName="Click" />
                                <asp:AsyncPostBackTrigger ControlID="btnCart2" EventName="Click" />
                                <asp:AsyncPostBackTrigger ControlID="btnCart3" EventName="Click" />
                                <asp:AsyncPostBackTrigger ControlID="btnCart4" EventName="Click" />
                                <asp:AsyncPostBackTrigger ControlID="btnCart5" EventName="Click" />
                            </Triggers>
                        </asp:UpdatePanel>
                        
                        <div style="text-align: right; width: 100%; margin-top: 5px;">
                            <asp:Button ID="btnBlockOverride" runat="server" Text="Yes, Override Block"  CausesValidation="false" />
                            <asp:Button ID="btnBlockCancel" runat="server" Text="No, Cancel Transaction"  CausesValidation="false" />
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
                function DetectKeyedCC(e) {
                    var evtobj = window.event ? event : e //distinguish between IE's explicit event object (window.event) and Firefox's implicit.
                    var unicode = evtobj.charCode ? evtobj.charCode : evtobj.keyCode
                    var actualkey = String.fromCharCode(unicode - 48)

                    if ((unicode >= 48 && unicode <= 57) || (unicode >= 96 && unicode <= 105)) { //(isNumber(actualkey)) {
                        //keyed entry so clear swiped flag
                        if (document.getElementById("<%=txtSwiped.ClientID %>").value.length < 20) {
                            document.getElementById("<%=txtSwiped.ClientID %>").value = "";
                            document.getElementById("<%= txtPINEPB.ClientID%>").value = "";
                            document.getElementById("<%= txtCreditDebit.ClientID %>").value = "credit";
                        }
                    }
                }
            </script>

            <script type="text/javascript" language="javascript" id="5">
                function ClearFormFields() {

                    // document.getElementById("<%= txtZip.ClientID%>").value = "";
                    document.getElementById("<%= tbCreditCardNumber.ClientID%>").value = "";
                    var SecurityCode = document.getElementById("<%= tbSecurityCode.ClientID%>")

                    if (SecurityCode != null && SecurityCode.value == '') {
                        document.getElementById("<%= tbSecurityCode.ClientID%>").value = "";
                    }

                    document.getElementById("<%= tbFirstName.ClientID%>").value = "";
                    document.getElementById("<%= tbLastName.ClientID%>").value = "";
                    document.getElementById("<%= txtPINEPB.ClientID%>").value = "";
                    document.getElementById("<%= txtPINKSN.ClientID%>").value = "";
                    document.getElementById("<%= txtTrack1.ClientID%>").value = "";
                    document.getElementById("<%= txtTrack2Data.ClientID%>").value = "";
                    document.getElementById("<%= txtTrackKSN.ClientID%>").value = "";
                    document.getElementById("<%= txtSwiped.ClientID%>").value = "";
                    document.getElementById("<%= txtPrintData.ClientID%>").value = "";
                    document.getElementById("<%= txtPrintStatus.ClientID%>").value = "";
                    document.getElementById("<%= ddlMonth.ClientID %>").selectedIndex = 0;
                    document.getElementById("<%= ddlYear.ClientID %>").selectedIndex = 0;
                    document.getElementById("<%= ddlCountry.ClientID %>").selectedIndex = 0;
                    document.getElementById("<%= txtSwipeStatus.ClientID%>").value = "Click button to scan card or enter manually.";
                }

                function ProcessCard() {
                    document.getElementById("<%=lblStatus.ClientID %>").innerHTML = "Opening Reader";
                    //Clear all fields including hidden values
                    ClearFormFields();
                    if (OpenReader() == 0) {
                        document.getElementById("<%= txtSwipeStatus.ClientID%>").value = "Unable to open reader"
                        return 0;
                    }

                    document.getElementById("<%=lblStatus.ClientID %>").innerHTML = "Swipe Card";

                    if (RequestCard() == 0) {
                        CloseReader();
                        return 0;
                    }

                    var AllowCredit = document.getElementById("<%=txtAllowCredit.ClientID %>").value
                    var AllowDebit = document.getElementById("<%=txtAllowDebit.ClientID %>").value

                    if (AllowCredit == "T" && AllowDebit == "T") {
                        document.getElementById("<%=lblStatus.ClientID %>").innerHTML = "Please select Debit or Credit";

                        var ReturnValue = SelectCreditDebit();

                        if (ReturnValue == 0)  //Error or user cancelled
                        {
                            ClearFormFields();
                            document.getElementById("<%= txtSwipeStatus.ClientID%>").value = "Cancelled by customer"
                            CloseReader();
                            return 0;
                        }

                        if (ReturnValue == 1)  //Credit card selected, skip PIN
                        {
                            document.getElementById("<%= txtSwipeStatus.ClientID%>").value = "Credit Card swipe successful"
                            CloseReader();
                            return 1;
                        }
                        document.getElementById("<%=lblStatus.ClientID %>").innerHTML = "Please enter your PIN";

                        if (RequestPIN() == 0) {
                            ClearFormFields();
                            document.getElementById("<%= txtSwipeStatus.ClientID%>").value = "Customer cancelled PIN entry"
                            CloseReader();
                            return 0;
                        }

                        document.getElementById("<%= txtSwipeStatus.ClientID%>").value = "PIN Debit swipe successful"
                        CloseReader();
                        return 1;
                    }


                    if (AllowCredit == "T" && AllowDebit == "F") {
                        document.getElementById("<%= txtSwipeStatus.ClientID%>").value = "Customer swiped credit card"
                        CloseReader();
                        return 1; // document.getElementById("<%=lblStatus.ClientID %>").innerHTML = "Please select Debit or Credit";

                   }

                   if (AllowCredit == "F" && AllowDebit == "T") {

                       document.getElementById("<%=lblStatus.ClientID %>").innerHTML = "Please enter your PIN";

                       if (RequestPIN() == 0) {
                           ClearFormFields();
                           document.getElementById("<%= txtSwipeStatus.ClientID%>").value = "Customer cancelled PIN entry"
                            CloseReader();
                            return 0;
                        }

                        CloseReader();
                        return 1;
                    }

                }

                function OpenReader() {
                    var Result = document.JMTIPADLIB.MTIPADOpen();
                    if (Result != 1) {
                        document.getElementById("<%=txtSwipeStatus.ClientID %>").innerHTML = "Error opening card reader";
                        document.getElementById("<%=lblStatus.ClientID %>").innerHTML = "Error opening card reader";
                        return 0;
                    }
                    return 1;
                }


                function CloseReader() {
                    document.JMTIPADLIB.MTIPADEndSession(0);
                    var iResult = document.JMTIPADLIB.MTIPADClose();

                }

                function RequestCard() {

                    var CardInfo;
                    var opStatus = new Array(1);
                    var temp;
                    var Loop
                    var BeepCode = 1;   //0 = Turns off Beep
                    var MessageCode = 2;  //3 = Please Swipe Again

                    for (Loop = 0; Loop < 3; Loop++) {
                        var CardInfo = document.JMTIPADLIB.MTIPADRequestCard(30, MessageCode, BeepCode, opStatus, CardInfo);
                        var iopStatus = document.JMTIPADLIB.getOpStatusCode();
                        var Message
                        if (iopStatus != 0) {
                            switch (iopStatus) {
                                case 1: Message = "Cancelled by Customer"; break;
                                case 2: Message = "Operation timed out"; break;
                                default: Message = "Error requesting card: " + iopStatus; break;
                            }
                            document.getElementById("<%=txtSwipeStatus.ClientID %>").value = Message;
                            document.getElementById("<%=lblStatus.ClientID %>").innerHTML = Message;
                            CloseReader;
                            return 0;
                        }

                        if (CardInfo.EncTrack2 == null) {
                            document.getElementById("<%=lblStatus.ClientID %>").innerHTML = "Unable to read card, Please try again";
                            BeepCode = 2;
                            MessageCode = 3;
                        }
                        else {
                            break; //iopStatus = 0 and EncTrack2 has data
                        }

                    }

                    if (CardInfo.EncTrack2 == null) {
                        document.getElementById("<%=txtSwipeStatus.ClientID %>").value = "Unable to read card after 3 attempts";
                        document.getElementById("<%=lblStatus.ClientID %>").innerHTML = "Unable to read card after 3 attempts";
                        CloseReader;
                        return 0;
                    }

                    document.getElementById("<%= txtTrack2Data.ClientID %>").value = CardInfo.EncTrack2;
                    document.getElementById("<%= txtTrack1.ClientID %>").value = CardInfo.Track1;
                    document.getElementById("<%= txtTrackKSN.ClientID %>").value = CardInfo.KSN;
                    document.getElementById("<%= txtPrintData.ClientID %>").value = CardInfo.EncMP;
                    document.getElementById("<%= txtPrintStatus.ClientID %>").value = CardInfo.MPSTS;
                    ParseTrack1(CardInfo.Track1);
                    return 1;

                }

                function SelectCreditDebit() {

                    var opStatus = new Array(1);
                    var fnKey = new Array(1);
                    var Message;

                    document.JMTIPADLIB.MTIPADSelectCreditDebit(30, 0, fnKey, opStatus);
                    var iopStatus = document.JMTIPADLIB.getOpStatusCode();
                    var iFnKey = document.JMTIPADLIB.getFnKeyPressed();

                    document.getElementById("<%=lblStatus.ClientID %>").innerHTML = "fnKey = " + iFnKey + " opStatus = " + iopStatus;

                    if (iopStatus != 0) {
                        switch (iopStatus) {
                            case 1: Message = "Cancelled by customer"; break;
                            case 2: Message = "Operation Timed Out"; break;
                            default: Message = "Problem selecting debit/credit"; break;
                        }
                        document.getElementById("<%=lblStatus.ClientID %>").innerHTML = Message;
                        CloseReader;
                        return 0;
                    }

                    var ReturnValue;
                    document.getElementById("<%= txtCreditDebit.ClientID %>").value = "";
                    switch (iFnKey) {
                        case 113:
                            document.getElementById("<%= txtCreditDebit.ClientID %>").value = "credit";
                            Message = "Credit";
                            ReturnValue = 1;
                            break;
                        case 116:
                            document.getElementById("<%= txtCreditDebit.ClientID %>").value = "debit";
                            Message = "Debit";
                            ReturnValue = 2;
                            break;
                    }
                    document.getElementById("<%=lblStatus.ClientID %>").innerHTML = Message;
                    return ReturnValue;
                }

                function RequestPIN() {
                    var pinInfo;
                    var opStatus = new Array(1);
                    var temp;
                    var pinInfo = document.JMTIPADLIB.MTIPADRequestPIN(30, 0, 4, 6, 0, 0, opStatus, pinInfo);

                    var iopStatus = document.JMTIPADLIB.getOpStatusCode();
                    if (iopStatus != 0) {
                        switch (iopStatus) {
                            case 1: Message = "Cancelled by customer"; break;
                            case 2: Message = "Operation Timed Out"; break;
                            default: Message = "Problem requesting PIN " + iopStatus; break;
                        }
                        //document.getElementById("<%= txtCreditDebit.ClientID %>").appendChild = Message;
                        CloseReader;
                        return 0;
                    }

                    document.getElementById("<%= txtPINEPB.ClientID %>").value = pinInfo.EPB;
                    document.getElementById("<%= txtPINKSN.ClientID %>").value = pinInfo.KSN;
                    return 1;
                }

                function ParseTrack1(trackData) {

                    var Fields = trackData.split("^");
                    var Name = Fields[1].split("/");
                    document.getElementById("<%=tbCreditCardNumber.ClientID %>").value = Fields[0].substring(2, 8) + "******" + Fields[0].substring(14)
                    document.getElementById("<%=txtSwiped.ClientID %>").value = "swiped"; //Set value since card was swiped

                    document.getElementById("<%=ddlMonth.ClientID %>").value = FormatMonth(Fields[2].substring(2, 4));
                    document.getElementById("<%=ddlYear.ClientID %>").value = FormatYear(Fields[2].substring(0, 2));

                    if (Name.length > 1) {
                        document.getElementById("<%=tbFirstName.ClientID %>").value = Name[0];
                        document.getElementById("<%=tbLastName.ClientID %>").value = Name[1];
                    }
                    else {
                        document.getElementById("<%=tbLastName.ClientID %>").value = Fields[1];
                    }
                }

                function FormatMonth(tMonth) {
                    if (tMonth.length == 2) {
                        return tMonth;
                    }
                    else {
                        if (tMonth.length == 1) {
                            return "0" + tMonth;
                        }
                        else
                            return "";
                    }
                }

                function FormatYear(tYear) {
                    if (tYear.length == 4) {
                        return tYear;
                    }
                    else {
                        if (tYear.length == 2) {
                            return "20" + tYear;
                        }
                        else
                            return "";
                    }
                }

            </script>
        </asp:PlaceHolder> 
 
   
  </div>
        

        <div id="dvObjectHolder">  </div>
        <br /><br />
          <script lang="JavaScript" type="text/javascript">
              if (window.navigator.appName.toLowerCase().indexOf("netscape") != -1) { // set object for Netscape:
                  document.getElementById('dvObjectHolder').innerHTML = "        <applet type=\"application/x-java-applet;version=1.6\"" +
	       	        "codebase =\".\"" +
			        "archive = \"/magtek/JMTIPADLIB.jar\"" +
			        "code=\"JMTIPADLIB.class\"" +
			        "name=\"JMTIPADLIB\"" +
	   		        "scriptable=\"true\"" +
	  		        "style=\"visibility:hidden;\"" +
			        "mayscript=\"mayscript\"" +
			        "pluginspage=\"http://java.com/en/download/index.jsp\"" + ">" +
	        	        "<param name=\"cache_option\" value=\"No\">" +
			        "<param name=\"classloader_cache\" value=\"true\">" +
			        "<param name=\"dll_ver\" value=\"1.0.0\">" +
			        "<param name=\"dll_auto_update\" value=\"Yes\">" +
	                "</applet>";
              }
              else if (window.navigator.appName.toLowerCase().indexOf('internet explorer') != -1) { //set object for IE
                  document.getElementById('dvObjectHolder').innerHTML = "<object type=\"application/x-java-applet;version=1.6\"" +
	       	        "codebase =\".\"" +
			        "archive = \"/Magtek/JMTIPADLIB.jar\"" +
			        "code=\"JMTIPADLIB.class\"" +
			        "name=\"JMTIPADLIB\"" +
	        	        "height=\"0\" width=\"0\" >" +
	        	        "<param name=\"mayscript\" value=\"true\">" +
	    		        "<param name=\"classloader_cache\" value=\"true\">" +
		    	        "<param name=\"cache_option\" value=\"No\">" +
		    	        "<param name=\"dll_ver\" value=\"1.0.0\">" +
		    	        "<param name=\"dll_auto_update\" value=\"Yes\">" +
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
 
 