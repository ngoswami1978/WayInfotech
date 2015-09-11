<%@ Page Language="VB" AutoEventWireup="false" CodeFile="PUSR_BOHelpDeskSearchConfigure.aspx.vb"
    Inherits="Popup_PUSR_BOHelpDeskSearchConfigure" %>

<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>HelpDesk:Configure Search</title>

    <script src="../JavaScript/AAMS.js" type="text/javascript"></script>

    <%-- <base target="_self"/>--%>
    <link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />
    
 <link rel="stylesheet" type="text/css" media="all" href="../Calender/calendar-blue.css"
        title="win2k-cold-1" />

    <script type="text/javascript" src="../Calender/calendar.js"></script>

    <!-- import the language module -->

    <script type="text/javascript" src="../Calender/lang/calendar-en.js"></script>

    <!-- import the calendar setup module -->

    <script type="text/javascript" src="../Calender/calendar-setup.js"></script>



</head>
<body oncontextmenu=" return false;">
    <form id="frmConfigSearch" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server" AsyncPostBackTimeout="660">
        </asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" >
            <ContentTemplate>
                <table width="860px" align="left" height="486px">
                    <tr>
                        <td valign="top">
                            <table width="100%" align="left">
                                <tr>
                                    <td align="right">
                                        <a href="#" class="LinkButtons" onclick="window.opener.location.href ='../BackOfficeHelpDesk/BOHDRPT_HelpDeskDynamicReport.aspx?Reload=T';window.close();">
                                            Close</a>&nbsp;&nbsp;&nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="heading" align="center" valign="top">
                                        Configure Search</td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                            <tr>
                                                <td align="LEFT" class="redborder">
                                                    <table width="100%" border="0" align="left" cellpadding="0" cellspacing="0">
                                                        <tr>
                                                            <td class="textbold" colspan="6" align="center" height="25px" valign="TOP">
                                                                <asp:Label ID="lblError" runat="server" CssClass="ErrorMsg" Text=""></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <%-- <tr>
                                                                 <td  class="textbold"   style ="width:70%;"  colspan ="5"  valign="TOP">&nbsp;                                                             
                                                                </td>
                                                                
                                                                <td  class="textbold" align="left"   style ="width:30%"  valign="TOP">
                                                                    &nbsp;</td>
                                                            </tr>
                                                               <tr>
                                                                 <td  class="textbold"   style ="width:70%;"  colspan ="5"  valign="TOP">&nbsp;                                                             
                                                                </td>
                                                                
                                                                <td  class="textbold" align="left"   style ="width:30%;height:25px"  valign="TOP"></td> 
                                                                   
                                                            </tr>--%>
                                                        <tr>
                                                            <td style="height: 19px">
                                                            </td>
                                                            <td class="textbold" style="height: 19px">
                                                                &nbsp; &nbsp; &nbsp; &nbsp;Select Set &nbsp; &nbsp; &nbsp;
                                                                <asp:DropDownList ID="drpSelectSet" runat="server" CssClass="dropdownlist" Width="70px"
                                                                    TabIndex="29" AutoPostBack="True">
                                                                </asp:DropDownList></td>
                                                            <td colspan="3" style="height: 19px">
                                                            </td>
                                                            <td style="width: 12%; height: 19px;" class="center">
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="textbold" style="width: 60%;" colspan="5" valign="TOP">
                                                                &nbsp;
                                                            </td>
                                                            <td class="textbold" align="left" style="width: 40%; height: 25px" valign="TOP">
                                                                <asp:Button ID="btnNewSet" CssClass="button" runat="server" Text="New Set" />
                                                                <asp:Button ID="btnSave" CssClass="button" runat="server" Text="Save" />
                                                                <asp:Button ID="btnSelectAll" CssClass="button" runat="server" Text="Select All" />&nbsp;
                                                                <asp:Button ID="btnDeSelectAll" CssClass="button" runat="server" Text="DeSelect All" /></td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="6" height="4" align="center">
                                                                <asp:GridView ID="gvHelpDeskSearchCofig" runat="server" BorderWidth="1" BorderColor="#d4d0c8"
                                                                    AutoGenerateColumns="False" Width="95%" TabIndex="9" OnRowDataBound="gvHelpDeskSearchCofig_RowDataBound" >
                                                                    <Columns>
                                                                        <asp:BoundField DataField="DM_FIELD_NAME" HeaderText="Field Name" ItemStyle-HorizontalAlign="Left"
                                                                            HeaderStyle-HorizontalAlign="Left" />
                                                                        <asp:TemplateField HeaderText="Select/Deselect" ItemStyle-HorizontalAlign="Left"
                                                                            HeaderStyle-HorizontalAlign="Left">
                                                                            <ItemTemplate>
                                                                                <asp:CheckBox ID="chkSelected" runat="server" Checked='<%# GetEnableorDisableOrChecked(Eval("DM_SEARCH_FIELD"),Eval("DM_SEARCH_DEFAULT")) %>'
                                                                                 OnCheckedChanged="chkSelected_CheckedChanged" AutoPostBack ="true"         Enabled='<%# GetEnableorDisable(Eval("DM_SEARCH_DEFAULT")) %>' />
                                                                                <asp:HiddenField ID="DM_FIELD_ID" Value='<% # Eval("DM_FIELD_ID") %>' runat="server" />
                                                                                <asp:HiddenField ID="DM_SEARCH_DEFAULT" Value='<% # Eval("DM_SEARCH_DEFAULT") %>'
                                                                                    runat="server" />
                                                                                     <asp:HiddenField ID="HdValue" Value='<% # Eval("Value") %>'
                                                                                    runat="server" />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Set Default Value" ItemStyle-HorizontalAlign="Left"
                                                                            HeaderStyle-HorizontalAlign="Left">
                                                                            <ItemTemplate>
                                                                                <asp:TextBox ID="txtAgencyName" runat="server" CssClass="textbox" Width="170px" MaxLength="30" Visible ="false" TabIndex ="1" ></asp:TextBox>
                                                                                <asp:TextBox ID="txtLTRNo" runat="server" CssClass="textbox" Width="170px" MaxLength="9" Visible ="false"  TabIndex ="1" ></asp:TextBox>
                                                                                <asp:TextBox ID="txtPTRNo"  TabIndex ="1" runat="server" CssClass="textbox" Width="170px"
                                                                                    MaxLength="9" Visible ="false" ></asp:TextBox>
                                                                                <asp:TextBox ID="txtOfficeID"  TabIndex ="1" runat="server" CssClass="textbox" Width="170px"
                                                                                    MaxLength="9" Visible ="false" ></asp:TextBox>
                                                                                <asp:DropDownList  ID="DlstCompVertical"  TabIndex ="1" onkeyup="gotop(this.id)" runat="server" Visible ="false"
                                                                                    CssClass="dropdownlist" Width="176px" AutoPostBack="false" ></asp:DropDownList> 
                                                                                <asp:TextBox ID="TextBox1" TabIndex ="1" runat="server" CssClass="textbox" Width="170px"
                                                                                    MaxLength="9" Visible ="false"></asp:TextBox>
                                                                                <asp:TextBox ID="txtLoggedBy" TabIndex ="1" runat="server" CssClass="textbox" Width="170px"
                                                                                    MaxLength="30" Visible ="false"></asp:TextBox>
                                                                                <asp:TextBox ID="txtCallerName"  TabIndex ="1" runat="server" CssClass="textbox" Width="170px"
                                                                                    MaxLength="30" Visible ="false"></asp:TextBox>
                                                                                <asp:TextBox ID="txtWorkOrderNo"  TabIndex ="1" runat="server" CssClass="textbox"
                                                                                    Width="170px" MaxLength="9" Visible ="false"></asp:TextBox>
                                                                                <asp:TextBox ID="txtAddresses"  TabIndex ="1" runat="server" CssClass="textbox" Width="170px"
                                                                                    MaxLength="100" Visible ="false"></asp:TextBox>
                                                                                <asp:TextBox ID="txtTit"  TabIndex ="1" runat="server" CssClass="textbox" Width="170px"
                                                                                    MaxLength="100" Visible ="false"></asp:TextBox>
                                                                                <asp:DropDownList  ID="ddlQueryGroup"  TabIndex ="1" onkeyup="gotop(this.id)" runat="server" Visible ="false"
                                                                                   OnSelectedIndexChanged ="ddlQueryGroup_SelectedIndexChanged"  CssClass="dropdownlist" Width="176px" AutoPostBack="True">
                                                                                    <asp:ListItem Selected="True" Value="">--All--</asp:ListItem>
                                                                                    <asp:ListItem Value="1">Functional</asp:ListItem>
                                                                                    <asp:ListItem Value="2">Technical</asp:ListItem>
                                                                                </asp:DropDownList>
                                                                                <asp:DropDownList ID="ddlQuerySubGroup"  TabIndex ="1" onkeyup="gotop(this.id)" runat="server" Visible ="false" OnSelectedIndexChanged ="ddlQuerySubGroup_SelectedIndexChanged"
                                                                                    CssClass="dropdownlist" Width="176px" AutoPostBack="True">
                                                                                    <asp:ListItem Selected="True">--All--</asp:ListItem>
                                                                                </asp:DropDownList>
                                                                                <asp:DropDownList ID="ddlQueryCategory"  TabIndex ="1" onkeyup="gotop(this.id)" runat="server" Visible ="false" OnSelectedIndexChanged="ddlQueryCategory_SelectedIndexChanged"
                                                                                    CssClass="dropdownlist" Width="176px" AutoPostBack="True">
                                                                                    <asp:ListItem Selected="True" Value="">--All--</asp:ListItem>
                                                                                </asp:DropDownList>
                                                                                <asp:DropDownList ID="ddlQuerySubCategory"  TabIndex ="1" onkeyup="gotop(this.id)" Visible ="false"
                                                                                    runat="server" CssClass="dropdownlist" Width="176px">
                                                                                    <asp:ListItem Selected="True">--All--</asp:ListItem>
                                                                                </asp:DropDownList>
                                                                                <asp:DropDownList ID="ddlQueryStatus"  TabIndex ="1" onkeyup="gotop(this.id)" runat="server" Visible ="false"
                                                                                    CssClass="dropdownlist" Width="176px">
                                                                                    <asp:ListItem Selected="True" Value="">--All--</asp:ListItem>
                                                                                </asp:DropDownList>
                                                                                <asp:DropDownList ID="ddlQueryPriority"  TabIndex ="1" onkeyup="gotop(this.id)" runat="server" Visible ="false"
                                                                                    CssClass="dropdownlist" Width="176px">
                                                                                    <asp:ListItem Selected="True">--All--</asp:ListItem>
                                                                                </asp:DropDownList>
                                                                                <asp:DropDownList ID="ddlCoordinator1"  TabIndex ="1" onkeyup="gotop(this.id)" runat="server" Visible ="false"
                                                                                    CssClass="dropdownlist" Width="176px">
                                                                                </asp:DropDownList>
                                                                                <asp:DropDownList ID="ddlCoordinator2"  TabIndex ="1" onkeyup="gotop(this.id)" runat="server" Visible ="false"
                                                                                    CssClass="dropdownlist" Width="176px">
                                                                                </asp:DropDownList>
                                                                                <asp:DropDownList ID="ddlDisposition"  TabIndex ="1" onkeyup="gotop(this.id)" runat="server" Visible ="false"
                                                                                    CssClass="dropdownlist" Width="176px">
                                                                                </asp:DropDownList>
                                                                                <asp:DropDownList ID="ddlAOffice"  TabIndex ="1" onkeyup="gotop(this.id)" runat="server" Visible ="false"
                                                                                    CssClass="dropdownlist" Width="176px">
                                                                                </asp:DropDownList>
                                                                                <asp:DropDownList ID="ddlAgencyAOffice"  TabIndex ="1" onkeyup="gotop(this.id)" runat="server" Visible ="false"
                                                                                    CssClass="dropdownlist" Width="176px">
                                                                                </asp:DropDownList>
                                                                                <asp:DropDownList ID="ddlFollowup"  TabIndex ="1" onkeyup="gotop(this.id)" runat="server" Visible ="false"
                                                                                    CssClass="dropdownlist" Width="176px">
                                                                                </asp:DropDownList>
                                                                                <asp:DropDownList ID="ddlAssignedTo"  TabIndex ="1" onkeyup="gotop(this.id)" runat="server" Visible ="false"
                                                                                    CssClass="dropdownlist" Width="176px">
                                                                                </asp:DropDownList>
                                                                                <asp:DropDownList ID="ddlContactType"  TabIndex ="1" onkeyup="gotop(this.id)" runat="server" Visible ="false"
                                                                                    CssClass="textbold" Width="176px">
                                                                                </asp:DropDownList>
                                                                                 <asp:DropDownList ID="ddlCustomerCategory"  TabIndex ="1" onkeyup="gotop(this.id)" runat="server" Visible ="false"
                                                                                    CssClass="textbold" Width="176px">
                                                                                </asp:DropDownList>
                                                                                 <asp:DropDownList ID="DlstAgencyStatus"  TabIndex ="1" onkeyup="gotop(this.id)" runat="server" Visible ="false"
                                                                                    CssClass="textbold" Width="176px">
                                                                                </asp:DropDownList>
                                                                                 <asp:DropDownList ID="DlstAgencyType"  TabIndex ="1" onkeyup="gotop(this.id)" runat="server" Visible ="false"
                                                                                    CssClass="textbold" Width="176px">
                                                                                </asp:DropDownList>
                                                                                <asp:Panel ID="PnlOpenDateFrom" runat="server" Visible="false">
                                                                                    <asp:TextBox ID="txtQueryOpenedDateFrom" runat="server" CssClass="textbox" Width="138px"
                                                                                      Visible ="true"    TabIndex="1"></asp:TextBox>
                                                                                    <img tabindex="1" id="imgOpenedDateFrom" alt="" src="../Images/calender.gif" title="Date selector"
                                                                                    visible ="true"   runat ="server"   style="cursor: pointer" />
                                                                                
                                                                                </asp:Panel>
                                                                                 <asp:Panel ID="PnlOpenDateTo" runat="server" Visible="false">
                                                                                     <asp:TextBox ID="txtQueryOpenedDateTo" runat="server" CssClass="textbox" Width="138px" TabIndex="1"></asp:TextBox>
                                                                                    <img tabIndex="1" runat ="server"   id="imgOpenedDateTo" alt="" src="../Images/calender.gif"  title="Date selector" style="cursor: pointer" />
                                                                                </asp:Panel>
                                                                                 <asp:Panel ID="PnlCloseDateFrom" runat="server" Visible="false">
                                                                                      <asp:TextBox ID="txtCloseDateFrom" runat="server" CssClass="textbox" Width="138px" TabIndex="1"></asp:TextBox>
                                                                                    <img tabIndex="1"   runat ="server"  id="imgCloseDateFrom" alt="" src="../Images/calender.gif"  title="Date selector" style="cursor: pointer" />
                                                                                
                                                                                </asp:Panel>
                                                                                 <asp:Panel ID="PnlCloseDateTo" runat="server" Visible="false">
                                                                                     <asp:TextBox ID="txtCloseDateTo" runat="server" CssClass="textbox" Width="138px" TabIndex="1"></asp:TextBox>
                                                                                <img tabIndex="1"  runat ="server"  id="imgCloseDateTo" alt="" src="../Images/calender.gif"  title="Date selector" style="cursor: pointer" />
                                                                                
                                                                                </asp:Panel>
                                                                                 <asp:Panel ID="PnlDateAssigned" runat="server" Visible="false">
                                                                                  <asp:TextBox ID="txtDateAssigned" runat="server" CssClass="textbox" Width="138px" TabIndex="1"></asp:TextBox>
                                                                                    <img tabIndex="1"  runat ="server"  id="imgDateAssigned" alt="" src="../Images/calender.gif"  title="Date selector" style="cursor: pointer" />  
                                                                                
                                                                                </asp:Panel>
                                                                                <asp:CheckBox ID="ChkLastCall" runat ="server" Visible ="false" />
                                                                                <asp:TextBox ID="txtHD_IR_REF"  TabIndex ="1" runat="server" CssClass="textbox" Width="170px"
                                                                                    MaxLength="9" Visible ="false" ></asp:TextBox>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                    <HeaderStyle CssClass="Gridheading" />
                                                                    <RowStyle CssClass="ItemColor" HorizontalAlign="Left" />
                                                                    <AlternatingRowStyle CssClass="lightblue" HorizontalAlign="Left" />
                                                                </asp:GridView>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="6" height="12">
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="6" height="12">
                                                                <asp:HiddenField ID="hdAction" Value="" runat="server" />
                                                                &nbsp;
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </form>
</body>
<script type ="text/javascript" language ="javascript" >
   function SelectDate(textBoxid,imgId)
    {   
                 Calendar.setup
                 (
                     {
                           inputField : textBoxid,
                           ifFormat :"%d/%m/%Y %H:%M",
                           button :imgId,
                           onmousedown :true
                     }
                 )                            
                                                        
    }
    
     function SelectDateForAssignDate(textBoxid,imgId)
    {   
                 Calendar.setup
                 (
                     {
                           inputField : textBoxid,
                           ifFormat :"%d/%m/%Y",
                           button :imgId,
                           onmousedown :true
                     }
                 )                            
                                                        
    }
    
      function DateValidation(strDt)
    {
      // document.getElementById("lblError").innerHTML ="";
      // alert(strDt);
      var v=document.getElementById(strDt).value;
        if(v!='')
        {
           // alert(v)
            if (isDate(v,"dd/MM/yyyy HH:mm") == false)	
            {
            document.getElementById("lblError").innerHTML ="Invalid Date(dd/MM/yyyy HH:mm) format";
            document.getElementById(strDt).focus();
            return false;
            }
        }
        else
        {
       // document.getElementById("lblError").innerHTML ="";
        }
    }
    
       function DateValidationForAssignDate(strDt)
    {
     
      var v=document.getElementById(strDt).value;
        if(v!='')
        {
           // alert(v)
            if (isDate(v,"d/M/yyyy") == false)	
            {
            document.getElementById("lblError").innerHTML ="Invalid Date(dd/MM/yyyy) format";
            document.getElementById(strDt).focus();
            return false;
            }
        }
        else
        {
       // document.getElementById("lblError").innerHTML ="";
        }
    }
    
    
    
    function NumericValue (strValue,str)
    {
              //document.getElementById("lblError").innerHTML ="";
               var v=document.getElementById(strValue).value;
                if(v!='')
                {
                      if(IsDataValid(v,3)==false)
                        {
                        document.getElementById("lblError").innerHTML= '' + str + " is not valid.";
                        document.getElementById(strValue).focus();
                        return false;
                        } 
               }   
        
    }
    
    
    function FormValidation()
    {   
    
            try
            {   
                //{debugger;}
                var gridID,colDrp,ColTxt;                   
                
                if(document.getElementById("gvHelpDeskSearchCofig") !=null)
                {        
                        gridID=document.getElementById('<%=gvHelpDeskSearchCofig.ClientID%>');
                      
                      //  alert(gridID);
                      var FieldIdOpenDateFrom;
                      var FieldIdOpenDateTo;
                      var FieldIdCloseDateFrom;
                      var FieldIdCloseDateTo;
                      var FieldIdAsiignedDate;
                      
                        for(intcnt=1;intcnt<=gridID.rows.length-1;intcnt++)
                        {        
                             var FieldValue=gridID.rows[intcnt].cells[0].innerHTML;  
                           //  alert(FieldValue);
                             
                             if (gridID.rows[intcnt].cells[2].innerHTML !='')                 
                             {
                             
                                 var FieldId=gridID.rows[intcnt].cells[2].children[0].id ;  
                                   // alert(FieldId);
                             
                                    if (FieldId !=null)                 
                                      {
                                                if  (FieldValue== 'LTR No')
                                                 {
                                                        var v=document.getElementById(FieldId).value;
                                                        if(v!='')
                                                        {
                                                              if(IsDataValid(v,3)==false)
                                                                {
                                                                document.getElementById("lblError").innerHTML= 'LTR No is not valid.';
                                                                document.getElementById(FieldId).focus();
                                                                return false;
                                                                } 
                                                       }   
                                        
                                                 
                                                 }   
                                                   if  (FieldValue== 'PTR Number')
                                                 {
                                                              var v=document.getElementById(FieldId).value;
                                                                if(v!='')
                                                                {
                                                                      if(IsDataValid(v,3)==false)
                                                                        {
                                                                        document.getElementById("lblError").innerHTML= 'PTR No is not valid.';
                                                                        document.getElementById(FieldId).focus();
                                                                        return false;
                                                                        } 
                                                               }   
                                        
                                                        
                                                 }   
                                                 
                                                   if  (FieldValue== 'Work Order No')
                                                 {
                                                                var v=document.getElementById(FieldId).value;
                                                                if(v!='')
                                                                {
                                                                      if(IsDataValid(v,3)==false)
                                                                        {
                                                                        document.getElementById("lblError").innerHTML= 'Work Order No is not valid.';
                                                                        document.getElementById(FieldId).focus();
                                                                        return false;
                                                                        } 
                                                               }   
                                        
                                                 
                                                 }   
                                                 
                                                  if  (FieldValue== 'Open Date From')
                                                       {     
                                                          FieldIdOpenDateFrom=gridID.rows[intcnt].cells[2].children[0].children[0].id ;                                                           
                                                            
                                                       }
                                                        
                                                       if  (FieldValue== 'Open Date To')
                                                     {
                                                          FieldIdOpenDateTo=gridID.rows[intcnt].cells[2].children[0].children[0].id ;
                                                           
                                                     }   
                                                     
                                                       if  (FieldValue== 'Close Date From')
                                                     {                                                     
                                                             FieldIdCloseDateFrom=gridID.rows[intcnt].cells[2].children[0].children[0].id ;
                                                          
                                                     }   
                                                     
                                                       if  (FieldValue== 'Close Date To')
                                                     {                                                     
                                                          var FieldIdCloseDateTo=gridID.rows[intcnt].cells[2].children[0].children[0].id ;
                                                                                                                    
                                                     }   
                                                     
                                                       if  (FieldValue== 'Assigned Date Time')
                                                     {                                                                                                             
                                                          
                                                          FieldIdAsiignedDate=gridID.rows[intcnt].cells[2].children[0].children[0].id ;                                                                                                                                 
                                                     
                                                     }                                                    
                                      }                  
                             }                
                        }
                        
                        
                      if ( FieldIdOpenDateFrom !=null && FieldIdOpenDateTo !=null && FieldIdCloseDateFrom !=null && FieldIdCloseDateTo !=null && FieldIdAsiignedDate !=null)
                      {
                             if (document.getElementById(FieldIdOpenDateFrom)!=null)
                             {    
                                if (document.getElementById(FieldIdOpenDateFrom).value.trim().length>0)
                                {                 
                                   if (isDate(document.getElementById(FieldIdOpenDateFrom).value.trim(),"dd/MM/yyyy HH:mm") == false)	
                                    {
                                        document.getElementById('lblError').innerText ='Open date from is not valid.';
                                        document.getElementById(FieldIdOpenDateFrom).focus();
                                        return false;   
                                    }         
                                }
                             }
                             
                                    if (document.getElementById(FieldIdOpenDateTo)!=null)
                             {    
                                 if (document.getElementById(FieldIdOpenDateTo).value.trim().length>0)
                                {             
                                   if (isDate(document.getElementById(FieldIdOpenDateTo).value.trim(),"dd/MM/yyyy HH:mm") == false)	
                                    {
                                        document.getElementById('lblError').innerText ='Open date to is not valid.';
                                        document.getElementById(FieldIdOpenDateTo).focus();
                                        return false;   
                                    }         
                                 }
                            }
                             
                        
                              if (document.getElementById(FieldIdCloseDateFrom)!=null)
                             {       
                                  if (document.getElementById(FieldIdCloseDateFrom).value.trim().length>0)
                                {       
                                   if (isDate(document.getElementById(FieldIdCloseDateFrom).value.trim(),"dd/MM/yyyy HH:mm") == false)	
                                    {
                                        document.getElementById('lblError').innerText ='Close date from is not valid.';
                                        document.getElementById(FieldIdCloseDateFrom).focus();
                                        return false;   
                                    }         
                                }
                             }
                               
                               if (document.getElementById(FieldIdCloseDateTo)!=null)
                             {     
                                    if (document.getElementById(FieldIdCloseDateTo).value.trim().length>0)
                                   {           
                                       if (isDate(document.getElementById(FieldIdCloseDateTo).value.trim(),"dd/MM/yyyy HH:mm") == false)	
                                        {
                                            document.getElementById('lblError').innerText ='Close date to is not valid.';
                                            document.getElementById(FieldIdCloseDateTo).focus();
                                            return false;   
                                        }         
                                     }
                             }
                             
                                 if (document.getElementById(FieldIdOpenDateFrom).value.trim().length>0 &&  document.getElementById(FieldIdOpenDateTo).value.trim().length>0)
                                  {
                                      if (compareDates(document.getElementById(FieldIdOpenDateFrom).value,"dd/MM/yyyy HH:mm",document.getElementById(FieldIdOpenDateTo).value,"dd/MM/yyyy HH:mm")=='1')
                                    {
                                        document.getElementById('lblError').innerText ='Open date to should be greater than or equal to open date from.';
                                        document.getElementById(FieldIdOpenDateTo).focus();
                                        return false;            
                                    }
                                 }
                                
                                  if (document.getElementById(FieldIdCloseDateFrom).value.trim().length>0 &&  document.getElementById(FieldIdCloseDateTo).value.trim().length>0)
                                   {           
                                     var dtFrom=document.getElementById(FieldIdCloseDateFrom).value;
                                     var dtTo=document.getElementById(FieldIdCloseDateTo).value;
                                     dtFrom=dtFrom.trim();
                                     dtTo=dtTo.trim();
                                       if (compareDates(dtFrom,"dd/MM/yyyy HH:mm",dtTo,"dd/MM/yyyy HH:mm")=='1')
                                       {
                                            document.getElementById('lblError').innerText ='Close date to should be greater than or equal to close date from.';
                                             document.getElementById(FieldIdCloseDateTo).focus();
                                            return false;
                                       }               
                                   }
                             
                                  if(document.getElementById(FieldIdAsiignedDate)!=null)
                                 {         
                                     if(document.getElementById(FieldIdAsiignedDate).value.trim().length>0)
                                        {
                                            if (isDate(document.getElementById(FieldIdAsiignedDate).value,"d/M/yyyy") == false)	
                                            {
                                               document.getElementById('lblError').innerText = "Assigned date is not valid.";			
	                                           document.getElementById(FieldIdAsiignedDate).focus();
	                                           return false;  
                                            }
                                      }
                                }
                           
                           
                           
                           
                           
                      
                      }
                     
                        
                           
                  }        
             }catch(err){}
   }
   
   
   
   function FormValidation2()
    {   
    
            try
            {   
                //{debugger;}
                var gridID,colDrp,ColTxt;                   
                
                if(document.getElementById("gvHelpDeskSearchCofig") !=null)
                {        
                        gridID=document.getElementById('<%=gvHelpDeskSearchCofig.ClientID%>');
                      
                      //  alert(gridID);
                      
                        for(intcnt=1;intcnt<=gridID.rows.length-1;intcnt++)
                        {        
                             var FieldValue=gridID.rows[intcnt].cells[0].innerHTML;  
                           //  alert(FieldValue);
                             
                             if (gridID.rows[intcnt].cells[2].innerHTML !='')                 
                             {
                             
                                 var FieldId=gridID.rows[intcnt].cells[2].children[0].id ;  
                                   // alert(FieldId);
                             
                                    if (FieldId !=null)                 
                                      {
                                                if  (FieldValue== 'LTR No')
                                                 {
                                                        var v=document.getElementById(FieldId).value;
                                                        if(v!='')
                                                        {
                                                              if(IsDataValid(v,3)==false)
                                                                {
                                                                document.getElementById("lblError").innerHTML= 'LTR No is not valid.';
                                                                document.getElementById(FieldId).focus();
                                                                return false;
                                                                } 
                                                       }   
                                        
                                                 
                                                 }   
                                                   if  (FieldValue== 'PTR Number')
                                                 {
                                                              var v=document.getElementById(FieldId).value;
                                                                if(v!='')
                                                                {
                                                                      if(IsDataValid(v,3)==false)
                                                                        {
                                                                        document.getElementById("lblError").innerHTML= 'PTR No is not valid.';
                                                                        document.getElementById(FieldId).focus();
                                                                        return false;
                                                                        } 
                                                               }   
                                        
                                                        
                                                 }   
                                                 
                                                   if  (FieldValue== 'Work Order No')
                                                 {
                                                                var v=document.getElementById(FieldId).value;
                                                                if(v!='')
                                                                {
                                                                      if(IsDataValid(v,3)==false)
                                                                        {
                                                                        document.getElementById("lblError").innerHTML= 'Work Order No is not valid.';
                                                                        document.getElementById(FieldId).focus();
                                                                        return false;
                                                                        } 
                                                               }   
                                        
                                                 
                                                 }   
                                                 
                                                  if  (FieldValue== 'Open Date From')
                                                     {
//                                                             alert(FieldValue);
//                                                             alert("" + gridID.rows[intcnt].cells[2].children[0].id);
//                                                           
                                                           var FieldId2=gridID.rows[intcnt].cells[2].children[0].children[0].id ;  
                                                          //  alert(FieldId2);
                                                            if (FieldId2 !=null)
                                                            {
                                                                 var v=document.getElementById(FieldId2).value;
                                                                // alert(v);
                                                            if(v!='')
                                                            {
                                                               // alert(v)
                                                                if (isDate(v,"dd/MM/yyyy HH:mm") == false)	
                                                                {
                                                                document.getElementById("lblError").innerHTML ="Open Date From is not valid";
                                                                document.getElementById(FieldId2).focus();
                                                                return false;
                                                                }
                                                            }
                                                            
                                                            }
                                                          
                                                        
                                                     
                                                     }       
                                                       if  (FieldValue== 'Open Date To')
                                                     {
                                                     
                                                          var FieldId2=gridID.rows[intcnt].cells[2].children[0].children[0].id ;
                                                          
                                                            if (FieldId2 !=null)
                                                            {
                                                                var v=document.getElementById(FieldId2).value;
                                                                if(v!='')
                                                                {
                                                                   // alert(v)
                                                                    if (isDate(v,"dd/MM/yyyy HH:mm") == false)	
                                                                    {
                                                                    document.getElementById("lblError").innerHTML ="Open Date To is not valid";
                                                                    document.getElementById(FieldId2).focus();
                                                                    return false;
                                                                    }
                                                                }
                                                            }
                                                     
                                                     }   
                                                     
                                                       if  (FieldValue== 'Close Date From')
                                                     {
                                                     
                                                             var FieldId2=gridID.rows[intcnt].cells[2].children[0].children[0].id ;
                                                            if (FieldId2 !=null)
                                                            {
                                                                   var v=document.getElementById(FieldId2).value;
                                                                    if(v!='')
                                                                    {
                                                                       // alert(v)
                                                                        if (isDate(v,"dd/MM/yyyy HH:mm") == false)	
                                                                        {
                                                                        document.getElementById("lblError").innerHTML ="Close Date From is not valid";
                                                                        document.getElementById(FieldId2).focus();
                                                                        return false;
                                                                        }
                                                                    }
                                                            }
                                                     
                                                     }   
                                                     
                                                       if  (FieldValue== 'Close Date To')
                                                     {
                                                     
                                                          var FieldId2=gridID.rows[intcnt].cells[2].children[0].children[0].id ;
                                                          
                                                            if (FieldId2 !=null)
                                                            {
                                                                 var FieldId2=gridID.rows[intcnt].cells[2].children[0].children[0].id ;
                                                                if(v!='')
                                                                {
                                                                   // alert(v)
                                                                    if (isDate(v,"dd/MM/yyyy HH:mm") == false)	
                                                                    {
                                                                    document.getElementById("lblError").innerHTML ="Close Date To is not valid";
                                                                    document.getElementById(FieldId2).focus();
                                                                    return false;
                                                                    }
                                                                } 
                                                            }
                                                     }   
                                                     
                                                       if  (FieldValue== 'Assigned Date Time')
                                                     {
                                                          var FieldId2=gridID.rows[intcnt].cells[2].children[0].children[0].id ;
                                                          
                                                            if (FieldId2 !=null)
                                                            {
                                                                var v=document.getElementById(FieldId2).value;
                                                                if(v!='')
                                                                {
                                                                   // alert(v)
                                                                    if (isDate(v,"dd/MM/yyyy HH:mm") == false)	
                                                                    {
                                                                    document.getElementById("lblError").innerHTML ="Assigned Date Time is not valid";
                                                                    document.getElementById(FieldId2).focus();
                                                                    return false;
                                                                    }
                                                                }
                                                            
                                                            }                    
                                                     
                                                     } 
                                                 
                                                 
                                                 
                                                 
                                    
                                     
                                                   
                                      }                  
                             }                
                        }   
                  }  
                  
                  document.getElementById("lblError").innerHTML ="";      
             }catch(err){}
   }
    
</script>

</html>
