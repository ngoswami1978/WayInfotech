<%@ Page Language="VB" AutoEventWireup="false" CodeFile="MSUP_AG_BCase_Inc_PastPayment.aspx.vb"
    Inherits="Setup_MSUP_AG_BCase_Inc_PastPayment" %>

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
    
    
    function Data()
    {
         document.getElementById('txtTotalOne').value="890";
         return false;
    }
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


function TabMethodAgencyIncentiveGroup(id,total)
{  
//{debugger;} 
        var ctextIncentiveFront;
        var ctextIncentiveBack;
        var HIncentivecontrol;
        var HIncentiveFlush;
        
        ctextIncentiveFront = id.substring(0,29);        
        ctextIncentiveBack = id.substring(31,40);   
       
        for(var i=0;i<total;i++)
        {
            HIncentiveFlush = "0" + i;
            HIncentivecontrol = ctextIncentiveFront +  HIncentiveFlush + ctextIncentiveBack;
            if (document.getElementById(HIncentivecontrol).className != "displayNone")
            {
                document.getElementById(HIncentivecontrol).className="headingtabactive";
            }
           
        }
       
       var strChain_Code="";
        strChain_Code = document.getElementById('hdEnChainCode').value;
       document.getElementById(id).className="headingtab";     
      
       document.getElementById('lblSubIncentivePanelClick').value =id; 
       
    
      
       
       if (id == (ctextIncentiveFront +  "00" + ctextIncentiveBack))
       {   
            window.location.href="MSUP_AG_BCase_Inc_CPS.aspx?Action=U&Chain_Code=" + strChain_Code  ;              
            return false;
       }       
       else if (id == (ctextIncentiveFront +  "01" + ctextIncentiveBack))
       {   
            window.location.href="MSUP_AG_BCase_Inc_Slab.aspx?Action=U&Chain_Code=" + strChain_Code  ;   
            return false;                      
       }
       else if (id == (ctextIncentiveFront +  "02" + ctextIncentiveBack))
       {
           window.location.href="MSUP_AG_BCase_Inc_FixedInc.aspx?Action=U&Chain_Code=" + strChain_Code  ;   
            return false;   
       }
       else if (id == (ctextIncentiveFront +  "03" + ctextIncentiveBack))
       {    
            window.location.href="MSUP_AG_BCase_Inc_Commitments.aspx?Action=U&Chain_Code=" + strChain_Code  ;   
            return false;   
       }
       
        else if (id == (ctextIncentiveFront +  "04" + ctextIncentiveBack))
       {
             window.location.href="MSUP_AG_BCase_Inc_Breakup.aspx?Action=U&Chain_Code=" + strChain_Code  ;   
            return false;   
       }
           else if (id == (ctextIncentiveFront +  "05" + ctextIncentiveBack))
       {
             window.location.href="MSUP_AG_BCase_Inc_PastPayment.aspx?Action=U&Chain_Code=" + strChain_Code  ;   
            return false;   
       }
          else if (id == (ctextIncentiveFront +  "06" + ctextIncentiveBack))
       {
             window.location.href="MSUP_AG_BCase_Inc_ProposedDeal.aspx?Action=U&Chain_Code=" + strChain_Code  ;   
            return false;   
       }
}

    </script>

</head>
<body>
    <form id="form1" runat="server">
        <table width="860px" class="border_rightred left">
            <tr>
                <td class="top">
                    <table width="100%">
                        <tr>
                            <td class="top">
                                <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td class="top left" style="width: 80%">
                                            <span class="menu">Travel Agency->Agency Group-></span><span class="sub_menu">Bussiness
                                                Case-&gt;Incentive</span>
                                        </td>
                                        <td class="right" style="width: 20%">
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td class="heading center" colspan="2" style="width: 100%">
                                            Manage Past Payment</td>
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
                                            <asp:HiddenField ID="lblSubIncentivePanelClick" runat="server"></asp:HiddenField>
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
                                                                <td valign="top" class="redborder">
                                                                    <table width="830x" border="0" cellspacing="0" cellpadding="0">
                                                                        <tr>
                                                                            <td valign="top" style="width: 830px; padding-left: 7px; padding-bottom: 7px;">
                                                                                <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                                                                    <tr>
                                                                                        <td valign="top" style="height: 5PX">
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td class="top" style="height: 22px; width: 100%" colspan="2">
                                                                                            <asp:Repeater ID="theTabIncentiveGroupStrip" runat="server">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Button ID="Button3" Width="100px" CssClass="headingtabactive" runat="server"
                                                                                                        Text="<%# Container.DataItem %>" />
                                                                                                </ItemTemplate>
                                                                                            </asp:Repeater>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td class="redborder top" colspan="2" style="width: 100%; height: 150px;">
                                                                                            <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                                                                                <tr>
                                                                                                    <td class="center TOP" style="width: 820px;">
                                                                                                        <asp:Label ID="lblError" CssClass="ErrorMsg" runat="server" EnableViewState="False"></asp:Label>
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td class="top" style="width: 100%;">
                                                                                                        <table width="827px" border="0" class="left" cellpadding="2" cellspacing="0">
                                                                                                            <tr>
                                                                                                                <td valign="top" style="height: 10PX">
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                                <td valign="top" align="center">
                                                                                                                    <table border="0" cellpadding="0" cellspacing="0" width="680px">
                                                                                                                        <tr>
                                                                                                                            <td>
                                                                                                                            </td>
                                                                                                                        </tr>
                                                                                                                    </table>
                                                                                                                </td>
                                                                                                                <td valign="top">
                                                                                                                    <table border="0" cellpadding="0" cellspacing="0" width="120px">
                                                                                                                        <tr>
                                                                                                                            <td>
                                                                                                                            </td>
                                                                                                                        </tr>
                                                                                                                    </table>
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                                <td height="10px">
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                                <td class="textbold" colspan="6" style="width: 100%" valign="top">
                                                                                                                    <asp:GridView ID="GvPastPaid" runat="server" AutoGenerateColumns="False" TabIndex="6"
                                                                                                                        Width="100%" EnableViewState="true" AllowSorting="True" ShowFooter="false">
                                                                                                                        <Columns>
                                                                                                                              <asp:TemplateField  HeaderText ="PAST PAYMENTS (IF ANY)" HeaderStyle-Width="20%"  HeaderStyle-Wrap="false" >                                                                                                                               
                                                                                                                              </asp:TemplateField>
                                                                                                                           
                                                                                                                            <asp:BoundField HeaderText="Amount" DataField="AMOUNT" 
                                                                                                                                HeaderStyle-Width="20%" ItemStyle-Wrap="true"></asp:BoundField>
                                                                                                                            <asp:BoundField HeaderText="Period" DataField="PERIOD" HeaderStyle-Width="20%">
                                                                                                                            </asp:BoundField>
                                                                                                                            <asp:BoundField HeaderText="Seg Paid For" DataField="SEGPAIDFOR" 
                                                                                                                                HeaderStyle-Width="15%" ItemStyle-HorizontalAlign="right"></asp:BoundField>
                                                                                                                                <asp:BoundField HeaderText="Gross" DataField="GROSS" 
                                                                                                                                HeaderStyle-Width="15%" ItemStyle-HorizontalAlign="right"></asp:BoundField>
                                                                                                                                <asp:BoundField HeaderText="CPS" DataField="CPS"
                                                                                                                                HeaderStyle-Width="20%" ItemStyle-HorizontalAlign="right"></asp:BoundField>
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
                    </table>
                    &nbsp;
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
