using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TenmoServer.DAO;
using TenmoServer.Models;
using TenmoServer.Security;

namespace TenmoServer.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class TransferController : ControllerBase
    {
        private readonly IUserDAO userDAO;
        private readonly IAccountInfoDAO accountInfoDAO; 

        public TransferController (IUserDAO _userDAO, IAccountInfoDAO _accountInfoDAO)
        {
            userDAO = _userDAO;
            accountInfoDAO = _accountInfoDAO;
        }
        [HttpGet]
        public IActionResult UserList()
        {
            int userId = 0;
            foreach (var claim in User.Claims)
            {
                if (claim.Type == "sub")
                {
                    userId = int.Parse(claim.Value);
                }
            }
            List<User> userList = userDAO.Users(userId);
            return Ok(userList);
        }
        [HttpPut("reject")]
        public IActionResult Rejecting(Transfer transfer)
        {
            int fromUserId = 0;
            foreach (var claim in User.Claims)
            {
                if (claim.Type == "sub")
                {
                    fromUserId = int.Parse(claim.Value);
                }
            }
            int shouldBeOne = accountInfoDAO.Reject(fromUserId, transfer.Id);
            if (shouldBeOne == 1)
            {

                string back = "Rejected";

                return Ok(back);
            }
            else
            {
                string back = "Error Occured";

                return BadRequest(back);
            }
        }

        [HttpPut("approve")]
        public IActionResult Approving (Transfer transfer)
        {
            int fromUserId = 0;
            foreach (var claim in User.Claims)
            {
                if (claim.Type == "sub")
                {
                    fromUserId = int.Parse(claim.Value);
                }
            }
            int shouldBeThree = accountInfoDAO.Approve(fromUserId, transfer.ToUserID, transfer.Amount, transfer.Id);
            if (shouldBeThree == 3)
            {

                string back = "Transfer Successful";

                return Ok(back);
            }
            else
            {
                string back = "Transfer Failed";

                return BadRequest(back);
            }
        }
        [HttpPost("request")]
        public IActionResult Requesting(TransferRequest request)
        {
            int fromUserId = 0;
            foreach (var claim in User.Claims)
            {
                if (claim.Type == "sub")
                {
                    fromUserId = int.Parse(claim.Value);
                }
            }
            int shouldBeOne = accountInfoDAO.Request(fromUserId, request.ToUserID, request.Amount);
            if (shouldBeOne == 1)
            {

                string back = "Request Successful";

                return Ok(back);
            }
            else
            {
                string back = "Request Failed";

                return BadRequest(back);
            }
        }
        [HttpPost]
        public IActionResult Transfering(TransferRequest request)
        {
            int fromUserId = 0;
            foreach (var claim in User.Claims)
            {
                if (claim.Type == "sub")
                {
                    fromUserId = int.Parse(claim.Value);
                }
            }
            int shouldBeThree = accountInfoDAO.Transfer(fromUserId, request.ToUserID, request.Amount);
            if (shouldBeThree == 3)
            {
                
                string back = "Transfer Successful";
                
                return Ok(back);
            }
            else
            {
                string back = "Transfer Failed";
                
                return BadRequest(back);
            }
        }
        [HttpGet("record")]
        public IActionResult Record()
        {
            int userId = 0;
            foreach (var claim in User.Claims)
            {
                if (claim.Type == "sub")
                {
                    userId = int.Parse(claim.Value);
                }
            }
            List<Transfer> result = accountInfoDAO.transfersRecord(userId);
            return Ok(result);
        }
    }
}