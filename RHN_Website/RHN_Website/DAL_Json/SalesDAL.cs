using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RHN_Website.Helpers;
using RHN_Website.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace RHN_Website.DAL_Json
{
    public static class SalesDAL
    {
        public static List<Sale> GetSales()
        {
            var json = File.ReadAllText(Constants.JsonDB);
            var Sales = new List<Sale>();
            try
            {
                var jObject = JObject.Parse(json);

                if (jObject != null)
                {
                    JArray SalesArrary = (JArray)jObject[Constants.salesCollection];
                    if (SalesArrary != null)
                    {
                        foreach (var item in SalesArrary)
                        {
                            Sales.Add(new Sale()
                            {
                                Id = (Guid)(item["Id"]),
                                ProductId = (Guid)(item["ProductId"]),
                                Quantity = (int)item["Quantity"],
                                Date = (DateTime)item["Date"],
                                TotalSellingPrice = (double)item["TotalSellingPrice"]
                            });
                        }
                    }
                }

                return Sales;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public static void AddSale(Sale newSale)
        {
            var javaScriptSerializer = new JavaScriptSerializer();
            var newSaleJSON = javaScriptSerializer.Serialize(newSale);
            try
            {
                var json = File.ReadAllText(Constants.JsonDB);
                var jsonObj = JObject.Parse(json);
                var SalesList = jsonObj.GetValue(Constants.salesCollection) as JArray;
                var newSalesList = JObject.Parse(newSaleJSON);
                SalesList.Add(newSalesList);

                jsonObj[Constants.salesCollection] = SalesList;
                string newJsonResult = JsonConvert.SerializeObject(jsonObj,
                                       Formatting.Indented);
                File.WriteAllText(Constants.JsonDB, newJsonResult);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public static void UpdateSale(Sale Sale)
        {
            string json = File.ReadAllText(Constants.JsonDB);

            try
            {
                var jObject = JObject.Parse(json);
                JArray SalesList = (JArray)jObject[Constants.salesCollection];

                if (Sale.Id != null)
                {
                    foreach (var dbSale in SalesList.Where(obj => obj["Id"].Value<string>() == Sale.Id.ToString()))
                    {
                        dbSale["ProductId"] = Sale.ProductId;
                        dbSale["Quantity"] = Sale.Quantity;
                        dbSale["Salename"] = Sale.Date;
                        dbSale["Password"] = Sale.TotalSellingPrice;
                    }

                    jObject[Constants.salesCollection] = SalesList;
                    string output = JsonConvert.SerializeObject(jObject, Formatting.Indented);
                    File.WriteAllText(Constants.JsonDB, output);
                }
                else
                {
                    //Console.Write("Invalid Company ID, Try Again!");
                    //UpdateSale();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public static void DeleteSale(Sale Sale)
        {
            var json = File.ReadAllText(Constants.JsonDB);
            try
            {
                var jObject = JObject.Parse(json);
                JArray SalesList = (JArray)jObject[Constants.salesCollection];

                if (Sale.Id != null)
                {
                    var SaleToDeleted = SalesList.FirstOrDefault(obj => obj["Id"].Value<string>() == Sale.Id.ToString());

                    SalesList.Remove(SaleToDeleted);

                    string output = JsonConvert.SerializeObject(jObject, Formatting.Indented);
                    File.WriteAllText(Constants.JsonDB, output);
                }
                else
                {
                    //Console.Write("Invalid Company ID, Try Again!");
                    //UpdateCompany();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}