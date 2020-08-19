using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.DAO
{
    public class QuickSelectDAO : IQuickSelectDAO
    {

        public string Connectionstring { get; set; }
        public string getTop5WeightedTopicsSQL = "SELECT TOP 4 name, weight FROM " +
            "(SELECT name, weight FROM curriculum " +
            "UNION ALL SELECT name, weight FROM pathway) AS combinedTables " +
            "ORDER BY weight DESC;";

        public QuickSelectDAO(string connection)
        {

            Connectionstring = connection;
        }

        public List<DatabaseItem> GetTop5WeightedTopics()
        {
            List<DatabaseItem> top5Topics = new List<DatabaseItem>();
            try
            {
                using (SqlConnection conn = new SqlConnection(Connectionstring))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(getTop5WeightedTopicsSQL, conn);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        DatabaseItem item = new DatabaseItem();
                        item.Name = Convert.ToString(reader["name"]);
                        item.Weight = Convert.ToInt32(reader["weight"]);
                        top5Topics.Add(item);
                    }
                }
            }
            catch(Exception e)
            {

            }

            return top5Topics;
        }

    }
}
