<%@ Page Language="VB" ValidateRequest="false" EnableEventValidation="false"  AutoEventWireup="false" CodeFile="TASR_PrintOrderDoc.aspx.vb" Inherits="TravelAgency_TASR_PrintOrderDoc" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body style="padding:0px 0px 0px 0px;margin:0px 0px 0px 0px">
    <form id="form1" runat="server">
   <asp:Image ID="imgOrderDocPrnt" Width="670px" Height="820px" runat="server"/>
   <asp:Panel ID="pnlEmail" Visible="false" runat ="server">
        <asp:Panel ID="Panel1" runat="server">
    											                    <table width="800px" border="0" cellspacing="0" cellpadding="0">
    											                                <tr>
    											                                        <td style="vertical-align:top"> <asp:Label ID="lblTo" runat="server" Text="Mail To:  " Width="80px"></asp:Label>  </td>
    											                                       <td style="width:552px;word-wrap:break-word">
                                                                                           &nbsp;<asp:Literal ID="lblMailTo" runat="server" Text="Label" ></asp:Literal></td>
    											                                </tr>
    											                             <%--   <tr>
            											                                <td> <asp:Label ID="lblMailFrom" runat="server" Text="Mail From" Width="100px"></asp:Label> </td>      
            											                                <td> <asp:TextBox ID="txtMailFrom" runat="server"  ReadOnly="true" Width="700px"></asp:TextBox>  </td>
    											                                </tr>--%>
    											                                
    											                                <tr>
            											                                <td> <asp:Label ID="lblSubject" runat="server" Text="Subject" Width="80px"></asp:Label> </td>      
            											                                <td> <asp:TextBox ID="txtSubject" runat="server"  ReadOnly="true" Width="552px"  ></asp:TextBox>  </td>
    											                                </tr>
    											                                
    											                                <tr align="left" >
    											                                        <td colspan="2" >
    											                                         <div id="txtMailBody" runat="server"  style ="width:700px;"   ></div>
    											                                       <%-- <asp:TextBox ID="txtMailBody" runat="server"  ReadOnly="true"  TextMode="MultiLine" Width="805px" Height="300px"></asp:TextBox>--%>
    											                                        </td>
    											                                </tr>
    											                    </table>
    											                    </asp:Panel>
   </asp:Panel> 
    </form>
</body>
</html>
