using AutoMapper;

namespace RegistrationPortal.Services;

public class UserService : IUserService
{
    private readonly IUserData _userData;
    private readonly IMapper _mapper;

    public UserService(IUserData userData, IMapper mapper)
    {
        _userData = userData;
        _mapper = mapper;
    }

    //public async Task<List<UserDto>> GetUsers()
    //{
    //    var users = await _userData.GetUsers();
    //    return _mapper.Map<List<UserDto>>(users);
    //}

    public async Task<UserDto> GetUser(string id)
    {
        var user = await _userData.GetUser(id);
        return _mapper.Map<UserDto>(user);
    }

    public async Task<UserDto> GetUserFromAuthentication(string objectId)
    {
        var user =  await _userData.GetUserFromAuthentication(objectId);
        return _mapper.Map<UserDto>(user);
    }

    public async Task CreateUser(UserDto userDto)
    {
        var user = _mapper.Map<User>(userDto);
        await _userData.CreateUser(user);
    }

    public async Task UpdateUser(UserDto userDto)
    {
        var user = _mapper.Map<User>(userDto);
        await _userData.UpdateUser(user);
    }
}
