using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Student_management_System.Controller
{
    class DB_Controller
    {
        SqlConnection conn;

        public SqlConnection getDBConnection()
        {
            try
            {
                #region ORIGINAL DB
                string conString = @"Data Source=DESKTOP-9P855LF\SQLEXPRESS;Initial Catalog=ABC_School;User ID=Malsha97;Password=12345;";
                #endregion

                #region TEST DB
                //string conString = @"Data Source=DESKTOP-L1H8DUA\SQLEXPRESS;Initial Catalog=Test_OXYpharamacySysData;Integrated Security=True;";
                #endregion

                #region DEV DB
                //string conString = @"Data Source=DESKTOP-PNIPOHC\SQLEXPRESS;Initial Catalog=OXYpharamacySysData;Integrated Security=True;";
                #endregion

                conn = new SqlConnection(conString);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                //ec.ErrorLogging(ex, Environment.MachineName.ToString());
            }
            return conn;
        }
    }
}
