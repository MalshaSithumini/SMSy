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
    public partial class Form_UserMaster : Form
    {
        User_Model um = new User_Model();
        User_Controller userCon = new User_Controller();
        
        public Form_UserMaster()
        {
            InitializeComponent();
            loadGrid();
        }
        public void loadGrid()
        {
            um.UserID = textBox_userid.Text;
            //um.StuName1 = textBox_stname.Text;
            //stuMod.StuClassID1 = textBox_classid.Text;
            userCon.loadGridUsingAlluser(dataGridView1, um);
        }

        private void textBox_userid_TextChanged(object sender, EventArgs e)
        {
            loadGrid();
        }
    }
}
