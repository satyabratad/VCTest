<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="EditLookupItem.ascx.vb" Inherits=".EditLookupItem" %>
<div id="divAccountDetails3" style="display: block;">
    <div class="col-xs-12 col-sm-6">
        <div class="row">
            <asp:Panel runat="server" ID="divSelectedItem">
                <div class="col-xs-12">
                    <div class="form-group form-group-sm">

                        <asp:Label runat="server" class="control-label" for="lblSelectedItemValue" ID="lblSelectedItem" Text="Selected Item">
                        </asp:Label>
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
                                <asp:Label runat="server" class="control-label" for="lblLookupAccount1Value" ID="lblLookupAccount1" Text='<%#Eval("Label") %>'>
                                </asp:Label>
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
                        <asp:TextBox runat="server" required="true" MaxLength="40" ID="txtAmount"
                            class="form-control input-sm" onkeypress="return isNumberKey(event);"></asp:TextBox>

                    </div>
                </div>
            </asp:Panel>
        </div>

        <div class="pull-right">
            <input type="button" name="btnUpdate" value="Update Cart" onclick="return updateCartItem();" id="btnUpdate" title="Update Cart" class="btn btn-primary ">
        </div>
        <br>
    </div>
</div>
