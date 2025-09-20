
public class UserService : IUserService {
    public Task<User> CreateUserAsync(CreateUserDto dto) {
        // Implementation here
        return Task.FromResult(new User());
    }

    public Task<IEnumerable<User>> GetAllUsersAsync() {
        // Implementation here
        return Task.FromResult(new List<User>().AsEnumerable());
    }
}
