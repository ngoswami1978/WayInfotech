<%@ Page Language="VB" AutoEventWireup="false" CodeFile="MSUP_AgencyProducts.aspx.vb" Inherits="TravelAgency_MSUP_AgencyProducts" %>

<%@ Register Src="../UserControl/MenuControl.ascx" TagName="MenuControl" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Manage Agency Products</title>
    <link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" media="all" href="../Calender/calendar-blue.css"
        title="win2k-cold-1" />
    <link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" language="javascript">
    function ChkEmptyProductName()
    {
    if(document.getElementById("txtProductName").value=="")
    {
    document.getElementById("lblError").innerText="Product is Mandatory"
    document.getElementById("txtProductName").focus();
    return false;
    }
        }
    
    </script>

    <link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />
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
                                                                    <td class="textbold" style="width: 300px; height: 14px;">
                                                                    </td>
                                                                    <td class="textbold" style="width: 140px; height: 14px;" nowrap="nowrap">
                                                                       Product Name<span class="Mandatory">*</span>
                                                                    </td>
                                                                    <td style="height: 14px; width: 253px;" colspan="3">
                                                                        &nbsp;<asp:DropDownList ID="drpProductNames" runat="server" CssClass="textbold" Width="352px">
                                                                        </asp:DropDownList></td>
                                                                                                                                       
                                                                    <td style="width: 171px; height: 14px;">
                                                                     <asp:Button ID="btnAdd" runat="server" TabIndex="21" CssClass="button" Text="Add" Width="88px" AccessKey="S" />
                                                                    </td> 
                                                                     
                                                                    
                                                                </tr> 
                                                                
                                                                <tr>
                                                                    <td class="textbold" style="width: 300px; height: 14px;">
                                                                        </td>
                                                                    <td class="textbold" style="width: 140px; height: 14px;" nowrap="nowrap" >
                                                                        Date of Installation
                                                                    </td>
                                                                    <td nowrap="nowrap" style="height: 14px; width: 154px;">
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
                                                                      <td class="textbold" style="width: 141px; height: 20px;" nowrap="nowrap">
                                                                        No. of Terminal Online</td>
                                                                    <td style="height: 20px; width: 211px;">
                                                                    <asp:TextBox ID="txtEquipQty" runat="server" Width="56px" CssClass="textbox" ></asp:TextBox>    
                                                                    </td>
                                                                      
                                                                                                                                                                                                         
                                                                    <td style="width: 171px; height: 14px;">
                                                                    <asp:Button ID="btnSve" runat="server" TabIndex="21" CssClass="button" Text="Save" Width="88px" AccessKey="S" />
                                                                    </td>
                                                                   
                                                                    </tr>
                                                               
                                                                
                                                                <tr>
                                                                    <td class="textbold" style="width: 300px; height: 20px;">
                                                                    </td>
                                                                     <td></td>
                                                                      <td style="width: 154px"></td>
                                                                       <td style="width: 141px"></td>
                                                                        <td style="width: 211px"></td>
                                                                        
                                                                     <td style="width: 171px; height: 20px;">
                                                                     <asp:Button ID="btnReset" runat="server" TabIndex="21" CssClass="button" Text="Reset" Width="88px" AccessKey="R" /></td>
                                                                   
                                                                </tr>
                                                              
                                                                <tr>
                                                                <td style="height: 116px; width: 300px;"></td>
                                                                <td colspan="4" style="height: 116px">
                                                                <asp:GridView ID="gvAgencyProduct" runat="server"  EnableViewState ="false" AutoGenerateColumns="False" TabIndex="5" Width="92%">
                                                                         <Columns>                                               
                                                                         <asp:TemplateField HeaderText="Installation Date">
                                                                                 <ItemTemplate> 
                                                                                 <asp:HiddenField ID="hdProductID" runat="server" Value=' <%#Eval("PRODUCTID")%> ' />
                                                                                 <asp:HiddenField ID="hdRowID" runat="server" Value='<%#Eval("ROWID")%>' />
                                                                                 <%#Eval("DATE_INSTALLATION")%>                                                                                                                                                   
                                                                                 </ItemTemplate>
                                                                         </asp:TemplateField>
                                                                         
                                                                         <asp:TemplateField HeaderText="Product Name">
                                                                                 <ItemTemplate> 
                                                                                       <%#Eval("PRODUCTNAME")%>                    
                                                                                 </ItemTemplate>
                                                                         </asp:TemplateField>  
                                                                         <asp:TemplateField HeaderText="No. Of Terminals Online">
                                                                                 <ItemTemplate> 
                                                                                       <%#Eval("TERMINALS_ONLINE")%>
                                                                                 </ItemTemplate>
                                                                         </asp:TemplateField>                                                       
                                                                                    <asp:TemplateField HeaderText="Action">
                                                                                        <ItemStyle HorizontalAlign="Center" />
                                                                                        <ItemTemplate>
                                                                                            <a href="#" class="LinkButtons" id="linkEdit" runat="server">Edit</a> 
                                                                                            <a href="#" class="LinkButtons" id="linkDelete" runat="server">Delete</a>
                                                                                        </ItemTemplate>
                                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                                    </asp:TemplateField>
                                                                                                                                                                                                                                                                  
                                                                                        
                                                                                 
                                                                         </Columns>
                                                                            <AlternatingRowStyle CssClass="lightblue" />
                                                                            <RowStyle CssClass="textbold" />
                                                                            <HeaderStyle CssClass="Gridheading" />
                                                                         </asp:GridView>
                                                                </td>
                                                                <td style="height: 116px; width: 211px;"></td>
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
