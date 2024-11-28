using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using UserProfileMVC.Models;
using System.Configuration;

namespace UserProfileMVC.DAL
{
    public class UserRepository
    {
        private string connectionString;

        public UserRepository()
        {
            // Get the connection string from Web.config
            connectionString = ConfigurationManager.ConnectionStrings["UserProfileDB"].ConnectionString;
        }

        // Method to retrieve all users
        public List<User> GetAllUsers()
        {
            List<User> users = new List<User>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string sql = "SELECT * FROM Users";
                SqlCommand cmd = new SqlCommand(sql, conn);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    User user = new User
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        Username = reader["Username"].ToString(),
                        Email = reader["Email"].ToString(),
                        Password = reader["Password"].ToString()
                    };
                    users.Add(user);
                }
            }

            return users;
        }

        // Method to add a new user
        public void AddUser(User user)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string sql = "INSERT INTO Users (Username, Email, Password) VALUES (@Username, @Email, @Password)";
                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@Username", user.Username);
                cmd.Parameters.AddWithValue("@Email", user.Email);
                cmd.Parameters.AddWithValue("@Password", user.Password);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        // Method to update an existing user
        public void UpdateUser(User user)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string sql = "UPDATE Users SET Username = @Username, Email = @Email, Password = @Password WHERE Id = @Id";
                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@Id", user.Id);
                cmd.Parameters.AddWithValue("@Username", user.Username);
                cmd.Parameters.AddWithValue("@Email", user.Email);
                cmd.Parameters.AddWithValue("@Password", user.Password);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        // Method to delete a user
        public void DeleteUser(int id)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string sql = "DELETE FROM Users WHERE Id = @Id";
                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@Id", id);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}
