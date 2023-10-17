namespace Etiqa.Domain.DataModels
{
    public class UserSkill : EntityBase
    {
        public long UserId { get; set; }
        public string? Skill { get; set; }
        public User User { get; set; } = null!;
    }
}
