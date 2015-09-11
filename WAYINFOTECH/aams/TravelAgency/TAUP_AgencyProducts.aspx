<%@ Page Language="VB" AutoEventWireup="false" CodeFile="TAUP_AgencyProducts.aspx.vb" Inherits="TravelAgency_TAUP_AgencyProducts" %>

<%@ Register Src="../UserControl/MenuControl.ascx" TagName="MenuControl" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Manage Agency Products</title>
    <link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" media="all" href="../Calender/calendar-blue.css"
        title="win2k-cold-1" />
    <link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />
        <!-- Designed by Mukund  -->
    <script type="text/javascript" language="javascript">
    
    
    
    </script>

</head>
<script type="text/javascript" src="../JavaScript/AAMS.js"></script>
<!-- import the calendar script -->
<script type="text/javascript" src="../Calender/calendar.js"></script>
<!-- import the language module -->
<script type="text/javascript" src="../Calender/lang/calendar-en.js"></script>
<!-- import the calendar setup module -->
<script type="text/javascript" src="../Calender/calendar-setup.js"></script>

<body>
    <form id="form1" runat="server">
      <table width="860px" align="left" style="height: 486px" class="border_rightred">
            <tr>
                <td valign="top">
                    <table width="100%">
                        <tr>
                            <td valign="top">
                                <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td valign="top" align="left">
                                            <span class="menu">Setup-></span><span class="sub_menu">Agency</span>
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
                                                    <td class="textbold" align="center" valign="TOP" rowspan="0" style="height: 21px">
                                                        <asp:Label ID="lblError" class="ErrorMsg" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="100%" valign="top">
                                                       <asp:Panel ID="pnlEmployee" runat="server" Width="100%">
                                                            <table border="0" align="left" cellpadding="2" cellspacing="0" style="width: 92%">
                                                                
                                                                
                                                                 <tr>
                                                                    <td class="textbold">
                                                                    </td>
                                                                    <td class="textbold" nowrap="nowrap">
                                                                       Product Name<span class="Mandatory">*</span>
                                                                    </td>
                                                                    <td colspan="3">
                                                                        <asp:DropDownList  onkeyup="gotop(this.id)"  ID="drpProductList" runat="server" CssClass="textbold" Width="370px">
                                                                        </asp:DropDownList></td>
                                                                                                                                       
                                                                    <td>
                                                                     <asp:Button ID="btnAdd" runat="server" TabIndex="21" CssClass="button" Text="Add" Width="88px" AccessKey="N" />
                                                                    </td> 
                                                                     
                                                                    
                                                                </tr> 
                                                                
                                                                <tr>
                                                                    <td class="textbold" style="height: 20px;">
                                                                        </td>
                                                                    <td class="textbold" nowrap="nowrap" >
                                                                        Date of Installation<span class="Mandatory">*</span>
                                                                    </td>
                                                                    <td nowrap="nowrap">
                                                                        <asp:TextBox ID="txtInstallDate"  TabIndex="1" CssClass="textbox" runat="server" Width="96px"></asp:TextBox>
                                                                        <img id="imgDateOnline" alt="" src="../Images/calender.gif" title="Date selector"
                                                                            style="cursor: pointer" />

                                                                        <script type="text/javascript">
                                                                                                    Calendar.setup({
                                                                                                    inputField     :    '<%=txtInstallDate.clientId%>',
                                                                                                    ifFormat       :    "%d/%m/%Y",
                                                                                                    button         :    "imgDateOnline",
                                                                                                    //align          :    "Tl",
                                                                                                    singleClick    :    true
                                                                                                    });
                                                                        </script>
                                                                    </td>
                                                                      <td class="textbold" nowrap="nowrap">
                                                                        No. of Terminal Online</td>
                                                                    <td>
                                                                    <asp:TextBox ID="txtTerminalOnline" runat="server" Width="57px" CssClass="textbox" AutoCompleteType="Disabled" ></asp:TextBox>    
                                                                    </td>
                                                                      
                                                                                                                                                                                                         
                                                                    <td>
                                                                    <asp:Button ID="btnSve" runat="server" TabIndex="21" CssClass="button" Text="Save" Width="88px" AccessKey="S" />
                                                                    </td>
                                                                   
                                                                    </tr>
                                                               
                                                                
                                                                <tr>
                                                                    <td class="textbold" style="width: 100px; height: 20px;">
                                                                    </td>
                                                                     <td style="width: 150px"></td>
                                                                      <td style="width: 150px"></td>
                                                                       <td style="width: 150px"></td>
                                                                        <td style="width: 150px"></td>
                                                                        
                                                                     <td>
                                                                     <asp:Button ID="btnReset" runat="server" TabIndex="21" CssClass="button" Text="Reset" Width="88px" AccessKey="R" /></td>
                                                                   
                                                                </tr>
                                                              
                                                                <tr>
                                                                <td></td>
                                                                <td colspan="4">
                                                                <asp:GridView ID="gvAgencyProduct" HeaderStyle-ForeColor="white" runat="server" AutoGenerateColumns="False" TabIndex="5" Width="600px" AllowSorting="True">
                                                                         <Columns>  
                                                                               <asp:TemplateField HeaderText="Product Name" SortExpression="PRODUCTNAME">
                                                                                 <ItemTemplate>
                                                                                <asp:Label Text='<%#Eval("PRODUCTNAME")%>' ID="lblproductname" runat="server" ></asp:Label>                                                                                                                                                                                          
                                                                                 </ItemTemplate>
                                                                         </asp:TemplateField>                                         
                                                                         <asp:TemplateField HeaderText="Installation Date" SortExpression="DATE_INSTALLATION">
                                                                                 <ItemTemplate>
                                                                                <asp:Label Text='<%#Eval("DATE_INSTALLATION")%>' ID="lblDateofIns" runat="server" ></asp:Label> 
                                                                               <asp:HiddenField ID="hdProductID" runat="server" Value='<%#Eval("PRODUCTID")%>' />
                                                                                <asp:HiddenField ID="hdRowNo" runat="server" Value='<%#Eval("ROWID")%>' />                                                                                                                                                                                                                                                                                                          
                                                                                 </ItemTemplate>
                                                                         </asp:TemplateField>
                                                                          <asp:BoundField DataField="TERMINALS_ONLINE" SortExpression="TERMINALS_ONLINE" HeaderText="No. Of Terminals Online" />
                                                                           <asp:TemplateField HeaderText="Action">
                                                                                        <ItemStyle HorizontalAlign="Center" />
                                                                                        <ItemTemplate>                                                                                            
                                                                                          <asp:LinkButton  CssClass="LinkButtons" id="linkEdit" CommandName="EditX" runat="server" Text="Edit" CommandArgument='<%#Eval("TempRowCount")%>'></asp:LinkButton>&nbsp;
                                                                                          <asp:LinkButton  CssClass="LinkButtons" id="lnkDel" CommandName="DeleteX" runat="server" Text="Delete" CommandArgument='<%#Eval("TempRowCount")%>'></asp:LinkButton>                                                                                         
                                                                                           </ItemTemplate>
                                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                                    </asp:TemplateField>
                                                                         </Columns>
                                                                            <AlternatingRowStyle CssClass="lightblue" />
                                                                            <RowStyle CssClass="textbold" />
                                                                            <HeaderStyle CssClass="Gridheading" ForeColor="White" />
                                                                         </asp:GridView>
                                                                </td>
                                                                <td></td>
                                                                </tr>                                                                                                                                                          
                                                                 
                                                                <tr>
                                                <td></td>
                                                <td colspan="2" class="ErrorMsg" style="height: 19px">
                                                        Field Marked * are Mandatory
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
    
    </form>
</body>
</html>
