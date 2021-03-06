<%@ Page Language="VB" AutoEventWireup="false" CodeFile="SAUP_AIR_NONAIR_OTH_Status.aspx.vb" Inherits="Sales_SAUP_AIR_NONAIR_OTH_Status" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>AAMS::Sales::Manage Air & Non-Air and Product Status</title>
    <link href="../CSS/AAMS.css" type="text/css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server" defaultbutton="btnSave">
     <table width="860px" align="left" height="486px" class="border_rightred">
            <tr>
                <td valign="top">
                    <table width="100%" align="left">
                        <tr>
                            <td valign="top"  align="left" >
                            <span class="menu">Sales-&gt;</span><span class="sub_menu">Manage Air & Non-Air and Product Status</span></td>
                        </tr>
                        <tr>
                            <td class="heading" align="center" valign="top">
                                Manage Air & Non-Air and Product Status</td>
                        </tr>
                        <tr>
                            <td valign="top">
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td class="redborder center">
                                    <table border="0" cellpadding="2" cellspacing="1" style="width: 100%" class="left">
                                                                        <tr>
                                                                            <td colspan="4" class="center gap">
                                                                </td>
                                                                        </tr>
                                        <tr>
                                            <td style="width: 176px">
                                            </td>
                                            <td class="gap" colspan="2" style="text-align: center">
                                                    <asp:Label ID="lblError" runat="server" CssClass="ErrorMsg" EnableViewState="False"></asp:Label></td>
                                            <td>
                                            </td>
                                        </tr>
                                                                        <tr>
                                                                            <td style="width: 176px">
                                                                            </td>
                                                                            <td class="textbold">
                                                                                Status<span class="Mandatory" >*</span></td>
                                                                            <td style="width: 308px">
                                                                                &nbsp;<asp:TextBox ID="txtStatus" runat="server" CssClass="textbox" MaxLength="1000" TabIndex="1" Width="208px"></asp:TextBox></td>
                                                                            <td>
                                                                    <asp:Button ID="btnSave" CssClass="button" runat="server" Text="Save" TabIndex="3" AccessKey="s" /></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="width: 176px">
                                                                            </td>
                                                                            <td class="textbold">
                                                                                Close</td>
                                                                            <td style="width: 308px">
                                                                                &nbsp;<asp:CheckBox ID="chkClose" runat="server" TabIndex="2" /></td>
                                                                            <td>
                                                                    <asp:Button ID="btnNew" CssClass="button" runat="server" Text="New" TabIndex="4" AccessKey="n" /></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="width: 176px; height: 26px;">
                                                                            </td>
                                                                            <td class="textbold" style="height: 26px">
                                                             </td>
                                                                            <td style="width: 308px; height: 26px;">
                                                                    </td>
                                                                            <td style="height: 26px">
                                                                    <asp:Button ID="btnReset" CssClass="button" runat="server" Text="Reset" TabIndex="5" AccessKey="r" /></td>
                                                                        </tr>
                                        <tr>
                                            <td style="width: 176px; height: 26px">
                                            </td>
                                            <td class="textbold" style="height: 26px">
                                            </td>
                                            <td style="width: 308px; height: 26px">
                                            </td>
                                            <td style="height: 26px">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 176px; height: 26px">
                                            </td>
                                            <td class="ErrorMsg" colspan="2">
                                            Field Marked * are Mandatory</td>
                                            <td style="height: 26px">
                                        <asp:HiddenField ID="hdID" runat ="server" />
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
     if(document.getElementById('<%=txtStatus.ClientId%>').value =='')
        {
            document.getElementById('<%=lblError.ClientId%>').innerText='Air,Non-Air and Others status is mandatory.'
            document.getElementById('<%=txtStatus.ClientId%>').focus();
            return false;
        }

       return true; 
        
    }
    
    function ClearControls()
    {
        document.getElementById("txtStatus").value=""
        document.getElementById("lblError").innerText=""
        return false;
    }
    </script>
</body>
</html>
