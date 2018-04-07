using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSR_System
{
    class DeliveryClass
    {
        //CONNECTION SET
        static string conStr = System.Configuration.ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
        SqlConnection con = new SqlConnection(conStr);

        //INSERT NEW PROCESS
        public void InsertProcess
            (
                DateTime _Date,
                string _Route,
                string _DSRname,
                decimal _ShortEmpty,
                decimal _ExcessEmpty,
                decimal _cash,
                decimal _Cheque,
                decimal _Credit,
                decimal _Discount,
                decimal _Expenses,
                decimal _Expairi,
                decimal _GasOut,
                decimal _ToGiveGoods,
                decimal _ToGaveGoods,
                string _FinalResult,
                int _TotalSalesBottle,
                decimal _TotalValue
            )
        {
            SqlCommand cmd = new SqlCommand(@"INSERT INTO Delivery_Table VALUES ('" + _Date + "','" + _Route + "','" + _DSRname + "','" + _ShortEmpty + "','" + _ExcessEmpty + "','" + _cash + "','" + _Cheque + "','" + _Credit + "','" + _Discount + "','" + _Expenses + "','" + _Expairi + "','" + _GasOut + "','" + _ToGiveGoods + "', '" + _ToGaveGoods + "', '" + _FinalResult + "', '" + _TotalSalesBottle + "', '" + _TotalValue + "')", con);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }
    }
}
