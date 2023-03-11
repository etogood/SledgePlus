using AutoMapper;
using SledgePlus.Data.Models;

namespace SledgePlus.WPF.Models.DTOs;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<User, UserDTO>();
        CreateMap<UserDTO, User>();

        CreateMap<Role, RoleDTO>();
        CreateMap<RoleDTO, Role>();

        CreateMap<Group, GroupDTO>();
        CreateMap<GroupDTO, Group>();

        CreateMap<Lesson, LessonDTO>();
        CreateMap<LessonDTO, Lesson>();

        CreateMap<Section, SectionDTO>();
        CreateMap<SectionDTO, Section>();

        CreateMap<SectionLesson, SectionLessonDTO>();
        CreateMap<SectionLessonDTO, SectionLesson>();
    }
}