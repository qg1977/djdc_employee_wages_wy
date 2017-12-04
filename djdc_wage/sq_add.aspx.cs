using System;
using System.Data;

namespace djdc_wage
{
    public partial class sq_add : System.Web.UI.Page
    {
        string accken_all = "";

        public DataAccess.Class1 dac = new DataAccess.Class1();
        string FileName;
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
              

                FileName = System.Web.HttpContext.Current.Server.MapPath("~/");
                dac = new DataAccess.Class1(FileName);
                get_titleid();

            }
        }


        public void get_titleid()
        {
            if (Request.QueryString["accken"] != null && Request.QueryString["accken"] != "")
            {
                accken_all = Request.QueryString["accken"];//接收传递进来的参数
                dac.accken_back();
                Response.Redirect("money_sql.aspx?accken=" + accken_all);
                return;
            }
            else
            {
                accken_all = dac.get_accken();
            }
            label_wxid.Text = accken_all;
            label_wxid.Visible = false;
            

            if (label_wxid.Text.Trim() == "-1")
            {
                //ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "alert('没有获得微信号，请重新刷新一次！');", true);
                Label1_error.Text = "没有获得微信号，请退出重新刷新一次！";
                return;
            }

            //LogTextHelper.Info("第二步：用户名是(sq_add)："+ accken_temp);


            string sqlstring;
            DataTable dt;

            sqlstring = "select ID from person where 微信ID='" + label_wxid.Text.Trim() + "'";
            dt = dac.return_select(sqlstring);
            if (dt.Rows.Count>0)
            {
                Response.Redirect("money_sql.aspx?accken=" + label_wxid.Text.Trim());
                return;
            }

            sqlstring= "select ID,部门名称 from z_fcname where 删除=0";
            dt = dac.return_select(sqlstring);

            //中心思想就是将下拉列表的数据源绑定一个表(这里没有对表进行赋值)
            dropProv.DataSource = dt.DefaultView;
            //设置DropDownList空间显示项对应的字段名,假设表里面有两列,一列绑定下拉列表的Text,另一列绑定Value
            dropProv.DataTextField = dt.Columns[1].ColumnName;
            dropProv.DataValueField = dt.Columns[0].ColumnName;

            dropProv.DataBind();
           
        }



        protected void tj_Click(object sender, EventArgs e)
        {
            Label1_error.Text = "";

            FileName = System.Web.HttpContext.Current.Server.MapPath("~/");
            dac = new DataAccess.Class1(FileName);

            if (label_wxid.Text.Trim() == "-1")
            {
                //ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "alert('没有获得微信号，请重新刷新一次！');", true);
                Label1_error.Text = "没有获得微信号，请退出重新刷新一次！";
                return;
            }

            //ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "alert('text：" + dropProv.SelectedValue + "！   values :"+dropProv.SelectedItem.Text+ "');", true);
            string  bmnametemp1 = dropProv.SelectedItem.Text.Trim();
            string bmidtempid = dropProv.SelectedValue.Trim();
            string nametemp1 = name.Text.ToString().Trim();

            string sqlstring = "select ID from person where 姓名=ltrim(Rtrim('" + nametemp1.Trim() + "'))";
            DataTable dt = dac.return_select(sqlstring);
            if (dt.Rows.Count<=0)
            {
                //ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "alert('没有在电厂花名册中找到：“"+nametemp1.Trim()+"”!');", true);
                Label1_error.Text = "没有在电厂花名册中找到：“" + nametemp1.Trim() + "”!";
                return;
            }
            string peridtempid = dt.Rows[0]["ID"].ToString();


            sqlstring = "select isnull(微信ID,'') 微信ID from person where 姓名=ltrim(Rtrim('" + nametemp1.Trim() + "'))";
            dt = dac.return_select(sqlstring);
            if (dt.Rows[0]["微信ID"].ToString().Trim()!="")
            {
                //ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "alert('“" + nametemp1.Trim() + "”已经绑定了微信号!');", true);
                Label1_error.Text = "“" + nametemp1.Trim() + "”已经绑定了微信号!";
                return;
            }



            sqlstring = "select ID from permoney where 部门ID='" + bmidtempid + "' and 员工ID='"+peridtempid+"'";
            //ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "alert('"+sqlstring+"！');", true);
            dt = dac.return_select(sqlstring);
            if (dt.Rows.Count<=0)
            {
                //ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "alert('没有在电厂工资名单的“"+bmnametemp1+"”部门中找到姓名为：“"+nametemp1+"”的员工！');", true);
                Label1_error.Text = "没有在电厂工资名单的“" + bmnametemp1 + "”部门中找到姓名为：“" + nametemp1 + "”的员工！";
                return;
            }

            sqlstring = "update person set 微信ID=ltrim(rtrim('" + label_wxid.Text.Trim() + "')) where ID='" + peridtempid + "'";
            dac.insert_update_delete(sqlstring);

            //ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "alert('绑定完毕！');", true);
            Label1_error.Text = "绑定完毕！";
            Response.Redirect("money_sql.aspx?accken=" + label_wxid.Text.Trim());
            return;
        }
    }
}