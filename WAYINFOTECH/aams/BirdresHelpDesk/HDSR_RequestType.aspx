<%@ Page Language="VB" AutoEventWireup="false" CodeFile="HDSR_RequestType.aspx.vb" Inherits="BirdresHelpDesk_HDSR_RequestType" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
      <title>Request Type</title>
      <link href="../CSS/AAMS.css" type="text/css" rel="stylesheet" />
     <script src="../JavaScript/AAMS.js" type="text/javascript"></script>
     <script type="text/javascript" language ="javascript" > 
     function  NewHDSRRequestType()
       {    
           window.location="HDUP_RequestType.aspx?Action=I";
           return false;
       }  
    function RequestTypeReset()
    {       
        document.getElementById("lblError").innerHTML="";    
        document.getElementById("txtType").value="";
        document.getElementById("txtTypeToPrint").value="";
        document.getElementById("drpCatName").selectedIndex=0;         
        if (document.getElementById("gvRequestType")!=null) 
        document.getElementById("gvRequestType").style.display ="none"; 
        document.getElementById("drpCatName").focus(); 
        return false;
    }
     function RequestTypeMandatory()
    {
//        if (  document.getElementById("txtTypeToPrint").value!="")
//         {
//           if(IsDataValid(document.getElementById("txtTypeToPrint").value,2)==false)
//            {
//            document.getElementById("lblError").innerHTML="Type To Print is not valid.";
//            document.getElementById("txtTypeToPrint").focus();
//            return false;
//            } 
//         }   
         return true;
     }
     function DeleteFunction(CheckBoxObj)
      {   
        if (confirm("Are you sure you want to delete?")==true)
        {   
            window.location.href="HDSR_RequestType.aspx?Action=D|"+CheckBoxObj + "|"+ document.getElementById("<%=drpCatName.ClientID%>").value + "|"+ document.getElementById("<%=txtType.ClientID%>").value + "|"+ document.getElementById("<%=txtTypeToPrint.ClientID%>").value;
            return false;
        }
    }
      function EditFunction(CheckBoxObj)
    {                
          window.location ="HDUP_RequestType.aspx?Action=U&HD_RETYPE_ID=" + CheckBoxObj; 
          return false;
    }   
    </script>
    
</head>
<body>
    <form id="form1" runat="server" defaultbutton ="btnSearch" defaultfocus ="drpCatName">
      <table width="860px" align="left" class="border_rightred" >
            <tr>
                <td valign="top">
                    <table width="100%" >
                        <tr>
                            <td valign="top"  align="left" ><span class="menu">
Birdres HelpDesk-></span><span class="sub_menu">Request Type</span></td>
                        </tr>
                        <tr>
                            <td class="heading" align="center" valign="top" >Search Request Type</td>
                        </tr>
                        <tr>
                            <td valign="top">
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td  align ="left" class="redborder">                                 
                                                        <table width="100%" border="0"  cellpadding="0" cellspacing="0" >
                                                            <tr>
                                                                <td  class="textbold" colspan="6" align="center" style="height:25px;" valign="TOP"><asp:Label ID="lblError" runat="server" CssClass="ErrorMsg"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width:10%;" class="textbold" >&nbsp;</td>
                                                                <td style="width:14%;" class="textbold" ></td>
                                                                <td style="width:16%;" class="textbold" ><span class="textbold">Category Name</span></td>
                                                                <td style="width:20%;" class="textbold"><asp:DropDownList ID="drpCatName" runat ="server" CssClass ="dropdownlist" Width="167px" TabIndex="1"  >
                                                                    <asp:ListItem>---Select One---</asp:ListItem>
                                                                    <asp:ListItem Value="1">Category1</asp:ListItem>
                                                                    <asp:ListItem Value="2">Categoey2</asp:ListItem>
                                                                </asp:DropDownList></td>
                                                                <td style="width:10%;" ></td>
                                                                <td style="width:30%;" ><asp:Button ID="btnSearch" CssClass="button" runat="server" Text="Search" TabIndex="4" AccessKey="a" /></td>
                                                            </tr>
                                                             <tr>
                                                                <td  class="textbold" colspan="6" align="center" valign="TOP" >                                                                
                                                                </td>
                                                            </tr>
                                                              <tr>
                                                                <td style="width:10%;" class="textbold" >&nbsp;</td>
                                                                <td style="width:14%;" class="textbold" ></td>
                                                                <td style="width:16%;" class="textbold" ><span class="textbold">Type</span></td>
                                                                <td style="width:20%;" class="textbold"><asp:TextBox ID="txtType" runat ="server" CssClass ="textbox" Width="161px" MaxLength="30" TabIndex="2" ></asp:TextBox></td>
                                                                <td style="width:10%;" ></td>
                                                                <td style="width:30%;" ><asp:Button ID="btnNew" CssClass="button" runat="server" Text="New" TabIndex="5" AccessKey="n" /></td>
                                                            </tr>
                                                                <tr>
                                                                <td  class="textbold" colspan="6" align="center" valign="TOP" >                                                                
                                                                </td>
                                                            </tr>
                                                              <tr>
                                                                <td style="width:10%;" class="textbold" >&nbsp;</td>
                                                                <td style="width:14%;" class="textbold" ></td>
                                                                <td style="width:16%;" class="textbold" ><span class="textbold">Type To Print</span></td>
                                                                <td style="width:20%;" class="textbold"><asp:TextBox ID="txtTypeToPrint" runat ="server" CssClass ="textbox" Width="161px" MaxLength="30" TabIndex="3" ></asp:TextBox></td>
                                                                <td style="width:10%;" ></td>
                                                                <td style="width:30%;" ><asp:Button ID="btnReset" CssClass="button" runat="server" Text="Reset" TabIndex="6" AccessKey="r" /></td>
                                                            </tr>
                                                             <tr>
                                                                <td  class="textbold" colspan="6" align="center" valign="TOP" style="height:10px;">                                                                
                                                                </td>
                                                            </tr>
                                                             <tr>
                                                                <td style="width:10%;" class="textbold"  >&nbsp;</td>
                                                                <td style="width:14%;" class="textbold" ></td>
                                                                <td style="width:36%;" colspan="2" class="ErrorMsg" >Field Marked * are Mandatory</td>                                                               
                                                                <td style="width:10%;" ></td>
                                                                <td style="width:30%;" ></td>
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
                                                        <table width="830px" border="0" cellspacing="0" cellpadding="0"> 
                                                          <tr>
                                                               <td><asp:GridView ID="gvRequestType" runat="server"  AutoGenerateColumns="False" TabIndex="7" width="830px" EnableViewState="False" >
                                                                                 <Columns>
                                                                                 <asp:TemplateField HeaderText="Category Name">
                                                                                                <itemtemplate>
                                                                                                    <%#Eval("HD_RETYPE_NAME")%>
                                                                                                    <asp:HiddenField ID="HDRETYPEID" runat="server" Value='<%#Eval("HD_RETYPE_ID")%>' />
                                                                                                </itemtemplate>
                                                                                        </asp:TemplateField>     
                                                                                        <asp:BoundField DataField="HD_RETYPE_NAME" HeaderText="Request Type " />
                                                                                        <asp:BoundField DataField="HD_RETYPE_PRINT"  HeaderText="Type to Print" />
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
