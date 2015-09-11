<%@ Page Language="VB" AutoEventWireup="false" CodeFile="TRRPT_RequestForTraining.aspx.vb"
    Inherits="Training_TRRPT_RequestForTraining" %>

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

    <title>AAMS::Training::Request for Training</title>

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
          var strAgencyName=document.getElementById("txtAgencyName").value;
          strAgencyName=strAgencyName.replace("&","%26")
               type = "../TravelAgency/TASR_AgencyStaff.aspx?Popup=T&AgencyName="+strAgencyName ;
   	            window.open(type,"aaTrainingStaff","height=600,width=900,top=30,left=20,scrollbars=1,status=1");	 
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
           document.getElementById('<%=lblError.ClientId%>').innerText = "Request date from is not valid.";			
	       document.getElementById('<%=txtStartDateFrom.ClientId%>').focus();
	       return(false);  
        }
        } 
         //      Checking txtOpenDateTo .
        if(document.getElementById('<%=txtStartDateTo.ClientId%>').value != '')
        {
        if (isDate(document.getElementById('<%=txtStartDateTo.ClientId%>').value,"d/M/yyyy") == false)	
        {
           document.getElementById('<%=lblError.ClientId%>').innerText = "Request date to is not valid.";			
	       document.getElementById('<%=txtStartDateTo.ClientId%>').focus();
	       return(false);  
        }
        } 
              
   
       
       
        //    compareDates(dateOpen,dateformat1,dateAssignee,dateformat2) {
        
        if (document.getElementById('<%=txtStartDateFrom.ClientId%>').value != '' && document.getElementById('<%=txtStartDateTo.ClientId%>').value != '')
        { 
           if (compareDates(document.getElementById('<%=txtStartDateFrom.ClientId%>').value,"d/M/yyyy",document.getElementById('<%=txtStartDateTo.ClientId%>').value,"d/M/yyyy")==1)
               {
                    document.getElementById('<%=lblError.ClientId%>').innerText ='Request date to should be greater than or equal to Request date from.'
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
                                                <span class="menu">Training -&gt;</span><span class="sub_menu">Request for Training</span></td>
                                        </tr>
                                        <tr>
                                            <td class="heading center">
                                                Request for Training Report</td>
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
                                                                    <td style="width: 35px; height: 20px;">
                                                                    </td>
                                                                    <td class="textbold" style="width: 135px; height: 20px;">
                                                                        Agency</td>
                                                                    <td colspan="3" style="height: 20px">
                                                                        <asp:TextBox ID="txtAgencyName" runat="server" CssClass="textbox" MaxLength="50"
                                                                            TabIndex="1" Width="513px"></asp:TextBox>
                                                                        <img id="Img2" tabindex="2" runat="server" alt="Select & Add Agency Name" onclick="PopupPage(1)"
                                                                            src="../Images/lookup.gif" style="cursor:pointer;" /></td>
                                                                    <td class="left" style="width: 12%; height: 20px;">
                                                                        <asp:Button ID="btnDisplay" TabIndex="18" CssClass="button" runat="server" Text="Display" AccessKey="d" /></td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="width: 35px; height: 16px;">
                                                                    </td>
                                                                    <td class="textbold" style="width: 135px; height: 16px;">
                                                                        Whole Group</td>
                                                                    <td colspan="3" style="height: 16px">
                                                                        <asp:CheckBox ID="chbWholeGroup" runat="server" CssClass="textbold" Style="position: relative"
                                                                            TabIndex="3" TextAlign="Left" Width="144px" />&nbsp;
                                                                    </td>
                                                                    <td style="width: 12%; height: 16px;" class="left">
                                                                        <asp:Button ID="btnReset" CssClass="button" runat="server" Text="Reset" TabIndex="19" AccessKey="r" /></td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="width: 35px; height: 20px;">
                                                                    </td>
                                                                    <td class="textbold" style="width: 135px; height: 20px;">
                                                                        Course</td>
                                                                    <td style="height: 20px;" colspan="3">
                                                                        <asp:DropDownList ID="ddlCourse" runat="server" CssClass="dropdownlist" Width="519px"
                                                                            TabIndex="4" Style="position: relative" onkeyup="gotop(this.id)">
                                                                           
                                                                        </asp:DropDownList>&nbsp;</td>
                                                                    <td class="left" style="height: 20px">
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="width: 35px; height: 20px">
                                                                    </td>
                                                                    <td class="textbold" style="width: 135px; height: 20px">
                                                                        Agency Staff</td>
                                                                    <td colspan="3" style="height: 20px">
                                                                        <asp:TextBox ID="txtAgencyStaff" runat="server" CssClass="textbox" Style="position: relative"
                                                                            TabIndex="5" Width="513px" MaxLength="50"></asp:TextBox>
                                                                        <img id="Img5" runat="server" alt="Select & Add Agency Staff" onclick="PopupPage(2)"
                                                                            src="../Images/lookup.gif"  tabindex="6" style="position: relative;cursor:pointer;" /></td>
                                                                    <td class="left" style="height: 20px">
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="width: 35px; height: 20px">
                                                                    </td>
                                                                    <td class="textbold" style="width: 135px; height: 20px">
                                                                        Agency Type</td>
                                                                    <td style="width: 212px; height: 20px">
                                                                        <asp:DropDownList ID="drpAgencyType" runat="server" CssClass="dropdownlist" onkeyup="gotop(this.id)"
                                                                            Style="position: relative" TabIndex="7" Width="167px">
                                                                        </asp:DropDownList></td>
                                                                    <td class="textbold" style="width: 130px; height: 20px">
                                                                        AOffice</td>
                                                                    <td style="height: 20px">
                                                                        <asp:DropDownList ID="ddlAOffice" runat="server" CssClass="dropdownlist" Width="168px"
                                                                            TabIndex="8" Style="position: relative" onkeyup="gotop(this.id)">
                                                                          
                                                                        </asp:DropDownList></td>
                                                                    <td class="left" style="height: 20px">
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="width: 35px; height: 20px;">
                                                                    </td>
                                                                    <td class="textbold" style="width: 135px; height: 20px;">
                                                                        City</td>
                                                                    <td style="width: 212px; height: 20px;">
                                                                        <asp:DropDownList ID="drpCity1" runat="server" CssClass="dropdownlist" Style="position: relative"
                                                                            TabIndex="9" Width="168px" onkeyup="gotop(this.id)">
                                                                        </asp:DropDownList></td>
                                                                    <td class="textbold" style="width: 130px; height: 20px;">
                                                                        Region</td>
                                                                    <td style="height: 20px">
                                                                        <asp:DropDownList ID="ddlRegion" runat="server" CssClass="dropdownlist" Style="left: 0px;
                                                                            position: relative; top: 0px" TabIndex="10" Width="168px" onkeyup="gotop(this.id)">
                                                                        </asp:DropDownList>&nbsp;</td>
                                                                    <td class="left" style="height: 20px">
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="width: 35px; height: 20px">
                                                                    </td>
                                                                    <td class="textbold" style="width: 135px; height: 20px">
                                                                        Employee</td>
                                                                    <td style="width: 212px; height: 20px">
                                                                        <asp:DropDownList ID="ddlEmployee" runat="server" CssClass="dropdownlist" Width="168px"
                                                                            TabIndex="11" Style="position: relative" onkeyup="gotop(this.id)">
                                                                          
                                                                        </asp:DropDownList></td>
                                                                    <td class="textbold" style="width: 130px; height: 20px">
                                                                        Request Source</td>
                                                                    <td style="height: 20px">
                                                                        <asp:DropDownList ID="ddlRequestSource" runat="server" CssClass="dropdownlist" Width="168px"
                                                                            TabIndex="12" Style="position: relative" onkeyup="gotop(this.id)">
                                                                            <asp:ListItem Selected="True" Value="">--All--</asp:ListItem>
                                                                            <asp:ListItem Text="Both" Value="2"></asp:ListItem>
                                                                            <asp:ListItem Text="Client" Value="0"></asp:ListItem>
                                                                            <asp:ListItem Text="Web Request" Value="1"></asp:ListItem>
                                                                        </asp:DropDownList></td>
                                                                    <td class="left" style="height: 20px">
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="width: 35px; height: 20px">
                                                                    </td>
                                                                    <td class="textbold" style="width: 135px; height: 20px">
                                                                        RequestID</td>
                                                                    <td style="width: 212px; height: 20px">
                                                                        <asp:TextBox ID="txtRequestId" runat="server" CssClass="textbox" Style="left: 0px;
                                                                            position: relative; top: 0px" TabIndex="13" Width="160px" MaxLength="15"></asp:TextBox></td>
                                                                    <td class="textbold" style="width: 130px; height: 20px">
                                                                        </td>
                                                                    <td style="height: 20px">
                                                                        </td>
                                                                    <td class="left" style="height: 20px">
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="width: 35px; height: 20px;">
                                                                    </td>
                                                                    <td class="textbold" style="width: 135px; height: 20px;">
                                                                        Request Date From</td>
                                                                    <td style="width: 212px; height: 20px;">
                                                                        <asp:TextBox ID="txtStartDateFrom" runat="server" CssClass="textbox" Style="left: 0px;
                                                                            position: relative; top: 0px" TabIndex="14" Width="160px" MaxLength="10"></asp:TextBox>
                                                                        <img id="imgStartFrom" alt="" src="../Images/calender.gif" style="cursor: pointer;
                                                                            position: relative" tabindex="15" title="Date selector" />

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
                                                                    <td class="textbold" style="width: 130px; height: 20px;">
                                                                        Request Date To
                                                                    </td>
                                                                    <td style="height: 20px">
                                                                        <asp:TextBox ID="txtStartDateTo" runat="server" CssClass="textbox" Style="left: 0px;
                                                                            position: relative; top: 0px" TabIndex="16" Width="161px" MaxLength="10"></asp:TextBox>
                                                                        <img id="imgStartTo" alt="" src="../Images/calender.gif" style="cursor: pointer;
                                                                            position: relative; left: 0px; top: 0px;" tabindex="17" title="Date selector" />

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
                                                                    <td class="left" style="height: 20px">
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="width: 35px">
                                                                    </td>
                                                                    <td style="width: 135px">
                                                                    </td>
                                                                    <td colspan="4">
                                                                        <input id="hdDeleteId" runat="server" style="width: 1px" type="hidden" enableviewstate="true" />
                                                                        <asp:HiddenField ID="hdCourseLCode1" runat="server" />
                                                                        <asp:HiddenField ID="hdRoomID" runat="server" />
                                                                        <asp:HiddenField ID="hdCourseStaff1" runat="server" />
                                                                        <input type="hidden" id="hdAgencyNameId" runat="server" value="" style="width: 5px" />
                                                                        <input type="hidden" id="hdAgencyName" runat="server" value="" style="width: 5px" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="6">
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
