using System.ComponentModel.DataAnnotations;

namespace Etiqa.Domain.ApiModels
{
    public record CreateUserRequest(
        [Required] string Username,
        [Required, EmailAddress] string Email,
        [Required] string PhoneNo,
        [Required] string Hobby,
        IEnumerable<string> UserSkill);
}