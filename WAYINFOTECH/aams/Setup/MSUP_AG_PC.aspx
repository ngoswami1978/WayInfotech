<%@ Page Language="VB" AutoEventWireup="false" CodeFile="MSUP_AG_PC.aspx.vb" Inherits="Setup_MSUP_AG_PC" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
   <title>AAMS::Travel Agency::Manage PC</title>
    <link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />
     <link rel="stylesheet" type="text/css" media="all" href="../Calender/calendar-blue.css" title="win2k-cold-1" />
    <script src="../JavaScript/AAMS.js" type="text/javascript"></script>
   <link href="../Calender/calendar-blue.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../Calender/calendar.js"></script>
<script type="text/javascript" src="../Calender/lang/calendar-en.js"></script>
<script type="text/javascript" src="../Calender/calendar-setup.js"></script>
<script type="text/javascript" src="../JavaScript/subModal.js"></script>
<link rel="stylesheet" type="text/css" href="../JavaScript/style.css" />
<link rel="stylesheet" type="text/css" href="../JavaScript/subModal.css" />
<script type="text/javascript" language="javascript"> 
function TabMethodAgencyGroup(id,total)
{   
        var ctextFront;
        var ctextBack;
        var Hcontrol;
        var HFlush;
        
        ctextFront = id.substring(0,15);        
        ctextBack = id.substring(17,26);   
       
        for(var i=0;i<total;i++)
        {
            HFlush = "0" + i;
            Hcontrol = ctextFront +  HFlush + ctextBack;
            if (document.getElementById(Hcontrol).className != "displayNone")
            {
                document.getElementById(Hcontrol).className="headingtabactive";
            }
           
        }
       
       var strChain_Code=document.getElementById("hdEnChainCode").value;
       document.getElementById(id).className="headingtab";     
      
       document.getElementById('lblPanelClick').value =id; 
       
    
      
       if (id == (ctextFront +  "00" + ctextBack))
       {   
            window.location.href="MSUP_AgencyGroup.aspx?Action=U&Chain_Code=" + strChain_Code  ;   
            return false;
       }       
       else if (id == (ctextFront +  "01" + ctextBack))
       {   
            window.location.href="MSUP_AG_CRSDetails.aspx?Action=U&Chain_Code=" + strChain_Code  ;   
            return false;                      
       }
       else if (id == (ctextFront +  "02" + ctextBack))
       {
           window.location.href="MSUP_AG_Competition.aspx?Action=U&Chain_Code=" + strChain_Code  ;   
            return false;   
       }
       else if (id == (ctextFront +  "03" + ctextBack))
       {    
            window.location.href="MSUP_AG_Staff.aspx?Action=U&Chain_Code=" + strChain_Code  ;   
            return false;   
       }
       
        else if (id == (ctextFront +  "04" + ctextBack))
       {
             window.location.href="MSUP_AG_PC.aspx?Action=U&Chain_Code=" + strChain_Code  ;   
            return false;   
       }
       
       else if (id == (ctextFront +  "05" + ctextBack))
       {
             window.location.href="MSUP_AG_Contract.aspx?Action=U&Chain_Code=" + strChain_Code  ;   
            return false;   
       }
       else if (id == (ctextFront +  "06" + ctextBack))
       {
             window.location.href="MSUP_AG_BusinessCase.aspx?Action=U&Chain_Code=" + strChain_Code  ;   
            return false;   
       }
}

</script>

</head>
<body >
    <form id="form1" runat="server"   >
        <table width="860px"  class="border_rightred left">
            <tr>
                <td class="top">
                    <table width="100%">
                        <tr>
                            <td class="top">
                                <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td class="top left" style="width:80%">
                                            <span class="menu">Travel Agency->Agency Group-></span><span class="sub_menu">PC</span>
                                        </td>
                                         <td class="right" style="width:20%">
                                             &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td class="heading center" colspan="2" style="width:100%">
                                            Manage PC</td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td class="top">
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td class="top" colspan="2">
                                            <asp:HiddenField ID="lblPanelClick" runat="server"></asp:HiddenField>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="top" style="height: 22px;width:100%" colspan="2"> 
                                            <asp:Repeater ID="theTabStrip" runat="server" >
                                                <ItemTemplate>
                                                    <asp:Button ID="Button1" width="100px" CssClass="headingtabactive" runat="server" Text="<%# Container.DataItem %>" />
                                                   
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </td>
                                        
                                    </tr>
                                   <tr>
                                        <td class="redborder top" colspan="2" style="width: 100%">
                                            <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td class="center TOP">
                                                        <asp:Label ID="lblError" CssClass="ErrorMsg" runat="server" EnableViewState="False"></asp:Label>
                                                    </td>
                                                </tr>
                                                   <tr>
                                                    <td class="center TOP">                                                        
                                                    </td>
                                                </tr>
                                                  <tr>
                                                    <td  align ="right"  style ="width:800px"  >
                                                        <asp:Button accessKey="e"  id="btnExport" tabIndex=2 runat="server" CssClass="button" Text="Export"></asp:Button>
                                                    </td>
                                                </tr>
                                                   <tr>
                                                    <td class="center TOP" style ="height:5px:">
                                                        
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="top" style="width: 100%;">
                                                         <asp:GridView ID="gvPcInstallation" runat="server"  EnableViewState ="true" AutoGenerateColumns="False" TabIndex="5" width="1500px" AllowSorting="false"  HeaderStyle-ForeColor="white"  HeaderStyle-HorizontalAlign="left"  RowStyle-HorizontalAlign="left" >
                                                                                         <Columns>                                               
                                                                                                          
                                                                                                              <asp:BoundField DataField="LCODE" HeaderText="LCode"  SortExpression="LCODE" ItemStyle-Width="5%"/>
                                                                <asp:BoundField DataField="AGENCY" HeaderText="Agency Name" SortExpression="AGENCY" ItemStyle-Wrap="false"  />
                                                                                                                <asp:BoundField DataField="DATE" HeaderText="Date" ItemStyle-Wrap="false"  SortExpression="DATE" ItemStyle-Width="5%"/>
                                                                                                          
                                                                                                             <asp:BoundField DataField="CPUTYPE" HeaderText="CPU Type"  SortExpression ="CPUTYPE"   ItemStyle-Width="3%">
                                                                                                                 <ItemStyle Wrap="False" />
                                                                                                                 <HeaderStyle Wrap="False" />
                                                                                                             </asp:BoundField>                                                                                                                                                                     
                                                                                                             <asp:BoundField DataField="CPUNO" HeaderText="CPU No." SortExpression ="CPUNO" ItemStyle-Width="3%">
                                                                                                                 <ItemStyle Wrap="True" />
                                                                                                                 <HeaderStyle Wrap="False" />
                                                                                                             </asp:BoundField>                                                                                                                                                                     
                                                                                                             <asp:BoundField DataField="MONTYPE" HeaderText="Monitor Type"  SortExpression ="MONTYPE" ItemStyle-Width="4%">
                                                                                                                 <ItemStyle Wrap="False" />
                                                                                                                 <HeaderStyle Wrap="true"  />
                                                                                                             </asp:BoundField>  
                                                                                                             <asp:BoundField DataField="MONNO" HeaderText="Monitor No."  SortExpression ="MONNO" ItemStyle-Width="3%">
                                                                                                                 <ItemStyle Wrap="false"  />
                                                                                                                 <HeaderStyle Wrap="true"  />
                                                                                                             </asp:BoundField>  
                                                                                                             <asp:BoundField DataField="KBDTYPE" HeaderText="Kbd Type"  SortExpression ="KBDTYPE"  ItemStyle-Width="3%">
                                                                                                                  <ItemStyle Wrap="false"  />
                                                                                                                 <HeaderStyle Wrap="true"  />
                                                                                                             </asp:BoundField>  
                                                                                                             <asp:BoundField DataField="KBDNO" HeaderText="Kbd No."  SortExpression ="KBDNO" ItemStyle-Width="3%">
                                                                                                                  <ItemStyle Wrap="false"  />
                                                                                                                 <HeaderStyle Wrap="true"  />
                                                                                                             </asp:BoundField>  
                                                                                                             <asp:BoundField DataField="MSETYPE" HeaderText="Mse Type"  SortExpression ="MSETYPE"  ItemStyle-Width="3%">
                                                                                                                  <ItemStyle Wrap="false"  />
                                                                                                                 <HeaderStyle Wrap="true"  />
                                                                                                             </asp:BoundField>  
                                                                                                             <asp:BoundField DataField="MSENO" HeaderText="Mse No." SortExpression ="MSENO"  ItemStyle-Width="3%">
                                                                                                                  <ItemStyle Wrap="false"  />
                                                                                                                 <HeaderStyle Wrap="true"  />
                                                                                                             </asp:BoundField>     
                                                                                                             <asp:BoundField DataField="OrderNumber" HeaderText="Order No." SortExpression ="OrderNumber"  ItemStyle-Width="3%">
                                                                                                                 <ItemStyle Wrap="false"  />
                                                                                                                 <HeaderStyle Wrap="true"  />
                                                                                                             </asp:BoundField>     
                                                                                                             <asp:BoundField DataField="REMARKS" HeaderText="Remarks" SortExpression ="REMARKS" ItemStyle-Width="10%">
                                                                                                                 <ItemStyle Wrap="False" />
                                                                                                                 <HeaderStyle Wrap="False" />
                                                                                                             </asp:BoundField>     
                                                                                                             <asp:BoundField DataField="CHALLANDATE" HeaderText="Challan Date" SortExpression ="CHALLANDATE"  ItemStyle-Width="3%">
                                                                                                                  <ItemStyle Wrap="false"  />
                                                                                                                 <HeaderStyle Wrap="true"  />
                                                                                                             </asp:BoundField>     
                                                                                                             <asp:BoundField DataField="CHALLANNUMBER" HeaderText="Challan No." SortExpression ="CHALLANNUMBER" ItemStyle-Width="3%">
                                                                                                                  <ItemStyle Wrap="false"  />
                                                                                                                 <HeaderStyle Wrap="true"  />
                                                                                                             </asp:BoundField> 
                                                                                                          <asp:BoundField DataField="Employee_Name" HeaderText="Logged By" SortExpression ="Employee_Name" ItemStyle-Width="5%">
                                                                                                                  <ItemStyle Wrap="false"  />
                                                                                                                 <HeaderStyle Wrap="true"  />
                                                                                                             </asp:BoundField>  
                                                                                                             <asp:BoundField DataField="LoggedDateTime" HeaderText="Logged Date Time"  SortExpression ="LoggedDateTime" ItemStyle-Width="5%">
                                                                                                                 <ItemStyle Wrap="false"  />
                                                                                                                 <HeaderStyle Wrap="true"  />
                                                                                                             </asp:BoundField>      
                                                                                                             <asp:BoundField DataField="CHALLANSTATUS" HeaderText="Challan Status" SortExpression ="CHALLANSTATUS" ItemStyle-Width="3%">
                                                                                                                 <ItemStyle Wrap="false"  />
                                                                                                                 <HeaderStyle Wrap="true"  />
                                                                                                             </asp:BoundField>   
                                                                                                             <asp:BoundField DataField="CDRNO" HeaderText="Cdr No."   SortExpression ="CDRNO" ItemStyle-Width="3%">
                                                                                                                  <ItemStyle Wrap="false"  />
                                                                                                                 <HeaderStyle Wrap="true"  />
                                                                                                             </asp:BoundField>
                                                                                                             <asp:BoundField DataField="LastModifiedDate" HeaderText="Last Modified Date"   SortExpression ="LastModifiedDate" ItemStyle-Width="3%">
                                                                                                                  <ItemStyle Wrap="false"  />
                                                                                                                 <HeaderStyle Wrap="true"  />
                                                                                                             </asp:BoundField>                                                                                                                  
                                                                                                             
                                                                                                         </Columns>
                                                                                                        <AlternatingRowStyle CssClass="lightblue" />
                                                                                                        <RowStyle CssClass="textbold" Wrap="false"   />
                                                                                                        <HeaderStyle CssClass="Gridheading" Wrap ="true"  />   
                                                                                                        
                                                                                         </asp:GridView>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="top" style="width: 100%;">
                                                     <asp:Panel ID="pnlPaging" runat="server" Visible="false" Width="50%">
                                                      <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                      <tr class="paddingtop paddingbottom"  >                                                                                                                                                
                                                                          <td style="width: 30%" class="left" nowrap="true"><span  class="textbold"><b>&nbsp;No. of records found</b></span>&nbsp;&nbsp;<asp:TextBox ID="txtTotalRecordCount" runat ="server"  Width="105px" CssClass="textboxgrey" ReadOnly="True" ></asp:TextBox></td>
                                                                          <td style="width: 25%" class="right">                                                                             
                                                                              <asp:LinkButton ID="lnkPrev" CssClass="LinkButtons" runat="server" CommandName="Prev"><< Prev</asp:LinkButton></td>
                                                                          <td style="width: 20%" class="center">
                                                                              <span class ="textbold" ><b>Page No</b></span>&nbsp;&nbsp;<asp:DropDownList ID="ddlPageNumber" Width="70px" CssClass="dropdownlist" runat="server" AutoPostBack="True" >
                                                                              </asp:DropDownList></td>
                                                                          <td style="width: 25%" class="left">
                                                                              <asp:LinkButton ID="lnkNext" runat="server" CssClass="LinkButtons" CommandName="Next">Next >></asp:LinkButton></td>
                                                                      </tr>
                                                                  </table></asp:Panel>
                                                                  <input type="hidden" runat="server" id="hdEnChainCode" style="width: 21px" />
                                                                      <input type="hidden" runat="server" id="hdChainCode" style="width: 21px" />
                                                                      <input type="hidden" runat="server" id="hdRecordOnCurrentPage" style="width: 21px" />
                                                                      
                                                       
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    &nbsp;
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
