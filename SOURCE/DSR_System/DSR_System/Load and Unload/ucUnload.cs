using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Threading.Tasks;

namespace DSR_System
{
    public partial class ucUnload : UserControl
    {
        private static ucUnload Unload_Instance;
        public static ucUnload Unload
        {
            get
            {
                Unload_Instance = null;
                if (Unload_Instance == null)
                    Unload_Instance = new ucUnload();
                return Unload_Instance;
            }
        }

        //CONNECTION SET
        static string conStr = System.Configuration.ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
        SqlConnection con = new SqlConnection(conStr);

        //VARIABLE DECLARATION
        decimal totValue = 0.00m;
        decimal cash = 0.00m, cheque = 0.00m, credit = 0.00m, discount = 0.00m, expenses = 0.00m, expiri = 0.00m, gasOut = 0.00m, giveGoods = 0.00m, gaveGoods = 0.00m, shortEmpty = 0.00m, excessEmpty = 0.00m;

        public ucUnload()
        {
            InitializeComponent();

            MaxLengthMethod();
        }

        void MaxLengthMethod()
        {
            SetMaxLength(txtRoute, 30);
            SetMaxLength(txtCash, 18);
            SetMaxLength(txtCheque, 18);
            SetMaxLength(txtCredit, 18);
            SetMaxLength(txtExpenses, 18);
            SetMaxLength(txtExpairi, 18);
            SetMaxLength(txtGasOut, 18);
            SetMaxLength(txtGiveGoods, 18);
            SetMaxLength(txtGaveGoods, 18);
            SetMaxLength(txtShortEmpty, 18);
            SetMaxLength(txtExcessEmpty, 18);
        }


        //SET TEXTBOX MAXLENGHT
        private void SetMaxLength(Bunifu.Framework.UI.BunifuMetroTextbox metroTextbox, int maxLength)
        {
            try
            {
                foreach (Control ctl in metroTextbox.Controls)
                {
                    if (ctl.GetType() == typeof(TextBox))
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

        #region SELECTED DSR DATA LOAD
        void SelectedDsrDataLoad()
        {
            try
            {
                dataGridViewUnload.Columns["ItemName"].ReadOnly = true;
                dataGridViewUnload.Columns["LoadCases"].ReadOnly = true;
                dataGridViewUnload.Columns["LoadBottle"].ReadOnly = true;
                dataGridViewUnload.Columns["SaleBottle"].ReadOnly = true;
                dataGridViewUnload.Columns["Value"].ReadOnly = true;

                SqlCommand cmdCheck = new SqlCommand("SELECT ItemName, LoadCases, LoadBottle, UnloadCases, UnloadBottle, SaleBottle, Value FROM LoadUnload_Table WHERE Route = '" + txtRoute.Text + "' AND Date = '" + dtpDate.Value + "' AND DSRname = '" + cbxDSRname.selectedValue + "'", con);
                con.Open();
                SqlDataReader drCheck = cmdCheck.ExecuteReader();
                if (drCheck.Read() == true)
                {
                    con.Close();
                    SqlDataAdapter sda = new SqlDataAdapter("SELECT ItemName, LoadCases, LoadBottle, UnloadCases, UnloadBottle, SaleBottle, Value FROM LoadUnload_Table WHERE Route = '" + txtRoute.Text + "' AND Date = '" + dtpDate.Value + "' AND DSRname = '" + cbxDSRname.selectedValue + "'", con);
                    con.Open();
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    con.Close();
                    dataGridViewUnload.Rows.Clear();
                    foreach (DataRow item in dt.Rows)
                    {
                        int n = dataGridViewUnload.Rows.Add();
                        dataGridViewUnload.Rows[n].Cells[0].Value = item["ItemName"].ToString();
                        dataGridViewUnload.Rows[n].Cells[1].Value = item["LoadCases"].ToString();
                        dataGridViewUnload.Rows[n].Cells[2].Value = item["LoadBottle"].ToString();
                        dataGridViewUnload.Rows[n].Cells[3].Value = item["UnloadCases"].ToString();
                        dataGridViewUnload.Rows[n].Cells[4].Value = item["UnloadBottle"].ToString();
                        dataGridViewUnload.Rows[n].Cells[5].Value = item["SaleBottle"].ToString();
                        dataGridViewUnload.Rows[n].Cells[6].Value = item["Value"].ToString();
                    }
                }
                else
                {
                    con.Close();
                    MessageBox.Show("Record does not found!", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    AfterSaveClearAll(this);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        private void ucUnload_Load(object sender, EventArgs e)
        {
            try
            {
                dtpDate.Value = DateTime.Today;

                //DATABASE DATA LOAD TO THE DROPDOWN
                SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM DSR_Table", con);
                con.Open();
                DataTable dt = new DataTable();
                sda.Fill(dt);
                con.Close();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    cbxDSRname.AddItem(dt.Rows[i][0].ToString());
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            SelectedDsrDataLoad();
        }

        #region SALES AND VALUES CALCULATION
        //SALES AND VALUES CALCULATION
        void SalesAndValues()
        {
            try
            {
                int salesBottle = 0, OneCaseBot = 0, loadBottle = 0, loadCase = 0, unLoadBottle = 0, unLoadCase = 0;
                int totLoadBottle = 0, totUnloadBottle = 0, salesTotBottle = 0;
                decimal OneBotPrice = 0.00m, value = 0.00m;
                totValue = 0.00m;

                for (int row = 0; row < dataGridViewUnload.Rows.Count; ++row)
                {
                    //ALL VARIABLES SET DEFAULT ZEloadBottleRO
                    OneCaseBot = 0; OneBotPrice = 0.00m; loadBottle = 0; loadCase = 0; unLoadBottle = 0; unLoadCase = 0;
                    totLoadBottle = 0; salesBottle = 0; value = 0.00m;

                    //GET ITEM TABLE DETAILS
                    SqlCommand cmd1 = new SqlCommand("SELECT * FROM Item_Table WHERE ItemName = '" + dataGridViewUnload.Rows[row].Cells["ItemName"].Value.ToString() + "'", con);
                    con.Open();
                    SqlDataReader dr1 = cmd1.ExecuteReader();
                    if (dr1.Read() == true)
                    {
                        OneCaseBot = int.Parse(dr1["OnceCaseBottle"].ToString());
                        OneBotPrice = Convert.ToDecimal(dr1["OneBotPrice"].ToString());
                    }
                    con.Close();

                    //GET LOAD DETAILS
                    loadBottle = int.Parse(dataGridViewUnload.Rows[row].Cells["LoadBottle"].Value.ToString());
                    loadCase = int.Parse(dataGridViewUnload.Rows[row].Cells["LoadCases"].Value.ToString());

                    //GET UNLOAD DETAILS
                    unLoadBottle = int.Parse(dataGridViewUnload.Rows[row].Cells["UnloadBottle"].Value.ToString());
                    unLoadCase = int.Parse(dataGridViewUnload.Rows[row].Cells["UnloadCases"].Value.ToString());

                    //TOTAL LOAD BOTTLE
                    totLoadBottle = (loadCase * OneCaseBot) + loadBottle;

                    //TOTAL UNLOAD BOTTLE
                    totUnloadBottle = (unLoadCase * OneCaseBot) + unLoadBottle;

                    //SALES BOTTLE
                    salesBottle = totLoadBottle - totUnloadBottle;

                    //SALES BOTTLE DISPLAY IN THE DATAGRIVIEW
                    dataGridViewUnload.Rows[row].Cells["SaleBottle"].Value = salesBottle.ToString();

                    //CALULATE VALUE
                    value = salesBottle * OneBotPrice;

                    //VALUE DISPLAY DATAGRIDVIEW
                    dataGridViewUnload.Rows[row].Cells["Value"].Value = value.ToString();

                    //CALCULATE SALES TOTAL BOTTLE
                    salesTotBottle += int.Parse(dataGridViewUnload.Rows[row].Cells["SaleBottle"].Value.ToString());

                    //CALCULATE TOTAL VALUE
                    totValue += Convert.ToDecimal(dataGridViewUnload.Rows[row].Cells["Value"].Value.ToString());
                }
                lblTotBottle.Text = salesTotBottle.ToString();
                lblTotValue.Text = "Rs " + totValue.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        // PROCESS CALCULATION METHOD
        void ProcessCalc()
        {
            try
            {
                decimal processVal1 = 0.00m, processVal2 = 0.00m, processVal3 = 0.00m;

                if (
                    decimal.TryParse(txtCash.Text, out cash) &&
                    decimal.TryParse(txtCheque.Text, out cheque) &&
                    decimal.TryParse(txtCredit.Text, out credit) &&
                    decimal.TryParse(txtDiscount.Text, out discount) &&
                    decimal.TryParse(txtExpenses.Text, out expenses) &&
                    decimal.TryParse(txtExpairi.Text, out expiri) &&
                    decimal.TryParse(txtGasOut.Text, out gasOut) &&
                    decimal.TryParse(txtGiveGoods.Text, out giveGoods) &&
                    decimal.TryParse(txtGaveGoods.Text, out gaveGoods) &&
                    decimal.TryParse(txtShortEmpty.Text, out shortEmpty) &&
                    decimal.TryParse(txtExcessEmpty.Text, out excessEmpty)
                    )
                {
                    //SHORT AND EXCESS EMPTY
                    processVal1 = (totValue + shortEmpty) - excessEmpty;

                    processVal2 = processVal1 - (cash + cheque + credit + discount + expenses + expiri + gasOut);

                    //TO GAVE NAD GIVE
                    processVal3 = (processVal2 + gaveGoods) - giveGoods;

                    //NEGATIVE TO POSSITIVE
                    decimal non_neg_processVal3 = Math.Abs(processVal3);

                    //MAKE THE DECISIONS "SHORT" OR "EXCESS"
                    if (totValue > non_neg_processVal3)
                    {
                        //SHORT 
                        lblShortExcess.Text = "Short";
                        lblShortExcess.ForeColor = Color.Red;
                        //ShortBlink();
                    }
                    else if (totValue < non_neg_processVal3)
                    {
                        //EXCESS
                        lblShortExcess.Text = "Excess";
                        lblShortExcess.ForeColor = Color.ForestGreen;
                        //ExcessBlink();
                    }
                    else MessageBox.Show("Process Error!", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                    MessageBox.Show("You must fill the each values!", "Can't be Blank", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //SAVE PROCESS ALL DATA
        DeliveryClass ObjDelivery = new DeliveryClass();
        void SaveProcess()
        {
            try
            {
                if (!string.IsNullOrEmpty(txtRoute.Text) && cbxDSRname.selectedIndex != -1)
                {
                    ObjDelivery.InsertProcess(dtpDate.Value, txtRoute.Text, cbxDSRname.selectedValue, shortEmpty, excessEmpty, cash, cheque, credit, discount, expenses, expiri, gasOut, giveGoods, gaveGoods, lblShortExcess.Text);
                    MessageBox.Show("Successful", "Successful Message", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    AfterSaveClearAll(this);
                }
                else
                    MessageBox.Show("Please enter details!", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //RESET ALL CONTROL
        void AfterSaveClearAll(Control con)
        {
            try
            {
                foreach (Control c in con.Controls)
                {
                    if (c is TextBox)
                        ((TextBox)c).Text = "0.00";

                    if (c is ComboBox)
                        ((ComboBox)c).SelectedIndex = -1;
                    else
                        AfterSaveClearAll(c);
                }
                txtRoute.Text = string.Empty;
                lblShortExcess.Text = string.Empty;

                //CLEAR DATAGRIDVIEW
                dataGridViewUnload.Rows.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtCash_Click(object sender, EventArgs e)
        {
            
        }

        private void dataGridViewUnload_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dataGridViewUnload.Columns[e.ColumnIndex].Name == "UnloadBottle" || dataGridViewUnload.Columns[e.ColumnIndex].Name == "UnloadCases")
                {
                    SalesAndValues();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtCash_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != 8 && e.KeyChar != 127 && e.KeyChar != 46)
                e.Handled = true;
        }

        private void txtCheque_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != 8 && e.KeyChar != 127 && e.KeyChar != 46)
                e.Handled = true;
        }

        private void txtCredit_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(!char.IsDigit(e.KeyChar) && e.KeyChar != 8 && e.KeyChar != 127 && e.KeyChar != 46)
                e.Handled = true;
        }

        private void txtCash_MouseClick(object sender, MouseEventArgs e)
        {
            
        }

        private void dataGridViewUnload_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            // ALLOW DATAGRIDVIEW CELL ENTER VALUE DIGIT ONLY = PART 1
            e.Control.KeyPress -= new KeyPressEventHandler(DataGridViewCell_KeyPress);
            if (dataGridViewUnload.CurrentCell.ColumnIndex == 3 || dataGridViewUnload.CurrentCell.ColumnIndex == 4)
            {
                TextBox tb = e.Control as TextBox;
                if (tb != null)
                {
                    tb.KeyPress += new KeyPressEventHandler(DataGridViewCell_KeyPress);
                }
            }
        }

        // ALLOW DATAGRIDVIEW CELL ENTER VALUE DIGIT ONLY = PART 2
        private void DataGridViewCell_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtDiscount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != 8 && e.KeyChar != 127 && e.KeyChar != 46)
                e.Handled = true;
        }

        private void txtExpenses_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != 8 && e.KeyChar != 127 && e.KeyChar != 46)
                e.Handled = true;
        }

        private void txtExpairi_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != 8 && e.KeyChar != 127 && e.KeyChar != 46)
                e.Handled = true;
        }

        private void txtGasOut_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != 8 && e.KeyChar != 127 && e.KeyChar != 46)
                e.Handled = true;
        }

        private void txtGiveGoods_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != 8 && e.KeyChar != 127 && e.KeyChar != 46)
                e.Handled = RecreatingHandle;
        }

        private void txtGaveGoods_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != 8 && e.KeyChar != 127 && e.KeyChar != 46)
                e.Handled = true;
        }

        private void txtShortEmpty_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != 8 && e.KeyChar != 127 && e.KeyChar != 46)
                e.Handled = true;
        }

        private void txtExcessEmpty_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != 8 && e.KeyChar != 127 && e.KeyChar != 46)
                e.Handled = true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveProcess();
        }

        private void btnProcess_Click(object sender, EventArgs e)
        {
            ProcessCalc();
        }
    }
}
