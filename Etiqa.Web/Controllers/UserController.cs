using Etiqa.Domain.ClientModels;
using Microsoft.AspNetCore.Mvc;

namespace Etiqa.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        [HttpPost]
        public IActionResult Create(CreateUserRequest request)
        {
            return Ok(200);
        }

        [HttpGet("{id:int}")]
        public IActionResult Get(int id)
        {
            return Ok(id);
        }

        [HttpPut("{id:int}")]
        public IActionResult Update(int id, UpdateUserRequest request)
        {
            return Ok(id);
        }

        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            return Ok(id);
        }
    }
}