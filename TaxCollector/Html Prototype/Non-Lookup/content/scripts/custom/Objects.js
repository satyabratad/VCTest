var dbObject={
	"Products":null,
	"ContactInfo":null,
	"BillingDetails":null,
	"ConfirmEmail":null,
	"CustomerName":"Duval Country Tax Collector"
}
//Products contains 
//1. Product Details
//2. Property Address
//3. Amount
var products = [];
var contactInfo = null;
var billingDetails=null;

function UpdateDbObject(){
	dbObject.Products=products;	
	dbObject.ContactInfo=contactInfo;	
	dbObject.BillingDetails=billingDetails;	
}

function SerializeDbObject(){
	return  JSON.stringify(dbObject);
}
function DeSrializeDbObject(json){
	debugger;
	var obj=jQuery.parseJSON(json)
	dbObject.Products=obj.Products;
	dbObject.ContactInfo=obj.ContactInfo;
	dbObject.BillingDetails=obj.BillingDetails;
	
	products=obj.Products;
	contactInfo = obj.ContactInfo;
	billingDetails=obj.BillingDetails;
}
function clearDbObject(){
	dbObject.Products=null;
	dbObject.ContactInfo=null;
	dbObject.BillingDetails=null;
}
function getParameterByName( name ){
    var regexS = "[\\?&]"+name+"=([^&#]*)", 
  regex = new RegExp( regexS ),
  results = regex.exec( window.location.search );
  if( results == null ){
    return "";
  } else{
    return decodeURIComponent(results[1].replace(/\+/g, " "));
  }
}
 function getKeyFromJson(JsonObj){
		for (var key in JsonObj) {
		    return  key;
		   }
	}
function getValueFromJson(JsonObj){
	debugger;
		for (var key in JsonObj) {
		    return JsonObj[key];
		}	
	}
function redirect(url){
	var baseUrl=window.location.href.split('/').slice(0,-1).join("/");
	window.location.href=baseUrl+"/"+url;
}