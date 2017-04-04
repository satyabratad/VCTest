<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="CreditCard.ascx.vb" Inherits="B2P.PaymentLanding.Express.Web.CreditCard" %>

<%--<div class="form-group form-group-sm">
  <label class="control-label" for="txtCreditFirstName">First Name:</label>
  <asp:TextBox id="txtCreditFirstName" cssclass="form-control input-sm" placeholder="First Name" maxlength="20" runat="server" />
</div>

<div class="form-group form-group-sm">
  <label class="control-label" for="txtCreditLastName">Last Name:</label>
  <asp:TextBox id="txtCreditLastName" cssclass="form-control input-sm" placeholder="Last Name" maxlength="30" runat="server" />
</div>--%>

<div class="form-group form-group-sm">
  <label class="control-label" for="txtNameonCard"><asp:Literal ID="litNameonCard" runat="server" text="<%$ Resources:WebResources, lblNameonCard %>" />:</label>
  <asp:TextBox id="txtNameonCard" cssclass="form-control input-sm" placeholder="Name on Card" maxlength="60" runat="server" />
</div>

<div class="form-group form-group-sm">
  <label class="control-label" for="txtCreditCardNumber"><asp:Literal ID="litCreditCardNumber" runat="server" text="<%$ Resources:WebResources, lblCreditCardNumber %>" />:<asp:Literal id="litCardImages" runat="server" /></label>
  <asp:TextBox id="txtCreditCardNumber" type="tel" cssclass="form-control input-sm txtCreditCardNumber card-image" placeholder="Credit Card Number" maxlength="20" autocomplete="off" runat="server" />
</div>

<div class="row">
  <div class="col-xs-12 col-sm-8">
    <div class="form-group form-group-sm">
      <label class="control-label" for="txtExpireDate"><asp:Literal ID="litExpirationDate" runat="server" text="<%$ Resources:WebResources, lblExpirationDate %>" />:</label>
      <asp:TextBox id="txtExpireDate" type="tel" cssclass="form-control input-sm txtExpireDate" placeholder="MM / YY" maxlength="7" runat="server" />
    </div>
  </div>

  <div class="col-xs-12 col-sm-4">
    <div class="form-group form-group-sm">
      <label class="control-label" for="txtCCV">CVV: <a href="#" data-toggle="modal" data-target="#ccInfoModal" data-backdrop="static" data-keyboard="false" title="CVV Info" tabindex="-1">?</a></label>
      <asp:TextBox id="txtCCV" type="tel" cssclass="form-control input-sm txtCCV cvv" placeholder="CVV" maxlength="4" autocomplete="off" runat="server" />
    </div>
  </div>
</div>