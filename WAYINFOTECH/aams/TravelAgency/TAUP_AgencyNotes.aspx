<%@ Page Language="VB" AutoEventWireup="false" CodeFile="TAUP_AgencyNotes.aspx.vb" Inherits="TravelAgency_TAUP_AgencyNotes" ValidateRequest="false" EnableEventValidation='false'  %>

<%@ Register Src="../UserControl/MenuControl.ascx" TagName="MenuControl" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Agency Notes</title>
    <link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" media="all" href="../Calender/calendar-blue.css"
        title="win2k-cold-1" />
       
    <script type="text/javascript" language="javascript">
    function ChkEmptyText()
    {
    if(document.getElementById("txtNotes").value=="")
    {
    document.getElementById("lblError").innerText="Notes cannot be blank";
    document.getElementById("txtNotes").focus();
    return false;
    }
    }
    
    </script>
</head>
<script type="text/javascript" src="../JavaScript/AAMS.js"></script>
<!-- import the calendar script -->
<script type="text/javascript" src="../Calender/calendar.js"></script>
<!-- import the language module -->
<script type="text/javascript" src="../Calender/lang/calendar-en.js"></script>
<!-- import the calendar setup module -->
<script type="text/javascript" src="../Calender/calendar-setup.js"></script>

<body >
    <form id="form1" runat="server">
    <table width="860px" align="left" style="height: 486px" class="border_rightred">
            <tr>
                <td valign="top" style="width: 856px; height: 505px;">
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
                                                    <td class="textbold" align="center" valign="TOP" rowspan="0" style="height: 25px" colspan="3">
                                                        <asp:Label ID="lblError" class="ErrorMsg" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                <td width="10%"></td>
                                                   <td valign="top" style="width: 80%">
                                                            <table width="100%" border="0" align="left" cellpadding="2" cellspacing="0">
                                                                
                                                                 <tr>
                                                                <td nowrap="nowrap" align="left">
                                                                <asp:TextBox ID="txtNotes" runat="server" TextMode="MultiLine" Height="136px" MaxLength="2000" Width="728px" CssClass="textbox" ></asp:TextBox>
                                                                     <asp:Button ID="btnSave" runat="server" TabIndex="2" CssClass="button" Text="Save" Width="72px" AccessKey="S" />
                                                                     
                                                                     </td>
                                                                     <td valign="bottom"   >                                                                         
                                                                     </td>
                                                                <td></td>
                                                                </tr> 
                                                                <tr align ="left">
                                                                  <td class ="textbold">
                                                                      <asp:LinkButton ID="LnkInstallationDetails" runat ="server" Visible ="false" Text ="Installation Details"   ></asp:LinkButton> 
                                                                  </td> 
                                                                </tr>
                                                                <tr>                                                               
                                                                <td colspan="3">
                                                                <asp:GridView ID="gvAgencyNotes" runat="server"  EnableViewState ="true" AutoGenerateColumns="False" TabIndex="5" Width="100%" AllowSorting="true" HeaderStyle-ForeColor="white" >
                                                                     <Columns>                                               
                                                                         <asp:TemplateField HeaderText="User Name" HeaderStyle-HorizontalAlign="left" ItemStyle-Wrap="false" SortExpression="EmployeeID" >
                                                                                 <ItemTemplate> 
                                                                                      <%#Eval("EmployeeName")%>                                                                      
                                                                                 </ItemTemplate>
                                                                                 <ItemStyle Width="20%" />
                                                                                 
                                                                         </asp:TemplateField>
                                                                         <asp:TemplateField HeaderText="Date Time" HeaderStyle-HorizontalAlign="left"  ItemStyle-Wrap="false" SortExpression="DateTime">
                                                                                 <ItemTemplate>
                                                                                      <%#Eval("DateTime")%>  
                                                                                 </ItemTemplate>
                                                                                  <ItemStyle Width="20%" />
                                                                         </asp:TemplateField> 
                                                                            
                                                                          <asp:TemplateField HeaderText="Notes" HeaderStyle-HorizontalAlign="left" ItemStyle-Wrap="true" SortExpression="Notes" >
                                                                                 <ItemTemplate> 
                                                                                 <asp:Label ID="lblNotes" runat="server" Text='<%#Eval("Notes")%> '></asp:Label>
                                                                                      
                                                                                 </ItemTemplate>
                                                                                  <ItemStyle Width="60%" Wrap="true" HorizontalAlign="left" />
                                                                                 
                                                                         </asp:TemplateField>   
                                                                       
                                                                                  
                                                                     </Columns>
                                                                            <AlternatingRowStyle CssClass="lightblue" />
                                                                            <RowStyle CssClass="textbold" />
                                                                            <HeaderStyle CssClass="Gridheading" Wrap="false" />
                                                                            
                                                                         </asp:GridView>
                                                                </td>
                                                               
                                                               
                                                                </tr>                
                                                            
                                                                
                                                            </table>
                                                      
                                                    </td>
                                                     <td width="10%"></td>
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
