<%@ Page Language="VB" AutoEventWireup="false" CodeFile="PUSR_NPID.aspx.vb" Inherits="Popup_PUSR_NPID" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="head1" runat="server">
    <title>AAMS:Manage ISP Plan</title>
     <base target="_self"/>
    <script src="../JavaScript/AAMS.js" type="text/javascript"></script> 
    <script language ="javascript" type ="text/javascript" >
          function ISPPlanReset()
            {
//                document.getElementById("txtISPName").value="";         
               // document.getElementById("drpCityName").selectedIndex=0;  
                 document.getElementById("txtNpid").value="";            
                document.getElementById("lblError").innerHTML="";   
                 
                if ( document.getElementById("grdvISPPlan")!=null)       
                document.getElementById("grdvISPPlan").style.display ="none"; 
                document.getElementById("txtISPName").focus();  
                return false;
            }
    </script> 

<link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" defaultbutton="btnSearch" runat="server" defaultfocus="txtISPName">
        <table width="860px" align="left" class="border_rightred" style="height: 486px;">
            <tr>
                <td valign="top" style="height: 482px">
                    <table width="100%" align="left">
                        <tr>
                            <td valign="top" align="left">
                                <span class="menu">ISP-&gt;</span><span class="sub_menu">ISP Plan</span>
                            </td>
                        </tr>
                        <tr>
                            <td class="heading" align="center" valign="top">
                                Search ISP Plan </td>
                        </tr>
                        <tr>
                            <td valign="top" style="height: 314px">
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td align="LEFT" class="redborder">
                                            <table width="100%" border="0" align="left" cellpadding="0" cellspacing="0">
                                                                <tr>
                                        <td class="redborder center">
                                    <table border="0" cellpadding="2" cellspacing="1" style="width: 100%" class="left">
                                                                        <tr>
                                                                            <td colspan="4" class="center gap">
                                                                <asp:Label ID="lblError" runat="server" CssClass="ErrorMsg"></asp:Label></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="width: 245px">
                                                                            </td>
                                                                            <td class="textbold">
                                                                                ISP Plan &nbsp;Name</td>
                                                                            <td>
                                                                    <asp:TextBox ID="txtISPName" runat="server" CssClass="textbox" Width="208px" TabIndex="1"></asp:TextBox>
                                                                    </td>
                                                                            <td>
                                                                    <asp:Button ID="btnSearch" CssClass="button" runat="server" Text="Search" TabIndex="4" /></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="width: 245px">
                                                                            </td>
                                                                            <td class="textbold">
                                                                                City Name</td>
                                                                            <td>
                                                                                <%--<asp:TextBox ID="txtCity" runat="server" CssClass="textboxgrey" ReadOnly="True"
                                                                                    Width="208px" TabIndex="2"></asp:TextBox>--%>
                                                                    <asp:DropDownList ID="drpCityName" runat="server" CssClass="dropdownlist" Width="214px">
                                                                    </asp:DropDownList></td>
                                                                            <td>
                                                                    <asp:Button ID="btnReset" CssClass="button" runat="server" Text="Reset" TabIndex="5" /></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="width: 245px">
                                                                            </td>
                                                                            <td class="textbold">
                                                                                NPID</td>
                                                                            <td>
                                                                                <asp:TextBox ID="txtNpid" runat="server" CssClass="textbold" Width="208px" TabIndex="3"></asp:TextBox></td>
                                                                            <td>
                                                                    </td>
                                                                        </tr>
                                                                        
                                        <tr>
                                            
                                        </tr>
                                                                        <tr>
                                                                            <td colspan="4">
                                                                                <asp:GridView ID="grdvISPPlan" runat="server"  AutoGenerateColumns="False" TabIndex="6" Width="100%"    >
                                                 <Columns>
                                               
                                                            <asp:BoundField DataField="ISPName" HeaderText="ISP Name"   />
                                                            <asp:BoundField DataField="NPID" HeaderText="NPID"   />
                                                          <%--   <asp:BoundField DataField="ISPPlanID" HeaderText="NPID"   />--%>
                                                            <%--<asp:BoundField DataField="BandWidth" HeaderText="Bandwidth"   />
                                                            <asp:BoundField DataField="ContentionRatio" HeaderText="Contention Ratio"   />
                                                            <asp:BoundField DataField="InstallationCharge" HeaderText="Installation Charge"   />                                                            
                                                            <asp:BoundField DataField="MonthlyCharge" HeaderText="Monthly Charge"   />                                                            
                                                            <asp:BoundField DataField="EQPIncluded" HeaderText="Equipment Included"   />                                                            
                                                            <asp:BoundField DataField="EQPOneTimeCharge" HeaderText="Equipment One Time Charge"   />                                                            
                                                            <asp:BoundField DataField="EQPMonthlyRental" HeaderText="Equipment Monthly Rental "   />                                                            
                                                            <asp:BoundField DataField="VATPercentage" HeaderText="VAT Percentage"   />
                                                            <asp:BoundField DataField="DaysRequired" HeaderText="Delivery Time"   />                                                            
                                                            <asp:BoundField DataField="Remarks" HeaderText="Remarks"   /> --%>                                                          
                                                             <asp:TemplateField HeaderText="Action">                                                                        
                                                                            <ItemTemplate>
                                                                            <asp:LinkButton  ID="lnkSelect" Runat="server" CssClass="LinkButtons" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "NPID") + "|" + DataBinder.Eval(Container.DataItem, "ISPPlanID") %>'>Select</asp:LinkButton>
                                                                            </ItemTemplate>                                                                       
                                                                </asp:TemplateField>
                                                         
                                                                                                        
                                                 
                                                 </Columns>
                                                    <AlternatingRowStyle CssClass="lightblue" />
                                                    <RowStyle CssClass="textbold" />
                                                    <HeaderStyle CssClass="Gridheading" />
                                                    
                                                 </asp:GridView>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
              <td> <asp:Literal ID="litIspPlanId" runat="server"></asp:Literal></td>
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
