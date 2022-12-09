using System;
using System.Threading.Tasks;
using DAL.Entities;
using DAL.Interfaces;
using Microsoft.Extensions.Caching.Memory;

namespace DAL.Repositories
{
    public sealed class CommentRepositoryCachingDecorator : ICommentRepository
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IMemoryCache _memoryCache;

        private const uint CACHE_EXPIRATION_MUNUTES = 10;
        private const uint CACHE_SIZE = 10;
        private MemoryCacheEntryOptions _cacheOptions;

        public CommentRepositoryCachingDecorator(ICommentRepository commentRepository, IMemoryCache memoryCache)
        {
            _commentRepository = commentRepository;
            _memoryCache = memoryCache;
            _cacheOptions = new MemoryCacheEntryOptions()
                .SetPriority(CacheItemPriority.Normal)
                .SetSize(CACHE_SIZE)
                .SetAbsoluteExpiration(TimeSpan.FromMinutes(CACHE_EXPIRATION_MUNUTES));
        }



        public async Task AddAsync(Comment comment)
        {
            await _commentRepository.AddAsync(comment);
            System.Diagnostics.Debug.WriteLine("Add element from cache");
            _memoryCache.Set(comment.CommentID, comment, _cacheOptions);
        }


        public async Task<Comment> DeleteAsync(int commentID)
        {
            Comment deletedComment = await _commentRepository.DeleteAsync(commentID);

            if (deletedComment != null)
            {
                System.Diagnostics.Debug.WriteLine("Delete element from cache");
                _memoryCache.Remove(commentID);
            }

            return deletedComment;
        }


        public async Task<object> GetByDogIdAsync(int dogID) 
        {                                                        
            return await _commentRepository.GetByDogIdAsync(dogID); 
        }


        public async Task<Comment> GetByCommentIdAsync(int commentID)
        {
            Comment comment;

            if (!_memoryCache.TryGetValue(commentID, out comment))
            {
                comment = await _commentRepository.GetByCommentIdAsync(commentID);
                if (comment != null)
                {
                    _memoryCache.Set(commentID, comment, _cacheOptions);
                }
            }

            return comment;
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
