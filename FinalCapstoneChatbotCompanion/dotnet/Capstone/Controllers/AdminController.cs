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
    [Authorize(Roles = "admin")]
    [Route("[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminDAO adminDAO;
        private readonly ICompanyDAO companyDAO;

        public AdminController(IAdminDAO _adminDAO, ICompanyDAO _companyDAO)
        {
            adminDAO = _adminDAO;
            companyDAO = _companyDAO;
        }

        [Authorize]
        [HttpGet("requestrecord")]
        public List<DatabaseItem> ReturnAllRequests()
        {
            List<DatabaseItem> allItems = adminDAO.AllRequests();
            
            return allItems;
        }

        [Authorize]
        [HttpGet]
        public List<DatabaseItem> ReturnAllItems()
        {
            List<DatabaseItem> allItems = adminDAO.AllItems();
            List<DatabaseItem> companies = companyDAO.CompanyInformation();
            allItems.AddRange(companies);

            return allItems;
        }

        [Authorize]
        [HttpPost("add")]
        public IActionResult AddDatabaseItem(DatabaseItem item)
        {

            List<DatabaseItem> allItems = adminDAO.AllItems();

            if (item.CategoryId != 3)
            {

                foreach (DatabaseItem existingItem in allItems)
                {
                    if (item.Name.ToLower() == existingItem.Name.ToLower())
                    {
                        return BadRequest("This name is already in use");
                    }
                    else
                    {
                        continue;
                    }
                }
            }

            bool result = false;

            if (item.CategoryId == 1)
            {
                result = adminDAO.AddPathwayToDatabase(item);
            }
            else if (item.CategoryId == 2)
            {
                result = adminDAO.AddCurriculumToDatabase(item);
            }
            else if (item.CategoryId == 3)
            {
                result = adminDAO.AddQuoteToDatabase(item);
            }
            else if (item.CategoryId == 4)
            {
                result = adminDAO.AddPositionToDatabase(item);
            }
            else if(item.CategoryId == 5)
            {
                result = adminDAO.AddCompanyToDatabase(item);
            }


            if (result)
            {
                return Ok();
            } else
            {
                return BadRequest("Something went wrong");
            }
        }

        [Authorize]
        [HttpPut("update")]
        public IActionResult UpdateDatabaseItem(DatabaseItem item)
        {
            bool result = false;


            List<DatabaseItem> allItems = adminDAO.AllItems();

            foreach (DatabaseItem existingItem in allItems)
            {
                if (item.Name == existingItem.Name && item.ID != existingItem.ID)
                {
                    return BadRequest("This name is already in use");
                }
                else
                {
                    continue;
                }
            }

            if (item.CategoryId == 5)
            {
                result = adminDAO.UpdateCompanyForDatabase(item);
            }
            else
            {
                result = adminDAO.UpdateItemInDatabase(item);
            }

            if (result)
            {
                return Ok();
            }
            else
            {
                return BadRequest("Something went wrong");
            }
        }

        [Authorize]
        [HttpDelete("delete/{tableName}/{id}")]
        public IActionResult DeleteDatabaseItem(string tableName, int id)
        {
            bool result = adminDAO.DeleteItemInDatabase(tableName, id);

            if (result)
            {
                return Ok();
            }
            else
            {
                return BadRequest("Something went wrong");
            }
        }
    }
}