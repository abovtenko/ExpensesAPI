using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ExpensesAPI.Models;
using Newtonsoft.Json;
using ExpensesAPI.Data;

namespace ExpensesAPI.Controllers
{
    [RoutePrefix("api/transactions")]
    public class TransactionController : ApiController
    {
        private readonly ExpensesContext _context = new ExpensesContext();

        [HttpPost]
        [Route("")]
        public HttpResponseMessage Post([FromBody]Transaction model)
        {
            if (ModelState.IsValid)
            {
                _context.Transactions.Add(model);
                var content = new { location = $"{Request.RequestUri.Host}/api/transactions/{model.TransactionID}" };

                return Request.CreateResponse(HttpStatusCode.Created, JsonConvert.SerializeObject(content));
            }

            return Request.CreateResponse(HttpStatusCode.BadRequest);

        }

        [HttpPut]
        [Route("")]
        public HttpResponseMessage Put([FromBody] Transaction model)
        {
            if (ModelState.IsValid)
            {
                var target = _context.Transactions.Where(x => x.TransactionID == model.TransactionID).SingleOrDefault();
                if (target == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                }

                target.UserID = model.UserID;
                target.TransactionDate = model.TransactionDate;
                target.Description = model.Description;
                target.CreditAmount = model.CreditAmount;
                target.DebitAmount = model.DebitAmount;
                var content = new { location = $"{Request.RequestUri.Host}/api/transactions/{model.TransactionID }" };

                return Request.CreateResponse(HttpStatusCode.OK, JsonConvert.SerializeObject(content));
            }

            return Request.CreateResponse(HttpStatusCode.BadRequest);
        }

        
        [HttpDelete]
        [Route("{id}")]
        public HttpResponseMessage Delete(int id)
        {
            var target = _context.Transactions.Where(x => x.TransactionID == id).SingleOrDefault();
            if (target == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            _context.Transactions.Remove(target);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [Route("{id}")]
        public HttpResponseMessage Get(int id)
        {
            var result = _context.Transactions.Where(x => x.TransactionID == id).SingleOrDefault();
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
            var result = _context.Transactions.ToList();
            var response = Request.CreateResponse(HttpStatusCode.OK, JsonConvert.SerializeObject(result));

            return response;
        }
    }
}
