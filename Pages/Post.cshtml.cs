using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using FitFriends.Data;
using FitFriends.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace FitFriends.Pages
{
    public class PostModel : PageModel
    {
        private readonly ApplicationDbContext _ctx;  //dependency injections
        public PostModel(ApplicationDbContext ctx)
        {
            _ctx = ctx;  //per te komunikuar me db
        }

        [BindProperty]
        public Post Post { get; set; } ///object i modelit POST
      
        

        public void OnGet([FromRoute] int id)
        {
            _ctx.Set<Post>(); //i referohemi tabeles Post ne databaze
            var post = _ctx.Posts
                .Include(x => x.Comments )      //includi ben join tek databaza dhe sjell gjitha komentet ne faqe
                .FirstOrDefault(p => p.PostId == id); //kap te parin postim qe ploteson kte kusht
                                                                      //ne db
            if (post == null || !post.IsPublic)
            {
                RedirectToPage("/Index");
            }

            Post = post;

        }

        [ValidateAntiForgeryToken]
        public IActionResult OnPostPublishComment( CommentViewModel newComment)
        {
           
                Comment comment = new Comment
                {
                    AuthorName = newComment.AuthorName,
                    Body = newComment.Body,
                    EmailAddress=newComment.EmailAddress,
                    PostId = newComment.PostId,             //komenti i perket postit me kte Id
                    IsPublic = true
                };

                _ctx.Set<Comment>();
                _ctx.Comments.Add(comment); //per te ruajtur komentet ne db 
                _ctx.SaveChanges();

            return RedirectToPage("/Post", newComment.PostId);
        }

       

        [ValidateAntiForgeryToken]
        public IActionResult OnPostDeletePost(int id)  //per te fshir postimin
        {
            var post = _ctx.Posts               
                .FirstOrDefault(p=>p.PostId==id);
            
          
          if (post != null)
            {
               post.IsDeleted = true;
                _ctx.SaveChanges();
                return RedirectToPage("/Index");
            }
                
            return RedirectToPage("/Post");
        }

        [ValidateAntiForgeryToken]                //per te ber undelete postimin
        public IActionResult OnPostUnDeletePost( int id)               //asp-route-id tek view.
        {
            var post = _ctx.Posts.
              FirstOrDefault(p => p.PostId == id); //mer postin e pare qe ka kte id
            
           if (post!=null)
           {
                post.IsDeleted = false;
                _ctx.SaveChanges();
              return RedirectToPage("/Index");
           }

           return RedirectToPage("/Post");
        }

        public IActionResult OnPostDeleteComment([FromRoute] int id) ///metoda fshirjes se komentit.
        {
            //var post = _ctx.Posts.Find(postid);
            var comment = _ctx.Comments.FirstOrDefault(c => c.CommentId == id && c.IsPublic);

            if (comment != null)
            {
                comment.IsPublic = false;
                _ctx.SaveChanges();

                return RedirectToPage("/Post");
            }

                  return Page();
        }

        public class CommentViewModel                  //mban inputet qe duhet te plotesoi personi qe do komentoi
        {
            public int CommentId { get; set; }
               
            [MaxLength(100, ErrorMessage = "You have exceeded the maximum length of 100 characters")]
            public string AuthorName { get; set; }

            [Required]
            public string EmailAddress { get; set; }

            [Required]
            public int PostId { get; set; }

            [Required]
            [MaxLength(1000, ErrorMessage = "You have exceeded the maximum length of 1000 characters")]
            public string Body { get; set; }
        }
    }
}