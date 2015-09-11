<%@ Page Language="VB" AutoEventWireup="false" CodeFile="TARPT_ReTaining.aspx.vb" Inherits="Training_TARPT_ReTaining" %>
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
    <title>AAMS::Training::ReTraining</title>
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
   
        function disableChkbox()
        {
              document.getElementById("chbWholeGroup").disabled = true;
        }  
        
        function AgencyValidation()
        {
            var OldAgencyName;
            var NewAgencyName;
            var l=0;
            OldAgencyName= document.getElementById('<%=hdtxtAgencyName.ClientId%>').value;
            NewAgencyName= document.getElementById('<%=txtAgency.ClientId%>').value;
                        
            var r = NewAgencyName.length ;            
            while(l < NewAgencyName.length && NewAgencyName[l] == '  ')
            {l++;}
            while(r > l && NewAgencyName[r] == '  ')
            {r-=1;}        
            
            if (OldAgencyName != '')
            {
                if (NewAgencyName.substring(l, r+1) != '')
                {
                   if (NewAgencyName.substring(l, r+1) == OldAgencyName)
                    {   
                        document.getElementById("chbWholeGroup").disabled = false;
                    }                  
                    else
                    {                        
                        document.getElementById("chbWholeGroup").disabled = true;
                    }
                }
             }   
        }
        
        
    
</script>
</head>
<body onload="return disableChkbox();" >
    <form id="form1" runat="server"  defaultfocus="txtAgency" defaultbutton="btnSearch">
    <div>
     <table>
    <tr>
    <td>
    <table width="860px"   class="border_rightred left">
            <tr>
                <td class="top">
                    <table width="860px" class="left">
                        <tr>
                            <td valign="top">
                            <span class="menu"> Training -&gt;</span><span class="sub_menu">ReTraining</span></td>
                        </tr>
                        <tr>
                            <td class="heading center" >
                                ReTraining</td>
                        </tr>
                        <tr>
                            <td >
                                <table width="860px" border="0" cellspacing="0" cellpadding="0" style="height: 274px">
                                    <tr valign="top">
                                        <td class="redborder center" style="height: 277px"  >
                                    <table border="0" cellpadding="2" cellspacing="1" style="width: 100%" class="left">
                                                                        <tr>
                                                                            <td class="center gap" colspan="6" style="height: 8pt"  >
                                                                <asp:Label ID="lblError" runat="server" CssClass="ErrorMsg" EnableViewState="False"></asp:Label></td>
                                                                        </tr>
                                        <tr>
                                            <td style="width: 60px">
                                            </td>
                                            <td class="textbold">
                                                Agency</td>
                                            <td colspan="3">
                                                <asp:TextBox ID="txtAgency" runat="server" CssClass="textbox" MaxLength="50" TabIndex="2"
                                                    Width="489px" ></asp:TextBox >
                                                <img id="Img2" runat="server" alt="Select & Add Agency Name" onclick="PopupPage(1)"
                                                    src="../Images/lookup.gif" style="cursor:pointer;" /></td>
                                            <td class="left" style="width: 12%">
                                            <asp:Button ID="btnSearch" CssClass="button" runat="server" Text="Display" TabIndex="3" AccessKey="d" /></td>
                                        </tr>
                                       <tr>
                                            <td>
                                            </td>
                                            <td class="textbold" style="width: 120px">
                                                Whole Group</td>
                                            <td style="width: 180px">
                                                <asp:CheckBox ID="chbWholeGroup" runat="server" CssClass="dropdown" /></td>
                                            <td class="textbold" style="width: 100px">
                                                Agency Type</td>
                                            <td style="width: 180px">
                                                <asp:DropDownList ID="drpAgencyType" runat="server" CssClass="dropdownlist" onkeyup="gotop(this.id)"
                                                    Style="position: relative" TabIndex="11" Width="141px">
                                                </asp:DropDownList></td>
                                            <td class="left" style="width: 150px">
                                               <asp:Button ID="btnReset" CssClass="button" runat="server" Text="Reset" TabIndex="3" AccessKey="r" /></td>
                                        </tr>
                                        <tr>
                                            <td>
                                            </td>
                                            <td class="textbold">
                                                Course</td>
                                            <td colspan="3">
                                             <asp:DropDownList ID="ddlCourse" runat="server" CssClass="dropdownlist" Width="494px" TabIndex="2" onkeyup="gotop(this.id)">
                                                  
                                             </asp:DropDownList></td>
                                            <td class="left" style="width: 12%">
                                            </td>
                                        </tr>
                                          <tr>
                                            <td>
                                            </td>
                                            <td class="textbold">
                                                Location</td>
                                            <td colspan="3">
                                                <asp:TextBox ID="txtTrainingRoom" runat="server" CssClass="textbox" MaxLength="50" TabIndex="2"
                                                    Width="488px"></asp:TextBox>
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
                                                    TabIndex="2" Width="489px" MaxLength="50"></asp:TextBox>
                                                <img id="Img3" runat="server" alt="Select & Add Agency Staff" onclick="PopupPage(2)"
                                                    src="../Images/lookup.gif" style="cursor:pointer;" /></td>
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
alt="Select & Add Agency Name" src="../Images/lookup.gif" runat="server" />
                                                </td>
                                            <td class="textbold">
                                                Trainer 2</td>
                                            <td><asp:TextBox ID="txtTrainer2" runat="server" CssClass="textbox" MaxLength="40" TabIndex="2"
                                                    Width="136px"></asp:TextBox>  <IMG style="CURSOR: pointer" id="Img5" onclick="PopupPageTrainerForTraining(1,'txtTrainer2')" 
alt="Select & Add Agency Name" src="../Images/lookup.gif" runat="server" />
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
                                                <asp:DropDownList ID="ddlRegion" runat="server" CssClass="dropdownlist" Width="142px" TabIndex="2" onkeyup="gotop(this.id)">
                                                   
                                                 
                                                </asp:DropDownList></td>
                                            <td class="left">
                                            </td>
                                        </tr>
                                      
                                          <tr>
                                            <td>
                                            </td>
                                            <td class="textbold">
                                            <asp:DropDownList ID="ddlDateType" runat="server" CssClass="dropdownlist" onkeyup="gotop(this.id)"
                                                    TabIndex="2" Width="90px">
                                                    <asp:ListItem Text="" Value="0"></asp:ListItem>
                                                    <asp:ListItem Text="Start Date" Value="1"></asp:ListItem>
                                                    <asp:ListItem Text="End Date" Value="2"></asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td class="textbold">
                                                From&nbsp;&nbsp;  <asp:TextBox ID="txtStartDateFrom" runat="server" CssClass="textbox" Width="132px" TabIndex="2" MaxLength="10"></asp:TextBox>
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
                                                <asp:TextBox ID="txtStartDateTo" runat="server" CssClass="textbox" Width="136px" TabIndex="2" MaxLength="10"></asp:TextBox>
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
                                            <td class="left">
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
                                            
                                                <input id="hdDeleteId" runat="server" style="width: 5px" type="hidden" enableviewstate="true"/>
                    <asp:HiddenField ID="hdCourseLCode" runat ="server" />
                                                <asp:HiddenField ID="hdRoomID" runat ="server" />
                                                <asp:HiddenField ID="hdCourseStaff" runat ="server" />
                                                <asp:HiddenField ID="hdCourseId" runat=server />
                                                <asp:HiddenField ID="hdtxtAgencyName" runat=server />
                                                <input type="hidden" id="hdEmployeePageName" style="width:1px" runat='server' />
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
        
       
       
        //    compareDates(dateOpen,dateformat1,dateAssignee,dateformat2) {
         if (document.getElementById('<%=txtStartDateFrom.ClientId%>').value != '' && document.getElementById('<%=txtStartDateTo.ClientId%>').value != '')
        {
           if (compareDates(document.getElementById('<%=txtStartDateFrom.ClientId%>').value,"d/M/yyyy",document.getElementById('<%=txtStartDateTo.ClientId%>').value,"d/M/yyyy")==1)
               {
                    document.getElementById('<%=lblError.ClientId%>').innerText ='Date to should be greater than or equal to date from.'
                    return false;
               }
        }
        
      
       return true; 
        
    
    
    }
   
   
 
    </script>
</body>
</html>
