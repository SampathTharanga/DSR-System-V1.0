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

        public ucUnload()
        {
            InitializeComponent();
        }
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

        void SalesAndValues()
        {
            int salesBottle = 0, OneCaseBot = 0, loadBottle = 0, loadCase = 0, unLoadBottle = 0, unLoadCase = 0;
            int totLoadBottle = 0, totUnloadBottle = 0, salesTotBottle = 0;
            decimal OneBotPrice = 0.00m, value = 0.00m, totValue = 0.00m;

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

        private void dataGridViewUnload_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridViewUnload.Columns[e.ColumnIndex].Name == "Reference")
            {
                //your code goes here
            }
        }

        private void txtRoute_KeyDown(object sender, KeyEventArgs e)
        {
            //SEARCH DSR DATA LOAD TO DATAGRIDVIEW
            //if (e.KeyCode == Keys.Enter)
            //{
            //    dataGridViewUnload.Columns["ItemName"].ReadOnly = true;
            //    dataGridViewUnload.Columns["LoadCases"].ReadOnly = true;
            //    dataGridViewUnload.Columns["LoadBottle"].ReadOnly = true;
            //    dataGridViewUnload.Columns["SaleBottle"].ReadOnly = true;
            //    dataGridViewUnload.Columns["Value"].ReadOnly = true;
            //    SqlDataAdapter da1 = new SqlDataAdapter("SELECT ItemName, LoadCases, LoadBottle, UnloadCases, UnloadBottle, SaleBottle, Value FROM LoadUnload_Table WHERE Route = '" + txtRoute.Text + "'", con);
            //    con.Open();
            //    DataTable dt1 = new DataTable();
            //    da1.Fill(dt1);
            //    con.Close();
            //    dataGridViewUnload.Rows.Clear();
            //    foreach (DataRow item in dt1.Rows)
            //    {
            //        int n = dataGridViewUnload.Rows.Add();
            //        dataGridViewUnload.Rows[n].Cells[0].Value = item["ItemName"].ToString();
            //        dataGridViewUnload.Rows[n].Cells[1].Value = item["LoadCases"].ToString();
            //        dataGridViewUnload.Rows[n].Cells[2].Value = item["LoadBottle"].ToString();
            //        dataGridViewUnload.Rows[n].Cells[3].Value = item["UnloadCases"].ToString();
            //        dataGridViewUnload.Rows[n].Cells[4].Value = item["UnloadBottle"].ToString();
            //        dataGridViewUnload.Rows[n].Cells[5].Value = item["SaleBottle"].ToString();
            //        dataGridViewUnload.Rows[n].Cells[6].Value = item["Value"].ToString();
            //    }
            //}
        }
    }
}
