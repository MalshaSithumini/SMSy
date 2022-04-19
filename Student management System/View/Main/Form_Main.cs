using Student_management_System.Controller;
using Student_management_System.Model;
using Student_management_System.View.Attendence;
using Student_management_System.View.Master;
using Student_management_System.View.Student;
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

namespace Student_management_System.View.Main
{
    public partial class Form_Main : Form
    {
        DB_Controller dbcon = new DB_Controller();
        SqlConnection sqlconn ;
        User_Model um = new User_Model();
        public Form_Main()
        {
            InitializeComponent();
            sqlconn = dbcon.getDBConnection();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
          
        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void toolStripButton1_Click_1(object sender, EventArgs e)
        {
            Form_StudentDetail newChild = new Form_StudentDetail();
            newChild.MdiParent = this;
            newChild.Show();
        }

        private void toolStripButton_parent_Click(object sender, EventArgs e)
        {
            Form_ParentDetail newChild = new Form_ParentDetail();
            newChild.MdiParent = this;
            newChild.Show();
        }

        private void classMasterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form_ClassMaster newChild = new Form_ClassMaster();
            newChild.MdiParent = this;
            newChild.Show();
        }

        private void logOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            this.Hide();
            try
            {
                if (sqlconn.State.ToString() != "Open")
                {
                    sqlconn.Open();
                }

                {

                    //string userid = ["User_ID"].ToString();


                    string sql = "INSERT into User_Log (User_ID,Date,Description)VALUES('" + um.UserID + "',getdate(),'Logout')";
                    SqlCommand cmnd = new SqlCommand(sql, sqlconn);
                    cmnd.ExecuteNonQuery();

                }



                Form_Login f = new Form_Login();
                f.ShowDialog();
                this.Close();
                sqlconn.Close();
            }
            catch (Exception ex)
            {
                //ec.ErrorLogging(ex, System.Environment.MachineName.ToString());
                MessageBox.Show(ex.ToString());
            }

        }

        private void userMasterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form_UserMaster newChild = new Form_UserMaster();
            newChild.MdiParent = this;
            newChild.Show();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            Form_Attendence newChild = new Form_Attendence();
            newChild.MdiParent = this;
            newChild.Show();
        }

        private void userMasterToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Form_AttendenceView newChild = new Form_AttendenceView();
            newChild.MdiParent = this;
            newChild.Show();
        }
    }
}
