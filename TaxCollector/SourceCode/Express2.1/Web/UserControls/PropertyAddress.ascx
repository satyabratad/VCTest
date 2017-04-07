<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="PropertyAddress.ascx.vb" Inherits=".PropertyAddress" %>

<div class="form-group form-group-sm">
    <asp:Panel runat="server" id="pnlHeading">
        <asp:Label runat="server" ID="lblHeading" class="contentHeading">Property Address</asp:Label>
    </asp:Panel>
</div>
<div class="form-group form-group-sm">
    <asp:Panel runat="server" id="pnlAddress1">
        <label class="control-label" for="txtAddress1" id="lblPropAddress1">
            <asp:Label ID="lblAddress1" runat="server">Address1</asp:Label>
        </label>
        <asp:TextBox runat="server" maxlength="40" id="txtAddress1" class="form-control input-sm"
            onkeypress="return restrictInput(event, restrictionTypes.AlphaNumericAndExtraChars)"
            onpaste="return reformatInput(this, restrictionTypes.AlphaNumericAndExtraChars)" ></asp:TextBox>
    </asp:Panel>
</div>
<div class="form-group form-group-sm">
    <asp:Panel runat="server" id="pnlAddress2">
        <label class="control-label" for="txtAddress2" id="lblPropAddress2">
            <asp:Label id="lblAddress2" runat="server">Address2</asp:Label>
        </label>
        <asp:TextBox runat="server" maxlength="40" id="txtAddress2" class="form-control input-sm"
            onkeypress="return restrictInput(event, restrictionTypes.AlphaNumericAndExtraChars)"
            onpaste="return reformatInput(this, restrictionTypes.AlphaNumericAndExtraChars)" ></asp:TextBox>
    </asp:Panel>
</div>
<div class="form-group form-group-sm">
    <asp:Panel runat="server" id="pnlCity">
        <label class="control-label" for="txtCity" id="lblPropCity">
            <asp:Label id="lblCity" runat="server">City</asp:Label>
        </label>
        <asp:TextBox runat="server" maxlength="40" id="txtCity" class="form-control input-sm"
            onkeypress="return restrictInput(event, restrictionTypes.AlphaNumericAndExtraChars)"
            onpaste="return reformatInput(this, restrictionTypes.AlphaNumericAndExtraChars)" ></asp:TextBox>
    </asp:Panel>
</div>
<div class="form-group form-group-sm">
    <asp:Panel runat="server" id="pnlState">
        <label class="control-label" for="ddlState" id="lblPropState">
            <asp:Label id="lblState" runat="server">State</asp:Label>
        </label>
        <asp:DropDownList runat="server" id="ddlState" class="form-control input-sm">
        </asp:DropDownList>
    </asp:Panel>
</div>
<div class="form-group form-group-sm">
    <asp:Panel runat="server" id="pnlZip">
        <label class="control-label" for="txtZip" id="lblPropZip">
            <asp:Label id="lblZip" runat="server">Zip</asp:Label>
        </label>
        <asp:TextBox runat="server" maxlength="40" id="txtZip" class="form-control input-sm"
            onkeypress="return isNumberKey(event);" ></asp:TextBox>
    </asp:Panel>
</div>
<asp:HiddenField ID="hdAmount" runat="server" Value="" />
<div class="form-group form-group-sm">
    <asp:Panel runat="server" id="pnlAmount">
        <label class="control-label" for="txtAmount" id="lblPropAmount">
            <asp:Label id="lblAmount" runat="server">Amount</asp:Label>
        </label>
        <asp:TextBox runat="server" required="true" maxlength="40" id="txtAmount"
            class="form-control input-sm" onkeypress="return isNumberKey(event);" ></asp:TextBox>
    </asp:Panel>
</div>
