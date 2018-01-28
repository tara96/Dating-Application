using EasyDatingMain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EasyDatingMain.API
{
    public class MessageController : Controller
    {
        [HttpGet]
        public MessageListModel GetPosts(string username)
        {
            var posts = new MessageListModel();

            try
            {

                using (var db = new OurDbContext())
                {
                    //int användarID = db.userAccount.Single(x => x.Username == username).ID;

                    //var meddelanden = from m in db.Message
                    //                  where m.ReceiverID == användarID
                    //                  select m;

                    //foreach(var m in meddelanden)
                    //{
                    //    var sender = m.Sender;
                    //    var text = m.Content;
                    //    posts.Add
                    //}

                    int userID = db.userAccount.FirstOrDefault(x => x.Username == username).ID;

                    var result = from m in db.Message
                                 where m.ReceiverID == userID
                                 select m;

                    foreach (var item in result)
                    {
                        var message = new MessageModel();

                        message.Content = item.Content;
                        message.Sender = db.userAccount.FirstOrDefault(x => x.ID == item.SenderID).Username;

                        posts.Add(message);
                    }
                }

                return posts;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }

        }
        [HttpPost]
        public void SendPost(MessageModel post)
        {

            if (ModelState.IsValid)
            {
                //    using (OurDbContext db = new OurDbContext())
                //    {
                //        db.Message.Add(post);
                //        db.SaveChanges();
                //    }
                //    ModelState.Clear();

                //}

                try
                {
                    using (var db = new OurDbContext())
                    {
                        int receiverID = db.userAccount.FirstOrDefault(x => x.Username == post.Receiver).ID;
                        int senderID = db.userAccount.FirstOrDefault(x => x.Username == post.Sender).ID;

                        var message = new MessageModel()
                        {
                            ReceiverID = receiverID,
                            SenderID = senderID,
                            Content = post.Content,
                            Sender = post.Sender,
                            Receiver = post.Receiver
                        };

                        db.Message.Add(message);
                        db.SaveChanges();

                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}