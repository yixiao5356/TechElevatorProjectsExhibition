using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.DAO
{
    public interface IResponseProcessingDAO
    {
        string ResponseProcess(string response);
        string RequestProcess(string request);
    }
}
