using MedBook_RazorPages.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using NETCore.MailKit.Core;
using System;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.IO;
using System.Threading.Tasks;

namespace MedBook_RazorPages.Pages
{
    public class signupModel : PageModel
    {
        [BindProperty]
        public Users users { get; set; }

        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IEmailService _emailService;

        private DatabaseContext db;

        private readonly ILogger<signupModel> _logger;

        public signupModel(DatabaseContext _db, ILogger<signupModel> logger) {
            db = _db;
            _logger = logger;
        }

        public void OnGet()
        {
            users = new Users();
            _logger.LogInformation("Se face request de tip get");
        }

        public async Task<IActionResult> OnPost()
        {

            if (users.FirstName != "" && users.LastName != ""  && users.Email != "" && (users.UserType == 1 || users.UserType == 2)) {
                _logger.LogInformation("Adaugarea informatiilor in baza de date");
                Console.WriteLine(users.Email);
                _logger.LogInformation("Se cripteaza parola");


                users.Password = BCrypt.Net.BCrypt.HashPassword(users.Password);
                _logger.LogInformation("Se adauga informatiile culese din pagina");

                db.Users.Add(users);
                db.SaveChanges();

                var ActivationCode = Guid.NewGuid();

                SendVerificationLinkEmail(users.Email, ActivationCode.ToString());

                _logger.LogInformation("Informatiile au fost salvate si se face redirect catre pagina index");
                return RedirectToPage("index");
            }
            return RedirectToPage("signup");
            

        }

        [NonAction]

        public void SendVerificationLinkEmail(string emailID, string activationCode, string emailFor = "VerifyAccount")
        {
            var vefifyUrl = "/User/" + emailFor + "/" + activationCode;
            var link = "Account created"; 


            var fromEmail = new MailAddress("dragosaioane1997@gmail.com", "Validation User");
            var toEmail = new MailAddress(emailID);
            var fromEmailPassword = "Colosal13245.";

            string subject = "";
            string body = "";
            if (emailFor == "VerifyAccount")
            {

                subject = "Your account is succesfullycreated!";

                body = "<br/><br/> We are excited to tell you that your account is " +
                   "succesfully created. Please click on the below link to verify your account" +
                   "<br/><br/><a href='" + link + "'>" + link + "</a>";
            }

            else if (emailFor == "ResetPassword")
            {
                subject = "Reset Password";
                body = "Hi,<br/><br/> We got request for reset your account password. Please click on the below link to reset your password" +
                    "<br/><br/><a href=" + link + ">Reset Password link <a/>";

            }

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromEmail.Address, fromEmailPassword)
            };
            using (var message = new MailMessage(fromEmail, toEmail)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            })

                smtp.Send(message);
        }

    }
}