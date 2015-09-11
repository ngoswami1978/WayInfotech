<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ChangPassword.aspx.vb" Inherits="ChangPassword" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>AAMS::Change Password</title>
    <link href="CSS/AAMS.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" language="javascript" >
    /*********************************************************************
                        Method for Reset
    *********************************************************************/
    function ConfirmPasswordReset()
    {
        document.getElementById("txtOldPassword").value="";       
        document.getElementById("txtNewPassword").value="";
        document.getElementById("txtConfirmPassword").value="";    
        return false;
    }
    /*********************************************************************
            Method for check New Password and Check Password
    *********************************************************************/
    function CheckNewConfirmPassword()
    {
        if(document.getElementById("txtOldPassword").value=="")
       {
          document.getElementById("lblError").innerHTML="Please enter old password";
          document.getElementById("txtOldPassword").focus();
          return false;           
       }
       if(document.getElementById("txtNewPassword").value=="")
       {
          document.getElementById("lblError").innerHTML="Please enter new password";
          document.getElementById("txtNewPassword").focus();
          return false;           
       }
       if(document.getElementById("txtConfirmPassword").value=="")
       {
          document.getElementById("lblError").innerHTML="Please enter confirm password.";
          document.getElementById("txtConfirmPassword").focus();
          return false;           
       }   
       if(document.getElementById("txtNewPassword").value!=document.getElementById("txtConfirmPassword").value)
       {
          document.getElementById("lblError").innerHTML="Please enter new password and confirm password same.";
          document.getElementById("txtNewPassword").focus();
          return false;           
       } 
    }
    
    </script>
</head>

<body>
    <form id="form1" runat="server">
    <table   border="0" align="center" cellpadding="0" cellspacing="0" id="Table_01" class="redborder" style="width: 860px">
    <tr>
                <%--<td bgcolor="#1B61A9">
                </td>--%>
              <td  bgcolor="#1B61A9" style="width: 403px">
                </td>
                 <td  bgcolor="#1B61A9">
                </td>
               <%-- <td>
                    <img src="images/login_slice_03.jpg" height="16" alt="" style="width: 440px" />
                </td>--%>
                <td style="height: 4px;">
                    <img src="images/spacer.gif" width="1" height="14" alt="" />
                 </td>
            </tr>
    <tr valign="top" >
                <td valign="top" style="height: 47px; width: 403px;">
                    <img src="Images/login_slice_04.jpg" width="421" height="43" alt="" /></td>
                <td style="height: 47px"  >
                    <img src="images/login_slice_05.jpg" height="43" alt="" style="width: 440px" /></td>
                <td style="height: 47px;">
                    <img src="images/spacer.gif" width="1" height="43" alt="" />
                    </td>
            </tr>
            
            <tr>
            
            <td colspan="3" style="height: 486px">
             <table width="860px" align="left" height="486px">
            <tr>
                <td valign="top" style="width: 860px">
                    <table width="100%" align="left">
                        
                        <tr>
                            <td class="heading" align="center" valign="top" style="width: 880px">
                                Change Password
                            </td>
                        </tr>
                        <tr>
                            <td valign="top">
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td align="LEFT" >
                                 
                                                        <table width="100%" border="0" align="left" cellpadding="0" cellspacing="0" >
                                                            <tr>
                                                                <td  class="textbold" colspan="6" align="center" height="25px" valign="TOP">
                                                                <asp:Label ID="lblError" runat="server" CssClass="ErrorMsg"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td width="15%" class="textbold">
                                                                    &nbsp;</td>
                                                                <td width="15%" class="textbold">
                                                                    Old Password <span class="ErrorMsg">*</span></td>
                                                                <td width="20%" style="height: 25px" >
                                                                    <asp:TextBox ID="txtOldPassword" TextMode="Password" CssClass="textbox" TabIndex="1" runat="server"></asp:TextBox></td>
                                                               
                                                                <td width="25%">
                                                                    <asp:Button ID="btnChange" CssClass="button" runat="server" Text="Change" TabIndex="4" /></td>
                                                            </tr>
                                                            <tr>
                                                                
                                                                <td style="height: 25px">
                                                                   </td>
                                                                <td class="textbold" style="height: 25px"><span class="ErrorMsg"></span>&nbsp;New Password <span class="ErrorMsg">*</span></td>
                                                                <td style="height: 25px">
                                                                <asp:TextBox ID="txtNewPassword" TextMode="Password" CssClass="textbox" TabIndex="2" runat="server"></asp:TextBox></td>
                                                                <td style="height: 25px">
                                                                    <asp:Button ID="btnReset" CssClass="button" runat="server" Text="Reset" TabIndex="5" /></td>
                                                            </tr>
                                                            
                                                            <tr>                                                                
                                                                <td>
                                                                    &nbsp;</td>
                                                                 <td class="textbold">
                                                                     Confirm Password <span class="ErrorMsg">*</span>
                                                                </td>
                                                                <td style="height: 25px">
                                                                    <asp:TextBox ID="txtConfirmPassword" TextMode="Password" CssClass="textbox" TabIndex="3" runat="server"></asp:TextBox></td>
                                                                <td>
                                                                    </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                </td>
                                                                <td class="textbold">
                                                                </td>
                                                                <td>
                                                                    </td>
                                                                <td>
                                                                </td>
                                                            </tr>
                                                            
                                                            <tr height="20px">
                                                              <td class="textbold" style="height: 20px">
                                                                        &nbsp;</td>
                                                                    <td colspan="2" class="ErrorMsg" style="height: 20px">
                                                                        Field Marked * are Mandatory</td>
                                                                    <td style="height: 20px">
                                                                        &nbsp;</td>
                                                                                                                                 
                                                            </tr>
                                                            
                                                           
                                                            <tr>
                                                                <td colspan="4" height="12">
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
            
            </td>
            
            </tr>
    </table>
    </form>
</body>
</html>
