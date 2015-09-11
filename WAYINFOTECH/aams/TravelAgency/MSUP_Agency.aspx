<%@ Page Language="VB" AutoEventWireup="false" CodeFile="MSUP_Agency.aspx.vb" Inherits="TravelAgency_MSUP_Agency" EnableEventValidation="false" %>

<%@ Register Src="../UserControl/MenuControl.ascx" TagName="MenuControl" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>AAMS: Agency</title>
    <link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" media="all" href="../Calender/calendar-blue.css"
        title="win2k-cold-1" />
    <link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />
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
<script language="javascript" type="text/javascript">
function PopupAgencyGroup()
{
  var type;
  type = "../Popup/PUSR_AgencyGroup.aspx" 
  var strReturn;   
  if (window.showModalDialog)
  {
      strReturn=window.showModalDialog(type,null,'dialogWidth:880px;dialogHeight:600px;help:no;');       
  }
  else
  {
      strReturn=window.open(type,null,'height=600,width=880,top=30,left=20,scrollbars=1');       
  }	   
  if (strReturn != null)
  {
      var sPos = strReturn.split('|'); 
      
      document.getElementById('<%=hdChainId.ClientID%>').value=sPos[0];
      document.getElementById('<%=txtAgencyGroup.ClientID%>').value=sPos[1];
  }
}
function PopupEmployee()
{
  var type;
  type = "../Popup/PUSR_Employee.aspx" 
  var strReturn;   
  if (window.showModalDialog)
  {
      strReturn=window.showModalDialog(type,null,'dialogWidth:880px;dialogHeight:600px;help:no;');       
  }
  else
  {
      strReturn=window.open(type,null,'height=600,width=880,top=30,left=20,scrollbars=1');       
  }	     
  if (strReturn != null)
  {
      var sPos = strReturn.split('|'); 
      
      document.getElementById('<%=hdRespId.ClientID%>').value=sPos[0];
      document.getElementById('<%=txtAResponsibility.ClientID%>').value=sPos[1];
  }
}
//function PopupAgencyGroupOfficeID()
//{
//  var type;
//   //alert("Popup Called");
//  var city=document.getElementById('<%=hdCity.ClientId%>').value;
//  //alert(city);
// // type = "../Popup/PUSR_OfficeId.aspx"
//  type = "../Popup/PUSR_OfficeId.aspx?Action=U|"+city; 
// //alert(type);
//  var strReturn;   
//  if (window.showModalDialog)
//  {     
//      strReturn=window.showModalDialog(type,null,'dialogWidth:880px;dialogHeight:600px;help:no;');       
//   }
//  else
//  {     
//     strReturn=window.open(type,null,'height=600,width=880,top=30,left=20,scrollbars=1');       
//  }	   
//  if (strReturn != null)
//  {
//     var sPos = strReturn; 
//     document.getElementById('<%=txtAoffice.ClientID%>').value=strReturn;
//   }
//}

</script>

<body onload="PageLoadMethod();">
    <form id="form1" runat="server">
        <table width="860px" align="left" style="height: 486px" class="border_rightred">
            <tr>
                <td valign="top">
                    <table width="100%">
                        <tr>
                            <td valign="top">
                                <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td valign="top" align="left">
                                            <span class="menu">Setup-></span><span class="sub_menu">Agency</span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td bgcolor="#1A61A9" class="heading" align="center">
                                            Manage Agency
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td valign="top">
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td valign="top">
                                            <uc1:MenuControl ID="MenuControl1" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" class="redborder">
                                            <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td class="textbold" align="center" valign="TOP" rowspan="0">
                                                        <asp:Label ID="lblError" class="ErrorMsg" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="100%" valign="top">
                                                        <asp:Panel ID="pnlEmployee" runat="server" Width="100%">
                                                            <table width="100%" border="0" align="left" cellpadding="2" cellspacing="0">
                                                                <tr>
                                                                    <td class="textbold">
                                                                        &nbsp;</td>
                                                                    <td class="textbold">
                                                                        Agency Group<span class="Mandatory">*</span></td>
                                                                    <td colspan="3">
                                                                        <asp:TextBox ID="txtAgencyGroup" MaxLength="40" Width="468px" TabIndex="1" CssClass="textboxgrey" ReadOnly="true"
                                                                            runat="server"></asp:TextBox>
                                                                        <img src="../Images/lookup.gif" alt="Select & Add Agency Group." onclick="javascript:return PopupAgencyGroup();" />
                                                                        <asp:HiddenField ID="hdChainId" runat="server" />
                                                                        <asp:HiddenField ID="hdCity" runat="server" />
                                                                    </td>
                                                                    <td>
                                                                        <asp:Button ID="btnSave" runat="server" TabIndex="20" CssClass="button" Text="Save" Width="125px" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="textbold">
                                                                    </td>
                                                                    <td class="textbold">
                                                                        Name <span class="Mandatory">*</span></td>
                                                                    <td colspan="3">
                                                                        <asp:TextBox ID="txtName" runat="server" CssClass="textbox" MaxLength="40" TabIndex="1"
                                                                            Width="468px"></asp:TextBox></td>
                                                                    <td>
                                                                        <asp:Button ID="btnNew" runat="server" TabIndex="21" CssClass="button" Text="New" Width="125px" /></td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="textbold">
                                                                    </td>
                                                                    <td class="textbold">
                                                                        Address1 <span class="Mandatory">*</span></td>
                                                                    <td colspan="3">
                                                                        <asp:TextBox ID="txtAddress1" runat="server" CssClass="textbox" MaxLength="40" TabIndex="1"
                                                                            Width="468px"></asp:TextBox></td>
                                                                    <td>
                                                                        <asp:Button ID="btnReset" runat="server" TabIndex="22" CssClass="button" Text="Reset" Width="125px" /></td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="textbold">
                                                                    </td>
                                                                    <td class="textbold">
                                                                        Address2<span class="Mandatory"></span></td>
                                                                    <td colspan="3">
                                                                        <asp:TextBox ID="txtAddress2" runat="server" CssClass="textbox" MaxLength="40" TabIndex="1"
                                                                            Width="468px"></asp:TextBox></td>
                                                                    <td><asp:Button ID="btnHistory" runat="server" TabIndex="22" CssClass="button" Text="History" Width="125px" /></td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="textbold" style="width: 5%">
                                                                    </td>
                                                                    <td class="textbold" style="width: 19%">
                                                                        City <span class="Mandatory">*</span></td>
                                                                    <td style="width: 20%">
                                                                        <asp:DropDownList ID="drpCity" runat="server" TabIndex="4" Width="137px" CssClass="textbold">
                                                                        </asp:DropDownList></td>
                                                                    <td class="textbold" style="width: 20%">
                                                                        Pin Code</td>
                                                                    <td style="width: 20%">
                                                                        <asp:TextBox ID="txtPinCode" runat="server" CssClass="textbox" MaxLength="30" TabIndex="5"></asp:TextBox></td>
                                                                    <td style="width: 23%"><asp:Button ID="btnMiscViewDoc" runat="server" TabIndex="22" CssClass="button" Text="Misc.View Doc" Width="125px" /></td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="textbold">
                                                                    </td>
                                                                    <td class="textbold">
                                                                        Country</td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtCountry" MaxLength="30" TabIndex="5" CssClass="textbox"
                                                                            runat="server"></asp:TextBox></td>
                                                                    <td class="textbold">
                                                                        Office Name</td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtOfficeName" runat="server" CssClass="textbox" MaxLength="30" TabIndex="5"></asp:TextBox></td>
                                                                    <td><asp:Button ID="txtPtypeChallan" runat="server" TabIndex="22" CssClass="button" Text="PType Challan" Width="125px" /></td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="textbold">
                                                                    </td>
                                                                    <td class="textbold">
                                                                        Email</td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtEmail" CssClass="textbox" MaxLength="100" TabIndex="7" runat="server"></asp:TextBox></td>
                                                                    <td class="textbold">
                                                                        Status</td>
                                                                    <td>
                                                                        <asp:DropDownList ID="drpStatus" runat="server" TabIndex="4" Width="137px" CssClass="textbold">
                                                                        </asp:DropDownList></td>
                                                                    <td><asp:Button ID="btnAProductivity" runat="server" TabIndex="22" CssClass="button" Text="1 A Productivity" Width="125px" /></td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="textbold">
                                                                    </td>
                                                                    <td class="textbold">
                                                                        Phone</td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtPhone" MaxLength="20" CssClass="textbox" TabIndex="9" runat="server"></asp:TextBox></td>
                                                                    <td class="textbold">
                                                                        IATA ID</td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtIataId" runat="server" CssClass="textbox" MaxLength="30" TabIndex="5"></asp:TextBox></td>
                                                                    <td><asp:Button ID="btnAllCrsProductivity" runat="server" TabIndex="22" CssClass="button" Text="All CRS Productivity" Width="125px" /></td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="textbold">
                                                                        &nbsp;</td>
                                                                    <td class="textbold">
                                                                        Fax</td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtFax" MaxLength="15" CssClass="textbox" TabIndex="11" runat="server"></asp:TextBox></td>
                                                                    <td class="textbold">
                                                                        Web Site</td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtWevSite" runat="server" CssClass="textbox" MaxLength="30" TabIndex="5"></asp:TextBox></td>
                                                                    <td><asp:Button ID="btnDailyBooking" runat="server" TabIndex="22" CssClass="button" Text="Daily Booking" Width="125px" /></td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="textbold">
                                                                    </td>
                                                                    <td class="subheading" colspan="4">
                                                                        Amadeus Specific</td>
                                                                    <td><asp:Button ID="btnGroupCase" runat="server" TabIndex="22" CssClass="button" Text="Group Case" Width="125px" /></td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="textbold">
                                                                    </td>
                                                                    <td class="textbold">
                                                                        1Aoffice
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtAoffice" MaxLength="15" TabIndex="13"   CssClass="textbox" runat="server"></asp:TextBox>
                                                                        <img src="../Images/lookup.gif" alt="Select & Add Agency Group." onclick="javascript:return PopupAgencyGroupOfficeID;" /></td>
                                                                    <td class="textbold">
                                                                        A Responsibility</td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtAResponsibility" runat="server" CssClass="textboxgrey" ReadOnly="true" MaxLength="15" TabIndex="13" ></asp:TextBox>
                                                                        <img src="../Images/lookup.gif" alt="Select & Add Agency Group." onclick="javascript:return PopupEmployee();" />
                                                                        <asp:HiddenField ID="hdRespId" runat="server" /></td>
                                                                    <td><asp:Button ID="btnOrderFeasability" runat="server" TabIndex="22" CssClass="button" Text="Order Feasability" Width="125px" /></td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="textbold">
                                                                    </td>
                                                                    <td class="textbold">
                                                                        Type</td>
                                                                    <td>
                                                                        <asp:DropDownList ID="drpType" runat="server" TabIndex="4" Width="137px" CssClass="textbold">
                                                                        </asp:DropDownList></td>
                                                                    <td class="textbold">
                                                                        Priority</td>
                                                                    <td>
                                                                        <asp:DropDownList ID="drpPriority" runat="server" TabIndex="4" Width="137px" CssClass="textbold">
                                                                        </asp:DropDownList></td>
                                                                    <td></td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="textbold">
                                                                    </td>
                                                                    <td class="textbold">
                                                                        Date Online</td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtDateOnline" runat="server" CssClass="textbox" MaxLength="15"
                                                                            TabIndex="11"></asp:TextBox>
                                                                        <img id="imgDateOnline" alt="" src="../Images/calender.gif" title="Date selector"
                                                                            style="cursor: pointer" />

                                                                        <script type="text/javascript">
                                                                                                    Calendar.setup({
                                                                                                    inputField     :    '<%=txtDateOnline.clientId%>',
                                                                                                    ifFormat       :    "%d/%m/%Y",
                                                                                                    button         :    "imgDateOnline",
                                                                                                    //align          :    "Tl",
                                                                                                    singleClick    :    true
                                                                                                    });
                                                                        </script>

                                                                    </td>
                                                                    <td class="textbold">
                                                                        Date Offline</td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtDateOffline" runat="server" CssClass="textbox" MaxLength="15"
                                                                            TabIndex="11"></asp:TextBox>
                                                                        <img id="imgDateOffline" alt="" src="../Images/calender.gif" title="Date selector"
                                                                            style="cursor: pointer" />

                                                                        <script type="text/javascript">
                                                                                                    Calendar.setup({
                                                                                                    inputField     :    '<%=txtDateOffline.clientId%>',
                                                                                                    ifFormat       :    "%d/%m/%Y",
                                                                                                    button         :    "imgDateOffline",
                                                                                                    //align          :    "Tl",
                                                                                                    singleClick    :    true
                                                                                                    });
                                                                        </script>

                                                                    </td>
                                                                    <td>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="textbold">
                                                                    </td>
                                                                    <td class="textbold">
                                                                        Include in CC roster</td>
                                                                    <td>
                                                                        <asp:CheckBox ID="rdCCRoster" runat="server" /></td>
                                                                    <td class="textbold">
                                                                        File Number</td>
                                                                    <td><asp:DropDownList ID="drpFileNumber" runat="server" TabIndex="4" Width="137px" CssClass="textbold">
                                                                    </asp:DropDownList></td>
                                                                    <td>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="textbold">
                                                                    </td>
                                                                    <td class="textbold">
                                                                        Reason</td>
                                                                    <td colspan="3">
                                                                        <asp:TextBox ID="txtReason" runat="server" CssClass="textbox" MaxLength="15" TabIndex="13" Width="468px"></asp:TextBox></td>
                                                                    <td>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="textbold">
                                                                    </td>
                                                                    <td class="subheading" colspan="4">
                                                                        Connectivity</td>
                                                                    <td><asp:Button ID="btnConnectivityHistory" runat="server" TabIndex="22" CssClass="button" Text="Connectivity History" Width="125px" /></td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="textbold">
                                                                    </td>
                                                                    <td class="textbold">
                                                                        Connectivity</td>
                                                                    <td class="textbold">
                                                                        Online Status</td>
                                                                    <td class="textbold">
                                                                        Installation Date</td>
                                                                    <td class="textbold">
                                                                        Order Number</td>
                                                                    <td>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="textbold">
                                                                    </td>
                                                                    <td class="textbold">
                                                                        Primary</td>
                                                                    <td>
                                                                        <asp:DropDownList ID="drpPrimaryOnlineStatus" runat="server" TabIndex="4" Width="137px" CssClass="textbold">
                                                                        </asp:DropDownList></td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtPrimaryDate" runat="server" CssClass="textbox" MaxLength="15"
                                                                            TabIndex="11"></asp:TextBox>
                                                                        <img id="imgPrimaryDate" alt="" src="../Images/calender.gif" title="Date selector"
                                                                            style="cursor: pointer" />

                                                                        <script type="text/javascript">
                                                                                                    Calendar.setup({
                                                                                                    inputField     :    '<%=txtPrimaryDate.clientId%>',
                                                                                                    ifFormat       :    "%d/%m/%Y",
                                                                                                    button         :    "imgPrimaryDate",
                                                                                                    //align          :    "Tl",
                                                                                                    singleClick    :    true
                                                                                                    });
                                                                        </script>

                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtPrimaryOrderNumber" runat="server" CssClass="textbox" MaxLength="15" TabIndex="11"> </asp:TextBox>
                                                                    </td>
                                                                    <td>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="textbold">
                                                                    </td>
                                                                    <td class="textbold">
                                                                        Backup</td>
                                                                    <td>
                                                                        <asp:DropDownList ID="drpBackupOnlineStatus" runat="server" TabIndex="4" Width="137px" CssClass="textbold">
                                                                        </asp:DropDownList></td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtBackupDate" runat="server" CssClass="textbox" MaxLength="15"
                                                                            TabIndex="11"></asp:TextBox>
                                                                        <img id="imgBackupDate" alt="" src="../Images/calender.gif" tabindex="16" title="Date selector"
                                                                            style="cursor: pointer" />

                                                                        <script type="text/javascript">
                                                                                                    Calendar.setup({
                                                                                                    inputField     :    '<%=txtBackupDate.clientId%>',
                                                                                                    ifFormat       :    "%d/%m/%Y",
                                                                                                    button         :    "imgBackupDate",
                                                                                                    //align          :    "Tl",
                                                                                                    singleClick    :    true
                                                                                                    });
                                                                        </script>

                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtBackupOrderNumber" runat="server" CssClass="textbox" MaxLength="15" TabIndex="11"></asp:TextBox></td>
                                                                    <td>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="textbold">
                                                                        &nbsp;</td>
                                                                    <td colspan="4" class="ErrorMsg">
                                                                        Field Marked * are Mandatory</td>
                                                                    <td>
                                                                        &nbsp;</td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
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
    </form>
</body>
</html>
