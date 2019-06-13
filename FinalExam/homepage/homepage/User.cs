using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace homepage
{
    public partial class User : Form
    {
        public User()
        {
            InitializeComponent();
        }

        private void User_Load(object sender, EventArgs e)
        {
            label2.Text = Homepage.loginuser;
            listView1.Visible = false;
            panel1.Visible = true;
            label8.Visible = false;
            listView2.Visible = false;
    }

        private void button1_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
            listView1.Visible = true;
           int id=0;
            //点击签到后加载数据
            SqlConnection con = DBHelper.GetConnection();
            con.Open();
            string name = label2.Text; 
            string sql = "select id from staff where username='" + name + "'";
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    MessageBox.Show(reader["id"].ToString());
                    id = int.Parse(reader["id"].ToString());
                }
            }
            reader.Close();
            DateTime date = DateTime.Now;
            bool flag = true;
            //插入签到信息
            sql = "insert into attend(Id,sflag,starttime) values(" + id + ",'" + flag + "','" + date+"')";
            cmd = new SqlCommand(sql, con);
            cmd.ExecuteNonQuery();
            MessageBox.Show(name + "签到成功！");
            //显示签到信息
            sql = "select staff.Id as '职工号',staff.name as '姓名',sflag as '是否签到',starttime as '签到时间',endtime as '签退时间',department.name as '部门名称' from staff,department,attend where staff.Id=attend.Id and staff.departId=department.Id";
            cmd = new SqlCommand(sql, con);
            reader = cmd.ExecuteReader();
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

        private void 公告栏ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
            listView1.Visible = false;
            label8.Visible = true;
            listView2.Visible = true;
            //显示公告
            SqlConnection con = DBHelper.GetConnection();
            con.Open();
            String sql = "select * from ad";
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    string[] str = { reader["Id"].ToString(), reader["content"].ToString(), reader["date"].ToString() };
                    ListViewItem lv = new ListViewItem(str);
                    listView2.Items.Add(lv);
                }
                reader.Close();
                con.Close();
            }
        }

        private void 个人信息ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelfInfo s = new SelfInfo();
            s.Show();
        }
    }
}
