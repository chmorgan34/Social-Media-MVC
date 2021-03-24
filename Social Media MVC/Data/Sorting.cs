using Social_Media_MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Social_Media_MVC.Data
{
    public static class Sorting
    {
        public static double HotScore(Entry entry, long ticksNow)
        {
            return entry.VoteScore / (ticksNow - entry.DateCreated.Ticks);
        }

        public static double ControversyScore(Entry entry)
        {
            if (entry.Upvotes <= 0 || entry.Downvotes <= 0)
            {
                return 0;
            }

            int magnitude = entry.Upvotes + entry.Downvotes;
            double balance;
            if (entry.Upvotes > entry.Downvotes)
            {
                balance = (double)entry.Downvotes / entry.Upvotes;
            }
            else
            {
                balance = (double)entry.Upvotes / entry.Downvotes;
            }

            return Math.Pow(magnitude, balance);
        }
    }
}
