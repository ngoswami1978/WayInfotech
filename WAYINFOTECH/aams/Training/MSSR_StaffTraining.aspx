<%@ Page Language="VB" AutoEventWireup="false" CodeFile="MSSR_StaffTraining.aspx.vb" Inherits="Training_MSSR_StaffTraining" %>

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
    <title>AAMS::Training::Search Staff Training</title>
    <script language="javascript" type="text/javascript">
         
              
 
     function PopupPage(id)
         {
         var type;
         if (id=="1")
         {
              type = "../TravelAgency/TASR_Agency.aspx?Popup=T" ;
   	            window.open(type,"aaStaffTrainingAgency","height=600,width=900,top=30,left=20,scrollbars=1,status=1");	    
          }
         if (id=="2")
         {
              if (document.getElementById("txtAgency").value!="")
              {
              
              var strAgencyName=document.getElementById("txtAgency").value;
              strAgencyName=strAgencyName.replace("&","%26")
                   type = "../TravelAgency/TASR_AgencyStaff.aspx?Popup=T&AgencyName="+strAgencyName ;
   	                window.open(type,"aaTrainingStaff","height=600,width=900,top=30,left=20,scrollbars=1,status=1");	 
   	           }
   	           else
   	           {
   	             document.getElementById('<%=lblError.ClientId%>').innerText='Agency is mandatory.'
                 document.getElementById('<%=txtAgency.ClientId%>').focus();
                 return false;
   	           
   	           }
         }
    
        
           
     }
    
  
    
    
    </script>
</head>
<body>
    <form id="form1" runat="server" defaultbutton="btnSearch" defaultfocus="txtAgency">
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
                            <span class="menu"> Training -&gt;</span><span class="sub_menu">Staff Training Search</span></td>
                        </tr>
                        <tr>
                            <td class="heading center" >
                                Search Staff Training</td>
                        </tr>
                        <tr>
                            <td >
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td class="redborder center">
                                    <table border="0" cellpadding="2" cellspacing="1" style="width: 100%" class="left">
                                                                        <tr>
                                                                            <td class="center gap" colspan="6"  >
                                                                <asp:Label ID="lblError" runat="server" CssClass="ErrorMsg" EnableViewState="False"></asp:Label></td>
                                                                        </tr>
                                        <tr>
                                            <td>
                                            </td>
                                            <td class="textbold">
                                                Agency Name <span class="Mandatory"> *</span></td>
                                            <td colspan="3">
                                                <asp:TextBox ID="txtAgency" runat="server" CssClass="textboxgrey" MaxLength="50" TabIndex="2"
                                                    Width="530px" ReadOnly="True"></asp:TextBox>
                                                <img id="Img2" runat="server" alt="Select & Add Agency Name" onclick="PopupPage(1)"
                                                    src="../Images/lookup.gif" style="cursor:pointer;" /></td>
                                            <td class="left" style="width: 12%">
                                            <asp:Button ID="btnSearch" CssClass="button" runat="server" Text="Display" TabIndex="3" AccessKey="d" /></td>
                                        </tr>
                                        <tr>
                                            <td>
                                                </td>
                                            <td class="textbold"> Agency Staff</td>                                                                               
                                            <td colspan="3">
                                                <asp:TextBox ID="txtAgencyStaff" runat="server" CssClass="textbox" 
                                                    TabIndex="2" Width="530px" MaxLength="50"></asp:TextBox>
                                                <img id="Img3" runat="server" alt="Select & Add Agency Staff" onclick="PopupPage(2)"
                                                    src="../Images/lookup.gif" style="cursor:pointer;" /></td>
                                            <td style="width: 12%;" class="left">
                                               <asp:Button ID="btnReset" CssClass="button" runat="server" Text="Reset" TabIndex="3" AccessKey="r" /></td>
                                        </tr>
                                          <tr>
                                            <td>
                                            </td>
                                            <td class="textbold">
                                                Course</td>
                                            <td colspan="3">
                                             <asp:DropDownList ID="ddlCourse" runat="server" CssClass="dropdownlist" Width="536px" TabIndex="2" onkeyup="gotop(this.id)">
                                                   
                                             </asp:DropDownList></td>
                                            <td class="left">
                                               </td>
                                        </tr>
                                      
                                          <tr>
                                            <td>
                                            </td>
                                            <td class="textbold">
                                                Start Date From
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtStartDateFrom" runat="server" CssClass="textbox" Width="170px" TabIndex="2" MaxLength="10"></asp:TextBox>
                                                <img id="imgStartDateFrom" alt="" tabindex="2" src="../Images/calender.gif"  title="Date selector" style="cursor: pointer" />
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
                                                Start Date To
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtStartDateTo" runat="server" CssClass="textbox" Width="170px" TabIndex="2" MaxLength="10"></asp:TextBox>
                                                <img id="imgStartDateTo" alt="" tabindex="2" src="../Images/calender.gif"  title="Date selector" style="cursor: pointer" />
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
                                               </td>
                                        </tr>
                                        <tr>
                                            <td>
                                            </td>
                                            <td class="textbold">
                                                Status</td>
                                            <td>
                                                <asp:DropDownList ID="ddlStatus" runat="server" CssClass="dropdownlist" onkeyup="gotop(this.id)"
                                                    Width="176px">
                                                </asp:DropDownList></td>
                                            <td class="textbold">
                                            </td>
                                            <td>
                                            </td>
                                            <td class="left">
                                            </td>
                                        </tr>
                                          <tr>
                                            <td>  </td> <td class="textbold" >   Group By</td>    <td>
                                                <asp:RadioButtonList ID="RadioButtonList1" runat="server" CellPadding="1" CellSpacing="1"
                                                    RepeatDirection="Horizontal" CssClass="dropdownlist" TabIndex="2">
                                                    <asp:ListItem Selected="True">People</asp:ListItem>
                                                    <asp:ListItem>Session</asp:ListItem>
                                                </asp:RadioButtonList></td><td class="textbold" > </td><td>
                                                </td>
                                            <td>
                                            </td>
                                        </tr>                           
                                        <tr>
                                            <td ></td>
                                            <td >       </td>    <td  colspan="4">
                                            
                                                <input id="hdDeleteId" runat="server" style="width: 1px" type="hidden" enableviewstate="true"/>
                                                <asp:HiddenField ID="hdCourseLCode" runat ="server" />
                                                <asp:HiddenField ID="hdCourseStaff" runat ="server" />
                                                 
                                                </td>
                                        </tr>
                                                                        <tr>
                                                                            <td colspan="6" class="ErrorMsg" >
                                                                                Field Marked * are Mandatory</td>
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
      
      
     if(document.getElementById('<%=txtAgency.ClientId%>').value == '')
        {
             document.getElementById('<%=lblError.ClientId%>').innerText='Agency is mandatory.'
             document.getElementById('<%=txtAgency.ClientId%>').focus();
             return false;
        }
        //      Checking txtStartDateFrom .
        if(document.getElementById('<%=txtStartDateFrom.ClientId%>').value != '')
        {
            if (isDate(document.getElementById('<%=txtStartDateFrom.ClientId%>').value,"d/M/yyyy") == false)	
            {
               document.getElementById('<%=lblError.ClientId%>').innerText = "Start date from is not valid.";			
	           document.getElementById('<%=txtStartDateFrom.ClientId%>').focus();
	           return(false);  
            }
        } 
         //      Checking txtStartDateTo .
        if(document.getElementById('<%=txtStartDateTo.ClientId%>').value != '')
        {
            if (isDate(document.getElementById('<%=txtStartDateTo.ClientId%>').value,"d/M/yyyy") == false)	
            {
               document.getElementById('<%=lblError.ClientId%>').innerText = "Start date to is not valid.";			
	           document.getElementById('<%=txtStartDateTo.ClientId%>').focus();
	           return(false);  
            }
        } 
         
    
       
        //    compareDates(dateOpen,dateformat1,dateAssignee,dateformat2) {
        
        if (document.getElementById('<%=txtStartDateFrom.ClientId%>').value != '' && document.getElementById('<%=txtStartDateTo.ClientId%>').value != '')
        {
        
           if (compareDates(document.getElementById('<%=txtStartDateFrom.ClientId%>').value,"d/M/yyyy",document.getElementById('<%=txtStartDateTo.ClientId%>').value,"d/M/yyyy")==1)
               {
                    document.getElementById('<%=lblError.ClientId%>').innerText ='Start date to should be greater than or equal to start date from.'
                    return false;
               }
       }
       return true; 
        
    
    
    }
    
   
 
    </script>
</body>
</html>
