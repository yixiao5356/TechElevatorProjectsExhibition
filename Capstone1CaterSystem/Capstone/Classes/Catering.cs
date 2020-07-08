using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Classes
{
    public class Catering
    {
        // This class should contain all the "work" for catering
        public Catering()
        {
            //Calling FileAccess class
            FileAccess access = new FileAccess();
            Access = access;
            items = access.Origin();
            //Accessing Method from FileAccess Class
            foreach (CateringItem item in items)
            {
                itemsDictionary[item.ItemNumber] = item;
            }
        }

        private FileAccess Access { get; }

        private List<CateringItem> items = new List<CateringItem>();

        public Dictionary<string, CateringItem> itemsDictionary = new Dictionary<string, CateringItem> { };

        public decimal AccountBalance { get; set; } = 0;

        public decimal AccountBalanceHolder { get; set; } = 0;

        public Dictionary<CateringItem, int> ShoppingCart { get; set; } = new Dictionary<CateringItem, int> { };




        public string[] Inventory()
        {
            //For each CateringItem in the Array. This is how it is assigned. 
            string[] result = new string[items.Count];
            for (int i = 0; i < items.Count; i++)
            {
                List<string> inter = new List<string> { };
                inter.Add(items[i].ItemNumber);
                inter.Add(items[i].ItemName);
                inter.Add("$" + items[i].ItemPrice.ToString("F2"));
                inter.Add(items[i].ItemType);
                inter.Add(items[i].ItemAmount.ToString());
                string itemString = string.Join(" ", inter.ToArray());
                result[i] = itemString;
            }
            return result;
        }

        public string AddMoney(int dollar)
        {

            try
            {
                AccountBalance += dollar;
                AccountBalanceHolder += dollar;
                if (AccountBalance > 5000)
                {
                    AccountBalance -= dollar;
                    throw new Exception("exceed Maximun");
                }
                else
                {
                    Access.Audit(DateTime.Now.ToString(), "ADD MONEY", dollar, AccountBalance);
                    return "Money added";
                }

            }
            catch (Exception e)
            {
                return e.Message;
            }

        }

        public string SelectItem(string itemNumber, int amount)
        {
            CateringItem selecteditem;
            bool contain = itemsDictionary.ContainsKey(itemNumber);
            if (contain)
            {
                selecteditem = itemsDictionary[itemNumber];

                if (selecteditem.ItemAmount != 0)
                {
                    if (selecteditem.ItemAmount - amount >= 0)
                    {
                        if (AccountBalance - (decimal)selecteditem.ItemPrice * amount <= AccountBalanceHolder)
                        {
                            selecteditem.ItemAmount -= amount;
                            AccountBalance -= (decimal)selecteditem.ItemPrice * amount;
                            if (!ShoppingCart.ContainsKey(selecteditem))
                            {
                                if (amount < 0)
                                {
                                    return "what are you talking about";
                                }
                                else
                                {

                                    ShoppingCart[selecteditem] = amount;
                                    selecteditem.TimeChange = DateTime.Now.ToString();
                                    if (amount < 0)
                                    {

                                        return "item outcart";
                                    }
                                    else
                                    {
                                        return "item incart";
                                    }
                                }
                            }
                            else
                            {
                                ShoppingCart[selecteditem] += amount;
                                selecteditem.TimeChange = DateTime.Now.ToString();
                                if (amount < 0)
                                {
                                    return "item outcart";
                                }
                                else
                                {
                                    return "item incart";
                                }
                            }
                        }
                        else
                        {
                            return "don't steal money";
                        }
                    }
                    else
                    {
                        return "not enough item";
                    }
                }
                else
                {
                    return "item sold out";
                }



            }
            else
            {

                return "Code does not exist";
            }



        }

        public Dictionary<string, int> GetChange()
        {
            decimal getChange = AccountBalance;
            Dictionary<string, int> result = new Dictionary<string, int> { };
            result["twenties dollar bill"] = 0;
            result["ten dollar bill"] = 0;
            result["five dollar bill"] = 0;
            result["one dollar bill"] = 0;
            result["Quarter"] = 0;
            result["Dime"] = 0;
            result["Nickel"] = 0;
            while (AccountBalance >= 20)
            {
                AccountBalance -= 20;
                result["twenties dollar bill"] += 1;
            }
            while (AccountBalance >= 10)
            {
                AccountBalance -= 10;
                result["ten dollar bill"] += 1;
            }
            while (AccountBalance >= 5)
            {
                AccountBalance -= 5;
                result["five dollar bill"] += 1;
            }
            while (AccountBalance >= 1)
            {
                AccountBalance -= 1;
                result["one dollar bill"] += 1;
            }
            while (AccountBalance >= 0.25M)
            {
                AccountBalance -= 0.25M;
                result["Quarter"] += 1;
            }
            while (AccountBalance >= 0.1M)
            {
                AccountBalance -= 0.1M;
                result["Dime"] += 1;
            }
            while (AccountBalance >= 0.05M)
            {
                AccountBalance -= 0.05M;
                result["Nickel"] += 1;
            }
            if (getChange != 0)
            {
                Access.Audit(DateTime.Now.ToString(), "GIVE CHANGE", getChange, AccountBalance);
            }

            return result;
        }

        public string[] PrintReport()
        {
            List<string> result = new List<string> { };
            decimal totalSpend = 0;
            foreach (KeyValuePair<CateringItem, int> item in ShoppingCart)
            {
                string log = "";
                log += item.Value.ToString() + " ";
                log += item.Key.ItemName.ToString() + " ";
                log += item.Key.ItemNumber.ToString();


                string itemString = item.Value.ToString() + " ";

                switch (item.Key.ItemType)
                {
                    case "B":
                        itemString += "Beverage ";
                        break;
                    case "A":
                        itemString += "Appetizer ";
                        break;
                    case "E":
                        itemString += "Entree ";
                        break;
                    case "D":
                        itemString += "Dessert ";
                        break;
                }
                itemString += item.Key.ItemName + " ";
                itemString += "$" + item.Key.ItemPrice.ToString("F2") + " ";
                decimal itemTotal = (decimal)(item.Key.ItemPrice * item.Value);
                Access.Audit(item.Key.TimeChange, log, itemTotal, (AccountBalance - itemTotal));


                totalSpend += itemTotal;
                itemString += "$" + itemTotal.ToString("F2");
                result.Add(itemString);
            }
            Access.SalesReport(ShoppingCart);
            string totalString = "Total: $" + totalSpend.ToString("F2");
            result.Add(totalString);
            return result.ToArray();
        }

    }
}
