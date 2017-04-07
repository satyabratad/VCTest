<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ConvenienceFee.ascx.vb" Inherits="B2P.PaymentLanding.Express.Web.ConvenienceFee" %>
<div class="form-group form-group-sm">
  <asp:CheckBox id="chkFeeAgree"
                cssclass="checkbox"
                runat="server" />
</div>

<div id="pnlEmailInfo" class="form-group form-group-sm" visible="false" runat="server">
  <label class="control-label" for="txtEmailAddress">
    <asp:Literal id="litEmailAddressIntro" text="<%$ Resources:WebResources, ConfirmEmailInfo %>" runat="server" />
  </label>
  </div>