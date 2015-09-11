<%@ Page Language="VB" AutoEventWireup="false" CodeFile="MSSR_CorporateCode.aspx.vb" Inherits="Order_MSSR_CorporateCode" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Corporate Code Search</title>
     <script src="../JavaScript/AAMS.js" type="text/javascript"></script>
    <script type="text/javascript" language ="javascript" >
    function CorporateCodeReset()
    {        
        document.getElementById("lblError").innerHTML=""; 
        document.getElementById("txtCorporateCode").value=""; 
         document.getElementById("txtCorporateQualifier").value=""; 
         document.getElementById("txtDescription").value=""; 
        if (document.getElementById("gvCorporateCode")!=null) 
        document.getElementById("gvCorporateCode").style.display ="none"; 
        document.getElementById("txtCorporateCode").focus(); 
        
                
        return false;
    }
//     function CorporateCodeMandatory()
//    {
//       if (  document.getElementById("txtCorporateQualifier").value!="")
//         {
//           if(IsDataValid(document.getElementById("txtCorporateQualifier").value,3)==false)
//            {
//            document.getElementById("lblError").innerHTML="Corporate qualifier is not valid.";
//            document.getElementById("txtCorporateQualifier").focus();
//            return false;
//            } 
//         } 
//          if (  document.getElementById("txtCorporateCode").value!="")
//         {
//           if(IsDataValid(document.getElementById("txtCorporateCode").value,6)==false)
//            {
//            document.getElementById("lblError").innerHTML="Corporate code is not valid.";
//            document.getElementById("txtCorporateCode").focus();
//            return false;
//            } 
//         } 
//          return true;
//     }
//     
     
        function SelectFunction(str3)
    {   
        
        var pos=str3.split('|'); 
        if  (window.opener.document.forms['form1']['txtCorporateCode']!=null)
        {
	    window.opener.document.forms['form1']['txtCorporateCode'].value=pos[0];
	    } 
        if  (window.opener.document.forms['form1']['ddlCorporateCode']!=null)
        {
         window.opener.document.forms['form1']['ddlCorporateCode'].value=pos[0];
	    } 
	    if  (window.opener.document.forms['form1']['hdCCodeId']!=null)
        {
	    window.opener.document.forms['form1']['hdCCodeId'].value=pos[0];
	    }
	   
	     if  (window.opener.document.forms['form1']['txtCorporateQualifier']!=null)
        {
	    window.opener.document.forms['form1']['txtCorporateQualifier'].value=pos[1];
	    }
	    	window.close();
	}
     function EditFunction(CorporateRowID)
    {   
                  
        window.location.href="MSUP_CorporateCode.aspx?Action=U&CorporateRowID="+CorporateRowID;       
        return false;
           }
    function DeleteFunction(CorporateRowID)
    {   
    
//        if (confirm("Are you sure you want to delete?")==true)
//        {          
//            window.location.href="MSSR_CorporateCode.aspx?Action=D&CorporateRowID="+CorporateRowID;                   
//            return false;
//        }
         if (confirm("Are you sure you want to delete?")==true)
            {  
               document.getElementById('hdCorporateCodeID').value = CorporateRowID;
               document.forms['frmCorCode'].submit();
               //return true;        
            }
            else
            {
                return false;
            }
    }
      function DeleteFunction2(CorporateRowID)
    {   
     if (confirm("Are you sure you want to delete?")==true)
            { 
              
               document.getElementById('<%=hdCorporateCodeID.ClientId%>').value = CorporateRowID;
               document.forms['frmCorCode'].submit();
              // return true;        
            }
            return false;
//        if (confirm("Are you sure you want to delete?")==true)
//        {          
//            window.location.href="MSSR_CorporateCode.aspx?PopUp=T&Action=D&CorporateRowID="+CorporateRowID;
//            return false;
//        }
    }
    function NewFunction()
    {   
    
        window.location.href="MSUP_CorporateCode.aspx?Action=I";       
        return false;
    }
    </script>
    <link href="../CSS/AAMS.css" type="text/css" rel="stylesheet" />
</head>
<body bgcolor="ffffff"   >
    <form id="frmCorCode" runat="server"  defaultfocus="txtCorporateCode">
    
     <table width="860px" align="left" height="486px" class="border_rightred">
            <tr>
                <td valign="top">
                    <table width="100%" align="left">
                        <tr>
                            <td valign="top"  align="left" >
                            <span class="menu">Travel Agency-></span><span class="sub_menu">Corporate Code</span>
                            </td>
                        </tr>
                        <tr>
                            <td class="heading" align="center" valign="top">
                               Corporate Code
                            </td>
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
                                                                <td class="textbold" style="width:15%;">
                                                                    Corporate Code</td>
                                                                <td style="width: 192px; height: 22px" >
                                                                   <asp:TextBox ID="txtCorporateCode" runat ="server" CssClass ="textbox" Width="208px" MaxLength="2" TabIndex="2"></asp:TextBox>
                                                                    </td>
                                                                <td width="15%" style="height: 22px">
                                                                    </td>
                                                                <td width="30%" style="height: 22px">
                                                                    <asp:Button ID="btnSearch" CssClass="button" runat="server" Text="Search" TabIndex="3" AccessKey="A" /></td>
                                                            </tr>
                                                             <tr>
                                                                <td  class="textbold" colspan="6" align="center" valign="TOP">                                                                
                                                                </td>
                                                            </tr>
                                                             <tr>
                                                                <td width="6%" class="textbold" style="height: 22px">
                                                                    &nbsp;</td>
                                                                <td class="textbold" style="width: 57px; height: 22px;">
                                                                    </td>
                                                                <td class="textbold" style="width:130px; height: 22px;">
                                                                    Corporate Qualifier</td>
                                                                <td style="width: 192px; height: 22px" >
                                                                   <asp:TextBox ID="txtCorporateQualifier" runat ="server" CssClass ="textbox"  Width="208px" MaxLength="1" TabIndex="2"></asp:TextBox>
                                                                    </td>
                                                                <td width="15%" style="height: 22px">
                                                                    </td>
                                                                <td width="30%" style="height: 22px">
                                                                    <asp:Button ID="btnNew" CssClass="button" runat="server" Text="New" TabIndex="4" AccessKey="N" /></td>
                                                            </tr>
                                                               <tr>
                                                                <td  class="textbold" colspan="6" align="center" valign="TOP">                                                                
                                                                </td>
                                                            </tr>
                                                             <tr>
                                                                <td width="6%" class="textbold" style="height: 22px">
                                                                    &nbsp;</td>
                                                                <td class="textbold" style="width: 57px; height: 22px;">
                                                                    </td>
                                                                <td class="textbold" style="width: 130px; height: 22px;">
                                                                    Description</td>
                                                                <td style="width: 192px; height: 22px" >
                                                                   <asp:TextBox ID="txtDescription" runat ="server" CssClass ="textbox" Width="208px" MaxLength="30" TabIndex="2"></asp:TextBox>
                                                                    </td>
                                                                <td width="21%" style="height: 22px">
                                                                    </td>
                                                                <td width="30%" style="height: 22px">
                                                                    <asp:Button ID="btnExport" runat="server" CssClass="button" TabIndex="3" Text="Export" AccessKey="E" /></td>
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
                                                                <td class="textbold" style="width: 130px; height: 22px;">
                                                              
                                                                    </td>
                                                                <td class="textbold" style="width: 192px; height: 22px;">
                                                                
                                                                </td>
                                                                <td style="height: 22px">
                                                                </td>
                                                                <td style="height: 22px">
                                                                    <asp:Button ID="btnReset" CssClass="button" runat="server" Text="Reset" TabIndex="6" AccessKey="R" /></td>
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
                                                                <td style="width: 130px">
                                                                    &nbsp;</td>
                                                                <td style="width: 192px">
                                                                    &nbsp;<input id="hdRecordOnCurrentPage" runat="server" style="width: 6px" type="hidden" />
                                                                    &nbsp;
                                                                    <asp:HiddenField ID="hdCorporateCodeID" runat="server" />
                                                                    </td>
                                                                <td>
                                                                    &nbsp;</td>
                                                                <td>
                                                                    </td>
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
                                                                    <asp:GridView ID="gvCorporateCode" runat="server"  AutoGenerateColumns="False" TabIndex="7" Width="100%" EnableViewState="False" AllowSorting="True"    >
                                                                      <Columns>                                               
                                                                         <asp:TemplateField HeaderText="Corporate Code" SortExpression="Code">
                                                                                 <ItemTemplate>
                                                                                 <asp:HiddenField ID="rowIDHidden" runat="server" Value='<%#Eval("RowID")%>' />
                                                                                <%#Eval("Code")%>
                                                                                </ItemTemplate>
                                                                         </asp:TemplateField>
                                                                         <asp:TemplateField HeaderText="Qualifier" SortExpression ="Qualifier">
                                                                         <ItemTemplate>
                                                                         <%#Eval("Qualifier")%>
                                                                         </ItemTemplate>
                                                                         </asp:TemplateField>
                                                                                                                                                  
                                                                         <asp:TemplateField HeaderText="Description" SortExpression="Description">
                                                                         <ItemTemplate>
                                                                         <%#Eval("Description")%>
                                                                         </ItemTemplate>
                                                                         </asp:TemplateField>
                                                                              
                                                                                                                                                     
                                                                                    <asp:TemplateField HeaderText="Action">
                                                                                        <ItemStyle HorizontalAlign="Center" />
                                                                                        <ItemTemplate>
                                                                                            <a href="#" class="LinkButtons" id="linkEdit" runat="server">Edit</a> &nbsp;
                                                                                            <a href="#" class="LinkButtons" id="linkDelete" runat="server">Delete</a>&nbsp;
                                                                                            <asp:LinkButton ID="lnkSelect" CssClass="LinkButtons" Text="Select" runat="server" CommandName="Select"
                                                                           CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Code") + "|" + DataBinder.Eval(Container.DataItem, "Qualifier") %>'></asp:LinkButton>
                                                                                        </ItemTemplate>
                                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                                    </asp:TemplateField>
                                                                                                                                                                                                                                                                          
                                                                                        
                                                                                 
                                                                         </Columns>
                                                                            <AlternatingRowStyle CssClass="lightblue" />
                                                                            <RowStyle CssClass="textbold" />
                                                                            <HeaderStyle CssClass="Gridheading" ForeColor="white" />
                                                                     </asp:GridView>                                                                    
                                                                    </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="6" style="height: 12px">
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
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="6" style="height: 12px">
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
    <!-- Code by Abhishek -->
    
  
    </form>
</body>
</html>
