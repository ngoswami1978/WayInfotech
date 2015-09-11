<%@ Page Language="VB" AutoEventWireup="false" CodeFile="MSSR_AgencyOfficeID.aspx.vb"
    Inherits="MSSR_AgencyOfficeID" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="head1" runat="server">
    <title>AAMS:Manage Office ID</title>
     <base target="_self" />
    <script src="../JavaScript/AAMS.js" type="text/javascript"></script>
  
    <link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />
</head>
  <script type="text/javascript" language="javascript">
    function SelectFunction(str3)
    {   
        var pos=str3; 
        if  (window.opener.form1['txtOfficeID1']!=null)
        {
	    window.opener.form1['txtOfficeID1'].value=pos;
	    } 
	     if  (window.opener.form1['txtOfficeId']!=null)
        {
	    window.opener.form1['txtOfficeId'].value=pos;
	    }
	      if  (window.opener.form1['txtOfficeID1']!=null)
        {
	    window.opener.form1['txtOfficeID1'].value=pos;
	    }
	     if  (window.opener.form1['hdOfficeID']!=null)
        {
	    window.opener.form1['hdOfficeID'].value=pos;
	    } 
	    	window.close();
	}
  function EditFunction(OfficeID)
    {  
       window.location.href="MSUP_OfficeId.aspx?Action=U&OfficeID="+OfficeID;
       return false;
       
    }
    function GenerateFunction()
    {
        window.location.href="MSUP_AgencyGenOfficeId.aspx?Action=I";
        return false;
    }
    function PopupCorporateCode() 
    {
   
    var type;

 
    type = "MSSR_CorporateCode.aspx?Popup=T" ;
   	window.open(type,"aa","height=600,width=880,top=30,left=20,scrollbars=1,status=1");	
    return false;  
}  
</script>
<body >
    <form id="form1" defaultbutton="btnSearch" runat="server">
        <table width="860px" align="left" class="border_rightred" style="height: 486px;">
            <tr>
                <td valign="top" style="height: 482px">
                    <table width="100%" align="left">
                        <tr>
                            <td valign="top" align="left">
                                <span class="menu">Travel Agency-></span><span class="sub_menu">Office ID</span>
                            </td>
                        </tr>
                        <tr>
                            <td class="heading" align="center" valign="top">
                                Search Office Id</td>
                        </tr>
                        <tr>
                            <td valign="top" style="height: 314px">
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td align="LEFT" class="redborder">
                                            <table width="100%" border="0" align="left" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td class="textbold" colspan="6" align="center" style="height: 15px">
                                                        &nbsp;<asp:Label ID="lblError" runat="server" CssClass="ErrorMsg"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="textbold" style="width: 227px; height: 19px;">
                                                        &nbsp;</td>
                                                    <td class="textbold" style="width: 305px; height: 19px;">
                                                    </td>
                                                    <td style="width: 200px; height: 19px;">
                                                    </td>
                                                    <td style="width: 94px; height: 19px;">
                                                    </td>
                                                    <td width="15%" style="height: 19px">
                                                    </td>
                                                    <td width="15%" style="height: 19px">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="textbold" style="width: 227px; height: 22px;">
                                                    </td>
                                                    <td class="textbold" style="width: 305px; height: 22px;" align="left">
                                                        Office ID</td>
                                                    <td style="width: 20%; height: 22px;">
                                                        <asp:TextBox ID="txtOfficeId" runat="server" CssClass="textbox" MaxLength="30" TabIndex="1"
                                                            Width="160px"></asp:TextBox></td>
                                                    <td class="textbold" style="width: 94px; height: 22px;" align="center">
                                                        </td>
                                                    <td style="width: 20%; height: 22px;">
                                                        </td>
                                                    <td style="width: 23%; height: 22px;">
                                                        <asp:Button ID="btnSearch" runat="server" TabIndex="3" CssClass="button" Text="Search" AccessKey="A" /></td>
                                                </tr>
                                                <tr>
                                                    <td class="textbold" colspan="6" align="center" valign="TOP" style="height: 7px">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="textbold" style="height: 25px; width: 227px;">
                                                    </td>
                                                    <td class="textbold" style="width: 305px; height: 25px;" align="left">
                                                        City</td>
                                                    <td class="textbold" style="width: 200px; height: 25px;">
                                                        <asp:DropDownList  onkeyup="gotop(this.id)"  ID="drpCity" runat="server" TabIndex="2" Width="165px" CssClass="textbold">
                                                        </asp:DropDownList></td>
                                                    <td class="textbold" style="width: 94px; height: 25px;">
                                                    </td>
                                                    <td style="height: 25px">
                                                    </td>
                                                    <td style="height: 25px">
                                                        <asp:Button ID="btnGenerate" CssClass="button" runat="server" TabIndex="5" Text="Generate" AccessKey="G" /></td>
                                                </tr>
                                                <tr>
                                                    <td class="textbold" colspan="6" align="center" valign="TOP" style="height: 7px">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="textbold" style="width: 227px; height: 25px">
                                                    </td>
                                                    <td class="textbold" style="width: 305px; height: 25px">
                                                        Corporate Code</td>
                                                    <td class="textbold" style="width: 200px; height: 25px">
                                                        <asp:TextBox ID="ddlCorporateCode" runat="server" CssClass="textboxgrey" MaxLength="1" TabIndex="4" Width="160px"></asp:TextBox></td>
                                                    <td class="textbold" style="width: 94px; height: 25px">
                                                        <img alt="" onclick="javascript:return PopupCorporateCode();" style="cursor:pointer;" 
                                                            src="../Images/lookup.gif" /></td>
                                                    <td style="height: 25px">
                                                    </td>
                                                    <td style="height: 25px">
                                                        <asp:Button ID="btnExport" runat="server" CssClass="button" Style="position: relative"
                                                            TabIndex="21" Text="Export" AccessKey="E" /></td>
                                                </tr>
                                                 <tr>
                                                    <td class="textbold" colspan="6" align="center" valign="TOP" style="height: 7px">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="textbold" style="width: 227px; height: 25px">
                                                    </td>
                                                    <td style="width: 305px; height: 25px;">
                                                    </td>
                                                    <td class="textbold" style="width: 200px; height: 25px;">
                                                        <asp:CheckBox ID="chkunallocatedid" class="textbold" runat="server" Text="UnAllocated OfficeId"
                                                            TabIndex="4" Width="168px" Checked="True" /></td>
                                                    <td class="textbold" style="width: 94px; height: 25px;">
                                                    </td>
                                                    <td style="height: 25px">
                                                    </td>
                                                    <td style="height: 25px">
                                                        <asp:Button ID="btnReset" CssClass="button" runat="server" TabIndex="5" Text="Reset" style="left: 0px; position: relative" AccessKey="R" />&nbsp;</td>
                                                </tr>
                                             
                                                   <tr>
                                                    <td class="textbold" colspan="6" align="center" valign="TOP" style="height: 7px">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="textbold" style="width: 227px; height: 25px">
                                                    </td>
                                                    <td style="width: 305px; height: 25px">
                                                    </td>
                                                    <td class="textbold" style="width: 200px; height: 25px">
                                                    </td>
                                                    <td class="textbold" style="width: 94px; height: 25px">
                                                    </td>
                                                    <td style="height: 25px">
                                                    </td>
                                                    <td style="height: 25px">
                                                        </td>
                                                </tr>
                                                <tr>
                                                    <td class="textbold" style="width: 227px; height: 25px">
                                                    </td>
                                                    <td style="width: 305px; height: 25px">
                                                    </td>
                                                    <td class="textbold" style="width: 200px; height: 25px">
                                                    </td>
                                                    <td class="textbold" style="width: 94px; height: 25px">
                                                    </td>
                                                    <td style="height: 25px">
                                                    </td>
                                                    <td style="height: 25px">
                                                    </td>
                                                </tr>
                                                   <tr>
                                                    <td class="textbold" colspan="6" align="center" valign="TOP" style="height: 7px">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="center" class="textbold" colspan="6" valign="top">
                                                     <asp:HiddenField ID="hdCity" runat="server" />
                                                      <asp:HiddenField ID="hdCityText" runat="server" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="6" style="height: 4px">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="7" style="height: 4px">
                                                        <asp:GridView ID="grdAgencyOfficeId" BorderWidth="1" BorderColor="#d4d0c8" runat="server"
                                                            AutoGenerateColumns="False" ItemStyle-CssClass="ItemColor" AlternatingItemStyle-CssClass="lightblue"
                                                            HeaderStyle-CssClass="Gridheading" Width="100%" AllowSorting="True"   HeaderStyle-ForeColor="white" >
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="Office Id" SortExpression="OFFICEID">
                                                                    <ItemTemplate>
                                                                        <asp:HiddenField ID="HiddenOfficeId" runat="server" Value='<%#Eval("OFFICEID")%>' />
                                                                        <%#Eval("OFFICEID")%>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Left" />
                                                                    <HeaderStyle HorizontalAlign="Left"  Width="10%"/>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Agency" SortExpression="NAME">
                                                                    <ItemTemplate>
                                                                        <%#Eval("NAME")%>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Left" />
                                                                    <HeaderStyle HorizontalAlign="Left" Width="25%"/>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="CID" SortExpression="CID">
                                                                    <ItemTemplate>
                                                                        <%#Eval("CID")%>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Left" />
                                                                    <HeaderStyle HorizontalAlign="Left" Width="10%"/>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Date Of Processing" SortExpression="PROCESSING_DATE">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblPD" runat="server" Text='<%#Eval("PROCESSING_DATE")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Left" />
                                                                    <HeaderStyle HorizontalAlign="Left"  Width="10%"/>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Terminal Id" SortExpression="TERMINALID">
                                                                    <ItemTemplate>
                                                                        <%#Eval("TERMINALID")%>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Left" />
                                                                    <HeaderStyle HorizontalAlign="Left" Width="10%" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Remarks" SortExpression="REMARKS">
                                                                    <ItemTemplate>
                                                                        <%#Eval("REMARKS")%>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Left" />
                                                                    <HeaderStyle HorizontalAlign="Left" Width="25%" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Action">
                                                                    <ItemTemplate>
                                                                       <a href="#" class="LinkButtons"  id="lnkEdit" runat="server">Edit</a> 
                                                                       <asp:LinkButton ID="lnkSelect" CssClass="LinkButtons" Text="Select" runat="server" CommandName="Select"
                                                                            CommandArgument='<%# DataBinder.Eval(Container.DataItem, "OFFICEID") %>'></asp:LinkButton>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Left" />
                                                                    <HeaderStyle HorizontalAlign="Left" Width="10%" />
                                                                </asp:TemplateField>
                                                            </Columns>
                                                            <AlternatingRowStyle CssClass="lightblue" />
                                                            <RowStyle CssClass="textbold" />
                                                            <HeaderStyle CssClass="Gridheading" />
                                                        </asp:GridView>
                                                    </td>
                                                </tr>
                                                 <tr>                                                   
                                                    <td colspan="7" valign ="top"  >
                                         <asp:Panel ID="pnlPaging" runat="server" Visible="false" Width="100%">
                                                      <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                      <tr style="padding-top : 4pt; padding-bottom : 4pt">                                                                                                                                                
                                                                          <td style="width: 30%" class="left"><span  class="textbold"><b>&nbsp;No. of records found</b></span>&nbsp;&nbsp;<asp:TextBox ID="txtTotalRecordCount" runat ="server"  Width="105px" CssClass="textboxgrey" ></asp:TextBox></td>
                                                                          <td style="width: 25%" class="right">                                                                             
                                                                              <asp:LinkButton ID="lnkPrev" CssClass="LinkButtons" runat="server" CommandName="Prev"><< Prev</asp:LinkButton></td>
                                                                          <td style="width: 20%" class="center">
                                                                              <span class ="textbold" ><b>Page No</b></span>&nbsp;&nbsp;<asp:DropDownList  onkeyup="gotop(this.id)"  ID="ddlPageNumber" Width="70px" CssClass="dropdownlist" runat="server" AutoPostBack="True" >
                                                                              </asp:DropDownList></td>
                                                                          <td style="width: 25%" class="left">
                                                                              <asp:LinkButton ID="lnkNext" runat="server" CssClass="LinkButtons" CommandName="Next">Next >></asp:LinkButton></td>
                                                                      </tr>
                                                                  </table></asp:Panel>
                                                                   <asp:TextBox ID="txtRecordOnCurrentPage" runat ="server"  Width="73px" CssClass="textboxgrey" Visible ="false"  ></asp:TextBox>      
                                                    </td>
                                                     
                                                </tr>  
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                                <asp:Literal ID="litOfficeId" runat="server"></asp:Literal></td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
