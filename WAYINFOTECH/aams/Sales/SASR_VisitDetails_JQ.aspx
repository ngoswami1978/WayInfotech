<%@ Page Language="VB" AutoEventWireup="false" CodeFile="SASR_VisitDetails_JQ.aspx.vb"
    Inherits="Sales_SASR_VisitDetails_JQ" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>AAMS::Sales::Visit Details</title>
    <link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/Sales.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" media="all" href="../Calender/calendar-blue.css"
        title="win2k-cold-1" />

    <script src="../JavaScript/AAMS.js" type="text/javascript"></script>
    <script src="jquery-1.6.js" type="text/javascript"></script>

    <link href="../Calender/calendar-blue.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="../Calender/calendar.js"></script>

    <script type="text/javascript" src="../Calender/lang/calendar-en.js"></script>

    <script type="text/javascript" src="../Calender/calendar-setup.js"></script>

    <script type="text/javascript" src="../JavaScript/subModal.js"></script>

    <link rel="stylesheet" type="text/css" href="../JavaScript/style.css" />
    <link rel="stylesheet" type="text/css" href="../JavaScript/subModal.css" />
    <script type="text/javascript" language="javascript" >
    
    function FillVisitDetails(intSEQUENCENO,trRowID)
    {
        // var empid = $("#" + RowID + "> TD:nth-child(2)").html(); 
         //var ManagerName = $("#tabVisitDetails > tbody > tr[id=" + trRowID + "] > td:first> input[value=Update]").hide();
         try
         {
             trRowID="trVisitDetails" + trRowID
             var ManagerName = $("#tabVisitDetails > tbody > tr[id=" + trRowID + "] > TD:nth-child(2)").html(); 
             var ReportingManagerName = $("#tabVisitDetails > tbody > tr[id=" + trRowID + "] > TD:nth-child(3)").html(); 
             var PersonMet = $("#tabVisitDetails > tbody > tr[id=" + trRowID + "] > TD:nth-child(4)").html(); 
             var Designation = $("#tabVisitDetails > tbody > tr[id=" + trRowID + "] > TD:nth-child(5)").html(); 
             var InTime = $("#tabVisitDetails > tbody > tr[id=" + trRowID + "] > TD:nth-child(6)").html(); 
             var OutTime = $("#tabVisitDetails > tbody > tr[id=" + trRowID + "] > TD:nth-child(7)").html(); 
             
             $("input#hdVisitDetails").val(intSEQUENCENO);
             $("input#txtPersonMet").val(PersonMet);
            $("input#txtPersonMet").val(PersonMet);
            $("input#txtInTime").val(InTime);
            $("input#txtDesignation").val(Designation);
            $("input#txtOutTime").val(OutTime);
            $("#ddlManager option").each(function() {   this.selected = $(this).text() == ManagerName; }); 
            $("#ddlReportingManager option").each(function() {   this.selected = $(this).text() == ReportingManagerName; }); 
            $("#btnHTMLAddVisitDetails").attr('value','Update');
         
             
             
         }
         catch(err){alert(err)}
    }
    
    
    function UpdateVisitDetails(intSEQUENCENO)
    {
    
              
                var ManagerID= $("select#ddlManager").val();
                 var ManagerName= $("select[id$='ddlManager'] :selected").text();
                 var PersonMet = $("input#txtPersonMet").val();
                 var InTime= $("input#txtInTime").val();
                 var ReportingManagerID= $("select#ddlReportingManager").val();
                   var ReportingManagerName=  $("select[id$='ddlReportingManager'] :selected").text();
                 var Designation = $("input#txtDesignation").val();
                 var OutTime= $("input#txtOutTime").val();
                 var Action="U";
                 var SEQUENCENO=intSEQUENCENO;
                 
                 
          var options =
             {
                error: function(msg) { alert(msg.responseText) },
                type: "POST",
                url: "SASR_VisitDetails_JQ.aspx/UpdateVisitDetails",
                data: "{\"sManagerID\":\"" + ManagerID + "\",\"sManagerName\":\"" + ManagerName + "\",\"sPersonMet\":\"" + PersonMet + "\",\"sInTime\":\"" + InTime + "\",\"sReportingManagerID\":\"" + ReportingManagerID + "\",\"sReportingManagerName\":\"" + ReportingManagerName + "\",\"sDesignation\":\"" + Designation + "\",\"sOutTime\":\"" + OutTime + "\",\"sAction\":\"" + Action + "\",\"sSEQUENCENO\":\"" + SEQUENCENO + "\"}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function(response)
                 {
                    if (response.d != "")
                     {      
                        
                        bindGrid(response)
                        $("#btnHTMLAddVisitDetails").attr('value','Add');
                        $("input#txtPersonMet").val('');
                        $("input#txtInTime").val('');
                        $("input#txtDesignation").val('');
                        $("input#txtOutTime").val('');
                        $("select[id$='ddlManager']").val('99999');
                        $("select[id$='ddlReportingManager']").val('99999');
                     }
                     else
                     {
                           alert("Updation failed!" + personMet);
                     }
                  }
             
            };
            //Call the PageMethods
            $.ajax(options); 
    }
    function AddVisitDetails()
    {
        
            //  var empid = $("#" + RowID + "> TD:nth-child(2) > input[type=text]").val();
//            var empid = $("#" + RowID + "> TD:nth-child(2)").html();            
//            var empname =$("#" + RowID + "> TD:nth-child(3) > input[type=text]").val();
//            var dept = $("#" + RowID + "> TD:nth-child(4) > input[type=text]").val();
//            var age = $("#" + RowID + "> TD:nth-child(5) > input[type=text]").val();
//            var address =$("#" + RowID + "> TD:nth-child(6) > input[type=text]").val();
                
                var buttonText =$("#btnHTMLAddVisitDetails").attr('value');
                
                if (buttonText=="Update")
                {
                    UpdateVisitDetails($("input#hdVisitDetails").val());
                }
                else
                {
                     var ManagerID= $("select#ddlManager").val();
                     var ManagerName= $("select[id$='ddlManager'] :selected").text();
                     var PersonMet = $("input#txtPersonMet").val();
                     var InTime= $("input#txtInTime").val();
                     var ReportingManagerID= $("select#ddlReportingManager").val();
                     var ReportingManagerName=  $("select[id$='ddlReportingManager'] :selected").text();
                     var Designation = $("input#txtDesignation").val();
                     var OutTime= $("input#txtOutTime").val();
                     var Action="I";
                     var SEQUENCENO="";
                    var options =
                    {
                        error: function(msg) { alert(msg.responseText) },
                        type: "POST",
                        url: "SASR_VisitDetails_JQ.aspx/UpdateVisitDetails",
                        data: "{\"sManagerID\":\"" + ManagerID + "\",\"sManagerName\":\"" + ManagerName + "\",\"sPersonMet\":\"" + PersonMet + "\",\"sInTime\":\"" + InTime + "\",\"sReportingManagerID\":\"" + ReportingManagerID + "\",\"sReportingManagerName\":\"" + ReportingManagerName + "\",\"sDesignation\":\"" + Designation + "\",\"sOutTime\":\"" + OutTime + "\",\"sAction\":\"" + Action + "\",\"sSEQUENCENO\":\"" + SEQUENCENO + "\"}",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function(response)
                         {
                            if (response.d != "")
                             {      
                                bindGrid(response)
                                $("input#txtPersonMet").val('');
                                $("input#txtInTime").val('');
                                $("input#txtDesignation").val('');
                                $("input#txtOutTime").val('');
                                $("select[id$='ddlManager']").val('99999');
                                $("select[id$='ddlReportingManager']").val('99999');
                             }
                             else
                             {
                                   alert("Updation failed!");
                             }
                          }
                     
                    };
               
            //Call the PageMethods
                $.ajax(options); 
             }
           // return false;   
    }
    
    function bindGrid(response)
    {
        
        if (window.ActiveXObject) 
        { 
            doc=new ActiveXObject("Microsoft.XMLDOM"); 
            doc.async="false"; 
            doc.loadXML(response); 

        } 
        else 
        { 
            var parser=new DOMParser(); 
            var doc=parser.parseFromString(response,"text/xml"); 
        } 
        var dsRoot=doc.documentElement;
        var objVISITDETAILEmployee = dsRoot.getElementsByTagName('DETAIL');    
         var objVISITDETAILEmployeeDetails="";
        
         if (objVISITDETAILEmployee[0].getAttribute("SEQUENCENO")!='')
         {
             objVISITDETAILEmployeeDetails="<table id='tabVisitDetails' width='85%' border='0' cellpading='0' cellspacing='0'>"
             
             for (var count = 0; count < objVISITDETAILEmployee.length; count++) 
              {    
                tdVisitDetails.innerHTML="";
                if (count==0)
                {
                   objVISITDETAILEmployeeDetails+= "<tr  class='Gridheading' style='color:white'> <td>S.No</td><td>Manager</td><td>Reporting Manager </td><td>Person Met</td><td>Designation </td><td>In Time</td><td>Out Time </td><td>Action</td></tr>";   
                }
                   
                if ( count % 2 == 1 )  
                {
                 objVISITDETAILEmployeeDetails+="<tr class='lightblue' id = 'trVisitDetails" + (count + 1) + "'>";
                }
                else
                {
                objVISITDETAILEmployeeDetails+="<tr class='textbold' id = 'trVisitDetails" + (count + 1) + "'>";
                }
                    
                    objVISITDETAILEmployeeDetails+="<td style='width:5%'>" + objVISITDETAILEmployee[count].getAttribute("SEQUENCENO") + "</td>" ; 
                    objVISITDETAILEmployeeDetails+="<td style='width:15%'>" + objVISITDETAILEmployee[count].getAttribute("MANAGER_NAME") + "</td>" ; 
                    objVISITDETAILEmployeeDetails+="<td style='width:15%'>" + objVISITDETAILEmployee[count].getAttribute("IMMEDIATE_MANAGERNAME") + "</td>" ; 
                    objVISITDETAILEmployeeDetails+="<td style='width:15%'>" + objVISITDETAILEmployee[count].getAttribute("CONTACT_NAME") + "</td>" ; 
                    objVISITDETAILEmployeeDetails+="<td style='width:15%'>" + objVISITDETAILEmployee[count].getAttribute("DESIGNATION") + "</td>" ; 
                    objVISITDETAILEmployeeDetails+="<td style='width:10%'>" + objVISITDETAILEmployee[count].getAttribute("INTIME") + "</td>" ; 
                    objVISITDETAILEmployeeDetails+="<td style='width:10%'>" + objVISITDETAILEmployee[count].getAttribute("OUTTIME") + "</td>" ; 
                    objVISITDETAILEmployeeDetails+="<td style='width:15%'><a href='#' onclick='FillVisitDetails("  + objVISITDETAILEmployee[count].getAttribute("SEQUENCENO") + " , " + (count + 1) + ")'> update </a></td>" ; 
                    objVISITDETAILEmployeeDetails+="</tr>" ;
                }
            objVISITDETAILEmployeeDetails+="</table>"   ;
            try
            {
               tdVisitDetails.innerHTML=objVISITDETAILEmployeeDetails;
            }
            catch(err){alert("Error" + err );}
           
         }
            
    }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <table width="1080px" class="border_rightred left">
            <tr>
                <td class="top">
                    <table width="100%">
                        <tr>
                            <td class="top left" style="width: 80%">
                                <span class="menu">Sales-> DSR -> </span><span class="sub_menu">Visit Details</span>
                            </td>
                            <td class="right" style="width: 20%">
                                <asp:LinkButton ID="lnkClose" CssClass="LinkButtons" runat="server" OnClientClick="return fnCallLogID()">Close</asp:LinkButton>
                                &nbsp; &nbsp;&nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td class="heading center" colspan="2" style="width: 100%">
                                Visit Detais</td>
                        </tr>
                        <tr>
                            <td colspan="2" style="width: 100%">
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td class="redborder top" colspan="2" style="width: 100%">
                                            <table width="100%" border="0" cellpadding="2" cellspacing="2">
                                                <tr>
                                                    <td class="center TOP">
                                                        <asp:Label ID="lblError" CssClass="ErrorMsg" runat="server" EnableViewState="False"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="top" style="width: 100%;">
                                                        <table width="100%" border="0" class="left" cellpadding="2" cellspacing="0">
                                                            <tr>
                                                                <td class="textbold" rowspan="6" style="width: 2%">
                                                                </td>
                                                                <td class="textbold" style="width: 15%">
                                                                    Agency Name</td>
                                                                <td colspan="3" style="width: 75%">
                                                                    <asp:TextBox ID="txtAgencyName" runat="server" CssClass="textboxgrey TextTitleCase"
                                                                        Width="530px" ReadOnly="True" TabIndex="20"></asp:TextBox></td>
                                                                <td style="width: 8%">
                                                                    <asp:Button ID="btnSave" runat="server" TabIndex="3" CssClass="button topMargin"
                                                                        Text="Save" Width="106px" OnClientClick="return ManageCallLogPage()" AccessKey="s" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="textbold">
                                                                    Address</td>
                                                                <td colspan="3">
                                                                    <asp:TextBox ID="txtAddress" runat="server" CssClass="textboxgrey TextTitleCase"
                                                                        ReadOnly="True" Rows="3" TabIndex="20" TextMode="MultiLine" Width="530px"></asp:TextBox></td>
                                                                <td class="center top">
                                                                    <asp:Button ID="btnReset" runat="server" TabIndex="3" CssClass="button topMargin"
                                                                        Text="Reset" Width="106px" AccessKey="r" /></td>
                                                            </tr>
                                                            <tr>
                                                                <td class="textbold">
                                                                    City</td>
                                                                <td style="width: 30%">
                                                                    <asp:TextBox ID="txtCity" runat="server" CssClass="textboxgrey" ReadOnly="True" Width="165px"></asp:TextBox></td>
                                                                <td style="width: 15%" class="textbold">
                                                                    Country</td>
                                                                <td style="width: 30%">
                                                                    <asp:TextBox ID="txtCountry" runat="server" CssClass="textboxgrey" ReadOnly="True"
                                                                        Width="165px"></asp:TextBox></td>
                                                                <td>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="textbold">
                                                                    Date From
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtDateFrom" runat="server" CssClass="textboxgrey" ReadOnly="True"
                                                                        Width="165px"></asp:TextBox>
                                                                </td>
                                                                <td class="textbold">
                                                                    Date To
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtDateTo" runat="server" CssClass="textboxgrey" ReadOnly="True"
                                                                        Width="165px"></asp:TextBox>
                                                                </td>
                                                                <td>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="textbold">
                                                                    Chain Code
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtChainCode" runat="server" CssClass="textboxgrey" ReadOnly="True"
                                                                        Width="165px"></asp:TextBox>
                                                                </td>
                                                                <td class="textbold">
                                                                    Lcode
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtLcode" runat="server" CssClass="textboxgrey" ReadOnly="True"
                                                                        Width="165px"></asp:TextBox>
                                                                </td>
                                                                <td>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="textbold">
                                                                    Office ID
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtOfficeID" runat="server" CssClass="textboxgrey" ReadOnly="True"
                                                                        Width="165px"></asp:TextBox>
                                                                </td>
                                                                <td class="textbold">
                                                                </td>
                                                                <td>
                                                                </td>
                                                                <td>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="top" style="width: 100%;">
                                                        <table width="100%" border="0" class="tabOutherA" cellpadding="2" cellspacing="2">
                                                            <tr>
                                                                <td class="textbold" style="width: 65%">
                                                                    <table width="95%" border="0" cellpadding="0" cellspacing="0" class="tabMIDT">
                                                                        <tr>
                                                                            <td colspan="6" style="width: 100%" class="center">
                                                                                Average Last Three Month MIDT</td>
                                                                        </tr>
                                                                        <tr class="center bold">
                                                                            <td style="width: 25%">
                                                                                1A</td>
                                                                            <td style="width: 25%">
                                                                                1B</td>
                                                                            <td style="width: 12%">
                                                                                1G</td>
                                                                            <td style="width: 13%">
                                                                                1P</td>
                                                                            <td style="width: 12%">
                                                                                1W</td>
                                                                            <td style="width: 13%">
                                                                                Total</td>
                                                                        </tr>
                                                                        <tr class="right">
                                                                            <td>
                                                                                <asp:Literal ID="lit1A" runat="server"></asp:Literal>
                                                                            </td>
                                                                            <td>
                                                                                <asp:Literal ID="lit1B" runat="server"></asp:Literal>
                                                                            </td>
                                                                            <td>
                                                                                <asp:Literal ID="lit1G" runat="server"></asp:Literal>
                                                                            </td>
                                                                            <td>
                                                                                <asp:Literal ID="lit1P" runat="server"></asp:Literal>
                                                                            </td>
                                                                            <td>
                                                                                <asp:Literal ID="lit1W" runat="server"></asp:Literal>
                                                                            </td>
                                                                            <td>
                                                                                <asp:Literal ID="litTotal" runat="server"></asp:Literal>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                                <td class="textbold center" style="width: 18%">
                                                                    On contract</td>
                                                                <td style="width: 17%">
                                                                    <asp:TextBox ID="txtOnContract" runat="server" CssClass="textboxgrey" ReadOnly="True"
                                                                        Width="120px"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="textbold" style="width: 65%" rowspan="2">
                                                                    <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                                                        <tr>
                                                                            <td style="width: 50%" rowspan="4">
                                                                                <table width="90%" class="tab" border="0" cellpadding="0" cellspacing="0">
                                                                                    <tr>
                                                                                        <td style="width: 45%">
                                                                                        </td>
                                                                                        <td style="width: 55%">
                                                                                            Last Three Month BIDT</td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td>
                                                                                            Month-1
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:Literal ID="litMonth1" runat="server"></asp:Literal>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td>
                                                                                            Month-2
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:Literal ID="litMonth2" runat="server"></asp:Literal>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td>
                                                                                            Month-3
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:Literal ID="litMonth3" runat="server"></asp:Literal>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </td>
                                                                            <td class="top" style="width: 30%">
                                                                                Past Month Daily Motive</td>
                                                                            <td class="top" style="width: 20%">
                                                                                <asp:TextBox ID="txtPastMonthDailyMotive" runat="server" CssClass="textboxgrey" ReadOnly="True"
                                                                                    Width="70px"></asp:TextBox></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="top" style="width: 30%">
                                                                                Current Month Daily Motive</td>
                                                                            <td class="top" style="width: 20%">
                                                                                <asp:TextBox ID="txtCurrentMonthDailyMotive" runat="server" CssClass="textboxgrey"
                                                                                    ReadOnly="True" Width="70px"></asp:TextBox></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                            </td>
                                                                            <td>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                            </td>
                                                                            <td>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                                <td class="textbold top" style="width: 18%">
                                                                    Business commit %/Segs
                                                                </td>
                                                                <td class="top" style="width: 17%">
                                                                    <asp:TextBox ID="txtBusinessCommit" runat="server" CssClass="textboxgrey" ReadOnly="True"
                                                                        Width="120px"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr class="top">
                                                                <td class="textbold top">
                                                                    Latest Month 1A %/Segs
                                                                </td>
                                                                <td class="top">
                                                                    <asp:TextBox ID="txtLatestMonth1A" runat="server" CssClass="textboxgrey" ReadOnly="True"
                                                                        Width="120px"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>                                    <tr>
                                                    <td class="top" style="width: 100%;">
                                                        <table width="100%" border="0" cellpadding="2" cellspacing="2" class="tabVisitDetails">
                                                            <tr>
                                                                <td style="width: 15%;">
                                                                </td>
                                                                <td style="width: 23%;">
                                                                </td>
                                                                <td style="width: 15%;">
                                                                </td>
                                                                <td style="width: 23%;">
                                                                </td>
                                                                <td style="width: 9%;">
                                                                </td>
                                                                <td style="width: 9%;">
                                                                </td>
                                                                <td style="width: 10%;">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="7" style="width: 100%;" class="heading">
                                                                    Visit Details
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="7" style="width: 100%;">
                                                                    <asp:Label ID="lblErrorVisitDetails" runat="server" CssClass="Mandatory" EnableViewState="false"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    Accompanied By Manager
                                                                </td>
                                                                <td>
                                                                    <asp:DropDownList ID="ddlManager" runat="server" CssClass="dropdownlist" Width="150px">
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td>
                                                                    Person Met <span class="Mandatory">*</span>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtPersonMet" runat="server" CssClass="textbox" Width="150px"></asp:TextBox>
                                                                </td>
                                                                <td>
                                                                    In Time <span class="Mandatory">*</span>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtInTime" runat="server" CssClass="textbox" Width="70px"></asp:TextBox>
                                                                </td>
                                                                <td class="center">
                                                                <input type="button" value="Edit" onclick="AddVisitDetails()" id="btnHTMLAddVisitDetails" />
                                                                    <asp:Button ID="btnAddVisitDetails" Text="Add" runat="Server" CssClass="button" Width="60px" OnClientClick="return AddVisitDetails()" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    Accompanied By Reporting Manager
                                                                </td>
                                                                <td>
                                                                    <asp:DropDownList ID="ddlReportingManager" runat="server" CssClass="dropdownlist"
                                                                        Width="150px">
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td>
                                                                    Designation <span class="Mandatory">*</span>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtDesignation" runat="server" CssClass="textbox" Width="150px"></asp:TextBox>
                                                                </td>
                                                                <td>
                                                                    Out Time <span class="Mandatory">*</span>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtOutTime" runat="server" CssClass="textbox" Width="70px"></asp:TextBox>
                                                                </td>
                                                                <td class="center">
                                                                    <asp:Button ID="btnCancelVisitDetails" Text="Cancel" runat="Server" CssClass="button"
                                                                        Width="60px" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td  id="tdVisitDetails" colspan="7" style="width: 100%;">
                                                                <a href="#" onclick="" id=""/>
                                                                </td>
                                                                </tr>
                                                            <tr>
                                                                <td  colspan="7" style="width: 100%;">
                                                                    <asp:GridView ID="gvVisitDetails" runat="server" AutoGenerateColumns="False" TabIndex="6"
                                                                        Width="85%">
                                                                        <Columns>
                                                                            <asp:BoundField DataField="SEQUENCENO" HeaderText="S.No" />
                                                                            <asp:BoundField DataField="MANAGER_NAME" HeaderText="Manager" />
                                                                            <asp:BoundField DataField="IMMEDIATE_MANAGERNAME" HeaderText="Reporting Manager" />
                                                                            <asp:BoundField DataField="CONTACT_NAME" HeaderText="Person Met" />
                                                                            <asp:BoundField DataField="DESIGNATION" HeaderText="Designation" />
                                                                            <asp:BoundField DataField="INTIME" HeaderText="In Time" />
                                                                            <asp:BoundField DataField="OUTTIME" HeaderText="Out Time" />
                                                                            <asp:TemplateField HeaderText="Action">
                                                                                <ItemTemplate>
                                                                                    <asp:LinkButton ID="lnkEdit" runat="server" CommandName="EditX" Text="Edit" CssClass="LinkButtons"
                                                                                        CommandArgument='<%# DataBinder.Eval(Container.DataItem, "SEQUENCENO") + "|" + DataBinder.Eval(Container.DataItem, "MANAGERID")+ "|" + DataBinder.Eval(Container.DataItem, "MANAGER_NAME")+ "|" + DataBinder.Eval(Container.DataItem, "IMMEDIATE_MANAGERID")+"|" + DataBinder.Eval(Container.DataItem, "IMMEDIATE_MANAGERNAME")+ "|" + DataBinder.Eval(Container.DataItem, "CONTACT_NAME")+ "|" + DataBinder.Eval(Container.DataItem, "DESIGNATION")+ "|" + DataBinder.Eval(Container.DataItem, "INTIME")+ "|" + DataBinder.Eval(Container.DataItem, "OUTTIME") %>'></asp:LinkButton>&nbsp;
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                        </Columns>
                                                                        <AlternatingRowStyle CssClass="lightblue" />
                                                                        <RowStyle CssClass="textbold" />
                                                                        <HeaderStyle CssClass="Gridheading" ForeColor="white" />
                                                                    </asp:GridView>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    Visit Sub Type
                                                                </td>
                                                                <td style="width: 38%" colspan="2">
                                                                    <asp:RadioButton ID="rdServiceCall" runat="server" GroupName="VisitSubType" Text="Service Call" />
                                                                    <asp:RadioButton ID="rdStrategicCall" runat="server" GroupName="VisitSubType" Text="Strategic Call" />
                                                                </td>
                                                                <td>
                                                                    <input type="hidden" id="hdVisitDetails" runat="server" style="width: 1px" />
                                                                </td>
                                                                <td>
                                                                </td>
                                                                <td>
                                                                </td>
                                                                <td>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="top" style="width: 100%;">
                                                        <table width="100%" border="1" cellpadding="2" cellspacing="2" class="tabVisitDetails">
                                                            <tr>
                                                                <td style="width: 12%;">
                                                                </td>
                                                                <td style="width: 12%;">
                                                                </td>
                                                                <td style="width: 13%;">
                                                                </td>
                                                                <td style="width: 11%;">
                                                                </td>
                                                                <td style="width: 12%;">
                                                                </td>
                                                                <td style="width: 12%;">
                                                                </td>
                                                                <td style="width: 10%;">
                                                                </td>
                                                                <td style="width: 9%;">
                                                                </td>
                                                                <td style="width: 9%;">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="9" style="width: 100%;" class="heading">
                                                                    Service Call
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="9" style="width: 100%;">
                                                                    <asp:Label ID="lblErrorServiceCall" runat="server" CssClass="Mandatory" EnableViewState="false"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr class="top">
                                                                <td>
                                                                    Department<span class="Mandatory">*</span>
                                                                </td>
                                                                <td>
                                                                    <asp:DropDownList ID="ddlDepartment" runat="server" CssClass="dropdownlist" Width="110px">
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td>
                                                                    Deptt Specific
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtDepttSpecific" runat="server" CssClass="textbox" Width="100px"></asp:TextBox><br />
                                                                    <asp:DropDownList ID="ddlDepttSpecific" runat="server" CssClass="dropdownlist" Width="100px">
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td>
                                                                    Detailed Discussion/Issue Reported
                                                                </td>
                                                                <td style="width: 31%" colspan="3">
                                                                    <asp:TextBox ID="txtDetailedDiscussion" runat="server" CssClass="textbox" Width="200px"
                                                                        TextMode="MultiLine" Rows="3" Height="40px"></asp:TextBox>
                                                                </td>
                                                                <td class="center">
                                                                    <asp:Button ID="btnAddServiceCall" Text="Add" runat="Server" CssClass="button" Width="60px" />
                                                                    <asp:Button ID="btnCancelServiceCall" Text="Cancel" runat="Server" CssClass="button topMargin"
                                                                        Width="60px" />
                                                                </td>
                                                            </tr>
                                                            <tr class="top">
                                                                <td>
                                                                    Status<span class="Mandatory">*</span>
                                                                </td>
                                                                <td>
                                                                    <asp:DropDownList ID="ddlStatus" runat="server" CssClass="dropdownlist" Width="110px">
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td>
                                                                    Assigned to
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtAssignedTo" runat="server" CssClass="textbox" Width="100px"></asp:TextBox><img
                                                                        alt="AssingedTo" onclick="javascript:return EmployeePageVisitDetails();" src="../Images/lookup.gif"
                                                                        tabindex="1" style="cursor: pointer;" /></td>
                                                                <td>
                                                                    Closer Date
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtCloserDate" runat="server" CssClass="textbox" Width="110px"></asp:TextBox>
                                                                </td>
                                                                <td colspan="3" style="width: 27%">
                                                                    <asp:CheckBox ID="chkPreviousVisitOpenItems" runat="server" Text="Prev visit Open items &nbsp;"
                                                                        TextAlign="left"></asp:CheckBox>
                                                                </td>
                                                                <td class="center">
                                                                </td>
                                                            </tr>
                                                            <tr class="top">
                                                                <td>
                                                                    Competition Info/Mkt info Remarks
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtCompetitionInfo" runat="server" CssClass="textbox" Width="110px"></asp:TextBox>
                                                                </td>
                                                                <td>
                                                                    Target Closer Date <span class="Mandatory">*</span>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtTargetCloserDate" runat="server" CssClass="textbox" Width="110px"></asp:TextBox>
                                                                </td>
                                                                <td>
                                                                    Follow-up Remarks
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtFollowUpRemarks" runat="server" CssClass="textbox" Width="110px"></asp:TextBox>
                                                                </td>
                                                                <td colspan="3" style="width: 27%">
                                                                    <asp:CheckBox ID="chkPreviousVisitItems" runat="server" Text="Previous visit items &nbsp;&nbsp;&nbsp;&nbsp;"
                                                                        TextAlign="left"></asp:CheckBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="9" style="width: 100%;">
                                                                    <asp:GridView ID="gvServiceCall" runat="server" AutoGenerateColumns="False" TabIndex="6"
                                                                        Width="90%">
                                                                        <Columns>
                                                                            <asp:BoundField DataField="SEQUENCENO" HeaderText="S.No" />
                                                                            <asp:BoundField DataField="DEPARTMENT_NAME" HeaderText="Department" />
                                                                            <asp:BoundField DataField="DEPARTMENT_SPECIFIC" HeaderText="Deptt Specific" />
                                                                            <asp:BoundField DataField="SC_DISCUSSIONISSUE_REMARKS" HeaderText="Detailed Discussion/Issue Reported"
                                                                                HeaderStyle-Wrap="true" />
                                                                            <asp:BoundField DataField="SC_STATUSID_NAME" HeaderText="Status" />
                                                                            <asp:BoundField DataField="ASSIGNTO_EMPLOYEE_NAME" HeaderText="Assigned to" />
                                                                            <asp:BoundField DataField="SC_COMPETITION_MKT_INFO_REMARKS" HeaderText="Competition  Info/Mkt info Remarks"
                                                                                HeaderStyle-Wrap="true" />
                                                                            <asp:BoundField DataField="CLOSER_DATETIME" HeaderText="Closer Date" />
                                                                            <asp:BoundField DataField="TARGET_CLOSER_DATETIME" HeaderText="Target Closer Date" />
                                                                            <asp:BoundField DataField="SC_FOLLOWUP_REMARKS" HeaderText="Followup Remarks" />
                                                                            <asp:TemplateField HeaderText="Action">
                                                                                <ItemTemplate>
                                                                                    <asp:LinkButton ID="lnkEdit" runat="server" CommandName="EditX" Text="Edit" CssClass="LinkButtons"
                                                                                        CommandArgument='<%# DataBinder.Eval(Container.DataItem, "SEQUENCENO") + "|" + DataBinder.Eval(Container.DataItem, "MANAGERID")+ "|" + DataBinder.Eval(Container.DataItem, "MANAGER_NAME")+ "|" + DataBinder.Eval(Container.DataItem, "IMMEDIATE_MANAGERID")+"|" + DataBinder.Eval(Container.DataItem, "IMMEDIATE_MANAGERNAME")+ "|" + DataBinder.Eval(Container.DataItem, "CONTACT_NAME")+ "|" + DataBinder.Eval(Container.DataItem, "DESIGNATION")+ "|" + DataBinder.Eval(Container.DataItem, "INTIME")+ "|" + DataBinder.Eval(Container.DataItem, "OUTTIME") %>'></asp:LinkButton>&nbsp;
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                        </Columns>
                                                                        <AlternatingRowStyle CssClass="lightblue" />
                                                                        <RowStyle CssClass="textbold" />
                                                                        <HeaderStyle CssClass="Gridheading" ForeColor="white" />
                                                                    </asp:GridView>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="9" style="width: 100%;">
                                                                    <input type="hidden" runat="server" id="hdServiceCall" style="width: 1px" />
                                                                    <input type="hidden" runat="server" id="hdAssingedTo" style="width: 1px" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="top" style="width: 100%;">
                                                        <table width="100%" border="1" cellpadding="2" cellspacing="2" class="tabVisitDetails">
                                                            <tr>
                                                                <td style="width: 20%;">
                                                                </td>
                                                                <td style="width: 20%;">
                                                                </td>
                                                                <td style="width: 20%;">
                                                                </td>
                                                                <td style="width: 40%;">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="4" style="width: 100%;" class="heading">
                                                                    Strategic Visits
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="4" style="width: 100%;">
                                                                    <asp:Label ID="lblStrategicVisits" runat="server" CssClass="Mandatory" EnableViewState="false"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr class="top">
                                                                <td>
                                                                    Strategic Visits<span class="Mandatory">*</span>
                                                                </td>
                                                                <td>
                                                                    <asp:RadioButton ID="rbRetention" runat="server" Text="Retention" />
                                                                </td>
                                                                <td>
                                                                    <asp:RadioButton ID="rbTarget" runat="server" Text="Target" />
                                                                </td>
                                                                <td>
                                                                    <asp:RadioButton ID="rbAirNonair" runat="server" Text="Air & Non air Product & others" />
                                                                </td>
                                                            </tr>
                                                            <tr class="top">
                                                                <td colspan="4" style="width: 100%;">
                                                                    <table width="100%" border="1" cellpadding="2" cellspacing="2" class="tabVisitDetails">
                                                                        <tr>
                                                                            <td style="width: 9%;">
                                                                            </td>
                                                                            <td style="width: 12%;">
                                                                            </td>
                                                                            <td style="width: 9%;">
                                                                            </td>
                                                                            <td style="width: 11%;">
                                                                            </td>
                                                                            <td style="width: 12%;">
                                                                            </td>
                                                                            <td style="width: 12%;">
                                                                            </td>
                                                                            <td style="width: 10%;">
                                                                            </td>
                                                                            <td style="width: 9%;">
                                                                            </td>
                                                                            <td style="width: 16%;">
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td colspan="9" style="width: 100%;" class="heading">
                                                                                Retention
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td colspan="9" style="width: 100%;">
                                                                                <asp:Label ID="lblRetention" runat="server" CssClass="Mandatory" EnableViewState="false"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr class="top">
                                                                            <td>
                                                                                Existing Deal<span class="Mandatory">*</span>
                                                                            </td>
                                                                            <td>
                                                                                <asp:TextBox ID="txtExistingDeal" runat="server" CssClass="textbox" Width="100px"></asp:TextBox>
                                                                            </td>
                                                                            <td>
                                                                                CPS <span class="Mandatory">*</span>
                                                                            </td>
                                                                            <td>
                                                                                <asp:TextBox ID="txtCPS" runat="server" CssClass="textbox" Width="100px"></asp:TextBox>
                                                                            </td>
                                                                            <td>
                                                                                Retention Reason <span class="Mandatory">*</span>
                                                                            </td>
                                                                            <td>
                                                                                <asp:DropDownList ID="ddlRetentionReason" runat="server" CssClass="dropdownlist"
                                                                                    Width="110px">
                                                                                </asp:DropDownList>
                                                                            </td>
                                                                            <td>
                                                                                <asp:Button ID="btnAddRetention" Text="Add" runat="Server" CssClass="button" Width="60px" />
                                                                                <asp:Button ID="btnCancelRetention" Text="Cancel" runat="Server" CssClass="button topMargin"
                                                                                    Width="60px" />
                                                                            </td>
                                                                            <td colspan="2" rowspan="3">
                                                                                <asp:GridView ID="gvPreviousRetentionRemarks" runat="server" AutoGenerateColumns="False"
                                                                                    TabIndex="6" Width="90%">
                                                                                    <Columns>
                                                                                        <asp:BoundField DataField="SEQUENCENO" HeaderText="Date" />
                                                                                        <asp:BoundField DataField="DEPARTMENT_NAME" HeaderText="Previous Remarks" />
                                                                                    </Columns>
                                                                                    <AlternatingRowStyle CssClass="lightblue" />
                                                                                    <RowStyle CssClass="textbold" />
                                                                                    <HeaderStyle CssClass="Gridheading" ForeColor="white" />
                                                                                </asp:GridView>
                                                                            </td>
                                                                        </tr>
                                                                        <tr class="top">
                                                                            <td>
                                                                                Retention Status <span class="Mandatory">*</span>
                                                                            </td>
                                                                            <td>
                                                                                <asp:DropDownList ID="ddlRetentionStatus" runat="server" CssClass="dropdownlist"
                                                                                    Width="110px">
                                                                                </asp:DropDownList>
                                                                            </td>
                                                                            <td>
                                                                                New CPS<span class="Mandatory">*</span>
                                                                            </td>
                                                                            <td>
                                                                                <asp:TextBox ID="txtNewCPS" runat="server" CssClass="textbox" Width="100px"></asp:TextBox>
                                                                            </td>
                                                                            <td>
                                                                                1A Approved New Deal<span class="Mandatory">*</span>
                                                                            </td>
                                                                            <td>
                                                                                <asp:TextBox ID="txt1AApprovedNewDeal" runat="server" CssClass="textbox" Width="110px"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr class="top">
                                                                            <td>
                                                                                Competition Info/Mkt info Remarks
                                                                            </td>
                                                                            <td>
                                                                                <asp:TextBox ID="txtRetentionCompetitionInfo" runat="server" CssClass="textbox" Width="110px"></asp:TextBox>
                                                                            </td>
                                                                            <td>
                                                                                Detailed Discussion/Issue Reported <span class="Mandatory">*</span>
                                                                            </td>
                                                                            <td>
                                                                                <asp:TextBox ID="txtRetentionDetailedDiscussion" runat="server" CssClass="textbox"
                                                                                    Width="110px"></asp:TextBox>
                                                                            </td>
                                                                            <td>
                                                                                Signed On/Conversion On (date)
                                                                            </td>
                                                                            <td>
                                                                                <asp:TextBox ID="txtRetentionSignedOn" runat="server" CssClass="textbox" Width="110px"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr class="top">
                                                                            <td>
                                                                                Target- Segs/% of Business<span class="Mandatory">*</span>
                                                                            </td>
                                                                            <td>
                                                                                <asp:TextBox ID="txtRetentionTargetSegs" runat="server" CssClass="textbox" Width="110px"></asp:TextBox>
                                                                            </td>
                                                                            <td>
                                                                            </td>
                                                                            <td>
                                                                            </td>
                                                                            <td>
                                                                            </td>
                                                                            <td>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td colspan="9" style="width: 100%;">
                                                                                <asp:GridView ID="gvRetention" runat="server" AutoGenerateColumns="False" TabIndex="6"
                                                                                    Width="90%">
                                                                                    <Columns>
                                                                                        <asp:BoundField DataField="SEQUENCENO" HeaderText="S.No" />
                                                                                        <asp:BoundField DataField="STR_TYPENAME" HeaderText="Visit Subtype" />
                                                                                        <asp:BoundField DataField="EXISTINGDEAL" HeaderText="Existing Deal" />
                                                                                        <asp:BoundField DataField="CPS" HeaderText="CPS" />
                                                                                        <asp:BoundField DataField="SVR_REASON_NAME" HeaderText="Retention Reason" />
                                                                                        <asp:BoundField DataField="SVR_STATUS_NAME" HeaderText="Status/ Retention Phase" />
                                                                                        <asp:BoundField DataField="A1APPROVED_NEW_DEAL" HeaderText="1A Approved New Dea"
                                                                                            HeaderStyle-Wrap="true" />
                                                                                        <asp:BoundField DataField="NEWCPS" HeaderText="New CPS" />
                                                                                        <asp:BoundField DataField="STR_COMPETITION_MKT_INFO_REMARKS" HeaderText="Competition  Info/Mkt info Remarks"
                                                                                            HeaderStyle-Wrap="true" />
                                                                                        <asp:BoundField DataField="STR_DISCUSSIONISSUE_REMARKS" HeaderText="Detailed Discussion/Issue Reported"
                                                                                            HeaderStyle-Wrap="true" />
                                                                                        <asp:BoundField DataField="STR_SIGNON_DATE" HeaderText="Signed On/Conversion On"
                                                                                            HeaderStyle-Wrap="true" />
                                                                                        <asp:BoundField DataField="STR_TARGET_SEG" HeaderText="Target- Segs/% of Business"
                                                                                            HeaderStyle-Wrap="true" />
                                                                                        <asp:BoundField DataField="STR_PREV_REMARKS" HeaderText="Previous remarksDD/MM/YY"
                                                                                            HeaderStyle-Wrap="true" />
                                                                                        <asp:BoundField DataField="STR_PREV_REMARKS" HeaderText="Previous remarks" HeaderStyle-Wrap="true" />
                                                                                        <asp:TemplateField HeaderText="Action">
                                                                                            <ItemTemplate>
                                                                                                <asp:LinkButton ID="lnkEdit" runat="server" CommandName="EditX" Text="Edit" CssClass="LinkButtons"
                                                                                                    CommandArgument='<%# DataBinder.Eval(Container.DataItem, "SEQUENCENO") + "|" + DataBinder.Eval(Container.DataItem, "MANAGERID")+ "|" + DataBinder.Eval(Container.DataItem, "MANAGER_NAME")+ "|" + DataBinder.Eval(Container.DataItem, "IMMEDIATE_MANAGERID")+"|" + DataBinder.Eval(Container.DataItem, "IMMEDIATE_MANAGERNAME")+ "|" + DataBinder.Eval(Container.DataItem, "CONTACT_NAME")+ "|" + DataBinder.Eval(Container.DataItem, "DESIGNATION")+ "|" + DataBinder.Eval(Container.DataItem, "INTIME")+ "|" + DataBinder.Eval(Container.DataItem, "OUTTIME") %>'></asp:LinkButton>&nbsp;
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                    </Columns>
                                                                                    <AlternatingRowStyle CssClass="lightblue" />
                                                                                    <RowStyle CssClass="textbold" />
                                                                                    <HeaderStyle CssClass="Gridheading" ForeColor="white" />
                                                                                </asp:GridView>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td colspan="9" style="width: 100%;">
                                                                                <input type="hidden" runat="server" id="hdRetention" style="width: 1px" />
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                            <tr class="top">
                                                                <td colspan="4" style="width: 100%;">
                                                                    <table width="100%" border="1" cellpadding="2" cellspacing="2" class="tabVisitDetails">
                                                                        <tr>
                                                                            <td style="width: 9%;">
                                                                            </td>
                                                                            <td style="width: 12%;">
                                                                            </td>
                                                                            <td style="width: 9%;">
                                                                            </td>
                                                                            <td style="width: 11%;">
                                                                            </td>
                                                                            <td style="width: 12%;">
                                                                            </td>
                                                                            <td style="width: 12%;">
                                                                            </td>
                                                                            <td style="width: 10%;">
                                                                            </td>
                                                                            <td style="width: 9%;">
                                                                            </td>
                                                                            <td style="width: 16%;">
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td colspan="9" style="width: 100%;" class="heading">
                                                                                Target
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td colspan="9" style="width: 100%;">
                                                                                <asp:Label ID="lblRetentionTarget" runat="server" CssClass="Mandatory" EnableViewState="false"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr class="top">
                                                                            <td>
                                                                                1A Approved New Deal<span class="Mandatory">*</span>
                                                                            </td>
                                                                            <td>
                                                                                <asp:TextBox ID="txtTarget1AApprovedNewDeal" runat="server" CssClass="textbox" Width="100px"></asp:TextBox>
                                                                            </td>
                                                                            <td>
                                                                                CPS <span class="Mandatory">*</span>
                                                                            </td>
                                                                            <td>
                                                                                <asp:TextBox ID="txtTargetCPS" runat="server" CssClass="textbox" Width="100px"></asp:TextBox>
                                                                            </td>
                                                                            <td>
                                                                                Status<span class="Mandatory">*</span>
                                                                            </td>
                                                                            <td>
                                                                                <asp:DropDownList ID="ddlTargetStatus" runat="server" CssClass="dropdownlist" Width="110px">
                                                                                </asp:DropDownList>
                                                                            </td>
                                                                            <td>
                                                                                <asp:Button ID="btnTargetAdd" Text="Add" runat="Server" CssClass="button" Width="60px" />
                                                                                <asp:Button ID="btnbtnTargetCancel" Text="Cancel" runat="Server" CssClass="button topMargin"
                                                                                    Width="60px" />
                                                                            </td>
                                                                            <td colspan="2" rowspan="2">
                                                                                <asp:GridView ID="gvTargetPreviousRemarks" runat="server" AutoGenerateColumns="False"
                                                                                    TabIndex="6" Width="90%">
                                                                                    <Columns>
                                                                                        <asp:BoundField DataField="SEQUENCENO" HeaderText="Date" />
                                                                                        <asp:BoundField DataField="DEPARTMENT_NAME" HeaderText="Previous Remarks" />
                                                                                    </Columns>
                                                                                    <AlternatingRowStyle CssClass="lightblue" />
                                                                                    <RowStyle CssClass="textbold" />
                                                                                    <HeaderStyle CssClass="Gridheading" ForeColor="white" />
                                                                                </asp:GridView>
                                                                            </td>
                                                                        </tr>
                                                                        <tr class="top">
                                                                            <td>
                                                                                Competition Info/Mkt info Remarks
                                                                            </td>
                                                                            <td>
                                                                                <asp:TextBox ID="txtTargetCompetitionInfo" runat="server" CssClass="textbox" Width="110px"></asp:TextBox>
                                                                            </td>
                                                                            <td>
                                                                                Detailed Discussion/Issue Reported <span class="Mandatory">*</span>
                                                                            </td>
                                                                            <td>
                                                                                <asp:TextBox ID="txtTargetDetailedDiscussion" runat="server" CssClass="textbox" Width="110px"></asp:TextBox>
                                                                            </td>
                                                                            <td>
                                                                                Signed On/Conversion On (date)
                                                                            </td>
                                                                            <td>
                                                                                <asp:TextBox ID="txtTargetSignedOn" runat="server" CssClass="textbox" Width="110px"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr class="top">
                                                                            <td>
                                                                                Target- Segs/% of Business<span class="Mandatory">*</span>
                                                                            </td>
                                                                            <td>
                                                                                <asp:TextBox ID="txtTargetTargetSegs" runat="server" CssClass="textbox" Width="110px"></asp:TextBox>
                                                                            </td>
                                                                            <td>
                                                                            </td>
                                                                            <td>
                                                                            </td>
                                                                            <td>
                                                                            </td>
                                                                            <td>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td colspan="9" style="width: 100%;">
                                                                                <asp:GridView ID="gvTarget" runat="server" AutoGenerateColumns="False" TabIndex="6"
                                                                                    Width="90%">
                                                                                    <Columns>
                                                                                        <asp:BoundField DataField="SEQUENCENO" HeaderText="S.No" />
                                                                                        <asp:BoundField DataField="STT_TYPENAME" HeaderText="Visit Subtype" />
                                                                                        <asp:BoundField DataField="A1APPROVED_NEW_DEAL" HeaderText="1A Approved New Dea"
                                                                                            HeaderStyle-Wrap="true" />
                                                                                        <asp:BoundField DataField="CPS" HeaderText="CPS" />
                                                                                        <asp:BoundField DataField="SVT_STATUS_NAME" HeaderText="Status/ Target Phase" />
                                                                                        <asp:BoundField DataField="STT_COMPETITION_MKT_INFO_REMARKS" HeaderText="Competition  Info/Mkt info Remarks"
                                                                                            HeaderStyle-Wrap="true" />
                                                                                        <asp:BoundField DataField="STT_DISCUSSIONISSUE_REMARKS" HeaderText="Detailed Discussion/Issue Reported"
                                                                                            HeaderStyle-Wrap="true" />
                                                                                        <asp:BoundField DataField="STT_SIGNON_DATE" HeaderText="Signed On/Conversion On"
                                                                                            HeaderStyle-Wrap="true" />
                                                                                        <asp:BoundField DataField="STT_TARGET_SEG" HeaderText="Target- Segs/% of Business"
                                                                                            HeaderStyle-Wrap="true" />
                                                                                        <asp:BoundField DataField="STT_PREV_REMARKS" HeaderText="Previous remarksDD/MM/YY"
                                                                                            HeaderStyle-Wrap="true" />
                                                                                        <asp:BoundField DataField="STT_PREV_REMARKS" HeaderText="Previous remarks" HeaderStyle-Wrap="true" />
                                                                                        <asp:TemplateField HeaderText="Action">
                                                                                            <ItemTemplate>
                                                                                                <asp:LinkButton ID="lnkEdit" runat="server" CommandName="EditX" Text="Edit" CssClass="LinkButtons"
                                                                                                    CommandArgument='<%# DataBinder.Eval(Container.DataItem, "SEQUENCENO") + "|" + DataBinder.Eval(Container.DataItem, "MANAGERID")+ "|" + DataBinder.Eval(Container.DataItem, "MANAGER_NAME")+ "|" + DataBinder.Eval(Container.DataItem, "IMMEDIATE_MANAGERID")+"|" + DataBinder.Eval(Container.DataItem, "IMMEDIATE_MANAGERNAME")+ "|" + DataBinder.Eval(Container.DataItem, "CONTACT_NAME")+ "|" + DataBinder.Eval(Container.DataItem, "DESIGNATION")+ "|" + DataBinder.Eval(Container.DataItem, "INTIME")+ "|" + DataBinder.Eval(Container.DataItem, "OUTTIME") %>'></asp:LinkButton>&nbsp;
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                    </Columns>
                                                                                    <AlternatingRowStyle CssClass="lightblue" />
                                                                                    <RowStyle CssClass="textbold" />
                                                                                    <HeaderStyle CssClass="Gridheading" ForeColor="white" />
                                                                                </asp:GridView>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td colspan="9" style="width: 100%;">
                                                                                <input type="hidden" runat="server" id="hdTarget" style="width: 1px" />
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                            <tr class="top">
                                                                <td colspan="4" style="width: 100%;">
                                                                    <table width="100%" border="1" cellpadding="2" cellspacing="2" class="tabVisitDetails">
                                                                        <tr>
                                                                            <td style="width: 9%;">
                                                                            </td>
                                                                            <td style="width: 12%;">
                                                                            </td>
                                                                            <td style="width: 9%;">
                                                                            </td>
                                                                            <td style="width: 11%;">
                                                                            </td>
                                                                            <td style="width: 12%;">
                                                                            </td>
                                                                            <td style="width: 12%;">
                                                                            </td>
                                                                            <td style="width: 10%;">
                                                                            </td>
                                                                            <td style="width: 9%;">
                                                                            </td>
                                                                            <td style="width: 16%;">
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td colspan="9" style="width: 100%;" class="heading">
                                                                                Air & Non Air Product & Others
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td colspan="9" style="width: 100%;">
                                                                                <asp:Label ID="lblAirNonAirProduct" runat="server" CssClass="Mandatory" EnableViewState="false"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr class="top">
                                                                            <td>
                                                                                Product Name<span class="Mandatory">*</span>
                                                                            </td>
                                                                            <td>
                                                                                <asp:DropDownList ID="ddlProductName" runat="server" CssClass="dropdownlist" Width="110px">
                                                                                </asp:DropDownList>
                                                                            </td>
                                                                            <td>
                                                                                Revenue <span class="Mandatory">*</span>
                                                                            </td>
                                                                            <td>
                                                                                <asp:TextBox ID="txtRevenue" runat="server" CssClass="textbox" Width="100px"></asp:TextBox>
                                                                            </td>
                                                                            <td>
                                                                                Status<span class="Mandatory">*</span>
                                                                            </td>
                                                                            <td>
                                                                                <asp:DropDownList ID="ddlAirNonAirStatus" runat="server" CssClass="dropdownlist"
                                                                                    Width="110px">
                                                                                </asp:DropDownList>
                                                                            </td>
                                                                            <td>
                                                                                <asp:Button ID="btnAirNonAirAdd" Text="Add" runat="Server" CssClass="button" Width="60px" />
                                                                                <asp:Button ID="btnAirNonAirCancel" Text="Cancel" runat="Server" CssClass="button topMargin"
                                                                                    Width="60px" />
                                                                            </td>
                                                                            <td colspan="2" rowspan="2">
                                                                                <asp:GridView ID="gvAirNonAirPreviousRemarks" runat="server" AutoGenerateColumns="False"
                                                                                    TabIndex="6" Width="90%">
                                                                                    <Columns>
                                                                                        <asp:BoundField DataField="SEQUENCENO" HeaderText="Date" />
                                                                                        <asp:BoundField DataField="DEPARTMENT_NAME" HeaderText="Previous Remarks" />
                                                                                    </Columns>
                                                                                    <AlternatingRowStyle CssClass="lightblue" />
                                                                                    <RowStyle CssClass="textbold" />
                                                                                    <HeaderStyle CssClass="Gridheading" ForeColor="white" />
                                                                                </asp:GridView>
                                                                            </td>
                                                                        </tr>
                                                                        <tr class="top">
                                                                            <td>
                                                                                Competition Info/Mkt info Remarks
                                                                            </td>
                                                                            <td>
                                                                                <asp:TextBox ID="txtAirNonAirCompetitionInfo" runat="server" CssClass="textbox" Width="110px"></asp:TextBox>
                                                                            </td>
                                                                            <td>
                                                                                Detailed Discussion/Issue Reported <span class="Mandatory">*</span>
                                                                            </td>
                                                                            <td>
                                                                                <asp:TextBox ID="txtAirNonAirDetailedDiscussion" runat="server" CssClass="textbox"
                                                                                    Width="110px"></asp:TextBox>
                                                                            </td>
                                                                            <td>
                                                                                Signed On/Conversion On (date)
                                                                            </td>
                                                                            <td>
                                                                                <asp:TextBox ID="txtAirNonAirSignedOn" runat="server" CssClass="textbox" Width="110px"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td colspan="9" style="width: 100%;">
                                                                                <asp:GridView ID="gvAirNonAir" runat="server" AutoGenerateColumns="False" TabIndex="6"
                                                                                    Width="90%">
                                                                                    <Columns>
                                                                                        <asp:BoundField DataField="SEQUENCENO" HeaderText="S.No" />
                                                                                        <asp:BoundField DataField="STA_TYPENAME" HeaderText="Visit Subtype" />
                                                                                        <asp:BoundField DataField="PRODUCT_NAME" HeaderText="Product Name" />
                                                                                        <asp:BoundField DataField="REVENUE" HeaderText="Revenue" />
                                                                                        <asp:BoundField DataField="SV_STATUS_NAME" HeaderText="Status" />
                                                                                        <asp:BoundField DataField="STA_COMPETITION_MKT_INFO_REMARKS" HeaderText="Competition  Info/Mkt info Remarks"
                                                                                            HeaderStyle-Wrap="true" />
                                                                                        <asp:BoundField DataField="STA_DISCUSSIONISSUE_REMARKS" HeaderText="Detailed Discussion/Issue Reported"
                                                                                            HeaderStyle-Wrap="true" />
                                                                                        <asp:BoundField DataField="STA_SIGNON_DATE" HeaderText="Signed On/Conversion On"
                                                                                            HeaderStyle-Wrap="true" />
                                                                                        <asp:BoundField DataField="STA_PREV_REMARKS" HeaderText="Previous remarksDD/MM/YY"
                                                                                            HeaderStyle-Wrap="true" />
                                                                                        <asp:BoundField DataField="STA_PREV_REMARKS" HeaderText="Previous remarks" HeaderStyle-Wrap="true" />
                                                                                        <asp:TemplateField HeaderText="Action">
                                                                                            <ItemTemplate>
                                                                                                <asp:LinkButton ID="lnkEdit" runat="server" CommandName="EditX" Text="Edit" CssClass="LinkButtons"
                                                                                                    CommandArgument='<%# DataBinder.Eval(Container.DataItem, "SEQUENCENO") + "|" + DataBinder.Eval(Container.DataItem, "MANAGERID")+ "|" + DataBinder.Eval(Container.DataItem, "MANAGER_NAME")+ "|" + DataBinder.Eval(Container.DataItem, "IMMEDIATE_MANAGERID")+"|" + DataBinder.Eval(Container.DataItem, "IMMEDIATE_MANAGERNAME")+ "|" + DataBinder.Eval(Container.DataItem, "CONTACT_NAME")+ "|" + DataBinder.Eval(Container.DataItem, "DESIGNATION")+ "|" + DataBinder.Eval(Container.DataItem, "INTIME")+ "|" + DataBinder.Eval(Container.DataItem, "OUTTIME") %>'></asp:LinkButton>&nbsp;
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                    </Columns>
                                                                                    <AlternatingRowStyle CssClass="lightblue" />
                                                                                    <RowStyle CssClass="textbold" />
                                                                                    <HeaderStyle CssClass="Gridheading" ForeColor="white" />
                                                                                </asp:GridView>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td colspan="9" style="width: 100%;">
                                                                                <input type="hidden" runat="server" id="hdAirNonAir" style="width: 1px" />
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
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
