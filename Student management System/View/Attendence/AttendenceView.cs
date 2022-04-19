using Student_management_System.Controller;
using Student_management_System.Model;
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

namespace Student_management_System.View.Attendence
{
    public partial class Form_AttendenceView : Form
    {
        DB_Controller DbCon = new DB_Controller();
        Attendence_Model attMod = new Attendence_Model();
        Attendence_Controller attCon = new Attendence_Controller();
        SqlConnection sqlconn;
        Student_Model stuMod = new Student_Model();
      
        public Form_AttendenceView()
        {
            InitializeComponent();
            sqlconn = DbCon.getDBConnection();
           
        }

        private void button_view_Click(object sender, EventArgs e)
        {
            try
            {
                if (sqlconn.State.ToString() != "Open")
                {
                    sqlconn.Open();
                }

                SqlCommand sqlCmd = new SqlCommand();
                sqlCmd.Connection = sqlconn;
                sqlCmd.CommandType = CommandType.Text;

                sqlCmd.CommandText=" SELECT a.Date,s.Student_Name,a.Attendence_Status FROM Student_Master s INNER JOIN Attendence_Detail a  ON  s.Reg_No = a.Reg_No where a.date between '" + dateTimePicker_st.Text + "' and '" + dateTimePicker_end.Text + "';";

                //SqlDataAdapter sqlDataAdap = new SqlDataAdapter(sqlCmd);
                //DataSet ds = new DataSet();
                //sqlDataAdap.Fill(ds);

                SqlDataReader result = sqlCmd.ExecuteReader();
                dataGridView1.Rows.Clear();

                while (result.Read())
                {

                    int n = dataGridView1.Rows.Add();
                  
                    dataGridView1.Rows[n].Cells[0].Value = result["Date"].ToString();
                    dataGridView1.Rows[n].Cells[1].Value = result["Student_Name"].ToString();

                    string stat = "Absent";
                    if (result["Attendence_Status"].ToString() == "True") {
                        stat = "Present";
                    }

                    dataGridView1.Rows[n].Cells[2].Value = stat;

                }
                sqlconn.Close();
            }
            catch (Exception ex)
            {
                //ec.ErrorLogging(ex, System.Environment.MachineName.ToString());
                MessageBox.Show(ex.ToString());
            }

        }

        private void loadGridUsingAllatten(DataGridView dataGridView1, Student_Model stuMod)
        {
           

        }
    }
    }
