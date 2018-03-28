using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSR_System
{
    class ItemClass
    {
        //CONNECTION SET
        static string conStr = System.Configuration.ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
        SqlConnection con = new SqlConnection(conStr);

        //NEW ITEM ADD
        public void NewItemAdd(string _ItemName, int _CaseBot, decimal _OneBotPrice)
        {
            SqlCommand cmd = new SqlCommand(@"INSERT INTO Item_Table VALUES('" + _ItemName + "','" + _CaseBot + "','" + _OneBotPrice + "')", con);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }

        //ITEMS UPDATE
        public void UpdateItem(string _itemName, int _botCase, string _oldItemName, decimal _1BotPrice)
        {
            SqlCommand cmd = new SqlCommand("UPDATE Item_Table SET ItemName ='" + _itemName + "', OnceCaseBottle='" + _botCase + "', OneBotPrice = '"+ _1BotPrice + "' WHERE ItemName='" + _oldItemName + "'", con);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }

        //CHECK AVAILABLE ITEMS
        bool checkItem;
        public bool ItemAvailable(string _enterVal)
        {
            checkItem = false;
            SqlCommand com = new SqlCommand("SELECT * FROM Item_Table WHERE ItemName = '" + _enterVal + "'", con);
            con.Open();
            SqlDataReader dr = com.ExecuteReader();
            if (dr.Read() == true)
                checkItem = true;
            con.Close();
            return checkItem;
        }
    }
}
