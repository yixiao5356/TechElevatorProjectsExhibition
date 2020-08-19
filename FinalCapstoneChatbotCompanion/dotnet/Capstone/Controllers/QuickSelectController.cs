using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Capstone.DAO;
using Capstone.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Capstone.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class QuickSelectController : ControllerBase
    {
        private readonly IQuickSelectDAO quickSelectDAO;

        public QuickSelectController(IQuickSelectDAO _quickSelectDAO)
        {
            quickSelectDAO = _quickSelectDAO;
        }


        [HttpGet]
        public List<DatabaseItem> ReturnTop5Topics()
        {
            List<DatabaseItem> top5Topics = quickSelectDAO.GetTop5WeightedTopics();

            return top5Topics;
        }

    }
}