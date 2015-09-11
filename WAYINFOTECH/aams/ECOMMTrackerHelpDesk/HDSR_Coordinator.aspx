<%@ Page Language="VB" AutoEventWireup="false" CodeFile="HDSR_Coordinator.aspx.vb" Inherits="ETHelpDesk_MSSR_Coordinator" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
   <link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />
    <title>AAMS::HelpDesk::Search Customer Category</title>
    <script src="../JavaScript/ETracker.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
    function EditFunction(CoordinatorID,CoordinatorID1)
    {           
          window.location.href="HDUP_Coordinator.aspx?Action=U&Aoffice="+CoordinatorID+"&CoType="+CoordinatorID1;               
          return false;
    }
 
    function DeleteFunction(CoordinatorID,CoordinatorID1,CoordinatorID2)
    {   
        if (confirm("Are you sure you want to delete?")==true)
        {  
//           var Aoffice=document.getElementById('<%=ddlAOffice.ClientID %>').value
//           var Coordinator=""
//           if(document.getElementById('<%=rbCoordinator1.ClientID %>').checked==true)
//           Coordinator="1";
//           if(document.getElementById('<%=rbCoordinator2.ClientID %>').checked==true)
//           Coordinator="2";
//          var Name= document.getElementById('<%=txtName.ClientID %>').value      
//                       
//           
//          window.location.href="HDSR_Coordinator.aspx?Action=D|"+ CoordinatorID +"|"+CoordinatorID1+"|"+CoordinatorID2+"|"+Aoffice +"|"+Coordinator+"|"+Name;                                       
        
         document.getElementById('<%=hdID.ClientId%>').value  ="D|"+CoordinatorID +"|"+CoordinatorID1+"|"+CoordinatorID2;
            return true;
        }
         return false;
         
    }
   
    </script>
</head>
<body >
    <form id="form1" runat="server" defaultbutton="btnSearch">
   
     <div>
    <table width="860px"  height="486px" class="border_rightred left">
            <tr>
                <td class="top">
                    <table width="100%" class="left">
                        <tr>
                            <td   >
                            <span class="menu">ETrackers HelpDesk-&gt;</span><span class="sub_menu">Coordinator Search</span></td>
                        </tr>
                        <tr>
                            <td class="heading center" >
                                Search Coordinator</td>
                        </tr>
                        <tr>
                            <td >
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td class="redborder center" >
                                    <table border="0" cellpadding="2" cellspacing="1" style="width: 100%" class="left">
                                                                        <tr>
                                                                            <td colspan="4" class="center gap">
                                                                <asp:Label ID="lblError" runat="server" CssClass="ErrorMsg"></asp:Label></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="width: 10%">             
                                                                                <input id="hdData" runat="server" style="width: 1px" type="hidden" /></td>
                                                                            <td class="textbold"> Coordinator</td>
                                                                            <td style="width: 308px">
                                                                            <asp:RadioButton ID="rbCoordinator1" runat="server" CssClass="dropdownlist" GroupName="r1" Text="Coordinator1" Width="110px" />
                                                                            <asp:RadioButton ID="rbCoordinator2" runat="server" CssClass="dropdownlist" GroupName="r1" Text="Coordinator2" Width="110px" />
                                                                                </td>
                                                                            <td> <asp:Button ID="btnSearch" CssClass="button" runat="server" Text="Search" TabIndex="3" AccessKey="a" /></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>    </td>
                                                                            <td class="textbold" >   Name</td>
                                                                            <td >  
                                                                    <asp:TextBox ID="txtName" runat="server" CssClass="textbold" Width="208px" MaxLength="25" TabIndex="1"></asp:TextBox></td>
                                                                            <td>  <asp:Button ID="btnNew" CssClass="button" runat="server" Text="New" TabIndex="4" AccessKey="n" /></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td >          </td>
                                                                            <td class="textbold" >   AOffice</td>
                                                                            <td > 
                                                                                <asp:DropDownList ID="ddlAOffice" runat="server" CssClass="dropdownlist" Width="214px" onkeyup="gotop(this.id)">
                                                                                </asp:DropDownList></td>
                                                                            <td >   <asp:Button ID="btnReset" CssClass="button" runat="server" Text="Reset" TabIndex="5" AccessKey="r" /></td>
                                                                        </tr>
                                        <tr>
                                            <td>
                                            </td>
                                            <td class="textbold">
                                                <input id="hdRecordOnCurrentPage" runat="server" style="width: 6px" type="hidden" />
                                               <asp:HiddenField ID="hdID" runat="server" />
                                                </td>
                                            <td>
                                            </td>
                                            <td>
                                                <asp:Button ID="btnExport" runat="server" CssClass="button" TabIndex="3" Text="Export" AccessKey="e" /></td>
                                        </tr>
                                                                          <tr>
                                                                        </tr>
                                                                        <tr>
                                                                            <td colspan="4" class="center">
                                                                                <asp:GridView EnableViewState="False" ID="gvCoordinator" runat="server"  AutoGenerateColumns="False" TabIndex="6" Width="80%" AllowSorting="True">
                                                                                <Columns>
                                                                                    <asp:BoundField HeaderText="AOffice" DataField="Aoffice" SortExpression="Aoffice" >
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <ItemStyle HorizontalAlign="Left" />
                                                                                    </asp:BoundField>
                                                                                    <asp:BoundField HeaderText="Name" DataField="EMPLOYEE_NAME" SortExpression="EMPLOYEE_NAME" >
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <ItemStyle HorizontalAlign="Left" />
                                                                                    </asp:BoundField>
                                                                                   <asp:BoundField DataField="COORDINATOR_TYPE" HeaderText="Coordinator Type" SortExpression="COORDINATOR_TYPE" >
                                                                                       <HeaderStyle HorizontalAlign="Left" />
                                                                                       <ItemStyle HorizontalAlign="Left" />
                                                                                   </asp:BoundField>
                                                                                     <asp:TemplateField HeaderText="Action" >
                                                            <ItemTemplate >
                                                            <asp:LinkButton ID="lnkEdit" runat="server" CommandName ="EditX" Text ="Edit" CssClass ="LinkButtons"></asp:LinkButton>&nbsp;
                                                               <asp:LinkButton ID="lnkDelete" runat="server" CommandName ="DeleteX" Text ="Delete" CssClass ="LinkButtons"></asp:LinkButton> 
                                                                 <asp:HiddenField ID="hdID" runat="server" Value='<%#Eval("Aoffice")%>'/>  
                                                                  <asp:HiddenField ID="hdID1" runat="server" Value='<%#Eval("COORDINATOR_TYPE")%>'/> 
                                                                   <asp:HiddenField ID="hdID2" runat="server" Value='<%#Eval("EmployeeID")%>'/>    
                                                             </ItemTemplate>
                                                                                         <HeaderStyle HorizontalAlign="Left" />
                                                                                         <ItemStyle HorizontalAlign="Left" />
                                                           </asp:TemplateField>                                                  
                                                 
                                                 </Columns>
                                                    <AlternatingRowStyle CssClass="lightblue" />
                                                    <RowStyle CssClass="textbold" />
                                                    <HeaderStyle CssClass="Gridheading" ForeColor="white" />                                                    
                                                 </asp:GridView>
                                                                            </td>
                                                                        </tr>
                                        <tr>
                                            <td class="center" colspan="4">
                                             <asp:Panel ID="pnlPaging" runat="server" Visible="false" Width="80%">
                                                      <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                      <tr class="paddingtop paddingbottom"  >                                                                                                                                                
                                                                          <td style="width: 30%" class="left"><span  class="textbold"><b>&nbsp;No. of records found</b></span>&nbsp;&nbsp;<asp:TextBox ID="txtTotalRecordCount" runat ="server"  Width="70px" CssClass="textboxgrey" ReadOnly="True" ></asp:TextBox></td>
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
    </div>
   
    </form>
</body>
</html>
