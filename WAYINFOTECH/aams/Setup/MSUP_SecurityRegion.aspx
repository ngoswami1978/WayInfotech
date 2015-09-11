<%@ Page Language="VB" AutoEventWireup="false" CodeFile="MSUP_SecurityRegion.aspx.vb" Inherits="Setup_MSUP_SecurityRegion" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>AAMS: Security Region</title>
    <link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />
      <script src="../JavaScript/AAMS.js" type="text/javascript"></script>
        <script language="javascript" type="text/javascript">
        function  NewMSUPSecurityRegion()
       {    
           window.location="MSUP_SecurityRegion.aspx?Action=I";
           return false;
       }    
     function CheckMandatoty()
    {
        if (document.getElementById("txtRegionName").value.trim()=="")
        {          
             document.getElementById("lblError").innerHTML="Security region is mandatory."
             document.getElementById("txtRegionName").focus();
             return false;
        }
        if (  document.getElementById("txtRegionName").value!="")
         {
           if(IsDataValid(document.getElementById("txtRegionName").value,2)==false)
            {
            document.getElementById("lblError").innerHTML="Security region is not valid.";
            document.getElementById("txtRegionName").focus();
            return false;
            } 
         }   
        return true;      
    }
    </script>
</head>
<body>
    <form id="frmSecRegion" runat="server" defaultfocus ="txtRegionName">
    <div>
     <table width="860px" align="left" height="486px" class="border_rightred">
            <tr>
                <td valign="top">
                <table width="100%" align="left" >
                          <tr>
                            <td valign="top"  align="left">
                            <span class="menu">Setup-></span><span class="sub_menu">Security Region</span>
                            </td>
                        </tr>
                       
                                   
                        <tr>
                            <td class="heading" align="center" valign="top" >
                                Manage Security Region
                            </td>
                        </tr>
                       
                        
                        <tr>
                            <td  >
                                <table border="0" cellpadding="0" cellspacing="0"  width="100%">
                                   
                                    <tr>
                                        <td width="100%" class="redborder">
                                            <table align="left" border="0" cellpadding="2" cellspacing="0" width="100%">
                                              
                                                <tr>
                                                    
                                                    <td class="textbold" style="height: 28px;width:100%" colspan="4" valign="top" >
                                                          <table width="100%" border="0" align="left" cellpadding="0" cellspacing="0" >
                                                            <%--<tr>
                                                                <td width="20px"  class="textbold" colspan="6" align="center" height="25px" valign="TOP">
                                                                    &nbsp;</td>
                                                            </tr>--%>
                                                              <tr>
                                                                  <td align="center" class="textbold" colspan="6" height="20px" valign="top"><asp:Label ID="lblError" runat="server" CssClass="ErrorMsg" Text=""></asp:Label>
                                                                  </td>
                                                              </tr>
                                                            <tr>
                                                                <td width="6%" class="textbold" style="height: 22px">
                                                                    &nbsp;</td>
                                                                <td class="textbold" style="height: 22px; width: 348px;">
                                                                    </td>
                                                                <td width="12%" style="height: 22px" align=left >
                                                                    Region Name<span class="Mandatory">* </span> </td>
                                                                <td style="height: 22px; width: 176px;" >
                                                                    <span class="textbold">
                                                                    <asp:TextBox ID="txtRegionName" runat="server" CssClass="textfield" TabIndex="1" Width="177px" MaxLength="10"></asp:TextBox></span></td>
                                                                <td width="17%" style="height: 22px">
                                                                    </td>
                                                                <td width="18%" style="height: 22px">
                                                                    <asp:Button ID="btnSave" runat="server" CssClass="button" Text="Save" TabIndex="3" AccessKey="A" /></td>
                                                            </tr>
                                                             <tr>
                                                                <td  class="textbold" colspan="6" align="center" valign="TOP">                                                                
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="textbold" style="height: 22px">
                                                                    &nbsp;</td>
                                                                <td class="textbold" style="height: 22px; width: 348px;">
                                                                    </td>
                                                                <td style="height: 22px" align=left>
                                                                    </td>
                                                                <td class="textbold" style="height: 22px; width: 176px;">
                                                                    </td>
                                                                <td style="height: 22px">
                                                                </td>
                                                                <td style="height: 22px">
                                                                    <asp:Button ID="btnNew" runat="server" CssClass="button" Text="New" TabIndex="4" AccessKey="N" /></td>
                                                            </tr>
                                                             <tr>
                                                                <td  class="textbold" colspan="6" align="center" valign="TOP">                                                                
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="textbold" style="height: 22px">
                                                                    &nbsp;</td>
                                                                <td class="textbold" style="height: 22px; width: 348px;">
                                                                </td>
                                                                <td style="height: 22px" align=left colspan="2" class="ErrorMsg" >
                                                                    Field Marked * are Mandatory</td>
                                                                <td style="height: 22px">
                                                                    &nbsp;</td>
                                                                <td style="height: 22px">
                                                                    <asp:Button ID="btnReset" runat="server" CssClass="button" Text="Reset" TabIndex="5" AccessKey="R" /></td>
                                                            </tr>
                                                             <tr>
                                                                <td  class="textbold" colspan="6" align="center" valign="TOP">                                                                
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="textbold" style="height: 15px">
                                                                    &nbsp;</td>
                                                                    <td style="height: 15px"></td>
                                                                <td colspan="2"  style="height: 15px" >
                                                                    Select&nbsp; Aoffice</td>
                                                                <td style="height: 15px">
                                                                    &nbsp;</td>
                                                                <td style="height: 15px">
                                                                    &nbsp;</td>
                                                            </tr> 
                                                             <tr>
                                                                <td  class="textbold" colspan="6" align="center" valign="TOP">                                                                
                                                                </td>
                                                            </tr>                                                            
                                                              <tr>
                                                                  <td class="textbold">
                                                                  </td>
                                                                  <td>
                                                                  </td>
                                                                  <td colspan="2">
                                                                  <asp:GridView ID="dbgrdManageSecurityRegion" runat="server" AutoGenerateColumns="False" Width="100%" TabIndex="6">
                                                            <Columns>                          
                                                            
                                                            <asp:BoundField DataField="Aoffice" HeaderText=" Aoffice" />                                                                                                                                                                                                                                                                       
                                                            <asp:TemplateField  HeaderText="Select">                                                            
                                                                <ItemStyle HorizontalAlign="Left"/>   
                                                                <ItemTemplate>
                                                                    <asp:CheckBox CssClass="LinkButtons" ID="btnEdit" Checked='<%# FindStatus(Eval("Select")) %>' CausesValidation="false"  runat="server" ></asp:CheckBox>                                                                         
                                                                    <!--<asp:LinkButton CssClass="LinkButtons" ID="btnDelete" CausesValidation="false" Text="Delete"   runat="server"></asp:LinkButton>  -->
                                                                </ItemTemplate>                                                                                                                                
                                                            </asp:TemplateField>                                                                                                                          
                                                            </Columns> 
                                                            <AlternatingRowStyle  CssClass="lightblue"/>
                                                            <HeaderStyle CssClass="Gridheading" HorizontalAlign="Left"/>
                                                            <RowStyle CssClass="textbold"/>
                                                          
                                                            </asp:GridView>
                                                                      </td>
                                                                  <td>
                                                                  </td>
                                                                  <td>
                                                                  </td>
                                                              </tr>
                                                            <tr>
                                                                <td colspan="6" style="height: 4px">
                                                                    </td>
                                                            </tr>
                                                            <tr height="20px">
                                                                <td colspan="6" height="4">
                                                                     </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="6" style="height: 12px">
                                                                </td>
                                                            </tr>
                                                        </table>
                                                        <br />
                                                           
                                                     </td>
                                                    <td width="18%" rowspan="1" valign="top" >
                                                        </td>
                                                </tr>
                                                <tr>
                                                    
                                                    <td class="textbold" colspan="5">                                                        
                                                          
                                                        </td>
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
            <tr>
                <td  valign="top" style="height: 23px">
                
                  
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>

</html>
