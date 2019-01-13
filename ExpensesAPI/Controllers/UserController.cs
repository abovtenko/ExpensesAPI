using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ExpensesAPI.Models;
using Newtonsoft.Json;

namespace ExpensesAPI.Controllers
{
    [RoutePrefix("api/users")]
    public class UserController : ApiController
    {
        List<Transaction> transactions = new List<Transaction>
        {
            new Transaction
            {
                TransactionID = 1,
                UserID = 1,
                TransactionDate = "2019-09-09",
                Description = "",
                CreditAmount = 0,
                DebitAmount = 12.67
            }
        };

        List<User> users = new List<User>
        {
            new User
            {
                UserID = 1,
                Username = "firstUser"
            }
        };

        [HttpPost]
        [Route("")]
        public HttpResponseMessage Post([FromBody]User model)
        {
            if (ModelState.IsValid)
            {
                users.Add(model);
                return Request.CreateResponse(HttpStatusCode.Created, JsonConvert.SerializeObject(model));
            }

            return Request.CreateResponse(HttpStatusCode.BadRequest);

        }

        [HttpPut]
        [Route("")]
        public HttpResponseMessage Put([FromBody] User model)
        {
            if (ModelState.IsValid)
            {
                var toModify = users.Where(x => x.UserID == model.UserID).SingleOrDefault();
                if (toModify == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                }

                toModify.Username = model.Username;

                return Request.CreateErrorResponse(HttpStatusCode.OK, JsonConvert.SerializeObject(toModify));
            }

            return Request.CreateResponse(HttpStatusCode.BadRequest);
        }

        [HttpDelete]
        [Route("{id}")]
        public HttpResponseMessage Delete(int id)
        {
            var toDelete = users.Where(x => x.UserID == id).SingleOrDefault();
            if (toDelete == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            users.Remove(toDelete);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [Route("{id}")]
        public HttpResponseMessage Get(int id)
        {
            var result = users.Where(x => x.UserID == id).SingleOrDefault();
            if (result == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            var response = Request.CreateResponse(HttpStatusCode.OK, JsonConvert.SerializeObject(result));
            return response;
        }

        [HttpGet]
        [Route("{id}/transactions")]
        public HttpResponseMessage GetTransactions(int id)
        {
            var user = users.Where(x => x.UserID == id).SingleOrDefault();
            if (user == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            var result = transactions.Where(x => x.UserID == user.UserID);
            var response = Request.CreateResponse(HttpStatusCode.OK, JsonConvert.SerializeObject(result));
            return response;
        }

        [Route("")]
        public HttpResponseMessage GetAll()
        {
            var response = Request.CreateResponse(HttpStatusCode.OK, JsonConvert.SerializeObject(users));
            return response;
        }
    }
}
