<%@ Page Language="VB" AutoEventWireup="false" CodeFile="TAUP_Agency.aspx.vb" Inherits="TravelAgency_MSUP_Agency" EnableEventValidation="false" %>

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
    <link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />
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
/*********************
for AAMS Dubai
****************8888*/
function ShowAllCRSProductivityManageAgency_dubai()
{
      var Agency=document.getElementById("txtName").value;
      var Add=document.getElementById("txtAddress1").value;
      var dropdown=document.getElementById("drpCity");
      var City=dropdown.options[dropdown.selectedIndex].text;
      var LimAoff=document.getElementById("hdLimAoff").value;
      var LimReg=document.getElementById("hdLimReg").value;
      var LimOwnOff=document.getElementById("hdLimOwnOff").value;
      var LCode=document.getElementById("hdEnLcode").value;
    //  var Aoff=document.getElementById("hdEnAoffice").value
      var Aoff=document.getElementById("txtAoffice").value;
      var Country=document.getElementById("txtCountry").value;
      var parameter="&Aoff=" + Aoff + "&LCode=" + LCode + "&Agency=" + Agency + "&Add=" + Add + "&City=" + City + "&Country=" + Country + "&LimAoff=" +  LimAoff + "&LimReg=" + LimReg + "&LimOwnOff=" + LimOwnOff;
      var type="../Productivity/PRD_BIDT_CRSDetails_dubai.aspx?Popup2=T"+parameter;

             if ( LCode!='' )
             {  
               window.open(type,"aa","height=600,width=880,top=30,left=20,scrollbars=1,status=1");
              }
}


/*********************************************************************
                        Code for Call Back Information
 *********************************************************************/
 







//    function URLEncode (clearString) 
//        {
//          var output = '';
//          var x = 0;
//          clearString = clearString.toString();
//          var regex = /(^[a-zA-Z0-9_.]*)/;
//          while (x < clearString.length) {
//            var match = regex.exec(clearString.substr(x));
//            if (match != null && match.length > 1 && match[1] != '') {
//    	        output += match[1];
//              x += match[1].length;
//            } else {
//              if (clearString[x] == ' ')
//                output += '+';
//              else {
//                var charCode = clearString.charCodeAt(x);
//                var hexVal = charCode.toString(16);
//                output += '%' + ( hexVal.length < 2 ? '0' : '' ) + hexVal.toUpperCase();
//              }
//              x++;
//            }
//          }
//          return output;
//        }

//   
//   
//   
//     function CheckSelection()
//    {
//      
//    }
    
    
    

  
  
 
</script>
<%--<body onload="PageLoadMethod();">--%>

<body  >
    <form id="form1" runat="server" defaultfocus="txtName">
        <table width="860px" align="left" style="height: 486px" class="border_rightred">
            <tr>
                <td valign="top">
                    <table width="100%">
                        <tr>
                            <td valign="top">
                                <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td valign="top" align="left">
                                            <span class="menu">Travel Agency-&gt;</span><span class="sub_menu">Agency</span>
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
                                                    <td class="textbold" align="center" valign="TOP" rowspan="0" style="height:25px">
                                                        <asp:Label ID="lblError" class="ErrorMsg" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="100%" valign="top">
                                                        <asp:Panel ID="pnlEmployee" runat="server" Width="100%">
                                                            <table width="100%" border="0" align="left" cellpadding="2" cellspacing="0">
                                                                <tr>
                                                                    <td >
                                                                        </td>
                                                                    <td>Agency Group<span class="Mandatory"><span class="Mandatory">*</span></span></td>
                                                                    <td colspan="3">
                                                                    <asp:TextBox ID="txtAgencyGroup" MaxLength="40" Width="492px" TabIndex="1" CssClass="textboxgrey" ReadOnly="true"
                                                                            runat="server"></asp:TextBox>
                                                                        <img src="../Images/lookup.gif" onclick="javascript:return PopupAgencyGroupManageAgency();" id="IMG1" style="cursor:pointer;"  alt="" runat ="server"  />
                                                                        <asp:HiddenField ID="hdFileNo" runat="server" />
                                                                        
                                                                        <asp:HiddenField ID="hdChainId" runat="server" />
                                                                        <asp:HiddenField ID="hdCity" runat="server" />
                                                                        <asp:HiddenField ID="hdLimAoff" runat="server" />
                                                                        <asp:HiddenField ID="hdLimReg" runat="server" />
                                                                        <asp:HiddenField ID="hdLimOwnOff" runat="server" />
                                                                        <asp:HiddenField ID="hdLcode" runat="server" />
                                                                        &nbsp;
                                                                        <asp:HiddenField ID="hdIPAddressID" runat="server" />
                                                                        <asp:HiddenField ID="hdEnLcode" runat="server" />
                                                                        <asp:HiddenField ID="hdEnAoffice" runat="server" />
                                                                       
                                                                    </td>
                                                                    <td>
                                                                        <asp:Button ID="btnSave" runat="server" TabIndex="39" CssClass="button" Text="Save" Width="125px" AccessKey="S" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                    </td>
                                                                    <td class="textbold">
                                                                        Name <span class="Mandatory">*</span></td>
                                                                    <td colspan="3">
                                                                        <asp:TextBox ID="txtName" runat="server" CssClass="textbox" MaxLength="100" TabIndex="3"
                                                                            Width="492px"></asp:TextBox></td>
                                                                    <td>
                                                                        <asp:Button ID="btnNew" runat="server" TabIndex="40" CssClass="button" Text="New" Width="125px" AccessKey="N" /></td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                    </td>
                                                                    <td class="textbold">
                                                                        Address1 <span class="Mandatory">*</span></td>
                                                                    <td colspan="3">
                                                                        <asp:TextBox ID="txtAddress1" runat="server" CssClass="textbox" MaxLength="100" TabIndex="4"
                                                                            Width="492px"></asp:TextBox></td>
                                                                    <td>
                                                                        <asp:Button ID="btnReset" runat="server" TabIndex="41" CssClass="button" Text="Reset" Width="125px" AccessKey="R" /></td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                    </td>
                                                                    <td class="textbold">
                                                                        Address2<span class="Mandatory"></span></td>
                                                                    <td colspan="3">
                                                                        <asp:TextBox ID="txtAddress2" runat="server" CssClass="textbox" MaxLength="100" TabIndex="5"
                                                                            Width="492px"></asp:TextBox></td>
                                                                    <td>
                                                                    <input type="button" Class="button" TabIndex="42"  value="History" onclick="javascript:PopHistoryManageAgency();" style="width:125px" id="Button1" runat="server" accesskey="H"   />                                                                    
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td >
                                                                    </td>
                                                                    <td class="textbold" style="width:150px">
                                                                        City <span class="Mandatory">*</span></td>
                                                                    <td style="width:200px">
                                                                        <asp:DropDownList  onkeyup="gotop(this.id)"  ID="drpCity" runat="server" TabIndex="6" Width="137px" CssClass="dropdownlist" Height="20px">
                                                                        </asp:DropDownList></td>
                                                                    <td class="textbold" style="width:178px">
                                                                        Pin Code</td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtPinCode" runat="server" CssClass="textbox" MaxLength="6" TabIndex="7"></asp:TextBox></td>
                                                                    <td style="width:23%"> <input type="button" class="button"  tabindex="43"  value="Misc.View Doc" onclick="javascript:ViewMiscDocManageAgency();" style="width:125px" id="btnMiscViewDoc" runat ="server" accesskey="M"  /><%--<asp:Button ID="btnMiscViewDoc" runat="server" TabIndex="40" CssClass="button" Text="Misc.View Doc" Width="125px" />--%></td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                    </td>
                                                                    <td class="textbold">
                                                                        Country</td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtCountry" MaxLength="30" TabIndex="8" CssClass="textboxgrey" ReadOnly="true"
                                                                            runat="server"></asp:TextBox></td>
                                                                    <td class="textbold">
                                                                        Office Name</td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtOfficeName" runat="server" CssClass="textbox" MaxLength="20" TabIndex="9"></asp:TextBox></td>
                                                                    <td >
                                                                    <input type="button" ID="btnAProductivity" runat="server" TabIndex="44" Class="button" value="1 A Productivity" style="width: 125px" onclick="javascript:return Show1AProductivityManageAgency();" accesskey="A"/></td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="height: 26px">
                                                                    </td>
                                                                    <td class="textbold" style="height: 26px">
                                                                        Email</td>
                                                                    <td style="height: 26px">
                                                                        <asp:TextBox ID="txtEmail" CssClass="textbox" MaxLength="100" TabIndex="10" runat="server"></asp:TextBox></td>
                                                                    <td class="textbold" style="height: 26px">
                                                                        Status</td>
                                                                    <td>
                                                                        <asp:DropDownList  onkeyup="gotop(this.id)"  ID="drpStatus" runat="server" TabIndex="11" Width="137px" CssClass="dropdownlist" Height="20px">
                                                                        </asp:DropDownList></td>
                                                                    <td style="height: 26px">
                                                                    <%--<asp:Button ID="btnAProductivity" runat="server" TabIndex="42" CssClass="button" Text="1 A Productivity" Width="125px" />--%>
                                                                    <input type="button" ID="btnAllCrsProductivity" runat="server" TabIndex="45" Class="button" value="All CRS Productivity" style="width: 125px" onclick="javascript:return ShowAllCRSProductivityManageAgency();" accesskey="C"/></td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                    </td>
                                                                    <td class="textbold">
                                                                        Phone</td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtPhone" MaxLength="30" CssClass="textbox" TabIndex="12" runat="server"></asp:TextBox></td>
                                                                    <td class="textbold">
                                                                        IATA ID</td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtIataId" runat="server" CssClass="textbox" MaxLength="20" TabIndex="13"></asp:TextBox></td>
                                                                    <td>
                                                                    <%--<asp:Button ID="btnAllCrsProductivity" runat="server" TabIndex="43" CssClass="button" Text="All CRS Productivity" Width="125px" /></td>--%>
                                                                    <input type="button" id="btnDailyBooking" runat="server" tabIndex="46" class="button" value="Daily Booking" style="width: 125px" onclick="javascript:return ShowDailyBookingsManageAgency();" accesskey="D"/><%--<input type="button" ID="btnAllCrsProductivity" runat="server" TabIndex="45" Class="button" value="All CRS Productivity" style="width: 125px" onclick="javascript:return ShowAllCRSProductivityManageAgency_dubai();" accesskey="C"/>--%></tr>
                                                                <tr>
                                                                    <td>
                                                                        &nbsp;</td>
                                                                    <td class="textbold">
                                                                        Fax</td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtFax" MaxLength="30" CssClass="textbox" TabIndex="14" runat="server"></asp:TextBox></td>
                                                                    <td class="textbold">
                                                                        IATA Status</td>
                                                                    <td>
                                                                        <asp:DropDownList  onkeyup="gotop(this.id)"  ID="drpIataStatus" runat="server" TabIndex="15" Width="137px" CssClass="dropdownlist" Height="20px" AppendDataBoundItems="True">
                                                                        </asp:DropDownList></td>
                                                                    <td>
                                                                    <input type="button" id="btnGroupCase" runat="server" tabIndex="47"  class="button" value="Group Case" style="width: 125px" onclick="javascript:return GroupCaseManageAgency();" accesskey="G"/><%--<asp:Button ID="btnDailyBooking" runat="server" TabIndex="44" CssClass="button" Text="Daily Booking" Width="125px" />--%></td>
                                                                </tr>
                                                                   <tr>
                                                                    <td>
                                                                        &nbsp;</td>
                                                                    <td class="textbold">
                                                                        Customer Category</td>
                                                                    <td>
                                                                        <asp:DropDownList  onkeyup="gotop(this.id)"  ID="drpCustomerCategory" runat="server" Width="137px" CssClass="dropdownlist" TabIndex="16" Height="20px">
                                                                        </asp:DropDownList> </td>
                                                                    <td class="textbold">
                                                                        Web Site</td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtWevSite" runat="server" CssClass="textbox" MaxLength="100" TabIndex="17"></asp:TextBox></td>
                                                                    <td>  <asp:Button ID="txtPtypeChallan" runat="server" TabIndex="48" CssClass="button" Text="PType Challan" Width="125px" AccessKey="P" Visible="false" /><%--<asp:Button ID="btnGroupCase" runat="server" TabIndex="45" CssClass="button" Text="Group Case" Width="125px" Visible="True"  OnClientClick="GroupCase();" />--%></td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                    </td>
                                                                    <td class="textbold">
                                                                        Type</td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtGroupClassification" runat="server" CssClass="textboxgrey" MaxLength="30" ReadOnly="true"
                                                                            TabIndex="18"></asp:TextBox></td>
                                                                    <td class="textbold">
                                                                        IP Address</td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtIPAddress" runat="server" CssClass="textboxgrey" MaxLength="15"
                                                                            TabIndex="19"></asp:TextBox><img  id="img2" src="../Images/lookup.gif" onclick="javascript:return PopupIPDefinitionManageAgency();" runat="server" style="cursor:pointer;"     /></td>
                                                                    <td>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                    </td>
                                                                    <td class="textbold">
                                                                        Company Vertical</td>
                                                                    <td>
                                                                        <asp:TextBox ID="TxtCompVertical" runat="server" CssClass="textboxgrey" MaxLength="30" ReadOnly="true"
                                                                            TabIndex="20"></asp:TextBox></td>
                                                                    <td class="textbold">
                                                                        PAN No.</td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtPanNo" runat="server" CssClass="textbox" MaxLength="15" TabIndex="21"></asp:TextBox></td>
                                                                    <td>
                                                                    </td>
                                                                </tr>
                                                                <tr><td colspan="5"></td><td></td></tr>
                                                                <tr>
                                                                    <td></td>
                                                                    <td class="subheading" colspan="4">Amadeus Specific</td>
                                                                    <td></td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                    </td>
                                                                    <td class="textbold">
                                                                        Aoffice
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtAoffice" MaxLength="15" TabIndex="22" CssClass="textboxgrey" ReadOnly="true" runat="server"></asp:TextBox>
                                                                       <%-- <img onclick="javascript:return PopupAgencyGroupOfficeIDManageAgency();"
                                                                            src="../Images/lookup.gif" />--%></td>
                                                                    <td class="textbold">
                                                                        1 A Responsibility</td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtAResponsibility" runat="server" CssClass="textboxgrey" ReadOnly="true" MaxLength="15" TabIndex="23" ></asp:TextBox>
                                                                        <img  id="img1A" src="../Images/lookup.gif" onclick="javascript:return PopupEmployeeManageAgency();" runat="server"  style="cursor:pointer;"  tabIndex="24"  />
                                                                        <asp:HiddenField ID="hdRespId" runat="server" /></td>
                                                                    <td>
                                                                    <%--<asp:Button ID="btnOrderFeasability" runat="server" TabIndex="46" CssClass="button" Text="Order Feasability" Width="125px"  Visible ="false"  />--%>
                                                                    <input type="button" ID="btnOrderFeasability" runat="server" TabIndex="49" Class="button" value="Order Feasibility" style="width: 125px" onclick="javascript:return OrderFeasibilityManageAgency();" accesskey="O"/>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                    </td>
                                                                    <td class="textbold">
                                                                        Date Online</td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtDateOnline" runat="server" CssClass="textbox" MaxLength="10"
                                                                            TabIndex="25"></asp:TextBox>
                                                                        <img id="imgDateOnline" TabIndex="26" alt="" src="../Images/calender.gif" title="Date selector" runat="server"
                                                                            style="cursor: pointer" />&nbsp;

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
                                                                    <asp:TextBox ID="txtDateOffline" runat="server" CssClass="textbox" MaxLength="10"
                                                                            TabIndex="29"></asp:TextBox>
                                                                        <img id="imgDateOffline" TabIndex="30" alt="" src="../Images/calender.gif" title="Date selector" runat="server" 
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
                                                                    <td style="height: 24px">
                                                                    </td>
                                                                    <td class="textbold" style="height: 24px">
                                                                        File Number</td>
                                                                    <td style="height: 24px"><asp:TextBox id="txtFileNo" tabIndex="30" runat="server" ReadOnly="true" CssClass="textboxgrey" MaxLength="30"></asp:TextBox>
                                                                       <%-- <asp:DropDownList  onkeyup="gotop(this.id)"  ID="drpFileNumber" runat="server" TabIndex="25" Width="137px" CssClass="dropdown" Height="25px">
                                                                    </asp:DropDownList>--%></td>
                                                                    <td style="height: 24px" class="textbold">
                                                                        </td>
                                                                    <td>
                                                                    
                                                                        </td>
                                                                    <td style="height: 24px">
                                                                    </td>
                                                                </tr>
                                                             
                                                                <tr>
                                                                    <td></td>
                                                                    <td  colspan="4"></td>
                                                                    <td></td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                    </td>
                                                                    <td class="subheading" colspan="4">
                                                                        Connectivity</td>
                                                                    <td></td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                    </td>
                                                                    <td class="textbold">
                                                                        <strong>Connectivity</strong></td>
                                                                    <td class="textbold">
                                                                        <strong>Online Status</strong></td>
                                                                    <td class="textbold">
                                                                        <strong>
                                                                        Installation Date</strong></td>
                                                                    <td class="textbold" style="width: 169px">
                                                                        <strong>
                                                                        Order Number</strong></td>
                                                                    <td>
                                                                    <input type="button" Class="button" TabIndex="50"  value="Connectivity History" onclick="javascript:PopConnectivityHistoryManageAgency();" style="width:125px" id="btnConnectivityHistory"  runat ="server"  /></td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                    </td>
                                                                    <td class="textbold">
                                                                        Primary</td>
                                                                    <td>
                                                                        <asp:DropDownList  onkeyup="gotop(this.id)"  ID="drpPrimaryOnlineStatus" runat="server" TabIndex="31" Width="137px" CssClass="dropdownlist" Height="20px">
                                                                        </asp:DropDownList></td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtPrimaryDate" runat="server" CssClass="textbox" MaxLength="10"
                                                                            TabIndex="32"></asp:TextBox>
                                                                        <img id="imgPrimaryDate" alt="" TabIndex="33" src="../Images/calender.gif" title="Date selector" runat="server" 
                                                                            style="cursor: pointer" />&nbsp;

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
                                                                        <asp:TextBox ID="txtPrimaryOrderNumber" runat="server" CssClass="textbox" MaxLength="10" TabIndex="34"></asp:TextBox></td>
                                                                    <td>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                    </td>
                                                                    <td class="textbold">
                                                                        Backup</td>
                                                                    <td>
                                                                        <asp:DropDownList  onkeyup="gotop(this.id)"  ID="drpBackupOnlineStatus" runat="server" TabIndex="35" Width="137px" CssClass="dropdownlist" Height="20px">
                                                                        </asp:DropDownList></td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtBackupDate" runat="server" CssClass="textbox" MaxLength="10"
                                                                            TabIndex="36"></asp:TextBox>
                                                                        <img id="imgBackupDate" alt="" src="../Images/calender.gif"  tabindex="37" title="Date selector" runat="server" 
                                                                            style="cursor: pointer" />&nbsp;                                                                            
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
                                                                        <asp:TextBox ID="txtBackupOrderNumber" runat="server" CssClass="textbox" MaxLength="10" TabIndex="38"></asp:TextBox></td>
                                                                    <td>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        &nbsp;</td>
                                                                    <td colspan="4" class="ErrorMsg">
                                                                        Field Marked * are Mandatory</td>
                                                                    <td>
<input type="hidden" id="hdEmployeePageName" style="width:1px" runat='server' />
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
