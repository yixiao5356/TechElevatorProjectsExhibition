using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Capstone.Classes
{
    public class FileAccess
    {
        // This class should contain any and all details of access to files
        private string filePath = Environment.CurrentDirectory + @"\cateringsystem.csv";
        private string auditPath = Environment.CurrentDirectory + @"\Log.csv";
        private string salesReportPath = Environment.CurrentDirectory + @"\SalesReport.rpt";
        //We just pulled from our File
        public List<string> originList = new List<string> { };
        public List<CateringItem> Origin()
        {
            //Method to be pulled in Catering Class
            List<CateringItem> result = new List<CateringItem> { };
            using (StreamReader sr = new StreamReader(filePath))
            {

                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();
                    originList.Add(line);
                    string[] itemInfo = line.Split('|');
                    CateringItem item = new CateringItem();
                    item.ItemNumber = itemInfo[0];
                    item.ItemName = itemInfo[1];
                    item.ItemPrice = double.Parse(itemInfo[2]);
                    item.ItemType = itemInfo[3];
                    result.Add(item);
                    //Breaking down the information in to Objects to pass to Catering Class
                }

            }
            return result;
        }

        public void Audit(string time, string did, decimal amountChanged, decimal accountBalance)
        {
            using (StreamWriter sw = new StreamWriter(auditPath, true))
            {
                sw.WriteLine(time + " " + did + " $" + amountChanged.ToString("F2") + " $" + accountBalance.ToString("F2"));
            }
            //Writing Log after Transaction
        }
        public void SalesReport(Dictionary<CateringItem, int> shopingCart)
        {
            if (!File.Exists(salesReportPath))
            {
                using (File.Create(salesReportPath))
                {

                }
            }

            List<string> resultArray = new List<string> { };
            using (StreamReader sr = new StreamReader(salesReportPath))
            {
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();
                    string[] reportLineArray = line.Split('|');
                    List<CateringItem> RemoveItems = new List<CateringItem> { };
                    foreach (KeyValuePair<CateringItem, int> item in shopingCart)
                    {

                        if (reportLineArray[0] == item.Key.ItemName)
                        {
                            int newTotalAmount = int.Parse(reportLineArray[1]) + item.Value;
                            reportLineArray[2] = reportLineArray[2].Replace("$", "");
                            decimal newSalesTotal = decimal.Parse(reportLineArray[2]) + (decimal)(item.Value * item.Key.ItemPrice);
                            string[] newLine = new string[3];
                            newLine[0] = reportLineArray[0];
                            newLine[1] = newTotalAmount.ToString();
                            newLine[2] = "$" + newSalesTotal.ToString("F2");
                            string result = string.Join("|", newLine);
                            resultArray.Add(result);
                            CateringItem toRemove = item.Key;
                            RemoveItems.Add(toRemove);
                            line = null;
                        }
                    }
                    foreach(CateringItem item in RemoveItems)
                    {
                        shopingCart.Remove(item);
                    }
                    resultArray.Add(line);
                }
                foreach (KeyValuePair<CateringItem, int> item in shopingCart)
                {
                    string[] newLine = new string[3];
                    newLine[0] = item.Key.ItemName;
                    newLine[1] = item.Value.ToString();
                    newLine[2] = "$" + (item.Key.ItemPrice * item.Value).ToString("F2");
                    string result = string.Join("|", newLine);
                    resultArray.Add(result);
                }
            }
            using (StreamWriter sw = new StreamWriter(salesReportPath, false))
            {
                foreach (string item in resultArray)
                {
                    if(item != null)
                    {
                    sw.WriteLine(item);
                    }
                }
            }

        }
    }
}
