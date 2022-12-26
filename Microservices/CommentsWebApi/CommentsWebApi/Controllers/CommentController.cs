using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using Microsoft.AspNetCore.Http;
using BLL.Interfaces;
using CommentsWebApi.DTOs;
using AutoMapper;
using BLL.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace CommentsWebApi.Controllers
{

    [Route("api/[controller]/[action]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _commentService;
        private readonly ILogger<CommentController> _logger;
        private readonly IMapper _mapper;


        public CommentController(ICommentService commentService, ILogger<CommentController> logger, IMapper mapper, IEmailSenderService emailSenderService)
        {
            _commentService = commentService;
            _logger = logger;
            _mapper = mapper;
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize]
        public async Task<IActionResult> AddComment([FromBody] CommentDTO commentDto)
        {
            await _commentService.Add(_mapper.Map<CommentModel>(commentDto));
            _logger.LogInformation("Comment added!");
            return Ok();
        }



        [HttpGet("{dogID}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetComments(int dogID)
        {
            var comments = await _commentService.GetByDogId(dogID);
            _logger.LogInformation("Comments received!");
            return Ok(comments);
        }


        [HttpDelete("{commentID}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteComment(int commentID)
        {
            var deletedComment = await _commentService.Delete(commentID);
            _logger.LogInformation("Comment deleted");
            return Ok();
        }


        [HttpGet("{commentID}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize]
        public async Task<IActionResult> UpvoteComment(int commentID)
        {
            await _commentService.Upvote(commentID);
            _logger.LogInformation("Upvoted!");
            return Ok();
        }


        [HttpGet("{commentID}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize]
        public async Task<IActionResult> DownvoteComment(int commentID)
        {
            await _commentService.Downvote(commentID);
            _logger.LogInformation("Downvoted!");
            return Ok();
        }
    }
}
