using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace XF_SQLiteDB_Test
{
    public class UserOperation
    {
        readonly SQLiteAsyncConnection db;

        public UserOperation(string dbPath)
        {
            db = new SQLiteAsyncConnection(dbPath);
            db.CreateTableAsync<Users>().Wait();
        }

        public Task<List<Users>> GetUsersAsync()
        {
            //Get all notes.
            return db.Table<Users>().ToListAsync();
        }

        public Task<Users> GetUserAsync(int id)
        {
            // Get a specific note.
            return db.Table<Users>().Where(i => i.Id == id).FirstOrDefaultAsync();
        }
        public Task<Users> GetUserAsync(string username)
        {
            // Get a specific note.
            return db.Table<Users>().Where(i => i.Username == username).FirstOrDefaultAsync();
        }

        public Task<int> SaveUserAsync(Users user)
        {
            if (user.Id != 0)
            {
                // Update an existing note.
                return db.UpdateAsync(user);
            }
            else
            {
                // Save a new note.
                return db.InsertAsync(user);
            }
        }

        public Task<int> DeleteUserAsync(Users user)
        {
            // Delete a note.
            return db.DeleteAsync(user);
        }
    }
}
