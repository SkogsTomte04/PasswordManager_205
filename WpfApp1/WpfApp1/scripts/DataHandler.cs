using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
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
        string dbPath = "";

        string connStr = "";



        public DataHandler()
        {
            string appDataPath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "PasswordManager");

            dbPath = Path.Combine(appDataPath, "Database1.mdf");

            connStr = $@"Data Source=(LocalDB)\MSSQLLocalDB;
            AttachDbFilename={dbPath};
            Integrated Security=True;
            Database=PasswordManagerDB;";

            if (!Directory.Exists(appDataPath))
                Directory.CreateDirectory(appDataPath);

            if (!File.Exists(dbPath))
            {
                // Copy from app folder 
                File.Copy("Data\\Database1.mdf", dbPath, overwrite: false);
                MessageBox.Show("File does not exist, creating...");
            }
        }
        public void AddUser(string username,string password)
        {
            string query = "INSERT INTO Users (MasterUsername, MasterPasswordHash) VALUES (@Username, @PasswordHash)";


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
                try
                {
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        string userID = "";
                        string masterPassHash = "";
                        string passtoHash = "";
                        cmd.Parameters.AddWithValue("@username", user);


                        using var reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            userID = reader.GetInt32(1).ToString();
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
                        else { conn.Close(); MessageBox.Show("SQL READER ERROR"); return null; }
                    }

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
                catch (Exception ex) { MessageBox.Show(ex.Message); }
                return null;
            }
        }
        public void testDB()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();

                    using (SqlCommand cmd = new SqlCommand("SELECT * FROM Users WHERE Username = @Username", conn))
                    {
                        cmd.Parameters.AddWithValue("@Username", "skogstomte101");

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    string passwordHash = reader["PasswordHash"].ToString();
                                    // Use the data
                                }
                            }
                            else
                            {
                                MessageBox.Show("No rows found — check your query or data.");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
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
