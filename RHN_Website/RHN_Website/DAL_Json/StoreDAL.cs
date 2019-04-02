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
    public class StoreDAL
    {
        public List<Store> GetStores()
        {
            var json = File.ReadAllText(Constants.storeJsonDB);
            var Stores = new List<Store>();
            try
            {
                var jObject = JObject.Parse(json);

                if (jObject != null)
                {
                    JArray StoresArrary = (JArray)jObject[Constants.storeCollection];
                    if (StoresArrary != null)
                    {
                        foreach (var item in StoresArrary)
                        {
                            Stores.Add(new Store()
                            {
                                ProductId = (Guid)(item["ProductId"]),
                                QuantityInStore = (int)item["QuantityInStore"],
                                QuantityInSold = (int)item["QuantityInSold"],
                                Finished = (bool)item["Finished"]
                            });
                        }
                    }
                }

                return Stores;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public void AddStore(Store newStore)
        {
            var javaScriptSerializer = new JavaScriptSerializer();
            var newStoreJSON = javaScriptSerializer.Serialize(newStore);
            try
            {
                var json = File.ReadAllText(Constants.storeJsonDB);
                var jsonObj = JObject.Parse(json);
                var StoresList = jsonObj.GetValue(Constants.storeCollection) as JArray;
                var newStoresList = JObject.Parse(newStoreJSON);
                StoresList.Add(newStoresList);

                jsonObj[Constants.storeCollection] = StoresList;
                string newJsonResult = JsonConvert.SerializeObject(jsonObj,
                                       Formatting.Indented);
                File.WriteAllText(Constants.storeJsonDB, newJsonResult);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public void UpdateStore(Store Store)
        {
            string json = File.ReadAllText(Constants.storeJsonDB);

            try
            {
                var jObject = JObject.Parse(json);
                JArray StoresList = (JArray)jObject[Constants.storeCollection];

                if (Store.ProductId != null)
                {
                    foreach (var dbStore in StoresList.Where(obj => obj["ProductId"].Value<Guid>() == Store.ProductId))
                    {
                        dbStore["QuantityInStore"] = Store.QuantityInStore;
                        dbStore["QuantityInSold"] = Store.QuantityInSold;
                        dbStore["Finished"] = Store.Finished;
                    }

                    jObject[Constants.storeCollection] = StoresList;
                    string output = JsonConvert.SerializeObject(jObject, Formatting.Indented);
                    File.WriteAllText(Constants.storeJsonDB, output);
                }
                else
                {
                    //Console.Write("Invalid Company ID, Try Again!");
                    //UpdateStore();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public void DeleteStore(Store Store)
        {
            var json = File.ReadAllText(Constants.storeJsonDB);
            try
            {
                var jObject = JObject.Parse(json);
                JArray StoresList = (JArray)jObject[Constants.storeCollection];

                if (Store.ProductId != null)
                {
                    var StoreToDeleted = StoresList.FirstOrDefault(obj => obj["ProductId"].Value<Guid>() == Store.ProductId);

                    StoresList.Remove(StoreToDeleted);

                    string output = JsonConvert.SerializeObject(jObject, Formatting.Indented);
                    File.WriteAllText(Constants.storeJsonDB, output);
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