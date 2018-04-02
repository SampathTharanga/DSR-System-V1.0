using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSR_System
{
    class UserClass
    {
        // DATABASE CONNECTION
        static string conStr = System.Configuration.ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
        SqlConnection con = new SqlConnection(conStr);

        string USERNAME = "Admin", PASSWORD = "Admin";

        //USER REGISTRATION
        public void UserRegistration()
        {
            string secQu = string.Empty, ans = string.Empty;
            SqlCommand cmd1 = new SqlCommand("INSERT INTO User_Table VALUES ( '" + USERNAME + "', '" + PASSWORD + "', '" + secQu + "', '" + ans + "')", con);
            con.Open();
            cmd1.ExecuteNonQuery();
            con.Close();
        }

        //CHECK EXIST USER
        bool checkUser;
        public bool CheckExistUser()
        {
            checkUser = false;
            SqlCommand com = new SqlCommand("SELECT UserName FROM User_Table WHERE UserName = '" + USERNAME + "'", con);
            con.Open();
            SqlDataReader dr = com.ExecuteReader();
            if (dr.Read() == true)
                checkUser = true;
            con.Close();
            return checkUser;
        }

        //LOGIN CHECK
        public SqlDataReader LoginUser(string _userName, string _password)
        {
            con.Close();
            SqlCommand com = new SqlCommand("SELECT * FROM User_Table WHERE UserName='" + _userName + "' AND Password='" + _password + "'", con);
            con.Open();
            SqlDataReader dr = com.ExecuteReader();
            return dr;
        }



        //USER UPDATE
        public void UserUpdate(string _pass, string _secQu, string _ans)
        {
            SqlCommand cmdUp = new SqlCommand("UPDATE User_Table SET Password = '" + _pass + "',SecQuestion = '" + _secQu + "', Answer = '" + _ans + "' WHERE UserName = '" + USERNAME + "'", con);
            con.Open();
            cmdUp.ExecuteNonQuery();
            con.Close();
        }

        //CHECK EXIST DSR
        bool checkDSR;
        public bool CheckExistDSR(string _dsrName)
        {
            checkDSR = false;
            SqlCommand com = new SqlCommand("SELECT DSRName FROM DSR_Table WHERE DSRName = '" + _dsrName + "'", con);
            con.Open();
            SqlDataReader dr = com.ExecuteReader();
            if (dr.Read() == true)
                checkDSR = true;
            con.Close();
            return checkDSR;
        }

        //DSR REGISTRATION
        public void RegistrationDSR(string _dsrName, string _name)
        {
            SqlCommand cmd1 = new SqlCommand(@"INSERT INTO DSR_Table VALUES ('" + _dsrName + "', '" + _name + "')", con);
            con.Open();
            cmd1.ExecuteNonQuery();
            con.Close();
        }

        //USER FROGET PASSWORD
        public SqlDataReader FrogetPassword(string _userName, string _secQue, string _ans)
        {
            con.Close();
            SqlCommand cmd = new SqlCommand("SELECT UserName FROM User_Table WHERE UserName='" + _userName + "' AND SecQuestion='" + _secQue + "' AND Answer='" + _ans + "'", con);
            con.Open();
            SqlDataReader dr =  cmd.ExecuteReader();
            return dr;
        }
    }
}
