<%@ Page Language="VB" ValidateRequest="false" EnableEventValidation="false"   AutoEventWireup="false" CodeFile="TASR_ViewOrderDoc.aspx.vb" Inherits="TravelAgency_TASR_ViewOrderDoc" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>View Order Document</title>
    <base target="_self"/>
    <link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" media="all" href="../Calender/calendar-blue.css"
        title="win2k-cold-1" />
        
    <link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../JavaScript/AAMS.js"></script>

    <script language="javascript" type="text/javascript" >
    
    function PrintImage()
    {
    document.getElementById('ifrmPrnt').width = "100%"; 
    document.getElementById('ifrmPrnt').height  = "100%"; 
    document.frames(0).focus();
	document.frames(0).print(); 
	return false;	
    }
    
    function ViewAllDoc()
    {
    document.getElementById('<%=btnSaveAs.ClientID%>').disable=true;
   document.getElementById('<%=btnPrint.ClientID%>').disable=true;
        window.location.href="../TravelAgency/TASR_ViewOrderDoc.aspx?FileNo="+document.getElementById('<%=hdFileNo.ClientID%>').value;
    }
    
//    function my_onkeydown_handler()
//		{	
//			switch (event.keyCode)
//			{
//			case 122 : // 'F11'
//			event.returnValue = false;
//			event.keyCode = 0; 			
//			break; 
//			}
//		}
//		
//		document.attachEvent("onkeydown", my_onkeydown_handler);			
//	grdViewDocument_ctl03_imgDocument
function GetWidth()
{

var imag=window.document.images["grdViewDocument_ctl03_imgDocument"];
var a=imag.width;
var b=imag.height;
}	
    </script>
</head>
<script type="text/javascript" src="../JavaScript/AAMS.js"></script>

<body oncontextmenu="return false;" >
    <form id="frmViewOrderDoc" runat="server">
    
  <table width="860px" align="left" style="height: 486px">
            <tr>
                <td valign="top" style="height: 482px">
                    <table width="100%">
                        <tr>
                            <td valign="top">
                                <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td valign="top" align="left"  style="width:860px;">
                                            <span class="menu">Travel Agency-></span><span class="sub_menu">Order Document</span>
                                        </td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td bgcolor="#1A61A9" class="heading" align="center" style="width:860px;">
                                           <span id="spHeader" runat="server">View Order Document</span> 
                                        </td>
                                        <td bgcolor="#1A61A9"></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td valign="top" style="height: 181px">
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                     <tr>
                                        <td valign="top" class="redborder">
                                            <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                                     <tr>
                                                         <td class="textbold" align="center" valign="TOP" rowspan="0" style="height: 15px">
                                                            <asp:Label ID="lblError" class="ErrorMsg" runat="server"></asp:Label>
                                                          </td>
                                                    </tr>
                                                    <tr>
                                                       <td>
                                                          <table style="width:860px;">                                                                 
                                                                <tr>
                                                                    <td class="textbold" style="height: 22px">Agency Name</td>
                                                                    <td colspan="5" style="height: 22px">
                                                                    <asp:TextBox ID="txtAgencyName" CssClass="textboxgrey" runat="server" Width="402px" ReadOnly="True"></asp:TextBox>
                                                                    </td>                                                                      
                                                                
                                                                </tr>                                                                        
                                                                <tr>
                                                                    <td class="textbold">Order No.</td>
                                                                    <td style="width: 170px"><asp:TextBox ID="txtOrderName" runat="server" CssClass="textboxgrey" ReadOnly="True" Width="136px"></asp:TextBox></td>
                                                                    <td class="textbold">Order Type</td>
                                                                    <td style="width:150px"> <asp:TextBox ID="txtOrderType" runat="server" CssClass="textboxgrey" Width="136px" ReadOnly="True"></asp:TextBox> </td>
                                                                    <td class="textbold" colspan="2">
                                                                    <asp:button id="btnZoomIn" runat="server" Text="Zoom In"  CssClass="button" ></asp:button>
								                                        <asp:button id="btnZoomOut" runat="server"  Text="Zoom Out" CssClass="button"></asp:button>
								                                        <asp:button id="btnFitToPage" runat="server" Text="Fit To Page"  CssClass="button" Width="72px"></asp:button>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                        <td class="textbold">Sequence  Number</td>
                                                                        <td style="width: 170px"> <asp:TextBox ID="txtSqenceNumber" runat="server" CssClass="textboxgrey" Width="136px" ReadOnly="True"></asp:TextBox>   </td>
                                                                        <td class="textbold">File Number</td>
                                                                     <td style="width: 150px">  <asp:TextBox ID="txtFileNo" runat="server" CssClass="textboxgrey" ReadOnly="True" Width="136px"></asp:TextBox>    </td>                                                                        
                                                                       <td colspan="2" nowrap="nowrap" >
								                                        <asp:button id="btnSaveAs" runat="server" Text="SaveAs"  CssClass="button" ></asp:button>
								                                        <asp:button id="btnPrint" runat="server"  Text="Print" CssClass="button"></asp:button>
								                                        <asp:button id="btnViewAll" runat="server" Text="ViewAll"  CssClass="button" Width="72px"></asp:button>
								                                        </td>
								                                </tr>  
                                                          </table>
                                                       </td>  
                                                       </tr>
                                                      <tr>
                                                         <td width="100%">      
                                                           <table width="100%">
                    											    <asp:HiddenField ID="hdContentType" runat="server"  /> 
                                                           <tr> <td>  </td>  </tr>
											                    <tr >                    											
											                    <td colspan="6">
											                    <asp:GridView ID="grdViewDocument" runat="server" AllowPaging="true" PagerSettings-Position=TopAndBottom  AutoGenerateColumns="False" PageSize="1"  width="100%">
											                        <Columns>
                    											       											                    
    											                    <asp:TemplateField>
    											                    <ItemTemplate>
    											                    
    											                    <% If hdContentType.Value.Trim() = "1" Or hdContentType.Value.Trim() = "4" Then%>
    											                   
    											                    <asp:Image ID="imgDocument" Width='<%#imgWidth %>' Height='<%#imgHeight %>' galleryimg="no" runat="server" ImageUrl='<%# GetImageDoc(Eval("ID")) %>' />
    											                    
    											                     <%End If%>
    											                     
    											                   <%If hdContentType.Value.Trim() = "3" Then%>
    											                    
    											                    <iframe id="ifrmPdf" runat="server" width="800px" height="680px" src='<%# GetImageDoc(Eval("ID")) %>' ></iframe>
    											                    <%btnPrint.Enabled = False%>
    											                    <%btnSaveAs.Enabled = False%>
    											                    
    											                     <%End If%>
    											                     
    											                   <%If hdContentType.Value.Trim() = "2" Then%>
    											                    <asp:Panel ID="pnlEmail" runat="server">
    											                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
    											                                <tr>
    											                                        <td> <asp:Label ID="lblTo" runat="server" Text="Mail To:  " Width="100px"></asp:Label>  </td>
    											                                       <td>
    											                                        <asp:TextBox ID="txtTo" runat="server"  ReadOnly="true"  Width="700px" Text='<%#Eval("EmailTo") %>' ></asp:TextBox>
    											                                        </td>
    											                                </tr>
    											                              <%--  <tr>
            											                                <td> <asp:Label ID="lblMailFrom" runat="server" Text="Mail From" Width="100px"></asp:Label> </td>      
            											                                <td> <asp:TextBox ID="txtMailFrom" runat="server"  ReadOnly="true" Width="700px" Text='<%#Eval("EmailFrom") %>'></asp:TextBox>  </td>
    											                                </tr>--%>
    											                                
    											                                <tr>
            											                                <td> <asp:Label ID="lblSubject" runat="server" Text="Subject" Width="100px"></asp:Label> </td>      
            											                                <td><asp:TextBox ID="txtSubject" runat="server"  ReadOnly="true" Width="700px" Text='<%#Eval("EmailSubject") %>' ></asp:TextBox></td>
    											                                </tr>
    											                                
    											                                <tr>
    											                                        <td colspan="2" align ="left"  ><div id="txtMailBody" runat="server"  style ="width:805px; padding-top:7px;"  ><%#Eval("EmailBody") %></div></td>
    						<%--					                                        <asp:TextBox ID="txtMailBody" runat="server"  ReadOnly="true"  TextMode="MultiLine" Width="805px" Height="300px" Text='<%#Eval("EmailBody") %>' ></asp:TextBox>--%>
    											                                        <%--</td>--%>
    											                                </tr>
    											                    </table>
    											                    </asp:Panel>
    											                    <%End If%>
    											                    </ItemTemplate>
    											                    </asp:TemplateField>
    											                    
    										                        </Columns>
											                    </asp:GridView>
											                    <asp:HiddenField ID="hdFileNo" runat="server" />
											                    <asp:HiddenField ID="hdMailTo" runat="server" />
											                    <asp:HiddenField ID="hdMailSubject" runat="server" />
											                    <asp:HiddenField ID="hdMailBody" runat="server" />
											                    
											                    </td>
                    											
											                    </tr> 
											                   
											                   
											                  
											                 </table>
                                                          </td>
                                              
                                                     </tr>
                                                     <tr>
                                                     <td>
                                                     <iframe id="ifrmPrnt" visible="false"   style="vertical-align:top; padding-top:0px;" width="0px" height="0px"   frameborder="0" src="TASR_PrintOrderDoc.aspx"></iframe>
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
