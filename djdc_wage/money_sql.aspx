<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="money_sql.aspx.cs" Inherits="djdc_wage.money_sql" %>

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
                  color="#990000">查询工资(<asp:Label ID="Label_name" runat="server" Text="Label"></asp:Label>)</font></b></td>
      </tr>
    </tbody>
  </table>

    <table width="100%" align="center" bgcolor="#dde5e7" 
            border="0" cellspacing="1" cellpadding="0">
    <tbody>
      <tr>
        <td width="100%" height="25" align="left" 
                bgcolor="#eef0f7">
                 <asp:Label ID="label_wxid" runat="server" Text=""></asp:Label>
            <asp:Label ID="label_perid" runat="server" Text=""></asp:Label>
                </td>
        

      </tr>

                <tr>
        <td height="125" align="center" bgcolor="#eef0f7" style="color:red;">
            <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
        </td>
       
      </tr>


      <tr>
        <td height="125" align="left" bgcolor="##f8fbfe">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;请选择月份：</td>
        

      </tr>
       <tr>
        <td height="125" align="center" bgcolor="#eef0f7">
        <asp:DropDownList ID="Dropmonth" runat="server" Font-Size="30pt" Height="80px" Width="550px" AutoPostBack="true" OnSelectedIndexChanged="Drpmonth_SelectedIndexChanged">
       </asp:DropDownList>
        </td>
       
      </tr>
      <tr>
        <td height="125" align="left" bgcolor="##f8fbfe">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;工资信息：</td>
       

      </tr>

      <tr>
        <td height="125" align="center" bgcolor="#eef0f7">
            <asp:Repeater ID="Repeater_money" runat="server">
            <HeaderTemplate>
<table width="85%" border="1">
  <tr>
    <td>工资类型</td>
    <td>金额</td>
  </tr>
   </HeaderTemplate>
    <ItemTemplate>
  <tr>
    <td><%#Eval("工资类型")%></td>
    <td><%#Eval("金额")%></td>
  </tr>

    </ItemTemplate>
    <FooterTemplate>
        </table>

    </FooterTemplate>
</asp:Repeater>


            </asp:Repeater>

        </td>
       
      </tr>
        <tr>
        <td height="125" align="center" bgcolor="#eef0f7" style="color:red;">
            <asp:Label ID="Label1_error" runat="server" Text=""></asp:Label>
        </td>
       
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