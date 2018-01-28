using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EasyDatingMain.Models
{
    public class Profile
    {
        [Key]
        public int ID { get; set; }
        public string Description { get; set; }
        public string ProfilePicture { get; set; }
        public string Searching { get; set; }
        public int age { get; set; }
    }
}