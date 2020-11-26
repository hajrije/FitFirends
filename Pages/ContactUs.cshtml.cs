using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FitFriends.Data;
using FitFriends.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FitFriends.Pages
{
    public class ContactUsModel : PageModel
    {
        public readonly ApplicationDbContext _db;
        public ContactUsModel(ApplicationDbContext db)
        {
            _db = db;
        }

       
        
        [BindProperty]
        public MessageModel sms { get; set; } 
        public Message Message { get; set; } //object i modelit Message.
        
        public void OnGet(int id)  //id eshte parameter standarte ,nuk mund ta shkruajm id-n ndryshe.
        {
            _db.Set<Message>(); //i referohemi  Message ne db.
            var message = _db.Messages.FirstOrDefault(m => m.Id == id); //kap te parin postim qe ploteson ate kusht
                                                                                              // nga db
            if (message == null )
            {
                RedirectToPage("/Index");
            }

            Message = message;
        }


        [ValidateAntiForgeryToken]
        public IActionResult OnPostSend(MessageModel sms)
        {
            if (ModelState.IsValid)
            {
                Message message = new Message
                {
                    Name = sms.Name,
                    Email = sms.Email,
                    Messagee = sms.Message

                };

                _db.Set<Message>(); //i  referohemi modelit Message.
                _db.Add(message);
                _db.SaveChanges();
              

                return Page();
            }

            return Page();
        }

    }

    public class MessageModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Message { get; set; }
    }

   
}