using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using TenmoClient.Data;

namespace TenmoClient
{
    public class TransferService : AuthService
    {
        string endPoint = "transfer";
        public string Reject (API_Transfer chosenTransfer)
        {
            RestRequest request = new RestRequest(API_BASE_URL + endPoint + "/reject");
            request.AddJsonBody(chosenTransfer);
            IRestResponse response = client.Put(request);
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
                string result = response.Content;
                return result;
            }
        }

        public string Approve(API_Transfer chosenTransfer)
        {
            RestRequest request = new RestRequest(API_BASE_URL + endPoint + "/approve");
            request.AddJsonBody(chosenTransfer);
            IRestResponse response = client.Put(request);
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
                string result = response.Content;
                return result;
            }
        }

        public string Request(int toUserId, decimal amount)
        {
            API_TransferRequest transfer = new API_TransferRequest();
            transfer.ToUserID = toUserId;
            transfer.Amount = amount;
            RestRequest request = new RestRequest(API_BASE_URL + endPoint + "/request");
            request.AddJsonBody(transfer);
            IRestResponse response = client.Post(request);
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
                string result = response.Content;
                return result;
            }
        }

        public List<API_User> UserList()
        {
            string URL = API_BASE_URL + endPoint;
            RestRequest request = new RestRequest(URL);
            IRestResponse<List<API_User>> response = client.Get<List<API_User>>(request);
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

        public string Transfering (int toUserId, decimal amount)
        {
            API_TransferRequest transfer = new API_TransferRequest();
            transfer.ToUserID = toUserId;
            transfer.Amount = amount;
            RestRequest request = new RestRequest(API_BASE_URL + endPoint);
            request.AddJsonBody(transfer);
            IRestResponse response = client.Post(request);
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
                string result = response.Content;
                return result;
            }
        }

        public List<API_Transfer> TransferList()
        {
            string URL = API_BASE_URL + endPoint + "/record";
            RestRequest request = new RestRequest(URL);
            IRestResponse<List<API_Transfer>> response = client.Get<List<API_Transfer>>(request);
            if (response.ResponseStatus != ResponseStatus.Completed)
            {
                throw new Exception("An error occurred communicating with the server.");
            }
            else if (!response.IsSuccessful)
            {
                
                throw new Exception("An error message was received" + response.StatusCode);


            }
            else
            {
                return response.Data;
            }
        }
    }
}
