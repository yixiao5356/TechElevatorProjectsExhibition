using Capstone.DAO;
using Capstone.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Transactions;

namespace CapstoneTest
{
    [TestClass]
    class ResponseProcessingDAOTest
    {
        
            private string connectionString = @"Server=.\SQLEXPRESS;Database=final_capstone;Trusted_Connection=True;";

            private TransactionScope transaction;

            [TestInitialize]
            public void Initalize()
            {
                transaction = new TransactionScope();

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string sql_insert = "SET IDENTITY_INSERT positions ON INSERT INTO positions (id, name, description, categoryID) " +
                        "VALUES (9999, 'Tech Elevator instructor', 'position is about to open up', 9999) SET IDENTITY_INSERT positions OFF;";
                    SqlCommand cmd = new SqlCommand(sql_insert, conn);
                    int count = cmd.ExecuteNonQuery();

                    Assert.AreEqual(1, count, "Insert into positions succeeded");
                }
            }

            [TestCleanup]
            public void Cleanup()
            {
                transaction.Dispose();
            }

        [TestMethod]
        public void KeyWordSearch()
        {
            ResponseProcessingDAO response = new ResponseProcessingDAO(connectionString);

            DatabaseItem result = response.GetItemList("resume");

            Assert.IsNotNull(result);

            Assert.AreEqual("resume", result.Name);
        }


    }
}
