using backend.Models;
using backend.Repositories;
using backend.ViewModels;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Services
{
    public class CommentService
    {
        private readonly CommentRepository _commentRepository;
        private ILogger<CommentService> _logger;

        public CommentService(CommentRepository commentRepository, ILoggerFactory loggerFactory)
        {
            _commentRepository = commentRepository;
            _logger = loggerFactory.CreateLogger<CommentService>();
        }

        private List<CommentViewModel> ToViewModels(List<Comment> commentEntities)
        {
            return commentEntities.Select(_commentRepository.ToViewModel).ToList();
        }

        public async Task<List<CommentViewModel>> GetAllAsync()
        {
            return ToViewModels(await _commentRepository.GetAllAsync());
        }

        public async Task<CommentViewModel> GetByIdAsync(int id)
        {
            return _commentRepository.ToViewModel(await _commentRepository.GetByIdAsync(id));
        }

        public async Task<CommentViewModel> CreateAsync(CommentViewModel commentViewModel)
        {
            var comment = new Comment
            {
                comment = commentViewModel.comment,
                count = commentViewModel.count
            };
            return _commentRepository.ToViewModel(await _commentRepository.CreateAsync(comment));
        }

        public async Task<CommentViewModel> UpdateAsync(int id, CommentViewModel commentViewModel)
        {
            var comment = await _commentRepository.GetByIdAsync(id);
            if (comment == null)
            {
                _logger.LogError("Comment not found with id: {0}", id);
                throw new ArgumentNullException();
            }

            comment.comment = commentViewModel.comment;
            comment.count = commentViewModel.count;

            await _commentRepository.SaveChangesAsync();
            return _commentRepository.ToViewModel(comment);
        }

        public async Task<CommentViewModel> DeleteAsync(int id)
        {
            var comment = await _commentRepository.GetByIdAsync(id);
            if (comment == null)
            {
                _logger.LogError("Comment not found with id: {0}", id);
                throw new ArgumentNullException();
            }

            return _commentRepository.ToViewModel(await _commentRepository.DeleteAsync(comment.id));
        }
    }
}