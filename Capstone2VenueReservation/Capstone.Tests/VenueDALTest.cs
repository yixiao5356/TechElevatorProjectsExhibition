using Capstone.DAL;
using Capstone.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Capstone.Tests
{
    [TestClass]
    public class VenueDALTest: ParentTest
    {
        [TestMethod]
        public void NameListTest()
        {
            VenueDAL test = new VenueDAL(connectionString);
            List<string> result = test.NameList();
            string expect = "";
            string selectVenue = "SELECT * FROM venue;";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(selectVenue, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    expect = Convert.ToString(reader["name"]);
                }
            }
            Assert.IsTrue(result.Contains(expect));
        }
        [TestMethod]
        public void VenueDetailTest()
        {
            VenueDAL test = new VenueDAL(connectionString);
            Venue result = test.VenueDetails(connectionString, "5");
            Assert.AreEqual(5, result.Id);
        }
    }
}
