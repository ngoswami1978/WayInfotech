<%@ Page Language="VB" AutoEventWireup="false" CodeFile="INVUP_Supplier.aspx.vb" Inherits="Inventory_INVUP_Supplier" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title> Add/Modify Supplier </title>
    <link href="../CSS/AAMS.css" type="text/css" rel="stylesheet"/>
      <script src="../JavaScript/AAMS.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
     function  NewFunction()
       {    
           window.location="INVUP_Supplier.aspx?Action=I";
           return false;
       }  
    
     function CheckMandatoty()
    {
        if (document.getElementById("txtName").value.trim()=="")
        {          
             document.getElementById("lblError").innerHTML="Supplier Name is mandatory.";
             document.getElementById("txtName").focus();
             return false;
        }        
         if (document.getElementById("txtConTitle").value.trim()=="")
        {           
          document.getElementById("lblError").innerHTML="Contact Title is mandatory.";
          document.getElementById("txtConTitle").focus();
          return false;
        }   
        if (document.getElementById("txtConName").value.trim()=="")
        {           
          document.getElementById("lblError").innerHTML="Contact name is mandatory.";
          document.getElementById("txtConName").focus();
          return false;
        }
          if (document.getElementById("txtAddress").value.trim()=="")
        {           
          document.getElementById("lblError").innerHTML="Address is mandatory.";
          document.getElementById("txtAddress").focus();
          return false;
        }
        if (document.getElementById("drpCity").selectedIndex==0)
        {           
          document.getElementById("lblError").innerHTML="City is mandatory.";
          document.getElementById("drpCity").focus();
          return false;
        }
           if (document.getElementById("txtPostalCode").value.trim()=="")
        {           
          document.getElementById("lblError").innerHTML="Postal Code is mandatory.";
          document.getElementById("txtPostalCode").focus();
          return false;
        }
        if (document.getElementById("drpCountry").selectedIndex==0)
        {           
          document.getElementById("lblError").innerHTML="Country is mandatory.";
          document.getElementById("drpCountry").focus();
          return false;
        }
        if (document.getElementById("txtPhone").value.trim()=="")
        {           
          document.getElementById("lblError").innerHTML="Phone No is mandatory.";
          document.getElementById("txtPhone").focus();
          return false;
        }
         if(document.getElementById("txtEmail").value!='')
        {              
            if(checkEmail(document.getElementById("txtEmail").value)==false)
            {
                document.getElementById("lblError").innerHTML='Email is not valid.';
                document.getElementById("txtEmail").focus();
                return false;
            }
        }
          if (document.getElementById("txtPayTems").value.trim().length>300)
        {
             document.getElementById("lblError").innerHTML="Payment Terms can't be greater than 300 characters."
             document.getElementById("txtPayTems").focus();
             return false;
        }  
        
      
         if (document.getElementById("txtNotes").value.trim().length>300)
        {
             document.getElementById("lblError").innerHTML="Notes can't be greater than 300 characters."
             document.getElementById("txtNotes").focus();
             return false;
        }  
       
    
    }
    
    function PopupTestForSupplier()
{

          var type;        
            type = "../Inventory/INVSR_Supplier.aspx?Popup=T" ;
   	        window.open(type,"aa","height=600,width=920,top=30,left=20,scrollbars=1,status=1");	
            return false;

}
    </script>
</head>
<body>
    <form id="form1" runat="server" defaultfocus="txtName">
    
    <table width="100%" align="left" height="486px" class="border_rightred">
            <tr>
                <td valign="top">
                    <table width="100%" align="left">
                        <tr>
                            <td valign="top"  align="left" >
                            <span class="menu">Inventory-&gt;</span><span class="sub_menu">Supplier</span>
                            </td>
                        </tr>
                        <tr>
                            <td class="heading" align="center" valign="top" >
                                Manage Supplier
                            </td>
                        </tr>
                          <tr>
                            <td valign="top" style="height: 366px">
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td class="redborder center">
                                    <table border="0" cellpadding="2" cellspacing="1" style="width: 100%" class="left">
                                        <tr>
                                           
                                            <td class="gap" colspan="7" style="text-align: center">
                                                    <asp:Label ID="lblError" runat="server" CssClass="ErrorMsg" EnableViewState="False"></asp:Label></td>
                                           </tr>                
                                            <tr>
                                                <td style="width:18%">
                                                </td>
                                                <td class="textbold" style="width:12%" >
                                                    Supplier Name<span class="Mandatory" >*</span></td>
                                                <td style="width: 43%" colspan="4">
                                                    <asp:TextBox ID="txtName" runat="server" CssClass="textbox" MaxLength="50" TabIndex="1" Width="385px"></asp:TextBox></td>   
                                                <td style="width: 27%" align ="center" >
                                                      <asp:Button ID="btnSave" CssClass="button" runat="server" Text="Save" TabIndex="15" AccessKey="S" /></td>
                                            </tr>
                                             <tr>
                                                <td style="width:18%">
                                                </td>
                                                <td class="textbold" style="width:15%">
                                                   Contact Name <span class="Mandatory" >*</span></td>
                                                <td style="width: 40%" colspan ="4">
                                                  <asp:TextBox ID="txtConTitle" runat="server" CssClass="textbox" MaxLength="5" TabIndex="2"
                                                        Width="40px"></asp:TextBox>&nbsp;&nbsp;<asp:TextBox id="txtConName" tabIndex=3 runat="server" CssClass="textbox" MaxLength="50" Width="330px"></asp:TextBox></td>
                                                    
                                                <td style="width: 27%" valign ="top" align ="center">
                                                    <asp:Button ID="btnNew" runat="server" CssClass="button" Text="New" TabIndex="16"  AccessKey="N"/></td>
                                            </tr>
                                              <tr>
                                                <td style="width:18%">
                                                </td>
                                                <td class="textbold" style="width:15%">
                                                    Address<span class="Mandatory" >*</span></td>
                                                <td style="width: 40%" colspan ="4">
                                                    <asp:TextBox ID="txtAddress" runat="server" CssClass="textbox" MaxLength="300" TabIndex="4" Width="383px" TextMode="MultiLine" Height="33px"></asp:TextBox>&nbsp;&nbsp;
                                                </td>
                                                <td style="width: 27%" valign ="top" align ="center">
                                                      <asp:Button ID="btnReset" CssClass="button" runat="server" Text="Reset" TabIndex="17"  AccessKey="R"/></td>
                                            </tr>
                                                <tr>
                                                <td style="width:18%">
                                                </td>
                                                <td class="textbold" style="width:12%">
                                                    City<span class="Mandatory" >*</span></td>
                                                <td style="width: 15%"><asp:DropDownList onkeyup="gotop(this.id)" ID="drpCity" CssClass="dropdown" runat="server" Width="128px" TabIndex="5" >
                                                </asp:DropDownList></td>
                                                 <td style="width: 1%">                                                                             
                                                </td>                                                                            
                                                 <td style="width: 8%" nowarp="true"  class="textbold">
                                                     Postal Code<span class="Mandatory" >*</span>
                                                </td>
                                                 <td style="width: 15%">
                                                     <asp:TextBox ID="txtPostalCode" runat="server" CssClass="textbox" MaxLength="10" TabIndex="6"
                                                         Width="122px"></asp:TextBox>&nbsp;</td>    
                                                <td style="width: 27%">
                                                      </td>
                                            </tr>
                                                <tr>
                                                <td style="width:18%">
                                                </td>
                                                <td class="textbold" style="width:12%" >
                                                    Country<span class="Mandatory" >*</span></td>
                                                <td style="width: 15%"><asp:DropDownList onkeyup="gotop(this.id)" ID="drpCountry"  CssClass="dropdown" runat="server" Width="128px" TabIndex="7" >
                                                </asp:DropDownList></td>
                                                 <td style="width: 1%">                                                                             
                                                </td>                                                                            
                                                 <td class="textbold">
                                                     State</td>
                                                 <td style="width: 15%">
                                                    <asp:TextBox ID="txtState" runat="server" CssClass="textbox" MaxLength="25" TabIndex="8" Width="122px"></asp:TextBox></td>    
                                                <td style="width: 27%">
                                                    </td>
                                            </tr>
                                             <tr>
                                                <td style="width:18%">
                                                </td>
                                                <td class="textbold" style="width:12%">
                                                    Phone No<span class="Mandatory" >*</span></td>
                                                <td style="width: 15%">
                                                    <asp:TextBox ID="txtPhone" runat="server" CssClass="textbox" MaxLength="30" TabIndex="9" Width="122px"></asp:TextBox></td>
                                                 <td style="width: 1%">                                                                             
                                                </td>                                                                            
                                                 <td class="textbold">
                                                   Fax No</td>
                                                 <td style="width: 15%">
                                                  <asp:TextBox id="txtFax" tabIndex=10 runat="server" CssClass="textbox" MaxLength="30" Width="122px"></asp:TextBox>
                                                </td>    
                                                <td style="width: 27%">
                                                      </td>
                                            </tr>
                                             <tr>
                                                <td style="width:18%">
                                                </td>
                                                <td class="textbold" style="width:12%" >
                                                    E- mail</td>
                                                <td style="width: 43%" colspan="4">
                                                     <asp:TextBox ID="txtEmail" runat="server" CssClass="textbox" MaxLength="50" TabIndex="11" Width="385px"></asp:TextBox></td>   
                                                <td style="width: 27%" align ="center" >
                                                      </td>
                                            </tr>
                                             <tr>
                                                <td style="width:18%">
                                                </td>
                                                <td class="textbold" style="width:12%" >
                                                   Web site</td>
                                                <td style="width: 43%" colspan="4">
                                                      <asp:TextBox id="txtWeb" tabIndex=12 runat="server" CssClass="textbox" MaxLength="50" Width="385px"></asp:TextBox></td>   
                                                <td style="width: 27%" align ="center" >
                                                      </td>
                                            </tr>
                                             <tr>
                                                <td style="width:18%" valign ="top">
                                                </td>
                                                <td class="textbold" style="width:12%"  valign ="top">
                                                    Payment Terms</td>
                                                <td style="width: 43%"  colspan="4" valign ="top">
                                                    <asp:TextBox ID="txtPayTems" runat="server" CssClass="textbox" MaxLength="300" TabIndex="13" Width="383px" Height="42px" TextMode="MultiLine"></asp:TextBox></td>
                                                 
                                                <td style="width: 27%">
                                                     </td>
                                            </tr>
                                             <tr>
                                                <td style="width:18%" valign ="top">
                                                </td>
                                                <td class="textbold" style="width:12%"  valign ="top">
                                                     Notes</td>
                                                <td style="width: 43%"  colspan="4" valign ="top" >
                                                    <asp:TextBox id="txtNotes" tabIndex=14 runat="server" CssClass="textbox" MaxLength="25" Width="383px" Height="42px" TextMode="MultiLine"></asp:TextBox></td> 
                                               
                                                <td style="width: 27%">
                                                     </td>
                                            </tr>
                                        <tr>
                                            <td>
                                            </td>
                                            <td colspan="5" class="ErrorMsg">
                                                Field Marked * are Mandatory</td>
                                            <td >
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                            </td>
                                            <td  colspan ="5">
                                                &nbsp;</td>
                                            <td >
                                            </td>
                                        </tr>
                                                     
                                                                      
                                                                        <tr>
                                                                            <td colspan="7" style="text-align: center; height: 23px;"><%--<img src="../Images/lookup.gif" onclick="javascript:return PopupTestForSupplier();" id="IMG1" />--%>
                                                                                <asp:HiddenField ID="hdID" runat="server" />
                                                                                &nbsp;</td>
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
