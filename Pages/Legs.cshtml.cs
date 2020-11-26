using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FitFriends.Data;
using FitFriends.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace FitFriends.Pages
{
    public class LegsModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        public LegsModel(ApplicationDbContext db)
        {
            _db = db;
        }

        public List<LegsSummary> LegsSummaries { get; set; }
        public void OnGet()
        {
            List<Post> postModel = _db.Posts.Where(i => i.CategoryId == 6 && i.IsPublic && !i.IsDeleted).Include(x => x.Comments).ToList();   //objecti(postModel) i listes se postimeve 
                                                                                                                                              //merr listen e postimeve me categoryid=6(legs) nga databaza
            LegsSummaries = postModel.Select(p => new LegsSummary
            {

                Id = p.PostId,
                Title = p.Title,
                Excerpt = p.Excerpt,
                PublishTime = p.PubDate,
                CommentCount = p.Comments.Where(c => c.IsPublic).Count() //te numerohen vetem komentet qe jane publike.

            }).ToList();
        }



        public class LegsSummary
        {
            public int Id { get; set; }
            public string Title { get; set; }
            public DateTime PublishTime { get; set; }
            public string Excerpt { get; set; }
            public bool IsPublic { get; set; }
            public int CommentCount { get; set; }
        }
    }
}