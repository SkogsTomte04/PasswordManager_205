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
                conn.Close();
            }




        }
        public void AddCredentials(ActiveUser User, string[] credentials )
        {
            // credentials = { _domain, _email, _username, _password, _note }
            DateTime date = DateTime.Now;

            // Encrypt the password
            string encrypted_password = KeyLoader.Encrypt(System.Text.Encoding.UTF8.GetBytes(credentials[3]), User.GetDataKey());


            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = @"INSERT INTO Credentials
                        (UserId, ServiceName, Email, Username, EncryptedPassword, Notes, DateCreated, DateUpdated)
                        VALUES
                        (@UserId, @ServiceName, @Email, @Username, @EncryptedPassword, @Notes, @DateCreated, @DateUpdated)";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@UserId", User._userId);
                    cmd.Parameters.AddWithValue("@ServiceName", credentials[0]);
                    cmd.Parameters.AddWithValue("@Email", credentials[1]);
                    cmd.Parameters.AddWithValue("@Username", credentials[2]);
                    cmd.Parameters.AddWithValue("@EncryptedPassword", encrypted_password); // Already encrypted
                    cmd.Parameters.AddWithValue("@Notes", credentials[4]);
                    cmd.Parameters.AddWithValue("@DateCreated", date);
                    cmd.Parameters.AddWithValue("@DateUpdated", date);

                    try
                    {
                        conn.Open();
                        int rows = cmd.ExecuteNonQuery();
                        if (rows > 0)
                        {
                            MessageBox.Show("Credential added successfully.");
                        }
                        else
                        {
                            MessageBox.Show("No rows inserted");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message);
                    }
                    conn.Close();
                    PrintAllPasswords();
                }
            }

        }
        public ActiveUser LogInUser(string user, string pass)
        {
            
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                string query = "SELECT MasterPasswordHash, UserId FROM Users WHERE MasterUsername = @username";
                try
                {
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        int userID = 0;
                        string masterPassHash = "";
                        string passtoHash = "";
                        cmd.Parameters.AddWithValue("@username", user);


                        using var reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            userID = reader.GetInt32(1);
                            masterPassHash = reader.GetString(0);
                            passtoHash = HashService.ComputetoHash(pass);
                            if (HashService.CheckHash(passtoHash, masterPassHash))
                            {
                                if (!KeyLoader.IsUserInitialized(user))
                                {
                                    KeyLoader.FirstTimeSetup(user, pass);
                                }
                                MessageBox.Show("Password passed the hash test");
                                conn.Close();
                                ActiveUser activeUser = new ActiveUser(userID, user, pass);
                                return activeUser;
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
        public void PrintAllPasswords()
        {
            string query = "SELECT ServiceName, UserId, EncryptedPassword FROM Credentials";

            using (SqlConnection conn = new SqlConnection(connStr))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    int id = reader.GetInt32(1);
                    string username = reader.GetString(0);
                    string passwordHash = reader.GetString(2);

                    MessageBox.Show($"UserId: {id}, \nServiceName: {username},\nEncrypted: {passwordHash}");
                }
            }
        }
        

    }
}
