<%@ Page Language="VB" AutoEventWireup="false" CodeFile="MTSR_MarketShare.aspx.vb"
    Inherits="Market_MTSR_MarketShare" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>AAMS: Market</title>
    <link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="../JavaScript/AAMS.js"></script>

    <script type="text/javascript" language="javascript">
    
    
function chkShowBreakup()
{

if(document.getElementById("chkShowBr").checked==true)
{
  document.getElementById("btnGraph").style.display ="none";  
         
}
else
        {
            document.getElementById("btnGraph").style.display ="block";  
        }
}
    
         function GraphFunction(FMonth,TMonth,FYear,TYear,LimAoff,LimReg,LimOwnOff,AirCode,SelectedBy, SelectedByValue,AirLineName,City,Country,Region,Aoff,OnCarr,GType,Com_Ver)
     {
         var parameter="Case=MarketShareGraphRowWise&Fmonth=" + FMonth  + "&TMonth=" + TMonth + "&FYear=" + FYear +  "&TYear=" + TYear +   "&LimAoff=" +  LimAoff + "&LimReg=" + LimReg + "&LimOwnOff=" + LimOwnOff + "&AirCode=" + AirCode + "&SelectedBy=" +  SelectedBy + "&SelectedByValue=" + SelectedByValue + "&AirLineName=" + AirLineName +  "&City=" + City + "&Country=" + Country  + "&Region=" + Region   +  "&Aoff=" + Aoff  + "&OnCarr=" + OnCarr + "&GType=" + GType  +  "&Param=1"  + "&Com_Ver=" + Com_Ver ;
         type="../RPSR_ReportShow.aspx?Popup=T&" + parameter;   
         window.open(type,"AirLineDetials","height=600,width=920,top=30,left=20,scrollbars=1,status=1");     
         return false;        
                                           
     }
    function SearchValidate()
    {
        if(document.getElementById("drpAirLineName").selectedIndex=='0')
        {
        document.getElementById("lblError").innerHTML="Airline Name is Mandatory";
        return false;
        }
         document.getElementById("lblError").innerHTML="";
    }
    
    function showHideBreakup()
{
            var a = null;
            var f = document.forms[0];
            var e = f.elements["rdSummaryOption"];

            for (var i=0; i < e.length; i++)
            {
            if (e[i].checked)
            {
            a = e[i].value;
            break;
            }
            }


    if(a=='6')
    {
    document.getElementById("dv1").style.display='none';
    }
    else
    {
    document.getElementById("dv1").style.display='block';
    }

}




     function CallPrint( strid )
{
var prtContent = document.getElementById( strid );
prtContent.border = 0; //set no border here
var strOldOne=prtContent.innerHTML;
var WinPrint = window.open('','','letf=100,top=100,width=1000,height=1000,toolbar=0,scrollbars=1,status=0,resizable=1');
WinPrint.document.write(prtContent.outerHTML);
WinPrint.document.close();
WinPrint.focus();
WinPrint.print();
WinPrint.close();

}


    </script>

</head>
<body>
    <form id="form1" runat="server">
        <table width="860px" align="left" class="border_rightred">
            <tr>
                <td valign="top" style="width: 860px; height: 245px;">
                    <table width="100%" align="left">
                        <tr>
                            <td valign="top" align="left">
                                <span class="menu">Market-></span><span class="sub_menu">Market Share</span>
                            </td>
                        </tr>
                        <tr>
                            <td class="heading" align="center" valign="top">
                                Market Share (Airline)</td>
                        </tr>
                        <tr>
                            <td valign="top">
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td align="LEFT">
                                            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                <tr>
                                                    <td style="width: 860px;" class="redborder" valign="top">
                                                        <table width="860px" border="0" align="left" cellpadding="0" cellspacing="0">
                                                            <tr>
                                                                <td width="860px">
                                                                    <table width="860px" border="0" align="left" cellpadding="0" cellspacing="0">
                                                                        <tr>
                                                                            <td class="textbold" colspan="7" align="center" style="height: 15px">
                                                                                &nbsp;<asp:Label ID="lblError" runat="server" CssClass="ErrorMsg"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="textbold" height="25" width="6%">
                                                                            </td>
                                                                            <td class="textbold" style="width: 162px">
                                                                                Airline Name<span class="Mandatory">*</span></td>
                                                                            <td style="width: 172px">
                                                                                <asp:DropDownList ID="drpAirLineName" runat="server" CssClass="dropdownlist" Width="227px"
                                                                                    TabIndex="1">
                                                                                </asp:DropDownList></td>
                                                                            <td align="left" class="textbold" style="width: 115px">
                                                                            </td>
                                                                            <td class="textbold" style="width: 115px" align="left">
                                                                            </td>
                                                                            <td style="width: 236px">
                                                                                <div id="dv1" runat="server">
                                                                                    <asp:CheckBox ID="chkShowBr" runat="server" Text="Show Breakup of Bookings" Width="192px"
                                                                                        CssClass="textbold" TabIndex="1" />
                                                                                </div>
                                                                            </td>
                                                                            <td style="width: 176px" align="center">
                                                                                <asp:Button ID="btnSearch" CssClass="button" runat="server" Text="Search" AccessKey="A"
                                                                                    TabIndex="2" /></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="textbold" style="height: 25px">
                                                                            </td>
                                                                            <td class="textbold" style="width: 162px">
                                                                                Country</td>
                                                                            <td style="width: 172px">
                                                                                <asp:DropDownList ID="drpCountry" runat="server" CssClass="dropdownlist" Width="227px"
                                                                                    TabIndex="1">
                                                                                </asp:DropDownList></td>
                                                                            <td align="left" class="textbold" style="width: 115px">
                                                                            </td>
                                                                            <td class="textbold" style="width: 115px" align="left">
                                                                                City</td>
                                                                            <td style="width: 236px">
                                                                                <asp:DropDownList ID="drpCity" runat="server" CssClass="dropdownlist" Width="227px"
                                                                                    TabIndex="1">
                                                                                </asp:DropDownList></td>
                                                                            <td align="center">
                                                                                <asp:Button ID="btnExport" runat="server" CssClass="button" Text="Export" AccessKey="E"
                                                                                    TabIndex="2" /></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="textbold" style="height: 25px">
                                                                            </td>
                                                                            <td class="textbold" style="width: 162px">
                                                                                1a Office</td>
                                                                            <td style="width: 172px">
                                                                                <asp:DropDownList ID="drpOneAoffice" runat="server" CssClass="dropdownlist" Width="227px"
                                                                                    TabIndex="1">
                                                                                </asp:DropDownList></td>
                                                                            <td align="left" class="textbold" style="width: 115px">
                                                                            </td>
                                                                            <td class="textbold" style="width: 115px" align="left">
                                                                                Region</td>
                                                                            <td style="width: 236px" class="textbold">
                                                                                <asp:DropDownList ID="drpRegion" runat="server" CssClass="dropdownlist" Width="227px"
                                                                                    TabIndex="1">
                                                                                </asp:DropDownList></td>
                                                                            <td align="center">
                                                                                <asp:Button ID="btnReset" CssClass="button" runat="server" Text="Reset" AccessKey="R"
                                                                                    TabIndex="2" /></td>
                                                                        </tr>
                                                                        <tr id="trGroupType" runat="server" >
                                                                            <td class="textbold" style="height: 25px">
                                                                            </td>
                                                                            <td class="textbold" style="width: 162px">
                                                                                Group Type</td>
                                                                            <td style="width: 172px">
                                                                                <asp:DropDownList ID="drpLstGroupType" runat="server" CssClass="dropdown" onkeyup="gotop(this.id)"
                                                                                    TabIndex="1" Width="227px">
                                                                                </asp:DropDownList></td>
                                                                            <td align="left" class="textbold" style="width: 115px">
                                                                            </td>
                                                                            <td align="left" class="textbold" style="width:160px">
                                                                                Company Vertical</td>
                                                                            <td class="textbold" style="width: 236px">
                                                                                <asp:DropDownList ID="DlstCompVertical" runat="server" CssClass="dropdownlist" onkeyup="gotop(this.id)"
                                                                                    TabIndex="1" Width="104px">
                                                                                </asp:DropDownList></td>
                                                                            <td align="center">
                                                                                <asp:Button ID="BtnGraph" CssClass="button" runat="server" Text="Graph" AccessKey="G"
                                                                                    TabIndex="2" /></td>
                                                                        </tr>
                                                                        <tr height="5px">
                                                                            <td class="textbold">
                                                                            </td>
                                                                            <td class="subheading" align="left" colspan="4">
                                                                                Summary Options
                                                                            </td>
                                                                            <td>
                                                                            </td>
                                                                            <td align="center">
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="textbold" style="height: 20px">
                                                                            </td>
                                                                            <td align="left" class="textbold" colspan="4" style="height: 20px">
                                                                                <asp:RadioButtonList ID="rdSummaryOption" runat="server" CssClass="textbold" RepeatDirection="Horizontal"
                                                                                    Width="400px" TabIndex="1">
                                                                                    <asp:ListItem Value="1">City</asp:ListItem>
                                                                                    <asp:ListItem Selected="True" Value="2">Country</asp:ListItem>
                                                                                    <asp:ListItem Value="4">Region</asp:ListItem>
                                                                                    <asp:ListItem Value="3">Amadeus Office</asp:ListItem>
                                                                                    <asp:ListItem Value="6">Agency</asp:ListItem>
                                                                                </asp:RadioButtonList></td>
                                                                            <td style="height: 20px">
                                                                            </td>
                                                                            <td style="height: 20px">
                                                                            </td>
                                                                        </tr>
                                                                        <tr height="18px">
                                                                            <td class="textbold">
                                                                            </td>
                                                                            <td class="subheading" colspan="2">
                                                                                Date From</td>
                                                                            <td class="subheading" colspan="1">
                                                                            </td>
                                                                            <td class="subheading" colspan="2">
                                                                                Date To</td>
                                                                            <td>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="textbold" style="height: 25px">
                                                                            </td>
                                                                            <td class="textbold" style="width: 162px; height: 25px">
                                                                                Month &nbsp;</td>
                                                                            <td style="width: 172px; height: 25px">
                                                                                <asp:DropDownList ID="drpMonthF" runat="server" CssClass="dropdownlist" Width="104px"
                                                                                    TabIndex="1">
                                                                                </asp:DropDownList></td>
                                                                            <td align="left" class="textbold" style="width: 115px; height: 25px">
                                                                            </td>
                                                                            <td class="textbold" style="width: 115px; height: 25px" align="left">
                                                                                Month</td>
                                                                            <td style="width: 236px; height: 25px">
                                                                                <asp:DropDownList ID="drpMonthTo" runat="server" CssClass="dropdownlist" Width="104px"
                                                                                    TabIndex="1">
                                                                                </asp:DropDownList></td>
                                                                            <td style="height: 25px">
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="textbold" style="height: 25px">
                                                                            </td>
                                                                            <td class="textbold" style="height: 25px; width: 162px;">
                                                                                Year</td>
                                                                            <td style="height: 25px; width: 172px;">
                                                                                <asp:DropDownList ID="drpYearF" runat="server" CssClass="dropdownlist" Width="104px"
                                                                                    TabIndex="1">
                                                                                </asp:DropDownList></td>
                                                                            <td align="left" class="textbold" style="width: 115px; height: 25px">
                                                                            </td>
                                                                            <td class="textbold" style="height: 25px; width: 115px;" align="left">
                                                                                Year</td>
                                                                            <td style="height: 25px; width: 236px;">
                                                                                <asp:DropDownList ID="drpYearTo" runat="server" CssClass="dropdownlist" Width="104px"
                                                                                    TabIndex="1">
                                                                                </asp:DropDownList></td>
                                                                            <td style="height: 25px">
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="textbold" style="height: 25px">
                                                                            </td>
                                                                            <td class="textbold" style="width: 162px; height: 25px">
                                                                            </td>
                                                                            <td style="width: 172px; height: 25px">
                                                                            </td>
                                                                            <td class="textbold" style="width: 115px; height: 25px">
                                                                            </td>
                                                                            <td class="textbold" style="width: 115px; height: 25px">
                                                                            </td>
                                                                            <td style="width: 236px; height: 25px">
                                                                            </td>
                                                                            <td style="height: 25px">
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="7" valign="top">
                                                                    <asp:GridView EnableViewState="true" AllowSorting="true" HeaderStyle-ForeColor="white"
                                                                        ID="grdvMarketShare" ShowFooter="true" runat="server" AutoGenerateColumns="False"
                                                                        HorizontalAlign="Center" Width="100%" HeaderStyle-CssClass="Gridheading" RowStyle-CssClass="ItemColor"
                                                                        AlternatingRowStyle-CssClass="lightblue">
                                                                        <Columns>
                                                                            <asp:TemplateField SortExpression="SELECTBY" HeaderText="Selected By" HeaderStyle-Wrap="false">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblSELECTBYVal" runat="server" Text='<%# Eval("SELECTBY") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <FooterTemplate>
                                                                                </FooterTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:BoundField DataField="chain_code" ItemStyle-Width="100px" SortExpression="chain_code"
                                                                                HeaderText="Chain Code" />
                                                                            <asp:TemplateField SortExpression="Name" HeaderText="Agency Name" ItemStyle-Width="120px">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblName" runat="server" Text='<%# Eval("Name") %>' Width="120px"></asp:Label>
                                                                                </ItemTemplate>
                                                                                <FooterTemplate>
                                                                                </FooterTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField SortExpression="Address" HeaderText="Address" HeaderStyle-Wrap="false"
                                                                                ItemStyle-Wrap="true" ItemStyle-Width="140px">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblAddress" runat="server" Text='<%# Eval("Address") %>' Width="140px"></asp:Label>
                                                                                </ItemTemplate>
                                                                                <FooterTemplate>
                                                                                </FooterTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:BoundField DataField="CITY" HeaderText="City" SortExpression="CITY" ItemStyle-Wrap="false"
                                                                                HeaderStyle-Wrap="false" />
                                                                            <asp:TemplateField HeaderText="1A" SortExpression="A" ItemStyle-Width="90px" ItemStyle-HorizontalAlign="Right">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblAVal" runat="server" Text='<%# Eval("A") %>' CssClass="right"></asp:Label>
                                                                                </ItemTemplate>
                                                                                <FooterTemplate>
                                                                                </FooterTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="1B" SortExpression="B" ItemStyle-Width="90px" ItemStyle-HorizontalAlign="Right">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblBVal" runat="server" Text='<%# Eval("B") %>' CssClass="right"></asp:Label>
                                                                                </ItemTemplate>
                                                                                <FooterTemplate>
                                                                                </FooterTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="1G" SortExpression="G" ItemStyle-Width="90px" ItemStyle-HorizontalAlign="Right">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblGVal" runat="server" Text='<%# Eval("G") %>' CssClass="right"></asp:Label>
                                                                                </ItemTemplate>
                                                                                <FooterTemplate>
                                                                                </FooterTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="1P" SortExpression="P" ItemStyle-Width="90px" ItemStyle-HorizontalAlign="Right">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblPVal" runat="server" Text='<%# Eval("P") %>' CssClass="right"></asp:Label>
                                                                                </ItemTemplate>
                                                                                <FooterTemplate>
                                                                                </FooterTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="1W" SortExpression="W" ItemStyle-Width="90px" ItemStyle-HorizontalAlign="Right">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblWVal" runat="server" Text='<%# Eval("W") %>' CssClass="right"></asp:Label>
                                                                                </ItemTemplate>
                                                                                <FooterTemplate>
                                                                                </FooterTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Total" SortExpression="TOTAL" ItemStyle-Width="90px"
                                                                                ItemStyle-HorizontalAlign="Right">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblTotalVal" runat="server" Text='<%# Eval("TOTAL") %>' CssClass="right"></asp:Label>
                                                                                </ItemTemplate>
                                                                                <FooterTemplate>
                                                                                </FooterTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Action">
                                                                                <ItemTemplate>
                                                                                    <a href="#" class="LinkButtons" id="LnkGraph" runat="server">Graph</a>
                                                                                    <asp:HiddenField ID="hdSelectedBy" runat="server" Value='<%#Eval("SELECTBY")%>' />
                                                                                </ItemTemplate>
                                                                                <ItemStyle Wrap="False" Width="30px" />
                                                                            </asp:TemplateField>
                                                                        </Columns>
                                                                        <AlternatingRowStyle CssClass="lightblue" />
                                                                        <HeaderStyle CssClass="Gridheading" HorizontalAlign="Left" Wrap="False" />
                                                                        <FooterStyle CssClass="Gridheading right" HorizontalAlign="Right" Wrap="False" />
                                                                        <RowStyle CssClass="textbold" />
                                                                    </asp:GridView>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="7" valign="top">
                                                                    <asp:GridView EnableViewState="true" ID="grdvMktShareBrResult" AllowSorting="true"
                                                                        HeaderStyle-ForeColor="white" ShowFooter="true" runat="server" AutoGenerateColumns="False"
                                                                        HorizontalAlign="Center" Width="930px" HeaderStyle-CssClass="Gridheading" RowStyle-CssClass="ItemColor"
                                                                        AlternatingRowStyle-CssClass="lightblue">
                                                                        <Columns>
                                                                            <asp:TemplateField SortExpression="SELECTBY" HeaderText="Select By">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblSELECTBYVal" runat="server" Text='<%# Eval("SELECTBY") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <FooterTemplate>
                                                                                </FooterTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:BoundField DataField="chain_code" ItemStyle-Width="100px" SortExpression="chain_code"
                                                                                HeaderText="Chain Code" />
                                                                            <asp:BoundField DataField="NAME" ItemStyle-Width="100px" SortExpression="NAME" HeaderText="Agency Name" />
                                                                            <asp:BoundField DataField="ADDRESS" ItemStyle-Width="120px" SortExpression="ADDRESS"
                                                                                HeaderText="Address" />
                                                                            <asp:BoundField DataField="CITY" ItemStyle-Width="100px" SortExpression="CITY" HeaderText="City" />
                                                                            <asp:TemplateField SortExpression="CRSCODETEXT" HeaderText="CRS Code">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblCRSCODETEXTVal" runat="server" Text='<%# Eval("CRSCODETEXT") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <FooterTemplate>
                                                                                </FooterTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField SortExpression="BOOKINGACTIVE" HeaderText="Booking Active" ItemStyle-HorizontalAlign="Right">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblBOOKINGACTIVEVal" runat="server" Text='<%# Eval("BOOKINGACTIVE") %>'
                                                                                        CssClass="right"></asp:Label>
                                                                                </ItemTemplate>
                                                                                <FooterTemplate>
                                                                                </FooterTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField SortExpression="CANCELACTIVE" HeaderText="Cancel Active" ItemStyle-HorizontalAlign="Right">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblCANCELACTIVEVal" runat="server" Text='<%# Eval("CANCELACTIVE") %>'
                                                                                        CssClass="right"></asp:Label>
                                                                                </ItemTemplate>
                                                                                <FooterTemplate>
                                                                                </FooterTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField SortExpression="BOOKINGPASSIVE" HeaderText="Booking Passive" ItemStyle-HorizontalAlign="Right">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblPVal" runat="server" Text='<%# Eval("BOOKINGPASSIVE") %>' CssClass="right"></asp:Label>
                                                                                </ItemTemplate>
                                                                                <FooterTemplate>
                                                                                </FooterTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField SortExpression="CANCELPASSIVE" HeaderText="Cancel Passive" ItemStyle-HorizontalAlign="Right">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblCANCELPASSIVEVal" runat="server" Text='<%# Eval("CANCELPASSIVE") %>'
                                                                                        CssClass="right"></asp:Label>
                                                                                </ItemTemplate>
                                                                                <FooterTemplate>
                                                                                </FooterTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField SortExpression="LATE" HeaderText="Late" ItemStyle-HorizontalAlign="Right">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblLATEVal" runat="server" Text='<%# Eval("LATE") %>' CssClass="right"></asp:Label>
                                                                                </ItemTemplate>
                                                                                <FooterTemplate>
                                                                                </FooterTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField SortExpression="NULLACTIVE" HeaderText="Null Active" ItemStyle-HorizontalAlign="Right">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblNULLACTIVEVal" runat="server" Text='<%# Eval("NULLACTIVE") %>'
                                                                                        CssClass="right"></asp:Label>
                                                                                </ItemTemplate>
                                                                                <FooterTemplate>
                                                                                </FooterTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField SortExpression="NULLPASSIVE" HeaderText="Null Passive" ItemStyle-HorizontalAlign="Right">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblNULLPASSIVEVal" runat="server" Text='<%# Eval("NULLPASSIVE") %>'
                                                                                        CssClass="right"></asp:Label>
                                                                                </ItemTemplate>
                                                                                <FooterTemplate>
                                                                                </FooterTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField SortExpression="NETBOOKINGS" HeaderText="Net Bookings" ItemStyle-HorizontalAlign="Right">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblNETBOOKINGSVal" runat="server" Text='<%# Eval("NETBOOKINGS") %>'
                                                                                        CssClass="right"></asp:Label>
                                                                                </ItemTemplate>
                                                                                <FooterTemplate>
                                                                                </FooterTemplate>
                                                                            </asp:TemplateField>
                                                                        </Columns>
                                                                        <AlternatingRowStyle CssClass="lightblue" />
                                                                        <HeaderStyle CssClass="Gridheading" HorizontalAlign="Left" Wrap="False" />
                                                                        <FooterStyle CssClass="Gridheading right" HorizontalAlign="Right" Wrap="False" />
                                                                        <RowStyle CssClass="textbold" />
                                                                    </asp:GridView>
                                                                </td>
                                                            </tr>
                                                            <!-- code for paging----->
                                                            <tr>
                                                                <td colspan="7" valign="top">
                                                                    <asp:Panel ID="pnlPaging" runat="server" Visible="false" Width="800px">
                                                                        <table border="0" cellpadding="0" cellspacing="0" style="width: 800px">
                                                                            <tr class="paddingtop paddingbottom">
                                                                                <td style="width: 243px" class="left" nowrap="nowrap">
                                                                                    <span class="textbold"><b>&nbsp;No. of records found</b></span>&nbsp;&nbsp;<asp:TextBox
                                                                                        ID="txtTotalRecordCount" runat="server" Width="105px" CssClass="textboxgrey"
                                                                                        ReadOnly="true"></asp:TextBox></td>
                                                                                <td style="width: 200px" class="right">
                                                                                    <asp:LinkButton ID="lnkPrev" CssClass="LinkButtons" runat="server" CommandName="Prev"><< Prev</asp:LinkButton></td>
                                                                                <td style="width: 356px" class="center">
                                                                                    <span class="textbold"><b>Page No</b></span>&nbsp;&nbsp;<asp:DropDownList ID="ddlPageNumber"
                                                                                        Width="70px" CssClass="dropdownlist" runat="server" AutoPostBack="True">
                                                                                    </asp:DropDownList></td>
                                                                                <td style="width: 187px" class="left">
                                                                                    <asp:LinkButton ID="lnkNext" runat="server" CssClass="LinkButtons" CommandName="Next">Next >></asp:LinkButton></td>
                                                                            </tr>
                                                                        </table>
                                                                    </asp:Panel>
                                                                </td>
                                                            </tr>
                                                            <!-- code for paging----->
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                                <asp:HiddenField ID="hdA" runat="server" />
                                <asp:HiddenField ID="hdB" runat="server" />
                                <asp:HiddenField ID="hdG" runat="server" />
                                <asp:HiddenField ID="hdH" runat="server" />
                                <asp:HiddenField ID="hdP" runat="server" />
                                <asp:HiddenField ID="hdW" runat="server" />
                                <asp:HiddenField ID="hdTotal" runat="server" />
                                <asp:HiddenField ID="HiddenField1" runat="server" />
                                <asp:HiddenField ID="hdBOOKINGACTIVE" runat="server" />
                                <asp:HiddenField ID="hdCANCELACTIVE" runat="server" />
                                <asp:HiddenField ID="hdBOOKINGPASSIVE" runat="server" />
                                <asp:HiddenField ID="hdCANCELPASSIVE" runat="server" />
                                <asp:HiddenField ID="hdLATE" runat="server" />
                                <asp:HiddenField ID="hdNULLACTIVE" runat="server" />
                                <asp:HiddenField ID="hdNULLPASSIVE" runat="server" />
                                <asp:HiddenField ID="hdNETBOOKINGS" runat="server" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr valign="top">
                <td valign="top" style="padding-left: 4px; height: 21px;">
                </td>
            </tr>
            <tr>
            </tr>
        </table>
    </form>
</body>

<script type="text/javascript" language="javascript">
//showHideBreakup();
 //chkShowBreakup();
</script>

</html>
