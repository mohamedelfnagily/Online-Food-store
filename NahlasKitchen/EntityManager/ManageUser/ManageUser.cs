using NahlasKitchen.Data;
using NahlasKitchen.Models;
using System.Security.Cryptography;
using System.Text;

namespace NahlasKitchen.EntityManager.ManageUser
{
    public class ManageUser : IManageUser
    {
        AppdbContext db = new AppdbContext();
        readonly IHttpContextAccessor myContext = new HttpContextAccessor();
        
        public void addUser(User user)
        {
            var myUsers = db.Users.ToList();
            int id = 1;
            if(myUsers.Count !=0)
            {
                id = myUsers.Max(e => e.Id) + 1;
            }
            user.Id = id;
            user.mobileNumber = "01" + user.mobileNumber;
            user.Password = encryptPw(user.Password);
            db.Users.Add(user);
            db.SaveChanges();
        }

        public void AddUserProfilePic(IFormFile pic)
        {
            throw new NotImplementedException();
        }

        public void deleteUser(int id)
        {
            var myUser = db.Users.FirstOrDefault(e => e.Id == id);
            if(myUser!=null)
            {
                db.Users.Remove(myUser);
                db.SaveChanges();
            }
        }

        public void downloadImage()
        {
            throw new NotImplementedException();
        }

        public List<User> getAllUsers()
        {
            var myUsers = db.Users.ToList();
            if(myUsers.Count !=0)
            {
                return myUsers;
            }
            else
            {
                return null;
            }
        }

        public User getUserById(int id)
        {
            var selectedUser = db.Users.FirstOrDefault(e => e.Id == id);
            if (selectedUser != null)
            {
                return selectedUser;
            }
            else
            {
                return null;
            }
        }

        public void updateUser(User user)
        {
            int userId = int.Parse(myContext.HttpContext.Session.GetString("UserId"));
            var myUser = getUserById(userId);
            if(myUser!=null)
            {
                myUser.FirstName = user.FirstName;
                myUser.LastName = user.LastName;
                myUser.Email = user.Email;
                myUser.mobileNumber = user.mobileNumber;
                myUser.UserName = user.UserName;
                db.SaveChanges();
            }
        }

        public string encryptPw(string pw)
        {
            using var sha1 = SHA1.Create();
            return Convert.ToHexString(sha1.ComputeHash(Encoding.UTF8.GetBytes(pw)));
        }
    }
}
