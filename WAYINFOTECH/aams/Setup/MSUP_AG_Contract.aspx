<%@ Page Language="VB" AutoEventWireup="false" CodeFile="MSUP_AG_Contract.aspx.vb" Inherits="Setup_MSUP_AG_Contract" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
   <title>AAMS::Travel Agency::Manage Contract</title>
    <link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />
     <link rel="stylesheet" type="text/css" media="all" href="../Calender/calendar-blue.css" title="win2k-cold-1" />
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
             window.location.href="MSUP_AG_BusinessCase.aspx?Action=U&Chain_Code=" + strChain_Code  ;   
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
            return false;   
       }
       else if (id == (ctextFront +  "06" + ctextBack))
       {
             window.location.href="MSUP_AG_BusinessCase.aspx?Action=U&Chain_Code=" + strChain_Code  ;   
            return false;   
       }
}

</script>

</head>
<body >
    <form id="form1" runat="server"  defaultbutton="btnSave"  >
        <table width="840px"  class="border_rightred left">
            <tr>
                <td class="top">
                    <table width="100%">
                        <tr>
                            <td class="top">
                                <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td class="top left" style="width:80%">
                                            <span class="menu">Travel Agency->Agency Group-></span><span class="sub_menu">Contract</span>
                                        </td>
                                         <td class="right" style="width:20%">
                                             &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td class="heading center" colspan="2" style="width:100%">
                                            Manage Contract</td>
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
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="top" style="height: 22px;width:100%" colspan="2"> 
                                            <asp:Repeater ID="theTabStrip" runat="server" >
                                                <ItemTemplate>
                                                    <asp:Button ID="Button1" width="100px" CssClass="headingtabactive" runat="server" Text="<%# Container.DataItem %>" />
                                                   
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </td>
                                        
                                    </tr>
                                    <tr>
                                        <td  class="redborder top" colspan="2" style="width:100%">
                                            <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td class="center TOP"   >
                                                    <asp:Label ID="lblError" CssClass="ErrorMsg" runat="server" EnableViewState="False"></asp:Label>
                                                        </td>
                                                </tr>
                                                <tr>
                                                    <td class="top" style="width:100%;">
                                                  
                                                            <table width="100%" border="0" class="left" cellpadding="2" cellspacing="0">
                                                                <tr>
                                                                    <td class="textbold" style="width:90%" >
                                                                    <asp:Panel ID="pnlCall" runat="server" Width="100%">
                                                                    <table width="100%" border="0" class="left" cellpadding="2" cellspacing="0">
                                                                        <tr>
                                                                            <td style="width: 2%"></td>
                                                                            <td class="gap" colspan="4"></td>
                                                                        </tr>
                                                                  <tr>
                                                                    <td ></td>
                                                                    <td class="subheading" colspan="4">
                                                                        Agency Details</td>
                                                                </tr>
                                                                   
                                                                      
                                                                
                                                                
                                                                    </table>
                                                                       
                                                                       </asp:Panel>
                                                                        &nbsp; &nbsp;
                                                                       
                                                                    </td>
                                                                    <td class="center top " colspan="2" rowspan="1">
                                                                        <asp:Button ID="btnSave" runat="server" TabIndex="3" CssClass="button topMargin" Text="Save" Width="100px" OnClientClick="return ManageCallLogPage()" AccessKey="s" /><br />
                                                                        &nbsp;<asp:Button ID="btnNew" runat="server" TabIndex="3" CssClass="button topMargin" Text="New" Width="100px" AccessKey="n" />&nbsp;<br />
                                                                         <asp:Button ID="btnReset" runat="server" TabIndex="3" CssClass="button topMargin" Text="Reset" Width="100px" AccessKey="r" /><br />
                                                                        &nbsp;
                                                                       
                                                                        </td>
                                                                </tr>
                                                                
                                                                  <tr>
                                                                  <td> <input type="hidden" runat="server" id="hdEnChainCode" style="width: 21px" />
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
                    &nbsp;
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
