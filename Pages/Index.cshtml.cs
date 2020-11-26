using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FitFriends.Data;
using FitFriends.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FitFriends.Pages
{
    public class IndexModel : PageModel

    {
        private readonly ApplicationDbContext _ctx;
        public IndexModel(ApplicationDbContext ctx)
        {
            _ctx = ctx;
        }

        public List<PostSummaryModel> PostSummaries { get;set; } //do mbaj listen e postimeve.
        
        public void OnGet() 
        {
            List<Post> postModels = _ctx.Posts.Where(p => p.IsPublic && !p.IsDeleted).Include(x=>x.Comments).ToList();   // .include i ben join
            
            PostSummaries = postModels.Select(p => new PostSummaryModel        //tabelave ne db dhe sjell te gjitha te dhenat e postimit perfshire komentet.
            {
                Id = p.PostId,
                Title = p.Title,
                Excerpt = p.Excerpt,
                PublishTime = p.PubDate,
                CommentCount = p.Comments.Where(c=>c.IsPublic).Count() //te numerohen vetem komentet qe jane publike.
            }).ToList();
        }



        public class PostSummaryModel
        {
            public int Id { get; set; }
            public string Title { get; set; }
            public DateTime PublishTime { get; set; }
            public string Excerpt { get; set; }
            public int CommentCount { get; set; }
        }
    }
}
