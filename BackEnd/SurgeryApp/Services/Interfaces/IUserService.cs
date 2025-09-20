
public interface IUserService {
    Task<User> CreateUserAsync(CreateUserDto dto);
    Task<IEnumerable<User>> GetAllUsersAsync();
}
