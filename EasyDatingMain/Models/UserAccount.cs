using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EasyDatingMain.Models;
using Microsoft.Ajax.Utilities;

namespace EasyDatingMain.Models
{
    public class UserAccount 
    {
        [Key]
        public int ID { get; set; }

        [Required(ErrorMessage = "Fullname is required.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [RegularExpression(@"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$")]
        public string Mail { get; set; }

        [Required(ErrorMessage ="Username is required.")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Compare("Password", ErrorMessage = "Please confirm your password.")]
        [DataType(DataType.Password)]
        public string Confirm_Password { get; set; }
        public bool Visible { get; set; }
        public string Description { get; set; }
        public string ProfilePicture { get; set; }
        public int age { get; set; }


    }
}