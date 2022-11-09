using System;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CommentsWebApi.Controllers
{

    [ApiController]
    public class ErrorController : Controller
    {
        //[NonAction]
        //[Route("/Error-Development")]
        //public  ErrorDevelopment(IWebHostEnvironment webHostEnvironment)
        //{
        //    if (webHostEnvironment.EnvironmentName != "Development")
        //    {
        //        throw new InvalidOperationException("This should not be in /Error-Development");
        //    }

        //    var context = HttpContext.Features.Get<IExceptionHandlerFeature>();

        //    return 
        //}


        [NonAction]
        [Route("/Error")]
        public IActionResult Error()
        {
            var context = HttpContext.Features.Get<IExceptionHandlerPathFeature>();

            if (context.Error is null)
            {
                throw new InvalidOperationException("You should not be here!");
            }

            return Problem(title: context.Error.Message);

        }

        
    }
}
