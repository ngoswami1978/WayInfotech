<%@ page language="VB" autoeventwireup="false" inherits="Setup_MS_Style, App_Web_mssr_style.aspx.4cd3357d" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Way: Add/Modify Style Master</title>
    <link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />

    <script src="../JavaScript/WAY.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript">
    
    function SelectFunction(str3)
    {   
        //alert(str3);        
        //{debugger}
        var pos=str3.split('|'); 
        if (window.opener.document.forms['form1']['hdW_StyleId']!=null)
        {
        window.opener.document.forms['form1']['hdW_StyleId'].value=pos[0];
        }        
        if (window.opener.document.forms['form1']['hdBarcodeNo']!=null)
        {
        window.opener.document.forms['form1']['hdBarcodeNo'].value=pos[1];
        }
        if (window.opener.document.forms['form1']['txtBarCode']!=null)
        {
        window.opener.document.forms['form1']['txtBarCode'].value=pos[1];
        }
        if (window.opener.document.forms['form1']['txtStyleName']!=null)
        {
        window.opener.document.forms['form1']['txtStyleName'].value=pos[2];
        }                
        if (window.opener.document.forms['form1']['txtDesignName']!=null)
        {
        window.opener.document.forms['form1']['txtDesignName'].value=pos[3];
        }        
        if (window.opener.document.forms['form1']['txtShadeNo']!=null)
        {
        window.opener.document.forms['form1']['txtShadeNo'].value=pos[4];
        }       
        if (window.opener.document.forms['form1']['txtMRP']!=null)
        {
        window.opener.document.forms['form1']['txtMRP'].value=pos[5];
        } 
        window.close();
   }
   function AirlineReset()
    {
        document.getElementById("txtBarCode").value="";       
        document.getElementById("txtStyleName").value="";       
        document.getElementById("txtDesignName").selectedIndex=0;
        document.getElementById("txtShadeNo").value="";       
        document.getElementById("txtMRP").value="";
    }
 
    function EditFunction(strW_StyleId)
    {           
          window.location.href="MSUP_Style.aspx?Action=U&W_StyleId="+strW_StyleId;
          return false;
    }
    function DeleteFunction(strW_StyleId)
    {   
        if (confirm("Are you sure you want to delete?")==true)
        {
         document.getElementById('hdDelete').value = strW_StyleId;
         document.forms['form1'].submit();           
        }
        else
        {
            return false;
        }
    }
       function DeleteFunction2(strW_StyleId)
          {   
               if (confirm("Are you sure you want to delete?")==true)
            {   
               document.getElementById('hdDelete').value = strW_StyleId;
               document.forms['form1'].submit(); 
            }  
            else
            {
                return false;
            }         
        }
    function NewFunction()
    {   
      window.location.href="MSUP_Style.aspx?Action=I";       
      return false;
    }
    </script>

</head>
<body>
    <form id="form1" runat="server" defaultbutton="btnSearch" defaultfocus="txtBarCode">
        <div>
            <table width="860px" align="left" class="border_rightred">
                <tr>
                    <td valign="top">
                        <table width="100%" align="left">
                            <tr>
                                <td valign="top" align="left">
                                    <span class="menu">Setup-></span><span class="sub_menu">Style</span>
                                </td>
                            </tr>
                            <tr>
                            <tr>
                                <td class="heading" align="center" valign="top" style="height: 10px">
                                    Search Style
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                        <tr>
                                            <td width="100%" class="redborder">
                                                <table align="left" border="0" cellpadding="2" cellspacing="0" width="100%">
                                                    <tr>
                                                        <td class="textbold" style="width: 100%" colspan="6" valign="top">
                                                            <table width="100%" border="0" align="left" cellpadding="1" cellspacing="1">
                                                                <tr>
                                                                    <td class="textbold" colspan="6" align="center" valign="TOP">
                                                                        <asp:Label ID="lblError" runat="server" CssClass="ErrorMsg"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="textbold" style="width: 10%;">
                                                                        Bar Code</td>
                                                                    <td align="left" style="width: 20%;">
                                                                        <asp:TextBox ID="txtBarCode" runat="server" CssClass="textfield" TabIndex="1" Width="177px"
                                                                            MaxLength="200"></asp:TextBox></td>
                                                                    <td style="width: 20%;" class="right">
                                                                        Style Name&nbsp;</td>
                                                                    <td style="width: 10%;">
                                                                        <asp:TextBox ID="txtStyleName" runat="server" CssClass="textfield" MaxLength="50"
                                                                            TabIndex="1" Width="177px"></asp:TextBox></td>
                                                                    <td style="width: 10%;">
                                                                    </td>
                                                                    <td style="width: 10%;" class="right">
                                                                        <asp:Button ID="btnSearch" runat="server" CssClass="button" Text="Search" TabIndex="4"
                                                                            OnClick="btnSearch_Click" AccessKey="A" /></td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="textbold" colspan="6" align="center" valign="TOP">
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="textbold">
                                                                        Design No</td>
                                                                    <td align="left">
                                                                        <asp:TextBox ID="txtDesignName" runat="server" CssClass="textfield" TabIndex="3"
                                                                            Width="176px" MaxLength="50"></asp:TextBox></td>
                                                                    <td style="width: 20%;" class="right">
                                                                        Shade No &nbsp;</td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtShadeNo" runat="server" CssClass="textfield" MaxLength="50" TabIndex="3"
                                                                            Width="176px"></asp:TextBox></td>
                                                                    <td>
                                                                    </td>
                                                                    <td style="width: 10%;" class="right">
                                                                        <asp:Button ID="btnNew" runat="server" CssClass="button" Text="New" TabIndex="5"
                                                                            AccessKey="N" /></td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="textbold" colspan="6" align="center" valign="TOP">
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="textbold">
                                                                        Mrp</td>
                                                                    <td >
                                                                        <asp:TextBox ID="txtMRP" runat="server" CssClass="textfield" MaxLength="25" TabIndex="3"
                                                                            Width="176px"></asp:TextBox></td>
                                                                    <td style="width: 20%;" class="right">
                                                                        &nbsp;</td>
                                                                    <td>
                                                                    </td>
                                                                    <td>
                                                                    </td>
                                                                    <td style="width: 10%;" class="right">
                                                                        <asp:Button ID="btnReset" runat="server" CssClass="button" Text="Reset" TabIndex="6"
                                                                            AccessKey="R" /></td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="center" colspan="6" align="center" valign="TOP">
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="textbold">
                                                                    </td>
                                                                    <td align="left">
                                                                    </td>
                                                                    <td style="width: 20%;" class="right">
                                                                        &nbsp;</td>
                                                                    <td>
                                                                    </td>
                                                                    <td>
                                                                    </td>
                                                                    <td style="width: 10%;" class="right">
                                                                        <asp:Button ID="btnExport" runat="server" CssClass="button" Text="Export" TabIndex="6"
                                                                            AccessKey="E" /></td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="center" colspan="6" align="center" valign="TOP" style="height: 17px">
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="6" style="width: 100%">
                                                                        <asp:GridView ID="dbgrdManageStyle" HeaderStyle-Wrap="false" HeaderStyle-ForeColor="white"
                                                                            runat="server" AutoGenerateColumns="False" Width="100%" EnableViewState="False"
                                                                            AllowSorting="True">
                                                                            <Columns>
                                                                                <asp:TemplateField HeaderText="BarcodeNo" SortExpression="BarcodeNo">
                                                                                    <ItemTemplate>
                                                                                        <%#Eval("BarcodeNo")%>
                                                                                        <asp:HiddenField ID="hdW_StyleId" runat="server" Value='<%#Eval("W_StyleId")%>' />
                                                                                        <asp:HiddenField ID="hdBarcodeNo" runat="server" Value='<%#Eval("BarcodeNo")%>' />
                                                                                    </ItemTemplate>
                                                                                    <ItemStyle Width="100px" />
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="StyleName" SortExpression="StyleName">
                                                                                    <ItemTemplate>
                                                                                        <%#Eval("StyleName")%>
                                                                                    </ItemTemplate>
                                                                                    <ItemStyle Width="300px" Wrap="True" />
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="DesignNo" SortExpression="DesignNo">
                                                                                    <ItemTemplate>
                                                                                        <%#Eval("DesignNo")%>
                                                                                    </ItemTemplate>
                                                                                    <ItemStyle Width="100px" Wrap="True" />
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="ShadeNo" SortExpression="ShadeNo">
                                                                                    <ItemTemplate>
                                                                                        <%#Eval("ShadeNo")%>
                                                                                    </ItemTemplate>
                                                                                    <ItemStyle Width="100px" Wrap="False" />
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="MRP" SortExpression="MRP">
                                                                                    <ItemTemplate>
                                                                                        <%#Eval("MRP")%>
                                                                                    </ItemTemplate>
                                                                                    <ItemStyle Width="100px" Wrap="False" />
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Action">
                                                                                    <ItemTemplate>
                                                                                        <asp:LinkButton ID="lnkSelect" runat="server" CssClass="LinkButtons" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "W_StyleId") + "|" + DataBinder.Eval(Container.DataItem, "BarcodeNo") + "|" + DataBinder.Eval(Container.DataItem, "StyleName")+ "|" + DataBinder.Eval(Container.DataItem, "DesignNo")+ "|" + DataBinder.Eval(Container.DataItem, "ShadeNo") + "|" + DataBinder.Eval(Container.DataItem, "MRP") +"|"  %> '>Select</asp:LinkButton>&nbsp;
                                                                                        <a href="#" class="LinkButtons" id="linkEdit" runat="server">Edit</a>&nbsp; <a href="#"
                                                                                            class="LinkButtons" id="linkDelete" runat="server">Delete</a>
                                                                                    </ItemTemplate>
                                                                                    <ItemStyle Wrap="False" />
                                                                                </asp:TemplateField>
                                                                            </Columns>
                                                                            <HeaderStyle CssClass="Gridheading" HorizontalAlign="Left" ForeColor="White" Wrap="False" />
                                                                            <RowStyle CssClass="ItemColor" />
                                                                            <AlternatingRowStyle CssClass="lightblue" />
                                                                        </asp:GridView>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="6" height="12">
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td valign="top" colspan="6">
                                                                        <asp:Panel ID="pnlPaging" runat="server" Visible="false" Width="800px">
                                                                            <table border="0" cellpadding="0" cellspacing="0" style="width: 800px">
                                                                                <tr class="paddingtop paddingbottom">
                                                                                    <td style="width: 243px; height: 29px;" class="left" nowrap="nowrap">
                                                                                        <span class="textbold"><b>&nbsp;No. of records found</b></span>&nbsp;&nbsp;<asp:TextBox
                                                                                            ID="txtTotalRecordCount" runat="server" Width="105px" CssClass="textboxgrey"
                                                                                            ReadOnly="true"></asp:TextBox></td>
                                                                                    <td style="width: 200px; height: 29px;" class="right">
                                                                                        <asp:LinkButton ID="lnkPrev" CssClass="LinkButtons" runat="server" CommandName="Prev"><< Prev</asp:LinkButton></td>
                                                                                    <td style="width: 356px; height: 29px;" class="center">
                                                                                        <span class="textbold"><b>Page No</b></span>&nbsp;&nbsp;<asp:DropDownList ID="ddlPageNumber"
                                                                                            Width="70px" CssClass="dropdownlist" runat="server" AutoPostBack="True">
                                                                                        </asp:DropDownList></td>
                                                                                    <td style="width: 187px; height: 29px;" class="left">
                                                                                        <asp:LinkButton ID="lnkNext" runat="server" CssClass="LinkButtons" CommandName="Next">Next >></asp:LinkButton></td>
                                                                                </tr>
                                                                            </table>
                                                                        </asp:Panel>
                                                                        <asp:TextBox ID="txtRecordOnCurrReco" Visible="false" runat="server"></asp:TextBox>
                                                                    </td>
                                                                </tr>
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
                                                            <asp:HiddenField ID="hdDelete" runat="server" />
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
                    <td valign="top" style="height: 6px">
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
