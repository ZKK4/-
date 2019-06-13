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
    public partial class DepartGovern : Form
    {
        public DepartGovern()
        {
            InitializeComponent();
        }
        SqlDataAdapter sda;
        DataSet ds;
        private void DepartGovern_Load(object sender, EventArgs e)
        {
            SqlConnection con = DBHelper.GetConnection();
            con.Open();
            string sqlcmd = "select Id as '部门编号',name as '部门名称',manager as '部门经理',managerId as '经理编号' from department";
            SqlCommand cmd = new SqlCommand(sqlcmd, con);
            sda = new SqlDataAdapter(cmd);
            ds = new DataSet();
            sda.Fill(ds, "stu");
            dataGridView1.DataSource = ds.Tables["stu"];
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            //数据总数
            sqlcmd = "select count(*) from department";
            cmd = new SqlCommand(sqlcmd, con);
            label3.Text = cmd.ExecuteScalar().ToString();

            //将部门名称加载到istview中
            sqlcmd = "select * from department";
            cmd = new SqlCommand(sqlcmd, con);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    listBox1.Items.Add(reader["name"].ToString()); 
                }
            }
            reader.Close();
            con.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            sda.Update(ds, "stu");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int r = this.dataGridView1.CurrentRow.Index;
            this.dataGridView1.Rows.Remove(this.dataGridView1.Rows[r]);
            sda.Update(ds, "stu");
        }

        private void button3_Click(object sender, EventArgs e)
        {//查询
            string selected = listBox1.Text;
            SqlConnection con = DBHelper.GetConnection(); con.Open();
            String sql = "select Id as '部门编号',name as '部门名称',manager as '部门经理',managerId as '经理编号' from department where name=N'"+selected+"'";
            if (selected == "")
            {
                sql = "select Id as '部门编号',name as '部门名称',manager as '部门经理',managerId as '经理编号' from department";
            }
            SqlCommand cmd = new SqlCommand(sql, con);
            sda = new SqlDataAdapter(cmd);
            ds = new DataSet();
            sda.Fill(ds, "stu");
            dataGridView1.DataSource = ds.Tables["stu"];
        }
    }
}
