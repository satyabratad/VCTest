<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="EditContactInfo.ascx.vb" Inherits="B2P.PaymentLanding.Express.Web.EditContactInfo" %>


<asp:Panel runat="server" ID="pnlEditContactInfo">
    <div class="row">
        <div class="col-xs-6 contentHeadingBlack14">Contact Info</div>
        <div class="col-xs-6">
            <div class="pull-right">
                 <asp:LinkButton Text="Edit Contact Info" runat="server" ID="lnkEdit" />
            </div>
        </div>
    </div>
    <hr>
    <div class="row">
        <asp:Label runat="server" CssClass="col-xs-12 contentHeadingBlack13" ID="lblContactInfoName"></asp:Label>
    </div>
    <div class="row">
        <asp:Label runat="server" CssClass="col-xs-12 contentBlack13" ID="lblContactAddress"></asp:Label>
    </div>
</asp:Panel>

