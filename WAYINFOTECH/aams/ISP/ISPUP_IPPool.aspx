<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ISPUP_IPPool.aspx.vb" Inherits="ISP_ISPUP_IPPool" ValidateRequest="false"   %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>AAMS:Manage IP Pool</title>
    <link rel="stylesheet" type="text/css" media="all" href="../Calender/calendar-blue.css" title="win2k-cold-1" />   
    <link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../JavaScript/AAMS.js"></script>
<script type="text/javascript" language="javascript">

function DeleteFunction(CheckBoxObj)
    {   
         if (confirm("Are you sure you want to delete?")==true)
                  {        
                    document.getElementById("hdDele").value= CheckBoxObj ;   
                  }
                else
                {
                 document.getElementById("hdDele").value="";
                 return false;
                }
    } 
    
    
    function EditFunction(CheckBoxObj)
    {   
       document.getElementById("hdEdit").value= CheckBoxObj ;   
       
    } 
    
function validateManageIP()
{
var ProviderValue=document.getElementById("drpProvider").options[document.getElementById("drpProvider").selectedIndex].value;
var str=ProviderValue.split("|")[1];

        if (document.getElementById("drpProvider").selectedIndex=='0')
         {
            document.getElementById("lblError").innerHTML="Provider Name is Mandatory";
            document.getElementById("drpProvider").focus();
            return false;
           
         } 
         
             if(str=='True')
             {
                  if (document.getElementById("drpPOP").selectedIndex=='0')
                 {
                    document.getElementById("lblError").innerHTML="POP is Mandatory for this Provider";
                    document.getElementById("drpPOP").focus();
                    return false;
                  } 
             }
         
          if (  document.getElementById("txtIPAddress").value.trim()=='')
         {
            document.getElementById("lblError").innerHTML="IP Address is Mandatory";
            document.getElementById("txtIPAddress").focus();
            return false;
         } 
         
                 
           if (  document.getElementById("txtSubNtMask").value.trim()=='')
         {
            document.getElementById("lblError").innerHTML="Subnet Mask is Mandatory";
            document.getElementById("txtSubNtMask").focus();
            return false;
         } 
         
         
          if (  document.getElementById("txtRouterIP").value.trim()=='')
         {
            document.getElementById("lblError").innerHTML="Router IP is Mandatory";
            document.getElementById("txtRouterIP").focus();
            return false;
         } 
         
         
         if (  document.getElementById("txtNoTerminal").value.trim()=='')
         {
            document.getElementById("lblError").innerHTML="No. of Terminal is Mandatory";
            document.getElementById("txtNoTerminal").focus();
            return false;
         } 
         
         
         
         
        if (  document.getElementById("txtIPAddress").value!="")
         {
           if(isValidIPAddress(document.getElementById("txtIPAddress").value)==false)
            {
            document.getElementById("lblError").innerHTML="IP address is not valid.";
            document.getElementById("txtIPAddress").focus();
            return false;
            } 
         } 
         
         if (  document.getElementById("txtRouterIP").value!="")
         {
           if(isValidIPAddress(document.getElementById("txtRouterIP").value)==false)
            {
            document.getElementById("lblError").innerHTML="Router IP is not valid.";
            document.getElementById("txtRouterIP").focus();
            return false;
            } 
         }
         
         
         if (  document.getElementById("txtSubNtMask").value!="")
         {
           if(IsDataValid(document.getElementById("txtSubNtMask").value,3)==false)
            {
            document.getElementById("lblError").innerHTML="Subnet Mask is Numeric";
            document.getElementById("txtSubNtMask").focus();
            return false;
            } 
         } 
          
           if (  document.getElementById("txtNoTerminal").value!="")
         {
           if(IsDataValid(document.getElementById("txtNoTerminal").value,3)==false)
            {
            document.getElementById("lblError").innerHTML="No. of Terminal is Numeric";
            document.getElementById("txtNoTerminal").focus();
            return false;
            } 
         } 
        
         
}

function NewIPPool()
    {
        window.location.href="ISPUP_IPPool.aspx?Action=I";
        return false;
    }

    
</script>
</head>
<body>
   <form id="form1"  defaultfocus="drpProvider" runat="server">
        <table width="860px" align="left" class="border_rightred" style="height: 486px;">
            <tr>
                <td valign="top">
                    <table width="100%" align="left">
                        <tr>
                            <td valign="top" align="left">
                                <span class="menu">Travel Agency-></span><span class="sub_menu">IP Pool</span>
                            </td>
                        </tr>
                        <tr>
                            <td class="heading" align="center" valign="top">
                                Manage IP Pool
                            </td>
                        </tr>
                        <tr>
                            <td valign="top" style="height: 481px">
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td align="LEFT" class="redborder">
                                            <table width="100%" border="0" align="left" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td class="textbold" colspan="6" align="center">
                                                        &nbsp;<asp:Label ID="lblError" runat="server" CssClass="ErrorMsg"></asp:Label>
                                                    </td>
                                                </tr>
                                                 <tr>
                                                    <td width="6%" class="textbold" >
                                                        &nbsp;</td>
                                                    <td width="18%" class="textbold">
                                                      </td>
                                                    <td width="20%">
                                                      </td>
                                                    <td width="18%">
                                                      </td>
                                                    <td width="20%">
                                                        </td>
                                                    <td width="18%">
                                                      </td>
                                                </tr>
                                                <tr>
                                                    <td class="textbold" height="25px">
                                                        &nbsp;</td>
                                                    <td class="textbold">
                                                        Provider<span class="Mandatory" >*</span></td>
                                                    <td>
                                                    
                                                        <asp:DropDownList ID="drpProvider" CssClass="dropdownlist" Width="137px" runat="server" TabIndex="1">
                                                        </asp:DropDownList></td>
                                                    <td class="textbold">
                                                        Pop</td>
                                                    <td><asp:DropDownList ID="drpPOP" CssClass="dropdownlist" Width="137px" runat="server" TabIndex="1">
                                                    </asp:DropDownList></td>
                                                    <td class="center">
                                                        <asp:Button ID="btnSave" CssClass="button" runat="server" Text="Save" TabIndex="3" /></td>
                                                </tr>
                                                <tr>
                                                    <td class="textbold" height="25px">
                                                        &nbsp;</td>
                                                    <td class="textbold">
                                                        IP Address <span class="Mandatory" >*</span></td>
                                                    <td>
                                                        <asp:TextBox ID="txtIPAddress" runat="server" CssClass="textbox" MaxLength="40" TabIndex="1"></asp:TextBox></td>
                                                    <td class="textbold">
                                                        Subnet Mask <span class="Mandatory" >*</span></td>
                                                    <td>
                                                        <asp:TextBox ID="txtSubNtMask" runat="server" CssClass="textbox" MaxLength="40" TabIndex="1"></asp:TextBox></td>
                                                    <td class="center">
                                                        <asp:Button ID="btnNew" CssClass="button" runat="server" Text="New" TabIndex="3" /></td>
                                                </tr>
                                                <tr>
                                                    <td class="textbold" style="height: 25px">
                                                    </td>
                                                    <td class="textbold" style="height: 25px" >
                                                        Router IP <span class="Mandatory" >*</span></td>
                                                    <td style="height: 25px">
                                                        <asp:TextBox ID="txtRouterIP" runat="server" CssClass="textbox" MaxLength="40" TabIndex="1"></asp:TextBox></td>
                                                    <td  class="textbold" style="height: 25px">
                                                        No. of Terminal <span class="Mandatory" >*</span></td>
                                                    <td style="height: 25px">
                                                        <asp:TextBox ID="txtNoTerminal" runat="server" CssClass="textbox" MaxLength="40" TabIndex="1"></asp:TextBox></td>
                                                    <td class="center" style="height: 25px">
                                                        <asp:Button ID="btnReset" CssClass="button" runat="server" Text="Reset" TabIndex="3" /></td>
                                                </tr>
                                                <tr>
                                                    <td class="textbold" style="height: 25px">
                                                    </td>
                                                    <td class="textbold">
                                                    </td>
                                                    <td>
                                                    </td>
                                                    <td class="textbold">
                                                        Ace &nbsp;No.</td>
                                                    <td>
                                                        <asp:TextBox ID="txtAccNumber" runat="server" CssClass="textbox" MaxLength="40" TabIndex="1"></asp:TextBox></td>
                                                    <td class="center">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="textbold" style="height: 25px">
                                                    </td>
                                                    <td class="textbold">
                                                        Remarks</td>
                                                    <td colspan="3" rowspan="2">
                                                    <asp:TextBox ID="txtRemarks" runat="server" CssClass="textbox" Height="40px" TextMode="MultiLine" Width="448px" TabIndex="1"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="textbold" style="height: 25px">
                                                    </td>
                                                    <td class="textbold">
                                                    </td>
                                                    <td>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="textbold" style="height: 25px">
                                                    </td>
                                                    <td class="textbold">
                                                    </td>
                                                    <td>
                                                    </td>
                                                    <td class="textbold">
                                                    </td>
                                                    <td align="right" class="center">
                                                        &nbsp; &nbsp; &nbsp; &nbsp;
                                                        <asp:Button ID="btnAdd" CssClass="button" runat="server" Text="Add" TabIndex="2" /></td>
                                                    <td>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="textbold" style="height: 25px">
                                                    </td>
                                                    <td class="textbold">
                                                    </td>
                                                    <td>
                                                    </td>
                                                    <td class="textbold">
                                                    </td>
                                                    <td align="right" class="center">
                                                    </td>
                                                    <td>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="6" height="12">
                                                     <asp:GridView ID="grdvIPPool" runat="server" AllowSorting="true" HeaderStyle-ForeColor="white"  AutoGenerateColumns="False" TabIndex="4" Width="100%">
                                                 <Columns>
                                                           
                                                         
                                                            <asp:BoundField DataField="IPAddress" HeaderText="IP Address"  SortExpression="IPAddress"  >
                                                                <ItemStyle Wrap="True" Width="70px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="SubnetMask" HeaderText="Subnet Mask" HeaderStyle-Wrap="false"  SortExpression="SubnetMask"  >
                                                                <ItemStyle Wrap="True"  Width="70px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="RouterIP" HeaderText="Router IP" SortExpression="RouterIP" >
                                                                <ItemStyle Wrap="True"  Width="60px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="NumberOfTerminal" HeaderText="No. Of Terminal" HeaderStyle-Wrap="false" SortExpression="NumberOfTerminal">
                                                                <ItemStyle Width="90px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="AceNumber" HeaderText="Ace No." SortExpression="AceNumber" >
                                                                <ItemStyle Wrap="True" Width="70px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="Remarks" HeaderText="Remarks"  SortExpression="Remarks"  >
                                                                <ItemStyle Wrap="True" Width="190px" />
                                                            </asp:BoundField>
                                                           
                                                           
                                                            <asp:TemplateField HeaderText="Action" >
                                                            <ItemTemplate>
                                                                <asp:LinkButton  ID="linkEdit" Text="Edit" Runat="server" CssClass="LinkButtons" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "ProviderID") + "|" + DataBinder.Eval(Container.DataItem, "PopID")+ "|" + DataBinder.Eval(Container.DataItem, "ROWID") + "|" + DataBinder.Eval(Container.DataItem, "IPAddressID") %>'></asp:LinkButton>&nbsp;
                                                               <asp:LinkButton ID="btnDelete" runat="server" CommandName ="DeleteX" Text ="Delete" CssClass="LinkButtons"></asp:LinkButton>
                                                              
                                                              <asp:HiddenField ID="hdProviderID" runat="server" Value='<%#Eval("ProviderID")%>' />   
                                                                <asp:HiddenField ID="hdPopID" runat="server" Value='<%#Eval("PopID")%>' />   
                                                                <asp:HiddenField ID="hdROWID" runat="server" Value='<%#Eval("ROWID")%>' />   
                                                                <asp:HiddenField ID="hdIPAddressID" runat="server" Value='<%#Eval("IPAddressID")%>' /> 
                                                             </ItemTemplate>
                                                                <ItemStyle Wrap="False" />
                                                                <HeaderStyle Width="130px" />
                                                           </asp:TemplateField>
                                                                                                        
                                                 
                                                 </Columns>
                                                    <AlternatingRowStyle CssClass="lightblue" />
                                                    <RowStyle CssClass="textbold" />
                                                    <HeaderStyle CssClass="Gridheading" ForeColor="White" />
                                                    
                                                 </asp:GridView>
                                                    </td>
                                                </tr>
                                                
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                                <asp:HiddenField ID="hdFinalXml" runat="server" />
                                <asp:HiddenField ID="hdDele" runat="server" />
                                <asp:HiddenField ID="hdEdit" runat="server" />
                                <asp:HiddenField ID="TempEdit" runat="server" />
                                <asp:HiddenField ID="hdProviderName" runat="server" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
