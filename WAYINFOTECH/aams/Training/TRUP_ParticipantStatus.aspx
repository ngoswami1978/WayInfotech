<%@ Page Language="VB" AutoEventWireup="false" CodeFile="TRUP_ParticipantStatus.aspx.vb" Inherits="Training_TRUP_ParticipantStatus" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
	<title>AAMS::Training::Manage Participant Status</title>
	<link href="../CSS/AAMS.css" type="text/css" rel="stylesheet" />
</head>
<body>
	<form id="frmManageContactType" runat="server" defaultbutton="btnSave">
		<table width="860px" align="left" height="486px" class="border_rightred">
			<tr>
				<td valign="top">
					<table width="100%" align="left">
						<tr>
							<td valign="top" align="left">
								<span class="menu">Training-&gt;</span><span class="sub_menu">Manage Participant Status</span></td>
						</tr>
						<tr>
							<td class="heading" align="center" valign="top">
								Manage Participant Status
							</td>
						</tr>
						<tr>
							<td valign="top">
								<table width="100%" border="0" cellspacing="0" cellpadding="0">
									<tr>
										<td class="redborder center">
											<table border="0" cellpadding="2" cellspacing="1" style="width: 100%" class="left">
												<tr>
													<td style="width: 100px">
													</td>
													<td class="gap" colspan="2" style="text-align: center">
														<asp:Label ID="lblError" runat="server" CssClass="ErrorMsg" EnableViewState="False"></asp:Label></td>
													<td>
													</td>
												</tr>
												<tr>
													<td style="width: 100px">
													</td>
													<td class="textbold" style="width: 140px">
														Participant Status<span class="Mandatory">*</span></td>
													<td style="width: 358px">
														<asp:TextBox ID="txtParticipantStatusName" runat="server" CssClass="textbox" MaxLength="100"
															TabIndex="1" Width="308px"></asp:TextBox></td>
													<td>
														<asp:Button ID="btnSave" CssClass="button" runat="server" Text="Save" TabIndex="9" AccessKey="s" /></td>
												</tr>
												<tr>
													<td>
													</td>
													<td class="textbold">
													</td>
													<td>
													</td>
													<td>
														<asp:Button ID="btnNew" CssClass="button" runat="server" Text="New" TabIndex="10" AccessKey="n" /></td>
												</tr>
												<tr>
													<td style="height: 26px;">
													</td>
													<td class="textbold" style="height: 26px">
													</td>
													<td style="height: 26px;">
													</td>
													<td style="height: 26px">
														<asp:Button ID="btnReset" CssClass="button" runat="server" Text="Reset" TabIndex="11" AccessKey="r" /></td>
												</tr>
												<tr>
													<td >
													</td>
													<td >
													</td>
													<td >
													</td>
													<td style="height: 26px">
														&nbsp;</td>
												</tr>
												<tr>
													<td style="height: 26px">
														<asp:HiddenField ID="hdID" runat="server" />
													</td>
													<td colspan="2" style="height: 26px" class="ErrorMsg">
														&nbsp;Field Marked * are Mandatory</td>
													<td style="height: 26px">
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
	</form>

	<script language="javascript" type="text/javascript">
    function ValidateForm()
    {
      document.getElementById('<%=lblError.ClientId%>').innerText=''
     if(document.getElementById('<%=txtParticipantStatusName.ClientId%>').value =='')
        {
            document.getElementById('<%=lblError.ClientId%>').innerText='Participant Status is mandatory.'
            document.getElementById('<%=txtParticipantStatusName.ClientId%>').focus();
            return false;
        }

       return true; 
        
    }
    
    function ClearControls()
    {
        document.getElementById("txtParticipantStatusName").value=""
        document.getElementById("lblError").innerText=""
        return false;
    }
	</script>

</body>
</html>
