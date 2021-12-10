using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Sigma3.Objects;
using SQLite;

namespace Sigma3.Services
{
    public static class AppService
    {
        
        static SQLiteAsyncConnection database;
        async static Task Init()
        {
            if (database != null) return;
            var databasePath = Path.Combine(FileSystem.AppDataDirectory, "app.db");

            database = new SQLiteAsyncConnection(databasePath);
            await database.CreateTableAsync<User>();

            await AddUserAsync(Constants.DEMO_USER);
        }

        async public static Task AddUserAsync(User user)
        {
            await Init();

            if (user.Email == "Demo")
            {
                user.UserFollowing = await Constants.GetDefaultFollowing();
            }

            await database.InsertAsync(user);
        }

        async public static Task DeleteUserAsync(int id)
        {
            await Init();

            await database.DeleteAsync<User>(id);
        }

        async public static Task<User> GetUserByEmailAsync(string email, string password)
        {
             await Init();

             var users = await database.Table<User>().ToListAsync();
         

             return users.Find(user => user.Email.Equals(email) && user.Password.Equals(password));
        }

        async public static void UpdateUser(User user)
        {
            await Init();
        }
    }
}
