<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="OrderDetails.ascx.vb" Inherits=".OrderDetails" %>
<%@ Import Namespace="B2P.PaymentLanding.Express" %>
<%@ Import Namespace="B2P.PaymentLanding.Express.Web" %>
<div>
<div>
<asp:Repeater ID="rptOrder" runat="server">
    <HeaderTemplate>
        <table class="table" style="width: 100%;" id="tblOrder">
    </HeaderTemplate>
    
    <ItemTemplate>
       <tbody>     
                <tr id="trIndex" >                    
                    <td class="table-row" style="width:90%" ><%# Eval("Item") %></td>                    
                    <td class="table-row" style="width:10%" align="right"><%# (Eval("Amount")) %></td>
                </tr>
                
           <asp:Repeater ID="rptOrderAccount" runat="server" >
               <HeaderTemplate>
                    <table class="table" style="width: 100%;" id="tblOrder">
               </HeaderTemplate>
               <ItemTemplate >
                <tbody> 
               <tr id="trAccindex" >                  
                    <td class="table-row" style="width:90%" ><%# Eval("Label") %></td>                    
                    <td class="table-row" style="width:10%" align="right"><%# (Eval("Value")) %></td>
                </tr>
             </tbody>
               </ItemTemplate>
                <FooterTemplate>
                    </table>
                </FooterTemplate>
           </asp:Repeater>
       
    </tbody>
   </ItemTemplate>
    <FooterTemplate>
        </table>
    </FooterTemplate>


 </asp:Repeater>
    </div>
 <div>
<table class="table" style="width: 100%;" id="tblsubTotal">
    <tr >
        <td class="table-header">Sub Total</td>
        <td class="table-header" style="float:right ">
            
            <asp:Label ID="lblSubTotal" runat="server" Text=""></asp:Label>
        </td>
    </tr>
</table>
</div>

</div>
