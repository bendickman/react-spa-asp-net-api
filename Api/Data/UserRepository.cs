public class UserRepository : IUserRepository
{
    private List<UserEntity> users = new()
    {
        //password is secret
        new UserEntity(3522, "ben", "K7gNU3sdo+OL0wNhqoVWhr3g6s1xYv72ol/pe/Unols=", "blue", "Admin")
    };

    public UserEntity? GetByUsernameAndPassword(string username, string password)
    {
        var user = users.SingleOrDefault(u =>
        {
            return u.Name == username && u.Password == password.Sha256();
        });
        return user;
    }
}
