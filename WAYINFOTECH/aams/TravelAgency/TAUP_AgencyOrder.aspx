<%@ Page Language="VB"   MaintainScrollPositionOnPostback ="true"   AutoEventWireup="false" CodeFile="TAUP_AgencyOrder.aspx.vb" Inherits="TravelAgency_MSUP_AgencyOrder" ValidateRequest="false" EnableEventValidation="false" %>
<%@ Register Src="../UserControl/MenuControl.ascx" TagName="MenuControl" TagPrefix="uc1" %>

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
  function MandForCompanyVertical()
	{ 
	 // debugger;
	    if ((document.getElementById ('hDCompanyVertical').value=='3') && (  document.getElementById ('hDCompanyVerticalSelectByUser').value=='3' ||  document.getElementById ('hDCompanyVerticalSelectByUser').value=='') )
	    {
		    var	arr	= showModalDialog("CompanyVetical.aspx","","font-family:Verdana;font-size:12; dialogWidth:43;dialogHeight:14" );
		    if (arr	!= null) 
		    {		   
		         document.getElementById('hDCompanyVerticalSelectByUser').value = arr;
		    }     
		   // alert(document.getElementById('hDCompanyVertical').value);
		  //  $get("<%=BtnSave.ClientID %>").click(); 
		}    
	} 
  
</script>
<body  >
    <form id="form1" runat="server" defaultfocus="rdlNewCancel">
        <table width="860px" align="left" style="height: 486px" class="border_rightred">
            <tr>
                <td valign="top">
                    <table width="860px">
                        <tr>
                            <td valign="top">
                                <table width="860px" border="0" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td valign="top" align="left" style="width:860px;">
                                            <span class="menu">Travel Agency-&gt;</span><span class="sub_menu">Agency</span>
                                        </td>
                                       
                                    </tr>
                                    <tr>
                                        <td bgcolor="#1A61A9" class="heading" align="center" style="width:860px;">
                                            Manage Agency Order&nbsp;</td>
                                        </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td valign="top" style="width:860px;">
                                <table width="860px" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td valign="top">
                                            <uc1:MenuControl ID="MenuControl1" runat="server" />
                                        </td>
                                    </tr> 
                                    <tr> 
                                           <td valign="top" class="redborder"> 
                                             <table width="860x" border="0" cellspacing="0" cellpadding="0">                     
                                                         <tr>
                                                                        <td valign="top"  style="width:845px;padding-left:7px; padding-bottom:7px;">
                                                                            <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                                                               <tr>
                                                                                   <td valign="top" >                                                              
                                                                                      <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                                                                            <tr>
                                                                                            <td valign="top" style ="height:5PX"></td>
                                                                                           </tr>   
                                                                                          <tr>
                                                                                             <td valign="middle" style="height: 22px"><asp:Button ID="btnOrder" runat="server" Text="Order Details" CssClass="headingtab" Width="72px" />&nbsp;<asp:Button ID="btnMail" runat="server" Text="Mailing List" CssClass="headingtab" Enabled="False" Width="72px" /></td>
                                                                                          </tr>
                                                                                      </table> 
                                                                                       </td>
                                                                                </tr>                                                            
                                                                                <tr>
                                                                                    <td valign="top" class="redborder"   >
                                                                                       <table width="100%" border="0" cellpadding="0" cellspacing="0">                                                                                                                                         
                                                                                            <tr>
                                                                                                <td  style="width:100%" valign="top">
                                                                                                    <asp:Panel ID="pnlAgencyOrder" runat="server" Width="100%">
                                                                                                       <table width="830" border="0" align="left" cellpadding="0" cellspacing="0">
                                                                                                            <tr>
                                                                                                            <td  colspan ="6" class="textbold" align="center" valign="TOP" rowspan="0">
                                                                                                                <asp:Label ID="lblError" class="ErrorMsg" runat="server"></asp:Label>
                                                                                                            </td>
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                                <td style="width:5%">
                                                                                                                    </td>
                                                                                                                <td style="width:20%">
                                                                                                                    </td>
                                                                                                                <td colspan="2" style="width:30%"><asp:RadioButtonList ID="rdlNewCancel" runat="server" CssClass="dropdown" RepeatDirection="Horizontal"
                                                                                                                        Width="300px" CellPadding="0" CellSpacing="0"  TabIndex="1" AutoPostBack="True">
                                                                                                                        <asp:ListItem Selected="True" Value="T">New Order</asp:ListItem>
                                                                                                                        <asp:ListItem Value="f">Cancellation</asp:ListItem>
                                                                                                                    </asp:RadioButtonList></td> 
                                                                                                                  <td style="width:30%"></td>
                                                                                                                    <td style="width:25%" ></td>                                                                    
                                                                                                            </tr>
                                                                                                             <tr>
                                                                                                                 <td  colspan="6" align="center" valign="TOP"></td>
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                                <td style="width:5%"></td>
                                                                                                                <td class="textbold" style="width:20%">Order Number</td>
                                                                                                                <td style="width:20%"><asp:TextBox ID="txtOrderNumber" runat="server" CssClass="textboxgrey" TabIndex="2" ReadOnly="True"></asp:TextBox></td>
                                                                                                                <td class="textbold" style="width:15%">Order Status<span class="Mandatory">*</span></td>
                                                                                                                <td style="width:20%"><asp:DropDownList  onkeyup="gotop(this.id)"  ID="ddlOrderStatus" runat="server" TabIndex="3" Width="137px" CssClass="textbold">
                                                                                                                </asp:DropDownList></td>
                                                                                                                <td style="width:20%">
                                                                                                                    <asp:Button ID="btnNew" runat="server" CssClass="button" TabIndex="39" Text="New" AccessKey="N" /></td>
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                                 <td  colspan="6" align="center" valign="TOP"></td>
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                                <td style="width: 4px"></td>
                                                                                                                <td class="textbold" style="width: 15%">Order Type<span class="Mandatory">*</span></td>
                                                                                                                <td colspan="3" style="width:30%"><asp:DropDownList  onkeyup="gotop(this.id)"  ID="ddlOrderType" runat="server" TabIndex="4" Width="437px" CssClass="textbold" AutoPostBack="True">
                                                                                                                </asp:DropDownList>&nbsp;</td>                                                                    
                                                                                                                <td style="width:35%"><asp:Button ID="btnSave" runat="server" TabIndex="40" CssClass="button" Text="Save" AccessKey="S" /></td>
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                                 <td  colspan="6" align="center" valign="TOP"></td>
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                                <td style="width:4px"></td>
                                                                                                                <td class="textbold" style="width:15%">ISP Name</td>
                                                                                                                <td style="width:15%"><asp:DropDownList  onkeyup="gotop(this.id)"  ID="drpIspName" runat="server" TabIndex="5" Width="139px" CssClass="textbold" AutoPostBack="True">
                                                                                                                </asp:DropDownList>&nbsp;</td>
                                                                                                                <td class="textbold" style="width:15%">Plan ID</td>
                                                                                                                <td style="width:15%"><asp:DropDownList id="drpPlainId" tabIndex="6" onkeyup="gotop(this.id)" runat="server" Width="137px" CssClass="textbold">
                                                                                                                </asp:DropDownList><%--<asp:TextBox ID="txtPlainId" CssClass="textboxgrey" TabIndex="6" runat="server" ReadOnly="True"></asp:TextBox>&nbsp;<img tabIndex="8" onclick="javascript:return PopupISPPlanIdAorder();" src="../Images/lookup.gif" />--%></td>
                                                                                                                <td style="width:35%"><asp:Button ID="btnReset" runat="server" TabIndex="41" CssClass="button" Text="Reset" AccessKey="R" /></td>
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                                 <td  colspan="6" align="center" valign="TOP"></td>
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                                <td style="height:25px; width: 4px;">
                                                                                                                </td>
                                                                                                                <td class="textbold">
                                                                                                                    Pending With</td>
                                                                                                                <td>
                                                                                                                    <asp:TextBox ID="txtPendingWith" runat="server" CssClass="textboxgrey"
                                                                                                                        TabIndex="8" ReadOnly="True"></asp:TextBox>
                                                                                                                    <img TabIndex="9" src="../Images/lookup.gif" onclick="javascript:return PopupPagePendingWithAorder();" style="cursor:pointer;"  /></td>
                                                                                                                <td class="textbold">
                                                                                                                    Processed By</td>
                                                                                                                <td>
                                                                                                                    <asp:TextBox ID="txtProcessedBy" runat="server" CssClass="textboxgrey" ReadOnly="True"
                                                                                                                        TabIndex="10"></asp:TextBox>&nbsp;<%--<img TabIndex="11" src="../Images/lookup.gif" alt="Select & Add Employee" onclick="javascript:return PopupPageProcessedByAorder();" />--%></td>
                                                                                                                <td><asp:Button ID="btnSendMail" runat="server" TabIndex="44" CssClass="button" Text="Send Mail" Enabled="False" Visible="False" /></td>
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                                 <td colspan="6" align="center" valign="TOP"></td>
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                                <td >
                                                                                                                </td>
                                                                                                                <td>
                                                                                                                    <input id="hdIspNameId" runat="server" type="hidden" />
                                                                                                                    <input id="hdPendingWithId" runat="server" type="hidden" />
                                                                                                                    <input id="hdProcessedById" runat="server" type="hidden" />
                                                                                                                    <input id="hdOrderTypeNew" runat="server" type="hidden" />
                                                                                                                    <input id="hdOrderTypeCancel" runat="server" type="hidden" />
                                                                                                                    <input id="hdISPId" runat="server"  type="hidden" />
                                                                                                                    <input id="hdISPIdName" runat="server" type="hidden" />
                                                                                                                    <input id="hdNPID" runat="server" type="hidden" />  
                                                                                                                    <input id="hdLcode" runat="server" type="hidden" />
                                                                                                                    
<input type="hidden" id="hdEmployeePageName" style="width:1px" runat='server' />
                                                                                                                    </td>
                                                                                                                <td>
                                                                                                                </td>
                                                                                                                <td class="textbold">
                                                                                                                </td>
                                                                                                                <td>
                                                                                                                </td>
                                                                                                                <td>
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                           
                                                                                                            <tr>
                                                                                                                <td  style="width: 4px" >
                                                                                                                </td>
                                                                                                                <td class="subheading" colspan="4">
                                                                                                                    Agency Detail</td>
                                                                                                                <td>
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                                 <td  colspan="6" align="center" valign="TOP"></td>
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                                <td style="height:49px; width: 4px;">
                                                                                                                </td>
                                                                                                                <td class="textbold" style="height: 49px">
                                                                                                                    Name<span class="Mandatory">*</span></td>
                                                                                                                <td colspan="3" style="height: 49px">
                                                                                                                    <asp:TextBox ID="txtAgencyName" TabIndex="11"
                                                                                                                        CssClass="textboxgrey" runat="server" Width="437px" ReadOnly="True"></asp:TextBox>&nbsp;
                                                                                                                    <asp:HiddenField ID="hdAgencyNameId" runat="server"  />
                                                                                                                    <asp:HiddenField ID="hdCity" runat="server" />
                                                                                                                      <asp:HiddenField ID="hdIspOrder" runat="server" />
                                                                                                                      <asp:HiddenField ID="hdDaysforExpected" runat="server" />
                                                                                                                      <asp:HiddenField id="hDCompanyVertical" runat="server"></asp:HiddenField>
                                                                                                                       <asp:HiddenField id="hDCompanyVerticalSelectByUser" runat="server"></asp:HiddenField>
                                                                                                                        </td>
                                                                                                                       
                                                                                                                <td style="height: 49px">
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                                 <td   colspan="6" align="center" valign="TOP"></td>
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                                <td  style="height:25px; width: 4px;">
                                                                                                                </td>
                                                                                                                <td class="textbold">
                                                                                                                    Address</td>
                                                                                                                <td colspan="3">
                                                                                                                    <asp:TextBox ID="txtAgencyAddress" runat="server" CssClass="textboxgrey" TabIndex="12"
                                                                                                                        TextMode="MultiLine" Width="437px" Height="50px" ReadOnly="True" Rows="5" MaxLength="500"></asp:TextBox></td>
                                                                                                               
                                                                                                                <td></td>
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                                 <td  colspan="6" align="center" valign="TOP"></td>
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                                <td style="width: 4px">
                                                                                                                </td>
                                                                                                                <td>
                                                                                                                    </td>
                                                                                                                <td>
                                                                                                                  
                                                                                                                  

                                                                                                                </td>
                                                                                                                <td >
                                                                                                                    </td>
                                                                                                                <td>
                                                                                                                  

                                                                                                                </td>
                                                                                                                <td>
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                                 <td   colspan="6" align="center" valign="TOP"></td>
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                                <td style="width: 4px; height: 7px;" >
                                                                                                                </td>
                                                                                                                <td class="subheading" colspan="4" style="height: 7px">
                                                                                                                    Dates</td>
                                                                                                                <td style="height: 7px">
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                                 <td  colspan="6" align="center" valign="TOP"></td>
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                                <td style="height:25px; width: 4px;">
                                                                                                                </td>
                                                                                                                <td class="textbold">
                                                                                                                    Processed</td>
                                                                                                                <td class="textbold">
                                                                                                                    <asp:TextBox ID="txtDateProcessed" runat="server" CssClass="textboxgrey" TabIndex="14"></asp:TextBox> 
                                                                                                                   </td>
                                                                                                                <td>
                                                                                                                    </td>
                                                                                                                <td>
                                                                                                                    </td>
                                                                                                                <td>
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                                 <td colspan="6" align="center" valign="TOP"></td>
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                                <td style="height:28px; width: 4px;">
                                                                                                                </td>
                                                                                                                <td class="textbold" style="height: 28px">
                                                                                                                    Approval</td>
                                                                                                                <td style="height: 28px">
                                                                                                                    <asp:TextBox ID="txtDateApproval" runat="server" CssClass="textbox" MaxLength="10"
                                                                                                                        TabIndex="15"></asp:TextBox>
                                                                                                                    <img id="imgDateApproval" alt="" src="../Images/calender.gif" title="Date selector"
                                                                                                                       TabIndex="16" style="cursor: pointer" /></td>
                                                                                                                <td class="textbold" style="height: 28px">

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
                                                                                                                <td style="height: 28px">
                                                                                                                    <asp:TextBox ID="txtDateApplied" runat="server" CssClass="textbox" MaxLength="10" TabIndex="17"></asp:TextBox>
                                                                                                                    <img id="imgDateApplied" alt="" src="../Images/calender.gif" TabIndex="18" title="Date selector"
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
                                                                                                                <td style="height: 28px">
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                                 <td  colspan="6" align="center" valign="TOP"></td>
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                                <td style="height:25px; width: 4px;">
                                                                                                                </td>
                                                                                                                <td class="textbold" style="height: 25px">
                                                                                                                    Message Sent</td>
                                                                                                                <td style="height: 25px">
                                                                                                                    <asp:TextBox ID="txtDateMessage" runat="server" CssClass="textbox" MaxLength="10"
                                                                                                                        TabIndex="19"></asp:TextBox>
                                                                                                                    <img id="imgDateMessage" alt="" src="../Images/calender.gif" TabIndex="20" title="Date selector"
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
                                                                                                                <td class="textbold" style="height: 25px">

                                                                                                                   
                                                                                                                   

                                                                                                                    Received<span class="Mandatory">*</span></td>
                                                                                                                <td style="height: 25px">
                                                                                                                    <asp:TextBox ID="txtDateReceived" runat="server" CssClass="textbox" MaxLength="10" TabIndex="21"></asp:TextBox>
                                                                                                                    <img id="imgDateReceived" alt="" src="../Images/calender.gif" TabIndex="22" title="Date selector"
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
                                                                                                                <td style="height: 25px">
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                                 <td colspan="6" align="center" valign="TOP"></td>
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                                <td  style="height:25px; width: 4px;">
                                                                                                                </td>
                                                                                                                <td class="textbold">
                                                                                                                    Exp.
                                                                                                                    Installation</td>
                                                                                                                <td>
                                                                                                                    <asp:TextBox ID="txtDateExp" runat="server" CssClass="textbox" MaxLength="10" TabIndex="23"></asp:TextBox>
                                                                                                                    <img id="imgDateExp" alt="" src="../Images/calender.gif" TabIndex="24" title="Date selector"
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
                                                                                                                    <asp:TextBox ID="txtDateSentBack" runat="server" CssClass="textbox" MaxLength="10" TabIndex="25"></asp:TextBox>
                                                                                                                    <img id="imgDateSentBack" alt="" src="../Images/calender.gif" TabIndex="26" title="Date selector"
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
                                                                                                                <td>
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                                 <td  colspan="6" align="center" valign="TOP"></td>
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                                <td style="width: 4px">
                                                                                                                </td>
                                                                                                                <td>
                                                                                                                </td>
                                                                                                                <td>
                                                                                                                </td>
                                                                                                                <td>
                                                                                                                </td>
                                                                                                                <td>
                                                                                                                </td>
                                                                                                                <td>
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                                 <td  colspan="6" align="center" valign="TOP"></td>
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                                <td style="width: 4px">
                                                                                                                </td>
                                                                                                                <td class="subheading" colspan="4">
                                                                                                                    For Marketing Department</td>
                                                                                                                <td>
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                                 <td   colspan="6" align="center" valign="TOP"></td>
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                                <td  style="height:25px; width: 4px;">
                                                                                                                </td>
                                                                                                                <td class="textbold">
                                                                                                                    Receiving Date</td>
                                                                                                                <td>
                                                                                                                    <asp:TextBox ID="txtDateMdReceiving" runat="server" CssClass="textbox" MaxLength="10" TabIndex="27"></asp:TextBox>
                                                                                                                    <img id="imgDateMdReceiving" alt="" src="../Images/calender.gif" title="Date selector"
                                                                                                                         TabIndex="28" style="cursor: pointer"  runat ="server" />
                                                                                                                          <img id="imgDateMdReceiving2" alt="" src="../Images/calender.gif" title="Date selector"
                                                                                                                       TabIndex="28" style="cursor: pointer" runat ="server" visible ="false"  />
                                                                                                                         <script type="text/javascript">
                                                                                                                                                Calendar.setup({
                                                                                                                                                inputField     :    '<%=txtDateMdReceiving.clientId%>',
                                                                                                                                                ifFormat       :    "%d/%m/%Y",
                                                                                                                                                button         :    "imgDateMdReceiving",
                                                                                                                                                //align          :    "Tl",
                                                                                                                                                singleClick    :    true
                                                                                                                                                });
                                                                                                                                                 </script>
                                                                                                                        </td>
                                                                                                                <td class="textbold">
                                                                                                                    Resending Date</td>
                                                                                                                <td>
                                                                                                                    <asp:TextBox ID="txtDateMdResending" runat="server" CssClass="textbox" MaxLength="10" TabIndex="29"></asp:TextBox>
                                                                                                                    <img id="imgDateMdResending" alt="" src="../Images/calender.gif" title="Date selector"
                                                                                                                         TabIndex="30" style="cursor: pointer"  runat ="server" />
                                                                                                                         <img id="imgDateMdResending2" alt="" src="../Images/calender.gif" title="Date selector"
                                                                                                                       TabIndex="30" style="cursor: pointer" runat ="server" visible ="false"  />
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
                                                                                                                <td>
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                                 <td  colspan="6" align="center" valign="TOP"></td>
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                                <td  style="width: 4px">
                                                                                                                </td>
                                                                                                                <td class="subheading" colspan="4">
                                                                                                                    Others Details</td>
                                                                                                                <td>
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                                 <td  colspan="6" align="center" valign="TOP"></td>
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                                <td style="height:25px; width: 4px;">
                                                                                                                </td>
                                                                                                                <td class="textbold" style="height: 25px">
                                                                                                                    Office ID </td>
                                                                                                                <td style="height: 25px">
                                                                                                                    <asp:TextBox ID="txtOfficeID1"  runat="server" CssClass="textboxgrey"   MaxLength="29" TabIndex="31"></asp:TextBox>
                                                                                                                    <img TabIndex="32" src="../Images/lookup.gif" onclick="javascript:return PopupAgencyGroupOfficeIDAorder();" style="cursor:pointer;"  /></td>
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
                                                                                                                <td rowspan="2" style="height: 14px">
                                                                                                                    <asp:TextBox ID="txtATID" runat="server" CssClass="textbox" Height="50px" MaxLength="20"
                                                                                                                        TabIndex="2" TextMode="MultiLine" Width="132px"></asp:TextBox></td>
                                                                                                                <td style="height: 25px">
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                           <tr>
                                                                                                               <td style="width: 4px; height: 22px">
                                                                                                               </td>
                                                                                                               <td class="textbold" style="height: 22px">
                                                                                                               </td>
                                                                                                               <td style="height: 22px">
                                                                                                               </td>
                                                                                                               <td style="height: 22px">
                                                                                                               </td>
                                                                                                           </tr>
                                                                                                            <tr>
                                                                                                                 <td colspan="6" align="center" valign="TOP"></td>
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                                <td  style="height:25px; width: 4px;">
                                                                                                                </td>
                                                                                                                <td class="textbold">
                                                                                                                    Receiving Office ID</td>
                                                                                                                <td>
                                                                                                                    <asp:TextBox ID="txtReceivingOfficeID" runat="server" CssClass="textbox" MaxLength="20" TabIndex="35"></asp:TextBox></td>
                                                                                                                <td class="textbold">
                                                                                                                    Agency PC Req.</td>
                                                                                                                <td>
                                                                                                                    <asp:TextBox ID="txtAgencyPcReq" runat="server" CssClass="textbox" MaxLength="20" TabIndex="35" Width="129px"></asp:TextBox></td>
                                                                                                                <td>
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                                 <td   colspan="6" align="center" valign="TOP"></td>
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                                <td style="height:14px; width: 4px;">
                                                                                                                </td>
                                                                                                                <td class="textbold" style="height: 14px">
                                                                                                                    Amadeus PC Req</td>
                                                                                                                <td style="height: 14px">
                                                                                                                    <asp:TextBox ID="txtAmadeusPcReq" runat="server" CssClass="textbox" MaxLength="20" TabIndex="36"></asp:TextBox></td>
                                                                                                                <td class="textbold" style="height: 14px">
                                                                                                                    Amadeus Printer Req.</td>
                                                                                                                <td style="height: 14px">
                                                                                                                    <asp:TextBox ID="txtAmadeusPrinterReq" runat="server" CssClass="textbox" MaxLength="20" TabIndex="37"></asp:TextBox></td>
                                                                                                                <td style="height: 14px">
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                                 <td  colspan="6" align="center" valign="TOP"></td>
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                                <td style="width: 4px; height: 65px;">
                                                                                                                </td>
                                                                                                                <td class="textbold" style="height: 65px">
                                                                                                                    Remarks</td>
                                                                                                                <td colspan="3" style="height: 65px">
                                                                                                                    <asp:TextBox ID="txtremarks" runat="server" CssClass="textbox" MaxLength="1000" TabIndex="38" Height="70px" TextMode="MultiLine" Width="437px"></asp:TextBox></td>
                                                                                                                <td style="height: 65px">
                                                                                                                </td>
                                                                                                                
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                                 <td  colspan="6" align="center" valign="TOP"></td>
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                                <td style="height: 25px; width: 4px;">
                                                                                                                </td>
                                                                                                                <td style="height: 25px">
                                                                                                                    <asp:HiddenField ID="hdIspProviderID" runat="server" />
                                                                                                                </td>
                                                                                                                <td style="height: 25px">
                                                                                                                <asp:HiddenField ID="hdIspPlanId" runat="server" /><asp:HiddenField ID="hdOrderStatus" runat="server" />
                                                                                                                </td>
                                                                                                                <td style="height: 25px">
                                                                                                                </td>
                                                                                                                <td style="height: 25px">
                                                                                                                </td>
                                                                                                                <td style="height: 25px">
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                                 <td  colspan="6" align="center" valign="TOP"></td>
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                                <td style="width: 4px">
                                                                                                                    &nbsp;</td>
                                                                                                                <td colspan="4" class="ErrorMsg">
                                                                                                                    Field Marked * are Mandatory</td>
                                                                                                                <td>
                                                                                                                    &nbsp;</td>
                                                                                                            </tr>                                                           
                                                                                                        </table>
                                                                                                    </asp:Panel>
                                                                                                                    <asp:TextBox ID="txtOfficeID2" runat="server" CssClass="textbox" MaxLength="20" TabIndex="33" Visible="False"></asp:TextBox></td>
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
                </td>
            </tr>
             <tr>
             <td  align ="center" valign="top"  style="width:1330px;padding-left:4px;">
                  <table border="0" cellpadding ="0" cellspacing ="0" style="width:1330px">                     
                       <tr>                        
                          <td style="width:1330px;"  valign="top" class="redborder" > 
                              <table border="0" cellpadding ="0" cellspacing ="0" style="width:1330px">
                                  <tr>
                                       <td class="textbold" colspan="6" style="width:1330px; height: 100px;" valign="top">
                                                     <asp:GridView  ID="grdOrder" runat="server"  AutoGenerateColumns="False" TabIndex="6" Width="100%" EnableViewState="true"  AllowSorting ="true"  HeaderStyle-ForeColor="white" >
                                                                              
                                                                              
                                                                                
                                                                      <Columns>
                                                                               <asp:TemplateField HeaderText="Order Number" SortExpression ="ORDER_NUMBER">
                                                                                   <ItemTemplate>
                                                                                       <asp:Label ID="lblLocationCode" runat="server" Text='<%#Eval("ORDER_NUMBER")%>'></asp:Label>
                                                                                   </ItemTemplate>
                                                                                   <ItemStyle HorizontalAlign="Left" Wrap="False" />
                                                                                   <HeaderStyle HorizontalAlign="Left" Width="5%" Wrap="False" />
                                                                               </asp:TemplateField>
                                                                               <asp:TemplateField HeaderText="Type" SortExpression ="ORDER_TYPE_NAME">
                                                                                   <ItemTemplate>
                                                                                       <%#Eval("ORDER_TYPE_NAME")%>
                                                                                   </ItemTemplate>
                                                                                   <ItemStyle HorizontalAlign="Left" Wrap="False" />
                                                                                   <HeaderStyle HorizontalAlign="Left" Width="5%" Wrap="False" />
                                                                               </asp:TemplateField>
                                                                               <asp:TemplateField HeaderText="Status" SortExpression ="ORDER_STATUS_NAME">
                                                                                   <ItemTemplate>
                                                                                        <%#Eval("ORDER_STATUS_NAME")%>
                                                                                   </ItemTemplate>
                                                                                   <ItemStyle HorizontalAlign="Left" Wrap="False" />
                                                                                   <HeaderStyle HorizontalAlign="Left" Width="5%" Wrap="False" />
                                                                               </asp:TemplateField>
                                                                               <asp:TemplateField HeaderText="Approval Date" SortExpression ="APPROVAL_DATE">
                                                                                   <ItemTemplate>
                                                                                         <%#Eval("APPROVAL_DATE")%>
                                                                                         <%--<asp:Label ID="lblAPD" runat="server" Text='<%#Eval("APPROVAL_DATE")%>'></asp:Label> --%>
                                                                                   </ItemTemplate>
                                                                                   <ItemStyle HorizontalAlign="Left" Wrap="False" />
                                                                                   <HeaderStyle HorizontalAlign="Left" Width="5%" Wrap="False" />
                                                                               </asp:TemplateField>
                                                                               <asp:TemplateField HeaderText="Applied Date" SortExpression ="APPLIED_DATE">
                                                                                   <ItemTemplate>
                                                                                         <%#Eval("APPLIED_DATE")%>
                                                                                          <%--<asp:Label ID="lblAD" runat="server" Text='<%#Eval("APPLIED_DATE")%>'></asp:Label>--%> 
                                                                                   </ItemTemplate>
                                                                                   <ItemStyle HorizontalAlign="Left" Wrap="False" />
                                                                                   <HeaderStyle HorizontalAlign="Left" Width="5%" Wrap="False" />
                                                                               </asp:TemplateField>
                                                                               <asp:TemplateField HeaderText="Agency PC" SortExpression ="OPC">
                                                                                   <ItemTemplate>
                                                                                        <%#Eval("OPC")%>
                                                                                   </ItemTemplate>
                                                                                   <ItemStyle HorizontalAlign="Left" Wrap="False" />
                                                                                   <HeaderStyle HorizontalAlign="Left" Width="5%" Wrap="False" />
                                                                               </asp:TemplateField>
                                                                               <asp:TemplateField HeaderText="Amadeus PC" SortExpression ="APC">
                                                                                   <ItemTemplate>
                                                                                        <%#Eval("APC")%>
                                                                                   </ItemTemplate>
                                                                                   <ItemStyle HorizontalAlign="Left" Width="5%" Wrap="False" />
                                                                                   <HeaderStyle HorizontalAlign="Left" Wrap="False" />
                                                                               </asp:TemplateField>
                                                                               <asp:TemplateField HeaderText="Amadeus Printer" SortExpression ="APR">
                                                                                   <ItemTemplate>
                                                                                        <%#Eval("APR")%>
                                                                                   </ItemTemplate>
                                                                                   <ItemStyle HorizontalAlign="Left" Wrap="False" />
                                                                                   <HeaderStyle HorizontalAlign="Left" Width="5%" Wrap="False" />
                                                                               </asp:TemplateField>
                                                                               <asp:TemplateField HeaderText="Remarks"  SortExpression ="REMARKS">
                                                                                   <ItemTemplate>
                                                                                       <%#Eval("REMARKS")%>
                                                                                   </ItemTemplate>
                                                                                   <ItemStyle HorizontalAlign="Left" Wrap="true" Width="40%" />
                                                                                   <HeaderStyle HorizontalAlign="Left" Width="40%" Wrap="False" />
                                                                               </asp:TemplateField>
                                                                               <asp:TemplateField HeaderText="OfficeID" SortExpression ="OFFICEID">
                                                                                   <ItemTemplate>
                                                                                         <%#Eval("OFFICEID")%>
                                                                                   </ItemTemplate>
                                                                                   <ItemStyle HorizontalAlign="Left" Wrap="False" />
                                                                                   <HeaderStyle HorizontalAlign="Left" Width="8%" Wrap="False" />
                                                                               </asp:TemplateField>
                                                                              <%-- <asp:TemplateColumn HeaderText="Office ID2">
                                                                                   <ItemTemplate>
                                                                                        <%#Eval("OFFICEID1")%>
                                                                                   </ItemTemplate>
                                                                                   <ItemStyle HorizontalAlign="Left" Wrap="False" />
                                                                                   <HeaderStyle HorizontalAlign="Left" Width="5%" Wrap="False" />
                                                                               </asp:TemplateColumn>--%>
                                                                               <asp:TemplateField HeaderText="Pending With Employee"  SortExpression ="PENDINGWITHNAME">
                                                                                   <ItemTemplate>
                                                                                         <%#Eval("PENDINGWITHNAME")%>
                                                                                   </ItemTemplate>
                                                                                   <ItemStyle HorizontalAlign="Left" Wrap="False" />
                                                                                   <HeaderStyle HorizontalAlign="Left" Width="8%" Wrap="False" />
                                                                               </asp:TemplateField>
                                                                               <asp:TemplateField HeaderText="Action" HeaderStyle-HorizontalAlign="left"  ItemStyle-HorizontalAlign="left"  >
                                                                                   <ItemTemplate>
                                                                                       <asp:LinkButton ID="lnkEdit" runat="server" CssClass="LinkButtons" Text="Edit" CommandName="EditX" CommandArgument='<%#Eval("ORDERID")%>'></asp:LinkButton>&nbsp;&nbsp;
                                                                                       <asp:LinkButton ID="lnkDelete" runat="server" CssClass="LinkButtons" Text="Delete" CommandName="DeleteX" CommandArgument=' <%#Eval("ORDERID")%>'></asp:LinkButton>&nbsp;&nbsp;
                                                                                       <a href="#" class="LinkButtons" id="linkHistory" runat="server">History</a>&nbsp;&nbsp;
                                                                                       <a href="#" class="LinkButtons" id="linkViewDoc" runat="server">View Doc</a>&nbsp;&nbsp;
                                                                                       <a href="#" class="LinkButtons" id="linkPtype"   runat="server">Ptype</a>
                                                                                       <asp:HiddenField ID="hdOrderId" runat ="server"  Value =' <%#Eval("ORDERID")%>'/>
                                                                                       <asp:HiddenField ID="hdOrderNo" runat ="server"  Value =' <%#Eval("ORDER_NUMBER")%>'/>
                                                                                       <asp:HiddenField ID="hdOrderType" runat ="server"  Value =' <%#Eval("ORDER_TYPE_NAME")%>'/>
                                                                                       <asp:HiddenField ID="hdOrderQty" runat ="server"  Value =' <%#Eval("OPC")%>'/>
                                                                                       <asp:HiddenField ID="hdOrderQtyAPC" runat ="server"  Value =' <%#Eval("APC")%>'/>
                                                                                       
                                                                                       <asp:HiddenField ID="hdOrderRemarks" runat ="server"  Value =' <%#Eval("REMARKS")%>'/>    
                                                                                       <asp:HiddenField ID="hdORDERTYPEID" runat ="server"  Value =' <%#Eval("ISORDERTYPEID")%>'/>    
                                                                                                                                                                          
                                                                                     
                                                                                   </ItemTemplate>
                                                                                   <HeaderStyle HorizontalAlign="Left" Width="20%" Wrap="False" />
                                                                                   <ItemStyle Wrap="False" />
                                                                               </asp:TemplateField>
                                                                           </Columns>                                                                       
                                                                    <AlternatingRowStyle CssClass="lightblue" />
                                                                    <RowStyle CssClass="textbold" />
                                                                    <HeaderStyle CssClass="Gridheading" />
                                                                    <pagersettings  
                                                                      pagebuttoncount="5"/>
                                                                             
                                                   
                                                    
                                                 </asp:GridView>
                                                   
                                                   
                                                  </td>
                                  </tr>
                            </table>                                         
                       </td>
                       </tr>
                        <tr>
                                                                <td colspan="6" >
                                                                 <asp:Panel ID="pnlPaging" runat="server" Visible="false" Width="100%">
                                                      <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                      <tr class="paddingtop paddingbottom"  >                                                                                                                                                
                                                                          <td style="width: 30%" class="left"><span  class="textbold"><b>&nbsp;No. of records found</b></span>&nbsp;&nbsp;<asp:TextBox ID="txtTotalRecordCount" runat ="server"  Width="105px" CssClass="textboxgrey" ReadOnly ="true"  ></asp:TextBox></td>
                                                                          <td style="width: 25%" class="right">                                                                             
                                                                              <asp:LinkButton ID="lnkPrev" CssClass="LinkButtons" runat="server" CommandName="Prev"><< Prev</asp:LinkButton></td>
                                                                          <td style="width: 20%" class="center">
                                                                              <span class ="textbold" ><b>Page No</b></span>&nbsp;&nbsp;<asp:DropDownList ID="ddlPageNumber" Width="70px" CssClass="dropdownlist" runat="server" AutoPostBack="True" >
                                                                              </asp:DropDownList></td>
                                                                          <td style="width: 25%" class="left">
                                                                              <asp:LinkButton ID="lnkNext" runat="server" CssClass="LinkButtons" CommandName="Next">Next >></asp:LinkButton></td>
                                                                      </tr>
                                                                  </table></asp:Panel>
                                                                </td>
                                                            </tr>
                    </table>
             </td>                 
             </tr>
        </table>
    </form>
</body>
</html>
