<%@ Page Language="VB" AutoEventWireup="false" CodeFile="MTRptAgencyAirlineBreakup.aspx.vb" Inherits="Market_MTRptAgencyAirlineBreakup" %>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
     <title>Search ISP Plan</title>
    <script src="../JavaScript/AAMS.js" type="text/javascript"></script> 
    <link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />
    <script type ="text/javascript" language ="javascript">
     function PopupAgencyPage()
        {
            var type;       
            type = "../TravelAgency/TASR_Agency.aspx?Popup=T" ;
   	        window.open(type,"aa6","height=600,width=920,top=30,left=20,scrollbars=1,status=1");	
            return false;
        }
           function ActDeAct()
     {
        IE4 = (document.all);
        NS4 = (document.layers);
        var whichASC;
        whichASC = (NS4) ? e.which : event.keyCode;
        if(whichASC!=9)
        {
        document.getElementById("hdAgencyName").value="";       
        }
    	
     }
     
     function CheckValidation()
     {  
     
            if(document.getElementById('<%=txtLcode.ClientID%>').value.trim()!="")
              {
                if(IsDataValid(document.getElementById("txtLcode").value,12)==false)
                {
                    document.getElementById('lblError').innerHTML='Invalid Lcode.';             
                    document.getElementById("txtLcode").focus();
                    return false;
                 }                  
              }
              
              if(document.getElementById('<%=txtChainCode.ClientID%>').value.trim()!="")
              {
                if(IsDataValid(document.getElementById("txtChainCode").value,12)==false)
                {
                    document.getElementById('lblError').innerHTML='Invalid Chain Code.';             
                    document.getElementById("txtChainCode").focus();
                    return false;
                 }                  
              }
              
                 if(parseInt(document.getElementById("dlstYearFrom").value) >parseInt(document.getElementById("dlstYearTo").value))
                    {                   
                    document.getElementById("lblError").innerHTML='Date range is not valid.';          
                    document.getElementById("dlstYearFrom").focus();
                    return false;
                    } 
                   
//                   if(parseInt(document.getElementById("dlstYearTo").value)- parseInt(document.getElementById("dlstYearFrom").value)>4)
//                    {                   
//                    document.getElementById("lblError").innerHTML='Maximun number of years should be 4 years.';          
//                    document.getElementById("dlstYearFrom").focus();
//                    return false;
//                    } 
              
       
     }
    </script>
</head>
<body >
    <form id="form1" runat="server" defaultbutton ="btnPrint" defaultfocus="txtAgencyName">
    <table width="860px" class="border_rightred">
            <tr>
                <td valign="top" style="height: 339px">
                    <table width="100%" >
                        <tr>
                            <td valign="top"  align="left" >
                            <span class="menu">Market-&gt;</span><span class="sub_menu">Agency Airline Breakup Report</span></td>
                        </tr>
                        <tr>
                            <td class="heading" align="center" valign="top">
                                <span >Agency Airline Breakup Report</span></td>
                        </tr>
                        <tr>
                            <td valign="top" style="height: 117px">
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td class="redborder" >
                                    <table border="0" cellpadding="2" cellspacing="1" style="width: 100%">
                                                                       <tr>
                                                                            <td colspan="7" align ="center" ><asp:Label ID="lblError" runat="server" CssClass="ErrorMsg"></asp:Label></td>                                                                        
                                                                        </tr>
                                                                         <tr>
                                                                            <td colspan="7" ></td> 
                                                                        </tr>
                                                                       <tr>
                                                                            <td style="width:3%; height: 26px;" class="textbold" >
                                                                                &nbsp;</td>
                                                                            <td style="width:15%; height: 26px;" class="textbold"><span class="textbold">Agency Name</span>
                                                                            </td>
                                                                            <td colspan="4" style="width:70%; height: 26px;"><asp:TextBox ID="txtAgencyName" runat="server" MaxLength="40" Width="560px" TabIndex="1" CssClass="textbox" Height="18px"></asp:TextBox>&nbsp;<img tabIndex="1" src="../Images/lookup.gif" onclick="javascript:return PopupAgencyPage();" style="cursor:pointer;"  /></td>
                                                                              <td style="width: 12%; height: 26px;">
                                                                               <asp:Button ID="btnPrint" CssClass="button" runat="server" Text="Print" TabIndex="12"  AccessKey="P"/></td>
                                                                       </tr> 
                                                                        <tr>
                                                                            <td class="textbold" style="width: 3%">
                                                                            </td>
                                                                            <td class="textbold" style="width: 15%"> Airline
                                                                            </td>
                                                                            <td style="width: 70%"  colspan ="4"><asp:DropDownList ID="dlstAirline" runat="server" Width="561px" TabIndex="1" CssClass="dropdownlist" >
                                                                            </asp:DropDownList></td>
                                                                            <td style="width: 12%">
                                                                               <asp:Button ID="btnExport" CssClass="button" runat="server" Text="Export" TabIndex="13" AccessKey="E" /></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="width:3%" class="textbold" >
                                                                                &nbsp;</td>
                                                                            <td style="width:15%" class="textbold"><span class="textbold">City</span>
                                                                            </td>
                                                                            <td style="width:26%">
                                                                                <asp:DropDownList ID="dlstCity" runat="server" Width="190px" TabIndex="1" CssClass="dropdownlist" >
                                                                                </asp:DropDownList></td>
                                                                             <td style="width:3%" class="textbold" >
                                                                                &nbsp;</td>
                                                                            <td style="width:15%" class="textbold">
                                                                                Region&nbsp;</td>
                                                                            <td style="width:26%"><asp:DropDownList ID="dlstRegion" runat="server" Width="190px" TabIndex="1" CssClass="dropdownlist" >
                                                                            </asp:DropDownList></td>    
                                                                            <td style="width:12%">
                                                                               <asp:Button ID="btnReset" CssClass="button" runat="server" Text="Reset" TabIndex="14" AccessKey="R" /></td>
                                                                        </tr>                                                                       
                                                                         <tr>
                                                                            <td style="width:3%" class="textbold" >
                                                                                &nbsp;</td>
                                                                            <td style="width:15%" class="textbold"><span class="textbold">Country</span>
                                                                            </td>
                                                                            <td style="width:26%">
                                                                                <asp:DropDownList ID="dlstCountry" runat="server" Width="190px" TabIndex="1" CssClass="dropdownlist" >
                                                                                </asp:DropDownList></td>
                                                                             <td style="width:3%" class="textbold" >
                                                                                &nbsp;</td>
                                                                            <td style="width:15%" class="textbold"><span class="textbold">Aoffice</span></td>
                                                                            <td style="width:26%">                                                                              <asp:DropDownList ID="dlstAoffice" runat="server" Width="190px" TabIndex="1" CssClass="dropdownlist" >
                                                                            </asp:DropDownList></td>    
                                                                            <td style="width:12%">
                                                                               </td>
                                                                        </tr>                                                                       
                                        <tr>
                                            <td class="textbold" style="width: 3%">
                                            </td>
                                            <td class="textbold" style="width: 15%;height: 26px">
                                                Lcode</td>
                                            <td style="height: 26px;">
                                                <asp:TextBox ID="txtLcode" runat="server" CssClass="textbox" Width="190px" Height="18px" TabIndex="1"></asp:TextBox></td>
                                            <td class="textbold" style="width: 3%">
                                            </td>
                                            <td class="textbold" style="width: 15%;height: 26px">
                                                Chain Code</td>
                                            <td>
                                                <asp:TextBox ID="txtChaincode" runat="server" CssClass="textbox" Width="190px" Height="18px" TabIndex="1"></asp:TextBox></td>
                                            <td style="width: 12%">
                                            </td>
                                        </tr>
                                                                                                                                       
                                                                          <tr>
                                                                            <td style="width:3%" class="textbold" >
                                                                                &nbsp;</td>
                                                                            <td style="width:15%" class="textbold">
                                                                                From<%--<span class="Mandatory">*</span>--%>
                                                                            </td>
                                                                            <td style="width:26%">
                                                                                <asp:DropDownList ID="dlstMonthFrom" runat="server" Width="100px" TabIndex="1" CssClass="dropdownlist" ><asp:ListItem Value="1">January</asp:ListItem>
                                                                                            <asp:ListItem Value="2">February</asp:ListItem>
                                                                                            <asp:ListItem Value="3">March</asp:ListItem>
                                                                                            <asp:ListItem Value="4">April</asp:ListItem>
                                                                                            <asp:ListItem Value="5">May</asp:ListItem>
                                                                                            <asp:ListItem Value="6">June</asp:ListItem>
                                                                                            <asp:ListItem Value="7">July</asp:ListItem>
                                                                                            <asp:ListItem Value="8">August</asp:ListItem>
                                                                                            <asp:ListItem Value="9">September</asp:ListItem>
                                                                                            <asp:ListItem Value="10">October</asp:ListItem>
                                                                                            <asp:ListItem Value="11">November</asp:ListItem>
                                                                                            <asp:ListItem Value="12">December</asp:ListItem>
                                                                                </asp:DropDownList>&nbsp;&nbsp;<asp:DropDownList ID="dlstYearFrom" runat="server" Width="83px" TabIndex="1" CssClass="dropdownlist" >
                                                                                </asp:DropDownList></td>
                                                                             <td style="width:3%" class="textbold" >
                                                                                &nbsp;</td>
                                                                            <td style="width:15%" class="textbold"><span class="textbold">To</span><%--<span class="Mandatory">*</span>--%>
                                                                            </td>
                                                                            <td style="width:26%"><asp:DropDownList ID="dlstMonthTo" runat="server" Width="100px" TabIndex="1" CssClass="dropdownlist" ><asp:ListItem Value="1">January</asp:ListItem>
                                                                                            <asp:ListItem Value="2">February</asp:ListItem>
                                                                                            <asp:ListItem Value="3">March</asp:ListItem>
                                                                                            <asp:ListItem Value="4">April</asp:ListItem>
                                                                                            <asp:ListItem Value="5">May</asp:ListItem>
                                                                                            <asp:ListItem Value="6">June</asp:ListItem>
                                                                                            <asp:ListItem Value="7">July</asp:ListItem>
                                                                                            <asp:ListItem Value="8">August</asp:ListItem>
                                                                                            <asp:ListItem Value="9">September</asp:ListItem>
                                                                                            <asp:ListItem Value="10">October</asp:ListItem>
                                                                                            <asp:ListItem Value="11">November</asp:ListItem>
                                                                                            <asp:ListItem Value="12">December</asp:ListItem>
                                                                                </asp:DropDownList>&nbsp;&nbsp;<asp:DropDownList ID="dlstYearTo" runat="server" Width="82px" TabIndex="1" CssClass="dropdownlist" >
                                                                                </asp:DropDownList>                                                                              
                                                                                </td>    
                                                                            <td style="width:12%">
                                                                               </td>
                                                                        </tr>  
                                                                        <tr>
                                                                           <td colspan ="7"> <input type="hidden" id="hdAgencyName" runat="server" value="" style="width: 5px" />
                                                                               &nbsp;&nbsp;
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
    </form>
</body>
</html>