using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Capstone.DAL
{
    public class ReservationDAL
    {
        private string ConnectionString { get; set; }
        public ReservationDAL(string connectionstring)
        {
            ConnectionString = connectionstring;
            AvailableIdList = new HashSet<int>();
        }
        public HashSet<int> AvailableIdList { get; set; }
        public List<Space> AvailableSpaceList(DateTime date, int days, int month, int participant, int where)
        {
            List<Reservation> reservedspace = new List<Reservation>();
            List<Space> result = new List<Space>();

            string availableSpaceSelect = "  SELECT r.reservation_id reservationid, s.id spaceid, s.name name, s.daily_rate rate, s.max_occupancy maxocc, s.is_accessible accessible, r.start_date starting, r.end_date ending, s.open_from openfrom, s.open_to opento, s.max_occupancy maxocc " +
                "FROM space s LEFT OUTER JOIN reservation r ON s.id = r.space_id WHERE s.venue_id = @id; "; //AND ((r.start_date IS NULL OR @enddate < r.start_date) OR (r.end_date IS NULL OR @date > r.end_date) AND (@month >= s.open_from AND @month <= s.open_to));";
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(availableSpaceSelect, conn);
                //cmd.Parameters.AddWithValue("@date", date);
                //cmd.Parameters.AddWithValue("@enddate", date.AddDays(days));
                //cmd.Parameters.AddWithValue("@month", month);
                cmd.Parameters.AddWithValue("@id", where);


                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    try
                    {

                        Reservation item = new Reservation();
                        item.StartDate = Convert.ToDateTime(reader["starting"]);
                        item.EndDate = Convert.ToDateTime(reader["ending"]);
                        item.Id = Convert.ToInt32(reader["reservationid"]);
                        item.Space = Convert.ToString(reader["name"]);
                        reservedspace.Add(item);
                    }
                    catch
                    {

                    }
                }
                reader.Close();
                SqlCommand cmd2 = new SqlCommand(availableSpaceSelect, conn);
                cmd2.Parameters.AddWithValue("@id", where);
                SqlDataReader reader2 = cmd2.ExecuteReader();
                while (reader2.Read())
                {
                    int openFrom = 0;
                    int openTo = 13;
                    Space space = new Space();
                    int maxocc = Convert.ToInt32(reader2["maxocc"]);
                    string name = Convert.ToString(reader2["name"]);
                    try
                    {
                        openFrom = Convert.ToInt32(reader2["openfrom"]);
                        openTo = Convert.ToInt32(reader2["opento"]);
                    }
                    catch
                    {

                    }
                    //List<Reservation> sameId = new List<Reservation>();
                    //foreach (Reservation item in reservedspace)
                    //{
                    //    Reservation holder = new Reservation();
                    //    if (item.Space == Convert.ToString(reader2["name"]))
                    //    {
                    //        holder = item;
                    //        sameId.Add(holder);
                    //    }

                    //}
                    bool isGood = true;
                    foreach (Reservation item in reservedspace) //I need this to be pushed
                    {
                        if (item.Space == name)
                        {

                            if ((DateTime.Compare(date, item.EndDate) < 0 && DateTime.Compare(date.AddDays(days), item.StartDate) > 0))
                            {
                                isGood = false;

                            }
                        }
                    }
                    if (isGood)
                    {
                        if (month > openFrom && month < openTo)
                        {
                            if (maxocc > participant)
                            {

                                space.Id = Convert.ToInt32(reader2["spaceid"]);
                                space.Name = name;
                                space.DailyRate = Convert.ToDecimal(reader2["rate"]);
                                space.MaxOccupancy = Convert.ToInt32(reader2["maxocc"]);
                                space.Accessible = YesOrNo(Convert.ToBoolean(reader2["accessible"]));
                                if (!AvailableIdList.Contains(space.Id))
                                {

                                    AvailableIdList.Add(space.Id);

                                    result.Add(space);
                                }
                            }
                        }

                    }
                    else
                    {
                        isGood = true;
                    }
                }

            }
            return result;


        }

        public Reservation toReserve(int id, string reserveFor, DateTime when, int days, int attendee)
        {
            if (AvailableIdList.Contains(id))
            {
                Reservation result = new Reservation();
                string addToReserve = "INSERT INTO reservation (space_id, number_of_attendees, start_date, end_date, reserved_for) " +
                    "VALUES (@space_id, @number_of_attendees, @start_date, @end_date, @reserved_for);" +
                    "SELECT TOP 1 r.reservation_id Confirmation#, v.name venue, s.name space, r.reserved_for reservedFor, r.number_of_attendees attendees, r.start_date ArrvialDate, r.end_date DepartDate, s.daily_rate rate FROM reservation r  " +
                    "JOIN space s ON r.space_id = s.id JOIN venue v on s.venue_id = v.id ORDER BY r.reservation_id desc; ";
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(addToReserve, conn);
                    cmd.Parameters.AddWithValue("@space_id", id);
                    cmd.Parameters.AddWithValue("@number_of_attendees", attendee);
                    cmd.Parameters.AddWithValue("@start_date", when);
                    cmd.Parameters.AddWithValue("@end_date", when.AddDays(days));
                    cmd.Parameters.AddWithValue("@reserved_for", reserveFor);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        result.Id = Convert.ToInt32(reader["Confirmation#"]);
                        result.Space = Convert.ToString(reader["space"]);
                        result.Venue = Convert.ToString(reader["venue"]);
                        result.ReserveFor = Convert.ToString(reader["reservedFor"]);
                        result.NumberOfAttendee = Convert.ToInt32(reader["attendees"]);
                        result.StartDate = Convert.ToDateTime(reader["ArrvialDate"]);
                        result.EndDate = Convert.ToDateTime(reader["DepartDate"]);
                        result.Total = Convert.ToInt32(reader["rate"]) * days;
                    }
                }

                return result;
            }
            else
            {
                return new Reservation();
            }
        }

        public string YesOrNo(bool isTure)
        {
            string result;
            if (isTure)
            {
                result = "Yes";
            }
            else
            {
                result = "No";
            }
            return result;
        }

    }
}
