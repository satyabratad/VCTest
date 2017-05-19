<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="report.aspx.vb" Inherits="Bill2PayAdmin45.report" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Report</title>

    <%-- Hides the excel option in the export dropdown list --%>
    <script type="text/javascript">

        window.onload = function () {

            var formatDropDown = document.getElementById('ReportViewer1_ctl05_ctl04_ctl00_Menu');

            var formats = formatDropDown.childNodes;

            if (formatDropDown != null) {
                formatDropDown.removeChild(formats[3]);
            }
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <asp:ToolkitScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="true" />
        <div>
           <rsweb:reportviewer 
               ID="ReportViewer1" 
               runat="server" 
               ShowCredentialPrompts="False" 
               ProcessingMode="Remote" 
               AsyncRendering="False" 
               ShowPrintButton="False" 
               PageCountMode="Estimate"  
               Width="100%" 
               Height="100%"
               SizeToReportContent="True" 
               ShowParameterPrompts="False" 
               ShowPromptAreaButton="False" 
               ShowRefreshButton="False" 
               ShowPageNavigationControls="True" 
               ShowZoomControl="false"
               ShowDocumentMapButton="False" 
               ShowBackButton="True">
           </rsweb:reportviewer>                       
        </div>
    </form>
</body>
</html>
