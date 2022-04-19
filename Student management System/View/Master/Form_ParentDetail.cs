using Student_management_System.Controller;
using Student_management_System.View.Main;
using Student_management_System.View.Student;
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
    public partial class Form_ParentDetail : Form
    {
        Student_Model stuMod = new Student_Model();
        Student_Controller stuCon = new Student_Controller();
        
        public Form_ParentDetail()
        {
            InitializeComponent();
            loadGrid();
        }
        public void loadGrid()
        {
            stuMod.RegNo1 = textBox_regno.Text;
            //stuMod.StuName1 = textBox_stname.Text;
            //stuMod.StuClassID1 = textBox_classid.Text;
            stuCon.loadGridAllstu(dataGridView1, stuMod);
        }

        private void textBox_regno_TextChanged(object sender, EventArgs e)
        {
            loadGrid();
        }

        private void button_cancel_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            string RegNo = dataGridView1.SelectedRows[0].Cells[0].Value.ToString(); ;


            Form_StudentAdd newChild = new Form_StudentAdd(RegNo);
            newChild.MdiParent = Form_Main.ActiveForm;
            newChild.Show();
        }
    }
}
