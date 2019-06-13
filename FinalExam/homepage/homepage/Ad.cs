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
    public partial class Ad : Form
    {
        public Ad()
        {
            InitializeComponent();
        }

        private void Ad_Load(object sender, EventArgs e)
        {
            listView1.Visible = true;
            textBox2.Visible = false;
            button3.Visible = false;
            SqlConnection con = DBHelper.GetConnection();
            con.Open();
            String sql = "select * from ad";
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    string[] str = { reader["Id"].ToString(), reader["content"].ToString(), reader["date"].ToString()};
                    ListViewItem lv = new ListViewItem(str);
                    listView1.Items.Add(lv);
                }
                reader.Close();
                con.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {//删除公告
            listView1.Visible = true;
            textBox2.Visible = false;
            button3.Visible = false;
            button1.Visible = true;
            label2.Visible = true;
            textBox1.Visible = true;
            int ID = int.Parse(textBox1.Text);
            SqlConnection con = DBHelper.GetConnection();
            con.Open();
            string sql = "delete from ad where Id="+ID;
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlDataReader reader = cmd.ExecuteReader();
            listView1.Items.Clear();
            Ad_Load(sender, e);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            listView1.Visible = false;
            textBox2.Visible = true;
            button3.Visible = true;
            button1.Visible = false;
            label2.Visible = false;
            textBox1.Visible = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {//插入公告
            string content = textBox2.Text;
            DateTime date = DateTime.Now;
            String sql = "select count(*) from ad";
            SqlConnection con = DBHelper.GetConnection();
            con.Open();
            SqlCommand cmd = new SqlCommand(sql, con);
            int id = int.Parse(cmd.ExecuteScalar().ToString())+1;
            sql = "insert into ad values("+id+",N'"+content+"','"+date+"')";
            cmd = new SqlCommand(sql, con);
            cmd.ExecuteScalar();
            MessageBox.Show("发布公告成功");
            textBox2.Text = "";
            //重新显示
            listView1.Items.Clear();//首先清空表，再重新查询
            listView1.Visible = true;
            textBox2.Visible = false;
            button3.Visible = false;
            button1.Visible = true;
            label2.Visible = true;
            textBox1.Visible = true;
            sql = "select * from ad";
            cmd = new SqlCommand(sql, con);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    string[] str = { reader["Id"].ToString(), reader["content"].ToString(), reader["date"].ToString() };
                    ListViewItem lv = new ListViewItem(str);
                    listView1.Items.Add(lv);
                }
                reader.Close();
                con.Close();
            }
        }
    }
}
