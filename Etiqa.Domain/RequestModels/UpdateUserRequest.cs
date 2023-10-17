using System.ComponentModel.DataAnnotations;

namespace Etiqa.Domain.RequestModels
{
    public record UpdateUserRequest(
        [Required] string Username,
        [Required, EmailAddress] string Email,
        [Required] string PhoneNo,
        string? Hobby,
        IEnumerable<string>? UserSkill);
}