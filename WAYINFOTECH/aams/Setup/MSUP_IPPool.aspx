<%@ Page Language="VB" AutoEventWireup="false" CodeFile="MSUP_IPPool.aspx.vb" Inherits="Setup_MSUP_IPPool" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>AAMS: Employee</title>
    <link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />

    <script src="../JavaScript/AAMS.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript">
      function  NewMSUPIPPool()
   {    
       window.location="MSUP_IPPool.aspx?Action=I";
       return false;
   }
//    function IPPoolDataReset()
//    {
//        document.getElementById("txtPoolName").value="";
//        document.getElementById("txtIpAddress").value="";
//        document.getElementById("lblError").innerHTML="";     
//        document.getElementById("drpLstAoffice").selectedIndex=0;        
//        document.getElementById("drpLstDepartment").selectedIndex=0;  
//        if (document.getElementById("dbgrdIpPool")!=null) 
//        document.getElementById("dbgrdIpPool").style.display ="none";  
//        document.getElementById("txtPoolName").focus();     
//        return false;
//    }    
     function CheckMandatotyForAdd()
    {
        if (document.getElementById("txtPoolName").value.trim()=="")
        {          
             document.getElementById("lblError").innerHTML="Pool name is mandatory.";
             document.getElementById("txtPoolName").focus();
             return false;
        }
          if (document.getElementById("txtPoolName").value!="")
             {
               if(IsDataValid(document.getElementById("txtPoolName").value,7)==false)
                {
                document.getElementById("lblError").innerHTML="Pool name is not valid.";
                document.getElementById("txtPoolName").focus();
                return false;
                } 
             } 
        
        if (document.getElementById("drpLstAoffice").selectedIndex==0)
        {          
             document.getElementById("lblError").innerHTML="Aoffice is mandatory.";
             document.getElementById("drpLstAoffice").focus();
             return false;
        }
        
        if (document.getElementById("drpLstDepartment").selectedIndex==0)
        {           
          document.getElementById("lblError").innerHTML="Department is mandatory.";
          document.getElementById("drpLstDepartment").focus();
          return false;
        }        
       
        if (document.getElementById("txtIpAddress").value.trim()=="")
        {           
          document.getElementById("lblError").innerHTML="IP address is mandatory.";
          document.getElementById("txtIpAddress").focus();
          return false;
        }   
         if (  document.getElementById("txtIpAddress").value!="")
         {
           if(IsDataValid(document.getElementById("txtIpAddress").value,5)==false)
            {
            document.getElementById("lblError").innerHTML="IP address is not valid.";
            document.getElementById("txtIpAddress").focus();
            return false;
            } 
         } 
         if (  document.getElementById("txtIpAddress").value!="")
         {
           if(isValidIPAddress(document.getElementById("txtIpAddress").value)==false)
            {
            document.getElementById("lblError").innerHTML="IP address is not valid.";
            document.getElementById("txtIpAddress").focus();
            return false;
            } 
         } 
         
       return true;
    }
        function CheckMandatotyForSave()
      {
        if (document.getElementById("txtPoolName").value.trim()=="")
        {          
             document.getElementById("lblError").innerHTML="Pool name is mandatory.";
             document.getElementById("txtPoolName").focus();
             return false;
        }
        if (document.getElementById("txtPoolName").value!="")
             {
               if(IsDataValid(document.getElementById("txtPoolName").value,7)==false)
                {
                document.getElementById("lblError").innerHTML="Pool name is not valid.";
                document.getElementById("txtPoolName").focus();
                return false;
                } 
             } 
           
          
        
        if (document.getElementById("drpLstAoffice").selectedIndex==0)
        {          
             document.getElementById("lblError").innerHTML="Aoffice is mandatory.";
             document.getElementById("drpLstAoffice").focus();
             return false;
        }
        
        if (document.getElementById("drpLstDepartment").selectedIndex==0)
        {           
          document.getElementById("lblError").innerHTML="Department is mandatory.";
          document.getElementById("drpLstDepartment").focus();
          return false;
        }  
          if (  document.getElementById("txtIpAddress").value!="")
         {
           if(IsDataValid(document.getElementById("txtIpAddress").value,5)==false)
            {
            document.getElementById("lblError").innerHTML="IP address is not valid.";
            document.getElementById("txtIpAddress").focus();
            return false;
            } 
         } 
       return true;
    }
    </script>

</head>
<body>
    <form id="frmIPpoolUpdate" runat="server" defaultfocus="txtPoolName">
        <table width="850px" align="left" height="486px" class="border_rightred">
            <tr>
                <td valign="top">
                    <table width="100%" align="left">
                        <tr>
                            <td valign="top" align="left">
                                <span class="menu">Setup-&gt;</span><span class="sub_menu">IP Pool</span></td>
                        </tr>
                        <tr>
                            <td class="heading" align="center" valign="top">
                                Manage IP Pool</td>
                        </tr>
                        <tr>
                            <td>
                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                    <tr>
                                        <td width="100%" class="redborder">
                                            <table align="left" border="0" cellpadding="2" cellspacing="0" width="100%">
                                                <tr>
                                                    <td class="textbold" style="height: 28px; width: 100%" colspan="4" valign="top">
                                                        <table width="100%" border="0" align="left" cellpadding="0" cellspacing="0">
                                                            <tr>
                                                                <td class="textbold" colspan="6" align="center" height="25px" valign="TOP">
                                                                    <asp:Label ID="lblError" runat="server" CssClass="ErrorMsg" Text=""></asp:Label></td>
                                                            </tr>
                                                            <tr>
                                                                <td width="6%" class="textbold" style="height: 22px">
                                                                    &nbsp;</td>
                                                                <td class="textbold" style="height: 22px; width: 332px;">
                                                                </td>
                                                                <td style="height: 22px; width: 116px;">
                                                                    Pool Name<span class="Mandatory">* </span>
                                                                </td>
                                                                <td height="22px">
                                                                    <span class="textbold">
                                                                        <asp:TextBox ID="txtPoolName" runat="server" CssClass="textfield" TabIndex="1" MaxLength="50"></asp:TextBox></span></td>
                                                                <td width="21%" style="height: 22px">
                                                                </td>
                                                                <td width="18%" style="height: 22px">
                                                                    <asp:Button ID="btnSave" runat="server" CssClass="button" Text="Save" TabIndex="6" AccessKey="A" /></td>
                                                            </tr>
                                                            <tr>
                                                                <td class="textbold" colspan="6" align="center" valign="TOP">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="textbold" style="height: 22px">
                                                                    &nbsp;</td>
                                                                <td class="textbold" style="height: 22px; width: 332px;">
                                                                </td>
                                                                <td height="22px" style="width: 116px">
                                                                    Aoffice<span class="Mandatory">* </span>
                                                                </td>
                                                                <td class="textbold" style="width: 142px; height: 22px">
                                                                    <span class="ErrorMsg">
                                                                        <asp:DropDownList  onkeyup="gotop(this.id)"  ID="drpLstAoffice" runat="server" CssClass="dropdown" TabIndex="2">
                                                                        </asp:DropDownList></span></td>
                                                                <td style="height: 22px">
                                                                </td>
                                                                <td style="height: 22px">
                                                                    <asp:Button ID="btnNew" runat="server" CssClass="button" Text="New" TabIndex="7"  AccessKey="N"/></td>
                                                            </tr>
                                                            <tr>
                                                                <td class="textbold" colspan="6" align="center" valign="TOP">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="textbold" style="height: 22px">
                                                                    &nbsp;</td>
                                                                <td class="textbold" style="height: 22px; width: 332px;">
                                                                </td>
                                                                <td style="height: 22px; width: 116px;">
                                                                    Department<span class="Mandatory">* </span>
                                                                </td>
                                                                <td style="width: 142px; height: 22px">
                                                                    <asp:DropDownList  onkeyup="gotop(this.id)"  ID="drpLstDepartment" runat="server" CssClass="dropdown" TabIndex="3">
                                                                    </asp:DropDownList></td>
                                                                <td style="height: 22px">
                                                                </td>
                                                                <td style="height: 22px">
                                                                    <asp:Button ID="btnReset" runat="server" CssClass="button" Text="Reset" TabIndex="8" AccessKey="R" /></td>
                                                            </tr>
                                                            <tr>
                                                                <td class="textbold" style="height: 14px">
                                                                </td>
                                                                <td class="textbold" style="width: 332px; height: 14px">
                                                                </td>
                                                                <td colspan="2" style="height: 14px" align="center" class="subheading">
                                                                    Add IP</td>
                                                                <td>
                                                                </td>
                                                                <td>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="textbold" style="height: 14px">
                                                                </td>
                                                                <td class="textbold" style="height: 14px; width: 332px;">
                                                                </td>
                                                                <td style="height: 22px; width: 116px;">
                                                                    IP<span class="Mandatory">* </span>
                                                                </td>
                                                                <td style="height: 14px; width: 142px;">
                                                                    <asp:TextBox ID="txtIpAddress" runat="server" CssClass="textfield" TabIndex="4" MaxLength="15"></asp:TextBox></td>
                                                                <td style="height: 14px">
                                                                </td>
                                                                <td style="height: 14px">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="textbold" style="height: 18px">
                                                                </td>
                                                                <td class="textbold" style="height: 18px; width: 332px;">
                                                                </td>
                                                                <td style="height: 18px; width: 116px;">
                                                                </td>
                                                                <td style="height: 18px; width: 142px;">
                                                                    <asp:Button ID="btnAdd" runat="server" CssClass="button" Text="Add" TabIndex="5" /></td>
                                                                <td style="height: 18px">
                                                                </td>
                                                                <td style="height: 18px">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="textbold" style="height: 14px">
                                                                </td>
                                                                <td class="textbold" style="height: 14px; width: 332px;">
                                                                </td>
                                                                <td style="height: 14px; width: 116px;">
                                                                </td>
                                                                <td style="width: 142px; height: 14px">
                                                                </td>
                                                                <td style="height: 14px">
                                                                </td>
                                                                <td style="height: 14px">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="textbold" style="height: 14px">
                                                                </td>
                                                                <td class="textbold" style="width: 332px; height: 14px">
                                                                </td>
                                                                <td colspan="2" style="height: 14px" class="ErrorMsg">
                                                                    Field Marked * are Mandatory</td>
                                                                <td style="height: 14px">
                                                                </td>
                                                                <td style="height: 14px">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="textbold" style="height: 14px">
                                                                </td>
                                                                <td class="textbold" style="width: 332px; height: 14px">
                                                                </td>
                                                                <td class="ErrorMsg" colspan="2" style="height: 14px">
                                                                </td>
                                                                <td style="height: 14px">
                                                                </td>
                                                                <td style="height: 14px">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="textbold" style="height: 14px">
                                                                </td>
                                                                <td class="textbold" style="width: 332px; height: 14px">
                                                                </td>
                                                                <asp:GridView ID="GridView1" runat="server">
                                                                </asp:GridView>
                                                                <td style="height: 14px" colspan="2">
                                                                    <asp:GridView ID="dbgrdIpPool" runat="server" AutoGenerateColumns="false" Width="100%"
                                                                        HorizontalAlign="Center" TabIndex="9">
                                                                        <Columns>
                                                                            <asp:BoundField DataField="IPAddress" HeaderText="IPs" />
                                                                            <asp:TemplateField HeaderText="Action">
                                                                                <ItemStyle HorizontalAlign="Left" />
                                                                                <ItemTemplate>
                                                                                    <asp:LinkButton CssClass="LinkButtons" ID="btnDelete" CausesValidation="false" Text="Delete"
                                                                                        CommandArgument='<%# Eval("IPAddress") %>'  CommandName="Deletex"
                                                                                        runat="server" OnClientClick="return ConfirmDelete()"></asp:LinkButton>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                        </Columns>
                                                                        <AlternatingRowStyle CssClass="lightblue" />
                                                                        <HeaderStyle CssClass="Gridheading" HorizontalAlign="Left" />
                                                                        <RowStyle CssClass="textbold" />
                                                                    </asp:GridView>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="textbold" style="height: 14px">
                                                                </td>
                                                                <td class="textbold" style="width: 332px; height: 14px">
                                                                </td>
                                                                <td class="ErrorMsg" colspan="2" style="height: 14px">
                                                                </td>
                                                                <td style="height: 14px">
                                                                </td>
                                                                <td style="height: 14px">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="textbold" style="height: 14px">
                                                                </td>
                                                                <td class="textbold" style="width: 332px; height: 14px">
                                                                </td>
                                                                <td class="ErrorMsg" colspan="2" style="height: 14px">
                                                                </td>
                                                                <td style="height: 14px">
                                                                </td>
                                                                <td style="height: 14px">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="textbold" style="height: 14px">
                                                                </td>
                                                                <td class="textbold" style="width: 332px; height: 14px">
                                                                </td>
                                                                <td class="ErrorMsg" colspan="2" style="height: 14px">
                                                                </td>
                                                                <td style="height: 14px">
                                                                </td>
                                                                <td style="height: 14px">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="textbold" style="height: 23px">
                                                                </td>
                                                                <td colspan="3" class="ErrorMsg">
                                                                <td style="height: 23px">
                                                                </td>
                                                                <td style="height: 23px">
                                                                    &nbsp;</td>
                                                            </tr>
                                                            <tr>
                                                                <td class="textbold">
                                                                    &nbsp;</td>
                                                                <td colspan="2">
                                                                </td>
                                                                <td style="width: 142px">
                                                                    &nbsp;</td>
                                                                <td>
                                                                    &nbsp;</td>
                                                                <td>
                                                                    &nbsp;</td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="6">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="6" height="12">
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
                                                    </td>
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
