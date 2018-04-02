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
                        MessageBox.Show("Incorrect details!", "Recover Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
