using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Transactions;
using System.Data.SqlClient;
using Capstone.DAO;
using Capstone.Models;

namespace CapstoneTest
{
    [TestClass]
    public class UserSqlDAOTest
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

                string sql_insert = "INSERT INTO users (username, password_hash, salt, user_role) " +
                    "VALUES ('notauser', 'jjjjjjjjj', 'kkkkkkkkkk', 'user');";
                SqlCommand cmd = new SqlCommand(sql_insert, conn);
                int count = cmd.ExecuteNonQuery();

                Assert.AreEqual(1, count, "Insert into user failed");
            }
        }

        [TestCleanup]
        public void Cleanup()
        {
            transaction.Dispose();
        }


        [TestMethod]
        public void GetUserTest()
        {
            UserSqlDAO access = new UserSqlDAO(connectionString);

            User user = access.GetUser("notauser");

            Assert.IsNotNull(user);

            Assert.AreEqual("user", user.Role);
        }

        [TestMethod]
        public void AddUserTest()
        {
            UserSqlDAO access = new UserSqlDAO(connectionString);

            User user = access.AddUser("anotheruser", "password", "admin");

            Assert.IsNotNull(user);

            Assert.AreEqual("admin", user.Role);

            user = access.GetUser("anotheruser");

            Assert.IsNotNull(user);

            Assert.AreEqual("admin", user.Role);
        }
    }
}
