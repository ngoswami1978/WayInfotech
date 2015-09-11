<%@ Page Language="VB" AutoEventWireup="false" CodeFile="LoginControlPanel.aspx.vb" Inherits="LoginControlPanel" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
	<link href="CSS/AAMS.css" rel="stylesheet" type="text/css" />
	<title>AAMS::Training::Search Course Level</title>



</head>
<body >
	<form id="form1" runat="server" defaultbutton="btnSearch" defaultfocus="txtCourseLevel" >
		<div>
			<table width="860px" class="border_rightred left">
				<tr>
					<td class="top">
						<table width="100%" class="left">
							<tr>
								<td>
									<span class="menu">Admin -&gt;</span><span class="sub_menu"> LoginControlPanel</span></td>
							</tr>
							<tr>
								<td class="heading center">
									Search Login User</td>
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
														<td class="textbold" style="width: 15%">
                                                            User Name</td>
														<td colspan="3">
															<asp:TextBox ID="txtUserName" runat="server" CssClass="textbox" MaxLength="50" TabIndex="2"
																Width="258px"></asp:TextBox>
														</td>
														<td style="width: 18%;">
															<asp:Button ID="btnSearch" CssClass="button" runat="server" Text="Search" TabIndex="3" AccessKey="a" /></td>
													</tr>
													<tr>
														<td>
														</td>
														<td class="textbold">
														</td>
														<td style="width: 35%">
														</td>
														<td class="textbold">
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
														</td>
														<td>
															&nbsp;</td>
														<td>
														</td>
														<td>
															&nbsp;</td>
														<td>
                                                            </td>
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
															</td>
                                                    </tr>
													<tr>
														<td>
														</td>
														<td>
                                                            &nbsp;</td>
														<td colspan="4">
                                                            &nbsp;</td>
													</tr>
													<tr>
														<td colspan="6">
															<asp:GridView ID="gvLogin" runat="server" AutoGenerateColumns="False" TabIndex="6" AllowSorting ="true"  HeaderStyle-ForeColor="white" 
																Width="100%" EnableViewState="true"  >
																<Columns>
																	<asp:BoundField HeaderText="User Id" DataField="UserId" SortExpression ="UserId" />
																	<asp:BoundField HeaderText="User IP Address" DataField="IPAddress" SortExpression ="IPAddress" />
																	<asp:TemplateField HeaderText="Action" Visible="false">
																		<ItemTemplate>
																			<asp:LinkButton ID="lnkLogOut" runat="server" CommandName="LogOutX" Text="LogOut" CssClass="LinkButtons" CommandArgument='<%#Container.DataItem("UserId") & "|" & Container.DataItem("IPAddress") %>'></asp:LinkButton>
																			
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
                                                    <td colspan="6" valign ="top"  >
                                                    <asp:Panel ID="pnlPaging" runat="server" Visible="false" Width="100%">
                                                      <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                      <tr style="padding-top : 4pt; padding-bottom : 4pt">                                                                                                                                                
                                                                          <td style="width: 30%" class="left"><span  class="textbold"><b>&nbsp;No. of records found</b></span>&nbsp;&nbsp;<asp:TextBox ID="txtRecordCount" runat="server" CssClass="textboxgrey" MaxLength="3" TabIndex="3" Width="100px" ReadOnly="True" Text ="0" Visible="True"></asp:TextBox></td>
                                                                          <td style="width: 25%" class="right">                                                                             
                                                                            </td>
                                                                          <td style="width: 20%" class="center">
                                                                             </td>
                                                                          <td style="width: 25%" class="left">
                                                                            </td>
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

