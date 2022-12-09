using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using Microsoft.AspNetCore.Http;
using BLL.Interfaces;
using CommentsWebApi.Models;
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
            if (ModelState.IsValid)
            {
                try
                {
                    await _commentService.Add(_mapper.Map<CommentModel>(commentDto));
                }
                catch (Exception ex)
                {
                    _logger.LogError("Comment NOT added! Internal error " + ex.Message);
                    throw new Exception(ex.Message);
                }

                _logger.LogInformation("Comment added!");
                return Ok();
            }

            return BadRequest();
        }


        [HttpGet("{dogID}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetComments(int dogID)
        {
            var comments = await _commentService.GetByDogId(dogID);

            if (comments == null)
            {
                _logger.LogInformation($"There are no comments with dogID: {dogID}!");
                return NotFound();
            }

            _logger.LogInformation("Comments received!");
            return Ok(comments);
        }


        [HttpDelete("{commentID}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> DeleteComment(int commentID)
        {
            try
            {
                var deletedComment = await _commentService.Delete(commentID);

                if (deletedComment != null)
                {
                    _logger.LogInformation("Comment deleted!");
                    return Ok();
                }
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Comment NOT deleted!");
                return BadRequest(ex.Message);
            }

            _logger.LogWarning("Comment NOT deleted!!!");
            return BadRequest();
        }


        [HttpGet("{commentID}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize]
        public async Task<IActionResult> UpvoteComment(int commentID)
        {
            try
            {
                await _commentService.Upvote(commentID);
            }
            catch (Exception ex )
            {
                _logger.LogWarning("Comment Not Found!");
                return BadRequest(ex.Message);
            }

            _logger.LogInformation("Upvoted!");
            return Ok();
        }


        [HttpGet("{commentID}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize]
        public async Task<IActionResult> DownvoteComment(int commentID)
        {
            try
            {
                await _commentService.Downvote(commentID);
            }
            catch (Exception ex)
            {
                _logger.LogWarning("Comment Not Found!");
                return BadRequest(ex.Message);
            }

            _logger.LogInformation("Downvoted!");
            return Ok();
        }
    }
}
