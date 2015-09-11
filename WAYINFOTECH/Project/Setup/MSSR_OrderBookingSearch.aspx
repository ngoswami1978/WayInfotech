<%@ Page Language="VB" AutoEventWireup="false" CodeFile="MSSR_OrderBookingSearch.aspx.vb"
    Inherits="Setup_MSSR_QUALITYORDERSEARCH" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>WAY: Employee</title>
    <link href="../CSS/WAY.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="../JavaScript/WAY.js"></script>

    <link rel="stylesheet" type="text/css" media="all" href="../Calender/calendar-blue.css"
        title="win2k-cold-1" />
</head>
<!-- import the calendar script -->

<script type="text/javascript" src="../Calender/calendar.js"></script>

<!-- import the language module -->

<script type="text/javascript" src="../Calender/lang/calendar-en.js"></script>

<!-- import the calendar setup module -->

<script type="text/javascript" src="../Calender/calendar-setup.js"></script>

<script language="javascript" type="text/javascript">
 
 function ValidateOrderSearch()
       {       
       if(document.getElementById('<%=txtOrderNo.ClientId%>').value != '')
        {
        
         if (IsDataValid(document.getElementById('<%=txtOrderNo.ClientId%>').value.trim(),3) == false)	
        {
           document.getElementById('<%=lblError.ClientId%>').innerText = "Order No. Should Numeric.";			
	       document.getElementById('<%=txtOrderNo.ClientId%>').focus();
	       return(false);  
        }
        
        }        
        
        if(document.getElementById('<%=txtOrderDateFrom.ClientId%>').value != '')
        {
        if (isDate(document.getElementById('<%=txtOrderDateFrom.ClientId%>').value,"d/M/yyyy") == false)	
        {
           document.getElementById('<%=lblError.ClientId%>').innerText = "Order Date Format is Invalid.";			
	       document.getElementById('<%=txtOrderDateFrom.ClientId%>').focus();
	       return(false);  
        }
       }
         
        if(document.getElementById('<%=txtOrderDateTo.ClientId%>').value != '')
        {
        if (isDate(document.getElementById('<%=txtOrderDateTo.ClientId%>').value,"d/M/yyyy") == false)	
        {
           document.getElementById('<%=lblError.ClientId%>').innerText = "Order Date-To Format is Invalid.";			
	       document.getElementById('<%=txtOrderDateTo.ClientId%>').focus();
	       return(false);  
        }
       }
         if(document.getElementById('<%=txtOrderDateTo.ClientId%>').value != '' && document.getElementById('<%=txtOrderDateFrom.ClientId%>').value != '')
        {
        if (compareDates(document.getElementById('<%=txtOrderDateFrom.ClientId%>').value,"d/M/yyyy",document.getElementById('<%=txtOrderDateTo.ClientId%>').value,"d/M/yyyy")=='1')	
        {
           document.getElementById('<%=lblError.ClientId%>').innerText = "Order From Date Cann't be Greater than Date-To.";			
	       document.getElementById('<%=txtOrderDateTo.ClientId%>').focus();
	       return(false);  
        }
       }  
       }
       
   function DeleteFunction(strW_StyleOrderID)
    {   
        if (confirm("Are you sure you want to delete?")==true)
        {
         document.getElementById('hdDelete').value = strW_StyleOrderID;
         document.forms['form1'].submit();           
        }
        else
        {
            return false;
        }
    }
    
     function DeleteFunction2(strW_StyleOrderID)
          {   
               if (confirm("Are you sure you want to delete?")==true)
            {   
               document.getElementById('hdDelete').value = strW_StyleOrderID;
               document.forms['form1'].submit(); 
            }  
            else
            {
                return false;
            }         
        }
    
    function EditFunction(strW_StyleOrderID)
    {           
          window.location.href="MSUP_MultipleOrderBooking.aspx?Action=U&W_StyleOrderID="+strW_StyleOrderID;
          return false;
    }
    
    function NewFunction()
    {   
      window.location.href="MSUP_MultipleOrderBooking.aspx?Action=I";       
      return false;
    }
 
</script>

<body>
    <form id="form1" runat="server" defaultbutton="btnSearch" defaultfocus="txtOrderNo">
        <table width="860px" align="left" class="border_rightred" style="height: 486px;">
            <tr>
                <td valign="top">
                    <table width="100%" align="left">
                        <tr>
                            <td valign="top" align="left">
                                <span class="menu">Setup-></span><span class="sub_menu">Search Order Booking</span>
                            </td>
                        </tr>
                        <tr>
                            <td class="heading" align="center" valign="top">
                                Search Order Booking</td>
                        </tr>
                        <tr>
                            <td valign="top">
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td align="LEFT" class="redborder">
                                            <table width="100%" border="0" align="left" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td class="textbold" colspan="6" align="center" style="height:15px">
                                                        <asp:Label ID="lblError" runat="server" CssClass="ErrorMsg"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="6%" >
                                                    </td>
                                                    <td width="15%" class="textbold">
                                                        Order Date From</td>
                                                    <td width="30%">
                                                        <asp:TextBox ID="txtOrderDateFrom" runat="server" CssClass="textbox" MaxLength="10"
                                                            TabIndex="11"></asp:TextBox>
                                                        <img id="imgOrderDateFrom" alt="" src="../Images/calender.gif" style="cursor: pointer"
                                                            title="Date selector" />

                                                        <script type="text/javascript">
                                                                                                    Calendar.setup({
                                                                                                    inputField     :    '<%=txtOrderDateFrom.ClientId%>',
                                                                                                    ifFormat       :     "%d/%m/%Y",
                                                                                                    button         :    "imgOrderDateFrom",
                                                                                                    singleClick    :    true
                                                                                                    });
                                                        </script>

                                                    </td>
                                                    <td style="width: 109px">
                                                        <span class="textbold">Order Date To</span></td>
                                                    <td width="21%">
                                                        <asp:TextBox ID="txtOrderDateTo" runat="server" CssClass="textbox" MaxLength="10"
                                                            TabIndex="11"></asp:TextBox><img id="imgOrderDateTo" alt="" src="../Images/calender.gif"
                                                                style="cursor: pointer" title="Date selector" />

                                                        <script type="text/javascript">
                                                                                                    Calendar.setup({
                                                                                                    inputField     :    '<%=txtOrderDateTo.ClientId%>',
                                                                                                    ifFormat       :     "%d/%m/%Y",
                                                                                                    button         :    "imgOrderDateTo",
                                                                                                    singleClick    :    true
                                                                                                    });
                                                        </script>

                                                    </td>
                                                    <td width="18%">
                                                        <asp:Button ID="btnSearch" CssClass="button" runat="server" Text="Search" TabIndex="3"  OnClientClick="return ValidateOrderSearch()" AccessKey="A" /></td>
                                                </tr>
                                                <tr>
                                                    <td >
                                                    </td>
                                                    <td class="textbold" >
                                                        Quality</td>
                                                    <td >
                                                        <asp:DropDownList ID="drpQuality" CssClass="dropdownlist" runat="server" Width="153px" onkeyup="gotop(this.id)"
                                                            TabIndex="2">
                                                        </asp:DropDownList></td>
                                                    <td class="textbold" >
                                                        Order No</td>
                                                    <td >
                                                        <asp:TextBox ID="txtOrderNo" runat="server" CssClass="textfield" MaxLength="50" TabIndex="3"
                                                            Width="132px"></asp:TextBox></td>
                                                    <td >
                                                        <asp:Button ID="btnNew" CssClass="button" runat="server" Text="New" TabIndex="3"
                                                            AccessKey="N" /></td>
                                                </tr>
                                                <tr>
                                                    <td style="height: 22px">
                                                    </td>
                                                    <td class="textbold" style="height: 22px">
                                                        Logged By</td>
                                                    <td style="height: 22px">
                                                        <asp:DropDownList ID="drpLoggedBy" runat="server" CssClass="dropdownlist" TabIndex="2" onkeyup="gotop(this.id)"
                                                            Width="152px">
                                                        </asp:DropDownList></td>
                                                    <td style="height: 22px">
                                                    </td>
                                                    <td style="height: 22px">
                                                    </td>
                                                    <td style="height: 22px">
                                                        <asp:Button ID="btnReset" CssClass="button" runat="server" Text="Reset" TabIndex="3"
                                                            AccessKey="R" /></td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                    </td>
                                                    <td class="textbold" visible="false">
                                                    </td>
                                                    <td class="textbold">
                                                    </td>
                                                    <td class="textbold" style="width: 109px">
                                                    </td>
                                                    <td>
                                                    </td>
                                                    <td>
                                                        <asp:Button ID="btnExport" runat="server" CssClass="button" TabIndex="3" Text="Export"
                                                            AccessKey="E" /></td>
                                                </tr>
                                                <tr>
                                                    <td colspan="6" style="height: 5px;">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="6">
                                                        <asp:GridView ID="dbgrdManageStyle" runat="server" AutoGenerateColumns="False" TabIndex="6"
                                                            Width="100%" EnableViewState="true" AllowSorting="true" HeaderStyle-ForeColor="white">
                                                            <Columns>
                                                                <asp:BoundField HeaderText="OrderNumber" DataField="ORDERNUMBER" SortExpression="ORDERNUMBER">
                                                                    <ItemStyle Width="10%" />
                                                                </asp:BoundField>
                                                                <asp:BoundField HeaderText="OrderDate" DataField="ORDERDATE" SortExpression="ORDERDATE">
                                                                    <ItemStyle Width="20%" />
                                                                </asp:BoundField>
                                                                <asp:BoundField HeaderText="Quality" DataField="QUALITY" SortExpression="QUALITY">
                                                                    <ItemStyle Width="20%" />
                                                                </asp:BoundField>
                                                                <asp:BoundField HeaderText="Design" DataField="DESIGN" SortExpression="DESIGN">
                                                                    <ItemStyle Width="20%" />
                                                                </asp:BoundField>
                                                                <asp:BoundField HeaderText="ShadeNo" DataField="SHADENO" SortExpression="SHADENO">
                                                                    <ItemStyle Width="15%" />
                                                                </asp:BoundField>
                                                                <asp:BoundField HeaderText="MRP" DataField="MRP" SortExpression="MRP">
                                                                    <ItemStyle Width="5%" />
                                                                </asp:BoundField>
                                                                <asp:BoundField HeaderText="LoggerName" DataField="LOGGERNAME" SortExpression="LOGGERNAME">
                                                                    <ItemStyle Width="15%" />
                                                                </asp:BoundField>
                                                                <asp:BoundField HeaderText="Qty" DataField="QTY" SortExpression="QTY">
                                                                    <ItemStyle Width="15%" />                                                                    
                                                                </asp:BoundField>
                                                                <asp:TemplateField HeaderText="Action">
                                                                    <ItemTemplate>
                                                                        <asp:HiddenField ID="hdW_StyleOrderID" runat="server" Value='<%#Eval("W_StyleOrderID")%>' />
                                                                        <asp:LinkButton ID="lnkEdit" CssClass="LinkButtons" Text="Edit" runat="server" CommandName="EditX"
                                                                            CommandArgument='<%#Eval("W_StyleOrderID")%>'></asp:LinkButton>&nbsp;
                                                                        <asp:LinkButton ID="lnkDelete" CssClass="LinkButtons" Text="Delete" runat="server"
                                                                            CommandName="DeleteX" CommandArgument='<%#Eval("W_StyleOrderID")%>'></asp:LinkButton>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                            <AlternatingRowStyle CssClass="lightblue" />
                                                            <RowStyle CssClass="textbold" />
                                                            <HeaderStyle CssClass="Gridheading" ForeColor="White" />
                                                            <PagerSettings PageButtonCount="5" />
                                                        </asp:GridView>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="6" valign="top">
                                                        <asp:Panel ID="pnlPaging" runat="server" Visible="false" Width="100%">
                                                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                <tr style="padding-top: 4pt; padding-bottom: 4pt">
                                                                    <td style="width: 30%" class="left">
                                                                        <span class="textbold"><b>&nbsp;No. of records found</b></span>&nbsp;&nbsp;<asp:TextBox
                                                                            ID="txtTotalRecordCount" runat="server" Width="105px" CssClass="textboxgrey"
                                                                            ReadOnly="true"></asp:TextBox></td>
                                                                    <td style="width: 25%" class="right">
                                                                        <asp:LinkButton ID="lnkPrev" CssClass="LinkButtons" runat="server" CommandName="Prev"><< Prev</asp:LinkButton></td>
                                                                    <td style="width: 20%" class="center">
                                                                        <span class="textbold"><b>Page No</b></span>&nbsp;&nbsp;<asp:DropDownList ID="ddlPageNumber"
                                                                            Width="70px" CssClass="dropdownlist" runat="server" AutoPostBack="True">
                                                                        </asp:DropDownList></td>
                                                                    <td style="width: 25%" class="left">
                                                                        <asp:LinkButton ID="lnkNext" runat="server" CssClass="LinkButtons" CommandName="Next">Next >></asp:LinkButton></td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="6">
                                                        <asp:HiddenField ID="hdDelete" runat="server" />
                                                        <asp:HiddenField ID="hdRecordOnCurrentPage" runat="server" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    <asp:HiddenField ID="hdW_StyleId" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:TextBox ID="txtRecordOnCurrentPage" runat="server" Width="73px" CssClass="textboxgrey"
                        Visible="false"></asp:TextBox>
                    <input type="hidden" id="hdCtrlId" runat="server" />
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
