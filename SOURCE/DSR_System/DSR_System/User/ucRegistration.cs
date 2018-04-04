using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace DSR_System
{
    public partial class ucRegistration : UserControl
    {
        private static ucRegistration Reg_Instance;
        public static ucRegistration RegFunc
        {
            get
            {
                Reg_Instance = null;
                if (Reg_Instance == null)
                    Reg_Instance = new ucRegistration();
                return Reg_Instance;
            }
        }


        //DATABASE CONNECTION
        static string conStr = System.Configuration.ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
        SqlConnection con = new SqlConnection(conStr);

        public ucRegistration()
        {
            InitializeComponent();
        }

        UserClass ObjUserCls = new UserClass();

        //LOGIN USER DETAILS UPDATE
        void Registration()
        {
            try
            {
                if (!string.IsNullOrEmpty(txtPass.Text) && !string.IsNullOrEmpty(txtRePass.Text) && cbxSecQue.selectedIndex > -1 && !string.IsNullOrEmpty(txtAnswer.Text))
                {
                    SqlCommand cmd = new SqlCommand("SELECT Password,SecQuestion,Answer FROM User_Table WHERE UserName = '" + txtUserName.Text + "'", con);
                    con.Open();
                    SqlDataReader drUserCheck = cmd.ExecuteReader();
                    if (drUserCheck.Read())
                    {
                        con.Close();
                        if (txtPass.Text == txtRePass.Text)
                        {
                            ObjUserCls.UserUpdate(txtPass.Text, cbxSecQue.selectedIndex.ToString(), txtAnswer.Text);
                            MessageBox.Show("Password updated successful!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                            MessageBox.Show("Please enter same password!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        ClearAll(this);
                    }
                    else
                        con.Close();
                }
                else
                    MessageBox.Show("Please filling all the details!", "ERROR MESSAGE", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            Registration();
        }

        //RESET ALL CONTROL
        void ClearAll(Control con)
        {
            try
            {
                foreach (Control c in con.Controls)
                {
                    if (c is TextBox)
                        ((TextBox)c).Clear();

                    if (c is ComboBox)
                        ((ComboBox)c).SelectedIndex = -1;
                    else
                        ClearAll(c);
                }
                txtUserName.Text = "Admin";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void btnRegister_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtName.Text) && !string.IsNullOrEmpty(txtDSRName.Text))
                {
                    if (ObjUserCls.CheckExistDSR(txtDSRName.Text) != true)
                    {
                        ObjUserCls.RegistrationDSR(txtDSRName.Text, txtName.Text);
                        MessageBox.Show("DSR registration successful!", "Success message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ClearAll(this);
                        txtName.Focus();
                    }
                    else
                    {
                        MessageBox.Show("This DSR name exist!", "Warning message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtDSRName.Text = string.Empty;
                        txtDSRName.Focus();
                    }
                }
                else MessageBox.Show("Please enter Name and DSR name!", "Error message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
