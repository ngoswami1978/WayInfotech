<%@ Page Language="VB" AutoEventWireup="false" CodeFile="HDUP_CustomerCategory.aspx.vb" Inherits="BirdresHelpDesk_MSUP_CustomerCategory" ValidateRequest="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>AAMS::HelpDesk::Manage Customer Category</title>
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
                            <span class="menu">Birdres HelpDesk-&gt;</span><span class="sub_menu">Manage Category Search</span></td>
                        </tr>
                        <tr>
                            <td class="heading" align="center" valign="top">
                                Manage Customer Category</td>
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
                                            <td class="gap" style="text-align: center;" colspan="2">
                                                    <asp:Label ID="lblError" runat="server" CssClass="ErrorMsg" EnableViewState="False"></asp:Label></td>
                                            <td style="width: 50px">
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                                                        <tr>
                                                                            <td style="width: 176px">
                                                                            </td>
                                                                            <td class="textbold" style="width: 180px">
                                                                                Category Name<span class="Mandatory" >*</span></td>
                                                                            <td style="width: 228px">
                                            <asp:TextBox ID="txtCategory" runat="server" CssClass="textbox" MaxLength="25" TabIndex="1" Width="208px"></asp:TextBox></td>
                                                                            <td style="width: 50px">
                                                                            </td>
                                                                            <td>
                                                                    <asp:Button ID="btnSave" CssClass="button" runat="server" Text="Save" TabIndex="9" AccessKey="s" /></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="width: 176px">
                                                                            </td>
                                                                            <td class="textbold" style="width: 180px">
                                                                                Color<span class="Mandatory" >*</span></td>
                                                                            <td style="width: 228px">
                                            <asp:TextBox ID="txtColor" runat="server" CssClass="textbox" MaxLength="25" TabIndex="1"
                                                Width="208px" ReadOnly="True"></asp:TextBox></td>
                                                                            <td style="width: 50px">
                                        
                                                                                                <img id="BtnColor"  alt="Forecolor" hspace="2" src="../Images/lookup.gif"
                                                                                                    align="absMiddle" vspace="1" onClick="foreColor(txtColor)"></td>
                                                                            <td>
                                                                    <asp:Button ID="btnNew" CssClass="button" runat="server" Text="New" TabIndex="10" AccessKey="n" /></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="width: 176px; height: 26px;">
                                                                            </td>
                                                                            <td class="textbold" style="width: 180px;">
                                                             </td>
                                                                            <td style="width: 228px;">
                                                                    </td>
                                                                            <td style="width: 50px">
                                                                            </td>
                                                                            <td style="height: 26px">
                                                                    <asp:Button ID="btnReset" CssClass="button" runat="server" Text="Reset" TabIndex="11" AccessKey="r" /></td>
                                                                        </tr>
                                        <tr>
                                            <td style="width: 176px; height: 26px">
                                            </td>
                                            <td class="textbold" style="width: 180px;">
                                            </td>
                                            <td style="width: 228px;">
                                            </td>
                                            <td style="width: 50px">
                                            </td>
                                            <td style="height: 26px">
                                        
                                                                                                </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 176px; height: 26px">
                                            </td>
                                            <td class="textbold" style="width: 180px;">
                                                <asp:HiddenField ID="hdColor" runat ="server" />
                                            </td>
                                            <td style="width: 228px;">
                                        <asp:HiddenField ID="hdID" runat ="server" />
                                            </td>
                                            <td style="width: 50px">
                                            </td>
                                            <td style="height: 26px">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 176px; height: 26px">
                                            </td>
                                            <td colspan="2" class="ErrorMsg">
                                                Field Marked * are Mandatory</td>
                                            <td colspan="1" style="width: 50px">
                                            </td>
                                            <td style="height: 26px">
                                            </td>
                                        </tr>
                                                                          <tr>
                                                                        </tr>
                                                                        <tr>
                                                                            <td colspan="5" style="text-align: center">
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
     if(document.getElementById('<%=txtCategory.ClientId%>').value =='')
        {
        document.getElementById('<%=txtCategory.ClientId%>').focus();
            document.getElementById('<%=lblError.ClientId%>').innerText='Category is mandatory.'
            return false;
        }
        
        if(document.getElementById('<%=hdColor.ClientId%>').value =='')
        {
            document.getElementById('<%=lblError.ClientId%>').innerText='Color is mandatory.'
            return false;
        }
        
  //      hdColor.Value
//        else
//        {
//            var strValue = document.getElementById('<%=txtCategory.ClientId%>').value
//            reg = new RegExp("^[a-zA-Z ]+$"); 

//            if(reg.test(strValue) == false) 
//            {

//                document.getElementById('<%=lblError.ClientId%>').innerText ='Category should contain only alphabets.'
//                return false;

//             }
//        }
       return true; 
        
    }
    
    function ClearControls()
    {
        document.getElementById("txtColor").value=""
        document.getElementById("txtCategory").value=""
        document.getElementById("lblError").innerText=""
        return false;
    }
    </script>
</body>
</html>
