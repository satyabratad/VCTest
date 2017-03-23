// JavaScript source code
var bill2payPaymentConfirm = {
showCart: function(){
	debugger;
	$("#clientName").html(dbObject.CustomerName);
	var param=getParameterByName('dbObject');
	DeSrializeDbObject(param);
	$("#cartCount").html((dbObject.Products.length==0?"":dbObject.Products.length));
	bill2payPaymentConfirm.populateSubtotal();
	bill2payPaymentConfirm.populateGrid();
	bill2payPaymentConfirm.populateContactInfo();
	bill2payPaymentConfirm.populateBillingDetails();
},
redirectToPaymentSuccess: function(){
	if(validateForm()){
		debugger;
		var confirmMail=$('#txtEmailAddress').val();
	dbObject.ConfirmEmail=confirmMail;
	var json=SerializeDbObject();		
    redirect('PaymentSuccess.html?ShowCart=Y&dbObject=' + json);
	}		
},
redirectToCartGrid: function(){
	var json=SerializeDbObject();		
    redirect('Home.html?ShowCart=Y&dbObject=' + json);	
},
addMoreItems: function(){
	debugger;
	var json=SerializeDbObject();		
    redirect('Home.html?dbObject=' + json);	
},
populateSubtotal: function(){
	var total=bill2payProducts.getCartTotalAmount();
	$("#paysubTotalAmount").html(total);
},
editContactInfo: function(){
	var json=SerializeDbObject();	
	redirect('ContactInfo.html?dbObject=' + json);		
},
editPaymentDetails: function(){
	var json=SerializeDbObject();	
	redirect('Payment.html?dbObject=' + json);		
},
populateContactInfo: function(){
	debugger;
	var cinfoName='';
	var cinfoAddress='';
	if(dbObject.ContactInfo.ContactName.length>0)
	 cinfoName=dbObject.ContactInfo.ContactName;
	if(dbObject.ContactInfo.Address1.length>0)
	 cinfoAddress+=dbObject.ContactInfo.Address1+",";
	 if(dbObject.ContactInfo.Address2.length>0)
	 cinfoAddress+=dbObject.ContactInfo.Address2+",";
	 if(dbObject.ContactInfo.City.length>0)
	 cinfoAddress+=dbObject.ContactInfo.City+",";
	 	 if(dbObject.ContactInfo.State.length>0)
	 cinfoAddress+=dbObject.ContactInfo.State+" ";
	 	 if(dbObject.ContactInfo.Zip.length>0)
	 cinfoAddress+=dbObject.ContactInfo.Zip;
	 
	 $("#cinfoName").html(cinfoName);
	 $("#cinfoAddress").html(cinfoAddress);	
},
populateBillingDetails: function(){
	debugger;
	if(dbObject.BillingDetails!=null){
		var paymentMethod="Visa ***"+dbObject.BillingDetails.CardNumber.substr(dbObject.BillingDetails.CardNumber.length-4,4);
		$("#paymentMethod").html(paymentMethod);
		$("#expDate").html(dbObject.BillingDetails.ExpDate.replace('_','/'));
	 	$("#billZip").html(dbObject.BillingDetails.Zip);	
	} 
},
populateGrid: function () {
		debugger;
		$("#cartGrid").css("display","block");
		var html = '';
		var totalAmount=0;
		var cellClass='';
		 html += '<table class="table" style="width: 100%;">';
		 html += '<thead>';
         html += '<tr>';
          html += '<td class="table-header" width="5%"></td>';
         html += '<td class="table-header" width="30%">Item</td>';
         html += '<td class="table-header" width="55%">Details</td>';
         html += '<td class="table-header" width="10%">Amount</td>';
         html += '</tr>';
         html += '</thead>';
         html += '<tbody>';
        
	    for (var i = 0; i < dbObject.Products.length; i++) {
	       
	       cellClass=(i%2==0?"table-row":"table-alternateRow");
	       
	        html += '<tr>';
	        var row=dbObject.Products[i];
	       
	        //Product Info
	        var details='';
	        if(row.ACC1!=null){
				details+=getValueFromJson(row.ACC1)+", ";
			}
			 if(row.ACC2!=null){
				details+=getValueFromJson(row.ACC2)+", ";
			}
			 if(row.ACC3!=null){
				details+=getValueFromJson(row.ACC3)+", ";
			}
			//Property Address
			var propAddr='<br/><strong>Property Address:</strong><br/>';
			if(row.Address1.length>0){
				propAddr+=row.Address1+", ";
			}
			if(row.Address2.length>0){
				propAddr+=row.Address2+", ";
			}
			if(row.City.length>0){
				propAddr+=row.City+", ";
			}
			if(row.State.length>0){
				propAddr+=row.State+" ";
			}
			if(row.Zip.length>0){
				propAddr+=row.Zip;
			}
			if(propAddr!=null){
				details=details.substring(0,details.length - 2);
				details+=propAddr;
			}
			
			//Amount
			var amount=row.Amount;
			
			//prepare rows
			
			html += '<td class='+cellClass+'>'+(i+1) 
			
			html+= '</td>';
			
			 html += '<td class='+cellClass+'>' + row.ProductName + '</td>';
			 html += '<td class='+cellClass+'>' + details + '</td>';
			 html += '<td class='+cellClass+' align="right">$' + parseFloat(amount).toFixed(2) + '</td>';
						
	        html += '</tr>';
	        totalAmount=(parseFloat(totalAmount)+parseFloat(amount)).toFixed(2);
	    }
	    
	    //subtotal
	     html += '<tr>';
	     html += ' <td class="table-row-bold" colspan="3" align="right">Subtotal (' + products.length + ' items) </td>';
		 html += '<td class="table-row-bold" align="right">$' + totalAmount + '</td>';
		 html += '</tr>';
		 //Conv fee
		   html += '<tr>';
           html += '<td class="table-alternateRow" colspan="3" align="right">';
           html += '<span>Convenience Fee </span>';
           html += '<a href="#" data-toggle="modal" data-target="#feeInfoModal"  data-keyboard="false" title="Fee Info" tabindex="-1">';
           html +='<i class="fa fa-question-circle fa-1" aria-hidden="true"></i>';
           html += '<span class="text-hide">Fee Information</span>';
		   html += '</a>';
           html += '<span>:</span>';
           html += '</td>';
           html += '<td class="table-alternateRow" align="right">';
		   html += '$6.00';
           html += '</td>';
           html += '</tr>';
           html += '<tr>';
           html += '<td class="table-row-bold" colspan="3" align="right">';
           html += '<span>Total Amount:</span>';
           html += '</div>';
           html += '</td>';
           html += '<td class="table-row-bold" align="right">';
           html += '$' + parseFloat(parseFloat(totalAmount)+6).toFixed(2);
           html += '</td>';
           html += '</tr>';
		  	
	    html += '</tbody>';
	    html += '</table>';
	    //append created html to the table body:
	   	bill2payAccountDetails.clear();
	   	$('#cartGrid').html('');
	    $('#cartGrid').append(html);
	   
    },

};