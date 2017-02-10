using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Models;
using backend.ViewModels;
using backend.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace backend.Repositories
{
    public class CommentRepository : IRepository<int, Comment, CommentViewModel>
    {
        private readonly ApplicationDbContext _context;

        public CommentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Comment>> GetAllAsync()
        {
            return await _context.Comments.ToListAsync();
        }

        public async Task<Comment> GetByIdAsync(int id)
        {
            return await _context.Comments.Where(x => x.id == id).FirstOrDefaultAsync();
        }

        public async Task<Comment> CreateAsync(Comment entity)
        {
            _context.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Comment> DeleteAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            _context.Remove(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public CommentViewModel ToViewModel(Comment entity)
        {
            if(entity == null)
            {
                return null;
            }

            return new CommentViewModel
            {
                id = entity.id,
                comment = entity.comment,
                count = entity.count
            };
        }
    }
}
