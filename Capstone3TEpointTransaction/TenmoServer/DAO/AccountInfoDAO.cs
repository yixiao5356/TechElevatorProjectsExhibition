using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using TenmoServer.Models;

namespace TenmoServer.DAO
{
    public class AccountInfoDAO : IAccountInfoDAO
    {
        private string connectionString;
        public AccountInfoDAO(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }

        private string getSingleAccount = "SELECT balance FROM accounts Where user_id = @userId;";
        private string TransferCommand =
            "BEGIN TRANSACTION; " +
            "UPDATE accounts " +
            "SET balance = balance - @transferAmount Where user_id = @fromUserId; " +
            "UPDATE accounts " +
            "SET balance = balance + @transferAmount Where user_id = @toUserId; " +
            "INSERT INTO transfers(transfer_type_id, transfer_status_id, account_from, account_to, amount) " +
            "VALUES(2, 2, @fromUserId, @toUserId, @transferAmount); " +
            "COMMIT TRANSACTION;";
        private string PastTransfer = "  SELECT t.transfer_id transferId, uf.username fromUser, " +
            "ut.username toUser, ut.user_id toUserId, tt.transfer_type_desc type, ts.transfer_status_desc status, t.amount amount " +
            "FROM transfers t " +
            "JOIN transfer_statuses ts ON ts.transfer_status_id = t.transfer_status_id " +
            "JOIN transfer_types tt ON tt.transfer_type_id = t.transfer_type_id " +
            "JOIN users uf on t.account_from = uf.user_id " +
            "JOIN users ut on t.account_to = ut.user_id " +
            "WHERE t.account_from = @id OR t.account_to = @id;";
        private string RequestCommand = "INSERT INTO transfers(transfer_type_id, transfer_status_id, account_from, account_to, amount) " +
            "VALUES(1, 1, @fromUserId, @toUserId, @transferAmount);";
        private string ApproveCommand =
            "BEGIN TRANSACTION; " +
            "UPDATE accounts " +
            "SET balance = balance - @transferAmount Where user_id = @fromUserId; " +
            "UPDATE accounts " +
            "SET balance = balance + @transferAmount Where user_id = @toUserId; " +
            "UPDATE transfers " +
            "SET transfer_status_id = 2, account_to = account_from, account_from = @fromUserId  WHERE transfer_id = @transferId;" +
            "COMMIT TRANSACTION;";
        private string RejectCommand = "UPDATE transfers " +
            "SET transfer_status_id = 3, account_to = account_from, account_from = @fromUserId WHERE transfer_id = @transferId;";
        public int Reject(int fromUserId, int transferId)
        {
            int result = 0;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(RejectCommand, conn);
                    cmd.Parameters.AddWithValue("@fromUserId", fromUserId);
                    cmd.Parameters.AddWithValue("@transferId", transferId);
                    result = cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                throw;
            }

            return result;
        }

        public int Approve (int fromUserID, int toUserID, decimal transferAmount, int transferId)
        {
            int result = 0;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(ApproveCommand, conn);
                    cmd.Parameters.AddWithValue("@fromUserId", fromUserID);
                    cmd.Parameters.AddWithValue("@toUserId", toUserID);
                    cmd.Parameters.AddWithValue("@transferAmount", transferAmount);
                    cmd.Parameters.AddWithValue("@transferId", transferId);
                    result = cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                throw;
            }

            return result;
        }
        
        public int Request (int fromUserID, int toUserID, decimal transferAmount)
        {
            int result = 0;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(RequestCommand, conn);
                    cmd.Parameters.AddWithValue("@fromUserId", fromUserID);
                    cmd.Parameters.AddWithValue("@toUserId", toUserID);
                    cmd.Parameters.AddWithValue("@transferAmount", transferAmount);
                    result = cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                throw;
            }

            return result;
        }

        public List<Transfer> transfersRecord (int userId)
        {
            List<Transfer> result = new List<Transfer>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(PastTransfer, conn);
                    cmd.Parameters.AddWithValue("@id", userId);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Transfer holder = new Transfer();
                        holder.Id = Convert.ToInt32(reader["transferId"]);
                        holder.FromName = Convert.ToString(reader["fromUser"]);
                        holder.ToName = Convert.ToString(reader["toUser"]);
                        holder.ToUserID = Convert.ToInt32(reader["toUserId"]);
                        holder.Status = Convert.ToString(reader["status"]);
                        holder.Type = Convert.ToString(reader["type"]);
                        holder.Amount = Convert.ToDecimal(reader["amount"]);
                        result.Add(holder);
                    }
                }
            }
            catch (SqlException ex)
            {
                throw;
            }
            return result;
        }

        public int Transfer (int fromUserID, int toUserID, decimal transferAmount)
        {
            int result = 0;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(TransferCommand, conn);
                    cmd.Parameters.AddWithValue("@fromUserId", fromUserID);
                    cmd.Parameters.AddWithValue("@toUserId", toUserID);
                    cmd.Parameters.AddWithValue("@transferAmount", transferAmount);
                    result = cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                throw;
            }

            return result;
        }


        public decimal UserBalance (int userId)
        {
            decimal result = 0;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(getSingleAccount, conn);
                    cmd.Parameters.AddWithValue("@userId", userId);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {

                        result = Convert.ToDecimal(reader["balance"]);
                    }
                }
            }
            catch (SqlException ex)
            {
                throw;
            }

            return result;
        }
    }
}
