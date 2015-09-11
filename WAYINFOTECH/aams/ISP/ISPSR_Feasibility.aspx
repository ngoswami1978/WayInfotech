<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ISPSR_Feasibility.aspx.vb"
    Inherits="ISP_ISPSR_Feasibility" MaintainScrollPositionOnPostback ="true" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>AAMS: Agency</title>
    <link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" media="all" href="../Calender/calendar-blue.css"
        title="win2k-cold-1" />

    <script type="text/javascript" src="../JavaScript/AAMS.js"></script>

</head>

<script type="text/javascript" src="../JavaScript/AAMS.js"></script>

<!-- import the calendar script -->

<script type="text/javascript" src="../Calender/calendar.js"></script>

<!-- import the language module -->

<script type="text/javascript" src="../Calender/lang/calendar-en.js"></script>

<!-- import the calendar setup module -->

<script type="text/javascript" src="../Calender/calendar-setup.js"></script>

<script language="javascript" type="text/javascript">

function DummyFeasiblity()
{
if(document.getElementById('<%=chkDummyFeasiblity.ClientID%>').checked==true)
{
//document.getElementById("txtAgencyName").disabled=true;
//document.getElementById("txtAgencyName").className='textboxgrey';
document.getElementById('<%=hdSpan.ClientID%>').style.display='none';

}
else
{
//document.getElementById("txtAgencyName").disabled=false;
//document.getElementById("txtAgencyName").className='textbox';
document.getElementById('<%=hdSpan.ClientID%>').style.display='block';

}


}




  function CheckValidation()
  {
    if(document.getElementById('<%=txtRequestId.ClientID%>').value.trim()!="")
              {
                if(IsDataValid(document.getElementById("txtRequestId").value,3)==false)
                {
                    document.getElementById('lblError').innerHTML='Requested id is not valid.';             
                    document.getElementById("txtRequestId").focus();
                    return false;
                 }                  
              }
  if(document.getElementById('<%=txtDateTo.ClientId%>').value != '')
        {
        if (isDate(document.getElementById('<%=txtDateTo.ClientId%>').value,"d/M/yyyy") == false)	
        {
           document.getElementById('<%=lblError.ClientId%>').innerText = "Date of RequestDate To is not valid.";			
	       document.getElementById('<%=txtDateTo.ClientId%>').focus();
	       return(false);  
        }
        }  
  if(document.getElementById('<%=txtDateFrom.ClientId%>').value != '')
        {
        if (isDate(document.getElementById('<%=txtDateFrom.ClientId%>').value,"d/M/yyyy") == false)	
        {
           document.getElementById('<%=lblError.ClientId%>').innerText = "Date of RequestDate From is not valid.";			
	       document.getElementById('<%=txtDateFrom.ClientId%>').focus();
	       return(false);  
        }
        }  
  }
  function PopupAgencyPage()
{
//    var type;
//  type = "../Popup/PUSR_Agency.aspx" 
//  var strReturn;   
//  if (window.showModalDialog)
//  {
//      strReturn=window.showModalDialog(type,null,'dialogWidth:880px;dialogHeight:600px;help:no;');       
//  }
//  else
//  {
//      strReturn=window.open(type,null,'height=600,width=880,top=30,left=20,scrollbars=1');       
//  }	   
//  if (strReturn != null)
//  {
//        var sPos = strReturn.split('|'); 
//        document.getElementById('<%=hidLcode.ClientID%>').value=sPos[0];        
//        document.getElementById('<%=txtAgencyName.ClientID%>').value=sPos[1];        
//        document.getElementById('<%=txtAgencyName.ClientID%>').focus();
//  }

          var type;
         // type = "../Popup/PUSR_Agency.aspx" ;
            type = "../TravelAgency/TASR_Agency.aspx?Popup=T" ;
   	        window.open(type,"aa","height=600,width=880,top=30,left=20,scrollbars=1");	
            return false;
}
 
function FeasibilityReset()
    {
         document.getElementById('<%=chkDummyFeasiblity.ClientID%>').checked=false;
        document.getElementById("txtAgencyName").value="";   
        document.getElementById("txtRequestId").value="";
        document.getElementById("ddlApprovedBy").selectedIndex=0;      
        document.getElementById("txtDateTo").value="";
        document.getElementById("txtDateFrom").value="";         
        document.getElementById("ddlFeasibleStatus").selectedIndex=0;
        document.getElementById("txtIspName").value="";
        if(document.getElementById("gvISPFeasibilityRequest")!=null)  
        document.getElementById("gvISPFeasibilityRequest").style.display ="none";
        document.getElementById("hidIspId").value="";
        document.getElementById("hidLcode").value="";
    }
    function NewFunction()
    {  
        window.location.href="ISPUP_Feasibility.aspx?Action=I";      
        return false;
    }  
    function EditFunction(FeasibilityRequestID)
    {               
        window.location.href="ISPUP_Feasibility.aspx?Action=U&Msg=U&FeasibilityRequestID="+FeasibilityRequestID;      
        return false;
          }
    function DeleteFunction(FeasibilityRequestID)
    {  
    
        if (confirm("Are you sure you want to delete?")==true)
        {          
            //window.location.href="ISPSR_Feasibility.aspx?Action=D&FeasibilityRequestID="+FeasibilityRequestID; 
           // hdDeleteID
            document.getElementById('<%=hdDeleteID.ClientId%>').value = FeasibilityRequestID;
            document.forms['form1'].submit();
       }
       return false;
    }
    
    function PopupIspPage()
  {
 

            type = "../ISP/MSSR_ISP.aspx?Popup=T" ;
   	        window.open(type,"aa","height=600,width=880,top=30,left=20,scrollbars=1,status=1");	
            return false;
          
  }
</script>

<body onload="DummyFeasiblity();" >
    <form id="form1" defaultbutton="btnSearch" defaultfocus="txtAgencyName" runat="server">
    <table><tr><td>
        <table width="860px" align="left" class="border_rightred" >
            <tr>
                <td valign="top">
                    <table width="100%" align="left">
                        <tr>
                            <td valign="top" align="left">
                                <span class="menu">ISP-></span><span class="sub_menu">ISP Feasibility Request</span></td>
                        </tr>
                        <tr>
                            <td class="heading" align="center" valign="top">
                                ISP Feasibility Request Search
                            </td>
                        </tr>
                        <tr>
                            <td valign="top">
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td align="LEFT" class="redborder">
                                            <table width="100%" border="0" align="left" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td class="textbold" colspan="6" style="height: 15px" align="center">
                                                        <asp:Label ID="lblError" runat="server" CssClass="ErrorMsg"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="textbold" style="height: 14px; width: 40px;">
                                                        &nbsp;</td>
                                                    <td class="textbold" style="height: 14px; width: 119px;">
                                                        Agency Name</td>
                                                    <td colspan="3" style="height: 14px" nowrap="nowrap" id="td1" runat="server">
                                                        <table cellpadding="0" cellspacing="0" border="0">
                                                            <tr>
                                                                <td style="height: 21px; width: 465px;">
                                                                    <asp:TextBox ID="txtAgencyName" CssClass="textbox" MaxLength="40" runat="server"
                                                                        Width="458px" TabIndex="2"></asp:TextBox>
                                                                </td>
                                                                <td style="width: 5px;"></td>
                                                                <td style="width: 18px; height: 21px">
                                                                    <img id="hdSpan" runat="server" alt="" onclick="javascript:return PopupAgencyPage();"
                                                                        src="../Images/lookup.gif" style="cursor:pointer;" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td  style="height: 27px">
                                                        <asp:Button ID="btnSearch" CssClass="button" runat="server" Text="Search" TabIndex="3" AccessKey="A" /></td>
                                                </tr>
                                                <tr>
                                                    <td  >
                                                    </td>
                                                    <td class="textbold" >
                                                        New Agency</td>
                                                    <td class="textbold">
                                                        <asp:CheckBox ID="chkDummyFeasiblity" runat="server" /></td>
                                                    <td class="textbold">
                                                    </td>
                                                    <td >
                                                    </td>
                                                    <td style="height: 27px">
                                                        <asp:Button ID="btnNew" CssClass="button" runat="server" Text="New" TabIndex="3" AccessKey="N" /></td>
                                                </tr>
                                                <tr>
                                                    <td class="textbold" style="height: 27px; width: 40px;">
                                                        &nbsp;</td>
                                                    <td class="textbold" style="height: 27px; width: 119px;">
                                                        Request ID</td>
                                                    <td style="width: 214px; height: 27px;">
                                                        <asp:TextBox ID="txtRequestId" runat="server" TabIndex="2" CssClass="textbox" MaxLength="40"></asp:TextBox></td>
                                                    <td class="textbold" style="height: 27px; width: 116px;">
                                                        Logged By</td>
                                                    <td style="height: 27px; width: 213px;">
                                                        <asp:DropDownList ID="ddlApprovedBy" runat="server" CssClass="dropdownlist" TabIndex="2"
                                                            Width="137px">
                                                        </asp:DropDownList></td>
                                                    <td style="height: 27px">
                                                        <asp:Button ID="btnExport" runat="server" CssClass="button" TabIndex="3" Text="Export" AccessKey="E" /></td>
                                                </tr>
                                                <tr>
                                                    <td class="textbold" style="height: 27px; width: 40px;">
                                                    </td>
                                                    <td class="textbold" style="height: 27px; width: 119px;">
                                                        Request Date From</td>
                                                    <td style="height: 27px; width: 214px;">
                                                        <asp:TextBox ID="txtDateFrom" TabIndex="2" runat="server" CssClass="textbox" MaxLength="40"></asp:TextBox>
                                                        <img id="Img2" alt="" src="../Images/calender.gif" tabindex="16" title="Date selector"
                                                            style="cursor: pointer" />

                                                        <script type="text/javascript">
                                                                                                    Calendar.setup({
                                                                                                    inputField     :    '<%=txtDateFrom.clientId%>',
                                                                                                    ifFormat       :    "%d/%m/%Y",
                                                                                                    button         :    "Img2",
                                                                                                    //align          :    "Tl",
                                                                                                    singleClick    :    true
                                                                                                    });
                                                        </script>

                                                    </td>
                                                    <td class="textbold" style="height: 27px; width: 116px;">
                                                        Request Date To</td>
                                                    <td style="height: 27px; width: 213px;">
                                                        <asp:TextBox ID="txtDateTo" runat="server" CssClass="textbox" MaxLength="40" TabIndex="2"></asp:TextBox>
                                                        <img id="Img1" alt="" src="../Images/calender.gif" style="cursor: pointer" title="Date selector" /><script
                                                            type="text/javascript">
                                                                                                    Calendar.setup({
                                                                                                    inputField     :    '<%=txtDateTo.clientId%>',
                                                                                                    ifFormat       :    "%d/%m/%Y",
                                                                                                    button         :    "Img1",
                                                                                                    //align          :    "Tl",
                                                                                                    singleClick    :    true
                                                                                                    });
                                                        </script></td>
                                                    <td style="height: 27px">
                                                        <asp:Button ID="btnReset" CssClass="button" runat="server" Text="Reset" TabIndex="3" AccessKey="R" /></td>
                                                </tr>
                                                <tr>
                                                    <td class="textbold" style="width: 40px; height: 27px">
                                                    </td>
                                                    <td class="textbold" style="width: 119px; height: 27px">
                                                        ISP Name</td>
                                                    <td style="width: 214px; height: 27px">
                                                        <asp:TextBox ID="txtIspName" runat="server" CssClass="textbox" MaxLength="40" TabIndex="2"></asp:TextBox>
                                                        <img id="Img3" runat="server" alt="" onclick="javascript:return PopupIspPage();"
                                                            src="../Images/lookup.gif" /></td>
                                                    <td class="textbold" style="width: 116px; height: 27px">
                                                        Feasibility Status</td>
                                                    <td style="width: 213px; height: 27px">
                                                        <asp:DropDownList ID="ddlFeasibleStatus" runat="server" CssClass="dropdownlist" TabIndex="2"
                                                            Width="137px">
                                                        </asp:DropDownList></td>
                                                    <td style="height: 27px">
                                                        </td>
                                                </tr>
                                                <tr>
                                                    <td class="textbold" style="width: 40px; height: 27px">
                                                    </td>
                                                    <td class="textbold" style="width: 119px; height: 27px">
                                                        Country</td>
                                                    <td style="width: 214px; height: 27px">
                                                        <asp:DropDownList ID="drpISPCountry" runat="server" CssClass="dropdownlist" TabIndex="2"
                                                            Width="137px">
                                                        </asp:DropDownList></td>
                                                    <td class="textbold" style="width: 116px; height: 27px">
                                                        City</td>
                                                    <td style="width: 213px; height: 27px">
                                                        <asp:DropDownList ID="drpISPCity" runat="server" CssClass="dropdownlist" TabIndex="2"
                                                            Width="137px">
                                                        </asp:DropDownList></td>
                                                    <td style="height: 27px">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="textbold" style="height: 25px; width: 40px;">
                                                        &nbsp;</td>
                                                    <td class="textbold" style="height: 25px; width: 119px;" colspan="2" valign="top">
                                                        &nbsp;<asp:HiddenField ID="hidIspId" runat="server" />
                                                        <input id="hdRecordOnCurrentPage" runat="server" style="width: 6px" type="hidden" /></td>
                                                    <td class="textbold" style="height: 25px; width: 116px;">
                                                    </td>
                                                    <td style="height: 25px; width: 213px;">
                                                        &nbsp;
                                                        <asp:HiddenField ID="hidLcode" runat="server" />
                                                        <asp:HiddenField ID="hdCountry" runat="server" />
                                                         <asp:HiddenField ID="hdDeleteID" runat="server" />
                                                    </td>
                                                    <td >
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="6" >
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
        </td></tr>
        <tr>
        <td class="redborder" valign="top" >
         <asp:GridView ID="gvISPFeasibilityRequest" runat="server" AutoGenerateColumns="False"
                                                            TabIndex="7" Width="1500px" EnableViewState="False" AllowSorting="True" HeaderStyle-HorizontalAlign ="Left">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="Request ID" SortExpression="RequestID">
                                                                    <ItemTemplate>
                                                                        <asp:HiddenField ID="rowHidden1" runat="server" Value='<%#eval("RequestID")%>' />
                                                                       <%#eval("RequestID")%>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle Width="100px"  />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Agency Name" SortExpression="AgencyName">
                                                                    <ItemTemplate>
                                                                        <asp:HiddenField ID="hdAgencyName" runat="server" Value='<%#eval("AgencyName")%>' />
                                                                        <%#eval("AgencyName")%>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle Width="300px" />
                                                                </asp:TemplateField>
                                                              
                                                                <asp:TemplateField HeaderText="Address" SortExpression="ADDRESS">
                                                                    <ItemTemplate>
                                                                     <%#eval("ADDRESS")%>
                                                                    </ItemTemplate>
                                                                    <ItemStyle Width="300px" />
                                                                    <HeaderStyle Width="300px" />
                                                                </asp:TemplateField>
                                                                
                                                              
                                                                
                                                                <asp:TemplateField HeaderText="City" SortExpression="City">
                                                                    <ItemTemplate>
                                                                        <asp:HiddenField ID="hdCity" runat="server" Value='<%#eval("City")%>' />
                                                                        <%#eval("City")%>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle Width="150px" />
                                                                </asp:TemplateField>
                                                                
                                                                 <asp:TemplateField HeaderText="OfficeID" SortExpression="OFFICEID">
                                                                    <ItemTemplate>
                                                                    <%#Eval("OFFICEID")%>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle Width="150px" />
                                                                </asp:TemplateField>
                                                                
                                                                 <asp:TemplateField HeaderText="Pin Code" SortExpression="PINCODE">
                                                                    <ItemTemplate>
                                                                     <%#eval("PINCODE")%>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle Width="150px" />
                                                                </asp:TemplateField>
                                                                
                                                                 <asp:TemplateField HeaderText="Con. Person" SortExpression="STAFFNAME">
                                                                    <ItemTemplate>
                                                                     <%#Eval("STAFFNAME")%>
                                                                    </ItemTemplate>
                                                                    <ItemStyle Width="300px" />
                                                                    <HeaderStyle Width="300px" />
                                                                </asp:TemplateField>
                                                                
                                                                 <asp:TemplateField HeaderText="Con. Number" SortExpression="PHONE">
                                                                    <ItemTemplate>
                                                                     <%#eval("PHONE")%>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle Width="200px" />
                                                                </asp:TemplateField>
                                                                
                                                                <asp:TemplateField HeaderText="Requested By" SortExpression="LoggedBy">
                                                                    <ItemTemplate>
                                                                     <%#eval("LoggedBy")%>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle Width="200px" />
                                                                </asp:TemplateField>
                                                                
                                                                <asp:TemplateField HeaderText="Requested Date" SortExpression="LoggedDatetime">
                                                                    <ItemTemplate>
                                                                     <%#eval("LoggedDatetime")%>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle Width="200px" />
                                                                </asp:TemplateField>
                                                                
                                                                
                                                                <asp:TemplateField HeaderText="ISP Name" SortExpression="Name" >
                                                                    <ItemTemplate>
                                                                        <asp:HiddenField ID="rowHidden2" runat="server" Value=' <%#eval("Name")%>' />
                                                                         <%#eval("Name")%>
                                                                        <%--<asp:CheckBox ID="chk" runat="server" />--%>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle Width="300px" />
                                                                    
                                                                </asp:TemplateField>
                                                                
                                                                 <asp:TemplateField HeaderText="Feasibility Date" SortExpression="FeasibleDate">
                                                                    <ItemTemplate>
                                                                    <%#eval("FeasibleDate")%>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle Width="150px" />
                                                                </asp:TemplateField>
                                                                
                                                                
                                                                <asp:TemplateField HeaderText="Status" SortExpression="FEASIBLESTATUSNAME">
                                                                    <ItemTemplate>
                                                                         <%#eval("FEASIBLESTATUSNAME")%>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle Width="150px" />
                                                                </asp:TemplateField>
                                                                
                                                                
                                                                <asp:TemplateField HeaderText="Action">
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                    <ItemTemplate>
                                                                        <a href="#" class="LinkButtons" id="linkEdit" runat="server">Edit</a> <a href="#"
                                                                            class="LinkButtons" id="linkDelete" runat="server">Delete</a>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle Width="200px" HorizontalAlign="Center" />
                                                                </asp:TemplateField>
                                                            </Columns>
                                                            <AlternatingRowStyle CssClass="lightblue" />
                                                            <RowStyle CssClass="textbold" />
                                                            <HeaderStyle CssClass="Gridheading" ForeColor="White" />
                                                        </asp:GridView>
           <asp:Panel ID="pnlPaging" runat="server" Visible="false" Width="100%">
                                                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                <tr class="paddingtop paddingbottom">
                                                                    <td style="width: 10%" class="left">
                                                                        <span class="textbold"><b>&nbsp;No. of records found</b></span>&nbsp;&nbsp;<asp:TextBox
                                                                            ID="txtTotalRecordCount" runat="server" Width="105px" CssClass="textboxgrey" ReadOnly="True"></asp:TextBox></td>
                                                                    <td style="width: 5%" class="right">
                                                                        <asp:LinkButton ID="lnkPrev" CssClass="LinkButtons" runat="server" CommandName="Prev"><< Prev</asp:LinkButton></td>
                                                                    <td style="width: 10%" class="center">
                                                                        <span class="textbold"><b>Page No</b></span>&nbsp;&nbsp;<asp:DropDownList ID="ddlPageNumber"
                                                                            Width="70px" CssClass="dropdownlist" runat="server" AutoPostBack="True">
                                                                        </asp:DropDownList></td>
                                                                    <td style="width: 35%" class="left">
                                                                        <asp:LinkButton ID="lnkNext" runat="server" CssClass="LinkButtons" CommandName="Next">Next >></asp:LinkButton></td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
        </td></tr>
        </table>
       
    </form>
</body>
</html>
