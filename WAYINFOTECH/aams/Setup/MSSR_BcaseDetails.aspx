<%@ Page Language="VB" AutoEventWireup="false" CodeFile="MSSR_BcaseDetails.aspx.vb"
    Inherits="Setup_MSSR_BcaseDetails" %>

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
//{debugger;} 
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
//{debugger;} 
        var ctextSubFront;
        var ctextSubBack;
        var HSubcontrol;
        var HSubFlush;
        
        ctextSubFront = id.substring(0,18);        
        ctextSubBack = id.substring(20,29);   
       
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
            //window.location.href="MSSR_AG_BCase_GroupMIDT.aspx?Action=U&Chain_Code=" + strChain_Code  ;  
            document.getElementById('pnlDetails').className='displayBlock';    
             document.getElementById('pnlRemarks').className='displayNone';        
            return false;
       }       
       else if (id == (ctextSubFront +  "01" + ctextSubBack))
       {   
           // window.location.href="MSSR_AG_BCase_AgencyMIDT.aspx?Action=U&Chain_Code=" + strChain_Code  ;   
            document.getElementById('pnlDetails').className='displayNone';    
            document.getElementById('pnlRemarks').className='displayBlock';
            return false;                      
       }
      
}

    </script>

</head>
<body>
    <form id="form1" runat="server">
        <table width="845px" class="border_rightred left">
            <tr>
                <td class="top" width="845px">
                    <table width="100%">
                        <tr>
                            <td class="top">
                                <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td class="top left" style="width: 80%">
                                            <span class="menu">Travel Agency->Agency Group-></span><span class="sub_menu">Bussiness
                                                Case->A Bussiness Case Details</span>
                                        </td>
                                        <td class="right" style="width: 20%">
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td class="heading center" colspan="2" style="width: 100%">
                                           Bussiness Case Details</td>
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
                                            <table width="845px" border="0" cellspacing="0" cellpadding="0">
                                                <tr>
                                                    <td valign="top" style="width:830px; padding-left: 7px; padding-bottom: 7px;">
                                                        <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                                            <tr>
                                                                <td valign="top" style="height: 5PX">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="top" style="height: 22px; width: 100%" colspan="2">
                                                                    <asp:Repeater ID="theTabSubStrip" runat="server">
                                                                        <ItemTemplate>
                                                                            <asp:Button ID="Button2" Width="100px" CssClass="headingtabactive" runat="server"
                                                                                Text="<%# Container.DataItem %>" />
                                                                        </ItemTemplate>
                                                                    </asp:Repeater>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="redborder top" style="width: 100%; ">
                                                                    <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                                                        <tr>
                                                                            <td class="center TOP" style="width: 830px;">
                                                                                <asp:Label ID="lblError" CssClass="ErrorMsg" runat="server" EnableViewState="False"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="top" style="width: 95%;padding-left: 7px; padding-bottom: 7px;">
                                                                                <table width="800px" border="0" class="left" cellpadding="2" cellspacing="0">
                                                                                    <tr>
                                                                                        <td>
                                                                                            <asp:Panel ID="pnlDetails" runat="server" CssClass ="displayBlock">
                                                                                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                                                                    <tr>
                                                                                                        <td>
                                                                                                            <asp:Panel ID="PnlAgencyDetails" runat="server" Width="100%" Visible="true">
                                                                                                                <table width="100%" border="0" cellspacing="2" cellpadding="2">
                                                                                                                    <tr>
                                                                                                                        <td class="subheading" colspan="5">
                                                                                                                            Group Details</td>
                                                                                                                    </tr>
                                                                                                                    <tr>
                                                                                                                        <td class="textbold" style="width: 15%">
                                                                                                                            Group Name</td>
                                                                                                                        <td colspan="4" style="width: 70%">
                                                                                                                            <asp:TextBox ID="txtGroupName" runat="server" CssClass="textboxgrey" MaxLength="50"
                                                                                                                                TabIndex="3" Width="698px" ReadOnly="True"></asp:TextBox>
                                                                                                                        </td>
                                                                                                                    </tr>
                                                                                                                    <tr>
                                                                                                                        <td class="textbold" style="width: 15%;">
                                                                                                                            Cahin Code</td>
                                                                                                                        <td style="width: 29%;">
                                                                                                                            <asp:TextBox ID="txtChainCode" runat="server" CssClass="textboxgrey" MaxLength="50"
                                                                                                                                ReadOnly="True" TabIndex="3" Width="206px"></asp:TextBox></td>
                                                                                                                        <td class="textbold" style="width: 2%;">
                                                                                                                        </td>
                                                                                                                        <td class="textbold" style="width: 10%;">
                                                                                                                            Region</td>
                                                                                                                        <td class="textbold" style="width: 29%;">
                                                                                                                            <asp:TextBox ID="txtRegion" runat="server" CssClass="textboxgrey" MaxLength="50"
                                                                                                                                ReadOnly="True" TabIndex="3" Width="206px"></asp:TextBox></td>
                                                                                                                    </tr>
                                                                                                                    <tr>
                                                                                                                        <td class="textbold" style="width: 15%;">
                                                                                                                            Contract Period</td>
                                                                                                                        <td style="width: 29%;">
                                                                                                                            <asp:TextBox ID="txtContractPeriod" runat="server" CssClass="textboxgrey" MaxLength="50"
                                                                                                                                ReadOnly="True" TabIndex="3" Width="206px"></asp:TextBox></td>
                                                                                                                        <td class="textbold" style="width: 2%;">
                                                                                                                        </td>
                                                                                                                        <td class="textbold" style="width: 10%;">
                                                                                                                            Account Manager</td>
                                                                                                                        <td class="textbold" style="width: 29%;">
                                                                                                                            <asp:TextBox ID="txtActManager" runat="server" CssClass="textboxgrey" MaxLength="50"
                                                                                                                                ReadOnly="True" TabIndex="3" Width="204px"></asp:TextBox></td>
                                                                                                                    </tr>
                                                                                                                    <tr>
                                                                                                                        <td class="textbold" style="width: 15%;" valign="top">
                                                                                                                            Approvers</td>
                                                                                                                        <td style="width: 29%;" valign="top">
                                                                                                                            <asp:ListBox ID="LstApprovers" runat="server" Width="209px"></asp:ListBox></td>
                                                                                                                        <td class="textbold" style="width: 2%;">
                                                                                                                        </td>
                                                                                                                        <td class="textbold" style="width: 10%;" valign="top">
                                                                                                                            Billing Cycle</td>
                                                                                                                        <td class="textbold" style="width: 29%;" valign="top">
                                                                                                                            <asp:TextBox ID="txtBillCycle" runat="server" CssClass="textboxgrey" MaxLength="50"
                                                                                                                                ReadOnly="True" TabIndex="3" Width="206px"></asp:TextBox></td>
                                                                                                                    </tr>
                                                                                                                    <tr>
                                                                                                                        <td class="textbold" style="width: 15%">
                                                                                                                            </td>
                                                                                                                        <td style="width: 29%">
                                                                                                                            </td>
                                                                                                                        <td class="textbold" style="width: 2%">
                                                                                                                        </td>
                                                                                                                        <td class="textbold" style="width: 10%">
                                                                                                                        </td>
                                                                                                                        <td class="textbold" style="width: 29%">
                                                                                                                        </td>
                                                                                                                    </tr>
                                                                                                                </table>
                                                                                                            </asp:Panel>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td class="subheading" align="center">
                                                                                                            Group MIDT</td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td class="">
                                                                                                            &nbsp;</td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td>
                                                                                                            <table border="1" cellspacing="0" cellpadding="0" width="840px">
                                                                                                                <tr>
                                                                                                                    <td>
                                                                                                                        <table border="0" cellspacing="0" cellpadding="0" width="840px">
                                                                                                                            <tr>
                                                                                                                                <td>
                                                                                                                                    <table border="0" cellspacing="0" cellpadding="1" width="1000x">
                                                                                                                                        <tr class="Gridheading">
                                                                                                                                            <td width="10%">
                                                                                                                                                Last Available</td>
                                                                                                                                            <td width="8%">
                                                                                                                                                1A</td>
                                                                                                                                            <td width="8%">
                                                                                                                                                1B</td>
                                                                                                                                            <td width="8%">
                                                                                                                                                1G</td>
                                                                                                                                            <td width="8%">
                                                                                                                                                1P</td>
                                                                                                                                            <td width="8%">
                                                                                                                                                1W</td>
                                                                                                                                            <td width="10%">
                                                                                                                                                Total</td>
                                                                                                                                            <td width="8%">
                                                                                                                                                1A%</td>
                                                                                                                                            <td width="8%">
                                                                                                                                                1B%</td>
                                                                                                                                            <td width="8%">
                                                                                                                                                1G%</td>
                                                                                                                                            <td width="8%">
                                                                                                                                                1P%</td>
                                                                                                                                            <td width="8%">
                                                                                                                                                1W%</td>
                                                                                                                                        </tr>
                                                                                                                                    </table>
                                                                                                                                </td>
                                                                                                                            </tr>
                                                                                                                            <tr>
                                                                                                                                <td>
                                                                                                                                    <asp:Panel ID="pnlAmadeus" runat="server" ScrollBars="Vertical" Width="100%" Height="100px">
                                                                                                                                        <asp:GridView ID="GvBGroupMIDT" runat="server" AutoGenerateColumns="False" TabIndex="6"
                                                                                                                                            ShowFooter="true" ShowHeader="false" Width="1000px" EnableViewState="true" AllowSorting="false">
                                                                                                                                            <Columns>
                                                                                                                                                <asp:BoundField HeaderText="Last Available" DataField="LASTAVAIL" SortExpression="LASTAVAIL"
                                                                                                                                                    ItemStyle-Width="10%" HeaderStyle-Width="10%"></asp:BoundField>
                                                                                                                                                <asp:BoundField HeaderText="1A" DataField="A" SortExpression="A" HeaderStyle-Width="8%"
                                                                                                                                                    ItemStyle-Width="8%"></asp:BoundField>
                                                                                                                                                <asp:BoundField HeaderText="1B" DataField="B" SortExpression="B" HeaderStyle-Width="8%"
                                                                                                                                                    ItemStyle-Width="8%" ItemStyle-Wrap="true"></asp:BoundField>
                                                                                                                                                <asp:BoundField HeaderText="1G" DataField="G" SortExpression="G" HeaderStyle-Width="8%"
                                                                                                                                                    ItemStyle-Width="8%"></asp:BoundField>
                                                                                                                                                <asp:BoundField HeaderText="1P" DataField="P" SortExpression="P" HeaderStyle-Width="8%"
                                                                                                                                                    ItemStyle-Width="8%"></asp:BoundField>
                                                                                                                                                <asp:BoundField HeaderText="1W" DataField="W" SortExpression="W" HeaderStyle-Width="8%"
                                                                                                                                                    ItemStyle-Width="8%" ItemStyle-Wrap="true"></asp:BoundField>
                                                                                                                                                <asp:BoundField HeaderText="Total" DataField="TOTAL" SortExpression="TOTAL" HeaderStyle-Width="10%"
                                                                                                                                                    ItemStyle-Width="10%" ItemStyle-Wrap="true"></asp:BoundField>
                                                                                                                                                <asp:BoundField HeaderText="1A %" DataField="A_PER" SortExpression="APER" HeaderStyle-Width="8%"
                                                                                                                                                    ItemStyle-Width="8%"></asp:BoundField>
                                                                                                                                                <asp:BoundField HeaderText="1B %" DataField="B_PER" SortExpression="BPER" HeaderStyle-Width="8%"
                                                                                                                                                    ItemStyle-Width="8%" ItemStyle-Wrap="true"></asp:BoundField>
                                                                                                                                                <asp:BoundField HeaderText="1G %" DataField="G_PER" SortExpression="GPER" HeaderStyle-Width="8%"
                                                                                                                                                    ItemStyle-Width="8%"></asp:BoundField>
                                                                                                                                                <asp:BoundField HeaderText="1P %" DataField="P_PER" SortExpression="PPER" HeaderStyle-Width="8%"
                                                                                                                                                    ItemStyle-Width="8%"></asp:BoundField>
                                                                                                                                                <asp:BoundField HeaderText="1W %" DataField="W_PER" SortExpression="WPER" HeaderStyle-Width="8%"
                                                                                                                                                    ItemStyle-Width="8%" ItemStyle-Wrap="true"></asp:BoundField>
                                                                                                                                            </Columns>
                                                                                                                                            <AlternatingRowStyle CssClass="lightblue" />
                                                                                                                                            <RowStyle CssClass="textbold" />
                                                                                                                                            <HeaderStyle CssClass="Gridheading" ForeColor="White" HorizontalAlign="Left" />
                                                                                                                                            <FooterStyle CssClass="Gridheading" />
                                                                                                                                        </asp:GridView>
                                                                                                                                    </asp:Panel>
                                                                                                                                </td>
                                                                                                                            </tr>
                                                                                                                        </table>
                                                                                                                    </td>
                                                                                                                </tr>
                                                                                                                <tr>
                                                                                                                    <td class="">
                                                                                                                        &nbsp;</td>
                                                                                                                </tr>
                                                                                                                <tr>
                                                                                                                    <td class="subheading" align="center">
                                                                                                                        Agency MIDT</td>
                                                                                                                </tr>
                                                                                                                <tr>
                                                                                                                    <td class="">
                                                                                                                        &nbsp;</td>
                                                                                                                </tr>
                                                                                                                <tr>
                                                                                                                    <td>
                                                                                                                        <table border="0" cellspacing="0" cellpadding="0" width="840px">
                                                                                                                            <tr>
                                                                                                                                <td>
                                                                                                                                    <table border="0" cellspacing="0" cellpadding="1" width="1000x">
                                                                                                                                        <tr class="Gridheading">
                                                                                                                                            <td width="50px;">
                                                                                                                                                S.No.</td>
                                                                                                                                            <td width="120px;">
                                                                                                                                                Agency Name</td>
                                                                                                                                            <td width="100px;">
                                                                                                                                                Office Id</td>
                                                                                                                                            <td width="130px;">
                                                                                                                                                Address</td>
                                                                                                                                            <td width="100px;">
                                                                                                                                                1A</td>
                                                                                                                                            <td width="100px;">
                                                                                                                                                1B</td>
                                                                                                                                            <td width="100px;">
                                                                                                                                                1G</td>
                                                                                                                                            <td width="100px;">
                                                                                                                                                1P</td>
                                                                                                                                            <td width="100px;">
                                                                                                                                                1W</td>
                                                                                                                                            <td width="100px;">
                                                                                                                                                TTP</td>
                                                                                                                                        </tr>
                                                                                                                                    </table>
                                                                                                                                </td>
                                                                                                                            </tr>
                                                                                                                            <tr>
                                                                                                                                <td>
                                                                                                                                    <asp:Panel ID="Panel1" runat="server" ScrollBars="Vertical" Width="100%" Height="100px">
                                                                                                                                        <asp:GridView ID="GvBAgencyMIDT" runat="server" AutoGenerateColumns="False" TabIndex="6"
                                                                                                                                            ShowHeader="false" Width="1000px" EnableViewState="true" AllowSorting="false"
                                                                                                                                            ShowFooter="true">
                                                                                                                                            <Columns>
                                                                                                                                                <asp:TemplateField HeaderText="S.No." ItemStyle-Width="50px" HeaderStyle-Width="50px">
                                                                                                                                                    <ItemTemplate>
                                                                                                                                                        <asp:Label ID="lblSNo" runat="server" Width="50px"></asp:Label>
                                                                                                                                                    </ItemTemplate>
                                                                                                                                                </asp:TemplateField>
                                                                                                                                                <asp:BoundField HeaderText="Agency Name" DataField="Name"  HeaderStyle-Width="130px"
                                                                                                                                                    ItemStyle-Width="120px"></asp:BoundField>
                                                                                                                                                <asp:BoundField HeaderText="Office Id" DataField="OfficeID" 
                                                                                                                                                    ItemStyle-Width="100px" HeaderStyle-Width="100px" ItemStyle-Wrap="true"></asp:BoundField>
                                                                                                                                                <asp:BoundField HeaderText="Address" DataField="Address" 
                                                                                                                                                    ItemStyle-Width="130px"></asp:BoundField>
                                                                                                                                                <asp:BoundField HeaderText="1A" DataField="A" HeaderStyle-Width="100px"
                                                                                                                                                    ItemStyle-Width="100px"></asp:BoundField>
                                                                                                                                                <asp:BoundField HeaderText="1B" DataField="B"  HeaderStyle-Width="100px"
                                                                                                                                                    ItemStyle-Width="100px"></asp:BoundField>
                                                                                                                                                <asp:BoundField HeaderText="1G" DataField="G"  HeaderStyle-Width="100px"
                                                                                                                                                    ItemStyle-Width="100px"></asp:BoundField>
                                                                                                                                                <asp:BoundField HeaderText="1P" DataField="P" HeaderStyle-Width="100px"
                                                                                                                                                    ItemStyle-Width="100px"></asp:BoundField>
                                                                                                                                                <asp:BoundField HeaderText="1W" DataField="W"  HeaderStyle-Width="100px"
                                                                                                                                                    ItemStyle-Width="100px" ItemStyle-Wrap="true"></asp:BoundField>
                                                                                                                                                <asp:BoundField HeaderText="TTP" DataField="TTP"  HeaderStyle-Width="100px"
                                                                                                                                                    ItemStyle-Width="100px" ItemStyle-Wrap="true"></asp:BoundField>
                                                                                                                                            </Columns>
                                                                                                                                            <AlternatingRowStyle CssClass="lightblue" />
                                                                                                                                            <RowStyle CssClass="textbold" />
                                                                                                                                            <HeaderStyle CssClass="Gridheading" ForeColor="White" HorizontalAlign="left" />
                                                                                                                                            <FooterStyle CssClass="Gridheading" ForeColor="White" />
                                                                                                                                        </asp:GridView>
                                                                                                                                    </asp:Panel>
                                                                                                                                </td>
                                                                                                                            </tr>
                                                                                                                        </table>
                                                                                                                    </td>
                                                                                                                </tr>
                                                                                                                <tr>
                                                                                                                    <td class="">
                                                                                                                        &nbsp;</td>
                                                                                                                </tr>
                                                                                                                <tr>
                                                                                                                    <td class="">
                                                                                                                        &nbsp;</td>
                                                                                                                </tr>
                                                                                                                <tr>
                                                                                                                    <td>
                                                                                                                        <table border="0" cellspacing="0" cellpadding="1" width="840px">
                                                                                                                            <tr>
                                                                                                                                <td width="48%">
                                                                                                                                    <table border="0" cellspacing="0" cellpadding="1" width="100%">
                                                                                                                                        <tr>
                                                                                                                                            <td class="subheading" align="center">
                                                                                                                                                Connectivity
                                                                                                                                            </td>
                                                                                                                                        </tr>
                                                                                                                                        <tr>
                                                                                                                                            <td class="">
                                                                                                                                                &nbsp;</td>
                                                                                                                                        </tr>
                                                                                                                                        <tr>
                                                                                                                                            <td>
                                                                                                                                                <table border="0" cellspacing="0" cellpadding="0" width="100%">
                                                                                                                                                    <tr>
                                                                                                                                                        <td>
                                                                                                                                                            <table border="0" cellspacing="0" cellpadding="1" width="400x">
                                                                                                                                                                <tr class="Gridheading">
                                                                                                                                                                    <td width="30%">
                                                                                                                                                                        Connectivity</td>
                                                                                                                                                                    <td width="20%">
                                                                                                                                                                        Unit Cost</td>
                                                                                                                                                                    <td width="20%">
                                                                                                                                                                        No.</td>
                                                                                                                                                                    <td width="30%">
                                                                                                                                                                        Total Cost</td>
                                                                                                                                                                </tr>
                                                                                                                                                            </table>
                                                                                                                                                        </td>
                                                                                                                                                    </tr>
                                                                                                                                                    <tr>
                                                                                                                                                        <td>
                                                                                                                                                            <asp:Panel ID="Panel2" runat="server" ScrollBars="Vertical" Width="400px" Height="100px">
                                                                                                                                                                <asp:GridView ID="GvConnectivity" runat="server" AutoGenerateColumns="False" TabIndex="6"
                                                                                                                                                                    ShowHeader="false" Width="400px" EnableViewState="true" AllowSorting="false"
                                                                                                                                                                    ShowFooter="true">
                                                                                                                                                                    <Columns>
                                                                                                                                                                        <asp:BoundField HeaderText="Connectivity" DataField="BC_ONLINE_CATG_NAME" 
                                                                                                                                                                            ItemStyle-Width="30%" HeaderStyle-Width="30%"></asp:BoundField>
                                                                                                                                                                        <asp:BoundField HeaderText="Unit Cost" DataField="BC_ONLINE_CATG_COST" 
                                                                                                                                                                            ItemStyle-Width="20%" HeaderStyle-Width="20%" ItemStyle-Wrap="true"></asp:BoundField>
                                                                                                                                                                        <asp:BoundField HeaderText="No." DataField="CONN_COUNT"  HeaderStyle-Width="20%"
                                                                                                                                                                            ItemStyle-Width="20%"></asp:BoundField>
                                                                                                                                                                        <asp:BoundField HeaderText="Total Cost" DataField="TOTAL"  
                                                                                                                                                                            ItemStyle-Width="30%" HeaderStyle-Width="30%" ItemStyle-HorizontalAlign="right">
                                                                                                                                                                        </asp:BoundField>
                                                                                                                                                                    </Columns>
                                                                                                                                                                    <AlternatingRowStyle CssClass="lightblue" />
                                                                                                                                                                    <RowStyle CssClass="textbold" />
                                                                                                                                                                    <HeaderStyle CssClass="Gridheading" ForeColor="White" />
                                                                                                                                                                    <FooterStyle CssClass="Gridheading" ForeColor="White" />
                                                                                                                                                                </asp:GridView>
                                                                                                                                                            </asp:Panel>
                                                                                                                                                        </td>
                                                                                                                                                    </tr>
                                                                                                                                                </table>
                                                                                                                                            </td>
                                                                                                                                        </tr>
                                                                                                                                    </table>
                                                                                                                                </td>
                                                                                                                                <td width="4%">
                                                                                                                                </td>
                                                                                                                                <td width="48%">
                                                                                                                                    <table border="0" cellspacing="0" cellpadding="1" width="100%">
                                                                                                                                        <tr>
                                                                                                                                            <td class="subheading" align="center">
                                                                                                                                                Hardware
                                                                                                                                            </td>
                                                                                                                                        </tr>
                                                                                                                                        <tr>
                                                                                                                                        </tr>
                                                                                                                                        <tr>
                                                                                                                                            <td class="">
                                                                                                                                                &nbsp;</td>
                                                                                                                                        </tr>
                                                                                                                                        <tr>
                                                                                                                                            <td>
                                                                                                                                                <table border="0" cellspacing="0" cellpadding="0" width="100%">
                                                                                                                                                    <tr>
                                                                                                                                                        <td>
                                                                                                                                                            <table border="0" cellspacing="0" cellpadding="1" width="400px">
                                                                                                                                                                <tr class="Gridheading">
                                                                                                                                                                    <td width="30%">
                                                                                                                                                                        Hardware</td>
                                                                                                                                                                    <td width="20%">
                                                                                                                                                                        Unit Cost</td>
                                                                                                                                                                    <td width="20%">
                                                                                                                                                                        No.</td>
                                                                                                                                                                    <td width="30%">
                                                                                                                                                                        Total Cost</td>
                                                                                                                                                                </tr>
                                                                                                                                                            </table>
                                                                                                                                                        </td>
                                                                                                                                                    </tr>
                                                                                                                                                    <tr>
                                                                                                                                                        <td>
                                                                                                                                                            <asp:Panel ID="Panel3" runat="server" ScrollBars="Vertical" Width="400px" Height="100px">
                                                                                                                                                                <asp:GridView ID="GvHardware" runat="server" AutoGenerateColumns="False" TabIndex="6"
                                                                                                                                                                    ShowHeader="false" Width="400px" EnableViewState="true" AllowSorting="false"
                                                                                                                                                                    ShowFooter="true">
                                                                                                                                                                    <Columns>
                                                                                                                                                                        <asp:BoundField HeaderText="Hardware" DataField="BC_ONLINE_CATG_NAME" 
                                                                                                                                                                            ItemStyle-Width="30%" HeaderStyle-Width="30%"></asp:BoundField>
                                                                                                                                                                        <asp:BoundField HeaderText="Unit Cost" DataField="BC_EQP_CATG_COST" 
                                                                                                                                                                            ItemStyle-Width="20%" HeaderStyle-Width="20%" ItemStyle-Wrap="true"></asp:BoundField>
                                                                                                                                                                        <asp:BoundField HeaderText="NO." DataField="PRODUCT_COUNT"  HeaderStyle-Width="20%"
                                                                                                                                                                            ItemStyle-Width="20%"></asp:BoundField>
                                                                                                                                                                        <asp:BoundField HeaderText="Total Cost" DataField="TOTAL" 
                                                                                                                                                                            ItemStyle-Width="30%" HeaderStyle-Width="30%" ItemStyle-HorizontalAlign="right">
                                                                                                                                                                        </asp:BoundField>
                                                                                                                                                                    </Columns>
                                                                                                                                                                    <AlternatingRowStyle CssClass="lightblue" />
                                                                                                                                                                    <RowStyle CssClass="textbold" />
                                                                                                                                                                    <HeaderStyle CssClass="Gridheading" ForeColor="White" />
                                                                                                                                                                    <FooterStyle CssClass="Gridheading" ForeColor="White" />
                                                                                                                                                                </asp:GridView>
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
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                     <tr>
                                                                                                        <td class="">
                                                                                                            &nbsp;</td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td class="subheading" align="center">
                                                                                                            Incentive Plans</td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td class="">
                                                                                                            &nbsp;</td>
                                                                                                    </tr>
                                                                                                    
                                                                                                     <tr>
                                                                                                        <td class="">
                                                                                                            &nbsp;</td>
                                                                                                    </tr>
                                                                                                </table>
                                                                                            </asp:Panel>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td>
                                                                                            <asp:Panel ID="pnlRemarks" runat="server" CssClass ="displayNone" >
                                                                                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                                                                    <tr>
                                                                                                        <td class="">
                                                                                                            &nbsp;</td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td class="subheading" align="center">
                                                                                                            Region Remarks
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td class="">
                                                                                                            &nbsp;<asp:TextBox ID="TextBox2" runat="server" CssClass="textboxgrey" MaxLength="50"
                                                                                                                ReadOnly="True" TabIndex="3" Width="791px" Height="81px"></asp:TextBox></td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td class="">
                                                                                                            &nbsp;</td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td class="">
                                                                                                            &nbsp;</td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td class="subheading" align="center">
                                                                                                            Market (DEL) Remarks
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td class="">
                                                                                                            &nbsp;<asp:TextBox ID="TextBox1" runat="server" CssClass="textboxgrey" MaxLength="50"
                                                                                                                ReadOnly="True" TabIndex="3" Width="791px" Height="81px"></asp:TextBox></td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td class="">
                                                                                                            &nbsp;</td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td class="">
                                                                                                            &nbsp;</td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td class="subheading" align="center">
                                                                                                            Approved By ( Sign & Date)
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td class="">
                                                                                                            <table border="1" cellpadding="0" cellspacing="0">
                                                                                                                <tr>
                                                                                                                    <td>
                                                                                                                        <b>Markets</b></td>
                                                                                                                    <td>
                                                                                                                    </td>
                                                                                                                    <td style="width: 3px">
                                                                                                                    </td>
                                                                                                                    <td>
                                                                                                                        <asp:TextBox ID="TextBox3" runat="server" CssClass="textboxgrey" MaxLength="50" ReadOnly="True"
                                                                                                                            TabIndex="3" Width="690px"></asp:TextBox></td>
                                                                                                                </tr>
                                                                                                                <tr>
                                                                                                                    <td>
                                                                                                                        <b>H.O.D Markets</b></td>
                                                                                                                    <td>
                                                                                                                    </td>
                                                                                                                    <td style="width: 3px">
                                                                                                                    </td>
                                                                                                                    <td>
                                                                                                                        <asp:TextBox ID="TextBox4" runat="server" CssClass="textboxgrey" MaxLength="50" ReadOnly="True"
                                                                                                                            TabIndex="3" Width="690px"></asp:TextBox></td>
                                                                                                                </tr>
                                                                                                                <tr>
                                                                                                                    <td>
                                                                                                                        <b>C.E.O.</b></td>
                                                                                                                    <td>
                                                                                                                    </td>
                                                                                                                    <td style="width: 3px">
                                                                                                                    </td>
                                                                                                                    <td>
                                                                                                                        <asp:TextBox ID="TextBox5" runat="server" CssClass="textboxgrey" MaxLength="50" ReadOnly="True"
                                                                                                                            TabIndex="3" Width="690px"></asp:TextBox></td> 
                                                                                                                </tr>
                                                                                                            </table>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td class="">
                                                                                                            &nbsp;</td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td class="">
                                                                                                            &nbsp;</td>
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
    </form>
</body>
</html>
