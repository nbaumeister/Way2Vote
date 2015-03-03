using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Way2Vote.DTOs;

namespace Way2Vote.Controllers
{
    public class PollController : ApiController
    {
        // GET: api/Poll
        public IEnumerable<Poll> Get()
        {
            return new Poll[] { new Poll { Name = "Poll1", Autor = "Veit" }, new Poll { Name = "Poll2", Autor = "Peter" } };
        }

        // GET: api/Poll/5
        public Poll Get(int id)
        {
            switch (id){
                case 1:
                    return new Poll { Name = "Poll1", Autor = "Veit" };
                    break;
                case 2: 
                    return new Poll { Name = "Poll2", Autor = "Peter" };
                    break;
            }
            throw new HttpResponseException(HttpStatusCode.NotFound);
        }

        // POST: api/Poll
        public void Post([FromBody]Poll value)
        {
        }

        // PUT: api/Poll/5
        public void Put(int id, [FromBody]Poll value)
        {
        }

        // DELETE: api/Poll/5
        public void Delete(int id)
        {
        }
    }
}
