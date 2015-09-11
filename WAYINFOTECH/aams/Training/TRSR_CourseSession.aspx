<%@ Page Language="VB" AutoEventWireup="false" ValidateRequest="false"     CodeFile="TRSR_CourseSession.aspx.vb" Inherits="Training_TRSR_CourseSession" MaintainScrollPositionOnPostBack="true"  %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" media="all" href="../Calender/calendar-blue.css" title="win2k-cold-1" />
<script type="text/javascript" src="../Calender/calendar.js"></script>
<script type="text/javascript" src="../JavaScript/AAMS.js"></script>

<!-- import the language module -->

<script type="text/javascript" src="../Calender/lang/calendar-en.js"></script>
<script type="text/javascript" src="../Calender/calendar-setup.js"></script>
    <title>AAMS::Training::Search Course Session</title>
    <script language="javascript" type="text/javascript">
      function EditFunction(CourseSessionID)
			{
				 window.location.href="TRUP_CourseSession.aspx?Action=U&CourseSessionID=" +CourseSessionID
				 return false;
			}
			
	function DeleteFunction(CourseSessionID)
	{
		 			 
		 if (confirm("Are you sure you want to delete?")==true)
            {    
               document.getElementById('hdDeleteID').value = CourseSessionID
               return true;        
            }
            return false;
	}

    
    </script>
</head>
<body>
    <form id="form1" runat="server" defaultfocus="txtAgency" defaultbutton="btnSearch">
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
                            <span class="menu"> Training -&gt;</span><span class="sub_menu">Course Session Search</span></td>
                        </tr>
                        <tr>
                            <td class="heading center" >Search Course Session</td>
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
                                            <td class="textbold">Agency</td>
                                            <td colspan="3">
                                                <asp:TextBox ID="txtAgency" runat="server" CssClass="textbox" MaxLength="50" TabIndex="2"
                                                    Width="530px"></asp:TextBox>
                                                <img id="Img2" runat="server" alt="Select & Add Agency Name" onclick="PopupPageCourseSession(1)"
                                                    src="../Images/lookup.gif"  style="cursor:pointer;" /></td>
                                            <td class="left" style="width: 12%">
                                            <asp:Button ID="btnSearch" CssClass="button" runat="server" Text="Search" TabIndex="3" AccessKey="a" /></td>
                                        </tr>
                                        <tr>
                                            <td></td>
                                            <td class="textbold"> Course</td>                                                                               
                                            <td colspan="3">
                                             <asp:DropDownList ID="ddlCourse" runat="server" CssClass="dropdownlist" Width="536px" TabIndex="2" onkeyup="gotop(this.id)">
                                                </asp:DropDownList>&nbsp;
                                            </td>
                                            <td style="width: 12%;" class="left">
                                                <asp:Button ID="btnNew" CssClass="button" runat="server" Text="New" TabIndex="3" AccessKey="n" /></td>
                                        </tr>
                                          <tr>
                                            <td></td>
                                            <td class="textbold">Training Room</td>
                                            <td colspan="3">
                                                <asp:TextBox ID="txtTrainingRoom" runat="server" CssClass="textboxgrey" MaxLength="50" TabIndex="2"
                                                    Width="530px" ReadOnly="True"></asp:TextBox>
                                                <img id="Img1" runat="server" alt="Select & Add Training Room" onclick="PopupPageCourseSession(3)"
                                                    src="../Images/lookup.gif" style="cursor:pointer;" /></td>
                                            <td class="left">
                                                <asp:Button ID="btnExport" runat="server" CssClass="button" TabIndex="3" Text="Export" AccessKey="e" /></td>
                                        </tr>
                                        <tr>
                                            <td></td>
                                            <td class="textbold">Participant Name</td>
                                            <td colspan="3">
                                                <asp:TextBox ID="txtParticipant" runat="server" CssClass="textbox" TabIndex="2" Width="530px" MaxLength="50"></asp:TextBox>
                                                </td>
                                            <td class="left">
                                               <asp:Button ID="btnReset" CssClass="button" runat="server" Text="Reset" TabIndex="3" AccessKey="r" /></td>
                                        </tr>
                                        <tr>
                                            <td></td>
                                            <td class="textbold">Trainer 1</td>
                                            <td>
                                                <asp:TextBox ID="txtTrainer1" runat="server" CssClass="textbox" MaxLength="40" TabIndex="2"
                                                    Width="170px"></asp:TextBox>  <IMG style="CURSOR: pointer" id="Img3" onclick="PopupPageTrainerForTraining(1,'txtTrainer1')" 
alt="Select & Add Employee" src="../Images/lookup.gif" runat="server" /></td>
                                            <td class="textbold">Trainer 2</td>
                                            <td>
                                                <asp:TextBox ID="txtTrainer2" runat="server" CssClass="textbox" MaxLength="40" TabIndex="2"
                                                    Width="170px"></asp:TextBox>  <IMG style="CURSOR: pointer" id="Img4" onclick="PopupPageTrainerForTraining(1,'txtTrainer2')" 
alt="Select & Add Employee" src="../Images/lookup.gif" runat="server" /></td>
                                            <td ></td>
                                        </tr>
                                        <tr>
                                            <td></td>
                                            <td class="textbold">AOffice</td>
                                            <td>
                                                <asp:DropDownList ID="ddlAOffice" runat="server" CssClass="dropdownlist" Width="176px" TabIndex="2" onkeyup="gotop(this.id)">
                                                  </asp:DropDownList></td>
                                            <td class="textbold">Region</td>
                                            <td>
                                                <asp:DropDownList ID="ddlRegion" runat="server" CssClass="dropdownlist" Width="176px" TabIndex="2" onkeyup="gotop(this.id)">
                                                 </asp:DropDownList></td>
                                            <td ></td>
                                        </tr>
                                        <tr>
                                            <td></td>
                                            <td class="textbold">OfficeID</td>
                                            <td>
                                                <asp:TextBox ID="txtOfficeID" runat="server" CssClass="textbox" MaxLength="10" 
                                                    Width="170px" TabIndex="2"></asp:TextBox></td>
                                            <td ></td>
                                            <td></td>
                                            <td ></td>
                                        </tr>
                                          <tr>
                                            <td></td>
                                            <td class="textbold"> 
                                                <asp:DropDownList ID="ddlDateType" runat="server" CssClass="dropdownlist" Width="90px" TabIndex="2" onkeyup="gotop(this.id)">
                                                <asp:ListItem Value="0" Text=""></asp:ListItem>
                                                <asp:ListItem Text="Start Date" Value="1"></asp:ListItem>
                                                <asp:ListItem Text="End Date" Value="2"></asp:ListItem>
                                                </asp:DropDownList></td>
                                            <td class="textbold">
                                                From&nbsp; &nbsp;<asp:TextBox ID="txtStartDateFrom" runat="server" CssClass="textbox" Width="132px" TabIndex="2" MaxLength="10"></asp:TextBox>
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
                                                To</td>
                                            <td class="textbold">
                                                <asp:TextBox ID="txtStartDateTo" runat="server" CssClass="textbox" Width="132px" TabIndex="2" MaxLength="10"></asp:TextBox>
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
                                            <td ></td>
                                        </tr>
                                          <tr>
                                            <td>  </td>
                                             <td class="textbold" >   Internal Session</td>  
                                               <td>
                                                <asp:CheckBox ID="chkInternalSession" runat="server" TabIndex="2" /></td><td class="textbold" > Show On Web</td><td>
                                                <asp:CheckBox ID="chkShowOnWeb" runat="server" CssClass="textbold" TabIndex="2" /></td>
                                            <td>
                                            </td>
                                        </tr>                           
                                        <tr>
                                            <td ></td>
                                            <td >       </td>
                                                <td  colspan="4">
                                            <input id="hdDeleteId" runat="server" style="width: 1px" type="hidden" enableviewstate="true"/>
                                                <input id="hdRecordOnCurrentPage" runat="server" style="width: 6px" type="hidden" />
                                                <asp:HiddenField ID="hdCourseLCode" runat ="server" />
                                                <asp:HiddenField ID="hdRoomID" runat ="server" />
                                                <asp:HiddenField ID="hdCourseStaff" runat ="server" />
                                                <asp:HiddenField ID="hdParticipant" runat ="server" />
                                                <asp:HiddenField ID="hdEmployeeID" runat ="server" />
                                                
<input type="hidden" id="hdEmployeePageName" style="width:1px" runat='server' />
                                            </td>
                                        </tr>
                                                                        <tr>
                                                                            <td colspan="6" ></td>
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
<td class="redborder">
   <asp:GridView  ID="gvCourseSession" runat="server"  AutoGenerateColumns="False" Width="1050px" TabIndex="6" EnableViewState="False" CellPadding="0" AllowSorting="True" HeaderStyle-ForeColor="white">
    <Columns>
        <asp:BoundField HeaderText="Course" DataField="TR_COURSE_NAME" SortExpression="TR_COURSE_NAME" >
         <ItemStyle Width="22%" />
        </asp:BoundField>
        <asp:BoundField HeaderText="Training Room" DataField="TR_CLOCATION_NAME" SortExpression="TR_CLOCATION_NAME" >
        <ItemStyle Width="21%" />
        </asp:BoundField>
        <asp:BoundField HeaderText="Trainer 1" DataField="TRAINER1" SortExpression="TRAINER1" >
        <ItemStyle Width="9%" />
        </asp:BoundField>
        <asp:BoundField HeaderText="Trainer 2" DataField="TRAINER2" SortExpression="TRAINER2" >
        <ItemStyle Width="9%" />
        </asp:BoundField>
        <asp:BoundField HeaderText="Start Date" DataField="TR_COURSES_EXPECT_DATE" SortExpression="TR_COURSES_EXPECT_DATE" >
        <ItemStyle Width="8%" />
        </asp:BoundField>
        <asp:BoundField HeaderText="End Date" DataField="TR_COURSES_END_DATE" SortExpression="TR_COURSES_END_DATE" >
        <ItemStyle Width="8%" />
        </asp:BoundField>
        <asp:BoundField HeaderText="Start Time" DataField="TR_COURSES_START_TIME" SortExpression="TR_COURSES_START_TIME" >
        <ItemStyle Width="4%" />
        </asp:BoundField>
        <asp:BoundField HeaderText="End Time" DataField="TR_COURSES_END_TIME" SortExpression="TR_COURSES_END_TIME" >
        <ItemStyle Width="4%" />
        </asp:BoundField>
        <asp:BoundField HeaderText="Actual No of Participants" DataField="TR_COURSES_NBPART" SortExpression="TR_COURSES_NBPART"  ItemStyle-CssClass="center">
        <ItemStyle Width="4%" />
        </asp:BoundField>
         <asp:BoundField HeaderText="Shortlist" DataField="ActualNoParticipant" SortExpression="ActualNoParticipant"  ItemStyle-CssClass="center">
        <ItemStyle Width="4%" />
        </asp:BoundField>
        <asp:TemplateField HeaderText="Action">
<ItemTemplate>
<asp:LinkButton ID="lnkSelect" runat="server" CommandName ="SelectX" Text ="Select" CssClass="LinkButtons"></asp:LinkButton>&nbsp;
<asp:LinkButton ID="lnkEdit" runat="server" CommandName ="EditX" Text ="Edit" CssClass="LinkButtons"></asp:LinkButton>&nbsp;
<asp:LinkButton ID="lnkDelete" runat="server" CommandName ="DeleteX" Text ="Delete" CssClass="LinkButtons"></asp:LinkButton>&nbsp;
<asp:HiddenField ID="hdCourseSessionID" runat="server" Value='<%#Eval("TR_COURSES_ID")%>' />   
<asp:HiddenField ID="hdCourseID" runat="server" Value='<%#Eval("TR_COURSE_ID")%>' />   

</ItemTemplate>
            <ItemStyle Width="7%" />
</asp:TemplateField>                                                  

</Columns>
    <AlternatingRowStyle CssClass="lightblue" />
    <RowStyle CssClass="textbold" />
    <HeaderStyle CssClass="Gridheading" />                                                    
 </asp:GridView>
    <asp:Panel ID="pnlPaging" runat="server" Visible="false" Width="80%" CssClass="left">
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr class="paddingtop paddingbottom">
                <td style="width: 30%" class="left">
                    <span class="textbold"><b>&nbsp;No. of records found</b></span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:TextBox
                        ID="txtTotalRecordCount" runat="server" Width="80px" CssClass="textboxgrey" ReadOnly="True"></asp:TextBox></td>
                <td style="width: 25%" class="right">
                    <asp:LinkButton ID="lnkPrev" CssClass="LinkButtons" runat="server" CommandName="Prev"><< Prev</asp:LinkButton></td>
                <td style="width: 20%" class="center">
                    <span class="textbold"><b>Page No</b></span>&nbsp;&nbsp;<asp:DropDownList ID="ddlPageNumber"
                        Width="70px" CssClass="dropdownlist" runat="server" AutoPostBack="True">
                    </asp:DropDownList></td>
                <td style="width: 25%" class="left">
                    <asp:LinkButton ID="lnkNext" runat="server" CssClass="LinkButtons" CommandName="Next">Next >></asp:LinkButton></td>
            </tr>
            <tr class="paddingtop paddingbottom">
                <td style="width: 30%" class="left">
                    <span class="textbold"><b>&nbsp;Actual No. of Participants</b></span>&nbsp;&nbsp;<asp:TextBox
                        ID="txtTotalActualPartRecordCount" runat="server" Width="80px" CssClass="textboxgrey"
                        ReadOnly="True"></asp:TextBox></td>
                <td style="width: 25%" class="right">
                </td>
                <td style="width: 20%" class="center">
                </td>
                <td style="width: 25%" class="left">
                </td>
            </tr>
        </table>
    </asp:Panel>
    </td></tr>
    
</table>
</td></tr>
</table>

    </div>
    </form>
    </body>
</html>

