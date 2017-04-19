<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="PropertyAddress.ascx.vb" Inherits=".PropertyAddress" %>
<asp:Panel ID="pnlAddress" runat="server">
    <div class="form-group form-group-sm">
        <asp:Panel runat="server" ID="pnlHeading">
            <asp:Label runat="server" ID="lblHeading" class="contentHeading" Text="<%$ Resources:WebResources, PropertyAddressTitle %>"></asp:Label>
        </asp:Panel>
    </div>
    <div class="form-group form-group-sm">
        <asp:Panel runat="server" ID="pnlAddress1">
            <label class="control-label" for="txtAddress1" id="lblPropAddress1">
                <asp:Label ID="lblAddress1" runat="server" Text="<%$ Resources:WebResources, Address1Label %>"></asp:Label>
            </label>
            <asp:TextBox runat="server" MaxLength="40" ID="txtAddress1" class="form-control input-sm"
                onkeypress="return restrictInput(event, restrictionTypes.AlphaNumericAndExtraChars)"
                onpaste="return reformatInput(this, restrictionTypes.AlphaNumericAndExtraChars)"></asp:TextBox>
        </asp:Panel>
    </div>
    <div class="form-group form-group-sm">
        <asp:Panel runat="server" ID="pnlAddress2">
            <label class="control-label" for="txtAddress2" id="lblPropAddress2">
                <asp:Label ID="lblAddress2" runat="server" Text="<%$ Resources:WebResources, Address2Label %>"></asp:Label>
            </label>
            <asp:TextBox runat="server" MaxLength="40" ID="txtAddress2" class="form-control input-sm"
                onkeypress="return restrictInput(event, restrictionTypes.AlphaNumericAndExtraChars)"
                onpaste="return reformatInput(this, restrictionTypes.AlphaNumericAndExtraChars)"></asp:TextBox>
        </asp:Panel>
    </div>
    <div class="form-group form-group-sm">
        <asp:Panel runat="server" ID="pnlCity">
            <label class="control-label" for="txtCity" id="lblPropCity">
                <asp:Label ID="lblCity" runat="server" Text="<%$ Resources:WebResources, CityLabel %>"></asp:Label>
            </label>
            <asp:TextBox runat="server" MaxLength="40" ID="txtCity" class="form-control input-sm"
                onkeypress="return restrictInput(event, restrictionTypes.AlphaNumericAndExtraChars)"
                onpaste="return reformatInput(this, restrictionTypes.AlphaNumericAndExtraChars)"></asp:TextBox>
        </asp:Panel>
    </div>
    <div class="form-group form-group-sm">
        <asp:Panel runat="server" ID="pnlState">
            <label class="control-label" for="ddlState" id="lblPropState">
                <asp:Label ID="lblState" runat="server" Text="<%$ Resources:WebResources, StateLabel %>"></asp:Label>
            </label>
            <asp:DropDownList runat="server" ID="ddlState" class="form-control input-sm">
            </asp:DropDownList>
        </asp:Panel>
    </div>
    <div class="form-group form-group-sm">
        <asp:Panel runat="server" ID="pnlZip">
            <label class="control-label" for="txtZip" id="lblPropZip">
                <asp:Label ID="lblZip" runat="server" Text="<%$ Resources:WebResources, ZipLabel %>"></asp:Label>
            </label>
            <asp:TextBox runat="server" MaxLength="40" ID="txtZip" class="form-control input-sm"
                onkeypress="return restrictInput(event, restrictionTypes.ZipCode)" onpaste="return reformatInput(this, restrictionTypes.ZipCode)"></asp:TextBox>
        </asp:Panel>
    </div>


</asp:Panel>
<div class="form-group form-group-sm">
    <asp:Panel runat="server" ID="pnlAmount">
        <label class="control-label" for="txtAmount" id="lblPropAmount">
            <asp:Label ID="lblAmount" runat="server" Text="<%$ Resources:WebResources, AmountLabel %>"></asp:Label>
        </label>
        <asp:TextBox runat="server" required="true" MaxLength="40" ID="txtAmount"
            class="form-control input-sm" onkeypress="return isNumberKey(event);"></asp:TextBox>
    </asp:Panel>
</div>
