<%@ Page Language="VB" AutoEventWireup="false" CodeFile="TASR_AgencyStatus.aspx.vb" Inherits="TravelAgency_TASR_AgencyStatus" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Agency Status</title>
     <script src="../JavaScript/AAMS.js" type="text/javascript"></script>
    <script type="text/javascript" language ="javascript" >      
     
      function  NewMSUPAgencyStatus()
       {        
           window.location="TAUP_AgencyStatus.aspx?Action=I";
           return false;
       }
        function DeleteFunction(CheckBoxObj)
          {   
            if (confirm("Are you sure you want to delete?")==true)
            {       
              
                window.location.href="TASR_AgencyStatus.aspx?Action=D|"+CheckBoxObj + "|"+ document.getElementById("<%=txtStatus.ClientID%>").value;
                return false;
            }
        }
      function EditFunction(CheckBoxObj)
    {
            
              window.location ="TAUP_AgencyStatus.aspx?Action=U&AgencyStatusID=" + CheckBoxObj;  
      
          return false;
    }   
     
    function AgencyStatusReset()
    {
        document.getElementById("txtStatus").value="";         
        document.getElementById("lblError").innerHTML="";    
        if ( document.getElementById("gvAgencyStatus")!=null)       
        document.getElementById("gvAgencyStatus").style.display ="none"; 
        document.getElementById("txtStatus").focus();  
        return false;
    }
   function AgencyStatusMandatory()
    {
        if (document.getElementById("txtStatus").value!="")
         {
           if(IsDataValid(document.getElementById("txtStatus").value,7)==false)
            {
            document.getElementById("lblError").innerHTML="Agency Status is not valid.";
            document.getElementById("txtStatus").focus();
            return false;
            } 
         }
         return true;
     }
  
    </script>
    <link href="../CSS/AAMS.css" type="text/css" rel="stylesheet" />
</head>
<body bgcolor="ffffff" style="font-size: 12pt; font-family: Times New Roman" >
    <form id="frmSerAgencyStatus" runat="server"  defaultfocus ="txtStatus"  defaultbutton="btnSearch">
    
     <table width="860px" align="left" height="486px" class="border_rightred">
            <tr>
                <td valign="top">
                    <table width="100%" align="left">
                        <tr>
                            <td valign="top"  align="left" >
                            <span class="menu">TravelAgency-&gt;</span><span class="sub_menu">Agency Status</span></td>
                        </tr>
                        <tr>
                            <td class="heading" align="center" valign="top">
                                Search Agency Status</td>
                        </tr>
                        <tr>
                            <td valign="top">
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td align="LEFT" class="redborder">
                                 
                                                        <table width="100%" border="0" align="left" cellpadding="0" cellspacing="0" >
                                                            <tr>
                                                                <td  class="textbold" colspan="6" align="center" height="25px" valign="TOP">
                                                                <asp:Label ID="lblError" runat="server" CssClass="ErrorMsg"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td width="6%" class="textbold" style="height: 22px">
                                                                    &nbsp;</td>
                                                                <td class="textbold" style="width: 57px; height: 22px;">
                                                                    </td>
                                                                <td class="textbold" style="width: 100px; height: 22px;">
                                                                    &nbsp;Agency Status 
                                                                </td>
                                                                <td style="width: 192px; height: 22px" >
                                                                   <asp:TextBox ID="txtStatus" runat ="server" CssClass ="textbox" Width="208px" MaxLength="20" EnableViewState="False" TabIndex="1"></asp:TextBox>
                                                                    </td>
                                                                <td width="21%" style="height: 22px">
                                                                    </td>
                                                                <td width="30%" style="height: 22px">
                                                                    <asp:Button ID="btnSearch" CssClass="button" runat="server" Text="Search" TabIndex="2" AccessKey="A" /></td>
                                                            </tr>
                                                             <tr>
                                                                <td  class="textbold" colspan="6" align="center" valign="TOP">                                                                
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="textbold" style="height: 22px">
                                                                    &nbsp;</td>
                                                                <td class="textbold" style="width: 57px; height: 22px;">
                                                                    </td>
                                                                <td class="textbold" style="width: 90px; height: 22px;">
                                                                    </td>
                                                                <td class="textbold" style="width: 192px; height: 22px;">
                                                                    </td>
                                                                <td style="height: 22px">
                                                                </td>
                                                                <td style="height: 22px">
                                                                    <asp:Button ID="btnNew" CssClass="button" runat="server" Text="New" TabIndex="3" AccessKey="N" /></td>
                                                            </tr>
                                                             <tr>
                                                                <td  class="textbold" colspan="6" align="center" valign="TOP">                                                                
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="textbold">
                                                                    &nbsp;</td>
                                                                <td class="textbold" style="width: 57px">
                                                                </td>
                                                                <td style="width: 90px">
                                                                    &nbsp;<input id="hdRecordOnCurrentPage" runat="server" style="width: 6px" type="hidden" /></td>
                                                                <td style="width: 192px">
                                                                    &nbsp;</td>
                                                                <td>
                                                                    &nbsp;</td>
                                                                <td>
                                                                    <asp:Button ID="btnExport" runat="server" CssClass="button" TabIndex="4" Text="Export" AccessKey="E" /></td>
                                                            </tr>                                                           
                                                            <tr>
                                                                <td class="textbold">
                                                                </td>
                                                                <td class="textbold" style="width: 57px">
                                                                </td>
                                                                <td style="width: 90px">
                                                                </td>
                                                                <td style="width: 192px">
                                                                </td>
                                                                <td>
                                                                </td>
                                                                <td>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="textbold" style="height: 22px">
                                                                </td>
                                                                <td class="textbold" style="width: 57px; height: 22px;">
                                                                </td>
                                                                <td style="width: 90px; height: 22px;">
                                                                </td>
                                                                <td style="width: 192px; height: 22px;">
                                                                </td>
                                                                <td style="height: 22px">
                                                                </td>
                                                                <td style="height: 22px">
                                                                    <asp:Button ID="btnReset" CssClass="button" runat="server" Text="Reset" TabIndex="5" AccessKey="R" /></td>
                                                            </tr>
                                                            <tr>
                                                                <td class="textbold">
                                                                    &nbsp;</td>
                                                                <td colspan="2" >
                                                                    </td>
                                                                <td style="width: 192px">
                                                                    &nbsp;</td>
                                                                <td>
                                                                    &nbsp;</td>
                                                                <td>
                                                                    &nbsp;</td>
                                                            </tr>
                                                            
                                                            <tr>
                                                                <td colspan="6" height="4">
                                                                    <asp:GridView ID="gvAgencyStatus" runat="server"  AutoGenerateColumns="False" TabIndex="6" Width="100%" EnableViewState="False" AllowSorting="True"    >
                                                                  <Columns>
                                                                        <asp:TemplateField HeaderText="Agency Status" SortExpression="Agency_Status_Name">
                                                                            <itemtemplate>
                                                                                <%#Eval("Agency_Status_Name")%>
                                                                                <asp:HiddenField ID="hdAgencyStatusID" runat="server" Value='<%#Eval("AgencyStatusID")%>' />
                                                                            </itemtemplate>
                                                                            <itemstyle horizontalalign="Left" />
                                                                            <headerstyle horizontalalign="Left" />
                                                                          </asp:TemplateField>     
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
                                                    <HeaderStyle CssClass="Gridheading" ForeColor="White" />
                                                    
                                                 </asp:GridView>
                                                                    
                                                                    </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="6" height="4">
                                                                  <asp:Panel ID="pnlPaging" runat="server" Visible="false" Width="100%">
                                                      <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                      <tr class="paddingtop paddingbottom"  >                                                                                                                                                
                                                                          <td style="width: 30%" class="left"><span  class="textbold"><b>&nbsp;No. of records found</b></span>&nbsp;&nbsp;<asp:TextBox ID="txtTotalRecordCount" runat ="server"  Width="105px" CssClass="textboxgrey" ReadOnly ="true"  ></asp:TextBox></td>
                                                                          <td style="width: 25%" class="right">                                                                             
                                                                              <asp:LinkButton ID="lnkPrev" CssClass="LinkButtons" runat="server" CommandName="Prev"><< Prev</asp:LinkButton></td>
                                                                          <td style="width: 20%" class="center">
                                                                              <span class ="textbold" ><b>Page No</b></span>&nbsp;&nbsp;<asp:DropDownList ID="ddlPageNumber" Width="70px" CssClass="dropdownlist" runat="server" AutoPostBack="True" >
                                                                              </asp:DropDownList></td>
                                                                          <td style="width: 25%" class="left">
                                                                              <asp:LinkButton ID="lnkNext" runat="server" CssClass="LinkButtons" CommandName="Next">Next >></asp:LinkButton></td>
                                                                      </tr>
                                                                  </table></asp:Panel>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="6" >
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
    <!-- Code by Rakesh -->
    
  
    </form>
</body>
</html>
