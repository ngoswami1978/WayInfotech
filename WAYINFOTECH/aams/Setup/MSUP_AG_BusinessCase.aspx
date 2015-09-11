<%@ Page Language="VB" AutoEventWireup="false" CodeFile="MSUP_AG_BusinessCase.aspx.vb"
    Inherits="Setup_MSUP_AG_BusinessCase" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>AAMS::Travel Agency::Manage Business Case</title>
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
    
    
    
       function EditFunction(BCaseID, ChainCode)               
        {          
               var type;       
                type = "../Incentive/INCUP_BacseDetails.aspx?Action=U&Chain_Code=" + ChainCode + "&BCaseID=" + BCaseID; 
   	            //window.open(type,"IncUp","height=900,width=1224,top=30,left=20,scrollbars=1,status=1");	                    
               window.location =type;
                return false;
        }   
        
                  function NewFunction(ChainCode)
            {
            var type;
            type = "../Incentive/INCUP_BacseDetails.aspx?Action=N&Chain_Code=" + ChainCode;

            window.open(type,"aa","height=630,width=1000,top=30,left=20,scrollbars=1,status=1,resizable=1;");
           // window.location =type;
            return false;
            }

        
             function DetailsFunction(BCaseID, ChainCode)
        {          
              var type;       
             type = "../Incentive/INCUP_BacseDetails.aspx?Action=U&Chain_Code=" + ChainCode + "&BCaseID=" + BCaseID; 
   	            window.open(type,"IncDetails","height=630,width=1000,top=30,left=20,scrollbars=1,status=1,resizable=1");	        
             // window.location ="MSSR_BcaseDetails.aspx?Chain_Code=" + ChainCode + "&BCaseID=" + BCaseID; 
              return false;
        }   
        
             function ViewDocFunction(BCaseID, ChainCode)
        {        
        
            var type;       
           // type = "MSUP_BcaseDetails.aspx?Action=N&Chain_Code=" + ChainCode; 
   	          //  window.open(type,"aa","height=600,width=920,top=30,left=20,scrollbars=1,status=1");	         
            //  window.location ="MSUP_City.aspx?Chain_Code=" + ChainCode + "&BCaseID=" + BCaseID; 
              return false;
        }   
function TabMethodAgencyGroup(id,total)
{   
//{debugger;} 
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
       
       var strChain_Code="";
        strChain_Code = document.getElementById('hdEnChainCode').value;
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
            return false;   
       }
}



function TabMethodAgencySubGroup(id,total)
{  
//{debugger;} 
        var ctextSubFront;
        var ctextSubBack;
        var HSubcontrol;
        var HSubFlush;
        
        ctextSubFront = id.substring(0,23);        
        ctextSubBack = id.substring(25,34);   
       
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
        strChain_Code = document.getElementById('hdEnChainCode').value;
       document.getElementById(id).className="headingtab";     
      
       document.getElementById('lblSubPanelClick').value =id; 
       
    
      
        if (id == (ctextSubFront +  "00" + ctextSubBack))
       {   
            window.location.href="MSSR_AG_BCase_GroupMIDT.aspx?Action=U&Chain_Code=" + strChain_Code  ;              
            return false;
       }       
       else if (id == (ctextSubFront +  "01" + ctextSubBack))
       {   
            window.location.href="MSSR_AG_BCase_AgencyMIDT.aspx?Action=U&Chain_Code=" + strChain_Code  ;   
            return false;                      
       }
       else if (id == (ctextSubFront +  "02" + ctextSubBack))
       {
           window.location.href="MSSR_AG_BCase_Connectivity.aspx?Action=U&Chain_Code=" + strChain_Code  ;   
            return false;   
       }
       else if (id == (ctextSubFront +  "03" + ctextSubBack))
       {    
            window.location.href="MSSR_AG_BCase_Hardware.aspx?Action=U&Chain_Code=" + strChain_Code  ;   
            return false;   
       }
       
        else if (id == (ctextSubFront +  "04" + ctextSubBack))
       {
             window.location.href="MSSR_AG_BCase_Hardware.aspx?Action=U&Chain_Code=" + strChain_Code  ;   
            return false;   
       }
}

    </script>

</head>
<body>
    <form id="form1" runat="server">
        <table width="835px" class="border_rightred left">
            <tr>
                <td class="top">
                    <table width="100%">
                        <tr>
                            <td class="top">
                                <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td class="top left" style="width: 80%">
                                            <span class="menu">Travel Agency->Agency Group-></span><span class="sub_menu">Business
                                                Case</span>
                                        </td>
                                        <td class="right" style="width: 20%">
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td class="heading center" colspan="2" style="width: 100%">
                                            Business Case</td>
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
                                        <td valign="top" class="redborder">
                                            <table width="830x" border="0" cellspacing="0" cellpadding="0">
                                                <tr>
                                                    <td valign="top">
                                                        <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                                            <tr>
                                                                <td valign="top" style="height: 5PX">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="center TOP">
                                                                    <asp:Label ID="lblError" CssClass="ErrorMsg" runat="server" EnableViewState="False"></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <table width="100%" border="0" cellspacing="2" cellpadding="2">
                                                            <tr>
                                                                <td align="center" style="width: 518px">
                                                                </td>
                                                                <td>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="left" style="width: 518px">
                                                                  <div id="DivFAColor" runat ="server" visible ="false"  ><b> Note: &nbsp;<asp:Label ID="lblFAColor" runat ="server"  BackColor ="LightSeaGreen" Text="&nbsp;&nbsp;&nbsp;" ></asp:Label>  Denotes currently running business deal.</b></div>
                                                                </td>
                                                                <td align="center">
                                                                    <asp:Button ID="btnNew" runat="server" AccessKey="N" CssClass="button" TabIndex="3"
                                                                        Text="New" /></td>
                                                            </tr>
                                                            <tr>
                                                                <td class="subheading" colspan="2" align="center">
                                                                    Existing &amp; Old Deals</td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="2">
                                                                    <asp:GridView ID="GvBCaseDeals" runat="server" AutoGenerateColumns="False" TabIndex="6"
                                                                        ShowFooter="false" ShowHeader="true" Width="100%" EnableViewState="true">
                                                                        <Columns>
                                                                            <%--<asp:BoundField HeaderText="S.No." ItemStyle-Width="20px" HeaderStyle-Width="20px"></asp:BoundField>--%>
                                                                            <asp:BoundField HeaderText="BCaseId" ItemStyle-Width="70px" DataField="BC_ID" SortExpression="BC_ID"
                                                                            HeaderStyle-Width="20px"></asp:BoundField>
                                                                           <asp:BoundField HeaderText="Deal Type" DataField ="INC_TYPE_NAME" ItemStyle-Width="20%" ></asp:BoundField>
                                                                            <asp:BoundField HeaderText="Valid From" DataField="BC_EFFECTIVE_FROM" HeaderStyle-Width="16%"
                                                                                ItemStyle-Width="16%" ItemStyle-Wrap="true"></asp:BoundField>
                                                                            <asp:BoundField HeaderText="Valid Till" DataField="BC_VALID_TILL" HeaderStyle-Width="16%" ItemStyle-Width="16%">
                                                                            </asp:BoundField>
                                                                                <asp:BoundField HeaderText="Status" DataField="APPROVAL_STATUS_NAME" HeaderStyle-Width="10%"
                                                                    SortExpression="APPROVAL_STATUS_NAME" ItemStyle-Width="16%"></asp:BoundField>
                                                                            <asp:TemplateField>
                                                                                <HeaderTemplate>
                                                                                    <asp:Label ID="lblAction" runat="server" Text="Action"></asp:Label>
                                                                                </HeaderTemplate>
                                                                                <ItemTemplate>
                                                                                    <a href="#" class="LinkButtons" id="linkDetails" runat="server">Details</a>&nbsp;&nbsp;
                                                                                    <a href="#" class="LinkButtons" id="linkViewDoc" visible ="false"  runat="server">ViewDoc</a>&nbsp;&nbsp;
                                                                                    <a href="#" class="LinkButtons" id="linkEdit" runat="server" visible ="false" >Edit</a>&nbsp;&nbsp;
                                                                                    <asp:HiddenField ID="hdBCaseID" runat ="server" Value='<%#Eval("BC_ID" )%>' />
                                                                                    <asp:HiddenField ID="hdActive" runat ="server" Value='<%#Eval("Active" )%>' />
                                                                                </ItemTemplate>
                                                                                <ItemStyle Width="20%" CssClass="ItemColor" />
                                                                                <HeaderStyle Wrap="False" CssClass="Gridheading" />
                                                                            </asp:TemplateField>
                                                                        </Columns>
                                                                        <AlternatingRowStyle CssClass="lightblue" />
                                                                        <RowStyle CssClass="textbold" />
                                                                        <HeaderStyle CssClass="Gridheading" ForeColor="White" HorizontalAlign="Left" />
                                                                        <FooterStyle CssClass="Gridheading" />
                                                                    </asp:GridView>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="">
                                                        &nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <input type="hidden" runat="server" id="hdEnChainCode" style="width: 21px" />
                                                        <input type="hidden" runat="server" id="hdChainCode" style="width: 21px" /></td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                    </td>
                    </tr>
                    </table>
    </form>
</body>
</html>
