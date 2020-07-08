using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace TenmoClient
{
    public class AccountBalanceService:AuthService
    {
        public decimal AccountBalance()
        {
            string URL = API_BASE_URL + "accountbalance";
            RestRequest request = new RestRequest(URL);
            IRestResponse<decimal> response = client.Get<decimal>(request);
            if (response.ResponseStatus != ResponseStatus.Completed)
            {
                throw new Exception("An error occurred communicating with the server.");
            }
            else if (!response.IsSuccessful)
            {
                
                    throw new Exception("An error message was received");

                
            }
            else
            {
                return response.Data;
            }
            
        }
    }
}
