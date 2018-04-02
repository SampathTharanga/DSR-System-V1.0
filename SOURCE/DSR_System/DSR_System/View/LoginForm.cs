using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DSR_System
{
    public partial class LoginForm : Form
    {
        // DATABASE CONNECTION
        static string conStr = System.Configuration.ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
        SqlConnection con = new SqlConnection(conStr);

        public LoginForm()
        {
            InitializeComponent();
        }

        UserClass ObjUserCl = new UserClass();
        private void LoginForm_Load(object sender, EventArgs e)
        {
            try
            {
                if (ObjUserCl.CheckExistUser() != true)
                {
                    //DEFAULT USER ACCOUNT CREATE
                    ObjUserCl.UserRegistration();
                    MessageBox.Show("Default user registration completed!", "Registration", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void LoginFunction()
        {
            try
            {
                if (!string.IsNullOrEmpty(txtUseName.Text) && !string.IsNullOrEmpty(txtPassword.Text))
                {
                    SqlDataReader drm = ObjUserCl.LoginUser(txtUseName.Text, txtPassword.Text);
                    if (drm.Read())
                    {
                        con.Close();
                        this.Hide();
                        MainForm ObjMn = new MainForm();
                        ObjMn.ShowDialog();
                    }
                    else
                    {
                        con.Close();
                        MessageBox.Show("Please enter correct details", "USER ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtPassword.Text = string.Empty;
                        txtUseName.Text = string.Empty;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSignIn_Click(object sender, EventArgs e)
        {
            LoginFunction();
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if(e.KeyCode == Keys.Enter)
                {
                    LoginFunction();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void llblFroget_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FrogetPasswordForm frmForget = new FrogetPasswordForm();
            frmForget.ShowDialog();
        }
    }
}
