<%@ Page Language="VB"  AutoEventWireup="false" CodeFile="PRDSR_RegionWiseBudget.aspx.vb" Inherits="Productivity_PRDSR_RegionWiseBudget" ValidateRequest="false" EnableEventValidation="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />
    <title>AAMS::Inventory::Manage Scrap</title>

    <script src="../JavaScript/AAMS.js" type="text/javascript"></script>
 <script language="javascript" type="text/javascript">
   function validateResult(id)
{

       if(document.getElementById(id).value =='')
        {
            
        }
        else
        {
        var strValue = document.getElementById(id).value;
        
            reg = new RegExp("^[0-9.]+$"); 
            if(reg.test(strValue) == false) 
            {
                document.getElementById('<%=lblError.ClientId%>').innerText ='Only Number allowed.';
                 document.getElementById(id).focus();
                return false;
             }
            
        
        }
        
}

function ValidateRegisterPage()
{

var dotcount=0;

     for(intcnt=1;intcnt<=document.getElementById('gvRegionWise').rows.length-1;intcnt++)
    {   
     if (document.getElementById('gvRegionWise').rows[intcnt].cells[2].children.length == "1")
    {    
    if (document.getElementById('gvRegionWise').rows[intcnt].cells[2].children[0].type=="text")
    { 
    var strValue = document.getElementById('gvRegionWise').rows[intcnt].cells[2].children[0].value.trim();
    if (IsDataValid(strValue,5)==false)
    {
    document.getElementById('gvRegionWise').rows[intcnt].cells[2].children[0].focus();
    document.getElementById("lblError").innerHTML="Target is Numeric";
    return false;
    }
	if (strValue != '')
          {
            reg = new RegExp("^[0-9.]+$"); 
            if(reg.test(strValue) == false) 
            {
              document.getElementById('lblError').innerText ='Only Number allowed.';       
              return false;
             }
          }
        for (var i=0; i < strValue.length; i++) 
	        {
		        if (strValue.charAt(i)=='.')
		         { 
		         dotcount=dotcount+1;
		          }
		   }
            if(dotcount>1)
            {
                document.getElementById('<%=gvRegionWise.ClientID%>').rows[intcnt].cells[2].children[0].focus();
                document.getElementById("lblError").innerHTML="Target Invalid";
                return false;
            }
            
     dotcount=0; 
          
     }
    }
  }
}
    function ChangeControlStatus(id,id1)
    {

    var d =new Date();
    var curr_year=d.getFullYear();
    var curr_mon=d.getMonth();
     var Year=document.getElementById("drpYear").value.trim();
     
     if (Year<curr_year)
     {
       document.getElementById(id).disabled=true;
        document.getElementById(id).className='textboxgrey';
     }
      else if(Year==curr_year && id1<curr_mon+1)
     {
      document.getElementById(id).disabled=true;
      document.getElementById(id).className='textboxgrey';
      }
     else
     {
     document.getElementById(id).disabled=false;
      document.getElementById(id).className='textbox';
     }
//     else if(id1<curr_mon+1)
//       {
//       document.getElementById(id).disabled=true;
//       document.getElementById(id).className='textboxgrey';
//       }
          
        return false;
    }
    function MandatoryFunction()
    {
     if(document.getElementById("drpRegion").value=='')
        {
        document.getElementById("drpRegion").focus();
        document.getElementById("lblError").innerHTML ="Region Name is Mandatory";
        return false;
        }
    }

//    function HistoryFunction(str3)
     function ShowPTRHistory()
      {  
      var RegionID=document.getElementById("drpRegion").value.trim();
       var Year=document.getElementById("drpYear").value.trim();
       var str3=RegionID+"|"+Year;
           if(document.getElementById("drpRegion").value=='')
        {
        document.getElementById("drpRegion").focus();
        document.getElementById("lblError").innerHTML ="Region Name is Mandatory";
        return false;
        }
     var type="../Popup/PUSR_RegionWiseBudget.aspx?Str="+str3;
     var strReturn;
     if (window.showModalDialog)
     {
     strReturn=window.showModalDialog(type,null,'dialogWidth:880px;dialogHeight:600px;help:no;');       
     }
    else
    {  
       strReturn=window.open(type,null,'height=600,width=880,top=30,left=20,scrollbars=1');   
     }
       
    }
    

    
    
   </script>
   
</head>
<body >
    <form id="form1" defaultbutton="btnSearch" defaultfocus="drpRegion" runat="server">
        <div>
            <table width="840px" class="border_rightred left">
                <tr>
                    <td class="top">
                        <table width="100%" class="left">
                            <tr>
                                <td>
                                    <span class="menu">Productivity -&gt;</span><span class="sub_menu">Region Wise Budget</span></td>
                            </tr>
                            <tr>
                                <td class="heading center">
                                    Search Region Wise Budget</td>
                            </tr>
                            <tr>
                                <td>
                                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                        <tr>
                                            <td class="redborder center">
                                                <table border="0" cellpadding="2" cellspacing="1" style="width: 100%" class="left">
                                                    <tr>
                                                        <td class="center" colspan="7" style="height: 25px">
                                                            <asp:Label ID="lblError" runat="server" CssClass="ErrorMsg" EnableViewState="False"></asp:Label></td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 10%">
                                                        </td>
                                                        <td class="textbold" style="width: 10%">
                                                            Region<span class="Mandatory">* </span></td>
                                                        <td class="textbold" colspan="2">
                                                            <asp:DropDownList ID="drpRegion" runat="server" CssClass="dropdownlist" Style="position: relative; left: 0px; top: 0px;"
                                                                TabIndex="1" Width="144px">
                                                            </asp:DropDownList></td>
                                                        <td class="textbold" style="width: 10%">
                                                            Year<span class="Mandatory">* </span></td>
                                                        <td style="width: 20%">
                                                            <asp:DropDownList ID="drpYear" runat="server" CssClass="dropdownlist" Style="left: 0px;
                                                                position: relative; top: 0px" TabIndex="2" Width="72px">
                                                            </asp:DropDownList></td>
                                                        <td class="center" style="width: 15%">
                                                            <asp:Button ID="btnSearch" CssClass="button" runat="server" Text="Search" TabIndex="3" /></td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                        </td>
                                                        <td class="textbold">
                                                        </td>
                                                        <td class="textbold" colspan="2">
                                                            &nbsp;</td>
                                                        <td class="textbold" style="width: 8%">
                                                        </td>
                                                        <td style="width: 22%">
                                                            &nbsp;</td>
                                                        <td class="center">
                                                            <asp:Button ID="btnSave" CssClass="button" runat="server" Text="Save" TabIndex="4" OnClientClick="return ValidateRegisterPage()" /></td>
                                                    </tr>
                                                    <tr>
                                                        <td style="height: 26px">
                                                        </td>
                                                        <td class="textbold" style="height: 26px">
                                                        </td>
                                                        <td class="ErrorMsg" colspan="2" style="height: 26px">
                                                            Field Marked * are Mandatory
                                                        </td>
                                                        <td class="textbold" style="width: 8%; height: 26px;">
                                                        </td>
                                                        <td style="height: 26px">
                                                        </td>
                                                        <td class="center" style="height: 26px">
                                                            <asp:Button ID="btnReset" CssClass="button" runat="server" Text="Reset" TabIndex="5" style="position: relative" /></td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                        </td>
                                                        <td class="textbold">
                                                        </td>
                                                        <td style="width: 20%">
                                                                                                                </td>
                                                        <td class="textbold" style="width: 4%">
                                                        </td>
                                                        <td class="textbold" style="width: 8%">
                                                        </td>
                                                        <td>
                                                        </td>
                                                        <td class="center">
                                                        
                                                         <input type="button" ID="btnHistory" runat="server" TabIndex="5" Class="button" value="History" style="width: 72px" onclick="javascript:return ShowPTRHistory();"/>
                                                       <%-- <asp:Button ID="btnHistory" CssClass="button" runat="server" Text="History" TabIndex="5" style="position: relative" />--%>
                                                        
                                                        </td>
                                                        
                                                    </tr>
                                                    <tr>
                                                    
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                        </td>
                                                        <td colspan="5">
                                                            <asp:GridView ID="gvRegionWise" runat="server" AutoGenerateColumns="False" TabIndex="6"
                                                                Width="100%">
                                                                <Columns>
                                                                    <asp:TemplateField  HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"    HeaderText="Month">
                                                                        <ItemTemplate >
                                                                            <asp:Label  runat="server" ID="txtmonth" Text='<%# MonthName(Eval("MONTH"))%>'  CssClass="textbox"></asp:Label>
                                                                            <asp:HiddenField ID="hdMonth" runat="server" Value='<%#Eval("MONTH")%>' />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    
                                                                  <asp:BoundField HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" HeaderText="Year" DataField="Year">
                                                                        <ItemStyle Width="20%" />
                                                                    </asp:BoundField>
                                                                   
                                                               <%-- <asp:TemplateField HeaderText="UserId">
                                                                        <ItemTemplate>
                                                                            <asp:Label  runat="server" ID="txtUSERID" Text='<%#Eval("USERID")%>'  CssClass="textbox"></asp:Label>
                                                                              
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField> --%>
                                                                     
                                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" HeaderText="Target">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox    Width="144px"  ID="txtTarget" Text='<% #DataBinder.Eval(Container.DataItem, "TARGET") %>'
                                                                               MaxLength="9"  runat="server" CssClass="textboxgrey"></asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField  HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" HeaderText="Action">
                                                                        <ItemTemplate>
                                                                             
                                                                            <%-- <asp:HiddenField ID="hdProductID" runat="server" Value='<%#Eval("REGION")%>' /> --%>
                                                                             <asp:LinkButton ID="lnkEdit" runat="server" CommandName ="EditX" Text ="Edit"   CssClass="LinkButtons"></asp:LinkButton> 
                                                                              <%-- <asp:LinkButton ID="lnkHistory" runat="server" CommandName ="HistoryX" Text ="History"   CssClass="LinkButtons"></asp:LinkButton> --%>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                                <AlternatingRowStyle CssClass="lightblue center" />
                                                                <RowStyle CssClass="textbold center" />
                                                                <HeaderStyle CssClass="Gridheading center" />
                                                            </asp:GridView>
                                                        </td>
                                                        <td class="center">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="7" style="width: 100%" valign="top">
                                                          <asp:HiddenField ID="hdRegion" runat="server" />
                                                         <asp:HiddenField ID="hdYear" runat="server" />  &nbsp;</td>
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
        </div>
    </form>

 

</body>
</html>
