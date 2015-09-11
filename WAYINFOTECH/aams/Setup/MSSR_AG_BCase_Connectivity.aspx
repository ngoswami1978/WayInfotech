<%@ Page Language="VB" AutoEventWireup="false" CodeFile="MSSR_AG_BCase_Connectivity.aspx.vb"
    Inherits="Setup_MSSR_AG_BCase_Connectivity" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>AAMS::Travel Agency::Manage Bussiness Case</title>
    <link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" media="all" href="../Calender/calendar-blue.css"
        title="win2k-cold-1" />

    <script src="../JavaScript/AAMS.js" type="text/javascript"></script>

    <link href="../Calender/calendar-blue.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="../Calender/calendar.js"></script>

    <script type="text/javascript" src="../Calender/lang/calendar-en.js"></script>

    <script type="text/javascript" src="../Calender/calendar-setup.js"></script>

    <script type="text/javascript" src="../JavaScript/subModal.js"></script>

    <link rel="stylesheet" type="text/css" href="../JavaScript/style.css" />
    <link rel="stylesheet" type="text/css" href="../JavaScript/subModal.css" />

    <script type="text/javascript" language="javascript"> 
function TabMethodAgencyGroup(id,total)
{   
        var ctextFront;
        var ctextBack;
        var Hcontrol;
        var HFlush;
        
        ctextFront = id.substring(0,15);        
        ctextBack = id.substring(17,26);   
       
        for(var i=0;i<total;i++)
        {
            HFlush = "0" + i;
            Hcontrol = ctextFront +  HFlush + ctextBack;
            if (document.getElementById(Hcontrol).className != "displayNone")
            {
                document.getElementById(Hcontrol).className="headingtabactive";
            }
           
        }
       
       var strChain_Code="";
        strChain_Code = document.getElementById('hdEnChainCode').value;
       document.getElementById(id).className="headingtab";     
      
       document.getElementById('lblPanelClick').value =id; 
       
    
      
       if (id == (ctextFront +  "00" + ctextBack))
       {   
            window.location.href="MSUP_AgencyGroup.aspx?Action=U&Chain_Code=" + strChain_Code  ;              
            return false;
       }       
       else if (id == (ctextFront +  "01" + ctextBack))
       {   
            window.location.href="MSUP_AG_CRSDetails.aspx?Action=U&Chain_Code=" + strChain_Code  ;   
            return false;                      
       }
       else if (id == (ctextFront +  "02" + ctextBack))
       {
           window.location.href="MSUP_AG_Competition.aspx?Action=U&Chain_Code=" + strChain_Code  ;   
            return false;   
       }
       else if (id == (ctextFront +  "03" + ctextBack))
       {    
            window.location.href="MSUP_AG_Staff.aspx?Action=U&Chain_Code=" + strChain_Code  ;   
            return false;   
       }
       
        else if (id == (ctextFront +  "04" + ctextBack))
       {
             window.location.href="MSUP_AG_PC.aspx?Action=U&Chain_Code=" + strChain_Code  ;   
            return false;   
       }
       
       else if (id == (ctextFront +  "05" + ctextBack))
       {
             window.location.href="MSUP_AG_Contract.aspx?Action=U&Chain_Code=" + strChain_Code  ;   
            return false;   
       }
       else if (id == (ctextFront +  "06" + ctextBack))
       {
            return false;   
       }
}



function TabMethodAgencySubGroup(id,total)
{   
        var ctextSubFront;
        var ctextSubBack;
        var HSubcontrol;
        var HSubFlush;
        
        ctextSubFront = id.substring(0,23);        
        ctextSubBack = id.substring(25,34); 
        
        for(var i=0;i<total;i++)
        {
            HSubFlush = "0" + i;
            HSubcontrol = ctextSubFront +  HSubFlush + ctextSubBack;
            if (document.getElementById(HSubcontrol).className != "displayNone")
            {
                document.getElementById(HSubcontrol).className="headingtabactive";
            }
           
        }
       
       var strChain_Code="";
        strChain_Code = document.getElementById('hdEnChainCode').value;
       document.getElementById(id).className="headingtab";     
      
       document.getElementById('lblSubPanelClick').value =id; 
       
    
      
         if (id == (ctextSubFront +  "00" + ctextSubBack))
       {   
            window.location.href="MSSR_AG_BCase_GroupMIDT.aspx?Action=U&Chain_Code=" + strChain_Code  ;              
            return false;
       }       
       else if (id == (ctextSubFront +  "01" + ctextSubBack))
       {   
            window.location.href="MSSR_AG_BCase_AgencyMIDT.aspx?Action=U&Chain_Code=" + strChain_Code  ;   
            return false;                      
       }
       else if (id == (ctextSubFront +  "02" + ctextSubBack))
       {
           window.location.href="MSSR_AG_BCase_Connectivity.aspx?Action=U&Chain_Code=" + strChain_Code  ;   
            return false;   
       }
       else if (id == (ctextSubFront +  "03" + ctextSubBack))
       {    
            window.location.href="MSSR_AG_BCase_Hardware.aspx?Action=U&Chain_Code=" + strChain_Code  ;   
            return false;   
       }
       
        else if (id == (ctextSubFront +  "04" + ctextSubBack))
       {
             window.location.href="MSUP_AG_Incentive.aspx?Action=U&Chain_Code=" + strChain_Code  ;   
            return false;   
       }

}
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <table width="860px" class="border_rightred left">
            <tr>
                <td class="top" width="860px">
                    <table width="100%">
                        <tr>
                            <td class="top">
                                <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td class="top left" style="width: 80%">
                                            <span class="menu">Travel Agency->Agency Group-></span><span class="sub_menu">Bussiness
                                                Case->Connectivity</span>
                                        </td>
                                        <td class="right" style="width: 20%">
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td class="heading center" colspan="2" style="width: 100%">
                                            Connectivity</td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td class="top">
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td class="top" colspan="2">
                                            <asp:HiddenField ID="lblPanelClick" runat="server"></asp:HiddenField>
                                            <asp:HiddenField ID="lblSubPanelClick" runat="server"></asp:HiddenField>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="top" style="height: 22px; width: 100%" colspan="2">
                                            <asp:Repeater ID="theTabStrip" runat="server">
                                                <ItemTemplate>
                                                    <asp:Button ID="Button1" Width="100px" CssClass="headingtabactive" runat="server"
                                                        Text="<%# Container.DataItem %>" />
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" class="redborder">
                                            <table width="860x" border="0" cellspacing="0" cellpadding="0">
                                                <tr>
                                                    <td valign="top" style="width: 845px; padding-left: 7px; padding-bottom: 7px;">
                                                        <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                                             <tr>
                                                                <td valign="top" style="height: 5PX">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="top" style="height: 22px; width: 100%" colspan="2">
                                                                    <asp:Repeater ID="theTabSubGroupStrip" runat="server">
                                                                        <ItemTemplate>
                                                                            <asp:Button ID="Button2" Width="100px" CssClass="headingtabactive" runat="server"
                                                                                Text="<%# Container.DataItem %>" />
                                                                        </ItemTemplate>
                                                                    </asp:Repeater>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="redborder top" colspan="2" style="width: 100%; height: 177px;">
                                                                    <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                                                        <tr>
                                                                            <td class="center TOP" style="width: 845px;">
                                                                                <asp:Label ID="lblError" CssClass="ErrorMsg" runat="server" EnableViewState="False"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="top" style="width: 100%;">
                                                                                <table width="800px" border="0" class="left" cellpadding="2" cellspacing="0">
                                                                                    <tr>
                                                                                        <td class="textbold" style="width: 90%">
                                                                                        </td>
                                                                                        <td class="center top ">
                                                                                            <asp:Button ID="btnExport" runat="server" TabIndex="3" CssClass="button topMargin"
                                                                                                Text="Export" Width="100px" AccessKey="E" />
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td class="textbold" style="width: 90%">
                                                                                        </td>
                                                                                        <td class="center top ">
                                                                                            <asp:Button ID="BtnPrint" runat="server" TabIndex="3" CssClass="button topMargin"
                                                                                                Text="Print" Width="100px" AccessKey="p" />
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td class="textbold" style="width: 90%">
                                                                                        </td>
                                                                                        <td class="center top ">
                                                                                            <asp:Button ID="btnReset" runat="server" TabIndex="3" CssClass="button topMargin"
                                                                                                Text="Reset" Width="100px" AccessKey="r" />
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
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td align="center" valign="top" style="width: 860px; padding-left: 4px;">
                    <table border="0" cellpadding="0" cellspacing="0" style="width: 860px">
                        <tr>
                            <td style="width: 860px;" valign="top" class="redborder">
                                <table border="0" cellpadding="0" cellspacing="0" style="width: 860px">
                                    <tr>
                                        <td>
                                            <table border="1" cellpadding="0" cellspacing="0">
                                                <tr style="background-color: silver">
                                                    <td align ="left"  height="30px;">
                                                        Existing Deal
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <table width="860px" border ="0" cellpadding ="0" cellspacing="0">
                                                            <tr style="background-color: silver">
                                                                <td id="HeaderUpper1" width="100px" align ="left" >
                                                                    Validity</td>
                                                                <td id="HeaderUpper2" runat="server" width="530px">
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="textbold" colspan="6" style="width:100%" valign="top">
                                            <asp:GridView ID="GvConnectivity" runat="server" AutoGenerateColumns="False" TabIndex="6"
                                                Width="100%" EnableViewState="true" AllowSorting="True" ShowFooter ="true" >
                                                <Columns>
                                                    <asp:BoundField HeaderText="Connectivity" DataField="CONNECTIVITY" SortExpression="CONNECTIVITY"
                                                        HeaderStyle-Width="30%"></asp:BoundField>
                                                    <asp:BoundField HeaderText="Unit Cost" DataField="UNITCOST" SortExpression="UNITCOST"
                                                        HeaderStyle-Width="20%" ItemStyle-Wrap="true"></asp:BoundField>
                                                    <asp:BoundField HeaderText="NO." DataField="NUM" SortExpression="NUM" HeaderStyle-Width="20%">
                                                    </asp:BoundField>
                                                    <asp:BoundField HeaderText="Total Cost" DataField="TOTALCOST" SortExpression="TOTALCOST"
                                                        HeaderStyle-Width="30%" ItemStyle-HorizontalAlign="right"  ></asp:BoundField>
                                                </Columns>
                                                <AlternatingRowStyle CssClass="lightblue" />
                                                <RowStyle CssClass="textbold" />
                                                <HeaderStyle CssClass="Gridheading" ForeColor="White" />
                                                <FooterStyle CssClass="Gridheading" ForeColor="White" />
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="6"  width="860px">
                                <asp:Panel ID="pnlPaging" runat="server" Visible="false" Width="100%">
                                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                        <tr class="paddingtop paddingbottom">
                                            <td style="width: 30%" class="left">
                                                <span class="textbold"><b>&nbsp;No. of records found</b></span>&nbsp;&nbsp;<asp:TextBox
                                                    ID="txtTotalRecordCount" runat="server" Width="105px" CssClass="textboxgrey"
                                                    ReadOnly="True"></asp:TextBox></td>
                                            <td style="width: 25%" class="right">
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
                </td>
            </tr>
            <tr>
                <td>
                    <input type="hidden" runat="server" id="hdEnChainCode" style="width: 21px" />
                    <input type="hidden" runat="server" id="hdChainCode" style="width: 21px" /></td>
            </tr>
        </table>
    </form>
</body>
</html>
