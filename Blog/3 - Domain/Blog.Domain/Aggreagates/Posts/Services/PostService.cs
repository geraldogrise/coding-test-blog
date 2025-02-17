using Blog.Domain.Aggreagates.Posts.Repository;
using Blog.Domain.Aggreagates.Posts;
using Blog.Domain.Aggreagates.Posts.Services;
using Blog.Domain.Services;
using MediatR;
using Blog.Domain.Aggreagates.Users;


namespace Blog.Domain.Aggreagates.Posts.Services
{
    public class PostService : Service, IPostService
    {
        private readonly IPostRepository _postRepository;

        public PostService(IMediator mediator,
                             IPostRepository postRepository
                             ) : base(mediator)
        {
            _postRepository = postRepository;
        }

        public Post Insert(Post post)
        {
            _postRepository.Add(post);
            _postRepository.SaveChanges();
            return post;
        }


        public Post Update(Post post)
        {
            _postRepository.Add(post);
            _postRepository.SaveChanges();
            return post;
        }

        public bool Delete(int id)
        {
            _postRepository.Remove(GetById(id));
            _postRepository.SaveChanges();
            return true;
        }

        public Post GetById(int id)
        {
            return _postRepository.Find(x => x.Id == id).FirstOrDefault();
        }

        public List<Post> GetByUser(int id_user)
        {
            return _postRepository.GetByUser(id_user).ToList();
        }

        public List<Post> GetAll()
        {
            return _postRepository.GetAll().ToList();
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            _postRepository.Dispose();
        }

    }
}
