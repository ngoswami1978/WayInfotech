<%@ Page Language="VB" AutoEventWireup="false" CodeFile="INCSR_BacseDetails.aspx.vb"
    Inherits="Incentive_INCSR_BacseDetails" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>AAMS::Travel Agency::Manage Business Case</title>
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
              document.getElementById('pnlRemarksRegion').className='displayNone';  
              document.getElementById('PnlRemMarket').className='displayNone'; 
                 document.getElementById('PnlApprovers').className='displayNone';    
            return false;
       }       
       else if (id == (ctextSubFront +  "01" + ctextSubBack))
       {   
           // window.location.href="MSSR_AG_BCase_AgencyMIDT.aspx?Action=U&Chain_Code=" + strChain_Code  ;   
            document.getElementById('pnlDetails').className='displayNone';   
              document.getElementById('pnlRemarksRegion').className='displayBlock';
             document.getElementById('PnlRemMarket').className='displayNone';
              document.getElementById('PnlApprovers').className='displayNone';
            return false;                      
       }
       
        else if (id == (ctextSubFront +  "02" + ctextSubBack))
       {   
           // window.location.href="MSSR_AG_BCase_AgencyMIDT.aspx?Action=U&Chain_Code=" + strChain_Code  ;   
            document.getElementById('pnlDetails').className='displayNone';  
            document.getElementById('pnlRemarksRegion').className='displayNone';
              document.getElementById('PnlRemMarket').className='displayBlock';
             document.getElementById('PnlApprovers').className='displayNone';
           
            return false;                      
       }
       
        else if (id == (ctextSubFront +  "03" + ctextSubBack))
       {   
           // window.location.href="MSSR_AG_BCase_AgencyMIDT.aspx?Action=U&Chain_Code=" + strChain_Code  ;   
            document.getElementById('pnlDetails').className='displayNone';    
            document.getElementById('pnlRemarksRegion').className='displayNone';
            document.getElementById('PnlRemMarket').className='displayNone';
             document.getElementById('PnlApprovers').className='displayBlock';
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
                                        <td class="top left" style="width: 100%">
                                            <span class="menu">Travel Agency->Agency Group-></span><span class="sub_menu">Business
                                                Case->Business Case Details</span>
                                        </td>
                                        <td class="right" style="width: 20%">
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td class="heading center" colspan="2" width="830px">
                                            Business Case Details</td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            <a href="#" class="LinkButtons" onclick="window.close();">Close</a>&nbsp;&nbsp;&nbsp;</td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td valign="top">
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td class="top" style="width: 100%" colspan="2">
                                            <asp:Repeater ID="theTabStrip" runat="server">
                                                <ItemTemplate>
                                                    <asp:Button ID="Button1" Width="100px" CssClass="headingtabactive" runat="server"
                                                        Text="<%# Container.DataItem %>" />
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top">
                                            <!-- <td valign="top" class="redborder"> -->
                                            <table width="960px" border="0" cellspacing="0" cellpadding="0">
                                                <tr>
                                                    <td valign="top" style="width: 945px; padding-left: 7px; padding-bottom: 7px;">
                                                        <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                                            <tr>
                                                                <td class="top" style="height: 22px; width: 100%" colspan="2">
                                                                    <asp:Repeater ID="theTabSubStrip" runat="server">
                                                                        <ItemTemplate>
                                                                            <asp:Button ID="Button2" Width="120px" CssClass="headingtabactive" runat="server"
                                                                                Text="<%# Container.DataItem %>" />
                                                                        </ItemTemplate>
                                                                    </asp:Repeater>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="redborder top" style="width: 100%;">
                                                                    <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                                                        <tr>
                                                                            <td class="center TOP" style="width: 830px;">
                                                                                <asp:Label ID="lblError" CssClass="ErrorMsg" runat="server" EnableViewState="False"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="top" style="width: 100%; padding-left: 7px; padding-bottom: 7px;">
                                                                                <table width="938px" border="0" class="left" cellpadding="2" cellspacing="0">
                                                                                    <tr>
                                                                                        <td>
                                                                                            <asp:Panel ID="pnlDetails" runat="server" CssClass="displayBlock">
                                                                                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                                                                    <tr>
                                                                                                        <td>
                                                                                                            <asp:Panel ID="PnlGroupDetails" runat="server" Width="840px" Visible="true">
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
                                                                                                                            Chain Code</td>
                                                                                                                        <td style="width: 29%;">
                                                                                                                            <asp:TextBox ID="txtChainCode" runat="server" CssClass="textboxgrey" MaxLength="50"
                                                                                                                                ReadOnly="True" TabIndex="3" Width="280px"></asp:TextBox></td>
                                                                                                                        <td class="textbold" style="width: 2%;">
                                                                                                                        </td>
                                                                                                                        <td class="textbold" style="width: 10%;">
                                                                                                                            Region</td>
                                                                                                                        <td class="textbold" style="width: 29%;">
                                                                                                                            <asp:TextBox ID="txtRegion" runat="server" CssClass="textboxgrey" MaxLength="50"
                                                                                                                                ReadOnly="True" TabIndex="3" Width="280px"></asp:TextBox></td>
                                                                                                                    </tr>
                                                                                                                    <tr>
                                                                                                                        <td class="textbold" style="width: 15%;">
                                                                                                                            Contract Period</td>
                                                                                                                        <td style="width: 29%;">
                                                                                                                            <asp:TextBox ID="txtContractPeriod" runat="server" CssClass="textboxgrey" MaxLength="50"
                                                                                                                                ReadOnly="True" TabIndex="3" Width="280px"></asp:TextBox></td>
                                                                                                                        <td class="textbold" style="width: 2%;">
                                                                                                                        </td>
                                                                                                                        <td class="textbold" style="width: 10%;">
                                                                                                                            Account Manager</td>
                                                                                                                        <td class="textbold" style="width: 29%;">
                                                                                                                            <asp:TextBox ID="txtActManager" runat="server" CssClass="textboxgrey" MaxLength="50"
                                                                                                                                ReadOnly="True" TabIndex="3" Width="280px"></asp:TextBox></td>
                                                                                                                    </tr>
                                                                                                                    <tr>
                                                                                                                        <td class="textbold" style="width: 15%;" valign="top">
                                                                                                                            Billing Cycle</td>
                                                                                                                        <td style="width: 29%;" valign="top">
                                                                                                                            <asp:TextBox ID="txtBillCycle" runat="server" CssClass="textboxgrey" MaxLength="50"
                                                                                                                                ReadOnly="True" TabIndex="3" Width="280px"></asp:TextBox></td>
                                                                                                                        <td class="textbold" style="width: 2%;">
                                                                                                                        </td>
                                                                                                                        <td class="textbold" style="width: 10%;" valign="top">
                                                                                                                        </td>
                                                                                                                        <td class="textbold" style="width: 29%;" valign="top">
                                                                                                                        </td>
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
                                                                                                        <td class="">
                                                                                                            &nbsp;</td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td>
                                                                                                            <table border="0" cellspacing="0" cellpadding="0" width="840px">
                                                                                                                <tr>
                                                                                                                    <td valign="top" class="redborder top" style="width: 95%; padding-right: 7px; padding-bottom: 7px;">
                                                                                                                        <table cellpadding="0" cellspacing="0" border="0">
                                                                                                                            <tr>
                                                                                                                                <td class="subheading" align="center" width="830px">
                                                                                                                                    Group MIDT</td>
                                                                                                                            </tr>
                                                                                                                            <tr>
                                                                                                                                <td>
                                                                                                                                    <table border="0" cellspacing="0" cellpadding="0" width="840px">
                                                                                                                                        <tr>
                                                                                                                                            <td>
                                                                                                                                                <table border="0" cellspacing="0" cellpadding="1" width="900x">
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
                                                                                                                                                <asp:Panel ID="pnlGroupMIDT" runat="server" Width="100%">
                                                                                                                                                    <asp:GridView ID="GvBGroupMIDT" runat="server" AutoGenerateColumns="False" TabIndex="6"
                                                                                                                                                        ShowHeader="false" Width="900px" EnableViewState="true" AllowSorting="false">
                                                                                                                                                        <Columns>
                                                                                                                                                            <asp:BoundField HeaderText="Last Available" DataField="LASTAVAIL" SortExpression="LASTAVAIL">
                                                                                                                                                                <HeaderStyle Width="10%" />
                                                                                                                                                                <ItemStyle Width="10%" />
                                                                                                                                                            </asp:BoundField>
                                                                                                                                                            <asp:BoundField HeaderText="1A" DataField="A" SortExpression="A">
                                                                                                                                                                <HeaderStyle Width="8%" />
                                                                                                                                                                <ItemStyle Width="8%" />
                                                                                                                                                            </asp:BoundField>
                                                                                                                                                            <asp:BoundField HeaderText="1B" DataField="B" SortExpression="B">
                                                                                                                                                                <HeaderStyle Width="8%" />
                                                                                                                                                                <ItemStyle Width="8%" Wrap="True" />
                                                                                                                                                            </asp:BoundField>
                                                                                                                                                            <asp:BoundField HeaderText="1G" DataField="G" SortExpression="G">
                                                                                                                                                                <HeaderStyle Width="8%" />
                                                                                                                                                                <ItemStyle Width="8%" />
                                                                                                                                                            </asp:BoundField>
                                                                                                                                                            <asp:BoundField HeaderText="1P" DataField="P" SortExpression="P">
                                                                                                                                                                <HeaderStyle Width="8%" />
                                                                                                                                                                <ItemStyle Width="8%" />
                                                                                                                                                            </asp:BoundField>
                                                                                                                                                            <asp:BoundField HeaderText="1W" DataField="W" SortExpression="W">
                                                                                                                                                                <HeaderStyle Width="8%" />
                                                                                                                                                                <ItemStyle Width="8%" Wrap="True" />
                                                                                                                                                            </asp:BoundField>
                                                                                                                                                            <asp:BoundField HeaderText="Total" DataField="TOTAL" SortExpression="TOTAL">
                                                                                                                                                                <HeaderStyle Width="10%" />
                                                                                                                                                                <ItemStyle Width="10%" Wrap="True" />
                                                                                                                                                            </asp:BoundField>
                                                                                                                                                            <asp:BoundField HeaderText="1A %" DataField="A_PER" SortExpression="APER">
                                                                                                                                                                <HeaderStyle Width="8%" />
                                                                                                                                                                <ItemStyle Width="8%" />
                                                                                                                                                            </asp:BoundField>
                                                                                                                                                            <asp:BoundField HeaderText="1B %" DataField="B_PER" SortExpression="BPER">
                                                                                                                                                                <HeaderStyle Width="8%" />
                                                                                                                                                                <ItemStyle Width="8%" Wrap="True" />
                                                                                                                                                            </asp:BoundField>
                                                                                                                                                            <asp:BoundField HeaderText="1G %" DataField="G_PER" SortExpression="GPER">
                                                                                                                                                                <HeaderStyle Width="8%" />
                                                                                                                                                                <ItemStyle Width="8%" />
                                                                                                                                                            </asp:BoundField>
                                                                                                                                                            <asp:BoundField HeaderText="1P %" DataField="P_PER" SortExpression="PPER">
                                                                                                                                                                <HeaderStyle Width="8%" />
                                                                                                                                                                <ItemStyle Width="8%" />
                                                                                                                                                            </asp:BoundField>
                                                                                                                                                            <asp:BoundField HeaderText="1W %" DataField="W_PER" SortExpression="WPER">
                                                                                                                                                                <HeaderStyle Width="8%" />
                                                                                                                                                                <ItemStyle Width="8%" Wrap="True" />
                                                                                                                                                            </asp:BoundField>
                                                                                                                                                        </Columns>
                                                                                                                                                        <AlternatingRowStyle CssClass="lightblue" />
                                                                                                                                                        <RowStyle CssClass="textbold" />
                                                                                                                                                        <HeaderStyle CssClass="Gridheading" HorizontalAlign="Left" />
                                                                                                                                                        <FooterStyle CssClass="Gridheading" />
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
                                                                                                                <tr>
                                                                                                                    <td class="">
                                                                                                                        &nbsp;</td>
                                                                                                                </tr>
                                                                                                                <tr>
                                                                                                                    <td valign="top" class="redborder top" style="width: 95%; padding-right: 7px; padding-bottom: 7px;">
                                                                                                                        <table cellpadding="0" cellspacing="0" border="0">
                                                                                                                            <tr>
                                                                                                                                <td class="subheading" align="center" width="830px">
                                                                                                                                    Agency MIDT (Agency which are pass of this deal )</td>
                                                                                                                            </tr>
                                                                                                                            <tr>
                                                                                                                                <td class="">
                                                                                                                                    &nbsp;</td>
                                                                                                                            </tr>
                                                                                                                            <tr>
                                                                                                                                <td valign="top">
                                                                                                                                    <table border="0" cellspacing="0" cellpadding="0" width="840px">
                                                                                                                                        <tr>
                                                                                                                                            <td>
                                                                                                                                                <table border="0" cellspacing="0" cellpadding="0" width="900x">
                                                                                                                                                    <tr class="Gridheading">
                                                                                                                                                        <td width="50px;">
                                                                                                                                                            S.No.</td>
                                                                                                                                                        <td width="110px;">
                                                                                                                                                            Agency Name</td>
                                                                                                                                                        <td width="90px;">
                                                                                                                                                            Office Id</td>
                                                                                                                                                        <td width="140px;">
                                                                                                                                                            Address</td>
                                                                                                                                                        <td width="100px;">
                                                                                                                                                            1A</td>
                                                                                                                                                        <td width="90px;">
                                                                                                                                                            1B</td>
                                                                                                                                                        <td width="90px;">
                                                                                                                                                            1G</td>
                                                                                                                                                        <td width="90px;">
                                                                                                                                                            1P</td>
                                                                                                                                                        <td width="90px;">
                                                                                                                                                            1W</td>
                                                                                                                                                        <td width="90px;">
                                                                                                                                                            TTP</td>
                                                                                                                                                    </tr>
                                                                                                                                                </table>
                                                                                                                                            </td>
                                                                                                                                        </tr>
                                                                                                                                        <tr>
                                                                                                                                            <td>
                                                                                                                                                <asp:Panel ID="PnlAgencyMIDT" runat="server" Width="100%">
                                                                                                                                                    <asp:GridView ID="GvBAgencyMIDT" runat="server" AutoGenerateColumns="False" TabIndex="6"
                                                                                                                                                        ShowHeader="false" Width="900px" EnableViewState="true" AllowSorting="false"
                                                                                                                                                        ShowFooter="true">
                                                                                                                                                        <Columns>
                                                                                                                                                            <asp:TemplateField HeaderText="S.No." ItemStyle-Width="40px" HeaderStyle-Width="40px">
                                                                                                                                                                <ItemTemplate>
                                                                                                                                                                    <asp:Label ID="lblSNo" runat="server" Width="40px"></asp:Label>
                                                                                                                                                                </ItemTemplate>
                                                                                                                                                            </asp:TemplateField>
                                                                                                                                                            <asp:BoundField HeaderText="Agency Name" DataField="Name" HeaderStyle-Width="110px"
                                                                                                                                                                ItemStyle-Width="110px"></asp:BoundField>
                                                                                                                                                            <asp:BoundField HeaderText="Office Id" DataField="OfficeID" ItemStyle-Width="90px"
                                                                                                                                                                HeaderStyle-Width="90px" ItemStyle-Wrap="true"></asp:BoundField>
                                                                                                                                                            <asp:BoundField HeaderText="Address" DataField="Address" HeaderStyle-Width="120px"
                                                                                                                                                                ItemStyle-Width="120px"></asp:BoundField>
                                                                                                                                                            <asp:BoundField HeaderText="1A" DataField="A" HeaderStyle-Width="90px" ItemStyle-Width="90px">
                                                                                                                                                            </asp:BoundField>
                                                                                                                                                            <asp:BoundField HeaderText="1B" DataField="B" HeaderStyle-Width="90px" ItemStyle-Width="90px">
                                                                                                                                                            </asp:BoundField>
                                                                                                                                                            <asp:BoundField HeaderText="1G" DataField="G" HeaderStyle-Width="90px" ItemStyle-Width="90px">
                                                                                                                                                            </asp:BoundField>
                                                                                                                                                            <asp:BoundField HeaderText="1P" DataField="P" HeaderStyle-Width="90px" ItemStyle-Width="90px">
                                                                                                                                                            </asp:BoundField>
                                                                                                                                                            <asp:BoundField HeaderText="1W" DataField="W" HeaderStyle-Width="90px" ItemStyle-Width="90px"
                                                                                                                                                                ItemStyle-Wrap="true"></asp:BoundField>
                                                                                                                                                            <asp:BoundField HeaderText="TTP" DataField="TTP" HeaderStyle-Width="90px" ItemStyle-Width="90px"
                                                                                                                                                                ItemStyle-Wrap="true"></asp:BoundField>
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
                                                                                                                        </table>
                                                                                                                    </td>
                                                                                                                </tr>
                                                                                                                <tr>
                                                                                                                    <td class="">
                                                                                                                        &nbsp;</td>
                                                                                                                </tr>
                                                                                                                <tr>
                                                                                                                    <td valign="top" class="redborder top" style="width: 95%; padding-right: 7px; padding-bottom: 7px;">
                                                                                                                        <table cellpadding="0" cellspacing="0" border="0">
                                                                                                                            <tr>
                                                                                                                                <td>
                                                                                                                                    <table border="0" cellspacing="0" cellpadding="1" width="840px">
                                                                                                                                        <tr>
                                                                                                                                            <td width="48%" valign="top">
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
                                                                                                                                                        <td valign="top">
                                                                                                                                                            <table border="0" cellspacing="0" cellpadding="0" width="100%">
                                                                                                                                                                <tr>
                                                                                                                                                                    <td valign="top">
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
                                                                                                                                                                    <td valign="top">
                                                                                                                                                                        <asp:Panel ID="PnlConnectivity" runat="server" Width="400px">
                                                                                                                                                                            <asp:GridView ID="GvConnectivity" runat="server" AutoGenerateColumns="False" TabIndex="6"
                                                                                                                                                                                ShowHeader="false" Width="400px" EnableViewState="true" AllowSorting="false"
                                                                                                                                                                                ShowFooter="true">
                                                                                                                                                                                <Columns>
                                                                                                                                                                                    <asp:BoundField HeaderText="Connectivity" DataField="BC_ONLINE_CATG_NAME" ItemStyle-Width="30%"
                                                                                                                                                                                        HeaderStyle-Width="30%"></asp:BoundField>
                                                                                                                                                                                    <asp:BoundField HeaderText="Unit Cost" DataField="BC_ONLINE_CATG_COST" ItemStyle-Width="20%"
                                                                                                                                                                                        HeaderStyle-Width="20%" ItemStyle-Wrap="true"></asp:BoundField>
                                                                                                                                                                                    <asp:BoundField HeaderText="No." DataField="CONN_COUNT" HeaderStyle-Width="20%" ItemStyle-Width="20%">
                                                                                                                                                                                    </asp:BoundField>
                                                                                                                                                                                    <asp:BoundField HeaderText="Total Cost" DataField="TOTAL" ItemStyle-Width="30%" HeaderStyle-Width="30%"
                                                                                                                                                                                        ItemStyle-HorizontalAlign="right"></asp:BoundField>
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
                                                                                                                                            <td width="48%" valign="top">
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
                                                                                                                                                        <td valign="top">
                                                                                                                                                            <table border="0" cellspacing="0" cellpadding="0" width="100%">
                                                                                                                                                                <tr>
                                                                                                                                                                    <td valign="top">
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
                                                                                                                                                                    <td valign="top">
                                                                                                                                                                        <asp:Panel ID="PnlHardware" runat="server" Width="400px">
                                                                                                                                                                            <asp:GridView ID="GvHardware" runat="server" AutoGenerateColumns="False" TabIndex="6"
                                                                                                                                                                                ShowHeader="false" Width="400px" EnableViewState="true" AllowSorting="false"
                                                                                                                                                                                ShowFooter="true">
                                                                                                                                                                                <Columns>
                                                                                                                                                                                    <asp:BoundField HeaderText="Hardware" DataField="BC_EQP_CATG_TYPE" ItemStyle-Width="30%"
                                                                                                                                                                                        HeaderStyle-Width="30%"></asp:BoundField>
                                                                                                                                                                                    <asp:BoundField HeaderText="Unit Cost" DataField="BC_EQP_CATG_COST" ItemStyle-Width="20%"
                                                                                                                                                                                        HeaderStyle-Width="20%" ItemStyle-Wrap="true"></asp:BoundField>
                                                                                                                                                                                    <asp:BoundField HeaderText="NO." DataField="PRODUCT_COUNT" HeaderStyle-Width="20%"
                                                                                                                                                                                        ItemStyle-Width="20%"></asp:BoundField>
                                                                                                                                                                                    <asp:BoundField HeaderText="Total Cost" DataField="TOTAL" ItemStyle-Width="30%" HeaderStyle-Width="30%"
                                                                                                                                                                                        ItemStyle-HorizontalAlign="right"></asp:BoundField>
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
                                                                                                                
                                                                                                                  <!--  Start of Inccentive Plan
                                                                                                    -->
                                                                                                    <tr>
                                                                                                        <td class="">
                                                                                                            &nbsp;</td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td valign="top" class="redborder top" style="width:830px; padding-right: 7px; padding-bottom: 7px;">
                                                                                                            <table cellpadding="0" cellspacing="0" border="0">
                                                                                                                <tr>
                                                                                                                    <td class="subheading" align="center" width="830px">
                                                                                                                        Incentive Plans</td>
                                                                                                                </tr>
                                                                                                                <tr>
                                                                                                                    <td class="">
                                                                                                                        &nbsp;</td>
                                                                                                                </tr>
                                                                                                                <tr>
                                                                                                                    <td class="">
                                                                                                                        <table border="0" cellpadding="2" cellspacing="0">
                                                                                                                            <tr>
                                                                                                                                <td align="left">
                                                                                                                                    <table border="0" cellpadding="2" cellspacing="1" width="500px">
                                                                                                                                        <tr>
                                                                                                                                            <td style="width: 135px">
                                                                                                                                                <b>Type </b>
                                                                                                                                            </td>
                                                                                                                                            <td colspan="5">
                                                                                                                                                <b>
                                                                                                                                                    <asp:Label ID="lblType" runat="server"></asp:Label></b>
                                                                                                                                            </td>
                                                                                                                                        </tr>
                                                                                                                                        <tr>
                                                                                                                                            <td style="width: 135px">
                                                                                                                                                <b>
                                                                                                                                                    <asp:Label ID="PnlLblCase" runat="server" Text="Case Name" Width="100%" Visible="true"></asp:Label></b>
                                                                                                                                            </td>
                                                                                                                                            <td>
                                                                                                                                                <b>
                                                                                                                                                    <asp:Label ID="LblCase" runat="server" CssClass="textbox" Width="131px"></asp:Label></b>
                                                                                                                                            </td>
                                                                                                                                            <td>
                                                                                                                                            </td>
                                                                                                                                            <td>
                                                                                                                                            </td>
                                                                                                                                            <td>
                                                                                                                                            </td>
                                                                                                                                        </tr>
                                                                                                                                        <tr>
                                                                                                                                            <td style="width: 135px">
                                                                                                                                                <b>
                                                                                                                                                    <asp:Label ID="pnlLabelSoleAmt" Text="Sole Amount" runat="server" Width="100%">
                                                                                                                                       
                                                                                                                                                    </asp:Label></b>
                                                                                                                                            </td>
                                                                                                                                            <td>
                                                                                                                                                <b>
                                                                                                                                                    <asp:Label ID="LblSoleAmt" runat="server" CssClass="textbox" Width="131px"></asp:Label></b>
                                                                                                                                            </td>
                                                                                                                                            <td>
                                                                                                                                            </td>
                                                                                                                                            <td>
                                                                                                                                                <b>
                                                                                                                                                    <asp:Label ID="pnlLblSoleAmtBonus" Text="Bonus" runat="server" Width="100%">
                                                                                                                                      
                                                                                                                                                    </asp:Label>
                                                                                                                                                </b>
                                                                                                                                            </td>
                                                                                                                                            <td>
                                                                                                                                                <b>
                                                                                                                                                    <asp:Label ID="LblSoleAmtBonus" runat="server" CssClass="textbox" Width="131px"></asp:Label></b>
                                                                                                                                            </td>
                                                                                                                                        </tr>
                                                                                                                                        <tr>
                                                                                                                                            <td style="width: 135px">
                                                                                                                                                <b>
                                                                                                                                                    <asp:Label Text="Fixed Amount" ID="pnlLblFixPayment" runat="server" Width="100%">
                                                                                                                                       
                                                                                                                                                    </asp:Label>
                                                                                                                                                </b>
                                                                                                                                            </td>
                                                                                                                                            <td>
                                                                                                                                                <b>
                                                                                                                                                    <asp:Label ID="LblFixPayment" runat="server" CssClass="textbox" Width="131px"></asp:Label></b>
                                                                                                                                            </td>
                                                                                                                                            <td>
                                                                                                                                            </td>
                                                                                                                                            <td>
                                                                                                                                            </td>
                                                                                                                                            <td>
                                                                                                                                            </td>
                                                                                                                                        </tr>
                                                                                                                                        <tr>
                                                                                                                                            <td style="width: 135px">
                                                                                                                                                <b>
                                                                                                                                                    <asp:Label ID="pnlLblUpfront" Text="Upfront Amount" runat="server" Width="100%">
                                                                                                                                       
                                                                                                                                                    </asp:Label></b>
                                                                                                                                            </td>
                                                                                                                                            <td>
                                                                                                                                                <b>
                                                                                                                                                    <asp:Label ID="LblUpfront" runat="server" CssClass="textbox" Width="131px"></asp:Label></b>
                                                                                                                                            </td>
                                                                                                                                            <td>
                                                                                                                                            </td>
                                                                                                                                            <td>
                                                                                                                                            </td>
                                                                                                                                            <td>
                                                                                                                                            </td>
                                                                                                                                        </tr>
                                                                                                                                        <tr>
                                                                                                                                            <td style="width: 135px">
                                                                                                                                                <b>
                                                                                                                                                    <asp:Label ID="pnlLblFixUpfront" Text="Fix Upfront" runat="server" Width="100%">
                                                                                                                                      
                                                                                                                                                    </asp:Label>
                                                                                                                                                </b>
                                                                                                                                            </td>
                                                                                                                                            <td>
                                                                                                                                                <b>
                                                                                                                                                    <asp:Label ID="LblFixUpfront" runat="server" CssClass="textbox" Width="131px"></asp:Label></b>
                                                                                                                                            </td>
                                                                                                                                            <td>
                                                                                                                                            </td>
                                                                                                                                            <td>
                                                                                                                                            </td>
                                                                                                                                            <td>
                                                                                                                                            </td>
                                                                                                                                        </tr>
                                                                                                                                    </table>
                                                                                                                                </td>
                                                                                                                            </tr>
                                                                                                                            <tr>
                                                                                                                                <td>
                                                                                                                                    <asp:Panel ID="pnlIncentivePlan" runat="server" Width="100%">
                                                                                                                                        <asp:Repeater ID="RepIncPlan" runat="server">
                                                                                                                                            <HeaderTemplate>
                                                                                                                                                <table>
                                                                                                                                            </HeaderTemplate>
                                                                                                                                            <ItemTemplate>
                                                                                                                                                <tr>
                                                                                                                                                    <td>
                                                                                                                                                        <b>
                                                                                                                                                            <table>
                                                                                                                                                                <tr>
                                                                                                                                                                    <td style="width: 135px">
                                                                                                                                                                        <b>Case Name</b></td>
                                                                                                                                                                    <td>
                                                                                                                                                                        <b>
                                                                                                                                                                            <asp:Label ID="lblCase" runat="server" Text='<%#Eval("INC_PLAN_NAME") %>'></asp:Label></b></td>
                                                                                                                                                                </tr>
                                                                                                                                                            </table>
                                                                                                                                                    </td>
                                                                                                                                                </tr>
                                                                                                                                                <tr>
                                                                                                                                                    <td>
                                                                                                                                                        <asp:CheckBoxList ID="ChkListCriteria" runat="server" RepeatDirection="Horizontal"
                                                                                                                                                            Enabled="false" RepeatColumns="8">
                                                                                                                                                        </asp:CheckBoxList>
                                                                                                                                                    </td>
                                                                                                                                                </tr>
                                                                                                                                                <tr>
                                                                                                                                                    <td valign="top">
                                                                                                                                                        <asp:GridView ID="GvIncPlan" runat="server" AutoGenerateColumns="False" TabIndex="6"
                                                                                                                                                            ShowHeader="true" Width="400px" EnableViewState="true" AllowSorting="false" ShowFooter="true"
                                                                                                                                                            OnRowDataBound="GvIncPlan_RowDataBound">
                                                                                                                                                            <Columns>
                                                                                                                                                                <asp:BoundField HeaderText="Start" DataField="SLABS_START" ItemStyle-HorizontalAlign="Center"
                                                                                                                                                                    ItemStyle-Width="35%" HeaderStyle-Width="35%"></asp:BoundField>
                                                                                                                                                                <asp:BoundField HeaderText="End" DataField="SLABS_END" ItemStyle-HorizontalAlign="Center"
                                                                                                                                                                    ItemStyle-Width="35%" HeaderStyle-Width="35%" ItemStyle-Wrap="true"></asp:BoundField>
                                                                                                                                                                <asp:BoundField HeaderText="Amount" DataField="SLABS_RATE" HeaderStyle-Width="30%"
                                                                                                                                                                    ItemStyle-HorizontalAlign="Right" ItemStyle-Width="30%"></asp:BoundField>
                                                                                                                                                            </Columns>
                                                                                                                                                            <AlternatingRowStyle CssClass="lightblue" />
                                                                                                                                                            <RowStyle CssClass="textbold" />
                                                                                                                                                            <HeaderStyle CssClass="Gridheading" ForeColor="White" />
                                                                                                                                                            <FooterStyle CssClass="Gridheading" ForeColor="White" />
                                                                                                                                                        </asp:GridView>
                                                                                                                                                    </td>
                                                                                                                                                </tr>
                                                                                                                                                <tr>
                                                                                                                                                    <td>
                                                                                                                                                        <asp:HiddenField ID="hdCaseId" runat="server" Value='<%# Eval("Case_Id") %>' />
                                                                                                                                                    </td>
                                                                                                                                                </tr>
                                                                                                                                            </ItemTemplate>
                                                                                                                                            <FooterTemplate>
                                                                                                                                                </table></FooterTemplate>
                                                                                                                                        </asp:Repeater>
                                                                                                                                    </asp:Panel>
                                                                                                                                </td>
                                                                                                                            </tr>
                                                                                                                        </table>
                                                                                                                        &nbsp;</td>
                                                                                                                </tr>
                                                                                                                <!--
                                                                                                           End of Inccentive Plan
                                                                                                     -->
                                                                                                            </table>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                            </table>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                  
                                                                                                </table>
                                                                                            </asp:Panel>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td>
                                                                                            <asp:Panel ID="PnlApprovers" runat="server" CssClass="displayNone">
                                                                                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                                                                    <tr>
                                                                                                        <td class="">
                                                                                                            &nbsp;</td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td class="subheading" align="center">
                                                                                                            Approvers
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td>
                                                                                                            <asp:GridView ID="GvApprovers" runat="server" Width="100%" AutoGenerateColumns="False"
                                                                                                                TabIndex="6" ShowHeader="true" EnableViewState="true">
                                                                                                                <Columns>
                                                                                                                    <asp:TemplateField HeaderText="Lavel" ItemStyle-Width="120px" HeaderStyle-Width="120px">
                                                                                                                        <ItemTemplate>
                                                                                                                            <asp:Label ID="lblLavel" runat="server" Width="180px"></asp:Label>
                                                                                                                        </ItemTemplate>
                                                                                                                    </asp:TemplateField>
                                                                                                                    <asp:BoundField HeaderText="Approvers Name" DataField="EMPLOYEE_NAME"></asp:BoundField>
                                                                                                                    <asp:BoundField HeaderText="Status" DataField="APPROVAL_STATUS_NAME"></asp:BoundField>
                                                                                                                </Columns>
                                                                                                                <AlternatingRowStyle CssClass="lightblue" />
                                                                                                                <RowStyle CssClass="textbold" Wrap="true" />
                                                                                                                <HeaderStyle CssClass="Gridheading" ForeColor="White" HorizontalAlign="Left" />
                                                                                                                <FooterStyle CssClass="Gridheading" />
                                                                                                            </asp:GridView>
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
                                                                                            <asp:Panel ID="pnlRemarksRegion" runat="server" CssClass="displayNone">
                                                                                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                                                                    <tr>
                                                                                                        <td class="">
                                                                                                            &nbsp;</td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td class="subheading" align="center">
                                                                                                            Remarks (Region)
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td class="">
                                                                                                            <asp:GridView EnableViewState="true" ID="gvRegionRem" runat="server" AutoGenerateColumns="False"
                                                                                                                TabIndex="6" Width="800px">
                                                                                                                <Columns>
                                                                                                                    <%--  <asp:BoundField HeaderText="Remarks Type" DataField="BC_REMARKS_TYPE" HeaderStyle-Width="20%" />--%>
                                                                                                                    <asp:BoundField HeaderText="User" DataField="EmployeeName" HeaderStyle-Width="20%" />
                                                                                                                    <asp:BoundField HeaderText="DateTime" DataField="REMARKS_DTTI" HeaderStyle-Width="20%" />
                                                                                                                    <asp:TemplateField HeaderStyle-Width="40%">
                                                                                                                        <ItemTemplate>
                                                                                                                            <asp:TextBox ID="txtDesc" runat="server" CssClass="textbox" Height="150px" BorderStyle="none"
                                                                                                                                TextMode="MultiLine" Wrap="true" Width="100%" ReadOnly="True" BorderColor="white"
                                                                                                                                BorderWidth="0px" Text='<%#Eval("BC_REMARKS") %>'></asp:TextBox>
                                                                                                                        </ItemTemplate>
                                                                                                                    </asp:TemplateField>
                                                                                                                </Columns>
                                                                                                                <AlternatingRowStyle CssClass="lightblue center" />
                                                                                                                <RowStyle CssClass="textbold center" />
                                                                                                                <HeaderStyle CssClass="Gridheading center" />
                                                                                                            </asp:GridView>
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
                                                                                            <asp:Panel ID="PnlRemMarket" runat="server" CssClass="displayNone">
                                                                                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                                                                    <tr>
                                                                                                        <td class="">
                                                                                                            &nbsp;</td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td class="subheading" align="center">
                                                                                                            Remarks (Market)
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td class="">
                                                                                                            <asp:GridView EnableViewState="true" ID="gvMarketRem" runat="server" AutoGenerateColumns="False"
                                                                                                                TabIndex="6" Width="800px">
                                                                                                                <Columns>
                                                                                                                    <%--                       <asp:BoundField HeaderText="Remarks Type" DataField="BC_REMARKS_TYPE" HeaderStyle-Width="20%" />  --%>
                                                                                                                    <asp:BoundField HeaderText="User" DataField="EmployeeName" HeaderStyle-Width="20%" />
                                                                                                                    <asp:BoundField HeaderText="DateTime" DataField="REMARKS_DTTI" HeaderStyle-Width="20%" />
                                                                                                                    <asp:TemplateField HeaderStyle-Width="40%">
                                                                                                                        <ItemTemplate>
                                                                                                                            <asp:TextBox ID="txtDesc" runat="server" CssClass="textbox" Height="150px" BorderStyle="none"
                                                                                                                                TextMode="MultiLine" Wrap="true" Width="100%" ReadOnly="True" BorderColor="white"
                                                                                                                                BorderWidth="0px" Text='<%#Eval("BC_REMARKS") %>'></asp:TextBox>
                                                                                                                        </ItemTemplate>
                                                                                                                    </asp:TemplateField>
                                                                                                                </Columns>
                                                                                                                <AlternatingRowStyle CssClass="lightblue center" />
                                                                                                                <RowStyle CssClass="textbold center" />
                                                                                                                <HeaderStyle CssClass="Gridheading center" />
                                                                                                            </asp:GridView>
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
                                                                            <td style="height: 21px">
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
                                    <tr>
                                        <td class="top" colspan="2">
                                            <asp:HiddenField ID="lblPanelClick" runat="server"></asp:HiddenField>
                                            <asp:HiddenField ID="lblSubPanelClick" runat="server"></asp:HiddenField>
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
