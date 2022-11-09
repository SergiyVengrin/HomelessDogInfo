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

namespace CommentsWebApi.Controllers.V2
{

    [Route("api/v{version:apiVersion}/[controller]/[action]")]
    [ApiController]
    [ApiVersion("2.0")]
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


        /// <summary>
        ///     Add Comment to db
        /// </summary>
        /// 
        /// <remarks>
        ///     Sample request
        /// 
        ///         POST /api/Comment/AddComment
        ///     
        /// </remarks>
        /// 
        /// <returns> Added comment to db </returns>

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
                    _logger.LogWarning("Comment NOT added! Internal error " + ex.Message);
                    //return StatusCode(500, ex.Message);
                    throw new Exception(ex.Message);
                }

                _logger.LogInformation("Comment added!");
                return Ok();
            }

            return BadRequest();
        }



        /// <summary>
        ///     Get All comments by articleId
        /// </summary>
        /// 
        /// <remarks>
        ///     Sample request
        ///     
        ///         GET /api/Comment/GetComments/articleId
        /// 
        /// </remarks>
        /// 
        /// <returns> All comments with the desired articleId </returns>

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



        /// <summary>
        ///     Delete comment from db by commentId
        /// </summary>
        /// 
        /// <remarks>
        ///     Sample request
        ///     
        ///         DELETE /api/Comment/DeleteComment/commentId
        /// 
        /// </remarks>
        /// <returns> Deletes comment from db </returns>

       


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
            catch (Exception ex)
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

