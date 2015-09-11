<%@ Page Language="VB" AutoEventWireup="false" CodeFile="INCUP_Pay_PaymentReceived.aspx.vb"
    Inherits="Incentive_INCUP_Pay_PaymentReceived" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>ISP Payment Received</title>
    <link href="../CSS/AAMS.css" type="text/css" rel="stylesheet" />

    <script src="../JavaScript/AAMS.js" type="text/javascript"></script>

    <link rel="stylesheet" type="text/css" media="all" href="../Calender/calendar-blue.css"
        title="win2k-cold-1" />

    <script type="text/javascript" src="../Calender/calendar.js"></script>

    <!-- import the language module -->

    <script type="text/javascript" src="../Calender/lang/calendar-en.js"></script>

    <!-- import the calendar setup module -->

    <script type="text/javascript" src="../Calender/calendar-setup.js"></script>

    <script language="javascript" type="text/javascript">
      
        
         function PopupEmployee()
        {
                       var type;    
                                           
                     var strEmployeePageName=document.getElementById("hdEmployeePageName").value;
                    if (strEmployeePageName!="")
                    {
                        type = "../Setup/" + strEmployeePageName+ "?Popup=T" ;  
                        //type = "../Setup/MSSR_Employee.aspx?Popup=T" ;
   	                    window.open(type,"POPEmp","height=600,width=880,top=30,left=20,scrollbars=1,status=1");	
                        return false;
                    }
                            
                                     
        }
       
        
       

    
    </script>

    <style type="text/css">

   .confirmationBackground {
	background-color:#434040;
	filter:alpha(opacity=20);
	opacity:0.7;
}
.confirmationPopup 
{
	
	background-color:#ffffdd;
	border:3px solid #0457b7;
	padding:px;
	width:250px;
	background-color:#ffffff;
	border-top-width:3px;

}

.strip_bluelogin	{
	background-image:url(../Images/strip_bluelogin.jpg);
	background-repeat:repeat-x;
	height:36px;
	font-family:Verdana;
	font-size:16px;
	color:#FFFFFF;
	padding-left:10px;
}

</style>
</head>
<body onload="window.focus();">
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server" AsyncPostBackTimeout="660">
        </asp:ScriptManager>
        <table width="810px" class="border_rightred" id="tbl" runat="server">
            <tr>
                <td valign="top" style="height: 520px">
                    <table width="100%">
                        <tr>
                            <td valign="top" align="left">
                                <span class="menu">Incentive-&gt;</span><span class="sub_menu">Cheque Creation</span>
                            </td>
                        </tr>
                        <tr>
                            <td class="heading" align="center" valign="top">
                                Incentive Cheque Creation&nbsp;</td>
                        </tr>
                        <tr>
                            <td valign="top">
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td align="LEFT" class="redborder">
                                            <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td class="textbold" colspan="6" align="center" valign="middle" style="height: 22px">
                                                        <asp:Label ID="lblError" runat="server" CssClass="ErrorMsg" EnableViewState="False"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="height: 2px;" class="textbold" colspan="6" align="center" valign="TOP">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="textbold" style="height: 22px; width: 7%">
                                                    </td>
                                                    <td class="subheading" colspan="3">
                                                        Group Details</td>
                                                    <td style="height: 22px; width: 7%">
                                                    </td>
                                                    <td style="height: 22px; width: 20%">
                                                        </td>
                                                </tr>
                                                <tr>
                                                    <td align="center" class="textbold" colspan="6" style="height: 2px" valign="top">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="textbold" style="height: 22px; width: 7%">
                                                        &nbsp;</td>
                                                    <td class="textbold" style="height: 22px; width: 7%">
                                                    </td>
                                                    <td class="textbold" style="height: 22px; width: 27%">
                                                        Payment &nbsp;No.</td>
                                                    <td style="height: 22px; width: 27%">
                                                        <asp:TextBox ID="txtPANO" runat="server" CssClass="textboxgrey" Width="200px" TabIndex="1"
                                                             ReadOnly="True"></asp:TextBox>
                                                    </td>
                                                    <td style="height: 22px; width: 7%">
                                                    </td>
                                                    <td style="height: 22px; width: 20%">
                                                        <asp:Button ID="btnSave" runat="server" CssClass="button" TabIndex="24" Text="Save"
                                                            AccessKey="S" /></td>
                                                </tr>
                                                <tr>
                                                    <td style="height: 2px;" class="textbold" colspan="6" align="center" valign="TOP">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="textbold" style="height: 22px; width: 7%">
                                                        &nbsp;</td>
                                                    <td class="textbold" style="height: 22px; width: 7%">
                                                    </td>
                                                    <td class="textbold" style="height: 22px; width: 27%">
                                                        Chain Code</td>
                                                    <td style="height: 22px; width: 27%">
                                                        <asp:TextBox ID="txtChainCode" runat="server" CssClass="textboxgrey" Width="200px" TabIndex="2"
                                                            MaxLength="3" ReadOnly="True"></asp:TextBox>
                                                    </td>
                                                    <td style="height: 22px; width: 7%">
                                                        </td>
                                                    <td style="height: 22px; width: 20%">
                                                        <asp:Button ID="btnReset" runat="server" AccessKey="R" CssClass="button" TabIndex="25"
                                                            Text="Reset" /></td>
                                                </tr>
                                                <tr>
                                                    <td style="height: 2px;" class="textbold" colspan="6" align="center" valign="TOP">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="textbold" style="height: 22px; width: 7%">
                                                        &nbsp;</td>
                                                    <td class="textbold" style="height: 22px; width: 7%">
                                                    </td>
                                                    <td class="textbold" style="height: 22px; width: 27%">
                                                        Chain Name</td>
                                                    <td style="height: 22px; width: 27%">
                                                        <asp:TextBox ID="txtChainName" runat="server" CssClass="textboxgrey" Width="200px" TabIndex="3"
                                                            MaxLength="3" ReadOnly="True"></asp:TextBox>
                                                    </td>
                                                    <td style="height: 22px; width: 7%">
                                                    </td>
                                                    <td style="height: 22px; width: 20%">
                                                        <asp:Button ID="BtnReject" runat="server" AccessKey="R" CssClass="button" TabIndex="27" 
                                                            Text="Reject" OnClientClick="return CallReject();" /></td>
                                                </tr>
                                                <tr>
                                                    <td style="height: 2px;" class="textbold" colspan="6" align="center" valign="TOP">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="textbold" style="width: 7%; height: 22px">
                                                    </td>
                                                    <td class="textbold" style="width: 7%; height: 22px">
                                                    </td>
                                                    <td class="textbold" style="width: 27%; height: 22px">
                                                        Address</td>
                                                    <td style="width: 27%; height: 22px">
                                                        <asp:TextBox ID="txtChainAddress" runat="server" CssClass="textboxgrey"
                                                            Width="200px" ReadOnly="True" Height="50px" Rows="5" TextMode="MultiLine" TabIndex="4"></asp:TextBox>
                                                    </td>
                                                    <td style="width: 7%; height: 22px">
                                                    </td>
                                                    <td style="width: 20%; height: 22px" valign="bottom">
                                                        <input type="button" id="btnPaymentDetails" runat="server" tabindex="1" class="button"
                                                            value="Process Payment" style="width: 155px" onclick="javascript:return ShowPaymentDetails();"
                                                            accesskey="P" /></td>
                                                </tr>
                                                <tr> 
                                                    <td style="height: 2px;" class="textbold" colspan="6" align="center" valign="TOP">
                                                    </td>
                                                </tr>
                                                
                                                    <tr>
                                                    <td class="textbold" style="height: 22px; width: 7%">
                                                        &nbsp;</td>
                                                    <td class="textbold" style="height: 22px; width: 7%">
                                                    </td>
                                                    <td class="textbold" style="height: 22px; width: 27%">
                                                        PAN &nbsp;No.</td>
                                                    <td style="height: 22px; width: 27%">
                                                        <asp:TextBox ID="txtPanNo" runat="server" CssClass="textboxgrey" Width="200px" TabIndex="2"
                                                            MaxLength="3" ReadOnly="True"></asp:TextBox>
                                                    </td>
                                                    <td style="height: 22px; width: 7%">
                                                        </td>
                                                    <td style="height: 22px; width: 20%"><asp:Button ID="BtnPaymentSheetReport" runat="server" AccessKey="B" CssClass="button"
                                                                                                 Visible ="true"     TabIndex="2" Text="Payment Detail Sheet" Width="155px"  />
                                                     </td>
                                                </tr>
                                               <tr> 
                                                    <td style="height: 2px;" class="textbold" colspan="6" align="center" valign="TOP">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="textbold" style="height: 22px; width: 7%">
                                                    </td>
                                                    <td class="subheading" colspan="3">
                                                        Quarter Payment&nbsp;<asp:Label ID="LblPLB" runat ="server" Text ="" ></asp:Label></td>
                                                    <td style="height: 22px; width: 7%">
                                                    </td>
                                                    <td style="height: 22px; width: 20%">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="height: 2px;" class="textbold" colspan="6" align="center" valign="TOP">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="textbold" style="height: 22px; width: 7%">
                                                        &nbsp;</td>
                                                    <td class="textbold" style="height: 22px; width: 7%">
                                                    </td>
                                                    <td class="textbold" style="height: 22px; width: 27%">
                                                        Bank Name<span class="Mandatory">*</span></td>
                                                    <td style="height: 22px; width: 27%">
                                                        <asp:TextBox ID="txtBankName" runat="server" CssClass="textbox" Width="200px" TabIndex="5"
                                                            MaxLength="100"></asp:TextBox></td>
                                                    <td style="height: 22px; width: 7%">
                                                    </td>
                                                    <td style="height: 22px; width: 20%">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="height: 2px;" class="textbold" colspan="6" align="center" valign="TOP">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="textbold" style="height: 22px; width: 7%">
                                                        &nbsp;</td>
                                                    <td class="textbold" style="height: 22px; width: 7%">
                                                    </td>
                                                    <td class="textbold" style="height: 22px; width: 27%">
                                                        Cheque No. <span class="Mandatory">*</span></td>
                                                    <td style="height: 22px; width: 27%">
                                                        <asp:TextBox ID="txtChqNo" runat="server" CssClass="textbox" Width="200px" TabIndex="6"
                                                            MaxLength="50"></asp:TextBox></td>
                                                    <td style="height: 22px; width: 7%">
                                                    </td>
                                                    <td style="height: 22px; width: 20%">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="height: 2px;" class="textbold" colspan="6" align="center" valign="TOP">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="textbold" style="height: 22px; width: 7%">
                                                        &nbsp;</td>
                                                    <td class="textbold" style="height: 22px; width: 7%">
                                                    </td>
                                                    <td class="textbold" style="height: 22px; width: 27%">
                                                        Cheque Date <span class="Mandatory">*</span>
                                                    </td>
                                                    <td style="height: 22px; width: 28%">
                                                        <asp:TextBox ID="txtChqDate" runat="server" CssClass="textbox" Width="200px" TabIndex="7"
                                                            MaxLength="10"></asp:TextBox>&nbsp;
                                                        <img id="imgChqDate" alt="" src="../Images/calender.gif" title="Date selector" style="cursor: pointer"
                                                            runat="server" />

                                                        <script type="text/javascript">
                                                                                                                                                Calendar.setup({
                                                                                                                                                inputField     :    '<%=txtChqDate.clientId%>',
                                                                                                                                                ifFormat       :    "%d/%m/%Y",
                                                                                                                                                button         :    "imgChqDate",
                                                                                                                                                //align          :    "Tl",
                                                                                                                                                singleClick    :    true
                                                                                                                                                });
                                                        </script>

                                                    </td>
                                                    <td style="height: 22px; width: 7%">
                                                    </td>
                                                    <td style="height: 22px; width: 20%">
                                                        <asp:Button ID="BtnConfirm" Text="" runat="server" CssClass="displayNone" /></td>
                                                </tr>
                                                <tr>
                                                    <td style="height: 2px;" class="textbold" colspan="6" align="center" valign="TOP">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="textbold" style="height: 22px; width: 7%">
                                                        &nbsp;</td>
                                                    <td class="textbold" style="height: 22px; width: 7%">
                                                    </td>
                                                    <td class="textbold" style="height: 22px; width: 27%">
                                                        Tds Amount
                                                    </td>
                                                    <td style="height: 22px; width: 27%">
                                                        <asp:TextBox ID="TxtTDSAmount" runat="server" CssClass="textbox right" Width="200px"
                                                            TabIndex="8" MaxLength="12" ReadOnly="false"></asp:TextBox>
                                                    </td>
                                                    <td style="height: 22px; width: 7%">
                                                    </td>
                                                    <td style="height: 22px; width: 20%">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="height: 2px;" class="textbold" colspan="6" align="center" valign="TOP">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="textbold" style="height: 22px; width: 7%">
                                                        &nbsp;</td>
                                                    <td class="textbold" style="height: 22px; width: 7%">
                                                    </td>
                                                    <td class="textbold" style="height: 22px; width: 27%">
                                                        Amount&nbsp;
                                                    </td>
                                                    <td style="height: 22px; width: 27%">
                                                        <asp:TextBox ID="txtAmt" runat="server" CssClass="textboxgrey right" Width="200px"
                                                            TabIndex="9" MaxLength="12" ReadOnly="True"></asp:TextBox>
                                                    </td>
                                                    <td style="height: 22px; width: 7%">
                                                    </td>
                                                    <td style="height: 22px; width: 20%">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="height: 2px;" class="textbold" colspan="6" align="center" valign="TOP">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="textbold" style="height: 22px; width: 7%">
                                                        &nbsp;</td>
                                                    <td class="textbold" style="height: 22px; width: 7%">
                                                    </td>
                                                    <td class="textbold" style="height: 22px; width: 27%">
                                                        Cheque Delivered To<%-- <span class ="Mandatory">*</span>--%>
                                                    </td>
                                                    <td style="height: 22px; width: 27%">
                                                        <asp:TextBox ID="txtEmployeeName" runat="server" CssClass="textboxgrey" MaxLength="100"
                                                            ReadOnly="True" TabIndex="10" Width="200px"></asp:TextBox>
                                                        &nbsp;&nbsp;<img tabindex="26" id="img1A" src="../Images/lookup.gif" onclick="javascript:return PopupEmployee();"
                                                        alt=""    runat="server" style="cursor: pointer;" /></td>
                                                    <td style="height: 22px; width: 7%" valign="top">
                                                        &nbsp;
                                                    </td>
                                                    <td style="height: 22px; width: 20%">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="height: 2px;" class="textbold" colspan="6" align="center" valign="TOP">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="textbold" style="height: 22px; width: 7%">
                                                        &nbsp;</td>
                                                    <td class="textbold" style="height: 22px; width: 7%">
                                                    </td>
                                                    <td class="textbold" style="height: 22px; width: 27%">
                                                        Cheque Delivered Date <span class="Mandatory">*</span>
                                                    </td>
                                                    <td style="height: 22px; width: 27%">
                                                        <asp:TextBox ID="txtChqDelDate" runat="server" CssClass="textbox" Width="200px" TabIndex="11"
                                                            MaxLength="10"></asp:TextBox>&nbsp;
                                                        <img id="imgDelChqDate" alt="" src="../Images/calender.gif" title="Date selector"
                                                            style="cursor: pointer" runat="server" />

                                                        <script type="text/javascript">
                                                                                                                                                Calendar.setup({
                                                                                                                                                inputField     :    '<%=txtChqDelDate.clientId%>',
                                                                                                                                                ifFormat       :    "%d/%m/%Y",
                                                                                                                                                button         :    "imgDelChqDate",
                                                                                                                                                //align          :    "Tl",
                                                                                                                                                singleClick    :    true
                                                                                                                                                });
                                                        </script>

                                                    </td>
                                                    <td style="height: 22px; width: 7%">
                                                    </td>
                                                    <td style="height: 22px; width: 20%">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="height: 2px;" class="textbold" colspan="6" align="center" valign="TOP">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="textbold" style="height: 22px; width: 7%">
                                                        &nbsp;</td>
                                                    <td class="textbold" style="height: 22px; width: 7%">
                                                    </td>
                                                    <td class="textbold" style="height: 22px; width: 27%" valign="top">
                                                        Cheque Received Agency&nbsp;</td>
                                                    <td colspan="2" style="height: 22px; width: 35%" valign="top">
                                                        <asp:TextBox ID="txtAgency" runat="server" CssClass="textbox" MaxLength="100" TabIndex="12"
                                                            Width="200px"></asp:TextBox></td>
                                                    <td style="height: 22px; width: 20%">
                                                        &nbsp;&nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="height: 2px;" class="textbold" colspan="6" align="center" valign="TOP">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="textbold" style="height: 22px; width: 7%">
                                                        &nbsp;</td>
                                                    <td class="textbold" style="height: 22px; width: 7%">
                                                    </td>
                                                    <td class="textbold" style="height: 22px; width: 27%">
                                                        Cheque Received Agency Date</td>
                                                    <td style="height: 22px; width: 27%">
                                                        <asp:TextBox ID="txtChqRecAgenceyDate" runat="server" CssClass="textbox" Width="200px"
                                                            TabIndex="13" MaxLength="10"></asp:TextBox>&nbsp;
                                                        <img id="imgRecAgencyDate" alt="" src="../Images/calender.gif" title="Date selector"
                                                            style="cursor: pointer" runat="server" />

                                                        <script type="text/javascript">
                                                                                                                                                Calendar.setup({
                                                                                                                                                inputField     :    '<%=txtChqRecAgenceyDate.clientId%>',
                                                                                                                                                ifFormat       :    "%d/%m/%Y",
                                                                                                                                                button         :    "imgRecAgencyDate",
                                                                                                                                                //align          :    "Tl",
                                                                                                                                                singleClick    :    true
                                                                                                                                                });
                                                        </script>

                                                    </td>
                                                    <td style="height: 22px; width: 7%">
                                                    </td>
                                                    <td style="height: 22px; width: 20%">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="height: 2px;" class="textbold" colspan="6" align="center" valign="TOP">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="6">
                                                        <asp:Panel ID="PnlAdjustment" runat="server" Width="100%">
                                                            <table style="width: 100%" cellpadding="0" cellspacing="0">
                                                                <tr>
                                                                    <td class="textbold" style="height: 22px; width: 7%">
                                                                    </td>
                                                                    <td class="subheading" colspan="3" style="height: 22px">
                                                                        Adjustment Payment</td>
                                                                    <td style="height: 22px; width: 7%">
                                                                    </td>
                                                                    <td style="height: 22px; width: 20%">
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="height: 2px;" class="textbold" colspan="6" align="center" valign="TOP">
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="textbold" style="height: 22px; width: 7%">
                                                                        &nbsp;</td>
                                                                    <td class="textbold" style="height: 22px; width: 7%">
                                                                    </td>
                                                                    <td class="textbold" style="height: 22px; width: 27%">
                                                                        Bank Name<span class="Mandatory">*</span></td>
                                                                    <td style="height: 22px; width: 27%">
                                                                        <asp:TextBox ID="txtAdjBankName" runat="server" CssClass="textbox" Width="200px"
                                                                            TabIndex="14" MaxLength="100"></asp:TextBox></td>
                                                                    <td style="height: 22px; width: 7%">
                                                                    </td>
                                                                    <td style="height: 22px; width: 20%">
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="height: 2px;" class="textbold" colspan="6" align="center" valign="TOP">
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="textbold" style="height: 22px; width: 7%">
                                                                        &nbsp;</td>
                                                                    <td class="textbold" style="height: 22px; width: 7%">
                                                                    </td>
                                                                    <td class="textbold" style="height: 22px; width: 27%">
                                                                        Cheque No. <span class="Mandatory">*</span></td>
                                                                    <td style="height: 22px; width: 27%">
                                                                        <asp:TextBox ID="txtAdjChqNo" runat="server" CssClass="textbox" Width="200px" TabIndex="15"
                                                                            MaxLength="50"></asp:TextBox></td>
                                                                    <td style="height: 22px; width: 7%">
                                                                    </td>
                                                                    <td style="height: 22px; width: 20%">
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="height: 2px;" class="textbold" colspan="6" align="center" valign="TOP">
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="textbold" style="height: 22px; width: 7%">
                                                                        &nbsp;</td>
                                                                    <td class="textbold" style="height: 22px; width: 7%">
                                                                    </td>
                                                                    <td class="textbold" style="height: 22px; width: 27%">
                                                                        Cheque Date <span class="Mandatory">*</span>
                                                                    </td>
                                                                    <td style="height: 22px; width: 27%">
                                                                        <asp:TextBox ID="txtAdjChqDate" runat="server" CssClass="textbox" Width="200px" TabIndex="1"
                                                                            MaxLength="16"></asp:TextBox>&nbsp;
                                                                        <img id="imgAdjChqDate" alt="" src="../Images/calender.gif" title="Date selector"
                                                                            style="cursor: pointer" runat="server" />

                                                                        <script type="text/javascript">
                                                                                                                                                Calendar.setup({
                                                                                                                                                inputField     :    '<%=txtAdjChqDate.clientId%>',
                                                                                                                                                ifFormat       :    "%d/%m/%Y",
                                                                                                                                                button         :    "imgAdjChqDate",
                                                                                                                                                //align          :    "Tl",
                                                                                                                                                singleClick    :    true
                                                                                                                                                });
                                                                        </script>

                                                                    </td>
                                                                    <td style="height: 22px; width: 7%">
                                                                    </td>
                                                                    <td style="height: 22px; width: 20%">
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="height: 2px;" class="textbold" colspan="6" align="center" valign="TOP">
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="textbold" style="height: 22px; width: 7%">
                                                                        &nbsp;</td>
                                                                    <td class="textbold" style="height: 22px; width: 7%">
                                                                    </td>
                                                                    <td class="textbold" style="height: 22px; width: 27%">
                                                                        Tds Amount
                                                                    </td>
                                                                    <td style="height: 22px; width: 27%">
                                                                        <asp:TextBox ID="TxtAdjTDSAmount" runat="server" CssClass="textbox right" Width="200px"
                                                                            TabIndex="1" MaxLength="17" ReadOnly="false"></asp:TextBox>
                                                                    </td>
                                                                    <td style="height: 22px; width: 7%">
                                                                    </td>
                                                                    <td style="height: 22px; width: 20%">
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="height: 2px;" class="textbold" colspan="6" align="center" valign="TOP">
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="textbold" style="height: 22px; width: 7%">
                                                                        &nbsp;</td>
                                                                    <td class="textbold" style="height: 22px; width: 7%">
                                                                    </td>
                                                                    <td class="textbold" style="height: 22px; width: 27%">
                                                                        Amount&nbsp;
                                                                    </td>
                                                                    <td style="height: 22px; width: 27%">
                                                                        <asp:TextBox ID="txtAdjAmt" runat="server" CssClass="textboxgrey right" Width="200px"
                                                                            TabIndex="1" MaxLength="18" ReadOnly="True"></asp:TextBox>
                                                                    </td>
                                                                    <td style="height: 22px; width: 7%">
                                                                    </td>
                                                                    <td style="height: 22px; width: 20%">
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="height: 2px;" class="textbold" colspan="6" align="center" valign="TOP">
                                                                    </td>
                                                                </tr>
                                                                <tr id="TrChqDelTo" runat="server" visible="false">
                                                                    <td class="textbold" style="height: 22px; width: 7%">
                                                                        &nbsp;</td>
                                                                    <td class="textbold" style="height: 22px; width: 7%">
                                                                    </td>
                                                                    <td class="textbold" style="height: 22px; width: 27%">
                                                                        Cheque Delivered To<%-- <span class ="Mandatory">*</span>--%>
                                                                    </td>
                                                                    <td style="height: 22px; width: 27%">
                                                                        <asp:TextBox ID="txtAdjEmployeeName" runat="server" CssClass="textboxgrey" MaxLength="19"
                                                                            ReadOnly="True" TabIndex="1" Width="200px"></asp:TextBox>
                                                                        &nbsp;&nbsp;<img tabindex="26" id="img2" src="../Images/lookup.gif" onclick="javascript:return PopupEmployee();"
                                                                            runat="server" style="cursor: pointer;" /></td>
                                                                    <td style="height: 22px; width: 7%" valign="top">
                                                                        &nbsp;
                                                                    </td>
                                                                    <td style="height: 22px; width: 20%">
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="height: 2px;" class="textbold" colspan="6" align="center" valign="TOP">
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="textbold" style="height: 22px; width: 7%">
                                                                        &nbsp;</td>
                                                                    <td class="textbold" style="height: 22px; width: 7%">
                                                                    </td>
                                                                    <td class="textbold" style="height: 22px; width: 27%">
                                                                        Cheque Delivered Date <span class="Mandatory">*</span>
                                                                    </td>
                                                                    <td style="height: 22px; width: 27%">
                                                                        <asp:TextBox ID="txtAdjChqDelDate" runat="server" CssClass="textbox" Width="200px"
                                                                            TabIndex="1" MaxLength="20"></asp:TextBox>&nbsp;
                                                                        <img id="imgAdjDelChqDate" alt="" src="../Images/calender.gif" title="Date selector"
                                                                            style="cursor: pointer" runat="server" />

                                                                        <script type="text/javascript">
                                                                                                                                                Calendar.setup({
                                                                                                                                                inputField     :    '<%=txtAdjChqDelDate.clientId%>',
                                                                                                                                                ifFormat       :    "%d/%m/%Y",
                                                                                                                                                button         :    "imgAdjDelChqDate",
                                                                                                                                                //align          :    "Tl",
                                                                                                                                                singleClick    :    true
                                                                                                                                                });
                                                                        </script>

                                                                    </td>
                                                                    <td style="height: 22px; width: 7%">
                                                                    </td>
                                                                    <td style="height: 22px; width: 20%">
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="height: 2px;" class="textbold" colspan="6" align="center" valign="TOP">
                                                                    </td>
                                                                </tr>
                                                                <tr id="TrChqRecAgency" runat="server" visible="false">
                                                                    <td class="textbold" style="height: 22px; width: 7%">
                                                                        &nbsp;</td>
                                                                    <td class="textbold" style="height: 22px; width: 7%">
                                                                    </td>
                                                                    <td class="textbold" style="height: 22px; width: 27%" valign="top">
                                                                        Cheque Received Agency&nbsp;</td>
                                                                    <td colspan="2" style="height: 22px; width: 35%" valign="top">
                                                                        <asp:TextBox ID="txtAdjAgency" runat="server" CssClass="textbox" MaxLength="21"
                                                                            TabIndex="1" Width="200px"></asp:TextBox></td>
                                                                    <td style="height: 22px; width: 20%">
                                                                        &nbsp;&nbsp;
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="height: 2px;" class="textbold" colspan="6" align="center" valign="TOP">
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="textbold" style="height: 22px; width: 7%">
                                                                        &nbsp;</td>
                                                                    <td class="textbold" style="height: 22px; width: 7%">
                                                                    </td>
                                                                    <td class="textbold" style="height: 22px; width: 27%">
                                                                        Cheque Received Agency Date</td>
                                                                    <td style="height: 22px; width: 27%">
                                                                        <asp:TextBox ID="txtAdjChqRecAgenceyDate" runat="server" CssClass="textbox" Width="200px"
                                                                            TabIndex="1" MaxLength="22"></asp:TextBox>&nbsp;
                                                                        <img id="imgAdjRecAgencyDate" alt="" src="../Images/calender.gif" title="Date selector"
                                                                            style="cursor: pointer" runat="server" />

                                                                        <script type="text/javascript">
                                                                                                                                                Calendar.setup({
                                                                                                                                                inputField     :    '<%=txtAdjChqRecAgenceyDate.clientId%>',
                                                                                                                                                ifFormat       :    "%d/%m/%Y",
                                                                                                                                                button         :    "imgAdjRecAgencyDate",
                                                                                                                                                //align          :    "Tl",
                                                                                                                                                singleClick    :    true
                                                                                                                                                });
                                                                        </script>

                                                                    </td>
                                                                    <td style="height: 22px; width: 7%">
                                                                    </td>
                                                                    <td style="height: 22px; width: 20%">
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="height: 2px;" class="textbold" colspan="6" align="center" valign="TOP">
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                            </table>
                                                        </asp:Panel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="textbold" style="height: 22px; width: 7%">
                                                        &nbsp;</td>
                                                    <td style="height: 11px">
                                                    </td>
                                                    <td colspan="2" class="ErrorMsg" style="height: 11px">
                                                        Field Marked * are Mandatory</td>
                                                    <td style="height: 11px">
                                                        &nbsp;</td>
                                                    <td style="height: 11px">
                                                        &nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td colspan="6">
                                                        <table>
                                                            <tr>
                                                                <td>
                                                                    <cc1:ConfirmButtonExtender ID="cnfrmBtnExt" runat="server" DisplayModalPopupID="mdlPopUpExt"
                                                                        TargetControlID="BtnConfirm" BehaviorID="cnfrmBtnExt">
                                                                    </cc1:ConfirmButtonExtender>
                                                                </td>
                                                                <td>
                                                                    <cc1:ModalPopupExtender ID="mdlPopUpExt" runat="server" TargetControlID="BtnConfirm"
                                                                        BackgroundCssClass="confirmationBackground" PopupControlID="pnlPopup" OkControlID="BtnFakeYes"
                                                                        CancelControlID="BtnFakeNo">
                                                                    </cc1:ModalPopupExtender>
                                                                    <asp:Panel ID="pnlPopup" runat="server" Width="600px" Height="250px" Style="display: none"
                                                                        HorizontalAlign="Center" CssClass="confirmationPopup">
                                                                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                                            <tr>
                                                                                <td class="strip_bluelogin" align="left" colspan="2">
                                                                                    Amadeus agent management system </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td height="30" colspan="2">
                                                                                    <asp:Label ID="lblReasonError" runat="server" CssClass="ErrorMsg" EnableViewState="False"></asp:Label></td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td class="textbold" colspan="2">
                                                                                    <%--<b>The payment needs further approval , if click yes the process will start from the
                                                                                        first level<br />
                                                                                        Do you want to proceed?</b>--%>
                                                                                      <b>The payment needs further payment advice generaion, if click yes<br/> the process will also remove
                                                                                        from the payment approval queue.<br />
                                                                                        Do you want to proceed?</b>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td height="30" colspan="2">
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td class="textbold" valign="middle" align="center">
                                                                                    &nbsp;&nbsp;&nbsp;&nbsp;Reason</td>
                                                                                <td>
                                                                                    <asp:TextBox ID="txtReason" runat="server" Text="" TextMode="MultiLine" Width="490px"
                                                                                        Rows="4" />
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td colspan="2" align="center">
                                                                                    <table border="0" width="40%" cellspacing="10" cellpadding="1">
                                                                                        <tr>
                                                                                            <td align="center">
                                                                                                <asp:Button ID="btnYes" runat="server" CssClass="button" Text="Yes" Width="60px"
                                                                                                    OnClientClick=" return ShowMandatory();" /></td>
                                                                                            <td align="center">
                                                                                                <asp:Button ID="btnNo" CssClass="button" runat="server" Text="No" Width="60px" OnClientClick="$find('mdlPopUpExt').hide(); return false;" />
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td align="center">
                                                                                                <asp:Button ID="BtnFakeYes" runat="server" Text="" Width="60px" CssClass="displayNone" /></td>
                                                                                            <td align="center">
                                                                                                <asp:Button ID="BtnFakeNo" runat="server" Text="" Width="60px" CssClass="displayNone" />
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </asp:Panel>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                            <asp:HiddenField ID="hdEmployeeID" runat="server" />
                                            <asp:HiddenField ID="hdAdjEmployeeID" runat="server" />
                                            <asp:HiddenField ID="hdAadjustmentPayment" runat="server" Value="0" />
                                            <input type="hidden" id="hdEmployeePageName" style="width: 1px" runat='server' />
                                            <asp:HiddenField ID="hdYear" runat="server" />
                                            <asp:HiddenField ID="hdMonth" runat="server" />
                                            <asp:HiddenField ID="hdPaymentType" runat="server" />
                                            <asp:HiddenField ID="hdBCID" runat="server" />
                                            <asp:HiddenField ID="hdChainCode" runat="server" />
                                             <asp:HiddenField ID="hdPLBCYcle" runat="server" />
                                            <asp:HiddenField ID="HdPLBPeriod" runat="server" />
                                           
                                            
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

    <script language="javascript" type="text/javascript">
    function ValidateForm()
    {
 
      document.getElementById('<%=lblError.ClientId%>').innerText=''
      
         //Validating Bank Name .
         
          if(document.getElementById('<%=txtBankName.ClientId%>').value =='')
        {
            document.getElementById('<%=lblError.ClientId%>').innerHTML='Bank name is mandatory.'
            document.getElementById('<%=txtBankName.ClientId%>').focus();
            return false;
        }
        
        //Validating Cheque Number .
      
       if(document.getElementById('<%=txtChqNo.ClientId%>').value =='')
        {
            document.getElementById('<%=lblError.ClientId%>').innerHTML='Cheque no. is mandatory.'
            document.getElementById('<%=txtChqNo.ClientId%>').focus();
            return false;
        }
         
         //Validating Cheque Date .
         
         
          if(document.getElementById('txtChqDate').value == '')
        {
            document.getElementById('<%=lblError.ClientId%>').innerHTML='Cheque date is mandatory.'
            document.getElementById('<%=txtChqDate.ClientId%>').focus();
            return false;
        }  
        else
        {        
             if (isDate(document.getElementById('txtChqDate').value,"d/M/yyyy") == false)	
            {
               document.getElementById('lblError').innerHTML = "Cheque date is not valid.";			
	           document.getElementById('txtChqDate').focus();
	           return(false);  
            }
        }   
        
             var txtval;
            var txtVal=document.getElementById("TxtTDSAmount").value.trim();
       ///   alert(txtVal);
             if(IsDataValid(txtVal,5)==false)
          {
              document.getElementById("lblError").innerHTML="TDS amount is not valid.";
              document.getElementById("TxtTDSAmount").focus();
              return false;
          }        
         
         //Validating Cheque Delivered Date  .
         
          if(document.getElementById('txtChqDelDate').value == '')
        {
            document.getElementById('<%=lblError.ClientId%>').innerHTML='Cheque delivered date is mandatory.'
            document.getElementById('<%=txtChqDelDate.ClientId%>').focus();
            return false;
        }  
        else
        {        
             if (isDate(document.getElementById('txtChqDelDate').value,"d/M/yyyy") == false)	
            {
               document.getElementById('lblError').innerHTML = "Cheque delivered date is not valid.";			
	           document.getElementById('txtChqDelDate').focus();
	           return(false);  
            }
        }       
            
         
           //Validating Cheque received agency .
         
//          if(document.getElementById('<%=txtAgency.ClientId%>').value =='')
//        {
//            document.getElementById('<%=lblError.ClientId%>').innerHTML='Cheque received agency is mandatory.'
//            document.getElementById('<%=txtAgency.ClientId%>').focus();
//            return false;
//        }

      

    if(document.getElementById('<%=txtAgency.ClientId%>').value.trim() !='')
        {
        if(document.getElementById('txtChqRecAgenceyDate').value == '')
        {
            document.getElementById('<%=lblError.ClientId%>').innerHTML='Cheque received agency date is mandatory.'
            document.getElementById('<%=txtChqRecAgenceyDate.ClientId%>').focus();
            return false;
        }  
        else
        {        
             if (isDate(document.getElementById('txtChqRecAgenceyDate').value,"d/M/yyyy") == false)	
            {
               document.getElementById('lblError').innerHTML = "Cheque received agency date is not valid.";			
	           document.getElementById('txtChqRecAgenceyDate').focus();
	           return(false);  
            }
        } 
      }
      
       if(document.getElementById('txtChqRecAgenceyDate').value != '')
        {
        
            if(document.getElementById('<%=txtAgency.ClientId%>').value =='')
        {
            document.getElementById('<%=lblError.ClientId%>').innerHTML='Cheque received agency is mandatory.'
            document.getElementById('<%=txtAgency.ClientId%>').focus();
            return false;
        }
        
          
             if (isDate(document.getElementById('txtChqRecAgenceyDate').value,"d/M/yyyy") == false)	
            {
               document.getElementById('lblError').innerHTML = "Cheque received agency date is not valid.";			
	           document.getElementById('txtChqRecAgenceyDate').focus();
	           return(false);  
            }
        } 
        
        
        //In case of Validation cheque for Adjustment Payment
        
        
        if ( document.getElementById ('hdAadjustmentPayment').value=="1")
        {
        
                     if(document.getElementById('<%=txtAdjBankName.ClientId%>').value =='')
                {
                    document.getElementById('<%=lblError.ClientId%>').innerHTML='Bank name is mandatory.'
                    document.getElementById('<%=txtAdjBankName.ClientId%>').focus();
                    return false;
                }
                
                //Validating Cheque Number .
              
               if(document.getElementById('<%=txtAdjChqNo.ClientId%>').value =='')
                {
                    document.getElementById('<%=lblError.ClientId%>').innerHTML='Cheque no. is mandatory.'
                    document.getElementById('<%=txtAdjChqNo.ClientId%>').focus();
                    return false;
                }
                 
                 //Validating Cheque Date .
                 
                 
                  if(document.getElementById('txtAdjChqDate').value == '')
                {
                    document.getElementById('<%=lblError.ClientId%>').innerHTML='Cheque date is mandatory.'
                    document.getElementById('<%=txtAdjChqDate.ClientId%>').focus();
                    return false;
                }  
                else
                {        
                     if (isDate(document.getElementById('txtAdjChqDate').value,"d/M/yyyy") == false)	
                    {
                       document.getElementById('lblError').innerHTML = "Cheque date is not valid.";			
                       document.getElementById('txtAdjChqDate').focus();
                       return(false);  
                    }
                }   
                
                     var txtval;
                    var txtVal=document.getElementById("TxtAdjTDSAmount").value.trim();
               ///   alert(txtVal);
                     if(IsDataValid(txtVal,5)==false)
                  {
                      document.getElementById("lblError").innerHTML="TDS amount is not valid.";
                      document.getElementById("TxtAdjTDSAmount").focus();
                      return false;
                  }        
                 
                 //Validating Cheque Delivered Date  .
                 
                  if(document.getElementById('txtAdjChqDelDate').value == '')
                {
                    document.getElementById('<%=lblError.ClientId%>').innerHTML='Cheque delivered date is mandatory.'
                    document.getElementById('<%=txtAdjChqDelDate.ClientId%>').focus();
                    return false;
                }  
                else
                {        
                     if (isDate(document.getElementById('txtAdjChqDelDate').value,"d/M/yyyy") == false)	
                    {
                       document.getElementById('lblError').innerHTML = "Cheque delivered date is not valid.";			
                       document.getElementById('txtAdjChqDelDate').focus();
                       return(false);  
                    }
                }       
                    
              

//            if(document.getElementById('<%=txtAdjAgency.ClientId%>').value.trim() !='')
//                {
//                if(document.getElementById('txtAdjChqRecAgenceyDate').value == '')
//                {
//                    document.getElementById('<%=lblError.ClientId%>').innerHTML='Cheque received agency date is mandatory.'
//                    document.getElementById('<%=txtAdjChqRecAgenceyDate.ClientId%>').focus();
//                    return false;
//                }  
//                else
//                {        
//                     if (isDate(document.getElementById('txtAdjChqRecAgenceyDate').value,"d/M/yyyy") == false)	
//                    {
//                       document.getElementById('lblError').innerHTML = "Cheque received agency date is not valid.";			
//                       document.getElementById('txtAdjChqRecAgenceyDate').focus();
//                       return(false);  
//                    }
//                } 
//              }
              
               if(document.getElementById('txtAdjChqRecAgenceyDate').value != '')
                {
                
                    if(document.getElementById('<%=txtAgency.ClientId%>').value =='')
                {
                    document.getElementById('<%=lblError.ClientId%>').innerHTML='Cheque received agency is mandatory.'
                    document.getElementById('<%=txtAgency.ClientId%>').focus();
                    return false;
                }
                
                  
                     if (isDate(document.getElementById('txtAdjChqRecAgenceyDate').value,"d/M/yyyy") == false)	
                    {
                       document.getElementById('lblError').innerHTML = "Cheque received agency date is not valid.";			
                       document.getElementById('txtAdjChqRecAgenceyDate').focus();
                       return(false);  
                    }
                } 
                
        
        
        
        
        
        }
         
         
         //Validating Cheque Received Agency Date  .
         
           
         
      
      

       return true; 
        
    }
    function CallReject()
    {
        document.getElementById('<%=lblReasonError.ClientId%>').innerHTML="";
        document.getElementById('<%=txtReason.ClientId%>').value='';
        $get("<%=btnConfirm.ClientID %>").click();
        $get("<%=btnYes.ClientID %>").focus();
        return false;
    }
   
        function ShowMandatory()
         {
         
                if(document.getElementById('<%=txtReason.ClientId%>').value =='')
            {
                document.getElementById('<%=lblReasonError.ClientId%>').innerHTML='Please provide the reason for the rejection.'
                document.getElementById('<%=txtReason.ClientId%>').focus();
                return false;
            }
               if (document.getElementById("txtReason").value.trim().length>500)
        {
             document.getElementById("lblReasonError").innerHTML="Reason can't be greater than 500 characters."
             document.getElementById("txtReason").focus();
             return false;
        }  
            
              document.getElementById('<%=lblReasonError.ClientId%>').innerHTML="";
             return true;
         }

    
  
     function  ShowPaymentDetails()
          {
            var ObjPayId="";
            ObjPayId= document.getElementById('<%=txtPANO.ClientId%>').value;
            var ObjPayTime="";
            ObjPayTime= document.getElementById('hdPaymentType').value;
            var objBCID="";
            objBCID= document.getElementById('hdBCID').value;
            var objChainCode="";
            objChainCode= document.getElementById('hdChainCode').value;
            var objMonth="";
            objMonth= document.getElementById('hdMonth').value;
            var objYear="";
            objYear= document.getElementById('hdYear').value;
            var ObjPLB =""
            ObjPLB= document.getElementById('hdPLBCYcle').value;
            // alert(ObjPLB)
             if( ObjPLB== "True" ||  ObjPLB== "TRUE")
             {
               ObjPLB="1";
             }  
               else
               {
                 ObjPLB="0";
               }
            
            var objPeriod =""
            objPeriod= document.getElementById('HdPLBPeriod').value;
           
             if  (ObjPayId!="" &&  objBCID!=""  &&  objChainCode!="" )
              {            
                var Param="BCaseID=" + objBCID + "&Chain_Code=" + objChainCode  + "&PayId=" + ObjPayId  + "&PayTime=" + ObjPayTime  + "&OnlyShow=T"  +"&Month=" + objMonth + "&Year=" + objYear  +  "&PLB=" + ObjPLB + "&Period=" + objPeriod  ;
                var type;
              
                if (ObjPayTime=='U' )
                {
                   type= "INCUP_PaymentProcessUpfront.aspx?" + Param;           
                }
                else
                {
                  type= "INCUP_PaymentProcess.aspx?" + Param;
                }            
                window.open(type,"IncPay","height=630,width=900,top=30,left=20,scrollbars=1,status=1,resizable=1");	        
              
                return false;
             }
          }    
          
            function  PaymentSheetReport()
          {
          ///alert("Abhi");
         // {debugger;}
            var ObjPayId="";
            ObjPayId= document.getElementById('<%=txtPANO.ClientId%>').value;
            var ObjPayTime="";
            ObjPayTime= document.getElementById('hdPaymentType').value;
            var objBCID="";
            objBCID= document.getElementById('hdBCID').value;
            var objChainCode="";
            objChainCode= document.getElementById('hdChainCode').value;
            var objMonth="";
            objMonth= document.getElementById('hdMonth').value;
            var objYear="";
            objYear= document.getElementById('hdYear').value;
            var objCurPayNo ="";
            var ObjPLB ="";
            ObjPLB= document.getElementById('hdPLBCYcle').value;           
            var objPeriod =""
            objPeriod= document.getElementById('HdPLBPeriod').value.split("-");
            var ObjPeriodF ="";
            var ObjPeriodT ="";
            if(objPeriod.length==2)
            {
            ObjPeriodF=objPeriod[0];
            ObjPeriodT=objPeriod[1];
            }
            
             if  (ObjPayId!="" &&  objBCID!=""  &&  objChainCode!="" )
              {            
                var Param="BCaseID=" + objBCID + "&Chain_Code=" + objChainCode  +"&Month=" + objMonth + "&Year=" + objYear   +  "&PayType=" + ObjPayTime  + "&CurPayNo=" + objCurPayNo +   "&PayId=" + ObjPayId +   "&PeriodFrom=" + ObjPeriodF +   "&PeriodTo=" + ObjPeriodT +   "&PLB=" + ObjPLB +  "&Case=PaymentSheet" ;
               // alert(Param);
                var type;
              
                if (ObjPayTime=='U' )
                {
                   type= "../RPSR_ReportShow.aspx?" + Param;            
                }
                else
                {
                  type= "../RPSR_ReportShow.aspx?" + Param;           
                }  
                          
                window.open(type,"IncPayRec","height=630,width=900,top=30,left=20,scrollbars=1,status=1,resizable=1");	        
                           
             }
               return false;
          }        
      
  
    </script>

</body>
</html>
