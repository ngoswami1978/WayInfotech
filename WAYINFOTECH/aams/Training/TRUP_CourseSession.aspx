<%@ Page Language="VB" AutoEventWireup="false" CodeFile="TRUP_CourseSession.aspx.vb"
    Inherits="Training_TRUP_CourseSession" EnableEventValidation="false" ValidateRequest="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>AAMS::Training::Manage Course Session</title>
    <link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" media="all" href="../Calender/calendar-blue.css"
        title="win2k-cold-1" />
    <script src="../JavaScript/AAMS.js" type="text/javascript"></script>
    <link href="../Calender/calendar-blue.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../Calender/calendar.js"></script>
<script type="text/javascript" src="../Calender/lang/calendar-en.js"></script>
    <script type="text/javascript" src="../Calender/calendar-setup.js"></script>
    
    <script language ="javascript" type="text/javascript">
      function  ShowWorkOrderHistory()
  {
    
  var ptrsID=document.getElementById("hdEnPageCourseSessionID").value.trim();

   var type="TRHR_CourseSession.aspx?TR_COURSES_ID="+ptrsID
    window.open(type,"aaSessionHistory",'height=600,width=900,top=30,left=20,scrollbars=1,status=1');     
    return false;
  }
    </script>
</head>
<body onload="HideShowManageCourseSession()">
    <form id="form1" runat="server" defaultbutton="btnSave">
        <table width="860px" class="border_rightred left">
            <tr>
                <td class="top" style="height: 699px">
                    <table width="100%">
                        <tr>
                            <td class="top">
                                <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td class="top left">
                                            <span class="menu">Training-></span><span class="sub_menu">Course Session</span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="heading center">Manage Course Session</td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td class="top">
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td class="top" colspan="2"><asp:HiddenField ID="lblPanelClick" runat="server"></asp:HiddenField>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="top" style="height: 22px; width: 80%">
                                            <asp:Repeater ID="theTabStrip" runat="server">
                                                <ItemTemplate>
                                                    <asp:Button ID="Button1" CssClass="headingtabactive" runat="server" Text="<%# Container.DataItem %>" />&nbsp;
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </td>
                                        <td class="right" style="width: 20%">&nbsp; &nbsp; &nbsp;&nbsp;
                                        </td>
                                    </tr>
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
                                                                    <asp:Panel ID="pnlSession" runat="server" Width="100%">
                                                                        <table width="100%" border="0" class="left" cellpadding="2" cellspacing="0">
                                                                            <tr>
                                                                                <td style="width: 2%"></td>
                                                                                <td class="gap" colspan="7"></td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td class="textbold"></td>
                                                                                <td class="textbold" style="width: 20%">
                                                                                    Course Type</td>
                                                                                <td colspan="3">
                                                                                    <asp:DropDownList ID="ddlCourseType" runat="server" TabIndex="2" Width="174px" CssClass="textbold"
                                                                                        AutoPostBack="True" onkeyup="gotop(this.id)">
                                                                                        <asp:ListItem Selected="True" Value="">--All--</asp:ListItem>
                                                                                        <asp:ListItem Value="1">Web</asp:ListItem>
                                                                                    </asp:DropDownList></td>
                                                                                <td colspan="2"></td>
                                                                                <td ></td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td ></td>
                                                                                <td class="textbold" style="width: 20%">
                                                                                    Course Title<span class="Mandatory">*</span>
                                                                                </td>
                                                                                <td colspan="3">
                                                                                    <asp:DropDownList ID="ddlCourseTitle" runat="server" TabIndex="2" Width="536px" CssClass="textbold"
                                                                                        onchange="FillLevelManageCourseSession()" onkeyup="gotop(this.id)">
                                                                                    </asp:DropDownList></td>
                                                                                <td class="top" colspan="2">
                                                                                    <img id="Img1" runat="server" alt="Select & Add Training Room" onclick="PopupPageManageCourseSession(2)"
                                                                                        src="../Images/lookup.gif" style="vertical-align: text-top; cursor: pointer;"
                                                                                        class="topMargin" visible="false" /></td>
                                                                                <td ></td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td ></td>
                                                                                <td class="textbold" style="width: 20%">
                                                                                    Course Level</td>
                                                                                <td style="width: 30%">
                                                                                    <asp:TextBox ID="txtCourseLevel" runat="server" CssClass="textboxgrey" ReadOnly="True"
                                                                                        TabIndex="20" Width="170px"></asp:TextBox></td>
                                                                                <td class="textbold" style="width: 15%">
                                                                                    Course Duration</td>
                                                                                <td style="width: 22%">
                                                                                    <asp:TextBox ID="txtDuration" runat="server" CssClass="textboxgrey" ReadOnly="True"
                                                                                        TabIndex="20" Width="170px"></asp:TextBox></td>
                                                                                <td  colspan="2"></td>
                                                                                <td ></td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="width: 2%">
                                                                                    <input id="hdTrainingRoomPage" style="width: 2px" type="hidden" runat="server" />
                                                                                </td>
                                                                                <td class="textbold">
                                                                                    Training Room <span class="Mandatory">*</span>
                                                                                </td>
                                                                                <td colspan="3" class="top">
                                                                                    <asp:TextBox ID="txtTrainingRoom" runat="server" CssClass="textboxgrey" Width="532px"
                                                                                        ReadOnly="True" TabIndex="20" Height="50px" Rows="4" TextMode="MultiLine"></asp:TextBox>
                                                                                </td>
                                                                                <td class="top left" colspan="2" rowspan="4" style="width: 7%">
                                                                                    <img id="Img2" runat="server" alt="Select & Add Training Room" onclick="PopupPageManageCourseSession(1)"
                                                                                        src="../Images/lookup.gif" style="vertical-align: text-top; cursor: pointer;"
                                                                                        class="topMargin" />
                                                                                    &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;</td>
                                                                                <td class="top left" rowspan="10" style="width: 2%">
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td ></td>
                                                                                <td class="textbold">1A Office</td>
                                                                                <td>
                                                                                    <asp:DropDownList ID="ddlAOffice" runat="server" AutoPostBack="True" CssClass="dropdownlist"
                                                                                        onkeyup="gotop(this.id)" TabIndex="2" Width="176px">
                                                                                    </asp:DropDownList></td>
                                                                                <td class="textbold">Internal Session</td>
                                                                                <td><asp:CheckBox ID="chkInternalSession" runat="server" /></td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td ></td>
                                                                                <td class="textbold">
                                                                                    Trainer 1 <span class="Mandatory">*</span>
                                                                                </td>
                                                                                <td><asp:DropDownList ID="ddlTrainer1" runat="server" TabIndex="2" Width="174px" CssClass="textbold"
                                                                                        onkeyup="gotop(this.id)">
                                                                                    </asp:DropDownList></td>
                                                                                <td class="textbold">Trainer 2
                                                                                </td>
                                                                                <td>
                                                                                    <asp:DropDownList ID="ddlTrainer2" runat="server" TabIndex="2" Width="174px" CssClass="textbold"
                                                                                        onkeyup="gotop(this.id)">
                                                                                    </asp:DropDownList>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td class="textbold"><input id="hdSubCategory" runat="server" style="width: 1px" type="hidden" /></td>
                                                                                <td class="textbold">Max No. of Participant
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="txtParticipantMaxNo" runat="server" CssClass="textboxgrey" ReadOnly="True"
                                                                                        TabIndex="20" Width="170px"></asp:TextBox></td>
                                                                                <td class="textbold">
                                                                                    Actual No</td>
                                                                                <td>
                                                                                    <asp:TextBox ID="txtActualNo" runat="server" CssClass="textboxgrey" ReadOnly="True"
                                                                                        TabIndex="20" Width="170px"></asp:TextBox>&nbsp;</td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td ></td>
                                                                                <td class="textbold">
                                                                                    Start Date &nbsp;<span class="Mandatory">*</span></td>
                                                                                <td>
                                                                                    <asp:TextBox ID="txtStartDate" runat="server" CssClass="textbox" Width="170px" TabIndex="2"></asp:TextBox>
                                                                                    <img id="imgStartDate" alt="" tabindex="2" src="../Images/calender.gif" title="Date selector"
                                                                                        style="cursor: pointer" runat="server" />

                                                                                    <script type="text/javascript">
                                                  Calendar.setup({
                                                  inputField     :    '<%=txtStartDate.clientId%>',
                                                  ifFormat       :    "%d/%m/%Y",
                                                  button         :    "imgStartDate",
                                                 //align          :    "Tl",
                                                  singleClick    :    true
                                                
                                                               });
                                                                                    </script>

                                                                                    &nbsp;
                                                                                </td>
                                                                                <td class="textbold">
                                                                                    Start Time(HH:MM) <span class="Mandatory">*</span></td>
                                                                                <td>
                                                                                    <asp:TextBox ID="txtStartTimeHH" runat="server" CssClass="textbox" TabIndex="2" Width="71px" onkeyup='checknumeric(this.id)' MaxLength="2"></asp:TextBox>
                                                                                    :

                                                                                    <asp:TextBox ID="txtStartTimeMM" runat="server" CssClass="textbox" TabIndex="2" Width="71px" onkeyup='checknumeric(this.id)' MaxLength="2"></asp:TextBox></td>
                                                                                <td></td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td ></td>
                                                                                <td class="textbold">
                                                                                    End Date <span class="Mandatory">*</span>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="txtEndDate" runat="server" CssClass="textbox" Width="170px" TabIndex="2" ></asp:TextBox>
                                                                                    <img id="imgEndDate" alt="" tabindex="2" src="../Images/calender.gif" title="Date selector"
                                                                                        style="cursor: pointer" runat="server" />

                                                                                    <script type="text/javascript">
                                                  Calendar.setup({
                                                  inputField     :    '<%=txtEndDate.clientId%>',
                                                  ifFormat       :    "%d/%m/%Y",
                                                  button         :    "imgEndDate",
                                                 //align          :    "Tl",
                                                  singleClick    :    true
                                                 
                                                               });
                                                                                    </script>

                                                                                </td>
                                                                                <td class="textbold">
                                                                                    End Time(HH:MM) <span class="Mandatory">*</span></td>
                                                                                <td>
                                                                                    <asp:TextBox ID="txtEndTimeHH" runat="server" CssClass="textbox" TabIndex="2" Width="71px" onkeyup='checknumeric(this.id)' MaxLength="2"></asp:TextBox>
                                                                                    :
                                                                                    <asp:TextBox ID="txtEndTimeMM" runat="server" CssClass="textbox" TabIndex="2" Width="71px" onkeyup='checknumeric(this.id)' MaxLength="2"></asp:TextBox></td>
                                                                                <td>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td >&nbsp;</td>
                                                                                <td class="textbold">
                                                                                    Required Configuration Or Notes</td>
                                                                                <td colspan="3">
                                                                                    <asp:TextBox ID="txtNotes" runat="server" CssClass="textbox" Height="50px" Rows="4"
                                                                                        TabIndex="20" TextMode="MultiLine" Width="532px"></asp:TextBox></td>
                                                                                <td colspan="1">
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td ></td>
                                                                                <td ></td>
                                                                                <td colspan="3"></td>
                                                                                <td ></td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td ></td>
                                                                                <td ></td>
                                                                                <td>
                                                                                    <input id="hdCourseID" runat="server" style="width: 1px" type="hidden" />
                                                                                    <input id="hdLocationId" runat="server" style="width: 1px" type="hidden" />
                                                                                    <input id="hdPageStatus" runat="server" style="width: 1px" type="hidden" />
                                                                                    <input id="hdPageCourseSessionID" runat="server" style="width: 1px" type="hidden" />
                                                                                    <input id="hdCourseSessionCourseLevel" runat="server" style="width: 1px" type="hidden" />
                                                                                    <input id="hdDuration" runat="server" style="width: 1px" type="hidden" />
                                                                                    <input id="hdNoOfTest" runat="server" style="width: 1px" type="hidden" />
                                                                                    <input id="hdEnPageCourseSessionID" runat="server" style="width: 1px" type="hidden" />
                                                                                    <input type="hidden" id="hdEmployeePageName" style="width:1px" runat='server' />
                                                                                </td>
                                                                                <td ></td>
                                                                                <td>&nbsp;</td>
                                                                                <td></td>
                                                                            </tr>
                                                                        </table>
                                                                    </asp:Panel>
                                                                </td>
                                                                <td class="center top " colspan="2" rowspan="1">
                                                                    <asp:Button ID="btnSave" runat="server" TabIndex="3" CssClass="button topMargin"
                                                                        Text="Save" Width="100px" OnClientClick="return ManageCourseSessionPage()" AccessKey="s" /><br />
                                                                    <asp:Button ID="btnNew" runat="server" TabIndex="3" CssClass="button topMargin" Text="New"
                                                                        Width="100px" AccessKey="n" /><br />
                                                                        
                                                                       
                                                                    <asp:Button ID="btnReset" runat="server" TabIndex="3" CssClass="button topMargin"
                                                                        Text="Reset" Width="100px" AccessKey="r" /><br />
                                                                        
                                                                         <asp:Button ID="btnHistory" runat="server" TabIndex="3" CssClass="button topMargin" OnClientClick ="return ShowWorkOrderHistory();"
                                                                        Text="History" Width="100px" AccessKey="h" /><br />
                                                                       
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td></td>
                                                                <td></td>
                                                                <td  colspan="2" ></td>
                                                            </tr>
                                                            <tr>
                                                                <td class="ErrorMsg" style="width: 10%">
                                                                    Field Marked * are Mandatory</td>
                                                                <td>&nbsp; &nbsp;</td>
                                                                <td></td>
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
                    <asp:HiddenField ID="hdTabType" runat="server" Value="0" />
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
