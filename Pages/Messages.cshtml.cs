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
    [Authorize(Roles = "Admin")]
    public class MessagesModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        public MessagesModel(ApplicationDbContext db)
        {
            _db = db;
        }

        public List<MessagesSummeriesModel> MessagesSummaries { get; set; } //percakton qe kjo faqe do mbaj liste me sms.

        public void OnGet()
        {
            List<Message> messageModels = _db.Messages.ToList();

            MessagesSummaries =messageModels.Select(m => new MessagesSummeriesModel 
            {
               MessageId = m.Id,
               Name=m.Name,
               Email=m.Email,
               Message=m.Messagee

            }).ToList();
        }

       //kemi vetem metoden onget sepse po marrim nje nga nje mesazhet nga databaza.



     public class MessagesSummeriesModel
        {
            public int MessageId { get; set; }
            public string Name { get; set; }
            public string Email { get; set; }
            public string Message { get; set; }
        }





    }



    
}