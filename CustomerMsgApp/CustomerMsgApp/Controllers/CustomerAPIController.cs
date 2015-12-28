using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CustomerMsgApp.Service;
using System.Web.Http;
using System.Net.Http;
using System.Net;
using System.Threading.Tasks;
using CustomerMsgApp.Model;

namespace CustomerMsgApp.Controllers
{
    public class CustomerAPIController : ApiController
    {
        public CustomerAPIController()
        {
        }

        #region Method
        // GET api/userapi
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/userapi/5
        public string Get(int? id)
        {
            return "value";
        }

        // POST api/userapi
        public void Post([FromBody]string value)
        {
        }

        // PUT api/userapi/5
        public void Put(int id, [FromBody]string value)
        {
        }

        [HttpPost]
        public HttpResponseMessage UserLogin(Login Login)
        {
            try
            {
                if (Login.UserName != null && Login.Password != null)
                {
                    var loginModel = CustomerService.Login(Login.UserName, Login.Password);
                    if (loginModel)
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, Login);
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.NotFound);
                    }
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        [HttpGet]
        public IEnumerable<Customer> GetAllCustomer()
        {
            try
            {
                var custList = CustomerService.GetAllCustomer();
                return custList;
            }
            catch (Exception)
            {
                return null;
            }
        }

        [HttpPost]
        public async Task SendMessage()
        {
            await CustomerService.SendEmail("2bhumikapatel@gmail.com", "bhumika@scrumbees.com", "New Message", "Hello");
            CustomerService.SendSMS("+16016214288", "+919979190606", "New Message"); // (601) 621-4288
        }

        #endregion

        public class Login
        {
            public string UserName { get; set; }
            public string Password { get; set; }
        }

    }
}