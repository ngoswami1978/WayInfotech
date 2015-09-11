<%@ Page Language="VB" AutoEventWireup="false" CodeFile="TRRPT_PendingTraining.aspx.vb"
    Inherits="Training_TRRPT_PendingTraining" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" media="all" href="../Calender/calendar-blue.css"
        title="win2k-cold-1" />

    <script type="text/javascript" src="../Calender/calendar.js"></script>

    <script type="text/javascript" src="../JavaScript/AAMS.js"></script>

    <!-- import the language module -->

    <script type="text/javascript" src="../Calender/lang/calendar-en.js"></script>

    <script type="text/javascript" src="../Calender/calendar-setup.js"></script>

    <title>AAMS::Training::Pending Training Report</title>

    <script language="javascript" type="text/javascript">
     	
 function AgencyValidation()
     {
        document.getElementById("hdAgencyName").value="";
         document.getElementById("chbWholeGroup").disabled=true;
    	document.getElementById("chbWholeGroup").checked=false;
     }
   
 
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
              
                type = "../Training/TRSR_TrainingRooms.aspx?Popup=T";
   	            window.open(type,"aaTrainingRoom","height=600,width=900,top=30,left=20,scrollbars=1,status=1");           
   	             return false; 
           }
          if (id=="3")
          {
        var strAgencyName=document.getElementById("txtAgencyName").value;
        strAgencyName=strAgencyName.replace("&","%26")
                type = "../TravelAgency/TASR_AgencyStaff.aspx?Popup=T&AgencyName="+strAgencyName ;
 	            window.open(type,"aaTrainingStaff","height=600,width=900,top=30,left=20,scrollbars=1,status=1");	 
    	             return false; 
         }
        
          }
    
      function disableChkbox()
  {
     document.getElementById("chbWholeGroup").disabled = true;
   	     
  }      
     
    
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

</head>
<body onload="return disableChkbox();" >
    <form id="form1" runat="server" defaultfocus="txtAgencyName" defaultbutton="btnDisplay">
        <div>
            <table>
                <tr>
                    <td>
                        <table width="860px" class="border_rightred left">
                            <tr>
                                <td class="top">
                                    <table width="100%" class="left">
                                        <tr>
                                            <td>
                                                <span class="menu">Training -&gt;</span><span class="sub_menu">Pending Training</span></td>
                                        </tr>
                                        <tr>
                                            <td class="heading center" style="height: 20px">
                                                Pending Training Report</td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                    <tr>
                                                        <td class="redborder center">
                                                            <table border="0" cellpadding="2" cellspacing="1" style="width: 100%" class="left">
                                                                <tr>
                                                                    <td class="center gap" colspan="6">
                                                                        <asp:Label ID="lblError" runat="server" CssClass="ErrorMsg" EnableViewState="False"></asp:Label></td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="width: 23px; height: 18px;">
                                                                    </td>
                                                                    <td class="textbold" style="width: 138px; height: 18px;">
                                                                        Agency</td>
                                                                    <td colspan="3" style="height: 18px">
                                                                        <asp:TextBox ID="txtAgencyName" runat="server" CssClass="textbox" MaxLength="50"
                                                                            TabIndex="1" Width="502px"></asp:TextBox>
                                                                        <img id="Img2" tabindex="2" runat="server" alt="Select & Add Agency Name" onclick="PopupPage(1)"
                                                                            src="../Images/lookup.gif" style="cursor:pointer;" />
                                                                        <%--<img src="../Images/lookup.gif" alt="Select & Add Agency Name" tabindex="2" onclick="javascript:return PopupAgencyPage();" /> --%>
                                                                    </td>
                                                                    <td class="left" style="width: 12%; height: 18px;">
                                                                        <asp:Button ID="btnDisplay" TabIndex="22" CssClass="button" runat="server" Text="Display" AccessKey="d" /></td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="width: 23px; height: 18px;">
                                                                    </td>
                                                                    <td class="textbold" style="width: 138px; height: 18px;">
                                                                        Whole Group</td>
                                                                    <td colspan="3" style="height: 18px">
                                                                        <asp:CheckBox ID="chbWholeGroup" runat="server" CssClass="textbold" Style="position: relative"
                                                                            TabIndex="3" TextAlign="Left" Width="144px" />&nbsp;
                                                                    </td>
                                                                    <td style="width: 12%; height: 18px;" class="left">
                                                                        <asp:Button ID="btnReset" CssClass="button" runat="server" Text="Reset" TabIndex="23" AccessKey="r" /></td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="width: 23px; height: 21px;">
                                                                    </td>
                                                                    <td class="textbold" style="width: 138px; height: 21px;">
                                                                        Course</td>
                                                                    <td style="height: 21px;" colspan="3">
                                                                        <asp:DropDownList ID="ddlCourse" runat="server" CssClass="dropdownlist" Width="507px"
                                                                            TabIndex="4" Style="position: relative" onkeyup="gotop(this.id)">
                                                                          
                                                                        </asp:DropDownList>&nbsp;</td>
                                                                    <td class="left" style="height: 21px">
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="width: 23px; height: 18px;">
                                                                    </td>
                                                                    <td class="textbold" style="width: 138px; height: 18px;">
                                                                        Location</td>
                                                                    <td style="height: 18px;" colspan="3">
                                                                        <asp:TextBox ID="txtTrainingRoom" runat="server" CssClass="textbox" Style="position: relative"
                                                                            TabIndex="5" Width="502px" MaxLength="50"></asp:TextBox>
                                                                        <img id="Img5" runat="server" tabindex="6" alt="Select & Add Training Room" class="topMargin"
                                                                            onclick="PopupPage(2)" src="../Images/lookup.gif" style="vertical-align: text-top;
                                                                            position: relative;cursor:pointer;"  />&nbsp;</td>
                                                                    <td class="left" style="height: 18px">
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="width: 23px; height: 18px">
                                                                    </td>
                                                                    <td class="textbold" style="width: 138px; height: 18px">
                                                                        Region</td>
                                                                    <td style="width: 218px; height: 18px">
                                                                        <asp:DropDownList ID="ddlRegion" runat="server" CssClass="dropdownlist" Style="position: relative"
                                                                            TabIndex="7" Width="168px" onkeyup="gotop(this.id)">
                                                                            
                                                                        </asp:DropDownList></td>
                                                                    <td class="textbold" style="width: 115px; height: 18px">
                                                                        Agency Type</td>
                                                                    <td style="height: 18px">
                                                                        <asp:DropDownList ID="drpAgencyType" runat="server" CssClass="dropdownlist" onkeyup="gotop(this.id)"
                                                                            Style="position: relative" TabIndex="8" Width="147px">
                                                                        </asp:DropDownList></td>
                                                                    <td class="left" style="height: 18px">
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="width: 23px; height: 18px;">
                                                                    </td>
                                                                    <td class="textbold" style="width: 138px; height: 18px;">
                                                                        AOffice</td>
                                                                    <td style="width: 218px; height: 18px;">
                                                                        <asp:DropDownList ID="ddlAOffice" runat="server" CssClass="dropdownlist" Width="168px"
                                                                            TabIndex="9" onkeyup="gotop(this.id)">
                                                                           
                                                                        </asp:DropDownList></td>
                                                                    <td class="textbold" style="height: 18px;">
                                                                        Agency Staff</td>
                                                                    <td style="height: 18px">
                                                                        <asp:TextBox ID="txtAgencyStaff" runat="server" CssClass="textbox" Style="position: relative"
                                                                            TabIndex="10" Width="140px" MaxLength="50"></asp:TextBox>
                                                                        <img id="Img4" runat="server" alt="Select & Add Agency Staff" onclick="PopupPage(3)"
                                                                            tabindex="11" src="../Images/lookup.gif" style="position: relative;cursor:pointer;"  /></td>
                                                                    <td class="left" style="height: 18px">
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="width: 23px; height: 18px">
                                                                    </td>
                                                                    <td class="textbold" style="width: 138px; height: 18px">
                                                                        Trainer 1</td>
                                                                    <td style="width: 218px; height: 18px">
                                                                    <asp:TextBox ID="txtTrainer1" runat="server" CssClass="textbox" MaxLength="40" TabIndex="2"
                                                    Width="170px"></asp:TextBox>  <IMG style="CURSOR: pointer" id="Img1" onclick="PopupPageTrainerForTraining(1,'txtTrainer1')" 
alt="Select & Add Employee" src="../Images/lookup.gif" runat="server" />
                                                                        </td>
                                                                    <td class="textbold" style="height: 18px">
                                                                        Trainer 2</td>
                                                                    <td style="height: 18px">
                                                                    <asp:TextBox ID="txtTrainer2" runat="server" CssClass="textbox" MaxLength="40" TabIndex="2"
                                                    Width="139px"></asp:TextBox>  <IMG style="CURSOR: pointer" id="Img3" onclick="PopupPageTrainerForTraining(1,'txtTrainer2')" 
alt="Select & Add Employee" src="../Images/lookup.gif" runat="server" />
                                                                        </td>
                                                                    <td class="left" style="height: 18px">
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="width: 23px; height: 18px;">
                                                                    </td>
                                                                    <td class="textbold" style="width: 138px; height: 18px;">
                                                                        <asp:DropDownList ID="ddlDateType" runat="server" CssClass="dropdownlist" onkeyup="gotop(this.id)"
                                                                            TabIndex="2" Width="90px">
                                                                            <asp:ListItem Text="" Value="0"></asp:ListItem>
                                                                            <asp:ListItem Text="Start Date" Value="1"></asp:ListItem>
                                                                            <asp:ListItem Text="End Date" Value="2"></asp:ListItem>
                                                                        </asp:DropDownList></td>
                                                                    <td style="width: 218px; height: 18px;" class="textbold">
                                                                        From &nbsp;
                                                                        <asp:TextBox ID="txtStartDateFrom" runat="server" CssClass="textbox" Style="position: relative;
                                                                            left: 0px; top: 0px;" TabIndex="14" Width="132px" MaxLength="10"></asp:TextBox>
                                                                        <img id="imgStartFrom" alt="" tabindex="15" src="../Images/calender.gif" title="Date selector"
                                                                            style="cursor: pointer; position: relative; left: 0px;" />

                                                                        <script type="text/javascript">
                                                                                                    Calendar.setup({
                                                                                                    inputField     :    '<%=txtStartDateFrom.clientId%>',
                                                                                                    ifFormat       :    "%d/%m/%Y",
                                                                                                    button         :    "imgStartFrom",
                                                                                                    //align          :    "Tl",
                                                                                                    singleClick    :    true
                                                                                                    });
                                                                        </script>

                                                                    </td>
                                                                    <td class="textbold" style="height: 18px;">
                                                                        To
                                                                    </td>
                                                                    <td style="height: 18px">
                                                                        <asp:TextBox ID="txtStartDateTo" runat="server" CssClass="textbox" Style="position: relative;
                                                                            left: 0px; top: 0px;" TabIndex="16" Width="141px" MaxLength="10"></asp:TextBox>
                                                                        <img id="imgStartTo" alt="" tabindex="17" src="../Images/calender.gif" title="Date selector"
                                                                            style="cursor: pointer; position: relative;" />

                                                                        <script type="text/javascript">
                                                                                                    Calendar.setup({
                                                                                                    inputField     :    '<%=txtStartDateTo.clientId%>',
                                                                                                    ifFormat       :    "%d/%m/%Y",
                                                                                                    button         :    "imgStartTo",
                                                                                                    //align          :    "Tl",
                                                                                                    singleClick    :    true
                                                                                                    });
                                                                        </script>

                                                                    </td>
                                                                    <td class="left" style="height: 18px">
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="width: 35px">
                                                                    </td>
                                                                    <td style="width: 138px">
                                                                    </td>
                                                                    <td colspan="4" >
                                                                        <input id="hdDeleteId" runat="server" style="width: 1px" type="hidden" enableviewstate="true" />
                                                                        <input type="hidden" id="hdAgencyNameId" runat="server" value="" style="width: 5px" />
                                                                        <input type="hidden" id="hdAgencyName" runat="server" value="" style="width: 5px" />
                                                                        <asp:HiddenField ID="hdCourseLCode1" runat="server" />
                                                                        <asp:HiddenField ID="hdRoomID" runat="server" />
                                                                        <asp:HiddenField ID="hdCourseStaff1" runat="server" />
                                                                        <input type="hidden" id="hdEmployeePageName" style="width:1px" runat='server' />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="6" style="height: 13px">
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
        </div>
    </form>
</body>
</html>
