<%@ Page Language="VB" AutoEventWireup="false" CodeFile="MSSR_Agency.aspx.vb" Inherits="TravelAgency_MSSR_Agency" EnableEventValidation="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>AAMS: Agency</title>
    <link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" media="all" href="../Calender/calendar-blue.css" title="win2k-cold-1" />   
    <link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />
</head>
<script type="text/javascript" src="../JavaScript/AAMS.js"></script>
<!-- import the calendar script -->
<script type="text/javascript" src="../Calender/calendar.js"></script>
<!-- import the language module -->
<script type="text/javascript" src="../Calender/lang/calendar-en.js"></script>
<!-- import the calendar setup module -->
<script type="text/javascript" src="../Calender/calendar-setup.js"></script>
 <script language="javascript" type="text/javascript">
    function AdvanceSearch()
    {           
        if(document.getElementById('<%=hdAdvanceSearch.ClientID%>').value==1)
        {
            document.getElementById('btnUp').src='../images/up.jpg';
            document.getElementById('<%=pnlAdvanceSearch.ClientID%>').style.display ='block'
            document.getElementById('<%=hdAdvanceSearch.ClientID%>').value='';
        }
        else
        {
            document.getElementById('btnUp').src="../images/down.jpg";
            document.getElementById('<%=pnlAdvanceSearch.ClientID%>').style.display ='none'
            document.getElementById('<%=hdAdvanceSearch.ClientID%>').value=1;
        }        
    }
    function OnloadAdvanceSearch()
    {            
       if(document.getElementById('<%=hdAdvanceSearch.ClientID%>').value==1)
       {            
           document.getElementById('<%=pnlAdvanceSearch.ClientID%>').style.display ='none'            
       }
       else
       {
           document.getElementById('<%=pnlAdvanceSearch.ClientID%>').style.display ='block'
       }
     }
     function AgencyReset()
    {
        document.getElementById("txtAoffice").value="";       
        //return false;
    }
    function EditFunction(CheckBoxObj)
    {           
        window.location.href="MSUP_Agency.aspx?Action=U|"+CheckBoxObj;       
        return false;
    }
    function DeleteFunction(CheckBoxObj)
    {   
        if (confirm("Are you sure you want to delete?")==true)
        {          
            window.location.href="MSSR_Agency.aspx?Action=D|"+CheckBoxObj+"|"+document.getElementById("txtAoffice").value;       
            return false;
        }
    }
    function NewFunction()
    {   
        window.location.href="MSUP_Agency.aspx?Action=I";       
        return false;
    }   
    </script>
<body onload="javascript:OnloadAdvanceSearch();">
    <form id="form1" runat="server">
        <table width="860px" align="left" class="border_rightred" style="height: 486px;">
            <tr>
                <td valign="top">
                    <table width="100%" align="left">
                        <tr>
                            <td valign="top" align="left">
                                <span class="menu">Setup-></span><span class="sub_menu">Agency</span>
                            </td>
                        </tr>
                        <tr>
                            <td class="heading" align="center" valign="top">
                                Search Agency
                            </td>
                        </tr>
                        <tr>
                            <td valign="top">
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td align="LEFT" class="redborder">
                                            <table width="100%" border="0" align="left" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td class="textbold" colspan="6" align="center">
                                                        &nbsp;<asp:Label ID="lblError" runat="server" CssClass="ErrorMsg"></asp:Label>
                                                    </td>
                                                </tr>
                                                 <tr>
                                                    <td width="6%" class="textbold" >
                                                        &nbsp;</td>
                                                    <td width="18%" class="textbold">
                                                      </td>
                                                    <td width="20%">
                                                      </td>
                                                    <td width="18%">
                                                      </td>
                                                    <td width="20%">
                                                        </td>
                                                    <td width="18%">
                                                      </td>
                                                </tr>
                                                <tr>
                                                    <td width="6%" class="textbold" height="25px">
                                                        &nbsp;</td>
                                                    <td width="18%" class="textbold">
                                                        Agency
                                                        Name</td>
                                                    <td colspan="3">
                                                        <asp:TextBox ID="txtAgencyName" CssClass="textbox" MaxLength="40" runat="server" Width="452px"></asp:TextBox><span class="textbold"></span></td>
                                                    <td width="18%">
                                                        <asp:Button ID="btnSearch" CssClass="button" runat="server" Text="Search" /></td>
                                                </tr>
                                                <tr>
                                                    <td class="textbold" height="25px">
                                                        &nbsp;</td>
                                                    <td class="textbold">
                                                        Short Name</td>
                                                    <td>
                                                        <asp:TextBox ID="txtShortName" runat="server" CssClass="textbox" MaxLength="40"></asp:TextBox></td>
                                                    <td class="textbold">
                                                        City</td>
                                                    <td>
                                                        <asp:DropDownList ID="drpCity" CssClass="dropdownlist" Width="137px" runat="server">
                                                        </asp:DropDownList></td>
                                                    <td>
                                                        <asp:Button ID="btnNew" CssClass="button" runat="server" Text="New" /></td>
                                                </tr>
                                                <tr>
                                                    <td class="textbold" height="25px">
                                                        &nbsp;</td>
                                                    <td class="textbold">
                                                        Office ID</td>
                                                    <td>
                                                        <asp:TextBox ID="txtOfficeId" runat="server" CssClass="textbox" MaxLength="40"></asp:TextBox></td>
                                                    <td class="textbold">
                                                        Country</td>
                                                    <td>
                                                        <asp:DropDownList ID="drpCountry" CssClass="dropdownlist" Width="137px" runat="server">
                                                        </asp:DropDownList></td>
                                                    <td>
                                                        <asp:Button ID="btnReset" CssClass="button" runat="server" Text="Reset" /></td>
                                                </tr>
                                                <tr>
                                                    <td class="textbold" style="height: 25px">
                                                    </td>
                                                    <td class="textbold" >
                                                        Online Status</td>
                                                    <td>
                                                        <asp:DropDownList ID="drpOnlineStatus" CssClass="dropdownlist" Width="137px" runat="server">
                                                        </asp:DropDownList></td>
                                                    <td  class="textbold">
                                                        Aoffice</td>
                                                    <td>
                                                        <asp:DropDownList ID="drpAoffice" CssClass="dropdownlist" Width="137px" runat="server">
                                                        </asp:DropDownList></td>
                                                    <td>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="textbold" style="height: 25px">
                                                    </td>
                                                    <td class="textbold">
                                                        CRS</td>
                                                    <td>
                                                        <asp:DropDownList ID="drpCRS" CssClass="dropdownlist" Width="137px" runat="server">
                                                        </asp:DropDownList></td>
                                                    <td class="textbold">
                                                    </td>
                                                    <td>
                                                    </td>
                                                    <td>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td >
                                                        &nbsp;</td>
                                                    <td colspan="4" class="subheading"> 
                                                        <img src="../Images/down.jpg" style="cursor:pointer" id="btnUp" onclick="Javascript:return AdvanceSearch();" />&nbsp;&nbsp;<a href="#" onclick="Javascript:return AdvanceSearch();" class="menu">Advance Search</a>&nbsp;
                                                    </td>
                                                    
                                                    <td >
                                                        &nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td colspan="6">
                                                        <asp:Panel ID="pnlAdvanceSearch"   runat="server" Width="100%">
                                                            <table style="width: 100%" cellpadding="0" cellspacing="0" border="0">
                                                                <tr>
                                                                    <td width="6%" class="textbold" style="height: 25px">
                                                                    </td>
                                                                    <td width="18%" class="textbold">
                                                                        Address</td>
                                                                    <td width="20%">
                                                                        <asp:TextBox ID="txtAddress" CssClass="textbox" MaxLength="40" runat="server"></asp:TextBox></td>
                                                                    <td width="18%">
                                                                        <span class="textbold">Agency Status</span></td>
                                                                    <td width="20%"><asp:DropDownList ID="drpAgencyStatus" CssClass="dropdownlist" Width="137px" runat="server">
                                                                    </asp:DropDownList></td>
                                                                    <td width="18%">
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="textbold" style="height: 25px">
                                                                    </td>
                                                                    <td class="textbold" >
                                                                        Agency Type</td>
                                                                    <td ><asp:DropDownList ID="drpAgencyType" CssClass="dropdownlist" Width="137px" runat="server">
                                                                    </asp:DropDownList></td>
                                                                    <td class="textbold">
                                                                        Email</td>
                                                                    <td >
                                                                        <asp:TextBox ID="txtEmail" runat="server" CssClass="textbox" MaxLength="40"></asp:TextBox></td>
                                                                    <td >
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="textbold" style="height: 25px">
                                                                    </td>
                                                                    <td class="textbold" >
                                                                        Date Offline</td>
                                                                    <td >
                                                                        <asp:TextBox ID="txtDateOffline" runat="server" CssClass="textbox" MaxLength="40"></asp:TextBox>
                                                                        <img id="imgDateOffline" alt="" src="../Images/calender.gif" TabIndex="16" title="Date selector" style="cursor: pointer" />

                                                                                                 <script type="text/javascript">
                                                                                                    Calendar.setup({
                                                                                                    inputField     :    '<%=txtDateOffline.clientId%>',
                                                                                                    ifFormat       :    "%d/%m/%Y",
                                                                                                    button         :    "imgDateOffline",
                                                                                                    //align          :    "Tl",
                                                                                                    singleClick    :    true
                                                                                                    });
                                                                                                  </script>
                                                                        </td>
                                                                    <td  class="textbold">
                                                                        Date Online</td>
                                                                    <td >
                                                                        <asp:TextBox ID="txtDateOnline" runat="server" CssClass="textbox" MaxLength="40"></asp:TextBox>
                                                                        <img id="imgDateOnline" alt="" src="../Images/calender.gif" TabIndex="16" title="Date selector" style="cursor: pointer" />

                                                                                                 <script type="text/javascript">
                                                                                                    Calendar.setup({
                                                                                                    inputField     :    '<%=txtDateOnline.clientId%>',
                                                                                                    ifFormat       :    "%d/%m/%Y",
                                                                                                    button         :    "imgDateOnline",
                                                                                                    //align          :    "Tl",
                                                                                                    singleClick    :    true
                                                                                                    });
                                                                                                  </script>
                                                                        </td>
                                                                    <td >
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="textbold" style="height: 25px">
                                                                    </td>
                                                                    <td class="textbold">
                                                                        Fax</td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtFax" runat="server" CssClass="textbox" MaxLength="40"></asp:TextBox></td>
                                                                    <td class="textbold" >
                                                                        File Number</td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtFielNumber" runat="server" CssClass="textbox" MaxLength="40"></asp:TextBox></td>
                                                                    <td>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="textbold" style="height: 25px">
                                                                    </td>
                                                                    <td class="textbold">
                                                                        IATA ID</td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtIATAId" runat="server" CssClass="textbox" MaxLength="40"></asp:TextBox></td>
                                                                    <td class="textbold">
                                                                        </td>
                                                                    <td >
                                                                        </td>
                                                                    <td >
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="textbold" >
                                                                    </td>
                                                                    <td class="textbold" >
                                                                    </td>
                                                                    <td>
                                                                    </td>
                                                                    <td class="textbold">
                                                                    </td>
                                                                    <td>
                                                                    </td>
                                                                    <td>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
                                                    <asp:HiddenField ID="hdAdvanceSearch" EnableViewState="true" runat="server" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="textbold">
                                                        &nbsp;</td>
                                                    <td colspan="2">
                                                    </td>
                                                    <td>
                                                        &nbsp;</td>
                                                    <td>
                                                        &nbsp;</td>
                                                    <td>
                                                        &nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td colspan="6" height="4">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="6" height="4">
                                                        <asp:DataGrid ID="grdAgency" BorderWidth="1" BorderColor="#d4d0c8" runat="server"
                                                            AutoGenerateColumns="False" ItemStyle-CssClass="ItemColor" AlternatingItemStyle-CssClass="lightblue"
                                                            HeaderStyle-CssClass="Gridheading" Width="100%">
                                                           <Columns>
                                                                <asp:TemplateColumn HeaderText="Location Code">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblLocationCode" runat="server" Text='<%#Eval("LOCATION_CODE")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Left" />
                                                                    <HeaderStyle HorizontalAlign="Left" />
                                                                </asp:TemplateColumn>
                                                                <asp:TemplateColumn HeaderText="Office Id">
                                                                    <ItemTemplate>
                                                                        <%#Eval("OfficeID")%>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Left" />
                                                                    <HeaderStyle HorizontalAlign="Left" />
                                                                </asp:TemplateColumn>
                                                                <asp:TemplateColumn HeaderText="Name">
                                                                    <ItemTemplate>
                                                                        <%#Eval("NAME")%>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Left" />
                                                                    <HeaderStyle HorizontalAlign="Left" />
                                                                </asp:TemplateColumn>
                                                                <asp:TemplateColumn HeaderText="Address">
                                                                    <ItemTemplate>
                                                                        <%#Eval("ADDRESS")%>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Left" />
                                                                    <HeaderStyle HorizontalAlign="Left" />
                                                                </asp:TemplateColumn>
                                                                <asp:TemplateColumn HeaderText="Address1">
                                                                    <ItemTemplate>
                                                                        <%#Eval("ADDRESS1")%>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Left" />
                                                                    <HeaderStyle HorizontalAlign="Left" />
                                                                </asp:TemplateColumn>
                                                                 <asp:TemplateColumn HeaderText="City">
                                                                    <ItemTemplate>
                                                                        <%#Eval("CITY")%>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Left" />
                                                                    <HeaderStyle HorizontalAlign="Left" />
                                                                </asp:TemplateColumn>
                                                                 <asp:TemplateColumn HeaderText="Country">
                                                                    <ItemTemplate>
                                                                        <%#Eval("COUNTRY")%>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Left" />
                                                                    <HeaderStyle HorizontalAlign="Left" />
                                                                </asp:TemplateColumn>
                                                                 <asp:TemplateColumn HeaderText="Online Status">
                                                                    <ItemTemplate>
                                                                        <%#Eval("ONLINE_STATUS")%>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Left" />
                                                                    <HeaderStyle HorizontalAlign="Left" />
                                                                </asp:TemplateColumn>
                                                                <asp:TemplateColumn HeaderText="Action">
                                                                    <ItemTemplate>
                                                                        <a href="#" class="LinkButtons" id="btnEdit" runat="server">Edit</a> <a href="#" class="LinkButtons" id="btnDelete" runat="server">
                                                                                                Delete</a>
                                                             <asp:HiddenField ID="hdCITY" runat="server" Value='<%#Eval("CITY")%>' />  
                                                                    </ItemTemplate>
                                                                </asp:TemplateColumn>
                                                            </Columns>
                                                            <AlternatingItemStyle CssClass="lightblue" />
                                                            <ItemStyle CssClass="textbold" />
                                                            <HeaderStyle CssClass="Gridheading" />
                                                        </asp:DataGrid></td>
                                                </tr>
                                                <tr>
                                                    <td colspan="6" height="12">
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
