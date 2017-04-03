
var bill2payAccountDetails = {

	submit: function () {

		bill2payProducts.addProducts();
		bill2payAccountDetails.updateCartHeader();
		bill2payAccountDetails.populateGrid();

       //window.location.href = 'file:///D:/Bill2Pay%20Html/Home.html?products=' + JSON.stringify(products)
    },
    redirectToSelt: function(){

	var json=SerializeDbObject();
	redirect('Home.html?dbObject=' + json);
},
    showCart: function(){


		$("#clientName").html(dbObject.CustomerName);
		try{
			var param=getParameterByName('dbObject');
			DeSrializeDbObject(param);
		}
		catch(err){

		}

		populateBreadcrumb();
		if(dbObject.Products!=null){

			$("#cartCount").html(dbObject.Products.length);
			//show cart grid
			param=getParameterByName('ShowCart');
			if(param=='Y')
			{
				bill2payAccountDetails.showgridFromCart();
			}
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
	$("#cartHeadingAmount").html(addThousandsSeparator(bill2payProducts.getCartTotalAmount()));

		$("#cartCount").html((dbObject.Products.length==0?"":dbObject.Products.length));
	},

	populateGrid: function () {

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

			html += '<td class='+cellClass+'>'
			html+= '<a onclick="removeItems('+i+');" >';
            html+= '<span style="color:#000000;cursor:pointer;" class="glyphicon glyphicon-trash"></span>';
            html+='</a>';
			html+= '</td>';

			 html += '<td class='+cellClass+'>' + row.ProductName + '</td>';
			 html += '<td class='+cellClass+'>' + details + '</td>';
			 html += '<td class='+cellClass+' align="right">$' + addThousandsSeparator(parseFloat(amount).toFixed(2)) + '</td>';

	        html += '</tr>';
	        totalAmount=(parseFloat(totalAmount)+parseFloat(amount)).toFixed(2);
	    }

	    //subtotal
	     html += '<tr>';
	     html += ' <td class="table-row-bold" colspan="3" align="right">Subtotal (' + products.length + ' item(s)): </td>';
		 html += '<td class="table-row-bold" align="right">$' + addThousandsSeparator(totalAmount) + '</td>';
		 html += '</tr>';

	    html += '</tbody>';
	    html += '</table>';
	    //append created html to the table body:
	   	bill2payAccountDetails.clear();
	   	$('#cartGrid').html('');
	    $('#cartGrid').append(html);

    },
   validateDuplicateItem: function (){

        var status = true;

        var acct1 =  $('#txtLookupAccount1').val();
        var acct2 =  $('#txtLookupAccount2').val();
        var acct3 =  $('#txtLookupAccount3').val();
        var flag = 0;

        if (dbObject.Products != null){        
            for (var i = 0; i < dbObject.Products.length; i++) {

                flag = 0;
                if (dbObject.Products[i].ProductName.toUpperCase()=="TAX BILL"){
			        if (acct1.toUpperCase() == getValueFromJson(dbObject.Products[i].ACC1).toUpperCase())
                    {
                        flag++;
                    }
                    if (acct2 == getValueFromJson(dbObject.Products[i].ACC2))
                    {
                        flag++;
                    }
                    if (acct3.toUpperCase() == getValueFromJson(dbObject.Products[i].ACC3).toUpperCase())
                    {
                        flag++;
                    }

                    if (flag==3)
                    {
                        status=false;
                        break;
                    }
                }
                else
                {
                    if (acct1.toUpperCase() == getValueFromJson(dbObject.Products[i].ACC1).toUpperCase())
                    {
                        flag++;
                    }

                    if (flag==1)
                    {
                        status=false;
                        break;
                    }
                }
		    }
        }
        return status;
   },
    removeItemsFromCart: function () {

    	var itemIndex=$('#selectedIndex').val();
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
		var json=SerializeDbObject();
		redirect('ContactInfo.html?dbObject=' + json);
      },
};