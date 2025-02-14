using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    class SQLService
    {
        private readonly string _connectionString = "Server=localhost;Database=KD;Trusted_Connection=True;";

        public List<string> GetList(string query)
        {
            var resultList = new List<string>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                try
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query,conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while(reader.Read())
                            {
                                string item =reader.GetString(0);
                                resultList.Add(item);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    resultList.Clear();
                    resultList.Add(ex.Message);
                }

                return resultList;
            }
        }

        /// <summary>
        /// The function gets a query that dont need retuen results
        /// Example: INSERT\UPDATE\ETC..
        /// and execute it in sql
        /// </summary>
        /// <param name="query">the query to execute</param>
        public void ExecuteNonQuery(string query)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                try
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.ExecuteNonQuery(); 
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred: {ex.Message}");
                }
            }
        }
        public void ExecuteNonQuery(string query,SqlParameter[] parameters)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                try
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Add parameters to the command
                        if (parameters != null)
                        {
                            command.Parameters.AddRange(parameters);
                        }
                        command.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    // Handle the error appropriately
                    Console.WriteLine($"An error occurred: {ex.Message}");
                }
            }
        }

        /// <summary>
        /// Executes a SQL query that returns a single value (e.g., a single column from a single row) and returns it as a string.
        /// </summary>
        /// <param name="query">the quert to run</param>
        /// <returns></returns>
        public string GetString(string query)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                try
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Execute the query and retrieve the first column from the first row
                        var result = command.ExecuteScalar();  // Returns the first column of the first row

                        if (result != null)
                        {
                            return result.ToString();  // Convert it to string and return
                        }
                        else
                        {
                            return null;  // Return null if no result found
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Handle the error appropriately
                    Console.WriteLine($"An error occurred: {ex.Message}");
                    return null;
                }
            }
        }

        public string GetString(string query, SqlParameter[] parameters)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                try
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Add parameters to the command
                        if (parameters != null)
                        {
                            command.Parameters.AddRange(parameters);
                        }
                        // Execute the query and retrieve the first column from the first row
                        var result = command.ExecuteScalar();  // Returns the first column of the first row

                        if (result != null)
                        {
                            return result.ToString();  // Convert it to string and return
                        }
                        else
                        {
                            return null;  // Return null if no result found
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Handle the error appropriately
                    Console.WriteLine($"An error occurred: {ex.Message}");
                    return null;
                }
            }
        }

        public DataTable GetDataTable(string query, SqlParameter[] parameters)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                try
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Add parameters to the command if provided
                        if (parameters != null)
                        {
                            command.Parameters.AddRange(parameters);
                        }

                        // Use SqlDataAdapter to fill the DataTable
                        SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
                        DataTable dataTable = new DataTable();
                        dataAdapter.Fill(dataTable);  // Fill the DataTable with the result of the query

                        return dataTable;  // Return the populated DataTable
                    }
                }
                catch (Exception ex)
                {
                    // Handle the error appropriately
                    Console.WriteLine($"An error occurred: {ex.Message}");
                    return null;  // Return null in case of an error
                }
            }
        }


    }
}
