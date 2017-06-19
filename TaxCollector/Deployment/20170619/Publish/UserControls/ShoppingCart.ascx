<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ShoppingCart.ascx.vb" Inherits=".ShoppingCart" %>
<div class="row">
    <div class="col-md-1 pull-right">
        <asp:LinkButton ID="btnCart" runat="server">
            <div id="cartDiv" class="caption img-responsive pull-right" style="margin-left: 2cm;min-width: 30px; max-width: 35px; min-height: 30px; max-height: 35px;">
                <span id="cartCount" runat="server" style="font-size: 14px; line-height: 16px; font-family: arial,sans-serif; color: #000000; font-weight: 700;"></span>
            </div>
        </asp:LinkButton>
    </div>
</div>
<br />
