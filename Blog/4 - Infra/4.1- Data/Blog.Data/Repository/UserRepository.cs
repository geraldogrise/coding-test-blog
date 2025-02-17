using Blog.Data.Context;
using Blog.Data.Core.Repository;
using Blog.Domain.Aggreagates.Users;
using Blog.Domain.Aggreagates.Users.Repository;


namespace Blog.Data.Repository
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(DatabaseContext context) : base(context)
        {

        }

        public User Signin(Login login)
        {
            return _context.Users.Where(x => x.Login.Equals(login.Username) && x.Password.Equals(login.Password)).FirstOrDefault();
        }
    }
}
