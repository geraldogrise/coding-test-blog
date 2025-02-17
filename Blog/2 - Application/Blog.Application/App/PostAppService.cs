using AutoMapper;
using Blog.Application.App.Core;
using Blog.Application.Interfaces;
using Blog.Application.ViewModels;
using Blog.Domain.Aggreagates.Posts;
using Blog.Domain.Aggreagates.Posts.Services;
using Blog.Domain.Notifications;
using MediatR;

namespace Blog.Application.App
{
    public class PostAppService : ApplicationService, IPostAppService
    {
        private readonly IMapper _mapper;

        private readonly IPostService _postService;

        public PostAppService(IMediator mediator,
                              INotificationHandler<DomainNotification> notifications,
                              IMapper mapper,
                              IPostService postService
                                      ) : base(mediator, notifications)
        {
            _mapper = mapper;
            _postService = postService;
        }


        public PostModel Insert(PostModel post)
        {
            var response = _postService.Insert(_mapper.Map<Post>(post));
            return _mapper.Map<PostModel>(response);
        }

        public PostModel Update(int id, PostModel post)
        {
            var response = _postService.Update(_mapper.Map<Post>(post));
            return _mapper.Map<PostModel>(response);
        }

        public bool Delete(int id)
        {
            return _postService.Delete(id);
        }

        public PostModel GetById(int id)
        {
            return _mapper.Map<PostModel>(_postService.GetById(id));
        }

        public List<PostModel> GetByUser(int id_user)
        {
            return _mapper.Map<List<PostModel>>(_postService.GetByUser(id_user));
        }

        public List<PostModel> GetAll()
        {
            return _mapper.Map<List<PostModel>>(_postService.GetAll());
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            _postService.Dispose();
        }

    }
}
