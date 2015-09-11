<%@ Page Language="VB" AutoEventWireup="false" CodeFile="MSSR_State.aspx.vb" Inherits="Setup_MSSR_State"  %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>AAMS: State</title>
    <link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />

    <script src="../JavaScript/AAMS.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript">
    function StateReset()
    {
        
        document.getElementById("txtStateName").value="";       
        document.getElementById("drpCountry").selectedIndex=0;
        //return false;
    }
    function EditFunction(CheckBoxObj)
    {           
        window.location.href="MSUP_State.aspx?Action=U|"+CheckBoxObj;       
        return false;
    }
    function DeleteFunction(CheckBoxObj)
    {   
        if (confirm("Are you sure you want to delete?")==true)
        {   
           // window.location.href="MSSR_State.aspx?Action=D|"+ CheckBoxObj +"|"+ document.getElementById("<%=txtStateName.ClientID%>").value +"|"+ document.getElementById("drpCountry").selectedIndex;                   
            //return false;
         document.getElementById('hdStateID').value = CheckBoxObj;
               document.forms['frmManageState'].submit();     
        }
        else
        {
            return false;
        }
    }
    function NewFunction()
    {   
        window.location.href="MSUP_State.aspx?Action=I";       
        return false;
    }
    </script>

</head>
<body>
    <form id="frmManageState" runat="server"  defaultfocus="txtStateName" defaultbutton ="btnSearch">
        <div>
            <table width="860px" align="left"  class="border_rightred">
                <tr>
                    <td valign="top">
                        <table width="99%" align="left">
                            <tr>
                                <td valign="top" align="left">
                                    <span class="menu">Setup-></span><span class="sub_menu">State</span>
                                </td>
                            </tr>
                           
                                <tr>
                                    <td class="heading" align="center" valign="top" style="height: 10px">
                                        Search State</td>
                                </tr>
                                <tr>
                                    <td>
                                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                            <tr>
                                                <td width="99%" class="redborder">
                                                    <table align="left" border="0" cellpadding="2" cellspacing="0" width="99%">
                                                        <tr>
                                                            <td class="textbold" >
                                                                <table width="98%" border="0" align="left" cellpadding="0" cellspacing="0">
                                                                   
                                                                    <tr>
                                                                        <td align="center" class="textbold" colspan="4" height="20px" valign="top">
                                                                            <asp:Label ID="lblError" runat="server" CssClass="ErrorMsg"></asp:Label></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="textbold" style="width: 10%">
                                                                        </td>
                                                                        <td align="left" style="width: 20%">
                                                                            State Name</td>
                                                                        <td class="textbold" style="width: 25%">
                                                                            <asp:TextBox ID="txtStateName" runat="server" CssClass="textfield" TabIndex="1" Width="174px" ></asp:TextBox>
                                                                        </td>
                                                                        <td style="width: 45%" class="center" >
                                                                            <asp:Button ID="btnSearch" runat="server" CssClass="button" Text="Search" TabIndex="3" AccessKey="A" /></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="textbold">
                                                                            &nbsp;</td>
                                                                        <td align="left">
                                                                            Country</td>
                                                                        <td class="textbold">
                                                                            <asp:DropDownList  onkeyup="gotop(this.id)"  ID="drpCountry" runat="server" CssClass="dropdown" TabIndex="2" Width="178px"
                                                                               >
                                                                            </asp:DropDownList></td>
                                                                        <td class="center">
                                                                            <asp:Button ID="btnNew" runat="server" CssClass="button" AccessKey="N" Text="New" TabIndex="4" /></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="textbold">
                                                                            </td>
                                                                        <td align="left">
                                                                        </td>
                                                                        <td>
                                                                        </td>
                                                                        <td class="center">
                                                                            <asp:Button ID="btnReset" runat="server" CssClass="button" Text="Reset" TabIndex="5"  AccessKey="R"/></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="textbold" style="height: 15px">
                                                                        </td>
                                                                        <td align="left" style="height: 15px">
                                                                        </td>
                                                                        <td style="height: 15px">
                                                                        <asp:HiddenField ID="hdStateID" runat="server" />
                                                                        </td>
                                                                        <td class="center" style="height: 15px">
                                                                            <asp:Button ID="btnExport" runat="server" CssClass="button" Text="Export" TabIndex="5"  AccessKey="E"/></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="textbold">
                                                                        </td>
                                                                        <td align="left">
                                                                        </td>
                                                                        <td>
                                                                        </td>
                                                                        <td class="gap">
                                                                        </td>
                                                                    </tr>
                                                                   
                                                                    <tr>
                                                                        <td colspan="4" style="width:100%" align="center">
                                                                            <asp:GridView ID="grdState" BorderWidth="1" AllowSorting="true" HeaderStyle-ForeColor="white" BorderColor="#d4d0c8" runat="server"
                                                                                AutoGenerateColumns="False" Width="100%" EnableViewState="false">
                                                                                <Columns>
                                                                                    <asp:TemplateField HeaderText="State Name" SortExpression="State_Name">
                                                                                        <itemtemplate>
                                                                                            <%#Eval("State_Name")%>
                                                                                            <asp:HiddenField ID="hdStateId" runat="server"  Value='<%#Eval("StateID")%>' />
                                                                                        </itemtemplate>
                                                                                        <itemstyle horizontalalign="Left" />
                                                                                        <headerstyle horizontalalign="Left" />
                                                                                    </asp:TemplateField>
                                                                                    <asp:BoundField DataField="Country_Name" SortExpression="Country_Name" HeaderText="Country Name" >
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <ItemStyle HorizontalAlign="Left" />
                                                                                    </asp:BoundField>
                                                                                    <asp:TemplateField HeaderText="Action">
                                                                                        <ItemStyle HorizontalAlign="Center" />
                                                                                        <ItemTemplate>
                                                                                            <a href="#" class="LinkButtons" id="linkEdit" runat="server">Edit</a> <a href="#" class="LinkButtons" id="linkDelete" runat="server">
                                                                                                Delete</a>
                                                                                        </ItemTemplate>
                                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                                    </asp:TemplateField>
                                                                                </Columns>
                                                                                <AlternatingRowStyle CssClass="lightblue" />
                                                                                <HeaderStyle CssClass="Gridheading" HorizontalAlign="Left" ForeColor="White" />
                                                                                <RowStyle CssClass="textbold" />
                                                                            </asp:GridView>
                                                                        </td>
                                                                    </tr>
                                                                    
                                                                      <!-- code for paging----->
                                            <tr>   
                                                    <td valign ="top" colspan="4"  >
                                                    <asp:Panel ID="pnlPaging" runat="server" Visible="false" Width="100%">
                                                      <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                                                                      <tr class="paddingtop paddingbottom">                                                                                                                                                
                                                                          <td style="width: 243px; height: 29px;" class="left" nowrap="nowrap"><span  class="textbold"><b>&nbsp;No. of records found</b></span>&nbsp;&nbsp;<asp:TextBox ID="txtTotalRecordCount" runat ="server"  Width="105px" CssClass="textboxgrey" ReadOnly="true" ></asp:TextBox></td>
                                                                          <td style="width: 200px; height: 29px;" class="right">                                                                             
                                                                              <asp:LinkButton ID="lnkPrev" CssClass="LinkButtons" runat="server" CommandName="Prev"><< Prev</asp:LinkButton></td>
                                                                          <td style="width: 356px; height: 29px;" class="center">
                                                                              <span class ="textbold" ><b>Page No</b></span>&nbsp;&nbsp;<asp:DropDownList ID="ddlPageNumber" Width="70px" CssClass="dropdownlist" runat="server" AutoPostBack="True" >
                                                                              </asp:DropDownList></td>
                                                                          <td style="width: 187px; height: 29px;" class="left">
                                                                              <asp:LinkButton ID="lnkNext" runat="server" CssClass="LinkButtons" CommandName="Next">Next >></asp:LinkButton></td>
                                                                      </tr>
                                                                  </table></asp:Panel>
                                                    </td>
                                                  
                                                    
                                                    
                                                </tr>
                                                
            <!-- code for paging----->
            
                                                                    <tr>
                                                                        <td colspan="4" height="12">
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
                        <asp:HiddenField ID="HiddenField1" runat="server" />
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
