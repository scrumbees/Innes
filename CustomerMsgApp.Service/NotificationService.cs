using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomerMsgApp.Model;
using System.Net.Mail;
using System.Configuration;
using Twilio;

namespace CustomerMsgApp.Service
{
    public partial class NotificationService
    {
        private readonly CustomerContext _CustomerService;

        public NotificationService()
        {
            this._CustomerService = new CustomerContext();
        }

        public bool Login(string UserName, string Password)
        {
            if (UserName == "" || Password == "")
            {
                return false;
            }

            string uname = System.Configuration.ConfigurationManager.AppSettings["UserName"];
            string pwd = System.Configuration.ConfigurationManager.AppSettings["Password"];

            if (UserName != string.Empty && Password != string.Empty)
            {
                if (UserName.ToLower() == uname.ToLower() && Password.ToLower() == pwd.ToLower())
                {
                    return true;
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

        public dynamic GetAllCustomer(NotificationSearch NotificationSearch)
        {

            var NotificationDataList = _CustomerService.NotificationData.AsQueryable();
            if (!string.IsNullOrEmpty(NotificationSearch.TourOpCode))
            {
                NotificationDataList = from e in NotificationDataList where e.TourOpCode.ToString().Trim().ToLower() == NotificationSearch.TourOpCode.ToString().Trim().ToLower() select e;
            }
            if (!string.IsNullOrEmpty(NotificationSearch.DirectOrAgent))
            {
                NotificationDataList = from e in NotificationDataList where e.DirectOrAgent.ToString().Trim().ToLower() == NotificationSearch.DirectOrAgent.ToString().Trim().ToLower() select e;
            }
            if (NotificationSearch.StartDate != null)
            {
                NotificationDataList = from e in NotificationDataList where e.StartDate == NotificationSearch.StartDate select e;
            }
            if (!string.IsNullOrEmpty(NotificationSearch.DeparturePoint))
            {
                NotificationDataList = from e in NotificationDataList where e.DeparturePoint.ToString().Trim().ToLower() == NotificationSearch.DeparturePoint.ToString().Trim().ToLower() select e;
            }
            if (!string.IsNullOrEmpty(NotificationSearch.ArrivalPoint))
            {
                NotificationDataList = from e in NotificationDataList where e.ArrivalPoint.ToString().Trim().ToLower() == NotificationSearch.ArrivalPoint.ToString().Trim().ToLower() select e;
            }
            if (NotificationSearch.TravelDate != null)
            {
                NotificationDataList = from e in NotificationDataList where e.TravelDate == NotificationSearch.TravelDate select e;
            }
            if (!string.IsNullOrEmpty(NotificationSearch.TravelDepatureTime))
            {
                NotificationDataList = from e in NotificationDataList where e.TravelDepatureTime.ToString().Trim().ToLower().Replace(" ", "") == NotificationSearch.TravelDepatureTime.ToString().Trim().ToLower().Replace(" ", "") select e;
            }
            if (!string.IsNullOrEmpty(NotificationSearch.TravelArrivalTime))
            {
                NotificationDataList = from e in NotificationDataList where e.TravelArrivalTime.ToString().Trim().ToLower().Replace(" ", "") == NotificationSearch.TravelArrivalTime.ToString().Trim().ToLower().Replace(" ", "") select e;
            }
            if (!string.IsNullOrEmpty(NotificationSearch.TravelDirection))
            {
                NotificationDataList = from e in NotificationDataList where e.TravelDirection.ToString().Trim().ToLower() == NotificationSearch.TravelDirection.ToString().Trim().ToLower() select e;
            }
            if (!string.IsNullOrEmpty(NotificationSearch.TransportCarrier))
            {
                NotificationDataList = from e in NotificationDataList where e.TransportCarrier.ToString().Trim().ToLower() == NotificationSearch.TransportCarrier.ToString().Trim().ToLower() select e;
            }
            if (!string.IsNullOrEmpty(NotificationSearch.TransportNumber))
            {
                NotificationDataList = from e in NotificationDataList where e.TransportNumber.ToString().Trim().ToLower() == NotificationSearch.TransportNumber.ToString().Trim().ToLower() select e;
            }
            if (!string.IsNullOrEmpty(NotificationSearch.TransportType))
            {
                NotificationDataList = from e in NotificationDataList where e.TransportType.ToString().Trim().ToLower() == NotificationSearch.TransportType.ToString().Trim().ToLower() select e;
            }
            if (!string.IsNullOrEmpty(NotificationSearch.TransportChain))
            {
                NotificationDataList = from e in NotificationDataList where e.TransportChain.ToString().Trim().ToLower() == NotificationSearch.TransportChain.ToString().Trim().ToLower() select e;
            }
            if (!string.IsNullOrEmpty(NotificationSearch.CountryName))
            {
                NotificationDataList = from e in NotificationDataList where e.CountryCode == ((from row in _CustomerService.NotificationData where row.CountryName.ToLower().Trim() == NotificationSearch.CountryName.ToLower().Trim() select row.CountryCode).FirstOrDefault()) select e;
            }
            if (!string.IsNullOrEmpty(NotificationSearch.ResortName))
            {
                NotificationDataList = from e in NotificationDataList where e.ResortCode == ((from row in _CustomerService.NotificationData where row.ResortName.ToLower().Trim() == NotificationSearch.ResortName.ToLower().Trim() select row.ResortCode).FirstOrDefault()) select e;
            }
            if (!string.IsNullOrEmpty(NotificationSearch.AccommodationName))
            {
                var AccommodationCode = (from row in _CustomerService.NotificationData where row.AccommodationName.Trim().ToLower().ToString() == NotificationSearch.AccommodationName.Trim().ToLower().ToString() select row.AccommodationCode).FirstOrDefault();
                NotificationDataList = from e in NotificationDataList where e.AccommodationCode == AccommodationCode select e;
            }
            var EmailCount = NotificationDataList.Where(x => x.Email != "NULL").Count();
            var MobileCount = NotificationDataList.Where(x => x.MobileNo != "NULL").Count();
            var customer = (from row in NotificationDataList
                            select new
                            {
                                BookingRef = row.BookingRef,
                                TourOpCode = row.TourOpCode,
                                PassengerId = row.PassengerId,
                                Title = row.Title,
                                FirstName = row.FirstName,
                                LastName = row.Surname,
                                MobileNo = row.MobileNo,
                                Email = row.Email,
                                DirectOrAgent = row.DirectOrAgent,
                                StartDate = row.StartDate,
                                DeparturePoint = row.DeparturePoint,
                                ArrivalPoint = row.ArrivalPoint
                            }).OrderByDescending(x => x.StartDate).ToList();

            var datatable = new
               {
                   sEcho = NotificationSearch.sEcho,
                   iTotalRecords = customer.OrderByDescending(e => e.StartDate).Skip(NotificationSearch.iDisplayStart).Take(NotificationSearch.iDisplayLength).Count(),
                   iTotalDisplayRecords = customer.Count(),
                   aaData = customer.OrderByDescending(e => e.StartDate).Skip(NotificationSearch.iDisplayStart).Take(NotificationSearch.iDisplayLength).ToList(),
                   EmailCount = EmailCount,
                   MobileCount = MobileCount
               };
            return datatable;
        }

        public List<NotificationData> GetCustomerSendMessage(NotificationSearch NotificationSearch)
        {
            var NotificationDataList = _CustomerService.NotificationData.AsQueryable();
            if (!string.IsNullOrEmpty(NotificationSearch.TourOpCode))
            {
                NotificationDataList = from e in NotificationDataList where e.TourOpCode.ToString().Trim().ToLower() == NotificationSearch.TourOpCode.ToString().Trim().ToLower() select e;
            }
            if (!string.IsNullOrEmpty(NotificationSearch.DirectOrAgent))
            {
                NotificationDataList = from e in NotificationDataList where e.DirectOrAgent.ToString().Trim().ToLower() == NotificationSearch.DirectOrAgent.ToString().Trim().ToLower() select e;
            }
            if (NotificationSearch.StartDate != null)
            {
                NotificationDataList = from e in NotificationDataList where e.StartDate == NotificationSearch.StartDate select e;
            }
            if (!string.IsNullOrEmpty(NotificationSearch.DeparturePoint))
            {
                NotificationDataList = from e in NotificationDataList where e.DeparturePoint.ToString().Trim().ToLower() == NotificationSearch.DeparturePoint.ToString().Trim().ToLower() select e;
            }
            if (!string.IsNullOrEmpty(NotificationSearch.ArrivalPoint))
            {
                NotificationDataList = from e in NotificationDataList where e.ArrivalPoint.ToString().Trim().ToLower() == NotificationSearch.ArrivalPoint.ToString().Trim().ToLower() select e;
            }
            if (NotificationSearch.TravelDate != null)
            {
                NotificationDataList = from e in NotificationDataList where e.TravelDate == NotificationSearch.TravelDate select e;
            }
            if (!string.IsNullOrEmpty(NotificationSearch.TravelDepatureTime))
            {
                NotificationDataList = from e in NotificationDataList where e.TravelDepatureTime.ToString().Trim().ToLower().Replace(" ", "") == NotificationSearch.TravelDepatureTime.ToString().Trim().ToLower().Replace(" ", "") select e;
            }
            if (!string.IsNullOrEmpty(NotificationSearch.TravelArrivalTime))
            {
                NotificationDataList = from e in NotificationDataList where e.TravelArrivalTime.ToString().Trim().ToLower().Replace(" ", "") == NotificationSearch.TravelArrivalTime.ToString().Trim().ToLower().Replace(" ", "") select e;
            }
            if (!string.IsNullOrEmpty(NotificationSearch.TravelDirection))
            {
                NotificationDataList = from e in NotificationDataList where e.TravelDirection.ToString().Trim().ToLower() == NotificationSearch.TravelDirection.ToString().Trim().ToLower() select e;
            }
            if (!string.IsNullOrEmpty(NotificationSearch.TransportCarrier))
            {
                NotificationDataList = from e in NotificationDataList where e.TransportCarrier.ToString().Trim().ToLower() == NotificationSearch.TransportCarrier.ToString().Trim().ToLower() select e;
            }
            if (!string.IsNullOrEmpty(NotificationSearch.TransportNumber))
            {
                NotificationDataList = from e in NotificationDataList where e.TransportNumber.ToString().Trim().ToLower() == NotificationSearch.TransportNumber.ToString().Trim().ToLower() select e;
            }
            if (!string.IsNullOrEmpty(NotificationSearch.TransportType))
            {
                NotificationDataList = from e in NotificationDataList where e.TransportType.ToString().Trim().ToLower() == NotificationSearch.TransportType.ToString().Trim().ToLower() select e;
            }
            if (!string.IsNullOrEmpty(NotificationSearch.TransportChain))
            {
                NotificationDataList = from e in NotificationDataList where e.TransportChain.ToString().Trim().ToLower() == NotificationSearch.TransportChain.ToString().Trim().ToLower() select e;
            }
            if (!string.IsNullOrEmpty(NotificationSearch.CountryName))
            {
                NotificationDataList = from e in NotificationDataList where e.CountryCode == ((from row in _CustomerService.NotificationData where row.CountryName.ToLower().Trim() == NotificationSearch.CountryName.ToLower().Trim() select row.CountryCode).FirstOrDefault()) select e;
            }
            if (!string.IsNullOrEmpty(NotificationSearch.ResortName))
            {
                NotificationDataList = from e in NotificationDataList where e.ResortCode == ((from row in _CustomerService.NotificationData where row.ResortName.ToLower().Trim() == NotificationSearch.ResortName.ToLower().Trim() select row.ResortCode).FirstOrDefault()) select e;
            }
            if (!string.IsNullOrEmpty(NotificationSearch.AccommodationName))
            {
                var AccommodationCode = (from row in _CustomerService.NotificationData where row.AccommodationName.Trim().ToLower().ToString() == NotificationSearch.AccommodationName.Trim().ToLower().ToString() select row.AccommodationCode).FirstOrDefault();
                NotificationDataList = from e in NotificationDataList where e.AccommodationCode == AccommodationCode select e;
            }

            var EmailCount = NotificationDataList.Where(x => x.Email != "NULL").Count();
            var MobileCount = NotificationDataList.Where(x => x.MobileNo != "NULL").Count();

            var customer = (from row in NotificationDataList
                            select row).OrderByDescending(x => x.StartDate).ToList();

            return customer;
        }

        public List<string> GetTourOpCode()
        {
            try
            {
                var datalist = _CustomerService.NotificationData.Where(n => n.TourOpCode != "NULL").AsQueryable();
                var data = (from row in datalist
                            where row.TourOpCode != null
                            select row.TourOpCode).Distinct().ToList();
                return data;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<string> GetDeparturePoint()
        {
            try
            {
                var datalist = _CustomerService.NotificationData.AsQueryable();
                var data = (from row in datalist
                            where row.DeparturePoint != null
                            select row.DeparturePoint).Distinct().ToList();
                return data;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<string> GetArrivalPoint()
        {
            try
            {
                var datalist = _CustomerService.NotificationData.AsQueryable();
                var data = (from row in datalist
                            where row.ArrivalPoint != null
                            select row.ArrivalPoint).Distinct().ToList();
                return data;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<string> GetTravelDirection()
        {
            try
            {
                var datalist = _CustomerService.NotificationData.AsQueryable();
                var data = (from row in datalist
                            where row.TravelDirection != null
                            select row.TravelDirection).Distinct().ToList();
                return data;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<string> GetTransportCarrier()
        {
            try
            {
                var datalist = _CustomerService.NotificationData.AsQueryable();
                var data = (from row in datalist
                            where row.TransportCarrier != null
                            select row.TransportCarrier).Distinct().ToList();
                return data;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<string> GetTransportNumber()
        {
            try
            {
                var datalist = _CustomerService.NotificationData.AsQueryable();
                var data = (from row in datalist
                            where row.TransportNumber != null
                            select row.TransportNumber).Distinct().ToList();
                return data;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<string> GetTransportType()
        {
            try
            {
                var datalist = _CustomerService.NotificationData.AsQueryable();
                var data = (from row in datalist
                            where row.TransportType != null
                            select row.TransportType).Distinct().ToList();
                return data;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<string> GetTransportChain()
        {
            try
            {
                var datalist = _CustomerService.NotificationData.AsQueryable();
                var data = (from row in datalist
                            where row.TransportChain != null
                            select row.TransportChain).Distinct().ToList();
                return data;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<string> GetCountryName()
        {
            try
            {
                var datalist = _CustomerService.NotificationData.AsQueryable();
                var data = (from row in datalist
                            where row.CountryName != null
                            select row.CountryName).Distinct().ToList();
                return data;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<string> GetResortName()
        {
            try
            {
                var datalist = _CustomerService.NotificationData.AsQueryable();
                var data = (from row in datalist
                            where row.ResortName != null
                            select row.ResortName).Distinct().ToList();
                return data;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<string> GetAccommodationName()
        {
            try
            {
                var datalist = _CustomerService.NotificationData.AsQueryable();
                var data = (from row in datalist
                            where row.AccommodationName != null
                            select row.AccommodationName).Distinct().ToList();
                return data;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static void SendEmail(string fromEmailAddress, string toEmailAddress, string emailSubject, string emailMessage)
        {
            try
            {
                MailMessage mailMessage = new MailMessage();
                mailMessage.From = new MailAddress(fromEmailAddress);
                mailMessage.Subject = emailSubject;
                mailMessage.Body = emailMessage;
                mailMessage.IsBodyHtml = true;
                mailMessage.To.Add(new MailAddress(toEmailAddress));

                Object state = mailMessage;
                var smtpClient = new SmtpClient();
                smtpClient.SendAsync(mailMessage, state);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void SendSMS(string fromNumber, string toNumber, string Message)
        {
            string ACCOUNT_SID = ConfigurationManager.AppSettings["ACCOUNTSID"];
            string AUTH_TOKEN = ConfigurationManager.AppSettings["AUTHTOKEN"];

            TwilioRestClient client = new TwilioRestClient(ACCOUNT_SID, AUTH_TOKEN);
            var res = client.SendSmsMessage(fromNumber, toNumber, Message);
        }

        public static bool EmailLogReportAdd(int Type,string Email, string Message)
        {
            try
            {
                using (CustomerContext db = new CustomerContext())
                {
                    EmailLogReport emaillogreport = new EmailLogReport();
                    emaillogreport.Type = Type;
                    emaillogreport.Email = Email;
                    emaillogreport.Message = Message;
                    db.EmailLogReport.Add(emaillogreport);
                    db.SaveChanges();
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
