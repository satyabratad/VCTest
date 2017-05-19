<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="addoffice.aspx.vb" Inherits="Bill2PayAdmin45.addoffice" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
  <head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Add Office :: Bill2Pay Administration</title>
    <link rel="stylesheet" type="text/css" href="../css/main.css" media="all" />
    <link rel="stylesheet" type="text/css" href="../css/content_normal.css" title="normal" />
    <link rel="stylesheet" type="text/css" href="../Content/themes/base/minified/jquery.ui.all.min.css" />
    
</head>
<body>
 <form id="form1" runat="server" defaultbutton="btnSubmit">

  <div class="top_header_main"></div>
  <custom:menu ID="menu" runat="server" />

  <div class="top_header_shadow"></div>
  
  <div class="content">
   <custom:logout ID="logout" runat="server" />
    <div class="header">Add Office</div>
    <div class="header_border"></div>
   
    
    
    <asp:ToolkitScriptManager ID="ScriptManager1" runat="server" />

  
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
      <ContentTemplate>
      
    
    
    <!--StartContent-->
    <div class="main_content">
    <fieldset>
        <legend>Add New Office</legend>
    
    <asp:Label ID="lblAUStatus" runat="server" CssClass="padding"></asp:Label>
    <asp:Panel ID="pnlResults" runat="server" Visible="false" CssClass="padding">
        <asp:LinkButton ID="lnkAddOffice" runat="server" Text="Add another office" OnClick="lnkAddOffice_Click" OnClientClick="document.location.href=document.location.href;"></asp:LinkButton> | 
        <asp:LinkButton ID="lnkViewOffices" runat="server" Text="Modify offices"></asp:LinkButton>
    </asp:Panel>
    
    <asp:Panel ID="pnlAddOffice" runat="server">
   
    <asp:ValidationSummary id="ValidationSummary1" runat="server"  HeaderText="There's an error in the information you entered. Please correct the following errors and continue."
         HyperLinkToFields="True"  ScrollIntoView="Top" CssClass="ErrorSummary" DisplayMode="bulletList">
	</asp:ValidationSummary>  <br />        
              
        
              
           
            <table  style="margin-left:20px;" cellspacing="4" width="75%" >
              <tr>
                <td>
                    Office Name:
                </td>
                <td>
                  <asp:TextBox ID="txtAOName" runat="server" MaxLength="30" TabIndex="1" CssClass="inputText" />
                   <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" 
                            runat="server" Enabled="True" TargetControlID="txtAOName" FilterType="Custom, LowercaseLetters, UppercaseLetters, Numbers" ValidChars=" ,.&-'">
                        </asp:FilteredTextBoxExtender>
                  <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtAOName" runat="server" Text="*" ErrorMessage="Required - Office Name"></asp:RequiredFieldValidator>
                </td>
                <td>
                    Phone:
                </td>
                <td>
                  <asp:TextBox ID="txtAOPhone" runat="server" MaxLength="10" TabIndex="7" CssClass="inputText" />
                   <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" 
                            runat="server" Enabled="True" TargetControlID="txtAOPhone" FilterType="Numbers">
                        </asp:FilteredTextBoxExtender>
                  <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtAOPhone" runat="server" Text="*" ErrorMessage="Required - Phone Number"></asp:RequiredFieldValidator>

                </td>
              </tr>
              <tr>
                <td>
                    Address:
                </td>
                <td>
                  <asp:TextBox ID="txtAOAddress" runat="server" MaxLength="30" TabIndex="2" CssClass="inputText" />
                   <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" 
                            runat="server" Enabled="True" TargetControlID="txtAOAddress" FilterType="Custom, LowercaseLetters, UppercaseLetters, Numbers" ValidChars=" ,.&-'">
                        </asp:FilteredTextBoxExtender>
                  <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="txtAOAddress" runat="server" Text="*" ErrorMessage="Required - Address"></asp:RequiredFieldValidator>
                  
                </td>
                 <td>
                     Client Code:
                </td>
                <td>
                  <asp:Label ID="lblAUClient" Visible="false" runat="server"></asp:Label> 
                      <telerik:RadComboBox ID="ddlAUClient" AppendDataBoundItems="true" Runat="server" Skin="Windows7" tabindex="8" >
                      <Items>
                            <telerik:RadComboBoxItem Text="Select One" Value="" Font-Italic="true" />
                        </Items>
                      </telerik:RadComboBox>&nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator4" InitialValue="Select One" ControlToValidate="ddlAUClient" runat="server" text="*" errormessage="Required - Client"></asp:RequiredFieldValidator>
                   
                      <asp:XmlDataSource runat="server" ID="XmlDataSource3" DataFile="~/resource/testclient.xml" />
                   
                      
                </td>
              </tr>
              <tr>
                <td>Address 2:</td>
                <td><asp:TextBox ID="txtAOAddress2" runat="server" MaxLength="30" TabIndex="3" CssClass="inputText" />
                 <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" 
                            runat="server" Enabled="True" TargetControlID="txtAOAddress2" FilterType="Custom, LowercaseLetters, UppercaseLetters, Numbers" ValidChars=" ,.&-'">
                        </asp:FilteredTextBoxExtender></td>
              <td>
                    
                </td>
                <td>
                  
                </td>
              </tr>
              <tr>
                <td>
                    City:
                </td>
                <td>
                  <asp:TextBox ID="txtAOCity" runat="server" MaxLength="30" TabIndex="4" CssClass="inputText" />
                   <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" 
                            runat="server" Enabled="True" TargetControlID="txtAOCity" FilterType="Custom, LowercaseLetters, UppercaseLetters, Numbers" ValidChars=" ,.&-'">
                        </asp:FilteredTextBoxExtender>
                  <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ControlToValidate="txtAOCity" runat="server" Text="*" ErrorMessage="Required - City"></asp:RequiredFieldValidator>
                  
                </td>
                
              </tr>
              <tr style="height:25px;">
                <td>
                    State:
                </td>
                <td>
                
                  <telerik:RadComboBox ID="ddlState" AppendDataBoundItems="true" runat="server" Skin="Windows7" DataTextField="name" DataValueField="abbreviation"
                    TabIndex="5">
                    <Items>
                            <telerik:RadComboBoxItem Text="Select One" Value="" Font-Italic="true" />
                        </Items>
                        </telerik:RadComboBox>&nbsp;
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" InitialValue="Select One" ControlToValidate="ddlState" runat="server" text="*" errormessage="Required - State"></asp:RequiredFieldValidator>
                            
                  <asp:XmlDataSource runat="server" ID="XmlDataSource1" DataFile="~/resource/usStates.xml" />
                  
                </td>
              </tr>
              <tr>
                <td>
                    Zip Code:
                </td>
                <td>
                  <asp:TextBox ID="txtAOZipCode" runat="server" MaxLength="5" TabIndex="6" CssClass="inputText" />
                   <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" 
                            runat="server" Enabled="True" TargetControlID="txtAOZipCode" FilterType="Numbers">
                        </asp:FilteredTextBoxExtender>
                  <asp:RequiredFieldValidator ID="RequiredFieldValidator7" ControlToValidate="txtAOZipCode" runat="server" Text="*" ErrorMessage="Required - Zip Code"></asp:RequiredFieldValidator>
                  
                </td>
              </tr>
              <tr><td>&nbsp;</td></tr>
            </table>
            <div class="padding">
              <des:ImageButton ImageUrl="/images/btnSaveOffice.jpg" ToolTip="Save Office"  ID="btnSubmit" Text="Save Office" runat="server" />
              
            </div>
            
          </asp:Panel>
  <br />
  </fieldset>  
  
  
  
 
            
            
   </div>
    <!--EndContent-->
  
  
  </ContentTemplate>
  <Triggers>
    
  </Triggers>
      </asp:UpdatePanel>  
 
   
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