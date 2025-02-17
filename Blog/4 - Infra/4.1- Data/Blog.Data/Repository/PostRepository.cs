using Azure;
using Blog.Data.Context;
using Blog.Data.Core.Repository;
using Blog.Domain.Aggreagates.Posts;
using Blog.Domain.Aggreagates.Posts.Repository;

namespace Blog.Data.Repository
{
    public class PostRepository : Repository<Post>, IPostRepository
    {
        public PostRepository(DatabaseContext context) : base(context)
        {

        }

        public List<Post> GetByUser(int id_user)
        {
           return _context.Posts.Where(x => x.Id_user == id_user).ToList();
        }
    }
}
