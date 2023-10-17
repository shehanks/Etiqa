using ErrorOr;
using Microsoft.AspNetCore.Mvc;

namespace Etiqa.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ApiControllerBase : ControllerBase
    {
        protected IActionResult Problem(List<Error> errors)
        {
            var firstError = errors.First();

            var statusCode = firstError.Type switch
            {
                ErrorType.NotFound => StatusCodes.Status404NotFound,
                ErrorType.Validation => StatusCodes.Status400BadRequest,
                ErrorType.Conflict => StatusCodes.Status409Conflict,
                _ => StatusCodes.Status500InternalServerError
            };

            return Problem(
                statusCode: statusCode,
                title: firstError.Description);
        }
    }
}
