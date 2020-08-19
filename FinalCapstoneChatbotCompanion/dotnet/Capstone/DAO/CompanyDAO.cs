using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.DAO
{
    public class CompanyDAO : ICompanyDAO
    {
        public string ResponseHolder { get; set; }
        public string Connectionstring { get; set; }
        public string companySql = "SELECT companies.id id, companies.name name, companies.description description, companies.location location, companies.number_of_employees number_of_employees, companies.number_of_grads number_of_grads, companies.names_of_grads names_of_grads, companies.glassdoor_rating glassdoor_rating, companies.website website, companies.categoryID categoryID, category.name categoryname FROM companies JOIN category ON companies.categoryID = category.ID;";
        public string companyKeywordListingSQL = "SELECT id, name FROM companies";

        public CompanyDAO(string connection)
        {
            Connectionstring = connection;
        }

        public Dictionary<int, string> GetCompanyKeyWordList()
        {

            Dictionary<int, string> result = new Dictionary<int, string>();
            List<Company> company = GetCompanyNames(companyKeywordListingSQL);
            foreach(Company item in company)
            {
                result.Add(item.ID, item.Name);
            }
            return result;
        }

        public List<Company> GetCompanyNames(string SQL)
        {
            List<Company> result = new List<Company>();
            using (SqlConnection conn = new SqlConnection(Connectionstring))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(SQL, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Company company = new Company();
                    company.ID = Convert.ToInt32(reader["id"]);
                    company.Name = Convert.ToString(reader["name"]);
                    result.Add(company);
                }
            }
            return result;
        }



        public List<DatabaseItem> CompanyInformation()
        {
            List<DatabaseItem> companies = new List<DatabaseItem>();

            using (SqlConnection conn = new SqlConnection(Connectionstring))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(companySql, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    DatabaseItem company = new DatabaseItem();
                    company.ID = Convert.ToInt32(reader["id"]);
                    company.CategoryId = Convert.ToInt32(reader["categoryID"]);
                    company.Name = Convert.ToString(reader["name"]);
                    company.Description = Convert.ToString(reader["description"]);
                    company.Location = Convert.ToString(reader["location"]);
                    company.NumberOfEmployees = Convert.ToString(reader["number_of_employees"]);
                    company.NumberOfGrads = Convert.ToInt32(reader["number_of_grads"]);
                    company.NamesOfGrads = Convert.ToString(reader["names_of_grads"]);
                    company.Rating = Convert.ToDecimal(reader["glassdoor_rating"]);
                    company.Website = Convert.ToString(reader["website"]);
                    company.CategoryName = Convert.ToString(reader["categoryname"]);
                    
                    companies.Add(company);
                }
                return companies;
            }

        }
    }
}
