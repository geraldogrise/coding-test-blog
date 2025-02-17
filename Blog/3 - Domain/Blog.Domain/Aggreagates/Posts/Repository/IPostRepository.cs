using Blog.Domain.Aggreagates.Users;
using Blog.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Domain.Aggreagates.Posts.Repository
{

    public interface IPostRepository : IRepository<Post>
    {
        List<Post> GetByUser(int id_user);
    }
}
