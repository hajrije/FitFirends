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
    public class NewPostModel : PageModel
    {
        public readonly ApplicationDbContext _ctx;  //dependency injections
        public NewPostModel(ApplicationDbContext ctx) //per te komunikuar me databazen
        {
            _ctx = ctx;

            Categories = _ctx.Categories.Select(c => new SelectListItem(c.Name,c.CategoryId.ToString())).ToList(); //mbush Categories

        }



        [BindProperty]
        public NewPostViewModel NewPost { get; set; }
        public List<SelectListItem> Categories { get; set; }
       
        public Post Post { get; set; } ///object i modelit POST
       
       
       
        public void OnGet([FromRoute] int id)       //merr te dhena nga db
        {
            _ctx.Set<Post>();
            var post = _ctx.Posts.Where(p => p.IsPublic).FirstOrDefault(p => p.PostId == id); //kap te parin postim qe ploteson ate kusht
                                                                       // nga db
            if (post == null || !post.IsPublic)
            {
               RedirectToPage("/Index");
            }

            Post = post;
        }


        [ValidateAntiForgeryToken]
        public IActionResult OnPostPublish( NewPostViewModel newPost)
        {
            if (ModelState.IsValid)
            {
                Post post = new Post
                {
                    Title = newPost.Title,
                    Excerpt = newPost.Excerpt,
                    Body = newPost.Body,
                   // Image = newPost.ImagePath,
                    IsPublic = true,
                   CategoryId = newPost.CategoryId
                };

                _ctx.Set<Post>();
                _ctx.Posts.Add(post); //per te ruajtur ne db te dhenat
                _ctx.SaveChanges();

                  
               return RedirectToPage("Index");

            }

            return Page(); //nqs model is not valid
        }

        [ValidateAntiForgeryToken]
        public IActionResult OnPostSaveDraft(NewPostViewModel newPost)
        {
            if (ModelState.IsValid)
            {
                Post post = new Post    //krijon post te ri draft
                {
                    Title = newPost.Title,
                    Excerpt = newPost.Excerpt,
                    Body = newPost.Body,
                    // Image = newPost.ImagePath,
                    IsPublic = false,                    //nuk publikohet por dergohet tek drafts.
                    CategoryId = newPost.CategoryId
                };

                _ctx.Set<Post>();
                _ctx.Posts.Add(post); //per te ruajtur ne db te dhenat
                _ctx.SaveChanges();

                
              return RedirectToPage("Drafts");
               
            }
             return Page();
        }
        


        public class NewPostViewModel
        {
            [Required]
            public int PostId { get; set; }

            [Required]
            public string Title { get; set; }

            [Required]
            public string Body { get; set; }
            public string Excerpt { get; set; }
            
           public int CategoryId { get; set; }
            public string ImagePath { get; set; }
        }


    }
}