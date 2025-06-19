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
        private string connStr = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Database1.mdf;Integrated Security=True;";

        public void AddUser(string username,string password)
        {
            string query = "INSERT INTO Users (MasterUsername, MasterPasswordHash) VALUES (@Username, @PasswordHash)";

            /*Random rand = new Random();

            int userId = rand.Next(100, 999);*/

            using (SqlConnection conn = new SqlConnection(connStr))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                // Add parameter values safely to prevent SQL injection
                cmd.Parameters.AddWithValue("@Username", username);
                cmd.Parameters.AddWithValue("@PasswordHash", password);
                Debug.WriteLine("Connection try");
                try
                {
                    conn.Open();

                    Debug.WriteLine("Connection open");

                    int rows = cmd.ExecuteNonQuery();
                    Console.WriteLine($"{rows} row(s) inserted.");
                }
                catch (SqlException ex)
                {
                    Debug.WriteLine("is this the message you are looking for: " + ex.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error inserting user: " + ex.Message);
                }
            }




        }
        public void PrintAllUsers()
        {
            string query = "SELECT UserId, MasterUsername, MasterPasswordHash FROM Users";

            using (SqlConnection conn = new SqlConnection(connStr))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                Debug.WriteLine("It could be working 111");
                while (reader.Read())
                {
                    Debug.WriteLine("It could be working");
                    int id = reader.GetInt32(0);
                    string username = reader.GetString(1);
                    string passwordHash = reader.GetString(2);

                    Debug.WriteLine($"UserId: {id}, Username: {username}, Hash: {passwordHash}");
                }
            }
        }

    }
}
