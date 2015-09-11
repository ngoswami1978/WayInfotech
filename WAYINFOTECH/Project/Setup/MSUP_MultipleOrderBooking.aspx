<%@ Page Language="VB" AutoEventWireup="false" CodeFile="MSUP_MultipleOrderBooking.aspx.vb" ValidateRequest="false"
    Inherits="Setup_MSUP_MultipleOrderBooking" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>WAY: Add/Modify Order Booking</title>
    <link href="../CSS/WAY.css" rel="stylesheet" type="text/css" />

    <script src="../JavaScript/WAY.js" type="text/javascript"></script>

     <script language="javascript" type="text/javascript">     
     
    function StyleValidation()
      {
          if(document.getElementById("drpQuality").selectedIndex=='0')
          {
              document.getElementById("lblError").innerHTML="Quality Name Cann't be blank";
              document.getElementById("drpQuality").focus();
              return false;
          }
                    
          if(document.getElementById("drpDesign").selectedIndex=='0')
          {
              document.getElementById("lblError").innerHTML="Design Name Cann't be blank";
              document.getElementById("drpDesign").focus();
              return false;
          }                    
          if(document.getElementById("txtQty").value=='')
          {
              document.getElementById("lblError").innerHTML="Quantity Cann't be blank";
              document.getElementById("txtQty").focus();
              return false;
          }          
     }        
   

        function DeleteFunction(TempRowID)
        {
           
           if (confirm("Are you sure you want to delete?")==true)
           {
                document.getElementById("hdDeleteFlag").value=TempRowID;
                return true;
           }
           else
           {
                document.getElementById("hdDeleteFlag").value="";
                return false;
           }
        }
        
        function EditFunction(TempRowID)
        {               
           document.getElementById("hdEditFlag").value=TempRowID;
        }
      
      
     function CheckMendatoty()
    {
        if (document.getElementById("txtBarCode").value=="")
        {          
                document.getElementById("lblError").innerText="Please enter the BarCode "
                document.getElementById("txtBarCode").focus();
                return false;
        }
        
        if (document.getElementById("txtStyleName").value=="")
        {           
                document.getElementById("lblError").innerText="Please enter the Style "
                document.getElementById("txtStyleName").focus();
                return false;
        }        
        if (document.getElementById("txtDesignName").value=="")
        {           
                document.getElementById("lblError").innerText="Please enter the Style "
                document.getElementById("txtDesignName").focus();
                return false;
        }
    } 
        
    function FillSecurity(strDropDownName)
         {    
            var varStyle;
            var varDesign;
            var varShadeNo;
            
            var StyleId;
            
            //alert (strDropDownName);
            
            document.getElementById('lblError').innerHTML="";
            varStyle=  document.getElementById('drpQuality').value;
            varDesign=  document.getElementById('drpDesign').value;
            varShadeNo=  document.getElementById('drpShadeNo').value;
            
            if (strDropDownName=="drpQuality")
            {
                StyleId=varStyle;
            }
            else if(strDropDownName=="drpDesign")
            {
                StyleId=varDesign;
            }
            else if(strDropDownName=="drpShadeNo")
            {
                StyleId=varShadeNo;
            }
            if ((varStyle!="") || (varDesign!="") || (varShadeNo!=""))
            {
                CallServerSearch(StyleId+'|OF',"This is context from client"); // Server view function
            }
           return false;
           }
           
    function ReceiveServerData(args, context)
       {   
           document.getElementById('lblError').innerText ="";
           var obj = new ActiveXObject("MsXml2.DOMDocument");
           var dsRoot=obj.documentElement;           
           
           if (args =="")
           { 
                document.getElementById('lblError').innerHTML="No Record Found";                   
           }      
          else
          {
               var parts = args.split("|");
               //alert(parts[0]);
               
               if (parseInt(parseFloat(parts[0])) == 0) { return false; }
                              
               document.getElementById('drpQuality').value=parts[0];
               document.getElementById('drpDesign').value=parts[0];
               document.getElementById('drpShadeNo').value=parts[0];
               document.getElementById('txtMrp').value=parts[1];                             
               document.getElementById('hdRunTimeQty').value=parts[1];
               
//               document.getElementById('lblError').innerHTML="";
//               var listItem;               
//               var subParts;
//               document.getElementById('drpQuality').options.length=0;
//               var ddlSecurityName = document.getElementById('drpQuality');
//                               
//                for (var i=1; i < parts.length;++i)
//                {
//                     subParts=parts[i].split("|")
//                     alert(subParts[0] +":" +subParts[1]);
//                     listItem = new Option(subParts[1],subParts[0]);
//                     drpQuality.options[i]=listItem;
//                }            
              } 
          return false;               
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <table width="860px" align="left" class="border_rightred" style="height: 486px;">
            <tr>
                <td valign="top">
                    <table width="100%" align="left">
                        <tr>
                            <td valign="top" align="left">
                                <span class="menu">Setup-></span><span class="sub_menu">Manage Order Booking</span>
                            </td>
                        </tr>
                        <tr>
                            <td class="heading" align="center" valign="top">
                                Manage Order Booking</td>
                        </tr>
                        <tr>
                            <td valign="top">
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td align="LEFT" class="redborder">
                                            <table width="100%" border="0" class="left" cellpadding="2" cellspacing="0">
                                                <tr>
                                                    <td class="textbold" style="width: 90%">
                                                        <table width="800px" border="0" class="left" cellpadding="2" cellspacing="0">
                                                            <tr>
                                                                <td class="textbold" style="width: 100px">
                                                                </td>
                                                                <td class="textbold" style="width: 130px">
                                                                </td>
                                                                <td colspan="3"  class="center TOP"   >
                                                                
                                                    <asp:Label ID="lblError" CssClass="ErrorMsg" runat="server"></asp:Label>
                                                        
                                                                </td>
                                                                <td style="width: 200px; height: 2px">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="textbold" style="width: 100px; ">
                                                                </td>
                                                                <td class="textbold" style="width: 130px; ">
                                                                    Order Number</td>
                                                                <td style="width: 258px">
                                                                    <asp:TextBox ID="txtOrderNo" runat="server" CssClass="textboxgrey" MaxLength="10" ReadOnly="True"
                                                                        TabIndex="1" Width="70px"></asp:TextBox></td>
                                                                <td class="textbold" >
                                                                </td>
                                                                <td style="width: 330px">
                                                                </td>
                                                                <td style="width: 200px">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="textbold" style="width: 100px">
                                                                </td>
                                                                <td class="textbold" style="width: 130px">
                                                                    Quality<span class="Mandatory">*</span>
                                                                </td>
                                                                <td style="width: 258px">
                                                                    <asp:DropDownList ID="drpQuality" runat="server" CssClass="dropdownlist" onkeyup="gotop(this.id)"
                                                                        Style="left: 0px; position: relative; top: 0px" TabIndex="1" Width="160px">
                                                                    </asp:DropDownList></td>
                                                                <td class="textbold" >
                                                                    Design <span class="Mandatory">*</span></td>
                                                                <td style="width: 330px">
                                                                    <asp:DropDownList ID="drpDesign" runat="server" CssClass="dropdownlist" onkeyup="gotop(this.id)"
                                                                        Style="left: 0px; position: relative; top: 0px" TabIndex="1" Width="160px">
                                                                    </asp:DropDownList></td>
                                                                <td style="width: 200px">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="textbold" style="width: 100px; height: 23px;">
                                                                </td>
                                                                <td class="textbold" style="height: 23px">
                                                                    Shade No &nbsp;<span class="Mandatory">*</span></td>
                                                                <td style="width: 258px; height: 23px;">
                                                                    <asp:DropDownList ID="drpShadeNo" runat="server" CssClass="dropdownlist" onkeyup="gotop(this.id)"
                                                                        Style="left: 0px; position: relative; top: 0px" TabIndex="1" Width="160px">
                                                                    </asp:DropDownList></td>
                                                                <td class="textbold" style="width: 80px; height: 23px;">
                                                                    MRP</td>
                                                                <td style="height: 23px">
                                                                    <asp:TextBox ID="txtMrp" runat="server" CssClass="textboxgrey" TabIndex="1" Width="59px"
                                                                        MaxLength="10" ReadOnly="True"></asp:TextBox></td>
                                                                <td style="height: 23px">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="textbold" style="width: 100px">
                                                                </td>
                                                                <td class="textbold">
                                                                    QTY<span class="Mandatory">*</span></td>
                                                                <td style="width: 258px">
                                                                    <asp:TextBox ID="txtQty" runat="server" CssClass="textbox" TabIndex="1" Width="59px"
                                                                        MaxLength="10"></asp:TextBox></td>
                                                                <td class="textbold" nowrap="nowrap">
                                                                </td>
                                                                <td>
                                                                    <asp:Button ID="btnAdd" runat="server" CssClass="button" Text="Add" Width="100px"
                                                                        TabIndex="1" AccessKey="S" /></td>
                                                            </tr>
                                                            <tr>
                                                                <td class="textbold" style="width: 100px">
                                                                </td>
                                                                <td class="textbold">
                                                                    </td>
                                                                <td style="width: 258px">
                                                                    </td>
                                                                <td class="textbold" nowrap="nowrap">
                                                                </td>
                                                                <td>
                                                                </td>
                                                            </tr>
                                                            <tr height="20px">
                                                            </tr>
                                                            <tr>
                                                                <td colspan="6" nowrap="nowrap" style="width: 100px">
                                                                    <asp:GridView ID="grdvProduct" AllowSorting="true" HeaderStyle-ForeColor="white"
                                                                        runat="server" AutoGenerateColumns="False" HorizontalAlign="Left" HeaderStyle-CssClass="Gridheading"
                                                                        RowStyle-CssClass="ItemColor" AlternatingRowStyle-CssClass="lightblue" Width="790px"
                                                                        TabIndex="2">
                                                                        <Columns>
                                                                            <asp:BoundField HeaderText="w_styleID" DataField="w_styleID" SortExpression="w_styleID"
                                                                                Visible="false" />
                                                                            <asp:BoundField HeaderText="Quality" DataField="StyleName" SortExpression="Quality"
                                                                                ItemStyle-Width="250px" />
                                                                            <asp:BoundField HeaderText="Design" DataField="DesignNo" SortExpression="Design" ItemStyle-Width="250px" />
                                                                            <asp:BoundField HeaderText="ShadeNo " DataField="ShadeNo" SortExpression="ShadeNo"
                                                                                ItemStyle-Width="200px" />
                                                                            <asp:BoundField HeaderText="MRP " DataField="MRP" SortExpression="MRP" ItemStyle-Width="150px" />
                                                                            <%--<asp:BoundField HeaderText="QTY " DataField="qty" SortExpression="QTY" ItemStyle-Width="50px" />--%>
                                                                            <asp:TemplateField HeaderText="Quantity" SortExpression="Qty" HeaderStyle-Wrap="false">
                                                                                         <ItemTemplate>
                                                                                             <asp:Label ID="lblQty" runat="server" Text='<%# Eval("qty") %>'></asp:Label>
                                                                                         </ItemTemplate>
                                                                                         <HeaderStyle Wrap="False" />
                                                                            </asp:TemplateField>
                                                                                     
                                                                            <asp:TemplateField HeaderText="Action">
                                                                                <ItemTemplate>
                                                                                    <asp:LinkButton ID="linkEdit" runat="server" CssClass="LinkButtons" Text="Edit"></asp:LinkButton>&nbsp;
                                                                                    <asp:LinkButton ID="linkDelete" runat="server" CssClass="LinkButtons" Text="Delete"></asp:LinkButton>
                                                                                    <asp:HiddenField ID="hdStyleId" runat="server" Value='<%#Eval("w_styleID")%>' />
                                                                                    <asp:HiddenField ID="hdRowID" runat="server" Value='<%#Eval("TempRow")%>' />
                                                                                </ItemTemplate>
                                                                                <ItemStyle Wrap="False" />
                                                                            </asp:TemplateField>
                                                                        </Columns>
                                                                        <AlternatingRowStyle CssClass="lightblue" />
                                                                        <HeaderStyle CssClass="Gridheading" HorizontalAlign="Left" Wrap="False" />
                                                                        <RowStyle CssClass="textbold" />
                                                                    </asp:GridView>
                                                                </td>
                                                                <td colspan="1" nowrap="nowrap">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="textbold" style="width: 100px">
                                                                </td>
                                                                <td class="textbold" style="width: 136px">
                                                                </td>
                                                                <td style="width: 258px">
                                                                </td>
                                                                <td class="textbold" style="width: 92px">
                                                                </td>
                                                                <td>                                                                    
                                                                </td>
                                                                <td>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="textbold" style="width: 100px">
                                                                </td>
                                                                <td class="subheading" colspan="4">
                                                                        Order Details</td>
                                                                <td>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="textbold" style="width: 100px">
                                                                    </td>
                                                                <td class="textbold" style="width: 136px">
                                                                    TotQty</td>
                                                                <td style="width: 258px">
                                                                    <asp:TextBox ID="txtTotQty" runat="server" CssClass="textboxgrey" MaxLength="10" ReadOnly="True"
                                                                        TabIndex="2" Width="59px"></asp:TextBox></td>
                                                                <td class="textbold" style="width: 92px">
                                                                    Created By</td>
                                                                <td>
                                                                    <asp:TextBox ID="txtCreatedBy" runat="server" CssClass="textboxgrey" MaxLength="100"
                                                                        ReadOnly="True" TabIndex="1" Width="170px"></asp:TextBox></td>
                                                                <td>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="textbold" style="width: 100px">
                                                                </td>
                                                                <td class="textbold" style="width: 136px">
                                                                    Order Date</td>
                                                                <td style="width: 258px">
                                                                    <asp:TextBox ID="txtOrderDate" runat="server" CssClass="textboxgrey" MaxLength="20"
                                                                        ReadOnly="True" TabIndex="2" Width="100px"></asp:TextBox></td>
                                                                <td class="textbold" style="width: 92px">
                                                                </td>
                                                                <td>
                                                                </td>
                                                                <td>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="textbold" style="width: 100px">
                                                                </td>
                                                                <td class="textbold" style="width: 136px">                                                                        
                                                                        <asp:HiddenField ID="hdLogedByName" runat="server" />
                                                                        <asp:HiddenField ID="hdEmpID" runat="server" />
                                                                        <asp:HiddenField ID="hdDeleteFlag" runat="server" />
                                                                        <asp:HiddenField ID="hdTempXml" runat="server" />
                                                                        <asp:HiddenField ID="hdEditFlag" runat="server" />
                                                                        <asp:HiddenField ID="hdReadonlyFlag" runat="server" Value="1" />
                                                                        <asp:HiddenField ID="hdStyleUniqe" runat="server" />
                                                                        <asp:HiddenField ID="hdCreatedby" runat="server" />
                                                                        <asp:HiddenField ID="hdRunTimeQty" runat="server" />                                                                        
                                                                        </td>
                                                                        
                                                                <td style="width: 258px">
                                                                </td>
                                                                <td class="textbold" style="width: 92px">
                                                                </td>
                                                                <td>
                                                                    <asp:HiddenField ID="hdStyleID" runat="server" />
                                                                </td>
                                                                <td>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td class="center top " colspan="2" rowspan="1">
                                                        <table cellpadding="0" cellspacing="0" border="0">
                                                            <tr>
                                                                <td>
                                                                    <asp:Button ID="btnSave" runat="server" CssClass="button" Text="Save" Width="100px"
                                                                        TabIndex="1" AccessKey="S" />
                                                                </td>
                                                            </tr>
                                                            <tr height="3px">
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:Button ID="btnNew" runat="server" CssClass="button" Text="New" Width="100px"
                                                                        TabIndex="1" AccessKey="N" />
                                                                </td>
                                                            </tr>
                                                            <tr height="3px">
                                                            </tr>
                                                            <tr>
                                                                <td style="height: 22px">
                                                                    <asp:Button ID="btnReset" runat="server" CssClass="button" Text="Reset" Width="100px"
                                                                        TabIndex="1" AccessKey="R" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="ErrorMsg" style="width: 10%">
                                                        Field Marked * are Mandatory</td>
                                                    <td>
                                                        &nbsp; &nbsp;
                                                    </td>
                                                    <td>
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
            <tr>
                <td>
                    <asp:TextBox ID="txtRecordOnCurrentPage" runat="server" Width="73px" CssClass="textboxgrey"
                        Visible="false"></asp:TextBox>
                    <input type="hidden" id="hdCtrlId" runat="server" />
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
