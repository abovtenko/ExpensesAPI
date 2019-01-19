using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ExpensesAPI.Models;
using Newtonsoft.Json;
using ExpensesAPI.Services;

namespace ExpensesAPI.Controllers
{
    [RoutePrefix("api/transactions")]
    public class TransactionController : ApiController
    {
        private readonly TransactionService _transactionService = new TransactionService();

        [HttpPost]
        [Route("")]
        public HttpResponseMessage Post([FromBody]Transaction model)
        {
            if (ModelState.IsValid)
            {
                _transactionService.Create(model);
                _transactionService.Save();
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
                _transactionService.Update(model);
                _transactionService.Save();
                var content = new { location = $"{Request.RequestUri.Host}/api/transactions/{model.TransactionID }" };

                return Request.CreateResponse(HttpStatusCode.OK, JsonConvert.SerializeObject(content));
            }

            return Request.CreateResponse(HttpStatusCode.BadRequest);
        }

        
        [HttpDelete]
        [Route("{id}")]
        public HttpResponseMessage Delete(int id)
        {
            _transactionService.Remove(id);
            _transactionService.Save();

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [Route("{id}")]
        public HttpResponseMessage Get(int id)
        {
            var result = _transactionService.Get(id);
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
            var result = _transactionService.GetAll();
            var response = Request.CreateResponse(HttpStatusCode.OK, JsonConvert.SerializeObject(result));

            return response;
        }
    }
}
