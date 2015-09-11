<%@ Page Language="VB" AutoEventWireup="false" CodeFile="SASR_GroupCategorySalesObjective.aspx.vb"
    Inherits="Sales_SASR_GroupCategorySalesObjective" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />
    <title>AAMS::Sales::Search Sales Objective</title>
    <link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="../JavaScript/AAMS.js">
    </script>

    <script language="javascript" type="text/javascript">
     
      function Mandatory()
    {  
       if ( document.getElementById("drpAoffice").value=='')
       {       
        document.getElementById("lblError").innerHTML ="Aoffice is mandatory";
        return false;
       }
      
    }  
    
  	
  			
     function Reset()
    {
      document.getElementById("lblError").innerHTML="";
      document.getElementById("drpAoffice").value="";
      document.getElementById("drpAoffice").focus();      
      document.getElementById("drpGroupCategory").value="";
      document.getElementById("drpRegion").value="";
      if (document.getElementById("gvGroupCategVisiit")!=null) 
        document.getElementById("gvGroupCategVisiit").style.display ="none"; 
      return false;
    }
    function NewFunction()
    {  
        window.location.href="SAUP_GroupCategorySalesObjective.aspx?Action=I";      
        return false;
    }  
    
      function EditFunction(GroupCat,Aoffice)
    {    
        window.location.href="SAUP_GroupCategorySalesObjective.aspx?Action=U&GroupCat=" + GroupCat + "&Aoffice=" + Aoffice;  
        return false;
     }
    function DeleteFunction(GroupCat,Aoffice)
    {  
        if (confirm("Are you sure you want to delete?")==true)
        {    
        
             document.getElementById('<%=hdID.ClientId%>').value  =GroupCat
             document.getElementById('<%=hdAOffice.ClientId%>').value  =Aoffice
             return true;
            }
            return false;
	}			

    </script>

</head>
<body>
    <form id="form1" runat="server" defaultbutton="btnSearch" defaultfocus="txtEquipmentType">
        <table width="860px" align="left" class="border_rightred" style="height: 486px;">
            <tr>
                <td valign="top">
                    <table width="100%" align="left">
                        <tr>
                            <td valign="top" align="left">
                                <span class="menu">Sales-&gt;</span><span class="sub_menu">Sales Objective</span></td>
                        </tr>
                        <tr>
                            <td class="heading" align="center" valign="top">
                                Search Sales Objective&nbsp;</td>
                        </tr>
                        <tr>
                            <td valign="top">
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td align="LEFT" class="redborder">
                                            <table width="100%" border="0" align="left" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td class="textbold" colspan="6" align="center" style="height: 25px">
                                                        <asp:Label ID="lblError" runat="server" CssClass="ErrorMsg"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="6%" class="textbold" style="height: 28px">
                                                        &nbsp;</td>
                                                    <td width="15%" class="textbold" style="height: 28px">
                                                        Aoffice<span class="Mandatory" >*</span></td>
                                                    <td style="width: 235px; height: 28px;">
                                                        <asp:DropDownList ID="drpAoffice" runat="server" CssClass="dropdownlist" Style="left: 0px;
                                                            position: relative; top: 0px" TabIndex="1" Width="160px" onkeyup="gotop(this.id)">
                                                        </asp:DropDownList></td>
                                                    <td style="width: 147px; height: 28px;">
                                                        <span class="textbold"> Agency Category</span></td>
                                                    <td width="21%" style="height: 28px"><asp:DropDownList ID="drpGroupCategory" runat="server" CssClass="dropdownlist" Style="left: 0px;
                                                            position: relative; top: 0px" TabIndex="1" Width="160px" onkeyup="gotop(this.id)">
                                                    </asp:DropDownList></td>
                                                    <td width="18%" style="height: 28px">
                                                        <asp:Button ID="btnSearch" CssClass="button" runat="server" Text="Search" TabIndex="2" AccessKey="a" /></td>
                                                </tr>
                                                <tr>
                                                    <td class="textbold" style="height: 28px">
                                                        &nbsp;</td>
                                                    <td class="textbold" style="height: 28px">
                                                        </td>
                                                    <td style="width: 235px; height: 28px;">
                                                       </td>
                                                    <td class="textbold" nowrap="nowrap" style="width: 147px; height: 28px;">
                                                        </td>
                                                    <td style="height: 28px">
                                                        </td>
                                                    <td style="height: 28px">
                                                        <asp:Button ID="btnNew" CssClass="button" runat="server" Text="New" TabIndex="2" AccessKey="n" /></td>
                                                </tr>
                                                <tr>
                                                    <td class="textbold" style="height: 28px">
                                                        &nbsp;</td>
                                                    <td class="textbold" nowrap="nowrap" style="height: 28px"></td>
                                                    <td class="textbold" style="width: 235px; height: 28px;">
                                                       </td>
                                                    <td nowrap="nowrap" class="textbold" style="width: 147px; height: 28px;">
                                                        &nbsp;</td>
                                                    <td style="height: 28px">
                                                        &nbsp;</td>
                                                    <td style="height: 28px">
                                                        <asp:Button ID="btnReset" CssClass="button" runat="server" Text="Reset" TabIndex="2" AccessKey="r" /></td>
                                                </tr>
                                                <tr>
                                                    <td class="textbold" style="height: 22px">
                                                        &nbsp;</td>
                                                    <td colspan="2" style="height: 22px">
                                                    </td>
                                                    <td style="width: 147px; height: 22px;">
                                                        &nbsp;</td>
                                                    <td style="height: 22px">
                                                        &nbsp;</td>
                                                    <td style="height: 22px">
                                                        <asp:Button ID="btnExport" runat="server" CssClass="button" Style="position: relative"
                                                            TabIndex="2" Text="Export" AccessKey="e" />&nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td class="textbold">
                                                    </td>
                                                    <td colspan="2"> &nbsp;
                                                    </td>
                                                    <td style="width: 147px">
                                                    </td>
                                                    <td>
                                                    </td>
                                                    <td>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="6" height="4">&nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="6" height="4" style="text-align: center">
                                                        <asp:GridView ID="gvGroupCategVisiit" runat="server" AutoGenerateColumns="False"
                                                            TabIndex="6" Width="80%" AllowSorting="True">
                                                            <Columns>
                                                                    <asp:BoundField DataField="REGION" HeaderText="Region "  SortExpression ="REGION" Visible ="false" >
                                                                    </asp:BoundField>                                                                     
                                                                    
                                                                     <asp:TemplateField HeaderText="AOFFICE" SortExpression="AOFFICE">                                                                                
                                                                            <ItemTemplate>
                                                                                <%#Eval("AOFFICE")%> 
                                                                                <asp:HiddenField ID="hdAoffice" runat="server" Value='<%#Eval("AOFFICE")%>' />                                                                          
                                                                            </ItemTemplate>                                                                                 
                                                                        </asp:TemplateField>
                                                                        
                                                                        <asp:TemplateField HeaderText="Agency Category" SortExpression="GROUP_CATG_NAME">                                                                                
                                                                            <ItemTemplate>
                                                                                <%#Eval("GROUP_CATG_NAME")%> 
                                                                                <asp:HiddenField ID="hdGroupCategory" runat="server" Value='<%#Eval("GROUP_CATG_ID")%>' />                                                                          
                                                                            </ItemTemplate>                                                                                 
                                                                        </asp:TemplateField>

                                                                     <asp:BoundField DataField="VISITCOUNT" HeaderText="Visit Count"  SortExpression ="VISITCOUNT"  >
                                                                    </asp:BoundField>  
                                                                    
                                                                    <asp:TemplateField HeaderText="Action" ItemStyle-Wrap="false" >
                                                                    <ItemTemplate>
                                                                        <a href="#" class="LinkButtons" id="linkEdit" runat="server">Edit</a>&nbsp;                                                                         
                                                                          <asp:LinkButton ID="linkDelete" runat="server" CommandName ="DeleteX" Text ="Delete" CssClass ="LinkButtons"></asp:LinkButton>&nbsp;
                                                                    </ItemTemplate>
                                                                    <HeaderStyle Width="10%" />
                                                                </asp:TemplateField>
                                                            </Columns>
                                                            <AlternatingRowStyle CssClass="lightblue" />
                                                            <RowStyle CssClass="textbold" />
                                                            <HeaderStyle CssClass="Gridheading" ForeColor="white" />
                                                        </asp:GridView>

                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="6" valign="top">
                                                        <asp:Panel ID="pnlPaging" runat="server" Visible="false" Width="100%">
                                                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                <tr style="padding-top: 4pt; padding-bottom: 4pt">
                                                                    <td style="width: 30%; height: 30px;" class="left">
                                                                        <span class="textbold"><b>&nbsp;No. of records found</b></span>&nbsp;&nbsp;<asp:TextBox
                                                                            ID="txtTotalRecordCount" runat="server" Width="105px" CssClass="textboxgrey" ReadOnly="True"></asp:TextBox></td>
                                                                    <td style="width: 25%; height: 30px; text-align: right;" class="right">
                                                                        <asp:LinkButton ID="lnkPrev" CssClass="LinkButtons" runat="server" CommandName="Prev"><< Prev</asp:LinkButton>&nbsp;</td>
                                                                    <td style="width: 20%; height: 30px;" class="center">
                                                                        <span class="textbold"><b>Page No</b></span>&nbsp;&nbsp;<asp:DropDownList onkeyup="gotop(this.id)"
                                                                            ID="ddlPageNumber" Width="70px" CssClass="dropdownlist" runat="server" AutoPostBack="True">
                                                                        </asp:DropDownList></td>
                                                                    <td style="width: 25%; height: 30px;" class="left">
                                                                        <asp:LinkButton ID="lnkNext" runat="server" CssClass="LinkButtons" CommandName="Next">Next >></asp:LinkButton></td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
                                                        <asp:TextBox ID="txtRecordOnCurrentPage" runat="server" Width="73px" CssClass="textboxgrey"
                                                            Visible="false"></asp:TextBox>
                                                        <asp:HiddenField ID="hdID" runat="server" />
                                                        <asp:HiddenField ID="hdAOffice" runat="server" />                                                        
                                                    </td>
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
