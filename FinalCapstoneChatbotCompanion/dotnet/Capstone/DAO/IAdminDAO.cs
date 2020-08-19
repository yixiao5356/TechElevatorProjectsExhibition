using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.DAO
{
    public interface IAdminDAO
    {
        List<DatabaseItem> AllItems();
        bool AddPathwayToDatabase(DatabaseItem item);
        bool AddCurriculumToDatabase(DatabaseItem item);
        bool AddCompanyToDatabase(DatabaseItem item);
        bool UpdateCompanyForDatabase(DatabaseItem item);
        bool AddPositionToDatabase(DatabaseItem item);
        bool AddQuoteToDatabase(DatabaseItem item);
        bool UpdateItemInDatabase(DatabaseItem item);
        bool DeleteItemInDatabase(string tableName, int id);
        List<DatabaseItem> AllRequests();

    }
}
