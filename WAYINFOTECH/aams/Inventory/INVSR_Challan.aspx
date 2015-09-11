<%@ Page Language="VB" AutoEventWireup="false" CodeFile="INVSR_Challan.aspx.vb" Inherits="Inventory_INVSR_Challan" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
<link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />
    <title>AAMS::Inventory::Search Challan</title>
    <script src="../JavaScript/AAMS.js" type="text/javascript"></script>
   <link rel="stylesheet" type="text/css" media="all" href="../Calender/calendar-blue.css" title="win2k-cold-1" />
<script type="text/javascript" src="../Calender/calendar.js"></script>
<!-- import the language module -->
<script type="text/javascript" src="../Calender/lang/calendar-en.js"></script>
<!-- import the calendar setup module -->
<script type="text/javascript" src="../Calender/calendar-setup.js"></script>
<script type="text/javascript" src="../JavaScript/DisableRightClick.js"></script>
  <script language="javascript" type="text/javascript">
 
  function EditFunction(ChallanID)
        {           
          window.location.href="INVUP_Challan.aspx?Action=U&ChallanID="+ChallanID;               
          
          return false;
        }
         function DeleteFunction(ChallanID)
        {
           
           if (confirm("Are you sure you want to delete?")==true)
           {
           document.getElementById("hdDeleteId").value=ChallanID;       
           
           }
           else
           {
                document.getElementById("hdDeleteId").value="";
                 return false;
           }
        }
  
  </script>
</head>
<body>
    <form id="form1" runat="server" defaultfocus="ddlGodown" defaultbutton="btnSearch">
     <table>
    <tr>
    <td>
    <table width="860px"  class="border_rightred left">
            <tr>
                <td class="top">
                    <table width="100%" class="left">
                        <tr>
                            <td  ><span class="menu"> Inventory -&gt;</span><span class="sub_menu">Challan </span></td>
                        </tr>
                        <tr>
                            <td class="heading center" >Search Challan</td>
                        </tr>
                        <tr>
                            <td >
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td class="redborder center">
                                    <table border="0" cellpadding="2" cellspacing="1" style="width: 100%" class="left">
                                                                        <tr>
                                                                            <td class="center gap" colspan="6"  >
                                                                <asp:Label ID="lblError" runat="server" CssClass="ErrorMsg" EnableViewState="False"></asp:Label></td>
                                                                        </tr>
                                        <tr>
                                            <td></td>
                                            <td class="textbold"> Godown</td>                                                                               
                                            <td colspan="3">
                                             <asp:DropDownList  onkeyup="gotop(this.id)"  ID="ddlGodown" runat="server" CssClass="dropdownlist" Width="532px" TabIndex="2">
                                                    <asp:ListItem Selected="True" Value="">--All--</asp:ListItem>
                                             </asp:DropDownList>&nbsp;
                                            </td>
                                            <td style="width: 12%;" class="left">
                                            <asp:Button ID="btnSearch" CssClass="button" runat="server" Text="Search" TabIndex="3" OnClientClick="return InventoryChallanPage()" AccessKey="a" /></td>
                                        </tr>
                                        <tr>
                                            <td></td>
                                            <td class="textbold">Challan Category <span class="Mandatory">*</span></td>
                                            <td>
                                                <asp:DropDownList  onkeyup="gotop(this.id)"  ID="ddlChallanCategory" runat="server" CssClass="dropdownlist" Width="176px" TabIndex="2">
                                                    <asp:ListItem Selected="True" Value="">--Select One--</asp:ListItem>
                                                    <asp:ListItem Value="1">Customer</asp:ListItem>
                                                    <asp:ListItem Value="2">Purchase Order</asp:ListItem>
                                                    <asp:ListItem Value="3">Replacement</asp:ListItem>      
                                                    <asp:ListItem Value="4">Stock Transfer</asp:ListItem>     
                                               
                                                </asp:DropDownList></td>
                                            <td class="textbold">Challan Type</td>
                                            <td>
                                                <asp:DropDownList  onkeyup="gotop(this.id)"  ID="ddlChallanType" runat="server" CssClass="dropdownlist" Width="176px" TabIndex="2">
                                                <asp:ListItem Selected="true" Value="" >--All--</asp:ListItem>
                                                <asp:ListItem  Value="1">Issue</asp:ListItem>
                                                <asp:ListItem  Value="2">Receive</asp:ListItem>
                                                </asp:DropDownList></td>
                                            <td class="left"><asp:Button ID="btnNew" CssClass="button" runat="server" Text="New" TabIndex="3" AccessKey="n" /></td>
                                        </tr>
                                          <tr>
                                            <td></td>
                                            <td class="textbold">&nbsp;Date From <span class="Mandatory">*</span></td>
                                            <td>
                                                <asp:TextBox ID="txtChallanDateFrom" runat="server" CssClass="textbox" Width="170px" TabIndex="2"></asp:TextBox>
                                                <img id="imgOpenedDateFrom" alt="" TabIndex="2" src="../Images/calender.gif"  title="Date selector" style="cursor: pointer" />
                                                <script type="text/javascript">
                                                  Calendar.setup({
                                                  inputField     :    '<%=txtChallanDateFrom.clientId%>',
                                                  ifFormat       :    "%d/%m/%Y",
                                                  button         :    "imgOpenedDateFrom",
                                                 //align          :    "Tl",
                                                  singleClick    :    true
                                                 
                                                               });
                                            </script></td>
                                            <td class="textbold">&nbsp;Date To <span class="Mandatory">*</span></td>
                                            <td>
                                                <asp:TextBox ID="txtChallanDateTo" runat="server" CssClass="textbox" Width="170px" TabIndex="2"></asp:TextBox>
                                                <img id="imgOpenedDateTo" alt="" TabIndex="2" src="../Images/calender.gif"  title="Date selector" style="cursor: pointer" />
                                                <script type="text/javascript">
                                                  Calendar.setup({
                                                  inputField     :    '<%=txtChallanDateTo.clientId%>',
                                                  ifFormat       :    "%d/%m/%Y",
                                                  button         :    "imgOpenedDateTo",
                                                 //align          :    "Tl",
                                                  singleClick    :    true
                                                
                                                               });
                                            </script>
                                                </td>
                                            <td class="left">
                                               <asp:Button ID="btnReset" CssClass="button" runat="server" Text="Reset" TabIndex="3" AccessKey="r" /></td>
                                        </tr>
                                          <tr>
                                            <td>  </td> <td class="textbold" >   Challan No.</td>    <td>
                                <asp:TextBox ID="txtChallanNo" runat="server" CssClass="textbox" TabIndex="2" MaxLength="50" Width="170px"></asp:TextBox></td><td > </td><td>
                                                &nbsp;</td>
                                            <td>
                                                <asp:Button ID="btnExport" runat="server" CssClass="button" TabIndex="3" Text="Export" OnClientClick="return InventoryChallanPage()" AccessKey="e" /></td>
                                        </tr>                           
                                        <tr>
                                            <td ></td>
                                            <td >       </td>    <td  colspan="4">
                                                <input id="hdDeleteId" runat="server" style="width: 1px" type="hidden" enableviewstate="true"/>
                                                <input id="hdRecordOnCurrentPage" runat="server" style="width: 6px" type="hidden" />
                                                </td>
                                        </tr>
                                                                        <tr>
                                                                            <td colspan="6" ></td>
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
    <tr><td class="top border_rightred">
<table width="100%">

<tr>
<td class="redborder top" >
 <asp:GridView  ID="gvChallan" runat="server"  AutoGenerateColumns="False" Width="1050px" TabIndex="6" EnableViewState="False" AllowSorting="True">
                                                                                <Columns>
                                                                               
                                                                                    <asp:BoundField HeaderText="Challan No" DataField="ChallanNumber" SortExpression="ChallanNumber" >
                                                                                     <ItemStyle Width="9%" />
                                                                                    </asp:BoundField>
                                                                                    <asp:BoundField HeaderText="Creation Date" DataField="CreationDate" SortExpression="CreationDate" >
                                                                                    <ItemStyle Width="11%" />
                                                                                    </asp:BoundField>
                                                                                    <asp:BoundField HeaderText="Date" DataField="ChallanDate" SortExpression="ChallanDate" >
                                                                                    <ItemStyle Width="8%" />
                                                                                    </asp:BoundField>
                                                                                    <asp:BoundField HeaderText="Category" DataField="ChallanCategory" SortExpression="ChallanCategory" >
                                                                                    <ItemStyle Width="10%" />
                                                                                    </asp:BoundField>
                                                                                    <asp:BoundField HeaderText="Type" DataField="ChallanType" SortExpression="ChallanType" >
                                                                                    <ItemStyle Width="5%" />
                                                                                    </asp:BoundField>
                                                                                    <asp:BoundField HeaderText="Supplier Name" DataField="SupplierName" SortExpression="SupplierName" >
                                                                                    <ItemStyle Width="10%" />
                                                                                    </asp:BoundField>
                                                                                    <asp:BoundField HeaderText="Agency Name" DataField="AgencyName" SortExpression="AgencyName" >
                                                                                    <ItemStyle Width="13%" />
                                                                                    </asp:BoundField>
                                                                                    <asp:BoundField HeaderText="Office ID" DataField="OfficeID" SortExpression="OfficeID" >
                                                                                    <ItemStyle Width="7%" />
                                                                                    </asp:BoundField>
                                                                                    <asp:BoundField HeaderText="From Godown" DataField="GodownName" SortExpression="GodownName" >
                                                                                    <ItemStyle Width="10%" />
                                                                                    </asp:BoundField>
                                                                                    <asp:BoundField HeaderText="To Godown" DataField="RGodownName" SortExpression="RGodownName" >
                                                                                    <ItemStyle Width="10%" />
                                                                                    </asp:BoundField>
                                                                                    
                                                                                    <asp:TemplateField HeaderText="Action">
                                                            <ItemTemplate>
                                                            <asp:LinkButton ID="lnkEdit" runat="server" CommandName ="EditX" Text ="Edit" CssClass="LinkButtons"></asp:LinkButton>&nbsp;
                                                            <asp:LinkButton ID="lnkDelete" runat="server" CommandName ="DeleteX" Text ="Delete" CssClass="LinkButtons"></asp:LinkButton>
                                                               <asp:HiddenField ID="hdChallanID" runat="server" Value='<%#Eval("ChallanID")%>' />   
                                                                
                                                             </ItemTemplate>
                                                                                        <ItemStyle Width="7%" />
                                                           </asp:TemplateField>                                                  
                                                 
                                                 </Columns>
                                                    <AlternatingRowStyle CssClass="lightblue" />
                                                    <RowStyle CssClass="textbold" />
                                                    <HeaderStyle CssClass="Gridheading" ForeColor="White" />                                                   
                                                 </asp:GridView>
                                                 
                                                     <asp:Panel ID="pnlPaging" runat="server" Visible="false" Width="100%">
                                                      <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                      <tr class="paddingtop paddingbottom"  >                                                                                                                                                
                                                                          <td style="width: 30%" class="left"><span  class="textbold"><b>&nbsp;No. of records found</b></span>&nbsp;&nbsp;<asp:TextBox ID="txtTotalRecordCount" runat ="server"  Width="105px" CssClass="textboxgrey" ReadOnly="True" ></asp:TextBox></td>
                                                                          <td style="width: 25%" class="right">                                                                             
                                                                              <asp:LinkButton ID="lnkPrev" CssClass="LinkButtons" runat="server" CommandName="Prev"><< Prev</asp:LinkButton></td>
                                                                          <td style="width: 20%" class="center">
                                                                              <span class ="textbold" ><b>Page No</b></span>&nbsp;&nbsp;<asp:DropDownList ID="ddlPageNumber" Width="70px" CssClass="dropdownlist" runat="server" AutoPostBack="True" >
                                                                              </asp:DropDownList></td>
                                                                          <td style="width: 25%" class="left">
                                                                              <asp:LinkButton ID="lnkNext" runat="server" CssClass="LinkButtons" CommandName="Next">Next >></asp:LinkButton></td>
                                                                      </tr>
                                                                  </table></asp:Panel>
  
</td></tr></table>
</td></tr>
</table>
   
   
    </form>
     <script language="javascript" type="text/javascript">
   
    function ValidateForm()
    {
      document.getElementById('<%=lblError.ClientId%>').innerText=''            
   if (compareDates(document.getElementById('<%=txtChallanDateFrom.ClientId%>').value,"dd/MM/yyyy",document.getElementById('<%=txtChallanDateTo.ClientId%>').value,"dd/MM/yyyy")==1)
       {
            document.getElementById('<%=lblError.ClientId%>').innerText ='Challan date to should be greater than or equal to Challan date from.';
            return false;
       }
            
       return true; 
        
    }
    
    </script>
</body>
</html>
