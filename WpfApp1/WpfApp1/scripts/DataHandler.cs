using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

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
                    Debug.WriteLine("Exception error: " + ex.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error inserting user: " + ex.Message);
                }
            }




        }
        public string[] LogInUser(string user, string pass)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                string query = "SELECT MasterPasswordHash, UserId FROM Users WHERE MasterUsername = @username";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    string userID = "";
                    string masterPassHash = "";
                    string passtoHash = "";
                    cmd.Parameters.AddWithValue("@username", user);


                    using var reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        userID = reader.GetString(1);
                        masterPassHash = reader.GetString(0);
                        passtoHash = HashService.ComputetoHash(pass);
                        if (HashService.CheckHash(passtoHash, masterPassHash))
                        {
                            MessageBox.Show("Password passed the hash test");
                            conn.Close();
                            return [user, userID];
                        }
                        else
                        {
                            MessageBox.Show("Password did NOT pass the hash test");
                            conn.Close();
                            return null;
                        }
                    }
                    else { conn.Close(); MessageBox.Show("SQL Reader Failed"); return null; }

                    /*int count = (int)cmd.ExecuteScalar();
                    if (count > 0)
                    {
                        MessageBox.Show("Login successful!");
                        // Navigate to main application window
                        return true;
                    }
                    else
                    {
                        MessageBox.Show("Invalid username or password.");
                        return false;
                    }*/
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
                while (reader.Read())
                {
                    int id = reader.GetInt32(0);
                    string username = reader.GetString(1);
                    string passwordHash = reader.GetString(2);

                    Debug.WriteLine($"UserId: {id}, Username: {username}, Hash: {passwordHash}");
                }
            }
        }

    }
}
