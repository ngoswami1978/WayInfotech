<%@ Page Language="VB" AutoEventWireup="false" CodeFile="TRSR_Exercise.aspx.vb" Inherits="Training_TRSR_Exercise" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
  <link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />
    <title>AAMS::Training::Search Exercise</title>
     <script language="javascript" type="text/javascript">
    
    function Edit(ExerciseID)
			{
				 window.location.href="TRSR_Exercise.aspx?Action=U&ExerciseID=" +ExerciseID
				 return false;
			}
			
	function Delete(ExerciseID)
	{
		 			 
		 if (confirm("Are you sure you want to delete?")==true)
            {    
               document.getElementById('<%=hdExerciseID.ClientId%>').value = ExerciseID
               return true;        
            }
            return false;
	}
			
    </script>
</head>
<body>
    <form id="form1" runat="server" defaultbutton="btnSearch" defaultfocus ="txtTitle">
    <div>
     <table width="860px"  class="border_rightred left">
            <tr>
                <td class="top">
                    <table width="100%" class="left">
                        <tr>
                            <td   >
                            <span class="menu"> Training -&gt;</span><span class="sub_menu">Search Exercise</span></td>
                        </tr>
                        <tr>
                            <td class="heading center" >
                                Search Exercise</td>
                        </tr>
                        <tr>
                            <td >
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td class="redborder center">
                                    <table border="0" cellpadding="2" cellspacing="1" style="width: 100%" class="left">
                                                                        <tr>
                                                                            <td class="center" colspan="6" style="height: 15px"  >
                                                                <asp:Label ID="lblError" runat="server" CssClass="ErrorMsg"></asp:Label></td>
                                                                        </tr>
                                        <tr>
                                            <td style="width: 10%">
                                                </td>
                                            <td class="textbold" style="width: 15%"> Exercise Title</td>                                                                               
                                            <td colspan="3">
                                            <asp:TextBox ID="txtTitle" runat="server" CssClass="textbox" MaxLength="100" TabIndex="2" Width="460px"></asp:TextBox>
                                            </td>
                                            <td style="width: 18%;" >
                                            <asp:Button ID="btnSearch" CssClass="button" runat="server" Text="Search" TabIndex="3" /></td>
                                        </tr>
                                          <tr>
                                            <td style="width: 5px">
                                            </td>
                                            <td class="textbold">  </td>
                                            <td style="width: 30%">
                                                </td>
                                            <td class="textbold" > </td>    <td>
                                                </td> <td>    
                                                <asp:Button ID="btnNew" CssClass="button" runat="server" Text="New" TabIndex="3" /></td>
                                        </tr>
                                          <tr>
                                            <td style="width: 5px">  </td> <td>   </td>    <td>
                                                &nbsp;</td><td > </td><td>
                                                &nbsp;</td>
                                            <td>
                                            <asp:Button ID="btnReset" CssClass="button" runat="server" Text="Reset" TabIndex="3" /></td>
                                        </tr>                           
                                        <tr>
                                            <td style="width: 5px" ></td>
                                            <td ><asp:HiddenField ID="hdExerciseId" runat ="server" />       </td> 
                                               <td  colspan="4">
                                            </td>
                                        </tr>
                                                                        <tr>
                                                                            <td colspan="6">
                                                                                <asp:GridView  ID="gvExercise" runat="server"  AutoGenerateColumns="False" TabIndex="6" Width="100%" EnableViewState="false" PageSize="25" >
                                                                                <Columns>
                                                                                    <asp:BoundField HeaderText="Title" DataField="TR_COURSE_NAME" />
                                                                                    <asp:BoundField HeaderText="Description" DataField="TR_COURSELEVEL_NAME" />
                                                                                    <asp:BoundField HeaderText="Url" DataField="TR_COURSE_DURATION"/>
                                                                                    <asp:BoundField HeaderText="Order" DataField="TR_COURSE_DURATION" >
                                                                                        <ItemStyle Width="5%" />
                                                                                    </asp:BoundField>
                                                                                    
                                                                                    <asp:TemplateField HeaderText="Action">
                                                            <ItemTemplate>
                                                            <asp:LinkButton ID="lnkSelect" CssClass="LinkButtons" Text="Select" runat="server" CommandName="SelectX" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "WO_ID") + "|" + DataBinder.Eval(Container.DataItem, "WO_NUMBER") %>'></asp:LinkButton>&nbsp;     
                                                            <asp:LinkButton ID="lnkEdit" runat="server" CommandName ="EditX" Text ="Edit" CssClass="LinkButtons"></asp:LinkButton>
                                                              <asp:LinkButton ID="lnkDelete" runat="server" CommandName ="DeleteX" Text ="Delete" CssClass="LinkButtons"></asp:LinkButton>
                                                               <asp:HiddenField ID="hdCourse" runat="server" Value='<%#Eval("TR_COURSE_ID")%>' />   
                                                               
                                                             </ItemTemplate>
                                                             <ItemStyle Width="10%" Wrap="False" />
                                                           </asp:TemplateField>                                                  
                                                 
                                                 </Columns>
                                                    <AlternatingRowStyle CssClass="lightblue" />
                                                    <RowStyle CssClass="textbold" />
                                                    <HeaderStyle CssClass="Gridheading" />
                                                    <pagersettings  
                                                      pagebuttoncount="5"/>
                                                   
                                                    
                                                 </asp:GridView>
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
