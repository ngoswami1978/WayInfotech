<%@ Page Language="VB" AutoEventWireup="false" CodeFile="HDUP_WorkOrderFollowUp.aspx.vb" Inherits="ETHelpDesk_HDUP_WorkOrderFollowUp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>AAMS::HelpDesk::Manage Order Follow Up</title>
    <link href="../CSS/AAMS.css" type="text/css" rel="stylesheet" />
</head>
<body style="font-size: 12pt; font-family: Times New Roman">
    <form id="frmManageContactType" runat="server" defaultbutton="btnSave">
     <table width="860px" align="left" height="486px" class="border_rightred">
            <tr>
                <td valign="top">
                    <table width="100%" align="left">
                        <tr>
                            <td valign="top"  align="left" >
                            <span class="menu">ETrackers HelpDesk-&gt;</span><span class="sub_menu">Manage Work Order FollowUp</span></td>
                        </tr>
                        <tr>
                            <td class="heading" align="center" valign="top">
                                Manage Order FollowUp</td>
                        </tr>
                        <tr>
                            <td valign="top">
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td class="redborder center">
                                    <table border="0" cellpadding="2" cellspacing="1" style="width: 100%" class="left">
                                        <tr>
                                            <td style="width: 176px">
                                            </td>
                                            <td colspan="2" style="text-align: center" class="gap">
                                                    <asp:Label ID="lblError" runat="server" CssClass="ErrorMsg" EnableViewState="False"></asp:Label></td>
                                            <td>
                                            </td>
                                        </tr>
                                                                        <tr>
                                                                            <td style="width: 176px">
                                                                            </td>
                                                                            <td class="textbold">
                                                                                Follow Up<span class="Mandatory" >*</span></td>
                                                                            <td style="width: 308px">
                                            <asp:TextBox ID="txtFollowUp" runat="server" CssClass="textbox" MaxLength="25" TabIndex="1" Width="208px"></asp:TextBox></td>
                                                                            <td>
                                                                    <asp:Button ID="btnSave" CssClass="button" runat="server" Text="Save" TabIndex="9" AccessKey="s" /></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="width: 176px">
                                                                            </td>
                                                                            <td class="textbold">
                                                                                </td>
                                                                            <td style="width: 308px">
                                                                    </td>
                                                                            <td>
                                                                    <asp:Button ID="btnNew" CssClass="button" runat="server" Text="New" TabIndex="10" AccessKey="n" /></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="width: 176px; height: 26px;">
                                                                            </td>
                                                                            <td class="textbold" style="height: 26px">
                                                             </td>
                                                                            <td style="width: 308px; height: 26px;">
                                                                    </td>
                                                                            <td style="height: 26px">
                                                                    <asp:Button ID="btnReset" CssClass="button" runat="server" Text="Reset" TabIndex="11" AccessKey="r" /></td>
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
                                            <td colspan="2" class="ErrorMsg">
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
     if(document.getElementById('<%=txtFollowUp.ClientId%>').value =='')
        {
            document.getElementById('<%=lblError.ClientId%>').innerText='Follow Up is mandatory.'
            document.getElementById('<%=txtFollowUp.ClientId%>').focus();
            return false;
        }
//        else
//        {
//            var strValue = document.getElementById('<%=txtFollowUp.ClientId%>').value
//            reg = new RegExp("^[a-zA-Z ]+$"); 

//            if(reg.test(strValue) == false) 
//            {

//                document.getElementById('<%=lblError.ClientId%>').innerText ='Follow Up should contain only alphabets.'
//                return false;

//             }
//        }
       return true; 
        
    }
    
    function ClearControls()
    {
        document.getElementById("txtFollowUp").value=""
        document.getElementById("lblError").innerText=""
        return false;
    }
    </script>
</body>

</html>
