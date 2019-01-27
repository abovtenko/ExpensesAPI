using System.Net;
using System.Net.Http;
using ExpensesCoreAPI.Models;
using Newtonsoft.Json;
using ExpensesCoreAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace ExpensesCoreAPI.Controllers
{
    [ApiController]
    [Route("api/transactions")]
    public class TransactionController : ControllerBase
    {
        private readonly IService<Transaction> _transactionService;

        public TransactionController(IService<Transaction> service)
        {
            _transactionService = service;
        }

        [HttpPost]
        [Route("")]
        public HttpResponseMessage Post([FromBody]Transaction model)
        {
            if (!ModelState.IsValid)
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);                
            }

            _transactionService.Create(model);
            _transactionService.Save();

            var response = new HttpResponseMessage(HttpStatusCode.Created);
            response.Content = new StringContent($"{{ location = {Request.Host}/api/transactions/{model.TransactionID} }}");

            return response;
        }

        [HttpPut]
        [Route("")]
        public HttpResponseMessage Put([FromBody] Transaction model)
        {
            if (!ModelState.IsValid)
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }

            _transactionService.Update(model);
            _transactionService.Save();

            var response = new HttpResponseMessage(HttpStatusCode.OK);
            response.Content = new StringContent($"{{ location = {Request.Host}/api/transactions/{model.TransactionID} }}");

            return response;
        }

        
        [HttpDelete]
        [Route("{id}")]
        public IActionResult Delete(int id)
        {
            _transactionService.Remove(id);
            _transactionService.Save();

            return Ok();
        }

        [Route("{id}")]
        public IActionResult Get(int id)
        {
            var result = _transactionService.Get(id);
            if (result == null)
            {
                return NotFound();
            }

            //var response = Request.CreateResponse(HttpStatusCode.OK, JsonConvert.SerializeObject(result));

            return Ok(result);
        }

        [Route("")]
        public IActionResult GetAll()
        {
            var result = _transactionService.GetAll();
            //var response = Request.CreateResponse(HttpStatusCode.OK, JsonConvert.SerializeObject(result));

            return Ok(JsonConvert.SerializeObject(result));
        }
    }
}
