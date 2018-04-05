using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSR_System
{
    class LoadUnloadClass
    {
        //CONNECTION SET
        static string conStr = System.Configuration.ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
        SqlConnection con = new SqlConnection(conStr);

        //NEW LOAD ADD
        int _UnloadCases = 0, _UnloadBottle = 0, _SaleBottle = 0;
        decimal _Value = 0.00m;
        public void NewLoadAdd(string _Route, DateTime _Date, string _DSRname, string _ItemName, int _LoadCases, int _LoadBottle)
        {
            SqlCommand cmd = new SqlCommand(@"INSERT INTO LoadUnload_Table VALUES ('" + _Route + "', '" + _Date + "', '" + _DSRname + "', '" + _ItemName + "', '" + _LoadCases + "', '" + _LoadBottle + "', '" + _UnloadCases + "','" + _UnloadBottle + "','" + _SaleBottle + "','" + _Value + "')", con);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }

        public void UpdateLoadUnload(string _Route, DateTime _Date, string _DSRname, string _ItemName, int _UnloadCases, int _UnloadBottle, int _SaleBottle, decimal _Value)
        {
            SqlCommand cmd = new SqlCommand("UPDATE LoadUnload_Table SET UnloadCases = '" + _UnloadCases + "', UnloadBottle = '" + _UnloadBottle + "', SaleBottle = '" + _SaleBottle + "', Value = '" + _Value + "' WHERE Route = '" + _Route + "' AND Date = '" + _Date + "' AND DSRname = '" + _DSRname + "' AND ItemName = '" + _ItemName + "'", con);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }
    }
}
