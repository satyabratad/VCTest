<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="CartDetails.ascx.vb" Inherits=".CartDetails" %>
<table class="table" style="width: 100%;" id="tblCartDetails" runat="server">
                                        <tbody><tr>
                                            <td width="65%" id="tdDisplayCartMsg" class="table-row-bold" style="border-collapse: !important;" align="right">
                                                <asp:Label style="color: #307ea5" id="cartProduct" runat="server">Tax Bill</asp:Label>&nbsp;Added to Cart
                                            </td>
                                            <td width="35%" class="submenu-caption" style="border-collapse: !important;" align="right">
                                                Cart Subtotal (<asp:Label id="cartHeadingCount"  runat="server">1</asp:Label> item(s)): <asp:Label id="cartHeadingAmount"  runat="server">$1.00</asp:Label>
                                            </td>
                                        </tr>
                                    </tbody></table>
