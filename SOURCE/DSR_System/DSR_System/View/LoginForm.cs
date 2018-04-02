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

            SetMaximumLength(txtUseName, 5);
            SetMaximumLength(txtPassword, 10);
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
                        txtUseName.Text = "Admin";
                    }
                }
                else
                    MessageBox.Show("Please enter login details!", "Login Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        //BUNIFU FRAMEWORK TEXTBOX MAXLENGTH SET
        private void SetMaximumLength(Bunifu.Framework.UI.BunifuMetroTextbox metroTextbox, int maximumLength)
        {
            try
            {
                foreach (Control ctl in metroTextbox.Controls)
                {
                    if (ctl.GetType() == typeof(TextBox))
                    {
                        var txt = (TextBox)ctl;
                        txt.MaxLength = maximumLength;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
