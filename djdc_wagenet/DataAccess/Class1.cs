using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data.OleDb;
using System.Data;
using System.Text.RegularExpressions;

using System.Net;
using System.IO;
using System.Data.SqlClient;

namespace DataAccess
{
    public class Class1
    {
        public string public_www =  "http://www.dq006.com";

        //一些特殊的东西，比如“通知公告”其在表titleall中的对应ID为
        public string z_titleid_1 = "1";//网站首页
        public string z_titleid_2 = "2";//关于我们
        public string z_titleid_3 = "3";//生产经营
        public string z_titleid_4 = "4";//服务管理
        public string z_titleid_5 = "5";//旅游发展
        public string z_titleid_6 = "6";//党群工作
        public string z_titleid_7 = "7";//职工园地
        public string z_titleid_8 = "8";//老网站

        public string z_titleid_9 = "9";//最新动态
        //public string z_titleid_10 = "10";//公司新闻
        public string z_titleid_11 = "11";//通知公告
        public string z_titleid_12 = "12";//公司活动
        public string z_titleid_13 = "13";//项目简报
        public string z_titleid_14 = "14";//市场动态参考 
        public string z_titleid_15 = "15";//荣誉展台
        public string z_titleid_17 = "17";//政策法规
        public string z_titleid_18 = "18";//资质证书
        public string z_titleid_19 = "19";//联系我们
        public string z_titleid_20 = "20";//资料下载
        public string z_titleid_21 = "21";//监督邮箱
        public string z_titleid_22 = "22";//汉江水利报


        public string z_titleid_52 = "52";//图片库

        public string z_titleid_53 = "53";//两学一做
        public string z_titleid_54 = "54";//致用课堂
        public string z_titleid_61 = "61";//学习贯彻十九大

        public string z_titleid_58 = "58";//我们的价值观

        public string z_titleid_59 = "59";//监督信箱

        public string z_bottom_ref = "1";//相关链接

        private string connectString;//连接语句
        private string conn;//网站的根目录

        SqlConnection cn;//前台与后台数据库建立的连接
        SqlDataAdapter da;//形成的游标
        DataSet ds = new DataSet();//数据库

        public Class1()
        { }
        public Class1(string conn)//获取网站的根目录　
        {

            this.conn = conn;
              connectString = "server=qds16172939.my3w.com;database=qds16172939_db;uid=qds16172939;pwd=infowindow2015;Connect Timeout=150";
            cn = new SqlConnection(connectString);
        }



        //删除服务器的 某一个文件
        public void DeleFile(string filePath)
        {
            try
            {
                System.IO.FileInfo DeleFile = new System.IO.FileInfo(filePath);
                if (DeleFile.Exists)
                {
                    DeleFile.Delete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //删除文件夹
        public void DeleteFolder(string path)
        {
            try
            {
                if (Directory.Exists(path)) //如果存在这个文件夹删除之
                {
                    foreach (string d in Directory.GetFileSystemEntries(path))
                    {
                        if (File.Exists(d))
                            File.Delete(d); //直接删除其中的文件
                        else
                            DeleteFolder(d); //递归删除子文件夹
                    }
                    Directory.Delete(path); //删除已空文件夹
                    Console.Write(path + " 文件夹删除成功");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        //执行一条修改或插入或删除的程序
        public void insert_update_delete(string sql_string)
        {
            SqlCommand cmd = new SqlCommand(sql_string, cn);
            cn.Open();
            cmd.ExecuteNonQuery();
            cn.Close();
        }


        //直接根据查询语句返回一个datatable
        public DataTable return_select(string sql_string)
        {
            SqlCommand cmd = new SqlCommand(sql_string, cn);
            da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();//数据库
            da.Fill(ds);

            DataTable dt = ds.Tables[0];
            cn.Close();

            return dt;
        }


        //截取文件后缀 名
        public string GetFileExt(string FullPath)
        {
            if (FullPath != "") return FullPath.Substring(FullPath.LastIndexOf('.') + 1).ToLower();
            else return "";
        }

        //暂时
        public void set_accken(string accken_string)
        {
            string sqlstring;

            sqlstring = "select appid from accken_temp";
            DataTable dt = return_select(sqlstring);
            if (dt.Rows.Count<=0)
            {
                sqlstring = "insert into accken_temp(appid) values ('"+accken_string.Trim()+"')";
                insert_update_delete(sqlstring);
            }else
            {
                sqlstring = "update accken_temp set appid='" + accken_string.Trim() + "'";
                insert_update_delete(sqlstring);

            }
        }

        public string get_accken()
        {
            string accken_all="-1";
            string sqlstring = "select appid from accken_temp";
            DataTable dt = return_select(sqlstring);

            if (dt.Rows.Count>0)
            {
                accken_all = dt.Rows[0]["appid"].ToString().Trim();

                accken_back();
            }

            return accken_all;
        }

        public void accken_back()
        {
            string sqlstring = "update accken_temp set appid='-1'";
            insert_update_delete(sqlstring);
        }


        public DataTable person_money_all(string perid,string pro3)
        {
            DataTable dt=null;

            string sqlstring;
            sqlstring = "select ID,金额,员工ID,姓名=isnull((select 姓名 from person where ID=p.员工ID),'')"
                   + ",条目ID,工资类型=isnull((select 条目名称 from Subject_name where ID=p.条目ID),'')"
                   + ",条目排序=isnull((select 排序 from Subject_mon where 月份='" + pro3 + "' and 条目ID=p.条目ID),0)"
                   + " from permoney p where 员工ID='" + perid + "' and 月份='" + pro3 + "'"
                        + " and 条目ID not in (select 条目ID from Subject_mon where 月份='" + pro3 + "' and 类型=1)"
                         + " order by 条目排序";
            dt = return_select(sqlstring);

            return dt;
        }



 //结束
    }

}

