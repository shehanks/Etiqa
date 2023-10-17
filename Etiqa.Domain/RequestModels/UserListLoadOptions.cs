using System.ComponentModel.DataAnnotations;

namespace Etiqa.Domain.RequestModels
{
    public record UserListLoadOptions(
        [Range(1, int.MaxValue)] int page,
        [Range(5, 50)] int pageSize,
        string? email)
        : EntityListLoadOptions(page, pageSize);
}
