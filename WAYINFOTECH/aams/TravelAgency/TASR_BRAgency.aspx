<%@ Page Language="VB" AutoEventWireup="false" EnableEventValidation ="false" ValidateRequest ="false"   CodeFile="TASR_BRAgency.aspx.vb" Inherits="TravelAgency_TASR_BRAgency" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
    <title>AAMS: Agency</title>
    <link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" media="all" href="../Calender/calendar-blue.css"
        title="win2k-cold-1" />
</head>

<script type="text/javascript" src="../JavaScript/AAMS.js"></script>

<script type="text/javascript" src="../Calender/calendar.js"></script>

<script type="text/javascript" src="../Calender/lang/calendar-en.js"></script>

<script type="text/javascript" src="../Calender/calendar-setup.js"></script>

<body onload="javascript:OnloadAdvanceSearchTravelAgency();">
    <form id="form1" runat="server" defaultfocus="txtAgencyName" defaultbutton="btnSearch">
        <table width="860px" class="border_rightred">
            <tr>
                <td>
                    <table width="100%">
                        <tr>
                            <td align="left">
                                <span class="menu">Travel Agency -&gt;</span><span class="sub_menu">Birdres Agency Search</span>
                            </td>
                        </tr>
                        <tr>
                            <td class="heading" align="center">
                                Birdres Search Agency
                            </td>
                        </tr>
                        <tr>
                            <td valign="top">
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td align="LEFT" class="redborder">
                                            <table width="850px" border="0" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td class="textbold" colspan="6" align="center">
                                                        &nbsp;<asp:Label ID="lblError" runat="server" CssClass="ErrorMsg"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 6%">
                                                    </td>
                                                    <td style="width: 18%">
                                                    </td>
                                                    <td style="width: 20%">
                                                    </td>
                                                    <td style="width: 18%">
                                                    </td>
                                                    <td style="width: 20%">
                                                    </td>
                                                    <td style="width: 18%">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                    </td>
                                                    <td class="textbold">
                                                        Agency Name</td>
                                                    <td colspan="3">
                                                        <asp:DropDownList onkeyup="gotop(this.id)" ID="drpSearchType" CssClass="dropdownlist"
                                                            Width="104px" runat="server" TabIndex="1">
                                                            <asp:ListItem>Contains</asp:ListItem>
                                                            <asp:ListItem>Starting With</asp:ListItem>
                                                            <asp:ListItem>Exactly</asp:ListItem>
                                                        </asp:DropDownList>&nbsp;&nbsp;<asp:TextBox ID="txtAgencyName" CssClass="textbox"
                                                            MaxLength="100" runat="server" Width="341px" TabIndex="2"></asp:TextBox><span class="textbold"></span></td>
                                                    <td>
                                                        <asp:Button ID="btnSearch" CssClass="button" runat="server" Text="Search" TabIndex="31"
                                                            AccessKey="A" /></td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                    </td>
                                                    <td class="textbold">
                                                        Short Name</td>
                                                    <td>
                                                        <asp:TextBox ID="txtShortName" runat="server" CssClass="textbox" MaxLength="40" TabIndex="3"></asp:TextBox></td>
                                                    <td class="textbold">
                                                        City</td>
                                                    <td>
                                                        <asp:DropDownList onkeyup="gotop(this.id)" ID="drpCity" CssClass="dropdownlist" Width="137px"
                                                            runat="server" TabIndex="4">
                                                        </asp:DropDownList></td>
                                                    <td>
                                                        <asp:Button ID="btnExport" runat="server" CssClass="button" TabIndex="33" Text="Export"
                                                            AccessKey="E" /></td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                    </td>
                                                    <td class="textbold">
                                                        Office ID</td>
                                                    <td>
                                                        <asp:TextBox ID="txtOfficeId" runat="server" CssClass="textbox" MaxLength="40" TabIndex="5"></asp:TextBox></td>
                                                    <td class="textbold">
                                                        Country</td>
                                                    <td>
                                                        <asp:DropDownList onkeyup="gotop(this.id)" ID="drpCountry" CssClass="dropdownlist"
                                                            Width="137px" runat="server" TabIndex="6">
                                                        </asp:DropDownList></td>
                                                    <td>
                                                        <asp:Button ID="btnReset" CssClass="button" runat="server" Text="Reset" TabIndex="34"
                                                            AccessKey="R" /></td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                    </td>
                                                    <td class="textbold">
                                                        CRS</td>
                                                    <td>
                                                        <asp:DropDownList onkeyup="gotop(this.id)" ID="drpCRS" CssClass="dropdownlist" Width="137px"
                                                            runat="server" TabIndex="8">
                                                        </asp:DropDownList></td>
                                                    <td class="textbold">
                                                        Aoffice</td>
                                                    <td>
                                                        <asp:DropDownList onkeyup="gotop(this.id)" ID="drpAoffice" CssClass="dropdownlist"
                                                            Width="137px" runat="server" TabIndex="9">
                                                        </asp:DropDownList></td>
                                                    <td>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                    </td>
                                                    <td colspan="4" class="subheading">
                                                        <img alt="" src="../Images/down.jpg" style="cursor: pointer" id="btnUp" runat="server"
                                                            tabindex="10" />&nbsp;&nbsp;<asp:LinkButton ID="lnkAdvance" CssClass="menu" Text="Advance Search"
                                                                runat="server"></asp:LinkButton>
                                                    </td>
                                                    <td>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="6">
                                                        <asp:Panel ID="pnlAdvanceSearch" runat="server" Width="100%">
                                                            <table style="width: 100%" cellpadding="0" cellspacing="0" border="0">
                                                                <tr>
                                                                    <td width="6%">
                                                                    </td>
                                                                    <td width="18%" class="textbold">
                                                                        Address</td>
                                                                    <td colspan="3">
                                                                        <asp:TextBox ID="txtAddress" CssClass="textbox" MaxLength="200" runat="server" TabIndex="10"
                                                                            Width="452px"></asp:TextBox><span class="textbold"></span></td>
                                                                    <td width="18%">
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td width="6%">
                                                                    </td>
                                                                    <td class="textbold" width="18%">
                                                                        Online Status</td>
                                                                    <td width="20%">
                                                                        <asp:DropDownList onkeyup="gotop(this.id)" ID="drpOnlineStatus" CssClass="dropdownlist"
                                                                            Width="137px" runat="server" TabIndex="11">
                                                                        </asp:DropDownList></td>
                                                                    <td width="18%">
                                                                        <span style="font-size: 9pt; font-family: Arial">Agency Status</span></td>
                                                                    <td width="20%">
                                                                        <asp:DropDownList onkeyup="gotop(this.id)" ID="drpAgencyStatus" CssClass="dropdownlist"
                                                                            Width="137px" runat="server" TabIndex="12">
                                                                        </asp:DropDownList></td>
                                                                    <td width="18%">
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                    </td>
                                                                    <td class="textbold">
                                                                        Agency Type</td>
                                                                    <td>
                                                                        <asp:DropDownList onkeyup="gotop(this.id)" ID="drpAgencyType" CssClass="dropdownlist"
                                                                            Width="137px" runat="server" TabIndex="13">
                                                                        </asp:DropDownList></td>
                                                                    <td class="textbold">
                                                                        Email</td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtEmail" runat="server" CssClass="textbox" MaxLength="40" TabIndex="14"></asp:TextBox></td>
                                                                    <td>
                                                                    </td>
                                                                </tr>
                                                                  <tr>
                                                                    <td ></td>
                                                                    <td class="textbold" >Date Offline From</td>
                                                                    <td ><asp:TextBox ID="txtDateOfflineF" runat="server" CssClass="textbox" MaxLength="40" TabIndex="1"></asp:TextBox>
                                                                        <img id="imgDateOfflineF" alt="" src="../Images/calender.gif" TabIndex="1" title="Date selector" style="cursor: pointer" />

                                                                                                 <script type="text/javascript">
                                                                                                    Calendar.setup({
                                                                                                    inputField     :    '<%=txtDateOfflineF.clientId%>',
                                                                                                    ifFormat       :    "%d/%m/%Y",
                                                                                                    button         :    "imgDateOfflineF",
                                                                                                    //align          :    "Tl",
                                                                                                    singleClick    :    true
                                                                                                    });
                                                                                                  </script>
                                                                        </td>
                                                                    <td  class="textbold">Date Offline To</td>
                                                                    <td ><asp:TextBox ID="txtDateOfflineT" runat="server" CssClass="textbox" MaxLength="40" TabIndex="1"></asp:TextBox>
                                                                        <img id="imgDateOfflineT" alt="" src="../Images/calender.gif" TabIndex="1" title="Date selector" style="cursor: pointer" />

                                                                                                 <script type="text/javascript">
                                                                                                    Calendar.setup({
                                                                                                    inputField     :    '<%=txtDateOfflineT.clientId%>',
                                                                                                    ifFormat       :    "%d/%m/%Y",
                                                                                                    button         :    "imgDateOfflineT",
                                                                                                    //align          :    "Tl",
                                                                                                    singleClick    :    true
                                                                                                    });
                                                                                                  </script>
                                                                        </td>
                                                                    <td ></td>
                                                                </tr>
                                                                  <tr>
                                                                    <td ></td>
                                                                    <td class="textbold" >Date Online From</td>
                                                                    <td ><asp:TextBox ID="txtDateOnlineF" runat="server" CssClass="textbox" MaxLength="40" TabIndex="1"></asp:TextBox>
                                                                        <img id="imgDateOnlineF" alt="" src="../Images/calender.gif" TabIndex="1" title="Date selector" style="cursor: pointer" />

                                                                                                 <script type="text/javascript">
                                                                                                    Calendar.setup({
                                                                                                    inputField     :    '<%=txtDateOnlineF.clientId%>',
                                                                                                    ifFormat       :    "%d/%m/%Y",
                                                                                                    button         :    "imgDateOnlineF",
                                                                                                    //align          :    "Tl",
                                                                                                    singleClick    :    true
                                                                                                    });
                                                                                                  </script>
                                                                        </td>
                                                                    <td  class="textbold">Date Online To</td>
                                                                    <td ><asp:TextBox ID="txtDateOnlineT" runat="server" CssClass="textbox" MaxLength="40" TabIndex="1"></asp:TextBox>
                                                                        <img id="imgDateOnlineT" alt="" src="../Images/calender.gif" TabIndex="1" title="Date selector" style="cursor: pointer" />

                                                                                                 <script type="text/javascript">
                                                                                                    Calendar.setup({
                                                                                                    inputField     :    '<%=txtDateOnlineT.clientId%>',
                                                                                                    ifFormat       :    "%d/%m/%Y",
                                                                                                    button         :    "imgDateOnlineT",
                                                                                                    //align          :    "Tl",
                                                                                                    singleClick    :    true
                                                                                                    });
                                                                                                  </script>
                                                                        </td>
                                                                    <td ></td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                    </td>
                                                                    <td class="textbold">
                                                                        Fax</td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtFax" runat="server" CssClass="textbox" MaxLength="40" TabIndex="19"></asp:TextBox></td>
                                                                    <td class="textbold">
                                                                        File Number</td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtFielNumber" runat="server" CssClass="textbox" MaxLength="5" TabIndex="20"></asp:TextBox></td>
                                                                    <td>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                    </td>
                                                                    <td class="textbold">
                                                                        IATA ID</td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtIATAId" runat="server" CssClass="textbox" MaxLength="20" TabIndex="21"></asp:TextBox></td>
                                                                    <td class="textbold">
                                                                        IP Address</td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtIPAddress" runat="server" CssClass="textbox" MaxLength="16" TabIndex="22"></asp:TextBox></td>
                                                                    <td>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="height: 21px">
                                                                    </td>
                                                                    <td class="textbold" style="height: 21px">
                                                                        Backup</td>
                                                                    <td style="height: 21px">
                                                                        <asp:DropDownList ID="drpBackupOnlineStatus" runat="server" CssClass="dropdownlist"
                                                                            onkeyup="gotop(this.id)" TabIndex="23" Width="137px">
                                                                        </asp:DropDownList></td>
                                                                    <td class="textbold" style="height: 21px">
                                                                        Phone</td>
                                                                    <td style="height: 21px">
                                                                        <asp:TextBox ID="txtPhone" runat="server" CssClass="textbox" MaxLength="30" TabIndex="24"></asp:TextBox></td>
                                                                    <td style="height: 21px">
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                    </td>
                                                                    <td class="textbold">
                                                                        Lcode</td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtLcode" runat="server" CssClass="textbox" MaxLength="9" TabIndex="25"></asp:TextBox></td>
                                                                    <td class="textbold">
                                                                        Chain Code</td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtChainCode" runat="server" CssClass="textbox" MaxLength="9" TabIndex="26"></asp:TextBox></td>
                                                                    <td>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                    </td>
                                                                    <td class="textbold">
                                                                        Website</td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtWeb" runat="server" CssClass="textbox" MaxLength="100" TabIndex="27"></asp:TextBox></td>
                                                                    <td class="textbold">
                                                                        Priority</td>
                                                                    <td>
                                                                        <asp:DropDownList onkeyup="gotop(this.id)" ID="drpPriority" CssClass="dropdownlist"
                                                                            Width="137px" runat="server" TabIndex="28">
                                                                        </asp:DropDownList></td>
                                                                    <td>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                    </td>
                                                                    <td class="textbold">
                                                                        1Aresponsibility</td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtAResponsibility" runat="server" CssClass="textbox" MaxLength="40"
                                                                            TabIndex="29" Width="131px"></asp:TextBox><img alt="" onclick="javascript:return EmployeePageTravelAgency();"
                                                                                src="../Images/lookup.gif" tabindex="30" style="cursor: pointer;" /></td>
                                                                    <td class="textbold" align="left">
                                                                        Agency Using Birdres</td>
                                                                    <td class="textbold">
                                                                        <asp:CheckBox ID="chkAgencyUsingBirdres" runat="server" /></td>
                                                                    <td>
                                                                        <input type="hidden" id="hdEmployeePageName" style="width: 1px" runat='server' /></td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
                                                        <asp:HiddenField ID="hdAdvanceSearch" EnableViewState="true" runat="server" />
                                                        <asp:HiddenField ID="hdEmployeeName" EnableViewState="true" runat="server" />
                                                        <asp:HiddenField ID="hdDeleteFlag" runat="server" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="6" class="gap">
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="height: 5px;">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="6" style="width: 840px" valign="top" class="gap redborder">
                                            <table width="840px">
                                                <tr>
                                                    <td colspan="6" style="width: 840px" valign="top">
                                                        <asp:GridView ID="grdAgency" runat="server" AutoGenerateColumns="False" TabIndex="34"
                                                            Width="840px" EnableViewState="false" AllowSorting="true" HeaderStyle-ForeColor="white">
                                                            <Columns>
                                                                <asp:BoundField DataField="CHAIN_CODE" HeaderText="Chain Code " SortExpression="CHAIN_CODE">
                                                                    <HeaderStyle Width="10%" Wrap="False" />
                                                                    <ItemStyle Wrap="False" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="LOCATION_CODE" HeaderText="Lcode " SortExpression="LOCATION_CODE">
                                                                    <HeaderStyle Width="6%" Wrap="False" />
                                                                    <ItemStyle Wrap="False" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="OfficeID" HeaderText="Office ID " SortExpression="OfficeID">
                                                                    <HeaderStyle Width="10%" Wrap="False" />
                                                                    <ItemStyle Wrap="False" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="NAME" HeaderText="Name " SortExpression="NAME">
                                                                    <HeaderStyle Width="15%" Wrap="False" />
                                                                    <ItemStyle Wrap="True" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="ADDRESS" HeaderText="Address " SortExpression="ADDRESS">
                                                                    <HeaderStyle Width="10%" Wrap="False" />
                                                                    <ItemStyle Wrap="True" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="ADDRESS1" HeaderText="Address1 " SortExpression="ADDRESS1">
                                                                    <HeaderStyle Width="9%" Wrap="False" />
                                                                    <ItemStyle Wrap="True" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="CITY" HeaderText="City " SortExpression="CITY">
                                                                    <HeaderStyle Width="10%" Wrap="False" />
                                                                    <ItemStyle Wrap="True" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="COUNTRY" HeaderText="Country " SortExpression="COUNTRY">
                                                                    <HeaderStyle Width="10%" Wrap="False" />
                                                                    <ItemStyle Wrap="True" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="ONLINE_STATUS" HeaderText="Online Status " SortExpression="ONLINE_STATUS">
                                                                    <HeaderStyle Width="10%" Wrap="False" />
                                                                    <ItemStyle Wrap="False" />
                                                                </asp:BoundField>
                                                                <asp:TemplateField HeaderText="Action">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="lnkSelect" runat="server" CssClass="LinkButtons" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "LOCATION_CODE") + "|" + DataBinder.Eval(Container.DataItem, "NAME")+ "|" + DataBinder.Eval(Container.DataItem, "CITY") + "|" + DataBinder.Eval(Container.DataItem, "COUNTRY")+ "|" + DataBinder.Eval(Container.DataItem, "PHONE") + "|" + DataBinder.Eval(Container.DataItem, "OfficeID")+ "|" + DataBinder.Eval(Container.DataItem, "FAX") + "|" + DataBinder.Eval(Container.DataItem,"ONLINE_STATUS") + "|" + DataBinder.Eval(Container.DataItem,"Aoffice") %>'>Select</asp:LinkButton>&nbsp;
                                                                        <%--<a href="#" class="LinkButtons" id="btnEdit" runat="server">Edit</a>&nbsp;<asp:LinkButton ID="btnDelete" runat="server" CommandName ="DeleteX" Text ="Delete" CssClass="LinkButtons"></asp:LinkButton>--%>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle Width="10%" />
                                                                    <ItemStyle Wrap="False" />
                                                                </asp:TemplateField>
                                                            </Columns>
                                                            <AlternatingRowStyle CssClass="lightblue" />
                                                            <RowStyle CssClass="textbold" />
                                                            <HeaderStyle CssClass="Gridheading" ForeColor="White" />
                                                        </asp:GridView>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="6" valign="top">
                                                        <asp:Panel ID="pnlPaging" runat="server" Visible="false" Width="840px">
                                                            <table border="0" cellpadding="0" cellspacing="0" width="840px">
                                                                <tr class="paddingtop paddingbottom">
                                                                    <td style="width: 30%" class="left">
                                                                        <span class="textbold"><b>&nbsp;No. of records found</b></span>&nbsp;&nbsp;<asp:TextBox
                                                                            ID="txtTotalRecordCount" runat="server" Width="105px" CssClass="textboxgrey"
                                                                            ReadOnly="True"></asp:TextBox></td>
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
                <td>
                    <asp:TextBox ID="txtRecordOnCurrentPage" runat="server" Width="73px" CssClass="textboxgrey"
                        Visible="false"></asp:TextBox></td>
            </tr>
        </table>
    </form>

    <script type="text/javascript">
    
    
    
    
    
     function SelectBRFunctionTravelAgency(str3,strAdd,strAdd2)
    {  
    
     
//        alert(str3); 
//          alert(strAdd); 
//            alert(strAdd2); 
//    
        var pos=str3.split('|'); 
        var passOutCode="0";

     if (window.opener.document.forms['form1']['ChkGrpProductivity']!=null)
            {    
             window.opener.document.forms['form1']['ChkGrpProductivity'].disabled=false;
            }

    // used in course session
    if (window.opener.document.forms['form1']['hdCourseLCode']!=null)
    {
    window.opener.document.forms['form1']['hdCourseLCode'].value=pos[0];
    window.opener.document.forms['form1']['txtAgency'].value=pos[1];
    window.close();
    }
  
    if (window.opener.document.forms['form1']['hdtxtAgencyName']!=null)
    {
        window.opener.document.forms['form1']['hdtxtAgencyName'].value=pos[1];        
        if(window.opener.document.forms['form1']['hdtxtAgencyName'].value.trim()!="")
        {
        if ( window.opener.document.forms['form1']['chbWholeGroup']!=null)
        {
            window.opener.document.forms['form1']['chbWholeGroup'].disabled=false;
        }
        }
    }   
        
        // Code For Training Module
        
         if (window.opener.document.forms['form1']['hdTrainingLCode']!=null)
        { 
        window.opener.document.forms['form1']['hdTrainingLCode'].value=pos[0];
        window.opener.document.forms['form1']['txtAgency'].value=pos[1];
        window.close();
        }
        
        //For Update
        if (window.opener.document.forms['form1']['hdAgencyNameParticipantBasket']!=null)
        { 
        window.opener.document.forms['form1']['hdAgencyNameParticipantBasket'].value=str3 + "\n" +strAdd;
        window.opener.document.forms['form1']['txtAgencyName'].value=pos[1];
        window.opener.document.forms['form1']['txtAddress'].value=strAdd + '\n ' + strAdd2;//pos[2];;
        window.opener.document.forms['form1']['txtCountry'].value=pos[3];
        window.opener.document.forms['form1']['txtCity'].value=pos[2];
        window.opener.document.forms['form1']['txtPhone'].value=pos[4];
        window.opener.document.forms['form1']['txtFax'].value=pos[6];
        window.opener.document.forms['form1']['txtOnlineStatus'].value=pos[7];
        //window.opener.document.forms['form1']['txtPriority'].value=pos[5];//pos[7];
       
         window.close();
         return false;
        }
        //


         if (window.opener.document.forms['form1']['hdAgencyStaffAgencyName']!=null)
        { 
        window.opener.document.forms['form1']['hdAgencyStaffAgencyName'].value=str3;
        window.opener.document.forms['form1']['txtAgencyName'].value=pos[1];
        window.close();
        }
      if (window.opener.document.forms['form1']['hdCallAgencyName']!=null)
        { 
        
        window.opener.document.forms['form1']['hdCallAgencyName'].value=str3;
        window.opener.document.forms['form1']['txtAgencyName'].value=pos[1];
        window.opener.document.forms['form1']['txtAddress'].value=strAdd + '\n ' + strAdd2;//pos[2];
        window.opener.document.forms['form1']['txtCountry'].value=pos[3];//pos[5];
        window.opener.document.forms['form1']['txtCity'].value=pos[2];//pos[4];
        window.opener.document.forms['form1']['txtPhone'].value=pos[4];//pos[6];
        window.opener.document.forms['form1']['txtFax'].value=pos[6];//pos[8];
        window.opener.document.forms['form1']['txtOnlineStatus'].value=pos[7];//pos[9];
        window.opener.document.forms['form1']['txtOfficeId'].value=pos[5];//pos[7];
        window.opener.document.forms['form1']['hdAOffice'].value=pos[8];//pos[10];
        
        window.opener.document.forms['form1']['txtPincode'].value=pos[9];//pos[7];
        window.opener.document.forms['form1']['txtEmail'].value=pos[10];//pos[10];
       
       
                 //@ Added By Abhishek
			            try
			            { 		
			             // alert(  window.opener.document.forms['form1']['hdLoggedDatetime'].value);	              
			              window.opener.document.forms['form1']['txtLoggedDate'].value=window.opener.document.forms['form1']['hdLoggedDatetime'].value;
			            }
                        catch(err){}
                      //@ Added By Abhishek
			            try			            
			            { 		
			                if(window.opener.document.forms['form1']['BtnChangeContext'] !=null)			              
			               {	                
			                window.opener.document.forms['form1']['BtnChangeContext'].click();
			               }
			            }
                        catch(err){}
       
       
          window.close();
           return false;
        }
         if (window.opener.document.forms['form1']['hdChallanLCode']!=null)
        { 
        window.opener.document.forms['form1']['hdChallanLCodeTemp'].value=pos[0];
        window.opener.document.forms['form1']['hdChallanLCode'].value=pos[0];
        window.opener.document.forms['form1']['txtAgencyName'].value=pos[1];
        window.opener.document.forms['form1']['txtAddress'].value=strAdd + '\n ' + strAdd2;//pos[2];
        window.opener.document.forms['form1']['txtCountry'].value=pos[3];//pos[5];
        window.opener.document.forms['form1']['txtCity'].value=pos[2];//pos[4];
        window.opener.document.forms['form1']['txtPhone'].value=pos[4];//pos[6];
        window.opener.document.forms['form1']['txtFax'].value=pos[6];//pos[8];
        window.opener.document.forms['form1']['txtOfficeId'].value=pos[5];;//pos[7];
           window.opener.document.forms['form1'].submit();    
           passOutCode="1";
            window.close();
        }
        if (passOutCode=="0")
        {
        if (window.opener.document.forms['form1']['hidLcode']!=null)
        {
        window.opener.document.forms['form1']['hidLcode'].value=pos[0];
        }
       // hdlcode.Value
        if (window.opener.document.forms['form1']['hdLcode']!=null)
        {
        window.opener.document.forms['form1']['hdLcode'].value=pos[0];
        }
        if (window.opener.document.forms['form1']['hdAgencyNameId']!=null)
        {
        window.opener.document.forms['form1']['hdAgencyNameId'].value=pos[0];
        }
         if (window.opener.document.forms['form1']['hdAgencyName']!=null)
        {
        window.opener.document.forms['form1']['hdAgencyName'].value=pos[0];
        window.opener.document.forms['form1']['txtAgencyName'].value=pos[1];
        
        }
          if (window.opener.document.forms['form1']['hdOfficeID']!=null)
        {
        window.opener.document.forms['form1']['hdOfficeID'].value=pos[5];//pos[7];   
        
        }
        
        
        if  (window.opener.document.forms['form1']['hdAgency']!=null)
        {
	    window.opener.document.forms['form1']['hdAgency'].value=pos[1];
	    }
        if  (window.opener.document.forms['form1']['txtAgencyName']!=null)
        {
	    window.opener.document.forms['form1']['txtAgencyName'].value=pos[1];
	    }
	     if  (window.opener.document.forms['form1']['txtAgencyAddress']!=null)
        {
	    window.opener.document.forms['form1']['txtAgencyAddress'].value=strAdd + ' ' + strAdd2 ;//pos[2] + ' ' + pos[3];
	    }
	    if  (window.opener.document.forms['form1']['hdAddress']!=null)
        {
	    window.opener.document.forms['form1']['hdAddress'].value=strAdd + ' ' + strAdd2;//pos[2] ;
	    }
	    if  (window.opener.document.forms['form1']['txtAddress']!=null)
        {
	    window.opener.document.forms['form1']['txtAddress'].value=strAdd + ' ' + strAdd2;//pos[2] ;
	    }
	     if  (window.opener.document.forms['form1']['hdCity']!=null)
        {
	   window.opener.document.forms['form1']['hdCity'].value=pos[2];//pos[4] ;
	    
	    }
	    
	    
	    
	    if  (window.opener.document.forms['form1']['txtCity']!=null)
        {
	    window.opener.document.forms['form1']['txtCity'].value=pos[2];//pos[4] ;
	    }
	    
	    if  (window.opener.document.forms['form1']['hdCountry']!=null)
        {
	    window.opener.document.forms['form1']['hdCountry'].value=pos[3];//pos[5];
	    }
	    if  (window.opener.document.forms['form1']['txtCountry']!=null)
        {
	   window.opener.document.forms['form1']['txtCountry'].value=pos[3];//pos[5];
	    }
	    
	     if  (window.opener.document.forms['form1']['hdPhone']!=null)
        {
	    window.opener.document.forms['form1']['hdPhone'].value=pos[4];//pos[6];
	    }
	    
	    if  (window.opener.document.forms['form1']['txtPhone']!=null)
        {
	    window.opener.document.forms['form1']['txtPhone'].value=pos[4];//pos[6];
	    }
	     if  (window.opener.document.forms['form1']['hdOffice']!=null)
        {
	    window.opener.document.forms['form1']['hdOffice'].value=pos[5];//pos[7];
	    }
	    
	    if  (window.opener.document.forms['form1']['txtOfficeId']!=null)
        {
	    window.opener.document.forms['form1']['txtOfficeId'].value=pos[5];//pos[7];
	    }
	    if  (window.opener.document.forms['form1']['txtOfficeID1']!=null)
        {
	    window.opener.document.forms['form1']['txtOfficeID1'].value=pos[5];//pos[7];
	    }
	      if  (window.opener.document.forms['form1']['hdFax']!=null)
        {
	   window.opener.document.forms['form1']['hdFax'].value=pos[6];//pos[8];
	    }
	    if  (window.opener.document.forms['form1']['txtFax']!=null)
        {
	    window.opener.document.forms['form1']['txtFax'].value=pos[6];//pos[8];
	    }
	    
	   if (window.opener.document.forms['form1']['txtAgencyName']!=null)
	   {
	       if(window.opener.document.forms['form1']['txtAgencyName'].value.trim()!="")
	       {
	          if ( window.opener.document.forms['form1']['chbWholeGroup']!=null)
	          {
               window.opener.document.forms['form1']['chbWholeGroup'].disabled=false;
              }
           }
	   }
	    if (window.opener.document.forms['form1']['drpCity'] !=null )
	    {		    
	         for (i=0;i<window.opener.document.forms['form1']['drpCity'].length;i++)
           {
                if (pos[2] == window.opener.document.forms['form1']['drpCity'].options[i].text) // pos[4]
               {  
                      
               window.opener.document.forms['form1']['drpCity'].options[i].selected = true;
                //document.forms(0).submit();
                break;
               }
           }
       }
        if (window.opener.document.forms['form1']['drpCountry'] !=null )
	    {		    
	         for (i=0;i<window.opener.document.forms['form1']['drpCountry'].length;i++)
           {
                if (pos[3] == window.opener.document.forms['form1']['drpCountry'].options[i].text) //pos[5]
               {  
                      
               window.opener.document.forms['form1']['drpCountry'].options[i].selected = true;
                //document.forms(0).submit();
                break;
               }
           }
       }
        if (window.opener.document.forms['form1']['drpAoffice'] !=null )
	    {		    
	         for (i=0;i<window.opener.document.forms['form1']['drpAoffice'].length;i++)
           {
                if (pos[8] == window.opener.document.forms['form1']['drpAoffice'].options[i].text)//pos[10]
               {  
                      
               window.opener.document.forms['form1']['drpAoffice'].options[i].selected = true;
                //document.forms(0).submit();
                break;
               }
           }
       }
       if (window.opener.document.forms['form1']['drpOnlineStatus'] !=null )
	    {		    
	         for (i=0;i<window.opener.document.forms['form1']['drpOnlineStatus'].length;i++)
           {
                if (pos[7] == window.opener.document.forms['form1']['drpOnlineStatus'].options[i].text)//pos[9]
               {  
                      
               window.opener.document.forms['form1']['drpOnlineStatus'].options[i].selected = true;
                //document.forms(0).submit();
                break;
               }
           }
       }
	   // pankaj
	  if  (window.opener.document.forms['form1']['txtAgencyName']!=null)
        {
	    window.opener.document.forms['form1']['txtAgencyName'].value=pos[1];
	   // window.opener.document.forms['form1']['hdAgencyID'].value=pos[0];
	    }
	     if  (window.opener.document.forms['form1']['hdAgencyID']!=null)
        {
	   window.opener.document.forms['form1']['hdAgencyID'].value=pos[0];
	    }
	    
	    // Code for filling Agency details
	    
	    if (window.opener.document.forms['form1']['hdLCode']!=null)
        { 
        
        window.opener.document.forms['form1']['txtAgencyName'].value=pos[1];
        window.opener.document.forms['form1']['txtAddress'].value=strAdd + ' ' + strAdd2;//pos[2];
        window.opener.document.forms['form1']['txtCountry'].value=pos[3];//pos[5];
        window.opener.document.forms['form1']['txtCity'].value=pos[2];//pos[4];
        window.opener.document.forms['form1']['txtPhone'].value=pos[4];//pos[6];
        window.opener.document.forms['form1']['txtFax'].value=pos[6];//pos[8];
        window.opener.document.forms['form1']['txtOnlineStatus'].value=pos[7];//pos[9];
        window.opener.document.forms['form1']['txtOfficeID'].value=pos[5];//pos[7];
        //window.opener.document.forms['form1']['hdAOffice'].value=pos[10];
          window.opener.document.forms['form1']['hdLCode'].value=pos[0];   
        }
        
        
 //Code Used In ISP Feasibility Request.
//	     if  (window.opener.document.forms['form1']['hdISPRequestPage']!=null)
//        {
//        window.opener.document.forms['form1']['hdlcode'].value=pos[0];
//            window.opener.document.forms['form1'].submit();
//        }
        
        if (window.opener.document.forms['form1']['hdISPRequestPage']!=null)
        { 
            window.opener.document.forms['form1']['hdlcode'].value=pos[0];
             window.opener.document.forms['form1']['hdAgency'].value=pos[1];
           // window.opener.document.forms['form1']['hdAddress'].value=strAdd;
            window.opener.document.forms['form1']['hdAddress'].value=strAdd + '\n ' + strAdd2;
            window.opener.document.forms['form1']['hdOffice'].value=pos[5];
            window.opener.document.forms['form1']['hdFax'].value=pos[6];
             window.opener.document.forms['form1']['hdPhone'].value=pos[4];
             window.opener.document.forms['form1']['hdPin'].value=pos[9];
               window.opener.document.forms['form1']['hdCountry'].value=pos[14];
                 window.opener.document.forms['form1']['hdCity'].value=pos[13];
                 window.opener.document.forms['form1']['hdConcernPerson'].value=pos[11];
            
            
            
            window.opener.document.forms['form1']['txtAgencyName'].value=pos[1];
            window.opener.document.forms['form1']['txtAddress'].value=strAdd + '\n ' + strAdd2;
            window.opener.document.forms['form1']['txtOfficeId'].value=pos[5];
            window.opener.document.forms['form1']['txtFax'].value=pos[6];
             window.opener.document.forms['form1']['txtPhone'].value=pos[4];
             window.opener.document.forms['form1']['txtPinCode'].value=pos[9];
             
             window.opener.document.forms['form1']['txtConcernPerson'].value=pos[12];
             
            window.opener.document.forms['form1']['txtCountry'].value=pos[3];
           window.opener.document.forms['form1']['txtCity'].value=pos[2];
             
         
        }
	    }
	  	window.close();
        }
             
    
    </script>

</body>
</html>
