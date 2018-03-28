using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace DSR_System
{
    public partial class ucItems : UserControl
    {
        private static ucItems Item_Instance;
        public static ucItems ItemFunc
        {
            get
            {
                Item_Instance = null;
                if (Item_Instance == null)
                    Item_Instance = new ucItems();
                return Item_Instance;
            }
        }

        //CONNECTION SET
        static string conStr = System.Configuration.ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
        SqlConnection con = new SqlConnection(conStr);

        public ucItems()
        {
            InitializeComponent();

            SetMaximumLength(txtItemName, 30);
            SetMaximumLength(txtBottleNum, 3);
            SetMaximumLength(txtOneBotPrice, 10);
        }

        ItemClass ObjItemCl = new ItemClass();
        string oldItemName = string.Empty;

        //BUNIFU FRAMEWORK TEXTBOX MAXLENGTH SET
        private void SetMaximumLength(Bunifu.Framework.UI.BunifuMetroTextbox metroTextbox, int maximumLength)
        {
            foreach (Control ctl in metroTextbox.Controls)
            {
                if (ctl.GetType() == typeof(TextBox))
                {
                    var txt = (TextBox)ctl;
                    txt.MaxLength = maximumLength;

                    // Set other properties & events here...
                }
            }
        }


        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtItemName.Text) && !string.IsNullOrEmpty(txtBottleNum.Text) && !string.IsNullOrEmpty(txtOneBotPrice.Text))
                {
                    if (btnAdd.Text == "SAVE")
                    {
                        if (ObjItemCl.ItemAvailable(txtItemName.Text) != true)
                        {
                            ObjItemCl.NewItemAdd(txtItemName.Text, int.Parse(txtBottleNum.Text), Convert.ToDecimal(txtOneBotPrice.Text));
                            MessageBox.Show("New item added successful!", "Successful message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtItemName.Focus();
                        }
                        else MessageBox.Show("Item does not exist!", "Error message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else if (btnAdd.Text == "UPDATE")
                    {
                        ObjItemCl.UpdateItem(txtItemName.Text, int.Parse(txtBottleNum.Text), oldItemName, Convert.ToDecimal(txtOneBotPrice.Text));
                        MessageBox.Show("Update successful!", "Successful message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtItemName.Focus();
                    }
                    else
                        MessageBox.Show("Save or Update problem!", "Error message", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    txtBottleNum.Text = string.Empty;
                    txtItemName.Text = string.Empty;
                    txtOneBotPrice.Text = string.Empty;
                    btnAdd.Text = "SAVE";
                    LoadData();
                }
                else
                    MessageBox.Show("Please fill all details!", "Error message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System failure", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void LoadData()
        {
            try
            {
                SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM Item_Table", con);
                con.Open();
                DataTable dt = new DataTable();
                sda.Fill(dt);
                dgvItem.Rows.Clear();
                foreach (DataRow item in dt.Rows)
                {
                    int n = dgvItem.Rows.Add();
                    dgvItem.Rows[n].Cells[0].Value = item["ItemName"].ToString();
                    dgvItem.Rows[n].Cells[1].Value = item["OnceCaseBottle"].ToString();
                    dgvItem.Rows[n].Cells[2].Value = item["OneBotPrice"].ToString();
                }
                con.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "System failure", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ucItems_Load(object sender, EventArgs e)
        {
            LoadData();
            btnAdd.Text = "SAVE";
        }

        private void dgvItem_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Delete)
                {
                    if (dgvItem.Rows.Count != 0)
                    {
                        DialogResult result = MessageBox.Show("Do you want to delete " + dgvItem.CurrentRow.Cells[0].Value.ToString() + " item?", "Are you sure?", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                        if (result == DialogResult.Yes)
                        {
                            //Delete item
                            SqlCommand cmd = new SqlCommand("DELETE FROM Item_Table WHERE ItemName='" + dgvItem.CurrentRow.Cells["ItemName"].Value.ToString() + "'", con);
                            con.Open();
                            cmd.ExecuteNonQuery();
                            con.Close();
                            LoadData();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System failure", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvItem_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                txtItemName.Text = dgvItem.CurrentRow.Cells[0].Value.ToString();
                txtBottleNum.Text = dgvItem.CurrentRow.Cells[1].Value.ToString();
                txtOneBotPrice.Text = dgvItem.CurrentRow.Cells[2].Value.ToString();
                btnAdd.Text = "UPDATE";
                oldItemName = dgvItem.CurrentRow.Cells[0].Value.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System failure", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvItem_Click(object sender, EventArgs e)
        {
            try
            {
                btnAdd.Text = "SAVE";
                txtBottleNum.Text = string.Empty;
                txtItemName.Text = string.Empty;
                txtOneBotPrice.Text = string.Empty;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System failure", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtBottleNum_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != 8 && e.KeyChar != 127)
                e.Handled = true;
        }

        private void txtOneBotPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != 8 && e.KeyChar != 127 && e.KeyChar != 46)
                e.Handled = true;
        }
    }
}
