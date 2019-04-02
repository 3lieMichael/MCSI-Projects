using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using SendGrid;
using SendGrid.Helpers.Mail;
using Web.SP.Models;

namespace Web.SP.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        [HttpPost]
        public ActionResult Contact(ContactFormModel contactForm)
        {
            ViewBag.Message = "Your contact page.";
            SendEmail(contactForm);
            return View("index");
        }
        static async Task SendEmail(ContactFormModel contactForm)
        {
            var apiKey = ConfigurationManager.AppSettings["sendGridAPIKey"];
            var defaultMail = ConfigurationManager.AppSettings["defaultMail"];
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress(contactForm.Email, contactForm.Name);
            var subject = contactForm.Subject;
            var to = new EmailAddress(defaultMail, "IESD Web Contact");
            var plainTextContent = contactForm.Message;
            var htmlContent = "<strong>" + contactForm.Message + "</strong>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);
        }
    }
}