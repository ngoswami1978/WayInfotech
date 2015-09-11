<%@ Page Language="VB" AutoEventWireup="false" CodeFile="BOHDUP_ChangeStatus.aspx.vb" Inherits="BOBirdresHelpDesk_HDUP_ChangeStatus" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Back Office HelpDesk:Call Log History</title>
    <script src="../JavaScript/BOHelpDesk.js" type="text/javascript"></script>
    <base target="_self"/>
     <link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />
</head>
<body  >
    <form id="frmPtrHistory" runat="server" >
        <table width="860px" align="left" height="386px" >
            <tr>
                <td valign="top">
                    <table width="100%" align="left">  
                         <tr>
                            <td align="right">
                                <a href="#" class="LinkButtons" onclick="window.close();">Close</a>&nbsp;&nbsp;&nbsp;</td>
                        </tr>                      
                        <tr>
                            <td class="heading" align="center" valign="top">
                                Call Log Change Status</td>
                        </tr>
                        <tr>
                            <td valign="top">
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td align="LEFT" class="redborder">
                                 
                                                        <table width="100%" border="0" align="left" cellpadding="0" cellspacing="0" >
                                                            <tr>
                                                                <td  class="textbold" colspan="6" align="center">
                                                                <asp:Label ID="lblError" runat="server" CssClass="ErrorMsg" Text=""></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="6" height="4" align="center">
                                                                    &nbsp;</td>
                                                            </tr>
                                                            <tr>
                                                                <td   style="height: 4px;width:10%">
                                                                  
                                                                </td>
                                                                <td style="width:20%" class="textbold"> Status<span class="Mandatory">*</span></td>
                                                                <td style="width:20%"><asp:DropDownList ID="ddlStatus" runat="Server" CssClass="dropdownlist" Width="208px" ></asp:DropDownList></td>
                                                                <td style="width:20%">
                                                                </td><td style="width:20%">
                                                                    <asp:Button ID="btnSave" runat="server" CssClass="button" Text="Save" OnClientClick="return MultipleChangeStatus()" /></td><td>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 10%; height: 4px">
                                                                </td>
                                                                <td class="textbold" style="width: 20%">
                                                                    Solution</td>
                                                                <td style="width: 20%">
                                                                </td>
                                                                <td style="width: 20%">
                                                                </td>
                                                                <td style="width: 20%">
                                                                </td>
                                                                <td>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 10%; height: 4px">
                                                                </td>
                                                                <td class="textbold" colspan="3">
                                                                    <asp:TextBox ID="txtSol" runat="server" CssClass="textbox" Height="150px" Rows="10"
                                                                        TextMode="MultiLine" Width="550px"></asp:TextBox></td>
                                                                <td style="width: 20%">
                                                                </td>
                                                                <td>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 10%; height: 4px">
                                                                </td>
                                                                 <td class="textbold" >
                                                                     No of Record selected</td>
                                                                <td class="textbold" colspan="2">
                                                                    <asp:TextBox ID="txtRecordCount" runat="server" CssClass="textboxgrey" ReadOnly="True"></asp:TextBox></td>
                                                                <td style="width: 20%">
                                                                </td>
                                                                <td>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 10%; height: 4px">
                                                                </td>
                                                                <td class="textbold" colspan="3">
                                                                </td>
                                                                <td style="width: 20%">
                                                                </td>
                                                                <td>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="ErrorMsg" colspan="6" style="height: 12px">
                                                                Field Marked * are Mandatory
                                                                </td>
                                                            </tr>
                                                        </table>
                                                  
                                        </td>
                                    </tr>
                                </table>
                                
                               
                                <input id="hdHD_RE_ID_Multiple" runat="server" style="width: 6px" type="hidden" /></td>
                        </tr>
                    </table>
                    
                                
               
            </tr>
        </table>
    </form>
</body>
</html>
