<%@ Page Language="VB" AutoEventWireup="false" CodeFile="PUSR_IspName.aspx.vb" Inherits="Popup_PUSR_IspName" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="head1" runat="server">
    <title>AAMS:Manage ISP Name</title>
     <base target="_self"/>
    <script src="../JavaScript/AAMS.js" type="text/javascript"></script> 
    <script language ="javascript" type ="text/javascript" >
          function ISPReset()
            {
                document.getElementById("txtISPName").value="";         
                //document.getElementById("drpCityName").selectedIndex=0;     
                document.getElementById("lblError").innerHTML="";    
                if ( document.getElementById("grdvISP")!=null)       
                document.getElementById("grdvISP").style.display ="none"; 
                document.getElementById("txtISPName").focus();  
                return false;
            }
    </script> 

<link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" defaultbutton="btnSearch" runat="server" defaultfocus="txtISPName">
        <table width="860px" align="left" class="border_rightred" style="height: 486px;">
            <tr>
                <td valign="top" style="height: 482px">
                    <table width="100%" align="left">
                        <tr>
                            <td valign="top" align="left">
                                <span class="menu">ISP-&gt;</span><span class="sub_menu">ISP Name</span>
                            </td>
                        </tr>
                        <tr>
                            <td class="heading" align="center" valign="top">
                                Search ISP </td>
                        </tr>
                        <tr>
                            <td valign="top" style="height: 314px">
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td align="LEFT" class="redborder">
                                            <table width="100%" border="0" align="left" cellpadding="0" cellspacing="0">
                                                                <tr>
                                        <td class="redborder center">
                                    <table border="0" cellpadding="2" cellspacing="1" style="width: 100%" class="left">
                                                                        <tr>
                                                                            <td colspan="4" class="center gap">
                                                                <asp:Label ID="lblError" runat="server" CssClass="ErrorMsg"></asp:Label></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="width: 245px">
                                                                            </td>
                                                                            <td class="textbold">
                                                                                ISP Name</td>
                                                                            <td>
                                                                    <asp:TextBox ID="txtISPName" runat="server" CssClass="textbold" Width="208px"></asp:TextBox>
                                                                    </td>
                                                                            <td>
                                                                    <asp:Button ID="btnSearch" CssClass="button" runat="server" Text="Search" /></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="width: 245px; height: 26px;">
                                                                            </td>
                                                                            <td class="textbold" height: 26px; valign ="middle">City Name</td>
                                                                            <td height: 26px; valign ="middle"><asp:DropDownList ID="drpCityName" runat="server"  Width="214px"  Height="26px" CssClass="textboxgrey" Enabled="False">
                                                                    </asp:DropDownList></td>
                                                                            <td style="height: 26px">
                                                                    <asp:Button ID="btnReset" CssClass="button" runat="server" Text="Reset" /></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="width: 245px; height: 23px;">
                                                                            </td>
                                                                            <td class="textbold" style="height: 23px">
                                                             </td>
                                                                            <td style="height: 23px">
                                                                    </td>
                                                                            <td style="height: 23px">
                                                                    </td>
                                                                        </tr>                                                                       
                                       
                                               
                                                <tr>
                                                    <td colspan="7" style="height: 4px">
                                                        <asp:GridView ID="grdvISP" BorderWidth="1" BorderColor="#d4d0c8" runat="server"
                                                            AutoGenerateColumns="False" ItemStyle-CssClass="ItemColor" AlternatingItemStyle-CssClass="lightblue"
                                                            HeaderStyle-CssClass="Gridheading" Width="100%">
                                                            <Columns>
                                                                <asp:BoundField DataField="Name" HeaderText="ISP Name"   />
                                                                <asp:BoundField DataField="Address" HeaderText="Address"   />
                                                                <asp:BoundField DataField="CityName" HeaderText="City Name"   />
                                                                <asp:BoundField DataField="PinCode" HeaderText="PIN Code"   />
                                                                <asp:BoundField DataField="CTCName" HeaderText="Contact Person "   />
                                                                <asp:BoundField DataField="Phone" HeaderText="Phone No."   />
                                                                <asp:BoundField DataField="Fax" HeaderText="Fax No."   />
                                                                <asp:BoundField DataField="Email" HeaderText="Email ID"   />
                                                                    <asp:TemplateField HeaderText="Action">                                                                        
                                                                            <ItemTemplate>
                                                                            <asp:LinkButton  ID="lnkSelect" Runat="server" CssClass="LinkButtons" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "ISPID") + "|" + DataBinder.Eval(Container.DataItem, "Name")+ "|" + DataBinder.Eval(Container.DataItem, "CityName") %>'>Select</asp:LinkButton>
                                                                            </ItemTemplate>                                                                       
                                                                </asp:TemplateField>
                                                            </Columns>
                                                            <AlternatingRowStyle CssClass="lightblue" />
                                                            <RowStyle CssClass="textbold" />
                                                            <HeaderStyle CssClass="Gridheading" />
                                                        </asp:GridView>
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
              <td> <asp:Literal ID="litIspName" runat="server"></asp:Literal></td>
            </tr>
        </table>
        </td> 
        </tr> 
        </table> 
    </form>
</body>
</html>
