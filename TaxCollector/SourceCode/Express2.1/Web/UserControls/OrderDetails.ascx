<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="OrderDetails.ascx.vb" Inherits=".OrderDetails" %>
<%@ Import Namespace="B2P.PaymentLanding.Express" %>
<%@ Import Namespace="B2P.PaymentLanding.Express.Web" %>
<%@ Register TagPrefix="uc1" TagName="SubTotal" Src="~/UserControls/SubTotal.ascx"  %>


<div>
    <div class="table-responsive" >
        <table   style="width: 100%;border:none" id="tblOrder">
            <asp:Repeater ID="rptOrder" runat="server">

                <ItemTemplate>
                    <tbody>
                        <tr id="trIndex" >
                            <td class="table-header"  style="width: 75%"><%# Eval("Item") %></td>
                            <td class="table-header" align="right"><%# FormatAmount(Eval("Amount")) %></td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <table style="width: 100%;border:none" id="tblOrderdet">
                                    <asp:Repeater ID="rptOrderAccount" runat="server">

                                        <ItemTemplate>
                                            <tbody>
                                                <tr id="trAccindex">
                                                    <td style="width: 75%"><%# Eval("Label") %></td>
                                                    <td align="right"><%# (Eval("Value")) %></td>
                                                </tr>
                                            </tbody>
                                        </ItemTemplate>

                                    </asp:Repeater>
                                </table>
                            </td>
                        </tr>
                    </tbody>
                </ItemTemplate>


            </asp:Repeater>
        </table>
    </div>
    
    <uc1:SubTotal runat="server" ID="SubTotal" />

</div>
