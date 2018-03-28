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
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        UserClass ObjUserCl = new UserClass();
        private void LoginForm_Load(object sender, EventArgs e)
        {
            //DEFAULT USER ACCOUNT CREATE
            if (ObjUserCl.UserRegistration() == true)
                MessageBox.Show("Default user registration completed!", "Registration", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
