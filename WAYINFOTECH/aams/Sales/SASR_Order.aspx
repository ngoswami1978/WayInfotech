<%@ Page Language="VB" AutoEventWireup="false" CodeFile="SASR_Order.aspx.vb" Inherits="Sales_SASR_Order"
    ValidateRequest="false" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=10.2.3600.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>AAMS::Sales::Search Agency Order</title>
    <link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" media="all" href="../Calender/calendar-blue.css"
        title="win2k-cold-1" />

    <script type="text/javascript" src="../Calender/calendar.js"></script>

    <script type="text/javascript" src="../JavaScript/AAMS.js"></script>

    <!-- import the language module -->

    <script type="text/javascript" src="../Calender/lang/calendar-en.js"></script>

    <script type="text/javascript" src="../Calender/calendar-setup.js"></script>

    <script language="javascript" type="text/javascript">
    function ValidateForm()
    {
      document.getElementById('<%=lblError.ClientId%>').innerText=''
            
       
       
        //      Checking txtStartDateFrom .
        if(document.getElementById('<%=txtDateFrom.ClientId%>').value != '')
        {
        if (isDate(document.getElementById('<%=txtDateFrom.ClientId%>').value,"d/M/yyyy") == false)	
        {
           document.getElementById('<%=lblError.ClientId%>').innerText = "From date is not valid.";			
	       document.getElementById('<%=txtDateFrom.ClientId%>').focus();
	       return(false);  
        }
        } 
         //      Checking txtStartDateTo .
        if(document.getElementById('<%=txtDateTo.ClientId%>').value != '')
        {
        if (isDate(document.getElementById('<%=txtDateTo.ClientId%>').value,"d/M/yyyy") == false)	
        {
           document.getElementById('<%=lblError.ClientId%>').innerText = "To date is not valid.";			
	       document.getElementById('<%=txtDateTo.ClientId%>').focus();
	       return(false);  
        }
        } 
         
  
        //    compareDates(dateOpen,dateformat1,dateAssignee,dateformat2) {
        
        if (document.getElementById('<%=txtDateFrom.ClientId%>').value != '' && document.getElementById('<%=txtDateTo.ClientId%>').value != '')
        {
           if (compareDates(document.getElementById('<%=txtDateFrom.ClientId%>').value,"d/M/yyyy",document.getElementById('<%=txtDateTo.ClientId%>').value,"d/M/yyyy")==1)
               {
                    document.getElementById('<%=lblError.ClientId%>').innerText ='From date to should be greater than or equal to To date .'
                    return false;
               }
           }
           
       return true; 
        
    
    
    }
  
   
 
    </script>

    <link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css"
        rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server" defaultfocus="txtAgency" defaultbutton="btnSearch">
        <div>
            <table>
                <tr>
                    <td>
                        <table width="860px" class="border_rightred left">
                            <tr>
                                <td class="top">
                                    <table width="100%" class="left">
                                        <tr>
                                            <td>
                                                <span class="menu">Sales -&gt;</span><span class="sub_menu">Agency Order Search</span></td>
                                        </tr>
                                        <tr>
                                            <td class="heading center">
                                                Search Agency Order</td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                    <tr>
                                                        <td class="redborder center">
                                                            <table border="0" cellpadding="2" cellspacing="1" style="width: 100%" class="left">
                                                                <tr>
                                                                    <td class="center gap" colspan="6">
                                                                        <asp:Label ID="lblError" runat="server" CssClass="ErrorMsg" EnableViewState="False"></asp:Label></td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                    </td>
                                                                    <td class="textbold">
                                                                        Company</td>
                                                                    <td colspan="3">
                                                                        <asp:TextBox ID="txtCompany" runat="server" CssClass="textbox" MaxLength="50" TabIndex="2"
                                                                            Width="498px"></asp:TextBox>
                                                                    </td>
                                                                    <td class="left" style="width: 20%">
                                                                        <asp:Button ID="btnSearch" CssClass="button" runat="server" Text="Search" TabIndex="3"
                                                                            AccessKey="a" /></td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                    </td>
                                                                    <td class="textbold">
                                                                        Date From
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtDateFrom" runat="server" CssClass="textbox" Width="170px" TabIndex="2"
                                                                            MaxLength="10"></asp:TextBox>
                                                                        <img id="imgStartDateFrom" alt="" src="../Images/calender.gif" title="Date selector"
                                                                            style="cursor: pointer" />

                                                                        <script type="text/javascript">
                                                  Calendar.setup({
                                                  inputField     :    '<%=txtDateFrom.clientId%>',
                                                  ifFormat       :    "%d/%m/%Y",
                                                  button         :    "imgStartDateFrom",
                                                 //align          :    "Tl",
                                                  singleClick    :    true
                                                 
                                                               });
                                                                        </script>

                                                                    </td>
                                                                    <td class="textbold">
                                                                        &nbsp;Date To
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtDateTo" runat="server" CssClass="textbox" Width="170px" TabIndex="2"
                                                                            MaxLength="10"></asp:TextBox>
                                                                        <img id="imgStartDateTo" alt="" src="../Images/calender.gif" title="Date selector"
                                                                            style="cursor: pointer" />

                                                                        <script type="text/javascript">
                                                  Calendar.setup({
                                                  inputField     :    '<%=txtDateTo.clientId%>',
                                                  ifFormat       :    "%d/%m/%Y",
                                                  button         :    "imgStartDateTo",
                                                 //align          :    "Tl",
                                                  singleClick    :    true
                                                
                                                               });
                                                                        </script>

                                                                    </td>
                                                                    <td class="left">
                                                                        <asp:Button ID="btnExport" runat="server" AccessKey="e" CssClass="button" TabIndex="3"
                                                                            Text="Export" /></td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                    </td>
                                                                    <td class="textbold">
                                                                    </td>
                                                                    <td>
                                                                    </td>
                                                                    <td class="textbold">
                                                                    </td>
                                                                    <td>
                                                                    </td>
                                                                    <td class="left">
                                                                        <asp:Button ID="btnReset" CssClass="button" runat="server" Text="Reset" TabIndex="3"
                                                                            AccessKey="r" /></td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                    </td>
                                                                    <td>
                                                                    </td>
                                                                    <td colspan="4">
                                                                        <input id="hdDeleteId" runat="server" style="width: 1px" type="hidden" enableviewstate="true" />
                                                                        <input id="hdRecordOnCurrentPage" runat="server" style="width: 6px" type="hidden" />
                                                                        &nbsp; &nbsp; &nbsp;&nbsp;
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="6">
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="6">
                                                                        <asp:GridView ID="grdOrder" runat="server" AutoGenerateColumns="False" Width="100%"
                                                                            TabIndex="6" EnableViewState="true" CellPadding="0" AllowSorting="True" HeaderStyle-ForeColor="white">
                                                                            <Columns>
                                                                                <asp:BoundField HeaderText="Company" DataField="AgencyName" SortExpression="AgencyName">
                                                                                    <ItemStyle Width="12%" />
                                                                                </asp:BoundField>
                                                                                <asp:BoundField HeaderText="Request Date" DataField="RequestDate" SortExpression="RequestDate"
                                                                                    ItemStyle-CssClass="left">
                                                                                    <ItemStyle Width="5%" />
                                                                                </asp:BoundField>
                                                                                <asp:TemplateField HeaderText="Action">
                                                                                    <ItemTemplate>
                                                                                        <asp:LinkButton ID="lnkView" runat="server" CommandName="ViewX" Text="View" CssClass="LinkButtons"
                                                                                            CommandArgument='<%#DataBinder.Eval(Container.DataItem,"OrderID")%>'></asp:LinkButton>
                                                                                        <input id="hdOrderID" runat="server" value='<%#DataBinder.Eval(Container.DataItem,"OrderID")%>'
                                                                                            type="hidden" />
                                                                                    </ItemTemplate>
                                                                                    <ItemStyle Width="4%" />
                                                                                </asp:TemplateField>
                                                                            </Columns>
                                                                            <AlternatingRowStyle CssClass="lightblue" />
                                                                            <RowStyle CssClass="textbold" />
                                                                            <HeaderStyle CssClass="Gridheading" />
                                                                        </asp:GridView>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="6">
                                                                        <asp:Panel ID="pnlPaging" runat="server" Visible="false" Width="90%" CssClass="left">
                                                                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                                <tr class="paddingtop paddingbottom">
                                                                                    <td style="width: 35%" class="left">
                                                                                        <span class="textbold"><b>&nbsp;No. of records found</b></span>&nbsp;&nbsp;<asp:TextBox
                                                                                            ID="txtTotalRecordCount" runat="server" Width="105px" CssClass="textboxgrey"
                                                                                            ReadOnly="True"></asp:TextBox></td>
                                                                                    <td style="width: 20%" class="right">
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
                                                            </table>
                                                            <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true"
                                                                Visible="false" />
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
        </div>
    </form>
</body>
</html>
