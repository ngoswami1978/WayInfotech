<%@ Page Language="VB" AutoEventWireup="false" CodeFile="MSSR_ISP.aspx.vb" Inherits="ISP_MSSR_ISP" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Search ISP</title>
     <script src="../JavaScript/AAMS.js" type="text/javascript"></script>
     <script language="javascript" type="text/javascript">
         function SelectFunction(str3)
        {   
          //  alert(str3);
            var pos=str3.split('|'); 
            
            
            
            
             if (window.opener.document.forms['form1']['hidIspId']!=null)
            {
            window.opener.document.forms['form1']['hidIspId'].value=pos[0];
            }
            if (window.opener.document.forms['form1']['hdISPId']!=null)
            {
            window.opener.document.forms['form1']['hdISPId'].value=pos[0];
            }
            if (window.opener.document.forms['form1']['txtIspName']!=null)
            {
            window.opener.document.forms['form1']['txtIspName'].value=pos[1];
            }
             if (window.opener.document.forms['form1']['txtIsp']!=null)
            {
            window.opener.document.forms['form1']['txtIsp'].value=pos[1];
            }
            if (window.opener.document.forms['form1']['txtISPCityName']!=null)
            {
            window.opener.document.forms['form1']['txtISPCityName'].value=pos[2];
            }
            
            if (window.opener.document.forms['form1']['txtNEWNPID']!=null)
            {
                  window.opener.document.forms['form1']['txtNEWNPID'].value='';
                  if (window.opener.document.forms['form1']['hdIspPlanId']!=null)
                  {
                    window.opener.document.forms['form1']['hdIspPlanId'].value='';
                  }
                    if (window.opener.document.forms['form1']['hdIspPlanId']!=null)
                  {
                    window.opener.document.forms['form1']['hdIspPlanId'].value='';
                  }
                  
                   if (window.opener.document.forms['form1']['hdIspProviderID']!=null)
                  {
                    window.opener.document.forms['form1']['hdIspProviderID'].value=pos[3];
                  }
            }
            
            
            window.close();
       } 
    function EditFunction(CheckBoxObj)
    {           
          window.location.href="MSUP_ISP.aspx?Action=U|"+CheckBoxObj;               
          return false;
    }
        function InsertISP()
        {
        window.location.href="MSUP_ISP.aspx?Action=I|";
        return false;
        }
        
    function DeleteFunction(ISPId)
    {   
//        if (confirm("Are you sure you want to delete?")==true)
//        {   
//        
//          window.location.href="MSSR_ISP.aspx?Action=D|"+ ISPId;                   
//          return false;
//        }
          if (confirm("Are you sure you want to delete?")==true)
                  {        
                    document.getElementById("hdDeleteISPID").value= ISPId ;   
                  }
                else
                {
                 document.getElementById("hdDeleteISPID").value="";
                 return false;
                }
    }
       function DeleteFunction2(ISPId)
    {   
        if (confirm("Are you sure you want to delete?")==true)
        { 
          window.location.href="MSSR_ISP.aspx?PopUp=T&Action=D|"+ ISPId;                   
          return false;
        }
    }
      function DeleteFunction3(ISPId,CityNmae)
    {   
  
        if (confirm("Are you sure you want to delete?")==true)
        { 
          window.location.href="MSSR_ISP.aspx?PopUp=T&" + "CityNmae=" + CityNmae + "&Action=D|"+ ISPId ;                   
          return false;
        }
    }
    
   
    </script>
    <link href="../CSS/AAMS.css" type="text/css" rel="stylesheet" />
</head>
<body >
    <form  defaultbutton="btnSearch" defaultfocus="txtISPName" id="frnISP" runat="server">
    <table width="860px" align="left"  class="border_rightred">
            <tr>
                <td valign="top">
                    <table width="100%" align="left">
                        <tr>
                            <td valign="top"  align="left" >
                            <span class="menu">ISP-&gt;</span><span class="sub_menu">ISP Search</span></td>
                        </tr>
                        <tr>
                            <td class="heading" align="center" valign="top">
                                Search <span style="font-family: Microsoft Sans Serif">ISP</span></td>
                        </tr>
                        <tr>
                            <td valign="top">
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td class="redborder center">
                                            <table border="0" cellpadding="2" cellspacing="1" style="width:800px;" class="left">
                                                    <tr>
                                                        <td colspan="4" class="center gap">
                                                        <asp:Label ID="lblError" runat="server" CssClass="ErrorMsg"></asp:Label></td>
                                                                </tr>
                                                    <tr>
                                                        <td style="width: 176px">
                                                        </td>
                                                        <td class="textbold">
                                                            ISP Name</td>
                                                        <td style="width: 308px">
                                                              <asp:TextBox ID="txtISPName" runat="server" CssClass="textbox" Width="208px" MaxLength="50" TabIndex="1"></asp:TextBox></td>
                                                        <td>
                                                             <asp:Button ID="btnSearch" CssClass="button" runat="server" Text="Search" TabIndex="4" AccessKey="A" /></td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 176px">
                                                        </td>
                                                        <td class="textbold">
                                                                                            City Name</td>
                                                        <td style="width: 308px">
                                                                                <asp:DropDownList  onkeyup="gotop(this.id)"  ID="drpCityName" runat="server" CssClass="dropdownlist" Width="214px" TabIndex="2">
                                                                                </asp:DropDownList></td>
                                                        <td>
                                                                                <asp:Button ID="btnNew" CssClass="button" runat="server" Text="New" TabIndex="5" AccessKey="N" /></td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 176px">
                                                        </td>
                                                        <td class="textbold">
                                                           <%-- Provider Name--%></td>
                                                        <td style="width: 308px">
                                              <%--  <asp:DropDownList  onkeyup="gotop(this.id)"  ID="drpIspProvider" runat="server" CssClass="dropdownlist" Width="214px" TabIndex="3" Height="30px">
                                                        </asp:DropDownList>--%></td>
                                                        <td>
                                                            <asp:Button ID="btnExport" runat="server" CssClass="button" TabIndex="6" Text="Export" AccessKey="E" /></td>
                                                     </tr>
                                                    <tr>
                                                        <td style="width: 176px; height: 26px;">
                                                        </td>
                                                        <td class="textbold" style="height: 26px">
                                                 </td>
                                                         <td style="width: 308px; height: 26px;">
                                                 </td>
                                                         <td style="height: 26px">
                                                       <asp:Button ID="btnReset" CssClass="button" runat="server" Text="Reset" TabIndex="7" AccessKey="R" /></td>
                                                 </tr>
                                                <tr>
                                                    <td style="width: 176px; height: 26px">
                                                    </td>
                                                    <td class="textbold" style="height: 26px">
                                                    </td>
                                                    <td style="width: 308px; height: 26px">
                                                        <input id="hdRecordOnCurrentPage" runat="server" style="width: 6px" type="hidden" /></td>
                                                    <td style="height: 26px">
                                                      </td>
                                                </tr>                                                                        
                                                <tr>                                            
                                                </tr>
                                               
                                             </table>
                                        </td>
                                    </tr>
                                     <tr>
                                                    <td >
                                                    </td>
                                                    <td >
                                                    </td>
                                                    <td >
                                                        </td>
                                                    <td >
                                                      </td>
                                                </tr>  
                                     <tr>
                                                     <td colspan ="4" valign ="top"  >
                                                          <table border="0" cellpadding="0" cellspacing="1" style="width:100%" class="left">
                                                             <tr>
                                                                <td colspan="4" class="redborder center" valign ="top">
                                                                    <asp:GridView EnableViewState="false" ID="grdvISP" runat="server"  AutoGenerateColumns="False" TabIndex="6" Width="100%" AllowSorting="True"  HeaderStyle-HorizontalAlign="left" RowStyle-HorizontalAlign="left"   >
                                                                         <Columns>
                                                                                   
                                                                                   <%--  <asp:BoundField DataField="ProviderName" HeaderStyle-Wrap="false"  HeaderText="Provider Name"  SortExpression="ProviderName"  >
                                                                                        <ItemStyle Wrap="True" Width="100px" />
                                                                                    </asp:BoundField>   --%>
                                                                                    <asp:BoundField DataField="Name" HeaderText="ISP Name"  SortExpression="Name"  >
                                                                                        <ItemStyle Wrap="True" Width="100px" />
                                                                                    </asp:BoundField>
                                                                                    <asp:BoundField DataField="Address" HeaderText="Address"  SortExpression="Address"  >
                                                                                        <ItemStyle Wrap="True"  Width="120px" />
                                                                                    </asp:BoundField>
                                                                                    <asp:BoundField DataField="CityName" HeaderText="City Name" SortExpression="CityName" >
                                                                                        <ItemStyle Wrap="True"  Width="60px" />
                                                                                    </asp:BoundField>
                                                                                    <asp:BoundField DataField="PinCode" HeaderText="PIN Code" SortExpression="PinCode" ItemStyle-Width="100px" HeaderStyle-Wrap ="false" />
                                                                                    <asp:BoundField DataField="CTCName" HeaderText="Contact Person " SortExpression="CTCName"    ItemStyle-Width="110px" >
                                                                                        <ItemStyle Wrap="True" />
                                                                                    </asp:BoundField>
                                                                                    <asp:BoundField DataField="Phone" HeaderText="Phone No."  SortExpression="Phone"  >
                                                                                        <ItemStyle Wrap="True" Width="90px" />
                                                                                    </asp:BoundField>
                                                                                    <asp:BoundField DataField="Fax" HeaderText="Fax No." SortExpression="Fax"  >
                                                                                        <ItemStyle Wrap="True"  Width="60px" />
                                                                                    </asp:BoundField>
                                                                                    <asp:BoundField DataField="Email" HeaderText="Email ID"  SortExpression="Email" >
                                                                                        <ItemStyle Wrap="True" Width="100px" />
                                                                                    </asp:BoundField>
                                                                                   
                                                                                    <asp:TemplateField HeaderText="Action" HeaderStyle-Width="130px" >
                                                                                    <ItemTemplate>
                                                                                        <asp:LinkButton  ID="lnkSelect" Runat="server" CssClass="LinkButtons" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "ISPID") + "|" + DataBinder.Eval(Container.DataItem, "Name")+ "|" + DataBinder.Eval(Container.DataItem, "CityName") + "|" + DataBinder.Eval(Container.DataItem, "ProviderID") + "|" + DataBinder.Eval(Container.DataItem, "ProviderName") %>'>Select</asp:LinkButton>&nbsp;
                                                                                        <a href="#" class="LinkButtons" id="linkEdit" runat="server">Edit</a>&nbsp;
                                                                                      <%--  <a href="#" class="LinkButtons" id="linkDelete" runat="server">Delete</a> --%>
                                                                                      <asp:LinkButton ID="btnDelete" runat="server" CommandName ="DeleteX" Text ="Delete" CssClass="LinkButtons"></asp:LinkButton><%--<a href="#" class="LinkButtons" id="linkDelete" runat="server">Delete</a> --%>
                                                                                        <asp:HiddenField ID="hdIspID" runat="server" Value='<%#Eval("ISPID")%>' />   
                                                                                     </ItemTemplate>
                                                                                        <ItemStyle Wrap="False" />
                                                                                   </asp:TemplateField>
                                                                                                                                
                                                                         
                                                                         </Columns>
                                                                            <AlternatingRowStyle CssClass="lightblue" />
                                                                            <RowStyle CssClass="textbold" HorizontalAlign ="left" />
                                                                            <HeaderStyle CssClass="Gridheading" ForeColor="white" Wrap ="false"   HorizontalAlign ="left"  />
                                                                            
                                                                         </asp:GridView>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="4">
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
                                                                    <asp:HiddenField ID="hdDeleteISPID" runat="server" />
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
