<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="CartGrid.ascx.vb" Inherits=".CartGridascx" %>
<div id="cartGrid" style="display: block;">
    <table class="table" style="width: 100%;">
        <thead>
            <tr>
                <td class="table-header" width="5%"></td>
                <td class="table-header" width="30%">Item</td>
                <td class="table-header" width="55%">Details</td>
                <td class="table-header" width="10%" align="right">Amount</td>
            </tr>
        </thead>
        <tbody>
            <tr>
                <td class="table-row"><a onclick="removeItems(0);"><span style="color: #000000; cursor: pointer;" class="glyphicon glyphicon-trash"></span></a></td>
                <td class="table-row">Tax Bill</td>
                <td class="table-row">ss, 123, 11<br>
                    <strong>Property Address:</strong><br>
                </td>
                <td class="table-row" align="right">$111.00</td>
            </tr>
            <tr>
                <td class="table-row-bold" colspan="3" align="right">Subtotal (1 item(s)): </td>
                <td class="table-row-bold" align="right">$111.00</td>
            </tr>
        </tbody>
    </table>
</div>
