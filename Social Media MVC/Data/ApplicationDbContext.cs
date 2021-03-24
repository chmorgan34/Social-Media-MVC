using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Social_Media_MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social_Media_MVC.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Entry> Entries { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }


        public IQueryable<Entry> GetEntriesQuery()
        {
            return Entries
                .Include(e => e.Author)
                .Include(e => e.Replies)
                .Include(e => e.SavedBy)
                .Include(e => e.HiddenBy)
                .Include(e => e.UpvotedBy)
                .Include(e => e.DownvotedBy)
                .Include(e => (e as Comment).Post);
        }

        public IQueryable<Entry> GetPostsQuery()
        {
            return Posts
                    .Include(p => p.Author)
                    .Include(p => p.SavedBy)
                    .Include(p => p.HiddenBy)
                    .Include(p => p.UpvotedBy)
                    .Include(p => p.DownvotedBy)
                    .Include(p => p.Replies);
        }

        public IQueryable<Entry> GetUnhiddenPostsQuery(ApplicationUser user)
        {
            return Posts
                    .Include(p => p.Author)
                    .Include(p => p.SavedBy)
                    .Include(p => p.HiddenBy)
                    .Include(p => p.UpvotedBy)
                    .Include(p => p.DownvotedBy)
                    .Include(p => p.Replies)
                    .Where(p => !p.HiddenBy.Contains(user));
        }

        public async Task<List<Comment>> GetReplyTree(Entry entry)
        {
            var replies = await Comments
                .Include(r => r.Author)
                .Include(r => r.SavedBy)
                .Include(r => r.HiddenBy)
                .Include(r => r.UpvotedBy)
                .Include(r => r.DownvotedBy)
                .Include(r => r.Post)
                .Include(r => r.RepliedTo)
                .Where(r => r.RepliedTo == entry)
                .ToListAsync();

            if (replies.Count > 0)
            {
                foreach (var reply in replies)
                {
                    reply.Replies = await GetReplyTree(reply);
                }
            }

            return replies;
        }

        public async Task<List<Comment>> GetUnhiddenReplyTree(Entry entry, ApplicationUser user)
        {
            var replies = await Comments
                .Include(r => r.Author)
                .Include(r => r.SavedBy)
                .Include(r => r.HiddenBy)
                .Include(r => r.UpvotedBy)
                .Include(r => r.DownvotedBy)
                .Include(r => r.Post)
                .Include(r => r.RepliedTo)
                .Where(r => !r.HiddenBy.Contains(user))
                .Where(r => r.RepliedTo == entry)
                .ToListAsync();

            if (replies.Count > 0)
            {
                foreach (var reply in replies)
                {
                    reply.Replies = await GetUnhiddenReplyTree(reply, user);
                }
            }

            return replies;
        }

        public async Task DeleteReplyTree(Entry entry)
        {
            var replies = await Comments
                .Include(r => r.Author)
                .Include(r => r.SavedBy)
                .Include(r => r.HiddenBy)
                .Include(r => r.UpvotedBy)
                .Include(r => r.DownvotedBy)
                .Include(r => r.Post)
                .Include(r => r.RepliedTo)
                .Where(r => r.RepliedTo == entry)
                .ToListAsync();

            if (replies.Count > 0)
            {
                foreach (var reply in replies)
                {
                    await DeleteReplyTree(reply);
                }
            }

            RemoveRange(replies);
        }
    }
}
