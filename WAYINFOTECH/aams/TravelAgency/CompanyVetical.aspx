<%@ Page Language="VB" AutoEventWireup="false" CodeFile="CompanyVetical.aspx.vb"
    Inherits="TravelAgency_CompanyVetical" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>AAMS:Change Company Vertical For Group</title>
    <link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" language="javascript">        
        function SetValue()
        {
         if (document.getElementById ('RdAmadeus').checked==false && document.getElementById ('RdResBird').checked==false )
          {
			    document.getElementById ('lblError').innerHTML='At least one item is mandatory.';
			    return false;
		  }
		   if (document.getElementById ('RdAmadeus').checked==true )
          {
			  window.returnValue ="1";
		  }
        
          if (document.getElementById ('RdAmadeus').checked==true )
          {
			  window.returnValue ="1";
		  }
		  else
		  {
		      window.returnValue ="2";
		  }			  
			window.close();
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <table width="500px" cellpadding="0" cellspacing="0">
            <tr>
                <td valign="top">
                    <table width="100%" cellpadding="0" cellspacing="0">
                        <tr>
                            <td class="heading" align="center" valign="top" style="height: 20px">
                                Comapny Vertical
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table class="redborder">
                                    <tr>
                                        <td align="center">
                                            <asp:Label ID="lblError" runat="server" CssClass="ErrorMsg"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center">
                                            <b>Location is currently available against Non 1A Vertical. Assign other Vertical for
                                                this location?</b>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" class="textbold">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center">
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center">
                                            Select Comapny Vertical&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:RadioButton
                                                ID="RdAmadeus" Text="Amadeus" runat="server"  GroupName ="CV" />&nbsp;&nbsp;
                                                <asp:RadioButton ID="RdResBird" Text="ResBird" runat="server" GroupName ="CV" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center">
                                            <input type="button" id="BtnSelect" runat="server" class="button" value="Select"
                                                onclick="return SetValue();" /></td>
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
