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
    public class PostsController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly UserManager<ApplicationUser> userManager;
        public PostsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }

        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(PostViewModel postViewModel)
        {
            if (postViewModel.Title != null && postViewModel.Content != null)
            {
                postViewModel.Title = postViewModel.Title.Trim();
                postViewModel.Content = postViewModel.Content.Trim();
            }

            if (TryValidateModel(postViewModel))
            {
                var user = await userManager.GetUserAsync(User);
                var newPost = new Post
                {
                    Title = postViewModel.Title,
                    Content = postViewModel.Content,
                    Author = user,
                };
                newPost.UpvotedBy.Add(user);

                context.Add(newPost);
                context.SaveChanges();

                return RedirectToAction("Details", "Entries", new { id = newPost.Id, showHidden = false });
            }
            else return View(postViewModel);
        }
    }
}
