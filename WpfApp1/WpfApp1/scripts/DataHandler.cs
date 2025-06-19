using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.scripts
{

    internal class DataHandler
    {
        private string connStr = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Database1.mdf;Integrated Security=True";
        
        public void AddUser(string username,string password)
        {
            Debug.WriteLine("TESTING");

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                try
                {
                    conn.Open();
                    Debug.WriteLine("Connection success");
                }
                catch (Exception e)
                {
                    Debug.WriteLine("Connection Error: " + e.ToString());
                }

                conn.Close();

            }



            
        }

    }
}
