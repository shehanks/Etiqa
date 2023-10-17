using System.ComponentModel.DataAnnotations;

namespace Etiqa.Domain.RequestModels
{
    public record UpdateUserRequest(
        [Required][RegularExpression(@"^[a-zA-Z0-9]{3,20}", ErrorMessage = "Incorrect username characters.")] string Username,
        [Required, EmailAddress] string Email,
        [Required] string PhoneNo,
        string? Hobby,
        IEnumerable<string>? UserSkill);
}