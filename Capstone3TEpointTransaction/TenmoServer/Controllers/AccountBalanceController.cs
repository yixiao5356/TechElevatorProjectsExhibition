using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TenmoServer.DAO;
using TenmoServer.Models;

namespace TenmoServer.Controllers
{

    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class AccountBalanceController : ControllerBase
    {
        private readonly IAccountInfoDAO accountInfo;

        public AccountBalanceController(IAccountInfoDAO _accountInfo)
        {
            accountInfo = _accountInfo;
        }

        [HttpGet]
        public IActionResult AccountInfo()
        {
            //return Ok("I am good");
            int userId = 0;
            foreach (var claim in User.Claims)
            {
                if (claim.Type == "sub")
                {
                    userId = int.Parse(claim.Value);
                }
            }
            decimal result = accountInfo.UserBalance(userId);
            return Ok(result);
        }
        
    }
}