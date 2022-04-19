using Student_management_System.Controller;
using Student_management_System.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Student_management_System.View.Master
{
    public partial class Form_ClassMaster : Form
    {
        Class_Model classMod = new Class_Model();
       Class_Controller classCon = new Class_Controller();

        bool updateStat = false;
        public Form_ClassMaster()
        {
            InitializeComponent();

            classCon.Classid(classMod);
            textBox_classid.Text = classMod.ClassID1;
            textBox_classname.Focus();
            
            classCon.getClassDetails(classMod, dataGridView1);

        }

       

        private void textBox_classname_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                if (textBox_classname.Text != "")
                {
                    if (updateStat == false)
                    {
                        button_add.Enabled = true;
                        button_add.Focus();
                    }
                    else
                    {
                        button_edit.Enabled = true;
                        button_edit.Focus();
                    }
                }
                else
                {
                    MessageBox.Show("Enter Class Name!", "Required Feild.", MessageBoxButtons.OK);
                }
            }
        }

        private void textBox_classid_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyData == Keys.Enter)
            //{
            //    if (textBox_classid.Text != "")
            //    {
            //        if (updateStat == false)
            //        {
            //            textBox_classname.Enabled = true;
            //            textBox_classname.Focus();
            //        }
                  
            //    }
            //    else
            //    {
            //        MessageBox.Show("Enter Class ID!", "Required Feild.", MessageBoxButtons.OK);
            //    }
            //}
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox_classid.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            textBox_classname.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();

            button_edit.Focus();
            button_add.Visible = false;
            button_add.Enabled = false;
            updateStat = true;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void button_add_Click(object sender, EventArgs e)
        {

            if (textBox_classid.Text == "" || textBox_classname.Text == "")
            {
                MessageBox.Show("Enter Class ID and Class Name");
            }
            {
                classMod.ClassID1 = textBox_classid.Text;
                classMod.ClassName1 = textBox_classname.Text;

                classCon.insertClass(classMod);

                classMod.ClassName1 = textBox_classname.Text = "";
                classCon.getClassDetails(classMod, dataGridView1);

                textBox_classid.Text = "";
                button_add.Enabled = false;
                classCon.Classid(classMod);
                textBox_classid.Text = classMod.ClassID1;
                textBox_classname.Focus();

            }

            textBox_classid.Text = "";
            textBox_classname.Text = "";

        }

        private void button_edit_Click(object sender, EventArgs e)
        {
            classMod.ClassID1 = textBox_classid.Text;
            classMod.ClassName1 = textBox_classname.Text;


            classCon.editClass(classMod);
            //classMod.ClassName1 = "";
            classCon.getClassDetails(classMod, dataGridView1);

            button_add.Enabled = false;
            //classCon.classid(classMod);
            textBox_classid.Text = classMod.ClassID1;
            textBox_classname.Text = "";
            textBox_classname.Focus();

            button_add.Visible = true;
            button_edit.Enabled = false;
            updateStat = false;

            textBox_classid.Text = "";
            textBox_classname.Text = "";
            textBox_searchcid.Text = "";
        }

        private void textBox_searchcid_TextChanged(object sender, EventArgs e)
        {
            classMod.ClassName1 = textBox_searchcid.Text;
            classCon.getClassDetails(classMod, dataGridView1);
        }

        private void button_cancel_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

       
    }
}
