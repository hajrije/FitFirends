using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FitFriends.Data;
using FitFriends.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace FitFriends.Pages
    
{    [Authorize(Roles = "Admin")]
    public class TrashModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        public TrashModel(ApplicationDbContext db)
        {
            _db = db;
        }

   
        public List<PostSummaryModel> PostSummaries { get; private set; }

        public void OnGet()
        {

            List<Post> postmodel = _db.Posts
                .Where(p => p.IsDeleted && p.IsPublic) //marim nga db postet qe kane isdeleted=1
                .Include(x => x.Comments)
                .ToList();

            PostSummaries = postmodel.Select(p => new PostSummaryModel
            {
                Id = p.PostId,
                Title = p.Title,
                PublishTime = p.PubDate,
                Excerpt = p.Excerpt,
                CommentCount = p.Comments.Count(),


            }).ToList();

           

        }
       
    
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