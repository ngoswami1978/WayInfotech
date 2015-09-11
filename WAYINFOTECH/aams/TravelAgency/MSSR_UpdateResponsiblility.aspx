<%@ Page Language="VB" AutoEventWireup="false" CodeFile="MSSR_UpdateResponsiblility.aspx.vb"
    ValidateRequest="false" Inherits="TravelAgency_MSSR_UpdateResponsiblility" %>

<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
    <link href="../CSS/AAMS.css" type="text/css" rel="stylesheet" />

    <script src="../JavaScript/AAMS.js" type="text/javascript"></script>

    <link rel="stylesheet" type="text/css" media="all" href="../Calender/calendar-blue.css"
        title="win2k-cold-1" />
    <!-- import the calendar script -->

    <script type="text/javascript" src="../Calender/calendar.js"></script>

    <!-- import the language module -->

    <script type="text/javascript" src="../Calender/lang/calendar-en.js"></script>

    <!-- import the calendar setup module -->

    <script type="text/javascript" src="../Calender/calendar-setup.js"></script>

    <script type="text/javascript">
     
     function HistoryForResUpdate(Lcode)
     {
       var parameter="LCODE=" + Lcode ;
       type = "../Popup/PUSR_UpdateAResHistory.aspx?" + parameter;
       window.open(type,"aa","height=600,width=800,top=30,left=20,scrollbars=1,status=1");            
       return false;
     }
     function PopupPageEmployeeRes(id,ctrlid)
        {
        
             if (id=="1")
             {                
                var strEmployeePageName=document.getElementById("hdEmployeePageName").value;
                document.getElementById("hdCtrlId").value = ctrlid
                if (strEmployeePageName!="")
                {
                    type = "../Setup/" + strEmployeePageName+ "?Popup=T&ctrlId="+ctrlid;   
   	                window.open(type,"EmployeeRes","height=600,width=900,top=30,left=20,scrollbars=1,status=1");     	
   	                return false;
   	            }
             }
        }
        
     
     function CheckValidation()
     {
     {debugger;}
     var mandatory =false;
       
       if (document.getElementById ('txtAgencyName').value =='' )
     {
        if (document.getElementById ('txtGroupName').value =='' )
        {
           if (document.getElementById ('dlstCity').selectedIndex ==0 )
             {        
                if (document.getElementById ('dlstCountry').selectedIndex ==0 )
                 {
                     if (document.getElementById ('dlstAoffice').selectedIndex ==0 )
                         {
                               if (document.getElementById ('txtOfficeID').value =='' )
                             {
                                   if (document.getElementById ('dlstCRS').selectedIndex ==0 )
                                     {
                                         if (document.getElementById ('dlstPriority').selectedIndex ==0 )
                                         {
                                             if (document.getElementById ('dlstOnlineStatus').selectedIndex ==0 )
                                             {
                                                    if (document.getElementById ('dlstAType').selectedIndex ==0 )
                                                   {
                                                        if (document.getElementById ('dlstPriority').selectedIndex ==0 )
                                                       {
                                                               if (document.getElementById ('txtResFrom').value =='' )
                                                               {
                                                                  if (document.getElementById ('txtResTo').value =='' )
                                                                    {
                                                                      // mandatory=true;
                                                                    }                                                                 
                                                               }
                                                       }
                                                   }   
                                             }
                                         }
                                     }                               
                             }                             
                         }                 
                 }
             
//               document.getElementById ('lblError').innerHTML='City is mandatory.';
//               document.getElementById ('dlstCity').focus();
//               return false;
             }    
        }
     }
     
       if (mandatory==true )
       {
               document.getElementById ('lblError').innerHTML='At least one item is mandatory.';
               document.getElementById ('txtAgencyName').focus();
               return false;
       
       }
     
     
     }
      function ResetLcode()
     {
        IE4 = (document.all);
        NS4 = (document.layers);
        var whichASC;
        whichASC = (NS4) ? e.which : event.keyCode;
          if(whichASC!=9 && whichASC!=18 && whichASC!=13)
        {
        document.getElementById("hdAgencyName").value="";     
        }
     }
       function ResetChainCode()
     {
        IE4 = (document.all);
        NS4 = (document.layers);
        var whichASC;
        whichASC = (NS4) ? e.which : event.keyCode;
          if(whichASC!=9 && whichASC!=18 && whichASC!=13)
        {
        document.getElementById("hdChainId").value="";     
        }
     }
          
     function EmployeePage()
{
         var type;   
       //  var strEmployeePageName=document.getElementById("hdEmployeePageName").value;
        if (strEmployeePageName!="")
        {   
           type = "../Setup/" + strEmployeePageName+ "?Popup=T" ;
   	        window.open(type,"aa","height=600,width=910,top=30,left=20,scrollbars=1,status=1");	
            return false;
        }
}
     function PopupAgencyPage()
        {
            var type;
            type = "../TravelAgency/TASR_Agency.aspx?Popup=T" ;
   	        window.open(type,"TAG","height=600,width=900,top=30,left=20,scrollbars=1,status=1");	
            return false;
        }    
  function PopupAgencyGroup()
{
            var type;
            type = "../Setup/MSSR_ManageAgencyGroup.aspx?Popup=T" ;
   	        window.open(type,"TAG","height=600,width=880,top=30,left=20,scrollbars=1,status=1");	
            return false;

}       
    
    </script>

</head>
<body>
    <form id="form1" runat="server" defaultbutton="btnSearch" defaultfocus="txtAgencyName">
        <asp:ScriptManager ID="ScriptManager1" runat="server" AsyncPostBackTimeout="660">
        </asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <table style="width: 845px" align="left" class="border_rightred">
                    <tr>
                        <td valign="top" style="width: 860px;">
                            <table style="width: 860px;">
                                <tr>
                                    <td valign="top" align="left">
                                        <span class="menu">Travel Agency-&gt;</span><span class="sub_menu">1A Responsibility</span></td>
                                </tr>
                                <tr>
                                    <td class="heading" align="center" valign="top">
                                        Search 1A Responsibility</td>
                                </tr>
                                <tr>
                                    <td valign="top" align="LEFT" style="width: 860px;">
                                        <table style="width: 860px;" border="0" cellspacing="0" cellpadding="0">
                                            <tr>
                                                <td align="left" class="redborder" style="width: 860px">
                                                    <table border="0" cellpadding="2" cellspacing="1" style="width: 100%" class="left">
                                                        <tr>
                                                            <td class="center" colspan="6">
                                                                <asp:Label ID="lblError" runat="server" CssClass="ErrorMsg" EnableViewState="False"></asp:Label></td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <input id="hdAgencyName" runat="server" style="width: 1px" type="hidden" /></td>
                                                            <td class="textbold" style="width: 131px">
                                                                Agency Name</td>
                                                            <td colspan="3">
                                                                <asp:TextBox ID="txtAgencyName" runat="server" CssClass="textbox TextTitleCase" MaxLength="100"
                                                                    TabIndex="1" Width="528px"></asp:TextBox><img id="Img2" runat="server" alt="Select & Add Agency Name" onclick="PopupAgencyPage()"
                                                                    src="../Images/lookup.gif" style="cursor: pointer;" /></td>
                                                            <td style="width: 120px;" class="center">
                                                                <asp:Button ID="btnSearch" CssClass="button" runat="server" Text="Search" TabIndex="2"
                                                                    AccessKey="a" /></td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <input id="hdChainId" runat="server" style="width: 1px" type="hidden" /></td>
                                                            <td class="textbold" style="width: 131px">
                                                                Group Name</td>
                                                            <td colspan="3">
                                                                <asp:TextBox ID="txtGroupName" runat="server" CssClass="textfield" EnableViewState="False"
                                                                    MaxLength="40" TabIndex="1" Width="528px"></asp:TextBox><img src="../Images/lookup.gif"
                                                                        onclick="javascript:return PopupAgencyGroup();" id="ImgAGroup" style="cursor: pointer;"
                                                                        alt="" runat="server" /></td>
                                                            <td style="width: 120px;" class="center">
                                                                <asp:Button ID="BtnAllocate" CssClass="button" runat="server" Text="Allocate" TabIndex="2"
                                                                    AccessKey="a" /></td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 3%">
                                                            </td>
                                                            <td class="textbold" style="width: 131px">
                                                                City</td>
                                                            <td style="width: 27%">
                                                                <asp:DropDownList onkeyup="gotop(this.id)" ID="dlstCity" runat="server" CssClass="dropdownlist"
                                                                    Width="176px" TabIndex="1">
                                                                </asp:DropDownList></td>
                                                            <td class="textbold" style="width: 15%">
                                                                Country</td>
                                                            <td style="width: 26%">
                                                                <asp:DropDownList onkeyup="gotop(this.id)" ID="dlstCountry" runat="server" CssClass="dropdownlist"
                                                                    Width="176px" TabIndex="1">
                                                                </asp:DropDownList></td>
                                                            <td class="center" style="width: 13%">
                                                                <asp:Button ID="BtnDeAllocate" CssClass="button" runat="server" Text="DeAllocate"
                                                                    TabIndex="2" AccessKey="n" /></td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                            </td>
                                                            <td class="textbold" style="width: 131px">
                                                                1A Office</td>
                                                            <td>
                                                                <asp:DropDownList onkeyup="gotop(this.id)" ID="dlstAoffice" runat="server" CssClass="dropdownlist"
                                                                    Width="176px" TabIndex="1">
                                                                </asp:DropDownList></td>
                                                            <td class="textbold">
                                                                Office ID</td>
                                                            <td>
                                                                <asp:TextBox ID="txtOfficeID" runat="server" CssClass="textbox" MaxLength="10" TabIndex="1"
                                                                    Width="170px"></asp:TextBox>&nbsp;</td>
                                                            <td class="center">
                                                                <asp:Button ID="btnReset" CssClass="button" runat="server" Text="Reset" TabIndex="2"
                                                                    AccessKey="r" /></td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                            </td>
                                                            <td class="textbold" style="width: 131px">
                                                                CRS</td>
                                                            <td>
                                                                <asp:DropDownList onkeyup="gotop(this.id)" ID="dlstCRS" runat="server" CssClass="dropdownlist"
                                                                    Width="176px"  TabIndex="1">
                                                                </asp:DropDownList></td>
                                                            <td class="textbold">
                                                                Online Status</td>
                                                            <td>
                                                                <asp:DropDownList onkeyup="gotop(this.id)" ID="dlstOnlineStatus" runat="server" CssClass="dropdownlist"
                                                                    Width="176px" TabIndex="1">
                                                                    <asp:ListItem Selected="True">All</asp:ListItem>
                                                                </asp:DropDownList></td>
                                                            <td class="center">
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                            </td>
                                                            <td class="textbold" style="width: 131px">
                                                                Agency Type</td>
                                                            <td>
                                                                <asp:DropDownList ID="dlstAType" runat="server" CssClass="dropdownlist" Width="176px"
                                                                    TabIndex="1" onkeyup="gotop(this.id)">
                                                                    <asp:ListItem Selected="True">All</asp:ListItem>
                                                                </asp:DropDownList></td>
                                                            <td class="textbold">
                                                                Priority</td>
                                                            <td>
                                                                <asp:DropDownList onkeyup="gotop(this.id)" ID="dlstPriority" runat="server" CssClass="dropdownlist"
                                                                    Width="176px" TabIndex="1">
                                                                    <asp:ListItem Selected="True">All</asp:ListItem>
                                                                </asp:DropDownList></td>
                                                            <td>
                                                            </td>
                                                        </tr>  
                                                        
                                                         <tr>
                                                            <td>
                                                            </td>
                                                            <td class="textbold" style="width: 131px">
                                                                Company Vertical</td>
                                                            <td>
                                                                <asp:DropDownList  onkeyup="gotop(this.id)"  ID="DlstCompVertical" CssClass="dropdownlist" Width="176px" runat="server" TabIndex="1">
                                                    </asp:DropDownList></td>
                                                            <td class="textbold">
                                                                </td>
                                                            <td>
                                                              </td>
                                                            <td>
                                                            </td>
                                                        </tr>  
                                                          <tr >                                                         
                                                            <td    colspan ="6" align="center" > 
                                                            </td>                                                           
                                                        </tr>                                                           
                                                          <tr >                                                         
                                                            <td  class="subheading"  colspan ="6" align="center" ><b>Ressponsibility</b> 
                                                            </td>                                                           
                                                        </tr>                                                       
                                                        <tr>
                                                            <td>
                                                            </td>
                                                            <td class="textbold" style="width: 131px">
                                                                1A Responsibility From</td>
                                                            <td>
                                                            <asp:TextBox id="txtResFrom" tabIndex=1 runat="server" CssClass="textbox" MaxLength="40" Width="169px"></asp:TextBox><img id="Img1" runat="server" alt="" onclick="PopupPageEmployeeRes(1,'txtResFrom')"  src="../Images/lookup.gif" style="cursor: pointer" />&nbsp;
                                                            </td>
                                                            <td class="textbold">
                                                                1A Responsibility To</td>
                                                            <td>
                                                                <asp:TextBox id="txtResTo" tabIndex=1 runat="server" CssClass="textboxgrey" MaxLength="40" Width="169px"></asp:TextBox><img id="Img3" runat="server" alt="" onclick="PopupPageEmployeeRes(1,'txtResTo')"  src="../Images/lookup.gif" style="cursor: pointer" />&nbsp;
                                                            </td>
                                                            <td>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                            </td>
                                                            <td style="width: 131px">
                                                                &nbsp;
                                                            </td>
                                                            <td colspan="4">
                                                                &nbsp; &nbsp; &nbsp; &nbsp;
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="textbold" align="center" valign="TOP" style="height: 10px;">
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                        <tr>
                                                        <td align="center" style="width: 830px;">
                                                            <asp:UpdateProgress ID="UpDateProgress2" AssociatedUpdatePanelID="UpdatePanel1" DisplayAfter="10"
                                                                runat="server">
                                                                <ProgressTemplate>
                                                                    <img alt="Loading.." src="../Images/loading.gif" id="imgLoad2" runat="server" />
                                                                </ProgressTemplate>
                                                            </asp:UpdateProgress>
                                                        </td>
                                                    </tr>
                    <tr>
                        <td valign="top" style="width: 860px;">
                            <table width="100%" cellpadding="0" cellspacing="0">
                                
                                <tr>
                                    <td>
                                        <asp:UpdatePanel ID="UPNLRes" runat="server">
                                            <ContentTemplate>
                                                <table>
                                                    <tr>
                                                        <td colspan="8" align="Left">
                                                            <asp:Button ID="btnSelectAll" runat="server" Text="Select All" CssClass="button"
                                                                Visible="False" Width="80px" />
                                                            <asp:Button ID="btnDeSelectAll" runat="server" Text="DeSelect All" CssClass="button"
                                                                Visible="False" />&nbsp;
                                                        </td>
                                                    </tr>
                                                 <%--   <tr>
                                                        <td colspan="8" align="center" style="width: 830px;">
                                                            <asp:UpdateProgress ID="UpDateProgress1" AssociatedUpdatePanelID="UPNLRes" DisplayAfter="10"
                                                                runat="server">
                                                                <ProgressTemplate>
                                                                    <img alt="Loading.." src="../Images/loading.gif" id="imgLoad" runat="server" />
                                                                </ProgressTemplate>
                                                            </asp:UpdateProgress>
                                                        </td>
                                                    </tr>   --%>                                                
                                                    <tr>
                                                        <td valign="top" style="padding-left: 4px;">
                                                            <table width="1250px" border="0" cellspacing="0" cellpadding="0" align="left">
                                                                <tr>
                                                                    <td style="width: 1250px;" class="redborder" valign="top">
                                                                        <asp:GridView ID="gvAResp" runat="server" PageSize="25" AutoGenerateColumns="False"
                                                                            TabIndex="18" Width="1250px" EnableViewState="true" RowStyle-VerticalAlign="Top"
                                                                            AllowSorting="true" HeaderStyle-ForeColor="white" AllowPaging="true" HeaderStyle-HorizontalAlign ="Left" >
                                                                            <Columns>
                                                                                <asp:TemplateField HeaderStyle-Width="50px">
                                                                                    <HeaderTemplate>
                                                                                        <asp:Label ID="lblAction" runat="server" Width="50px" Text="Select"></asp:Label>
                                                                                         <asp:CheckBox ID="chkCheckedHeader" runat="server" Checked="false" AutoPostBack="false" Visible ="false" 
                                                                                            OnCheckedChanged="chkCheckedHeader_CheckedChanged" />
                                                                                    </HeaderTemplate>
                                                                                    <ItemTemplate>
                                                                                        <asp:CheckBox ID="chkChecked" runat="server" Checked="false" AutoPostBack="false"
                                                                                            OnCheckedChanged="chkChecked_CheckedChanged" />
                                                                                        <asp:HiddenField ID="hdLCode" runat="server" Value='<%#Eval("LOCATION_CODE")%>' />
                                                                                        <asp:HiddenField ID="HdCheckedUnChecked" runat="server" Value='<%#Eval("GET_CHECKED")%>' />
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:BoundField DataField="CHAIN_CODE" HeaderText="Chain Code" SortExpression="CHAIN_CODE" />
                                                                                <asp:BoundField DataField="GROUP_NAME" HeaderText="Group Name" SortExpression="GROUP_NAME" />
                                                                                <asp:BoundField DataField="LOCATION_CODE" HeaderText="LCode" SortExpression="LOCATION_CODE" />
                                                                                <asp:BoundField DataField="NAME" HeaderText="Agency Name" SortExpression="NAME" />
                                                                                <asp:BoundField DataField="OFFICEID" HeaderText="Office ID" SortExpression="OFFICEID" />
                                                                                <asp:BoundField DataField="ADDRESS" HeaderText="Address1" SortExpression="ADDRESS" />
                                                                                <asp:BoundField DataField="ADDRESS1" HeaderText="Address2" SortExpression="ADDRESS1" />
                                                                                <asp:BoundField DataField="CITY_NAME" HeaderText="City" SortExpression="CITY_NAME" />
                                                                                <asp:BoundField DataField="COUNTRY_NAME" HeaderText="Country" SortExpression="COUNTRY_NAME" />
                                                                                <asp:BoundField DataField="ONLINE_STATUS" HeaderText="Online Status" SortExpression="ONLINE_STATUS" />
                                                                                <asp:BoundField DataField="RESP_ASSGN_FROM" HeaderText="1A Responsible" SortExpression="RESP_ASSGN_FROM" />
                                                                               <%-- <asp:BoundField DataField="RESP_ASSGN_TO" HeaderText="Responsiblity To" SortExpression="RESP_ASSGN_TO" />--%>
                                                                                <asp:TemplateField HeaderText="Action  " HeaderStyle-HorizontalAlign ="Center"  >
                                                                                    <ItemTemplate><asp:LinkButton ID="lnkHistory" runat="server" CommandName="HistoryX" Text="History"
                                                                                            CssClass="LinkButtons" Width="50px"></asp:LinkButton>                                                                                        
                                                                                    </ItemTemplate>
                                                                                   
                                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                                </asp:TemplateField>
                                                                            </Columns>
                                                                            <AlternatingRowStyle CssClass="lightblue" />
                                                                            <RowStyle CssClass="textbold" VerticalAlign="Top" />
                                                                            <HeaderStyle CssClass="Gridheading" />
                                                                            <PagerTemplate>
                                                                            </PagerTemplate>
                                                                        </asp:GridView>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="textbold" align="center" valign="TOP" style="height: 10px;">
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td valign="top" style="width: 840px;">
                                                            <asp:Panel ID="pnlPaging" runat="server" Visible="false" Width="100%">
                                                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                    <tr style="padding-top: 4pt; padding-bottom: 4pt">
                                                                        <td style="width: 30%" class="left">
                                                                            <span class="textbold"><b>&nbsp;No. of records found</b></span>&nbsp;&nbsp;<asp:TextBox
                                                                                ID="txtRecordCount" runat="server" CssClass="textboxgrey" MaxLength="3" TabIndex="19"
                                                                                Width="100px" ReadOnly="True" Text="0" Visible="True"></asp:TextBox></td>
                                                                        <td style="width: 25%" class="right">
                                                                            <asp:LinkButton ID="lnkPrev" CssClass="LinkButtons" runat="server" CommandName="Prev"><< Prev</asp:LinkButton></td>
                                                                        <td style="width: 20%" class="center">
                                                                            <span class="textbold"><b>Page No</b></span>&nbsp;&nbsp;<asp:DropDownList onkeyup="gotop(this.id)"
                                                                                ID="ddlPageNumber" Width="70px" CssClass="dropdownlist" runat="server" AutoPostBack="True">
                                                                            </asp:DropDownList></td>
                                                                        <td style="width: 25%" class="left">
                                                                            <asp:LinkButton ID="lnkNext" runat="server" CssClass="LinkButtons" CommandName="Next">Next >></asp:LinkButton></td>
                                                                    </tr>
                                                                </table>
                                                            </asp:Panel>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="height: 53px">
                                                            <asp:HiddenField ID="hdUpdateForSessionXml" runat="server" />
                                                            <asp:HiddenField ID="hdChechedItem" runat="server" />
                                                             <asp:HiddenField ID="hdCtrlId" runat="server" />                                                            
                                                             <input id="hdEmployeePageName" runat="server" style="width: 1px" type="hidden" />
                                                             <input style="WIDTH: 1px" id="hdResFrom" type="hidden" runat="server" />
                                                             <input style="WIDTH: 1px" id="hdResTo" type="hidden" runat="server" />
                                                            <asp:TextBox ID="txtRecordOnCurrentPage" runat="server" CssClass="textboxgrey" Visible="false"
                                                                Width="73px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <!-- Code by Dev Abhishek -->
            </ContentTemplate>
        </asp:UpdatePanel>
    </form>
</body>
</html>
