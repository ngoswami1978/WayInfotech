<%@ Page Language="VB" AutoEventWireup="false" CodeFile="MSUP_AG_BCase_Inc_CPS.aspx.vb"
    Inherits="Setup_MSUP_AG_BCase_Inc_CPS" %>

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
                                            Manage CPS</td>
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
                                                                                                                <td valign="top" align ="center"  >
                                                                                                                    <table border="0" cellpadding="0" cellspacing="0" width="660px">
                                                                                                                        <tr class="Gridheading">
                                                                                                                            <td>
                                                                                                                                Incentive
                                                                                                                            </td>
                                                                                                                            <td>
                                                                                                                                Rate
                                                                                                                            </td>
                                                                                                                            <td>
                                                                                                                                Expected Segments
                                                                                                                            </td>
                                                                                                                            <td>
                                                                                                                                Total Cost
                                                                                                                            </td>
                                                                                                                        </tr>
                                                                                                                        <tr class="textbold">
                                                                                                                            <td>
                                                                                                                                <asp:TextBox ID="txtIncentive" runat="server" CssClass="textbox"></asp:TextBox></td>
                                                                                                                            <td>
                                                                                                                                <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox></td>
                                                                                                                            <td>
                                                                                                                                <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox></td>
                                                                                                                            <td>
                                                                                                                                <asp:TextBox ID="txtTotalOne" runat="server" CssClass="textboxgrey right" ReadOnly="true"></asp:TextBox></td>
                                                                                                                        </tr>
                                                                                                                        <tr class="lightblue">
                                                                                                                            <td>
                                                                                                                                Total Cost</td>
                                                                                                                            <td>
                                                                                                                                <asp:TextBox ID="TextBox11" runat="server"></asp:TextBox></td>
                                                                                                                            <td>
                                                                                                                                <asp:TextBox ID="TextBox10" runat="server"></asp:TextBox></td>
                                                                                                                            <td>
                                                                                                                                <asp:TextBox ID="TextBox5" runat="server" CssClass="textboxgrey right" ReadOnly="true" ></asp:TextBox></td>
                                                                                                                        </tr>
                                                                                                                        <tr class="textbold">
                                                                                                                            <td >
                                                                                                                                Total Segments</td>
                                                                                                                            <td >
                                                                                                                                <asp:TextBox ID="TextBox12" runat="server"></asp:TextBox></td>
                                                                                                                            <td >
                                                                                                                                <asp:TextBox ID="TextBox9" runat="server"></asp:TextBox></td>
                                                                                                                            <td >
                                                                                                                                <asp:TextBox ID="TextBox6" runat="server" CssClass="textboxgrey right" ReadOnly="true"></asp:TextBox></td>
                                                                                                                        </tr>
                                                                                                                        <tr class="lightblue">
                                                                                                                            <td >
                                                                                                                                CPS</td>
                                                                                                                            <td >
                                                                                                                                <asp:TextBox ID="TextBox13" runat="server"></asp:TextBox></td>
                                                                                                                            <td >
                                                                                                                                <asp:TextBox ID="TextBox8" runat="server"></asp:TextBox></td>
                                                                                                                            <td >
                                                                                                                                <asp:TextBox ID="TextBox7" runat="server" CssClass="textboxgrey right" ReadOnly="true"></asp:TextBox></td>
                                                                                                                        </tr>
                                                                                                                    </table>
                                                                                                                </td>
                                                                                                                <td valign="top">
                                                                                                                    <table border="0" cellpadding="0" cellspacing="0" width="140px">
                                                                                                                        <tr>
                                                                                                                            <td class="textbold" style="width: 100%">
                                                                                                                            </td>
                                                                                                                            <td class="center top ">
                                                                                                                                <asp:Button ID="BtnSave" runat="server" TabIndex="3" CssClass="button topMargin"
                                                                                                                                    Text="Save" Width="100px" AccessKey="S" />&nbsp;</td>
                                                                                                                        </tr>
                                                                                                                        <tr>
                                                                                                                            <td class="textbold" style="width: 90%">
                                                                                                                            </td>
                                                                                                                            <td class="center top ">
                                                                                                                                <asp:Button ID="btnReset" runat="server" TabIndex="3" CssClass="button topMargin"
                                                                                                                                    Text="Reset" Width="100px" AccessKey="r" />&nbsp;</td>
                                                                                                                        </tr>
                                                                                                                        <tr>
                                                                                                                            <td class="textbold" style="width: 90%">
                                                                                                                            </td>
                                                                                                                            <td class="center top ">
                                                                                                                                &nbsp;</td>
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
