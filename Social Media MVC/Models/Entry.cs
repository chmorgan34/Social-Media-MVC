using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Social_Media_MVC.Models
{
    public abstract class Entry
    {
        public Entry()
        {
            DateCreated = DateTime.Now;
            Upvotes = 1;
            Downvotes = 0;
            VoteScore = 1;

            Replies = new List<Comment>();
            SavedBy = new List<ApplicationUser>();
            HiddenBy = new List<ApplicationUser>();
            UpvotedBy = new List<ApplicationUser>();
            DownvotedBy = new List<ApplicationUser>();
        }

        public int Id { get; set; }
        public string Content { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime DateCreated { get; set; }

        public int Upvotes { get; set; }
        public int Downvotes { get; set; }
        public int VoteScore { get; set; }

        public ApplicationUser Author { get; set; }
        public ICollection<Comment> Replies { get; set; }
        public ICollection<ApplicationUser> SavedBy { get; set; }
        public ICollection<ApplicationUser> HiddenBy { get; set; }
        public ICollection<ApplicationUser> UpvotedBy { get; set; }
        public ICollection<ApplicationUser> DownvotedBy { get; set; }
    }

    public class Post : Entry
    {
        public string Title { get; set; }
    }

    public class Comment : Entry
    {
        public Entry RepliedTo { get; set; }
        public Post Post { get; set; }
    }
}
