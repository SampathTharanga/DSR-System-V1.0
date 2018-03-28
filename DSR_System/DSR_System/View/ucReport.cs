using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DSR_System
{
    public partial class ucReport : UserControl
    {
        private static ucReport Report_Instance;
        public static ucReport ReportFunc
        {
            get
            {
                Report_Instance = null;
                if (Report_Instance == null)
                    Report_Instance = new ucReport();
                return Report_Instance;
            }
        }

        public ucReport()
        {
            InitializeComponent();
        }
    }
}
