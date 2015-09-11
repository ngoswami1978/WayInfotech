<%@ Page Language="VB" MaintainScrollPositionOnPostback="true"  AutoEventWireup="false" CodeFile="MSSR_ISPPlan.aspx.vb" Inherits="ISP_MSSR_ISPPlan" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Search ISP Plan</title>
    <script src="../JavaScript/AAMS.js" type="text/javascript"></script>
     <script language="javascript" type="text/javascript">
          function SelectFunction(str3)
        {   
          // alert(str3);
            var pos=str3.split('|'); 
            
          
           if (window.opener.document.forms['form1']['txtNPID']!=null)
            {
            window.opener.document.forms['form1']['txtNPID'].value=pos[0];
            }
            if (window.opener.document.forms['form1']['txtNpid']!=null)
            {
            window.opener.document.forms['form1']['txtNpid'].value=pos[0];
            }
             if (window.opener.document.forms['form1']['hdNPID']!=null)
            {
            window.opener.document.forms['form1']['hdNPID'].value=pos[0];
            }
            if (window.opener.document.forms['form1']['txtPlainId']!=null)
            {
            window.opener.document.forms['form1']['txtPlainId'].value=pos[0];
            }
            if (window.opener.document.forms['form1']['hdISPid']!=null)
            {
            window.opener.document.forms['form1']['hdISPid'].value=pos[1];
            }
              if (window.opener.document.forms['form1']['hdIspPlanId']!=null)
            {
            window.opener.document.forms['form1']['hdIspPlanId'].value=pos[1];
            }
           
            if (window.opener.document.forms['form1']['txtNEWNPID']!=null)
            {
                  window.opener.document.forms['form1']['txtNEWNPID'].value=pos[0];
                  if (window.opener.document.forms['form1']['hdIspPlanId']!=null)
                  {
                    window.opener.document.forms['form1']['hdIspPlanId'].value=pos[1];
                  }
            }
           
            window.close();
       } 
    function EditFunction(CheckBoxObj)
    {         
    //alert(CheckBoxObj);
          window.location.href="MSUP_ISPPlan.aspx?Action=U|"+CheckBoxObj;               
          return false;
    }
    
        function InsertISP()
        {
        window.location.href="MSUP_ISPPlan.aspx?Action=I";
        return false;
        }
        
    function DeleteFunction(ISPPlanID)
    {   
//        if (confirm("Are you sure you want to delete?")==true)
//        {   
//        
//          window.location.href="MSSR_ISPPlan.aspx?Action=D|"+ ISPId;                   
//          return false;
//        }
             if (confirm("Are you sure you want to delete?")==true)
                  {        
                    document.getElementById("hdDeleteISPPlanID").value= ISPPlanID ;   
                  }
                else
                {
                 document.getElementById("hdDeleteISPPlanID").value="";
                 return false;
                }


    }
     function DeleteFunction2(ISPId)
    {   
        if (confirm("Are you sure you want to delete?")==true)
        {   
        
          window.location.href="MSSR_ISPPlan.aspx?PopUp=T&Action=D|"+ ISPId;                   
          return false;
        }
    }
     function DeleteFunction3(ISPId,Lcode,IspName)
    {   
        if (confirm("Are you sure you want to delete?")==true)
        {   
      
        
        
          window.location.href="MSSR_ISPPlan.aspx?PopUp=T&" + "Lcode=" + Lcode + "&IspName=" + IspName + "&Action=D|"+ ISPId;                   
          return false;
        }
    }
   
    </script>
    <link href="../CSS/AAMS.css" type="text/css" rel="stylesheet" />
</head>
<body >
    <form id="frmISPPlan" runat="server" defaultbutton="btnSearch" defaultfocus ="drpIspProvider">
     <table width="860px" align="left" class="border_rightred">
            <tr>
                <td valign="top"  style="width:860px;">
                    <table width="100%" align="left">
                        <tr>
                            <td valign="top"  align="left" >
                            <span class="menu">ISP-&gt;</span><span class="sub_menu">ISP Plan Information Search</td>
                        </tr>
                        <tr>
                            <td class="heading" align="center" valign="top" style ="width:840px;" >
                               <span style="font-family: Microsoft Sans Serif ;" > Search ISP Plan Information</span></td>                        </tr>
                        <tr>
                            <td valign="top">
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td class="redborder" valign="top"  style="width:860px;">
                                    <table border="0" cellpadding="2" cellspacing="1">
                                                                        <tr>
                                                                            <td colspan="4" class="center gap">
                                                                <asp:Label ID="lblError" runat="server" CssClass="ErrorMsg"></asp:Label></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="width: 167px">
                                                                            </td>
                                                                            <td class="textbold" style="width: 96px">
                                                                                Provider Name</td>
                                                                            <td style="width: 309px"><asp:DropDownList  onkeyup="gotop(this.id)"  ID="drpIspProvider" runat="server" CssClass="dropdownlist" Width="214px" TabIndex="1">
                                                                            </asp:DropDownList></td>
                                                                            <td>
                                                                    <asp:Button ID="btnSearch" CssClass="button" runat="server" Text="Search" TabIndex="4" AccessKey="A" /></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="width: 167px; height: 26px;">
                                                                            </td>
                                                                            <td class="textbold" style="height: 26px; width: 96px;">
                                                                                City Name</td>
                                                                            <td style="height: 26px; width: 309px;">
                                                                    <asp:DropDownList  onkeyup="gotop(this.id)"  ID="drpCityName" runat="server" CssClass="dropdownlist" Width="214px" TabIndex="2">
                                                                    </asp:DropDownList></td>
                                                                            <td style="height: 26px">
                                                                    <asp:Button ID="btnNew" CssClass="button" runat="server" Text="New" TabIndex="5" AccessKey="N" /></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="width: 167px">
                                                                            </td>
                                                                            <td class="textbold" style="width: 96px">
                                                                                NPID</td>
                                                                            <td style="width: 309px">
                                                                                <asp:TextBox ID="txtNpid" runat="server" CssClass="textbox" Width="208px" TabIndex="3"></asp:TextBox></td>
                                                                            <td>
                                                <asp:Button ID="btnExport" runat="server" CssClass="button" TabIndex="6" Text="Export" AccessKey="E" /></td>
                                                                        </tr>
                                        <tr>
                                            <td style="width: 167px">
                                            </td>
                                            <td class="textbold" style="width: 96px">
                                            </td>
                                            <td style="width: 309px">
                                            </td>
                                            <td>
                                                                    <asp:Button ID="btnReset" CssClass="button" runat="server" Text="Reset" TabIndex="7" AccessKey="R" /></td>
                                        </tr>
                                                                                                                
                                       <tr>
                                        <td colspan ="4" style ="width:4px;">&nbsp;</td>
                                       </tr>                                 
                                       </table>
                                            <input id="hdRecordOnCurrentPage" runat="server" style="width: 6px" type="hidden" /></td>
                                    </tr>
                                    
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
             <tr>                        
              <td valign="top" style="padding-left:4px;" >
              <table  width="1840px" border="0" align="left" cellpadding="0" cellspacing="0"> 
                      <tr>  
                            <td class="redborder" valign ="top"> <asp:GridView EnableViewState="false" ID="grdvISPPlan" runat="server"  AutoGenerateColumns="False" TabIndex="4" Width="1850px" AllowSorting="True"   HeaderStyle-HorizontalAlign="Left"  >
                                                         <Columns>
                                                                     <asp:BoundField DataField="Name" HeaderText="ISP Name"  SortExpression="Name">
                                                                        <ItemStyle Wrap="True"  Width ="100px" />
                                                                        <HeaderStyle Wrap="False" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="Address" HeaderText="Address"  SortExpression="Address" ItemStyle-Width="250px" HeaderStyle-Wrap="false"  ItemStyle-Wrap="true"  />
                                                                     <asp:BoundField DataField="CityName" HeaderText="City"  SortExpression="CityName" ItemStyle-Width="120px" HeaderStyle-Wrap="false" ItemStyle-Wrap="true"  />
                                                                      <asp:BoundField DataField="CTCName" HeaderText="Contact Person"  SortExpression="CTCName" ItemStyle-Width="120px" HeaderStyle-Wrap="false" ItemStyle-Wrap="true"  />
                                                                         <asp:BoundField DataField="Phone" HeaderText="Contact No"  SortExpression="Phone" ItemStyle-Width="80px" HeaderStyle-Wrap="false" ItemStyle-Wrap="false"  />
                                                                    <asp:BoundField DataField="NPID" HeaderText="NPID"  SortExpression="NPID" ItemStyle-Width="120px" HeaderStyle-Wrap="False" ItemStyle-Wrap="false"  />
                                                                    <asp:BoundField DataField="BandWidth" HeaderText="Bandwidth" SortExpression="BandWidth" ItemStyle-Width="70px" HeaderStyle-Wrap="False" />
                                                                    <asp:BoundField DataField="ContentionRatio" HeaderText="Contention Ratio" SortExpression ="ContentionRatio" ItemStyle-Width="70px" HeaderStyle-Wrap="False"  ItemStyle-Wrap="false"  />
                                                                    <asp:BoundField DataField="InstallationCharge" HeaderText="Installation Charge" SortExpression="InstallationCharge" ItemStyle-Width="70px"  HeaderStyle-Wrap="False"  ItemStyle-Wrap="false"  />                                                            
                                                                    <asp:BoundField DataField="MonthlyCharge" HeaderText="Monthly Charge"  SortExpression="MonthlyCharge"  ItemStyle-Width="70px" HeaderStyle-Wrap="False"  ItemStyle-Wrap="false"  /> 
                                                                    <asp:TemplateField HeaderText="Equipment Included" SortExpression="EQPIncluded" ItemStyle-Width="70px" HeaderStyle-Wrap="False"  ItemStyle-Wrap="false" >
                                                                    <ItemTemplate>
                                                                    <asp:Label ID="lblEqpIncluded" runat="server" Text='<%#Eval("EQPIncluded") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    </asp:TemplateField>  
                                                                    <asp:BoundField DataField="EQPOneTimeCharge" HeaderText="Equipment One Time Charge" SortExpression="EQPOneTimeCharge" ItemStyle-Width="70px" HeaderStyle-Wrap="False"  ItemStyle-Wrap="false"  />                                                            
                                                                    <asp:BoundField DataField="EQPMonthlyRental" HeaderText="Equipment Monthly Rental " SortExpression="EQPMonthlyRental" ItemStyle-Width="70px" HeaderStyle-Wrap="False"  ItemStyle-Wrap="false"  />                                                            
                                                                    <asp:BoundField DataField="TotalSum" HeaderText="Total Sum" SortExpression="TotalSum"  ItemStyle-Width="70px" HeaderStyle-Wrap="False"  ItemStyle-Wrap="false"   />                                                            
                                                                    <asp:BoundField DataField="DaysRequired" HeaderText="Delivery Time" SortExpression="DaysRequired"  ItemStyle-Width="70px" HeaderStyle-Wrap="False"   ItemStyle-Wrap="false"  /> 
                                                                    <asp:TemplateField HeaderText="Action" ItemStyle-Width="140px"  HeaderStyle-Wrap="False" ItemStyle-Wrap="false">
                                                                    <ItemTemplate >
                                                                    <asp:LinkButton  ID="lnkSelect" Runat="server" CssClass="LinkButtons" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "NPID") + "|" +  DataBinder.Eval(Container.DataItem, "ISPPlanID") + "|" +  DataBinder.Eval(Container.DataItem, "ProviderID")+ "|" +  DataBinder.Eval(Container.DataItem, "ProviderName") %>'>Select</asp:LinkButton>&nbsp;<a href="#" class="LinkButtons" id="linkEdit" runat="server">Edit</a>&nbsp;&nbsp;<asp:LinkButton ID="btnDelete" runat="server" CommandName ="DeleteX" Text ="Delete" CssClass="LinkButtons"></asp:LinkButton><%--<a href="#" class="LinkButtons" id="linkDelete" runat="server">Delete</a> <%--<a href="#" class="LinkButtons" id="linkDelete" runat="server">Delete</a>&nbsp;&nbsp;--%>
                                                                    <asp:HiddenField ID="ISPPlanID" runat="server" Value='<%#Eval("ISPPlanID")%>' />  
                                                                     </ItemTemplate>
                                                                        <ItemStyle Wrap="False" />
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
             <tr>
                <td>
                      <asp:Panel ID="pnlPaging" runat="server" Visible="false" Width="840px">
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
                                                                          </table>
                     </asp:Panel>
                                                                                           
                </td>
             </tr>
             <tr>
                <td>
                      <asp:HiddenField ID="hdDeleteISPPlanID" runat="server" />
                  <asp:TextBox ID="txtRecordOnCurrentPage" runat="server" CssClass="textboxgrey" Visible="false"
                                                                                                    Width="73px"></asp:TextBox>
                </td>
             </tr>
        </table>
    </form>
</body>
</html>
