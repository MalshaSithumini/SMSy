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

    class Attendence_Controller
    {

        DB_Controller DbCon = new DB_Controller();
        SqlConnection sqlconn;
        Attendence_Model stuMod = new Attendence_Model();
       

    
        public Attendence_Controller()
        {
            sqlconn = DbCon.getDBConnection();
        }

        internal void loadGridUsingAllatten(DataGridView dataGridView1, Student_Model stuMod)
        {
            try
            {
                string query = "SELECT DISTINCT * FROM Student_Master WHERE Reg_No LIKE '" + stuMod.RegNo1 + "%' AND  Class_ID LIKE '" + stuMod.StuClassID1 + "%'  order by Reg_No";


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

                        dataGridView1.Rows[n].Cells[0].Value = result["Reg_No"].ToString();
                        dataGridView1.Rows[n].Cells[1].Value = result["Student_Name"].ToString();
                        //dataGridView1.Rows[n].Cells[2].Value = result["Birthday"].ToString();
                        //dataGridView1.Rows[n].Cells[3].Value = result["Address"].ToString();
                        //dataGridView1.Rows[n].Cells[4].Value = result["Mobile_No"].ToString();
                        dataGridView1.Rows[n].Cells[2].Value = result["Class_ID"].ToString();
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

        public DataSet loadattenDetailsToUpdate(Student_Model stuMod)
        {
            DataSet ds = new DataSet();
            try
            {
                if (sqlconn.State.ToString() != "Open")
                {
                    sqlconn.Open();
                }
                try
                {
                    string url = "SELECT Reg_No,Student_Name,Class_ID FROM Student_Master where Reg_No";
                    SqlCommand cmd = new SqlCommand(url, sqlconn);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);


                    da.Fill(ds, "Student_Master");

                    sqlconn.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                    //autoexceptions.AutoEmailSystem("CusMas Contrller SearchCustomerByCode method Problem :- " + ex.Message);
                }
            }
            catch (Exception ex)
            {
                //ec.ErrorLogging(ex, System.Environment.MachineName.ToString());
                MessageBox.Show(ex.ToString());
            }
            return ds;
        }

       
        internal void regAttendence(Attendence_Model attMod)
        {

            //try
            //{
            //    if (sqlconn.State.ToString() != "Open")
            //    {
            //        sqlconn.Open();

            //    }
            //    SqlCommand command = new SqlCommand();

            //    command.Connection = sqlconn;
            //    command.CommandType = CommandType.Text;


            //    command.CommandText = "INSERT into Attendence_Detail (Reg_No,Date,Attendence_Status) VALUES (@RegNo, @Date, @Status)";

            //    command.Parameters.AddWithValue("@RegNo", attMod.RegNo);
            //    command.Parameters.AddWithValue("@Date", attMod.Date);
            //    command.Parameters.AddWithValue("@Status", attMod.Status);


            //    int i = command.ExecuteNonQuery();

            //    if (i == 1)
            //    {
            //        status = true;
            //        MessageBox.Show("Successfully Registered..!", "Register Attendance", MessageBoxButtons.OK);
            //    }
            //    else
            //    {
            //        MessageBox.Show("not saved!", "Register Attendance", MessageBoxButtons.OK);
            //    }



            //    sqlconn.Close();
            //}
            //catch (Exception ex)
            //{
            //    //ec.ErrorLogging(ex, System.Environment.MachineName.ToString());
            //    MessageBox.Show(ex.ToString());
            //}
            //return status;

        }



    }

   
}
