using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Etiqa.Web.Controllers
{
    public class ErrorController : ControllerBase
    {
        private readonly ILogger<UserController> logger;

        public ErrorController(ILogger<UserController> logger)
        {
            this.logger = logger;
        }

        [HttpGet]
        [HttpPost]
        [HttpPut]
        [HttpDelete]
        [Route("/error")]
        public IActionResult Error()
        {
            var context = HttpContext.Features.Get<IExceptionHandlerFeature>();
            var exception = context?.Error;

            logger.Log(LogLevel.Error, exception?.ToString());

            var problemDetails = new ProblemDetails
            {
                Status = StatusCodes.Status500InternalServerError,
                Title = "An unexpected error occurred. Check the logs for more details.",
                Detail = exception?.Message
            };

            return StatusCode(StatusCodes.Status500InternalServerError, problemDetails);
        }
    }
}
