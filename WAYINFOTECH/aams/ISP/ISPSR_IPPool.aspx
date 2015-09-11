<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ISPSR_IPPool.aspx.vb" Inherits="ISP_ISPSR_IPPool" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>AAMS:IP Pool</title>
    <link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" media="all" href="../Calender/calendar-blue.css" title="win2k-cold-1" />   
    <script type="text/javascript" src="../JavaScript/AAMS.js"></script>
<!-- import the calendar script -->
<script type="text/javascript" src="../Calender/calendar.js"></script>
<!-- import the language module -->
<script type="text/javascript" src="../Calender/lang/calendar-en.js"></script>
<!-- import the calendar setup module -->
<script type="text/javascript" src="../Calender/calendar-setup.js"></script>
<script type="text/javascript" language="javascript">
function ShowHistory(strLcode)
{

             var type="../Popup/PUSR_ISPPoolHistory.aspx?LCODES="+strLcode;         
   	         window.open(type,"aa","height=600,width=905,top=30,left=20,scrollbars=1,status=1");	
             return false;
}
    function NewIPPool()
    {
        window.location.href="ISPUP_IPPool.aspx?Action=I";
        return false;
    }
    
    function EditFunction(CheckBoxObj,ip)
    {           
          window.location.href="ISPUP_IPPool.aspx?Action=U&IPAddressID="+CheckBoxObj+"&IPAddressIDOrig="+ip;               
          return false;
    }
    
      function SelectFunction(str3)
    {   
        //alert(str3);
        var pos=str3.split('|'); 
          
        if (window.opener.document.forms['form1']['txtIPAddress']!=null)
        {
        window.opener.document.forms['form1']['hdIPAddressID'].value=pos[0];
        window.opener.document.forms['form1']['txtIPAddress'].value=pos[1];
        window.close();
        }
     }   
  
   
   function DeleteFunction(CheckBoxObj)
    {   
         if (confirm("Are you sure you want to delete?")==true)
                  {        
                    document.getElementById("hdDeleteISPID").value= CheckBoxObj ;   
                  }
                else
                {
                 document.getElementById("hdDeleteISPID").value="";
                 return false;
                }
    } 
    
    
    
</script>
</head>
<body>
    <form id="form1"  defaultbutton="btnSearch" defaultfocus="drpProviders" runat="server">
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
                                Search IP Pool
                            </td>
                        </tr>
                        <tr>
                            <td valign="top">
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
                                                        Provider</td>
                                                    <td colspan="2">
                                                    <asp:DropDownList ID="drpProviders" runat="server" CssClass="dropdown" Width="320px" TabIndex="1" ></asp:DropDownList>
                                                        </td>
                                                    <td class="center">
                                                        <asp:Button ID="btnSearch" CssClass="button" runat="server" Text="Search" TabIndex="2" />
                                                        </td>
                                                    <td>
                                                        </td>
                                                </tr>
                                                <tr>
                                                    <td class="textbold" height="25px">
                                                        &nbsp;</td>
                                                    <td class="textbold">
                                                        Pop</td>
                                                    <td colspan="2">
                                                    <asp:DropDownList ID="drpPop" runat="server" CssClass="dropdown" Width="320px" TabIndex="1" ></asp:DropDownList>
                                                    
                                                        </td>
                                                    <td class="center">
                                                        <asp:Button ID="btnNew" CssClass="button" runat="server" Text="New" TabIndex="2" /></td>
                                                    <td>
                                                        </td>
                                                </tr>
                                                <tr>
                                                    <td class="textbold" style="height: 25px">
                                                    </td>
                                                    <td class="textbold" >
                                                        IP
                                                                        Address</td>
                                                    <td colspan="2">
                                                        <asp:TextBox ID="txtIPAddress" runat="server" CssClass="textbox" MaxLength="40" Width="312px" TabIndex="1"></asp:TextBox></td>
                                                    <td class="center">
                                                        <asp:Button ID="btnExport" CssClass="button" runat="server" Text="Export" TabIndex="2" /></td>
                                                    <td>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="textbold" style="height: 25px">
                                                    </td>
                                                    <td class="textbold" style="height: 25px">
                                                        Agency Name</td>
                                                    <td colspan="2" style="height: 25px">
                                                        <asp:TextBox ID="txtAgencyName" runat="server" CssClass="textbox" MaxLength="40" Width="312px" TabIndex="1"></asp:TextBox></td>
                                                    <td style="height: 25px" class="center"><asp:Button ID="btnReset" CssClass="button" runat="server" Text="Reset" TabIndex="2" /></td>
                                                    <td style="height: 25px">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="textbold" style="height: 25px;">
                                                    </td>
                                                    <td class="textbold">
                                                        Unallocated</td>
                                                    <td colspan="2">
                                                    <asp:CheckBox ID="chkUnallocated" runat="server" TabIndex="1" />
                                                    </td>
                                                    <td>
                                                    </td>
                                                    <td>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="6" height="4">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="6" height="4">
                                                       <asp:GridView EnableViewState="false" ID="grdvIPPool" runat="server"  AutoGenerateColumns="False" TabIndex="3" Width="100%" AllowSorting="True"    >
                                                 <Columns>
                                                           
                                                          
                                                            <asp:BoundField DataField="ProviderName" HeaderText="Provider Name"  SortExpression="ProviderName"  >
                                                                <ItemStyle Wrap="True" Width="120px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="PopName" HeaderText="POP"  SortExpression="PopName"  >
                                                                <ItemStyle Wrap="True"  Width="120px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="IPAddress" HeaderText="IP Address" SortExpression="IPAddress" >
                                                                <ItemStyle Wrap="True"  Width="60px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="SubnetMask" HeaderText="Subnet Mask" SortExpression="SubnetMask">
                                                                <ItemStyle Width="120px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="RouterIP" HeaderText="Router IP" SortExpression="RouterIP" >
                                                                <ItemStyle Wrap="True" Width="100px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="NumberOfTerminal" HeaderText="No. of Terminal"  SortExpression="NumberOfTerminal"  >
                                                                <ItemStyle Wrap="True" Width="100px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="OfficeID" HeaderText="OfficeID" SortExpression="OfficeID"  >
                                                                <ItemStyle Wrap="True"  Width="100px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="AgencyName" HeaderText="Agency Name" Visible="false" SortExpression="AgencyName" >
                                                                <ItemStyle Wrap="True" Width="150px" />
                                                            </asp:BoundField>
                                                           
                                                            <asp:BoundField DataField="AceNumber" HeaderText="Ace No."  SortExpression="AceNumber" >
                                                                <ItemStyle Wrap="True" Width="100px" />
                                                            </asp:BoundField>
                                                            
                                                             <asp:BoundField DataField="Remarks" HeaderText="Remarks"  SortExpression="Remarks" >
                                                                <ItemStyle Wrap="True" Width="190px" />
                                                            </asp:BoundField>
                                                            
                                                            <asp:TemplateField HeaderText="Action" >
                                                            <ItemTemplate>
                                                                <asp:LinkButton   ID="lnkSelect" Runat="server" CssClass="LinkButtons" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "IPAddressID") + "|" + DataBinder.Eval(Container.DataItem, "IPAddress") %>'>Select</asp:LinkButton>&nbsp;
                                                                <a href="#" class="LinkButtons" id="linkEdit" runat="server">Edit</a>&nbsp;
                                                                <asp:LinkButton ID="btnDelete" runat="server" CommandName ="DeleteX" Text ="Delete" CssClass="LinkButtons"></asp:LinkButton>
                                                                <%--<a href="#" class="LinkButtons" id="lnkHistory" runat="server">History</a>--%>
                                                <asp:LinkButton ID="lnkHistory" runat="server" CommandName ="DeleteX" Text ="History" CssClass="LinkButtons"></asp:LinkButton>
                                                                <asp:HiddenField ID="hdProviderID" runat="server" Value='<%#Eval("ProviderID")%>' />
                                                                <asp:HiddenField ID="hdIPAddressID" runat="server" Value='<%#Eval("IPAddressID")%>' /> 
                                                                <asp:HiddenField ID="hdLcode" runat="server" Value='<%#Eval("Lcode")%>' />   
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
                                                <tr>
                                                    <td colspan="6" height="12">
                                                    </td>
                                                </tr>
                                                  <tr>
                                            <td colspan="6">
                                            <asp:Panel ID="pnlPaging" runat="server" Visible="false" Width="100%">
                                                      <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                      <tr class="paddingtop paddingbottom"  >                                                                                                                                                
                                                                          <td style="width: 30%" class="left"><span  class="textbold"><b>&nbsp;No. of records found</b></span>&nbsp;&nbsp;<asp:TextBox ID="txtTotalRecordCount" runat ="server"  Width="105px" CssClass="textboxgrey" ></asp:TextBox></td>
                                                                          <td style="width: 25%" class="right">                                                                             
                                                                              <asp:LinkButton ID="lnkPrev" CssClass="LinkButtons" runat="server" CommandName="Prev" TabIndex="4"><< Prev</asp:LinkButton></td>
                                                                          <td style="width: 20%" class="center">
                                                                              <span class ="textbold" ><b>Page No</b></span>&nbsp;&nbsp;<asp:DropDownList ID="ddlPageNumber" Width="70px" CssClass="dropdownlist" runat="server" AutoPostBack="True" TabIndex="4" >
                                                                              </asp:DropDownList></td>
                                                                          <td style="width: 25%" class="left">
                                                                              <asp:LinkButton ID="lnkNext" runat="server" CssClass="LinkButtons" CommandName="Next" TabIndex="4">Next >></asp:LinkButton></td>
                                                                      </tr>
                                                                  </table></asp:Panel>
                                                <asp:HiddenField ID="hdDeleteISPID" runat="server" />
                                                <asp:HiddenField ID="hdDelMsg" runat="server" />
                                                <asp:TextBox ID="txtRecordOnCurrentPage" runat="server" CssClass="textboxgrey" Visible="false"
                                                    Width="73px"></asp:TextBox></td>
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
