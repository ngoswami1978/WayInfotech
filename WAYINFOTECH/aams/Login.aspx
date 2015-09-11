<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Login.aspx.vb" Inherits="_Default" ValidateRequest="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>AAMS: Amadeus Agent Managment System</title>
    <link href="CSS/AAMS.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" language="javascript" src="JavaScript/Login.js"></script>
   <noscript>
   <meta http-equiv="REFRESH" content="1; URL=Information.aspx" />
   
    </noscript>
<script type="text/jscript" language="javascript">
function f1()
{
document.getElementById("txtPassword").select();
}
    function PageLoad()
    {
        document.getElementById('<%=txtUserId.ClientID%>').focus();        
    }
</script>
</head>
<body onload="PageLoad()">
    <form id="form1" runat="server">
        <table width="901" height="571" border="0" align="center" cellpadding="0" cellspacing="0"
            id="Table_01">
            <tr>
                <td colspan="2" rowspan="4" bgcolor="#1B61A9">
                </td>
                <td colspan="2" bgcolor="#1B61A9">
                </td>
                <td colspan="6">
                    <img src="images/login_slice_03.jpg" width="584" height="14" alt="" /></td>
                <td>
                    <img src="images/spacer.gif" width="1" height="14" alt="" /></td>
            </tr>
            <tr>
                <td colspan="3">
                    <img src="images/login_slice_04.jpg" width="421" height="43" alt="" /></td>
                <td colspan="5">
                    <img src="images/login_slice_05.jpg" width="446" height="43" alt="" /></td>
                <td>
                    <img src="images/spacer.gif" width="1" height="43" alt="" /></td>
            </tr>
            <tr>
                <td rowspan="3" bgcolor="#1B61A9">
                    &nbsp;</td>
                <td colspan="7">
                    <img src="images/login_slice_07.jpg" width="671" height="1" alt="" /></td>
                <td>
                    <img src="images/spacer.gif" width="1" height="1" alt="" /></td>
            </tr>
            <tr>
<td colspan="7" rowspan="3">
<asp:image runat="server" id="imgLoginSlice" ImageUrl="images/login_slice_08.jpg" width="671" height="28" AlternateText="" /></td>
<td>
<img src="images/spacer.gif" width="1" height="26" alt="" /></td>
</tr>

            <tr>
                <td colspan="2" rowspan="7">
                    <img src="images/login_slice_09.jpg" width="33" height="310" alt="" /></td>
                <td>
                    <img src="images/spacer.gif" width="1" height="1" alt="" /></td>
            </tr>
            <tr>
                <td style="height: 1px">
                    <img src="images/login_slice_10.jpg" width="196" height="1" alt="" /></td>
                <td style="height: 1px">
                    <img src="images/spacer.gif" width="1" height="1" alt="" /></td>
            </tr>
            <tr>
                <td colspan="4" rowspan="6">
                    <img src="images/login_slice_11.jpg" width="490" height="463" alt="" /></td>
                <td colspan="4" background="images/login_slice_12.jpg">
                    &nbsp;</td>
                <td>
                    <img src="images/spacer.gif" width="1" height="60" alt="" /></td>
            </tr>
            <tr>
                <td rowspan="3">
                    <img src="images/login_slice_13.jpg" width="27" height="193" alt="" /></td>
                <td colspan="2">
                    <img src="images/login_slice_14.jpg" width="304" height="21" alt="" /></td>
                <td rowspan="3">
                    <img src="images/login_slice_15.jpg" width="46" height="193" alt="" /></td>
                <td>
                    <img src="images/spacer.gif" width="1" height="21" alt="" /></td>
            </tr>
            <tr>
                <td width="304" height="152" colspan="2" valign="top" background="images/login_slice_16.jpg"
                    >
                    <table width="85%" border="0" align="center" cellpadding="3" cellspacing="0">
                        <tr>
                            <td colspan="2" style="height:30px" align="center" class="ErrorMsg">
                                <asp:Label ID="lblError" runat="server" Height="20px"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td width="38%" class="textbold">
                                <strong>User Name <span class="Mandatory">*</span></strong></td>
                            <td width="62%">
                                <asp:TextBox ID="txtUserId" runat="server" CssClass="textfield" Width="150px"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td>
                                <span class="textbold"><strong>Password</strong></span> <strong class="Mandatory">*</strong></td>
                            <td>
                                <asp:TextBox ID="txtPassword" runat="server" CssClass="textfield" TextMode="Password"
                                    Width="150px" ></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td colspan="2">
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" class="ErrorMsg" style="height: 25px">
                                <strong>Fields marked * are mandatory</strong></td>
                        </tr>
                        <tr>
                            <td colspan="2" class="ErrorMsg" align="center" >
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" class="red" style="height: 10px">
                                <div align="center">
                                    <asp:ImageButton ID="imgLogin" runat="server" ImageUrl="images/login_btn.gif" OnClientClick="return Validate()" /></div>
                            </td>
                        </tr>
                    </table>
                </td>
                <td>
                    <img src="images/spacer.gif" width="1" height="10px" alt="" /></td>
            </tr>
            <tr>
                <td colspan="2">
                    <img src="images/login_slice_17.jpg" width="304" alt="" /></td>
                <td>
                    <img src="images/spacer.gif" width="1" height="20" alt="" /></td>
            </tr>
            <tr>
                <td rowspan="2">
                    <img src="images/login_slice_18.jpg" width="27" height="210" alt="" /></td>
                <td rowspan="2">
                    <img src="images/login_slice_19.jpg" width="20" height="210" alt="" /></td>
                <td colspan="2" rowspan="2">
                    <img src="images/login_slice_20.jpg" width="330" height="210" alt="" /></td>
                <td>
                    <img src="images/spacer.gif" width="1" height="55" alt="" /></td>
            </tr>
            <tr>
                <td colspan="2">
                    <img src="images/login_slice_21.jpg" width="33" height="155" alt="" /></td>
                <td>
                    <img src="images/spacer.gif" width="1" height="155" alt="" /></td>
            </tr>
            <tr>
                <td colspan="10" background="images/login_slice_22.jpg">
                    <div align="center">
                        <span class="Login_bottom">&copy; 2007 <a href="http://bis.co.in" class="Login_bottom">
                            Bird Information Systems (BIS)</a>.All Rights Reserved </span>
                    </div>
                </td>
                <td>
                    <img src="images/spacer.gif" width="1" height="21" alt="" /></td>
            </tr>
            <tr>
                <td>
                    <img src="images/spacer.gif" width="24" height="1" alt="" /></td>
                <td>
                    <img src="images/spacer.gif" width="9" height="1" alt="" /></td>
                <td>
                    <img src="images/spacer.gif" width="196" height="1" alt="" /></td>
                <td>
                    <img src="images/spacer.gif" width="87" height="1" alt="" /></td>
                <td>
                    <img src="images/spacer.gif" width="138" height="1" alt="" /></td>
                <td>
                    <img src="images/spacer.gif" width="69" height="1" alt="" /></td>
                <td>
                    <img src="images/spacer.gif" width="27" height="1" alt="" /></td>
                <td>
                    <img src="images/spacer.gif" width="20" height="1" alt="" /></td>
                <td>
                    <img src="images/spacer.gif" width="284" height="1" alt="" /></td>
                <td>
                    <img src="images/spacer.gif" width="46" height="1" alt="" /></td>
                <td>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
