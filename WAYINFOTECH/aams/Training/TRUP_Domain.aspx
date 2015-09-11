<%@ Page Language="VB" AutoEventWireup="false" CodeFile="TRUP_Domain.aspx.vb" Inherits="Training_TRUP_Domain" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
	<title>AAMS::Training::Manage Domain</title>
	<script language="javascript" type="text/javascript" src="../JavaScript/AAMS.js"></script>
	<link href="../CSS/AAMS.css" type="text/css" rel="stylesheet" />
</head>
<body >
	<form id="frmManageContactType" runat="server" defaultbutton="btnSave" defaultfocus="txtDomainName">
		<table width="860px" align="left" height="486px" class="border_rightred">
			<tr>
				<td valign="top">
					<table width="100%" align="left">
						<tr>
							<td valign="top" align="left">
								<span class="menu">Training-&gt;</span><span class="sub_menu">Manage FeedBack Domain </span></td>
						</tr>
						<tr>
							<td class="heading" align="center" valign="top">
								Manage FeedBack Domain 
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
														<asp:Label ID="lblError" runat="server" CssClass="ErrorMsg" EnableViewState="False"></asp:Label></td>
													<td>
													</td>
												</tr>
												<tr>
													<td>
													</td>
													<td class="textbold">
														Domain Name<span class="Mandatory">*</span></td>
													<td>
														<asp:TextBox ID="txtDomainName" runat="server" CssClass="textbox" MaxLength="100"
															TabIndex="2" Width="308px"></asp:TextBox></td>
													<td>
														<asp:Button ID="btnSave" CssClass="button" runat="server" Text="Save" TabIndex="3" AccessKey="s" /></td>
												</tr>
												<tr>
													<td>
													</td>
													<td class="textbold">
                                                        Order Number<span class="Mandatory">*</span></td>
													<td>
                                                        <asp:TextBox ID="txtOrderNo" runat="server" CssClass="textbox" MaxLength="2" TabIndex="2"
                                                            Width="308px" onkeyup="checknumeric(this.id)"></asp:TextBox></td>
													<td>
														<asp:Button ID="btnNew" CssClass="button" runat="server" Text="New" TabIndex="3" AccessKey="n" /></td>
												</tr>
												<tr>
													<td>
													</td>
													<td class="textbold" style="width: 18%;">
													</td>
													<td style="width: 40%;">
													</td>
													<td style="width: 35%;">
														<asp:Button ID="btnReset" CssClass="button" runat="server" Text="Reset" TabIndex="3" AccessKey="r" /></td>
												</tr>
												<tr>
													<td style="width: 10%;">
													</td>
													<td class="textbold">
													</td>
													<td>
													</td>
													<td>
														&nbsp;</td>
												</tr>
												<tr>
													<td>
													</td>
													<td colspan="2" style="height: 26px" class="ErrorMsg">
														&nbsp;Field Marked * are Mandatory</td>
													<td style="height: 26px">
													</td>
												</tr>
												<tr>
													<td>
														<asp:HiddenField ID="hdID" runat="server" />
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

	<script language="javascript" type="text/javascript">
    function ValidateForm()
    {
      document.getElementById('<%=lblError.ClientId%>').innerText=''
     if(document.getElementById('<%=txtDomainName.ClientId%>').value =='')
        {
            document.getElementById('<%=lblError.ClientId%>').innerText='Domain Name is mandatory.'
            document.getElementById('<%=txtDomainName.ClientId%>').focus();
            return false;
        }
        if(document.getElementById('txtOrderNo').value =='')
        {
            document.getElementById('<%=lblError.ClientId%>').innerText='Order number is mandatory.'
            document.getElementById('txtOrderNo').focus();
            return false;
        }

       return true; 
        
    }
    
    function ClearControls()
    {
        document.getElementById("txtDomainName").value=""
        document.getElementById("lblError").innerText=""
        return false;
    }
	</script>

</body>
</html>