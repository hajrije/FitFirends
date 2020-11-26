using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FitFriends.Models
{
    public class Category
    {
        public int CategoryId { get; set; } 
        public string Name { get; set; }       
        public List<Post> Posts { get; set; } = new List<Post>(); //1 kategori ka disa poste (1-to-M)
       
    }
}
