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
    public partial class SelfInfo : Form
    {
        private string pathname = string.Empty;//定义路径名变量
        public SelfInfo()
        {
            InitializeComponent();
        }

        private void SelfInfo_Load(object sender, EventArgs e)
        {
            button2.Visible = false;
            button3.Visible = false;
            string username = Homepage.loginuser;
            String sql = "select * from staff where username='" + username + "'";
            //创建连接
            SqlConnection con = DBHelper.GetConnection();
            con.Open();
            //执行sql语句
            SqlCommand sqlcommand = new SqlCommand(sql, con);
            SqlDataAdapter dbAdapter = new SqlDataAdapter(sqlcommand);
            DataSet ds = new DataSet();
            dbAdapter.Fill(ds);
            if (ds.Tables[0].Rows.Count != 0)
            {
                textBox1.Text = ds.Tables[0].Rows[0]["name"].ToString();
                textBox2.Text = ds.Tables[0].Rows[0]["position"].ToString();
                textBox3.Text = ds.Tables[0].Rows[0]["tel"].ToString();
                textBox4.Text = ds.Tables[0].Rows[0]["date"].ToString();
                textBox5.Text = ds.Tables[0].Rows[0]["username"].ToString();
                textBox6.Text = ds.Tables[0].Rows[0]["password"].ToString();
                pathname = @"F:\大三下学期课程文件\Windows\FinalExam\homepage\homepage\images\staff\" + ds.Tables[0].Rows[0]["picturepath"].ToString();
                pictureBox1.Image = Image.FromFile(pathname);
            }
            else
            {
                MessageBox.Show("数据不存在");
            }
            textBox1.Enabled = false;
            textBox2.Enabled = false;
            textBox3.Enabled = false;
            textBox4.Enabled = false;
            textBox5.Enabled = false;
            pictureBox1.Enabled = false;
            textBox6.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            button2.Visible = true;
            button1.Visible = false;
            textBox1.Enabled = true;
            textBox2.Enabled = true;
            textBox3.Enabled = true;
            textBox4.Enabled = true;
            textBox5.Enabled = false;
            pictureBox1.Enabled = true;
            textBox6.Enabled = true;
            button3.Visible=true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog file = new OpenFileDialog();

            file.InitialDirectory = ".";

            file.Filter = "所有文件(*.*)|*.*";

            file.ShowDialog();

            if (file.FileName != string.Empty)

            {
                try
                {
                    pathname = file.FileName;//获得文件的绝对路径
                    this.pictureBox1.Load(pathname);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //保存到数据库
            string[] s = pathname.Split('\\');
            string path = s[s.Length - 1];
            string name = textBox1.Text;
            string position = textBox2.Text;
            string tel = textBox3.Text;
            DateTime date = Convert.ToDateTime(textBox4.Text);
            string password = textBox6.Text;
            SqlConnection con = DBHelper.GetConnection();
            con.Open();
            string sql = "update staff set name=N'" + name + "',position=N'" + position + "',tel='" + tel + "',date='" + date + "',password='" + password + "',picturepath='" + path + "' where username='" + textBox5.Text + "'";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.ExecuteNonQuery();
            MessageBox.Show("修改成功！");
        }
    }
}

