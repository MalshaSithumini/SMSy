using Student_management_System.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Student_management_System.Controller
{
    class Student_Controller
    {
        
        DB_Controller DbCon = new DB_Controller();
        SqlConnection sqlconn;
        Student_Model stuMod = new Student_Model();

        //constructor
        public Student_Controller()
        {
            sqlconn = DbCon.getDBConnection();
        }

        //Student Insert
        public bool regStudent(Student_Model stuMod)
        {


            bool status = false;
            try
            {
                if (sqlconn.State.ToString() != "Open")
                {
                    sqlconn.Open();

                }
                SqlCommand command1 = new SqlCommand();

                command1.Connection = sqlconn;           
                command1.CommandType = CommandType.Text;
                

                command1.CommandText = "INSERT into Student_Master (Reg_No,Student_Name,Birthday,Address,Mobile_No,Class_ID,Student_Image) VALUES (@RegNo, @StudentName, @BirthDay, @Address, @Mobile, @ClassId, @StudentImage)";

                command1.Parameters.AddWithValue("@RegNo", stuMod.RegNo1);
                command1.Parameters.AddWithValue("@StudentName", stuMod.StuName1);
                command1.Parameters.AddWithValue("@BirthDay", stuMod.StuBirthday1);
                command1.Parameters.AddWithValue("@Address", stuMod.StuAddress1);
                command1.Parameters.AddWithValue("@Mobile", stuMod.Stumobile1);
                command1.Parameters.AddWithValue("@ClassId", stuMod.StuClassID1);
                command1.Parameters.AddWithValue("@StudentImage", stuMod.ImageData);

                int i = command1.ExecuteNonQuery();

               
                SqlCommand command2 = new SqlCommand();

                command2.Connection = sqlconn;
                command2.CommandType = CommandType.Text;

                command2.CommandText = "INSERT into Parent_Master (Reg_No,Father_Name,Father_ContactNo,Father_Occupation,Mother_Name,Mother_ContactNo,Mother_Occupation) VALUES (@RegNo, @FName, @FContact, @FOccup, @MName, @MContact, @MOccup)";

                command2.Parameters.AddWithValue("@RegNo", stuMod.RegNo1);
                command2.Parameters.AddWithValue("@FName", stuMod.Fname1);
                command2.Parameters.AddWithValue("@FContact", stuMod.FContact1);
                command2.Parameters.AddWithValue("@FOccup", stuMod.FOccupation1);
                command2.Parameters.AddWithValue("@MName", stuMod.Mname1);
                command2.Parameters.AddWithValue("@MContact", stuMod.MContact1);
                command2.Parameters.AddWithValue("@MOccup", stuMod.MOccupation1);
               
                int x = command2.ExecuteNonQuery();

                if (i > 0 && x > 0)
                {
                    status = true;
                    MessageBox.Show("Successfully Registered..!", "Register Customer", MessageBoxButtons.OK);
                }
                else
                {
                    MessageBox.Show("not saved!", "Register Customer", MessageBoxButtons.OK);
                }



                sqlconn.Close();
            }
            catch (Exception ex)
            {
                //ec.ErrorLogging(ex, System.Environment.MachineName.ToString());
                MessageBox.Show(ex.ToString());
            }
            return status;
        }
        //show Data Gridview
        internal void loadGridUsingAllstu(DataGridView dataGridView1, Student_Model stuMod)
        {
            try
            {
                string query = "SELECT DISTINCT * FROM Student_Master WHERE Reg_No LIKE '" + stuMod.RegNo1 + "%' AND Student_Name LIKE '" + stuMod.StuName1 + "%' AND Class_ID LIKE '" + stuMod.StuClassID1 + "%'  order by Reg_No";


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
                        dataGridView1.Rows[n].Cells[2].Value = result["Birthday"].ToString();                  
                        dataGridView1.Rows[n].Cells[3].Value = result["Address"].ToString();               
                        dataGridView1.Rows[n].Cells[4].Value = result["Mobile_No"].ToString();
                        dataGridView1.Rows[n].Cells[5].Value = result["Class_ID"].ToString();
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




        //StudentUpdate
        public bool editStudent(Student_Model stuMod)
        {
            bool status = false;
            try
            {
                if (sqlconn.State.ToString() != "Open")
                {
                    sqlconn.Open();
                }
                SqlCommand command1 = new SqlCommand();               

                command1.Connection = sqlconn;
                command1.CommandType = CommandType.Text;


                command1.CommandText = "UPDATE Student_Master SET Reg_No=@RegNo, Student_Name =@StudentName, Birthday =@BirthDay, Address =@Address, Mobile_No =@Mobile, Class_ID =@ClassId, Student_Image =@StudentImage where Reg_No=@RegNo";

                command1.Parameters.AddWithValue("@RegNo", stuMod.RegNo1);
                command1.Parameters.AddWithValue("@StudentName", stuMod.StuName1);
                command1.Parameters.AddWithValue("@BirthDay", stuMod.StuBirthday1);
                command1.Parameters.AddWithValue("@Address", stuMod.StuAddress1);
                command1.Parameters.AddWithValue("@Mobile", stuMod.Stumobile1);
                command1.Parameters.AddWithValue("@ClassId", stuMod.StuClassID1);
                command1.Parameters.AddWithValue("@StudentImage", stuMod.ImageData);

                int i = command1.ExecuteNonQuery();


                SqlCommand command2 = new SqlCommand();

                command2.Connection = sqlconn;
                command2.CommandType = CommandType.Text;

                command2.CommandText = "UPDATE Parent_Master SET Reg_No =@RegNo, Father_Name =@FName, Father_ContactNo =@FContact, Father_Occupation =@FOccup, Mother_Name =@MName, Mother_ContactNo =@MContact, Mother_Occupation =@MOccup where Reg_No=@RegNo";

                command2.Parameters.AddWithValue("@RegNo", stuMod.RegNo1);
                command2.Parameters.AddWithValue("@FName", stuMod.Fname1);
                command2.Parameters.AddWithValue("@FContact", stuMod.FContact1);
                command2.Parameters.AddWithValue("@FOccup", stuMod.FOccupation1);
                command2.Parameters.AddWithValue("@MName", stuMod.Mname1);
                command2.Parameters.AddWithValue("@MContact", stuMod.MContact1);
                command2.Parameters.AddWithValue("@MOccup", stuMod.MOccupation1);

                int x = command2.ExecuteNonQuery();

                if (i > 0 && x > 0)
                {
                    MessageBox.Show("Successfully Updated..!", "Update Student", MessageBoxButtons.OK);
                    status = true;
                }

                sqlconn.Close();
            }
            catch (Exception ex)
            {
                //ec.ErrorLogging(ex, System.Environment.MachineName.ToString());
                MessageBox.Show(ex.ToString());
            }
            return status;
        }

        //GetData to Table
        public DataSet loadStuDetailsToUpdate(Student_Model stuMod)
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
                    string url = "SELECT s.Reg_No,s.Student_Name,s.Birthday,s.Address,s.Mobile_No,s.Class_ID,s.Student_Image,p.Father_Name,p.Father_ContactNo,p.Father_Occupation,p.Mother_Name,p.Mother_ContactNo,p.Mother_Occupation FROM Student_Master s INNER JOIN Parent_Master p ON  s.Reg_No = p.Reg_No where s.Reg_No='"+stuMod.RegNo1 + "'; ";
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

        public void getStudentDetails(Student_Model stuMod, DataGridView dataGridView1)
        {
            try
            {
                if (sqlconn.State.ToString() != "Open")
                {
                    sqlconn.Open();
                }
                string query = "SELECT * FROM Student_Master WHERE Reg_No LIKE '" + stuMod.RegNo1 + "%'";

                SqlCommand cmd = new SqlCommand(query, sqlconn);
                SqlDataReader result = cmd.ExecuteReader();
                dataGridView1.Rows.Clear();

                while (result.Read())
                {
                    int n = dataGridView1.Rows.Add();
                    dataGridView1.Rows[n].Cells[0].Value = result["Reg_No"].ToString();
                    dataGridView1.Rows[n].Cells[1].Value = result["Student_Name"].ToString();
                    dataGridView1.Rows[n].Cells[2].Value = result["Birthday"].ToString();
                    dataGridView1.Rows[n].Cells[3].Value = result["Address"].ToString();
                    dataGridView1.Rows[n].Cells[4].Value = result["Mobile_No"].ToString();
                    dataGridView1.Rows[n].Cells[5].Value = result["Class_ID"].ToString();
                    dataGridView1.Rows[n].Cells[6].Value = result["Student_Image"].ToString();

                }
                sqlconn.Close();
            }
            catch (Exception ex)
            {
                //ec.ErrorLogging(ex, System.Environment.MachineName.ToString());
                MessageBox.Show(ex.ToString());
            }

        }

     

        //load gridview parent table
        
        internal void loadGridAllstu(DataGridView dataGridView1, Student_Model stuMod)
        {

            try
            {
                string query = "SELECT DISTINCT * FROM Parent_Master WHERE Reg_No LIKE '" + stuMod.RegNo1 + "%' /*AND Student_Name LIKE '" + stuMod.StuName1 + "%' AND Class_ID LIKE '" + stuMod.StuClassID1 + "%'  order by Reg_No*/";


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
                        dataGridView1.Rows[n].Cells[1].Value = result["Father_Name"].ToString();
                        dataGridView1.Rows[n].Cells[2].Value = result["Father_ContactNo"].ToString();
                        dataGridView1.Rows[n].Cells[3].Value = result["Father_Occupation"].ToString();
                        dataGridView1.Rows[n].Cells[4].Value = result["Mother_Name"].ToString();
                        dataGridView1.Rows[n].Cells[5].Value = result["Mother_ContactNo"].ToString();
                        dataGridView1.Rows[n].Cells[6].Value = result["Mother_Occupation"].ToString();
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

