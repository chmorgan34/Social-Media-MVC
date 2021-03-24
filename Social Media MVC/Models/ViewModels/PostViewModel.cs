using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Social_Media_MVC.Models.ViewModels
{
    public class PostViewModel
    {
        [Required, StringLength(300, MinimumLength = 1)]
        public string Title { get; set; }

        [Required, StringLength(40000)]
        public string Content { get; set; }
    }
}
