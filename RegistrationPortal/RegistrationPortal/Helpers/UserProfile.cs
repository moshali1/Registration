using AutoMapper;

namespace RegistrationPortal.Helpers;

public class UserProfile : Profile 
{
    public UserProfile()
    {
        CreateMap<User, UserDto>();
        CreateMap<UserDto, User>();
    }
}
