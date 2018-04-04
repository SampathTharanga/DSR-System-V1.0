using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using Bunifu;

namespace DSR_System
{
    public partial class ucLoad : UserControl
    {
        private static ucLoad Load_Instance;
        public static ucLoad LoadFunc
        {
            get
            {
                Load_Instance = null;
                if (Load_Instance == null)
                    Load_Instance = new ucLoad();
                return Load_Instance;
            }
        }

        //CONNECTION SET
        static string conStr = System.Configuration.ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
        SqlConnection con = new SqlConnection(conStr);

        LoadUnloadClass ObjLUClass = new LoadUnloadClass();

        public ucLoad()
        {
            InitializeComponent();
            SetMaxLength(txtRoute, 30);
        }

        //SET TEXTBOX MAXLENGHT
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

        private void ucLoad_Load(object sender, EventArgs e)
        {
            try
            {
                dtpDate.Value = DateTime.Today;

                LoadDataDgv();

                //DATABASE DATA LOAD TO THE DROPDOWN
                SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM DSR_Table", con);
                con.Open();
                DataTable dt = new DataTable();
                sda.Fill(dt);
                con.Close();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    //cbxDSRname.Items.Add(dt.Rows[i]["UserName"]);
                    cbxDSRname.AddItem(dt.Rows[i][0].ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void LoadDataDgv()
        {
            try
            {
                dataGridViewLoad.Columns["ItemName"].ReadOnly = true;
                SqlDataAdapter sda = new SqlDataAdapter("SELECT ItemName FROM Item_Table", con);
                con.Open();
                DataTable dt = new DataTable();
                sda.Fill(dt);
                con.Close();
                dataGridViewLoad.Rows.Clear();
                foreach (DataRow item in dt.Rows)
                {
                    int n = dataGridViewLoad.Rows.Add();
                    dataGridViewLoad.Rows[n].Cells[0].Value = item["ItemName"].ToString();
                    dataGridViewLoad.Rows[n].Cells[1].Value = "0";
                    dataGridViewLoad.Rows[n].Cells[2].Value = "0";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                bool ItemNotAdd = false;
                //COUNT EMPTY CELL
                int countEmptyCase = dataGridViewLoad.Rows.Cast<DataGridViewRow>().Select(row => (string)row.Cells["LoadCases"].Value).Where(v => string.IsNullOrEmpty(v)).Count();
                int countEmptyBottle = dataGridViewLoad.Rows.Cast<DataGridViewRow>().Select(row => (string)row.Cells["LoadBottle"].Value).Where(v => string.IsNullOrEmpty(v)).Count();

                if (countEmptyCase == 0 && countEmptyBottle == 0)
                {
                    if (!string.IsNullOrEmpty(txtRoute.Text) &&  cbxDSRname.selectedIndex != -1)
                    {
                        string route = string.Empty, dsrName = string.Empty, itemName = string.Empty;
                        int loadCase = 0, loadBottle = 0;
                        DateTime date = DateTime.Today;

                        route = txtRoute.Text;
                        dsrName = cbxDSRname.selectedValue;

                        for (int i = 0; i < dataGridViewLoad.Rows.Count; ++i)
                        {
                            loadCase = 0; loadBottle = 0;
                            itemName = string.Empty;

                            itemName = dataGridViewLoad.Rows[i].Cells["ItemName"].Value.ToString();
                            loadCase = int.Parse(dataGridViewLoad.Rows[i].Cells["LoadCases"].Value.ToString());
                            loadBottle = int.Parse(dataGridViewLoad.Rows[i].Cells["LoadBottle"].Value.ToString());


                            if (loadCase > 0 || loadBottle > 0)//CHECK AT LEAST ONE DATA AVAILABLE IN CASE AND BOTTLE COLUMNS
                            {
                                ObjLUClass.NewLoadAdd(route, date, dsrName, itemName, loadCase, loadBottle);
                                ItemNotAdd = true;
                            }
                        }

                        if (ItemNotAdd == false)
                            MessageBox.Show("Please add at least one item for load data.", "Warning Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        else
                        {
                            MessageBox.Show("Successful", "Successful Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadDataDgv();
                            ClearAll(this);
                        }


                    }
                    else MessageBox.Show("Please enter details!", "Warnning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else MessageBox.Show("Please all empty cell(s) value are must be Zero.", "Warnning", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System failure", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void dataGridViewLoad_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (!char.IsDigit(e.KeyChar) && e.KeyChar != 8 && e.KeyChar != 127)
            //    e.Handled = true;
        }
    }
}
