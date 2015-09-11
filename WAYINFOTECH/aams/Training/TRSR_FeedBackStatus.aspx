<%@ Page Language="VB" AutoEventWireup="false" CodeFile="TRSR_FeedBackStatus.aspx.vb" Inherits="Training_TRSR_FeedBackStatus" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
	<link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />
	<title>AAMS::Training::Search FeedBack Status</title>

	<script language="javascript" type="text/javascript">
         
    function Edit(FeedBackStatusId)
			{
				 window.location.href="TRUP_FeedBackStatus.aspx?Action=U&FeedBackStatusId=" +FeedBackStatusId
				 return false;
			}
			
	function Delete(FeedBackStatusId)
	{
		 			 
		 if (confirm("Are you sure you want to delete?")==true)
            {    
               document.getElementById('<%=hdFeedBackStatusId.ClientId%>').value = FeedBackStatusId
               return true;        
            }
            return false;
	}
			
	</script>

</head>
<body >
	<form id="form1" runat="server" defaultbutton="btnSearch" >
		<div>
			<table width="860px" class="border_rightred left">
				<tr>
					<td class="top">
						<table width="100%" class="left">
							<tr>
								<td>
									<span class="menu">Training -&gt;</span><span class="sub_menu"> FeedBack Status Search</span></td>
							</tr>
							<tr>
								<td class="heading center">
									Search FeedBack Status</td>
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
															 FeedBack Status</td>
														<td colspan="3">
															<asp:TextBox ID="txtFeedBackStatus" runat="server" CssClass="textbox" MaxLength="50" TabIndex="1"
																Width="400px"></asp:TextBox>
														</td>
														<td style="width: 18%;">
															<asp:Button ID="btnSearch" CssClass="button" runat="server" Text="Search" TabIndex="2" AccessKey="a" /></td>
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
															<asp:Button ID="btnNew" CssClass="button" runat="server" Text="New" TabIndex="3" AccessKey="n" /></td>
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
															<asp:Button ID="btnReset" CssClass="button" runat="server" Text="Reset" TabIndex="4" AccessKey="r" /></td>
                                                    </tr>
													<tr>
														<td>
														</td>
														<td>
															<asp:HiddenField ID="hdFeedBackStatusId" runat="server" />
														</td>
														<td colspan="4">
														</td>
													</tr>
													<tr>
														<td colspan="6">
															<asp:GridView ID="gvDomain" runat="server" AutoGenerateColumns="False" TabIndex="5"  HeaderStyle-ForeColor="white" 
																Width="100%" EnableViewState="false"  AllowSorting ="true"  >
																<Columns>
																<asp:BoundField HeaderText="FeedBack Status Order" DataField="TR_PART_MOOD_ORDER"  SortExpression ="TR_PART_MOOD_ORDER" ItemStyle-Width="22%" />
																	<asp:BoundField HeaderText="FeedBack Status " DataField="TR_PART_MOOD_NAME"  SortExpression ="TR_PART_MOOD_NAME" ItemStyle-Width="65%" ItemStyle-Wrap="true" />
																	
																	<asp:TemplateField HeaderText="Action">
																		<ItemTemplate>
																			<asp:LinkButton ID="lnkEdit" runat="server" CommandName="EditX" Text="Edit" CssClass="LinkButtons"></asp:LinkButton>&nbsp;
																			<asp:LinkButton ID="lnkDelete" runat="server" CommandName="DeleteX" Text="Delete"
																				CssClass="LinkButtons"></asp:LinkButton>
																			<asp:HiddenField ID="hdDomain" runat="server" Value='<%#Eval("TR_PART_MOOD_ID")%>' />
																		</ItemTemplate>
																		<ItemStyle Width="15%" />
																	</asp:TemplateField>
																</Columns>
																<AlternatingRowStyle CssClass="lightblue" />
																<RowStyle CssClass="textbold" />
																<HeaderStyle CssClass="Gridheading" ForeColor="White" />
																<PagerSettings PageButtonCount="5" />
															</asp:GridView>
														</td>
													</tr>
														  <tr>                                                   
                                                    <td colspan="6" valign ="top"  >
                                                    <asp:Panel ID="pnlPaging" runat="server" Visible="false" Width="100%">
                                                      <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                      <tr  class ="paddingtop paddingbottom"  >                                                                                                                                                
                                                                          <td style="width: 30%" class="left"><span  class="textbold"><b>&nbsp;No. of records found</b></span>&nbsp;&nbsp;<asp:TextBox ID="txtTotalRecordCount" runat ="server"  Width="105px" CssClass="textboxgrey" TabIndex="6" ReadOnly="True" ></asp:TextBox></td>
                                                                          <td style="width: 25%" class="right">                                                                             
                                                                              <asp:LinkButton ID="lnkPrev" CssClass="LinkButtons" runat="server" CommandName="Prev" TabIndex="7"><< Prev</asp:LinkButton></td>
                                                                          <td style="width: 20%" class="center">
                                                                              <span class ="textbold" ><b>Page No</b></span>&nbsp;&nbsp;<asp:DropDownList ID="ddlPageNumber" Width="70px" CssClass="dropdownlist" runat="server" AutoPostBack="True" TabIndex="9" >
                                                                              </asp:DropDownList></td>
                                                                          <td style="width: 25%" class="left">
                                                                              <asp:LinkButton ID="lnkNext" runat="server" CssClass="LinkButtons" CommandName="Next" TabIndex="10">Next >></asp:LinkButton></td>
                                                                      </tr>
                                                                  </table></asp:Panel>
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
			 <tr>
              <td style="height: 20px"><asp:TextBox ID="txtRecordOnCurrentPage" runat ="server"  Width="73px" CssClass="textboxgrey" Visible ="false"  ></asp:TextBox></td>
            </tr>
			</table>
		</div>
	</form>
</body>
</html>
