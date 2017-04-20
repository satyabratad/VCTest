<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="PaymentCartGrid.ascx.vb" Inherits=".PaymentCartGrid" %>
<%@ Import Namespace="B2P.PaymentLanding.Express" %>
<%@ Import Namespace="B2P.PaymentLanding.Express.Web" %>
<div id="cartGrid" style="display: block;">
    <!--Non-Lookup----------------------------------------------------------------------------------------->

    <asp:Repeater ID="rptNonLookup" runat="server">
        <HeaderTemplate>
            <table class="table" style="width: 100%;" id="tblNonLookup">
                <thead>
                    <tr>
                        <td class="table-header" width="5%"></td>
                        <td class="table-header" width="30%">Item</td>
                        <td class="table-header" width="55%">Details</td>
                        <td class="table-header" width="10%" align="right">Amount</td>
                    </tr>
                </thead>
        </HeaderTemplate>
        <ItemTemplate>
            <tbody>
                <tr id="trIndex" <%# Eval("Index") %>>
                    <td class="table-row" style="align-content: center; cursor: pointer;"><a onclick="removeItems(<%# Eval("Index") %>);" title="Delete Item"><i class="fa fa-trash-o fa-lg" aria-hidden="true"></i></a></td>
                    <td class="table-row"><%# Eval("Item") %></td>
                    <td class="table-row"><%# GetAccountInformation(Eval("Index")) %><br>
                        <strong>Property Address:</strong><br>
                        <%# GetPropertyAddress(Eval("Index")) %>
                    </td>
                    <td class="table-row" align="right"><%# FormatAmount(Eval("Amount")) %></td>
                </tr>
        </ItemTemplate>
        <FooterTemplate>
            <tr>
                <td class="table-row-bold" colspan="3" align="right">Subtotal (<%# GetCartItemCount() %> item(s)): </td>
                <td class="table-row-bold" align="right"><%# SubTotal() %></td>
            </tr>
            </tbody>
    </table>
        </FooterTemplate>
    </asp:Repeater>

    <!--End Non-Lookup------------------------------------------------------------------------------------->
    <!--Lookup----------------------------------------------------------------------------------------->

    <asp:Repeater ID="rptLookup" runat="server">
        <HeaderTemplate>
            <table class="table" style="width: 100%;" id="tblLookup">
                <thead>
                    <tr>
                        <td class="table-header" width="5%"></td>
                        <td class="table-header" width="5%"></td>
                        <td class="table-header" width="20%">Item</td>
                        <td class="table-header" width="50%">Details</td>
                        <td class="table-header" width="10%" align="right">Amount Due</td>
                        <td class="table-header" width="10%" align="right">Amount</td>
                    </tr>
                </thead>
        </HeaderTemplate>
        <ItemTemplate>
            <tbody>
                <tr id="trIndex" <%# Eval("Index") %>>
                    <td class="table-row" style="align-content: center; cursor: pointer;"><a onclick="removeItems(<%# Eval("Index") %>);" title="Delete Item"><i class="fa fa-trash-o fa-lg" aria-hidden="true"></i></a></td>
                    <td class="table-row" style="align-content: center; cursor: pointer;"><a onclick="editItems(<%# Eval("Index") %>);" title="Edit Item"><i class="fa fa-pencil-square-o fa-lg" aria-hidden="true"></i></a></td>
                    <td class="table-row"><%# Eval("Item") %></td>
                    <td class="table-row"><%# GetAccountInformation(Eval("Index")) %><br>
                        <strong>Property Address:</strong><br>
                        <%# GetPropertyAddress(Eval("Index")) %>
                    </td>
                    <td class="table-row" align="right"><%# FormatAmount(Eval("AmountDue")) %></td>
                    <td class="table-row" align="right"><%# FormatAmount(Eval("Amount")) %></td>
                </tr>
        </ItemTemplate>
        <FooterTemplate>
            <tr>
                <td class="table-row-bold" colspan="5" align="right">Subtotal (<%# GetCartItemCount() %> item(s)): </td>
                <td class="table-row-bold" align="right"><%# SubTotal() %></td>
            </tr>
            </tbody>
    </table>
        </FooterTemplate>
    </asp:Repeater>

    <!--End Lookup------------------------------------------------------------------------------------->
    <!--SSO----------------------------------------------------------------------------------------->

    <asp:Repeater ID="rptSSO" runat="server">
        <HeaderTemplate>
            <table class="table" style="width: 100%;" id="tblSSO">
                <thead>
                    <tr>
                        <td class="table-header" width="5%"></td>
                        <td class="table-header" width="20%">Item</td>
                        <td class="table-header" width="55%">Details</td>
                        <td class="table-header" width="10%" align="right">Amount Due</td>
                        <td class="table-header" width="10%" align="right">Amount</td>
                    </tr>
                </thead>
        </HeaderTemplate>
        <ItemTemplate>
            <tbody>
                <tr id="trIndex" <%# Eval("Index") %>>
                    <td class="table-row" style="align-content: center; cursor: pointer;"><a onclick="editItems(<%# Eval("Index") %>);" title="Edit Item"><i class="fa fa-pencil-square-o fa-lg" aria-hidden="true"></i></a></td>
                    <td class="table-row"><%# Eval("Item") %></td>
                    <td class="table-row"><%# GetAccountInformation(Eval("Index")) %><br>
                        <strong>Property Address:</strong><br>
                        <%# GetPropertyAddress(Eval("Index")) %>
                    </td>
                    <td class="table-row" align="right"><%# FormatAmount(Eval("AmountDue")) %></td>
                    <td class="table-row" align="right"><%# FormatAmount(Eval("Amount")) %></td>
                </tr>
        </ItemTemplate>
        <FooterTemplate>
            <tr>
                <td class="table-row-bold" colspan="4" align="right">Subtotal (<%# GetCartItemCount() %> item(s)): </td>
                <td class="table-row-bold" align="right"><%# SubTotal() %></td>
            </tr>
            </tbody>
    </table>
        </FooterTemplate>
    </asp:Repeater>

    <!--End SSO------------------------------------------------------------------------------------->

</div>
<!-- START DELETE CONFIRM MODAL DIALOG -->
<div id="myModal" class="modal fade" role="dialog">
    <div class="modal-dialog">
        <!-- Modal content-->
        <div class="modal-content">
            <div class="modal-header">
                <button type="button" class="close" data-dismiss="modal">
                    &times;</button>
                <h4 class="modal-title">Confirm Item delete.</h4>
            </div>
            <div class="modal-body">
                <div id="div1" role="alert" style="margin-top: 10px;" class="alert alert-success">
                    <div class="status-msg-text">
                        <span class="control-label">Are you sure you want to delete this item?</span>
                    </div>
                </div>
            </div>
            <div class="modal-footer">
                <button type="button" id="btnYes" name="btnYes" onclick="removeItemsFromCart();"
                    class="btn btn-primary" title="Yes" class="btn btn-default" data-dismiss="modal">
                    Yes</button>
                <button type="button" id="btnNo" name="btnNo" class="btn btn-primary" title="No"
                    class="btn btn-default" data-dismiss="modal">
                    No</button>
            </div>
        </div>
    </div>
</div>
<!-- END DELETE CONFIRM MODAL DIALOG -->
<input id="hdSelectedIndex" runat="server" type="hidden" value="" />
<input id="hdMode" type="hidden" value="" runat="server" />
<input id="hdEditAmount" type="hidden" value="" runat="server" />
<asp:Button ID="btnEditItem" runat="server" Style="display: none; visibility: hidden" Text="Edit" />
<script>
    function removeItems(Index) {
        $('#hdSelectedIndex').val(Index);
        $('#myModal').modal();
    }
    function removeItemsFromCart() {
        $('#hdMode').val("DELETE");
        $("#frmDefault").submit();
    }
    function editItems(Index) {
        $('#hdSelectedIndex').val(Index);
        $('#<%=btnEditItem.ClientID%>').click()
    }

    function updateCartCount(count) {
        $("#cartCount").html(count);
    }
</script>
