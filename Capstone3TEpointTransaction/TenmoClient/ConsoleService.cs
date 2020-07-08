using System;
using System.Collections.Generic;
using TenmoClient.Data;

namespace TenmoClient
{
    public class ConsoleService
    {
        private static readonly AuthService authService = new AuthService();
        private string UserName { get; set; }

        public void Run()
        {

            while (true)
            {
                Console.WriteLine("Welcome to TEnmo!");
                Console.WriteLine("1: Login");
                Console.WriteLine("2: Register");
                Console.WriteLine("3: Exit");
                Console.Write("Please choose an option: ");

                int loginRegister = -1;

                try
                {
                    if (!int.TryParse(Console.ReadLine(), out loginRegister))
                    {
                        Console.WriteLine("Invalid input. Please enter only a number.");
                    }

                    else if (loginRegister == 1)
                    {
                        LoginUser loginUser = PromptForLogin();
                        API_User user = authService.Login(loginUser);
                        if (user != null)
                        {
                            UserService.SetLogin(user);
                            MenuSelection();
                        }
                    }

                    else if (loginRegister == 2)
                    {
                        LoginUser registerUser = PromptForLogin();
                        bool isRegistered = authService.Register(registerUser);
                        if (isRegistered)
                        {
                            Console.WriteLine("");
                            Console.WriteLine("Registration successful. You can now log in.");
                        }
                    }

                    else if (loginRegister == 3)
                    {
                        Console.WriteLine("Goodbye!");
                        Environment.Exit(0);
                    }

                    else
                    {
                        Console.WriteLine("Invalid selection.");
                    }
                }

                catch (Exception ex)
                {
                    Console.WriteLine("Error - " + ex.Message);
                }
            }
        }

        private void MenuSelection()
        {
            int menuSelection = -1;
            while (menuSelection != 0)
            {
                Console.WriteLine("");
                Console.WriteLine("Welcome to TEnmo! Please make a selection: ");
                Console.WriteLine("1: View your current balance");
                Console.WriteLine("2: View your past transfers"); //view details through here
                Console.WriteLine("3: View your pending requests"); //ability to approve/reject through here(optional)
                Console.WriteLine("4: Send TE bucks");
                Console.WriteLine("5: Request TE bucks");//optional
                Console.WriteLine("6: View list of users");
                Console.WriteLine("7: Log in as different user");
                Console.WriteLine("0: Exit");
                Console.WriteLine("---------");
                Console.Write("Please choose an option: ");

                try
                {
                    if (!int.TryParse(Console.ReadLine(), out menuSelection))
                    {
                        Console.WriteLine("Invalid input. Please enter only a number.");
                    }
                    else if (menuSelection == 1)
                    {
                        AccountBalanceService accountBalance = new AccountBalanceService();
                        decimal balance = accountBalance.AccountBalance();
                        Console.WriteLine("Your current account balance is:" + balance.ToString("c"));
                    }
                    else if (menuSelection == 2)
                    {
                        int transferID = -1;
                        while (transferID != 0)
                        {

                            TransferService transfer = new TransferService();
                            List<API_Transfer> transfers = transfer.TransferList();
                            Console.WriteLine("--------");
                            Console.WriteLine("Past Transfers");
                            Console.WriteLine("ID".PadRight(10) + "From/To".PadRight(15) + "Amount".PadRight(10) + "status");
                            Console.WriteLine("----------");
                            foreach (API_Transfer item in transfers)
                            {
                                string fromTo = "";
                                if (item.Status == "Approved" || item.Status == "Rejected")
                                {

                                    if (UserName.ToLower() == item.FromName.ToLower())
                                    {
                                        fromTo = "To: " + item.ToName;
                                    }
                                    else
                                    {
                                        fromTo = "From: " + item.FromName;
                                    }
                                    Console.WriteLine(item.Id.ToString().PadRight(10) + fromTo.PadRight(15) + item.Amount.ToString("C").PadRight(10) + item.Status);
                                }
                                
                            }
                            Console.WriteLine("-------");
                            bool typeIsGood = false;
                            bool numISGood = false;
                            while (!typeIsGood || !numISGood)
                            {
                                Console.WriteLine("Please enter transfer ID to view details (0 to cancel):");
                                typeIsGood = int.TryParse(Console.ReadLine(), out transferID);
                                foreach (API_Transfer item in transfers)
                                {
                                    if (item.Id == transferID || transferID == 0)
                                    {
                                        numISGood = true;
                                        break;
                                    }
                                }
                            }
                            //if (transferID != 0)
                            //{
                            //    typeIsGood = true;
                            //    numISGood = true;
                            //}
                            //Todo
                            if (transferID != 0)
                            {
                                API_Transfer result = new API_Transfer();
                                foreach (API_Transfer item in transfers)
                                {
                                    if (item.Id == transferID)
                                    {
                                        result = item;
                                        break;
                                    }
                                }
                                Console.WriteLine("--------");
                                Console.WriteLine("Transfer Details");
                                Console.WriteLine("---------");
                                Console.WriteLine("Id: " + result.Id);
                                Console.WriteLine("From: " + result.FromName);
                                Console.WriteLine("To: " + result.ToName);
                                Console.WriteLine("Type: " + result.Type);
                                Console.WriteLine("Status: " + result.Status);
                                Console.WriteLine("Amount: " + result.Amount.ToString("C"));
                            }

                        }
                    }
                    else if (menuSelection == 3)
                    {
                        List<API_Transfer> From = new List<API_Transfer>();
                        int transferID = -1;
                        while (transferID != 0)
                        {

                            TransferService transfer = new TransferService();
                            List<API_Transfer> transfers = transfer.TransferList();
                            API_Transfer chosenTransfer = new API_Transfer();
                            Console.WriteLine("--------");
                            Console.WriteLine("Pending Transfers");
                            Console.WriteLine("ID".PadRight(10) + "From/To".PadRight(15) + "Amount");
                            Console.WriteLine("----------");
                            foreach (API_Transfer item in transfers)
                            {
                                string fromTo = "";
                                if (item.Type == "Request" && item.Status == "Pending")
                                {
                                    if (UserName.ToLower() == item.FromName.ToLower())
                                    {
                                        fromTo = "To: " + item.ToName;
                                        
                                    }
                                    else
                                    {
                                        fromTo = "From: " + item.FromName;
                                        From.Add(item);
                                    }
                                    Console.WriteLine(item.Id.ToString().PadRight(10) + fromTo.PadRight(15) + item.Amount.ToString("C"));
                                }
                                
                            }
                            Console.WriteLine("-------");
                            bool typeIsGood = false;
                            bool numISGood = false;
                            while (!typeIsGood || !numISGood)
                            {
                                Console.WriteLine("Please enter transfer ID to approve/reject (0 to cancel):");
                                typeIsGood = int.TryParse(Console.ReadLine(), out transferID);
                                foreach (API_Transfer item in From)
                                {
                                    if (item.Id == transferID || transferID == 0)
                                    {
                                        numISGood = true;
                                        chosenTransfer = item;
                                        break;
                                    }
                                }
                                if (transferID == 0)
                                {
                                    numISGood = true;
                                }
                            }

                            if (transferID != 0)
                            {
                                Console.WriteLine("1: Approve");
                                Console.WriteLine("2: Reject");
                                Console.WriteLine("0: Don't approve or reject");
                                Console.WriteLine("--------");
                                Console.WriteLine("Please choose an option:");
                                bool isGood = true;
                                do
                                {
                                    string input = Console.ReadLine();
                                    switch (input)
                                    {
                                        case "1":
                                            AccountBalanceService accountBalance = new AccountBalanceService();
                                            decimal balance = accountBalance.AccountBalance();
                                            if (chosenTransfer.Amount <= balance)
                                            {
                                                string approveResult = transfer.Approve(chosenTransfer);
                                                Console.WriteLine(approveResult);
                                                isGood = true;
                                            }
                                            else
                                            {
                                                Console.WriteLine("Insufficient ammount to approve");
                                                isGood = false;
                                            }
                                            break;
                                        case "2":
                                            string rejectResult = transfer.Reject(chosenTransfer);
                                            Console.WriteLine(rejectResult);
                                            isGood = true;
                                            break;
                                        case "0":
                                            isGood = true;
                                            break;
                                        default:
                                            isGood = false;
                                            break;
                                    }
                                }
                                while (isGood == false);
                            }
                        }
                    }
                    else if (menuSelection == 4)
                    {
                        int transferId = -1;
                        TransferService transfer = new TransferService();
                        List<API_User> users = ListUser(transfer);

                        while (transferId != 0)
                        {
                            bool intIsGood = false;
                            bool numIsGood = false;

                            while (!intIsGood || !numIsGood)
                            {
                                Console.WriteLine("Enter ID of user you are sending to (0 to cancel):");
                                intIsGood = int.TryParse(Console.ReadLine(), out transferId);
                                foreach (API_User item in users)
                                {
                                    if (transferId == item.UserId || transferId == 0)
                                    {
                                        numIsGood = true;
                                    }
                                }

                            }
                            if (transferId != 0)
                            {

                                decimal transferAmount = 0;
                                bool amountIsGood = false;
                                bool amountNotOver = false;
                                AccountBalanceService accountBalance = new AccountBalanceService();
                                decimal balance = accountBalance.AccountBalance();
                                while (!amountIsGood || !amountNotOver)
                                {
                                    Console.WriteLine("Enter amount:");
                                    amountIsGood = decimal.TryParse(Console.ReadLine(), out transferAmount);
                                    if (balance >= transferAmount)
                                    {
                                        amountNotOver = true;
                                    }
                                }
                                string result = transfer.Transfering(transferId, transferAmount);
                                Console.WriteLine(result);
                                transferId = 0;
                            }
                        }

                    }
                    else if (menuSelection == 5)
                    {
                        int transferId = -1;
                        TransferService transfer = new TransferService();
                        List<API_User> users = ListUser(transfer);

                        while (transferId != 0)
                        {
                            bool intIsGood = false;
                            bool numIsGood = false;

                            while (!intIsGood || !numIsGood)
                            {
                                Console.WriteLine("Enter ID of user you are sending to (0 to cancel):");
                                intIsGood = int.TryParse(Console.ReadLine(), out transferId);
                                foreach (API_User item in users)
                                {
                                    if (transferId == item.UserId || transferId == 0)
                                    {
                                        numIsGood = true;
                                    }
                                }

                            }
                            if (transferId != 0)
                            {

                                decimal transferAmount = 0;
                                bool amountIsGood = false;
                                AccountBalanceService accountBalance = new AccountBalanceService();
                                decimal balance = accountBalance.AccountBalance();
                                while (!amountIsGood)
                                {
                                    Console.WriteLine("Enter amount:");
                                    amountIsGood = decimal.TryParse(Console.ReadLine(), out transferAmount);
                                }
                                string result = transfer.Request(transferId, transferAmount);
                                Console.WriteLine(result);
                                transferId = 0;
                            }
                        }
                    }
                    else if (menuSelection == 6)
                    {
                        TransferService transfer = new TransferService();
                        List<API_User> users = transfer.UserList();
                        Console.WriteLine("--------------");
                        Console.WriteLine("Users");
                        Console.WriteLine("ID".PadRight(10) + "Name");
                        Console.WriteLine("---------------");
                        foreach (API_User item in users)
                        {
                            Console.WriteLine(item.UserId.ToString().PadRight(10) + item.Username);
                        }
                        Console.WriteLine("-----------");
                    }
                    else if (menuSelection == 7)
                    {
                        Console.WriteLine("");
                        UserService.SetLogin(new API_User()); //wipe out previous login info
                        return; //return to register/login menu
                    }
                    else if (menuSelection == 0)
                    {
                        Console.WriteLine("Goodbye!");
                        Environment.Exit(0);
                    }

                    else
                    {
                        Console.WriteLine("Please try again");
                        Console.WriteLine();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error - " + ex.Message);
                    Console.WriteLine();
                }
            }
        }

        public int PromptForTransferID(string action)
        {
            Console.WriteLine("");
            Console.Write("Please enter transfer ID to " + action + " (0 to cancel): ");
            if (!int.TryParse(Console.ReadLine(), out int auctionId))
            {
                Console.WriteLine("Invalid input. Only input a number.");
                return 0;
            }
            else
            {
                return auctionId;
            }
        }

        public LoginUser PromptForLogin()
        {
            Console.Write("Username: ");
            string username = Console.ReadLine();
            UserName = username;
            string password = GetPasswordFromConsole("Password: ");

            LoginUser loginUser = new LoginUser
            {
                Username = username,
                Password = password
            };
            return loginUser;
        }

        private string GetPasswordFromConsole(string displayMessage)
        {
            string pass = "";
            Console.Write(displayMessage);
            ConsoleKeyInfo key;

            do
            {
                key = Console.ReadKey(true);

                // Backspace Should Not Work
                if (!char.IsControl(key.KeyChar))
                {
                    pass += key.KeyChar;
                    Console.Write("*");
                }
                else
                {
                    if (key.Key == ConsoleKey.Backspace && pass.Length > 0)
                    {
                        pass = pass.Remove(pass.Length - 1);
                        Console.Write("\b \b");
                    }
                }
            }
            // Stops Receving Keys Once Enter is Pressed
            while (key.Key != ConsoleKey.Enter);
            Console.WriteLine("");
            return pass;
        }
        List<API_User> ListUser(TransferService transfer)
        {

            List<API_User> users = transfer.UserList();
            Console.WriteLine("--------------");
            Console.WriteLine("Users");
            Console.WriteLine("ID".PadRight(10) + "Name");
            Console.WriteLine("---------------");
            foreach (API_User item in users)
            {
                Console.WriteLine(item.UserId.ToString().PadRight(10) + item.Username);
            }
            Console.WriteLine("-----------");
            return users;
        }
    }
}
