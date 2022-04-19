using CrystalDecisions.CrystalReports.Engine;
using Student_management_System.Controller;
using Student_management_System.Dataset;
using Student_management_System.Model;
using Student_management_System.Report;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Student_management_System.View.Attendence
{
    public partial class Form_Attendence : Form
    {
        Attendence_Model attMod = new Attendence_Model();
        Attendence_Controller attCon = new Attendence_Controller();
        SqlConnection sqlconn;
        Student_Model stuMod = new Student_Model();
        DB_Controller DbCon = new DB_Controller();


        public Form_Attendence()
        {
            InitializeComponent();
            sqlconn = DbCon.getDBConnection();
            loadGrid();

        }

        public void loadGrid()
        {
            stuMod.RegNo1 = textBox_searchrno.Text;
            //stuMod.StuName1 = textBox_classid.Text;
            stuMod.StuClassID1 = textBox_classid.Text;
            attCon.loadGridUsingAllatten(dataGridView1, stuMod);
        }

        private void textBox_searchrno_TextChanged(object sender, EventArgs e)
        {
            loadGrid();
        }

        private void textBox_stname_TextChanged(object sender, EventArgs e)
        {
            loadGrid();
        }
        //bool status = false;
        private void button_save_Click(object sender, EventArgs e)
        {

            try
            {
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    string staus = "0";

                    DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)dataGridView1.Rows[i].Cells[3];
                    if (chk.Selected)
                    {
                        staus = "1";
                    }

                    attMod.RegNo = dataGridView1.Rows[i].Cells[0].Value.ToString();
                    attMod.Date = DateTime.Now.ToString("yyyy-MM-dd");
                    attMod.Status = staus;

                    //string date = dataGridView1.Rows[i].Cells[2].ToString();

                    sqlconn.Open();
                    string insertquery = "INSERT into Attendence_Detail(Reg_No,Date,Attendence_Status) VALUES (@RegNo, @Date, @Status)";
                    SqlCommand cmd = new SqlCommand(insertquery, sqlconn);
                    cmd.Parameters.AddWithValue("@RegNo", attMod.RegNo);
                    cmd.Parameters.AddWithValue("@Date", attMod.Date);
                    cmd.Parameters.AddWithValue("@Status", attMod.Status);

                    cmd.ExecuteNonQuery();
                    {
                        MessageBox.Show("Attendence Add Successfully");
                    }
                    sqlconn.Close();

                    this.Dispose();
                   
                }
                    
                
                

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void button_print_Click(object sender, EventArgs e)
        {
            try
            {
                ReportDocument rptDoc = new ReportDocument();

                DataTable dt_Attendence = new DataTable();
                dt_Attendence.Columns.Add("Reg_No", typeof(string));
                dt_Attendence.Columns.Add("Student_Name", typeof(string));
                dt_Attendence.Columns.Add("Class_ID", typeof(string));
                //dt_Attendence.Columns.Add("Attendence_Status", typeof(string));               
                //dt_Attendence.Columns.Add("Date", typeof(string));
                //dt_StudentDetail.Columns.Add("Mobile_No", typeof(string));

                for (int i = 0; i < dataGridView1.RowCount; i++)
                {
                    dt_Attendence.Rows.Add(
                        dataGridView1.Rows[i].Cells[0].Value.ToString(),
                        dataGridView1.Rows[i].Cells[1].Value.ToString(),
                        dataGridView1.Rows[i].Cells[2].Value.ToString()
                        //dataGridView1.Rows[i].Cells[3].Value.ToString()
                        //dataGridView1.Rows[i].Cells[5].Value.ToString(),
                        //dataGridView1.Rows[i].Cells[4].Value.ToString()
                        );
                }
                
                DataSet_attendence ds_atten = new DataSet_attendence();
                ds_atten.Tables[0].Merge(dt_Attendence);
                string path = Path.Combine(Environment.CurrentDirectory, @".\Report\CrystalReport_Attendence.rpt");

                rptDoc.Load(path);
                rptDoc.DataSourceConnections.Clear();
                rptDoc.SetDataSource(ds_atten);

                //CrystalReport_Attendence.RecordSelectionFormula = "month({TicketTranc.TranscDate}) = 5 ";
                //rptDoc.SetParameterValue("Title", "Student Details");

                //crystalReportViewer1.ReportSource = rptDoc;

                //crystalReportViewer1.Visible = true;
            }
            catch (Exception ex)
            {
                //ec.ErrorLogging(ex, System.Environment.MachineName.ToString());
                MessageBox.Show(ex.ToString());
            }
        }
    }


}




