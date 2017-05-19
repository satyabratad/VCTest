<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="WebForm1.aspx.vb" Inherits="Bill2PayAdmin45.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
  
   
  </div>
        <div id="dvObjectHolder">  </div>
        <br /><br />
          <script lang="JavaScript" type="text/javascript">
              if (window.navigator.appName.toLowerCase().indexOf("netscape") != -1) { // set object for Netscape:
                  document.getElementById('dvObjectHolder').innerHTML = "        <applet type=\"application/x-java-applet;version=1.6\"" +
	       	        "codebase =\".\"" +
			        "archive = \"/magtek/JMTIPADLIB.jar\"" +
			        "code=\"JMTIPADLIB.class\"" +
			        "name=\"JMTIPADLIB\"" +
	   		        "scriptable=\"true\"" +
	  		        "style=\"visibility:hidden;\"" +
			        "mayscript=\"mayscript\"" +
			        "pluginspage=\"http://java.com/en/download/index.jsp\"" + ">" +
	        	        "<param name=\"cache_option\" value=\"No\">" +
			        "<param name=\"classloader_cache\" value=\"true\">" +
			        "<param name=\"dll_ver\" value=\"1.0.0\">" +
			        "<param name=\"dll_auto_update\" value=\"Yes\">" +
	                "</applet>";
              }
              else if (window.navigator.appName.toLowerCase().indexOf('internet explorer') != -1) { //set object for IE
                  document.getElementById('dvObjectHolder').innerHTML = "<object type=\"application/x-java-applet;version=1.6\"" +
	       	        "codebase =\".\"" +
			        "archive = \"/magtek/JMTIPADLIB.jar\"" +
			        "code=\"JMTIPADLIB.class\"" +
			        "name=\"JMTIPADLIB\"" +
	        	        "height=\"0\" width=\"0\" >" +
	        	        "<param name=\"mayscript\" value=\"true\">" +
	    		        "<param name=\"classloader_cache\" value=\"true\">" +
		    	        "<param name=\"cache_option\" value=\"No\">" +
		    	        "<param name=\"dll_ver\" value=\"1.0.0\">" +
		    	        "<param name=\"dll_auto_update\" value=\"Yes\">" +
	    	        " </object>"
              }

        </script>
   
    </form>
</body>
</html>
