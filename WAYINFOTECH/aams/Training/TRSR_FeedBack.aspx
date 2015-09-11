<%@ Page Language="VB" AutoEventWireup="false" ValidateRequest="true" EnableEventValidation="false"  CodeFile="TRSR_FeedBack.aspx.vb" Inherits="Training_TRSR_FeedBack"
    MaintainScrollPositionOnPostback="true" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>AAMS::Training::Search FeedBack</title>
    <link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" media="all" href="../Calender/calendar-blue.css"
        title="win2k-cold-1" />

    <script type="text/javascript" src="../Calender/calendar.js"></script>

    <script type="text/javascript" src="../JavaScript/AAMS.js"></script>    
    <!-- import the language module -->

    <script type="text/javascript" src="../Calender/lang/calendar-en.js"></script>

    <script type="text/javascript" src="../Calender/calendar-setup.js"></script>

    <script language="javascript" type="text/javascript">
    // Code Added by Mukund on 26th March
    var st;
    
    function edCourse()
    {
       var txtCourseTitleFeedBack=document.getElementById("txtCourseTitleFeedBack").value;
       if(txtCourseTitleFeedBack!="")
       {
            document.getElementById('txtCourse').disabled=true;
            document.getElementById('txtCourse').className="textboxgrey";
       }
       else
       {
            document.getElementById('txtCourse').disabled=false;
            document.getElementById('txtCourse').className="dropdownlist";
       }
   
    }
    
    function keepSelectedID()
    {
    
    {debugger;}
    document.getElementById("hdFeedbackTopicID").value= document.getElementById('<%=drpFeedbackTopic.ClientId%>').options[document.getElementById('<%=drpFeedbackTopic.ClientId%>').selectedIndex].text;
     document.getElementById("hdFeedbackTopicIDIndex").value=document.getElementById('<%=drpFeedbackTopic.ClientId%>').selectedIndex;
       
   return false;
   
    }
    
   function fillCategoryName(s)
   {
   
   //debugger;
   
      document.getElementById('<%=drpFeedbackTopic.ClientId%>').options.length=0;
      document.getElementById('<%=drpFeedbackTopic.ClientId%>').disabled=true;
      document.getElementById('<%=drpFeedbackTopic.ClientId%>').options[0]=new Option("Loading...","0");  
      id=document.getElementById('<%=drpFeedbackDomain.ClientId%>').value;
      CourseId = document.getElementById('<%=txtCourse.ClientId%>').value;
      strIDlist = id +'|'+CourseId
      CallServer(strIDlist,"This is context from client");
      return false;
   }
   
    function ReceiveServerData(args, context)
    {        
            var obj = new ActiveXObject("MsXml2.DOMDocument");
         	var codes='';
			var names="--All--";
			var ddlCategoryName = document.getElementById('<%=drpFeedbackTopic.ClientId%>');
			ddlCategoryName.disabled=false;
			if (args=="") 
            {
             listItem = new Option(names, codes );
             ddlCategoryName.options[0] = listItem;
            }
            else
            {
                
                obj.loadXML(args);
			    var dsRoot=obj.documentElement; 
			    if (dsRoot !=null)
			    {   			
			    var orders = dsRoot.getElementsByTagName('DETAILS');
			    var text;     			
			    var listItem;
			    listItem = new Option(names, codes);
			    ddlCategoryName.options[0] = listItem;
			    for (var count = 0; count < orders.length; count++)
			    {
				    codes= orders[count].getAttribute("TR_CVALTOPIC_ID"); 
			        names=orders[count].getAttribute("TR_TOPICS"); 
				    listItem = new Option(names, codes);
				    ddlCategoryName.options[ddlCategoryName.length] = listItem;
			    }
			    }
			    else
			    {
			        listItem = new Option(names, codes );
                    ddlCategoryName.options[0] = listItem;
			    }
			}			
    }
    
    
    
    
    // Code Added by Mukund on 26th March
    
    
      function ValidateForm()
    {
      document.getElementById('<%=lblError.ClientId%>').innerText=''
     
        //      Checking txtOpenDateFrom .
        if(document.getElementById('<%=txtStartDateFrom.ClientId%>').value != '')
        {
        if (isDate(document.getElementById('<%=txtStartDateFrom.ClientId%>').value,"d/M/yyyy") == false)	
        {
           document.getElementById('<%=lblError.ClientId%>').innerText = "Start date from is not valid.";			
	       document.getElementById('<%=txtStartDateFrom.ClientId%>').focus();
	       return(false);  
        }
        } 
         //      Checking txtOpenDateTo .
        if(document.getElementById('<%=txtStartDateTo.ClientId%>').value != '')
        {
        if (isDate(document.getElementById('<%=txtStartDateTo.ClientId%>').value,"d/M/yyyy") == false)	
        {
           document.getElementById('<%=lblError.ClientId%>').innerText = "Start date to is not valid.";			
	       document.getElementById('<%=txtStartDateTo.ClientId%>').focus();
	       return(false);  
        }
        } 
            
        
   if (compareDates(document.getElementById('<%=txtStartDateFrom.ClientId%>').value,"d/M/yyyy",document.getElementById('<%=txtStartDateTo.ClientId%>').value,"d/M/yyyy")==1)
       { 
            document.getElementById('<%=lblError.ClientId%>').innerText ='Start date to should be greater than or equal to Start date from.'
            return false;
       }
       
    }
     function PopupPage(id)
         {
         var type;
         if (id=="1")
         {
              type = "TRSR_CourseSession.aspx?Popup=T" ;
   	            window.open(type,"aaCourseSessionFeedBack","height=600,width=900,top=30,left=20,scrollbars=1,status=1");	    
          }
        
    
           
     }
     
      function SelectAllAck() 
    {
   
       CheckAllDataGridCheckBoxesCourse(document.forms[0].chkAllSelect.checked)
    }
    function CheckAllDataGridCheckBoxesCourse(value) 
    {
    
        for(i=0;i<document.forms[0].elements.length;i++) 
        {
        
        var elm = document.forms[0].elements[i]; 
            if(elm.type == 'checkbox') 
            {
           
             //  if (elm.name !='chkInternalCourse' && elm.name != 'chlShowOnWeb' && elm.name != 'chkRequired')
               // {
                  elm.checked = value
               // }
            }
        }
    }
    
  
    
    
    </script>

</head>
<body onload="edCourse();">
    <form id="form1" runat="server" defaultbutton="btnSearch" defaultfocus="txtStartDateFrom">
        <table class="left">
            <tr>
                <td class="top">
                    <table width="860px">
                        <tr>
                            <td class="top">
                                <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td class="top left">
                                            <span class="menu">Training-&gt;FeedBack</span></td>
                                    </tr>
                                    <tr>
                                        <td class="heading center">
                                            Search FeedBack</td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td class="top">
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td class="redborder top" colspan="2" style="width: 100%">
                                            <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td class="center TOP">
                                                        <asp:Label ID="lblError" CssClass="ErrorMsg" runat="server" EnableViewState="False"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="top" style="width: 100%;">
                                                        <table width="100%" border="0" class="left" cellpadding="2" cellspacing="0">
                                                            <tr>
                                                                <td class="textbold" style="width: 90%">
                                                                    <asp:Panel ID="pnlRegister" runat="server" Width="100%">
                                                                        <table width="100%" border="0" class="left" cellpadding="2" cellspacing="0">
                                                                          <%--  <tr>
                                                                                <td style="width: 2%">
                                                                                </td>
                                                                                <td colspan="7" class="subheading">
                                                                                    Course Session
                                                                                </td>
                                                                            </tr>--%>
                                                                            <tr>
                                                                                <td class="textbold">
                                                                                </td>
                                                                                <td class="textbold" >
                                                                                    Course Session</td>
                                                                                <td colspan="3">
                                                                                    <asp:TextBox ID="txtCourseTitleFeedBack" runat="server" CssClass="textboxgrey" ReadOnly="True"
                                                                                        TabIndex="20" Width="464px"></asp:TextBox>
                                                                                    <img id="Img2" runat="server" alt="Select & Add Course Session" onclick="PopupPage(1)"
                                                                                        src="../Images/lookup.gif" style="cursor: pointer;" /></td>
                                                                                <td colspan="2" style="width: 26px" align ="left" >
                                                                                    </td>
                                                                                <td class="top" colspan="1" style="width: 2%">
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td class="textbold">
                                                                                </td>
                                                                                <td class="textbold" >
                                                                                    Training Room</td>
                                                                                <td colspan="3">
                                                                                    <asp:TextBox ID="txtTrainingRoomFeedBack" runat="server" CssClass="textboxgrey" Height="50px"
                                                                                        ReadOnly="True" Rows="4" TabIndex="20" TextMode="MultiLine" Width="461px"></asp:TextBox></td>
                                                                                <td class="top" colspan="2" style="width: 26px">
                                                                                </td>
                                                                                <td class="top" colspan="1">
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td class="textbold">
                                                                                </td>
                                                                                <td class="textbold" >
                                                                                    Start Date</td>
                                                                                <td>
                                                                                    <asp:TextBox ID="txtStartDateFeedBack" runat="server" CssClass="textboxgrey" ReadOnly="True"
                                                                                        TabIndex="20" Width="170px"></asp:TextBox></td>
                                                                                <td class="textbold" >
                                                                                    Course Level</td>
                                                                                <td style="width :195px">
                                                                                    <asp:TextBox ID="txtCourseLevelFeedBack" runat="server" CssClass="textboxgrey" ReadOnly="True"
                                                                                        TabIndex="20" Width="160px"></asp:TextBox></td>
                                                                                <td class="top" colspan="2" style="width: 26px">
                                                                                </td>
                                                                                <td class="top" colspan="1">
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td class="textbold">
                                                                                </td>
                                                                                <td class="textbold" style="width: 200px">
                                                                                    Max No. of Participant
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="txtMaxNoParticipantFeedBack" runat="server" CssClass="textboxgrey"
                                                                                        ReadOnly="True" TabIndex="20" Width="170px"></asp:TextBox></td>
                                                                                <td class="textbold" style="width: 115px">
                                                                                    NMC Trainer</td>
                                                                                <td style="width :195px">
                                                                                    <asp:TextBox ID="txtNMCTrainersFeedBack" runat="server" CssClass="textboxgrey" ReadOnly="True"
                                                                                        TabIndex="20" Width="160px"></asp:TextBox></td>
                                                                                <td class="top" colspan="2" style="width: 26px">
                                                                                </td>
                                                                                <td class="top" colspan="1">
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td class="textbold">
                                                                                </td>
                                                                                <td class="textbold" >
                                                                                    Course</td>
                                                                                <td colspan="3">
                                                                                    <asp:DropDownList  ID="txtCourse" runat="server" onkeyup="gotop(this.id)" CssClass="textbox" TabIndex="20" Width="459px"></asp:DropDownList></td>
                                                                                <td class="top" colspan="2" style="width: 26px">
                                                                                </td>
                                                                                <td class="top" colspan="1">
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td class="textbold">
                                                                                </td>
                                                                                <td class="textbold">
                                                                                </td>
                                                                                <td>
                                                                                </td>
                                                                                <td class="textbold">
                                                                                </td>
                                                                                <td>
                                                                                </td>
                                                                                <td class="top" colspan="2" style="width: 26px">
                                                                                </td>
                                                                                <td class="top" colspan="1">
                                                                                </td>
                                                                            </tr>
                                                                           
                                                                            
                                                                               
                                                                           <tr>
                                                                                <td class="textbold">
                                                                                </td>
                                                                                <td class="textbold" >
                                                                                    FeedBack</td>
                                                                                <td>
                                                                                    <asp:DropDownList onkeyup="gotop(this.id)" ID="ddlFeedback" runat="server" CssClass="dropdownlist"
                                                                                        TabIndex="4" Width="176px">
                                                                                    </asp:DropDownList>
                                                                                </td>
                                                                                <td class="textbold" >
                                                                                    NMC
                                                                                    Trainer</td>
                                                                                <td style="width :195px">
                                                                                <asp:TextBox ID="txtTrainer2" runat="server" CssClass="textbox" MaxLength="40" TabIndex="5"
                                                                                Width="160px"></asp:TextBox>                                                                                  
                                                                                <IMG style="CURSOR: pointer" id="Img5" onclick="PopupPageTrainerForTraining(1,'txtTrainer2')" alt="Select & Add Employee" src="../Images/lookup.gif" runat="server" /></td>                                                                                                                                                                
                                                                                <td colspan ="2" style="width: 26px" align ="left" >
                                                                                    &nbsp;</td>           
                                                                                <td >                                                                                
                                                                                </td>           
                                                                                                                                                     
                                                                            </tr>
                                                                            <tr>
                                                                                <td class="textbold">
                                                                                </td>
                                                                                <td class="textbold" >
                                                                                    AOffice</td>
                                                                                <td>
                                                                                    <asp:DropDownList ID="ddlAOffice" runat="server" CssClass="dropdownlist" onkeyup="gotop(this.id)"
                                                                                        TabIndex="6" Width="176px">
                                                                                    </asp:DropDownList></td>
                                                                                <td class="textbold" >
                                                                                    Feedback Domain</td>
                                                                                <td style="width :195px"><asp:DropDownList ID="drpFeedbackDomain" runat="server" CssClass="dropdownlist" onkeyup="gotop(this.id)"
                                                                                        TabIndex="6" Width="166px">
                                                                                </asp:DropDownList></td>
                                                                                <td class="top" colspan="2" style="width: 26px">
                                                                                </td>
                                                                                <td>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td class="textbold">
                                                                                </td>
                                                                                <td class="textbold" >
                                                                                    Feedback Topic
                                                                                </td>
                                                                                <td class="textbold" colspan="3"><asp:DropDownList ID="drpFeedbackTopic" runat="server" CssClass="dropdownlist" onkeyup="gotop(this.id)"
                                                                                        TabIndex="6" Width="466px">
                                                                                </asp:DropDownList></td>
                                                                                <td class="top" colspan="2" style="width: 26px">
                                                                                </td>
                                                                                <td>
                                                                                </td>
                                                                            </tr>
                                                                             <tr>
                                                                                <td class="textbold">
                                                                                </td>
                                                                                <td class="textbold" >
                                                                                    <asp:DropDownList ID="ddlDateType" runat="server" CssClass="dropdownlist" onkeyup="gotop(this.id)"
                                                                                        TabIndex="9" Width="90px">
                                                                                        <asp:ListItem Text="" Value=""></asp:ListItem>
                                                                                        <asp:ListItem Text="Start Date" Value="1"></asp:ListItem>
                                                                                        <asp:ListItem Text="End Date" Value="2"></asp:ListItem>
                                                                                    </asp:DropDownList>&nbsp;
                                                                                    From</td>
                                                                                <td>
                                                                                    <asp:TextBox ID="txtStartDateFrom" runat="server" CssClass="textbox" Width="130px"
                                                                                        TabIndex="10" MaxLength="10"></asp:TextBox>
                                                                                    <img id="imgStartDateFrom" alt="" tabindex="11" src="../Images/calender.gif" title="Date selector"
                                                                                        style="cursor: pointer" />

                                                                                    <script type="text/javascript">
                                                  Calendar.setup({
                                                  inputField     :    '<%=txtStartDateFrom.clientId%>',
                                                  ifFormat       :    "%d/%m/%Y",
                                                  button         :    "imgStartDateFrom",
                                                 //align          :    "Tl",
                                                  singleClick    :    true
                                                 
                                                               });
                                                                                    </script>

                                                                                </td>
                                                                                <td class="textbold" style="width: 115px">
                                                                                    To
                                                                                </td>
                                                                                <td >
                                                                                    <asp:TextBox ID="txtStartDateTo" runat="server" CssClass="textbox" Width="130px"
                                                                                        TabIndex="12" MaxLength="10"></asp:TextBox>
                                                                                    <img id="imgStartDateTo" alt="" tabindex="13" src="../Images/calender.gif" title="Date selector" style="cursor: pointer" />

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
                                                                                <td class="top" colspan="2" style="width: 26px">
                                                                                </td>
                                                                                <td class="top" colspan="1">
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td class="textbold">
                                                                                </td>
                                                                                <td class="textbold" colspan="3">
                                                                                    Ack&nbsp;
                                                                                    <asp:CheckBox ID="ChkAck" runat="server" Width="130px" TabIndex="13" Visible="False" />
                                                                                    <asp:DropDownList ID="ddlFeedbackAck" onkeyup="gotop(this.id)" runat="server" CssClass="dropdownlist">
                                                                                        <asp:ListItem Text ="--All--" Value ="0" Enabled ="true" ></asp:ListItem>
                                                                                        <asp:ListItem Text ="Ack" Value ="0" Enabled ="true" ></asp:ListItem>
                                                                                        <asp:ListItem Text ="Not Ack" Value ="0" Enabled ="true" ></asp:ListItem>                                                                                                                                                                            
                                                                                    </asp:DropDownList></td>
                                                                                <td>
                                                                                </td>
                                                                                <td class="top" colspan="2" style="width: 26px">
                                                                                </td>
                                                                                <td>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                </td>
                                                                                <td class="gap" colspan="7">
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                </td>
                                                                                <td class="gap" colspan="7">
                                                                                    &nbsp;</td>
                                                                            </tr>
                                                                        </table>
                                                                    </asp:Panel>
                                                                </td>
                                                                <td class="center top " colspan="2" rowspan="1">
                                                                    <asp:Button ID="btnSearch" runat="server" TabIndex="3" CssClass="button topMargin"
                                                                        Text="Search" Width="100px" OnClientClick="return ValidateForm();" AccessKey="a" /><br />
                                                                    <asp:Button ID="btnReset" runat="server" TabIndex="3" CssClass="button topMargin"
                                                                        Text="Reset" Width="100px" AccessKey="r" /><br />
                                                                    <asp:Button ID="btnExport" TabIndex="3" runat="server" CssClass="button topMargin"
                                                                        Width="100px" Text="Export" AccessKey="e" /><br />
                                                                    <asp:Button ID="btnSave" TabIndex="3" runat="server" CssClass="button topMargin"
                                                                        Width="100px" Text="Save" AccessKey="s"></asp:Button>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                </td>
                                                                <td>
                                                                    <input id="hdCourseSessionFeedBack" runat="server" style="width: 1px" type="hidden" />
                                                                    <input id="hdData" runat="server" style="width: 1px" type="hidden" />
                                                                    <input type="hidden" id="hdEmployeePageName" style="width:1px" runat='server' />
                                                                    <input type="hidden" id="hdFeedbackTopicID" style="width:1px" runat='server' />
                                                                    <input type="hidden" id="hdFeedbackTopicIDIndex" style="width:1px" runat='server' />
                                                                    </td>                                                                    
                                                                <td class="center" colspan="2" rowspan="1">
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
            <tr id="trRedBoarder" runat="server" >
                <td class="redborder top">
                    <asp:GridView ID="gvFeedBack" runat="server" HeaderStyle-ForeColor="white" AutoGenerateColumns="False"
                        TabIndex="6" Width="1400px" AllowSorting="True">
                        <Columns>
                            <asp:BoundField HeaderText="Name" DataField="NAME" SortExpression="NAME">
                                <ItemStyle Width="13%" Wrap="true" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Agency Name" DataField="AGENCY" SortExpression="AGENCY">
                                <ItemStyle Width="14%" Wrap="true" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Office ID" DataField="OFFICEID" SortExpression="OFFICEID">
                                <ItemStyle Width="10%" Wrap="true" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="FeedBack" DataField="FEEDBACK" SortExpression="FEEDBACK">
                                <ItemStyle Width="9%" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Course" DataField="COURSE" SortExpression="COURSE">
                                <ItemStyle Width="15%" Wrap="true" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Domain " DataField="DOMAIN" SortExpression="DOMAIN">
                                <ItemStyle Width="12%" Wrap="true" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Topic" DataField="TOPIC" SortExpression="TOPIC">
                                <ItemStyle Width="15%" Wrap="true" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Trainer" DataField="TRAINER" SortExpression="TRAINER">
                                <ItemStyle Width="8%" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Start Date" DataField="STARTDATE" SortExpression="STARTDATE">
                                <ItemStyle Width="8%" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Aoffice" DataField="AOFFICE" SortExpression="AOFFICE">
                                <ItemStyle Width="10%" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Status" DataField="STATUS" SortExpression="STATUS">
                                <ItemStyle Width="7%" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Comment" DataField="TR_COMMENT" SortExpression="TR_COMMENT">
                                <ItemStyle Width="15%" Wrap="true" />
                            </asp:BoundField>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    Ack
                                    <input type="checkbox" id="chkAllSelect" name="chkAllSelect" onclick="SelectAllAck();" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkAck" runat="server" />
                                    <asp:HiddenField ID="hdTR_COURSEP_ID" runat="server" Value='<%#Eval("TR_COURSEP_ID")%>' />
                                    <asp:HiddenField ID="hdTR_CVALTOPIC_ID" runat="server" Value='<%#Eval("TR_CVALTOPIC_ID")%>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <AlternatingRowStyle CssClass="lightblue" />
                        <RowStyle CssClass="textbold" />
                        <HeaderStyle CssClass="Gridheading center" />
                    </asp:GridView>
                    <asp:Panel ID="pnlPaging" runat="server" Visible="false" Width="80%">
                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                            <tr class="paddingtop paddingbottom">
                                <td style="width: 30%" class="left">
                                    <span class="textbold"><b>&nbsp;No. of records found</b></span>&nbsp;&nbsp;<asp:TextBox
                                        ID="txtTotalRecordCount" runat="server" Width="105px" CssClass="textboxgrey"
                                        ReadOnly="True"></asp:TextBox></td>
                                <td style="width: 25%" class="right">
                                    <asp:LinkButton ID="lnkPrev" CssClass="LinkButtons" runat="server" CommandName="Prev"><< Prev</asp:LinkButton></td>
                                <td style="width: 20%" class="center">
                                    <span class="textbold"><b>Page No</b></span>&nbsp;&nbsp;<asp:DropDownList onkeyup="gotop(this.id)"
                                        ID="ddlPageNumber" Width="70px" CssClass="dropdownlist" runat="server" AutoPostBack="True">
                                    </asp:DropDownList></td>
                                <td style="width: 25%" class="left">
                                    <asp:LinkButton ID="lnkNext" runat="server" CssClass="LinkButtons" CommandName="Next">Next >></asp:LinkButton></td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
            </tr>
            <!-- Code for Paging by Mukund-->
            <tr>
                <td colspan="6" valign="top">
                </td>
            </tr>
            <!-- Code for Paging by Mukund-->
        </table>
    </form>
</body>
</html>
