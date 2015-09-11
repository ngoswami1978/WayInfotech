<%@ Page Language="VB" AutoEventWireup="false" CodeFile="HDSR_IR.aspx.vb" Inherits="HelpDesk_HDSR_IR" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>HelpDesk: IR</title>
    <link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" media="all" href="../Calender/calendar-blue.css"
        title="win2k-cold-1" />
    <!-- import the calendar script -->

    <script type="text/javascript" src="../Calender/calendar.js"></script>

    <!-- import the language module -->

    <script type="text/javascript" src="../Calender/lang/calendar-en.js"></script>

    <!-- import the calendar setup module -->

    <script type="text/javascript" src="../Calender/calendar-setup.js"></script>

    <script type="text/javascript" src="../JavaScript/AAMS.js"></script>

    <script type="text/javascript" language="javascript">
 function PopupAgencyPage()
    {
        type = "../TravelAgency/TASR_Agency.aspx?Popup=T" ;
    	window.open(type,"aa","height=600,width=880,top=30,left=20,scrollbars=1,status=1");	
        return false;
    } 
      function EditFunction(CheckBoxObj)
   {           
          window.location.href="HDUP_IR.aspx?Action=U|"+CheckBoxObj;               
          return false;
    }
    
        function DeleteFunction(IRID)
        {
           
           if (confirm("Are you sure you want to delete?")==true)
           {
           document.getElementById("hdDeleteFlag").value=IRID;
           return true;
           }
           else
           {
                document.getElementById("hdDeleteFlag").value="";
                 return false;
           }
        }
        
    
    function PopupLoggedBy()
    {
    var type;      
            
         var strEmployeePageName=document.getElementById("hdEmployeePageName").value;
        if (strEmployeePageName!="")
        {
             type = "../Setup/" + strEmployeePageName+ "?Popup=T" ;
             window.open(type,"aa","height=600,width=880,top=30,left=20,scrollbars=1,status=1");	
             return false;
        }
    }
        //Method Written for Select Button    
         function SelectFunction(IRID)
        {   
            if (window.opener.document.forms['form1']['txtIRNo']!=null)
            {
            window.opener.document.forms['form1']['txtIRNo'].value=IRID;
            }
            window.close();
       }
    
    </script>

</head>
<body>
    <form id="form1" runat="server" defaultbutton="btnSearch">
        <table cellpadding="0" cellspacing="0" border="0">
            <tr>
                <td>
                    <!-- Code for Search Criteria -->
                    <table width="860px" align="left" class="border_rightred">
                        <tr>
                            <td valign="top" style="width: 860px;">
                                <table width="100%" align="left">
                                    <tr>
                                        <td valign="top" align="left">
                                            <span class="menu">HelpDesk-></span><span class="sub_menu">IR</span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="heading" align="center" valign="top">
                                            Search IR
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top">
                                            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                <tr>
                                                    <td align="LEFT">
                                                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                            <tr>
                                                                <td style="width: 860px;" class="redborder" valign="top">
                                                                    <table width="100%" border="0" align="left" cellpadding="0" cellspacing="0">
                                                                        <tr>
                                                                            <td class="textbold" colspan="6" align="center" style="height: 15px">
                                                                                <asp:Label ID="lblError" runat="server" CssClass="ErrorMsg" EnableViewState="False"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td width="6%" class="textbold" style="height: 25px">
                                                                                &nbsp;</td>
                                                                            <td width="18%" class="textbold" style="height: 25px">
                                                                                Agency Name
                                                                            </td>
                                                                            <td colspan="3" style="height: 25px">
                                                                                <asp:TextBox ID="txtAgencyName" CssClass="textbox" MaxLength="40" runat="server"
                                                                                    Width="93%" onblur="ActDeAct()" TabIndex="1"></asp:TextBox>
                                                                                <img src="../Images/lookup.gif" alt="Select & Add Agency Name" onclick="javascript:return PopupAgencyPage();" />
                                                                            </td>
                                                                            <td style="width: 176px; height: 25px;">
                                                                                <asp:Button ID="btnSearch" CssClass="button" runat="server" Text="Search" TabIndex="23"
                                                                                    AccessKey="a" /></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="textbold" width="6%" style="height: 25px">
                                                                            </td>
                                                                            <td class="textbold" width="18%" style="height: 25px">
                                                                                IR Title</td>
                                                                            <td width="20%" style="height: 25px" colspan="3">
                                                                                <asp:TextBox ID="txtIRTitle" runat="server" CssClass="textbox" MaxLength="40" Width="93%"
                                                                                    TabIndex="2"></asp:TextBox>
                                                                            </td>
                                                                            <td style="height: 25px; width: 176px;">
                                                                                <asp:Button ID="btnReset" CssClass="button" runat="server" Text="Reset" TabIndex="24"
                                                                                    AccessKey="r" />
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="textbold" width="6%" style="height: 25px">
                                                                            </td>
                                                                            <td class="textbold" width="18%" style="height: 25px">
                                                                                IR No.</td>
                                                                            <td width="20%" style="height: 25px">
                                                                                <asp:TextBox ID="txtIRNo" runat="server" CssClass="textbox" MaxLength="18" Width="136px"
                                                                                    TabIndex="2"></asp:TextBox></td>
                                                                            <td width="18%" class="textbold" style="height: 25px">
                                                                                &nbsp; &nbsp; &nbsp; LTR No.</td>
                                                                            <td width="20%" style="height: 25px">
                                                                                <asp:TextBox ID="txtLtrNo" runat="server" CssClass="textbox" MaxLength="40" Width="136px"
                                                                                    TabIndex="3"></asp:TextBox></td>
                                                                            <td style="height: 25px; width: 176px;">
                                                                                <asp:Button ID="btnExport" CssClass="button" runat="server" Text="Export" TabIndex="23"
                                                                                    AccessKey="e" /></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="textbold" height="25px">
                                                                                &nbsp;</td>
                                                                            <td class="textbold">
                                                                                Type</td>
                                                                            <td>
                                                                                <asp:DropDownList ID="drpIRType" runat="server" CssClass="dropdownlist" Width="142px"
                                                                                    TabIndex="4">
                                                                                </asp:DropDownList></td>
                                                                            <td class="textbold">
                                                                                &nbsp; &nbsp; &nbsp; Status</td>
                                                                            <td>
                                                                                <asp:DropDownList ID="drpIRStatus" runat="server" CssClass="dropdownlist" Width="142px"
                                                                                    TabIndex="5">
                                                                                </asp:DropDownList></td>
                                                                            <td style="width: 176px">
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="textbold" style="height: 25px">
                                                                            </td>
                                                                            <td class="textbold" style="height: 25px">
                                                                                Severity</td>
                                                                            <td style="height: 25px">
                                                                                <asp:DropDownList ID="drpSeverity" runat="server" CssClass="dropdownlist" Width="144px"
                                                                                    TabIndex="6">
                                                                                </asp:DropDownList></td>
                                                                            <td class="textbold" style="height: 25px">
                                                                                &nbsp; &nbsp; &nbsp; Follow Up</td>
                                                                            <td style="height: 25px">
                                                                                <asp:DropDownList ID="drpFollowup" runat="server" CssClass="dropdownlist" Width="142px"
                                                                                    TabIndex="7">
                                                                                </asp:DropDownList></td>
                                                                            <td style="width: 176px; height: 25px;">
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td colspan="6">
                                                                                <table style="width: 100%" cellpadding="0" cellspacing="0" border="0">
                                                                                    <tr>
                                                                                        <td width="6%" class="textbold" style="height: 25px">
                                                                                        </td>
                                                                                        <td width="18%" class="textbold">
                                                                                            Logged By</td>
                                                                                        <td width="20%">
                                                                                            <asp:TextBox ID="txtPendingWith" CssClass="textbox" MaxLength="40" runat="server"
                                                                                                Width="138px" TabIndex="8"></asp:TextBox>
                                                                                            <img src="../Images/lookup.gif" alt=" " onclick="javascript:return PopupLoggedBy();"
                                                                                                tabindex="9" />
                                                                                        </td>
                                                                                        <td width="18%">
                                                                                            <span class="textbold">&nbsp; &nbsp; &nbsp; Assigned To</span></td>
                                                                                        <td style="width: 170px">
                                                                                            <asp:DropDownList ID="drpAssignedTo" runat="server" CssClass="dropdownlist" Width="142px"
                                                                                                TabIndex="10">
                                                                                            </asp:DropDownList></td>
                                                                                        <td width="18%">
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td class="textbold" style="height: 25px">
                                                                                        </td>
                                                                                        <td class="textbold">
                                                                                            IR Type Category</td>
                                                                                        <td>
                                                                                            <asp:DropDownList ID="drpIRTypCat" runat="server" CssClass="dropdownlist" Width="144px"
                                                                                                TabIndex="11">
                                                                                            </asp:DropDownList></td>
                                                                                        <td class="textbold">
                                                                                            &nbsp; &nbsp; &nbsp; 1A Office</td>
                                                                                        <td style="width: 170px">
                                                                                            <asp:DropDownList ID="drp1aOffice" runat="server" CssClass="dropdownlist" Width="142px"
                                                                                                TabIndex="12">
                                                                                            </asp:DropDownList></td>
                                                                                        <td>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td class="textbold" style="height: 25px">
                                                                                        </td>
                                                                                        <td class="textbold" style="height: 25px">
                                                                                            Assigned Date</td>
                                                                                        <td style="height: 25px">
                                                                                            <asp:TextBox ID="txtAssignedDt" runat="server" CssClass="textbox" MaxLength="40"
                                                                                                Width="136px" TabIndex="13"></asp:TextBox>
                                                                                            <img id="imgReceivedFrom" alt="" src="../Images/calender.gif" tabindex="14" title="Date selector"
                                                                                                style="cursor: pointer" />

                                                                                            <script type="text/javascript">
                                                                                                    Calendar.setup({
                                                                                                    inputField     :    '<%=txtAssignedDt.clientId%>',
                                                                                                    ifFormat       :    "%d/%m/%Y",
                                                                                                    button         :    "imgReceivedFrom",
                                                                                                    //align          :    "Tl",
                                                                                                    singleClick    :    true
                                                                                                    });
                                                                                            </script>

                                                                                        </td>
                                                                                        <td class="textbold" style="height: 25px">
                                                                                        </td>
                                                                                        <td style="height: 25px; width: 170px;">
                                                                                        </td>
                                                                                        <td style="height: 25px">
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td class="textbold" style="height: 25px">
                                                                                        </td>
                                                                                        <td class="textbold">
                                                                                            IR Open Date From</td>
                                                                                        <td>
                                                                                            <asp:TextBox ID="txtOpenDtFrm" runat="server" CssClass="textbox" MaxLength="40" Width="136px"
                                                                                                TabIndex="15"></asp:TextBox>
                                                                                            <img id="imgApprovalFrom" alt="" src="../Images/calender.gif" title="Date selector"
                                                                                                style="cursor: pointer" tabindex="16" />

                                                                                            <script type="text/javascript">
                                                                                                    Calendar.setup({
                                                                                                    inputField     :    '<%=txtOpenDtFrm.clientId%>',
                                                                                                    ifFormat       :    "%d/%m/%Y",
                                                                                                    button         :    "imgApprovalFrom",
                                                                                                    //align          :    "Tl",
                                                                                                    singleClick    :    true
                                                                                                    });
                                                                                            </script>

                                                                                        </td>
                                                                                        <td class="textbold">
                                                                                            &nbsp; &nbsp; &nbsp; IR &nbsp;Open Date To</td>
                                                                                        <td style="width: 170px">
                                                                                            <asp:TextBox ID="txtOpenDtTo" runat="server" CssClass="textbox" MaxLength="40" Width="136px"
                                                                                                TabIndex="17"></asp:TextBox>
                                                                                            <img id="imgApprovalTo" alt="" src="../Images/calender.gif" title="Date selector"
                                                                                                style="cursor: pointer" tabindex="18" />

                                                                                            <script type="text/javascript">
                                                                                                    Calendar.setup({
                                                                                                    inputField     :    '<%=txtOpenDtTo.clientId%>',
                                                                                                    ifFormat       :    "%d/%m/%Y",
                                                                                                    button         :    "imgApprovalTo",
                                                                                                    //align          :    "Tl",
                                                                                                    singleClick    :    true
                                                                                                    });
                                                                                            </script>

                                                                                        </td>
                                                                                        <td>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td class="textbold" style="height: 25px">
                                                                                        </td>
                                                                                        <td class="textbold" style="height: 25px">
                                                                                            IR Close Date From</td>
                                                                                        <td style="height: 25px">
                                                                                            <asp:TextBox ID="txtCloseDtFrm" runat="server" CssClass="textbox" MaxLength="40"
                                                                                                Width="136px" TabIndex="19"></asp:TextBox>
                                                                                            <img id="imgSentFrom" alt="" src="../Images/calender.gif" tabindex="20" title="Date selector"
                                                                                                style="cursor: pointer" />

                                                                                            <script type="text/javascript">
                                                                                                    Calendar.setup({
                                                                                                    inputField     :    '<%=txtCloseDtFrm.clientId%>',
                                                                                                    ifFormat       :    "%d/%m/%Y",
                                                                                                    button         :    "imgSentFrom",
                                                                                                    //align          :    "Tl",
                                                                                                    singleClick    :    true
                                                                                                    });
                                                                                            </script>

                                                                                        </td>
                                                                                        <td class="textbold" style="height: 25px">
                                                                                            &nbsp; &nbsp; &nbsp; IR Close Date To
                                                                                        </td>
                                                                                        <td style="height: 25px; width: 170px;">
                                                                                            <asp:TextBox ID="txtCloseDtTo" runat="server" CssClass="textbox" MaxLength="40" Width="136px"
                                                                                                TabIndex="21"></asp:TextBox>
                                                                                            <img id="imgSentTo" alt="" src="../Images/calender.gif" style="cursor: pointer" tabindex="22"
                                                                                                title="Date selector" />

                                                                                            <script type="text/javascript">
                                                                                                    Calendar.setup({
                                                                                                    inputField     :    '<%=txtCloseDtTo.clientId%>',
                                                                                                    ifFormat       :    "%d/%m/%Y",
                                                                                                    button         :    "imgSentTo",
                                                                                                    //align          :    "Tl",
                                                                                                    singleClick    :    true
                                                                                                    });
                                                                                            </script>

                                                                                        </td>
                                                                                        <td style="height: 25px">
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                        <asp:HiddenField ID="hdLcode" runat="server" />
                                                        <asp:HiddenField ID="hdDeleteFlag" runat="server" />
                                                        <input type="hidden" id="hdEmployeePageName" style="width: 1px" runat='server' />
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
                <td>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <!-- Code for Search Result Gridview & Paging -->
                    <table cellpadding="0" cellspacing="0" border="0">
                        <tr>
                            <td valign="top" style="padding-left: 4px; height: 117px;">
                                <table width="1500px" border="0" align="left" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td class="redborder">
                                            <asp:GridView ID="grdvIR" runat="server" AllowSorting="true" HeaderStyle-ForeColor="white"
                                                AutoGenerateColumns="False" HorizontalAlign="Center" Width="100%" HeaderStyle-CssClass="Gridheading"
                                                RowStyle-CssClass="ItemColor" AlternatingRowStyle-CssClass="lightblue" EnableViewState="False">
                                                <Columns>
                                                    <asp:BoundField DataField="HD_RE_ID" HeaderText="LTR No." SortExpression="HD_RE_ID"
                                                        ItemStyle-Wrap="false" />
                                                    <asp:BoundField DataField="HD_IR_REF" HeaderText="IR No." SortExpression="HD_IR_REF"
                                                        ItemStyle-Wrap="false" />
                                                    <asp:BoundField DataField="NAME" HeaderText="Agency Name" SortExpression="NAME" />
                                                    <%--<asp:BoundField  DataField="ADDRESS" HeaderText="Address" ItemStyle-Wrap="true"  />--%>
                                                    <asp:TemplateField HeaderText="Address" ItemStyle-Width="200px" SortExpression="ADDRESS"
                                                        ItemStyle-Wrap="true">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblAddress" runat="server" Text='<%# Eval("ADDRESS") %>' Width="200px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="IR Title" SortExpression="HD_IR_TITLE" ItemStyle-Width="200px"
                                                        ItemStyle-Wrap="true">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblIRTitle" runat="server" Text='<%# Eval("HD_IR_TITLE") %>' Width="200px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <%-- <asp:BoundField DataField="HD_IR_TITLE" HeaderText="IR Title"   /> --%>
                                                    <asp:BoundField DataField="HD_STATUS_NAME" SortExpression="HD_STATUS_NAME" HeaderText="Status" />
                                                    <asp:BoundField DataField="HD_IR_FOLLOWUP_NAME" SortExpression="HD_IR_FOLLOWUP_NAME"
                                                        HeaderText="Follow Up Name" />
                                                    <asp:BoundField DataField="HD_IR_SEV_NAME" SortExpression="HD_IR_SEV_NAME" HeaderText="Severity Name" />
                                                    <asp:BoundField DataField="HD_IR_TYPE_NAME" SortExpression="HD_IR_TYPE_NAME" HeaderText="IR Type Name" />
                                                    <asp:BoundField DataField="Employee_Name" SortExpression="Employee_Name" HeaderText="Employee Name" />
                                                    <asp:TemplateField HeaderText="Open Date" SortExpression="HD_IR_OPENDATE">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblOpenDt" runat="server" Text='<%# Eval("HD_IR_OPENDATE")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Close Date" SortExpression="HD_IR_CLOSEDATE">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblClosenDt" runat="server" Text='<%# Eval("HD_IR_CLOSEDATE")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField SortExpression="ASSIGNEE_NAME" DataField="ASSIGNEE_NAME" HeaderText="Assignee Name" />
                                                    <asp:TemplateField HeaderText="Assigned Date" SortExpression="ASSIGNED_DATE">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblAssigneeDt" runat="server" Text='<%# Eval("ASSIGNED_DATE")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Action" ItemStyle-Wrap="false">
                                                        <ItemTemplate>
                                                            <a href="#" class="LinkButtons" id="linkEdit" runat="server">Edit</a>&nbsp;
                                                            <%--<a href="#" class="LinkButtons" id="linkDelete" runat="server">Delete</a>--%>
                                                            <asp:LinkButton ID="linkDelete" CssClass="LinkButtons" runat="server" Text="Delete"></asp:LinkButton>&nbsp;
                                                            <asp:LinkButton ID="lnkSelect" CssClass="LinkButtons" runat="server" Text="Select"
                                                                CommandArgument='<%#Eval("HD_IR_REF")%>'></asp:LinkButton>
                                                            <asp:HiddenField ID="hdIRID" runat="server" Value='<%#Eval("HD_IR_ID")%>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                                <AlternatingRowStyle CssClass="lightblue" />
                                                <HeaderStyle CssClass="Gridheading" HorizontalAlign="Left" Wrap="False" />
                                                <RowStyle CssClass="textbold" />
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                    
                                    <tr>
                                        <td valign="top">
                                            <asp:Panel ID="pnlPaging" runat="server" Visible="false" Width="800px">
                                                <table border="0" cellpadding="0" cellspacing="0" style="width: 800px">
                                                    <tr class="paddingtop paddingbottom">
                                                        <td style="width: 243px" class="left" nowrap="nowrap">
                                                            <span class="textbold"><b>&nbsp;No. of records found</b></span>&nbsp;&nbsp;<asp:TextBox
                                                                ID="txtTotalRecordCount" runat="server" Width="105px" CssClass="textboxgrey"
                                                                ReadOnly="True"></asp:TextBox></td>
                                                        <td style="width: 200px" class="right">
                                                            <asp:LinkButton ID="lnkPrev" CssClass="LinkButtons" runat="server" CommandName="Prev"><< Prev</asp:LinkButton></td>
                                                        <td style="width: 356px" class="center">
                                                            <span class="textbold"><b>Page No</b></span>&nbsp;&nbsp;<asp:DropDownList ID="ddlPageNumber"
                                                                Width="70px" CssClass="dropdownlist" runat="server" AutoPostBack="True">
                                                            </asp:DropDownList></td>
                                                        <td style="width: 187px" class="left">
                                                            <asp:LinkButton ID="lnkNext" runat="server" CssClass="LinkButtons" CommandName="Next">Next >></asp:LinkButton></td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                    
                                </table>
                                <asp:TextBox ID="txtRecordOnCurrReco" Visible="false" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </form>

    <script language="javascript" type="text/javascript">
    function ValidateForm()
    {
      document.getElementById('<%=lblError.ClientId%>').innerText=''
     if(document.getElementById('<%=txtLtrNo.ClientId%>').value !='')
         {
            var strValue = document.getElementById('<%=txtLtrNo.ClientId%>').value
            reg = new RegExp("^[0-9]+$"); 

            if(reg.test(strValue) == false) 
            {

                document.getElementById('<%=lblError.ClientId%>').innerText ='LTR number should contain only digits.'
                return false;

             }
        }
        if(document.getElementById('<%=txtIRNo.ClientId%>').value !='')
         {
            var strValue = document.getElementById('<%=txtIRNo.ClientId%>').value
            reg = new RegExp("^[0-9]+$"); 

            if(reg.test(strValue) == false) 
            {

                document.getElementById('<%=lblError.ClientId%>').innerText ='IR number should contain only digits.'
                return false;

             }
        }
        //      Checking txtOpenDateFrom .
        if(document.getElementById('<%=txtOpenDtFrm.ClientId%>').value != '')
        {
        if (isDate(document.getElementById('<%=txtOpenDtFrm.ClientId%>').value,"d/M/yyyy") == false)	
        {
           document.getElementById('<%=lblError.ClientId%>').innerText = "Open date from is not valid.";			
	       document.getElementById('<%=txtOpenDtFrm.ClientId%>').focus();
	       return(false);  
        }
        } 
         //      Checking txtOpenDateTo .
        if(document.getElementById('<%=txtOpenDtTo.ClientId%>').value != '')
        {
        if (isDate(document.getElementById('<%=txtOpenDtTo.ClientId%>').value,"d/M/yyyy") == false)	
        {
           document.getElementById('<%=lblError.ClientId%>').innerText = "Open date to is not valid.";			
	       document.getElementById('<%=txtOpenDtTo.ClientId%>').focus();
	       return(false);  
        }
        } 
         //      Checking txtCloseDateFrom .
        if(document.getElementById('<%=txtCloseDtFrm.ClientId%>').value != '')
        {
        if (isDate(document.getElementById('<%=txtCloseDtFrm.ClientId%>').value,"d/M/yyyy") == false)	
        {
           document.getElementById('<%=lblError.ClientId%>').innerText = "Close date from is not valid.";			
	       document.getElementById('<%=txtCloseDtFrm.ClientId%>').focus();
	       return(false);  
        }
        } 
         //      Checking txtCloseDateTo .
        if(document.getElementById('<%=txtCloseDtTo.ClientId%>').value != '')
        {
        if (isDate(document.getElementById('<%=txtCloseDtTo.ClientId%>').value,"d/M/yyyy") == false)	
        {
           document.getElementById('<%=lblError.ClientId%>').innerText = "Close date to is not valid.";			
	       document.getElementById('<%=txtCloseDtTo.ClientId%>').focus();
	       return(false);  
        }
        } 
         
          //      Checking txtAssignedDate .
        if(document.getElementById('<%=txtAssignedDt.ClientId%>').value != '')
        {
        if (isDate(document.getElementById('<%=txtAssignedDt.ClientId%>').value,"d/M/yyyy") == false)	
        {
           document.getElementById('<%=lblError.ClientId%>').innerText = "Assigned date is not valid.";			
	       document.getElementById('<%=txtAssignedDt.ClientId%>').focus();
	       return(false);  
        }
        } 
        // End function
         //****************************************************************
    //      Checking txtOpenDateFrom .
        if(document.getElementById('<%=txtOpenDtFrm.ClientId%>').value != '')
        { 
            if(document.getElementById('<%=txtOpenDtTo.ClientId%>').value == '') 
            { 
             document.getElementById('<%=lblError.ClientId%>').innerText = "Open date to is mandatory.";			
	            return(false);  
	        }
        } 
         if(document.getElementById('<%=txtOpenDtTo.ClientId%>').value != '')
        { 
            if(document.getElementById('<%=txtOpenDtFrm.ClientId%>').value == '') 
            { 
             document.getElementById('<%=lblError.ClientId%>').innerText = "Open date from is mandatory.";			
	            return(false);  
	        }
        } 
         //****************************************************************
    //      Checking txtCloseDateFrom .
        if(document.getElementById('<%=txtCloseDtFrm.ClientId%>').value != '')
        { 
            if(document.getElementById('<%=txtCloseDtTo.ClientId%>').value == '') 
            { 
             document.getElementById('<%=lblError.ClientId%>').innerText = "Close date to is mandatory.";			
	            return(false);  
	        }
        } 
         if(document.getElementById('<%=txtCloseDtTo.ClientId%>').value != '')
        { 
            if(document.getElementById('<%=txtCloseDtFrm.ClientId%>').value == '') 
            { 
             document.getElementById('<%=lblError.ClientId%>').innerText = "Close date from is mandatory.";			
	            return(false);  
	        }
        } 
        //    compareDates(dateOpen,dateformat1,dateAssignee,dateformat2) {
        
   if (compareDates(document.getElementById('<%=txtCloseDtFrm.ClientId%>').value,"d/M/yyyy",document.getElementById('<%=txtCloseDtTo.ClientId%>').value,"d/M/yyyy")==1)
       {
            document.getElementById('<%=lblError.ClientId%>').innerText ='Close date to should be greater than or equal to close date from.'
            return false;
       }
        if (compareDates(document.getElementById('<%=txtOpenDtFrm.ClientId%>').value,"d/M/yyyy",document.getElementById('<%=txtOpenDtTo.ClientId%>').value,"d/M/yyyy")==1)
       {
            document.getElementById('<%=lblError.ClientId%>').innerText ='Open date to should be greater than or equal to open date from.'
            return false;
       }
       return true; 
        
    }
    
   
    

    </script>

</body>
</html>
