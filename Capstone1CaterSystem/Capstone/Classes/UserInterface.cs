using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Classes
{
    public class UserInterface
    {
        // This class provides all user communications, but not much else.
        // All the "work" of the application should be done elsewhere
        // ALL instances of Console.ReadLine and Console.WriteLine should 
        // be in this class.

        private Catering catering = new Catering();

        public void RunInterface()
        {
            bool done = false;
            while (!done)
            {
                Console.WriteLine("(1) Display Catering Items");
                Console.WriteLine("(2) Order");
                Console.WriteLine("(3) Quit");
                string input = Console.ReadLine();
                if (input == "1")
                {
                    //Console.WriteLine("this is catering item");
                    //Console.ReadLine();
                    Console.WriteLine("Number  Name  Price  Type   InStock");
                    string[] inventoryArray = catering.Inventory();
                    foreach (string item in inventoryArray)
                    {
                        Console.WriteLine();
                        Console.WriteLine(item);
                    }

                }
                else if (input == "2")
                {
                    //Console.Clear();
                    SecondaryInterface();
                }
                else if (input == "3")
                {
                    done = true;

                }
            }

        }
        public void SecondaryInterface()
        {
            bool secondDone = false;

            while (!secondDone)
            {
                Console.WriteLine("(1) Add Money");
                Console.WriteLine("(2) Select Products");
                Console.WriteLine("(3) Complete Transaction");
                Console.WriteLine($"Current Account Balance: ${catering.AccountBalance.ToString("F2")}");
                string input = Console.ReadLine();
                if (input == "1")
                {
                    int moneyInput = 0;
                    do
                    {
                        //Console.WriteLine("this is add money");
                        //Console.ReadLine();
                        Console.WriteLine("Please insert your money: ");
                        try
                        {
                            moneyInput = int.Parse(Console.ReadLine());
                        }
                        catch
                        {
                            Console.WriteLine("Please add whole dollar amount");
                        }

                        string moneyInfoOutput = catering.AddMoney(moneyInput);

                        Console.WriteLine(moneyInfoOutput);

                    }
                    while (catering.AccountBalance > 5000);
                }
                else if (input == "2")
                {
                    //Console.WriteLine("this is select money");
                    //Console.ReadLine();
                    string productCodeInput;
                    Console.WriteLine("Please enter product code: ");
                    productCodeInput = Console.ReadLine();
                    Console.WriteLine("How many do you need: ");
                    int amountInput = int.Parse(Console.ReadLine());
                    string info = catering.SelectItem(productCodeInput, amountInput);
                    Console.WriteLine(info);

                }
                else if (input == "3")
                {
                    if (catering.AccountBalance < 0)
                    {
                        Console.WriteLine("Please Add Money to the Account Balance");
                    }
                    else
                    {
                        secondDone = true;
                        Console.WriteLine("Amount Type Name Price Total");
                        string[] result = catering.PrintReport();
                        foreach (string item in result)
                        {
                            Console.WriteLine(item);
                        }
                        Console.WriteLine("Changes Dispended");
                        Dictionary<string, int> change = catering.GetChange();
                        foreach (KeyValuePair<string, int> item in change)
                        {
                            if (item.Value > 0)
                            {
                                Console.WriteLine(item.Value + " " + item.Key);
                            }

                        }
                    }

                }
            }
        }
    }
}
