using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ExpensesAPI.Data;
using ExpensesAPI.Models;
using ExpensesAPI.Services;
using Newtonsoft.Json;

namespace ExpensesAPI.Controllers
{
    [RoutePrefix("api/users")]
    public class UserController : ApiController
    {
        private readonly UserService _userService = new UserService();

        [HttpPost]
        [Route("")]
        public HttpResponseMessage Post([FromBody]User model)
        {
            if (ModelState.IsValid)
            {
                _userService.Create(model);
                _userService.Save();
                var content = new { location = $"{Request.RequestUri.Host}/api/users/{model.UserID}"};

                return Request.CreateResponse(HttpStatusCode.Created, JsonConvert.SerializeObject(content));
            }

            return Request.CreateResponse(HttpStatusCode.BadRequest);

        }

        [HttpPut]
        [Route("")]
        public HttpResponseMessage Put([FromBody] User model)
        {
            if (ModelState.IsValid)
            {
                _userService.Update(model);
                /*
                if (target == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                }
                */
                _userService.Save();
                var content = new { location = $"{Request.RequestUri.Host}/api/users/{model.UserID}" };

                return Request.CreateResponse(HttpStatusCode.OK, JsonConvert.SerializeObject(content));
            }

            return Request.CreateResponse(HttpStatusCode.BadRequest);
        }

        [HttpDelete]
        [Route("{id}")]
        public HttpResponseMessage Delete(int id)
        {
            _userService.Remove(id);
            /*
            if (target == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
            */
            _userService.Save();

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [Route("{id}")]
        public HttpResponseMessage Get(int id)
        {
            var result = _userService.Get(id);
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
            var transactionService = new TransactionService();
            var result = transactionService.GetWhere(x => x.User.UserID == id).ToList();
            var response = Request.CreateResponse(HttpStatusCode.OK, JsonConvert.SerializeObject(result));

            return response;
        }

        [Route("")]
        public HttpResponseMessage GetAll()
        {
            var result = _userService.GetAll();
            var response = Request.CreateResponse(HttpStatusCode.OK, JsonConvert.SerializeObject(result));

            return response;
        }
    }
}
