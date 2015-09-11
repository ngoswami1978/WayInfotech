<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ISP_SRISPProvider.aspx.vb" Inherits="ISP_ISP_SRISPProvider" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Search ISP</title>
     <script src="../JavaScript/AAMS.js" type="text/javascript"></script>
     <script language="javascript" type="text/javascript">
      
         function ProviderMandatory()
    {
        if (  document.getElementById("txtISPProvider").value!="")
         {         
            document.getElementById("lblError").innerHTML="Provider name is mandatory.";
            document.getElementById("txtISPProvider").focus();
            return false;          
         }     
       
        
         return true;
     }
      
         function SelectFunction(str3)
        {   
            //alert(str3);
            var pos=str3.split('|');
            if (window.opener.document.forms['form1']['txtProviderName']!=null)
            {                
                  if (window.opener.document.forms['form1']['hdProviderID']!=null)
                  {
                    window.opener.document.forms['form1']['hdProviderID'].value=pos[0];
                  }
                  
                  if (window.opener.document.forms['form1']['txtProviderName']!=null)
                  {
                    window.opener.document.forms['form1']['txtProviderName'].value=pos[1];
                  }
            }   
            window.close();
        } 
        
    function EditFunction(CheckBoxObj)
    {           
          window.location.href="ISP_UPISPProvider.aspx?Action=U&ProviderId="+CheckBoxObj;               
          return false;
    }
        function NewMSUPProvider()
        {
        window.location.href="ISP_UPISPProvider.aspx?Action=I|";
        return false;
        }
        
    function DeleteFunction(CheckBoxObj)
    {   
         if (confirm("Are you sure you want to delete?")==true)
                  {        
                    document.getElementById("hdDeleteProviderID").value= CheckBoxObj ;   
                  }
                else
                {
                 document.getElementById("hdDeleteProviderID").value="";
                 return false;
                }
    }      
   
    </script>
    <link href="../CSS/AAMS.css" type="text/css" rel="stylesheet" />
</head>
<body >
    <form  defaultbutton="btnSearch" defaultfocus="txtISPProvider" id="form1" runat="server">
    <table width="860px" align="left"  class="border_rightred">
            <tr>
                <td valign="top">
                    <table width="100%" align="left">
                        <tr>
                            <td valign="top"  align="left" >
                            <span class="menu">ISP-&gt;</span><span class="sub_menu">ISP Provider Search </span></td>
                        </tr>
                        <tr>
                            <td class="heading" align="center" valign="top">
                                Search <span style="font-family: Microsoft Sans Serif">ISP Provider</span></td>
                        </tr>
                        <tr>
                            <td valign="top">
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td class="redborder center">
                                                <table border="0" cellpadding="2" cellspacing="1" style="width: 100%" class="left">
                                                                                    <tr>
                                                                                        <td colspan="4" class="center gap">
                                                                                           <asp:Label ID="lblError" runat="server" CssClass="ErrorMsg"></asp:Label></td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td style="width: 196px">
                                                                                        </td>
                                                                                        <td class="textbold" style="width: 100px">
                                                                                            ISP Provider</td>
                                                                                        <td style="width: 308px">
                                                                                              <asp:TextBox ID="txtISPProvider" runat="server" CssClass="textbox" Width="208px" MaxLength="100" TabIndex="1"></asp:TextBox>
                                                                                        </td>
                                                                                        <td>
                                                                                          <asp:Button ID="btnSearch" CssClass="button" runat="server" Text="Search" TabIndex="3" AccessKey="A" /></td>
                                                                                     </tr>
                                                                                    <tr>
                                                                                        <td style="width: 176px">
                                                                                        </td>
                                                                                        <td class="textbold">
                                                                                            </td>
                                                                                        <td style="width: 308px">
                                                                                        </td>
                                                                                        <td>
                                                                                       <asp:Button ID="btnNew" CssClass="button" runat="server" Text="New" TabIndex="4" AccessKey="N" /></td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td style="width: 176px; ">
                                                                                            <input id="hdRecordOnCurrentPage" runat="server" style="width: 6px" type="hidden" /></td>
                                                                                        <td class="textbold" >
                                                                                        </td>
                                                                                        <td style="width: 308px; ">
                                                                                         </td>
                                                                                        <td style="height: 26px">
                                                                                         <asp:Button ID="btnExport" runat="server" CssClass="button" TabIndex="5" Text="Export" AccessKey="E" /></td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td style="width: 176px; ">
                                                                                        </td>
                                                                                        <td class="textbold" >
                                                                                        </td>
                                                                                        <td style="width: 308px; ">
                                                                                            </td>
                                                                                        <td >
                                                                                            <asp:Button ID="btnReset" CssClass="button" runat="server" Text="Reset" TabIndex="6" AccessKey="R" /></td>
                                                                                    </tr>
                                                                                                                    
                                                                                    <tr>
                                                                                        
                                                                                    </tr>
                                                                                     <tr>
                                                                                   
                                                                                        <td colspan="3" align ="center" >
                                                                                           <asp:GridView EnableViewState="false" ID="grdvISPProvider" runat="server"  AutoGenerateColumns="False" TabIndex="6" Width="100%" AllowSorting="True"   HeaderStyle-HorizontalAlign="left" RowStyle-HorizontalAlign="left"  >
                                                                                             <Columns>
                                                                                           
                                                                                                        <asp:BoundField DataField="ProviderName" HeaderText="Provider Name"  SortExpression="ProviderName"  >
                                                                                                            <ItemStyle Wrap="True" Width="220px" />
                                                                                                        </asp:BoundField>
                                                                                                    
                                                                                                        <asp:TemplateField HeaderText="Action" HeaderStyle-Width="130px" >
                                                                                                        <ItemTemplate>
                                                                                                            <asp:LinkButton  ID="lnkSelect" Runat="server" CssClass="LinkButtons" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "ProviderID") + "|" + DataBinder.Eval(Container.DataItem, "ProviderName") %>'>Select</asp:LinkButton>&nbsp;
                                                                                                            <a href="#" class="LinkButtons" id="linkEdit" runat="server">Edit</a>&nbsp;
                                                                                                           <asp:LinkButton ID="btnDelete" runat="server" CommandName ="DeleteX" Text ="Delete" CssClass="LinkButtons"></asp:LinkButton> <%--<a href="#" class="LinkButtons" id="linkDelete" runat="server">Delete</a>--%>
                                                                                                            <asp:HiddenField ID="hdProviderID" runat="server" Value='<%#Eval("ProviderID")%>' />   
                                                                                                         </ItemTemplate>
                                                                                                            <ItemStyle Wrap="False" />
                                                                                                       </asp:TemplateField>
                                                                                                                                                    
                                                                                             
                                                                                             </Columns>
                                                                                                <AlternatingRowStyle CssClass="lightblue" />
                                                                                                <RowStyle CssClass="textbold" />
                                                                                                <HeaderStyle CssClass="Gridheading" ForeColor="white" />
                                                                                                
                                                                                             </asp:GridView>
                                                                                                                        </td>
                                                                                      <td></td>
                                                                                     </tr>
                                                                                    <tr>
                                                                                        <td colspan="4">
                                                                                        <asp:Panel ID="pnlPaging" runat="server" Visible="false" Width="100%">
                                                                                                  <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                                                                  <tr class="paddingtop paddingbottom"  >                                                                                                                                                
                                                                                                                      <td style="width: 30%" class="left"><span  class="textbold"><b>&nbsp;No. of records found</b></span>&nbsp;&nbsp;<asp:TextBox ID="txtTotalRecordCount" runat ="server"  Width="105px" CssClass="textboxgrey" ReadOnly="True" ></asp:TextBox></td>
                                                                                                                      <td style="width: 25%" class="right">                                                                             
                                                                                                                          <asp:LinkButton ID="lnkPrev" CssClass="LinkButtons" runat="server" CommandName="Prev"><< Prev</asp:LinkButton></td>
                                                                                                                      <td style="width: 20%" class="center">
                                                                                                                          <span class ="textbold" ><b>Page No</b></span>&nbsp;&nbsp;<asp:DropDownList ID="ddlPageNumber" Width="70px" CssClass="dropdownlist" runat="server" AutoPostBack="True" >
                                                                                                                          </asp:DropDownList></td>
                                                                                                                      <td style="width: 25%" class="left">
                                                                                                                          <asp:LinkButton ID="lnkNext" runat="server" CssClass="LinkButtons" CommandName="Next">Next >></asp:LinkButton></td>
                                                                                                                  </tr>
                                                                                                              </table></asp:Panel>
                                                                                            <asp:HiddenField ID="hdDeleteProviderID" runat="server" />
                                                                                            <asp:TextBox ID="txtRecordOnCurrentPage" runat="server" CssClass="textboxgrey" Visible="false"
                                                                                                Width="73px"></asp:TextBox></td>
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

