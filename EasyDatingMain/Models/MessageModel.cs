using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EasyDatingMain.Models
{
    public class MessageModel
    {
        [Key]
        public int MessageID { get; set; }
        public int ReceiverID { get; set; }
        public int SenderID { get; set; }
        public string Content { get; set; }
        public string Sender { get; set; } //Användarnamnet för den som skriver meddelandet
        public string Receiver { get; set; }// Användarnamn för den som får meddelandet


    }
}