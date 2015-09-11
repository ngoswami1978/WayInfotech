<%@ Page Language="VB" AutoEventWireup="false" CodeFile="HDUP_PtrAssignee.aspx.vb" Inherits="BirdresHelpDesk_HDUP_PtrAssignee" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Manage ISP Order Status</title>
    <link rel="stylesheet" href="../CSS/AAMS.css" type="text/css" />
    <link type="text/javascript" href="../JavaScript/AAMS.js" />
    
    <script type="text/javascript" language="javascript">
    
     function InsertPtrAssignee()
        {
        window.location.href="HDUP_PtrAssignee.aspx?Action=I|";
        return false;
        }
        
        function validateAssignee()
        {
         if(document.getElementById("txtAssigneeName").value=='')
        {
        document.getElementById("txtAssigneeName").focus();
        document.getElementById("lblError").innerHTML ="PTR Assignee Name is Mandatory";
        return false;
        }
        
        }
    
    </script>
</head>
<body>
    <form id="form1" runat="server">
     <table width="850px" align="left" height="486px" class="border_rightred">
            <tr>
                <td valign="top">
                    <table width="100%" align="left">
                        <tr>
                            <td valign="top" align="left">
                                <span class="menu">Birdres HelpDesk&gt;</span><span class="sub_menu">PTR Assignee</span></td>
                        </tr>
                        <tr>
                            <td class="heading" align="center" valign="top">
                                Manage PTR Assignee </td>
                        </tr>
                        <tr>
                            <td style="height: 209px" valign="top" >
                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                    <tr>
                                        <td width="100%" class="redborder">
                                            <table align="left" border="0" cellpadding="2" cellspacing="0" width="100%">
                                                <tr>
                                                    <td class="textbold" style="height: 28px; width: 100%" colspan="4" valign="top">
                                                        <table width="100%" border="0" align="left" cellpadding="0" cellspacing="0">
                                                            <tr>
                                                                <td class="textbold" colspan="6" align="center" height="22px">
                                                                    <asp:Label ID="lblError" runat="server" CssClass="ErrorMsg" Text=""></asp:Label></td>
                                                            </tr>
                                                            <tr>
                                                                <td width="6%" class="textbold" style="height: 22px">
                                                                    &nbsp;</td>
                                                                <td class="textbold" style="height: 22px; width: 81px;">
                                                                </td>
                                                                <td style="height: 22px; width: 149px;" class="textbold" nowrap="nowrap" >
                                                                  Assignee Name<span class="Mandatory">* </span>
                                                                </td>
                                                                <td height="22px" style="width: 195px" class="textbold" nowrap="nowrap" >
                                                                    &nbsp;<asp:TextBox ID="txtAssigneeName" runat="server" CssClass="textbold" TabIndex="1" MaxLength="25" Width="176px" ></asp:TextBox>
                                                                </td>
                                                                <td style="height: 22px; width: 19px;">
                                                                    <asp:Button ID="btnSave" runat="server" CssClass="button" Text="Save" TabIndex="6" AccessKey="s" /></td>
                                                                <td width="18%" style="height: 22px">
                                                                    </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="textbold" colspan="6"  >
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="textbold" style="height: 22px" >
                                                                    &nbsp;</td>
                                                                <td class="textbold" style="height: 22px" >
                                                                </td>
                                                                <td style="height: 22px">
                                                                </td>
                                                                <td class="textbold" style="height: 22px" >
                                                                    </td>
                                                                <td style="height: 22px">
                                                                    <asp:Button ID="btnNew" runat="server" CssClass="button" Text="New" TabIndex="7" AccessKey="n" /></td>
                                                                <td style="height: 22px">
                                                                    </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="textbold" colspan="6" align="center" valign="TOP">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="textbold" style="height: 22px">
                                                                    &nbsp;</td>
                                                                <td class="textbold" style="height: 22px; width: 81px;">
                                                                </td>
                                                                <td style="height: 22px; width: 149px;" class="textbold">
                                                                </td>
                                                                <td style="width: 195px; height: 22px">
                                                                    &nbsp;</td>
                                                                <td style="height: 22px; width: 19px;">
                                                                    <asp:Button ID="btnReset" runat="server" CssClass="button" Text="Reset" TabIndex="8" AccessKey="r" /></td>
                                                                <td style="height: 22px">
                                                                    </td>
                                                            </tr>
                                                            <tr height="20px" ></tr>
                                                           
                                                            <tr>
                                                                <td class="textbold" style="height: 14px">
                                                                </td>
                                                                <td class="textbold" style="width: 81px; height: 14px">
                                                                </td>
                                                                <td colspan="2" style="height: 14px" class="ErrorMsg">
                                                                    Field Marked * are Mandatory</td>
                                                                <td style="height: 14px; width: 19px;">
                                                                </td>
                                                                <td style="height: 14px">
                                                                </td>
                                                            </tr>
                                                            
                                                        </table>
                                                        <br />
                                                    </td>
                                                    <td width="18%" rowspan="1" valign="top">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="textbold" colspan="5" style="height: 19px">
                                                        &nbsp;</td>
                                                </tr>
                                                <tr style="font-size: 12pt; font-family: Times New Roman">
                                                    <td colspan="6" height="12">
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
                <td valign="top">
                    &nbsp;</td>
            </tr>
        </table>
    </form>
</body>
</html>
