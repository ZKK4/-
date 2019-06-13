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
    public partial class Homepage : Form
    {
        public static string loginuser;
        public Homepage()
        {
            InitializeComponent();
        }
        //查询数据库，查看登录用户or管理员是否存在
        private bool sqlcheck1()
        {
            string username = textBox1.Text;
            string password = textBox2.Text;
            SqlConnection con = DBHelper.GetConnection();
            string sql = "select count(*) from manager where username='" + username + "' and password='" + password + "'";
            SqlCommand cmd = new SqlCommand(sql, con);
            con.Open();
            int num = (int)cmd.ExecuteScalar();
            if (num == 1)
            {
                loginuser = username;
                return true;
            }
                
            else
            {
                return false;
            }
                
        }
        private bool sqlcheck2()
        {
            string username = textBox1.Text;
            string password = textBox2.Text;
            SqlConnection con = DBHelper.GetConnection();
            string sql = "select count(*) from staff where username='" + username + "' and password='" + password + "'";
            SqlCommand cmd = new SqlCommand(sql, con);
            con.Open();
            int num = (int)cmd.ExecuteScalar();
            if (num == 1)
            {
                loginuser = username;
                return true;
            }
            else
            {
                return false;
            }

        }
        //登录
        private void button1_Click(object sender, EventArgs e)
        {//管理员登陆
            if (radioButton1.Checked)
            {
                if (sqlcheck1()==true)
                {
                    Manager m = new Manager();
                    m.Show();
                }
                else
                {
                    MessageBox.Show("该管理员不存在！");
                    textBox1.Text = "";
                    textBox2.Text = "";
                }
            }//用户登录
            if(radioButton2.Checked)
            {
                if (sqlcheck2() == true)
                {
                    User u = new User();
                    u.Show();
                }
                else
                {
                    MessageBox.Show("该用户不存在！");
                    textBox1.Text = "";
                    textBox2.Text = "";
                }
            }
        }
        //注册
        private void button2_Click(object sender, EventArgs e)
        {
            Register r = new Register();
            r.Show();
        }
        //显示时间
        private Timer Timer;
        private void Homepage_Load(object sender, EventArgs e)
        {
            Timer = new Timer();
            Timer.Interval = 1000;
            Timer.Tick += new EventHandler(Timer_Tick);
            Timer.Start();
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            this.label3.Text = DateTime.Now.ToString("当前时间：HH:mm:ss");
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
    }
}
