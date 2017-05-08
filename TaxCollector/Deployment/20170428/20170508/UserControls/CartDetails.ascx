<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="CartDetails.ascx.vb" Inherits=".CartDetails" %>
<table class="table" style="width: 100%;" id="tblCartDetails" runat="server">
    <tbody>
        <tr>
            <td width="65%" id="tdDisplayCartMsg" class="table-row-bold" style="border-collapse: !important;" align="right">
                <asp:Label Style="color: #307ea5" ID="cartProduct" runat="server"></asp:Label>&nbsp;
                 <asp:Label ID="lblCartHeadingText1" runat="server"></asp:Label>
            </td>
            <td width="35%" class="submenu-caption" style="border-collapse: !important;" align="right">
                <asp:Label ID="lblCartText1" runat="server"></asp:Label>
                <asp:Label ID="lblCartHeadingCount" runat="server"></asp:Label>
                 <asp:Label ID="lblCartText2" runat="server"></asp:Label>               
                <asp:Label ID="lblCartHeadingAmount" runat="server"></asp:Label>
            </td>
        </tr>
    </tbody>
</table>
