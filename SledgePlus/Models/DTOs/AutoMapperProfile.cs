using AutoMapper;
using SledgePlus.Data.Models;

namespace SledgePlus.WPF.Models.DTOs;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<User, UserDTO>();
        CreateMap<UserDTO, User>();
    }
}