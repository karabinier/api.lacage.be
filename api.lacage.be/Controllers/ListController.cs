using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace api.lacage.be.Controllers
{
    public class ListController : ApiController
    {
        // GET api/list
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/list/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/list
        public void Post([FromBody]string value)
        {
        }

        // PUT api/list/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/list/5
        public void Delete(int id)
        {
        }
    }
}
