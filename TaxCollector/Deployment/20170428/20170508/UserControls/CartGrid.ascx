<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="CartGrid.ascx.vb" Inherits=".CartGrid" %>
<%@ Import Namespace="B2P.PaymentLanding.Express" %>
<%@ Import Namespace="B2P.PaymentLanding.Express.Web" %>
<div id="cartGrid" style="display: block;" class="table-responsive">
    <!--Non-Lookup----------------------------------------------------------------------------------------->

    <asp:Repeater ID="rptNonLookup" runat="server">
        <HeaderTemplate>
            <table class="table" style="width: 100%;" id="tblNonLookup">
                <thead>
                    <tr>
                        <td class="table-header" width="5%"></td>
                        <td class="table-header" width="30%"><asp:Literal runat="server" Text="<%$ Resources:WebResources, CartGridColumn_Item %>"></asp:Literal></td>
                        <td class="table-header" width="55%"><asp:Literal runat="server" Text="<%$ Resources:WebResources, CartGridColumn_Details %>"></asp:Literal></td>
                        <td class="table-header" width="10%" align="right"><asp:Literal runat="server" Text="<%$ Resources:WebResources, CartGridColumn_Amount %>"></asp:Literal></td>
                    </tr>
                </thead>
        </HeaderTemplate>
        <ItemTemplate>
            <tbody>
                <tr id="trIndex" <%# Eval("Index") %>>
                    <td class="<%# GetCssClass(Eval("Index")) %>" style="align-content: center; cursor: pointer;"><a onclick="removeItems(<%# Eval("Index") %>);" title="Delete Item"><i class="fa fa-trash-o fa-lg" aria-hidden="true"></i></a></td>
                    <td class="<%# GetCssClass(Eval("Index")) %>"><%# Eval("Item") %></td>
                    <td class="<%# GetCssClass(Eval("Index")) %>"><%# GetAccountInformation(Eval("Index")) %><br>
                        <strong><asp:Literal runat="server" Text="<%$ Resources:WebResources, CartGridCaption_PropAddress %>"></asp:Literal></strong><br>
                        <%# GetPropertyAddress(Eval("Index")) %>
                    </td>
                    <td class="<%# GetCssClass(Eval("Index")) %>" align="right"><%# FormatAmount(Eval("Amount")) %></td>
                </tr>
        </ItemTemplate>
        <FooterTemplate>
            <tr>
                <td class="table-row-bold" colspan="3" align="right"><%# FormatCartItemCount() %></td>
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
                        <td class="table-header" width="20%"><asp:Literal runat="server" Text="<%$ Resources:WebResources, CartGridColumn_Item %>"></asp:Literal></td>
                        <td class="table-header" width="50%"><asp:Literal runat="server" Text="<%$ Resources:WebResources, CartGridColumn_Details %>"></asp:Literal></td>
                        <td class="table-header" width="10%" align="right"><asp:Literal runat="server" Text="<%$ Resources:WebResources, CartGridColumn_Amountdue %>"></asp:Literal></td>
                        <td class="table-header" width="10%" align="right"><asp:Literal runat="server" Text="<%$ Resources:WebResources, CartGridColumn_Amount %>"></asp:Literal></td>
                    </tr>
                </thead>
        </HeaderTemplate>
        <ItemTemplate>
            <tbody>
                <tr id="trIndex" <%# Eval("Index") %>>
                    <td class="<%# GetCssClass(Eval("Index")) %>" style="align-content: center; cursor: pointer;"><a onclick="removeItems(<%# Eval("Index") %>);" title="Delete Item"><i class="fa fa-trash-o fa-lg" aria-hidden="true"></i></a></td>
                    
                    <td class="<%# GetCssClass(Eval("Index")) %>" style="align-content: center;"><a style="<%# GetEditIconVisivility(Eval("Index")) %>;cursor:pointer;" onclick="editItems(<%# Eval("Index") %>);" title="Edit Item"><i class="fa fa-pencil-square-o fa-lg" aria-hidden="true"></i></a></td>
                   
                    
                    <td class="<%# GetCssClass(Eval("Index")) %>"><%# Eval("Item") %></td>
                    <td class="<%# GetCssClass(Eval("Index")) %>"><%# GetAccountInformation(Eval("Index")) %><br>
                        <strong><asp:Literal runat="server" Text="<%$ Resources:WebResources, CartGridCaption_PropAddress %>"></asp:Literal></strong><br>
                        <%# GetPropertyAddress(Eval("Index")) %>
                    </td>
                    <td class="<%# GetCssClass(Eval("Index")) %>" align="right"><%# FormatAmount(Eval("AmountDue")) %></td>
                    <td class="<%# GetCssClass(Eval("Index")) %>" align="right"><%# FormatAmount(Eval("Amount")) %></td>
                </tr>
        </ItemTemplate>
        <FooterTemplate>
            <tr>
                <td class="table-row-bold" colspan="5" align="right"><%# FormatCartItemCount() %></td>
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
                        <td class="table-header" width="20%"><asp:Literal runat="server" Text="<%$ Resources:WebResources, CartGridColumn_Item %>"></asp:Literal></td>
                        <td class="table-header" width="55%"><asp:Literal runat="server" Text="<%$ Resources:WebResources, CartGridColumn_Details %>"></asp:Literal></td>
                        <td class="table-header" width="10%" align="right"><asp:Literal runat="server" Text="<%$ Resources:WebResources, CartGridColumn_Amountdue %>"></asp:Literal></td>
                        <td class="table-header" width="10%" align="right"><asp:Literal runat="server" Text="<%$ Resources:WebResources, CartGridColumn_Amount %>"></asp:Literal></td>
                    </tr>
                </thead>
        </HeaderTemplate>
        <ItemTemplate>
            <tbody>
                <tr id="trIndex" <%# Eval("Index") %>>
                  
                    <td class="<%# GetCssClass(Eval("Index")) %>" style="align-content: center;"><a  style="<%# GetEditIconVisivility(Eval("Index")) %>;cursor:pointer;" onclick="editItems(<%# Eval("Index") %>);" title="Edit Item"><i class="fa fa-pencil-square-o fa-lg" aria-hidden="true"></i></a></td>
                  
                    
                    <td class="<%# GetCssClass(Eval("Index")) %>"><%# Eval("Item") %></td>
                    <td class="<%# GetCssClass(Eval("Index")) %>"><%# GetAccountInformation(Eval("Index")) %><br>
                        <strong><asp:Literal runat="server" Text="<%$ Resources:WebResources, CartGridCaption_PropAddress %>"></asp:Literal></strong><br>
                        <%# GetPropertyAddress(Eval("Index")) %>
                    </td>
                    <td class="<%# GetCssClass(Eval("Index")) %>" align="right"><%# FormatAmount(Eval("AmountDue")) %></td>
                    <td class="<%# GetCssClass(Eval("Index")) %>" align="right"><%# FormatAmount(Eval("Amount")) %></td>
                </tr>
        </ItemTemplate>
        <FooterTemplate>
            <tr>
                <td class="table-row-bold" colspan="4" align="right"><%# FormatCartItemCount() %></td>
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
                <h4 class="modal-title"><asp:Label ID="lblConfirmDeleteHeader" Text="<%$ Resources:WebResources, lblConfirmDeleteHeader %>" runat="server"></asp:Label> </h4>
            </div>
            <div class="modal-body">
                <div id="div1" role="alert" style="margin-top: 10px;" class="alert alert-success">
                    <div class="status-msg-text">
                        <span class="control-label"><asp:Label ID="lblConfirmDeleteMessage"  Text="<%$ Resources:WebResources, lblConfirmDeleteMessage %>" runat="server"/></span>
                    </div>
                </div>
            </div>
            <div class="modal-footer">              
                 <asp:Button type="button" ID="btnYes" runat="server" CssClass="btn btn-default" title="Yes" OnClientClick="removeItemsFromCart();" Text="<%$ Resources:WebResources, YesButton %>"></asp:Button>
                            <asp:Button type="btnNo" ID="Button2" runat="server" CssClass="btn btn-default" title="No" OnClientClick="return false;" data-dismiss="modal" Text="<%$ Resources:WebResources, NoButton %>"></asp:Button>
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
