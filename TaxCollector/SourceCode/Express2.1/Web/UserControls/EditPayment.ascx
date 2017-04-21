<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="EditPayment.ascx.vb" Inherits="B2P.PaymentLanding.Express.Web.EditPayment" %>


    <div class="footer">
        <div class="container">

            <div class="row rowheight" style="background-color: #195575;">
                <div class="col-xs-12 col-sm-12 text-center">
                    <div class="col-sm-6">
                        <asp:Literal Id="litLink1" runat="server" />           
                    </div>
                   
                    <div class="col-sm-6">      
                       <asp:Literal Id="litBill2Pay" runat="server" />   
                    </div>
                </div>

            </div>
          
           <div class="row rowheight" style="background-color: #0e3042;">
                  <div class="col-xs-12 col-sm-12 text-center">
                    &copy; <asp:Literal ID="litYear" runat="server" /> <a href="http://www.bill2pay.com" target="_blank">BILL2PAY</a> LLC, ALL RIGHTS RESERVED. POWERED BY STREAM.
                 </div>
            </div>
        </div>
    </div>