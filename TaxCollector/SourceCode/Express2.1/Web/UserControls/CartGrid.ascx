<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="CartGrid.ascx.vb" Inherits=".CartGridascx" %>
<%@ Import Namespace="B2P.PaymentLanding.Express" %>
<%@ Import Namespace="B2P.PaymentLanding.Express.Web" %>
<div id="cartGrid" style="display: block;">
    <!--Non-Lookup----------------------------------------------------------------------------------------->
    <% If clientType = B2P.Cart.EClientType.NonLookup Then %>
     <% If cartItems.Count > 0 Then %>
    <table class="table" style="width: 100%;" id="tblNonLookup">
        <thead>
            <tr>
                <td class="table-header" width="5%"></td>
                <td class="table-header" width="30%">Item</td>
                <td class="table-header" width="55%">Details</td>
                <td class="table-header" width="10%" align="right">Amount</td>
            </tr>
        </thead>
        <tbody>
          
            <% For Each cartItem As B2P.Cart.Cart In cartItems %>
            <tr id="trIndex"<%=cartItem.Index %>>
                <td class="table-row" style="align-content:center;cursor:pointer;"><a onclick="removeItems(<%=cartItem.Index.ToString() %>);"><i class="fa fa-trash-o fa-lg" aria-hidden="true"></i></a></td>
                <td class="table-row"><%=cartItem.Item %></td>
                <td class="table-row"><%=GetAccountInformation(cartItem) %><br>
                    <strong>Property Address:</strong><br>
                    <%=GetPropertyAddress(cartItem) %>
                </td>
                <td class="table-row" align="right">$<%=FormatAmount(cartItem.Amount) %></td>
            </tr>
            <tr>
                <td class="table-row-bold" colspan="3" align="right">Subtotal (<%=GetCartItemCount() %> item(s)): </td>
                <td class="table-row-bold" align="right">$<%=SubTotal() %></td>
            </tr>
            <% Next %>
          
        </tbody>
    </table>
    <% End If %>
     <% End If %>
</div>
<!-- START DELETE CONFIRM MODAL DIALOG -->
                            <div id="myModal" class="modal fade" role="dialog">
                                <div class="modal-dialog">
                                    <!-- Modal content-->
                                    <div class="modal-content">
                                        <div class="modal-header">
                                            <button type="button" class="close" data-dismiss="modal">
                                                &times;</button>
                                            <h4 class="modal-title">
                                                Confirm Item delete.</h4>
                                        </div>
                                        <div class="modal-body">
                                            <div id="div1" role="alert" style="margin-top: 10px;" class="alert alert-success">
                                                <div class="status-msg-text">
                                                    <span class="control-label">Are you sure you want to delete this item?</span></div>
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
<input id="hdMode" type="hidden" value=""  runat="server" />
<script>
    function removeItems(Index) {
        $('#hdSelectedIndex').val(Index);
        $('#myModal').modal();
    }
    function removeItemsFromCart() {
        $('#hdMode').val("DELETE");
        var index=$('#hdSelectedIndex').val();
        $("#frmDefault").submit();
    }
    function updateCartCount(count) {
        $("#cartCount").html(count);
    }
</script>