<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="EditLookupItem.ascx.vb" Inherits=".EditLookupItem" %>
<div id="divAccountDetails3" style="display: block;">
    <div class="col-xs-12 col-sm-6">
        <div class="row">
            <asp:Panel runat="server" ID="Panel1">
                <div class="col-xs-12">
                    <div class="form-group form-group-sm">
                         <label class="control-label" for="txtAmount" id="lblHeading">
                            <asp:Label ID="Label1" runat="server" Text="<%$ Resources:WebResources, EditAmountCaption %>"></asp:Label>
                        </label>
                    </div>
                </div>
            </asp:Panel>
            <asp:Panel runat="server" ID="divSelectedItem">
                <div class="col-xs-12">
                    <div class="form-group form-group-sm">
                        <label class="control-label" for="lblSelectedItemValue">
                            <asp:Label runat="server" class="control-label" ID="lblSelectedItem" Text="<%$ Resources:WebResources, SelectedItemEdit %>">
                            </asp:Label>
                        </label>
                        <br>
                        <asp:Label runat="server" ID="lblSelectedItemValue"></asp:Label>

                    </div>
                </div>
            </asp:Panel>
            <asp:Panel runat="server" ID="divLookupAccount1Edit">
                <asp:Repeater ID="ripAccountIds" runat="server">
                    <ItemTemplate>
                        <div class="col-xs-12">
                            <div class="form-group form-group-sm" id="pnlEditAcc1">
                                <label class="control-label" for="lblLookupAccount1Value">
                                    <asp:Label runat="server" class="control-label" ID="lblLookupAccount1" Text='<%#Eval("Label") %>'>
                                    </asp:Label>
                                </label>

                                <br>
                                <asp:Label runat="server" ID="lblLookupAccount1Value" Text='<%#Eval("Value") %>'></asp:Label>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </asp:Panel>
            <asp:Panel runat="server" ID="pnlAmount">
                <div class="col-xs-12">
                    <div class="form-group form-group-sm">

                        <label class="control-label" for="txtAmount" id="lblPropAmount">
                            <asp:Label ID="lblAmount" runat="server" Text="<%$ Resources:WebResources, AmountLabel %>"></asp:Label>
                        </label>
                        <asp:TextBox runat="server" required="true" MaxLength="40" ID="txtAmountEdit"
                            class="form-control input-sm" onkeypress="return isNumberKey(event);"></asp:TextBox>

                    </div>
                </div>
            </asp:Panel>
        </div>

        <div class="pull-right">
            <asp:Button ID="btnUpdateItem" OnClientClick="return ValidateUpdateCartItem()" CssClass="btn btn-primary btn-sm" Text="<%$ Resources:WebResources, ButtonUpdateCart %>" ToolTip="<%$ Resources:WebResources, ButtonUpdateCart %>" runat="server" />
        </div>
        <br />
    </div>
</div>

<script type="text/javascript">
    function ValidateUpdateCartItem() {

         <% If Not SelectedItem Is Nothing Then%>
        var newValue = parseFloat($("#txtAmountEdit").val());
        var oldValue = parseFloat(<%= SelectedItem.AmountDue%>);
       
         var item = new ValidationItem();

        // Create instance of the form validator
         var validator = new FormValidator();
         validator.setErrorMessageHeader("Please review the following errors and resubmit the form:\n\n")
         validator.setInvalidCssClass("has-error");
         validator.setAlertBoxStatus(false);
        
         if ((!newValue>0) ||(newValue > oldValue)) {
             // Set the validator
             validator.addValidationItem(new ValidationItem("txtAmountEdit", fieldTypes.AmountDue, true, "Invalid Amount"));
             $("#txtAmountEdit").val(oldValue);
             return validator.validate();
         }
        <% Else  %>
        return false;
        <% End If %>
    }

</script>
