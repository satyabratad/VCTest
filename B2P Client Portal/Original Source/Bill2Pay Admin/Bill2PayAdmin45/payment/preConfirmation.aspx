<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="preConfirmation.aspx.vb" Inherits="Bill2PayAdmin45.preConfirmation" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript">
        function openWindow()
        {
            window.open('/Payment/Confirmation.aspx', '_blank');
            
        }
    </script>
</head>
<body onload="openWindow();">
    <form id="form1" runat="server">
    <div>
    
    </div>
    </form>
</body>
</html>
