<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="EditPayment.ascx.vb" Inherits="B2P.PaymentLanding.Express.Web.EditPayment" %>


<div class="row">
    <div class="col-xs-6 contentHeadingBlack14">Payment Details</div>
    <div class="col-xs-6">
        <div class="pull-right">
            <asp:LinkButton Text="Edit Payment Details" runat="server" ID="lnkEdit" />
        </div>
    </div>
</div>
<hr>
<div class="row">
    <div class="col-xs-5 contentHeadingBlack13">Payment Method: </div>
    <asp:Label runat="server" CssClass="contentBlack13" id="lblPaymentMethod"></asp:Label>
</div>
<asp:panel runat="server" CssClass="row" id="pnlExpDate">
    <div class="col-xs-5 contentHeadingBlack13" id="expDateCap">Expiration Date:</div>
    <asp:Label runat="server" cssclass="contentBlack13" id="lblExpDate"></asp:Label>
</asp:panel>
<asp:panel CssClass="row" id="pnlZip" runat="server">
    <asp:Label runat="server" ID="lblBillingZipCaption"  cssclass="col-xs-5 contentHeadingBlack13">Billing Zip:</asp:Label>
    <asp:Label runat="server" cssclass="contentBlack13" id="lblBillZip"></asp:Label>
</asp:panel>
    
