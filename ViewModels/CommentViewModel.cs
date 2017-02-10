using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.ViewModels
{
    public class CommentViewModel
    {
        public int id { get; set; }
        public string comment { get; set; }
        public int count { get; set; }
    }
}
