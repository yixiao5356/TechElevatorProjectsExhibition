using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using Capstone.Models;

namespace Capstone.DAL
{
    public class VenueDAL
    {
        private string ConnectionString { get; set; }
        public VenueDAL(string connectionstring)
        {
            ConnectionString = connectionstring;
        }
        public List<string> NameList()
        {
            List<string> result = new List<string> ();
            string select = "SELECT name FROM venue;";
            using(SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(select, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    result.Add(Convert.ToString(reader["name"]));
                }
            }
            return result;
        }
        public Venue VenueDetails(string connectionString, string input)
        {
            Venue venue = new Venue();
            string select = "SELECT v.id, v.name venuename, c.name cityname, c.state_abbreviation state, description FROM venue v JOIN city c on v.city_id = c.id WHERE v.id = @id;";
            string selectcategory = "SELECT ca.name category FROM venue v JOIN category_venue cv on v.id = cv.venue_id JOIN category ca ON ca.id = cv.category_id WHERE v.id = @id;";
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(select, conn);
                cmd.Parameters.AddWithValue("@id", input);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    
                    venue.Id = Convert.ToInt32(reader["id"]);
                    venue.Name = Convert.ToString(reader["venuename"]);
                    venue.Location = Convert.ToString(reader["cityname"]) + ", " + Convert.ToString(reader["state"]);
                    venue.description = Convert.ToString(reader["description"]);
                    
                }
            }
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                SqlCommand cmd2 = new SqlCommand(selectcategory, conn);
                cmd2.Parameters.AddWithValue("@id", venue.Id);
                SqlDataReader reader2 = cmd2.ExecuteReader();
                while (reader2.Read())
                {
                    venue.category += Convert.ToString(reader2["category"]) + " ";
                }
            }
            return venue;
        }
       
    }
}
