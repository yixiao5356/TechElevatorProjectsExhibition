using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System.IO;
using System.Text;
using System.Threading;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Capstone.DAO
{
    public class ResponseProcessingDAO : IResponseProcessingDAO
    {
        public string ResponseHolder { get; set; }
        public string Connectionstring { get; set; }
        public static DatabaseItem HoldOverItem { get; set; }
        public static List<DatabaseItem> HoldOverList { get; set; }
        public Dictionary<string, string> FoundKeyWords { get; set; }

        public string categoryKeywordListingSQL = "SELECT name FROM category";
        public string pathwayKeywordListingSQL = "SELECT name FROM pathway";
        public string curriculumKeywordListingSQL = "SELECT name FROM curriculum";
        public string positionKeywordListingSQL = "SELECT name FROM positions";
        public string getCategoryItemSQL = "SELECT category.id categoryid, category.name categoryname, category.description categorydescription FROM category WHERE name = @input";
        public string getPathwayItemSQL = "SELECT pathway.id pathwayid, pathway.name pathwayname, pathway.description pathwaydescription, pathway.website pathwaywebsite, category.name categoryname, category.description categorydescription, pathway.weight pathwayweight " +
            "FROM pathway INNER JOIN category ON pathway.categoryID = category.ID WHERE pathway.name = @input";
        public string getCurriculumItemSQL = "SELECT curriculum.id curriculumid, curriculum.name curriculumname, curriculum.description curriculumdescription, curriculum.website curriculumwebsite, category.name categoryname, category.description categorydescription, curriculum.weight curriculumweight " +
            "FROM curriculum INNER JOIN category ON curriculum.categoryID = category.ID WHERE curriculum.name = @input";
        public string getPostionItemSQL = "SELECT positions.id positionsid, positions.name positionsname, positions.description positionsdescription, positions.website positionswebsite, category.name categoryname, category.description categorydescription " +
            "FROM positions INNER JOIN category ON positions.categoryID = category.ID WHERE positions.name = @input";
        public string getMotivationQuoteSQL = "SELECT TOP 1 description FROM quotes ORDER BY NEWID()";
        public string getAllJobOpeningsSQL = "SELECT name, website FROM positions;";

        

        public ResponseProcessingDAO(string connection)
        {
            Connectionstring = connection;
        }
        public string ResponseProcess(string response)
        {

            FoundKeyWords = new Dictionary<string, string>();
            ResponseHolder = response.ToLower();
            string result = "";
            Dictionary<string, string> keywords = GetKeywordList();
            int keywordCounter = 0;
            string keyword = "";
            string keywordCategory = "";
            result = "I'm sorry, I couldn't quite understand that. If you want some examples of what to ask for, try typing !help.";

            //string[] responseArray = ResponseHolder.Split(".");
            //string tempString = string.Join(' ', responseArray);
            //responseArray = tempString.Split(",");
            //tempString = string.Join(' ', responseArray);
            //responseArray = tempString.Split("!");
            //tempString = string.Join(' ', responseArray);
            //responseArray = tempString.Split("?");
            //tempString = string.Join(' ', responseArray);
            //responseArray = tempString.Split(";");
            //tempString = string.Join(' ', responseArray);
            //responseArray = tempString.Split(":");
            //tempString = string.Join(' ', responseArray);

            //responseArray = tempString.Split(" ");

            foreach (KeyValuePair<string, string> item in keywords)
            {


                //    foreach(string word in responseArray)
                //    {
                //        if(word == item.Key)
                //        {
                //            keywordCounter++;
                //            keyword = item.Key;
                //            keywordCategory = item.Value;
                //            FoundKeyWords.Add(keyword, keywordCategory);
                //        }
                //    }

                if (ResponseHolder == item.Key.ToLower())
                {
                    keywordCounter++;
                    keyword = item.Key;
                    keywordCategory = item.Value;
                    keywordCounter = 0;
                    FoundKeyWords = new Dictionary<string, string>();
                    FoundKeyWords.Add(keyword, keywordCategory);
                    break;
                }

                else if (ResponseHolder.Contains(item.Key.ToLower()))
                {
                    keywordCounter++;
                    keyword = item.Key;
                    keywordCategory = item.Value;
                    FoundKeyWords.Add(keyword, keywordCategory);
                }

            }

            if (keywordCounter > 1)
            {
                result = ResponseToMultipleKeyWords();
            }
            else
            {
                result = ResponseToOneKeyWord();
            }

            if (result == "" || result == null)
            {
                result = "I'm sorry, I couldn't quite understand that. If you want some examples of what to ask for, try typing !help.";
            }
            return result;
        }

        public string ResponseToOneKeyWord()
        {
            string result = "";
            
           
            if (ResponseHolder.ToLower().Contains("motivation") || ResponseHolder.ToLower().Contains("quote") || ResponseHolder.ToLower().Contains("tired"))
            {
                result = GetMotivation();
                HoldOverItem = new DatabaseItem();
            }
            else if (ResponseHolder.ToLower().Contains("calendar") || ResponseHolder.ToLower().Contains("events"))
            {
                result = GetCalendarEvents();
                HoldOverItem = new DatabaseItem();
            }
            else if (ResponseHolder.ToLower().Contains("job") || ResponseHolder.ToLower().Contains("position") || ResponseHolder.ToLower().Contains("opening"))
            {
                result = GetAllJobListings();
                HoldOverItem = new DatabaseItem();
            }
            else if (ResponseHolder.ToLower() == "no" && HoldOverItem.Name != null)
            {
                HoldOverItem.Weight--;
                UpdateWeight(HoldOverItem);
                HoldOverItem = new DatabaseItem();
                result = "What did you want to learn more about? " + "\n";
                foreach (DatabaseItem item in HoldOverList)
                {
                    result += item.Name + "\n";
                }

                HoldOverList = new List<DatabaseItem>();

            }
            else if (ResponseHolder.ToLower() == "yes" && HoldOverItem.Name != null)
            {
                HoldOverList = new List<DatabaseItem>();
                HoldOverItem = new DatabaseItem();
                result = "Thank you";
            }
            else
            {
                HoldOverItem = new DatabaseItem();
                foreach (KeyValuePair<string, string> item in FoundKeyWords)
                {
                    if (item.Key != "")
                    {
                        DatabaseItem keyItem = GetItemList(item.Key, item.Value);

                        if (keyItem.Website == null)
                        {
                            keyItem.Website = "";
                        }

                        if (keyItem.Website.Contains("|"))
                        {
                            string[] websiteList = keyItem.Website.Split("|");
                            for (int i = 0; i < websiteList.Length; i++)
                            {
                                websiteList[i] = "<a target = '_blank' href = " + websiteList[i] + ">" + websiteList[i] + "</a>";
                            }
                            result = string.Join("\n", websiteList);
                        }
                        else
                        {
                            result = keyItem.Description + "\n" + "<a target = '_blank' href = " + keyItem.Website + ">" + keyItem.Website + "</a>";
                        }


                        if (keyItem.CategoryDescription != null && keyItem.CategoryName != null)
                        {
                            result += " \n\nThe information you requested was under the category " + keyItem.CategoryName + ": " + keyItem.CategoryDescription;
                            int updatedWeight = keyItem.Weight++;
                            UpdateWeight(keyItem);
                        }
                    }
                }
            }


            return result;
        }

        public string ResponseToMultipleKeyWords()
        {
            string result = "";

            if (ResponseHolder.ToLower().Contains("motivation") || ResponseHolder.ToLower().Contains("quote") || ResponseHolder.ToLower().Contains("tired"))
            {
                result = GetMotivation();
            }
            else if (ResponseHolder.ToLower().Contains("calendar") || ResponseHolder.ToLower().Contains("events"))
            {
                result = GetCalendarEvents();
                HoldOverItem = new DatabaseItem();
            }
            else if (ResponseHolder.ToLower().Contains("job") || ResponseHolder.ToLower().Contains("position") || ResponseHolder.ToLower().Contains("opening"))
            {
                result = GetAllJobListings();
            }
            else
            {
                List<DatabaseItem> keywordList = new List<DatabaseItem>();
                foreach (KeyValuePair<string, string> item in FoundKeyWords)
                {
                    if (item.Key != "")
                    {

                        DatabaseItem keyItem = GetItemList(item.Key, item.Value);
                        keywordList.Add(keyItem);

                        //result = keyItem.Description;
                        //if (keyItem.CategoryDescription != null && keyItem.CategoryName != null)
                        //{
                        //    result += " . \nThe information you requested was under the category " + keyItem.CategoryName + ". " + keyItem.CategoryDescription;
                        //}
                    }
                }

                keywordList = (keywordList.OrderBy(x => x.Weight)).ToList();

                List<DatabaseItem> positionList = keywordList.FindAll(x => x.CategoryName == "positions");
                if (positionList.Count > 0)
                {
                    foreach (DatabaseItem item in positionList)
                    {
                        result += item.Name + "\n\n" + item.Website + "\n\n";
                    }
                }
                else if (keywordList[keywordList.Count - 1].Weight - keywordList[keywordList.Count - 2].Weight >= 5)
                {
                    result = keywordList[keywordList.Count - 1].Description + "<a target = '_blank' href = " + keywordList[keywordList.Count - 1].Website + ">" + keywordList[keywordList.Count - 1].Website + "</a>";
                    result += " . \n\nThe information you requested was under the category " + keywordList[keywordList.Count - 1].CategoryName + ". " + keywordList[keywordList.Count - 1].CategoryDescription;
                    result += "\n\n" + "Is this what you were looking for (please click yes or no)?";
                    HoldOverItem = keywordList[keywordList.Count - 1];
                    HoldOverList = keywordList;
                }
                else
                {
                    HoldOverItem = new DatabaseItem();
                    result = "What do you want to learn more about? \n";
                    foreach (DatabaseItem item in keywordList)
                    {
                        result += item.Name + "\n";
                    }

                    //result = result.Substring(0, result.Length - 2);

                }
            }

            return result;
        }

       
        public string GetMotivation()
        {
            string result = "";
            using (SqlConnection conn = new SqlConnection(Connectionstring))
            {

                conn.Open();
                SqlCommand cmd = new SqlCommand(getMotivationQuoteSQL, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    result = Convert.ToString(reader["description"]);
                }
            }
            return result;
        }

        public string GetCalendarEvents()
        {
            string[] Scopes = { CalendarService.Scope.CalendarReadonly };
            string ApplicationName = "Google Calendar API .NET Quickstart";
            UserCredential credential;
            string result = "";
            using (var stream =
                new FileStream("credentials.json", FileMode.Open, FileAccess.Read))
            {
                // The file token.json stores the user's access and refresh tokens, and is created
                // automatically when the authorization flow completes for the first time.
                string credPath = "token.json";
                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    Scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;
            }

            // Create Google Calendar API service.
            var service = new CalendarService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });

            // Define parameters of request.
            EventsResource.ListRequest request = service.Events.List("techelevator.com_vfrsm13cdl6bman6kck7kdts00@group.calendar.google.com");
            request.TimeMin = DateTime.Now;
            request.TimeMax = DateTime.Now.AddDays(7);
            request.ShowDeleted = false;
            request.SingleEvents = true;
            request.OrderBy = EventsResource.ListRequest.OrderByEnum.StartTime;

            // List events.
            Events events = request.Execute();
            result = "Upcoming events: \n";
            if (events.Items != null && events.Items.Count > 0)
            {
                foreach (var eventItem in events.Items)
                {
                    string when = eventItem.Start.DateTime.ToString();
                    if (String.IsNullOrEmpty(when))
                    {
                        when = eventItem.Start.DateTime.ToString();
                    }
                    result += $"{eventItem.Summary} ({when})\n";
                }
            }
            else
            {
                result = "No upcoming events found.";
            }
            return result;
        }

        public Dictionary<string, string> GetKeywordList()
        {
            Dictionary<string, string> result = new Dictionary<string, string>();
            Dictionary<string, string> category = GetKeywordSQL(categoryKeywordListingSQL).ToDictionary((x) => x, (x) => "category");
            Dictionary<string, string> pathway = GetKeywordSQL(pathwayKeywordListingSQL).ToDictionary((x) => x, (x) => "pathway");
            Dictionary<string, string> curriculum = GetKeywordSQL(curriculumKeywordListingSQL).ToDictionary((x) => x, (x) => "curriculum");
            Dictionary<string, string> position = GetKeywordSQL(positionKeywordListingSQL).ToDictionary((x) => x, (x) => "positions");
            category.ToList().ForEach(x => result.Add(x.Key, x.Value));
            pathway.ToList().ForEach(x => result.Add(x.Key, x.Value));
            curriculum.ToList().ForEach(x => result.Add(x.Key, x.Value));
            position.ToList().ForEach(x => result.Add(x.Key, x.Value));
            return result;
        }

        public string GetAllJobListings()
        {
            string result = "";

            List<Jobs> jobs = new List<Jobs>();
            using (SqlConnection conn = new SqlConnection(Connectionstring))
            {

                conn.Open();
                SqlCommand cmd = new SqlCommand(getAllJobOpeningsSQL, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Jobs job = new Jobs();
                    job.SingleJobName = Convert.ToString(reader["name"]) + ":\n";
                    job.SingleJobWeb = Convert.ToString(reader["website"]) + "\n" + "\n";
                    if (job.SingleJobWeb.Contains("|"))
                    {
                        string[] webs = job.SingleJobWeb.Split("|");
                        for (int i = 0; i < webs.Length; i++)
                        {
                            webs[i] = "<a target = '_blank' href = " + webs[i] + ">" + webs[i] + "</a>";
                        }
                        job.SingleJobWeb = string.Join("\n", webs);
                    }
                    else
                    {
                        job.SingleJobWeb = "<a target = '_blank' href = " + job.SingleJobWeb + ">" + job.SingleJobWeb + "</a>";
                    }
                    jobs.Add(job);
                }

            }
            foreach (Jobs job in jobs)
            {
                result += job.SingleJobName + "\n";
                result += job.SingleJobWeb + "\n";
            }
            return result;
        }

        public List<string> GetKeywordSQL(string SQL)
        {
            List<string> result = new List<string>();
            using (SqlConnection conn = new SqlConnection(Connectionstring))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(SQL, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    string name = "";
                    name = Convert.ToString(reader["name"]);
                    result.Add(name);
                }
            }
            return result;
        }

        public DatabaseItem GetItemList(string keyword, string keywordCategory)
        {
            DatabaseItem result = new DatabaseItem();
            if (keywordCategory == "category")
            {
                try
                {
                    DatabaseItem resultCategory = GetItemSQL(getCategoryItemSQL, keyword, keywordCategory);

                    result = resultCategory;
                }


                catch (Exception e) { }
            }
            else if (keywordCategory == "pathway")
            {
                try
                {
                    DatabaseItem resultpathway = GetItemSQL(getPathwayItemSQL, keyword, keywordCategory);

                    result = resultpathway;

                }
                catch (Exception e) { }
            }
            else if (keywordCategory == "curriculum")
            {
                try
                {
                    DatabaseItem resultCurriculum = GetItemSQL(getCurriculumItemSQL, keyword, keywordCategory);

                    result = resultCurriculum;

                }
                catch (Exception e) { }
            }
            else if (keywordCategory == "positions")
            {
                try
                {
                    DatabaseItem resultPositions = GetItemSQL(getPostionItemSQL, keyword, keywordCategory);

                    result = resultPositions;

                }
                catch (Exception e) { }
            }
            return result;
        }

        public DatabaseItem GetItemSQL(string SQL, string keyword, string table)
        {
            DatabaseItem result = new DatabaseItem();
            using (SqlConnection conn = new SqlConnection(Connectionstring))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(SQL, conn);
                cmd.Parameters.AddWithValue("@input", keyword);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    DatabaseItem item = new DatabaseItem();
                    item.ID = Convert.ToInt32(reader[table + "id"]);
                    item.Name = Convert.ToString(reader[table + "name"]);
                    try
                    {
                        item.Description = Convert.ToString(reader[table + "description"]);
                        item.Website = Convert.ToString(reader[table + "website"]);
                        item.CategoryName = Convert.ToString(reader["categoryname"]);
                        item.CategoryDescription = Convert.ToString(reader["categorydescription"]);
                        item.Weight = Convert.ToInt32(reader[table + "weight"]);
                        result = item;
                        return result;
                    }
                    catch (Exception e)
                    {

                    }

                    result = item;

                }
            }

            return result;
        }

        public void UpdateWeight(DatabaseItem item)
        {
            using (SqlConnection conn = new SqlConnection(Connectionstring))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("UPDATE " + item.CategoryName + " SET weight=@updatedWeight WHERE name=@name;", conn);
                cmd.Parameters.AddWithValue("@name", item.Name);
                cmd.Parameters.AddWithValue("@updatedWeight", item.Weight);

                cmd.ExecuteNonQuery();

            }

        }


        public string RequestProcess(string request)
        {
            string actualRequest = request.Substring(9);
            actualRequest = actualRequest.Trim();
            if(actualRequest == "")
            {
                return "Please enter a valid request.";
            }
            bool doWeHaveItAlready = CheckRequest(actualRequest);
            string result = ResponseProcess(actualRequest);


            if (doWeHaveItAlready)
            {
                result = "This topic has already been requested and is under review.";
            }
            else if (result == "I'm sorry, I couldn't quite understand that. If you want some examples of what to ask for, try typing !help.")
            {
                AddRequest(actualRequest);
                result = "Thank you for your request. Our administrator will review your request and get back to you soon";
            }
            else
            {
                result = "We already have this topic in the database, here is the result: \n" + result;
            }
            return result;
        }

        public bool CheckRequest(string actualRequest)
        {
            string checkRecord = "SELECT * FROM requests;";

            List<string> records = new List<string>();
            bool result = false;
            using (SqlConnection conn = new SqlConnection(Connectionstring))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(checkRecord, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    string record = Convert.ToString(reader["name"]);
                    records.Add(record);
                }
            }
            foreach (string record in records)
            {
                if (record.ToLower() == actualRequest.ToLower())
                {
                    result = true;
                }
                else
                {
                    result = false;
                }
            }
            return result;
        }

        public void AddRequest(string actualRequest)
        {
            string addRecord = "INSERT INTO requests (name) VALUES (@actualRequest);";
            using (SqlConnection conn = new SqlConnection(Connectionstring))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(addRecord, conn);
                cmd.Parameters.AddWithValue("@actualRequest", actualRequest);
                cmd.ExecuteNonQuery();

            }
        }

    }
}
