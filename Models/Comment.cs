using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FitFriends.Models
{
    public class Comment
    {
        public int CommentId { get; set; }

        [Required(ErrorMessage = "Name required")]
        public string AuthorName { get; set; }


        [Required]
        public string EmailAddress { get; set; }


        [Required(ErrorMessage = "Comment text required")]
        public string Body { get; set; } 


        public DateTimeOffset PubDate { get; set; } = DateTimeOffset.Now;

       public bool IsPublic { get; set; }      


      public int PostId { get; set; } //foreign key (ky koment i perket postimit me kte id)s
        public Post Post { get; set; } //1 koment i perket nje postimi


    }
}
