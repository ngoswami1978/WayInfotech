<%@ Page Language="VB" AutoEventWireup="false" CodeFile="INCUP_BacseDetails_Remarks.aspx.vb"
    ValidateRequest="false" Inherits="INCUP_BacseDetails_Remarks" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>AAMS::Incentive::Manage Bussiness Case</title>
    <link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" media="all" href="../Calender/calendar-blue.css"
        title="win2k-cold-1" />

    <script src="../JavaScript/AAMS.js" type="text/javascript"></script>

    <link href="../Calender/calendar-blue.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="../Calender/calendar.js"></script>

    <script type="text/javascript" src="../Calender/lang/calendar-en.js"></script>

    <script type="text/javascript" src="../Calender/calendar-setup.js"></script>

    <script type="text/javascript" src="../JavaScript/subModal.js"></script>

    <link rel="stylesheet" type="text/css" href="../JavaScript/style.css" />
    <link rel="stylesheet" type="text/css" href="../JavaScript/subModal.css" />
<script type="text/javascript" language="javascript">

function TabMethodAgencySubGroup(id,total)
{  

try
{
        var ctextSubFront;
        var ctextSubBack;
        var HSubcontrol;
        var HSubFlush;
        var strRefreshAction="";
         
         try
        {
            strRefreshAction=document.getElementById("hdRefreshAction").value;
        }
        catch(err){}
         
        ctextSubFront = id.substring(0,18);        
        ctextSubBack = id.substring(20,29);   
       
       
       
       
        for(var i=0;i<total;i++)
        {
            HSubFlush = "0" + i;
            HSubcontrol = ctextSubFront +  HSubFlush + ctextSubBack;
            if (document.getElementById(HSubcontrol).className != "displayNone")
            {
                document.getElementById(HSubcontrol).className="headingtabactive";
            }
           
        }
       
       var strChain_Code="";
       var strBC_ID="";
       var strACTION="";
       
        strChain_Code = document.getElementById('hdEnChainCode').value;
        strBC_ID=document.getElementById("hdBcID").value;
         strACTION=document.getElementById("hdAction").value;
         
       document.getElementById(id).className="headingtab";     
      
       document.getElementById('lblSubPanelClick').value =id; 
       
    
      
        if (id == (ctextSubFront +  "00" + ctextSubBack))
       {   
            location.href="INCUP_BacseDetails.aspx?TabID=0&Chain_Code=" + strChain_Code + "&BCaseID="+strBC_ID + "&Action=" +strACTION + "&RefreshAction=" +  strRefreshAction ; 
            return false;  
       } 
       
       else if (id == (ctextSubFront +  "01" + ctextSubBack))
       {   
           location.href="INCUP_BacseDetails.aspx?TabID=1&Chain_Code=" + strChain_Code + "&BCaseID="+strBC_ID  + "&Action=" +strACTION + "&RefreshAction=" +  strRefreshAction ; 
            return false;                    
       }
        else if (id == (ctextSubFront +  "02" + ctextSubBack))
       {   
           location.href="INCUP_BacseDetails.aspx?BindGrid=T&TabID=2&Chain_Code=" + strChain_Code + "&BCaseID="+strBC_ID  + "&Action=" +strACTION + "&RefreshAction=" +  strRefreshAction ; 
            return false;                    
       }
             
       else if (id == (ctextSubFront +  "03" + ctextSubBack))
       {   
            document.getElementById('<%=hdTabID.ClientId %>').value='0';
            document.getElementById('<%=pnlRemarks1.ClientID %>').className='displayNone';
           document.getElementById('<%=pnlApprovars.ClientId %>').className='displayNone';
           document.getElementById('<%=pnlRemarks.ClientID %>').className='displayBlock';
           return false;                      
       }
       
        else if (id == (ctextSubFront +  "04" + ctextSubBack))
       {   
            document.getElementById('<%=hdTabID.ClientId %>').value='1';
            document.getElementById('<%=pnlRemarks.ClientID %>').className='displayNone';
            document.getElementById('<%=pnlApprovars.ClientID %>').className='displayNone';
            document.getElementById('<%=pnlRemarks1.ClientID %>').className='displayBlock';
            return false;                      
       }
       
       
        else if (id == (ctextSubFront +  "05" + ctextSubBack))
       {   
           
            document.getElementById('<%=hdTabID.ClientId %>').value='2';
            document.getElementById('<%=pnlRemarks.ClientID %>').className='displayNone';
            document.getElementById('<%=pnlApprovars.ClientID %>').className='displayBlock';
            document.getElementById('<%=pnlRemarks1.ClientID %>').className='displayNone';
            return false;                      
       }
       
       
     }  catch(err)
    			{
    			}  
      
}


</script>
  

</head>
<body >
    <form id="form1" runat="server">
        
       
        <table width="1000px" class="border_rightred left">
            <tr>
                <td class="top" width="1000px">
                    <table width="100%">
                        <tr>
                            <td class="top">
                                <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td class="top left" style="width: 80%">
                                            <span class="menu">Incentive-></span><span class="sub_menu"> Bussiness Case </span>
                                        </td>
                                        <td class="right" style="width: 20%">
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td class="heading center" colspan="2" style="width: 100%">
                                            Manage Bussiness Case
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td class="top">
                                <table width="100%" border="0" cellspacing="0" onload='tabSelection();' cellpadding="0">
                                    <tr>
                                        <td class="top" colspan="2">
                                            <asp:HiddenField ID="lblPanelClick" runat="server"></asp:HiddenField>
                                            <asp:HiddenField ID="lblSubPanelClick" runat="server"></asp:HiddenField>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="top" style="height: 22px; width: 100%" colspan="2">
                                            <asp:Repeater ID="theTabStrip" runat="server">
                                                <ItemTemplate>
                                                    <asp:Button ID="Button1" Width="100px" CssClass="headingtabactive" runat="server"
                                                        Text="<%# Container.DataItem %>" />
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top">
                                            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                <tr>
                                                    <td valign="top" style="width: 830px; padding-left: 7px; padding-bottom: 7px;">
                                                        
                                                                <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                                                    <tr>
                                                                        <td valign="top" style="height: 5px">
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="top" style="height: 22px; width: 100%" colspan="2">
                                                                            <asp:Repeater ID="theTabSubStrip" runat="server">
                                                                                <ItemTemplate>
                                                                                    <asp:Button ID="Button2" Width="100px" CssClass="headingtabactive" runat="server"
                                                                                        Text="<%# Container.DataItem %>" />
                                                                                </ItemTemplate>
                                                                            </asp:Repeater>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="redborder top" style="width: 100%;">
                                                                            <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                                                                <tr>
                                                                                    <td class="center TOP" style="width: 830px;">
                                                                                        <asp:Label ID="lblError" CssClass="ErrorMsg" runat="server" EnableViewState="False"></asp:Label>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td class="top" style="width: 100%;">
                                                                                        <table width="1000" border="0" class="left" cellpadding="2" cellspacing="0">
                                                                                            
                                                                                            <tr>
                                                                                                <td style="width: 1186px">
                                                                                                    <asp:Panel ID="pnlRemarks" runat="server" CssClass="displayNone">
                                                                                                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                                                                            <tr>
                                                                                                                <td >
                                                                                                                </td>
                                                                                                                <td style="width: 137px">
                                                                                                                </td>
                                                                                                                <td  style="width: 722px">
                                                                                                                </td>
                                                                                                                <td >
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                                <td >
                                                                                                                </td>
                                                                                                                <td  colspan="2" rowspan="5">
                                                                                                                    <span class="Mandatory"></span>
                                                                                                                    <asp:TextBox ID="txtRemarks" runat="server" CssClass="textbox" MaxLength="1000" TabIndex="3"
                                                                                                                        Width="889px" Height="81px" TextMode="MultiLine"></asp:TextBox>
                                                                                                                    &nbsp; &nbsp;
                                                                                                                </td>
                                                                                                                <td >
                                                                                                                    <asp:Button ID="btnSaveRemarks" runat="server" AccessKey="N" CssClass="button" TabIndex="3"
                                                                                                                        Text="Save" /></td>
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                                <td  style="height: 5pt;">
                                                                                                                </td>
                                                                                                                <td >
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                                <td class="" style="height: 22px">
                                                                                                                </td>
                                                                                                                <td class="" valign="top" style="height: 22px">
                                                                                                                    <asp:Button ID="btnResetRemarks" runat="server" AccessKey="N" CssClass="button" TabIndex="3"
                                                                                                                        Text="Reset" /></td>
                                                                                                            </tr>
                                                                                                            
                                                                                                           
                                                                                                            <tr>
                                                                                                                <td style="height: 11pt;">
                                                                                                                </td>
                                                                                                                <td class="" colspan="2" valign="top">
                                                                                                                </td>
                                                                                                                <td class="" valign="top">
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                                <td >
                                                                                                                </td>
                                                                                                                <td class="" colspan="2">
                                                                                                                    &nbsp;</td>
                                                                                                                <td >
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                                <td style="height: 7pt;">
                                                                                                                </td>
                                                                                                                <td  colspan="2" valign="top">
                                                                                                                    <asp:GridView ID="grdvRemarks" AutoGenerateColumns="false" runat="server" Width="894px">
                                                                                                                        <Columns>
                                                                                                                            <asp:TemplateField HeaderText="Given by">
                                                                                                                                <ItemTemplate>
                                                                                                                                    <%#Eval("EmployeeName")%>
                                                                                                                                </ItemTemplate>
                                                                                                                                <HeaderStyle Width="15%" />
                                                                                                                            </asp:TemplateField>
                                                                                                                            <asp:TemplateField HeaderText="Date" HeaderStyle-Width="15%">
                                                                                                                                <ItemTemplate>
                                                                                                                                    <%#Eval("REMARKS_DTTI")%>
                                                                                                                                </ItemTemplate>
                                                                                                                            </asp:TemplateField>
                                                                                                                            <asp:TemplateField HeaderText="Remarks" HeaderStyle-Width="90%">
                                                                                                                                <ItemTemplate>
                                                                                                                                    <%#Eval("BC_REMARKS")%>
                                                                                                                                </ItemTemplate>
                                                                                                                            </asp:TemplateField>
                                                                                                                        </Columns>
                                                                                                                        <FooterStyle CssClass="Gridheading" ForeColor="White" />
                                                                                                                        <HeaderStyle CssClass="Gridheading" ForeColor="White" />
                                                                                                                        <AlternatingRowStyle CssClass="lightblue" />
                                                                                                                    </asp:GridView>
                                                                                                                </td>
                                                                                                                <td >
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                            
                                                                                                        </table>
                                                                                                    </asp:Panel>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td style="width: 1186px">
                                                                                                    <asp:Panel ID="pnlRemarks1" runat="server" CssClass="displayNone">
                                                                                                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                                                                            <tr>
                                                                                                                <td >
                                                                                                                </td>
                                                                                                                <td  style="width: 137px">
                                                                                                                </td>
                                                                                                                <td style="width: 722px">
                                                                                                                </td>
                                                                                                                <td >
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                                <td >
                                                                                                                </td>
                                                                                                                <td  colspan="2" rowspan="5">
                                                                                                                    <span class="Mandatory"></span>
                                                                                                                    <asp:TextBox ID="txtMktRemarks" runat="server" CssClass="textbox" MaxLength="1000"
                                                                                                                        TabIndex="3" Width="889px" Height="81px" TextMode="MultiLine"></asp:TextBox>
                                                                                                                    &nbsp; &nbsp;&nbsp;
                                                                                                                </td>
                                                                                                                <td >
                                                                                                                    <asp:Button ID="btnMktRemarksSave" runat="server" AccessKey="N" CssClass="button"
                                                                                                                        TabIndex="3" Text="Save" /></td>
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                                <td style="height: 5pt;">
                                                                                                                </td>
                                                                                                                <td >
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                                <td class="">
                                                                                                                </td>
                                                                                                                <td class="" valign="top">
                                                                                                                    <asp:Button ID="btnResetMarketRemarks" runat="server" AccessKey="N" CssClass="button"
                                                                                                                        TabIndex="3" Text="Reset" /></td>
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                                
                                                                                                            <tr>
                                                                                                                <td style="height: 11pt;">
                                                                                                                </td>
                                                                                                                <td class="" colspan="2" valign="top">
                                                                                                                </td>
                                                                                                                <td >
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                                <td >
                                                                                                                </td>
                                                                                                                <td class="" colspan="2">
                                                                                                                    <asp:GridView ID="grdvMarketRemarks" AutoGenerateColumns="false" runat="server" Width="894px">
                                                                                                                        <Columns>
                                                                                                                            <asp:TemplateField HeaderText="Given by">
                                                                                                                                <ItemTemplate>
                                                                                                                                    <%#Eval("EmployeeName") %>
                                                                                                                                </ItemTemplate>
                                                                                                                                <HeaderStyle Width="15%" />
                                                                                                                            </asp:TemplateField>
                                                                                                                            <asp:TemplateField HeaderText="Date">
                                                                                                                                <ItemTemplate>
                                                                                                                                    <%#Eval("REMARKS_DTTI")%>
                                                                                                                                </ItemTemplate>
                                                                                                                                <HeaderStyle Width="15%" />
                                                                                                                            </asp:TemplateField>
                                                                                                                            <asp:TemplateField HeaderText="Remarks">
                                                                                                                                <ItemTemplate>
                                                                                                                                    <%#Eval("BC_REMARKS")%>
                                                                                                                                </ItemTemplate>
                                                                                                                                <HeaderStyle Width="90%" />
                                                                                                                            </asp:TemplateField>
                                                                                                                        </Columns>
                                                                                                                        <FooterStyle CssClass="Gridheading" ForeColor="White" />
                                                                                                                        <HeaderStyle CssClass="Gridheading" ForeColor="White" />
                                                                                                                        <AlternatingRowStyle CssClass="lightblue" />
                                                                                                                    </asp:GridView>
                                                                                                                </td>
                                                                                                                <td >
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                                <td style="height: 7pt;">
                                                                                                                </td>
                                                                                                                <td class="" colspan="2" valign="top">
                                                                                                                </td>
                                                                                                                <td >
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                            
                                                                                                        </table>
                                                                                                    </asp:Panel>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td style="width: 1186px">
                                                                                                    <asp:Panel ID="pnlApprovars" runat="server" CssClass="displayNone">
                                                                                                        <asp:GridView AutoGenerateColumns="false" ShowFooter="false" ShowHeader="true" ID="grdApprovars"
                                                                                                            runat="server" Width="703px">
                                                                                                            <Columns>
                                                                                                                <asp:TemplateField HeaderText="Level" ItemStyle-Width="120px">
                                                                                                                    <ItemTemplate>
                                                                                                                        <asp:Label ID="lblLevel" runat="server"></asp:Label>
                                                                                                                    </ItemTemplate>
                                                                                                                </asp:TemplateField>
                                                                                                                <asp:TemplateField HeaderText="Approvers Name" ItemStyle-Width="300px">
                                                                                                                    <ItemTemplate>
                                                                                                                        <asp:Label ID="lblApprovars" runat="server" Text='<%#Eval("EMPLOYEE_NAME") %>'></asp:Label>
                                                                                                                    </ItemTemplate>
                                                                                                                </asp:TemplateField>
                                                                                                                <asp:TemplateField HeaderText="Status" ItemStyle-Width="300px">
                                                                                                                    <ItemTemplate>
                                                                                                                        <asp:Label ID="lblApprovarsStatus" runat="server" Text='<%#Eval("APPROVAL_STATUS_NAME") %>'></asp:Label>
                                                                                                                    </ItemTemplate>
                                                                                                                </asp:TemplateField>
                                                                                                            </Columns>
                                                                                                            <RowStyle CssClass="textbold" />
                                                                                                            <HeaderStyle CssClass="Gridheading" ForeColor="White" />
                                                                                                            <AlternatingRowStyle CssClass="lightblue" />
                                                                                                        </asp:GridView>
                                                                                                    </asp:Panel>
                                                                                                </td>
                                                                                            </tr>
                                                                                     
                                                                                            <tr>
                                                                                                <td class="gap" style="height: 8pt; width: 1186px;">
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td colspan="3" style="height: 10pt;">
                                                                                                </td>
                                                                                            </tr>
                                                                                        </table>
                                                                                    </td>
                                                                                </tr>
                                                                                <!-- Business case Ends -->
                                                                                <tr>
                                                                                    <td>
                                                                                        <input type="hidden" runat="server" id="hdEnChainCode" style="width: 21px" />
                                                                                        <input type="hidden" runat="server" id="hdChainCode" style="width: 21px" />
                                                                                        <asp:HiddenField ID="hdBcID" runat="server" />
                                                                                        <asp:TextBox ID="hdTabID" runat="server"></asp:TextBox>
                                                                                        <asp:HiddenField ID="hdRecordOnCurrentPage" runat="server" EnableViewState="true"/>
                                                                                        <asp:HiddenField ID="hdAction" runat="server" EnableViewState="true"/>
                                                                                        <asp:HiddenField ID="hdRefreshAction" runat="server" EnableViewState="true"/>
                                                                                        
                                                                                        
                                                                                        
                                                                                    </td>
                                                                                </tr>
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
                </td>
            </tr>
        </table>

<script>


try
{

        if(document.getElementById('<%=hdTabID.ClientId %>').value=='0')
        {
                document.getElementById('<%=pnlRemarks.ClientID %>').className='displayBlock';  
                document.getElementById('<%=pnlRemarks1.ClientID %>').className='displayNone';  
                document.getElementById('<%=pnlApprovars.ClientID %>').className='displayNone';  
                 
                 document.getElementById("theTabSubStrip$ctl00$Button2").className="headingtabactive"
                 document.getElementById("theTabSubStrip$ctl01$Button2").className="headingtabactive"
                 document.getElementById("theTabSubStrip$ctl02$Button2").className="headingtabactive"
                 document.getElementById("theTabSubStrip$ctl03$Button2").className="headingtab"
                 document.getElementById("theTabSubStrip$ctl04$Button2").className="headingtabactive"
                 document.getElementById("theTabSubStrip$ctl05$Button2").className="headingtabactive"
                 
                 
        }
        else if(document.getElementById('<%=hdTabID.ClientId %>').value=='1')
        {
               document.getElementById('<%=pnlRemarks.ClientID %>').className='displayNone';  
                document.getElementById('<%=pnlRemarks1.ClientID %>').className='displayBlock';  
                document.getElementById('<%=pnlApprovars.ClientID %>').className='displayNone';  
                 
                 document.getElementById("theTabSubStrip$ctl00$Button2").className="headingtabactive"
                 document.getElementById("theTabSubStrip$ctl01$Button2").className="headingtabactive"
                 document.getElementById("theTabSubStrip$ctl02$Button2").className="headingtabactive"
                 document.getElementById("theTabSubStrip$ctl03$Button2").className="headingtabactive"
                 document.getElementById("theTabSubStrip$ctl04$Button2").className="headingtab"
                 document.getElementById("theTabSubStrip$ctl05$Button2").className="headingtabactive"
                 
                 
                 
        }
         else if(document.getElementById('<%=hdTabID.ClientId %>').value=='2')
        {
                
                
               document.getElementById('<%=pnlRemarks.ClientID %>').className='displayNone';  
                document.getElementById('<%=pnlRemarks1.ClientID %>').className='displayNone';  
                document.getElementById('<%=pnlApprovars.ClientID %>').className='displayBlock';  
                 
                 document.getElementById("theTabSubStrip$ctl00$Button2").className="headingtabactive"
                 document.getElementById("theTabSubStrip$ctl01$Button2").className="headingtabactive"
                 document.getElementById("theTabSubStrip$ctl02$Button2").className="headingtabactive"
                 document.getElementById("theTabSubStrip$ctl03$Button2").className="headingtabactive"
                 document.getElementById("theTabSubStrip$ctl04$Button2").className="headingtabactive"
                 document.getElementById("theTabSubStrip$ctl05$Button2").className="headingtab"
                 
                 
        }
        
    			
    			}
    			
    			 catch(err)
    			{
    			}
    			
</script>     

    </form>
</body>
</html>
