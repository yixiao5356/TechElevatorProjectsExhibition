using Capstone.DAL;
using Capstone.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Capstone.Tests
{
    [TestClass]
    public class ReservationDALtest : ParentTest
    {
        [TestMethod]
        public void OverlappintReservationTest()
        {
            string insertReservation1 = "  INSERT INTO reservation (space_id, number_of_attendees, start_date, end_date, reserved_for) " +
                "VALUES(1, 100, '2030-04-15', '2030-04-20', 'TestReservation0'); ";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(insertReservation1, conn);
                cmd.ExecuteNonQuery();
            }
            ReservationDAL test = new ReservationDAL(connectionString);

            DateTime dateholder = Convert.ToDateTime("2030/04/14");
            List<Space> testList = test.AvailableSpaceList(dateholder, 10, dateholder.Month, 30, 1);
            int result = 0;
            foreach (Space item in testList)
            {
                if (item.Id == 1)
                {
                    result = 1;
                }
            }
            Assert.AreEqual(0, result);
            test.toReserve(1, "causing problem", dateholder, 10, 30);
            string selectProblem = "SELECT * FROM reservation WHERE reserved_for = 'causing problem';";
            string holder = "";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(selectProblem, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    holder = Convert.ToString(reader["reserved_for"]);
                }
            }
            Assert.AreEqual("", holder);


        }
        [TestMethod]
        public void AvailableSpaveListTest()
        {
            ReservationDAL test = new ReservationDAL(connectionString);
            DateTime dateholder = Convert.ToDateTime("2050/05/01");
            List<Space> result = test.AvailableSpaceList(dateholder, 10, dateholder.Month, 10, 10);
            string select = "SELECT id FROM space WHERE venue_id = 10;";
            List<int> expect = new List<int>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(select, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    int holder = Convert.ToInt32(reader["id"]);
                    expect.Add(holder);
                }
            }
            bool isgood = false;
            foreach (Space space in result)
            {
                foreach (int id in expect)
                {
                    if (space.Id == id)
                    {
                        isgood = true;
                    }
                }
            }
            Assert.IsTrue(isgood);
        }
        [TestMethod]
        public void toReserveTest()
        {
            ReservationDAL test = new ReservationDAL(connectionString);
            test.AvailableIdList.Add(1);
            DateTime dateholder = Convert.ToDateTime("2050/04/01");
            test.toReserve(1, "causing problem", dateholder, 10, 30);
            string selectProblem = "SELECT * FROM reservation WHERE reserved_for = 'causing problem';";
            string holder = "";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(selectProblem, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    holder = Convert.ToString(reader["reserved_for"]);
                }
            }
            Assert.AreEqual("causing problem", holder);
        }

    }
}
