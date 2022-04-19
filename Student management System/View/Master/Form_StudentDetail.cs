using CrystalDecisions.CrystalReports.Engine;
using Student_management_System.Controller;
using Student_management_System.Dataset;
using Student_management_System.View.Main;
using Student_management_System.View.Student;
using System;
using System.Data;
using System.IO;
using System.Windows.Forms;

namespace Student_management_System.View.Master
{
    public partial class Form_StudentDetail : Form
    {
        Student_Model stuMod = new Student_Model();
        Student_Controller stuCon = new Student_Controller();
       

        public Form_StudentDetail()
        {
            InitializeComponent();
            loadGrid();
        }

        private void button_add_Click(object sender, EventArgs e)
        {
            Form_StudentAdd newChild = new Form_StudentAdd();
            newChild.MdiParent = Form_Main.ActiveForm;
            newChild.Show();
        }

        private void button_cancel_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        public void loadGrid()
        {
            stuMod.RegNo1 = textBox_regno.Text;
            stuMod.StuName1 = textBox_stuname.Text;
            stuMod.StuClassID1 = textBox_classid.Text;
            stuCon.loadGridUsingAllstu(dataGridView1, stuMod);
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            
            string RegNo = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();



            Form_StudentAdd newChild = new Form_StudentAdd(RegNo);
            newChild.MdiParent = Form_Main.ActiveForm;
            newChild.Show();
        }

        private void Form_StudentDetail_Load(object sender, EventArgs e)
        {
           
        }

        private void textBox_regno_TextChanged(object sender, EventArgs e)
        {
            loadGrid();
        }

        private void textBox_stuname_TextChanged(object sender, EventArgs e)
        {
            loadGrid();
        }

        private void textBox_classid_TextChanged(object sender, EventArgs e)
        {
            loadGrid();
        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
           
        }

        private void button_print_Click(object sender, EventArgs e)
        {
            try
            {
                ReportDocument rptDoc = new ReportDocument();

                DataTable dt_StudentDetail = new DataTable();
                //dt_StudentDetail.Columns.Add("Reg_No", typeof(string));
                dt_StudentDetail.Columns.Add("Student_Name", typeof(string));
                //dt_StudentDetail.Columns.Add("Birthday", typeof(string));
                //dt_StudentDetail.Columns.Add("Address", typeof(string));
                //dt_StudentDetail.Columns.Add("Class_ID", typeof(string));
                dt_StudentDetail.Columns.Add("Mobile_No", typeof(string));

                for (int i = 0; i < dataGridView1.RowCount; i++)
                {
                    dt_StudentDetail.Rows.Add(
                        //dataGridView1.Rows[i].Cells[0].Value.ToString(),
                        dataGridView1.Rows[i].Cells[1].Value.ToString(),
                        //dataGridView1.Rows[i].Cells[2].Value.ToString(),
                        //dataGridView1.Rows[i].Cells[3].Value.ToString(),
                        //dataGridView1.Rows[i].Cells[5].Value.ToString(),
                        dataGridView1.Rows[i].Cells[4].Value.ToString()
                        );
                }

                DataSet_Studentsdetail ds_student = new DataSet_Studentsdetail();
                ds_student.Tables[0].Merge(dt_StudentDetail);
                string path = Path.Combine(Environment.CurrentDirectory, @".\Report\CrystalReport1.rpt");

                rptDoc.Load(path);
                rptDoc.DataSourceConnections.Clear();
                rptDoc.SetDataSource(ds_student);

                //rptDoc.SetParameterValue("Title", "Student Details");

                crystalReportViewer1.ReportSource = rptDoc;

                crystalReportViewer1.Visible = true;
            }
            catch (Exception ex)
            {
                //ec.ErrorLogging(ex, System.Environment.MachineName.ToString());
                MessageBox.Show(ex.ToString());
            }
        }

        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {

        }
    }
}
