using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using Capstone.DAL;
using Capstone.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Capstone.Tests
{
    [TestClass]
    public class SpaceDALTest : ParentTest
    {
        [TestMethod]
        public void SpaceListTest()
        {
            SpaceDAL test = new SpaceDAL(connectionString);
            List<Space> result = test.SpaceList(11);
            string select = "SELECT id FROM space WHERE venue_id = 11;";
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
    }
}
