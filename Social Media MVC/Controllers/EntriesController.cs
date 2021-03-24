using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Social_Media_MVC.Data;
using Social_Media_MVC.Models;
using Social_Media_MVC.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Social_Media_MVC.Controllers
{
    public class EntriesController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;

        public EntriesController(ApplicationDbContext context, UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            this.context = context;
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Reply(int id, string content)
        {
            var user = await userManager.GetUserAsync(User);
            var entryToReply = await context.Entries
                .Include(e => (e as Comment).Post)
                .SingleAsync(e => e.Id == id);

            var comment = new Comment
            {
                Content = content,
                Author = user,
                RepliedTo = entryToReply
            };
            comment.UpvotedBy.Add(user);

            if (entryToReply is Post)
            {
                comment.Post = entryToReply as Post;
            }
            else if (entryToReply is Comment)
            {
                comment.Post = (entryToReply as Comment).Post;
            }

            entryToReply.Replies.Add(comment);
            context.SaveChanges();

            return Ok(comment.Id);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Edit(int id, string content)
        {
            var user = await userManager.GetUserAsync(User);
            var entry = await context.Entries
                .Include(e => e.Author)
                .SingleAsync(e => e.Id == id);

            if (entry.Author != user)
            {
                if (!User.IsInRole("admin"))
                {
                    return Unauthorized();
                }
            }

            entry.Content = content;
            context.SaveChanges();

            return Ok();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var user = await userManager.GetUserAsync(User);
            var entry = await context.Entries
                .Include(e => e.Author)
                .Include(e => e.SavedBy)
                .Include(e => e.HiddenBy)
                .Include(e => e.UpvotedBy)
                .Include(e => e.DownvotedBy)
                .Include(e => (e as Comment).RepliedTo)
                .Include(e => (e as Comment).Post)
                .SingleAsync(e => e.Id == id);

            if (entry.Author != user)
            {
                if (!User.IsInRole("admin"))
                {
                    return Unauthorized();
                }
            }

            await context.DeleteReplyTree(entry);
            context.Remove(entry);
            context.SaveChanges();

            return Ok();
        }

        public async Task<IActionResult> Details(int id, bool showHidden)
        {
            return View(await GetDetailsViewModel(id, showHidden));
        }
        public async Task<IActionResult> DetailsPartial(int id, bool showHidden)
        {
            return PartialView("Details", await GetDetailsViewModel(id, showHidden));
        }
        private async Task<DetailsViewModel> GetDetailsViewModel(int id, bool showHidden)
        {
            var selected = await context.Entries
                .Include(e => e.Author)
                .Include(e => e.SavedBy)
                .Include(e => e.HiddenBy)
                .Include(e => e.UpvotedBy)
                .Include(e => e.DownvotedBy)
                .Include(e => (e as Comment).RepliedTo)
                .Include(e => (e as Comment).Post)
                .Include(e => (e as Comment).Post.Author)
                .Include(e => (e as Comment).Post.SavedBy)
                .Include(e => (e as Comment).Post.HiddenBy)
                .Include(e => (e as Comment).Post.UpvotedBy)
                .Include(e => (e as Comment).Post.DownvotedBy)
                .SingleAsync(e => e.Id == id);

            Post post = null;
            Comment comment = null;

            if (selected is Post)
            {
                post = selected as Post;
            }
            else if (selected is Comment)
            {
                comment = (selected as Comment);
                post = comment.Post;
            }

            if (showHidden)
            {
                post.Replies = await context.GetReplyTree(post);
            }
            else
            {
                if (signInManager.IsSignedIn(User))
                {
                    var user = await userManager.GetUserAsync(User);
                    post.Replies = await context.GetUnhiddenReplyTree(post, user);
                }
                else
                {
                    post.Replies = await context.GetReplyTree(post);
                }
            }

            return new DetailsViewModel
            {
                Post = post,
                Comment = comment
            };
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Upvote(int id)
        {
            var user = await userManager.GetUserAsync(User);
            var entry = await context.Entries
                .Include(e => e.UpvotedBy)
                .SingleAsync(e => e.Id == id);

            entry.Upvotes += 1;
            entry.VoteScore += 1;
            entry.UpvotedBy.Add(user);
            context.SaveChanges();

            return Ok();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> RemoveUpvote(int id)
        {
            var user = await userManager.GetUserAsync(User);
            var entry = await context.Entries
                .Include(e => e.UpvotedBy)
                .SingleAsync(e => e.Id == id);

            entry.Upvotes -= 1;
            entry.VoteScore -= 1;
            entry.UpvotedBy.Remove(user);
            context.SaveChanges();

            return Ok();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Downvote(int id)
        {
            var user = await userManager.GetUserAsync(User);
            var entry = await context.Entries
                .Include(e => e.DownvotedBy)
                .SingleAsync(e => e.Id == id);

            entry.Downvotes += 1;
            entry.VoteScore -= 1;
            entry.DownvotedBy.Add(user);
            context.SaveChanges();

            return Ok();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> RemoveDownvote(int id)
        {
            var user = await userManager.GetUserAsync(User);
            var entry = await context.Entries
                .Include(e => e.DownvotedBy)
                .SingleAsync(e => e.Id == id);

            entry.Downvotes -= 1;
            entry.VoteScore += 1;
            entry.DownvotedBy.Remove(user);
            context.SaveChanges();

            return Ok();
        }
    }
}
