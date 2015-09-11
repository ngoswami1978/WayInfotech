<%@ Page Language="VB"  AutoEventWireup="false" CodeFile="TAUP_OnlineStatus.aspx.vb" Inherits="TravelAgency_TAUP_OnlineStatus" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Add/Modify Online Status</title>
     <script src="../JavaScript/AAMS.js" type="text/javascript"></script>
    <script type="text/javascript" language ="javascript" >      
      function  NewMSUPOnlineStatus()
       {        
           window.location="TAUP_OnlineStatus.aspx?Action=I";
           return false;
       }  
   function OnlineStatusMandatory()
    {
      if (  document.getElementById("txtStatusCode").value=="")
         {
            document.getElementById("lblError").innerHTML="Status Code is mandatory.";
            document.getElementById("txtStatusCode").focus();
            return false;         
         }
        if (  document.getElementById("txtStatusCode").value!="")
         {
           if(IsDataValid(document.getElementById("txtStatusCode").value,7)==false)
            {
            document.getElementById("lblError").innerHTML="Status Code is not valid.";
            document.getElementById("txtStatusCode").focus();
            return false;
            } 
         }
         if (  document.getElementById("txtOnlineStatus").value=="")
         {
            document.getElementById("lblError").innerHTML="Online Status is mandatory.";
            document.getElementById("txtOnlineStatus").focus();
            return false;         
         }
          if (  document.getElementById("txtOnlineStatus").value!="")
         {
           if(IsDataValid(document.getElementById("txtOnlineStatus").value,7)==false)
            {
            document.getElementById("lblError").innerHTML="Onlinle Status is not valid.";
            document.getElementById("txtOnlineStatus").focus();
            return false;
            } 
         }
         
           if(document.getElementById("txtSegExpected").value!='')
      {
          var txtVal=document.getElementById("txtSegExpected").value;
          if(IsDataValid(txtVal,3)==false)
          {
              document.getElementById("lblError").innerHTML="Please Enter valid Segment Expected.";
              document.getElementById("txtSegExpected").focus();
              return false;
          }
              if(parseInt(txtVal.trim(),10)>32767  || parseInt(txtVal.trim(),10)<0)
            {
                 document.getElementById("lblError").innerHTML="Please Enter valid Segment Expected."
                 document.getElementById("txtSegExpected").focus();
               return false;
          
            }     
      
      }
      
       if (  document.getElementById("txtUnitCost").value!="")
         {
           if(IsDataValid(document.getElementById("txtUnitCost").value,5)==false)
            {
            document.getElementById("lblError").innerHTML="Unit Cost is Numeric";
            document.getElementById("txtUnitCost").focus();
            return false;
            } 
         }
         
         
      
         
         return true;
     }
  
    </script>
    <link href="../CSS/AAMS.css" type="text/css" rel="stylesheet" />
</head>
<body bgcolor="ffffff"   >
    <form id="frmUpTravelAgency" runat="server"  defaultfocus="txtStatusCode">
    
     <table width="860px" align="left" height="486px" class="border_rightred">
            <tr>
                <td valign="top">
                    <table width="100%" align="left">
                        <tr>
                            <td valign="top"  align="left" >
                            <span class="menu">TravelAgency-&gt;</span><span class="sub_menu">Online Status</span>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="heading" align="center" valign="top">
                                Manage Online Status</td>
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
                                                                <td class="textbold" style="height: 22px; width: 129px;">
                                                                    &nbsp;</td>
                                                                <td class="textbold" style="width: 57px; height: 22px;">
                                                                    </td>
                                                                <td class="textbold" style="width: 137px; height: 22px;">
                                                                    Category Name</td>
                                                                <td style="width: 192px; height: 22px" >
                                                                  <asp:DropDownList ID="DlstConnectionCategory" runat="server" CssClass="dropdownlist" TabIndex="1" Width="216px">
                                                </asp:DropDownList>
                                                                    </td>
                                                                <td style="height: 22px; width: 33px;">
                                                                    </td>
                                                                <td width="30%" style="height: 22px"> <asp:Button ID="btnSave" CssClass="button" runat="server" Text="Save" TabIndex="3" AccessKey="S" />
                                                                    </td>
                                                            </tr>
                                                             <tr>
                                                                <td  class="textbold" colspan="6" align="center" valign="TOP">                                                                
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="textbold" style="height: 22px; width: 129px;">
                                                                    &nbsp;</td>
                                                                <td class="textbold" style="width: 57px; height: 22px;">
                                                                    </td>
                                                                <td class="textbold" style="width: 137px; height: 22px;">
                                                                    Status Code<span class="Mandatory">*</span></td>
                                                                <td style="width: 192px; height: 22px" >
                                                                   <asp:TextBox ID="txtStatusCode" runat ="server" CssClass ="textbox" Width="208px" MaxLength="6" TabIndex="1"></asp:TextBox>
                                                                    </td>
                                                                <td style="height: 22px; width: 33px;">
                                                                    </td>
                                                                <td width="30%" style="height: 22px"><asp:Button ID="btnNew" CssClass="button" runat="server" Text="New" TabIndex="4" AccessKey="N" />
                                                                   </td>
                                                            </tr>
                                                             <tr>
                                                                <td  class="textbold" colspan="6" align="center" valign="TOP">                                                                
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="textbold" style="height: 22px; width: 129px;">
                                                                    &nbsp;</td>
                                                                <td class="textbold" style="width: 57px; height: 22px;">
                                                                    </td>
                                                                <td class="textbold" style="width: 137px; height: 22px;">
                                                                    Online Status<span class="Mandatory">*</span></td>
                                                                <td class="textbold" style="width: 192px; height: 22px;">
                                                                    <asp:TextBox ID="txtOnlineStatus" runat="server" CssClass="textbox" MaxLength="30" Width="208px" TabIndex="2"></asp:TextBox></td>
                                                                <td style="height: 22px; width: 33px;">
                                                                </td>
                                                                <td style="height: 22px"> <asp:Button ID="btnReset" CssClass="button" runat="server" Text="Reset" TabIndex="5" AccessKey="R" />
                                                                    </td>
                                                            </tr>
                                                           <tr>
                                                                <td  class="textbold" colspan="6" align="center" valign="TOP">                                                                
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="textbold" style="height: 22px; width: 129px;">
                                                                </td>
                                                                <td class="textbold" style="width: 57px; height: 22px">
                                                                </td>
                                                                <td class="textbold" style="width: 137px; height: 22px">
                                                                    Segment Expected</td>
                                                                <td class="textbold" style="width: 192px; height: 22px">
                                                                    <asp:TextBox ID="txtSegExpected" runat="server" CssClass="textbox" MaxLength="5" TabIndex="2"
                                                                        Width="208px"></asp:TextBox></td>
                                                                <td style="height: 22px; width: 33px;">
                                                                </td>
                                                                <td style="height: 22px">
                                                                   </td>
                                                            </tr>
                                                            
                                                            <tr>
                                                                <td  class="textbold" colspan="6" align="center" valign="TOP">                                                                
                                                                </td>
                                                            </tr>
                                                            
                                                            <tr>
                                                                <td class="textbold" style="height: 21px; width: 129px;">
                                                                </td>
                                                                <td class="textbold" style="width: 57px; height: 21px">
                                                                </td>
                                                                <td class="textbold" style="width: 137px; height: 21px">
                                                                    Unit Cost (IN)</td>
                                                                <td class="textbold" style="width: 192px; height: 21px">
                                                                    <asp:TextBox ID="txtUnitCost" runat="server" CssClass="textboxgrey" ReadOnly ="true" MaxLength="8" TabIndex="2"
                                                                        Width="208px"></asp:TextBox></td>
                                                                <td style="width: 33px; height: 21px">
                                                                </td>
                                                                <td style="height: 21px">
                                                                </td>
                                                            </tr>
                                                             <tr>
                                                                <td  class="textbold" colspan="6" align="center" valign="TOP">                                                                
                                                                </td>
                                                            </tr>
                                                            
                                                            <tr>
                                                                <td class="textbold" style="height: 21px; width: 129px;">
                                                                </td>
                                                                <td class="textbold" style="width: 57px; height: 21px">
                                                                </td>
                                                                <td class="textbold" style="width: 137px; height: 21px">
                                                                    Unit Cost (NP)</td>
                                                                <td class="textbold" style="width: 192px; height: 21px">
                                                                    <asp:TextBox ID="txtNPUnitCost" runat="server" CssClass="textboxgrey" ReadOnly ="true" MaxLength="8" TabIndex="2"
                                                                        Width="208px"></asp:TextBox></td>
                                                                <td style="width: 33px; height: 21px">
                                                                </td>
                                                                <td style="height: 21px">
                                                                </td>
                                                            </tr>
                                                              <tr>
                                                                <td  class="textbold" colspan="6" align="center" valign="TOP">                                                                
                                                                </td>
                                                            </tr>
                                                            
                                                            <tr>
                                                                <td class="textbold" style="height: 21px; width: 129px;">
                                                                </td>
                                                                <td class="textbold" style="width: 57px; height: 21px">
                                                                </td>
                                                                <td class="textbold" style="width: 137px; height: 21px">
                                                                    Unit Cost (LK)</td>
                                                                <td class="textbold" style="width: 192px; height: 21px">
                                                                    <asp:TextBox ID="txtLKUnitCost" runat="server" CssClass="textboxgrey" ReadOnly ="true" MaxLength="8" TabIndex="2"
                                                                        Width="208px"></asp:TextBox></td>
                                                                <td style="width: 33px; height: 21px">
                                                                </td>
                                                                <td style="height: 21px">
                                                                </td>
                                                            </tr>
                                                              <tr>
                                                                <td  class="textbold" colspan="6" align="center" valign="TOP">                                                                
                                                                </td>
                                                            </tr>
                                                            
                                                            <tr>
                                                                <td class="textbold" style="height: 21px; width: 129px;">
                                                                </td>
                                                                <td class="textbold" style="width: 57px; height: 21px">
                                                                </td>
                                                                <td class="textbold" style="width: 137px; height: 21px">
                                                                    Unit Cost (BD)</td>
                                                                <td class="textbold" style="width: 192px; height: 21px">
                                                                    <asp:TextBox ID="txtBDUnitCost" runat="server" CssClass="textboxgrey" ReadOnly ="true" MaxLength="8" TabIndex="2"
                                                                        Width="208px"></asp:TextBox></td>
                                                                <td style="width: 33px; height: 21px">
                                                                </td>
                                                                <td style="height: 21px">
                                                                </td>
                                                            </tr>
                                                              <tr>
                                                                <td  class="textbold" colspan="6" align="center" valign="TOP">                                                                
                                                                </td>
                                                            </tr>
                                                            
                                                            <tr>
                                                                <td class="textbold" style="height: 21px; width: 129px;">
                                                                </td>
                                                                <td class="textbold" style="width: 57px; height: 21px">
                                                                </td>
                                                                <td class="textbold" style="width: 137px; height: 21px">
                                                                    Unit Cost (BT)</td>
                                                                <td class="textbold" style="width: 192px; height: 21px">
                                                                    <asp:TextBox ID="txtBTUnitCost" runat="server" CssClass="textboxgrey" ReadOnly ="true" MaxLength="8" TabIndex="2"
                                                                        Width="208px"></asp:TextBox></td>
                                                                <td style="width: 33px; height: 21px">
                                                                </td>
                                                                <td style="height: 21px">
                                                                </td>
                                                            </tr>
                                                              <tr>
                                                                <td  class="textbold" colspan="6" align="center" valign="TOP">                                                                
                                                                </td>
                                                            </tr>
                                                            
                                                            <tr>
                                                                <td class="textbold" style="height: 21px; width: 129px;">
                                                                </td>
                                                                <td class="textbold" style="width: 57px; height: 21px">
                                                                </td>
                                                                <td class="textbold" style="width: 137px; height: 21px">
                                                                    Unit Cost (ML)</td>
                                                                <td class="textbold" style="width: 192px; height: 21px">
                                                                    <asp:TextBox ID="txtMLUnitCost" runat="server" CssClass="textboxgrey" ReadOnly ="true" MaxLength="8" TabIndex="2"
                                                                        Width="208px"></asp:TextBox></td>
                                                                <td style="width: 33px; height: 21px">
                                                                </td>
                                                                <td style="height: 21px">
                                                                </td>
                                                                
                                                            </tr>
                                                              <tr>
                                                                <td  class="textbold" colspan="6" align="center" valign="TOP">                                                                
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="textbold" style="width: 129px">
                                                                    &nbsp;</td>
                                                                <td class="textbold" style="width: 57px">
                                                                </td>
                                                                <td colspan="2" style="height: 14px" class="ErrorMsg">
                                                                    Field Marked * are Mandatory</td>                                                                
                                                                <td style="width: 33px">
                                                                    &nbsp;</td>
                                                                <td>
                                                                    </td>
                                                            </tr>                                                           
                                                            <tr>
                                                                <td class="textbold" style="height: 19px; width: 129px;">
                                                                    &nbsp;</td>
                                                                <td colspan="2" style="height: 19px" >
                                                                    </td>
                                                                <td style="width: 192px; height: 19px;">
                                                                    &nbsp;</td>
                                                                <td style="height: 19px; width: 33px;">
                                                                    &nbsp;</td>
                                                                <td style="height: 19px">
                                                                    &nbsp;</td>
                                                            </tr>
                                                            
                                                            <tr>
                                                                <td colspan="6" height="4">
                                                                    &nbsp;</td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="6" >
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
