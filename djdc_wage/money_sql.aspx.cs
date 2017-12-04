using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace djdc_wage
{
    public partial class money_sql : System.Web.UI.Page
    {
        string accken_all = "";
        string pername_all = "";
        string perid_all = "0";

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
            Label1_error.Text = "";

            if (Request.QueryString["accken"] != null && Request.QueryString["accken"] != "")
            {
                accken_all = Request.QueryString["accken"];//接收传递进来的参数
            }
            else
            {
                accken_all = dac.get_accken();
            }
            label_wxid.Text = accken_all;
            label_wxid.Visible = false;
            label_perid.Visible = false;

            if (label_wxid.Text.Trim() == "-1")
            {
                //ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "alert('没有获得微信号，请重新刷新一次！');", true);
                Label1_error.Text = "没有获得微信号，请退出重新刷新一次！";
                return;
            }

            string sqlstring;
            DataTable dt;
            sqlstring = "select ID,姓名 from person where 微信ID='" + label_wxid.Text.Trim() + "'";
            dt = dac.return_select(sqlstring);
            if (dt.Rows.Count <= 0)
            {
                Response.Redirect("sq_add.aspx?accken=" + label_wxid.Text.Trim());
                return;
            }
            perid_all = dt.Rows[0]["ID"].ToString();
            pername_all = dt.Rows[0]["姓名"].ToString();
            Label_name.Text = pername_all.Trim();
            label_perid.Text = perid_all;

            sqlstring = "select distinct 月份 from permoney where 员工ID='"+perid_all+"'";
            dt = dac.return_select(sqlstring);
            if (dt.Rows.Count <= 0)
            {
                Label1_error.Text = "没有在电厂工资数据库中查询到“"+pername_all+"”的工资信息！";
                return;
            }
            

            Dropmonth.DataSource = dt.DefaultView;
            //Dropmonth.DataValueField = dt.Columns[0].ColumnName;
            Dropmonth.DataTextField = dt.Columns[0].ColumnName;
            Dropmonth.DataBind();
      
        }

        protected void Drpmonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            FileName = System.Web.HttpContext.Current.Server.MapPath("~/");
            dac = new DataAccess.Class1(FileName);

            Label1_error.Text = "";
            string monthtemp1 = Dropmonth.SelectedItem.Text.Trim();
            if (monthtemp1.Trim()=="")
            {
                Label1_error.Text = "请选择月份！";
                return;
            }
            string sqlstring = "";
            DataTable dt;
            //LogTextHelper.Info("我选择了月份"+monthtemp1);

            dt = dac.person_money_all(label_perid.Text.Trim(), monthtemp1);
            //LogTextHelper.Info("员工ID="+perid_all+"查询出的工资有" + dt.Rows.Count.ToString()+"条记录！");
            Repeater_money.DataSource = dt;
            Repeater_money.DataBind();

        }


//结束
    }
}