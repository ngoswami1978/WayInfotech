<%@ Page Language="VB" AutoEventWireup="false" CodeFile="HDUP_RequestType.aspx.vb" Inherits="HelpDesk_HDUP_RequestType" %>

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
            if (document.getElementById("drpCatName").selectedIndex==0)
            {
            document.getElementById("lblError").innerHTML="Category Name is mandatory.";
            document.getElementById("drpCatName").focus();
            return false;
            } 
              if (document.getElementById("txtType").value.trim()=="")
            {
            document.getElementById("lblError").innerHTML="Type is mandatory.";
            document.getElementById("txtType").focus();
            return false;
            } 
              if (document.getElementById("txtTypeToPrint").value.trim()=="")
            {
            document.getElementById("lblError").innerHTML="Type To Print is mandatory.";
            document.getElementById("txtTypeToPrint").focus();
            return false;
            } 

 
         return true;
     }
   
    </script>
    
</head>
<body>
    <form id="form1" runat="server">
      <table width="860px" align="left" class="border_rightred" >
            <tr>
                <td valign="top">
                    <table width="100%" >
                        <tr>
                            <td valign="top"  align="left" ><span class="menu">Help Desk-></span><span class="sub_menu">Request Type</span></td>
                        </tr>
                        <tr>
                            <td class="heading" align="center" valign="top" >
                                Manage Request Type</td>
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
                                                                <td style="width:16%;" class="textbold" ><span class="textbold">Category Name</span><span class ="Mandatory">*</span> </td>
                                                                <td style="width:20%;" class="textbold"><asp:DropDownList ID="drpCatName" runat ="server" CssClass ="dropdownlist" Width="167px" TabIndex="1" Height="18px"  >
                                                                    <asp:ListItem>---Select One---</asp:ListItem>
                                                                    <asp:ListItem Value="1">Category1</asp:ListItem>
                                                                    <asp:ListItem Value="2">Categoey2</asp:ListItem>
                                                                </asp:DropDownList></td>
                                                                <td style="width:10%;" ></td>
                                                                <td style="width:30%;" ><asp:Button ID="btnSave" CssClass="button" runat="server" Text="Save" TabIndex="4" AccessKey="s" /></td>
                                                            </tr>
                                                             <tr>
                                                                <td  class="textbold" colspan="6" align="center" valign="TOP" >                                                                
                                                                </td>
                                                            </tr>
                                                              <tr>
                                                                <td style="width:10%;" class="textbold" >&nbsp;</td>
                                                                <td style="width:14%;" class="textbold" ></td>
                                                                <td style="width:16%;" class="textbold" ><span class="textbold">Type</span><span class ="Mandatory">*</span></td>
                                                                <td style="width:20%;" class="textbold"><asp:TextBox ID="txtType" runat ="server" CssClass ="textbox" Width="161px" MaxLength="40" TabIndex="2" ></asp:TextBox></td>
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
                                                                <td style="width:16%;" class="textbold" ><span class="textbold">Type To Print</span><span class ="Mandatory">*</span></td>
                                                                <td style="width:20%;" class="textbold"><asp:TextBox ID="txtTypeToPrint" runat ="server" CssClass ="textbox" Width="161px" MaxLength="100" TabIndex="3" ></asp:TextBox></td>
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
    </form>
</body>
</html>
