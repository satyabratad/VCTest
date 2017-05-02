<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="SubTotal.ascx.vb" Inherits=".SubTotal" %>
<asp:HiddenField ID="hdCCAmount" runat="server" Value="" />
<div >
    <table class="table table-condensed table-no-border" id="tblsubTotal">
        <tr class="bg-primarydark">
            <td class="col-sm-6 text-uppercase"><asp:label runat="server" Text="<%$ Resources:WebResources, lblCartSubtotal %>"></asp:label></td>
            <td style="text-align: right">
                <label><asp:Label ID="lblSubTotal" runat="server" Text=""></asp:Label></label>
            </td>
        </tr>
    </table>
</div>
