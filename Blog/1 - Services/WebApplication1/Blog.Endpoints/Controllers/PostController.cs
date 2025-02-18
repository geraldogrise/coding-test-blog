using Blog.Application.Interfaces;
using Blog.Application.ViewModels;
using Blog.Data.Core.Repository;
using Blog.Domain.Aggreagates.Posts;
using Blog.Domain.Notifications;
using Blog.Endpoints.Controllers.Core;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Endpoints.Controllers
{

    [Route("api/posts")]
    [ApiController]
    public class PostController : BaseController
    {
        private readonly IPostAppService _postAppService;

        public PostController(INotificationHandler<DomainNotification> notifications,
                                               IMediator mediator,
                                               IPostAppService postAppService) : base(notifications, mediator)
        {
            _postAppService = postAppService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                return Response(_postAppService.GetAll());
            }
            catch (Exception ex)
            {
                RaiseError(ex.Message);
                return Response(null, ex.Message, null);
            }

        }

        [HttpGet("all")]
        public IActionResult GetPostsl()
        {
            try
            {
                return Response(_postAppService.GetPosts());
            }
            catch (Exception ex)
            {
                RaiseError(ex.Message);
                return Response(null, ex.Message, null);
            }

        }

        [Authorize]
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {

            try
            {
                var post = _postAppService.GetById(id);
                if (post == null)
                    return NotFound(new { success = false, message = "Post not found" });
                return Response(post);
            }
            catch (Exception ex)
            {
                RaiseError(ex.Message);
                return Response(null, ex.Message, null);
            }
        }


        [Authorize]
        [HttpPost]
        public IActionResult Create([FromBody] PostModel postModel)
        {
            try
            {
                var post = _postAppService.Insert(postModel);
                return Ok(new { success = true, message = "Post inserted successfully", data = post });

            }
            catch (Exception ex)
            {
                RaiseError(ex.Message);
                return Response(null, ex.Message, null);
            }
        }

        [Authorize]
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] PostModel postModel)
        {
            try
            {
                var updatedPost = _postAppService.Update(id, postModel);
                if (updatedPost == null)
                    return NotFound(new { success = false, message = "Post not found" });
                return Ok(new { success = true, message = "Post updated successfully", data = updatedPost });
            }
            catch (Exception ex)
            {
                RaiseError(ex.Message);
                return Response(null, ex.Message, null);
            }
        }

        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var success = _postAppService.Delete(id);
                if (!success)
                    return NotFound(new { success = false, message = "Post not found" });
                return Ok(new { success = true, message = "Post deleted successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }
    }
}
