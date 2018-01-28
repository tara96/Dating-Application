using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EasyDatingMain.Models
{
    public class UserInitializer : System.Data.Entity.CreateDatabaseIfNotExists<OurDbContext>
    {
        protected override void Seed(OurDbContext context)
        {
            context.userAccount.Add(new UserAccount
            {
                Name = "Lars Larsen",
                Mail = "Lars@hotmail.com",
                Username = "Larsen",
                Password = "123456",
                Confirm_Password = "123456",
                Visible = true,
                Description = "Hej snorisar",
                ProfilePicture = "tjejen.jpg",
                age = 90 
            });
            context.userAccount.Add(new UserAccount
            {
                Name = "Maja måne",
                Mail = "Maja@hotmail.com",
                Username = "Majan",
                Password = "123456",
                Confirm_Password = "123456",
                Visible = true,
                Description = "Hej snorisar",
                ProfilePicture = "tjejen.jpg",
                age = 90
            });
            context.userAccount.Add(new UserAccount
            {
                Name = "Rosa rånare",
                Mail = "Rosa@hotmail.com",
                Username = "Rosen",
                Password = "123456",
                Confirm_Password = "123456",
                Visible = true,
                Description = "Hej snorisar",
                ProfilePicture = "tjejen.jpg",
                age = 90
            });
            context.userAccount.Add(new UserAccount
            {
                Name = "Kurt Kurtis",
                Mail = "Kurt@hotmail.com",
                Username = "Kurten",
                Password = "123456",
                Confirm_Password = "123456",
                Visible = true,
                Description = "Hej snorisar",
                ProfilePicture = "tjejen.jpg",
                age = 90
            });
            base.Seed(context);
        }
    }
}