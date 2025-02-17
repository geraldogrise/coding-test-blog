using Blog.Domain.Interfaces;


namespace Blog.Domain.Aggreagates.Users.Repository
{
    public interface IUserRepository : IRepository<User>
    {
        public User Signin(Login login);
    }
}
