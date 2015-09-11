<%@ Page Language="VB" AutoEventWireup="false" CodeFile="SAUP_PREV_VisitedItems_.aspx.vb"
    Inherits="Sales_SAUP_PREV_VisitedItems_" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>AAMS::Sales::Previous Visit Details</title>
    <link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/Sales.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" media="all" href="../Calender/calendar-blue.css"
        title="win2k-cold-1" />

    <script src="../JavaScript/AAMS.js" type="text/javascript"></script>

    <link href="../Calender/calendar-blue.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="../Calender/calendar.js"></script>

    <script type="text/javascript" src="../Calender/lang/calendar-en.js"></script>

    <script type="text/javascript" src="../Calender/calendar-setup.js"></script>

    <script type="text/javascript" src="../JavaScript/subModal.js"></script>

    <link rel="stylesheet" type="text/css" href="../JavaScript/style.css" />
    <link rel="stylesheet" type="text/css" href="../JavaScript/subModal.css" />
<script type="text/javascript" src="date-picker/js/datepicker.js"></script>
        <link href="date-picker/css/demo.css"       rel="stylesheet" type="text/css" />
        <link href="date-picker/css/datepicker.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript">
    function fnCloseForm()
    {
        window.close();
        return false;
    }
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table width="860px" class="border_rightred left">
                <tr>
                    <td class="top">
                        <table width="100%">
                            <tr>
                                <td class="top left" style="width: 80%">
                                    <span class="menu">Sales-> DSR -> </span><span class="sub_menu">Previous visit items
                                        / Previous visit open items</span>
                                </td>
                                <td class="right" style="width: 20%">
                                    <asp:LinkButton ID="lnkClose" CssClass="LinkButtons" runat="server" OnClientClick="return fnCloseForm()">Close</asp:LinkButton>
                                    &nbsp; &nbsp;&nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td class="heading center" colspan="2" style="width: 100%">
                                    Previous visit items / Previous visit open items</td>
                            </tr>
                            <tr>
                                <td colspan="2" style="width: 100%">
                                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                        <tr>
                                            <td class="redborder top" colspan="2" style="width: 100%">
                                                <table width="100%" border="0" cellpadding="2" cellspacing="2">
                                                    <tr>
                                                        <td class="center TOP">
                                                            <asp:Label ID="lblError" CssClass="ErrorMsg" runat="server" EnableViewState="False"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <table width="100%" border="0" cellpadding="2" cellspacing="2" class="tabVisitDetails">
                                                                <tr>
                                                                    <td style="width: 12%;">
                                                                    <input type="text" class="w8em format-d-m-y divider-dash highlight-days-12" id="txt1"  />
                                                                   
                                                                    </td>
                                                                    <td style="width: 17%;">
                                                                    </td>
                                                                    <td style="width: 12%;">
                                                                    </td>
                                                                    <td style="width: 17%;">
                                                                    </td>
                                                                    <td style="width: 12%;">
                                                                    </td>
                                                                    <td style="width: 17%;">
                                                                    </td>
                                                                    <td style="width: 6%;">
                                                                    </td>
                                                                    <td style="width: 6%;">
                                                                    <input type="text" class="w8em format-d-m-y" id="Text1"  />
                                                                    </td>
                                                                </tr>
                                                                <tr class="top">
                                                                    <td>
                                                                        Status<span class="Mandatory">*</span>
                                                                    </td>
                                                                    <td>
                                                                        <asp:DropDownList ID="ddlServiceStatus" runat="server" CssClass="dropdownlist" Width="95%">
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                    <td>
                                                                        Assigned to
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtAssignedTo" runat="server" CssClass="textboxgrey" Width="92%"
                                                                            ReadOnly="True"></asp:TextBox></td>
                                                                    <td>
                                                                        Closer Date
                                                                    </td>
                                                                    <td colspan="2" style="width: 31%">
                                                                        <asp:TextBox ID="txtCloserDate" runat="server" CssClass="textbox" Width="80%"></asp:TextBox>
                                                                        <%--<img id="imgCloserDate" alt="" src="../Images/calender.gif" style="cursor: pointer"
                                                                            title="Date selector" />

                                                                        <script type="text/javascript">
                                                                                                    Calendar.setup({
                                                                                                    inputField     :    '<%=txtCloserDate.clientId%>',
                                                                                                    ifFormat       :    "%d/%m/%Y",
                                                                                                    button         :    "imgCloserDate",
                                                                                                    //align          :    "Tl",
                                                                                                    singleClick    :    true,
                                                                                                     showsTime      : true
                                                                                                    });                                                                                                  
                                                                        </script>--%>

                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="8" style="width: 100%;">
                                                                        <input type="hidden" runat="server" id="hdServiceCall" style="width: 1px" />
                                                                        <input type="hidden" runat="server" id="hdAssingedTo" style="width: 1px" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
