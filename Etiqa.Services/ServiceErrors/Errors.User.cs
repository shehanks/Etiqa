using ErrorOr;

namespace Etiqa.Services.ServiceErrors
{
    public static class Errors
    {
        public static class User
        {
            public static Error NotFound => Error.NotFound(
                code: "User.NotFound",
                description: "User not found");
        }
    }
}
