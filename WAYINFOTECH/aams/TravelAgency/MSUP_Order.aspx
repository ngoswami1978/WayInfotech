<%@ Page Language="VB" AutoEventWireup="false" CodeFile="MSUP_Order.aspx.vb" Inherits="TravelAgency_MSUP_Order"
    ValidateRequest="false" EnableEventValidation="false" %>

<%@ Register Src="../UserControl/ProductMenu.ascx" TagName="ProductMenu" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>AAMS: Agency</title>
    <link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" media="all" href="../Calender/calendar-blue.css"
        title="win2k-cold-1" />
    <link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />
</head>

<script type="text/javascript" src="../JavaScript/AAMS.js"></script>

<!-- import the calendar script -->

<script type="text/javascript" src="../Calender/calendar.js"></script>

<!-- import the language module -->

<script type="text/javascript" src="../Calender/lang/calendar-en.js"></script>

<!-- import the calendar setup module -->

<script type="text/javascript" src="../Calender/calendar-setup.js"></script>

<script type="text/javascript" language="javascript">
function MandForCompanyVertical()
	{ 
	 // debugger;
	    if ((document.getElementById ('hDCompanyVertical').value=='3') && (document.getElementById ('hDCompanyVerticalSelectByUser').value=='3' || document.getElementById ('hDCompanyVerticalSelectByUser').value=='') )
	    {
		    var	arr	=showModalDialog("CompanyVetical.aspx","","font-family:Verdana;font-size:12; dialogWidth:43;dialogHeight:14" );
		    if (arr	!= null) 
		    {		   
		         document.getElementById('hDCompanyVerticalSelectByUser').value = arr;
		    }     
		   // alert(document.getElementById('hDCompanyVertical').value);
		  //  $get("<%=BtnSave.ClientID %>").click(); 
		}    
	} 
 function ClearExpirtyDate()
 {
 {debugger;}
 var instDate=document.getElementById("txtDateMessage").value;
 if(document.getElementById("ddlOrderStatus").value=="7")
 {
 
 }
 
 
 
 }
</script>

<body>
    <form id="form1" runat="server" defaultfocus="rdlNewCancel">
        <table width="860px" align="left" style="height: 486px" class="border_rightred">
            <tr>
                <td valign="top">
                    <table width="100%">
                        <tr>
                            <td valign="top">
                                <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td valign="top" align="left">
                                            <span class="menu">Travel Agency-></span><span class="sub_menu">Order</span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td bgcolor="#1A61A9" class="heading" align="center">
                                            Manage Order
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td valign="top">
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td>
                                            <uc1:ProductMenu ID="ProductMenu1" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" class="redborder">
                                            <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td class="textbold" align="center" valign="TOP" rowspan="0">
                                                        <asp:Label ID="lblError" CssClass="ErrorMsg" runat="server" EnableViewState="False"></asp:Label></td>
                                                </tr>
                                                <tr>
                                                    <td width="100%" valign="top">
                                                        <asp:Panel ID="pnlEmployee" runat="server" Width="100%">
                                                            <table width="100%" border="0" align="left" cellpadding="2" cellspacing="0">
                                                                <tr>
                                                                    <td class="textbold">
                                                                    </td>
                                                                    <td class="textbold" style="width: 4489px">
                                                                        Name<span class="Mandatory">*</span></td>
                                                                    <td colspan="3">
                                                                        <asp:TextBox ID="txtAgencyName" TabIndex="13" CssClass="textboxgrey" runat="server"
                                                                            Width="552px" ReadOnly="True"></asp:TextBox>
                                                                        <img src="../Images/lookup.gif" alt="" onclick="javascript:return PopupAgencyPageMainOrder();"
                                                                            style="cursor: pointer;" />
                                                                        <asp:HiddenField ID="hdAgencyNameId" runat="server" />
                                                                        <input id="hdAgencyName" runat="server" style="width: 1px" type="hidden" />
                                                                        <asp:HiddenField ID="hdFileNo" runat="server" />
                                                                        <asp:HiddenField ID="hdOrderID" runat="server" />
                                                                        <asp:HiddenField ID="hdCity" runat="server" />
                                                                        <asp:HiddenField ID="hdISPIdName" runat="server" />
                                                                        <asp:HiddenField ID="hdISPId" runat="server" />
                                                                        <asp:HiddenField ID="hdNPID" runat="server" />
                                                                        <asp:HiddenField ID="hdAddress" runat="server" />
                                                                        <asp:HiddenField ID="hdOfficeID" runat="server" />
                                                                        <asp:HiddenField ID="OrderStatusDays" runat="server" />
                                                                         <asp:HiddenField id="hDCompanyVertical" runat="server"></asp:HiddenField>
                                                                                                                       <asp:HiddenField id="hDCompanyVerticalSelectByUser" runat="server"></asp:HiddenField>
                                                                    </td>
                                                                    <td style="width: 187px">
                                                                        <asp:Button ID="btnSave" runat="server" TabIndex="3" CssClass="button" Text="Save"
                                                                            OnClientClick="return OrderPage2MainOrder()" AccessKey="S" /></td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="textbold">
                                                                    </td>
                                                                    <td class="textbold" style="width: 4489px">
                                                                        Address</td>
                                                                    <td colspan="3">
                                                                        <asp:TextBox ID="txtAgencyAddress" runat="server" CssClass="textboxgrey" TabIndex="13"
                                                                            TextMode="MultiLine" Width="552px" Height="50px" ReadOnly="True" Rows="5"></asp:TextBox></td>
                                                                    <td style="width: 187px">
                                                                        <table cellpadding="0" cellspacing="0">
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:Button ID="btnNew" runat="server" TabIndex="3" CssClass="button" Text="New"
                                                                                        AccessKey="N" />
                                                                                </td>
                                                                            </tr>
                                                                            <tr height="3px">
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:Button ID="btnReset" runat="server" TabIndex="3" CssClass="button" Text="Reset"
                                                                                        AccessKey="R" />
                                                                                </td>
                                                                            </tr>
                                                                            <tr height="3px">
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <input type="button" id="btnHistory" runat="server" tabindex="3" class="button" value="History"
                                                                                        accesskey="H" />
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="textbold">
                                                                    </td>
                                                                    <td class="subheading" colspan="4">
                                                                        Order Detail</td>
                                                                    <td style="width: 187px">
                                                                        <asp:Button ID="btnViewDocument" runat="server" TabIndex="3" CssClass="button" Text="View Doc"
                                                                            AccessKey="V" /></td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="textbold" style="height: 43px">
                                                                    </td>
                                                                    <td class="textbold" style="width: 4489px; height: 43px;">
                                                                    </td>
                                                                    <td colspan="3" style="height: 43px">
                                                                        <asp:RadioButtonList ID="rdlNewCancel" runat="server" CssClass="dropdown" RepeatDirection="Horizontal"
                                                                            Width="500px" CellPadding="0" CellSpacing="0" AutoPostBack="True" TabIndex="2">
                                                                            <asp:ListItem Selected="True" Value="T">New Order</asp:ListItem>
                                                                            <asp:ListItem Value="f">Cancellation</asp:ListItem>
                                                                        </asp:RadioButtonList>
                                                                    </td>
                                                                    <td style="width: 187px; height: 43px;">
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="textbold">
                                                                    </td>
                                                                    <td class="textbold" style="width: 4489px">
                                                                        Order Number</td>
                                                                    <td style="width: 530px">
                                                                        <asp:TextBox ID="txtOrderNumber" runat="server" CssClass="textboxgrey" TabIndex="5"
                                                                            ReadOnly="True"></asp:TextBox></td>
                                                                    <td class="textbold" style="width: 22%">
                                                                        Order Status<span class="Mandatory">*</span></td>
                                                                    <td>
                                                                        <asp:DropDownList ID="ddlOrderStatus" runat="server" TabIndex="2" Width="176px" CssClass="dropdownlist">
                                                                        </asp:DropDownList></td>
                                                                    <td style="width: 187px">
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="textbold" style="height: 26px">
                                                                    </td>
                                                                    <td class="textbold" style="height: 26px; width: 4489px;">
                                                                        Order Type<span class="Mandatory">*</span></td>
                                                                    <td colspan="3" style="height: 26px">
                                                                        <asp:DropDownList ID="ddlOrderType" runat="server" TabIndex="2" Width="563px" CssClass="dropdownlist"
                                                                            AutoPostBack="True">
                                                                        </asp:DropDownList></td>
                                                                    <td style="height: 26px; width: 187px;">
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="height: 26px">
                                                                    </td>
                                                                    <td class="textbold" style="width: 4489px; height: 26px">
                                                                        ISP Name</td>
                                                                    <td style="width: 530px; height: 26px;">
                                                                        <asp:DropDownList ID="drpIspList" runat="server" CssClass="textbold" Width="136px"
                                                                            TabIndex="2" AutoPostBack="True">
                                                                        </asp:DropDownList></td>
                                                                    <td class="textbold" style="height: 26px">
                                                                        NPID</td>
                                                                    <td style="height: 26px">
                                                                        <asp:DropDownList ID="drpPlainId" TabIndex="6" onkeyup="gotop(this.id)" runat="server"
                                                                            Width="176px" CssClass="dropdownlist">
                                                                        </asp:DropDownList>
                                                                        <%--
                                                                        <asp:TextBox ID="txtPlainId" CssClass="textboxgrey" TabIndex="7" runat="server" Width="176px"></asp:TextBox>
                                                                        <img src="../Images/lookup.gif" alt="Select & Add ISP Name." onclick="javascript:return PopupISPPlanIdMainOrder();" id="IMG1"/>
                                                                        --%>
                                                                    </td>
                                                                    <td>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                    </td>
                                                                    <td class="textbold" style="width: 4489px">
                                                                        Pending With</td>
                                                                    <td style="width: 530px">
                                                                        <asp:TextBox ID="txtPendingWith" runat="server" CssClass="textbox" ReadOnly="True"
                                                                            TabIndex="2"></asp:TextBox>
                                                                        <img src="../Images/lookup.gif" alt="" onclick="javascript:return PopupPagePendingWithMainOrder();"
                                                                            style="cursor: pointer;" /></td>
                                                                    <td class="textbold">
                                                                        Processed By</td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtProcessedBy" CssClass="textboxgrey" TabIndex="9" runat="server"
                                                                            Width="173px" ReadOnly="True"></asp:TextBox>
                                                                    </td>
                                                                    <td style="width: 187px">
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                    </td>
                                                                    <td style="width: 4489px">
                                                                        <input id="hdIspNameId" runat="server" style="width: 1px" type="hidden" />
                                                                        <input id="hdPendingWithId" runat="server" style="width: 1px" type="hidden" />
                                                                        <input id="hdProcessedById" runat="server" style="width: 1px" type="hidden" />
                                                                        <input id="hdOrderTypeNew" runat="server" style="width: 1px" type="hidden" />
                                                                        <input id="hdOrderTypeCancel" runat="server" style="width: 1px" type="hidden" />
                                                                        <input id="hdOrderApproved" runat="server" style="width: 1px" type="hidden" />
                                                                    </td>
                                                                    <td style="width: 530px">
                                                                    </td>
                                                                    <td class="textbold">
                                                                    </td>
                                                                    <td>
                                                                    </td>
                                                                    <td style="width: 187px">
                                                                        <%-- <asp:Button ID="btnHistory" runat="server" TabIndex="22" CssClass="button" Text="History" />--%>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="height: 28px">
                                                                    </td>
                                                                    <td style="width: 4489px; height: 28px;">
                                                                    </td>
                                                                    <td style="width: 530px; height: 28px;">
                                                                    </td>
                                                                    <td style="height: 28px">
                                                                    </td>
                                                                    <td style="width: 872px; height: 28px;">
                                                                    </td>
                                                                    <td style="width: 187px; height: 28px;">
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                    </td>
                                                                    <td class="subheading" colspan="4">
                                                                        Dates</td>
                                                                    <td style="width: 187px">
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                    </td>
                                                                    <td class="textbold" style="width: 4489px">
                                                                        Processed</td>
                                                                    <td class="textbold" style="width: 530px">
                                                                        <asp:TextBox ID="txtDateProcessed" runat="server" CssClass="textboxgrey" TabIndex="11"
                                                                            ReadOnly="True"></asp:TextBox></td>
                                                                    <td>
                                                                    </td>
                                                                    <td>
                                                                    </td>
                                                                    <td style="width: 187px">
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                    </td>
                                                                    <td class="textbold" style="width: 4489px">
                                                                        Approval</td>
                                                                    <td style="width: 530px">
                                                                        <asp:TextBox ID="txtDateApproval" runat="server" CssClass="textbox" MaxLength="15"
                                                                            TabIndex="2"></asp:TextBox>
                                                                        <img id="imgDateApproval" alt="" src="../Images/calender.gif" title="Date selector"
                                                                            style="cursor: pointer" /></td>
                                                                    <td class="textbold">

                                                                        <script type="text/javascript">
                                                                                                    Calendar.setup({
                                                                                                    inputField     :    '<%=txtDateApproval.clientId%>',
                                                                                                    ifFormat       :    "%d/%m/%Y",
                                                                                                    button         :    "imgDateApproval",
                                                                                                    //align          :    "Tl",
                                                                                                    singleClick    :    true
                                                                                                    });
                                                                        </script>

                                                                        Applied</td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtDateApplied" runat="server" CssClass="textbox" MaxLength="15"
                                                                            TabIndex="2" Width="152px"></asp:TextBox>
                                                                        <img id="imgDateApplied" alt="" src="../Images/calender.gif" tabindex="16" title="Date selector"
                                                                            style="cursor: pointer" />

                                                                        <script type="text/javascript">
                                                                                                    Calendar.setup({
                                                                                                    inputField     :    '<%=txtDateApplied.clientId%>',
                                                                                                    ifFormat       :    "%d/%m/%Y",
                                                                                                    button         :    "imgDateApplied",
                                                                                                    //align          :    "Tl",
                                                                                                    singleClick    :    true
                                                                                                    });
                                                                        </script>

                                                                    </td>
                                                                    <td style="width: 187px">
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="height: 19px">
                                                                    </td>
                                                                    <td class="textbold" style="height: 19px; width: 4489px;">
                                                                        Message Sent</td>
                                                                    <td style="height: 19px; width: 530px;">
                                                                        <asp:TextBox ID="txtDateMessage" runat="server" CssClass="textbox" MaxLength="15"
                                                                            TabIndex="2"></asp:TextBox>
                                                                        <img id="imgDateMessage" alt="" src="../Images/calender.gif" tabindex="16" title="Date selector"
                                                                            style="cursor: pointer" />

                                                                        <script type="text/javascript">
                                                                                                    Calendar.setup({
                                                                                                    inputField     :    '<%=txtDateMessage.clientId%>',
                                                                                                    ifFormat       :    "%d/%m/%Y",
                                                                                                    button         :    "imgDateMessage",
                                                                                                    //align          :    "Tl",
                                                                                                    singleClick    :    true
                                                                                                    });
                                                                        </script>

                                                                    </td>
                                                                    <td class="textbold" style="height: 19px">
                                                                        Received<span class="Mandatory">*</span></td>
                                                                    <td style="height: 19px;">
                                                                        <asp:TextBox ID="txtDateReceived" runat="server" CssClass="textbox" MaxLength="15"
                                                                            TabIndex="2" Width="152px"></asp:TextBox>
                                                                        <img id="imgDateReceived" alt="" src="../Images/calender.gif" tabindex="16" title="Date selector"
                                                                            style="cursor: pointer" />

                                                                        <script type="text/javascript">
                                                                                                    Calendar.setup({
                                                                                                    inputField     :    '<%=txtDateReceived.clientId%>',
                                                                                                    ifFormat       :    "%d/%m/%Y",
                                                                                                    button         :    "imgDateReceived",
                                                                                                    //align          :    "Tl",
                                                                                                    singleClick    :    true
                                                                                                    });
                                                                        </script>

                                                                    </td>
                                                                    <td style="height: 19px; width: 187px;">
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                    </td>
                                                                    <td class="textbold" style="width: 4489px">
                                                                        Exp. Installation</td>
                                                                    <td style="width: 530px">
                                                                        <asp:TextBox ID="txtDateExp" runat="server" CssClass="textbox" MaxLength="15" TabIndex="2"></asp:TextBox>
                                                                        <img id="imgDateExp" alt="" src="../Images/calender.gif" tabindex="16" title="Date selector"
                                                                            style="cursor: pointer" />

                                                                        <script type="text/javascript">
                                                                                                    Calendar.setup({
                                                                                                    inputField     :    '<%=txtDateExp.clientId%>',
                                                                                                    ifFormat       :    "%d/%m/%Y",
                                                                                                    button         :    "imgDateExp",
                                                                                                    //align          :    "Tl",
                                                                                                    singleClick    :    true
                                                                                                    });
                                                                        </script>

                                                                    </td>
                                                                    <td class="textbold">
                                                                        Sent Back</td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtDateSentBack" runat="server" CssClass="textbox" MaxLength="15"
                                                                            TabIndex="2" Width="152px"></asp:TextBox>
                                                                        <img id="imgDateSentBack" alt="" src="../Images/calender.gif" tabindex="16" title="Date selector"
                                                                            style="cursor: pointer" />

                                                                        <script type="text/javascript">
                                                                                                    Calendar.setup({
                                                                                                    inputField     :    '<%=txtDateSentBack.clientId%>',
                                                                                                    ifFormat       :    "%d/%m/%Y",
                                                                                                    button         :    "imgDateSentBack",
                                                                                                    //align          :    "Tl",
                                                                                                    singleClick    :    true
                                                                                                    });
                                                                        </script>

                                                                    </td>
                                                                    <td style="width: 187px">
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                    </td>
                                                                    <td style="width: 4489px">
                                                                    </td>
                                                                    <td>
                                                                    </td>
                                                                    <td>
                                                                    </td>
                                                                    <td>
                                                                    </td>
                                                                    <td style="width: 187px">
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                    </td>
                                                                    <td class="subheading" colspan="4">
                                                                        For Marketing Department</td>
                                                                    <td style="width: 187px">
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                    </td>
                                                                    <td class="textbold" style="width: 4489px">
                                                                        Receiving Date</td>
                                                                    <td style="width: 530px">
                                                                        <asp:TextBox ID="txtDateMdReceivingMukund" runat="server" CssClass="textbox" MaxLength="20"
                                                                            TabIndex="2"></asp:TextBox>
                                                                        <img id="imgDateMdReceivingMukund" runat="server" alt="" src="../Images/calender.gif"
                                                                            title="Date selector" style="cursor: pointer" />

                                                                        <script type="text/javascript">
                                                                                                    Calendar.setup({
                                                                                                    inputField     :    '<%=txtDateMdReceivingMukund.clientId%>',
                                                                                                    ifFormat       :    "%d/%m/%Y",
                                                                                                    button         :    "imgDateMdReceivingMukund",
                                                                                                    //align          :    "Tl",
                                                                                                    singleClick    :    true
                                                                                                    });
                                                                        </script>

                                                                    </td>
                                                                    <td class="textbold">
                                                                        Resending Date</td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtDateMdResending" runat="server" CssClass="textbox" MaxLength="20"
                                                                            TabIndex="2" Width="152px"></asp:TextBox>
                                                                        <img id="imgDateMdResending" runat="server" alt="" src="../Images/calender.gif" title="Date selector"
                                                                            style="cursor: pointer" />

                                                                        <script type="text/javascript">
                                                                                                    Calendar.setup({
                                                                                                    inputField     :    '<%=txtDateMdResending.clientId%>',
                                                                                                    ifFormat       :    "%d/%m/%Y",
                                                                                                    button         :    "imgDateMdResending",
                                                                                                    //align          :    "Tl",
                                                                                                    singleClick    :    true
                                                                                                    });
                                                                        </script>

                                                                    </td>
                                                                    <td style="width: 187px">
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="height: 31px">
                                                                    </td>
                                                                    <td class="subheading" colspan="4" style="height: 31px">
                                                                        Others Details</td>
                                                                    <td style="height: 31px; width: 187px;">
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="height: 25px">
                                                                    </td>
                                                                    <td class="textbold" style="height: 25px; width: 4489px;">
                                                                        Office ID
                                                                    </td>
                                                                    <td style="height: 25px; width: 530px;">
                                                                        <asp:TextBox ID="txtOfficeID1" runat="server" CssClass="textboxgrey" MaxLength="20"
                                                                            TabIndex="9"></asp:TextBox>
                                                                        <img src="../Images/lookup.gif" alt="" onclick="javascript:return PopupAgencyGroupOfficeIDMainOrder();"
                                                                            style="cursor: pointer;" /></td>
                                                                    <td rowspan="2">
                                                                        <asp:Panel ID="pnlATID" runat="server">
                                                                            <table>
                                                                                <tr>
                                                                                    <td class="textbold" style="height: 25px; width: 4489px;">
                                                                                        ATID<span class="Mandatory">*</span></td>
                                                                                </tr>
                                                                            </table>
                                                                        </asp:Panel>
                                                                    </td>
                                                                    <td rowspan="2">
                                                                        <asp:TextBox ID="txtATID" runat="server" CssClass="textbox" Height="50px" MaxLength="20"
                                                                            TabIndex="2" TextMode="MultiLine" Width="152px"></asp:TextBox>
                                                                    </td>
                                                                    <td style="height: 25px; width: 187px;">
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="height: 25px">
                                                                    </td>
                                                                    <td class="textbold" style="width: 4489px; height: 25px">
                                                                    </td>
                                                                    <td style="width: 530px; height: 25px">
                                                                    </td>
                                                                    <td style="width: 187px; height: 25px">
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                    </td>
                                                                    <td class="textbold" style="width: 4489px">
                                                                        Receiving Office ID</td>
                                                                    <td style="width: 530px">
                                                                        <asp:TextBox ID="txtReceivingOfficeID" runat="server" CssClass="textbox" MaxLength="20"
                                                                            TabIndex="2"></asp:TextBox></td>
                                                                    <td class="textbold">
                                                                        Agency PC Req.</td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtAgencyPcReq" runat="server" CssClass="textbox" MaxLength="20"
                                                                            TabIndex="2" Width="152px"></asp:TextBox></td>
                                                                    <td style="width: 187px">
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                    </td>
                                                                    <td class="textbold" style="width: 4489px">
                                                                        Amadeus PC Req</td>
                                                                    <td style="width: 530px">
                                                                        <asp:TextBox ID="txtAmadeusPcReq" runat="server" CssClass="textbox" MaxLength="20"
                                                                            TabIndex="2"></asp:TextBox></td>
                                                                    <td class="textbold">
                                                                        Amadeus Printer Req.</td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtAmadeusPrinterReq" runat="server" CssClass="textbox" MaxLength="20"
                                                                            TabIndex="2" Width="152px"></asp:TextBox></td>
                                                                    <td style="width: 187px">
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                    </td>
                                                                    <td class="textbold" style="width: 4489px">
                                                                        Remarks</td>
                                                                    <td colspan="3">
                                                                        <asp:TextBox ID="txtremarks" runat="server" CssClass="textbox" MaxLength="20" TabIndex="2"
                                                                            Height="70px" TextMode="MultiLine" Width="544px"></asp:TextBox></td>
                                                                    <td style="width: 187px">
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="height: 30px">
                                                                    </td>
                                                                    <td style="height: 30px; width: 4489px;">
                                                                    </td>
                                                                    <td style="height: 30px; width: 530px;">
                                                                        <asp:HiddenField ID="HiddenFieldOffId" runat="server" />
                                                                    </td>
                                                                    <td class="textbold" style="height: 30px">
                                                                        <asp:HiddenField ID="hdEmpID" runat="server" />
                                                                    </td>
                                                                    <td style="height: 30px;">
                                                                        <asp:HiddenField ID="hdIspProviderID" runat="server" />
                                                                    </td>
                                                                    <td style="height: 30px; width: 187px;">
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="textbold" style="height: 11px">
                                                                        &nbsp;</td>
                                                                    <td colspan="4" class="ErrorMsg" style="height: 11px">
                                                                        Field Marked * are Mandatory</td>
                                                                    <td style="height: 11px; width: 187px;">
                                                                        <input type="hidden" id="hdEmployeePageName" style="width: 1px" runat='server' />
                                                                        &nbsp;</td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
                                                        <asp:TextBox ID="txtOfficeID2" runat="server" CssClass="textbox" MaxLength="20" TabIndex="2"
                                                            Visible="False" Width="152px"></asp:TextBox></td>
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
    </form>
</body>
</html>
