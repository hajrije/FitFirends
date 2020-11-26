using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FitFriends.Data;
using FitFriends.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;

namespace FitFriends.Pages
{
    [Authorize(Roles ="Admin")]
    public class DraftsModel : PageModel
    {
        private readonly ApplicationDbContext _db; //dependency injections to communicate with db;
        public DraftsModel(ApplicationDbContext db)
        {
            _db = db;
        }

      public List<DraftSummaryModel> DraftSummaries { get; set; } //do mbaj listen e postimeve DRAFT

            
        public void OnGet()
        {

            List<Post> postModel = _db.Posts.Where(i=>!i.IsPublic).ToList();   //objecti(postModel) i listes se postimeve 
                                                      //merr listen e postimeve ku (ispublic=false) nga databaza
            DraftSummaries = postModel.Select(p => new DraftSummaryModel
            {

             Id=p.PostId,            
             Title=p.Title,
             Excerpt=p.Excerpt,
             PublishTime=p.PubDate,
            // IsPublic=false,
            }).ToList();
            
        }
    }

    public class DraftSummaryModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime PublishTime { get; set; }
        public string Excerpt { get; set; }
        public bool IsPublic { get; set; }
    }

}