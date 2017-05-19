<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="editoffice.aspx.vb" Inherits="Bill2PayAdmin45.editoffice" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
  <head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Edit Office :: Bill2Pay Administration</title>
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
    <div class="header"><a href="/security/modifyoffice.aspx" title="Modify Users">Modify 
        Office</a> 
        &gt; Edit Office</div>
    <div class="header_border"></div>
   
    
    
    <asp:ToolkitScriptManager ID="ScriptManager1" runat="server" />

  
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
      <ContentTemplate>
      
    
    
    <!--StartContent-->
    <div class="main_content">
    
   
    
    
     <fieldset>  
        <legend>Edit Office Information</legend>
    
        
    <asp:Panel ID="pnlResults" runat="server" Visible="false" CssClass="padding">
         <br /><asp:Label ID="lblAUStatus" runat="server"></asp:Label><br /><br />
          <asp:LinkButton ID="lnkViewOffices" runat="server" Text="Return to office list"></asp:LinkButton>
            <br /><br />
    </asp:Panel>
    
    <asp:Panel ID="pnlAddOffice" runat="server">
   
    <asp:ValidationSummary id="ValidationSummary1" runat="server"  HeaderText="There's an error in the information you entered. Please correct the following errors and continue."
        HyperLinkToFields="True" ScrollIntoView="Top" CssClass="ErrorSummary" DisplayMode="bulletList">
	</asp:ValidationSummary>  <br />        
              
        
              
           
            <table  style="margin-left:20px;" cellspacing="4" width="75%" >
              <tr>
                <td>
                    Office Name:
                </td>
                <td>
                  <asp:TextBox ID="txtAOName" runat="server" MaxLength="30" TabIndex="1" />
                   <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" 
                            runat="server" Enabled="True" TargetControlID="txtAOName" FilterType="Custom, LowercaseLetters, UppercaseLetters, Numbers" ValidChars=" ,.&-'">
                        </asp:FilteredTextBoxExtender>
                  <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtAOName" runat="server" Text="*" ErrorMessage="Required - Office Name"></asp:RequiredFieldValidator>
                </td>
                <td>Status:</td>
                <td valign="top"><telerik:RadComboBox ID="ddlStatus" Runat="server" Skin="Windows7"  
                      EnableVirtualScrolling="true" tabindex="2" DataTextField="name" DataValueField="value" >
                         
                     </telerik:RadComboBox>
                     <asp:XmlDataSource runat="server" ID="XmlDataSource2" DataFile="~/resource/status.xml" />
                     &nbsp; 
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server"  ValidationGroup="Info"
                        ControlToValidate="ddlStatus" ErrorMessage="Required - Security status" InitialValue="Select One" Text="*">
                    </asp:RequiredFieldValidator> 
                </td>
                
              </tr>
              <tr>
                <td>
                    Address:
                </td>
                <td>
                  <asp:TextBox ID="txtAOAddress" runat="server" MaxLength="30" TabIndex="2" />
                   <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" 
                            runat="server" Enabled="True" TargetControlID="txtAOAddress" FilterType="Custom, LowercaseLetters, UppercaseLetters, Numbers" ValidChars=" ,.&-'">
                        </asp:FilteredTextBoxExtender>
                  <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="txtAOAddress" runat="server" Text="*" ErrorMessage="Required - Address"></asp:RequiredFieldValidator>
                  
                </td>
                 <td>
                     State:
                </td>
                <td>
                  <telerik:RadComboBox ID="ddlState" runat="server" Skin="Windows7"  DataTextField="name" DataValueField="abbreviation"
                    TabIndex="5"  />&nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator6" ControlToValidate="ddlState" runat="server" Text="*" ErrorMessage="Required - State"></asp:RequiredFieldValidator>
                                    
                  <asp:XmlDataSource runat="server" ID="XmlDataSource1" DataFile="~/resource/usStates.xml" />
                  
                </td>
              </tr>
              <tr>
                <td>Address 2:</td>
                <td><asp:TextBox ID="txtAOAddress2" runat="server" MaxLength="30" TabIndex="3" />
                 <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" 
                            runat="server" Enabled="True" TargetControlID="txtAOAddress2" FilterType="Custom, LowercaseLetters, UppercaseLetters, Numbers" ValidChars=" ,.&-'">
                        </asp:FilteredTextBoxExtender></td>
              <td>
                    Zip Code:
                </td>
                <td>
                  <asp:TextBox ID="txtAOZipCode" runat="server" MaxLength="5" TabIndex="6" />
                   <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" 
                            runat="server" Enabled="True" TargetControlID="txtAOZipCode" FilterType="Numbers">
                        </asp:FilteredTextBoxExtender>
                  <asp:RequiredFieldValidator ID="RequiredFieldValidator7" ControlToValidate="txtAOZipCode" runat="server" Text="*" ErrorMessage="Required - Zip Code"></asp:RequiredFieldValidator>
                  
                </td>
              </tr>
              <tr>
                <td>
                    City:
                </td>
                <td>
                  <asp:TextBox ID="txtAOCity" runat="server" MaxLength="30" TabIndex="4" />
                   <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender8" 
                            runat="server" Enabled="True" TargetControlID="txtAOCity" FilterType="Custom, LowercaseLetters, UppercaseLetters, Numbers" ValidChars=" ,.&-'">
                        </asp:FilteredTextBoxExtender>
                  <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ControlToValidate="txtAOCity" runat="server" Text="*" ErrorMessage="Required - City"></asp:RequiredFieldValidator>
                  
                </td>
                <td>
                    Phone:
                </td>
                <td>
                  <asp:TextBox ID="txtAOPhone" runat="server" MaxLength="10" TabIndex="7" />
                   <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender9" 
                            runat="server" Enabled="True" TargetControlID="txtAOPhone" FilterType="Numbers">
                        </asp:FilteredTextBoxExtender>
                  <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtAOPhone" runat="server" Text="*" ErrorMessage="Required - Phone Number"></asp:RequiredFieldValidator>

                </td>
              </tr>
             
              <tr><td>&nbsp;</td></tr>
            </table>
            <div style="padding-left:20px;">
              <des:ImageButton ID="btnSubmit" ImageUrl="/images/btnSaveOffice.jpg" ToolTip="Save Office" Text="Save Office" runat="server" />
              
            </div>
            
          </asp:Panel>
  <br />
  </fieldset>
  <br /><br />
  
  
 
            
            
   </div>
    <!--EndContent-->
  
  
  </ContentTemplate>
  <Triggers>
    
  </Triggers>
      </asp:UpdatePanel>  
 
  
  </div>
         <custom:timeout ID="timeoutControl" runat="server" />

</form>
      
      <div class="clear"></div> 
      <div class="footer_shadow">
        <custom:footer ID="Footer" runat="server" />
      </div>
</body> 
</html> 