using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using RstateAPI.Data;
using RstateAPI.Entities.ContactDetails;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
namespace RstateAPI.Controllers.EmailManager {
    [ApiController]
    [Route ("api/[controller]")]
    public class EmailServicesController : ControllerBase {
        private readonly IEmailSender _emailSender;
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly StoreContext store;
        string accountSid = "b4eb9d538d974a1600066fb25940ab66";
        string authToken = "b4eb9d538d974a1600066fb25940ab66";


        public EmailServicesController (StoreContext context, IEmailSender emailSender, IWebHostEnvironment hostingEnvironment) {
            store = context;
            TwilioClient.Init(accountSid, authToken);
            _emailSender = emailSender;
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpPost ("SendMail")]
        public async Task<ActionResult> SendMail (EmaiBody emailBody) {
            var user=store.appuser.Where(b=>b.Email==emailBody.useremail).FirstOrDefault();
            if(user!=null)
            return Ok("exist");

            await _emailSender.SendEmailAsync (emailBody.useremail, emailBody.owneremail, emailBody.mailBody);
            return Ok ();
        }
         [HttpPost ("wam")]
        public ActionResult SendWAM () {
             var message = MessageResource.Create(
            body: "Hi there!",
            from: new Twilio.Types.PhoneNumber("9304622361"),
            to: new Twilio.Types.PhoneNumber("9304622361")
        );
            return Ok (message);
        }

        [HttpPost ("{userId}")]
        public async Task<IActionResult> ConfirmedEmail (string userId) {
            var user = await store.appuser.FindAsync (userId);
            if (user == null) {
                return Ok (200);
            } else {
                user.EmailConfirmed = true;
                store.appuser.Update (user);
                await store.SaveChangesAsync ();

                return Ok (200);
            }
        }
    }
}