<%@ Page Language="VB" AutoEventWireup="false" CodeFile="TCUP_Priority.aspx.vb" Inherits="HelpDesk_Technical_MSUP_Priority" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
     <title>AAMS::Technical::Manage Priority</title>
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
                            <span class="menu">Technical-&gt;</span><span class="sub_menu">Manage Priority </span></td>
                        </tr>
                        <tr>
                            <td class="heading" align="center" valign="top">
                                Manage Priority</td>
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
                                            <td class="gap" colspan="2" style="text-align: center">
                                                    <asp:Label ID="lblError" runat="server" CssClass="ErrorMsg" EnableViewState="False"></asp:Label></td>
                                            <td>
                                            </td>
                                        </tr>
                                                                        <tr>
                                                                            <td style="width: 176px">
                                                                            </td>
                                                                            <td class="textbold">
                                                                                Priority Name<span class="Mandatory" >*</span></td>
                                                                            <td style="width: 308px">
                                                                                <asp:TextBox ID="txtPriority" runat="server" CssClass="textbox" MaxLength="25" TabIndex="1" Width="208px"></asp:TextBox></td>
                                                                            <td>
                                                                    <asp:Button ID="btnSave" CssClass="button" runat="server" Text="Save" TabIndex="9" AccessKey="s" /></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="width: 176px">
                                                                            </td>
                                                                            <td class="textbold">
                                                                                Color</td>
                                                                            <td style="width: 308px">
                                                                                <asp:TextBox ID="txtColor" runat="server" CssClass="textbox" MaxLength="25" ReadOnly="True"
                                                                                    TabIndex="1" Width="208px"></asp:TextBox>
                                                                                <img id="BtnColor" align="absMiddle" alt="Forecolor" hspace="2" onclick="foreColor(txtColor)"
                                                                                    src="../Images/lookup.gif" style="cursor: pointer" vspace="1" /></td>
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
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 176px; height: 26px">
                                            </td>
                                            <td colspan="2" style="height: 26px">
                                        <asp:HiddenField ID="hdID" runat ="server" />
                                        <input type="hidden" id="hdColor" runat="server" />
                                            </td>
                                            <td style="height: 26px">
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
    function foreColor(txtColor)
	{
		var	arr	= showModalDialog("ColorPalette.htm","","font-family:Verdana;	font-size:12; dialogWidth:18;	dialogHeight:18" );
		if (arr	!= null) 
		document.getElementById('hdColor').value = arr
		document.getElementById('txtColor').style.backgroundColor = arr
	} 
    function ValidateForm()
    {
      document.getElementById('<%=lblError.ClientId%>').innerText=''
     if(document.getElementById('<%=txtPriority.ClientId%>').value =='')
        {
            document.getElementById('<%=lblError.ClientId%>').innerText='Priority name is mandatory.'
            document.getElementById('<%=txtPriority.ClientId%>').focus();
            return false;
        }
//        else
//        {
//            var strValue = document.getElementById('<%=txtPriority.ClientId%>').value
//            reg = new RegExp("^[a-zA-Z ]+$"); 

//            if(reg.test(strValue) == false) 
//            {

//                document.getElementById('<%=lblError.ClientId%>').innerText ='Priority name should contain only alphabets.'
//                return false;

//             }
//        }
       return true; 
        
    }
    
    function ClearControls()
    {
        document.getElementById("txtContactType").value=""
        document.getElementById("lblError").innerText=""
        return false;
    }
    </script>
</body>
</html>
