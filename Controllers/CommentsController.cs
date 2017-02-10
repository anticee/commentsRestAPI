using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using backend.Services;
using backend.Models;
using backend.ViewModels;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    public class CommentsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly CommentService _commentService;

        public CommentsController(ApplicationDbContext context, CommentService commentService)
        {
            _commentService = commentService;
            _context = context;
        }

        // GET api/values
        [HttpGet]
        public async Task<IActionResult> GetComments()
        {
            return Ok(await _commentService.GetAllAsync());
        }

        // GET api/values/5
        [HttpGet("{id}", Name = "GetCommentById")]
        public async Task<IActionResult> GetCommentById(int id)
        {
            return Ok(await _commentService.GetByIdAsync(id));
        }

        // POST api/values
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]CommentViewModel model)
        {
            var comment = await _commentService.CreateAsync(model);
            return CreatedAtRoute("GetCommentById", new { id = comment.id}, comment);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]CommentViewModel model)
        {
            return Ok(await _commentService.UpdateAsync(id, model));
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await _commentService.DeleteAsync(id));
        }
    }
}
