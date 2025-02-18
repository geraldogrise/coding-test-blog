using Blog.Domain.Aggreagates.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Domain.Aggreagates.Posts.Services
{
    public interface IPostService : IDisposable
    {
        public Post Insert(Post post);

        public Post Update(int id, Post post);

        public bool Delete(int id);

        public Post GetById(int id);

        public List<Post> GetAll();

        public List<Post> GetByUser(int id_user);
    }
}
