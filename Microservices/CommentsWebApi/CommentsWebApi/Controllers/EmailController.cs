using System.Threading.Tasks;
using AutoMapper;
using BLL.Interfaces;
using BLL.Models;
using CommentsWebApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CommentsWebApi.Controllers
{

    [Route("api/[controller]/[action]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class EmailController : ControllerBase
    {
        private readonly IEmailSenderService _emailSenderService;
        private readonly ILogger<EmailController> _logger;
        private readonly IMapper _mapper;


        public EmailController(IEmailSenderService emailSenderService, ILogger<EmailController> logger, IMapper mapper)
        {
            _emailSenderService = emailSenderService;
            _logger = logger;
            _mapper = mapper;
        }



        /// <summary>
        ///     Sends email
        /// </summary>
        /// <remarks>
        ///     Sample request
        ///         
        ///         POST api/Email/SendEmail
        ///         
        /// </remarks>
        
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> SendEmail(EmailDTO emailDto)
        {
            try
            {
                await _emailSenderService.SendEmail(_mapper.Map<EmailModel>(emailDto));
                _logger.LogInformation("Email sent!");
                return Ok("Email sent!");
            }
            catch 
            {
                _logger.LogInformation("Email was not sent!");
                return StatusCode(500);
            }
        }

    }
}
