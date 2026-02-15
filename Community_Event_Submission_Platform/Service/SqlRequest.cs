using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;

namespace Community_Event_Submission_Platform.Service
{
    public class SqlRequest
    {
        public static DataTable ExecuteQuery(string query, Dictionary<string, string> parameters)
        {
            DataTable response = new DataTable();
            try
            {
                using (MySqlConnection connection = new MySqlConnection(ConfigurationManager.ConnectionStrings["Local"].ToString()))
                {
                    connection.Open();
                    using (MySqlCommand command = connection.CreateCommand())
                    {
                        command.CommandType = CommandType.Text;
                        command.CommandText = query;

                        if (parameters != null && parameters.Count > 0)
                        {
                            foreach (var param in parameters)
                            {
                                command.Parameters.AddWithValue(param.Key, param.Value);
                            }
                        }
                        MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                        adapter.Fill(response);
                    }
                    connection.Close();
                }
            }
            catch (Exception)
            {
                return response;
            }
            return response;
        }
    }
}