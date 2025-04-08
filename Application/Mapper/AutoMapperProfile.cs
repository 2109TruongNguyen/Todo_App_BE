using Application.Dto.Response;
using AutoMapper;
using Infrastructure.Entities;

namespace Application.Mapper;

public class AutoMapperProfile: Profile
{
    public AutoMapperProfile()
    {
        CreateMap<User, UserResponse>().ReverseMap();
    }
}