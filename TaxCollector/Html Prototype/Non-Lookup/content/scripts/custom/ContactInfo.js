// JavaScript source code
var bill2payContactInfo = {
showCart: function(){
	debugger;
	$("#clientName").html(dbObject.CustomerName);
	var param=getParameterByName('dbObject');
	DeSrializeDbObject(param);
	$("#cartCount").html((dbObject.Products.length==0?"":dbObject.Products.length));
	bill2payContactInfo.populateContactInfo();
},
 
populateContactInfo:function(){
	debugger;
	if(dbObject.ContactInfo.ContactName!=null)
	 $("#txtContactName").val(dbObject.ContactInfo.ContactName);
	if(dbObject.ContactInfo.Address1!=null)
		$("#txtAddress1").val(dbObject.ContactInfo.Address1);
	if(dbObject.ContactInfo.Address2!=null)
		$("#txtAddress2").val(dbObject.ContactInfo.Address2);
	if(dbObject.ContactInfo.City!=null)
	 	$("#txtCity").val(dbObject.ContactInfo.City);
	 	if(dbObject.ContactInfo.State!=null)
	 	$("#ddlState").val(dbObject.ContactInfo.State);
	if(dbObject.ContactInfo.Country!=null)
	 	$("#ddlCountry").val(dbObject.ContactInfo.Country);
	if(dbObject.ContactInfo.Zip!=null)
	 	$("#txtZip").val(dbObject.ContactInfo.Zip);	
	if(dbObject.ContactInfo.Phone!=null)
	 	$("#txtPhone").val(dbObject.ContactInfo.Phone);
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

    addContact: function(json=null)
     {
            debugger;
      

        contactInfo={
            "ContactName":$("#txtContactName").val(),
            "Address1":$("#txtAddress1").val(),
            "Address2":$("#txtAddress2").val(),
            "City":$("#txtCity").val(),
            "State":$("#ddlState").val(),
            "Country":$("#ddlCountry").val(),
            "Zip":$("#txtZip").val(),
            "Phone":$("#txtPhone").val()
           
        };

        UpdateDbObject();
        var json=SerializeDbObject();		
        redirect('Payment.html?dbObject=' + json);
      }

};