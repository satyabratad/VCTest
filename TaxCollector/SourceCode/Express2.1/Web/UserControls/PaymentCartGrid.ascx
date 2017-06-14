<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="PaymentCartGrid.ascx.vb" Inherits=".PaymentCartGrid" %>
<%@ Import Namespace="B2P.PaymentLanding.Express" %>
<%@ Import Namespace="B2P.PaymentLanding.Express.Web" %>
<div id="cartGrid" style="display: block;" class="table-responsive">
    <!--Non-Lookup----------------------------------------------------------------------------------------->

    <asp:Repeater ID="rptNonLookup" runat="server">
        <HeaderTemplate>
            <table class="table" style="width: 100%;" id="tblNonLookup">
                <thead>
                    <tr>
                        <td class="table-header" width="5%"></td>
                        <td class="table-header" width="30%"><asp:Literal runat="server" Text="<%$ Resources:WebResources, CartGridColumn_Item %>"></asp:Literal></td>
                        <td class="table-header" width="55%"><asp:Literal runat="server" Text="<%$ Resources:WebResources, CartGridColumn_Details %>"></asp:Literal></td>
                        <td class="table-header" width="10%" align="right"><asp:Literal runat="server" Text="<%$ Resources:WebResources, CartGridColumn_Amount %>"></asp:Literal></td>
                    </tr>
                </thead>
        </HeaderTemplate>
        <ItemTemplate>
            <tbody>
                <tr id="trIndex" <%# Eval("Index") %>>
                    <td class="<%# GetCssClass(Eval("Index")) %>" style="align-content: center;"><%#CType(Eval("Index"), Integer) + 1%></td>
                    <td class="<%# GetCssClass(Eval("Index")) %>"><%# Eval("Item") %></td>
                    <td class="<%# GetCssClass(Eval("Index")) %>"><%# GetAccountInformation(Eval("Index")) %>
                        <strong><asp:Literal runat="server" Text="<%$ Resources:WebResources, CartGridCaption_PropAddress %>"></asp:Literal></strong><br>
                        <%# GetPropertyAddress(Eval("Index")) %>
                    </td>
                    <td class="<%# GetCssClass(Eval("Index")) %>" align="right"><%# FormatAmount(Eval("Amount")) %></td>
                </tr>
        </ItemTemplate>
        <FooterTemplate>
            <tr>
                <td class="table-row-bold" colspan="3" align="right"><%# FormatCartItemCount() %></td>
                <td class="table-row-bold" align="right"><%# SubTotal() %></td>
            </tr>
            <% If BLL.SessionManager.IsConvenienceFeesApplicable Then %>
            <tr>
                <td class="table-alternateRow" colspan="3" align="right">
                    <span><asp:Literal ID="litFee" Text="<%$ Resources:WebResources,LblFee %>" runat="server" /> </span>
                    <a href="#" data-toggle="modal" data-target="#feeInfoModal" data-keyboard="false" title="Fee Info" tabindex="-1">
                        <i class="fa fa-question-circle fa-1" aria-hidden="true"></i>
                        <span class="text-hide"></span></a><span>:</span>
                </td>
                <td class="table-alternateRow" align="right"><%# GetConvenienceFee() %></td>
            </tr>
            <% End If %>
            <tr>
                <td class="table-row-bold" colspan="3" align="right"><span><asp:Literal ID="litTotal" Text="<%$ Resources:WebResources, lblCartTotalAmount %>" runat="server" /></span></td>
                <td class="table-row-bold" align="right"><%# Total() %></td>
            </tr>
            </tbody>
    </table>
        </FooterTemplate>
    </asp:Repeater>

    <!--End Non-Lookup------------------------------------------------------------------------------------->
    <!--Lookup----------------------------------------------------------------------------------------->

    <asp:Repeater ID="rptLookup" runat="server">
        <HeaderTemplate>
            <table class="table" style="width: 100%;" id="tblLookup">
                <thead>
                    <tr>
                        <td class="table-header" width="5%"></td>
                        <td class="table-header" width="20%"><asp:Literal runat="server" Text="<%$ Resources:WebResources, CartGridColumn_Item %>"></asp:Literal></td>
                        <td class="table-header" width="55%"><asp:Literal runat="server" Text="<%$ Resources:WebResources, CartGridColumn_Details %>"></asp:Literal></td>
                        <td class="table-header" width="10%" align="right"><asp:Literal runat="server" Text="<%$ Resources:WebResources, CartGridColumn_Amountdue %>"></asp:Literal></td>
                        <td class="table-header" width="10%" align="right"><asp:Literal runat="server" Text="<%$ Resources:WebResources, CartGridColumn_Amount %>"></asp:Literal></td>
                    </tr>
                </thead>
        </HeaderTemplate>
        <ItemTemplate>
            <tbody>
                <tr id="trIndex" <%# Eval("Index") %>>
                    <td class="<%# GetCssClass(Eval("Index")) %>" style="align-content: center;"><%# ctype(Eval("Index"), Integer) + 1%></td>
                    <td class="<%# GetCssClass(Eval("Index")) %>"><%# Eval("Item") %></td>
                    <td class="<%# GetCssClass(Eval("Index")) %>"><%# GetAccountInformation(Eval("Index")) %><br>
                        <strong><asp:Literal runat="server" Text="<%$ Resources:WebResources, CartGridCaption_PropAddress %>"></asp:Literal></strong><br>
                        <%# GetPropertyAddress(Eval("Index")) %>
                    </td>
                    <td class="<%# GetCssClass(Eval("Index")) %>" align="right"><%# FormatAmount(Eval("AmountDue")) %></td>
                    <td class="<%# GetCssClass(Eval("Index")) %>" align="right"><%# FormatAmount(Eval("Amount")) %></td>
                </tr>
        </ItemTemplate>
        <FooterTemplate>
            <tr>
                <td class="table-row-bold" colspan="4" align="right"><%# FormatCartItemCount() %></td>
                <td class="table-row-bold" align="right"><%# SubTotal() %></td>
            </tr>
            <% If BLL.SessionManager.IsConvenienceFeesApplicable Then %>
            <tr>
                <td class="table-alternateRow" colspan="4" align="right">
                    <span><asp:Literal ID="litFee" Text="<%$ Resources:WebResources, LblFee %>" runat="server" /> </span>
                    <a href="#" data-toggle="modal" data-target="#feeInfoModal" data-keyboard="false" title="Fee Info" tabindex="-1">
                        <i class="fa fa-question-circle fa-1" aria-hidden="true"></i>
                        <span class="text-hide"></span></a><span>:</span>
                </td>
                <td class="table-alternateRow" align="right"><%# GetConvenienceFee() %></td>
            </tr>
            <% End If %>
            <tr>
                <td class="table-row-bold" colspan="4" align="right"><span><asp:Literal ID="litTotal" Text="<%$ Resources:WebResources, lblCartTotalAmount %>" runat="server" /></span></td>
                <td class="table-row-bold" align="right"><%# Total() %></td>
            </tr>
            </tbody>
    </table>
        </FooterTemplate>
    </asp:Repeater>

    <!--End Lookup------------------------------------------------------------------------------------->
    <!--SSO----------------------------------------------------------------------------------------->

    <asp:Repeater ID="rptSSO" runat="server">
        <HeaderTemplate>
            <table class="table" style="width: 100%;" id="tblSSO">
                <thead>
                    <tr>
                        <td class="table-header" width="5%"></td>
                        <td class="table-header" width="20%"><asp:Literal runat="server" Text="<%$ Resources:WebResources, CartGridColumn_Item %>"></asp:Literal></td>
                        <td class="table-header" width="55%"><asp:Literal runat="server" Text="<%$ Resources:WebResources, CartGridColumn_Details %>"></asp:Literal></td>
                        <td class="table-header" width="10%" align="right"><asp:Literal runat="server" Text="<%$ Resources:WebResources, CartGridColumn_Amountdue %>"></asp:Literal></td>
                        <td class="table-header" width="10%" align="right"><asp:Literal runat="server" Text="<%$ Resources:WebResources, CartGridColumn_Amount %>"></asp:Literal></td>
                    </tr>
                </thead>
        </HeaderTemplate>
        <ItemTemplate>
            <tbody>
                <tr id="trIndex" <%# Eval("Index") %>>
                    <td class="<%# GetCssClass(Eval("Index")) %>" style="align-content: center;"><%# ctype(Eval("Index"), Integer) + 1%></td>
                    <td class="<%# GetCssClass(Eval("Index")) %>"><%# Eval("Item") %></td>
                    <td class="<%# GetCssClass(Eval("Index")) %>"><%# GetAccountInformation(Eval("Index")) %>
                        <strong><asp:Literal runat="server" Text="<%$ Resources:WebResources, CartGridCaption_PropAddress %>"></asp:Literal></strong><br>
                        <%# GetPropertyAddress(Eval("Index")) %>
                    </td>
                    <td class="<%# GetCssClass(Eval("Index")) %>" align="right"><%# FormatAmount(Eval("AmountDue")) %></td>
                    <td class="<%# GetCssClass(Eval("Index")) %>" align="right"><%# FormatAmount(Eval("Amount")) %></td>
                </tr>
        </ItemTemplate>
        <FooterTemplate>
            <tr>
                <td class="table-row-bold" colspan="4" align="right"><%# FormatCartItemCount() %></td>
                <td class="table-row-bold" align="right"><%# SubTotal() %></td>
            </tr>
            <% If BLL.SessionManager.IsConvenienceFeesApplicable Then %>
            <tr>
                <td class="table-alternateRow" colspan="4" align="right">
                    <span><asp:Literal ID="litFee" Text="<%$ Resources:WebResources, LblFee %>" runat="server" /> </span>
                    <a href="#" data-toggle="modal" data-target="#feeInfoModal" data-keyboard="false" title="Fee Info" tabindex="-1">
                        <i class="fa fa-question-circle fa-1" aria-hidden="true"></i>
                        <span class="text-hide"></span></a><span>:</span>
                </td>
                <td class="table-alternateRow" align="right"><%# GetConvenienceFee() %></td>
            </tr>
            <% End If %>
            <tr>
                <td class="table-row-bold" colspan="4" align="right"><span><asp:Literal ID="litTotal" Text="<%$ Resources:WebResources, lblCartTotalAmount %>" runat="server" /></span></td>
                <td class="table-row-bold" align="right"><%# Total() %></td>
            </tr>
            </tbody>
    </table>
        </FooterTemplate>
    </asp:Repeater>

    <!--End SSO------------------------------------------------------------------------------------->

</div>
      <!-- START FEE DIALOG -->
      <div class="modal fade" id="feeInfoModal" tabindex="-1" role="dialog" aria-hidden="true">
        <div class="modal-dialog">
          <div class="modal-content">
            <div class="modal-header">
              <button type="button" class="close" data-dismiss="modal" aria-label="Close" title="Close"><span aria-hidden="true">&times;</span></button>
              <h4>
                <asp:Literal id="Literal1" text="<%$ Resources:WebResources, FeeInfoModalTitle %>" runat="server" />
              </h4>
            </div>

            <div class="modal-body">
                <asp:Panel ID="pnlACHFee" runat="server" Visible="false">
                    <p class="text-primarydark"><strong><asp:Literal id="litBankAccountPaymentsTitle" text="<%$ Resources:WebResources, lblBankAccountPayments %>" runat="server" /></strong></p>
                    <hr />
                      <p>
                        <asp:Literal id="litACHFee" runat="server" />
                      </p> 
                    <br />
                </asp:Panel>
                <asp:Panel ID="pnlCCFee" runat="server" Visible="false">
                    <p class="text-primarydark"><strong><asp:Literal id="litCreditCardPaymentsTitle" text="<%$ Resources:WebResources, lblCreditCardPayments %>" runat="server" /></strong></p>
                    <hr />
                       <p>
                        <asp:Literal id="litCCFee" runat="server" />
                      </p>
                </asp:Panel>               
            </div>

            <div class="modal-footer">
              <asp:Button id="Button1"
                          text="<%$ Resources:WebResources, ButtonClose %>"
                          cssclass="btn btn-primary btn-sm"
                          data-dismiss="modal"
                          runat="server" />
            </div>
          </div>
        </div>
      </div>
      <!-- END FEE MODAL DIALOG -->
