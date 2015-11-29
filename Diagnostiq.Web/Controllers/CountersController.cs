using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Diagnostiq.Web.Controllers
{
    public class CountersController : ApiController
    {
        // GET: api/Counters
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Counters/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Counters
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Counters/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Counters/5
        public void Delete(int id)
        {
        }
    }
}
