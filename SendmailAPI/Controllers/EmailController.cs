using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SendmailAPI.Interfaces;
using SendmailAPI.Models;

namespace SendmailAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly IMailServices _mailServices;

        public EmailController(IMailServices mailServices)
        {
            _mailServices = mailServices;
        }
        [HttpPost("Send mail")]
        public IActionResult Sendmail([FromQuery]EmailReceiver emailReceiver)
        {
            _mailServices.SendEmail(emailReceiver);
            return Ok();
        }
    }
}
