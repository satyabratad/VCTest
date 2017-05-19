<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="DateTextBoxOurStyles.aspx.vb" Inherits="Bill2PayAdmin45.DateTextBoxOurStyles" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    Date Text Box Our Style
    <des:DateTextBox ID="txtStart" runat="server" CssClass="inputText" AutoHint="false">
                            <PopupCalendar ToggleImageUrl="/images/Calendar.jpg">
                            <Calendar CssClass="" BackColor="#F5F6EE" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" 
                                                              Font-Names="Verdana" Font-Size="X-Small" BackImageUrl="/images/bg_form.png"
                                                              JumpBackButtonImageUrl="{APPEARANCE}/Date And Time/LeftCmd2SolidGray.GIF" 
                                                              JumpForwardButtonImageUrl="{APPEARANCE}/Date And Time/RightCmd2SolidGray.GIF" 
                                                              NextMonthButtonImageUrl="{APPEARANCE}/Date And Time/RightCmdSolidGray.GIF" 
                                                              PrevMonthButtonImageUrl="{APPEARANCE}/Date And Time/LeftCmdSolidGray.GIF">
                            <PopupMonthYearPicker>
                            <MonthYearPicker PrevYearsButtonImageUrl="{APPEARANCE}/Date And Time/LeftCmdSolidGray.GIF" NextYearsButtonImageUrl="{APPEARANCE}/Date And Time/RightCmdSolidGray.GIF"></MonthYearPicker>
                            </PopupMonthYearPicker>
                            </Calendar>
                            </PopupCalendar>
                          </des:DateTextBox>
    </div>
    </form>
</body>
</html>
