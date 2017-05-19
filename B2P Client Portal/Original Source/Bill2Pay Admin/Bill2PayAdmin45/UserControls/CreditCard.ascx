<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="CreditCard.ascx.vb" Inherits="Bill2PayAdmin45.CreditCard1" %>

<div class="form-group form-group-sm">
  <label class="control-label" for="txtCreditCardNumber">Credit Card Number: <asp:Literal id="litCardImages" runat="server" /></label>
    <a href="#" id="lnkCardProcessInfo" class="tooltip-info pull-right" data-toggle="tooltip" data-placement="left" title="The convenience fees will be based on the card type entered.">
    <span class="fa fa-info-circle"></span>
  </a>
  <asp:TextBox id="txtCreditCardNumber" type="tel" cssclass="form-control input-sm txtCreditCardNumber card-image" placeholder="Credit Card Number" maxlength="20" aria-required="true" autocomplete="off" runat="server" />
</div>

<div class="row">
  <div class="col-xs-12 col-sm-8">
    <div class="form-group form-group-sm">
      <label class="control-label" for="txtExpireDate">Expiration Date:</label>
      <asp:TextBox id="txtExpireDate" type="tel" cssclass="form-control input-sm txtExpireDate" placeholder="MM / CCYY" maxlength="10" aria-required="true" runat="server" />
    </div>
  </div>

  <div class="col-xs-12 col-sm-4">
    <div class="form-group form-group-sm">
      <label class="control-label" for="txtCCV">CVV: <a href="#" id="lnkCvvInfo" data-toggle="modal" data-target="#ccInfoModal" data-backdrop="static" data-keyboard="false" title="CVV Info" tabindex="-1"><span class="fa fa-question-circle"></span></a></label>
      <asp:TextBox id="txtCCV" type="tel" cssclass="form-control input-sm txtCCV cvv" placeholder="CVV" maxlength="4" autocomplete="off" aria-required="true" runat="server" />
    </div>
  </div>
</div>

<div class="row">
  <div class="col-xs-12 col-sm-6">
    <div class="form-group form-group-sm">
      <label class="control-label" for="txtCreditFirstName">First Name:</label>
      <asp:TextBox id="txtCreditFirstName" cssclass="form-control input-sm" placeholder="First Name" maxlength="20" aria-required="true" runat="server" />
    </div>
  </div>
  <div class="col-xs-12 col-sm-6">
    <div class="form-group form-group-sm">
      <label class="control-label" for="txtCreditLastName">Last Name/Business:</label>
      <asp:TextBox id="txtCreditLastName" cssclass="form-control input-sm" placeholder="Last Name" maxlength="30" aria-required="true" runat="server" />
    </div>
  </div>
</div>