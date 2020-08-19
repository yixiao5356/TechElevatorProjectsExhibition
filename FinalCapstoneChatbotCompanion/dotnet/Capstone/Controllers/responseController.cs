using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Capstone.DAO;
using Capstone.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.Kestrel.Core.Internal.Http;

namespace Capstone.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ResponseController : ControllerBase
    {
        public string Name { get; set; }
        private readonly IResponseProcessingDAO responseProcessing;
        private readonly ICompanyDAO company;

        public ResponseController(IResponseProcessingDAO _response, ICompanyDAO _company)
        {
            responseProcessing = _response;
            company = _company;
        }

        [HttpPost]
        public IActionResult RespondToUser(RequestItem request)
        {
            Dictionary<int, string> companyList = company.GetCompanyKeyWordList();
            string responseString = "";
            if (request.Text.Contains("asdewwega"))
            {
              string[] processedText = request.Text.Split(" ");
              Name = processedText[1];
              responseString = "Hi, " + Name + ", how can I help you today? Try asking a question related to your curriculum, your pathway, any available jobs. If you need motivation you can ask for a motivational quote! You can type in !help for a list of suggested searches."+
                        " Don't see something you want to know more about? Try typing \"!request:\" plus what you'd like to learn more about.";
            }
            else
            {
                if(request.Text.ToLower() == "!help")
                {
                    responseString = "Try some of these commands:\nI need help with my resume.\nWhat are some available jobs?\nI want to learn more about objects.\nI don't understand SQL. \nI want a motivational quote. \nShow me upcoming events. \nShow me companies that hire software developers. \n!request: How many bits in a byte?";
                }
                else if (request.Text.ToLower().Contains("!request:"))
                {
                    responseString = responseProcessing.RequestProcess(request.Text);
                }
                else if (request.Text.ToLower() == "companies" || request.Text.ToLower() == "company")
                {

                    foreach (KeyValuePair<int, string> company in companyList)
                    {
                        responseString += company.Value + "\n";
                    }

                    responseItem responseOne = new responseItem(responseString);
                    return Ok(responseOne);
                }
                else
                {
                    List<DatabaseItem> companyInfo = company.CompanyInformation();
                    foreach (DatabaseItem company in companyInfo)
                    {
                        if (request.Text.ToLower() == company.Name.ToLower())
                        {
                            responseString += "Company: " + company.Name + "\n" + company.Description + "\n" + "Location: " + company.Location + "\n" + "Number of Employees: " + company.NumberOfEmployees + "\n" +
                            "Number of TE Grads: " + company.NumberOfGrads + "\n" + "Names of TE Grads: " + company.NamesOfGrads + "\n" + "Glassdoor Rating: " + company.Rating + "\n" + "<a href=" + company.Website + "</a" + "\n";
                            responseItem responseTwo = new responseItem(responseString);
                            return Ok(responseTwo);
                        }
                    }

                    responseString = responseProcessing.ResponseProcess(request.Text);
                }

            }
            responseItem response = new responseItem(responseString);
            return Ok(response); 
        }




        


    }
}
