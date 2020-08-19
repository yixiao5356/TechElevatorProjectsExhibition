using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.DAO
{
    public interface ICompanyDAO
    {
        List<DatabaseItem> CompanyInformation();
        Dictionary<int, string> GetCompanyKeyWordList();
        List<Company> GetCompanyNames(string SQL);

    }
}
