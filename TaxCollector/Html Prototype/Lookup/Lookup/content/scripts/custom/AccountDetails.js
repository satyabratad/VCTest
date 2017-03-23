
var bill2payAccountDetails = {

	submit: function () {
		debugger;
       
		bill2payProducts.addProducts();
		bill2payAccountDetails.updateCartHeader();		
		bill2payAccountDetails.populateGrid();			
       //window.location.href = 'file:///D:\Tasks\Bill2Pay\Bill2Pay%20Html\HomeLookup.html?products=' + JSON.stringify(products)
    },
    showCart: function(){
		debugger;
        $("#clientName").html(dbObject.CustomerName);
		var param=getParameterByName('dbObject');
		DeSrializeDbObject(param);
		$("#cartCount").html(dbObject.Products.length);	
		//show cart grid
		param=getParameterByName('ShowCart');		
		if(param=='Y')
		{
			bill2payAccountDetails.showgridFromCart();
		}
	},
    showgridFromCart: function(){
    	var x = document.getElementById("divAccountDetails1");
                        if (x != null) {
                            x.style.display = "none";
                        }
                        var y = document.getElementById("divAccountDetails2");
                        if (y != null) {
                            y.style.display = "block";
                        }
		bill2payAccountDetails.updateCartHeader();		
		bill2payAccountDetails.populateGrid();		
	},
    updateCartHeader: function(){
		//Cart header population
		$("#cartProduct").html($("#ddlCategories").val());
		$("#cartHeadingCount").html(dbObject.Products.length);
		$("#cartHeadingAmount").html(bill2payProducts.getCartTotalAmount);	
		
		$("#cartCount").html((dbObject.Products.length==0?"":dbObject.Products.length));	
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
          html += '<td class="table-header" width="5%"></td>';
         html += '<td class="table-header" width="40%">Item</td>';
         html += '<td class="table-header" width="40%">Details</td>';
         html += '<td class="table-header" width="10%">Amount</td>';
         html += '</tr>';
         html += '</thead>';
         html += '<tbody>';
        //bill2payAccountDetails.clearItemsFromCart(dbObject.Products.length-1);
        
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
			if(row.Amount!=null){
                if(isNaN(parseFloat(row.Amount))){
				amount=getValueFromJson(row.Amount);
                }
                else
                amount=row.Amount;
			}
			//prepare rows
			
			html += '<td class='+cellClass+'>' 
			html+= '<a onclick="removeItems('+i+');" >';
            html+= '<span class="glyphicon glyphicon-trash"></span>';
            html+='</a>';
			html+= '</td>';

            html += '<td class='+cellClass+'>' 
			html+= '<a onclick="editItems('+i+');" >';
            html+= '<span class="glyphicon glyphicon-edit"></span>';
            html+='</a>';
			html+= '</td>';
			
			 html += '<td class='+cellClass+'>' + row.ProductName.replace('_','& ') + '</td>';
			 html += '<td class='+cellClass+'>' + details + '</td>';
			 html += '<td class='+cellClass+' align="right">$' + parseFloat(amount).toFixed(2) + '</td>';
						
	        html += '</tr>';
	        totalAmount=(parseFloat(totalAmount)+parseFloat(amount)).toFixed(2);
	    }
	    
	    //subtotal
	     html += '<tr>';
	     html += ' <td class="table-row-bold" colspan="4" align="right">Subtotal (' + products.length + ' items) </td>';
		 html += '<td class="table-row-bold" align="right">$' + totalAmount + '</td>';
		 html += '</tr>';
		  	
	    html += '</tbody>';
	    html += '</table>';
	    //append created html to the table body:
	   	bill2payAccountDetails.clear();
	   	$('#cartGrid').html('');
	    $('#cartGrid').append(html);
	   
    },
   
    removeItemsFromCart: function () {
    	debugger;
    	var itemIndex=$('#selectedIndex').val();
		bill2payProducts.removeProduct(itemIndex);
		bill2payAccountDetails.updateCartHeader();		
		bill2payAccountDetails.populateGrid();
		
		if(dbObject.Products.length==0){
			$("#cartGrid").css("display","none");
			$("#tdDisplayCartMsg").html("No item added to cart");
		}	
    },
     clearItemsFromCart: function (itemIndex) {
    	debugger;
    	
		bill2payProducts.removeProduct(itemIndex);
		bill2payAccountDetails.updateCartHeader();		
		bill2payAccountDetails.populateGrid();
		
		if(dbObject.Products.length==0){
			$("#cartGrid").css("display","none");
			$("#tdDisplayCartMsg").html("No item added to cart");
		}	
    },
    clear: function () {
		$("#txtLookupAccount1").val('');
		$("#txtLookupAccount2").val('');
		$("#txtLookupAccount3").val('');
		$("#txtPropAddress1").val('');
		$("#txtPropAddress2").val('');
		$("#txtPropCity").val('');
		$("#ddlPropState").val('');
		$("#txtPropZip").val('');							
		$("#txtPropAmount").val('');
		 $('#cartGrid').html('');
    },
   submitToContactDetails: function () {
   debugger;    
		var json=SerializeDbObject();
        json=json.replace("&","_");	
		redirect('ContactInfoLookup.html?dbObject=' + json);	
      },
};