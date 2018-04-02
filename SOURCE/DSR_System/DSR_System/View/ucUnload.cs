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
        decimal DEFAULT_VAL = 0.00m, totValue = 0.00m;

        public ucUnload()
        {
            InitializeComponent();
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System failure", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        private void ucUnload_Load(object sender, EventArgs e)
        {
            try
            {
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
                MessageBox.Show(ex.Message, "System failure", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
        #endregion

        // PROCESS CALCULATION METHOD
        void ProcessCalc()
        {
            decimal processVal1 = 0.00m, processVal2 = 0.00m, processVal3 = 0.00m;

            if (
                decimal.TryParse(txtCash.Text, out decimal cash) &&
                decimal.TryParse(txtCheque.Text, out decimal cheque) &&
                decimal.TryParse(txtCredit.Text, out decimal credit) &&
                decimal.TryParse(txtDiscount.Text, out decimal discount) &&
                decimal.TryParse(txtExpenses.Text, out decimal expenses) &&
                decimal.TryParse(txtExpairi.Text, out decimal expiri) &&
                decimal.TryParse(txtGasOut.Text, out decimal gasOut) &&
                decimal.TryParse(txtGiveGoods.Text, out decimal giveGoods) &&
                decimal.TryParse(txtGaveGoods.Text, out decimal gaveGoods) &&
                decimal.TryParse(txtShortEmpty.Text, out decimal shortEmpty) &&
                decimal.TryParse(txtExcessEmpty.Text, out decimal excessEmpty)
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


        ////SHORT RESULT LINK BLINK CODE
        //private async void ShortBlink()
        //{
        //    while (true)
        //    {
        //        await Task.Delay(500);
        //        lblShortExcess.ForeColor = lblShortExcess.ForeColor == Color.Red ? Color.Orange : Color.Red;
        //    }
        //}

        ////EXCESS RESULT LINK BLINK CODE
        //private async void ExcessBlink()
        //{
        //    while (true)
        //    {
        //        await Task.Delay(500);
        //        lblShortExcess.ForeColor = lblShortExcess.ForeColor == Color.SeaGreen ? Color.LightGreen : Color.SeaGreen;
        //    }
        //}

        private void txtCash_Click(object sender, EventArgs e)
        {
            SalesAndValues();
        }

        private void dataGridViewUnload_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridViewUnload.Columns[e.ColumnIndex].Name == "UnloadBottle" || dataGridViewUnload.Columns[e.ColumnIndex].Name == "UnloadCases")
            {
                SalesAndValues();
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

        private void btnProcess_Click(object sender, EventArgs e)
        {
            ProcessCalc();
        }
    }
}
