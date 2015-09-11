<%@ Page Language="VB" AutoEventWireup="false" CodeFile="TRRPT_1AStaffTraining.aspx.vb" Inherits="Training_TRRPT_1AStaffTraining" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" media="all" href="../Calender/calendar-blue.css" title="win2k-cold-1" />
<script type="text/javascript" src="../Calender/calendar.js"></script>
<script type="text/javascript" src="../JavaScript/AAMS.js"></script>

<!-- import the language module -->

<script type="text/javascript" src="../Calender/lang/calendar-en.js"></script>
<script type="text/javascript" src="../Calender/calendar-setup.js"></script>
    <title>AAMS::Training:: 1A Staff Training Report</title>
    <script language="javascript" type="text/javascript">
     
     function PopupPage(id)
         {
         var type;
         if (id=="1")
         {
              type = "../TravelAgency/TASR_Agency.aspx?Popup=T" ;
   	            window.open(type,"aaTrainingAgency","height=600,width=900,top=30,left=20,scrollbars=1,status=1");	    
          }
         if (id=="2")
         {
          var strAgencyName=document.getElementById("txtAgency").value;
          strAgencyName=strAgencyName.replace("&","%26")
               type = "../TravelAgency/TASR_AgencyStaff.aspx?Popup=T&AgencyName="+strAgencyName ;
   	            window.open(type,"aaTrainingStaff","height=600,width=900,top=30,left=20,scrollbars=1,status=1");	 
         }
    
         if (id=="3")
         {
               
               type = "../Training/TRSR_TrainingRooms.aspx?Popup=T";
   	            window.open(type,"aaTrainingRoom","height=600,width=900,top=30,left=20,scrollbars=1,status=1");           
          }
    
           
     }
    
  
    
    
    </script>
</head>
<body>
    <form id="form1" runat="server">
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
                            <span class="menu"> Training -&gt;</span><span class="sub_menu">1A Staff Training Report</span></td>
                        </tr>
                        <tr>
                            <td class="heading center" >
                                1A Staff Training Report</td>
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
                                                Agency</td>
                                            <td colspan="3">
                                                <asp:TextBox ID="txtAgency" runat="server" CssClass="textbox" MaxLength="50" TabIndex="2"
                                                    Width="520px"></asp:TextBox>
                                                <img id="Img2" runat="server" alt="Select & Add Agency Name" onclick="PopupPage(1)"
                                                    src="../Images/lookup.gif" style="cursor:pointer;" /></td>
                                            <td class="left" style="width: 12%">
                                            <asp:Button ID="btnSearch" CssClass="button" runat="server" Text="Display" TabIndex="3" AccessKey="d" /></td>
                                        </tr>
                                        <tr>
                                            <td>
                                                </td>
                                            <td class="textbold"> Course</td>                                                                               
                                            <td colspan="3">
                                             <asp:DropDownList ID="ddlCourse" runat="server" CssClass="dropdownlist" Width="527px" TabIndex="2" onkeyup="gotop(this.id)">
                                                   
                                             </asp:DropDownList>&nbsp;
                                            </td>
                                            <td style="width: 12%;" class="left">
                                               <asp:Button ID="btnReset" CssClass="button" runat="server" Text="Reset" TabIndex="3" AccessKey="r" /></td>
                                        </tr>
                                          <tr>
                                            <td>
                                            </td>
                                            <td class="textbold">
                                                Location</td>
                                            <td colspan="3">
                                                <asp:TextBox ID="txtTrainingRoom" runat="server" CssClass="textbox" MaxLength="50" TabIndex="2"
                                                    Width="521px"></asp:TextBox>
                                                <img id="Img1" runat="server" alt="Select & Add Training Room" onclick="PopupPage(3)"
                                                    src="../Images/lookup.gif" style="cursor:pointer;" /></td>
                                            <td class="left">
                                               </td>
                                        </tr>
                                         <tr>
                                            <td>
                                            </td>
                                            <td class="textbold">
                                                Agency Staff</td>
                                            <td colspan="3">
                                                <asp:TextBox ID="txtAgencyStaff" runat="server" CssClass="textbox" 
                                                    TabIndex="2" Width="522px" MaxLength="50"></asp:TextBox>
                                                <img id="Img3" runat="server" alt="Select & Add Agency Staff" onclick="PopupPage(2)"
                                                    src="../Images/lookup.gif" style="cursor:pointer;" /></td>
                                            <td class="left">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                            </td>
                                            <td class="textbold">
                                                Agency Type</td>
                                            <td>
                                                <asp:DropDownList ID="drpAgencyType" runat="server" CssClass="dropdownlist" onkeyup="gotop(this.id)"
                                                    Style="position: relative" TabIndex="2" Width="176px">
                                                </asp:DropDownList></td>
                                            <td class="textbold">
                                            </td>
                                            <td>
                                            </td>
                                            <td class="left">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                            </td>
                                            <td class="textbold">
                                                Trainer 1</td>
                                            <td><asp:TextBox ID="txtTrainer1" runat="server" CssClass="textbox" MaxLength="40" TabIndex="2"
                                                    Width="170px"></asp:TextBox>  <IMG style="CURSOR: pointer" id="Img4" onclick="PopupPageTrainerForTraining(1,'txtTrainer1')" 
alt="Select & Add Employee" src="../Images/lookup.gif" runat="server" />
                                                </td>
                                            <td class="textbold">
                                                Trainer 2</td>
                                            <td><asp:TextBox ID="txtTrainer2" runat="server" CssClass="textbox" MaxLength="40" TabIndex="2"
                                                    Width="170px" Wrap="False"></asp:TextBox>  <IMG style="CURSOR: pointer" id="Img5" onclick="PopupPageTrainerForTraining(1,'txtTrainer2')" 
alt="Select & Add Employee" src="../Images/lookup.gif" runat="server" />
                                                </td>
                                            <td class="left">
                                               </td>
                                        </tr>
                                      
                                        <tr>
                                            <td>
                                            </td>
                                            <td class="textbold">
                                                AOffice</td>
                                            <td>
                                                <asp:DropDownList ID="ddlAOffice" runat="server" CssClass="dropdownlist" Width="176px" TabIndex="2" onkeyup="gotop(this.id)">
                                                    
                                                   
                                                </asp:DropDownList></td>
                                            <td class="textbold">
                                                Region</td>
                                            <td>
                                                <asp:DropDownList ID="ddlRegion" runat="server" CssClass="dropdownlist" Width="176px" TabIndex="2" onkeyup="gotop(this.id)">
                                                   
                                                 
                                                </asp:DropDownList></td>
                                            <td class="left">
                                            </td>
                                        </tr>
                                      
                                          <tr>
                                            <td>
                                            </td>
                                            <td class="textbold">
                                                &nbsp;<asp:DropDownList ID="ddlDateType" runat="server" CssClass="dropdownlist" onkeyup="gotop(this.id)"
                                                    TabIndex="2" Width="90px">
                                                    <asp:ListItem Value="0" Text=""></asp:ListItem>
                                                    <asp:ListItem Text="Start Date" Value="1"></asp:ListItem>
                                                    <asp:ListItem Text="End Date" Value="2"></asp:ListItem>
                                                </asp:DropDownList></td>
                                            <td class="textbold">
                                                From&nbsp;
                                                <asp:TextBox ID="txtStartDateFrom" runat="server" CssClass="textbox" Width="132px" TabIndex="2" MaxLength="10"></asp:TextBox>
                                                <img id="imgStartDateFrom" alt="" TabIndex="2" src="../Images/calender.gif"  title="Date selector" style="cursor: pointer" />
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
                                            <td>
                                                <asp:TextBox ID="txtStartDateTo" runat="server" CssClass="textbox" Width="167px" TabIndex="2" MaxLength="10"></asp:TextBox>
                                                <img id="imgStartDateTo" alt="" TabIndex="2" src="../Images/calender.gif"  title="Date selector" style="cursor: pointer" />
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
                                            <td class="left"><input type="hidden" id="hdEmployeePageName" style="width:1px" runat='server' />
                                               </td>
                                        </tr>
                                        <tr>
                                            <td ></td>
                                            <td >       </td>    <td  colspan="4">
                                            
                                                <input id="hdDeleteId" runat="server" style="width: 1px" type="hidden" enableviewstate="true"/>
                                                <asp:HiddenField ID="hdCourseLCode" runat ="server" />
                                                <asp:HiddenField ID="hdRoomID" runat ="server" />
                                                <asp:HiddenField ID="hdCourseStaff" runat ="server" />
                                                 
                                                </td>
                                        </tr>
                                                                        <tr>
                                                                            <td colspan="6" >
                                                                               
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
<table >

<tr>
</tr></table>
</td></tr>
</table>

    </div>
    </form>
    
     <script language="javascript" type="text/javascript">
    function ValidateForm()
    {
      document.getElementById('<%=lblError.ClientId%>').innerText=''
     
        //      Checking txtOpenDateFrom .
        if(document.getElementById('<%=txtStartDateFrom.ClientId%>').value != '')
        {
        if (isDate(document.getElementById('<%=txtStartDateFrom.ClientId%>').value,"d/M/yyyy") == false)	
        {
           document.getElementById('<%=lblError.ClientId%>').innerText = "Date from is not valid.";			
	       document.getElementById('<%=txtStartDateFrom.ClientId%>').focus();
	       return(false);  
        }
        } 
         //      Checking txtOpenDateTo .
        if(document.getElementById('<%=txtStartDateTo.ClientId%>').value != '')
        {
        if (isDate(document.getElementById('<%=txtStartDateTo.ClientId%>').value,"d/M/yyyy") == false)	
        {
           document.getElementById('<%=lblError.ClientId%>').innerText = "Date to is not valid.";			
	       document.getElementById('<%=txtStartDateTo.ClientId%>').focus();
	       return(false);  
        }
        } 
         //      Checking txtCloseDateFrom .
   /*     if(document.getElementById('<=txtEndDateFrom.ClientId>').value != '')
        {
        if (isDate(document.getElementById('<=txtEndDateFrom.ClientId>').value,"d/M/yyyy") == false)	
        {
           document.getElementById('<=lblError.ClientId>').innerText = "End date from is not valid.";			
	       document.getElementById('<=txtEndDateFrom.ClientId>').focus();
	       return(false);  
        }
        } 
         //      Checking txtCloseDateTo .
        if(document.getElementById('<=txtEndDateTo.ClientId>').value != '')
        {
        if (isDate(document.getElementById('<=txtEndDateTo.ClientId>').value,"d/M/yyyy") == false)	
        {
           document.getElementById('<%=lblError.ClientId%>').innerText = "End date to is not valid.";			
	       document.getElementById('<=txtEndDateTo.ClientId>').focus();
	       return(false);  
        }
        } 
         */
        
        //    compareDates(dateOpen,dateformat1,dateAssignee,dateformat2) {
        if (document.getElementById('<%=txtStartDateFrom.ClientId%>').value != '' && document.getElementById('<%=txtStartDateTo.ClientId%>').value != '')
        {
        
           if (compareDates(document.getElementById('<%=txtStartDateFrom.ClientId%>').value,"d/M/yyyy",document.getElementById('<%=txtStartDateTo.ClientId%>').value,"d/M/yyyy")==1)
               {
                    document.getElementById('<%=lblError.ClientId%>').innerText ='Date to should be greater than or equal to date from.'
                    return false;
               }
               
        }
       
  /*     if (document.getElementById('<=txtEndDateFrom.ClientId>').value != '' && document.getElementById('<=txtEndDateTo.ClientId>').value != '')
        {
            if (compareDates(document.getElementById('<=txtEndDateFrom.ClientId>').value,"d/M/yyyy",document.getElementById('<=txtEndDateTo.ClientId>').value,"d/M/yyyy")==1)
           {
                document.getElementById('<%=lblError.ClientId%>').innerText ='End date to should be greater than or equal to end date from.'
                return false;
           }
        }*/
       return true; 
        
    
    
    }
    
   
 
    </script>
</body>
</html>
