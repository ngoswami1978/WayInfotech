<%@ Page Language="VB" MaintainScrollPositionOnPostback="true" AutoEventWireup="false" CodeFile="ISPSR_Order.aspx.vb" Inherits="ISP_ISPSR_Order" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>AAMS: Agency</title>
    <link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" media="all" href="../Calender/calendar-blue.css" title="win2k-cold-1" />   
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
<body onload="javascript:OnloadAdvanceSearchForIspOrder();" >
    <form id="form1" runat="server" defaultbutton ="btnSearch" defaultfocus ="txtAgencyName">
    <table cellpadding="0" cellspacing="0" width="100%">
    <tr>
    <td>
    <!--Search Criteria Controls -->
    <table style="width:860px;" cellpadding ="0" cellspacing="0"  class="border_rightred">
            <tr >
                <td valign="top"  style="width:860px;" >
                    <table width="860px" >
                        <tr>
                            <td valign="top" align="left">
                                <span class="menu">ISP-&gt;</span><span class="sub_menu">ISP Order</span></td>
                        </tr>
                        <tr>
                            <td class="heading" align="center" valign="top">
                                Search ISP Order
                            </td>
                        </tr>
                        <tr>
                            <td valign="top">
                                <table width="860px" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td align="LEFT" >
                                        <table width="860px" border="0" cellspacing="0" cellpadding="0">
                                                <tr>
                                                        <td style="width: 860px;" class="redborder" valign="top" >
                                                          <table width="860px" border="0"   align="left" cellpadding="0" cellspacing="0">
                                                          
                        
                                                <tr>
                                                    <td  colspan="6" align="center" style="height: 15px">
                                                        &nbsp;<asp:Label ID="lblError" runat="server" CssClass="ErrorMsg"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="height: 25px">
                                                        &nbsp;</td>
                                                    <td  class="textbold" style="height: 25px">
                                                        Agency Name
                                                        <input type="hidden" id="hdAgencyNameId" runat="server" value="" style="width: 5px" />
                                                        <input type="hidden" id="hdAgencyName" runat="server" value="" style="width: 5px" />
                                                        </td>
                                                    <td colspan="3" style="height: 25px">
                                                        <asp:TextBox ID="txtAgencyName" CssClass="textbox" MaxLength="40" runat="server" Width="481px"  TabIndex="1" ></asp:TextBox> <img  tabIndex="2" src="../Images/lookup.gif" alt="" onclick="javascript:return PopupAgencyPageISPOrder();" style="cursor:pointer;" /></td>
                                                    <td>
                                                        <asp:Button ID="btnSearch" CssClass="button" runat="server" Text="Search" TabIndex="27" AccessKey="A" /></td>
                                                </tr>
                                                <tr>
                                                    <td  style="height: 25px">
                                                    </td>
                                                    <td class="textbold"  style="height: 25px">
                                                        City</td>
                                                    <td  style="height: 25px"><asp:DropDownList ID="ddlCity" runat="server" CssClass="dropdownlist" Width="137px" TabIndex="3">
                                                    </asp:DropDownList></td>
                                                    <td  class="textbold" style="height: 25px">
                                                        Country</td>
                                                    <td  style="height: 25px">
                                                        <asp:DropDownList ID="ddlCountry" runat="server" CssClass="dropdownlist" Width="137px" TabIndex="4">
                                                        </asp:DropDownList></td>
                                                    <td><asp:Button ID="btnNew" CssClass="button" runat="server" Text="New" TabIndex="28" AccessKey="N" /></td>
                                                </tr>
                                                <tr>
                                                    <td height="25px">
                                                        &nbsp;</td>
                                                    <td class="textbold" style="width: 200px">
                                                        NPID</td>
                                                    <td class="textbold" style="width: 200px">
                                                        <asp:TextBox ID="txtNpid" runat="server" CssClass="textboxgrey" ReadOnly="True" TabIndex="5"  ></asp:TextBox>
                                                        <img  tabIndex="6" src="../Images/lookup.gif" alt="" onclick="javascript:return PopupISPplanForIspOrder();"  style="cursor:pointer;" />
                                                        </td>
                                                    <td class="textbold" style="width: 150px">
                                                        Approved By</td>
                                                    <td style="width: 200px"><asp:DropDownList ID="ddlApprovedBy" runat="server" CssClass="dropdownlist" Width="137px" TabIndex="7">
                                                    </asp:DropDownList>&nbsp;</td>
                                                    <td style="width: 110px">
                                                        <asp:Button ID="btnExport" runat="server" CssClass="button" TabIndex="29" Text="Export" AccessKey="E" /></td>
                                                </tr>
                                                <tr>
                                                    <td  height="25px">
                                                        &nbsp;</td>
                                                    <td class="textbold">
                                                        Order Type</td>
                                                    <td colspan="3">
                                                        <asp:DropDownList ID="ddlOrderType" runat="server" CssClass="dropdownlist" Width="486px" TabIndex="8">
                                                        </asp:DropDownList></td>
                                                    <td>
                                                        <asp:Button ID="btnReset" CssClass="button" runat="server" Text="Reset" TabIndex="30" AccessKey="R" /></td>
                                                </tr>
                                                <tr>
                                                    <td style="height: 25px">
                                                    </td>
                                                    <td class="textbold" >
                                                        Order Number</td>
                                                    <td>
                                                        <asp:TextBox ID="txtOrderNumber" runat="server" CssClass="textbox" MaxLength="12" Wrap="False" TabIndex="9"></asp:TextBox></td>
                                                    <td  class="textbold">
                                                        ISP
                                                        Order Status</td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlIspOrderStatus" runat="server" CssClass="dropdownlist" Width="137px" TabIndex="10">
                                                        </asp:DropDownList></td>
                                                    <td>
                                                    </td>
                                                </tr>
                                                  <tr>
                                                    <td  style="height: 25px">
                                                    </td>
                                                    <td class="textbold" >
                                                        <span >Logged by </span>
                                                    </td>
                                                    <td><asp:DropDownList ID="ddlLoggedBy" runat="server" CssClass="dropdownlist" Width="137px" TabIndex="11">
                                                    </asp:DropDownList>&nbsp;
                                                    </td> 
                                                    <td  class="textbold"><span>CAF Account ID</span>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtCafNo" runat="server" CssClass="textbox" MaxLength="25" Wrap="False" Width="131px" TabIndex="12"></asp:TextBox></td>
                                                    <td>
                                                    </td>
                                                </tr>
                                                 <tr>
                                                    <td  style="height: 25px">
                                                    </td>
                                                    <td class="textbold" >
                                                        <span >WLL Number</span></td>
                                                    <td>
                                                        <asp:TextBox ID="txtMDNNo" runat="server" CssClass="textbox" MaxLength="25" Wrap="False" TabIndex="13"></asp:TextBox></td>
                                                    <td  class="textbold">
                                                    </td>
                                                    <td>
                                                        </td>
                                                    <td>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td >
                                                        &nbsp;</td>
                                                    <td colspan="4" class="subheading"> 
                                                        <img src="../Images/down.jpg" id="btnUp" onclick="Javascript:return AdvanceSearchForIspOrder();"  tabIndex="14" width="9" />&nbsp;&nbsp;Advance Search<asp:HiddenField ID="hdAdvanceSearch" runat="server" Value="" />
                                                    </td>
                                                    
                                                    <td >
                                                        &nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td colspan="6">
                                                        <asp:Panel ID="pnlAdvanceSearch" style="display:none"  runat="server" Width="860px">
                                                            <table style="width: 860px" cellpadding="0" cellspacing="0" border="0">
                                                                <tr>
                                                                    <td  style="height: 25px">
                                                                    </td>
                                                                    <td  class="textbold" style="width: 200px">
                                                                        Order Date From</td>
                                                                    <td style="width: 200px" >
                                                                        <asp:TextBox ID="txtOrderDateFrom" CssClass="textbox" MaxLength="10" runat="server" TabIndex="15"></asp:TextBox>
                                                                        <img id="imgOrderDateFrom" alt="" src="../Images/calender.gif" TabIndex="16" title="Date selector" style="cursor: pointer" />
                                                                        <script type="text/javascript">
                                                                                                    Calendar.setup({
                                                                                                    inputField     :    '<%=txtOrderDateFrom.clientId%>',
                                                                                                    ifFormat       :    "%d/%m/%Y",
                                                                                                    button         :    "imgOrderDateFrom",
                                                                                                    //align          :    "Tl",
                                                                                                    singleClick    :    true
                                                                                                    });
                                                                                                  </script>
                                                                        </td>
                                                                    <td style="width: 150px" class="textbox" >
                                                                        Order Date To</td>
                                                                    <td style="width: 200px" >
                                                                        <asp:TextBox ID="txtOrderDateTo" runat="server" CssClass="textbox" MaxLength="10"
                                                                            TabIndex="17" Width="129px"></asp:TextBox>
                                                                        <img id="ImgOrderDateTo" alt="" src="../Images/calender.gif" TabIndex="18" title="Date selector" style="cursor: pointer" />&nbsp;
                                                                         <script type="text/javascript">
                                                                                                    Calendar.setup({
                                                                                                    inputField     :    '<%=txtOrderDateTo.clientId%>',
                                                                                                    ifFormat       :    "%d/%m/%Y",
                                                                                                    button         :    "ImgOrderDateTo",
                                                                                                    //align          :    "Tl",
                                                                                                    singleClick    :    true
                                                                                                    });
                                                                                                  </script>
                                                                        
                                                                        </td>
                                                                    <td style="width: 110px">
                                                                    </td>
                                                                    <td >
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td  style="height: 25px">
                                                                    </td>
                                                                    <td class="textbold" style="height: 25px" >
                                                                        Online Date From</td>
                                                                    <td style="height: 25px" >
                                                                        <asp:TextBox ID="txtOnlineDateFrom" runat="server" CssClass="textbox" MaxLength="10"
                                                                            TabIndex="19"></asp:TextBox>
                                                                        <img id="imgOnlineDateFrom" alt="" src="../Images/calender.gif" TabIndex="20" title="Date selector" style="cursor: pointer" />&nbsp;
                                                                         <script type="text/javascript">
                                                                                                    Calendar.setup({
                                                                                                    inputField     :    '<%=txtOnlineDateFrom.clientId%>',
                                                                                                    ifFormat       :    "%d/%m/%Y",
                                                                                                    button         :    "imgOnlineDateFrom",
                                                                                                    //align          :    "Tl",
                                                                                                    singleClick    :    true
                                                                                                    });
                                                                                                  </script>
                                                                        </td>
                                                                    <td class="textbold" style="height: 25px">Online Date To
                                                                        </td>
                                                                    <td style="height: 25px" >
                                                                        <asp:TextBox ID="txtOnlineDateTo" runat="server" CssClass="textbox" MaxLength="10" TabIndex="21"></asp:TextBox>
                                                                        <img id="ImgOnlineDateTo" alt="" src="../Images/calender.gif" TabIndex="22" title="Date selector" style="cursor: pointer" />  <script type="text/javascript">
                                                                                                    Calendar.setup({
                                                                                                    inputField     :    '<%=txtOnlineDateTo.clientId%>',
                                                                                                    ifFormat       :    "%d/%m/%Y",
                                                                                                    button         :    "ImgOnlineDateTo",
                                                                                                    //align          :    "Tl",
                                                                                                    singleClick    :    true
                                                                                                    });
                                                                                                  </script>
                                                                        </td>
                                                                    <td style="height: 25px">
                                                                    </td>
                                                                    <td style="height: 25px" >
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td  style="height: 25px">
                                                                    </td>
                                                                    <td class="textbold" style="height: 25px" nowrap="nowrap" >
                                                                        Expected Online Date From</td>
                                                                    <td style="height: 25px" >
                                                                        <asp:TextBox ID="txtExpOnlineDateFrom" runat="server" CssClass="textbox" MaxLength="10"
                                                                            TabIndex="23"></asp:TextBox>
                                                                        <img id="imgExpOnlineDateFrom" alt="" src="../Images/calender.gif" TabIndex="24" title="Date selector" style="cursor: pointer" /> <script type="text/javascript">
                                                                                                    Calendar.setup({
                                                                                                    inputField     :    '<%=txtExpOnlineDateFrom.clientId%>',
                                                                                                    ifFormat       :    "%d/%m/%Y",
                                                                                                    button         :    "imgExpOnlineDateFrom",
                                                                                                    //align          :    "Tl",
                                                                                                    singleClick    :    true
                                                                                                    });
                                                                                                  </script></td> 
                                                                    <td  class="textbold" style="height: 25px">Expected Online Date To
                                                                        </td>
                                                                    <td style="height: 25px" >
                                                                        <asp:TextBox ID="txtExpOnlineDateTo" runat="server" CssClass="textbox" MaxLength="10" TabIndex="25"></asp:TextBox>
                                                                        <img id="ImgExpOnlineDateTo" alt="" src="../Images/calender.gif" TabIndex="26" title="Date selector" style="cursor: pointer" /><script type="text/javascript">
                                                                                                    Calendar.setup({
                                                                                                    inputField     :    '<%=txtExpOnlineDateTo.clientId%>',
                                                                                                    ifFormat       :    "%d/%m/%Y",
                                                                                                    button         :    "ImgExpOnlineDateTo",
                                                                                                    //align          :    "Tl",
                                                                                                    singleClick    :    true
                                                                                                    });
                                                                                                  </script> </td>                                
                                                                    <td style="height: 25px">
                                                                    </td>
                                                                       
                                                                    <td style="height: 25px" >
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td  style="height: 25px">
                                                                    </td>
                                                                    <td >
                                                                        </td>
                                                                    <td>
                                                                        &nbsp;
                                                                        
                                                                        </td>
                                                                    <td>
                                                                        </td>
                                                                    <td>
                                                                        &nbsp;
                                                                        
                                                                        
                                                                        </td>
                                                                    <td>
                                                                    </td>
                                                                    <td>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td  style="height: 25px">
                                                                    </td>
                                                                    <td  style="height: 25px">
                                                                        <asp:HiddenField ID="hdISPid" runat="server" /><asp:HiddenField ID="hdIspProviderID" runat="server" />
                                                                        </td>
                                                                    <td style="height: 25px">
                                                                        &nbsp;
                                                                       
                                                                        </td>
                                                                    <td  style="height: 25px">
                                                                        <input id="hdRecordOnCurrentPage" runat="server" style="width: 6px" type="hidden" /></td>
                                                                    <td style="height: 25px" >
                                                                        &nbsp;
                                                                        <asp:HiddenField ID="hdIspPlanId" runat="server" Value="" />
                                                                       
                                                                        
                                                                        </td>
                                                                    <td style="height: 25px">
                                                                    </td>
                                                                    <td style="height: 25px" >
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
                                        
                                          
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
           
                                                 <tr>
                                                    <td colspan="6" >
                                                        <asp:HiddenField ID="hdDeleteISPOrderID" runat="server" />
                                                        <asp:TextBox ID="txtRecordOnCurrentPage" runat="server" CssClass="textboxgrey" Visible="false"
                                                            Width="73px"></asp:TextBox>
                                                    </td> 
                                                </tr> 
        </table>
    
    </td>
    <td></td>
    </tr>
    <tr>
    <td colspan="2">
    <table cellpadding="0" cellspacing="0" width="100%">
    <!--Gridview Row and Paging if available -->
     <tr>
            <td valign="top" style="padding-left:4px;" >
              <table  width="1560px" border="0" align="left" cellpadding="0" cellspacing="0"> 
                                                                            <tr>  
                                                                                    <td class="redborder" valign ="top">
                                                                                      <asp:GridView ID="gvISPOrder" runat="server" AutoGenerateColumns="False" HorizontalAlign="Center"
                                                                                                Width="1560px" HeaderStyle-CssClass="Gridheading" RowStyle-CssClass="ItemColor"
                                                                                                AlternatingRowStyle-CssClass="lightblue" TabIndex="19" AllowSorting="True" RowStyle-VerticalAlign="top"  >
                                                                                                  <Columns>
                                                                                                    <asp:BoundField DataField="OrderNumber" HeaderText="Order Number" SortExpression="OrderNumber" >
                                                                                                        <ItemStyle Wrap="False" />
                                                                                                        <HeaderStyle Wrap="False" />
                                                                                                    </asp:BoundField>
                                                                                                    <asp:BoundField DataField="ISPName" HeaderText="ISP Name" SortExpression="ISPName" >
                                                                                                        <ItemStyle Wrap="true" Width ="150px" />
                                                                                                        <HeaderStyle Wrap="False" />
                                                                                                    </asp:BoundField>
                                                                                                    <asp:BoundField DataField="AgencyName" HeaderText="Agency Name" SortExpression="AgencyName" >
                                                                                                        <ItemStyle Wrap="true" Width ="180px" />
                                                                                                        <HeaderStyle Wrap="False" />
                                                                                                    </asp:BoundField>
                                                                                                    <asp:BoundField DataField="Address" HeaderText="Address" SortExpression="Address" >
                                                                                                        <ItemStyle Wrap="true" Width ="250px"  />
                                                                                                        <HeaderStyle Wrap="False" />
                                                                                                    </asp:BoundField>
                                                                                                    <asp:BoundField DataField="OfficeId" HeaderText="Office ID" SortExpression="OfficeId" >
                                                                                                        <ItemStyle Wrap="False" />
                                                                                                        <HeaderStyle Wrap="False" />
                                                                                                    </asp:BoundField>
                                                                                                    <asp:BoundField DataField="City" HeaderText="City" SortExpression="City"  >
                                                                                                        <ItemStyle Wrap="False" />
                                                                                                        <HeaderStyle Wrap="False" />
                                                                                                    </asp:BoundField>
                                                                                                    <asp:BoundField DataField="ApprovedDate" HeaderText="Approval Date" SortExpression="ApprovedDate" >
                                                                                                        <ItemStyle Wrap="False" />
                                                                                                        <HeaderStyle Wrap="False" />
                                                                                                    </asp:BoundField>
                                                                                                    <asp:BoundField DataField="OrderDate" HeaderText="Order Date" SortExpression="OrderDate" >
                                                                                                        <ItemStyle Wrap="False" />
                                                                                                        <HeaderStyle Wrap="False" />
                                                                                                    </asp:BoundField>
                                                                                                    <asp:BoundField DataField="StatusName" HeaderText="Order Status" SortExpression="StatusName" >
                                                                                                        <ItemStyle Wrap="False" />
                                                                                                        <HeaderStyle Wrap="False" />
                                                                                                    </asp:BoundField>
                                                                                                    <asp:BoundField DataField="OnlineDate" HeaderText="Online Date" SortExpression="OnlineDate"   >
                                                                                                        <ItemStyle Wrap="False" />
                                                                                                        <HeaderStyle Wrap="False" />
                                                                                                    </asp:BoundField> 
                                                                                                     <asp:BoundField DataField="AccountID" HeaderText="Account Id" SortExpression="AccountID"   >
                                                                                                        <ItemStyle Wrap="False" />
                                                                                                        <HeaderStyle Wrap="False" />
                                                                                                    </asp:BoundField>     
                                                                                                     <asp:BoundField DataField="LoginName" HeaderText="Login Name" SortExpression="LoginName"   >
                                                                                                        <ItemStyle Wrap="true" />
                                                                                                        <HeaderStyle Wrap="False" />
                                                                                                    </asp:BoundField>     
                                                                                                     <asp:BoundField DataField="StaticIP" HeaderText="Staic IP" SortExpression="StaticIP"   >
                                                                                                        <ItemStyle Wrap="true" />
                                                                                                        <HeaderStyle Wrap="False" />
                                                                                                    </asp:BoundField>     
                                                                                                     <asp:BoundField DataField="CancellationDate" HeaderText="Cancel Date" SortExpression="CancellationDate"   >
                                                                                                        <ItemStyle Wrap="False" />
                                                                                                        <HeaderStyle Wrap="False" />
                                                                                                    </asp:BoundField>                                                                                                       
                                                                                                    <asp:TemplateField HeaderText ="Action">   
                                                                                                       <ItemTemplate >                                                                                                    
                                                                                                              <a href="#" class="LinkButtons" id="linkEdit" runat="server">Edit</a> &nbsp;&nbsp;<asp:LinkButton ID="btnDelete" runat="server" CommandName ="DeleteX" Text ="Delete" CssClass="LinkButtons"></asp:LinkButton><%--<a href="#" class="LinkButtons" id="linkDelete" runat="server">Delete</a> --%>
                                                                                                              <asp:HiddenField ID="hdISPOrderID" runat="server" Value='<%#Eval("ISPOrderID")%>' />   
                                                                                                               <asp:HiddenField ID="hdLCODE" runat="server" Value='<%#Eval("LCODE")%>' />   
                                                                                                              
                                                                                                        </ItemTemplate>
                                                                                                        <ItemStyle Wrap="False" />
                                                                                                        <HeaderStyle Wrap="False" />
                                                                                                      </asp:TemplateField>   
                                                                                                </Columns>
                                                                                                <AlternatingRowStyle CssClass="lightblue" />
                                                                                                <HeaderStyle CssClass="Gridheading" HorizontalAlign="Left" Wrap="False" ForeColor="white" />
                                                                                                <RowStyle CssClass="textbold" />
                                                                                       </asp:GridView>
                                                                                       <!-- Design for paging. -->
                                                                                      </td>                                                                 
                                                                            </tr> 
                                                                    </table>
            </td>
            </tr>
               <tr>                                                   
                                                    <td colspan="6" valign ="top"  style="width:840px;" >
                                                    <asp:Panel ID="pnlPaging" runat="server" Visible="false" Width="100%">
                                                      <table border="0" cellpadding="0" cellspacing="0" width="840px">
                                                                      <tr style="padding-top : 4pt; padding-bottom : 4pt">                                                                                                                                                
                                                                          <td style="width: 30%" class="left"><span  class="textbold"><b>&nbsp;No. of records found</b></span>&nbsp;&nbsp;<asp:TextBox ID="txtTotalRecordCount" runat="server" CssClass="textboxgrey" MaxLength="3" TabIndex="3" Width="100px" ReadOnly="True" Text ="0" Visible="True"></asp:TextBox></td>
                                                                          <td style="width: 25%" class="right">                                                                             
                                                                              <asp:LinkButton ID="lnkPrev" CssClass="LinkButtons" runat="server" CommandName="Prev"><< Prev</asp:LinkButton></td>
                                                                          <td style="width: 20%" class="center">
                                                                              <span class ="textbold" ><b>Page No</b></span>&nbsp;&nbsp;<asp:DropDownList  onkeyup="gotop(this.id)"  ID="ddlPageNumber" Width="70px" CssClass="dropdownlist" runat="server" AutoPostBack="True" >
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
