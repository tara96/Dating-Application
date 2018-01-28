using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLayer.Repository
{
    public class UserRepository
    {
        public static MyDatabaseEntities Context { get; set; }
        public UserRepository()
        {
            Context = new MyDatabaseEntities();
        }
        public static bool UserExists(string username, string password)
        {
            using (var context = new MyDatabaseEntities())
            {
                return context.Users.Any(x => x.Username == username && x.Password == password);
            }
        }

             public static Users Get(string username)
        {
            using (var context = new MyDatabaseEntities())
            {
                return context.Users.FirstOrDefault(x => x.Username == username);
            }
        }

        public static List<Users> GetAll()
        {
            using (var context = new MyDatabaseEntities())
            {
                return context.Users.ToList();
            }
        }

        public static Users LoginUser(string userName, string password)
        {
            var user = Context.Users.FirstOrDefault(x => x.Username.Equals(userName, StringComparison.OrdinalIgnoreCase) &&
                                                        x.Password.Equals(password, StringComparison.OrdinalIgnoreCase));


            return user;
        }

    }
    }

