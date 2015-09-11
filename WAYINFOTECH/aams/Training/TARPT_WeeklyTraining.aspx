<%@ Page Language="VB" AutoEventWireup="false" CodeFile="TARPT_WeeklyTraining.aspx.vb" Inherits="Training_TARPT_WeeklyTraining" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" media="all" href="../Calender/calendar-blue.css" title="win2k-cold-1" />
<script type="text/javascript" src="../Calender/calendar.js"></script>
<script type="text/javascript" src="../JavaScript/AAMS.js" ></script>

<!-- import the language module -->

<script type="text/javascript" src="../Calender/lang/calendar-en.js"></script>
<script type="text/javascript" src="../Calender/calendar-setup.js"></script>
    <title>AAMS::Training::Weekly</title>
    <script language="javascript" type="text/javascript">
         
    
    </script>
</head>
<body>
    <form id="form1" runat="server" defaultfocus="ddlCourse" defaultbutton="btnSearch">
    <div>
     <table>
    <tr>
    <td>
    <table width="860px"  class="border_rightred left">
            <tr>
                <td class="top">
                    <table width="100%" class="left">
                        <tr>
                            <td   >
                            <span class="menu"> Training -&gt;</span><span class="sub_menu">Weekly Training&nbsp;</span></td>
                        </tr>
                        <tr>
                            <td class="heading center" >
                                Weekly Training</td>
                        </tr>
                        <tr>
                            <td >
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td class="redborder center" style="height: 30px">
                                    <table border="0" cellpadding="2" cellspacing="1" style="width: 100%; height: 47px;" class="left">
                                                                        <tr>
                                                                            <td class="center gap" colspan="6"  >
                                                                <asp:Label ID="lblError" runat="server" CssClass="ErrorMsg" EnableViewState="False"></asp:Label></td>
                                                                        </tr>
                                        <tr>
                                            
                                      
                                            
                                        </tr>
                                        <tr>
                                         
                                                                                       
                                            
                                        </tr>
                                        <tr>
                                            <td style="width: 9px">
                                            </td>
                                            <td class="textbold" style="width: 155px">
                                                Aoffice</td>
                                            <td>
                                                <asp:DropDownList ID="ddlAoffice" runat="server" CssClass="dropdownlist" Width="176px" TabIndex="2" onkeyup="gotop(this.id)">
                                                   
                                                </asp:DropDownList></td>
                                            <td class="textbold">
                                                Region</td>
                                            <td style="width: 273px">
                                                <asp:DropDownList ID="ddlReagion" runat="server" CssClass="dropdownlist" Width="176px" TabIndex="2" onkeyup="gotop(this.id)">
                                                 
                                                </asp:DropDownList></td>
                                            <td class="left">
                                                <asp:Button ID="btnSearch" runat="server" CssClass="button" TabIndex="3" Text="Display" AccessKey="d" /></td>
                                        </tr>
                                      
                                          <tr>
                                            <td style="width: 9px">
                                            </td>
                                            <td class="textbold" style="width: 155px"> 
                                            <asp:DropDownList ID="ddlDateType" runat="server" CssClass="dropdownlist" onkeyup="gotop(this.id)"
                                                    TabIndex="2" Width="90px">
                                                    <asp:ListItem Text="" Value="0"></asp:ListItem>
                                                    <asp:ListItem Text="Start Date" Value="1"></asp:ListItem>
                                                    <asp:ListItem Text="End Date" Value="2"></asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td class="textbold">
                                                From &nbsp;
                                                <asp:TextBox ID="txtStartDateFrom" runat="server" CssClass="textbox" Width="132px" TabIndex="2" MaxLength="10"></asp:TextBox>
                                                <img id="imgStartDateFrom" alt=""  src="../Images/calender.gif"  title="Date selector" style="cursor: pointer" />
                                                <script type="text/javascript">
                                                  Calendar.setup({
                                                  inputField     :    '<%=txtStartDateFrom.clientId%>',
                                                  ifFormat       :    "%d/%m/%Y",
                                                  button         :    "imgStartDateFrom",
                                                 //align          :    "Tl",
                                                  singleClick    :    true
                                                 
                                                               });
                                            </script></td>
                                            <td class="textbold">
                                                To
                                            </td>
                                            <td style="width: 273px">
                                                <asp:TextBox ID="txtStartDateTo" runat="server" CssClass="textbox" Width="132px" TabIndex="2" MaxLength="10"></asp:TextBox>
                                                <img id="imgStartDateTo" alt="" src="../Images/calender.gif"  title="Date selector" style="cursor: pointer" />
                                                <script type="text/javascript">
                                                  Calendar.setup({
                                                  inputField     :    '<%=txtStartDateTo.clientId%>',
                                                  ifFormat       :    "%d/%m/%Y",
                                                  button         :    "imgStartDateTo",
                                                 //align          :    "Tl",
                                                  singleClick    :    true
                                                
                                                               });
                                            </script>
                                                </td>
                                            <td class="left">
                                                <asp:Button ID="btnReset" runat="server" CssClass="button" TabIndex="3" Text="Reset" AccessKey="r" /></td>
                                        </tr>
                                        <tr>
                                            <td style="width: 9px">
                                            </td>
                                            <td class="textbold" style="width: 155px">
                                                &nbsp;</td>
                                            <td>
                                                &nbsp;
                                            </td>
                                            <td class="textbold">
                                            </td>
                                            <td style="width: 273px">
                                            </td>
                                            <td class="left">
                                            </td>
                                        </tr>
                                                                        <tr>
                                                                            <td colspan="6" class="ErrorMsg" >
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
    <tr><td class="top border_rightred">
</td></tr>
</table>

    </div>
    </form>
    
     <script language="javascript" type="text/javascript">
    function ValidateForm()
    {
      document.getElementById('<%=lblError.ClientId%>').innerText=''
      // Checking Agency 
      
      
    
        //      Checking txtStartDateFrom .
        if(document.getElementById('<%=txtStartDateFrom.ClientId%>').value != '')
        {
        if (isDate(document.getElementById('<%=txtStartDateFrom.ClientId%>').value,"d/M/yyyy") == false)	
        {
           document.getElementById('<%=lblError.ClientId%>').innerText = "Date from is not valid.";			
	       document.getElementById('<%=txtStartDateFrom.ClientId%>').focus();
	       return(false);  
        }
        } 
         //      Checking txtStartDateTo .
        if(document.getElementById('<%=txtStartDateTo.ClientId%>').value != '')
        {
        if (isDate(document.getElementById('<%=txtStartDateTo.ClientId%>').value,"d/M/yyyy") == false)	
        {
           document.getElementById('<%=lblError.ClientId%>').innerText = "Date to is not valid.";			
	       document.getElementById('<%=txtStartDateTo.ClientId%>').focus();
	       return(false);  
        }
        } 
         
        
   
       
        //    compareDates(dateOpen,dateformat1,dateAssignee,dateformat2) {
       
       if (document.getElementById('<%=txtStartDateFrom.ClientId%>').value != '' && document.getElementById('<%=txtStartDateTo.ClientId%>').value != '')
        {  
   if (compareDates(document.getElementById('<%=txtStartDateFrom.ClientId%>').value,"d/M/yyyy",document.getElementById('<%=txtStartDateTo.ClientId%>').value,"d/M/yyyy")==1)
       {
            document.getElementById('<%=lblError.ClientId%>').innerText ='Date to should be greater than or equal to Date from.'
            return false;
       }
       }
       
       
       
        
         
     
         //****************************************************************
          
        
           
       return true; 
        
    
    
    }
    
   
 
    </script>
</body>
</html>
