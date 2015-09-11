<%@ Page Language="VB" AutoEventWireup="false" CodeFile="MSUP_AgencyStaff.aspx.vb" Inherits="TravelAgency_MSUP_AgencyStaff" %>
<%@ Register Src="../UserControl/MenuControl.ascx" TagName="MenuControl" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>AAMS: Agency</title>
    <link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />    
     <script src="../JavaScript/AAMS.js" type="text/javascript"></script>  
       <script type="text/javascript" language ="javascript" >      
     
//      function  MSUPAgencyStaff()
//       {        
//           window.location="MSUP_AgencyStaff.aspx?Action=I";
//           return false;
//       }
        function DeleteFunction(CheckBoxObj)
          {   
            if (confirm("Are you sure you want to delete?")==true)
            {    
                window.location.href="MSUP_AgencyStaff.aspx?Action=D|"+CheckBoxObj ;
                return false;
            }
        }
      function EditFunction(CheckBoxObj)
    {            
              window.location ="MSUP_AgencyStaff.aspx?Action=U&AGENCYSTAFFID=" + CheckBoxObj;        
          return false;
    }  
   
   function AgencyStaffMandatory()
    {
        if (document.getElementById("txtName").value=="")
         {          
            document.getElementById("lblError").innerHTML="Name is mandatory.";
            document.getElementById("txtName").focus();
            return false;
          
         }
        if (document.getElementById("txtName").value!="")
         {
           if(IsDataValid(document.getElementById("txtName").value,7)==false)
            {
            document.getElementById("lblError").innerHTML="Name is not valid.";
            document.getElementById("txtName").focus();
            return false;
            } 
         }
          if (document.getElementById("txtDesig").value!="")
         {
           if(IsDataValid(document.getElementById("txtDesig").value,7)==false)
            {
            document.getElementById("lblError").innerHTML="Designation is not valid.";
            document.getElementById("txtDesig").focus();
            return false;
            } 
         }
           if(document.getElementById('<%=txtDob.ClientId%>').value != '')
        {
        if (isDate(document.getElementById('<%=txtDob.ClientId%>').value,"d/M/yyyy") == false)	
        {
           document.getElementById('<%=lblError.ClientId%>').innerText = "Date of birth is not valid.";			
	       document.getElementById('<%=txtDob.ClientId%>').focus();
	       return(false);  
        }
         } 
             if(document.getElementById('<%=txtDow.ClientId%>').value != '')
        {
        if (isDate(document.getElementById('<%=txtDow.ClientId%>').value,"d/M/yyyy") == false)	
        {
           document.getElementById('<%=lblError.ClientId%>').innerText = "Date of wedding is not valid.";			
	       document.getElementById('<%=txtDow.ClientId%>').focus();
	       return(false);  
        }
         } 
        
       
         if(document.getElementById("txtEmail").value!='')
         {              
        if(checkEmail(document.getElementById("txtEmail").value)==false)
        {
            document.getElementById("lblError").innerHTML='Email is not valid.';
            document.getElementById("txtEmail").focus();
            return false;
        }
    }    
      
         
         return true;
     }
  
    </script> 
   
</head>
<body>
    <form id="form1" runat="server" defaultfocus="txtName">
        <table width="860px" align="left" style="height: 486px" class="border_rightred">
            <tr>
                <td valign="top">
                    <table width="100%">
                        <tr>
                            <td valign="top">
                                <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td valign="top" align="left">
                                            <span class="menu">Setup-></span><span class="sub_menu">Agency</span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td bgcolor="#1A61A9" class="heading" align="center">
                                            Manage User
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td valign="top">
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td valign="top">
                                            <uc1:MenuControl ID="MenuControl1" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" class="redborder">
                                            <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td class="textbold" align="center" valign="TOP" rowspan="0"><asp:Label ID="lblError" class="ErrorMsg" runat="server"></asp:Label></td>
                                                </tr>                                                
                                                <tr>
                                                    <td width="100%" valign="top" >
                                                        <asp:Panel ID="pnlEmployee" runat="server" Width="100%">
                                                            <table width="100%" border="0" align="left" cellpadding="2" cellspacing="0">
                                                            <tr>
                                                            <td colspan="5" style="height:25px"></td>
                                                            </tr>
                                                            <tr>
                                                                    <td class="textbold" style="width: 5%">
                                                                    </td>
                                                                    <td class="textbold" style="width: 17%">
                                                                        </td>
                                                                    <td style="width: 20%">
                                                                        </td>
                                                                    <td class="textbold" style="width: 22%">
                                                                        </td>
                                                                    <td style="width: 20%">
                                                                        </td>
                                                                    <td style="width: 23%"></td>
                                                                </tr>
                                                             <tr>
                                                                    <td class="textbold" >
                                                                    </td>
                                                                    <td class="textbold" >
                                                                        Name <span class="Mandatory">*</span></td>
                                                                    <td colspan="3">
                                                                        <asp:TextBox ID="txtName" CssClass="textbox" runat="server" Width="485px" TabIndex="1"></asp:TextBox></td>
                                                                    <td><asp:Button ID="btnAdd" runat="server" TabIndex="12" CssClass="button" Text="Add" /></td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="textbold" >
                                                                    </td>
                                                                    <td class="textbold">
                                                                        Designation</td>
                                                                    <td colspan="3">
                                                                        <asp:TextBox ID="txtDesig" runat="server" CssClass="textbox" Width="485px" TabIndex="2"></asp:TextBox></td>
                                                                    <td>
                                                                        <asp:Button ID="btnSave" runat="server" TabIndex="13" CssClass="button" Text="Save" /></td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="textbold">
                                                                    </td>
                                                                    <td class="textbold">
                                                                        Phone No</td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtPhone" CssClass="textbox" runat="server" TabIndex="3"></asp:TextBox></td>
                                                                    <td class="textbold">
                                                                        Fax</td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtFax" CssClass="textbox" runat="server" TabIndex="4"></asp:TextBox></td>
                                                                    <td>
                                                                        <asp:Button ID="btnReset" runat="server" TabIndex="14" CssClass="button" Text="Reset" /></td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="textbold">
                                                                    </td>
                                                                    <td class="textbold">
                                                                        Date of Birth</td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtDob" runat="server" CssClass="textbox" TabIndex="5"></asp:TextBox></td>
                                                                    <td class="textbold">
                                                                        Date of Wedding</td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtDow" runat="server" CssClass="textbox" TabIndex="6"></asp:TextBox></td>
                                                                    <td>
                                                                        </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="textbold">
                                                                    </td>
                                                                    <td class="textbold">
                                                                        Email</td>
                                                                    <td colspan="3">
                                                                        <asp:TextBox ID="txtEmail" CssClass="textbox" runat="server" Width="485px" TabIndex="7"></asp:TextBox></td>
                                                                    <td>
                                                                        </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="textbold">
                                                                    </td>
                                                                    <td class="textbold">
                                                                        Responsible</td>
                                                                    <td>
                                                                        <asp:CheckBox ID="chkRes" runat="server" TabIndex="8" /></td>
                                                                    <td class="textbold">
                                                                        Correspondence</td>
                                                                    <td>
                                                                        <asp:CheckBox ID="chkCor" runat="server" TabIndex="9" /></td>
                                                                    <td>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="textbold">
                                                                    </td>
                                                                    <td class="textbold">
                                                                        Notes</td>
                                                                    <td colspan="3">
                                                                        <asp:TextBox ID="txtNotes" runat="server" CssClass="textbox" Height="72px" TextMode="MultiLine"
                                                                            Width="485px" TabIndex="10"></asp:TextBox></td>
                                                                    <td>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="textbold">
                                                                    </td>
                                                                    <td class="textbold">
                                                                        Contact Person</td>
                                                                    <td>
                                                                        <asp:DropDownList ID="drplstConPer" runat="server" CssClass="dropdown" TabIndex="11">
                                                                        </asp:DropDownList></td>
                                                                    <td class="textbold">
                                                                    </td>
                                                                    <td>
                                                                    </td>
                                                                    <td>
                                                                    </td>
                                                                </tr>
                                                                 <tr>
                                                                    <td class="textbold">
                                                                        &nbsp;</td>
                                                                    <td class="textbold">
                                                                        </td>
                                                                    <td>
                                                                        </td>
                                                                    <td class="textbold">
                                                                        </td>
                                                                    <td>
                                                                        </td>
                                                                    <td>
                                                                        &nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="textbold">
                                                                        &nbsp;</td>
                                                                    <td colspan="4" class="ErrorMsg">
                                                                        Field Marked * are Mandatory</td>
                                                                    <td>
                                                                        &nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="textbold">
                                                                    </td>
                                                                     <td colspan="4">
                                                                        &nbsp;&nbsp;
                                                                            <asp:GridView ID="gvAgencyStaff" runat="server"  AutoGenerateColumns="False" TabIndex="15" Width="100%" EnableViewState="False"    >
                                                                                 <Columns>
                                                                                <asp:TemplateField HeaderText="Name">
                                                                                    <itemtemplate>
                                                                                        <%#Eval("STAFFNAME")%>
                                                                                        <asp:HiddenField ID="hdAGENCYSTAFFID" runat="server" Value='<%#Eval("AGENCYSTAFFID")%>' />
                                                                                    </itemtemplate>
                                                                                </asp:TemplateField>     
                                                                                    <asp:BoundField DataField="DESIGNATION" HeaderText="Designation"   />                                                                                                                                                                     
                                                                                     <asp:TemplateField>
                                                                                            <HeaderTemplate>
                                                                                                <asp:Label ID="lblAction" runat="server" Text="Action"></asp:Label>
                                                                                            </HeaderTemplate>
                                                                                            <ItemTemplate>
                                                                                                   <a href="#" class="LinkButtons" id="linkEdit" runat="server">Edit</a> <a href="#" class="LinkButtons" id="linkDelete" runat="server">
                                                                                                                Delete</a>
                                                                                            </ItemTemplate>
                                                                                            <ItemStyle Width="20%" CssClass="ItemColor" />
                                                                                            <HeaderStyle Wrap="False" CssClass="Gridheading" />
                                                                                      </asp:TemplateField>
                                                                                 </Columns>
                                                                                <AlternatingRowStyle CssClass="lightblue" />
                                                                                <RowStyle CssClass="textbold" />
                                                                                <HeaderStyle CssClass="Gridheading" />                                                                                
                                                                                </asp:GridView>                                                                    
                                                                    </td>
                                                                    <td>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="textbold">
                                                                        &nbsp;</td>
                                                                    <td class="textbold">
                                                                        </td>
                                                                    <td>
                                                                        </td>
                                                                    <td class="textbold">
                                                                        </td>
                                                                    <td>
                                                                        </td>
                                                                    <%--<td>
                                                                        &nbsp;</td>--%>
                                                                </tr>
                                                                <tr>
                                                                    <td class="textbold">
                                                                        &nbsp;</td>
                                                                    <td colspan="4" class="ErrorMsg">
                                                                        <%--Field Marked * are Mandoatry--%></td>
                                                                    <td>
                                                                        &nbsp;</td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
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
