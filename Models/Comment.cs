using backend.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Models
{
    public class Comment
    {
        public int id { get; set; }
        public string comment { get; set; }
        public int count { get; set; }

        CommentViewModel ToViewModel(Comment entity)
        {
            if (entity == null)
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
