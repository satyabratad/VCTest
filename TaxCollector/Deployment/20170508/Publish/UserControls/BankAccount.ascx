<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="BankAccount.ascx.vb" Inherits="B2P.PaymentLanding.Express.Web.BankAccount" %>

<%--<div class="form-group form-group-sm">
  <label class="control-label" for="txtAchFirstName">First Name:</label>
  <asp:TextBox id="txtAchFirstName" cssclass="form-control input-sm" placeholder="First Name" maxlength="20" runat="server" />
</div>

<div class="form-group form-group-sm">
  <label class="control-label" for="txtAchLastName">Last Name:</label>
  <asp:TextBox id="txtAchLastName" cssclass="form-control input-sm" placeholder="Last Name" maxlength="30" runat="server" />
</div>--%>

<div class="form-group form-group-sm">
  <label class="control-label" for="txtNameonBankAccount">Name on Bank Account:</label>
  <asp:TextBox id="txtNameonBankAccount" cssclass="form-control input-sm" placeholder="Name on Bank Account" maxlength="60" runat="server"  />
</div>

<div class="form-group form-group-sm">
  <label class="control-label" for="ddlBankAccountType">Account Type:</label>
  <asp:DropDownList id="ddlBankAccountType"
                    cssclass="form-control input-sm" 
                    OnSelectedIndexChanged="ddlBankAccountType_SelectedIndexChanged" 
                    autopostback="True"
                    runat="server" />
</div>

<div class="form-group form-group-sm">
  <label class="control-label" for="txtBankRoutingNumber">Routing Number:
    <a href="#" data-toggle="modal" data-target="#achInfoModal" data-backdrop="static" data-keyboard="false" title="Routing Number Info" tabindex="-1">?</a>
  </label>
  <asp:TextBox id="txtBankRoutingNumber" type="tel" cssclass="form-control input-sm" placeholder="Routing Number" maxlength="9" autocomplete="off" runat="server" />
</div>

<div class="form-group form-group-sm">
  <label class="control-label" for="txtBankAccountNumber">Account Number:
      <a href="#" data-toggle="modal" data-target="#achInfoModal" data-backdrop="static" data-keyboard="false" title="Account Number Info" tabindex="-1">?</a>
  </label>
  <asp:TextBox id="txtBankAccountNumber" type="tel" cssclass="form-control input-sm" placeholder="Account Number" maxlength="17" autocomplete="off" runat="server" />
</div>

<%--<div class="form-group form-group-sm">
  <label class="control-label" for="txtBankAccountNumber2">Re-type Account Number:</label>
  <asp:TextBox id="txtBankAccountNumber2" type="tel" cssclass="form-control input-sm" placeholder="Account Number" maxlength="17" autocomplete="off" runat="server" />
</div>--%>
