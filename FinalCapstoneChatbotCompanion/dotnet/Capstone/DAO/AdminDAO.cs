using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.DAO
{
    public class AdminDAO : IAdminDAO
    {
        public string Connectionstring { get; set; }

        public AdminDAO(string connection)
        {

            Connectionstring = connection;
        }

        public string categoryKeywordListingSQL = "SELECT id, name FROM category";
        public string pathwayKeywordListingSQL = "SELECT name FROM pathway";
        public string curriculumKeywordListingSQL = "SELECT name FROM curriculum";
        public string positionKeywordListingSQL = "SELECT name FROM positions";
        public string getPathwayItemSQL = "SELECT pathway.id id, pathway.name name, pathway.description description, pathway.website website, pathway.weight weight, category.name categoryname, category.id categoryid " +
           "FROM pathway INNER JOIN category ON pathway.categoryID = category.ID";
        public string getCurriculumItemSQL = "SELECT curriculum.id id, curriculum.name name, curriculum.description description, curriculum.website website, curriculum.weight weight, category.name categoryname, category.id categoryid " +
            "FROM curriculum INNER JOIN category ON curriculum.categoryID = category.ID";
        public string getPostionItemSQL = "SELECT positions.id id, positions.name name, positions.description description, positions.website website, category.name categoryname, category.id categoryid " +
            "FROM positions INNER JOIN category ON positions.categoryID = category.ID";
        public string getMotivationQuoteSQL = "SELECT quotes.id id, quotes.name name, quotes.description description, category.name categoryname, category.id categoryid " +
            "FROM quotes INNER JOIN category ON quotes.categoryID = category.ID";

        //public List<string> GetKeywordSQL(string SQL)
        //{
        //    List<string> result = new List<string>();
        //    using (SqlConnection conn = new SqlConnection(Connectionstring))
        //    {
        //        conn.Open();
        //        SqlCommand cmd = new SqlCommand(SQL, conn);
        //        SqlDataReader reader = cmd.ExecuteReader();
        //        while (reader.Read())
        //        {
        //            string name = "";
        //            name = Convert.ToString(reader["name"]);
        //            result.Add(name);
        //        }
        //    }
        //    return result;
        //}


        //public Dictionary<string, string> GetKeywordList()
        //{
        //    Dictionary<string, string> result = new Dictionary<string, string>();
        //    Dictionary<string, string> category = GetKeywordSQL(categoryKeywordListingSQL).ToDictionary((x) => x, (x) => "category");
        //    Dictionary<string, string> pathway = GetKeywordSQL(pathwayKeywordListingSQL).ToDictionary((x) => x, (x) => "pathway");
        //    Dictionary<string, string> curriculum = GetKeywordSQL(curriculumKeywordListingSQL).ToDictionary((x) => x, (x) => "curriculum");
        //    Dictionary<string, string> position = GetKeywordSQL(positionKeywordListingSQL).ToDictionary((x) => x, (x) => "positions");
        //    category.ToList().ForEach(x => result.Add(x.Key, x.Value));
        //    pathway.ToList().ForEach(x => result.Add(x.Key, x.Value));
        //    curriculum.ToList().ForEach(x => result.Add(x.Key, x.Value));
        //    position.ToList().ForEach(x => result.Add(x.Key, x.Value));
        //    return result;
        //}

        
        public List<DatabaseItem> QuotesToDisplayToAdmin()
        {
            List<DatabaseItem> quoteItems = new List<DatabaseItem>();

            using (SqlConnection conn = new SqlConnection(Connectionstring))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(getMotivationQuoteSQL, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    DatabaseItem item = new DatabaseItem();
                    item.ID = Convert.ToInt32(reader["id"]);
                    item.Name = Convert.ToString(reader["name"]);
                    item.Description = Convert.ToString(reader["description"]);
                    item.CategoryName = Convert.ToString(reader["categoryname"]);
                    item.CategoryId = Convert.ToInt32(reader["categoryid"]);


                    quoteItems.Add(item);
                }
                return quoteItems;
            }

        }

        public List<DatabaseItem> PositionsToDisplayToAdmin()
        {
            List<DatabaseItem> positionItems = new List<DatabaseItem>();

            using (SqlConnection conn = new SqlConnection(Connectionstring))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(getPostionItemSQL, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    DatabaseItem item = new DatabaseItem();
                    item.ID = Convert.ToInt32(reader["id"]);
                    item.Name = Convert.ToString(reader["name"]);
                    item.Description = Convert.ToString(reader["description"]);
                    item.Website = Convert.ToString(reader["website"]);
                    //if (item.Website.Contains("|"))
                    //{
                    //    string[] webs = item.Website.Split("|");
                    //    for (int i = 0; i < webs.Length; i++)
                    //    {
                    //        webs[i] = "<a target = '_blank' href = " + webs[i] + ">" + webs[i] + "</a>";
                    //    }
                    //    item.Website = string.Join("\n", webs);
                    //}
                    //else
                    //{
                    //    item.Website = "<a target = '_blank' href = " + item.Website + ">" + item.Website + "</a>";
                    //}
                    item.CategoryName = Convert.ToString(reader["categoryname"]);
                    item.CategoryId = Convert.ToInt32(reader["categoryid"]);

                    positionItems.Add(item);
                }
                return positionItems;
            }

        }

        public List<DatabaseItem> CurriculumToDisplayToAdmin()
        {
            List<DatabaseItem> curriculumItems = new List<DatabaseItem>();

            using (SqlConnection conn = new SqlConnection(Connectionstring))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(getCurriculumItemSQL, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    DatabaseItem item = new DatabaseItem();
                    item.ID = Convert.ToInt32(reader["id"]);
                    item.Name = Convert.ToString(reader["name"]);
                    item.Description = Convert.ToString(reader["description"]);
                    item.CategoryName = Convert.ToString(reader["categoryname"]);
                    item.Weight = Convert.ToInt32(reader["weight"]);
                    item.CategoryId = Convert.ToInt32(reader["categoryid"]);
                    try
                    {
                        item.Website = Convert.ToString(reader["website"]);
                    //    if (item.Website.Contains("|"))
                    //    {
                    //        string[] webs = item.Website.Split("|");
                    //        for (int i = 0; i < webs.Length; i++)
                    //        {
                    //            webs[i] = "<a target = '_blank' href = " + webs[i] + ">" + webs[i] + "</a>";
                    //        }
                    //        item.Website = string.Join("\n", webs);
                    //    }
                    //    else
                    //    {
                    //        item.Website = "<a target = '_blank' href = " + item.Website + ">" + item.Website + "</a>";
                    //    }
                    }
                    catch(Exception e)
                    {

                    }
                    curriculumItems.Add(item);
                }
                return curriculumItems;
            }

        }

        public List<DatabaseItem> PathwayToDisplayToAdmin()
        {
            List<DatabaseItem> pathwayItems = new List<DatabaseItem>();

            using (SqlConnection conn = new SqlConnection(Connectionstring))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(getPathwayItemSQL, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                        DatabaseItem item = new DatabaseItem();
                        item.ID = Convert.ToInt32(reader["id"]);
                        item.Name = Convert.ToString(reader["name"]);
                        item.Description = Convert.ToString(reader["description"]);
                        item.CategoryName = Convert.ToString(reader["categoryname"]);
                        item.Weight = Convert.ToInt32(reader["weight"]);
                        item.CategoryId = Convert.ToInt32(reader["categoryid"]);
                    try
                    {
                        item.Website = Convert.ToString(reader["website"]);
                    //    if (item.Website.Contains("|"))
                    //    {
                    //        string[] webs = item.Website.Split("|");
                    //        for (int i = 0; i < webs.Length; i++)
                    //        {
                    //            webs[i] = "<a target = '_blank' href = " + webs[i] + ">" + webs[i] + "</a>";
                    //        }
                    //        item.Website = string.Join("\n", webs);
                    //    }
                    //    else
                    //    {
                    //        item.Website = "<a target = '_blank' href = " + item.Website + ">" + item.Website + "</a>";
                    //    }
                    }
                    catch(Exception e)
                    {

                    }

                    pathwayItems.Add(item);
                }
                return pathwayItems;
            }

        }

        public List<DatabaseItem> AllItems()
        {
            List<DatabaseItem> allItems = new List<DatabaseItem>();

            allItems.AddRange(PathwayToDisplayToAdmin());
            allItems.AddRange(CurriculumToDisplayToAdmin());
            allItems.AddRange(PositionsToDisplayToAdmin());
            allItems.AddRange(QuotesToDisplayToAdmin());

            return allItems;  
        }
            //Above just to send a list of all items (name, description, category name)

        public Dictionary<int,string> GetCategoryNameAndId()
        {
            Dictionary<int, string> categoryIdNamePair = new Dictionary<int, string>();
            try
            {
                using (SqlConnection conn = new SqlConnection(Connectionstring))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(categoryKeywordListingSQL, conn);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        categoryIdNamePair.Add(Convert.ToInt32(reader["id"]), Convert.ToString(reader["name"]));
                    }
                }
            }
            catch(Exception e)
            {

            }
            return categoryIdNamePair;
            
        }

        public bool AddPathwayToDatabase(DatabaseItem item)
        {
            
            bool result = false;
            int insertedItem = 0;
            Dictionary<int, string> categoryIdNamePair = GetCategoryNameAndId();
         

            using (SqlConnection conn = new SqlConnection(Connectionstring))
            {
                conn.Open();

                if (item.Website != null)
                {
                    SqlCommand cmd = new SqlCommand("INSERT INTO pathway (name, description, website, categoryID, weight) VALUES (@name, @description, @website, @categoryID, @weight);", conn);
                    cmd.Parameters.AddWithValue("@name", item.Name);
                    cmd.Parameters.AddWithValue("@description", item.Description);
                    cmd.Parameters.AddWithValue("@categoryID", item.CategoryId);
                    cmd.Parameters.AddWithValue("@weight", item.Weight);
                    cmd.Parameters.AddWithValue("@website", item.Website);
                    insertedItem = cmd.ExecuteNonQuery();
                }
                else
                {
                    SqlCommand cmd = new SqlCommand("INSERT INTO pathway (name, description, categoryID, weight) VALUES (@name, @description, @categoryID, @weight);", conn);
                    cmd.Parameters.AddWithValue("@name", item.Name);
                    cmd.Parameters.AddWithValue("@description", item.Description);
                    cmd.Parameters.AddWithValue("@categoryID", item.CategoryId);
                    cmd.Parameters.AddWithValue("@weight", item.Weight);
                    insertedItem = cmd.ExecuteNonQuery();
                }
              

                if (insertedItem == 1)
                {
                    result = true;
                }

            }

            return result;
        }

        public bool AddCurriculumToDatabase(DatabaseItem item)
        {

            bool result = false;
            Dictionary<int, string> categoryIdNamePair = GetCategoryNameAndId();


            using (SqlConnection conn = new SqlConnection(Connectionstring))
            {
                conn.Open();
                int insertedItem = 0;

                if (item.Website != null)
                {
                    SqlCommand cmd = new SqlCommand("INSERT INTO curriculum (name, description, website, categoryID, weight) VALUES (@name, @description, @website, @categoryID, @weight);", conn);
                    cmd.Parameters.AddWithValue("@name", item.Name);
                    cmd.Parameters.AddWithValue("@description", item.Description);
                    cmd.Parameters.AddWithValue("@categoryID", item.CategoryId);
                    cmd.Parameters.AddWithValue("@weight", item.Weight);
                    cmd.Parameters.AddWithValue("@website", item.Website);
                    insertedItem = cmd.ExecuteNonQuery();
                }
                else
                {
                    SqlCommand cmd = new SqlCommand("INSERT INTO curriculum (name, description, categoryID, weight) VALUES (@name, @description, @categoryID, @weight);", conn);
                    cmd.Parameters.AddWithValue("@name", item.Name);
                    cmd.Parameters.AddWithValue("@description", item.Description);
                    cmd.Parameters.AddWithValue("@categoryID", item.CategoryId);
                    cmd.Parameters.AddWithValue("@weight", item.Weight);
                    insertedItem = cmd.ExecuteNonQuery();
                }


                if (insertedItem == 1)
                {
                    result = true;
                }

            }

            return result;
        }

        public bool AddPositionToDatabase(DatabaseItem item)
        {

            bool result = false;
            Dictionary<int, string> categoryIdNamePair = GetCategoryNameAndId();


            using (SqlConnection conn = new SqlConnection(Connectionstring))
            {
                conn.Open();
                int insertedItem = 0;

                if (item.Website != null)
                {
                    SqlCommand cmd = new SqlCommand("INSERT INTO positions (name, description, website, categoryID, weight) VALUES (@name, @description, @website, @categoryID, @weight);", conn);
                    cmd.Parameters.AddWithValue("@name", item.Name);
                    cmd.Parameters.AddWithValue("@description", item.Description);
                    cmd.Parameters.AddWithValue("@categoryID", item.CategoryId);
                    cmd.Parameters.AddWithValue("@weight", item.Weight);
                    cmd.Parameters.AddWithValue("@website", item.Website);
                    insertedItem = cmd.ExecuteNonQuery();

                }
                else
                {
                    SqlCommand cmd = new SqlCommand("INSERT INTO positions (name, description, categoryID, weight) VALUES (@name, @description, @categoryID, @weight);", conn);
                    cmd.Parameters.AddWithValue("@name", item.Name);
                    cmd.Parameters.AddWithValue("@description", item.Description);
                    cmd.Parameters.AddWithValue("@categoryID", item.CategoryId);
                    cmd.Parameters.AddWithValue("@weight", item.Weight);
                    insertedItem = cmd.ExecuteNonQuery();
                }



                if (insertedItem == 1)
                {
                    result = true;
                }

            }

            return result;
        }

        public bool AddQuoteToDatabase(DatabaseItem item)
        {

            bool result = false;
            Dictionary<int, string> categoryIdNamePair = GetCategoryNameAndId();


            using (SqlConnection conn = new SqlConnection(Connectionstring))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO quotes (name, description, categoryID, weight) VALUES (@name, @description, @categoryID, @weight);", conn);
                cmd.Parameters.AddWithValue("@name", item.Name);
                cmd.Parameters.AddWithValue("@description", item.Description);
                cmd.Parameters.AddWithValue("@categoryID", item.CategoryId);
                cmd.Parameters.AddWithValue("@weight", item.Weight);


                int insertedItem = cmd.ExecuteNonQuery();

                if (insertedItem == 1)
                {
                    result = true;
                }

            }

            return result;
        }

        public bool UpdateItemInDatabase(DatabaseItem item)
        {
            bool result = false;
            int updatedItem = 0;
            Dictionary<int, string> categoryIdNamePair = GetCategoryNameAndId();
            using (SqlConnection conn = new SqlConnection(Connectionstring))
            {
                conn.Open();

                if (item.Website == null)
                {
                    SqlCommand cmd = new SqlCommand("UPDATE " + categoryIdNamePair[item.CategoryId] + " SET name=@name, description=@description, weight=@updatedWeight WHERE id=@id;", conn);
                    cmd.Parameters.AddWithValue("@updatedWeight", item.Weight);
                    cmd.Parameters.AddWithValue("@name", item.Name);
                    cmd.Parameters.AddWithValue("@id", item.ID);
                    cmd.Parameters.AddWithValue("@description", item.Description);
                    updatedItem = cmd.ExecuteNonQuery();
                }
                else
                {
                    SqlCommand cmd = new SqlCommand("UPDATE " + categoryIdNamePair[item.CategoryId] + " SET name=@name, description=@description, website=@website, weight=@updatedWeight WHERE id=@id;", conn);
                    cmd.Parameters.AddWithValue("@updatedWeight", item.Weight);
                    cmd.Parameters.AddWithValue("@name", item.Name);
                    cmd.Parameters.AddWithValue("@id", item.ID);
                    cmd.Parameters.AddWithValue("@description", item.Description);
                    cmd.Parameters.AddWithValue("@website", item.Website);
                    updatedItem = cmd.ExecuteNonQuery();
                }


                if(updatedItem == 1)
                {
                    result = true;
                }

            }

            return result;
        }

        public bool DeleteItemInDatabase(string tableName, int id)
        {
            bool result = false;

            using (SqlConnection conn = new SqlConnection(Connectionstring))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("Delete FROM " + tableName + " WHERE id=@id;", conn);
                cmd.Parameters.AddWithValue("@id", id);
                
               int deletedItem = cmd.ExecuteNonQuery();

                if (deletedItem == 1)
                {
                    result = true;
                }

            }

            return result;

        }
        public List<DatabaseItem> AllRequests()
        {
            List<DatabaseItem> RequestLists = new List<DatabaseItem>();

            string checkRecord = "SELECT * FROM requests;";

            using (SqlConnection conn = new SqlConnection(Connectionstring))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(checkRecord, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    DatabaseItem item = new DatabaseItem();
                    item.ID = Convert.ToInt32(reader["id"]);
                    item.Name = Convert.ToString(reader["name"]);
                    item.CategoryName = "requests";

                    RequestLists.Add(item);
                }


                return RequestLists;
            }
        }



        public bool AddCompanyToDatabase(DatabaseItem item)
        {

            bool result = false;
            //Dictionary<int, string> categoryIdNamePair = GetCategoryNameAndId();


            using (SqlConnection conn = new SqlConnection(Connectionstring))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO companies (name, description, location, number_of_employees, number_of_grads, names_of_grads, glassdoor_rating, website, categoryID) VALUES (@name, @description, @location, @number_of_employees, @number_of_grads, @names_of_grads, @glassdoor_rating, " +
                    "@website, @categoryID);", conn);

                if(item.Location == null)
                {
                    item.Location = "unknown";
                }
                if (item.NumberOfEmployees == null)
                {
                    item.NumberOfEmployees = "not known at this time";
                }
                if (item.NamesOfGrads == null)
                {
                    item.NamesOfGrads = "";
                }
                if (item.Website == null)
                {
                    item.Website = "";
                }
                cmd.Parameters.AddWithValue("@name", item.Name);
                cmd.Parameters.AddWithValue("@description", item.Description);
                cmd.Parameters.AddWithValue("@location", item.Location);
                cmd.Parameters.AddWithValue("@number_of_employees", item.NumberOfEmployees);
                cmd.Parameters.AddWithValue("@number_of_grads", item.NumberOfGrads);
                cmd.Parameters.AddWithValue("@names_of_grads", item.NamesOfGrads);
                cmd.Parameters.AddWithValue("@glassdoor_rating", item.Rating);
                cmd.Parameters.AddWithValue("@website", item.Website);
                cmd.Parameters.AddWithValue("@categoryID", item.CategoryId);
                


                int insertedItem = cmd.ExecuteNonQuery();

                if (insertedItem == 1)
                {
                    result = true;
                }

            }

            return result;
        }



        public bool UpdateCompanyForDatabase(DatabaseItem item)
        {

            bool result = false;
            //Dictionary<int, string> categoryIdNamePair = GetCategoryNameAndId();


            using (SqlConnection conn = new SqlConnection(Connectionstring))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("UPDATE companies SET name=@name, description=@description, location=@location, number_of_employees=@number_of_employees, number_of_grads=@number_of_grads, names_of_grads=@names_of_grads, glassdoor_rating=@glassdoor_rating, website=@website, categoryID=@categoryID WHERE id=@id;", conn);
                                                                                                               

                if (item.Location == null)
                {
                    item.Location = "unknown";
                }
                if (item.NumberOfEmployees == null)
                {
                    item.NumberOfEmployees = "not known at this time";
                }
                if (item.NamesOfGrads == null)
                {
                    item.NamesOfGrads = "";
                }
                if (item.Website == null)
                {
                    item.Website = "";
                }

                cmd.Parameters.AddWithValue("@id", item.ID);
                cmd.Parameters.AddWithValue("@name", item.Name);
                cmd.Parameters.AddWithValue("@description", item.Description);
                cmd.Parameters.AddWithValue("@location", item.Location);
                cmd.Parameters.AddWithValue("@number_of_employees", item.NumberOfEmployees);
                cmd.Parameters.AddWithValue("@number_of_grads", item.NumberOfGrads);
                cmd.Parameters.AddWithValue("@names_of_grads", item.NamesOfGrads);
                cmd.Parameters.AddWithValue("@glassdoor_rating", item.Rating);
                cmd.Parameters.AddWithValue("@website", item.Website);
                cmd.Parameters.AddWithValue("@categoryID", item.CategoryId);



                int insertedItem = cmd.ExecuteNonQuery();

                if (insertedItem == 1)
                {
                    result = true;
                }

            }

            return result;
        }
    }
}
