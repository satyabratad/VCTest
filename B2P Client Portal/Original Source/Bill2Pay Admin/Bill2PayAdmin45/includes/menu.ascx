<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="menu.ascx.vb" Inherits="Bill2PayAdmin45.menu" %>

<style type="text/css">
#menucontainer { min-width:900px; background: #000; height:26px;}

#cssm1 { padding:0; margin:0; font: 10pt 'Segoe UI'; }

#cssm1 { position: absolute;  z-index: 99; margin: 0 auto; float: left; line-height: 20px;  }

#cssm1 a { display: block; border: 0px solid #fff; background: #000; text-decoration: none; padding: 3px 5px; color:White; }
#cssm1 a:hover { background: #999; }


#cssm1 ul li, #cssm1 ul li ul li  { width: 140px; list-style-type:none; }

#cssm1 ul li { float: left; width: 140px; }

#cssm1 ul li ul, #cssm1:hover ul li ul, #cssm1:hover ul li:hover ul li ul{ 
	display:none;
	list-style-type:none; 
	width: 120px;
	}

#cssm1:hover ul, #cssm1:hover ul li:hover ul, #cssm1:hover ul li:hover ul li:hover ul { 
	display:block; 
	}

#cssm1:hover ul li:hover ul li:hover ul { 
	position: absolute;
	margin-left: 140px;
	margin-top: -20px;
	}

</style>

<div id="menucontainer">
    <div id="cssm1">
	    <ul>
		    <li id="liPayment" runat="server" visible="false"><a href="#">Payment &nbsp; &nbsp;  »</a>
		        <ul>
		            <li runat="server" id="litCounter" visible="false"><a href="/payment/newcounter.aspx">Counter</a></li>
			        <li runat="server" id="litCallCenter" visible="false"><a href="/payment/callcenter.aspx">Call Center</a></li>
                    <li runat="server" id="litPinPad" visible="false"><a href="/payment/pin-pad.aspx">Counter (Pin Pad)</a></li>
                    <li runat="server" id="litPinPadChip" visible="false"><a href="/payment/pinpadchip.aspx">Pin Pad Chip</a></li>
		        </ul>
		    </li>
		    
            <li><a href="/search/">Search</a></li>
		    
           
		    
            <li><a href="/profile/profiles.aspx">Profiles</a></li>

	        <li id="liMainReport" runat="server" visible="false"><a href="#">Reports &nbsp; &nbsp;  »</a>
                <ul>
                      <li runat="server" id="litPayment" visible="false"><a href="/reports/ReportViewer.aspx?type=pm">Payment Report</a></li>
                      <li runat="server" id="litRecon" visible="false"><a href="/reports/ReportViewer.aspx?type=rc">Reconciliation Report</a></li>
                      <li runat="server" id="litPayout" visible="false"><a href="/reports/ReportViewer.aspx?type=po">Payout Report</a></li>
                      <li runat="server" id="litB2PAdmin" visible="false"><a href="/reports/ReportViewer.aspx?type=ba">Internal Reports</a></li>
                </ul>
            </li>
            		    
		    <li runat="server" id="liMainSecurity" visible="false"><a href="#">Admin &nbsp; &nbsp; »</a>
				    <ul>
				    <li runat="server" id="liOffice" visible="false"><a href="#">Offices  &nbsp; &nbsp; »</a>
				        <ul>
				            <li><a href="/security/modifyoffice.aspx">Modify Office</a></li>
				            <li><a href="/security/addoffice.aspx">New Office</a></li>
				        </ul>
		            </li>
				    <li><a href="#">Users  &nbsp; &nbsp; »</a>
					    <ul>
					        <li><a href="/security/modifyuser.aspx">Modify User</a></li>
						    <li><a href="/security/adduser.aspx">New User</a></li>
					    </ul>
					</li>

					</ul>
			</li>

		    <li><a href="#">My Profile  &nbsp; &nbsp; »</a>
		        <ul>
		            <li><a href="/myprofile/chgpw.aspx">Change Password</a></li>
		        </ul>
		    
		    </li>

		    <li id="liBlockAccount" runat="server" visible="false"><a href="#">Block Account &nbsp; &nbsp;  »</a>
		        <ul>
		            <li><a href="/BlockAccount/AddBlock.aspx">Add Block</a></li>
			        <li><a href="/BlockAccount/SearchBlock.aspx">Search Block</a></li>
		        </ul>
		    </li>
		    
            <li><a href="/help/">Help</a></li>
	    </ul>	   
    </div>
</div>
