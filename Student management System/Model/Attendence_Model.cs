using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Student_management_System.Model
{
    class Attendence_Model
    {
        private string regNo;
        private string date;
        private string status;

        public string RegNo
        {
            get
            {
                return regNo;
            }

            set
            {
                regNo = value;
            }
        }

        public string Date
        {
            get
            {
                return date;
            }

            set
            {
                date = value;
            }
        }

        public string Status
        {
            get
            {
                return status;
            }

            set
            {
                status = value;
            }
        }
    }
}
