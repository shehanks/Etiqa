using System.ComponentModel.DataAnnotations.Schema;

namespace Etiqa.Domain.DataModels
{
    [Table("User")]
    public class User : EntityBase
    {
        public string? Username { get; set; }
        public string? Email { get; set; }
        public string? PhoneNo { get; set; }
        public string? Hobby { get; set; }

        public ICollection<UserSkill> UserSkills { get; set; } = new List<UserSkill>();
    }
}
