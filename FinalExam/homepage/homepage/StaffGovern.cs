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
    public partial class StaffGovern : Form
    {
        public StaffGovern()
        {
            InitializeComponent();
        }

        SqlDataAdapter sda;
        DataSet ds;
        private void StaffGovern_Load(object sender, EventArgs e)
        {
            //加载员工信息表
            SqlConnection con = DBHelper.GetConnection();
            con.Open();
            string sqlcmd = "select id as '职工号',name as '姓名',position as '职位',tel as '手机号码',date as '入职日期' from staff";
            SqlCommand cmd = new SqlCommand(sqlcmd, con);
            sda = new SqlDataAdapter(cmd);
            ds = new DataSet();
            sda.Fill(ds, "stu");
            dataGridView1.DataSource = ds.Tables["stu"];
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            //数据总数
            sqlcmd = "select count(*) from staff";
            cmd= new SqlCommand(sqlcmd, con);
            label3.Text = cmd.ExecuteScalar().ToString();
            con.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {//修改更新数据
            sda.Update(ds, "stu");
        }

        private void button2_Click(object sender, EventArgs e)
        {//删除一行数据
            int r = this.dataGridView1.CurrentRow.Index;
            this.dataGridView1.Rows.Remove(this.dataGridView1.Rows[r]);
            sda.Update(ds, "stu");
        }

        private void button3_Click(object sender, EventArgs e)
        {//查询操作
            string name = textBox1.Text;
            SqlConnection con = DBHelper.GetConnection();con.Open();
            String sql = "select id as '职工号',name as '姓名',position as '职位',tel as '手机号码',date as '入职日期' from staff where name=N'" + name+ "'";
            if (name == "")
            {
                sql = "select id as '职工号',name as '姓名',position as '职位',tel as '手机号码',date as '入职日期' from staff";
            }
            SqlCommand cmd = new SqlCommand(sql, con);
            sda = new SqlDataAdapter(cmd);
            ds = new DataSet();
            sda.Fill(ds, "stu");
            dataGridView1.DataSource = ds.Tables["stu"];
        }
    }
}
