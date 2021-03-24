using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Social_Media_MVC.Data;
using Social_Media_MVC.Models;
using Social_Media_MVC.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Social_Media_MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;

        public HomeController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            this.context = context;
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        public async Task<IActionResult> Index(string sort)
        {
            EntriesViewModel viewModel = new EntriesViewModel();

            IQueryable<Entry> dbQuery;
            if (signInManager.IsSignedIn(User))
            {
                viewModel.SignedIn = true;
                viewModel.CurrentUser = await userManager.GetUserAsync(User);
                dbQuery = context.GetUnhiddenPostsQuery(viewModel.CurrentUser);
            }
            else
            {
                viewModel.SignedIn = false;
                dbQuery = context.GetPostsQuery();
            }

            var ticksNow = DateTime.Now.Ticks;
            if (sort != null) sort = sort.ToLower();
            switch (sort)
            {
                case "hot":
                    viewModel.Sort = EntriesViewModel.SortType.Hot;
                    viewModel.Entries = await dbQuery.ToListAsync();
                    viewModel.Entries.Sort((x, y) => Sorting.HotScore(y, ticksNow).CompareTo(Sorting.HotScore(x, ticksNow)));
                    break;

                case "new":
                    viewModel.Sort = EntriesViewModel.SortType.New;
                    viewModel.Entries = await dbQuery
                        .OrderByDescending(p => p.DateCreated)
                        .ToListAsync();
                    break;

                case "controversial":
                    viewModel.Sort = EntriesViewModel.SortType.Controversial;
                    viewModel.Entries = await dbQuery.ToListAsync();
                    viewModel.Entries.Sort((x, y) => Sorting.ControversyScore(y).CompareTo(Sorting.ControversyScore(x)));
                    break;

                case "top":
                    viewModel.Sort = EntriesViewModel.SortType.Top;
                    viewModel.Entries = await dbQuery
                        .OrderByDescending(p => p.VoteScore)
                        .ToListAsync();
                    break;

                default:
                    viewModel.Sort = EntriesViewModel.SortType.Hot;
                    viewModel.Entries = await dbQuery.ToListAsync();
                    viewModel.Entries.Sort((x, y) => Sorting.HotScore(y, ticksNow).CompareTo(Sorting.HotScore(x, ticksNow)));
                    break;
            }

            return View("Entries", viewModel);
        }

        public async Task<IActionResult> Search(string query)
        {
            var viewModel = new EntriesViewModel();

            IQueryable<Entry> dbQuery;
            if (signInManager.IsSignedIn(User))
            {
                viewModel.SignedIn = true;
                viewModel.CurrentUser = await userManager.GetUserAsync(User);
                dbQuery = context.GetUnhiddenPostsQuery(viewModel.CurrentUser);
            }
            else
            {
                viewModel.SignedIn = false;
                dbQuery = context.GetPostsQuery();
            }

            viewModel.Entries = await dbQuery
                .Where(p => (p as Post).Title.Contains(query))
                .ToListAsync();

            return View("Entries", viewModel);
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
