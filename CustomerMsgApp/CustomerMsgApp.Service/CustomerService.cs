using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomerMsgApp.Model;
using System.Net.Mail;
using System.Configuration;
using Twilio;
using System.Net.Http;
using System.Net.Http.Headers;

namespace CustomerMsgApp.Service
{
    public partial class CustomerService
    {
        public CustomerService()
        {

        }

        public static bool Login(string UserName, string Password)
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

        public static List<Customer> GetAllCustomer()
        {
            try
            {
                var dataList = new List<Customer>();
                using (var db = new CustomerContext())
                {
                    var dataOBJ = db.Customer.AsQueryable();
                    dataList = dataOBJ.ToList();
                }
                return dataList;
            }
            catch (Exception ex)
            {
                //ErrorLog.InsertLog(ex);
                return null;
            }
        }

        public static async Task SendEmail(string fromEmailAddress, string toEmailAddress, string emailSubject, string emailMessage)
        {
            try
            {
                using (MailMessage mailMessage = new MailMessage())
                {
                    mailMessage.From = new MailAddress(fromEmailAddress);
                    mailMessage.Subject = emailSubject;
                    mailMessage.Body = emailMessage;
                    mailMessage.IsBodyHtml = true;
                    mailMessage.To.Add(new MailAddress(toEmailAddress));

                    using (var smtpClient = new SmtpClient())
                    {
                        await smtpClient.SendMailAsync(mailMessage);
                    }
                }
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

        //public async Task SendMessage(string from, string to, string message)
        //{
        //    var accountSid = "ACXXXXXXXXXXXXXXXXXXXXXXXXXXX";
        //    var authToken = "XXXXXXXXXXXXXXXXXXXXXXXXXXXX";
        //    var targeturi = "https://api.twilio.com/2010-04-01/Accounts/{0}/SMS/Messages";

        //    var client = new HttpClient();
        //    client.DefaultRequestHeaders.Authorization = CreateAuthenticationHeader("Basic", accountSid, authToken);

        //    var content = new StringContent(string.Format("From={0}&amp;To={1}&amp;Body={2}", from, to, message));
        //    content.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");

        //    var response = await client.Post(string.Format(targeturi, accountSid), content);
        //    if (response.IsSuccessStatusCode)
        //    {
        //        //the POST succeeded, so update the UI accordingly
        //    }
        //    else
        //    {
        //        //the POST failed, so update the UI accordingly
        //    }
        //    return response;
        //}
    }
}
