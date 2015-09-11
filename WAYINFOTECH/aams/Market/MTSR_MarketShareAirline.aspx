<%@ Page Language="VB" AutoEventWireup="false" CodeFile="MTSR_MarketShareAirline.aspx.vb"
    Inherits="Market_MTSR_MarketShareAirline" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />
    <title>AAMS::Market::Market Share Report</title>

    <script src="../JavaScript/AAMS.js" type="text/javascript"></script>

    <link rel="stylesheet" type="text/css" media="all" href="../Calender/calendar-blue.css"
        title="win2k-cold-1" />

    <script type="text/javascript" src="../Calender/calendar.js"></script>

    <!-- import the language module -->

    <script type="text/javascript" src="../Calender/lang/calendar-en.js"></script>

    <!-- import the calendar setup module -->

    <script type="text/javascript" src="../Calender/calendar-setup.js"></script>

    <script language="javascript" type="text/javascript"> 
    function SelectProdutivity()
     {
      
       var objdrpProductivity =document.getElementById('drpProductivity').value;
       var objtxtFrom =document.getElementById('txtFrom');
       var objtxtTo =document.getElementById('txtTo');       
       if (objdrpProductivity=='') 
       {
          objtxtFrom.disabled=true;
          objtxtTo.disabled=true;
          objtxtFrom.value='0';
          objtxtTo.value='0';
          objtxtFrom.className='textboxgrey';
          objtxtTo.className='textboxgrey';
       } 
    else if (objdrpProductivity=='7') 
       {
         
          objtxtFrom.disabled=false;
          objtxtTo.disabled=false;
          objtxtFrom.value='0';
          objtxtTo.value='0';
          objtxtFrom.className='textbox';
          objtxtTo.className='textbox';  
       }   
       else 
       {
          objtxtFrom.disabled=false;
          objtxtTo.disabled=true;
          objtxtFrom.value='0';
          objtxtTo.value='';  
          objtxtFrom.className='textbox';
          objtxtTo.className='textboxgrey';   
       }
       
     }
     
       function CheckValidation()
     {
          
               if(document.getElementById('<%=txtFrom.ClientID%>').value.trim()!="")
                  {
                    if(IsDataValid(document.getElementById("txtFrom").value,12)==false)
                    {
                        document.getElementById('lblError').innerHTML='Productivity range is not valid.';             
                        document.getElementById("txtFrom").focus();
                        return false;
                     }                  
                  }
               if(document.getElementById('<%=txtTo.ClientID%>').value.trim()!="")
              {
                if(IsDataValid(document.getElementById("txtTo").value,12)==false)
                {
                    document.getElementById('lblError').innerHTML='Productivity range is not valid.';             
                    document.getElementById("txtTo").focus();
                    return false;
                 }                  
              }
                 if(parseInt(document.getElementById("txtFrom").value) >parseInt(document.getElementById("txtTo").value))
                    {                   
                    document.getElementById("lblError").innerHTML='Productivity range is not valid.';          
                    document.getElementById("txtFrom").focus();
                    return false;
                    } 
     }
    
    </script>

</head>
<body>
    <form id="form1" runat="server" defaultfocus="rdSummOpt" defaultbutton="btnExport">
        <table width="845px" align="left" class="border_rightred">
            <tr>
                <td valign="top" style="width: 845px;">
                    <table width="100%" class="left">
                        <tr>
                            <td>
                                <span class="menu">Market -&gt;</span><span class="sub_menu">Market Share Report</span></td>
                        </tr>
                        <tr>
                            <td class="heading center">
                                Market Share Report</td>
                        </tr>
                        <tr>
                            <td valign="top">
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td align="LEFT">
                                            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                <tr>
                                                    <td style="width: 845px;" class="redborder" valign="top">
                                                        <table border="0" cellpadding="2" cellspacing="1" style="width: 845px;" class="left">
                                                            <tr>
                                                                <td class="center" colspan="8" style="height: 25px;">
                                                                    <asp:Label ID="lblError" runat="server" CssClass="ErrorMsg"></asp:Label></td>
                                                                <td class="center" colspan="1" style="height: 25px">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 10%;">
                                                                </td>
                                                                <td class="textbold" style="width: 10%;">
                                                                </td>
                                                                <td style="width: 14%;" class="textbold">
                                                                    Month</td>
                                                                <td class="textbold" style="width: 15%;">
                                                                    <asp:DropDownList ID="drpMonthFrom" runat="server" CssClass="dropdownlist" TabIndex="1"
                                                                        Width="96px">
                                                                        <asp:ListItem Value="1">January</asp:ListItem>
                                                                        <asp:ListItem Value="2">February</asp:ListItem>
                                                                        <asp:ListItem Value="3">March</asp:ListItem>
                                                                        <asp:ListItem Value="4">April</asp:ListItem>
                                                                        <asp:ListItem Value="5">May</asp:ListItem>
                                                                        <asp:ListItem Value="6">June</asp:ListItem>
                                                                        <asp:ListItem Value="7">July</asp:ListItem>
                                                                        <asp:ListItem Value="8">August</asp:ListItem>
                                                                        <asp:ListItem Value="9">September</asp:ListItem>
                                                                        <asp:ListItem Value="10">October</asp:ListItem>
                                                                        <asp:ListItem Value="11">November</asp:ListItem>
                                                                        <asp:ListItem Value="12">December</asp:ListItem>
                                                                    </asp:DropDownList></td>
                                                                <td class="textbold" style="width: 10%;">
                                                                    Year</td>
                                                                <td class="textbold" style="width: 15%;">
                                                                    <asp:DropDownList ID="drpYearFrom" runat="server" CssClass="dropdownlist" TabIndex="1"
                                                                        Width="96px">
                                                                    </asp:DropDownList></td>
                                                                <td class="left" colspan="1" style="width: 10%">
                                                                </td>
                                                                <td class="left" style="width: 25%" colspan="1">
                                                                    <asp:Button ID="btnExport" runat="server" CssClass="button" TabIndex="2" Text="Export"
                                                                        AccessKey="E" Visible="true" /></td>
                                                                <td class="left" colspan="1" style="width: 25%">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 10%">
                                                                </td>
                                                                <td class="textbold" style="width: 10%">
                                                                </td>
                                                                <td class="textbold" style="width: 10%">
                                                                    Company Vertical</td>
                                                                <td class="textbold" style="width: 15%">
                                                                    <asp:DropDownList ID="DlstCompVertical" runat="server" CssClass="dropdownlist" onkeyup="gotop(this.id)"
                                                                        TabIndex="1" Width="96px">
                                                                    </asp:DropDownList></td>
                                                                <td class="textbold" colspan="2" style="width: 10%">
                                                                    <asp:CheckBox ID="chkTBA" runat="server" Text="Include TBA" /></td>
                                                                <td class="textbold" style="width: 15%">
                                                                </td>
                                                                <td class="left" colspan="1" style="width: 10%">
                                                                </td>
                                                                <td class="left" colspan="1" style="width: 25%">
                                                                </td>
                                                                <td class="left" colspan="1" style="width: 25%">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 10%">
                                                                </td>
                                                                <td class="textbold" style="width: 10%">
                                                                </td>
                                                                <td class="textbold" colspan="2" >
                                                                    </td>
                                                                <td class="textbold" style="width: 10%">
                                                                </td>
                                                                <td class="textbold" style="width: 15%">
                                                                </td>
                                                                <td class="left" colspan="1" style="width: 10%">
                                                                </td>
                                                                <td class="left" colspan="1" style="width: 25%">
                                                                </td>
                                                                <td class="left" colspan="1" style="width: 25%">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="8" class="textbold">
                                                                    &nbsp;</td>
                                                                <td class="textbold" colspan="1">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                </td>
                                                                <td>
                                                                </td>
                                                                <td class="subheading" align="center" colspan="4">
                                                                    Summary option</td>
                                                                <td>
                                                                </td>
                                                                <td>
                                                                </td>
                                                                <td>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 3%;">
                                                                </td>
                                                                <td>
                                                                </td>
                                                                <td class="textbold" valign="top" colspan="4" align="center">
                                                                    <table cellpadding="0" cellspacing="0">
                                                                        <tr>
                                                                            <td>
                                                                                <asp:RadioButtonList ID="rdSummOpt" runat="server" CssClass="textbox" RepeatDirection="Horizontal"
                                                                                    Width="350px" TabIndex="1" >
                                                                                    <asp:ListItem Value="1">City</asp:ListItem>
                                                                                    <asp:ListItem Selected="True" Value="2">Country</asp:ListItem>
                                                                                    <asp:ListItem Value="3">Region</asp:ListItem>
                                                                                </asp:RadioButtonList></td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                                <td class="center" style="width: 10%">
                                                                </td>
                                                                <td class="center" style="width: 10%;">
                                                                </td>
                                                                <td class="center" style="width: 10%">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                </td>
                                                                <td colspan="6" align="center" valign ="top" >
                                                                    <table id="TblProductivity"  class ="displayNone" runat ="server"  >
                                                                        <tr>
                                                                            <td class="textbold" style="width: 13%" valign="top">Productivity</td>
                                                                            <td style="width: 20%"><asp:DropDownList ID="drpProductivity" runat="server" CssClass="dropdownlist" Width="165px"
                                                                                    TabIndex="1">
                                                                                    <asp:ListItem Selected="True">--All--</asp:ListItem>
                                                                                    <asp:ListItem Value="1">Equal To</asp:ListItem>
                                                                                    <asp:ListItem Value="2">Greater Than</asp:ListItem>
                                                                                    <asp:ListItem Value="3">Greater Than Equal To</asp:ListItem>
                                                                                    <asp:ListItem Value="4">Less Than</asp:ListItem>
                                                                                    <asp:ListItem Value="5">Less Than Equal To</asp:ListItem>
                                                                                    <asp:ListItem Value="6">Not Equal To</asp:ListItem>
                                                                                    <asp:ListItem Value="7">Between</asp:ListItem>
                                                                                </asp:DropDownList></td>
                                                                            <td style="width: 2%" valign="top">
                                                                            </td>
                                                                            <td style="width: 20%" valign="top"><asp:TextBox ID="txtFrom" runat="server" CssClass="textboxgrey" MaxLength="9" TabIndex="1"
                                                                                    Width="120px" Enabled="False">0</asp:TextBox></td>
                                                                            <td style="width: 23%" valign="top"><asp:TextBox ID="txtTo" runat="server" CssClass="textboxgrey" MaxLength="9" TabIndex="1"
                                                                                    Width="120px" Enabled="False">0</asp:TextBox></td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                                <td class="textbold" colspan="1">
                                                                </td>
                                                                <td colspan="1" class="textbold">
                                                                </td>
                                                                <td class="textbold" colspan="1">
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

<script type="text/javascript" language="javascript">
         function gotops(ddlname)
         {    
             if (event.keyCode==46 )
             {
                document.getElementById(ddlname).selectedIndex=0;
                SelectProdutivity();
             }
         } 
         function HideUndideProductivity()
         {
            //  alert(document.getElementById('rdSummOpt_0').checked);
             if (document.getElementById('rdSummOpt_0').checked==true)
            {
                 document.getElementById('TblProductivity').className ="displayBlock";                 
            }
            else
            {
                 var objtxtFrom =document.getElementById('txtFrom');
                 var objtxtTo =document.getElementById('txtTo');   
                 objtxtFrom.value="0";  
                 objtxtTo.value="0";                           
                 document.getElementById('TblProductivity').className ="displayNone";                 
            }
            return true;
         }
         
         HideUndideProductivity();
             
</script>

</html>
