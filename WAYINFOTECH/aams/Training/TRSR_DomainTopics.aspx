<%@ Page Language="VB" AutoEventWireup="false" CodeFile="TRSR_DomainTopics.aspx.vb" Inherits="Training_TRSR_DomainTopics" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
	<link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />
	<title>AAMS::Training::Search Topic</title>
<script language="javascript" type="text/javascript" src="../JavaScript/AAMS.js" ></script>
	<script language="javascript" type="text/javascript">
         
    function Edit(CourseId)
			{
				 window.location.href="TRUP_DomainTopics.aspx?Action=U&CourseId=" +CourseId
				 return false;
			}
			
	function Delete(TopicId)
	{
		 			 
		 if (confirm("Are you sure you want to delete?")==true)
            {    
               document.getElementById('<%=hdCourseId.ClientId%>').value = TopicId
               return true;        
            }
            return false;
	}
			
	</script>

</head>
<body >
	<form id="form1" runat="server" defaultbutton="btnSearch" defaultfocus="ddlCourse" >
		<div>
			<table width="860px" class="border_rightred left">
				<tr>
					<td class="top">
						<table width="100%" class="left">
							<tr>
								<td>
									<span class="menu">Training -&gt;</span><span class="sub_menu">FeedBack Topic Search</span></td>
							</tr>
							<tr>
								<td class="heading center">
									Search FeedBack Topic </td>
							</tr>
							<tr>
								<td>
									<table width="100%" border="0" cellspacing="0" cellpadding="0">
										<tr>
											<td class="redborder center">
												<table border="0" cellpadding="2" cellspacing="1" style="width: 100%" class="left">
													<tr>
														<td class="center" colspan="6">
															<asp:Label ID="lblError" runat="server" CssClass="ErrorMsg"></asp:Label></td>
													</tr>
													<tr>
														<td width="20%">
														</td>
														<td class="textbold" style="width: 10%">
															Course</td>
														<td colspan="3">
															  <asp:DropDownList  onkeyup="gotop(this.id)"  ID="ddlCourse" runat="server" CssClass="dropdownlist" Width="400px" TabIndex="2">
																	
															 </asp:DropDownList></td>
														<td style="width: 18%;">
															<asp:Button ID="btnSearch" CssClass="button" runat="server" Text="Search" TabIndex="3" AccessKey="a" /></td>
													</tr>
													<tr>
														<td>
														</td>
														<td class="textbold" style="width: 10%">
															</td>
														<td colspan="3">
															 <asp:DropDownList  onkeyup="gotop(this.id)"  ID="ddlDomain" runat="server" CssClass="dropdownlist" Width="400px" TabIndex="2" Visible="False">
																	
															 </asp:DropDownList></td>
														<td>
															<asp:Button ID="btnNew" CssClass="button" runat="server" Text="New" TabIndex="3" AccessKey="n" /></td>
													</tr>
													<tr>
														<td>
														</td>
														<td>
														</td>
														<td colspan="3">
															&nbsp;&nbsp;</td>
														<td>
                                                            <asp:Button ID="btnExport" runat="server" CssClass="button" TabIndex="3" Text="Export" AccessKey="e" /></td>
													</tr>
                                                    <tr>
                                                        <td>
                                                        </td>
                                                        <td>
                                                        </td>
                                                        <td>
                                                        </td>
                                                        <td>
                                                        </td>
                                                        <td>
                                                        </td>
                                                        <td>
															<asp:Button ID="btnReset" CssClass="button" runat="server" Text="Reset" TabIndex="3" AccessKey="r" /></td>
                                                    </tr>
													<tr>
														<td>
														</td>
														<td>
															<asp:HiddenField ID="hdCourseId" runat="server" />
														</td>
														<td colspan="4">
                                                            <input id="hdRecordOnCurrentPage" runat="server" style="width: 6px" type="hidden" /></td>
													</tr>
													<tr>
														<td colspan="6">
															<asp:GridView ID="gvDomain" runat="server" AutoGenerateColumns="False" TabIndex="6"
																Width="100%" EnableViewState="False" PageSize="25" AllowSorting="True" HeaderStyle-ForeColor="white" >
																<Columns>
																	<asp:BoundField HeaderText="Course" DataField="TR_COURSE_NAME" SortExpression="TR_COURSE_NAME" />
																	<asp:TemplateField HeaderText="Action">
																		<ItemTemplate>
																			<asp:LinkButton ID="lnkEdit" runat="server" CommandName="EditX" Text="Edit" CssClass="LinkButtons"></asp:LinkButton>&nbsp;
																			<asp:LinkButton ID="lnkDelete" runat="server" CommandName="DeleteX" Text="Delete"
																				CssClass="LinkButtons"></asp:LinkButton>
																			<asp:HiddenField ID="hdCourse" runat="server" Value='<%#Eval("TR_COURSE_ID")%>' />
																			<asp:HiddenField ID="hdDomain" runat="server" Value='<%#Eval("TR_CVALTOPIC_ID")%>' />
																		</ItemTemplate>
																		<ItemStyle Width="15%" />
																	</asp:TemplateField>
																</Columns>
																<AlternatingRowStyle CssClass="lightblue" />
																<RowStyle CssClass="textbold" />
																<HeaderStyle CssClass="Gridheading" />
																
															</asp:GridView>
														</td>
													</tr>
                                                    <tr>
                                                        <td colspan="6">
                                                          <asp:Panel ID="pnlPaging" runat="server" Visible="false" Width="100%">
                                                      <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                      <tr class="paddingtop paddingbottom"  >                                                                                                                                                
                                                                          <td style="width: 30%" class="left"><span  class="textbold"><b>&nbsp;No. of records found</b></span>&nbsp;&nbsp;<asp:TextBox ID="txtTotalRecordCount" runat ="server"  Width="105px" CssClass="textboxgrey" ReadOnly="True" ></asp:TextBox></td>
                                                                          <td style="width: 25%" class="right">                                                                             
                                                                              <asp:LinkButton ID="lnkPrev" CssClass="LinkButtons" runat="server" CommandName="Prev"><< Prev</asp:LinkButton></td>
                                                                          <td style="width: 20%" class="center">
                                                                              <span class ="textbold" ><b>Page No</b></span>&nbsp;&nbsp;<asp:DropDownList  onkeyup="gotop(this.id)"  ID="ddlPageNumber" Width="70px" CssClass="dropdownlist" runat="server" AutoPostBack="True" >
                                                                              </asp:DropDownList></td>
                                                                          <td style="width: 25%" class="left">
                                                                              <asp:LinkButton ID="lnkNext" runat="server" CssClass="LinkButtons" CommandName="Next">Next >></asp:LinkButton></td>
                                                                      </tr>
                                                                  </table></asp:Panel>
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
