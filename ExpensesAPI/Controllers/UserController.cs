using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ExpensesAPI.Data;
using ExpensesAPI.Models;
using Newtonsoft.Json;

namespace ExpensesAPI.Controllers
{
    [RoutePrefix("api/users")]
    public class UserController : ApiController
    {
        private readonly ExpensesContext _context = new ExpensesContext();

        [HttpPost]
        [Route("")]
        public HttpResponseMessage Post([FromBody]User model)
        {
            if (ModelState.IsValid)
            {
                _context.Users.Add(model);
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
                var target = _context.Users.Where(x => x.UserID == model.UserID).SingleOrDefault();
                if (target == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                }

                target.Username = model.Username;
                var content = new { location = $"{Request.RequestUri.Host}/api/users/{model.UserID}" };

                return Request.CreateResponse(HttpStatusCode.OK, JsonConvert.SerializeObject(content));
            }

            return Request.CreateResponse(HttpStatusCode.BadRequest);
        }

        [HttpDelete]
        [Route("{id}")]
        public HttpResponseMessage Delete(int id)
        {
            var target = _context.Users.Where(x => x.UserID == id).SingleOrDefault();
            if (target == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            _context.Users.Remove(target);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [Route("{id}")]
        public HttpResponseMessage Get(int id)
        {
            var result = _context.Users.Where(x => x.UserID == id).SingleOrDefault();
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
            var user = _context.Users.Where(x => x.UserID == id).SingleOrDefault();
            if (user == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            var result = _context.Transactions.Where(x => x.UserID == user.UserID);
            var response = Request.CreateResponse(HttpStatusCode.OK, JsonConvert.SerializeObject(result));

            return response;
        }

        [Route("")]
        public HttpResponseMessage GetAll()
        {
            var result = _context.Users.ToList();
            var response = Request.CreateResponse(HttpStatusCode.OK, JsonConvert.SerializeObject(result));

            return response;
        }
    }
}
