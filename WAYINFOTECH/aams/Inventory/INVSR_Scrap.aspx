<%@ Page Language="VB" AutoEventWireup="false" CodeFile="INVSR_Scrap.aspx.vb" Inherits="Inventory_INVSR_Scrap" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />
    <title>AAMS::Inventory::Search Scrap</title>

    <script src="../JavaScript/AAMS.js" type="text/javascript"></script>

    <link rel="stylesheet" type="text/css" media="all" href="../Calender/calendar-blue.css"
        title="win2k-cold-1" />
<script type="text/javascript" src="../JavaScript/DisableRightClick.js"></script>
    <script type="text/javascript" src="../Calender/calendar.js"></script>

    <!-- import the language module -->

    <script type="text/javascript" src="../Calender/lang/calendar-en.js"></script>

    <!-- import the calendar setup module -->

    <script type="text/javascript" src="../Calender/calendar-setup.js"></script>

    <script language="javascript" type="text/javascript">

 function PopupEmployeePage()
         {
            var type;
             var strEmployeePageName=document.getElementById("hdEmployeePageName").value;
            if (strEmployeePageName!="")
            {
            type = "../Setup/" + strEmployeePageName+ "?Popup=T" ;
            //type = "../Setup/MSSR_Employee.aspx?Popup=T" ;
   	        window.open(type,"LoggedBy","height=600,width=900,top=30,left=20,scrollbars=1,status=1");	
           // document.forms(0).submit();
            return false;
         }
            
     }
     
     function SelectFunction(str3)
        {   
            //alert(str3);
            var pos=str3.split('|'); 
//            if (window.opener.document.forms['form1']['hdWorkOrderNo']!=null)
//            {
//            window.opener.document.forms['form1']['hdWorkOrderNo'].value=pos[1];
//            window.opener.document.forms['form1']['txtWorkOrderNo'].value=pos[1];
//            }
            window.close();
       }


 function Edit(TrashID)
			{
				 window.location.href="INVUP_Scrap.aspx?Action=U&TrashID=" +TrashID
				 return false;
			}
			
	function Delete(TrashID)
	{
		 			 
		 if (confirm("Are you sure you want to delete?")==true)
            {    
               document.getElementById('<%=hdDeleteId.ClientId%>').value = TrashID
               return true;        
            }
            return false;
	}
    </script>

</head>
<body >
    <form id="form1" runat="server" defaultbutton="btnSearch">
        <div>
            <table width="840px" class="border_rightred left">
                <tr>
                    <td class="top">
                        <table width="100%" class="left">
                            <tr>
                                <td>
                                    <span class="menu">Inventory -&gt;</span><span class="sub_menu">Scrap Search</span></td>
                            </tr>
                            <tr>
                                <td class="heading center">
                                    Search Scrap</td>
                            </tr>
                            <tr>
                                <td>
                                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                        <tr>
                                            <td class="redborder center">
                                                <table border="0" cellpadding="2" cellspacing="1" style="width: 100%" class="left">
                                                    <tr>
                                                        <td class="center" colspan="7" height="25px">
                                                            <asp:Label ID="lblError" runat="server" CssClass="ErrorMsg"></asp:Label></td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 100px">
                                                        </td>
                                                        <td class="textbold" style="width: 15%">
                                                            Godown</td>
                                                        <td colspan="4" style="width: 52%">
                                                            <asp:DropDownList onkeyup="gotop(this.id)" ID="ddlGodown" runat="server" CssClass="dropdownlist"
                                                                TabIndex="2" Width="418px">
                                                                <asp:ListItem Selected="True">--All--</asp:ListItem>
                                                            </asp:DropDownList></td>
                                                        <td style="width: 25%;" class="center">
                                                            <asp:Button ID="btnSearch" CssClass="button" runat="server" Text="Search" TabIndex="4"  AccessKey="A"/></td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                        </td>
                                                        <td class="textbold" style="width: 8%">
                                                            Logged By</td>
                                                        <td style="width: 22%">
                                                            <asp:TextBox ID="txtLoggedBy" runat="server" CssClass="textbox" TabIndex="20" MaxLength="25"
                                                                ReadOnly="True"></asp:TextBox>
                                                            <img id="Img4" runat="server" alt="" onclick="PopupEmployeePage()"
                                                                src="../Images/lookup.gif" style="cursor:pointer;" /></td>
                                                        <td class="textbold" style="width: 4%">
                                                        </td>
                                                        <td class="textbold" style="width: 8%">
                                                            Trash ID</td>
                                                        <td style="width: 22%">
                                                            <asp:TextBox ID="txtTrashID" runat="server" CssClass="textbox" TabIndex="20" MaxLength="10"></asp:TextBox></td>
                                                        <td class="center" style="width: 25%">
                                                            <asp:Button ID="btnNew" CssClass="button" runat="server" Text="New" TabIndex="5" AccessKey="N" /></td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                        </td>
                                                        <td class="textbold" style="width: 8%">
                                                            From Date</td>
                                                        <td style="width: 20%">
                                                            <asp:TextBox ID="txtDateFrom" runat="server" CssClass="textbox" TabIndex="20" MaxLength="10"></asp:TextBox>
                                                            <img id="imgOpenedDateFrom" alt="" src="../Images/calender.gif" style="cursor: pointer"
                                                                title="Date selector" />

                                                            <script type="text/javascript">
                                                  Calendar.setup({
                                                  inputField     :    '<%=txtDateFrom.clientId%>',
                                                  ifFormat       :    "%d/%m/%Y",
                                                  button         :    "imgOpenedDateFrom",
                                                 //align          :    "Tl",
                                                  singleClick    :    true
                                                 
                                                               });
                                                            </script>

                                                        </td>
                                                        <td class="textbold" style="width: 4%">
                                                        </td>
                                                        <td class="textbold" style="width: 8%">
                                                            To Date</td>
                                                        <td>
                                                            <asp:TextBox ID="txtDateTo" runat="server" CssClass="textbox" TabIndex="20" MaxLength="10"></asp:TextBox>
                                                            <img id="imgOpenedDateTo" alt="" src="../Images/calender.gif" style="cursor: pointer"
                                                                title="Date selector" />

                                                            <script type="text/javascript">
                                                  Calendar.setup({
                                                  inputField     :    '<%=txtDateTo.clientId%>',
                                                  ifFormat       :    "%d/%m/%Y",
                                                  button         :    "imgOpenedDateTo",
                                                 //align          :    "Tl",
                                                  singleClick    :    true
                                                
                                                               });
                                                            </script>

                                                        </td>
                                                        <td style="width: 25%" class="center">
                                                            <asp:Button ID="btnReset" CssClass="button" runat="server" Text="Reset" TabIndex="6" AccessKey="R" /></td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                        </td>
                                                        <td class="textbold" style="width: 8%">
                                                        </td>
                                                        <td style="width: 20%">
                                                        </td>
                                                        <td class="textbold" style="width: 4%">
                                                        </td>
                                                        <td class="textbold" style="width: 8%">
                                                        </td>
                                                        <td>
                                                        </td>
                                                        <td class="center" style="width: 25%">
                                                            <asp:Button ID="btnExport" runat="server" CssClass="button" Style="position: relative" AccessKey="E"
                                                                TabIndex="21" Text="Export" /></td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                        </td>
                                                        <td>
<input type="hidden" id="hdEmployeePageName" style="width:1px" runat='server' />
                                                        </td>
                                                        <td colspan="4">
                                                            &nbsp; &nbsp; &nbsp;
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="7" style="width: 100%" valign="top">
                                                            <asp:GridView ID="gvScrap" runat="server" AutoGenerateColumns="False" TabIndex="7"
                                                                Width="100%" EnableViewState="False" AllowSorting="true" HeaderStyle-ForeColor="white">
                                                                <Columns>
                                                                    <asp:BoundField HeaderText="TrashID"  SortExpression="TrashID" DataField="TrashID" ItemStyle-Wrap="true" ItemStyle-Width="20%"  />
                                                                    <asp:BoundField HeaderText="Godown" SortExpression="GodownName" DataField="GodownName" ItemStyle-Wrap="true"
                                                                        ItemStyle-Width="25%" />
                                                                    <asp:BoundField HeaderText="Employee "  SortExpression="Employee" DataField="Employee" ItemStyle-Wrap="false"
                                                                        ItemStyle-Width="12%" />
                                                                    <asp:BoundField HeaderText="Logged Date "  SortExpression="LoggedDate" DataField="LoggedDate" ItemStyle-Wrap="false"
                                                                        ItemStyle-Width="13%" />
                                                                    <asp:TemplateField HeaderText="Action">
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="lnkSelect" CssClass="LinkButtons" Text="Select" runat="server"
                                                                                CommandName="SelectX" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "TrashID") + "|" + DataBinder.Eval(Container.DataItem, "GodownName") %>'></asp:LinkButton>&nbsp;
                                                                            <asp:LinkButton ID="lnkEdit" runat="server" CommandName="EditX" Text="Edit" CssClass="LinkButtons"></asp:LinkButton>&nbsp;
                                                                            <asp:LinkButton ID="lnkDelete" runat="server" CommandName="DeleteX" Text="Delete"
                                                                                CssClass="LinkButtons"></asp:LinkButton>
                                                                            <asp:HiddenField ID="hdOrderId" runat="server" Value='<%#Eval("TrashID")%>' />
                                                                        </ItemTemplate>
                                                                        <ItemStyle Width="20%" Wrap="False" />
                                                                        <HeaderStyle Wrap="False" />
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                                <AlternatingRowStyle CssClass="lightblue" />
                                                                <RowStyle CssClass="textbold" />
                                                                <HeaderStyle CssClass="Gridheading" />
                                                            </asp:GridView>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="7" valign="top">
                                                            <asp:Panel ID="pnlPaging" runat="server" Visible="false" Width="100%">
                                                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                    <tr style="padding-top: 4pt; padding-bottom: 4pt">
                                                                        <td style="width: 30%" class="left">
                                                                            <span class="textbold"><b>&nbsp;No. of records found</b></span>&nbsp;&nbsp;<asp:TextBox
                                                                                ID="txtTotalRecordCount" runat="server" Width="105px" CssClass="textboxgrey" ReadOnly="true"></asp:TextBox></td>
                                                                        <td style="width: 25%" class="right">
                                                                            <asp:LinkButton ID="lnkPrev" CssClass="LinkButtons" runat="server" CommandName="Prev"><< Prev</asp:LinkButton></td>
                                                                        <td style="width: 20%" class="center">
                                                                            <span class="textbold"><b>Page No</b></span>&nbsp;&nbsp;<asp:DropDownList onkeyup="gotop(this.id)"
                                                                                ID="ddlPageNumber" Width="70px" CssClass="dropdownlist" runat="server" AutoPostBack="True">
                                                                            </asp:DropDownList></td>
                                                                        <td style="width: 25%" class="left">
                                                                            <asp:LinkButton ID="lnkNext" runat="server" CssClass="LinkButtons" CommandName="Next">Next >></asp:LinkButton></td>
                                                                    </tr>
                                                                </table>
                                                            </asp:Panel>
                                                            <asp:TextBox ID="txtRecordOnCurrentPage" runat="server" Width="73px" CssClass="textboxgrey"
                                                                Visible="false"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                    <input id="hdDeleteId" runat="server" enableviewstate="true" style="width: 1px" type="hidden" />
                                    <input id="hdEmployeeID" runat="server" enableviewstate="true" style="width: 1px"
                                        type="hidden" />
                                    <input id="hdScrap" runat="server" enableviewstate="true" style="width: 1px"
                                        type="hidden" /></td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
    </form>

    <script language="javascript" type="text/javascript">
    function ValidateForm()
    {
      document.getElementById('<%=lblError.ClientId%>').innerText=''
     if(document.getElementById('<%=txtTrashID.ClientId%>').value !='')
         {
            var strValue = document.getElementById('<%=txtTrashID.ClientId%>').value
            reg = new RegExp("^[0-9]+$"); 

            if(reg.test(strValue) == false) 
            {

                document.getElementById('<%=lblError.ClientId%>').innerText ='Trash ID should contain only digits.'
                return false;

             }
        }
        
        //      Checking txtOpenDateFrom .
        if(document.getElementById('<%=txtDateFrom.ClientId%>').value != '')
        {
            if (isDate(document.getElementById('<%=txtDateFrom.ClientId%>').value,"d/M/yyyy") == false)	
            {
               document.getElementById('<%=lblError.ClientId%>').innerText = "Date from is not valid.";			
	           document.getElementById('<%=txtDateFrom.ClientId%>').focus();
	           return(false);  
            }
        } 
         //      Checking txtOpenDateTo .
        if(document.getElementById('<%=txtDateTo.ClientId%>').value != '')
        {
            if (isDate(document.getElementById('<%=txtDateTo.ClientId%>').value,"d/M/yyyy") == false)	
            {
               document.getElementById('<%=lblError.ClientId%>').innerText = "Date to is not valid.";			
	           document.getElementById('<%=txtDateTo.ClientId%>').focus();
	           return(false);  
            }
        } 
         
       
    
         
        //    compareDates(dateOpen,dateformat1,dateAssignee,dateformat2) {
         if (document.getElementById('<%=txtDateFrom.ClientId%>').value != '' && document.getElementById('<%=txtDateTo.ClientId%>').value != '')
        { 
           if (compareDates(document.getElementById('<%=txtDateFrom.ClientId%>').value,"d/M/yyyy",document.getElementById('<%=txtDateTo.ClientId%>').value,"d/M/yyyy")==1)
           {
                document.getElementById('<%=lblError.ClientId%>').innerText ='Date to should be greater than or equal to date from.'
                return false;
           }
       }
       
       return true; 
        
    }
    
   
 
    </script>

</body>
</html>
