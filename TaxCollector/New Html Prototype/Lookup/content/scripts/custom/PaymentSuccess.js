// JavaScript source code
var bill2payPaymentSuccess = {
showCart: function(){
	debugger;
	$("#clientName").html(dbObject.CustomerName);
	var param=getParameterByName('dbObject');
	DeSrializeDbObject(param);
	bill2payPaymentSuccess.populateGrid();
    $('#headerCustName').html('<h3>'+dbObject.CustomerName+'</h3>');
	$('#headerCustName').html(dbObject.CustomerName);
	if(dbObject.BillingDetails.PaymentType=='CC')
		{
			var paymentMethod="Visa ***"+dbObject.BillingDetails.CardNumber.substr(dbObject.BillingDetails.CardNumber.length-4,4);
			$("#paymentMethod").html(paymentMethod);

		}
		else{

			var item=dbObject.BillingDetails.NameonBank+"***"+dbObject.BillingDetails.BankAccountNumber.substr(dbObject.BillingDetails.BankAccountNumber.length-4,4);
			$('#paymentMethod').html(item);

		}
	$('#paymentDate').html(bill2payPaymentSuccess.getCurrentDate());
	//$('#paymentMethod').html(dbObject.CustomerName);
	$('#confirmMail').html(dbObject.ConfirmEmail);
	$('#footerCustName').html(dbObject.CustomerName);
	clearDbObject();
},
getCurrentDate: function(){
	debugger;
	var monthNames = ["January", "February", "March", "April", "May", "June",
  "July", "August", "September", "October", "November", "December"
];

var dateObj = new Date($.now());
var month = monthNames[dateObj.getMonth()]
var day = dateObj.getUTCDate();
var year = dateObj.getUTCFullYear();
var hour = dateObj.getHours();
var mins = dateObj.getMinutes();
var secs = dateObj.getSeconds();

return month+' '+day+', ' +year+' '+hour+':'+mins+':'+secs+' EST';

newdate = year + "/" + month + "/" + day;
},
redirectToHome: function(){
	var json=SerializeDbObject();
     redirect('Home.html?ShowCart=Y&dbObject=' + json);
},
redirectToNewHome: function(){
	
	redirect("Home.html");
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
editContactInfo: function(){
	var json=SerializeDbObject();
	redirect('ContactInfo.html?dbObject=' + json);
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
         html += '<td class="table-header" width="10%" align="right">Amount</td>';
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
				details+=getValueFromJson(row.ACC1)+",";
			}
			 if(row.ACC2!=null){
				details+=getValueFromJson(row.ACC2)+",";
			}
//			 if(row.ACC3!=null){
//				details+=getValueFromJson(row.ACC3)+",";
//			}
			//Property Address
			var propAddr='<br/><strong>Property Address:</strong><br/>';
            if(row.Name!=null){
				propAddr+=getValueFromJson(row.Name)+",";
			}
			if(row.Address1!=null){
				propAddr+=getValueFromJson(row.Address1)+",";
			}
//			if(row.Address2.length>0){
//				propAddr+=row.Address2+",";
//			}
//			if(row.City.length>0){
//				propAddr+=row.City+",";
//			}
//			if(row.State.length>0){
//				propAddr+=row.State+" ";
//			}
//			if(row.Zip.length>0){
//				propAddr+=row.Zip;
//			}
			if(propAddr!=null){
				details=details.substring(0,details.length - 1);
				details+=propAddr;
			}

			//Amount
			var amount=0;
			amount=bill2payAccountDetails.getItemAmount(i);

			//prepare rows

			html += '<td class='+cellClass+'>'+(i+1)

			html+= '</td>';

			 html += '<td class='+cellClass+'>' + row.ProductName.replace('_','& ') + '</td>';
			 html += '<td class='+cellClass+'>' + details + '</td>';
			 html += '<td class='+cellClass+' align="right">$' + addThousandsSeparator(parseFloat(amount).toFixed(2)) + '</td>';

	        html += '</tr>';
	        totalAmount=(parseFloat(totalAmount)+parseFloat(amount)).toFixed(2);
	    }

	    //subtotal
	     html += '<tr>';
	     html += ' <td class="table-row-bold" colspan="3" align="right">Subtotal (' + products.length + ' item(s)) </td>';
		 html += '<td class="table-row-bold" align="right">$' + addThousandsSeparator(totalAmount) + '</td>';
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
           html += '$' + addThousandsSeparator(parseFloat(parseFloat(totalAmount)+6).toFixed(2));
           html += '</td>';
           html += '</tr>';

	    html += '</tbody>';
	    html += '</table>';

	   	$('#cartGrid').html('');
	    $('#cartGrid').append(html);

    },

};