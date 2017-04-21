<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="SubTotal.ascx.vb" Inherits=".SubTotal" %>
<div class="table-responsive">
<table class="table" style="width: 100%;border:none" id="tblsubTotal">
    <tr >
        <td class="table-header" style="width:75%">Sub Total</td>
        <td class="table-header" align="right">
            
            <asp:Label ID="lblSubTotal" runat="server" Text=""></asp:Label>
        </td>
    </tr>
</table>
</div>