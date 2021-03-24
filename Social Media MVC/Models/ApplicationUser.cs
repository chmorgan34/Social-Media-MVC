using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Social_Media_MVC.Models
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            AuthoredEntries = new List<Entry>();
            SavedEntries = new List<Entry>();
            HiddenEntries = new List<Entry>();
            UpvotedEntries = new List<Entry>();
            DownvotedEntries = new List<Entry>();
        }

        [InverseProperty("Author")]
        public ICollection<Entry> AuthoredEntries { get; set; }

        [InverseProperty("SavedBy")]
        public ICollection<Entry> SavedEntries { get; set; }

        [InverseProperty("HiddenBy")]
        public ICollection<Entry> HiddenEntries { get; set; }

        [InverseProperty("UpvotedBy")]
        public ICollection<Entry> UpvotedEntries { get; set; }

        [InverseProperty("DownvotedBy")]
        public ICollection<Entry> DownvotedEntries { get; set; }
    }
}
