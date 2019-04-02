using RHN_Website.DAL_SQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RHN_Website.Controllers
{
    public class RestockController : ApiController
    {
        public IHttpActionResult RestockProduct(int id)
        {
            var product = StoreDAL.GetStoredProduct(Guid.NewGuid());
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }
    }
}
