<%@ page language="VB" autoeventwireup="false" inherits="RPSR_PTypeChallanShow, App_Web_rpsr_reportshow.aspx.cdcab7d2" validaterequest="false" enableeventvalidation="false" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=10.2.3600.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Report</title>
    <link href="CSS/AAMS.css" type="text/css" rel="stylesheet"/>
    <link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css"
        rel="stylesheet" type="text/css" />
    <link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css"
        rel="stylesheet" type="text/css" />
    <link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css"
        rel="stylesheet" type="text/css" />
       

    <link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css"
        rel="stylesheet" type="text/css" />
         <script language="javascript" type="text/javascript" >
         function TextContent()
         {  
           
            if (document.getElementById("hdStatus").value=="1")
            {
                NewsBody_rich.document.body.innerHTML=document.getElementById("hdData1").value;
                document.getElementById('hdData').value=NewsBody_rich.document.body.innerText;
                document.getElementById("hdData1").value="";
                NewsBody_rich.document.body.innerText="";
                __doPostBack('btnSave','');  
			}
			else
			{
			document.getElementById("NewsBody_rich").className="displayNone";
			}
         }
        </script>
</head>
<body >
 <form id="FrmReportShow" runat="server" >
     <br />
    <table  align="left" class="border_rightred">
    <tr></tr>
            <tr>
                <td valign="top" style="width:800px" id="tdSize" runat ="server" >
                    <table  style="width:97%" border="0" cellpadding="0" cellspacing="0" class="" bgcolor="#FFFFFF">
                     <tr>
                        <td  colspan ="2" style="text-align: center"><asp:Label ID="lblError" runat="server" CssClass="ErrorMsg" EnableViewState="False"></asp:Label>
                            </td>
                        
                    </tr>
                      <tr> 
                      <td style="width:10px">
                      </td>
                     <td style="width:100%">   
                         <CR:CrystalReportViewer ID="CRViewer1" runat="server" AutoDataBind="true" HasToggleGroupTreeButton="False" PrintMode="ActiveX" Width="100%" />
                     </td>
                     
                      </tr>       
                        <tr>
                            <td style="width: 10px">
                            </td>
                            <td style="width: 100%">
                            <div id="divDataConvert" runat="server"></div>
                            <input type="hidden" runat="server" id="hdData" />
                            <input type="hidden" runat="server" id="hdData1" />
                            <input type="hidden" runat="server" id="hdStatus" />
                                <asp:Button ID="btnSave" runat="server" CssClass="displayNone" Text="Button" />
                                <iframe id="NewsBody_rich" src="Popup/MsgBody.htm" width="100%" height="1px" font-name="Verdana" onload="TextContent()">
                                                                                </iframe>
                                </td>
                        </tr>
                    <tr>
                        <td align="right" style="height: 19px">
                            &nbsp;</td>
                        
                    </tr>
                    
                    </table>
    </td>
    </tr>
    </table>
    </form>
</body>

</html>

