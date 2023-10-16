using AutoMapper;
using Etiqa.Domain.ApiModels;
using Etiqa.Services.Contract;
using Microsoft.AspNetCore.Mvc;
using dm = Etiqa.Domain.DataModels;

namespace Etiqa.Web.Controllers
{
    public class UserController : ApiControllerBase
    {
        private readonly ILogger<UserController> logger;

        private readonly IUserService userService;

        private readonly IMapper mapper;

        public UserController(
            IUserService userService,
            IMapper mapper,
            ILogger<UserController> logger)
        {
            this.userService = userService;
            this.mapper = mapper;
            this.logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateUserRequest request)
        {
            var user = mapper.Map<dm.User>(request);
            var clientUser = await userService.AddUserAsync(user);
            return CreatedAtRoute(
                nameof(Get),
                routeValues: new { id = user.Id },
                clientUser);
        }

        [HttpGet("{id:int}", Name = "Get")]
        public async Task<IActionResult> Get(int id)
        {
            var errorOrUser = await userService.GetUserAsync(id);

            return errorOrUser.Match(
                user => Ok(user),
                errors => Problem(errors));
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, UpdateUserRequest request)
        {
            var user = mapper.Map<dm.User>(request);
            user.Id = id;
            await userService.UpdateUserAsync(user);
            return Accepted();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await userService.DeleteUserAsync(id);

            return deleted.Match(
                del => Accepted(del),
                errors => Problem(errors));
        }
    }
}