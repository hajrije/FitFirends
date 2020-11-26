using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FitFriends.Models
{
    public class Post
    {

        public int PostId { get; set; }
       
        [Required(ErrorMessage = "Title required")]
        public string Title { get; set; }
        public string Image { get; set; }

        [Required(ErrorMessage = "Post text required")]
        public string Body { get; set; }

        public DateTime PubDate { get; set; } = DateTime.Now;
        public DateTime LastModified { get; set; } = DateTime.Now;

        public bool IsPublic { get; set; }
        public bool IsDeleted { get; set; }
        public string Excerpt { get; set; }  
        


        public List<Comment> Comments { get; set; } // = new List<Comment>();  //coll nav prop.1 post ka shume komente.
          
        public Category Category { get; set; } //1 post i perket 1 kategorie
        public int CategoryId { get; set; } //fk between (category,post)
      
    }
}
