<%@ Page Language="VB" AutoEventWireup="false" CodeFile="TRUP_TrainingRooms.aspx.vb" Inherits="Training_TRUP_TrainingRooms" ValidateRequest="false" EnableEventValidation="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
   <link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />
    <script src="../JavaScript/AAMS.js" type="text/javascript"></script>
    <title>AAMS::Training::Manage Training Rooms</title>
    <script language="javascript" type="text/javascript">
   
    function fillTrainingText()
    {
     if (document.getElementById("chkOnSite").checked==true)
     {
         if (document.getElementById("ddlAOffice").selectedIndex != "0")
         {
         var item =document.getElementById("ddlAOffice").selectedIndex;         
         var Aoffice=document.getElementById("ddlAOffice").options[item].text;
         document.getElementById("txtTrainingRoom").value=Aoffice + "  Onsite";
         document.getElementById("ddlCity").value=document.getElementById("ddlAOffice").value.split('|')[0];
         document.getElementById("hdCityID").value=document.getElementById("ddlAOffice").value.split('|')[0];
         document.getElementById("ddlCity").disabled=true;            
         
         }
     }
     else
     {
     document.getElementById("txtTrainingRoom").value="";
     document.getElementById("ddlCity").disabled=false;
     }
    }
    
    function ValidateAdd()
    {
    
    if (document.getElementById("ddlCourse").selectedIndex == "0")
         {
            document.getElementById('<%=lblError.ClientId%>').innerText='Course is mandatory.'
             return false;
         }
         return true;
    
    }
    
    </script>
    
</head>
<body >
    <form id="form1" runat="server" defaultbutton="btnSave" defaultfocus="ddlAOffice">
    <div>
      <table width="860px"  class="border_rightred left">
            <tr>
                <td class="top">
                    <table width="100%" class="left">
                        <tr>
                            <td   >
                            <span class="menu"> Training -&gt;</span><span class="sub_menu">Training Rooms</span></td>
                        </tr>
                        <tr>
                            <td class="heading center" >
                                Manage Training Rooms</td>
                        </tr>
                        <tr>
                            <td >
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td class="redborder center">
                                    <table border="0" cellpadding="2" cellspacing="1" style="width: 100%" class="left">
                                                                        <tr>
                                                                            <td class="center" colspan="6"  >
                                                                <asp:Label ID="lblError" runat="server" CssClass="ErrorMsg" EnableViewState="False"></asp:Label></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td ></td>
                                                                            <td class="textbold" style="width: 18%">
                                                                                AOffice<span class="Mandatory">*</span>
                                                                            </td>
                                                                            <td ><asp:DropDownList ID="ddlAOffice" runat="server" CssClass="dropdownlist" Width="176px" TabIndex="2" AutoPostBack="True" onkeyup="gotop(this.id)">
                                                                         
                                                                            </asp:DropDownList></td>
                                                                            <td class="textbold" style="width: 20%"  >   
                                                <asp:CheckBox ID="chkOnSite" runat="server" CssClass="textbold" TabIndex="2" onclick="fillTrainingText()" />&nbsp;
                                                OnSite</td>
                                                                            <td style="width: 32%"  > </td>
                                                                            <td align="center" >   
                                            <asp:Button ID="btnSave" CssClass="button" runat="server" Text="Save" TabIndex="3" OnClientClick="return TrainingRoomUpdatePage()" AccessKey="s" /></td>
                                                                        </tr>
                                        <tr>
                                            <td>
                                            </td>
                                            <td class="textbold" style="width: 18%">
                                                City<span class="Mandatory">*</span>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlCity" runat="server" CssClass="dropdownlist" Width="176px" TabIndex="2" onkeyup="gotop(this.id)">
                                                    
                                                </asp:DropDownList></td>
                                            <td class="textbold" style="width: 20%">
                                            </td>
                                            <td style="width: 32%">
                                            </td>
                                            <td align="center">
                                                <asp:Button ID="btnNew" CssClass="button" runat="server" Text="New" TabIndex="3" AccessKey="n" /></td>
                                        </tr>
                                        <tr>
                                            <td>
                                            </td>
                                            <td class="textbold">
                                                Training Room <span class="Mandatory">*</span></td>
                                            <td colspan="3">
                                                <asp:TextBox ID="txtTrainingRoom" runat="server" CssClass="textbox"  TabIndex="2" Width="563px" Height="50px" TextMode="MultiLine" onkeyup="checkMaxLength(this.id,'50')" ></asp:TextBox></td>
                                            <td align="center" class="top">
                                            <asp:Button ID="btnReset" CssClass="button" runat="server" Text="Reset" TabIndex="3" AccessKey="r" /></td>
                                        </tr>
                                        <tr>
                                            <td>
                                            </td>
                                            <td class="textbold">
                                                Max No of Participant</td>
                                            <td>
                                                                                <asp:TextBox ID="txtNoOfParticipant" runat="server" CssClass="textbox" MaxLength="2" onkeyup="checknumeric(this.id)"
                                                                                    TabIndex="2" Width="154px"></asp:TextBox></td>
                                            <td class="textbold">
                                                </td>
                                            <td>
                                                                                </td>
                                            <td align="center">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                            </td>
                                            <td class="textbold">
                                                Course</td>
                                            <td colspan="3">
                                                <asp:DropDownList ID="ddlCourse" runat="server" CssClass="dropdownlist" onkeyup="gotop(this.id)"
                                                    TabIndex="2" Width="550px">
                                                </asp:DropDownList>
                                                &nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btnAdd" CssClass="button" runat="server" Text="Add" AccessKey="s" TabIndex="2" /></td>
                                            <td align="center">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                            </td>
                                            <td class="textbold">
                                            </td>
                                            <td colspan="3">
                                             
                                            
                                            <asp:GridView ID="gvMaxParticipants" runat="server" AutoGenerateColumns="False"  Width="100%"  AllowSorting ="true"  HeaderStyle-ForeColor="white" >
																<Columns>
																	<asp:BoundField HeaderText="Course" DataField="COURSE_NAME" SortExpression="COURSE_NAME" ItemStyle-Wrap ="true" HeaderStyle-HorizontalAlign ="center" />
																																		
																	<asp:TemplateField HeaderText="A" HeaderStyle-HorizontalAlign ="center">
																		<ItemTemplate>
																			<asp:TextBox ID="txtA" runat ="server" Text='<%#Eval("A")%>' CssClass="textbox right" Width="30px" MaxLength="3" />
																		</ItemTemplate>
																		<ItemStyle Width="5%"   />
																	</asp:TemplateField>
																	
																	<asp:TemplateField HeaderText="B" HeaderStyle-HorizontalAlign ="center">
																		<ItemTemplate>
																			<asp:TextBox ID="txtB" runat ="server" Text='<%#Eval("B")%>' CssClass="textbox right" Width="30px" MaxLength="3" />
																		</ItemTemplate>
																		<ItemStyle Width="5%"   />
																	</asp:TemplateField>
																	
																	<asp:TemplateField HeaderText="C" HeaderStyle-HorizontalAlign ="center">
																		<ItemTemplate>
																			<asp:TextBox ID="txtC" runat ="server" Text='<%#Eval("C")%>' CssClass="textbox right" Width="30px" MaxLength="3" />
																		</ItemTemplate>
																		<ItemStyle Width="5%"   />
																	</asp:TemplateField>
																	
																	<asp:TemplateField HeaderText="D" HeaderStyle-HorizontalAlign ="center">
																		<ItemTemplate>
																			<asp:TextBox ID="txtD" runat ="server" Text='<%#Eval("D")%>' CssClass="textbox right" Width="30px" MaxLength="3"  />
																		</ItemTemplate>
																		<ItemStyle Width="5%"   />
																	</asp:TemplateField>
																	
																	<asp:TemplateField HeaderText="E" HeaderStyle-HorizontalAlign ="center">
																		<ItemTemplate>
																			<asp:TextBox ID="txtE" runat ="server" Text='<%#Eval("E")%>' CssClass="textbox right" Width="30px" MaxLength="3" />
																		</ItemTemplate>
																		<ItemStyle Width="5%"  />
																	</asp:TemplateField>
																	
																	<asp:TemplateField HeaderText="G" HeaderStyle-HorizontalAlign ="center">
																		<ItemTemplate>
																			<asp:TextBox ID="txtG" runat ="server" Text='<%#Eval("G")%>' CssClass="textbox right" Width="30px" MaxLength="3" />
																		</ItemTemplate>
																		<ItemStyle Width="5%"  />
																	</asp:TemplateField>
																	
																	
																	<asp:TemplateField HeaderText="Action" HeaderStyle-HorizontalAlign ="center">
																		<ItemTemplate>
																			<asp:LinkButton ID="lnkDelete" runat="server" CommandName="DeleteX" Text="Delete" 	CssClass="LinkButtons" CommandArgument='<%#Eval("COURSE_ID")%>'></asp:LinkButton>
																			<asp:HiddenField ID="hdCourseId" runat="server" Value='<%#Eval("COURSE_ID")%>' />
																		</ItemTemplate>
																		<ItemStyle Width="10%" HorizontalAlign ="center" />
																	</asp:TemplateField>
																</Columns>
																<AlternatingRowStyle CssClass="lightblue" />
																<RowStyle CssClass="textbold" />
																<HeaderStyle CssClass="Gridheading" />
																<PagerSettings PageButtonCount="5" />
															</asp:GridView>
                                            
                                                
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                            </td>
                                            <td class="textbold">
                                            </td>
                                            <td>
                                            </td>
                                            <td class="textbold">
                                            </td>
                                            <td>
                                            </td>
                                            <td align="center">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td ></td>
                                            <td >       </td>    <td  colspan="4">
                                                
                                                <input id="hdPageLocationID" runat="server" style="width: 1px" type="hidden" enableviewstate="true"/>
                                                
                                            <input id="hdCity" runat="server" style="width: 1px" type="hidden" />
                                            <input id="hdCityID" runat="server" style="width: 1px" type="hidden" />
                                                <input id="hdData" runat="server" style="width: 1px" type="hidden" /></td>
                                        </tr>
                                                                        <tr>
                                                                            <td colspan="6" class="ErrorMsg" >
                                                                        Field Marked * are Mandatory
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
