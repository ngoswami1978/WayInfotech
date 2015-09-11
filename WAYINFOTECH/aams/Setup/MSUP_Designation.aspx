<%@ Page Language="VB" AutoEventWireup="false" CodeFile="MSUP_Designation.aspx.vb"
    Inherits="Setup_MSUP_Designation" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>AAMS: Manage Designation</title>
    <link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />

    <script src="../JavaScript/AAMS.js" type="text/javascript"></script>
    
    <script type="text/javascript" >
    function chkFields()
    {
        if (document.getElementById("txtDesignation").value=="")
        {          
             document.getElementById("lblError").innerText="Designation is Mandatory"
             document.getElementById("txtDesignation").focus();
             return false;
        }
        if(IsDataValid(document.getElementById("txtDesignation").value,2)==false)
            {   document.getElementById("lblError").innerHTML=" Designation is not valid.";
                document.getElementById("txtDesignation").focus();
                return false;
            }
    }
     
   
    function optionSelection()
    {
    if (document.getElementById("optEmployee").checked==true)
    {
      document.getElementById("cboDesignation").style.display ="none";
      document.getElementById("cboEmployee").style.display ="block";   
    }
   else  if (document.getElementById("optDesignation").checked==true)
    {
       document.getElementById("cboDesignation").style.display ="block";
       document.getElementById("cboEmployee").style.display ="none";   
    }
    else 
    {
       document.getElementById("cboDesignation").style.display ="block";
       document.getElementById("cboEmployee").style.display ="none";   
    }

    }
    
    function NewFunction()
    {   
        window.location.href="MSUP_Designation.aspx?Action=I";       
        return false;
    }
    </script>
    <link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />
</head>
<body  onload="optionSelection();" >
    <form id="frmUpdateManageDesignation" runat="server">
        <table width="845px" align="left" class="border_rightred">
            <tr>
                <td valign="top">
                    <table width="100%" align="left">
                        <tr>
                            <td valign="top" align="left">
                                <span class="menu">Setup-></span><span class="sub_menu">Designation</span>
                            </td>
                        </tr>
                        <tr>
                            <td class="heading" align="center" valign="top" style="height: 10px">
                                Manage Designation
                            </td>
                        </tr>
                        <tr>
                            <td valign="top">
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td align="LEFT" class="redborder">
                                            <table width="100%" border="0" align="left" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td class="textbold" colspan="4" height="25px" align="center" valign="TOP">
                                                        <asp:Label ID="lblError" runat="server" CssClass="ErrorMsg"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr >
                                                </tr>
                                                <tr>
                                                    <td class="textbold" width="20%">
                                                    </td>
                                                    <td class="textbold" width="10%">
                                                    </td>
                                                    <td width="20%">
                                                    </td>
                                                    <td style="text-align: right" width="10%">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="20%" class="textbold">
                                                        </td>
                                                    <td width="15%" class="textbold">
                                                        Designation Name <span class="Mandatory">*</span>
                                                    </td>
                                                    <td width="20%">
                                                        <asp:TextBox ID="txtDesignation" CssClass="textbox" runat="server" MaxLength="100" EnableViewState="False"></asp:TextBox></td>
                                                    <td width="20%" align="center" >
                                                        <asp:Button ID="btnSave" CssClass="button" runat="server" Text="Save" /></td>
                                                </tr>
                                                <tr>
                                                    <td class="textbold" colspan="4" align="center" valign="TOP">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width ="20%">
                                                    </td>
                                                    <td width = "15%">
                                                    </td>
                                                    <td width = "20%">
                                                    </td>
                                                    <td width ="20%" align="center">
                                                        <asp:Button ID="btnNew" CssClass="button" runat="server" Text="New"  AccessKey="N"/></td>
                                                </tr>
                                                <tr>
                                                    <td class="textbold" colspan="4" align="center" valign="TOP">
                                                    </td>
                                                </tr>
                                                
                                                <tr>
                                                    <td>
                                                        </td>
                                                    <td>
                                                    </td>
                                                    <td>
                                                    </td>
                                                    <td align="center">
                                                        <asp:Button ID="btnReset" CssClass="button" runat="server" Text="Reset" AccessKey="R" /></td>
                                                </tr>
                                                
                                                 <tr>
                                                    <td class="textbold" colspan="4" align="center" valign="TOP">
                                                    </td>
                                                </tr>
                                                
                                                 <tr>
                                                    <td>
                                                        </td>
                                                    <td>
                                                    </td>
                                                    <td>
                                                    </td>
                                                    <td align="center">
                                                        <asp:Button ID="btnGrantAll" CssClass="button" runat="server" Text="Grant All" /></td>
                                                </tr>
                                                
                                                 <tr>
                                                    <td class="textbold" colspan="4" align="center" valign="TOP">
                                                    </td>
                                                </tr>
                                                
                                                 <tr>
                                                    <td>
                                                        </td>
                                                    <td>
                                                    </td>
                                                    <td>
                                                    </td>
                                                    <td align="center">
                                                       <asp:Button ID="btnRevokeAll" CssClass="button" runat="server" Text="Revoke All" /></td>
                                                </tr>
                                                
                                                <tr><td>
                                                </td>
                                                    <td colspan="2" class="ErrorMsg">
                                                        Field Marked * are Mandatory
                                                    </td>
                                                    <td>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                    </td>
                                                    <td >
                                                    </td>
                                                    <td >
                                                    </td>
                                                    <td >
                                                    </td>
                                                </tr>
                                                <tr >
                                                
                                                    <td colspan="4" >
                                                                        <table cellpadding="0" cellspacing="0"  style="width: 100%">
                                                                            <tr>
                                                                                <td align="center" class ="subheading" colspan="4" >
                                                                                    Permission
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td class="textbold" width="12%" >
                                                                                </td>
                                                                                <td class="textbold" style="text-align: left; " width="12%">
                                                                                </td>
                                                                                <td class="textbold" width="12%" >
                                                                                </td>
                                                                                <td class="textbold" width="15%" style="height: 15px">
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td class="textbold" width="12%">
                                                                                </td>
                                                                                <td class="textbold" style="text-align: left" width="12%">
                                                                                </td>
                                                                                <td class="textbold" width="12%">
                                                                                </td>
                                                                                <td class="textbold" width="15%">
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td width="12%" class="textbold" style="height: 20px">
                                                                                </td>
                                                                                <td width="12%" class="textbold" style="text-align: left; height: 20px;">
                                                                                    <asp:RadioButton ID="optDesignation" CssClass="textfield" runat="server" GroupName="Copy"
                                                                                        TabIndex="2" Text="Copy Permission from Designation" Width="232px" Checked="True" /></td>
                                                                                <td width="12%" class="textbold" style="text-align: left; height: 20px;">
                                                                                    <asp:DropDownList  onkeyup="gotop(this.id)"  ID="cboDesignation" runat="server" CssClass="dropdown" TabIndex="3">
                                                                                    <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                                                    </asp:DropDownList></td>
                                                                                <td width="15%" class="textbold" style="height: 20px">
                                                                                </td>
                                                                            </tr>
                                                                            <tr >
                                                                                <td style="width: 100px;" >
                                                                                </td>
                                                                                <td style="width: 100px; ">
                                                                                </td>
                                                                                <td style="width: 100px; ">
                                                                                </td>
                                                                                <td style="width: 100px; ">
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="width: 100px; height: 20px;">
                                                                                </td>
                                                                                <td style="width: 90px; height: 20px; text-align: left;">
                                                                                    <asp:RadioButton ID="optEmployee" CssClass="textfield" runat="server" GroupName="Copy"
                                                                                        TabIndex="4" Text="Copy Permission from Employee" Width="231px" /></td>
                                                                                <td style="width: 100px; height: 20px">
                                                                                    <asp:DropDownList  onkeyup="gotop(this.id)"  ID="cboEmployee" runat="server" CssClass="dropdown" TabIndex="5">
                                                                                    <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                                                   </asp:DropDownList></td>
                                                                                <td style="width: 100px; height: 20px">
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="width: 100px; height: 20px">
                                                                                </td>
                                                                                <td style="width: 90px; height: 20px">
                                                                                </td>
                                                                                <td style="width: 100px; height: 20px; text-align: left;">
                                                                                    <asp:Button ID="btnApply" CssClass="button" runat="server" Text="Apply" /></td>
                                                                                <td style="width: 100px; height: 20px">
                                                                                </td>
                                                                            </tr>
                                                                          
                                                                            <tr>
                                                                                <td style="width: 100px; height: 19px;">
                                                                                </td>
                                                                                <td style="width: 25%; text-align: right; height: 19px;">
                                                                                </td>
                                                                                <td style="width: 100px; height: 19px;">
                                                                                    </td>
                                                                                <td style="width: 100px; height: 19px;">
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="width: 100px; height: 19px;">
                                                                                </td>
                                                                                <td style="width: 25%; text-align: right; height: 19px;">
                                                                                </td>
                                                                                <td style="width: 100px; height: 19px;">
                                                                                </td>
                                                                                <td style="width: 100px; height: 19px;">
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td colspan="4">
                                                                        <asp:GridView  ID="grdPermission" runat="server" AutoGenerateColumns="False" Width="100%" DataKeyNames="Sec_Group_ID"
                                                                            HeaderStyle-CssClass="heading" AlternatingRowStyle-CssClass="lightblue" RowStyle-CssClass="ItemColor" >
                                                                            
                                                                            <Columns>
                                                                            <asp:BoundField  DataField="Sec_Group" HeaderText="Group" />
                                                                              <asp:BoundField DataField="SecurityOptionSubName" HeaderText="SubGroup Name" />
                                                                                
                                                                                <asp:TemplateField HeaderText="View">
                                                                                <ItemTemplate>
                                                                                <asp:CheckBox ID="chkView" runat="server"/>
                                                                                </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                
                                                                                <asp:TemplateField HeaderText="Add">
                                                                                    <ItemTemplate>
                                                                                        <asp:CheckBox ID="chkAdd" runat="server" />
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                
                                                                                <asp:TemplateField HeaderText="Modify">
                                                                                    <ItemTemplate>
                                                                                        <asp:CheckBox ID="chkModify" runat="server"/>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                
                                                                                <asp:TemplateField HeaderText="Delete">
                                                                                    <ItemTemplate>
                                                                                        <asp:CheckBox ID="chkDelete" runat="server"/>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                
                                                                                <asp:TemplateField HeaderText="Print">
                                                                                    <ItemTemplate>
                                                                                        <asp:CheckBox ID="chkPrint" runat="server"/>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                 <asp:BoundField DataField="SecurityOptionID" HeaderText="Hidden" HeaderStyle-Width="1%" HeaderStyle-CssClass="displayNone" ItemStyle-CssClass="displayNone"></asp:BoundField>
                                                                                
                                                                            </Columns>
                                                                            <RowStyle CssClass="ItemColor" />
                                                                            <HeaderStyle CssClass="heading" />
                                                                            <AlternatingRowStyle CssClass="lightblue" />
                                                                        </asp:GridView></td>
                                                                            </tr>
                                                                        </table>
                                                                        <br />                                                                    
                                                                </td>
                                                    
                                                </tr>
                                                <tr>
                                                    <td >
                                                    </td>
                                                    <td >
                                                    </td>
                                                    <td >
                                                    </td>
                                                    <td >
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                    </td>
                                                    <td >
                                                    </td>
                                                    <td>
                                                    </td>
                                                    <td>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="4" height="4">
                                                    </td>
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
    </form>
</body>
</html>
