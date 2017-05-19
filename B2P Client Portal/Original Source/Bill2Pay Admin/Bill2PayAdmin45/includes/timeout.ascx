<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="timeout.ascx.vb" Inherits="Bill2PayAdmin45.timeout" %>
<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="ctl" %>

    <script type="text/javascript" src="../scripts/jquery-1.7.min.js"></script>
    <script type="text/javascript" src="../scripts/jquery-ui-1.8.16.min.js"></script>
        
<div>
                                                                        
        <ctl:Timeout ID="Timeout1" runat="server" title="Session Expiring" 
            TimeoutUrl="~/Welcome" CountDownSpanId="countDown1" 
            onraisingcallbackevent="Timeout1_RaisingCallbackEvent" >
            <Template>                                      
                <p>
                    <span class="ui-icon ui-icon-alert" style="float:left; margin: 1px 10px 20px 0;"></span>
                    Your session is about to Expire.
                </p>
                <span id="countDown1"></span>
                <p style="margin-left: 25px;">Click <b>OK</b> to continue your session.</p>                 
            </Template>
        </ctl:Timeout>   
                    
</div>
