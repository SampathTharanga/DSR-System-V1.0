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

            for (int month = 1; month <= 12; month++)
            {
                ShortVal = 0; ExcessVal = 0;
                while (true)
                {
                    SqlCommand cmd = new SqlCommand("SELECT * FROM Delivery_Table  WHERE MONTH(Date) = '" + month + "' AND YEAR(Date) = YEAR('"+ DateTime.Today + "')", con);
                    con.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.Read() == true)
                    {
                        if (dr["FinalResult"].ToString() == "Short")
                        {
                            con.Close();
                            ShortVal++;
                        }
                        else
                        {
                            con.Close();
                            ExcessVal++;
                        }
                    }
                    else
                    {
                        con.Close();
                        break;
                    }
                }
                ShortValue.Add(ShortVal);
                ExcessValue.Add(ExcessVal);
            }

            cartesianChart1.Series = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "Short",
                    Values = ShortValue
                }
                ,
                new LineSeries
                {
                    Title = "Excess",
                    Values = ExcessValue
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
                LabelFormatter = value => value.ToString("C")
            });

            cartesianChart1.LegendLocation = LegendLocation.Right;

            //modifying the series collection will animate and update the chart
            //cartesianChart1.Series.Add(new LineSeries
            //{
            //    Values =ShortValue,
            //    LineSmoothness = 0, //straight lines, 1 really smooth lines
            //    PointGeometry = Geometry.Parse("m 25 70.36218 20 -28 -20 22 -8 -6 z"),
            //    PointGeometrySize = 50,
            //    PointForeground = System.Windows.Media.Brushes.Gray
            //});

            //modifying any series values will also animate and update the chart
            cartesianChart1.Series[1].Values.Add(5d);


            cartesianChart1.DataClick += CartesianChart1OnDataClick;
        }

        private void CartesianChart1OnDataClick(object sender, ChartPoint chartPoint)
        {
            MessageBox.Show("You clicked (" + chartPoint.X + "," + chartPoint.Y + ")");
        }
    
    }
}
