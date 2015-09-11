<%@ Page Language="VB"  AutoEventWireup="false" CodeFile="TASR_OnlineStatus.aspx.vb" Inherits="TravelAgency_TASR_OnlineStatus" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Online Status</title>
     <script src="../JavaScript/AAMS.js" type="text/javascript"></script>
    <script type="text/javascript" language ="javascript" >      
     
      function  NewMSUPOnlineStatus()
       {        
           window.location="TAUP_OnlineStatus.aspx?Action=I";
           return false;
       }
        function DeleteFunction(CheckBoxObj)
          {   
            if (confirm("Are you sure you want to delete?")==true)
            {       
               // alert(document.getElementById("<%=txtOnlineStatus.ClientID%>").value);   
                window.location.href="TASR_OnlineStatus.aspx?Action=D|"+CheckBoxObj + "|"+ document.getElementById("<%=txtStatusCode.ClientID%>").value+ "|"+ document.getElementById("<%=txtOnlineStatus.ClientID%>").value;           
                return false;
            }
        }
      function EditFunction(CheckBoxObj)
    {
            
              window.location ="TAUP_OnlineStatus.aspx?Action=U&StatusCode=" + CheckBoxObj;  
      
          return false;
    }   
     
    function OnlineStatusReset()
    {
        document.getElementById("txtStatusCode").value="";
        document.getElementById("txtOnlineStatus").value="";      
        document.getElementById("lblError").innerHTML="";    
        if ( document.getElementById("gvOnlineStatus")!=null)       
        document.getElementById("gvOnlineStatus").style.display ="none"; 
        document.getElementById("txtStatusCode").focus();  
        return false;
    }
   function OnlineStatusMandatory()
    {
        if (document.getElementById("txtStatusCode").value!="")
         {
           if(IsDataValid(document.getElementById("txtStatusCode").value,7)==false)
            {
            document.getElementById("lblError").innerHTML="Status Code is not valid.";
            document.getElementById("txtStatusCode").focus();
            return false;
            } 
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
      
         
         
         return true;
     }
  
    </script>
    <link href="../CSS/AAMS.css" type="text/css" rel="stylesheet" />
</head>
<body bgcolor="ffffff" >
    <form id="frmTravelAgency" runat="server"  defaultfocus ="txtStatusCode"   defaultbutton="btnSearch" >
    
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
                                Search Online Status</td>
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
                                                                <td class="textbold" style="width: 154px; height: 22px;">
                                                                    Status Code</td>
                                                                <td style="width: 187px; height: 22px" >
                                                                   <asp:TextBox ID="txtStatusCode" runat ="server" CssClass ="textbox" Width="208px" MaxLength="6" EnableViewState="False" TabIndex="1"></asp:TextBox>
                                                                    </td>
                                                                <td style="height: 22px; width: 140px;">
                                                                    </td>
                                                                <td width="30%" style="height: 22px">
                                                                    <asp:Button ID="btnSearch" CssClass="button" runat="server" Text="Search" TabIndex="3" AccessKey="A" /></td>
                                                            </tr>
                                                             <tr>
                                                                <td  class="textbold" colspan="6" style="height:4px;"  align="center" valign="TOP">                                                                
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="textbold" style="height: 22px">
                                                                    &nbsp;</td>
                                                                <td class="textbold" style="width: 57px; height: 22px;">
                                                                    </td>
                                                                <td class="textbold" style="width: 154px; height: 22px;">
                                                                    Online Status</td>
                                                                <td class="textbold" style="width: 187px; height: 22px;">
                                                                    <asp:TextBox ID="txtOnlineStatus" runat="server" CssClass="textbox" MaxLength="30" Width="208px" EnableViewState="False" TabIndex="2"></asp:TextBox></td>
                                                                <td style="height: 22px; width: 140px;">
                                                                </td>
                                                                <td style="height: 22px">
                                                                    <asp:Button ID="btnNew" CssClass="button" runat="server" Text="New" TabIndex="4" AccessKey="N" /></td>
                                                            </tr>
                                                             <tr>
                                                                <td  class="textbold" colspan="6" style="height:4px;" align="center" valign="TOP">                                                                
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="textbold">
                                                                    &nbsp;</td>
                                                                <td class="textbold" style="width: 57px">
                                                                </td>
                                                                <td style="width: 154px" class="textbold">
                                                                    Segment Expected</td>
                                                                <td style="width: 187px">
                                                                    <asp:TextBox ID="txtSegExpected" runat="server" CssClass="textbox" EnableViewState="False"
                                                                        MaxLength="5" TabIndex="2" Width="208px"></asp:TextBox>&nbsp;</td>
                                                                <td style="width: 140px">
                                                                    &nbsp;</td>
                                                                <td>
                                                                    <asp:Button ID="btnExport" runat="server" CssClass="button" TabIndex="5" Text="Export" AccessKey="E" /></td>
                                                            </tr>                                                           
                                                            <tr>
                                                                <td  class="textbold" colspan="6" style="height:4px;" align="center" valign="TOP">                                                                
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="textbold">
                                                                </td>
                                                                <td class="textbold" style="width: 57px">
                                                                </td>
                                                                <td style="width: 154px">
                                                                    <input id="hdRecordOnCurrentPage" runat="server" style="width: 6px" type="hidden" /></td>
                                                                <td style="width: 187px">
                                                                </td>
                                                                <td style="width: 140px">
                                                                </td>
                                                                <td>
                                                                    <asp:Button ID="btnReset" CssClass="button" runat="server" Text="Reset" TabIndex="6" AccessKey="R" /></td>
                                                            </tr>
                                                            <tr>
                                                                <td class="textbold">
                                                                    &nbsp;</td>
                                                                <td colspan="2" >
                                                                    </td>
                                                                <td style="width: 187px">
                                                                    &nbsp;</td>
                                                                <td style="width: 140px">
                                                                    &nbsp;</td>
                                                                <td>
                                                                    &nbsp;</td>
                                                            </tr>
                                                            
                                                            <tr>
                                                                <td colspan="6" height="4">
                                                                    <asp:GridView ID="gvOnlineStatus" runat="server"  AutoGenerateColumns="False" TabIndex="7" Width="100%" EnableViewState="False" AllowSorting="True"    >
                                                                    <Columns>
                                                                         <asp:TemplateField HeaderText="Status Code" SortExpression="StatusCode">
                                                                            <itemtemplate>
                                                                                <%#Eval("StatusCode")%>
                                                                                <asp:HiddenField ID="hdStatusCode" runat="server" Value='<%#Eval("StatusCode")%>' />
                                                                            </itemtemplate>
                                                                            <itemstyle horizontalalign="Left" />
                                                                            <headerstyle horizontalalign="Left" />
                                                                          </asp:TemplateField>   
                                                                     <asp:BoundField DataField="OnlineStatus" HeaderText="Online Status" SortExpression="OnlineStatus" />
                                                                      <asp:BoundField DataField="SegExpected" HeaderText="Segment Expected " SortExpression="SegExpected" />
                                                                      <asp:BoundField DataField="UnitCost" HeaderText="Unit Cost (IN) " SortExpression="UnitCost"  ItemStyle-HorizontalAlign="right"  />
                                                                      <asp:BoundField DataField="NPUnitCost" HeaderText="Unit Cost (NP)" SortExpression="NPUnitCost" ItemStyle-HorizontalAlign="right"  />
                                                                      <asp:BoundField DataField="LKUnitCost" HeaderText="Unit Cost (LK)" SortExpression="LKUnitCost"   ItemStyle-HorizontalAlign="right" />
                                                                      <asp:BoundField DataField="BDUnitCost" HeaderText="Unit Cost (BD)" SortExpression="BDUnitCost"   ItemStyle-HorizontalAlign="right" />
                                                                      <asp:BoundField DataField="BTUnitCost" HeaderText="Unit Cost (BT)" SortExpression="BTUnitCost"   ItemStyle-HorizontalAlign="right"  />
                                                                      <asp:BoundField DataField="MLUnitCost" HeaderText="Unit Cost (ML)" SortExpression="MLUnitCost"  ItemStyle-HorizontalAlign="right"  />
                                                                      
                                                                    <asp:TemplateField>
                                                                    <HeaderTemplate>
                                                                        <asp:Label ID="lblAction" runat="server" Text="Action"></asp:Label>
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                    <a href="#" class="LinkButtons" id="linkEdit" runat="server">Edit</a> <a href="#" class="LinkButtons" id="linkDelete" runat="server">
                                                                                        Delete</a>
                                                                    </ItemTemplate>
                                                                    <ItemStyle Width="10%" CssClass="ItemColor" />
                                                                    <HeaderStyle Wrap="False" CssClass="Gridheading" />
                                                                </asp:TemplateField>
                                                 
                                                 
                                                 </Columns>
                                                    <AlternatingRowStyle CssClass="lightblue" />
                                                    <RowStyle CssClass="textbold" />
                                                    <HeaderStyle CssClass="Gridheading" ForeColor="White"/>
                                                    
                                                 </asp:GridView>
                                                                    
                                                                    </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="6" height="4">
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
    <!-- Code by Abhishek -->
    
  
    </form>
</body>
</html>
