using System;
using System.Threading.Tasks;
using AutoMapper;
using BLL.Interfaces;
using BLL.Models;
using BLL.Security;
using DAL.Entities;
using DAL.Exceptions;
using DAL.Interfaces;

namespace BLL.Services
{
    public sealed class CommentService : ICommentService
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public CommentService(ICommentRepository commentRepository, IUserRepository userRepository, IMapper mapper)
        {
            _commentRepository = commentRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }


        public async Task Add(CommentModel comment)
        {

            if (!comment.DateTime.HasValue)
            {
                comment.DateTime = DateTime.Now;
                comment.DateTime?.ToString("MM/dd/yyyy HH:mm");
            }

            var mappedComment = _mapper.Map<Comment>(comment);
            var dbUser = await _userRepository.GetUserAsync(mappedComment.User);


            if (dbUser != null
                && PasswordHasher.VerifyPassword(mappedComment.User.Password, dbUser.Password)
                && mappedComment.User.Username == dbUser.Username)
            {
                mappedComment.User.Password = dbUser.Password;
                await _commentRepository.AddAsync(mappedComment);
            }
            else
            {
                throw new InvalidUserDataException();
            }

        }


        public async Task<object> GetByDogId(int dogID)
        {
            return await _commentRepository.GetByDogIdAsync(dogID);
        }


        public async Task<CommentModel> Delete(int commentId)
        {
            return _mapper.Map<CommentModel>(await _commentRepository.DeleteAsync(commentId));
        }


        public async Task<CommentModel> GetByCommentId(int commentId)
        {
            return _mapper.Map<CommentModel>(await _commentRepository.GetByCommentIdAsync(commentId));
        }

        public async Task Upvote(int commentID)
        {
            await _commentRepository.Upvote(commentID);
        }
            
        public async Task Downvote(int commentID)
        {
            await _commentRepository.Downvote(commentID);
        }
    }
}
