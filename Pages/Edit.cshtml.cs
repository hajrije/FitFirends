using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using FitFriends.Data;
using FitFriends.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;

namespace FitFriends.Pages
{
    [Authorize(Roles ="Admin")]
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        public EditModel(ApplicationDbContext db)
        {
            _db = db;

            Categories = _db.Categories.Select(c => new SelectListItem(c.Name, c.CategoryId.ToString())).ToList(); //merr nga databaza Categorite dhe i vendos tek dropdownlist.
        }


        [BindProperty]
        public EditedPostModel EditedPost { get; set; } //do mbushet me vlera.
        
        public List<SelectListItem> Categories { get; set; }


        public void OnGet([FromRoute] int id) //id e postimit
        {
            _db.Set<Post>();     //i referohemi tabeles Post ne db       
            Post post = _db.Posts.FirstOrDefault(p => p.PostId == id); //gjejm postimin me id=id e postimit qe do editojme

            if (post == null)
            {
                RedirectToPage("/Index");
            }

            EditedPost = new EditedPostModel    //marim vlerat e reja nga databaza 
            {
                PostId = post.PostId,
                Title = post.Title,
                Body = post.Body,
                Excerpt = post.Excerpt,
                // Image = post.ImagePath,
                IsPublic = true,
                CategoryId = post.CategoryId
            };
        }

        [ValidateAntiForgeryToken]
        public IActionResult OnPostPublish([FromForm] EditedPostModel editedPost)
        {

            if (ModelState.IsValid)
            {
                var post = _db.Posts.Find(editedPost.PostId); //gjen postin me id qe duam te modifikojme.

                post.Title = editedPost.Title;         //cojme vlerat e reja ne databaze
                post.Excerpt = editedPost.Excerpt;
                post.Body = editedPost.Body;
                // post.Image = editedPost.ImagePath,
                post.IsPublic = true;
                post.CategoryId = editedPost.CategoryId;


                _db.Set<Post>();
                _db.Posts.Update(post);    //per te ruajtur ne db te dhenat
                _db.SaveChanges();




                return RedirectToPage("/Index");
            }
            return Page();

        }

        [ValidateAntiForgeryToken]
        public IActionResult OnPostSaveDraft(EditedPostModel editedPost)
        {
           if (ModelState.IsValid)
           {
                 var post = _db.Posts.Find(editedPost.PostId);
                {
                post.Title = editedPost.Title;
                post.Excerpt = editedPost.Excerpt;
                post.Body = editedPost.Body;
                // Image = newPost.ImagePath,
                post.IsPublic = false;                    //nuk publikohet,editohet dhe =>tek drafts.
                post.CategoryId = editedPost.CategoryId;
                 };

                _db.Set<Post>();
                _db.Posts.Update(post); //per te ruajtur ne db te dhenat
                _db.SaveChanges();

                return RedirectToPage("Drafts");
           }
            return Page(); //nqs kushti nuk plotesohet.
        }

    
   
    
        public class EditedPostModel
        {
           public int PostId { get; set; }
            
           [Required]
            public string Title { get; set; }
           
           [Required]
            public string Body { get; set; }
            public string Excerpt { get; set; }
            public int CategoryId { get; set; }
            public bool IsPublic { get; set; }
        }
    }
}