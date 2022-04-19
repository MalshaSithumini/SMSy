using Student_management_System.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Student_management_System.Controller
{
    class User_Controller
    {
        User_Model um = new User_Model();
        DB_Controller DBConnection = new DB_Controller();
        //ErrorHandling_Controller ec = new ErrorHandling_Controller();
        SqlConnection sqlconn;
        public bool stat;
       

        public User_Controller()
        {
            sqlconn = DBConnection.getDBConnection();
        }

        public void AllUserbyPassword(User_Model userMod)
        {
            try
            {
                if (sqlconn.State.ToString() == "Closed")
                {
                    sqlconn.Open();
                }

                string query = "Select User_Name from User_Master where Password = '" + userMod.Password + "' and User_Status ='True'";

                SqlCommand queryCommand = new SqlCommand(query, sqlconn);
                SqlDataReader dr = queryCommand.ExecuteReader();

                while (dr.Read())
                {
                    userMod.UserName = dr[0].ToString();
                }

                sqlconn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }


        public bool getUserByName(string userName)
        {
            if (sqlconn.State.ToString() != "Open")
            {
                sqlconn.Open();
            }
            Boolean stat = false;
            try
            {
                string query1 = "SELECT count(User_ID) FROM User_Master WHERE User_Name='" + userName + "'";
                SqlDataAdapter sda = new SqlDataAdapter(query1, sqlconn);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                if (dt.Rows[0][0].ToString() == "1")
                {
                    stat = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());              
            }
            sqlconn.Close();
            return stat;
        }



        public bool getUserByPassword(string password)
        {
            if (sqlconn.State.ToString() != "Open")
            {
                sqlconn.Open();
            }
            Boolean stat = false;
            try
            {
                string query1 = "SELECT count(User_ID) FROM User_Master WHERE Password='" + password + "'";
                SqlDataAdapter sda = new SqlDataAdapter(query1, sqlconn);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                if (dt.Rows[0][0].ToString() == "1")
                {
                    stat = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            sqlconn.Close();
            return stat;
        }


        internal void loadGridUsingAlluser(DataGridView dataGridView1, User_Model um)
        {
            try
            {
                string query = "SELECT DISTINCT * FROM User_Log WHERE User_ID LIKE '" + um.UserID + "%'  order by User_ID";


                //ResetDataGridView
                dataGridView1.Rows.Clear();

                //loadDataToGrid
                if (sqlconn.State.ToString() != "Open")
                {
                    sqlconn.Open();
                }

                SqlCommand cmd = new SqlCommand(query, sqlconn);
                SqlDataReader result = cmd.ExecuteReader();
                if (result.HasRows)
                {
                    while (result.Read())
                    {
                        int n = dataGridView1.Rows.Add();

                        dataGridView1.Rows[n].Cells[0].Value = result["User_ID"].ToString();
                        dataGridView1.Rows[n].Cells[1].Value = result["Date"].ToString();
                        dataGridView1.Rows[n].Cells[2].Value = result["Description"].ToString();
                        //dataGridView1.Rows[n].Cells[3].Value = result["Address"].ToString();
                        //dataGridView1.Rows[n].Cells[4].Value = result["Mobile_No"].ToString();
                        //dataGridView1.Rows[n].Cells[5].Value = result["Class_ID"].ToString();
                        //dataGridView1.Rows[n].Cells[6].Value = result["Student_Image"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                //ec.ErrorLogging(ex, System.Environment.MachineName.ToString());
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                sqlconn.Close();
            }


        }


    }

}
