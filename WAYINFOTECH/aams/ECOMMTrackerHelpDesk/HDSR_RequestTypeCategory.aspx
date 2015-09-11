<%@ Page Language="VB" AutoEventWireup="false" CodeFile="HDSR_RequestTypeCategory.aspx.vb" Inherits="ETHelpDesk_HDSR_RequestTypeCategory" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
      <title>PTR Type</title>
      <link href="../CSS/AAMS.css" type="text/css" rel="stylesheet" />
     <script src="../JavaScript/ETracker.js" type="text/javascript"></script>
     <script type="text/javascript" language ="javascript" > 
     function  NewHDUPReqTypeCategory()
       {    
           window.location="HDUP_RequestTypeCategory.aspx?Action=I";
           return false;
       }  
    function  PtrReqTypeCategoryReset()
    {       
        document.getElementById("lblError").innerHTML="";    
        document.getElementById("txtCategory").value=""; 
        if (document.getElementById("gvRequestTypeCategory")!=null) 
        document.getElementById("gvRequestTypeCategory").style.display ="none"; 
        document.getElementById("txtCategory").focus(); 
        return false;
    }
     function ReqTypeCategoryMandatory()
    {
//       
         return true;
     }
     function DeleteFunction(CheckBoxObj)
      {   
        if (confirm("Are you sure you want to delete?")==true)
        {   
            window.location.href="HDSR_RequestTypeCategory.aspx?Action=D|"+CheckBoxObj + "|"+ document.getElementById("<%=txtCategory.ClientID%>").value ;
            return false;
        }
    }
      function EditFunction(CheckBoxObj)
    {                
          window.location ="HDUP_RequestTypeCategory.aspx?Action=U&HD_RE_TYCAT_ID=" + CheckBoxObj; 
          return false;
    }   
    </script>
    

    <link href="../Calender/calendar-blue.css" media="all" rel="stylesheet" title="win2k-cold-1"
        type="text/css" />
    
</head>
<body>
    <form id="form1" runat="server" defaultbutton ="btnSearch" defaultfocus ="txtCategory">
      <table width="860px" align="left" class="border_rightred" >
            <tr>
                <td valign="top">
                    <table width="100%" >
                        <tr>
                            <td valign="top"  align="left" >
                                <strong><span style="font-size: 9pt; font-family: Trebuchet MS">ETrackers HelpDesk-&gt;</span><span class="menu"></span></strong><span class="sub_menu">Request Type Category</span></td>
                        </tr>
                        <tr>
                            <td class="heading" align="center" valign="top" >Search Request Type Category</td>
                        </tr>
                        <tr>
                            <td valign="top">
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td  align ="Center" class="redborder">                                 
                                                        <table width="100%" border="0"  cellpadding="0" cellspacing="0" >
                                                            <tr>
                                                                <td  class="textbold" colspan="6" align="center" style="height:25px;" valign="TOP"><asp:Label ID="lblError" runat="server" CssClass="ErrorMsg"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width:10%;" class="textbold" >&nbsp;</td>
                                                                <td style="width:14%;" class="textbold" ></td>
                                                                <td style="width:16%;" class="textbold" ><span class="textbold">Category</span></td>
                                                                <td style="width:20%;" class="textbold"><asp:TextBox ID="txtCategory" runat ="server" CssClass ="textbox" Width="161px" MaxLength="20" TabIndex="1" ></asp:TextBox></td>
                                                                <td style="width:10%;" ></td>
                                                                <td style="width:30%;" align ="left"><asp:Button ID="btnSearch" CssClass="button" runat="server" Text="Search" TabIndex="2" AccessKey="a" /></td>
                                                            </tr>
                                                             <tr>
                                                                <td  class="textbold" colspan="6" align="center" valign="TOP" >                                                                
                                                                </td>
                                                            </tr>
                                                              <tr>
                                                                <td style="width:10%;" class="textbold" >&nbsp;</td>
                                                                <td style="width:14%;" class="textbold" ></td>
                                                                <td style="width:16%;" class="textbold" ><span class="textbold"></span></td>
                                                                <td style="width:20%;" class="textbold"></td>
                                                                <td style="width:10%; " ></td>
                                                                <td style="width:30%; " align ="left" ><asp:Button ID="btnNew" CssClass="button" runat="server" Text="New" TabIndex="3" AccessKey="n" /></td>
                                                            </tr>
                                                                <tr>
                                                                <td  class="textbold" colspan="6" align="center" valign="TOP" >                                                                
                                                                </td>
                                                            </tr>
                                                              <tr>
                                                                <td style="width:10%;" class="textbold" >&nbsp;</td>
                                                                <td style="width:14%;" class="textbold" ></td>
                                                                <td style="width:36%;" colspan="2" class="ErrorMsg" >Field Marked * are Mandatory</td>
                                                               
                                                                <td style="width:10%;" ></td>
                                                                <td style="width:30%;" align ="left" ><asp:Button ID="btnReset" CssClass="button" runat="server" Text="Reset" TabIndex="4" AccessKey="r" /></td>
                                                            </tr>
                                                             <tr>
                                                                <td  class="textbold" colspan="6" align="center" valign="TOP" style="height:10px;">                                                                
                                                                </td>
                                                            </tr>
                                                            
                                                               <tr>
                                                                <td  class="textbold" colspan="6" align="center" valign="TOP">                                                                
                                                                </td>
                                                            </tr>
                                                              <tr>
                                                                <td  class="textbold" colspan="6" align="center" valign="TOP" style="height:10px;">                                                                
                                                                </td>
                                                            </tr>
                                                        </table>
                                                        <table width="600px" border="0" cellspacing="0" cellpadding="0"> 
                                                          <tr>
                                                               <td align ="center"><asp:GridView ID="gvRequestTypeCategory" runat="server"  AutoGenerateColumns="False" TabIndex="5" width="600px" EnableViewState="False" >
                                                                                 <Columns>
                                                                                 <asp:TemplateField HeaderText="Category Name">
                                                                                                <itemtemplate>
                                                                                                    <%#Eval("HD_RE_TYCAT_NAME")%>
                                                                                                    <asp:HiddenField ID="HDHDRETYCATID" runat="server" Value='<%#Eval("HD_RE_TYCAT_ID")%>' />
                                                                                                </itemtemplate>
                                                                                        </asp:TemplateField> 
                                                                                        <asp:TemplateField>
                                                                                                <HeaderTemplate>
                                                                                                    <asp:Label ID="lblAction" runat="server" Text="Action"></asp:Label>
                                                                                                </HeaderTemplate>
                                                                                                <ItemTemplate>
                                                                                                       <a href="#" class="LinkButtons" id="linkEdit" runat="server">Edit</a> &nbsp; <a href="#" class="LinkButtons" id="linkDelete" runat="server">
                                                                                                                    Delete</a>
                                                                                                </ItemTemplate>
                                                                                                <ItemStyle Width="20%" CssClass="ItemColor" />
                                                                                                <HeaderStyle Wrap="False" CssClass="Gridheading" />
                                                                                       </asp:TemplateField>
                                                                                 </Columns>
                                                                                        <AlternatingRowStyle CssClass="lightblue" />
                                                                                        <RowStyle CssClass="textbold" />
                                                                                        <HeaderStyle CssClass="Gridheading" />
                                                                                        
                                                                  </asp:GridView></td>
                                                         </tr>
                                                        <tr>
                                                            <td  class="textbold"  align="center" valign="TOP" style="height:10px;">                                                                
                                                            </td>
                                                        </tr>
                                                      </table>
                                                                  
                                        </td>
                                    </tr>
                                       <tr>
                                            <td  class="textbold"  align="center" valign="TOP" style="height:10px;">                                                                
                                            </td>
                                        </tr>
                                </table>
                            </td>
                        </tr>
                     
                    </table>
                </td>
            </tr>
        </table>
    <!-- Code by Dev Abhishek -->
    </form>
</body>
</html>
