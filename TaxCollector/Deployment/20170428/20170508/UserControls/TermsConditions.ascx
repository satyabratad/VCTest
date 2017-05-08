<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="TermsConditions.ascx.vb" Inherits="B2P.PaymentLanding.Express.Web.TermsConditions" %>

<div id="pnlEmailInfo" class="form-group form-group-sm"  runat="server">
  <label class="control-label" for="txtEmailAddress">
    <asp:Literal id="litEmailAddressIntro" text="<%$ Resources:WebResources, ConfirmEmailInfo %>" runat="server" />
  </label>
  <asp:TextBox id="txtEmailAddress" cssclass="form-control input-sm" placeholder="Email Address" runat="server" />
</div>
<div class="form-group form-group-sm">
  <asp:CheckBox id="chkTermsAgree" ToolTip="I Agree to Terms and Conditions"
                cssclass="checkbox"
                runat="server" />
</div>


