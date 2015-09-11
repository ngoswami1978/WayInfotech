<%@ Page Language="VB" AutoEventWireup="false" CodeFile="HDSR_WorkOrder.aspx.vb" Inherits="BirdresHelpDesk_HDSR_WorkOrder" MaintainScrollPositionOnPostback="true" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
   <title>AAMS::HelpDesk::Search Work Order</title>
    <link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />
    <link href="../Calender/calendar-blue.css" rel="stylesheet" type="text/css" />
    <!-- import the calendar script -->
<script type="text/javascript" src="../Calender/calendar.js"></script>
<!-- import the language module -->
<script type="text/javascript" src="../Calender/lang/calendar-en.js"></script>
<!-- import the calendar setup module -->

<script type="text/javascript" src="../Calender/calendar-setup.js"></script>
<script type="text/javascript" src="../JavaScript/AAMS.js"></script>
<script language ="javascript" type ="text/javascript">

  function SelectFunction(str3)
        {   
            //alert(str3);
            var pos=str3.split('|'); 
            if (window.opener.document.forms['form1']['hdWorkOrderNo']!=null)
            {
            window.opener.document.forms['form1']['hdWorkOrderNo'].value=pos[1];
            window.opener.document.forms['form1']['txtWorkOrderNo'].value=pos[1];
            
            }
               
            window.close();
       }
function PopupAgencyPage()
{
          var type;
            type = "../TravelAgency/TASR_BRAgency.aspx?Popup=T&HelpDeskType=BR" ;
   	        window.open(type,"aa","height=600,width=880,top=30,left=20,scrollbars=1,status=1");	
            return false;
}
function PopupEmployeePage()
{
          var type;
          var strEmployeePageName=document.getElementById("hdEmployeePageName").value;
          if (strEmployeePageName!="")
          {
            type = "../Setup/" + strEmployeePageName+ "?Popup=T" ;
            //type = "../Setup/MSSR_Employee.aspx?Popup=T" ;
   	        window.open(type,"aa","height=600,width=880,top=30,left=20,scrollbars=1,status=1");	
            return false;
         }
}

 function Edit(OrderID)
			{
				 window.location.href="HDUP_WorkOrder.aspx?Action=U&OrderID=" +OrderID
				 return false;
			}
			
	function Delete(OrderID)
	{
		 			 
		 if (confirm("Are you sure you want to delete?")==true)
            {    
               document.getElementById('<%=hdWOrderId.ClientId%>').value = OrderID
               return true;        
            }
            return false;
	}
</script>
    </head>
<body>
    <form id="form1"  defaultfocus="txtAgencyName" runat="server" defaultbutton="btnSearch" >
    <table  class="left">
    <tr>
    <td>
        <table width="860px" align="left" class="border_rightred" >
            <tr>
                <td valign="top">
                    <table width="100%" align="left">
                        <tr>
                            <td valign="top"  align="left">
                            <span class="menu">
Birdres HelpDesk-&gt;</span><span class="sub_menu">Work Order Search</span></td>
                        </tr>
                        <tr>
                            <td class="heading" align="center" valign="top">
                                Work Order Search
                            </td>
                        </tr>
                        <tr>
                            <td valign="top">
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td align="LEFT" class="redborder">
                                
                                                        <table width="100%" border="0" align="left" cellpadding="0" cellspacing="0" >
                                                            <tr>
                                                                <td  class="textbold" colspan="6" style="height: 15px" align="center" >
                                                          <asp:Label ID="lblError" runat="server" CssClass="ErrorMsg" EnableViewState="False" ></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="textbold" style="width: 40px; height: 25px">
                                                                </td>
                                                                <td class="textbold" style="width: 119px; height: 25px">
                                                                    Agency Name</td>
                                                                <td colspan="3" style="height: 25px">
                                                                    <asp:TextBox ID="txtAgencyName" CssClass="textbox" MaxLength="50" runat="server" Width="460px" TabIndex="1"></asp:TextBox>
                                                                        <img id="hdSpan" runat="server" alt=""  onclick="javascript:return PopupAgencyPage();" src="../Images/lookup.gif" style="cursor:pointer;"

 /></td>
                                                                <td style="height: 25px">
                                                                    <asp:Button ID="btnSearch" CssClass="button" runat="server" Text="Search" TabIndex="17" AccessKey="a" /></td>
                                                            </tr>
                                                            <tr>
                                                                <td class="textbold" style="width: 40px; height: 25px">
                                                                </td>
                                                                <td class="textbold" style="width: 119px; height: 25px">
                                                                    Work Order Title</td>
                                                                <td colspan="3" style="height: 25px">
                                                                    <asp:TextBox ID="txtOrderTitle" runat="server" CssClass="textbox" MaxLength="300" TabIndex="2"
                                                                        Width="460px"></asp:TextBox></td>
                                                                <td style="height: 25px">
                                                                    <asp:Button ID="btnReset" CssClass="button" runat="server" Text="Reset" TabIndex="18" AccessKey="r" /></td>
                                                            </tr>
                                                            
                                                            <tr>
                                                                <td class="textbold" style="height: 25px; width: 40px;">
                                                                    &nbsp;</td>
                                                                <td class="textbold" style="height: 25px; width: 119px;">
                                                                    LTR No.</td>
                                                                <td style="width: 214px; height: 25px;">
                                                                    <asp:TextBox ID="txtLTRNo" runat="server"  TabIndex="3" CssClass="textbox" MaxLength="9"></asp:TextBox></td>
                                                                <td class="textbold" style="height: 25px; width: 116px;">
                                                                    Work Order No.</td>
                                                                <td style="height: 25px; width: 213px;">
                                                                    <asp:TextBox ID="txtOrderNo" runat="server" CssClass="textbox" MaxLength="9" TabIndex="4"></asp:TextBox></td>
                                                                <td style="height: 25px">
                                                                    <asp:Button ID="btnExport" runat="server" CssClass="button" TabIndex="3" Text="Export" AccessKey="e" /></td>
                                                            </tr>
                                                            <tr>
                                                                <td class="textbold" style="width: 40px; height: 25px">
                                                                </td>
                                                                <td class="textbold" style="width: 119px; height: 25px">
                                                                    Status</td>
                                                                <td style="width: 214px; height: 25px">
                                                                    <asp:DropDownList ID="drpStatus" runat="server" CssClass="dropdownlist" TabIndex="5"
                                                                        Width="137px" onkeyup="gotop(this.id)">
                                                                    </asp:DropDownList></td>
                                                                <td class="textbold" style="width: 116px; height: 25px">
                                                                    Severity</td>
                                                                <td style="width: 213px; height: 25px">
                                                                    <asp:DropDownList ID="drpSeverity" runat="server" CssClass="dropdownlist" TabIndex="6"
                                                                        Width="137px" onkeyup="gotop(this.id)">
                                                                    </asp:DropDownList></td>
                                                                <td style="height: 25px">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="textbold" style="width: 40px; height: 25px">
                                                                </td>
                                                                <td class="textbold" style="width: 119px; height: 25px">
                                                                    Follow Up</td>
                                                                <td style="width: 214px; height: 25px">
                                                                    <asp:DropDownList ID="drpFollowUp" runat="server" CssClass="dropdownlist" TabIndex="7"
                                                                        Width="137px" onkeyup="gotop(this.id)">
                                                                    </asp:DropDownList></td>
                                                                <td class="textbold" style="width: 116px; height: 25px">
                                                                    Logged By</td>
                                                                <td style="width: 213px; height: 25px">
                                                                    <asp:TextBox ID="txtLoggedBy" runat="server" CssClass="textbox" MaxLength="50" TabIndex="8"></asp:TextBox>
                                                                    <img id="Img6" runat="server" alt=""  onclick="javascript:return PopupEmployeePage();" src="../Images/lookup.gif" style="cursor:pointer;" /></td>
                                                                <td style="height: 25px">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="textbold" style="width: 40px; height: 25px">
                                                                </td>
                                                                <td class="textbold" style="width: 119px; height: 25px">
                                                                    Assigned To</td>
                                                                <td style="width: 214px; height: 25px">
                                                                    <asp:DropDownList ID="drpAssignedTo" runat="server" CssClass="dropdownlist" TabIndex="9"
                                                                        Width="137px" onkeyup="gotop(this.id)">
                                                                    </asp:DropDownList></td>
                                                                <td class="textbold" style="width: 116px; height: 25px">
                                                                    Assigned Date</td>
                                                                <td style="width: 213px; height: 25px">
                                                                    <asp:TextBox ID="txtAssignedDate" runat="server" CssClass="textbox" MaxLength="10" TabIndex="10"></asp:TextBox>
                                                                    <img id="f_trigger_d1" alt="" src="../Images/calender.gif" style="cursor: pointer" title="Date selector" />
                                                                    <script type="text/javascript">
                                                                                                    Calendar.setup({
                                                                                                    inputField     :    '<%=txtAssignedDate.clientId%>',
                                                                                                    ifFormat       :    "%d/%m/%Y",
                                                                                                    button         :    "f_trigger_d1",
                                                                                                    //align          :    "Tl",
                                                                                                    singleClick    :    true
                                                                                                    });
                                                                                                  </script>
                                                                    </td>
                                                                <td style="height: 25px">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="textbold" style="width: 40px; height: 25px">
                                                                </td>
                                                                <td class="textbold" style="width: 119px; height: 25px">
                                                                    Work
                                                                    Order Type</td>
                                                                <td style="width: 214px; height: 25px">
                                                                    <asp:DropDownList ID="drpOrderType" runat="server" CssClass="dropdownlist" TabIndex="11"
                                                                        Width="137px" onkeyup="gotop(this.id)">
                                                                    </asp:DropDownList></td>
                                                                <td class="textbold" style="width: 116px; height: 25px">
                                                                    1AOffice</td>
                                                                <td style="width: 213px; height: 25px">
                                                                    <asp:DropDownList ID="drpAoffice" runat="server" CssClass="dropdownlist" TabIndex="12"
                                                                        Width="137px" onkeyup="gotop(this.id)">
                                                                    </asp:DropDownList></td>
                                                                <td style="height: 25px">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="textbold" style="height: 13px; width: 40px;">
                                                                </td>
                                                                <td class="textbold" style="height: 13px; width: 119px;">
                                                                    Open Date From</td>
                                                                <td style="height: 13px; width: 214px;">
                                                                    <asp:TextBox ID="txtOpenDateFrom"  TabIndex="13" runat="server" CssClass="textbox" MaxLength="10"></asp:TextBox>
                                                                        <img id="Img2" alt="" src="../Images/calender.gif"  title="Date selector" style="cursor: pointer" />

                                                                    <script type="text/javascript">
                                                                                                    Calendar.setup({
                                                                                                    inputField     :    '<%=txtOpenDateFrom.clientId%>',
                                                                                                    ifFormat       :    "%d/%m/%Y",
                                                                                                    button         :    "Img2",
                                                                                                    //align          :    "Tl",
                                                                                                    singleClick    :    true
                                                                                                    });
                                                                                                  </script></td>
                                                                <td class="textbold" style="height: 13px; width: 116px;">
                                                                    Open Date To</td>
                                                                <td style="height: 13px; width: 213px;">
                                                                    <asp:TextBox ID="txtOpenDateTo" runat="server" CssClass="textbox" MaxLength="10" TabIndex="14"></asp:TextBox>
                                                                    <img id="Img1" alt="" src="../Images/calender.gif" style="cursor: pointer"  
                                                                        title="Date selector" /><script type="text/javascript">
                                                                                                    Calendar.setup({
                                                                                                    inputField     :    '<%=txtOpenDateTo.clientId%>',
                                                                                                    ifFormat       :    "%d/%m/%Y",
                                                                                                    button         :    "Img1",
                                                                                                    //align          :    "Tl",
                                                                                                    singleClick    :    true
                                                                                                    });
                                                                                                  </script></td>
                                                                <td style="height: 13px"></td>
                                                            </tr>
                                                            <tr>
                                                                <td class="textbold" style="width: 40px; height: 13px">
                                                                </td>
                                                                <td class="textbold" style="width: 119px; height: 13px">
                                                                    Close Date From</td>
                                                                <td style="width: 214px; height: 13px">
                                                                    <asp:TextBox ID="txtCloseDateFrom" runat="server" CssClass="textbox" MaxLength="10" TabIndex="15"></asp:TextBox>
                                                                    <img id="Img3" alt="" src="../Images/calender.gif"  title="Date selector" style="cursor: pointer" />
                                                                    <script type="text/javascript">
                                                                                                    Calendar.setup({
                                                                                                    inputField     :    '<%=txtCloseDateFrom.clientId%>',
                                                                                                    ifFormat       :    "%d/%m/%Y",
                                                                                                    button         :    "Img3",
                                                                                                    //align          :    "Tl",
                                                                                                    singleClick    :    true
                                                                                                    });
                                                                                                  </script>
                                                                    </td>
                                                                <td class="textbold" style="width: 116px; height: 13px">
                                                                    Close Date To&nbsp;</td>
                                                                <td style="width: 213px; height: 13px">
                                                                    <asp:TextBox ID="txtCloseDateTo" runat="server" CssClass="textbox" MaxLength="10" TabIndex="16"></asp:TextBox>
                                                                    <img id="Img5" alt="" src="../Images/calender.gif" style="cursor: pointer" title="Date selector" />
                                                                    <script type="text/javascript">
                                                                                                    Calendar.setup({
                                                                                                    inputField     :    '<%=txtCloseDateTo.clientId%>',
                                                                                                    ifFormat       :    "%d/%m/%Y",
                                                                                                    button         :    "Img5",
                                                                                                    //align          :    "Tl",
                                                                                                    singleClick    :    true
                                                                                                    });
                                                                                                  </script>
                                                                    </td>
                                                                <td style="height: 13px">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="textbold" style="width: 40px; height: 13px">
                                                                </td>
                                                                <td class="textbold" style="width: 119px; height: 13px">
                                                                    </td>
                                                                <td style="width: 214px; height: 13px">
                                                                </td>
                                                                <td class="textbold" style="width: 116px; height: 13px">
                                                                </td>
                                                                <td style="width: 213px; height: 13px">
                                                                </td>
                                                                <td style="height: 13px">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="textbold" style="width: 40px; height: 13px">
                                                                </td>
                                                                <td class="textbold" style="width: 119px; height: 13px">
                                                                </td>
                                                                <td style="width: 214px; height: 13px">
                                                                </td>
                                                                <td class="textbold" style="width: 116px; height: 13px">
                                                                </td>
                                                                <td style="width: 213px; height: 13px">
                                                                </td>
                                                                <td style="height: 13px">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="textbold" style="width: 40px; height: 13px">
                                                                </td>
                                                                <td class="textbold" style="width: 119px; height: 13px">
                                                                <asp:HiddenField ID="hdAgencyID" runat="server" />
                                                                    &nbsp;&nbsp;
                                                                </td>
                                                                <td style="width: 214px; height: 13px"><asp:HiddenField ID="hdLoggedBy" runat="server" />
                                                                </td>
                                                                <td class="textbold" style="width: 116px; height: 13px"><asp:HiddenField ID="hdWOrderId" runat="server" />
                                                                </td>
                                                                <td style="width: 213px; height: 13px">
                                                                    <input id="hdRecordOnCurrentPage" runat="server" style="width: 6px" type="hidden" />                                                                   
                                                                    <input type="hidden" id="hdEmployeePageName" style="width:1px" runat='server' />
                                                                    </td>
                                                                <td style="height: 13px">
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
<td class="redborder top">
 <asp:GridView ID="gvOrder" runat="server" Width="1500px"  AutoGenerateColumns="False" TabIndex="19"  EnableViewState="False" AllowSorting="True" >
           <Columns>
               
               <asp:TemplateField HeaderText="Agency Name" SortExpression="AGENCYNAME">
             <ItemTemplate>
             <asp:Label ID="lblAgency" runat="server" Text='<%# Eval("AGENCYNAME") %>' Width="200px"></asp:Label>
             </ItemTemplate>
                   <ItemStyle Width="200px" Wrap="True" />
             </asp:TemplateField>
               <asp:TemplateField HeaderText="Address" SortExpression="ADDRESS" >
             <ItemTemplate>
             <asp:Label ID="lblAddress" runat="server" Text='<%# Eval("ADDRESS") %>' Width="200px"></asp:Label>
             </ItemTemplate>
                   <ItemStyle Width="200px" Wrap="True" />
             </asp:TemplateField>
               
              <asp:TemplateField HeaderText="Order No." SortExpression="WO_NUMBER">
             <ItemTemplate>
             <asp:Label ID="lblOrderNumber" runat="server" Text='<%# Eval("WO_NUMBER") %>' Width="100px"></asp:Label>
             </ItemTemplate>
                   <ItemStyle Width="100px" Wrap="True" />
             </asp:TemplateField>
               
               <asp:TemplateField HeaderText="Order Title" SortExpression="WO_TITLE">
             <ItemTemplate>
             <asp:Label ID="lblOrderTitle" runat="server" Text='<%# Eval("WO_TITLE") %>' Width="200px"></asp:Label>
             </ItemTemplate>
                   <ItemStyle Width="200px" Wrap="True" />
             </asp:TemplateField>
               
                              
                <asp:TemplateField HeaderText="LTR No." SortExpression="LTRNO" >
             <ItemTemplate>
             <asp:Label ID="lblLTR" runat="server" Text='<%# Eval("LTRNO") %>' Width="100px"></asp:Label>
             </ItemTemplate>
                   <ItemStyle Width="100px" Wrap="True" />
             </asp:TemplateField>
               
                                         
             <asp:TemplateField HeaderText="FollowUp" SortExpression="WO_FOLLOWUP_NAME" >
             <ItemTemplate>
             <asp:Label ID="lblFollowUp" runat="server" Text='<%# Eval("WO_FOLLOWUP_NAME") %>' Width="100px"></asp:Label>
             </ItemTemplate>
                   <ItemStyle Width="100px" Wrap="True" />
             </asp:TemplateField>
               
             
               <asp:TemplateField HeaderText="Severity" SortExpression="WO_SEVERITY_NAME" >
             <ItemTemplate>
             <asp:Label ID="lblSeverity" runat="server" Text='<%# Eval("WO_SEVERITY_NAME") %>' Width="100px"></asp:Label>
             </ItemTemplate>
                   <ItemStyle Width="100px" Wrap="True" />
             </asp:TemplateField>
               
             <asp:TemplateField HeaderText="Order Type" SortExpression="WO_TYPE_NAME">
             <ItemTemplate>
             <asp:Label ID="lblOrderType" runat="server" Text='<%# Eval("WO_TYPE_NAME") %>' Width="100px"></asp:Label>
             </ItemTemplate>
                   <ItemStyle Width="100px" Wrap="True" />
             </asp:TemplateField>
               
               <asp:TemplateField HeaderText="Logged By" SortExpression="LOGGEDBY" >
             <ItemTemplate>
             <asp:Label ID="lblLoggedBy" runat="server" Text='<%# Eval("LOGGEDBY") %>' Width="100px"></asp:Label>
             </ItemTemplate>
                   <ItemStyle Width="100px" Wrap="True" />
             </asp:TemplateField>
               
                           
               <asp:TemplateField HeaderText="Open Date"  SortExpression="WO_OPENDATE">
             <ItemTemplate>
             <asp:Label ID="lblOpenDate" runat="server" Text='<%# Eval("WO_OPENDATE") %>' Width="100px"></asp:Label>
             </ItemTemplate>
                   <ItemStyle Width="100px" Wrap="True" />
             </asp:TemplateField>
               
             <asp:TemplateField HeaderText="Close Date" SortExpression="WO_CLOSEDATE" >
             <ItemTemplate>
             <asp:Label ID="lblCloseDate" runat="server" Text='<%# Eval("WO_CLOSEDATE") %>' Width="100px"></asp:Label>
             </ItemTemplate>
                   <ItemStyle Width="100px" Wrap="True" />
             </asp:TemplateField>
               
                              
               <asp:TemplateField HeaderText="Assignee" SortExpression="WO_ASSIGNEE_NAME" >
             <ItemTemplate>
             <asp:Label ID="lblAssignee" runat="server" Text='<%# Eval("WO_ASSIGNEE_NAME") %>' Width="100px"></asp:Label>
             </ItemTemplate>
                   <ItemStyle Width="100px" Wrap="True" />
             </asp:TemplateField>
               
             <asp:TemplateField HeaderText="Assigned Date" SortExpression="ASSIGNED_DATE" >
             <ItemTemplate>
             <asp:Label ID="lblAssigneeDate" runat="server" Text='<%# Eval("ASSIGNED_DATE") %>' Width="100px"></asp:Label>
             </ItemTemplate>
                   <ItemStyle Width="100px" Wrap="True" />
             </asp:TemplateField>
               
              <asp:TemplateField HeaderText="Status" SortExpression="STATUS" >
             <ItemTemplate>
             <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("STATUS") %>' Width="100px"></asp:Label>
             </ItemTemplate>
                   <ItemStyle Width="100px" Wrap="True" />
             </asp:TemplateField>
                    
                <asp:TemplateField HeaderText="Action" >
                <ItemTemplate>
                <asp:LinkButton ID="lnkSelect" CssClass="LinkButtons" Text="Select" runat="server" CommandName="SelectX" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "WO_ID") + "|" + DataBinder.Eval(Container.DataItem, "WO_NUMBER") %>'></asp:LinkButton>&nbsp;     
                <asp:LinkButton ID="lnkEdit" runat="server" CommandName ="EditX" Text ="Edit" CssClass ="LinkButtons"></asp:LinkButton>&nbsp;
                   <asp:LinkButton ID="lnkDelete" runat="server" CommandName ="DeleteX" Text ="Delete" CssClass ="LinkButtons"></asp:LinkButton> 
                    
                    <asp:HiddenField ID="hdOrderId" runat="server" Value='<%#Eval("WO_ID")%>' />   
                 </ItemTemplate>
                    <ItemStyle Width="20%" Wrap="False" />
                    <HeaderStyle Wrap="False" />
               </asp:TemplateField>
                                            

            </Columns>
            <AlternatingRowStyle CssClass="lightblue" Wrap="False" />
            <RowStyle CssClass="textbold" HorizontalAlign="Left" />
            <HeaderStyle CssClass="Gridheading" HorizontalAlign="Left" ForeColor="White" />
        </asp:GridView>
                                               
<asp:Panel ID="pnlPaging" runat="server" Visible="false" Width="50%">
  <table border="0" cellpadding="0" cellspacing="0" width="100%">
                  <tr class="paddingtop paddingbottom"  >                                                                                                                                                
                      <td style="width: 35%" class="left"><span  class="textbold"><b>&nbsp;No. of records found</b></span>&nbsp;&nbsp;<asp:TextBox ID="txtTotalRecordCount" runat ="server"  Width="105px" CssClass="textboxgrey" ReadOnly="True" ></asp:TextBox></td>
                      <td style="width: 20%" class="right">                                                                             
                          <asp:LinkButton ID="lnkPrev" CssClass="LinkButtons" runat="server" CommandName="Prev"><< Prev</asp:LinkButton></td>
                      <td style="width: 20%" class="center">
                          <span class ="textbold" ><b>Page No</b></span>&nbsp;&nbsp;<asp:DropDownList ID="ddlPageNumber" Width="70px" CssClass="dropdownlist" runat="server" AutoPostBack="True" >
                          </asp:DropDownList></td>
                      <td style="width: 25%" class="left">
                          <asp:LinkButton ID="lnkNext" runat="server" CssClass="LinkButtons" CommandName="Next">Next >></asp:LinkButton></td>
                  </tr>
              </table></asp:Panel>
</td></tr>  

</table>

    </form>
     <script language="javascript" type="text/javascript">
    function ValidateForm()
    {
      document.getElementById('<%=lblError.ClientId%>').innerText='';
     if(document.getElementById('<%=txtLTRNo.ClientId%>').value !='')
         {
            var strValue = document.getElementById('<%=txtLTRNo.ClientId%>').value;
            reg = new RegExp("^[0-9]+$"); 

            if(reg.test(strValue) == false) 
            {

                document.getElementById('<%=lblError.ClientId%>').innerText ='LTR number should contain only digits.';
                return false;

             }
        }
        if(document.getElementById('<%=txtOrderNo.ClientId%>').value !='')
         {
            var strValue = document.getElementById('<%=txtOrderNo.ClientId%>').value;
            reg = new RegExp("^[0-9]+$"); 

            if(reg.test(strValue) == false) 
            {

                document.getElementById('<%=lblError.ClientId%>').innerText ='Work order number should contain only digits.';
                return false;

             }
        }
        //      Checking txtOpenDateFrom .
        if(document.getElementById('<%=txtOpenDateFrom.ClientId%>').value != '')
        {
        if (isDate(document.getElementById('<%=txtOpenDateFrom.ClientId%>').value,"d/M/yyyy") == false)	
        {
           document.getElementById('<%=lblError.ClientId%>').innerText = "Open date from is not valid.";			
	       document.getElementById('<%=txtOpenDateFrom.ClientId%>').focus();
	       return(false);  
        }
        } 
         //      Checking txtOpenDateTo .
        if(document.getElementById('<%=txtOpenDateTo.ClientId%>').value != '')
        {
        if (isDate(document.getElementById('<%=txtOpenDateTo.ClientId%>').value,"d/M/yyyy") == false)	
        {
           document.getElementById('<%=lblError.ClientId%>').innerText = "Open date to is not valid.";			
	       document.getElementById('<%=txtOpenDateTo.ClientId%>').focus();
	       return(false);  
        }
        } 
         //      Checking txtCloseDateFrom .
        if(document.getElementById('<%=txtCloseDateFrom.ClientId%>').value != '')
        {
        if (isDate(document.getElementById('<%=txtCloseDateFrom.ClientId%>').value,"d/M/yyyy") == false)	
        {
           document.getElementById('<%=lblError.ClientId%>').innerText = "Close date from is not valid.";			
	       document.getElementById('<%=txtCloseDateFrom.ClientId%>').focus();
	       return(false);  
        }
        } 
         //      Checking txtCloseDateTo .
        if(document.getElementById('<%=txtCloseDateTo.ClientId%>').value != '')
        {
        if (isDate(document.getElementById('<%=txtCloseDateTo.ClientId%>').value,"d/M/yyyy") == false)	
        {
           document.getElementById('<%=lblError.ClientId%>').innerText = "Close date to is not valid.";			
	       document.getElementById('<%=txtCloseDateTo.ClientId%>').focus();
	       return(false);  
        }
        } 
         
          //      Checking txtAssignedDate .
        if(document.getElementById('<%=txtAssignedDate.ClientId%>').value != '')
        {
        if (isDate(document.getElementById('<%=txtAssignedDate.ClientId%>').value,"d/M/yyyy") == false)	
        {
           document.getElementById('<%=lblError.ClientId%>').innerText = "Assigned date is not valid.";			
	       document.getElementById('<%=txtAssignedDate.ClientId%>').focus();
	       return(false);  
        }
        } 
        // End function
         //****************************************************************
    //      Checking txtOpenDateFrom .
//        if(document.getElementById('<%=txtOpenDateFrom.ClientId%>').value != '')
//        { 
//            if(document.getElementById('<%=txtOpenDateTo.ClientId%>').value == '') 
//            { 
//             document.getElementById('<%=lblError.ClientId%>').innerText = "Open date to is mandatory.";			
//	            return(false);  
//	        }
//        } 
//         if(document.getElementById('<%=txtOpenDateTo.ClientId%>').value != '')
//        { 
//            if(document.getElementById('<%=txtOpenDateFrom.ClientId%>').value == '') 
//            { 
//             document.getElementById('<%=lblError.ClientId%>').innerText = "Open date from is mandatory.";			
//	            return(false);  
//	        }
//        } 
         //****************************************************************
    //      Checking txtCloseDateFrom .
//        if(document.getElementById('<%=txtCloseDateFrom.ClientId%>').value != '')
//        { 
//            if(document.getElementById('<%=txtCloseDateTo.ClientId%>').value == '') 
//            { 
//             document.getElementById('<%=lblError.ClientId%>').innerText = "Close date to is mandatory.";			
//	            return(false);  
//	        }
//        } 
//         if(document.getElementById('<%=txtCloseDateTo.ClientId%>').value != '')
//        { 
//            if(document.getElementById('<%=txtCloseDateFrom.ClientId%>').value == '') 
//            { 
//             document.getElementById('<%=lblError.ClientId%>').innerText = "Close date from is mandatory.";			
//	            return(false);  
//	        }
//        } 
        //    compareDates(dateOpen,dateformat1,dateAssignee,dateformat2) {
        
       if (document.getElementById('<%=txtCloseDateFrom.ClientId%>').value != "" && document.getElementById('<%=txtCloseDateTo.ClientId%>').value != "")
       {
           if (compareDates(document.getElementById('<%=txtCloseDateFrom.ClientId%>').value,"d/M/yyyy",document.getElementById('<%=txtCloseDateTo.ClientId%>').value,"d/M/yyyy")==1)
               {
                    document.getElementById('<%=lblError.ClientId%>').innerText ='Close date to should be greater than or equal to close date from.'
                    return false;
               }
       }
        if (document.getElementById('<%=txtOpenDateFrom.ClientId%>').value != "" && document.getElementById('<%=txtOpenDateTo.ClientId%>').value != "")
       {
            if (compareDates(document.getElementById('<%=txtOpenDateFrom.ClientId%>').value,"d/M/yyyy",document.getElementById('<%=txtOpenDateTo.ClientId%>').value,"d/M/yyyy")==1)
           {
                document.getElementById('<%=lblError.ClientId%>').innerText ='Open date to should be greater than or equal to open date from.'
                return false;
           }
       }
       return true; 
        
    }
    
   
    
    </script>
</body>

</html>
