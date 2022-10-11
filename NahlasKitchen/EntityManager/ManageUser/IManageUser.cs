using NahlasKitchen.Models;

namespace NahlasKitchen.EntityManager.ManageUser
{
    public interface IManageUser
    {
        //This method gets all users in the data base
        public List<User> getAllUsers();
        //This method gets specific user by the id
        public User getUserById(int id);
        //This method Adds new user 
        public void addUser(User user);
        //This method will remove specific user from db
        public void deleteUser(int id);
        //This method will update the user data
        public void updateUser(User user);
        //This method is used for adding profile picture for the user
        public void AddUserProfilePic(IFormFile pic);
        //This method is used for downloading picture pic of the user
        public void downloadImage();
        //This method is used for password encryption
        public string encryptPw(string pw);

    }
}
