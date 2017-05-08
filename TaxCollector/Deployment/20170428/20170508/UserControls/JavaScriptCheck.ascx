<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="JavaScriptCheck.ascx.vb" Inherits=".JavaScriptCheck" %>
<!--// START NO SCRIPT CHECK //-->
    <noscript>
        <style type="text/css">
            .content {display:none;}
        </style>
        <div class="container">
                <div class="row" style="background-color:white; padding:5px;">
                    <div class="row">
                        <div class="col-xs-12">
                        <h3 class="text-primary"><asp:Literal id="litJavascriptHeading" text="<%$ Resources:WebResources, JavascriptHeading %>" runat="server" /></h3>
                        <hr />
                        <br />
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-xs-12 col-sm-6">
                            <asp:Panel ID="pnlJSMessage" runat="server" class="alert alert-danger alert-sm">
                                <div id="imgStatusMsgIcon" class="fa fa-exclamation-circle fa-2x status-msg-icon"></div>
                                    <div id="txtStatusMsg" class="status-msg-text"><asp:Literal id="litJavascriptMessage" text="<%$ Resources:WebResources, JavascriptMessage %>" runat="server" /></div>
                            </asp:Panel>
                                            
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        </div>
                    </div>
                    </div>
            </div>
    </noscript>
<!--// END NO SCRIPT CHECK //-->