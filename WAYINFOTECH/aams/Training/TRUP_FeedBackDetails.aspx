<%@ Page Language="VB" AutoEventWireup="false" CodeFile="TRUP_FeedBackDetails.aspx.vb" Inherits="Training_TRUP_FeedBackDetails" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
	<title>AAMS::Training::FeedBack Details</title>
	<script language="javascript" type="text/javascript" src="../JavaScript/AAMS.js"></script>
	<link href="../CSS/AAMS.css" type="text/css" rel="stylesheet" />
	 <script language="javascript" type="text/javascript">
    function fnWOID()
    {
          window.close();
        return false;
    }
     </script>
</head>
<body >
	<form id="form1" runat="server" >
		<table width="860px" align="left" height="486px" class="border_rightred">
			<tr>
				<td valign="top">
					<table width="100%" align="left">
						<tr>
							<td valign="top" align="right">
							<asp:LinkButton ID="lnkClose"  CssClass="LinkButtons" runat="server" OnClientClick="return fnWOID()" >Close</asp:LinkButton>&nbsp;&nbsp;&nbsp;
								</td>
						</tr>
                        <tr>
                            <td align="left" valign="top">
                            <span class="menu"> Training -&gt;</span><span class="sub_menu">Feedback Details</span>
                            </td>
                        </tr>
						<tr>
							<td class="heading" align="center" valign="top">
                                FeedBack Details
							</td>
						</tr>
						<tr>
							<td valign="top">
								<table width="100%" border="0" cellspacing="0" cellpadding="0">
									<tr>
										<td class="redborder center">
											<table border="0" cellpadding="2" cellspacing="1" style="width: 100%" class="left">
												<tr>
													<td>
													</td>
													<td class="gap" colspan="2" style="text-align: center">
														<asp:label ID="lblError" runat="server" CssClass="ErrorMsg" EnableViewState="False"></asp:label></td>
													<td>
													</td>
												</tr>
												<tr>
													<td>
													</td>
													<td class="textbold">
                                                       Participant Name</td>
													<td class="textbold" colspan="2">
                                                        <asp:TextBox ID="txtParticipantName" runat="server" CssClass="textboxgrey" ReadOnly="True" Width="357px"></asp:TextBox></td>
												</tr>
												<tr>
													<td>
													</td>
													<td class="textbold">
                                                        Agency Name </td>
													<td class="textbold" colspan="2">
                                                        <asp:TextBox ID="txtAgencyName" runat="server" CssClass="textboxgrey" ReadOnly="True" Width="357px"></asp:TextBox></td>
												</tr>
												<tr>
													<td>
													</td>
													<td class="textbold" >
                                                        Address</td>
													<td class="textbold" colspan="2">
                                                        <asp:TextBox ID="txtAgencyAddress" runat="server" CssClass="textboxgrey" ReadOnly="True" Width="357px" TextMode="MultiLine"></asp:TextBox></td>
												</tr>
                                                <tr>
                                                    <td>
                                                    </td>
                                                    <td class="textbold" >
                                                        Course</td>
                                                    <td class="textbold" colspan="2">
                                                        <asp:TextBox ID="txtCourseDetails" runat="server" CssClass="textboxgrey" ReadOnly="True" Width="357px"></asp:TextBox></td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                    </td>
                                                    <td class="textbold" >
                                                        Course Duration</td>
                                                    <td class="textbold" colspan="2">
                                                        <asp:TextBox ID="txtCourseDuration" runat="server" CssClass="textboxgrey" ReadOnly="True" Width="357px"></asp:TextBox></td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                    </td>
                                                    <td class="textbold" >
                                                        Course Level</td>
                                                    <td class="textbold" colspan="2">
                                                        <asp:TextBox ID="txtCourseLevel" runat="server" CssClass="textboxgrey" ReadOnly="True" Width="357px"></asp:TextBox></td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                    </td>
                                                    <td class="textbold" >
                                                        Course Session</td>
                                                    <td class="textbold" colspan="2">
                                                        <asp:TextBox ID="txtCourseSession" runat="server" CssClass="textboxgrey" ReadOnly="True" Width="357px"></asp:TextBox></td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                    </td>
                                                    <td class="textbold" >
                                                        Location
                                                    </td>
                                                    <td class="textbold" colspan="2">
                                                        <asp:TextBox ID="txtLocation" runat="server" CssClass="textboxgrey" ReadOnly="True" Width="357px"></asp:TextBox></td>
                                                </tr>
												<tr>
													<td style="width: 10%;">
													</td>
													<td class="textbold">
                                                        Trainer</td>
													<td class="textbold">
                                                        <asp:TextBox ID="txtTrainerName" runat="server" CssClass="textboxgrey" ReadOnly="True" Width="357px"></asp:TextBox></td>
													<td>
														&nbsp;</td>
												</tr>
                                                <tr>
                                                    <td style="width: 10%">
                                                    </td>
                                                    <td class="textbold">
                                                    </td>
                                                    <td class="gap">
                                                    </td>
                                                    <td>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 10%">
                                                    </td>
                                                    <td class="textbold">
                                                    </td>
                                                    <td>
                                                    </td>
                                                    <td>
                                                    </td>
                                                </tr>
												<tr>
													<td>
                                                        &nbsp;</td>
                                                    <td colspan="3">
                                                    <asp:GridView ID="gvCourse" runat="server" AutoGenerateColumns="False" TabIndex="6"
												Width="100%" EnableViewState="True" PageSize="25">
												<Columns>
													<asp:BoundField HeaderText="Domain" DataField="DOMAINNAME">
														<ItemStyle Width="25%" />
													</asp:BoundField>
													<asp:BoundField HeaderText="Topics" DataField="TOPICNAME">
														<ItemStyle Width="40%" />
													</asp:BoundField>
													<asp:BoundField HeaderText="Evaluation" DataField="Status">
														<ItemStyle Width="5%" />
													</asp:BoundField>
													<asp:BoundField HeaderText="Comment" DataField="COMMENTS">
														<ItemStyle Width="40%" />
													</asp:BoundField>
													
													
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
                                                    <td class="ErrorMsg" colspan="2">
                                                    </td>
                                                    <td>
                                                    </td>
                                                </tr>
												<tr>
												</tr>
												<tr>
													<td colspan="4" style="text-align: center">
														&nbsp;</td>
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
	</form>

	

</body>
</html>
