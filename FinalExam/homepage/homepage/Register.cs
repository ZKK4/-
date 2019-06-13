using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace homepage
{
    public partial class Register : Form
    {
        public Register()
        {
            InitializeComponent();
        }

        private void label7_Click(object sender, EventArgs e)
        {
            Register_Load(sender,e);
        }
        private void Register_Load(object sender, EventArgs e)
        {
            string str = "abcdefghigklmnopqrstuvwxyzABCDEFJHIGKLMNOPQRSTUVWXYZ1234567890";
            string yzm = "";
            Random rnd = new Random();
            for (int i = 0; i < 4; i++)
            {
                int index = rnd.Next(0, str.Length);
                string x = str.Substring(index, 1);
                yzm += x;
            }
            label7.Text = yzm;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string username = textBox1.Text;
            string password = textBox2.Text;
            string password2 = textBox3.Text;
            string name = textBox4.Text;
            string yzm = textBox5.Text;
            if(yzm != label7.Text)
            {
                MessageBox.Show("验证码输入错误！");
                textBox5.Text = "";
            }
            if(password != password2)
            {
                MessageBox.Show("确认密码输入有误！");
                textBox3.Text = "";
            }
            if (password == password2 && yzm == label7.Text)
            {
                SqlConnection con = DBHelper.GetConnection();
                con.Open();
                if (con.State == ConnectionState.Open)
                {
                    string sql = "select count(*) from staff where username='" + username + "'";
                    SqlCommand cmd = new SqlCommand(sql, con);
                    int num = (int)cmd.ExecuteScalar();
                    if (num == 1)
                    {
                        MessageBox.Show("该用户名已经存在！");
                    }
                    else
                    {
                        string cmdText = "insert into staff(username,password,name) values('" + username + "','" + password + "','" + name + "')";
                        SqlCommand rmd = new SqlCommand(cmdText, con);
                        rmd.ExecuteNonQuery();
                        MessageBox.Show("用户" + username + "注册成功！");
                    }
                }
            }
        }

        private void label8_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
