using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DSR_System
{
    public partial class DelRpDtSelectForm : Form
    {
        public DelRpDtSelectForm()
        {
            InitializeComponent();
        }


        public DateTime FromDate
        {
            get { return dtpFrom.Value; }
        }

        public DateTime ToDate
        {
            get { return dtpTo.Value; }
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
