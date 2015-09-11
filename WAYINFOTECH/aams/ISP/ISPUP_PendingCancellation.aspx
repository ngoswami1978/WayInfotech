<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ISPUP_PendingCancellation.aspx.vb" Inherits="ISP_ISPUP_PendingCancellation" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>AAMS: Agency</title>
    <link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />       
    <link rel="stylesheet" type="text/css" media="all" href="../Calender/calendar-blue.css"
        title="win2k-cold-1" />
   
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

 
</script>
<body>
    <form id="form1" runat="server" defaultfocus="drpIspOrderStatus">
        <table width="860px" align="left"  class="border_rightred">
            <tr>
                <td valign="top">
                    <table width="860px">
                        <tr>
                            <td valign="top">
                                <table width="860px" border="0" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td valign="top" align="left" style="width:860px;">
                                            <span class="menu">ISP-&gt;</span><span class="sub_menu">ISP Pending Cancellation</span>
                                        </td>
                                      
                                    </tr>
                                    <tr>
                                        <td bgcolor="#1A61A9" class="heading" align="center" style="width:860px;">
                                            Manage ISP Pending Cancellation&nbsp;</td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td valign="top" style="width:860px;">
                                <table width="860px" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td valign="top" class="redborder" >
                                           <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td style="width:100%" valign="top">
                                                        <asp:Panel ID="pnlIspOrder" runat="server" Width="100%">
                                                           <table width="800" border="0" align="left" cellpadding="0" cellspacing="0">
                                                                <tr>
                                                                <td  colspan ="6" class="textbold" align="center" valign="TOP" rowspan="0">
                                                                    <asp:Label ID="lblError"  CssClass="ErrorMsg" runat="server"></asp:Label>
                                                                </td>
                                                                </tr>
                                                                 <tr>
                                                                     <td  class="textbold" colspan="6" align="center" valign="TOP" style ="height :5px"></td>
                                                                </tr>
                                                                   <tr>
                                                                    <td class="textbold" style="width: 4px;" >
                                                                    </td>
                                                                    <td class="subheading" colspan="4" style="height: 19px" >
                                                                        Agency Detail</td>
                                                                    <td >
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                     <td  style ="height :5px"  class="textbold" colspan="6" align="center" valign="TOP"></td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="textbold" style="width:2%">
                                                                    </td>
                                                                    <td class="textbold" style ="width:23%" >Name</td>
                                                                    <td colspan="3" >
                                                                        <asp:TextBox ID="txtAgencyName" TabIndex="1"
                                                                            CssClass="textboxgrey" runat="server" Width="514px" ReadOnly="True"></asp:TextBox>
                                                                             <img src="../Images/lookup.gif"  onclick="javascript:return PopupAgencyPageIspPendingCancellation();" visible="false"  id="imgAgency" runat="server"  />
                                                                        <asp:HiddenField ID="hdAgencyNameId" runat="server"  />
                                                                            </td>
                                                                           
                                                                    <td style ="width:10%" valign ="top" >
                                                                        <asp:Button ID="btnSave" runat="server" TabIndex="24" CssClass="button" Text="Save" AccessKey="S" /></td>
                                                                </tr>
                                                                <tr>
                                                                     <td style ="height :5px" class="textbold" colspan="6" align="center" valign="TOP"></td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="textbold" style="width: 4px;">
                                                                    </td>
                                                                    <td class="textbold">
                                                                        Address</td>
                                                                    <td colspan="3">
                                                                        <asp:TextBox ID="txtAgencyAddress" runat="server" CssClass="textboxgrey" TabIndex="2"
                                                                            TextMode="MultiLine" Width="514px" Height="50px" ReadOnly="True" Rows="5"></asp:TextBox></td>
                                                                   
                                                                    <td valign ="top" >
                                                                        <asp:Button ID="btnReset" runat="server" TabIndex="25" CssClass="button" Text="Reset" AccessKey="R" /><br />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                     <td style ="height :5px" class="textbold" colspan="6" align="center" valign="TOP"></td>
                                                                </tr>
                                                                 <tr>
                                                                    <td class="textbold" style="width: 4px; height: 22px;">
                                                                    </td>
                                                                    <td class="textbold" style="height: 22px">City</td>
                                                                    <td style="width:30%; height: 22px;" >
                                                                        <asp:TextBox ID="txtCity" runat="server" CssClass="textboxgrey"
                                                                            TabIndex="3" ReadOnly="True"></asp:TextBox>
                                                                        </td>
                                                                    <td class="textbold" style="width:25%; height: 22px;">Country</td>
                                                                    <td style="width: 185px; height: 22px;">
                                                                        <asp:TextBox ID="txtCountry" CssClass="textboxgrey" TabIndex="4" runat="server" ReadOnly="True"></asp:TextBox>
                                                                        </td>
                                                                    <td style="height: 22px"></td>
                                                                </tr>
                                                                  <tr>
                                                                     <td style ="height :5px" class="textbold" colspan="6" align="center" valign="TOP"></td>
                                                                </tr>
                                                                 <tr>
                                                                    <td class="textbold" style="width: 4px; height: 21px;">
                                                                    </td>
                                                                    <td class="textbold" style="height: 21px">Phone</td>
                                                                    <td style="height: 21px">
                                                                        <asp:TextBox ID="txtPhone" runat="server" CssClass="textboxgrey"
                                                                            TabIndex="5" ReadOnly="True"></asp:TextBox>
                                                                        </td>
                                                                    <td class="textbold" style="height: 21px">Fax</td>
                                                                    <td style="width: 185px; height: 21px;">
                                                                        <asp:TextBox ID="txtFax" CssClass="textboxgrey" TabIndex="6" runat="server" ReadOnly="True"></asp:TextBox>
                                                                        </td>
                                                                    <td style="height: 21px">
                                                                        <asp:HiddenField ID="hdLcode" runat="server" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                     <td style="height: 5px" class="textbold" colspan="6" align="center" valign="TOP"></td>
                                                                </tr>
                                                                 <tr>
                                                                    <td class="textbold" style="width: 4px;">
                                                                    </td>
                                                                    <td class="textbold">Office Id</td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtOfficeId" runat="server" CssClass="textboxgrey"
                                                                            TabIndex="7" ReadOnly="True"></asp:TextBox>
                                                                        </td>
                                                                    <td class="textbold"></td>
                                                                    <td style="width: 185px">         
                                                                        </td>
                                                                    <td></td>
                                                                </tr>  
                                                                <tr>
                                                                    <td class="textbold" >
                                                                    </td>
                                                                    <td class="textbold">
                                                                        <input id="hdIspNameId" runat="server" style="width: 1px" type="hidden" />
                                                                        <input id="hdPendingWithId" runat="server" style="width: 1px" type="hidden" />
                                                                        <input id="hdProcessedById" runat="server" style="width: 1px" type="hidden" />
                                                                        <input id="hdOrderTypeNew" runat="server" style="width: 1px" type="hidden" />
                                                                        <input id="hdOrderTypeCancel" runat="server" style="width: 1px" type="hidden" />
                                                                        <input id="hdISPId" runat="server" style="width: 1px" type="hidden" />
                                                                        <input id="hdISPIdName" runat="server" style="width: 1px" type="hidden" />
                                                                        <input id="hdNPID" runat="server" style="width: 1px" type="hidden" />
                                                                         <input id="hdIspPlanId" runat="server" style="width: 1px" type="hidden" /></td>
                                                                    <td>
                                                                    </td>
                                                                    <td class="textbold">
                                                                    </td>
                                                                    <td style="width: 185px">
                                                                    </td>
                                                                    <td>
                                                                    </td>
                                                                </tr>
                                                                 <tr>
                                                                     <td style="height: 2px" class="textbold" colspan="6" align="center" valign="TOP"></td>
                                                                </tr>
                                                                  <tr>
                                                                    <td class="textbold" style="width: 4px;" >
                                                                    </td>
                                                                    <td class="subheading" colspan="4" style="height: 19px">
                                                                        ISP Details</td>
                                                                    <td style="height: 19px">
                                                                    </td>
                                                                </tr>
                                                                   <tr>
                                                                     <td  style ="height :5px"  class="textbold" colspan="6" align="center" valign="TOP"></td>
                                                                </tr>
                                                                 <tr>
                                                                    <td class="textbold" style="height:25px; width: 4px;">
                                                                    </td>
                                                                    <td class="textbold">ISP Name </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtIsp" runat="server" CssClass="textboxgrey"
                                                                            TabIndex="8" ReadOnly="True"></asp:TextBox>&nbsp;
                                                                        </td>
                                                                    <td class="textbold">ISP City Name</td>
                                                                    <td style="width: 185px">
                                                                        <asp:TextBox ID="txtISPCityName" CssClass="textboxgrey" TabIndex="9" runat="server" ReadOnly="True"></asp:TextBox>
                                                                        </td>
                                                                    <td>
                                                                        <asp:HiddenField ID="hdLoggedByID" runat="server" />
                                                                    </td>
                                                                </tr>
                                                                  <tr>
                                                                     <td  style ="height :5px"  class="textbold" colspan="6" align="center" valign="TOP"></td>
                                                                </tr>
                                                                 <tr>
                                                                    <td class="textbold" style="width: 4px;" >
                                                                    </td>
                                                                    <td class="subheading" colspan="4" style="height: 19px"> NPID (ISP Plan)</td>
                                                                    <td style="height: 19px">
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                     <td style ="height :5px"  class="textbold" colspan="6" align="center" valign="TOP"></td>
                                                                </tr>
                                                                 <tr>
                                                                    <td class="textbold" style="width: 4px;">
                                                                    </td>
                                                                    <td class="textbold">NPID&nbsp;<span class="Mandatory">*</span></td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtNPID" runat="server" CssClass="textboxgrey"
                                                                            TabIndex="10" ReadOnly="True"></asp:TextBox>&nbsp;
                                                                        
                                                                        </td>
                                                                    <td class="textbold">Band Width </td>
                                                                    <td style="width: 185px">
                                                                        <asp:TextBox ID="txtBandWidth" CssClass="textboxgrey" TabIndex="11" runat="server" ReadOnly="True"></asp:TextBox>
                                                                        </td>
                                                                    <td></td>
                                                                </tr>
                                                                <tr>
                                                                     <td style ="height :5px"  class="textbold" colspan="6" align="center" valign="TOP"></td>
                                                                </tr>
                                                             
                                                             
                                                                    <tr>
                                                                    <td class="textbold" style="width: 4px" >
                                                                    </td>
                                                                    <td class="subheading" colspan="4" style="height: 19px">Order Details</td>
                                                                    <td>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                     <td  class="textbold" colspan="6" align="center" valign="TOP" style ="height :5px"></td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="textbold" style="width:5%">
                                                                        </td>
                                                                    <td class="textbold" style="width:20%">
                                                                        </td>
                                                                    <td colspan="2" style="width:30%"><asp:RadioButtonList ID="rdlNewCancel" runat="server" CssClass="dropdown" RepeatDirection="Horizontal"
                                                                            Width="300px" CellPadding="0" CellSpacing="0" onclick="FillOrderType()" TabIndex="1" Visible="False">
                                                                            <asp:ListItem Selected="True" Value="t">New Order</asp:ListItem>
                                                                            <asp:ListItem Value="f">Cancellation</asp:ListItem>
                                                                        </asp:RadioButtonList></td> 
                                                                      <td style="width:185px"></td>
                                                                        <td style="width:25%" ></td>                                                                    
                                                                </tr>
                                                                 <tr>
                                                                     <td  class="textbold" colspan="6" align="center" valign="TOP" style ="height :5px"></td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="textbold"   style="width:5%; height: 21px;"></td>
                                                                    <td class="textbold" style="width:20%; height: 21px;">Order Number&nbsp;</td>
                                                                    <td style="width:20%; height: 21px;"><asp:TextBox ID="txtOrderNumber" runat="server" CssClass="textboxgrey" TabIndex="12" ReadOnly="True"></asp:TextBox></td>
                                                                    <td class="textbold" style="width:15%; height: 21px;">
                                                                        ISP Order Status<span class="Mandatory">*</span></td>
                                                                    <td style="width:185px; height: 21px;"><asp:DropDownList ID="drpIspOrderStatus" runat="server" TabIndex="13" Width="137px" CssClass="dropdownlist" AutoPostBack="True">
                                                                      </asp:DropDownList></td>
                                                                    <td style="width:20%; height: 21px;">
                                                                        </td>
                                                                </tr>
                                                              <%--  <tr>
                                                                     <td style ="height :5px" class="textbold" colspan="6" align="center" valign="TOP"></td>
                                                                </tr>--%>
                                                              <%--  <tr>
                                                                    <td class="textbold" style="width: 4px"></td>
                                                                    <td class="textbold" style="width: 15%">Order Type<span class="Mandatory">*</span></td>
                                                                    <td colspan="3" style="width:30%"><asp:DropDownList ID="ddlOrderType" runat="server" TabIndex="4" Width="437px" CssClass="textbold">
                                                                    </asp:DropDownList>&nbsp;</td>                                                                    
                                                                    <td style="width:35%"></td>
                                                                </tr>--%>
                                                                <tr>
                                                                     <td style ="height :5px" class="textbold" colspan="6" align="center" valign="TOP"></td>
                                                                </tr>
                                                                  <tr>
                                                                   <td class="textbold" style="width:4px"></td>
                                                                    <td class="textbold" style="width:15%">Order Date&nbsp;</td>
                                                                    <td style="width:15%">
                                                                        <asp:TextBox ID="txtOrderDate" runat="server" CssClass="textboxgrey" MaxLength="15" TabIndex="14" ReadOnly="True"></asp:TextBox>
                                                                        &nbsp; </td>
                                                                      <td class="textbold" style="width:15%">
                                                                          Orignal Order No. Against Cancellation<span class="Mandatory">*</span></td>
                                                                      <td style="width:185px"><asp:DropDownList id="drpNewOrderNoForCan" tabIndex=15 runat="server" Width="137px" CssClass="dropdownlist">
                                                                      </asp:DropDownList>
                                                                        </td>
                                                                    <td style="width:35%"></td> 
                                                                </tr>
                                                                  <tr>
                                                                     <td style ="height :5px" class="textbold" colspan="6" align="center" valign="TOP"></td>
                                                                </tr>
                                                                <tr>
                                                                   <td class="textbold" style="width:4px"></td>
                                                                    <td class="textbold" style="width:15%">
                                                                        <span >Logged by</span></td>
                                                                    <td style="width:15%">
                                                                       <asp:TextBox id="txtLoggedby" tabIndex=16 runat="server" CssClass="textboxgrey" ReadOnly="True"></asp:TextBox></td>
                                                                      <td class="textbold" style="width:15%">Approved by</td>
                                                                      <td style="width:185px">
                                                                          <asp:TextBox ID="txtApprovedBy" runat="server" CssClass="textboxgrey" MaxLength="15" ReadOnly="True"
                                                                              TabIndex="17"></asp:TextBox><img tabIndex="26" id="img1A" src="../Images/lookup.gif" onclick="javascript:return PopupEmployeeForISPCan();" runat="server"  style="cursor:pointer;"  />
                                                                        <asp:HiddenField ID="hdApprovedBy" runat="server" />
                                                                       <%--  <asp:DropDownList ID="drpApprovedBy" runat="server" TabIndex="25" Width="137px" CssClass="textbold" Visible="False">
                                                                      </asp:DropDownList>--%></td>
                                                                    <td style="width:35%"></td>
                                                                </tr>
                                                                  <tr>
                                                                     <td  style ="height :5px" class="textbold" colspan="6" align="center" valign="TOP"></td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="textbold" style="width:4px; height: 21px;"></td>
                                                                    <td class="textbold" style="width:15%; height: 21px;">
                                                                        WLL Number</td>
                                                                    <td style="width:15%; height: 21px;">
                                                                        <asp:TextBox ID="txtMDNNo" CssClass="textbox" TabIndex="19" runat="server" MaxLength="25"></asp:TextBox>
                                                                       </td>
                                                                    <td class="textbold" style="width:15%; height: 21px;">CAF Account ID</td>
                                                                    <td style="width:185px; height: 21px;">
                                                                        <asp:TextBox ID="txtCafAcId" CssClass="textbox" TabIndex="20" runat="server" MaxLength="25"></asp:TextBox></td>
                                                                    <td style="width:35%; height: 21px;"></td>
                                                                </tr>
                                                                <tr>
                                                                     <td   style ="height :5px" class="textbold" colspan="6" align="center" valign="TOP" ></td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="textbold" style="width: 4px; height: 21px;">
                                                                    </td>
                                                                    <td class="textbold" style="height: 21px">
                                                                        Cancellation Date</td>
                                                                    <td style="height: 21px">
                                                                        <asp:TextBox ID="txtCanDate" runat="server" CssClass="textbox"
                                                                            TabIndex="21" MaxLength="12"></asp:TextBox> <img id="ImgCanDate" alt="" src="../Images/calender.gif" style="cursor: pointer" tabindex="22"
                                                                            title="Date selector" /><script type="text/javascript">
                                                                                                    Calendar.setup({
                                                                                                    inputField     :    '<%=txtCanDate.clientId%>',
                                                                                                    ifFormat       :    "%d/%m/%Y",
                                                                                                    button         :    "ImgCanDate",
                                                                                                    //align          :    "Tl",
                                                                                                    singleClick    :    true
                                                                                                    }); </script></td>
                                                                    <td class="textbold" style="height: 21px">
                                                                        </td>
                                                                    <td style="height: 21px; width: 185px;">
                                                                        </td>
                                                                    <td style="height: 21px"></td>
                                                                </tr>
                                                                <tr>
                                                                     <td  style ="height :5px"  class="textbold" colspan="6" align="center" valign="TOP"></td>
                                                                </tr>
                                                                                                                     
                                                                 <tr>
                                                                    <td class="textbold" style="width: 4px;">
                                                                    </td>
                                                                    <td class="textbold" valign="top" >Cancellation Reason</td>
                                                                    <td colspan ="3" rowspan="2" style="height: 47px">
                                                                        <asp:TextBox ID="txtCanReason" CssClass="textbox" TabIndex="23" runat="server" Width="507px" MaxLength="300" TextMode="MultiLine" Height="56px"></asp:TextBox></td>
                                                                    
                                                                    <td></td>
                                                                </tr>
                                                                
                                                                <tr>
                                                                
                                                                </tr>
                                                                
                                                                  <tr>
                                                                     <td   style ="height :5px" class="textbold" colspan="6" align="center" valign="TOP"></td>
                                                                </tr>
                                                                 <tr>
                                                                    <td class="textbold" style="width: 4px;">
                                                                    </td>
                                                                    <td class="textbold" valign="top" ></td>
                                                                    <td colspan ="3">
                                                                        &nbsp;</td>
                                                                    
                                                                </tr>
                                                                <tr>
                                                                     <td  style="height :2px"  class="textbold" colspan="6" align="center" valign="TOP">
                                                                         <asp:HiddenField ID="HiddenField1" runat="server" />
                                                                     </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="textbold" style="width: 4px">
                                                                        &nbsp;</td>
                                                                    <td colspan="4" class="ErrorMsg">
                                                                        Field Marked * are Mandatory</td>
                                                                    <td><input type="hidden" id="hdEmployeePageName" style="width:1px" runat='server' />
                                                                        &nbsp;</td>
                                                                </tr>                                                           
                                                            </table>
                                                        </asp:Panel>
                                                    </td>
                                                </tr>
                                                   <tr>
                                                    <td style="width:100%" valign="top">
                                                        <asp:Panel ID="pnlPrevIspOrder" runat="server" Width="100%">
                                                         <table border ="0" cellpadding ="0" cellspacing="0"  width ="100%">
                                                            <tr>
                                                              <td  width ="100%">
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
