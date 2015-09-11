<%@ Page Language="VB" AutoEventWireup="false" CodeFile="TRUP_Register.aspx.vb" Inherits="Training_TRUP_Register" EnableEventValidation="false" ValidateRequest="false" MaintainScrollPositionOnPostback="true" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>AAMS::Training::Manage Course Session</title>
    <link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />
     <link rel="stylesheet" type="text/css" media="all" href="../Calender/calendar-blue.css" title="win2k-cold-1" />
    <script src="../JavaScript/AAMS.js" type="text/javascript"></script>
   <link href="../Calender/calendar-blue.css" rel="stylesheet" type="text/css" />
    <!-- import the calendar script -->
<script type="text/javascript" src="../Calender/calendar.js"></script>
<!-- import the language module -->
<script type="text/javascript" src="../Calender/lang/calendar-en.js"></script>
<!-- import the calendar setup module -->

<script type="text/javascript" src="../Calender/calendar-setup.js"></script>
<script language="javascript" type="text/javascript" >

      function  ShowWorkOrderHistory(TR_COURSEP_ID)
  {
    
  var ptrsID=document.getElementById("hdEnPageCourseSessionID").value.trim();

   var type="TRHR_CourseSessionRegister.aspx?TR_COURSES_ID=" + ptrsID + "&TR_COURSEP_ID=" + TR_COURSEP_ID


    window.open(type,"aaSessionHistoryRegister",'height=600,width=900,top=30,left=20,scrollbars=1,status=1');     
    return false;
  }
   
</script>

</head>
<body onload="HideShowRegister()" >
    <form id="form1" runat="server" defaultbutton="btnSave" >
        <table   class="left">
            <tr>
                <td class="top">
                    <table  width="860px">
                        <tr>
                            <td class="top">
                                <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td class="top left">
                                            <span class="menu">Training-></span><span class="sub_menu">Course Session</span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="heading center">
                                            Manage Course Session</td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td class="top">
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td class="top" colspan="2">
                                            <asp:HiddenField ID="lblPanelClick" runat="server"></asp:HiddenField>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="top" style="height: 22px;width:80%">
                                            <asp:Repeater ID="theTabStrip" runat="server">
                                                <ItemTemplate>
                                                    <asp:Button ID="Button1" CssClass="headingtabactive" runat="server" Text="<%# Container.DataItem %>" />&nbsp;
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </td>
                                        <td class="right" style="width:20%">
                                            &nbsp; &nbsp; &nbsp;&nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td  class="redborder top" colspan="2" style="width:100%">
                                            <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td class="center TOP"   >
                                                    <asp:Label ID="lblError" CssClass="ErrorMsg" runat="server" EnableViewState="False"></asp:Label>
                                                        </td>
                                                </tr>
                                                <tr>
                                                    <td class="top" style="width:100%;">
                                                  
                                                            <table width="100%" border="0" class="left" cellpadding="2" cellspacing="0">
                                                                <tr>
                                                                    <td class="textbold" style="width:90%" >
                                                                   
                                                                    <asp:Panel ID="pnlRegister" runat="server" Width="100%" >
                                                                    <table width="100%" border="0" class="left" cellpadding="2" cellspacing="0">
                                                                        <tr>
                                                                            <td style="width: 2%">
                                                                            </td>
                                                                            <td class="gap" colspan="7">
                                                                            
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="textbold">
                                                                            </td>
                                                                            <td class="textbold" style="width: 20%">
                                                                            </td>
                                                                            <td style="width: 30%">
                                                                            </td>
                                                                            <td class="textbold" style="width: 15%">
                                                                            </td>
                                                                            <td style="width: 22%">
                                                                            </td>
                                                                            <td class="top" colspan="2" style="width: 7%">
                                                                            </td>
                                                                            <td class="top" colspan="1" style="width: 2%">
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                    <td class="textbold">
                                                                    </td>
                                                                    <td class="textbold" style="width: 20%">
                                                                        Course Title</td>
                                                                    <td colspan="3">
                                                                        <asp:TextBox ID="txtCourseTitleRegisterTab" runat="server" CssClass="textboxgrey" ReadOnly="True"
                                                                            TabIndex="20" Width="532px"></asp:TextBox></td>
                                                                     <td class="top" colspan="2" style="width: 7%" >
                                                                        </td>
                                                                      <td class="top" colspan="1" style="width: 2%">
                                                                      </td>
                                                                </tr>
                                                                       
                                                                        <tr>
                                                                            <td class="textbold">
                                                                            </td>
                                                                            <td class="textbold" style="width: 20%">
                                                                                Training Room</td>
                                                                            <td colspan="3">
                                                                                <asp:TextBox ID="txtTrainingRoomRegisterTab" runat="server" CssClass="textboxgrey" Height="50px" ReadOnly="True"
                                                                                    Rows="4" TabIndex="20" TextMode="MultiLine" Width="532px"></asp:TextBox></td>
                                                                            <td class="top" colspan="2" style="width: 7%">
                                                                            </td>
                                                                            <td class="top" colspan="1" style="width: 2%">
                                                                            </td>
                                                                        </tr>
                                                                         <tr>
                                                                            <td class="textbold">
                                                                            </td>
                                                                            <td class="textbold" >
                                                                                Start Date</td>
                                                                            <td >
                                                                                <asp:TextBox ID="txtStartDateRegisterTab" runat="server" CssClass="textboxgrey" ReadOnly="True"
                                                                                    TabIndex="20" Width="170px"></asp:TextBox></td>
                                                                            <td class="textbold" >
                                                                                Course Level</td>
                                                                            <td >
                                                                        <asp:TextBox ID="txtCourseLevelRegisterTab" runat="server" CssClass="textboxgrey" ReadOnly="True"
                                                                            TabIndex="20" Width="170px"></asp:TextBox></td>
                                                                            <td colspan="2" >
                                                                            </td>
                                                                            <td   >
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td >
                                                                            </td>
                                                                            <td class="textbold">
                                                                                Max No. of Participant
                                                                            </td>
                                                                            <td>
                                                                                <asp:TextBox ID="txtMaxNoParticipantRegisterTab" runat="server" CssClass="textboxgrey" ReadOnly="True"
                                                                                    TabIndex="20" Width="170px"></asp:TextBox></td>
                                                                            <td class="textbold">
                                                                                ACO
                                                                                Trainers</td>
                                                                            <td>
                                                                                <asp:TextBox ID="txtNMCTrainersRegisterTab" runat="server" CssClass="textboxgrey" ReadOnly="True"
                                                                                    TabIndex="20" Width="170px"></asp:TextBox></td>
                                                                            <td colspan="2">
                                                                            </td>
                                                                            <td  >
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="textbold">
                                                                            </td>
                                                                            <td class="textbold">
                                                                                Total Marks</td>
                                                                            <td>
                                                                                <asp:TextBox ID="txtTotalMarks" runat="server" CssClass="textboxgrey" ReadOnly="True"
                                                                                    TabIndex="20" Width="170px"></asp:TextBox></td>
                                                                            <td class="textbold">
                                                                            </td>
                                                                            <td>
                                                                            </td>
                                                                            <td class="top" colspan="2">
                                                                            </td>
                                                                            <td class="top" colspan="1">
                                                                            </td>
                                                                        </tr>
                                                                        
                                                                        
                                                                        <tr>
                                                                            <td style="width: 2%">
                                                                            </td>
                                                                            <td class="gap" colspan="7">
                                                                             <input id="hdDefaultStatusID" runat="server" style="width: 1px" type="hidden" />
                                                                            <input id="hdCourseSessionEmployeePopup" runat="server" style="width: 1px" type="hidden" />
                                                <input id="hdCourseSessionPeoplePopup" runat="server" style="width: 1px" type="hidden" />
                                                <input id="hdCourseSessionBasketPopup" runat="server" style="width: 1px" type="hidden" />
                                                <input id="hdBasketListPopUpPage" runat="server" style="width: 1px" type="hidden" />
                                                <input id="hdData" runat="server" style="width: 1px" type="hidden" />
                                                <input id="hdCourseID" runat="server" style="width: 1px" type="hidden" />
                                                <input id="hdLocationId" runat="server" style="width: 1px" type="hidden" />
                                                <input id="hdPageStatus" runat="server" style="width: 1px" type="hidden" />
                                                <input id="hdPageCourseSessionID" runat="server" style="width: 1px" type="hidden" />
                                                <input id="hdCourseSessionCourseLevel" runat="server" style="width: 1px" type="hidden" />
                                                                        <input id="hdTrainingRoomPage" style="width: 2px" type="hidden" runat="server" />
                                                                       <input id="hdSubCategory" runat="server" style="width: 1px" type="hidden" />
                                                                       <input id="hdParticipantID" runat="server" style="width: 1px" type="hidden" />
                                                                        <input id="hdTotalMarks" runat="server" style="width: 1px" type="hidden" />
                                                                        <input id="hdTotalPracticalMarks" runat="server" style="width: 1px" type="hidden" />
                                                                        <input id="hdTotalTheoryMarks" runat="server" style="width: 1px" type="hidden" />
                                                   <asp:HiddenField ID="hdTabType" runat="server" Value="0" />
                                                                      <input id="hdDuration" runat="server" style="width: 1px" type="hidden" />
                                                                       <input id="hdNoOfTest" runat="server" style="width: 1px" type="hidden" />
                                                                       <input id="hdEnPageAoffice" runat="server" style="width: 1px" type="hidden" />
                                                                        <input id="hdEnPageCourseSessionID" runat="server" style="width: 1px" type="hidden" />
                                                                        <input id="hdEnCourseID" runat="server" style="width: 1px" type="hidden" />
                                                                        <input id="hdCheckQuestion" runat="server" style="width: 1px" type="hidden" />
                                                                        
<input type="hidden" id="hdEmployeePageName" style="width:1px" runat='server' />

                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="width: 2%">
                                                                            </td>
                                                                            <td class="gap" colspan="7">
                                                                                &nbsp;</td>
                                                                        </tr>
                                                                         <tr>
                                                                            <td style="width: 2%">
                                                                            </td>
                                                                            <td class="gap" colspan="4">
                                                                            </td>
                                                                        </tr>
                                                                        </table>
                                                                        </asp:Panel>
                                                                  
                                                                       
                                                                    </td> 
                                                                    <td class="center top " colspan="2" rowspan="1">
                                                                        <asp:Button ID="btnSave" runat="server" TabIndex="3" CssClass="button topMargin" Text="Save" Width="100px" OnClientClick="return ValidateRegisterPage()" AccessKey="s" /><br />
                                                                        <asp:Button ID="btnNew" runat="server" TabIndex="3" CssClass="button topMargin" Text="New" Width="100px" AccessKey="n" /><br />
                                                                         <asp:Button ID="btnReset" runat="server" TabIndex="3" CssClass="button topMargin" Text="Reset" Width="100px" AccessKey="r"  /><br />
                                                                        <asp:Button ID="btnEmployee" runat="server" TabIndex="3" CssClass="button topMargin" Text="Employee" Width="100px" OnClientClick="return PopupPageRegister(3)"  /><br />
                                                                        <asp:Button ID="btnPeople" runat="server" TabIndex="3" CssClass="button topMargin" Text="People" Width="100px" OnClientClick="return PopupPageRegister(4)"  /><br />
                                                                        <asp:Button ID="btnBasket" runat="server" TabIndex="3" CssClass="button topMargin" Text="Basket" Width="100px" OnClientClick="return PopupPageRegister(5)"  /><br />
                                                                        <asp:Button ID="btnEmailAll" runat="server" TabIndex="3" CssClass="button topMargin" Text="Email All" Width="100px" OnClientClick="return PopupPageRegister(6)" AccessKey="m"  /><br />
                                                                        <asp:Button ID="btnPrintAll" runat="server" TabIndex="3" CssClass="button topMargin" Text="Print All" Width="100px" OnClientClick="return PopupPageRegister(7)" AccessKey="p"  /><br />
                                                                        <asp:Button ID="btnExport" runat="server" TabIndex="3" CssClass="button topMargin" Text="Export" Width="100px"  AccessKey="e"  />
                                                                       
                                                                        <br />
                                                                        
                                                                        
                                                              
                                                                        </td>
                                                                </tr>
                                                                <tr>
                                                                    <td >
                                                                    </td>
                                                                    <td>
                                                                    </td>
                                                                    <td class="center" colspan="2" rowspan="1">
                                                                    </td>
                                                                </tr>
                                                                
                                                                <tr>
                                                                    <td  class="ErrorMsg" style="width:10%">
                                                                        Field Marked * are Mandatory</td>
                                                                    <td>                     &nbsp; &nbsp;                     </td>
                                                                    <td>
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
            <tr>
                <td class="redborder top">
                    &nbsp;</td>
            </tr>
        </table>
                
                


                
                                                                             <asp:GridView ID="gvRegisterTab" runat="server"  AutoGenerateColumns="False" TabIndex="6" Width="1500px" AllowSorting="True">
                                                                                <Columns>
                                                                                     <asp:BoundField HeaderText="Agency Name" DataField="AgencyName" SortExpression="AgencyName" HeaderStyle-Width="9%" >
                                                                                        
                                                                                     </asp:BoundField>
                                                                                    <asp:BoundField HeaderText="Participant Name" DataField="StaffName" SortExpression="StaffName" HeaderStyle-Width="9%" >
                                                                                        
                                                                                    </asp:BoundField>
                                                                                    <asp:BoundField HeaderText="Letter Send On" DataField="TR_CLETTER_DATESEND" SortExpression="TR_CLETTER_DATESEND" HeaderStyle-Width="6%" >
                                                                                       
                                                                                    </asp:BoundField>
                                                                                    <asp:BoundField HeaderText="Dis.Letter Send On" DataField="TR_PCLETTER_DATESEND" SortExpression="TR_PCLETTER_DATESEND" HeaderStyle-Width="6%" >
                                                                                       
                                                                                    </asp:BoundField>
                                                                                    
                                                                                    <asp:TemplateField HeaderText="Status" SortExpression="TR_PARTSTATUS_ID" HeaderStyle-Width="9%">
                                                                                   <ItemTemplate>
                                                                                   <asp:DropDownList ID="ddlStatus" runat="server" CssClass="dropdownlist"  ></asp:DropDownList>
                                                                                   </ItemTemplate>
                                                                                    
                                                                                    </asp:TemplateField>
                                                                                    
                                                                                    
                                                                                     <asp:TemplateField HeaderText="T Marks" SortExpression="TR_COURSEP_RESULT" HeaderStyle-Width="4%">
                                                                                   <ItemTemplate>
                                                                                   <asp:TextBox ID="txtResult" runat="server" CssClass="textboxgrey" ReadOnly="true" Width="90%" MaxLength="5"></asp:TextBox>
                                                                                   </ItemTemplate>                                                                                   
                                                                                   </asp:TemplateField>
                                                                                    
                                                                                   <asp:TemplateField HeaderText="P Marks" SortExpression="TR_PRACTICAL_MARKS" HeaderStyle-Width="4%">
                                                                                   <ItemTemplate>
                                                                                   <asp:TextBox ID="txtPResult" runat="server" CssClass="textbox" Width="90%" MaxLength="5"></asp:TextBox>
                                                                                   <input type="hidden" id="txtTotalPResult" runat="server" />
                                                                                   </ItemTemplate>
                                                                                   
                                                                                    </asp:TemplateField>
                                                                                    
                                                                                     <asp:TemplateField HeaderText="% Scored" SortExpression="TR_PERCENTAGE_MARKS" HeaderStyle-Width="4%">
                                                                                   <ItemTemplate>
                                                                                   <asp:TextBox ID="txtPercentage" runat="server" CssClass="textboxgrey" Width="90%" ReadOnly="true" MaxLength="5"></asp:TextBox>
                                                                                  
                                                                                   </ItemTemplate>
                                                                                  
                                                                                    </asp:TemplateField>
                                                                                    
                                                                                    <asp:BoundField HeaderText="Login" DataField="TR_COURSEP_LOGIN" SortExpression="TR_COURSEP_LOGIN" HeaderStyle-Width="7%" >
                                                                                       
                                                                                    </asp:BoundField>
                                                                                    <asp:BoundField HeaderText="Password" DataField="TR_COURSEP_PWD" SortExpression="TR_COURSEP_PWD" HeaderStyle-Width="8%">
                                                                                       
                                                                                    </asp:BoundField>
                                                                                    
                                                                                    <asp:TemplateField HeaderText="Ack">
                                                                                    <ItemTemplate>
                                                                                    <asp:CheckBox ID="chkAck" runat ="server" />
                                                                                    </ItemTemplate>
                                                                                        <HeaderStyle CssClass="displayNone" />
                                                                                        <ItemStyle CssClass="displayNone" />
                                                                                    </asp:TemplateField>
                                                                                    
                                                                                   <asp:TemplateField HeaderText="Remarks" SortExpression="TR_COURSEP_REMARKS" HeaderStyle-Width="11%">
                                                                                   <ItemTemplate>
                                                                                   <asp:TextBox ID="txtRemarks" runat="server" CssClass="textbox" MaxLength="300" Width="90%"></asp:TextBox>
                                                                                   </ItemTemplate>                                                                                  
                                                                                   </asp:TemplateField>
                                                                                    
                                                                                    <asp:BoundField HeaderText ="Basket Id" DataField ="TR_BASKET_REQUEST_ID" SortExpression ="TR_BASKET_REQUEST_ID" HeaderStyle-Width ="7%" >
                                                                                    
                                                                                    </asp:BoundField>
                                                                                    
                                                                                                                                                     
                                                                                    <asp:TemplateField HeaderText="Action">
                                                                                    <ItemTemplate>
                                                            <asp:LinkButton ID="lnkSelect" runat="server" CommandName ="SelectX" Text ="Details" CommandArgument ='<%# DataBinder.Eval(Container.DataItem, "Type") + "|" + DataBinder.Eval(Container.DataItem, "Id") + "|" + DataBinder.Eval(Container.DataItem,"Recordtype") + "|" + DataBinder.Eval(Container.DataItem,"TR_COURSEP_ID")   %>' CssClass="LinkButtons"></asp:LinkButton>&nbsp;
                                                            <asp:LinkButton ID="lnkDelete" runat="server" CommandName ="DeleteX" CommandArgument ='<%# DataBinder.Eval(Container.DataItem, "Type") + "|" + DataBinder.Eval(Container.DataItem, "Id") + "|" + DataBinder.Eval(Container.DataItem,"Recordtype") + "|" + DataBinder.Eval(Container.DataItem,"TR_COURSEP_ID")   %>' Text ="Delete" CssClass="LinkButtons"></asp:LinkButton>&nbsp;
                                                            <asp:LinkButton ID="lnkHistory" runat="server" CommandName ="HistoryX" CommandArgument ='<%# DataBinder.Eval(Container.DataItem, "TR_COURSEP_ID") %>' Text ="History" CssClass="LinkButtons"></asp:LinkButton>&nbsp;
                                                           
                                                            <asp:LinkButton ID="lnkInvitation" runat="server" CommandName ="InvitationX" Text ="Invitation Ltr" CommandArgument ='<%# DataBinder.Eval(Container.DataItem, "LCODE") + "|" + DataBinder.Eval(Container.DataItem, "TR_COURSEP_ID") + "|" + DataBinder.Eval(Container.DataItem, "StaffName")%>' CssClass="LinkButtons"></asp:LinkButton>&nbsp;
                                                            <asp:LinkButton ID="lnkDistinction" runat="server" CommandName ="DistinctionX" CommandArgument ='<%# DataBinder.Eval(Container.DataItem, "LCODE")+ "|" + DataBinder.Eval(Container.DataItem, "TR_COURSEP_ID")+ "|" + DataBinder.Eval(Container.DataItem, "StaffName") %>' Text ="Distinction Ltr" CssClass="LinkButtons"></asp:LinkButton>&nbsp;
                                                           
                                                              
                                                               <asp:HiddenField ID="hdUniqueID" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "Type") + "|" + DataBinder.Eval(Container.DataItem, "Id") + "|" + DataBinder.Eval(Container.DataItem,"Recordtype") + "|" + DataBinder.Eval(Container.DataItem,"TR_COURSEP_ID")   %>' />   
                                                             </ItemTemplate>
                                                           </asp:TemplateField>       
                                                                                 </Columns>
                                                                                    <AlternatingRowStyle CssClass="lightblue" />
                                                                                    <RowStyle CssClass="textbold" />
                                                                                    <HeaderStyle CssClass="Gridheading center" ForeColor="white" />                                                    
                                                                                 </asp:GridView>
            

    </form>
</body>
</html>
