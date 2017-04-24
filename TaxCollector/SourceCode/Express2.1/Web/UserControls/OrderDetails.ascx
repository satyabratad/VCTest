<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="OrderDetails.ascx.vb" Inherits=".OrderDetails" %>
<%@ Import Namespace="B2P.PaymentLanding.Express" %>
<%@ Import Namespace="B2P.PaymentLanding.Express.Web" %>
<%@ Register TagPrefix="uc1" TagName="SubTotal" Src="~/UserControls/SubTotal.ascx" %>


<div>
    <p>
        <strong>
            My Order Details
        </strong>
    </p>
    <hr />
    <div class="table-responsive">
        <table class="table table-condensed table-no-border" id="tblOrder">
            <asp:Repeater ID="rptOrder" runat="server">
                <ItemTemplate>
                    <tbody>
                        <tr id="trIndex" class="bg-primarydark">
                            <td class="col-sm-6 text-uppercase"><label><%# Eval("Item") %></label></td>
                            <td style="text-align:right"><label><%# FormatAmount(Eval("Amount")) %></label></td>
                        </tr>
                        <asp:Repeater ID="rptOrderAccount" runat="server">
                            <ItemTemplate>
                                <tr id="trAccindex">
                                    <td class="col-sm-6 text-uppercase"><%# Eval("Label") %></td>
                                    <td style="text-align: right"><%# (Eval("Value")) %></td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tbody>
                </ItemTemplate>
            </asp:Repeater>
        </table>
    </div>
    <uc1:SubTotal runat="server" ID="SubTotal" />
</div>
