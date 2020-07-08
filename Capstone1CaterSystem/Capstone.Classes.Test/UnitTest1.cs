using Microsoft.VisualStudio.TestTools.UnitTesting;
using Capstone;
using System.Collections.Generic;

namespace Capstone.Classes.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void CateringDictionary()
        {
            Catering test = new Catering();
            Dictionary<string, CateringItem> result = test.itemsDictionary;
            //CateringItem expect = new CateringItem();
            //expect.ItemNumber = "E1";
            //expect.ItemName = "Baked Chicken";
            //expect.ItemPrice = 8.85;
            //expect.ItemType = E;
            Assert.AreEqual(result["B3"].ItemName, "Beer");
        }
        [TestMethod]
        public void AddMoneyTest()
        {
            Catering test = new Catering();
            double expect = 100;
            test.AddMoney(100);
            Assert.AreEqual(expect, test.AccountBalance);
        }
        [TestMethod]
        public void MoneyExceedMaxium()
        {
            Catering test = new Catering();
            double expect = 0;
            test.AddMoney(5001);
            Assert.AreEqual(expect, test.AccountBalance);
        }
        [DataTestMethod]
        [DataRow("Code does not exist", "E10", 30)]
        [DataRow("Insufficient stock", "D1", 60)]
        [DataRow("Hey don't steal money!", "D2", -10)]
        public void EmptyShoppingCartTest(string expect, string itemNumber, int amount)
        {
            Catering test = new Catering();
            string result = test.SelectItem(itemNumber, amount);
            Assert.AreEqual(expect, result);

        }
        [DataTestMethod]
        [DataRow("Sold out", "E1", 10)]
        [DataRow("item outcart", "E1", -10)]
        public void SoldOutShoppingCartTest(string expect, string itemNumber, int amount)
        {
            Catering test = new Catering();
            test.SelectItem("E1", 50);
            string result = test.SelectItem(itemNumber, amount);
            Assert.AreEqual(expect, result);
        }
        [TestMethod]
        public void ChangeTest()
        {
            Catering test = new Catering();
            test.AccountBalance = 36.90M;
            Dictionary<string, int> result = test.GetChange();
            Dictionary<string, int> expect = new Dictionary<string, int> { };
            expect["twenties dollar bill"] = 1;
            expect["ten dollar bill"] = 1;
            expect["five dollar bill"] = 1;
            expect["one dollar bill"] = 1;
            expect["Quarter"] = 3;
            expect["Dime"] = 1;
            expect["Nickel"] = 1;
            CollectionAssert.AreEquivalent(expect, result);
        }
       
    }
}
