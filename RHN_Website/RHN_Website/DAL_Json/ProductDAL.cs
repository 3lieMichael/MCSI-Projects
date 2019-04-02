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
    public static class ProductDAL
    {
        public static List<Product> GetProducts()
        {
            var json = File.ReadAllText(Constants.JsonDB);
            var Products = new List<Product>();
            try
            {
                var jObject = JObject.Parse(json);

                if (jObject != null)
                {
                    JArray ProductsArrary = (JArray)jObject[Constants.productsCollection];
                    if (ProductsArrary != null)
                    {
                        foreach (var item in ProductsArrary)
                        {
                            Products.Add(new Product()
                            {
                                Id = (Guid)(item["Id"]),
                                Name = item["Name"].ToString(),
                                Description = item["Description"].ToString(),
                                SellingPrice = Convert.ToDouble(item["SellingPrice"]),
                                Image = item["Image"].ToString()
                            });
                        }
                    }
                }

                return Products;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public static void AddProduct(Product newProduct)
        {
            var javaScriptSerializer = new JavaScriptSerializer();
            var newProductJSON = javaScriptSerializer.Serialize(newProduct);
            try
            {
                var json = File.ReadAllText(Constants.JsonDB);
                var jsonObj = JObject.Parse(json);
                var ProductsList = jsonObj.GetValue(Constants.productsCollection) as JArray;
                var newProductsList = JObject.Parse(newProductJSON);
                ProductsList.Add(newProductsList);

                jsonObj[Constants.productsCollection] = ProductsList;
                string newJsonResult = JsonConvert.SerializeObject(jsonObj,
                                       Formatting.Indented);
                File.WriteAllText(Constants.JsonDB, newJsonResult);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public static void UpdateProduct(Product Product)
        {
            string json = File.ReadAllText(Constants.JsonDB);

            try
            {
                var jObject = JObject.Parse(json);
                JArray ProductsList = (JArray)jObject[Constants.productsCollection];

                if (Product.Id != null)
                {
                    foreach (var dbProduct in ProductsList.Where(obj => obj["Id"].Value<string>() == Product.Id.ToString()))
                    {
                        dbProduct["Name"] = !string.IsNullOrEmpty(Product.Name) ? Product.Name : "";
                        dbProduct["Description"] = !string.IsNullOrEmpty(Product.Description) ? Product.Description : "";
                        dbProduct["SellingPrice"] = Product.SellingPrice;
                        dbProduct["Image"] = Product.Image;
                    }

                    jObject[Constants.productsCollection] = ProductsList;
                    string output = JsonConvert.SerializeObject(jObject, Formatting.Indented);
                    File.WriteAllText(Constants.JsonDB, output);
                }
                else
                {
                    //Console.Write("Invalid Company ID, Try Again!");
                    //UpdateProduct();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public static void DeleteProduct(Product Product)
        {
            var json = File.ReadAllText(Constants.JsonDB);
            try
            {
                var jObject = JObject.Parse(json);
                JArray ProductsList = (JArray)jObject[Constants.productsCollection];

                if (Product.Id != null)
                {
                    var ProductToDeleted = ProductsList.FirstOrDefault(obj => obj["Id"].Value<string>() == Product.Id.ToString());

                    ProductsList.Remove(ProductToDeleted);

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