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
    public partial class ucReport : UserControl
    {
        private static ucReport Report_Instance;
        public static ucReport ReportFunc
        {
            get
            {
                Report_Instance = null;
                if (Report_Instance == null)
                    Report_Instance = new ucReport();
                return Report_Instance;
            }
        }

        //SET CONNECTION STRING
        static string conStr = System.Configuration.ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
        SqlConnection con = new SqlConnection(conStr);


        DateTime dateFrom, dateTo;
        public ucReport()
        {
            InitializeComponent();
        }

        private void btnDeliveryRptBtn_Click(object sender, EventArgs e)
        {
            try
            {
                DelRpDtSelectForm formDDSF = new DelRpDtSelectForm();
                formDDSF.ShowDialog();
                this.dateFrom = formDDSF.FromDate;
                this.dateTo = formDDSF.ToDate;
                DeliveryReportMethod();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void DeliveryReportMethod()
        {
            try
            {


                SqlDataAdapter daR1 = new SqlDataAdapter("SELECT * FROM Delivery_Table WHERE Date BETWEEN '" + dateFrom + "' AND '" + dateTo + "'", con);
                con.Open();
                DataSet dsR1 = new DataSet();
                daR1.Fill(dsR1, "Delivery_Table");
                MessageBox.Show(dsR1.Tables["Delivery_Table"].Rows.Count.ToString());
                if (dsR1.Tables["Delivery_Table"].Rows.Count == 0)
                {
                    MessageBox.Show("No data found!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                con.Close();

                DeliveryCrystalReport objDelRpt = new DeliveryCrystalReport();
                objDelRpt.SetDatabaseLogon("genesip", "genesip");
                objDelRpt.SetDataSource(dsR1.Tables["Delivery_Table"]);
                crystalReportViewer1.ReportSource = objDelRpt;
                crystalReportViewer1.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
