
using CrystalDecisions.CrystalReports.Engine;
using Student_management_System.Controller;
using Student_management_System.Dataset;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Student_management_System.View.Student
{
    public partial class Form_StudentAdd : Form
    {
        Student_Controller stuCon = new Student_Controller();
        Student_Model stuMod = new Student_Model();
        DB_Controller DbCon = new DB_Controller();
        SqlConnection sqlconn;

        public Form_StudentAdd()
        {
            InitializeComponent();
            sqlconn = DbCon.getDBConnection();
        }

        bool updateStat = false;
        public Form_StudentAdd(string RegNo)
        {
            InitializeComponent();

            updateStat = true;
            button_save.Visible = false;
            button_update.Visible = true;

            stuMod.RegNo1 = RegNo;
            DataSet dsStuSearch = stuCon.loadStuDetailsToUpdate(stuMod);


            if (dsStuSearch.Tables[0].Rows.Count > 0)
            {

                textBox_regno.Text = dsStuSearch.Tables[0].Rows[0][0].ToString();
                textBox_stname.Text = dsStuSearch.Tables[0].Rows[0][1].ToString();
                textBox_bd.Text = dsStuSearch.Tables[0].Rows[0][2].ToString();
                textBox_address.Text = dsStuSearch.Tables[0].Rows[0][3].ToString();
                textBox_mobileno.Text = dsStuSearch.Tables[0].Rows[0][4].ToString();
                textBox_classid.Text = dsStuSearch.Tables[0].Rows[0][5].ToString();

                textBox_fname.Text = dsStuSearch.Tables[0].Rows[0][7].ToString();
                textBox_fcontactno.Text = dsStuSearch.Tables[0].Rows[0][8].ToString();
                textBox_foccupation.Text = dsStuSearch.Tables[0].Rows[0][9].ToString();
                textBox_mname.Text = dsStuSearch.Tables[0].Rows[0][10].ToString();
                textBox_mcontactno.Text = dsStuSearch.Tables[0].Rows[0][11].ToString();
                textBox_moccupation.Text = dsStuSearch.Tables[0].Rows[0][12].ToString();

                view_photo((byte[])dsStuSearch.Tables[0].Rows[0][6]);

            }
        }

        private void textBox_regno_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                if (textBox_regno.Text != "")
                {
                    textBox_stname.Enabled = true;
                    textBox_stname.Focus();
                }
                else
                {
                    MessageBox.Show("Enter Registration No!", "Required Field.", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        private void textBox_stname_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                if (textBox_stname.Text != "")
                {
                    textBox_bd.Enabled = true;
                    textBox_bd.Focus();
                }
                else
                {
                    MessageBox.Show("Enter Student Name!", "Required Field.", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }


        }

        private void textBox_address_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox_address_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                if (textBox_address.Text != "")
                {
                    textBox_mobileno.Enabled = true;
                    textBox_mobileno.Focus();
                }
                else
                {
                    MessageBox.Show("Enter Address!", "Required Field.", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        private void textBox_classid_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyData == Keys.Enter)
            {
                if (textBox_mobileno.Text != "")
                {
                    button_select.Enabled = true;
                    button_select.Focus();
                }
                else
                {
                    MessageBox.Show("Enter Class ID!", "Required Field.", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }
        //select button
        private void button1_Click(object sender, EventArgs e)
        {
            Stream mystream = null;
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image File(*.jpg;*.jpeg;*.bmp)|*.jpg;*.jpeg;*.bmp";

            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                try
                {
                    if ((mystream = openFileDialog.OpenFile()) != null)
                    {
                        string FileName = openFileDialog.FileName;
                        if (mystream.Length > 512000)
                        {
                            MessageBox.Show("File Size Limit Exceeded");
                        }
                        else
                        {
                            pictureBox1.Load(FileName);
                        }
                    }
                }
                catch (Exception ex)
                {
                    //ec.ErrorLogging(ex, System.Environment.MachineName.ToString());
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        private void dateTimePicker1_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void textBox_address1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox_address1_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyData == Keys.Enter)
            //{
            //    if (textBox_address.Text != "")
            //    {
            //        textBox_mobileno.Enabled = true;
            //        textBox_mobileno.Focus();
            //    }
            //    else
            //    {
            //        MessageBox.Show("Enter Address!", "Required Field.", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //    }
            //}
        }

        private void textBox_mobileno_KeyDown(object sender, KeyEventArgs e)
        {
            string str = textBox_mobileno.Text;

            if (e.KeyData == Keys.Enter)
            {
                if (str != "")
                {
                    if ((str.Count(char.IsDigit) == 10) /*&& !str.StartsWith("0")*/)
                    {
                        textBox_classid.Enabled = true;
                        textBox_classid.Focus();
                    }
                    else
                    {
                        MessageBox.Show("Enter valid Mobile Number!", "Invalid Details.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        textBox_mobileno.Text = "";
                        textBox_mobileno.Focus();
                    }
                }
                else
                {
                    MessageBox.Show("Enter Mobile Number!", "Required Field.", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    textBox_mobileno.Text = "";
                    textBox_mobileno.Focus();
                }

            }
        }

        private void button_select_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                if (button_select.Text != "")
                {
                    textBox_fname.Enabled = true;
                    textBox_fname.Focus();
                }
                else
                {
                    MessageBox.Show("Enter Image!", "Required Field.", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        private void textBox_fname_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                if (textBox_fname.Text != "")
                {
                    textBox_fcontactno.Enabled = true;
                    textBox_fcontactno.Focus();
                }
                else
                {
                    MessageBox.Show("Enter Father Name!", "Required Field.", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        private void textBox_fcontactno_KeyDown(object sender, KeyEventArgs e)
        {
            string str = textBox_fcontactno.Text;

            if (e.KeyData == Keys.Enter)
            {
                if (str != "")
                {
                    if ((str.Count(char.IsDigit) == 10) /*&& !str.StartsWith("0")*/)
                    {
                        textBox_foccupation.Enabled = true;
                        textBox_foccupation.Focus();
                    }
                    else
                    {
                        MessageBox.Show("Enter valid Mobile Number!", "Invalid Details.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        textBox_fcontactno.Text = "";
                        textBox_fcontactno.Focus();
                    }
                }
                else
                {
                    MessageBox.Show("Enter Mobile Number!", "Required Field.", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    textBox_fcontactno.Text = "";
                    textBox_fcontactno.Focus();
                }

            }
        }

        private void textBox_foccupation_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                if (textBox_foccupation.Text != "")
                {
                    textBox_mname.Enabled = true;
                    textBox_mname.Focus();
                }
                else
                {
                    MessageBox.Show("Enter Father Occupation!", "Required Field.", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        private void textBox_mname_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                if (textBox_mname.Text != "")
                {
                    textBox_mcontactno.Enabled = true;
                    textBox_mcontactno.Focus();
                }
                else
                {
                    MessageBox.Show("Enter Mother Name!", "Required Field.", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        private void textBox_mcontactno_KeyDown(object sender, KeyEventArgs e)
        {
            string str = textBox_mcontactno.Text;

            if (e.KeyData == Keys.Enter)
            {
                if (str != "")
                {
                    if ((str.Count(char.IsDigit) == 10) /*&& !str.StartsWith("0")*/)
                    {
                        textBox_moccupation.Enabled = true;
                        textBox_moccupation.Focus();
                    }
                    else
                    {
                        MessageBox.Show("Enter valid Mobile Number!", "Invalid Details.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        textBox_mcontactno.Text = "";
                        textBox_mcontactno.Focus();
                    }
                }
                else
                {
                    MessageBox.Show("Enter Mobile Number!", "Required Field.", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    textBox_mcontactno.Text = "";
                    textBox_mcontactno.Focus();
                }

            }
        }

        private void button_cancel_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void textBox_moccupation_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                if (textBox_mcontactno.Text != "")
                {
                    button_update.Enabled = true;
                    button_update.Focus();
                }
                else
                {
                    MessageBox.Show("Enter Mother Occupation!", "Required Field.", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        private void textBox_bd_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                if (textBox_bd.Text != "")
                {
                    textBox_address.Enabled = true;
                    textBox_address.Focus();
                }
                else
                {
                    MessageBox.Show("Enter Birthday!", "Required Field.", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        private void textBox_mobileno_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        //save method
        private void button_save_Click(object sender, EventArgs e)
        {
            stuMod.RegNo1 = textBox_regno.Text;
            stuMod.StuName1 = textBox_stname.Text;
            stuMod.StuBirthday1 = textBox_bd.Text;
            stuMod.StuAddress1 = textBox_address.Text;
            stuMod.Stumobile1 = textBox_mobileno.Text;
            stuMod.StuClassID1 = textBox_classid.Text;
            //stuMod.ImageData = conv_photo();
            stuMod.Fname1 = textBox_fname.Text;
            stuMod.FContact1 = textBox_fcontactno.Text;
            stuMod.FOccupation1 = textBox_foccupation.Text;
            stuMod.Mname1 = textBox_mname.Text;
            stuMod.MContact1 = textBox_mcontactno.Text;
            stuMod.MOccupation1 = textBox_moccupation.Text;

            stuMod.ImageData = conv_photo();

            stuCon.regStudent(stuMod);

            textBox_regno.Text = "";
            textBox_stname.Text = "";
            textBox_bd.Text = "";
            textBox_address.Text = "";
            textBox_mobileno.Text = "";
            textBox_classid.Text = "";
            pictureBox1.Text = "";
            textBox_fname.Text = "";
            textBox_fcontactno.Text = "";
            textBox_foccupation.Text = "";
            textBox_mname.Text = "";
            textBox_mcontactno.Text = "";
            textBox_moccupation.Text = "";


        }


        private void button_update_Click(object sender, EventArgs e)
        {
            stuMod.RegNo1 = textBox_regno.Text;
            stuMod.StuName1 = textBox_stname.Text;
            stuMod.StuBirthday1 = textBox_bd.Text;
            stuMod.StuAddress1 = textBox_address.Text;
            stuMod.Stumobile1 = textBox_mobileno.Text;
            stuMod.StuClassID1 = textBox_classid.Text;
            stuMod.ImageData = conv_photo();
            stuMod.Fname1 = textBox_fname.Text;
            stuMod.FContact1 = textBox_fcontactno.Text;
            stuMod.FOccupation1 = textBox_foccupation.Text;
            stuMod.Mname1 = textBox_mname.Text;
            stuMod.MContact1 = textBox_mcontactno.Text;
            stuMod.MOccupation1 = textBox_moccupation.Text;

            stuCon.editStudent(stuMod);
            this.Dispose();
        }
        //photoConvert
        private byte[] conv_photo()
        {
            byte[] photo_aray = null;
            if (pictureBox1.Image != null)
            {
                SqlCommand command = new SqlCommand();
                MemoryStream mystream = new MemoryStream();
                pictureBox1.Image.Save(mystream, ImageFormat.Jpeg);
                photo_aray = new byte[mystream.Length];
                mystream.Position = 0;
                mystream.Read(photo_aray, 0, photo_aray.Length);
                command.Parameters.AddWithValue("@StudentImage", photo_aray);
            }
            return photo_aray;
        }

        private void view_photo(byte[] arr)
        {
            try
            {
                MemoryStream stream = new MemoryStream();
                byte[] image = arr;
                stream.Write(image, 0, image.Length);
                Bitmap bitmap = new Bitmap(stream);
                pictureBox1.Image = bitmap;
            }
            catch (Exception ex)
            {
                //ec.ErrorLogging(ex, System.Environment.MachineName.ToString());
                MessageBox.Show(ex.ToString());
            }

        }

        private void button_print_Click(object sender, EventArgs e)
        {
            try
            {
                ReportDocument rptDoc = new ReportDocument();

                string path = Path.Combine(Environment.CurrentDirectory, @".\Report\CrystalReportStudent.rpt");

                rptDoc.Load(path);
                rptDoc.DataSourceConnections.Clear();

                //rptDoc.SetDataSource(dt_StudentInfo);
                rptDoc.SetParameterValue("RegNo", textBox_regno.Text);
                rptDoc.SetParameterValue("Student_Name", textBox_stname.Text);
                rptDoc.SetParameterValue("Birthday", textBox_bd.Text);
                rptDoc.SetParameterValue("Address", textBox_address.Text);
                rptDoc.SetParameterValue("Mobile_No", textBox_mobileno.Text);
                rptDoc.SetParameterValue("Class_ID", textBox_classid.Text);
                //rptDoc.SetParameterValue("Father_Name", textBox_fname.Text);
                //rptDoc.SetParameterValue("Father_ContactNo", textBox_fcontactno.Text);
                //rptDoc.SetParameterValue("Father_Occupation", textBox_foccupation.Text);
                //rptDoc.SetParameterValue("Mother_Name", textBox_mname.Text);
                //rptDoc.SetParameterValue("Mother_ContactNo", textBox_mcontactno.Text);
                //rptDoc.SetParameterValue("Mother_Occcupation", textBox_moccupation.Text);


                //rptDoc.SetParameterValue("Title", "View Books");

                crystalReportViewer1.ReportSource = rptDoc;

                crystalReportViewer1.Visible = true;

            }
            catch (Exception EX)
            {
                MessageBox.Show(EX.ToString());
            }
        }



    }
}
