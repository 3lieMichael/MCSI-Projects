using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;

namespace RHN_Website.Helpers
{
    public static class Constants
    {
        public static readonly string basePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory);
        public static readonly string JsonDB = $"{basePath}{"/bin/data.json"}";
        public static readonly string usersJsonDB = $"{basePath}{"/bin/data/users.json"}";
        public static readonly string productsJsonDB = $"{basePath}{"/bin/data/products.json"}";
        public static readonly string salesJsonDB = $"{basePath}{"/bin/data/sales.json"}";
        public static readonly string storeJsonDB = $"{basePath}{"/bin/data/store.json"}";

        public const string usersCollection = "users";
        public const string productsCollection = "products";
        public const string salesCollection = "sales";
        public const string storeCollection = "store";
    }
}