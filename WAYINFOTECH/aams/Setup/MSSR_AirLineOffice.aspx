<%@ Page Language="VB" AutoEventWireup="false" CodeFile="MSSR_AirLineOffice.aspx.vb"
    Inherits="Setup_MSSR_AirLineOffice" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>AAMS: Add/Modify AirLineOffice</title>
    <link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />

    <script src="../JavaScript/AAMS.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript">
    function SelectFunction(str3,BDRLetter,HdVersion)
    {   
//        alert(str3);
//        alert(BDRLetter);
//        alert(HdVersion);
        var pos=str3.split('|'); 
         if (window.opener.document.forms['form1']['HdAROFID']!=null)
        {
        window.opener.document.forms['form1']['HdAROFID'].value=pos[0];
        }
          if (window.opener.document.forms['form1']['hdAirLineCode']!=null)
        {
        window.opener.document.forms['form1']['hdAirLineCode'].value=pos[1];
        }
        if (window.opener.document.forms['form1']['hdAilLineName']!=null)
        {
        window.opener.document.forms['form1']['hdAilLineName'].value=pos[2];
        }
         if (window.opener.document.forms['form1']['txtAirLineoffice']!=null)
        {
        window.opener.document.forms['form1']['txtAirLineoffice'].value=pos[2];
        }
         if (window.opener.document.forms['form1']['txtAirLineOfficeAdd']!=null)
        {
        window.opener.document.forms['form1']['txtAirLineOfficeAdd'].value=pos[3];
        }
          if (window.opener.document.forms['form1']['hdAoffice']!=null)
        {
        window.opener.document.forms['form1']['hdAoffice'].value=pos[4];
        }
        if (window.opener.document.forms['form1']['txtBdrLetter']!=null)
        {
        window.opener.document.forms['form1']['txtBdrLetter'].value=BDRLetter;//pos[5];
        }
          if (window.opener.document.forms['form1']['hdTemplateVersion']!=null)
        {
        window.opener.document.forms['form1']['hdTemplateVersion'].value=HdVersion;//pos[6];
        }
       // alert( window.opener.document.forms['form1']['txtBdrLetter'].value);
        window.close();
   }
   function AirlineReset()
    {
        document.getElementById("txtAirLinecode").value="";       
        document.getElementById("txtAirLineName").value="";       
        document.getElementById("cboAoffice").selectedIndex=0;
    }
 
    function EditFunction(CheckBoxObj)
    {           
          window.location.href="MSUP_AirLineOffice.aspx?Action=U&AR_OF_ID="+CheckBoxObj;                         
          return false;
    }
    function DeleteFunction(CheckBoxObj)
    {   
        if (confirm("Are you sure you want to delete?")==true)
        {   
      
             document.getElementById('hdDelete').value = CheckBoxObj;
              document.forms['form1'].submit(); 
          //window.location.href="MSSR_AirLineOffice.aspx?Action=D|"+ CheckBoxObj +"|"+ document.getElementById("<%=txtAirLinecode.ClientID%>").value +"|"+ document.getElementById("cboAoffice").selectedIndex +"|"+ document.getElementById("txtAirLineName").value;                   
         // return false;
        }
        else
        {
            return false;
        }
    }
       function DeleteFunction2(CheckBoxObj)
          {   
               if (confirm("Are you sure you want to delete?")==true)
            {   
               document.getElementById('hdDelete').value = CheckBoxObj;
               document.forms['form1'].submit(); 
            }  
            else
            {
                return false;
            }         
        }
    function NewFunction()
    {   
          window.location.href="MSUP_AirLineOffice.aspx?Action=I";       
          return false;
    }
    </script>

</head>
<body>
    <form id="form1" runat="server" defaultbutton="btnSearch" defaultfocus="txtAirLinecode">
        <div>
            <table width="860px" align="left" height="486px" class="border_rightred">
                <tr>
                    <td valign="top">
                        <table width="100%" align="left">
                            <tr>
                                <td valign="top" align="left">
                                    <span class="menu">Setup-></span><span class="sub_menu">AirLine Office</span>
                                </td>
                            </tr>
                            <tr>
                            <tr>
                                <td class="heading" align="center" valign="top" style="height: 10px">
                                    Search Airline Office
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
                                                                        Airline Code</td>
                                                                    <td style="width: 20%;">
                                                                       
                                                                            <asp:TextBox ID="txtAirLinecode" runat="server" CssClass="textfield" TabIndex="1"
                                                                                Width="177px" MaxLength="2"></asp:TextBox></td>
                                                                    <td style="width: 40%;" class="center">
                                                                        <asp:Button ID="btnSearch" runat="server" CssClass="button" Text="Search" TabIndex="4"
                                                                            OnClick="btnSearch_Click" AccessKey="A" /></td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="textbold" colspan="4" align="center" valign="TOP">
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="textbold">
                                                                    </td>
                                                                    <td style="height: 22px" align="left">
                                                                        Aoffice</td>
                                                                    <td class="textbold" style="height: 22px; width: 108px;">
                                                                        <asp:DropDownList  onkeyup="gotop(this.id)"  ID="cboAoffice" runat="server" CssClass="dropdown" TabIndex="2"
                                                                            Width="182px">
                                                                        </asp:DropDownList></td>
                                                                    <td style="height: 22px" class="center">
                                                                        <asp:Button ID="btnNew" runat="server" CssClass="button" Text="New" TabIndex="5" AccessKey="N" /></td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="textbold" colspan="4" align="center" valign="TOP">
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="textbold">
                                                                    </td>
                                                                    <td style="height: 22px" align="left">
                                                                        Airline Name</td>
                                                                    <td style="height: 22px; width: 108px;">
                                                                        <asp:TextBox ID="txtAirLineName" runat="server" CssClass="textfield" TabIndex="3"
                                                                            Width="176px" MaxLength="40"></asp:TextBox></td>
                                                                    <td style="height: 22px" class="center">
                                                                        <asp:Button ID="btnReset" runat="server" CssClass="button" Text="Reset" TabIndex="6"  AccessKey="R"/></td>
                                                                </tr>
                                                                 <tr>
                                                                    <td class="textbold" colspan="4" align="center" valign="TOP">
                                                                    </td>
                                                                </tr>
                                                                
                                                                <tr>
                                                                    <td colspan="2">
                                                                    </td>
                                                                    <td style="width: 108px">
                                                                        &nbsp;</td>
                                                                    <td class="center">
                                                                        <asp:Button ID="Button1" runat="server" CssClass="button" Text="Export" TabIndex="6"  AccessKey="E"/></td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="4" style="height: 4px">
                                                                    </td>
                                                                </tr>
                                                                <tr >
                                                                    <td colspan="4" style="width:100%">
                                                                        <asp:GridView ID="dbgrdManageAirLineOffice" HeaderStyle-Wrap="false"  HeaderStyle-ForeColor="white" runat="server" AutoGenerateColumns="False"
                                                                            Width="100%" EnableViewState="False" AllowSorting="True" >
                                                                            <Columns>
                                                                                <asp:TemplateField HeaderText="Airline Code" ItemStyle-Width="100px" SortExpression="Airline_Code">
                                                                                    <ItemTemplate>
                                                                                        <%#Eval("Airline_Code")%>     
                                                                                        <asp:HiddenField ID="hdAirCode" runat="server" Value='<%#Eval("Airline_Code")%>' />                                                                                      
                                                                                        <asp:HiddenField ID="hdAirlineCode" runat="server" Value='<%#Eval("AR_OF_ID")%>' />   
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Airline Name" ItemStyle-Width="150px" ItemStyle-Wrap="true"  SortExpression="Airline_Name">
                                                                                    <ItemTemplate>
                                                                                        <%#Eval("Airline_Name")%>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Airline Office Address" ItemStyle-Width="300px" ItemStyle-Wrap="true" SortExpression="AR_OF_Address">
                                                                                    <ItemTemplate>
                                                                                        <%#Eval("AR_OF_Address")%>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Aoffice" ItemStyle-Width="100px" ItemStyle-Wrap="false" SortExpression="Aoffice">
                                                                                    <ItemTemplate>
                                                                                        <%#Eval("Aoffice")%>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Action" >
                                                                                         <ItemTemplate>
                                                                                         <asp:LinkButton  ID="lnkSelect" Runat="server" CssClass="LinkButtons" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "AR_OF_ID") + "|" + DataBinder.Eval(Container.DataItem, "Airline_Code") + "|" + DataBinder.Eval(Container.DataItem, "Airline_Name")+ "|" + DataBinder.Eval(Container.DataItem, "AR_OF_Address")+ "|" + DataBinder.Eval(Container.DataItem, "Aoffice") + "|"   %> '>Select</asp:LinkButton>&nbsp;<a href="#" class="LinkButtons" id="linkEdit" runat="server">Edit</a>&nbsp;<a href="#" class="LinkButtons" id="linkDelete" runat="server">Delete</a>
                                                                                        </ItemTemplate>
                                                                                    <ItemStyle Wrap="False" />
                                                                                </asp:TemplateField>
                                                                            </Columns>
                                                                            <HeaderStyle CssClass="Gridheading" HorizontalAlign="Left" />
                                                                            <RowStyle CssClass="ItemColor" />
                                                                            <AlternatingRowStyle CssClass="lightblue" />
                                                                        </asp:GridView>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="4" height="12">
                                                                    </td>
                                                                </tr>
                                                                
                                                               
                                            <tr>   
                                                    <td valign ="top" colspan="4"  >
                                                    <asp:Panel ID="pnlPaging" runat="server" Visible="false" Width="800px">
                                                      <table border="0" cellpadding="0" cellspacing="0" style="width: 800px">
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
                                                                   <asp:TextBox ID="txtRecordOnCurrReco" Visible="false" runat="server" ></asp:TextBox>
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
