using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DSR_System
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            pnlMain.Controls.Clear();
            if (!pnlMain.Controls.Contains(ucLoad.LoadFunc))
            {
                pnlMain.Controls.Add(ucLoad.LoadFunc);
                ucLoad.LoadFunc.Dock = DockStyle.Fill;
                ucLoad.LoadFunc.BringToFront();
            }
        }

        private void btnSlideUnload_Click(object sender, EventArgs e)
        {
            pnlMain.Controls.Clear();
            if (!pnlMain.Controls.Contains(ucUnload.Unload))
            {
                pnlMain.Controls.Add(ucUnload.Unload);
                ucUnload.Unload.Dock = DockStyle.Fill;
                ucUnload.Unload.BringToFront();
            }
        }

        private void btnSlideLoad_Click(object sender, EventArgs e)
        {
            pnlMain.Controls.Clear();
            if (!pnlMain.Controls.Contains(ucLoad.LoadFunc))
            {
                pnlMain.Controls.Add(ucLoad.LoadFunc);
                ucLoad.LoadFunc.Dock = DockStyle.Fill;
                ucLoad.LoadFunc.BringToFront();
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }

        private void btnMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btnSlideUser_Click(object sender, EventArgs e)
        {
            pnlMain.Controls.Clear();
            if (!pnlMain.Controls.Contains(ucRegistration.RegFunc))
            {
                pnlMain.Controls.Add(ucRegistration.RegFunc);
                ucRegistration.RegFunc.Dock = DockStyle.Fill;
                ucRegistration.RegFunc.BringToFront();
            }
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            pnlMain.Controls.Clear();
            if (!pnlMain.Controls.Contains(ucReport.ReportFunc))
            {
                pnlMain.Controls.Add(ucReport.ReportFunc);
                ucReport.ReportFunc.Dock = DockStyle.Fill;
                ucReport.ReportFunc.BringToFront();
            }
        }

        private void btnSlideAbout_Click(object sender, EventArgs e)
        {
            pnlMain.Controls.Clear();
            if (!pnlMain.Controls.Contains(ucAbout.AboutFunc))
            {
                pnlMain.Controls.Add(ucAbout.AboutFunc);
                ucAbout.AboutFunc.Dock = DockStyle.Fill;
                ucRegistration.RegFunc.BringToFront();
            }
        }

        private void btnSlideItem_Click(object sender, EventArgs e)
        {
            pnlMain.Controls.Clear();
            if (!pnlMain.Controls.Contains(ucItems.ItemFunc))
            {
                pnlMain.Controls.Add(ucItems.ItemFunc);
                ucItems.ItemFunc.Dock = DockStyle.Fill;
                ucItems.ItemFunc.BringToFront();
            }
        }
    }
}
