using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LiveCharts;
using LiveCharts.Wpf;
using System.Windows.Media;
using System.Data.SqlClient;

namespace DSR_System
{
    public partial class ucChart : UserControl
    {
        private static ucChart Chart_Instance;
        public static ucChart ChartFunc
        {
            get
            {
                Chart_Instance = null;
                if (Chart_Instance == null)
                    Chart_Instance = new ucChart();
                return Chart_Instance;
            }
        }

        //CONNECTION SET
        static string conStr = System.Configuration.ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
        SqlConnection con = new SqlConnection(conStr);

        public ucChart()
        {
            InitializeComponent();

            //CHART FUNCTION HERE
            var ShortValue = new ChartValues<int>();
            var ExcessValue = new ChartValues<int>();
            int ShortVal = 0, ExcessVal = 0;
            int MonthVal = 0, YearVal = 0;

            SqlDataAdapter da = new SqlDataAdapter("SELECT Date,FinalResult FROM Delivery_Table", con);
            con.Open();
            DataTable dt = new DataTable();
            da.Fill(dt);
            con.Close();

            for (int month = 1; month <= 12; month++)
            {
                ShortVal = 0; ExcessVal = 0;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    MonthVal = DateTime.Parse(dt.Rows[i]["Date"].ToString()).Month;
                    YearVal = DateTime.Parse(dt.Rows[i]["Date"].ToString()).Year;

                    if (MonthVal == month && YearVal == DateTime.Today.Year)

                        if (dt.Rows[i]["FinalResult"].ToString() == "Short")
                            ShortVal++;
                        else
                            ExcessVal++;
                }

                ShortValue.Add(ShortVal);
                ExcessValue.Add(ExcessVal);
            }
            

            cartesianChart1.Series = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "Excess",
                    Values = ExcessValue
                }
                ,
                new LineSeries
                {
                    Title = "Short",
                    Values = ShortValue
                }
            };

            cartesianChart1.AxisX.Add(new Axis
            {
                Title = "Month",
                Labels = new[] { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" }
            });

            cartesianChart1.AxisY.Add(new Axis
            {
                Title = "Count",
                LabelFormatter = value => value + ""
            });

            cartesianChart1.LegendLocation = LegendLocation.Right;

            cartesianChart1.DataClick += CartesianChart1OnDataClick;
        }

        private void CartesianChart1OnDataClick(object sender, ChartPoint chartPoint)
        {
            MessageBox.Show("You clicked (" + chartPoint.X + "," + chartPoint.Y + ")");
        }
    
    }
}
