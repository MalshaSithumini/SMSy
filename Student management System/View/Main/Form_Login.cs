using Student_management_System.Controller;
using Student_management_System.Model;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Student_management_System.View.Main
{
    public partial class Form_Login : Form

    {
        User_Model um = new User_Model();
        User_Controller uc = new User_Controller();
        //ErrorHandling_Controller ec = new ErrorHandling_Controller();
        DB_Controller DbCon = new DB_Controller();
        SqlConnection sqlconn;

        public Form_Login()
        {
            InitializeComponent();
            sqlconn = DbCon.getDBConnection();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void textBox_username_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (uc.getUserByName(textBox_username.Text))
                {
                    textBox_password.Enabled = true;
                    textBox_password.Select();
                }
                else
                {
                    MessageBox.Show("Wrong User Name");
                }
            }
        }

        private void textBox_password_KeyDown(object sender, KeyEventArgs e)
        {

            try
            {
                if (sqlconn.State.ToString() != "Open")
                {
                    sqlconn.Open();
                }
                if (e.KeyCode == Keys.Enter)
                {
                    if (uc.getUserByPassword(textBox_password.Text))
                    {
                        this.Hide();



                        string query = "SELECT * from User_Master where User_Name='" + textBox_username.Text + "' AND Password='" + textBox_password.Text + "' AND User_Status=1 AND User_Type='User'";
                        SqlCommand cmd = new SqlCommand(query, sqlconn);
                        SqlDataReader result = cmd.ExecuteReader();
                        ///*int i =*/ cmd.ExecuteNonQuery();
                      

                        if (result.Read())
                        {
                            string userid = result["User_ID"].ToString();
                            result.Close();
                           
                            string sql = "INSERT into User_Log (User_ID,Date,Description)VALUES('"+ userid + "',getdate(),'Login')";
                            SqlCommand cmnd = new SqlCommand(sql, sqlconn);
                            cmnd.ExecuteNonQuery();
                          

                            //MessageBox.Show("Correct");
                        }


                       
                        Form_Main f = new Form_Main();
                        f.ShowDialog();
                        this.Close();
                        
                    }
                    else
                    {
                        MessageBox.Show("Wrong Password");
                    }
                }
              
                sqlconn.Close();
            }

             
            catch (Exception ex)
            {
                //ec.ErrorLogging(ex, System.Environment.MachineName.ToString());
                MessageBox.Show(ex.ToString());
            }
          
           
        }

  

        private void textBox_username_TextChanged(object sender, EventArgs e)
        {
            if (uc.getUserByName(textBox_username.Text))
            {
                textBox_username.Enabled = false;
                textBox_password.Enabled = true;
                textBox_password.Focus();
            }
        }


        
    
}
}
