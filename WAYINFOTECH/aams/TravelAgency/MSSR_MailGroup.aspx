<%@ Page Language="VB" AutoEventWireup="false" CodeFile="MSSR_MailGroup.aspx.vb" Inherits="Order_MSSR_MailGroup" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Product</title>
     <script src="../JavaScript/AAMS.js" type="text/javascript"></script>
    <script type="text/javascript" language ="javascript" >  
    
         function SelectFunction(str3,rowIndex)
        {   
           // alert(str3);
            var pos=str3.split('|');   
            try
            {       
                if(window.opener.document.forms['frmMailingList']['hdGroupId']!=null)
                {          
                    window.opener.document.forms['frmMailingList']['hdGroupId'].value=pos[0];
                    window.opener.document.forms['frmMailingList']['hdViewEmailDetailsByDept'].value="1";
                    window.opener.document.forms['frmMailingList'].submit();    
                      window.close();
                }
            }
            catch(err){}
            
            try
            {
                if(window.opener.document.forms['frmTrainingSendEmail']['hdCursorPosition']!=null)
                {  
                    
                        if(document.getElementById('gvEmailGroup')!=null)
                        {  
                            rowIndex =parseInt(rowIndex,10)
                            rowIndex=rowIndex+1;
                           var intPosition=window.opener.document.forms['frmTrainingSendEmail']['hdCursorPosition'].value;
                           var strEmailAddress =document.getElementById('gvEmailGroup').rows[rowIndex].cells[2].innerText
                           if (intPosition=="1")
                            {
                                window.opener.document.forms['frmTrainingSendEmail']['txtEmailTo'].value=strEmailAddress;
                                window.opener.document.forms['frmTrainingSendEmail']['txtEmailTo'].focus();
                            }
                            if (intPosition=="2")
                            {
                                window.opener.document.forms['frmTrainingSendEmail']['txtCC'].value=strEmailAddress;
                                window.opener.document.forms['frmTrainingSendEmail']['txtCC'].focus();
                            }
                            if (intPosition=="3")
                            {
                                window.opener.document.forms['frmTrainingSendEmail']['txtBcc'].value=strEmailAddress;
                                window.opener.document.forms['frmTrainingSendEmail']['txtBcc'].focus();
                            }
                        }
                            window.close();
                }
            }
            catch(err){}
            
       }    
    function EditFunction(id)
    {
    window.location.href="MSUP_MailGroup.aspx?Action=E&id="+id
    return false;
    }
    
    
    
    
    function DeleteFunction(id)
    {
    //window.location.href="MSSR_MailGroup.aspx?Action=D&id="+id
    //return false;
    //hdMailGroupID
     if (confirm("Are you sure you want to delete?")==true)
        {   
           document.getElementById('hdMailGroupID').value = id;
           document.forms['form1'].submit();   
        }
        else
        {
            return false;
        }
    }
  
    </script>
    <link href="../CSS/AAMS.css" type="text/css" rel="stylesheet" />
</head>
<body bgcolor="ffffff" >
    <form id="form1" runat="server"  defaultfocus ="txtgrpname" defaultbutton ="btnSearch"  >
    
     <table width="860px" align="left" height="486px" class="border_rightred">
            <tr>
                <td valign="top">
                    <table width="100%" align="left">
                        <tr>
                            <td valign="top"  align="left" >
                            <span class="menu">TravelAgency-&gt;</span><span class="sub_menu">Email Group</span></td>
                        </tr>
                        <tr>
                            <td class="heading" align="center" valign="top">
                                Search <span style="font-family: Microsoft Sans Serif">Email Group</span></td>
                        </tr>
                        <tr>
                            <td valign="top">
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td align="LEFT" class="redborder"  width="860px">
                                                         <table width="860px" border="0" align="left" cellpadding="0" cellspacing="0">
                                                         <tr>
                                                             <td>
                                                               <table width="850px" border="0" align="left" cellpadding="0" cellspacing="0" >
                                                                    <tr>
                                                                        <td  class="textbold" colspan="6" align="center" valign="TOP" style="height: 25px">
                                                                        <asp:Label ID="lblError" runat="server" CssClass="ErrorMsg"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td width="10%" class="textbold" style="height: 22px; width: 150px;">
                                                                            &nbsp;</td>
                                                                        <td class="textbold" nowrap="nowrap" style="height: 22px;" colspan="2">
                                                                             Group Name</td>
                                                                        <td style="width: 192px;" ><asp:TextBox ID="txtgrpname" runat ="server" CssClass ="textbox" Width="208px" MaxLength="30" TabIndex="2"></asp:TextBox>
                                                                            </td>
                                                                        <td width="15%" style="width: 20px">
                                                                            </td>
                                                                        <td width="30%" style="height: 22px" align="center" >
                                                                            <asp:Button ID="btnSearch" CssClass="button" runat="server" Text="Search" TabIndex="3" AccessKey="A" /></td>
                                                                    </tr>
                                                                     <tr>
                                                                        <td  class="textbold" colspan="6" align="center" valign="TOP">                                                                
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="textbold" style="height: 22px">
                                                                            &nbsp;</td>
                                                                        <td class="textbold" nowrap="nowrap"  colspan="2" style="height: 22px">
                                                                            Group Type</td>
                                                                        <td class="textbold" style="width: 192px; height: 22px;">
                                                                            <asp:DropDownList onkeyup="gotop(this.id)" ID="ddlCrs" runat="server" CssClass="dropdown" Width="214px" TabIndex="2" >
                                                                           <asp:ListItem Value ="0" Selected="True"  Text ="--All--"></asp:ListItem>
                                                                            <asp:ListItem Value ="1">MNC</asp:ListItem>
                                                                            <asp:ListItem Value ="2">ISP</asp:ListItem>
                                                                            <asp:ListItem Value ="3">1A Office</asp:ListItem>
                                                                            <asp:ListItem Value ="4">Training</asp:ListItem>
                                                                            </asp:DropDownList></td>
                                                                        <td style="height: 22px">
                                                                        </td>
                                                                        <td style="height: 22px"  align="center" >
                                                                            <asp:Button ID="btnNew" CssClass="button" runat="server" Text="New" TabIndex="4" AccessKey="N" /></td>
                                                                    </tr>
                                                                     <tr>
                                                                        <td  class="textbold" colspan="6" align="center" valign="TOP">                                                                
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="textbold">
                                                                            &nbsp;</td>
                                                                        <td colspan="2" class="textbold">
                                                                     </td>
                                                                        <td style="width: 192px">
                                                                            </td>
                                                                        <td>
                                                                            &nbsp;</td>
                                                                        <td  align="center" >
                                                                            <asp:Button ID="btnExport" runat="server" CssClass="button" TabIndex="5" Text="Export" AccessKey="E" /></td>
                                                                    </tr>                                                           
                                                                    <tr>
                                                                        <td class="textbold">
                                                                            </td>
                                                                        <td colspan="2" >
                                                                            </td>
                                                                        <td style="width: 192px">
                                                                            </td>
                                                                        <td>
                                                                            </td>
                                                                        <td>
                                                                            </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="textbold">
                                                                        </td>
                                                                        <td colspan="2">
                                                                        </td>
                                                                        <td style="width: 192px">
                                                                        </td>
                                                                        <td>
                                                                        </td>
                                                                        <td  align="center" >
                                                                            <asp:Button ID="btnReset" CssClass="button" runat="server" Text="Reset" TabIndex="6" AccessKey="R" /></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="textbold" style="height: 19px">
                                                                        </td>
                                                                        <td colspan="2" class="textbold" style="height: 19px">
                                                                            <input id="hdRecordOnCurrentPage" runat="server" style="width: 6px" type="hidden" />
                                                                            <input id="hdMailGroupID" runat="server" style="width: 6px" type="hidden" />
                                                                            </td>
                                                                        <td style="width: 192px; height: 19px;">
                                                                            </td>
                                                                        <td style="height: 19px">
                                                                        </td>
                                                                        <td style="height: 19px">
                                                                        </td>
                                                                    </tr>
                                                                    
                                                                    <tr>
                                                                        <td colspan="6"  style ="width:850px">
                                                                       
                                                                       
                                                                            
                                                                            </td>
                                                                    </tr>
                                                                  
                                                                    <tr>
                                                                        <td colspan="6" >
                                                                        </td>
                                                                    </tr>
                                                        </table>                                     
                                                             </td>
                                                          </tr>
                                                          <tr>
                                                         <td>
                                                              <table  width="860px" cellpadding="0" cellspacing ="0"  border ="0">
                                                                   <tr>
                                                                     <td   style ="width:860px">
                                                                                <asp:GridView ID="gvEmailGroup" runat="server"  AutoGenerateColumns="False" TabIndex="7" Width="860px" AllowSorting="True"    >
                                                                                 <Columns>
                                                                                 <asp:BoundField DataField="GroupId" Visible ="False"/>
                                                                                 <asp:BoundField DataField="GroupName" HeaderText="Group Name" SortExpression="GroupName"   ItemStyle-Wrap="true"  HeaderStyle-Wrap="false"   ItemStyle-Width="100px"   />
                                                                                 <asp:BoundField DataField="GroupType" HeaderText="Email Group Type" SortExpression="GroupType" ItemStyle-Wrap="true" itemStyle-Width="100px"    HeaderStyle-Wrap="false"  />
                                                                                  <asp:BoundField DataField="EmailID" HeaderText="Email Id" SortExpression="EmailID"  ItemStyle-Wrap="true"  HeaderStyle-Wrap="false"  ItemStyle-Width="510px" />
                                                                                            <asp:TemplateField  ItemStyle-Width="150px"    HeaderStyle-Wrap="false"  ItemStyle-Wrap="false"  HeaderText="Action"  >
                                                                                                    <ItemTemplate>
                                                                                                           <asp:LinkButton ID="btnEdit" runat="server" Text ="Edit" CausesValidation="false" CommandName="EditX" CommandArgument='<%#Eval("GROUPID")%>'
                                                                                                            CssClass="LinkButtons">                                                                                                            
                                                                                                           </asp:LinkButton>&nbsp;&nbsp;<asp:LinkButton ID="btnDelete" Text ="Delete" runat="server" CausesValidation="false" CommandName="DeleteX" CommandArgument='<%#Eval("GROUPID")%>'
                                                                                                            CssClass="LinkButtons">
                                                                                                        </asp:LinkButton>&nbsp;&nbsp;<asp:LinkButton ID="lnkSelect" CssClass="LinkButtons" Text="Select" runat="server" CommandName="SelectX" CommandArgument='<%# Eval("GROUPID")  %>'></asp:LinkButton>     
                                                                                                    </ItemTemplate>
                                                                                                    <ItemStyle  CssClass="ItemColor" />
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
                                                                <td colspan="6" style ="width:850px">
                                                                 <asp:Panel ID="pnlPaging" runat="server" Visible="false" Width="100%">
                                                                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                      <tr class="paddingtop paddingbottom"  >                                                                                                                                                
                                                                          <td style="width: 30%" class="left"><span  class="textbold"><b>&nbsp;No. of records found</b></span>&nbsp;&nbsp;<asp:TextBox ID="txtTotalRecordCount" runat ="server"  Width="105px" CssClass="textboxgrey" ></asp:TextBox></td>
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
                </td>
            </tr>
        </table>
    <!-- Code by Rakesh -->
    
  
    </form>
</body>
</html>
