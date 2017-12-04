<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="sq_add.aspx.cs" Inherits="djdc_wage.sq_add" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>丹江口电厂工资查询系统</title>
<link href="cssallfor/wap_global.css" type="text/css" rel="stylesheet">



</head>
<body>
    <form id="form1" runat="server">
    <div class="main">


<div class="con">
<!--公司简介-->
<div class="mainBoxs" style="margin: auto;">
<%--  <div class="titles">
<h2 style="text-align:center;"><s><img src="images/wap/i3.png" width="26" height="24"></s><a href="">提交加入申请</a></h2>
  </div>--%>

      <table width="100%" align="center" bgcolor="#f3a622" border="0" 
            cellspacing="1" cellpadding="0">
      <tbody>
      <tr>
        <td height="60" align="center"><b><font 
                  color="#990000">提交加入申请</font></b></td>
      </tr>
    </tbody>
  </table>

    <table width="100%" align="center" bgcolor="#dde5e7" 
            border="0" cellspacing="1" cellpadding="0">
    <tbody>
      <tr>
        <td width="100%" height="25" align="left" 
                bgcolor="#eef0f7">
                 <asp:Label ID="label_wxid" runat="server" Text="Label"></asp:Label>
                </td>
        

      </tr>
      <tr>
        <td height="125" align="left" bgcolor="##f8fbfe">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;请选择您所在的部门：</td>
        

      </tr>
       <tr>
        <td height="125" align="center" bgcolor="#eef0f7">
        <asp:DropDownList ID="dropProv" runat="server" Font-Size="30pt" Height="80px" Width="550px">
       </asp:DropDownList>
        </td>
       
      </tr>
      <tr>
        <td height="125" align="left" bgcolor="##f8fbfe">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;请输入姓名：</td>
       

      </tr>

      <tr>
        <td height="125" align="center" bgcolor="#eef0f7">
        <asp:TextBox ID="name" runat="server" Height="80" Width="550" Font-Size="30"></asp:TextBox>
        </td>
       
      </tr>
        <tr>
        <td height="125" align="center" bgcolor="#eef0f7" style="color:red;">
            <asp:Label ID="Label1_error" runat="server" Text=""></asp:Label>
        </td>
       
      </tr>
             
    </tbody>
  </table>

    <table width="100%" align="center" bgcolor="#eef0f7" border="0" 
            cellspacing="0" cellpadding="0">
    <tbody>
      <tr>
        <td height="30" align="center" style=" height:50px;"> <asp:Button ID="tj" runat="server" Text="提交申请" Height="70" Width="250" Font-Size="30" OnClick="tj_Click" />
         &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </td>
      </tr>
      <tr>
        <td height="50" align="center" style=" height:50px;">  </td>
      </tr>
    </tbody>
  </table>


  
    


</div>
<!--公司简介-->



    <div id="footer">
  <p>Copyright © 2017 丹江口水力发电厂工资系统&nbsp;&nbsp;&nbsp;&nbsp;  
  </p>
</div>






<div class="clear"></div>
</div>
     </div>
    </form>
</body>
</html>