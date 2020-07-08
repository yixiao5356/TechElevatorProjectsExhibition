using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TenmoServer.Models;

namespace TenmoServer
{
    public interface IAccountInfoDAO
    {
        decimal UserBalance(int userId);
        int Transfer(int fromUserID, int toUserID, decimal transferAmount);
        List<Transfer> transfersRecord(int userId);
        int Approve(int fromUserID, int toUserID, decimal transferAmount, int transferId);
        int Request(int fromUserID, int toUserID, decimal transferAmount);
        int Reject(int fromUserId, int transferId);
    }
}
