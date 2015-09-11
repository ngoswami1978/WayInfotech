<%@ Page Language="VB" AutoEventWireup="false" CodeFile="INVSR_Products.aspx.vb" Inherits="INVSR_Products" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>AAMS: Products</title>
    <link href="../CSS/AAMS.css" rel="stylesheet" type="text/css" />
    <script language ="javascript" type ="text/javascript" >
   
  function fnProductID()
    {   
    /* if (document.getElementById("hdPageHD_RE_ID").value != "")
    {
         if (window.opener.document.forms['form1']['hdEHD_RE_ID']!=null)
        { 
        var ind =document.getElementById("ddlQueryStatus").selectedIndex;
        if (ind ==0)
        {
        document.getElementById("lblError").innerText="Query Status is Mandatory";
        return false;
        }
        else
        {
        var text1=document.getElementById("ddlQueryStatus").options[ind].text;
        window.opener.document.forms['form1']['hdEHD_RE_ID'].value=document.getElementById("hdPageHD_RE_ID").value;
        window.opener.document.forms['form1']['hdPopupStatus'].value=document.getElementById("hdQueryStatus").value;
        window.opener.document.forms['form1'].submit();*/
        window.close();
        return false;
      //  }
      //  }    
    }
    </script>
    <script type="text/javascript" src="../JavaScript/DisableRightClick.js"></script>
</head>
<body>
    <form id="form1" runat="server"  defaultbutton ="btnSelect" defaultfocus ="btnSelect" >
        <table width="860px" align="left" class="border_rightred" style="height:486px;">
            <tr>
                <td valign="top">
                    <table width="100%" align="left">
                        <tr>
                            <td valign="top"  align="left" >
                            <span class="menu">Inventory-&gt;</span><span class="sub_menu">Product List</span></td>
                            <td class="right" style="width:20%"><asp:LinkButton ID="lnkClose"  CssClass="LinkButtons" runat="server" OnClientClick="return fnProductID()" >Close</asp:LinkButton> &nbsp; &nbsp;&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="heading" align="center" valign="top" colspan="2">
                                Product List</td>
                                
                        </tr>
                        <tr>
                            <td valign="top" colspan="2">
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td align="LEFT" class="redborder">
                                 
                                                        <table width="100%" border="0" align="left" cellpadding="0" cellspacing="0" >
                                                            <tr>
                                                                <td  class="textbold" colspan="6" align="center" >
                                                                <asp:Label ID="lblError" runat="server" CssClass="ErrorMsg" ></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="textbold" style="height: 23px">
                                                                    &nbsp;</td>
                                                               <td colspan="2" class="ErrorMsg">
                                                                   &nbsp;
                                                                    </td>
                                                                <td style="height: 23px">
                                                                    &nbsp;</td>
                                                                <td style="height: 23px">
                                                                    &nbsp;</td>
                                                                <td style="height: 23px">
                                                                    &nbsp;</td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="6" >
                                                                <asp:Button ID="btnSelect" runat="server" CssClass="button" Text="Select" OnClientClick="return ReturnData()" /></td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="6" >
                                                                 <asp:GridView EnableViewState="False" ID="gvProducts" runat="server"  AutoGenerateColumns="False" TabIndex="6" Width="100%">
                                                                    <Columns>
                                                                        <asp:TemplateField>
                                                                        <HeaderTemplate>
                                                                           <input type="checkbox" id="chkAllSelect" name="chkAllSelect" onclick="SelectAll();" /> 
                                                                        </HeaderTemplate>
                                                                            <ItemTemplate>                                                                            
                                                                             <input type="checkbox" id="chkSelect" name="chkSelect" runat="server"  /> 
                                                                             <asp:HiddenField ID="hdDataID" runat="server" Value='<% #Container.DataItem("PRODUCTID") + "|" + Container.DataItem("PRODUCTNAME") + "|" + Container.DataItem("SERIALNUMBER") + "|" + Container.DataItem("VENDORSR_NUMBER") %> ' />   
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                         <asp:BoundField DataField="PRODUCTNAME" HeaderText="Product Name" />
                                                                         <asp:BoundField DataField="SERIALNUMBER" HeaderText="Amadeus Serial" />
                                                                         <asp:BoundField DataField="VENDORSR_NUMBER" HeaderText="Vender Serial" />
                                                                         </Columns>
                                                    <AlternatingRowStyle CssClass="lightblue" />
                                                    <RowStyle CssClass="textbold" />
                                                    <HeaderStyle CssClass="Gridheading" />                                                    
                                                 </asp:GridView>
                                                                   </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="6" style="width: 100%">
                                                                    <input id="hdprodID" style="width: 1px" type="hidden" runat="server" />
                                                                     <input id="hdData" style="width: 1px" type="hidden" runat="server" />
                                                                    <input id="hdgodownID" style="width: 1px" type="hidden" runat="server" />
                                                                    <input id="hdchallanType" style="width: 1px" type="hidden" runat="server" />
                                                                    <input id="hdLCODE" style="width: 1px" type="hidden" runat="server" />
                                                                    <input id="hdIssChallanNo" style="width: 1px" type="hidden" runat="server" />
                                                                     
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="6"  class="right">
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
    <script language ="javascript" type="text/javascript">
   // Function for checking all check boxes.
  
    function SelectAll() 
    {
       CheckAllDataGridCheckBoxes(document.forms[0].chkAllSelect.checked)
    }
    function CheckAllDataGridCheckBoxes(value) 
    {
        for(i=0;i<document.forms[0].elements.length;i++) 
        {
        var elm = document.forms[0].elements[i]; 
            if(elm.type == 'checkbox') 
            {
              elm.checked = value
            }
        }
    }
    
	function ReturnData()
       {
       
           for(i=0;i<document.forms[0].elements.length;i++) 
            {
            var elm = document.forms[0].elements[i]; 
                    if(elm.type == 'checkbox') 
                    {
                         if (elm.checked == true && elm.id != "chkAllSelect")
                         {
                            var chkname=elm.id;
                            var gvname=chkname.split("_")[0];
                            var ctrlidname=chkname.split("_")[1];
                             if (document.getElementById("hdData").value == "")
                             {
                                document.getElementById("hdData").value =document.getElementById(gvname + "_" + ctrlidname + "_" + "hdDataID").value;
                             }
                             else
                             {
                                document.getElementById("hdData").value = document.getElementById("hdData").value + "," + document.getElementById(gvname + "_" + ctrlidname + "_" + "hdDataID").value;
                             }
                         
                         }
                      
                    }
            }
        
            var data= document.getElementById("hdData").value;
            if(data=="")
            {
                document.getElementById("lblError").innerText="Checked atleast one checkbox";
                return false;            
            }
           else
           {
                 if (window.opener.document.forms['form1']['hdProductListPopUpPage']!=null)
                 {
                    if (window.opener.document.forms['form1']['hdProductListPopUpPage'].value=="")
                    {
                        window.opener.document.forms['form1']['hdProductListPopUpPage'].value=data;
                    }
                    else
                    {
                        window.opener.document.forms['form1']['hdProductListPopUpPage'].value=window.opener.document.forms['form1']['hdProductListPopUpPage'].value + "," + data;
                    }
                    window.opener.document.forms(0).submit();
                    window.close();
                    return false;
                 }
           }
       }   
				
    
    </script>
</body>
</html>
