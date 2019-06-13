using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
namespace homepage
{
    public partial class Advent : Form
    {
        public Advent()
        {
            InitializeComponent();
        }

        private void Advent_Load(object sender, EventArgs e)
        {//窗体加载时显示表格数据
            SqlConnection con = DBHelper.GetConnection();
            con.Open();
            String sql = "select staff.id as '职工号',staff.name as '姓名',sflag as '是否签到',starttime as '签到时间',endtime as '签退时间',department.name as '部门名称' from staff,department,attend where staff.id=attend.Id and staff.departId=department.Id";
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    string[] str = { reader[0].ToString(), reader[1].ToString(), reader[2].ToString(), reader[3].ToString(), reader[4].ToString(), reader[5].ToString() };
                    ListViewItem lv = new ListViewItem(str);
                    listView1.Items.Add(lv);
                    listBox1.Items.Add(reader[5].ToString()); //将部门名称加载到istview中
                }
                reader.Close();
                con.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {//查询数据
            string selected = listBox1.Text;
            SqlConnection con = DBHelper.GetConnection();
            con.Open();
           //多表进行连接，然后进行条件查询department.name发生错误，why???????????
            listView1.Items.Clear();
            String sql = "select staff.Id as '职工号',staff.name as '姓名',sflag as '是否签到',starttime as '签到时间',endtime as '签退时间',department.name as '部门名称' from staff,department,attend where staff.Id=attend.Id and staff.departId=department.Id and department.name=N'"+selected+"'";
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlDataReader reader = cmd.ExecuteReader();
            if (selected == "")
            {
                sql = "select staff.Id as '职工号',staff.name as '姓名',sflag as '是否签到',starttime as '签到时间',endtime as '签退时间',department.name as '部门名称' from staff,department,attend where staff.Id=attend.Id and staff.departId=department.Id";
            }
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    string[] str = { reader[0].ToString(), reader[1].ToString(), reader[2].ToString(), reader[3].ToString(), reader[4].ToString(), reader[5].ToString() };
                    ListViewItem lv = new ListViewItem(str);
                    listView1.Items.Add(lv);
                }
                reader.Close();
                con.Close();
            }
        }
    }
}
