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
    public partial class ucChart : UserControl
    {
        private static ucChart Chart_Instance;
        public static ucChart ChartFunc
        {
            get
            {
                Chart_Instance = null;
                if (Chart_Instance == null)
                    Chart_Instance = new ucChart();
                return Chart_Instance;
            }
        }

        public ucChart()
        {
            InitializeComponent();
        }
    }
}
