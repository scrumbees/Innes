using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CustomerMsgApp.Service;
using System.Web.Http;
using System.Net.Http;
using System.Net;
using CustomerMsgApp.Model;
using System.Threading.Tasks;

namespace CustomerMsgApp.Controllers
{
    public class CustomerAPIController : ApiController
    {
        public NotificationService _NotificationService;

        public CustomerAPIController()
        {
            _NotificationService = new NotificationService();
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
        public bool UserLogin(Login Login)
        {
            try
            {
                if (Login.UserName != null && Login.Password != null)
                {
                    var loginModel = _NotificationService.Login(Login.UserName, Login.Password);
                    if (loginModel != null)
                    {
                        return loginModel;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        [HttpPost]
        public dynamic GetAllNotification(NotificationSearch NotificationSearch)
        {
            try
            {
                dynamic NotificationList;
                NotificationList = _NotificationService.GetAllCustomer(NotificationSearch);
                return NotificationList;
            }
            catch (Exception)
            {
                return null;
            }
        }

        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [HttpPost]
        public string CheckPassword(NotificationSearch NotificationSearch)
        {
            string pwd = System.Configuration.ConfigurationManager.AppSettings["Password"];
            if (NotificationSearch.Password.ToLower() == pwd.ToLower() && NotificationSearch.SendCountSMS == NotificationSearch.MobileCount)
            {
                SendMessage(NotificationSearch);
                return "1";
            }
            else if (NotificationSearch.Password.ToLower() != pwd.ToLower())
            {
                return "3";
            }
            else if (NotificationSearch.SendCountSMS != NotificationSearch.MobileCount)
            {
                return "2";
            }
            else
            {
                return "1";
            }
        }

        [HttpPost]
        public void SendMessage(NotificationSearch NotificationSearch)
        {
            string pwd = System.Configuration.ConfigurationManager.AppSettings["Password"];
            if (NotificationSearch.Password.ToLower() == pwd.ToLower())
            {
                var NotificationList = _NotificationService.GetCustomerSendMessage(NotificationSearch);
                if (NotificationList != null)
                {
                    foreach (var item in NotificationList)
                    {
                        if (item.Email != "NULL")
                        {
                            try
                            {
                                NotificationService.SendEmail("bhumika@scrumbees.com", item.Email, "Subject", NotificationSearch.Message);
                                NotificationService.EmailLogReportAdd(1, item.Email, NotificationSearch.Message);
                            }
                            catch (Exception)
                            {
                            }
                        }
                        if (item.MobileNo != "NULL")
                        {
                            try
                            {
                                NotificationService.SendSMS("+16016214288", item.MobileNo, NotificationSearch.Message); // (601) 621-4288
                                NotificationService.EmailLogReportAdd(2, item.MobileNo, NotificationSearch.Message);
                            }
                            catch (Exception)
                            {
                            }
                        }
                    }
                }
            }
        }

        public IEnumerable<string> GetTourOpCode()
        {
            try
            {
                dynamic NotificationList;
                NotificationList = _NotificationService.GetTourOpCode();
                return NotificationList;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public IEnumerable<string> GetDeparturePoint()
        {
            try
            {
                dynamic NotificationList;
                NotificationList = _NotificationService.GetDeparturePoint();
                return NotificationList;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public IEnumerable<string> GetArrivalPoint()
        {
            try
            {
                dynamic NotificationList;
                NotificationList = _NotificationService.GetArrivalPoint();
                return NotificationList;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public IEnumerable<string> GetTravelDirection()
        {
            try
            {
                dynamic NotificationList;
                NotificationList = _NotificationService.GetTravelDirection();
                return NotificationList;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public IEnumerable<string> GetTransportCarrier()
        {
            try
            {
                dynamic NotificationList;
                NotificationList = _NotificationService.GetTransportCarrier();
                return NotificationList;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public IEnumerable<string> GetTransportNumber()
        {
            try
            {
                dynamic NotificationList;
                NotificationList = _NotificationService.GetTransportNumber();
                return NotificationList;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public IEnumerable<string> GetTransportType()
        {
            try
            {
                dynamic NotificationList;
                NotificationList = _NotificationService.GetTransportType();
                return NotificationList;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public IEnumerable<string> GetTransportChain()
        {
            try
            {
                dynamic NotificationList;
                NotificationList = _NotificationService.GetTransportChain();
                return NotificationList;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public IEnumerable<string> GetCountryName()
        {
            try
            {
                dynamic NotificationList;
                NotificationList = _NotificationService.GetCountryName();
                return NotificationList;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public IEnumerable<string> GetResortName()
        {
            try
            {
                dynamic NotificationList;
                NotificationList = _NotificationService.GetResortName();
                return NotificationList;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public IEnumerable<string> GetAccommodationName()
        {
            try
            {
                dynamic NotificationList;
                NotificationList = _NotificationService.GetAccommodationName();
                return NotificationList;
            }
            catch (Exception)
            {
                return null;
            }
        }

        #endregion

        public class Login
        {
            public string UserName { get; set; }
            public string Password { get; set; }
        }
    }
}