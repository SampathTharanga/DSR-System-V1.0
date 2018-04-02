using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace DSR_System
{
    public partial class FrogetPasswordForm : Form
    {
        public FrogetPasswordForm()
        {
            InitializeComponent();

            SetMaxLength(txtAnswer, 30);
        }

        // DATABASE CONNECTION
        static string conStr = System.Configuration.ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
        SqlConnection con = new SqlConnection(conStr);

        UserClass ObjUser = new UserClass();

        private void btnRecover_Click(object sender, EventArgs e)
        {
            try
            {
                if (cbxSecQue.selectedIndex != -1 && !string.IsNullOrEmpty(txtAnswer.Text))
                {
                    SqlDataReader dr = ObjUser.FrogetPassword("Admin", cbxSecQue.selectedIndex.ToString(), txtAnswer.Text);
                    if (dr.Read() == true)
                    {
                        MessageBox.Show("Admin login password is " + dr["Password"].ToString() + ".", "Password Recover", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Application.Restart();
                    }
                    else
                    {
                        MessageBox.Show("Incorrect details!", "Recover Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        ClearAll(this);
                    }
                }
                else
                    MessageBox.Show("Please enter details!", "Recover Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch(Exception ex)
            {
                con.Close();
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }

        //TEXTBOX MAX LENGTH SET
        private void SetMaxLength(Bunifu.Framework.UI.BunifuMetroTextbox metroTextbox, int maxLength)
        {
            try
            {
                foreach (Control ctl in metroTextbox.Controls)
                {
                    if(ctl.GetType() == typeof(TextBox))
                    {
                        var txt = (TextBox)ctl;
                        txt.MaxLength = maxLength;
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
