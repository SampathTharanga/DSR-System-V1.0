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
    public partial class ucAbout : UserControl
    {
        private static ucAbout About_Instance;
        public static ucAbout AboutFunc
        {
            get
            {
                About_Instance = null;
                if (About_Instance == null)
                    About_Instance = new ucAbout();
                return About_Instance;
            }
        }

        public ucAbout()
        {
            InitializeComponent();
        }
    }
}
