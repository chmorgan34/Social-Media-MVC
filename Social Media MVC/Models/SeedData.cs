using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Social_Media_MVC.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Social_Media_MVC.Models
{
    public class SeedData
    {
        public static async Task Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ApplicationDbContext(serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
            {
                if (context.Entries.Any())
                {
                    return;
                }

                // Admin
                var userManager = serviceProvider.GetService<UserManager<ApplicationUser>>();
                var roleManager = serviceProvider.GetService<RoleManager<IdentityRole>>();
                var admin = new ApplicationUser
                {
                    UserName = "chmorgan"
                };
                await userManager.CreateAsync(admin, "testing123");
                await roleManager.CreateAsync(new IdentityRole("admin"));
                await userManager.AddToRoleAsync(admin, "admin");


                var rand = new Random();
                var randGen = new RandomGenerator();

                // Max 20 users
                var users = new List<ApplicationUser>();
                for (int i = 0; i < 20; i++)
                {
                    // Ensure unique UserNames
                    string name;
                    do
                    {
                        name = randGen.Name();
                    } while (users.Any(u => u.UserName == name));

                    users.Add(new ApplicationUser
                    {
                        UserName = name
                    });
                }

                // 50 posts
                var posts = new List<Post>();
                for (int i = 0; i < 50; i++)
                {
                    var OP = users[rand.Next(users.Count)];
                    var post = new Post
                    {
                        Title = randGen.Sentence(),
                        Content = randGen.Paragraph(),
                        DateCreated = randGen.DT(new DateTime(2021, 2, 1, 0, 0, 0)),
                        Author = OP
                    };
                    post.UpvotedBy.Add(OP);

                    // 1-20 comments
                    for (int j = 0; j < rand.Next(1, 21); j++)
                    {
                        var commenter = users[rand.Next(users.Count)];
                        var comment = new Comment
                        {
                            Content = randGen.Sentence(),
                            DateCreated = randGen.DT(post.DateCreated),
                            Author = commenter,
                            RepliedTo = post,
                            Post = post
                        };
                        comment.UpvotedBy.Add(commenter);

                        // 0-3 replies
                        for (int k = 0; k < rand.Next(4); k++)
                        {
                            var replier = users[rand.Next(users.Count)];
                            var reply = new Comment
                            {
                                Content = randGen.Sentence(),
                                DateCreated = randGen.DT(comment.DateCreated),
                                Author = replier,
                                RepliedTo = comment,
                                Post = post
                            };
                            reply.UpvotedBy.Add(replier);
                            comment.Replies.Add(reply);
                        }

                        post.Replies.Add(comment);
                    }
                    posts.Add(post);
                }
                context.Posts.AddRange(posts);

                context.SaveChanges();
            }
        }

        private static async Task AddAdmin(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetService<UserManager<ApplicationUser>>();
            var roleManager = serviceProvider.GetService<RoleManager<IdentityRole>>();
            var admin = new ApplicationUser
            {
                UserName = "chmorgan"
            };
            await userManager.CreateAsync(admin, "testing123");
            await roleManager.CreateAsync(new IdentityRole("admin"));
            await userManager.AddToRoleAsync(admin, "admin");
        }
    }

    public class RandomGenerator
    {
        List<string> words = new List<string>{ "characteristic", "format", "disappoint", "unanimous", "animal", "whole", "physical",
        "constraint", "photograph", "jelly", "turkey", "intensify", "winter", "curriculum", "attachment", "blade", "gold", "blast",
        "smash", "attract", "arise", "assertive", "scatter", "photocopy", "thick", "weapon", "grain", "at", "bomb", "doctor",
        "broccoli", "twin", "dare", "reach", "climb", "strap", "broken", "exclude", "sympathetic", "recession", "beam", "sense",
        "knit", "unaware", "rank", "native", "genuine", "win" };
        Random rand = new Random();
        DateTime now = DateTime.Now;


        public string Word()
        {
            return words[rand.Next(words.Count)];
        }

        public string Sentence()
        {
            // 5-20 words in a sentence
            var wordCount = rand.Next(5, 21);
            var sentence = "";

            // Capitalize the first letter
            var first = Word();
            first = first.First().ToString().ToUpper() + first.Substring(1);
            sentence += first + " ";

            for (int i = 0; i < wordCount; i++)
            {
                sentence += Word() + " ";
            }
            sentence = sentence.Trim();
            sentence += ". ";

            return sentence;
        }

        public string Paragraph()
        {
            // 3-10 sentences in a paragraph
            var sentenceCount = rand.Next(3, 11);

            var paragraph = "";
            for (int i = 0; i < sentenceCount; i++)
            {
                paragraph += Sentence();
            }

            return paragraph;
        }

        public string Name()
        {
            var word1 = Word();
            word1 = word1.First().ToString().ToUpper() + word1.Substring(1);

            var word2 = Word();
            word2 = word2.First().ToString().ToUpper() + word2.Substring(1);

            var word3 = Word();
            word3 = word3.First().ToString().ToUpper() + word3.Substring(1);

            return word1 + word2 + word3;
        }

        public DateTime DT(DateTime start)
        {
            var range = (now - start).TotalMinutes;
            return start.AddMinutes(range * rand.NextDouble());
        }
    }
}
