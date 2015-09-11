<%@ Page Language="VB" AutoEventWireup="false" CodeFile="MSSR_IPPool.aspx.vb" Inherits="Setup_MSSR_IPPool" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>AAMS: IP Pool</title>
    <link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />
    <script src="../JavaScript/AAMS.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
    function  NewMSUPIPPool()
   {    
       window.location="MSUP_IPPool.aspx?Action=I";
       return false;
   }
      function DeleteFunction(CheckBoxObj)
          {   
//            if (confirm("Are you sure you want to delete?")==true)
//            {          
//             
//                 window.location.href="MSSR_IPPool.aspx?Action=D|"+ CheckBoxObj +"|"+  document.getElementById("<%=txtPoolName.ClientID%>").value+"|"+ document.getElementById("drpLstAoffice").selectedIndex +"|"+ document.getElementById("<%=txtDepartment.ClientID%>").value;                         
//                return false;
//            }
                if (confirm("Are you sure you want to delete?")==true)
                  {        
                    document.getElementById("hdDeleteIpPool").value= CheckBoxObj ;   
                  }
                else
                {
                 document.getElementById("hdDeleteIpPool").value="";
                 return false;
                }


        }
   
      function EditFunction(CheckBoxObj)
    {           
         window.location ="MSUP_IPPool.aspx?Action=U&PoolId=" + CheckBoxObj;       
          return false;
    }   
    function IPPoolReset()
    {
        document.getElementById("txtPoolName").value="";
        document.getElementById("txtDepartment").value="";      
        document.getElementById("drpLstAoffice").selectedIndex=0;
        document.getElementById("lblError").innerHTML="";
       if (document.getElementById("dbgrdIpPOOL")!=null) 
         document.getElementById("dbgrdIpPOOL").style.display ="none";  
         document.getElementById("txtPoolName").focus(); 
        return false;
    }
       function CheckMandatoty()
    {
          if (document.getElementById("txtPoolName").value!="")
             {
               if(IsDataValid(document.getElementById("txtPoolName").value,7)==false)
                {
                document.getElementById("lblError").innerHTML="Pool name is not valid.";
                document.getElementById("txtPoolName").focus();
                return false;
                } 
             } 
               if (document.getElementById("txtDepartment").value!="")
             {
               if(IsDataValid(document.getElementById("txtDepartment").value,2)==false)
                {
                document.getElementById("lblError").innerHTML="Department is not valid.";
                document.getElementById("txtDepartment").focus();
                return false;
                } 
             }
          
             return true; 
      }
    </script>
</head>
<body >
    <form id="frmIPpoolSearch" runat="server" defaultfocus ="txtPoolName">  
     <table width="860px" align="left" height="486px" class="border_rightred">
            <tr>
                <td valign="top">
                <table width="100%" align="left"  >
                          <tr>
                            <td valign="top"  align="left" style="height: 20px">
                            <span class="menu">Setup-></span><span class="sub_menu">IP Pool</span></td>
                        </tr>                       
                                   
                        <tr>
                            <td class="heading" align="center" valign="top" >
                                Search IP Pool</td>
                        </tr>
                        <tr>
                            <td  >
                                <table border="0" cellpadding="1" cellspacing="0"  width="100%">                                   
                                    <tr>
                                        <td width="100%" class="redborder">
                                            <table align="left" border="0" cellpadding="2" cellspacing="0" width="100%">                                              
                                                <tr>                                                    
                                                    <td class="textbold" style="width:100%" colspan="4" valign="top" >
                                                          <table width="100%" border="0" align="left" cellpadding="0" cellspacing="2" >
                                                            <tr>
                                                                <td  class="textbold" colspan="6" align="center" height="25px" valign="TOP"><asp:Label ID="lblError" runat="server" CssClass="ErrorMsg" Text=""></asp:Label></td>
                                                            </tr>
                                                            <tr>
                                                                <td class="textbold" style="height:22px;width:126px;">
                                                                    &nbsp;</td>
                                                                <td class="textbold" style="height:22px;width:126px;">
                                                                    </td>
                                                                <td  style="height:22px" >
                                                                    IP Pool Name</td>
                                                                <td width="12%" style="height:22px" >
                                                                    <span class="textbold">
                                                                    <asp:TextBox ID="txtPoolName" runat="server" CssClass="textfield" TabIndex="1" MaxLength="50" EnableViewState="False"></asp:TextBox></span></td>
                                                                <td width="21%" style="height:22px">
                                                                </td>
                                                                <td  style="height:22px">
                                                                    <asp:Button ID="btnSearch" runat="server" CssClass="button" Text="Search" TabIndex="4" OnClick="btnSearch_Click"  AccessKey="A"/></td>
                                                            </tr>
                                                            <tr>
                                                                <td class="textbold" style="width:126px">
                                                                    &nbsp;</td>
                                                                <td class="textbold" style="width:126px">
                                                                    </td>
                                                                <td>
                                                                    Aoffice</td>
                                                                <td class="textbold">
                                                                    <asp:DropDownList  onkeyup="gotop(this.id)"  ID="drpLstAoffice" runat="server" CssClass="dropdown" TabIndex="2">
                                                                    </asp:DropDownList></td>
                                                                <td>
                                                                </td>
                                                                <td  style="height: 22px">
                                                                    <asp:Button ID="btnNew" runat="server" CssClass="button" Text="New" TabIndex="5"  AccessKey="N"/></td>
                                                            </tr>
                                                            <tr>
                                                                <td class="textbold" style="height:22px;width:126px;">
                                                                    &nbsp;</td>
                                                                <td class="textbold" style="height:22px;width:126px;">
                                                                </td>
                                                                <td style="height:22px">
                                                                   Department</td>
                                                                <td style="height:22px">
                                                                    <asp:TextBox ID="txtDepartment" runat="server" CssClass="textfield" TabIndex="3" MaxLength="50" EnableViewState="False"></asp:TextBox></td>
                                                                <td style="height:22px">
                                                                    &nbsp;</td>
                                                                <td style="height:22px">
                                                                    <asp:Button ID="btnExport" runat="server" CssClass="button" Text="Export" TabIndex="6"  AccessKey="E"/></td>
                                                            </tr>
                                                            <tr>
                                                                <td class="textbold" style="height:22px;width:126px;">
                                                                    &nbsp;</td>
                                                               <td colspan="2" class="ErrorMsg">
                                                                  </td>
                                                                <td style="height:22px">
                                                                    &nbsp;</td>
                                                                <td style="height:22px">
                                                                    &nbsp;</td>
                                                                <td style="height:22px"><asp:Button ID="btnReset" runat="server" CssClass="button" Text="Reset" TabIndex="7" AccessKey="R" /></td>
                                                            </tr>
                                                            <tr>
                                                                <td class="textbold" style="width:126px;">
                                                                    &nbsp;</td>
                                                                <td colspan="2" style="height:14px" >
                                                                    </td>
                                                                <td style="height:14px">
                                                                    &nbsp;</td>
                                                                <td style="height:14px">
                                                                    &nbsp;</td>
                                                                <td style="height:14px">
                                                                    &nbsp;</td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="6" style="height: 4px"></td>
                                                            </tr>
                                                              <tr>                                                    
                                                                <td colspan="6" valign="top"  > 
                                                                                            <asp:GridView  ID="dbgrdIpPOOL" runat="server"   AutoGenerateColumns="False" width="100%" RowStyle-HorizontalAlign ="Center"   HorizontalAlign="Center"  HeaderStyle-HorizontalAlign ="left" TabIndex="7" EnableViewState="False"  AllowSorting ="true" HeaderStyle-ForeColor="white"  >
                                                                                                <Columns> 
                                                                                                <asp:TemplateField HeaderText="IP Pool Name"   ItemStyle-Width="15%" ItemStyle-Wrap="false"  SortExpression ="PoolName" >
                                                                                                    <itemtemplate>
                                                                                                        <%#Eval("PoolName")%>
                                                                                                        <asp:HiddenField ID="hdPoolId" runat="server" Value='<%#Eval("PoolID")%>' />
                                                                                                    </itemtemplate>
                                                                                                </asp:TemplateField>                                                                                                                                                                                                                                                                                                                                                                                                                           
                                                                                              <%--  <asp:BoundField DataField="PoolName" HeaderText="IP Pool Name" >
                                                                                                    <ItemStyle Width="15%" Wrap="False" />
                                                                                                </asp:BoundField>--%>
                                                                                                    <asp:BoundField DataField="Department_Name" HeaderText="Department" SortExpression ="Department_Name" >
                                                                                                        <ItemStyle Width="15%" />
                                                                                                        <HeaderStyle Wrap="False" />
                                                                                                    </asp:BoundField>
                                                                                                    <asp:BoundField DataField="Aoffice" HeaderText="Aoffice" SortExpression ="Aoffice"  >
                                                                                                        <ItemStyle Width="12%" Wrap="False" />
                                                                                                        <HeaderStyle Wrap="False" />
                                                                                                    </asp:BoundField>                                                                                                                                               
                                                                                                    <asp:BoundField DataField="IPAddress" HeaderText="IPs"  SortExpression ="IPAddress" >
                                                                                                        <ItemStyle Width="40%" Wrap="True" />
                                                                                                    </asp:BoundField>                                                                                   
                                                                                                    <asp:TemplateField HeaderText="Action" >
                                                                                                                        <ItemTemplate>
                                                                                                                            <a href="#" class="LinkButtons" id="linkEdit" runat="server">Edit</a>&nbsp;&nbsp;<asp:LinkButton ID="btnDelete" runat="server" CommandName ="DeleteX" Text ="Delete" CssClass="LinkButtons"></asp:LinkButton> <%--<a href="#" class="LinkButtons" id="linkDelete" runat="server">Delete</a>--%>
                                                                                                                        </ItemTemplate>
                                                                                                        <ItemStyle Width="12%" Wrap="False" />
                                                                                                                    </asp:TemplateField>
                                                                                                                    
                                                                                                </Columns>
                                                                                                
                                                                                                <AlternatingRowStyle  CssClass="lightblue"/>                                                                                                
                                                                                                <HeaderStyle CssClass="Gridheading" HorizontalAlign="Left"/>
                                                                                                <RowStyle CssClass="textbold" HorizontalAlign="Left" />                                                                                                
                                                                                            </asp:GridView>                                                                                                                                       
                                                                </td>
                                                            </tr>                                                  
                                                            <tr>
                                                                <td colspan="6" style="height: 12px">
                                                                </td>
                                                            </tr>
                                                                                 <tr>                                                   
                                                    <td colspan="6" valign ="top"  >
                                                    <asp:Panel ID="pnlPaging" runat="server" Visible="false" Width="100%">
                                                      <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                      <tr style="padding-top : 4pt; padding-bottom : 4pt">                                                                                                                                                
                                                                          <td style="width: 30%" class="left"><span  class="textbold"><b>&nbsp;No. of records found</b></span>&nbsp;&nbsp;<asp:TextBox ID="txtRecordCount" runat="server" CssClass="textboxgrey" MaxLength="3" TabIndex="3" Width="100px" ReadOnly="True" Text ="0" Visible="True"></asp:TextBox></td>
                                                                          <td style="width: 25%" class="right">                                                                             
                                                                              <asp:LinkButton ID="lnkPrev" CssClass="LinkButtons" runat="server" CommandName="Prev"><< Prev</asp:LinkButton></td>
                                                                          <td style="width: 20%" class="center">
                                                                              <span class ="textbold" ><b>Page No</b></span>&nbsp;&nbsp;<asp:DropDownList  onkeyup="gotop(this.id)"  ID="ddlPageNumber" Width="70px" CssClass="dropdownlist" runat="server" AutoPostBack="True" >
                                                                              </asp:DropDownList></td>
                                                                          <td style="width: 25%" class="left">
                                                                              <asp:LinkButton ID="lnkNext" runat="server" CssClass="LinkButtons" CommandName="Next">Next >></asp:LinkButton></td>
                                                                      </tr>
                                                                  </table></asp:Panel>
                                                    </td>
                                                </tr>
                                                 <tr>
                                                    <td colspan="6" >
                                                        <asp:HiddenField ID="hdDeleteIpPool" runat="server" />
                                                        <asp:TextBox ID="txtRecordOnCurrentPage" runat="server" CssClass="textboxgrey" Visible="false"
                                                            Width="73px"></asp:TextBox>
                                                    </td> 
                                                </tr> 
                                                        </table>
                                                       <br />
                                                     </td>
                                                    <td width="18%" rowspan="1" valign="top" >
                                                     </td>
                                                </tr>
                                                <tr>                                                    
                                                    <td class="textbold" colspan="5"></td>
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
             
     </table>
    </form>
</body>
</html>
