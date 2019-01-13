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
    [RoutePrefix("api/transactions")]
    public class TransactionController : ApiController
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

        [HttpPost]
        [Route("")]
        public HttpResponseMessage Post([FromBody]Transaction model)
        {
            if (ModelState.IsValid)
            {
                transactions.Add(model);
                return Request.CreateResponse(HttpStatusCode.Created,  JsonConvert.SerializeObject(model));
            }

            return Request.CreateResponse(HttpStatusCode.BadRequest);

        }

        [HttpPut]
        [Route("")]
        public HttpResponseMessage Put([FromBody] Transaction model)
        {
            if (ModelState.IsValid)
            {
                var target = transactions.Where(x => x.TransactionID == model.TransactionID).SingleOrDefault();
                if (target == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                }

                target.UserID = model.UserID;
                target.TransactionDate = model.TransactionDate;
                target.Description = model.Description;
                target.CreditAmount = model.CreditAmount;
                target.DebitAmount = model.DebitAmount;

                return Request.CreateErrorResponse(HttpStatusCode.OK, JsonConvert.SerializeObject(target));
            }

            return Request.CreateResponse(HttpStatusCode.BadRequest);
        }

        
        [HttpDelete]
        [Route("{id}")]
        public HttpResponseMessage Delete(int id)
        {
            var target = transactions.Where(x => x.TransactionID == id).SingleOrDefault();
            if (target == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            transactions.Remove(target);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [Route("{id}")]
        public HttpResponseMessage Get(int id)
        {
            var result = transactions.Where(x => x.TransactionID == id).SingleOrDefault();
            if (result == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            var response = Request.CreateResponse(HttpStatusCode.OK, JsonConvert.SerializeObject(result));
            return response;
        }

        [Route("")]
        public HttpResponseMessage GetAll()
        {
            var response = Request.CreateResponse(HttpStatusCode.OK, JsonConvert.SerializeObject(transactions));
            return response;
        }
    }
}
