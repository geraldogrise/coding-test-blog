
using Blog.Application.ViewModels;
using Blog.Domain.Aggreagates.Users;

namespace Blog.Application.Interfaces
{
    public interface IPostAppService : IDisposable
    {
        public PostModel Insert(PostModel post);
        public PostModel Update(int id, PostModel post);
        public bool Delete(int id);
        public PostModel GetById(int id);
        public List<PostModel> GetByUser(int id_user);
        public List<PostModel> GetAll();
    }
}
