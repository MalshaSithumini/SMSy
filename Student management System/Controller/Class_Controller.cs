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
    class Class_Controller
    {
        DB_Controller DbCon = new DB_Controller();
        SqlConnection sqlconn;
        Class_Model classMod = new Class_Model();

        public Class_Controller()
        {
            sqlconn = DbCon.getDBConnection();
        }

        public void Classid(Class_Model classMod)
        {
            classMod.ClassID1 = "0000";

            try
            {

                if (sqlconn.State.ToString() != "Open")
                {
                    sqlconn.Open();
                }
                string query = "select MAX(Class_ID) as MaxCode from Class_Master";

                SqlCommand cmd = new SqlCommand(query, sqlconn);
                SqlDataReader result = cmd.ExecuteReader();

                while (result.Read())
                {
                    if (result["MaxCode"].ToString() == "")
                    {
                        classMod.ClassID1 = "0001";
                    }
                    else
                    {
                        classMod.ClassID1 = (Convert.ToInt16(result["MaxCode"]) + 1).ToString("0000");
                    }
                }
                sqlconn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                //ec.ErrorLogging(ex, Environment.MachineName.ToString());
            }
            //return Class_ID;
        }
        //insert class
        public void insertClass(Class_Model classMod)
        {
            try
            {
                if (sqlconn.State.ToString() != "Open")
                {
                    sqlconn.Open();
                }

                string query = "INSERT INTO Class_Master(Class_ID, Class_Name) VALUES ('" + classMod.ClassID1 + "', '" + classMod.ClassName1 + "')";

                SqlCommand cmd = new SqlCommand(query, sqlconn);
                int i = cmd.ExecuteNonQuery();

                if (i == 1)
                {
                    MessageBox.Show("Successfully Added..!", "Add Class", MessageBoxButtons.OK);
                }
                else
                {
                    MessageBox.Show("Error..!", "Add Class", MessageBoxButtons.OK);
                }
                sqlconn.Close();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }

            
        }
        //show data to gridview
        public void getClassDetails(Class_Model classMod, DataGridView dataGridView1)
        {
            try
            {
                if (sqlconn.State.ToString() != "Open")
                {
                    sqlconn.Open();
                }
                string query = "SELECT * FROM Class_Master WHERE Class_Name LIKE '" + classMod.ClassName1 + "%'";

                SqlCommand cmd = new SqlCommand(query, sqlconn);
                SqlDataReader result = cmd.ExecuteReader();
                dataGridView1.Rows.Clear();

                while (result.Read())
                {
                    int n = dataGridView1.Rows.Add();
                    dataGridView1.Rows[n].Cells[0].Value = result["Class_ID"].ToString();
                    dataGridView1.Rows[n].Cells[1].Value = result["Class_Name"].ToString();
                    //result.Close();
                }
               
                sqlconn.Close();
            }
            catch (Exception ex)
            {
                //ec.ErrorLogging(ex, System.Environment.MachineName.ToString());
                MessageBox.Show(ex.ToString());
            }

        }

       
        //edit class
        public void editClass(Class_Model classdMod)
        {
            try
            {
                if (sqlconn.State.ToString() != "Open")
                {
                    sqlconn.Open();
                }

                string query = "UPDATE Class_Master SET Class_Name = '" + classMod.ClassName1 + "' WHERE Class_ID = '" + classMod.ClassID1 + "'";

                SqlCommand cmd = new SqlCommand(query, sqlconn);
                int i = cmd.ExecuteNonQuery();

                if (i == 1)
                {
                    MessageBox.Show("Successfully Updated..!", "Update Class", MessageBoxButtons.OK);
                }
                else
                {
                    MessageBox.Show("Error..!", "Update Class", MessageBoxButtons.OK);
                }
                sqlconn.Close();
            }
            catch (Exception ex)
            {
                //ec.ErrorLogging(ex, System.Environment.MachineName.ToString());
                MessageBox.Show(ex.ToString());
            }
        }

        public string getClassIdByName(Class_Model classMod)
        {
            string classId = "";
            if (sqlconn.State.ToString() != "Open")
            {
                sqlconn.Open();
            }
            try
            {
                string query = "SELECT Class_ID FROM Class_Master where Class_Name = '" + classMod.ClassName1 + "'";
                SqlCommand cmd = new SqlCommand(query, sqlconn);
                SqlDataReader result = cmd.ExecuteReader();

                while (result.Read())
                {
                    classId = result["Class_ID"].ToString();
                    //result.Close();
                }

                sqlconn.Close();
            }
            catch (Exception ex)
            {
                //ec.ErrorLogging(ex, System.Environment.MachineName.ToString());
                MessageBox.Show(ex.ToString());
            }
            return classId;
        }


       


    }

}
