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
    public class UsersController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly UserManager<ApplicationUser> userManager;

        public UsersController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }

        [Authorize]
        public async Task<IActionResult> Authored()
        {
            var user = await userManager.GetUserAsync(User);
            var query = context.GetEntriesQuery();
            var entries = await query
                .Where(e => e.Author == user)
                .Where(e => !e.HiddenBy.Contains(user))
                .ToListAsync();

            return View("Entries", new EntriesViewModel
            {
                Entries = entries,
                SignedIn = true,
                CurrentUser = user
            });
        }

        [Authorize]
        public async Task<IActionResult> Saved()
        {
            var user = await userManager.GetUserAsync(User);
            var query = context.GetEntriesQuery();
            var entries = await query
                .Where(e => e.SavedBy.Contains(user))
                .Where(e => !e.HiddenBy.Contains(user))
                .ToListAsync();

            return View("Entries", new EntriesViewModel
            {
                Entries = entries,
                SignedIn = true,
                CurrentUser = user
            });
        }

        [Authorize]
        public async Task<IActionResult> Hidden()
        {
            var user = await userManager.GetUserAsync(User);
            var query = context.GetEntriesQuery();
            var entries = await query
                .Where(e => e.HiddenBy.Contains(user))
                .ToListAsync();

            return View("Entries", new EntriesViewModel
            {
                Entries = entries,
                SignedIn = true,
                CurrentUser = user
            });
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Save(int id)
        {
            var selected = await context.Entries.SingleAsync(p => p.Id == id);
            var user = await userManager.GetUserAsync(User);
            user.SavedEntries.Add(selected);
            context.SaveChanges();

            return Ok();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Unsave(int id)
        {
            var selected = await context.Entries
                .Include(p => p.SavedBy)
                .SingleAsync(p => p.Id == id);

            var user = await userManager.GetUserAsync(User);
            if (selected.SavedBy.Contains(user))
            {
                selected.SavedBy.Remove(user);
                context.SaveChanges();

                return Ok();
            }
            else
            {
                return NotFound();
            }
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Hide(int id)
        {
            var selected = await context.Entries.SingleAsync(p => p.Id == id);
            var user = await userManager.GetUserAsync(User);
            user.HiddenEntries.Add(selected);
            context.SaveChanges();

            return Ok();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Unhide(int id)
        {
            var selected = await context.Entries
                .Include(p => p.HiddenBy)
                .SingleAsync(p => p.Id == id);

            var user = await userManager.GetUserAsync(User);
            if (selected.HiddenBy.Contains(user))
            {
                selected.HiddenBy.Remove(user);
                context.SaveChanges();
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }
    }
}
