using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using Capstone.Models;

namespace Capstone.DAL
{
    public class SpaceDAL
    {
        private string ConnectionString { get; set; }
        public SpaceDAL(string connectionstring)
        {
            ConnectionString = connectionstring;
        }
        public List<Space> SpaceList(int id)
        {
            List<Space> result = new List<Space>();

            string select = "SELECT * FROM space WHERE venue_id = @id;";
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(select, conn);
                cmd.Parameters.AddWithValue("@id", id);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Space space = new Space();
                    space.Id = Convert.ToInt32(reader["id"]);
                    space.Name = Convert.ToString(reader["name"]);
                    space.Open = Month(Convert.ToString(reader["open_from"]));
                    space.Close = Month(Convert.ToString(reader["open_to"]));
                    space.DailyRate = Convert.ToDecimal(reader["daily_rate"]);
                    space.MaxOccupancy = Convert.ToInt32(reader["max_occupancy"]);
                    result.Add(space);
                }
            }
            return result;
        }
        public string Month(string num)
        {
            string result = "";
            switch (num)
            {
                case "1":
                    result = "Jan.";
                    break;
                case "2":
                    result = "Feb.";
                    break;
                case "3":
                    result = "Mar.";
                    break;
                case "4":
                    result = "Apr.";
                    break;
                case "5":
                    result = "May";
                    break;
                case "6":
                    result = "Jun.";
                    break;
                case "7":
                    result = "Jul.";
                    break;
                case "8":
                    result = "Aug.";
                    break;
                case "9":
                    result = "Sept.";
                    break;
                case "10":
                    result = "Oct.";
                    break;
                case "11":
                    result = "Nov.";
                    break;
                case "12":
                    result = "Dec.";
                    break;

            }



            return result;
        }
    }

}
