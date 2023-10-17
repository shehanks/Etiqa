using AutoMapper;
using Etiqa.Domain.ClientModels;
using Etiqa.Domain.RequestModels;
using cm = Etiqa.Domain.ClientModels;
using dm = Etiqa.Domain.DataModels;

namespace Etiqa.Domain
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<dm.User, cm.User>()
                .ForMember(d => d.Skills, opt => opt.MapFrom(x => x.UserSkills.Select(s => s.Skill)));

            CreateMap<CreateUserRequest, dm.User>().
                ForMember(d => d.UserSkills, opt => opt.MapFrom(s => s.UserSkill.Select(us => new dm.UserSkill() { Skill = us })));

            CreateMap<UpdateUserRequest, dm.User>().
                ForMember(d => d.UserSkills, opt => opt.MapFrom(s => s.UserSkill.Select(us => new dm.UserSkill() { Skill = us })));

            CreateMap<IEnumerable<cm.User>, UserList>().
                ForMember(d => d.Users, opt => opt.MapFrom(s => s));
        }
    }
}
