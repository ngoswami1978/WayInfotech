<%@ Page Language="VB" AutoEventWireup="false" CodeFile="SAUP_FollowupPlanDayCalender.aspx.vb"
    Inherits="Sales_SAUP_FollowupPlanDayCalender" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />
    <title>AAMS::Sales::Visit Plan</title>

    <script src="../JavaScript/AAMS.js" type="text/javascript"></script>

    <link rel="stylesheet" type="text/css" media="all" href="../Calender/calendar-blue.css"
        title="win2k-cold-1" />

    <script type="text/javascript" src="../Calender/calendar.js"></script>

    <!-- import the language module -->

    <script type="text/javascript" src="../Calender/lang/calendar-en.js"></script>

    <!-- import the calendar setup module -->

    <script type="text/javascript" src="../Calender/calendar-setup.js"></script>

    <script type="text/javascript">
    function HideParentImage()
    {
//         window.parent.document.getElementById('PnlCalenderImagePnl').className   ="displayNone";
//         window.parent.document.getElementById('CalenderImage').className  ="displayNone";
//         window.parent.document.getElementById('iframeID').height="480px";
    }
    
    function CalenderUpdation(str)
    {
     
        document.getElementById('HdSelectedDay').value=str;       
        $get("<%=btnFake.ClientID %>").click();  
          return false;
    }
    function CloseCalander()
    {
         window.close();
//       window.parent.document.getElementById('iframeID').src='';
//       window.parent.document.getElementById('BtnCancel').click();
         
    }
  
    </script>

    <style type="text/css">
       
       .Weekend
       { 
         background: #c1c1c1; 
       } 
        .Title
       { 
         
         
         color: #fff;
	     background:#1A61A9;
       } 
       
     .DayHeader
     {
        font-family: Verdana; 
        font-weight: bold; 
        font-size: 12px; 
        color: #000033;
      
     }


       table#ClnPlanDay  
        {
        font-family:"verdana";
        width: 384px;         
        background-color: #dee7f7;
        border: 1px #000000 solid; 
        border-collapse: collapse; 
        border-spacing: 0px; } 
         
        table#ClnPlanDay td 
        {
        font-family:"verdana";
        border: 1px #000000 solid; 
        font-family: Verdana; 
        font-weight: bold; 
        font-size: 12px; 
        
        color: #fff;
        height:20px;
         }
         
         table#ClnPlanDay td a
         {
            font-family:"verdana";
            font-family: Verdana; 
            font-weight: normal; 
            font-size: 12px;        
            color: #fff;
            height:20px;
            text-decoration:none
         } 
         
         
       .PlannedDays
       {
        background: #1861ad; 
       }
       
         .VisitedDay
       {
        background-color: #1861ad; 
       }
       
        .UnVisitedDay
       {
        background-color: #1861ad; 
       }
       
          .UnPlannedVisitedDay
       {
        background-color: #1861ad; 
       }
       
      .DefaultCellcolor
      {
          background-color: #dee7f7;
      }   
      
       .OtherMontDayCellcolor
      {
          background-color: #ffffff;
      }   
.modalCloseButton	{
	    background-image:url(../Images/strip_tab.jpg);
	    background-repeat:repeat-x;
	    background-color:#f9f9f9;	
	    font-family:Verdana;
	    font-size:10px;
	    color:#0457b7;
	    border-top:1px solid #0457b7;
	    border-bottom:2px solid #0457b7;
	    border-left:1px solid #0457b7;
	    border-right:1px solid #0457b7;
	    text-align:center;
	    vertical-align:middle;
	    text-decoration:none;
	    cursor:pointer ;
    }
        </style>
</head>
<body onload="HideParentImage();">
    <form id="form1" runat="server">
        <table width="620px" align="left" class="border_rightred">
            <tr>
                <td valign="top" style="width: 100%">
                    <asp:ScriptManager ID="Sc1" runat="server" AsyncPostBackTimeout="800">
                    </asp:ScriptManager>
                    <table width="100%" class="left">
                        <tr>
                            <td valign="top" align="right">
                                <input type="button" class="modalCloseButton" runat="server" value="Close" id="BtnColose"
                                    onclick="CloseCalander();" />
                            </td>
                        </tr>
                        <tr>
                            <td class="heading center">
                                Visit Plan</td>
                        </tr>
                        <tr>
                            <td valign="top">
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td align="LEFT">
                                            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                <tr>
                                                    <td style="width: 100%" class="redborder" valign="top">
                                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                            <ContentTemplate>
                                                                <table border="0" cellpadding="2" cellspacing="1" class="left" width="100%">
                                                                    <tr>
                                                                        <td class="center" colspan="7">
                                                                            <asp:Label ID="lblError" runat="server" CssClass="ErrorMsg"></asp:Label></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="width: 10px">
                                                                        </td>
                                                                        <td class="subheading" colspan="6">
                                                                            Agency Details</td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="width: 10px">
                                                                            <input id="Hidden1" runat="server" style="width: 1px" type="hidden" /></td>
                                                                        <td class="textbold" style="width: 80px">
                                                                            Agency Name</td>
                                                                        <td colspan="4">
                                                                            <asp:TextBox ID="txtAgencyName" runat="server" CssClass="textboxgrey" MaxLength="50"
                                                                                TabIndex="3" Width="380px" ReadOnly="True"></asp:TextBox>
                                                                        </td>
                                                                        <td align="center">
                                                                            <asp:Button AccessKey="r" ID="BtnSave" TabIndex="5" runat="server" CssClass="button"
                                                                                Text="Save" />
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="width: 10px">
                                                                        </td>
                                                                        <td class="textbold">
                                                                            Address</td>
                                                                        <td colspan="4" valign="top">
                                                                            <asp:TextBox ID="txtAdd" runat="server" CssClass="textboxgrey" Height="41px" MaxLength="50"
                                                                                ReadOnly="True" TabIndex="3" TextMode="MultiLine" Width="380px"></asp:TextBox>
                                                                        </td>
                                                                        <td valign="top" align="center">
                                                                            <asp:Button AccessKey="r" ID="btnReset" TabIndex="5" runat="server" CssClass="button"
                                                                                Text="Reset"></asp:Button>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="height: 10px">
                                                                        </td>
                                                                        <td class="textbold" style="height: 26px" align="left">
                                                                            City</td>
                                                                        <td style="height: 26px" align="left">
                                                                            <asp:TextBox ID="txtCity" runat="server" CssClass="textboxgrey" MaxLength="50" ReadOnly="True"
                                                                                TabIndex="3" Width="145px"></asp:TextBox></td>
                                                                        <td class="textbold" style="height: 26px" colspan="2" align="left">
                                                                            Country</td>
                                                                        <td class="textbold" style="height: 26px" align="left">
                                                                            <asp:TextBox ID="txtCountry" runat="server" CssClass="textboxgrey" MaxLength="50"
                                                                                ReadOnly="True" TabIndex="3" Width="145px"></asp:TextBox></td>
                                                                        <td style="height: 26px">
                                                                            <asp:Button AccessKey="c" ID="BtnClear" TabIndex="5" runat="server" CssClass="button"
                                                                                Text="Clear" Visible="False" /></td>
                                                                    </tr>
                                                                    <tr style="display: none;">
                                                                        <td>
                                                                        </td>
                                                                        <td class="textbold" align="left">
                                                                            Month</td>
                                                                        <td align="left">
                                                                            <asp:TextBox ID="TxtMonth" runat="server" CssClass="textboxgrey" MaxLength="50" ReadOnly="True"
                                                                                TabIndex="3" Width="150px"></asp:TextBox></td>
                                                                        <td class="textbold" colspan="2" align="left">
                                                                            Year</td>
                                                                        <td class="textbold" align="left">
                                                                            <asp:TextBox ID="TxtYear" runat="server" CssClass="textboxgrey" MaxLength="50" ReadOnly="True"
                                                                                TabIndex="3" Width="150px"></asp:TextBox></td>
                                                                        <td>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td colspan="7">
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                        </td>
                                                                        <td class="textbold">
                                                                        </td>
                                                                        <td colspan="4" align="left">
                                                                            <asp:UpdatePanel ID="UpdPnl" runat="server">
                                                                                <ContentTemplate>
                                                                                    <asp:Label ID="LblCalenderError" runat="server" CssClass="ErrorMsg"></asp:Label>
                                                                                    <asp:Calendar ID="ClnPlanDay" runat="server" SelectionMode="Day" TitleStyle-CssClass="Title"
                                                                                        SelectorStyle-Font-Bold="True" Font-Size="12" ShowNextPrevMonth="true" NextPrevFormat="FullMonth"  NextPrevStyle-ForeColor="white"  DayHeaderStyle-Font-Underline="false"
                                                                                        DayHeaderStyle-CssClass="DayHeader" SelectedDayStyle-Font-Bold="false"></asp:Calendar>
                                                                                    <asp:Button ID="BtnFake" runat="server" Style="display: none;" />
                                                                                    <asp:HiddenField ID="HdSelectedDay" runat="server" />
                                                                                    <asp:HiddenField ID="hdMonth" runat="server" />
                                                                                    <asp:HiddenField ID="hdYear" runat="server" />
                                                                                    <asp:HiddenField ID="HdUserDefinedVisit" runat="server" />
                                                                                    <asp:HiddenField ID="HdMaxVisit" runat="server" />
                                                                                    <asp:HiddenField ID="HdLCode" runat="server" />
                                                                                    <asp:HiddenField ID="HdUserDefinedVisitDays" runat="server" />
                                                                                </ContentTemplate>
                                                                            </asp:UpdatePanel>
                                                                        </td>
                                                                        <td>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                        </td>
                                                                        <td class="textbold">
                                                                        </td>
                                                                        <td colspan="4" align="center">
                                                                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                                  <tr>
                                                                            <td class="textbold" align="left">
                                                                                <asp:Label ID="LblPlanDay" runat="server" BackColor="Blue" Text="&nbsp;&nbsp;&nbsp;"
                                                                                    Width="15px" Height="15px"></asp:Label>&nbsp;Planned
                                                                            </td>
                                                                            <td class="textbold" align="left">
                                                                                <asp:Label ID="LblPlanDayVisited" runat="server" BackColor="Green" Text="&nbsp;&nbsp;&nbsp;"
                                                                                    Width="15px" Height="15px"></asp:Label>&nbsp;Planned & Visited
                                                                            </td>
                                                                            <td class="textbold" align="left">
                                                                                <asp:Label ID="LblUnPlanVisited" runat="server" BackColor="Yellow" Text="&nbsp;&nbsp;&nbsp;"
                                                                                    Width="15px" Height="15px"></asp:Label>&nbsp;Unplanned Visited
                                                                            </td>
                                                                            <td class="textbold" align="left">
                                                                                <asp:Label ID="LblPlanNotVisited" runat="server" BackColor="Red" Text="&nbsp;&nbsp;&nbsp;"
                                                                                    Width="15px" Height="15px"></asp:Label>&nbsp;Not Visited
                                                                            </td>
                                                                        </tr>
                                                                        <tr >
                                                                            <td class="textbold" align="left">
                                                                                <asp:Label ID="Label1" runat="server" BackColor="Purple" Text="&nbsp;&nbsp;&nbsp;"
                                                                                    Width="15px" Height="15px"></asp:Label>&nbsp;Backdated DSR Logged
                                                                            </td>
                                                                            <td class="textbold" align="left">
                                                                                <asp:Label ID="Label2" runat="server" BackColor="DarkSalmon" Text="&nbsp;&nbsp;&nbsp;"
                                                                                    Width="15px" Height="15px"></asp:Label>&nbsp;Planned Call By Manager
                                                                            </td>
                                                                            <td class="textbold" colspan ="2" align="left">
                                                                                <asp:Label ID="Label3" runat="server" BackColor="Fuchsia" Text="&nbsp;&nbsp;&nbsp;"
                                                                                    Width="15px" Height="15px"></asp:Label>&nbsp;Planned Visit Not Logged After 5 days
                                                                            </td>                                                                            
                                                                        </tr>
                                                                            </table>
                                                                        </td>
                                                                        <td>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
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
