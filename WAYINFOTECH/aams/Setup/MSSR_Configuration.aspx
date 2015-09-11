<%@ Page Language="VB" AutoEventWireup="false" CodeFile="MSSR_Configuration.aspx.vb" Inherits="Setup_MSSR_Configuration" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>AAMS:Configuration</title>
     <link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />
    <script src="../JavaScript/AAMS.js" type="text/javascript"> </script>
    
    <script language ="javascript" type="text/javascript">
    function ValidateConfiguration()
    {
    if(window.document.getElementById("drpConfigCriteria").selectedIndex=='0')
    {
        window.document.getElementById("lblError").innerHTML="Configuration Category is Mandatory";
        return false;
    }
    
           
             for(intcnt=1;intcnt<=document.getElementById('GrdvConfig').rows.length-1;intcnt++)
               {  
                if (document.getElementById('GrdvConfig').rows[intcnt].cells[1].children.length == "1")
                {
                    if (document.getElementById('GrdvConfig').rows[intcnt].cells[1].children[0].type=="text")
                    {                 
                       if(document.getElementById('GrdvConfig').rows[intcnt].cells[1].children[0].value.trim()=='')
                       {
                        document.getElementById('GrdvConfig').rows[intcnt].cells[1].children[0].focus();
                        document.getElementById("lblError").innerHTML="Config. Value is mandatory.";
                        return false;
                       }
                    } 
                }
             }
    }
    
    function ValidateSearch()
    {
     if(window.document.getElementById("drpConfigCriteria").selectedIndex=='0')
    {
        window.document.getElementById("lblError").innerHTML="Configuration Category is Mandatory";
        return false;
    }
    
    }
    </script>
   
</head>
<body >
    <form id="frmConfiguration" defaultbutton="btnSearch" defaultfocus="drpConfigCriteria" runat="server" accesskey="A">
        <div>
            <table width="860px" align="left" height="486px" class="border_rightred">
                <tr>
                    <td valign="top">
                        <table width="100%" align="left">
                            <tr>
                                <td valign="top" align="left">
                                    <span class="menu">Setup-></span><span class="sub_menu">Configuration</span>
                                </td>
                            </tr>
                         
                            <tr>
                                <td class="heading" align="center" valign="top" style="height: 10px">
                                    Configuration
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                        <tr>
                                            <td width="100%" class="redborder">
                                                <table align="left" border="0" cellpadding="2" cellspacing="0" width="100%">
                                                    <tr>
                                                        <td class="textbold" style="height: 28px; width: 100%" colspan="4" valign="top">
                                                            <table width="100%" border="0" align="left" cellpadding="1" cellspacing="1">
                                                                <tr>
                                                                    <td class="textbold" colspan="4" align="center" valign="TOP">
                                                                        <asp:Label ID="lblError" runat="server" CssClass="ErrorMsg" ></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="textbold" style="width: 10%">
                                                                    </td>
                                                                    <td align="left" style="width: 20%">
                                                                        Config. Category<span class="Mandatory">*</span></td>
                                                                    <td style="width: 20%;">
                                                                        <asp:DropDownList  onkeyup="gotop(this.id)"  ID="drpConfigCriteria" runat="server" CssClass="dropdown"
                                                                            Width="182px" AutoPostBack="True">
                                                                        </asp:DropDownList></td>
                                                                    <td style="width: 40%;" class="center">
                                                                        <asp:Button ID="btnSearch" runat="server" CssClass="button" Text="Search" TabIndex="1" /></td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="textbold" colspan="4" align="center" valign="TOP">
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="textbold" style="height: 24px">
                                                                    </td>
                                                                    <td style="height: 24px" align="left">
                                                                        </td>
                                                                    <td class="textbold" style="height: 24px; width: 108px;">
                                                                        </td>
                                                                    <td style="height: 24px" class="center">
                                                                        <asp:Button ID="btnSave" runat="server" CssClass="button" Text="Save" TabIndex="1" /></td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="textbold" colspan="4" align="center" valign="TOP">
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="textbold">
                                                                    </td>
                                                                    <td style="height: 22px" align="left">
                                                                        </td>
                                                                    <td style="height: 22px; width: 108px;">
                                                                        </td>
                                                                    <td style="height: 22px" class="center">
                                                                        <asp:Button ID="btnReset" runat="server" CssClass="button" Text="Reset" TabIndex="1" />
                                                                    </td>
                                                                </tr>
                                                                 <tr>
                                                                    <td class="ErrorMsg" colspan="4" align="left" valign="TOP" >Field Marked * are Mandatory
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="4" style="height: 4px">
                                                                    </td>
                                                                </tr>
                                                                <tr >
                                                                    <td colspan="4" style="width:100%">
                                                                        <asp:GridView ID="GrdvConfig" HeaderStyle-Wrap="false"  HeaderStyle-ForeColor="white" runat="server" AutoGenerateColumns="False"
                                                                            Width="100%" AllowSorting="True" TabIndex="2" >
                                                                            <Columns>
                                                                                <asp:TemplateField HeaderText="Field" SortExpression="FIELD_NAME">
                                                                                    <ItemTemplate>
                                                                                    <asp:Label ID="lblFieldName" runat="server" Text='<%#Eval("FIELD_NAME")%>'></asp:Label>
                                                                                    <asp:HiddenField ID="hdCCA_ID" runat="server" Value='<%#Eval("CCA_ID")%>' />
                                                                                    </ItemTemplate>
                                                                                    <ItemStyle Width="100px" />
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Value" SortExpression="FIELD_VALUE">
                                                                                    <ItemTemplate>
                                                                                    <asp:TextBox ID="txtValue" runat="server" Text='<%#Eval("FIELD_VALUE")%>'></asp:TextBox>
                                                                                       </ItemTemplate>
                                                                                    <ItemStyle Width="150px" Wrap="True" />
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Active">
                                                                                    <ItemTemplate>
                                                                                        <asp:CheckBox ID="chkActive" runat="server" Checked='<%#Eval("Active") %>' />
                                                                                    </ItemTemplate>
                                                                                    <ItemStyle Width="30px" Wrap="True" />
                                                                                </asp:TemplateField>
                                                                                
                                                                                <asp:TemplateField HeaderText="Remarks" SortExpression="Remarks" >
                                                                                         <ItemTemplate>
                                                                                        <asp:TextBox ID="txtRemarks" Width="300px" TextMode="MultiLine" Height="50px" Text='<%#Eval("Remarks") %>' runat="server" ></asp:TextBox>
                                                                                        </ItemTemplate>
                                                                                    <ItemStyle Wrap="False" Width="300px" />
                                                                                </asp:TemplateField>
                                                                            </Columns>
                                                                            <HeaderStyle CssClass="Gridheading" HorizontalAlign="Left" ForeColor="White" Wrap="False" />
                                                                            <RowStyle CssClass="ItemColor" />
                                                                            <AlternatingRowStyle CssClass="lightblue" />
                                                                        </asp:GridView>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="4" style="height: 12px">
                                                                    </td>
                                                                </tr>
                                                                
                                                                <!-- code for paging----->
                                            <tr>   
                                                    <td valign ="top" colspan="4"  >
                                                    <asp:Panel ID="pnlPaging" runat="server" Visible="false" Width="800px">
                                                      <table border="0" cellpadding="0" cellspacing="0" style="width: 800px">
                                                                      <tr class="paddingtop paddingbottom">                                                                                                                                                
                                                                          <td style="width: 243px; height: 29px;" class="left" nowrap="nowrap"><span  class="textbold"><b>&nbsp;No. of records found</b></span>&nbsp;&nbsp;<asp:TextBox ID="txtTotalRecordCount" runat ="server"  Width="105px" CssClass="textboxgrey" ></asp:TextBox></td>
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
                                                            </table>
                                                            <br />
                                                        </td>
                                                        <td width="18%" rowspan="1" valign="top">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="textbold" colspan="5">
                                                        </td>
                                                    </tr>
                                                    <tr style="font-size: 12pt; font-family: Times New Roman">
                                                        <td colspan="6" height="12">
                                                            &nbsp;</td>
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
                    <td valign="top" style="height: 6px">
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
