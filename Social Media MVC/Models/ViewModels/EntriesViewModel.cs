using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Social_Media_MVC.Models.ViewModels
{
    public class EntriesViewModel
    {
        public List<Entry> Entries { get; set; }
        public SortType Sort { get; set; }
        public string SearchQuery { get; set; }
        public bool SignedIn { get; set; }
        public ApplicationUser CurrentUser { get; set; }

        public enum SortType
        {
            Hot,
            New,
            Controversial,
            Top
        }
    }
}
