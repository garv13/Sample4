using Firebase.Database;
using Sample4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample4.Utilities
{
    class Authenticate
    {
        static FirebaseClient firebase = new FirebaseClient("https://samplebooks-178e7.firebaseio.com/");
        public static async Task<List<User>> GetAllUsers()
        {
            try
            {
                return (await firebase
                  .Child("Users")
                  .OnceAsync<User>()).Select(item => new User
                  {
                     
                      Username = item.Object.Username
                  }).ToList();
            }
            catch (Exception ex)
            {
                return new List<User>();
            }
        }
        public static async Task<User> GetUser(User user)
        {
            try
            {
                var allUsers = await GetAllUsers();
                return allUsers.Where(a => a.Username == user.Username).FirstOrDefault();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public static async Task<bool> AddUser(User user)
        {
            try
            {
                User u = await GetUser(user);
                if (u == null)
                {
                    await firebase.Child("Users").PostAsync(user);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

    }
}
