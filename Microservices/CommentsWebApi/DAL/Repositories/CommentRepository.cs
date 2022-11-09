using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.Data;
using DAL.Entities;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public sealed class CommentRepository : ICommentRepository
    {
        private readonly CommentContext _db = new CommentContext();


        public async Task AddAsync(Comment comment)
        {

            try
            {
                _db.Update(comment);
                await _db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public async Task<Comment> DeleteAsync(int commentID)
        {
            var comment = await _db.Comments.Where(c => c.CommentID == commentID).SingleAsync();
            try
            {
                if (comment != null)
                {
                    _db.Comments.Remove(comment);
                    _db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return comment;
        }


        public async Task<object> GetByDogIdAsync(int dogID)
        {
            return await (from c in _db.Comments
                          where c.DogID == dogID
                          select new
                          {
                              CommentID = c.CommentID,
                              DogID = c.DogID,
                              User = new User
                              {
                                  Email = c.User.Email,
                                  Password = c.User.Password,
                                  Username = c.User.Username
                              },
                              Text = c.Text,
                              Rating = c.Rating,
                              VoteCounts = c.VoteCounts,
                              DateTime = c.DateTime
                          }).ToListAsync();
        }


        public async Task<Comment> GetByCommentIdAsync(int commentID)
        {
            return await _db.Comments.Where(c => c.CommentID == commentID).SingleAsync();
        }

        public async Task Upvote(int commentID)
        {
            var comment = await GetByCommentIdAsync(commentID);

            if (comment is null)
            {
                throw new Exception("Comment not found");

            }

            comment.VoteCounts += 1;
            await _db.SaveChangesAsync();
        }

        public async Task Downvote(int commentID)
        {
            var comment = await GetByCommentIdAsync(commentID);

            if (comment is null)
            {
                throw new Exception("Comment not found");

            }

            comment.VoteCounts -= 1;
            await _db.SaveChangesAsync();
        }
    }
}
