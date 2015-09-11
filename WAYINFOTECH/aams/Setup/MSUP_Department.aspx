<%@ Page Language="VB" AutoEventWireup="false" CodeFile="MSUP_Department.aspx.vb"
    Inherits="Setup_MSUP_Department" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>AAMS: Department</title>
    <link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />

    <script src="../JavaScript/AAMS.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
   
   
  //     function DepartmentReset()
  //  {
  //      document.getElementById("txtDepartment").value="";
   //     document.getElementById("cboManagerName").selectedIndex=0;
   //     document.getElementById("lblError").innerHTML = ""
   //             return false;
   // }
    
    
    
    function DepartmentMandatory()
    {
                    if(document.getElementById("txtDepartment").value=="")
                    {
                        document.getElementById("lblError").innerText="Please enter Department Name.";
                        //document.getElementById("txtDepartmentName").focus();
                        document.getElementById("txtDepartment").focus();
                        return false;
                    }
                     
     
    }

    
    </script>

</head>
<body>
    <form id="frmDepartment" runat="server">
         <table width="860px" align="left" height="486px" class="border_rightred">
            <tr>
                <td valign="top">
                    <table width="100%" align="left">
                        <tr>
                            <td valign="top" align="left">
                                <span class="menu">Setup-></span><span class="sub_menu">Department</span>
                            </td>
                        </tr>
                        <tr>
                            <td class="heading" align="center" valign="top" style="height: 20px">
                                Update Department
                            </td>
                        </tr>
                        <tr>
                            <td valign="top" style="height: 346px">
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td align="LEFT" class="redborder">
                                            <table width="100%" border="0" align="left" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td class="textbold" colspan="6" align="center" height="25px" valign="TOP">
                                                        <asp:Label ID="lblError" runat="server" CssClass="ErrorMsg"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="20%" class="textbold" style="height: 22px">
                                                        &nbsp;</td>
                                                    <td width="20%" class="textbold" style="height: 22px">
                                                        Department Name <span class="Mandatory">*</span>  </td>
                                                    <td width="30%" style="height: 22px">
                                                        <asp:TextBox ID="txtDepartment" CssClass="textbox" runat="server" MaxLength="50" TabIndex="1"></asp:TextBox></td>
                                                    <td width="20%" style="height: 22px">
                                                        <asp:Button ID="btnSave" CssClass="button" runat="server" Text="Save" TabIndex="3"  AccessKey="S"/></td>
                                                </tr>
                                                <tr>
                                                    <td class="textbold" >
                                                    </td>
                                                    <td class="textbold">
                                                    </td>
                                                    <td>
                                                    </td>
                                                    <td>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="textbold" style="height: 22px">
                                                        &nbsp;</td>
                                                    <td class="textbold" style="height: 22px">
                                                        Manager Name</td>
                                                                       <td style="height: 22px">
                                                        <asp:DropDownList  onkeyup="gotop(this.id)"  ID="drpManagerName" runat="server" CssClass="dropdown" TabIndex="2">
                                                         </asp:DropDownList></td>
                                                        <td style="height: 22px">
                                                        <asp:Button ID="btnNew" CssClass="button" runat="server" AccessKey="N" Text="New" TabIndex="4" /></td>
                                                </tr>
                                                <tr>
                                                    <td class="textbold" colspan="6" align="center" valign="TOP">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="height: 22px">
                                                    </td>
                                                    <td class="textbold" style="height: 22px">
                                                    </td>
                                                    <td style="height: 22px">
                                                    </td>
                                                    <td style="height: 22px">
                                                        <asp:Button ID="btnReset" CssClass="button" runat="server" Text="Reset" TabIndex="5"  AccessKey="R" /></td>
                                                </tr>
                                                <tr>
                                                    <td style="height: 22px">
                                                    </td>
                                                    <td colspan="3" class="ErrorMsg" style="height: 19px">
                                                        Field Marked * are Mandatory
                                                     <td>   &nbsp;</td>
                                                    <td style="height: 22px">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="textbold" colspan="6" align="center" valign="TOP">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="6" height="12">
                                                        &nbsp;</td>
                                                </tr>
                                                <tr>
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
        </table>
    </form>
</body>
</html>
