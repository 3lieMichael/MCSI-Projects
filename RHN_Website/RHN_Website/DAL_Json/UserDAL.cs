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
    public class UserDAL
    {
        public static List<User> GetUsers()
        {
            var json = File.ReadAllText(Constants.JsonDB);
            var users = new List<User>();
            try
            {
                var jObject = JObject.Parse(json);

                if (jObject != null)
                {
                    JArray UsersArrary = (JArray)jObject[Constants.usersCollection];
                    if (UsersArrary != null)
                    {
                        foreach (var item in UsersArrary)
                        {
                            users.Add(new User() {
                                Id = (Guid)(item["Id"]),
                                Name = item["Name"].ToString(),
                                Surname = item["Surname"].ToString(),
                                Username = item["Username"].ToString(),
                                Password = item["Password"].ToString()
                            });
                        }
                    }
                }

                return users;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public static void AddUser(User newUser)
        {
            var javaScriptSerializer = new JavaScriptSerializer();
            var newUserJSON = javaScriptSerializer.Serialize(newUser);
            try
            {
                var json = File.ReadAllText(Constants.JsonDB);
                var jsonObj = JObject.Parse(json);
                var usersList = jsonObj.GetValue(Constants.usersCollection) as JArray;
                var newUsersList = JObject.Parse(newUserJSON);
                usersList.Add(newUsersList);

                jsonObj[Constants.usersCollection] = usersList;
                string newJsonResult = JsonConvert.SerializeObject(jsonObj,
                                       Formatting.Indented);
                File.WriteAllText(Constants.JsonDB, newJsonResult);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public static void UpdateUser(User user)
        {
            string json = File.ReadAllText(Constants.JsonDB);

            try
            {
                var jObject = JObject.Parse(json);
                JArray usersList = (JArray)jObject[Constants.usersCollection];

                if (user.Id != null)
                {
                    foreach (var dbUser in usersList.Where(obj => obj["Id"].Value<string>() == user.Id.ToString()))
                    {
                        dbUser["Name"] = !string.IsNullOrEmpty(user.Name) ? user.Name : "";
                        dbUser["Surname"] = !string.IsNullOrEmpty(user.Surname) ? user.Surname : "";
                        dbUser["Username"] = !string.IsNullOrEmpty(user.Username) ? user.Username : "";
                        dbUser["Password"] = !string.IsNullOrEmpty(user.Password) ? user.Password : "";
                    }

                    jObject[Constants.usersCollection] = usersList;
                    string output = JsonConvert.SerializeObject(jObject, Formatting.Indented);
                    File.WriteAllText(Constants.JsonDB, output);
                }
                else
                {
                    //Console.Write("Invalid Company ID, Try Again!");
                    //UpdateUser();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public static void DeleteUser(User user)
        {
            var json = File.ReadAllText(Constants.JsonDB);
            try
            {
                var jObject = JObject.Parse(json);
                JArray usersList = (JArray)jObject[Constants.usersCollection];

                if (user.Id != null)
                {
                    var userToDeleted = usersList.FirstOrDefault(obj => obj["Id"].Value<string>() == user.Id.ToString());

                    usersList.Remove(userToDeleted);

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