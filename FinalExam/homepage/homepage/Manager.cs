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
    public partial class Manager : Form
    {
        public Manager()
        {
            InitializeComponent();
        }
        SqlDataAdapter sda;
        DataSet ds;
        private void Manager_Load(object sender, EventArgs e)
        {
            label2.Text = Homepage.loginuser;
           
        }

        private void label3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void 员工管理ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            StaffGovern sg = new StaffGovern();
            sg.Show();
        }

        private void 部门管理ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            DepartGovern dg = new DepartGovern();
            dg.Show();
        }

        private void 考勤管理ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Advent ad = new Advent();
            ad.Show();
        }

        private void 公告管理ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Ad a = new Ad();
            a.Show();
        }
    }
}
