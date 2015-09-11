<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ISPSR_PendingCancellation.aspx.vb" Inherits="ISP_ISPSR_PendingCancellation" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>AAMS: Agency</title>
    <link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" media="all" href="../Calender/calendar-blue.css" title="win2k-cold-1" />   
    <link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />
    
<script type="text/javascript" src="../JavaScript/AAMS.js"></script>
<!-- import the calendar script -->
<script type="text/javascript" src="../Calender/calendar.js"></script>
<!-- import the language module -->
<script type="text/javascript" src="../Calender/lang/calendar-en.js"></script>
<!-- import the calendar setup module -->
<script type="text/javascript" src="../Calender/calendar-setup.js"></script>

 <script language="javascript" type="text/javascript">
  
 function EditFunction(CheckBoxObj,CheckBoxObj2)
    {           
          window.location.href="ISPUP_PendingCancellation.aspx?IspOrderId=" + CheckBoxObj + "&Lcode=" + CheckBoxObj2 ;               
          return false;
    }
    function DeleteFunction(CheckBoxObj)
    {   
               if (confirm("Are you sure you want to delete?")==true)
                  {        
                    document.getElementById("hdDeleteISPOrderID").value= CheckBoxObj ;   
                  }
                else
                {
                 document.getElementById("hdDeleteISPOrderID").value="";
                 return false;
                }
    }
 
 
     
    </script>
</head>
   
<body onload="javascript:OnloadAdvanceSearchIspPendingCancellation();" >
    <form id="form1" runat="server" defaultbutton ="btnSearch" defaultfocus ="txtAgencyName">
        <table style="width:860px;" cellpadding ="0" cellspacing="0"  class="border_rightred">
            <tr >
                <td valign="top"  style="width:860px;" >
                    <table width="860px" >
                        <tr>
                            <td valign="top" align="left">
                                <span class="menu">ISP-&gt;</span><span class="sub_menu">Search ISP Pending Cancellation</span></td>
                        </tr>
                        <tr>
                            <td class="heading" align="center" valign="top">
                                Search ISP Pending Cancellation&nbsp;</td>
                        </tr>
                        <tr>
                            <td valign="top">
                                <table width="860px" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td align="LEFT" >
                                        <table width="860px" border="0" cellspacing="2" cellpadding="0">
                                                <tr>
                                                        <td style="width: 860px;" class="redborder" valign="top" >
                                                          <table width="860px" border="0"   cellpadding="2" cellspacing="0">
                                                          <tr>
                                                    <td colspan="6" align="center" style="height: 15px">
                                                        <asp:Label ID="lblError" runat="server" CssClass="ErrorMsg"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td  rowspan="8" style="width: 60px" > &nbsp;</td>
                                                    <td class="textbold" >Agency Name
                                                        <input type="hidden" id="hdAgencyNameId" runat="server" value="" style="width: 5px" />
                                                        <input type="hidden" id="hdAgencyName" runat="server" value="" style="width: 5px" />
                                                        </td>
                                                    <td colspan="3" ><asp:TextBox ID="txtAgencyName" CssClass="textbox" MaxLength="40" runat="server" TabIndex="1" Width="452px" ></asp:TextBox> <img  tabIndex="2" src="../Images/lookup.gif" alt="" onclick="javascript:return PopupAgencyPageIspPendingCancellation();" style="cursor:pointer;" /></td>
                                                    <td><asp:Button ID="btnSearch" CssClass="button" runat="server" Text="Search" TabIndex="25" AccessKey="A" /></td>
                                                </tr>
                                                <tr>
                                                    <td class="textbold" style="width: 140px" >City</td>
                                                    <td style="width: 180px" ><asp:DropDownList ID="ddlCity" runat="server" CssClass="dropdownlist" Width="137px" TabIndex="3">
                                                    </asp:DropDownList></td>
                                                    <td class="textbold" style="width: 140px" >Country</td>
                                                    <td style="width: 180px" ><asp:DropDownList ID="ddlCountry" runat="server" CssClass="dropdownlist" Width="137px" TabIndex="4">
                                                        </asp:DropDownList></td>
                                                    <td style="width: 180px;"><asp:Button ID="btnExport" runat="server" CssClass="button" TabIndex="27" Text="Export" AccessKey="E" /></td>
                                                </tr>
                                                <tr>
                                                    <td class="textbold">NPID</td>
                                                    <td class="textbold"><asp:TextBox ID="txtNpid" runat="server" CssClass="textboxgrey" ReadOnly="True" Width="134px"  ></asp:TextBox>
                                                        <img  tabIndex="2" src="../Images/lookup.gif" alt="" onclick="javascript:return PopupIspPendingCancellation();"  style="cursor:pointer;" />
                                                        </td>
                                                    <td class="textbold">Approved By</td>
                                                    <td><asp:DropDownList ID="ddlApprovedBy" runat="server" CssClass="dropdownlist" Width="137px" TabIndex="6">
                                                    </asp:DropDownList>&nbsp;</td>
                                                    <td ><asp:Button ID="btnReset" CssClass="button" runat="server" Text="Reset" TabIndex="28" AccessKey="R" /></td>
                                                </tr>
                                                <tr>
                                                    <td class="textbold">Order Type</td>
                                                    <td colspan="3"><asp:DropDownList ID="ddlOrderType" runat="server" CssClass="dropdownlist" Width="460px" TabIndex="7">
                                                        </asp:DropDownList></td>
                                                    <td rowspan="5" >          &nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td class="textbold" >Order Number</td>
                                                    <td><asp:TextBox ID="txtOrderNumber" runat="server" CssClass="textbox" MaxLength="12" Wrap="False" TabIndex="8" Width="134px"></asp:TextBox></td>
                                                    <td  class="textbold">ISP Order Status</td>
                                                    <td><asp:DropDownList ID="ddlIspOrderStatus" runat="server" CssClass="dropdownlist" Width="137px" TabIndex="9">
                                                        </asp:DropDownList></td>
                                                </tr>
                                                  <tr>
                                                    <td class="textbold" ><span >Logged by </span>
                                                    </td><td><asp:DropDownList ID="ddlLoggedBy" runat="server" CssClass="dropdownlist" Width="139px" TabIndex="10">
                                                    </asp:DropDownList>
                                                    </td> 
                                                    <td  class="textbold"><span>CAF Account ID</span>
                                                    </td>
                                                    <td><asp:TextBox ID="txtCafNo" runat="server" CssClass="textbox" MaxLength="25" Wrap="False" Width="131px" TabIndex="11"></asp:TextBox></td>
                                                </tr>
                                                 <tr>
                                                    <td class="textbold" ><span >WLL Number</span></td>
                                                    <td><asp:TextBox ID="txtMDNNo" runat="server" CssClass="textbox" MaxLength="25" Wrap="False" TabIndex="12" Width="134px"></asp:TextBox></td>
                                                    <td  ></td>
                                                    <td></td>
                                                </tr>
                                                <tr>
                                                    <td colspan="4" class="subheading"> <img src="../Images/down.jpg" id="btnUp" onclick="Javascript:return AdvanceSearchIspPendingCancellation();"  tabIndex="13" width="9" />&nbsp;&nbsp;Advance Search<asp:HiddenField ID="hdAdvanceSearch" runat="server" Value="" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="6">
                                                        <asp:Panel ID="pnlAdvanceSearch" style="display:none"  runat="server" Width="100%">
                                                            <table  cellpadding="0" cellspacing="0" border="0">
                                                                <tr>
                                                                    <td  class="textbold" >
                                                                    </td>
                                                                    <td class="textbold" style="width: 60px">
                                                                    </td>
                                                                    <td class="textbold" style="width: 140px">
                                                                        Order Date From</td>
                                                                    <td width="20%" style="width: 180px">
                                                                        <asp:TextBox ID="txtOrderDateFrom" CssClass="textbox" MaxLength="10" runat="server" TabIndex="14" Width="107px"></asp:TextBox>
                                                                        <img id="imgOrderDateFrom" alt="" src="../Images/calender.gif" TabIndex="14" title="Date selector" style="cursor: pointer" />
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
                                                                    <td width="18%" style="width: 140px">
                                                                      Order Date To</td>
                                                                    <td width="20%" style="width: 180px">
                                                                        <asp:TextBox ID="txtOrderDateTo" runat="server" CssClass="textbox" MaxLength="10"
                                                                            TabIndex="15" Width="112px"></asp:TextBox>
                                                                        <img id="ImgOrderDateTo" alt="" src="../Images/calender.gif" TabIndex="16" title="Date selector" style="cursor: pointer" />&nbsp;
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
                                                                    <td width="18%" style="width: 180px">
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="textbold" >
                                                                    </td>
                                                                    <td class="textbold">
                                                                    </td>
                                                                    <td class="textbold"  >
                                                                        <span >Online Date From</span></td>
                                                                    <td >
                                                                        <asp:TextBox ID="txtOnlineDateFrom" runat="server" CssClass="textbox" MaxLength="10"
                                                                            TabIndex="17" Width="106px"></asp:TextBox>
                                                                        <img id="imgOnlineDateFrom" alt="" src="../Images/calender.gif" TabIndex="18" title="Date selector" style="cursor: pointer" />&nbsp;
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
                                                                    <td class="textbold" >Online Date To
                                                                        </td>
                                                                    <td  >
                                                                        <asp:TextBox ID="txtOnlineDateTo" runat="server" CssClass="textbox" MaxLength="10" TabIndex="19" Width="115px"></asp:TextBox>
                                                                        <img id="ImgOnlineDateTo" alt="" src="../Images/calender.gif" TabIndex="20" title="Date selector" style="cursor: pointer" />  <script type="text/javascript">
                                                                                                    Calendar.setup({
                                                                                                    inputField     :    '<%=txtOnlineDateTo.clientId%>',
                                                                                                    ifFormat       :    "%d/%m/%Y",
                                                                                                    button         :    "ImgOnlineDateTo",
                                                                                                    //align          :    "Tl",
                                                                                                    singleClick    :    true
                                                                                                    });
                                                                                                  </script>
                                                                        </td>
                                                                    <td  >
                                                                    </td>
                                                                </tr>
                                         
                                                               
                                                                <tr>
                                                                    <td  ></td>
                                                                    <td>
                                                                    </td>
                                                                    <td ><asp:HiddenField ID="hdISPid" runat="server" /><asp:HiddenField ID="hdIspProviderID" runat="server" />
                                                                        </td>
                                                                    <td ></td>
                                                                    <td ><input id="hdRecordOnCurrentPage" runat="server" style="width: 6px" type="hidden" /></td>
                                                                    <td ><asp:HiddenField ID="hdIspPlanId" runat="server" Value="" /></td>
                                                                    <td ></td>
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
            <td valign="top" style="padding-left:3px;" >
              <table  width="854px" border="0" align="left" cellpadding="0" cellspacing="0"> 
                                                                            <tr>  
                                                                                    <td class="redborder" valign ="top">
                                                                                      <asp:GridView ID="gvISPPendingCan" Width="850px" runat="server" AutoGenerateColumns="False" HorizontalAlign="Center" HeaderStyle-CssClass="Gridheading" RowStyle-CssClass="ItemColor"
                                                                                                AlternatingRowStyle-CssClass="lightblue" TabIndex="19" AllowSorting="True" UseAccessibleHeader="False" >
                                                                                                  <Columns>
                                                                                                    <asp:BoundField DataField="Sno" HeaderText="S. No." SortExpression="Sno" >
                                                                                                        <ItemStyle Wrap="False"  Width="50px" />
                                                                                                        <HeaderStyle Wrap="False"  Width="50px" />
                                                                                                    </asp:BoundField>
                                                                                                    <asp:BoundField DataField="OFFICEID" HeaderText="OfficeId" SortExpression="OFFICEID" >
                                                                                                        <ItemStyle Wrap="False"  Width="150px"/>
                                                                                                        <HeaderStyle Wrap="False"  Width="150px" />
                                                                                                    </asp:BoundField>
                                                                                                    <asp:BoundField DataField="AgencyName" HeaderText="Agency Name" SortExpression="AgencyName" >
                                                                                                        <ItemStyle Wrap="True"  />
                                                                                                        <HeaderStyle Wrap="False"   />
                                                                                                    </asp:BoundField>                                                                                                                                                                                                   
                                                                                                    <asp:TemplateField HeaderText ="Action"   >   
                                                                                                       <ItemTemplate >                                                                                                    
                                                                                                              <a href="#" class="LinkButtons" id="linkEdit" runat="server">Edit</a> &nbsp;&nbsp;<asp:LinkButton ID="btnDelete" runat="server" CommandName ="DeleteX" Text ="Delete" CssClass="LinkButtons"></asp:LinkButton>
                                                                                                              <asp:HiddenField ID="hdISPOrderID" runat="server" Value='<%#Eval("ISPOrderID")%>' />   
                                                                                                               <asp:HiddenField ID="hdLCODE" runat="server" Value='<%#Eval("LCODE")%>' />   
                                                                                                              
                                                                                                        </ItemTemplate>
                                                                                                        <ItemStyle Wrap="False" Width="120px" />
                                                                                                        <HeaderStyle Wrap="False" Width="120px"/>
                                                                                                      </asp:TemplateField>   
                                                                                                </Columns>
                                                                                                <AlternatingRowStyle CssClass="lightblue" />
                                                                                                <HeaderStyle CssClass="Gridheading" HorizontalAlign="Left" Wrap="False" ForeColor="White" />
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
                                                    <asp:Panel ID="pnlPaging" runat="server" Visible="false" Width="840px">
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
                                                 <tr>
                                                    <td colspan="6" >
                                                        <asp:HiddenField ID="hdDeleteISPOrderID" runat="server" />
                                                        <asp:TextBox ID="txtRecordOnCurrentPage" runat="server" CssClass="textboxgrey" Visible="false"
                                                            Width="73px"></asp:TextBox>
                                                    </td> 
                                                </tr> 
        </table>
    </form>
</body>
</html>
