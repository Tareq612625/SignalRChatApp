using SignalRChatApp.Interface;
using SignalRChatApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalRChatApp.Repository
{
    public class UserRepo : IUser
    {
        private readonly AppDbContext _dbContext;

        public UserRepo(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<User> GetByEmailAsync(string email)
        {
            return await _dbContext.User.FirstOrDefaultAsync(c => c.Email == email);
        }

        public async Task<User> GetByIdAsync(int id)
        {
            return await _dbContext.User.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task SaveAsync(User user)
        {
            _dbContext.Add(user);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<User>> SelectAllAsync()
        {
            return await _dbContext.User.ToListAsync();
        }

        public async Task UpdateAsync(User user)
        {
            _dbContext.Update(user);
            await _dbContext.SaveChangesAsync();
        }
    }
}
