using SignalRChatApp.Interface;
using SignalRChatApp.Models;

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
            return _dbContext.User.Where(c => c.Email == email).FirstOrDefault();
        }

        public async Task<User> GetByIdAsync(int Id)
        {

            return _dbContext.User.Where(c=>c.Id==Id).FirstOrDefault();
        }
      
        public async Task SaveAsync(User user)
        {
            await Task.Run(() =>
            {
                _dbContext.Add(user);
                _dbContext.SaveChanges();
            });
        }

        public async Task<IEnumerable<User>> SelectAllAsync()
        {
            await Task.Run(() =>
            {
                _dbContext.User.ToList();
            });

            return Enumerable.Empty<User>();
        }

        public async Task UpdateAsync(User user)
        {
            await Task.Run(() =>
            {
                _dbContext.Update(user);
                _dbContext.SaveChanges();
            });
        }

       
    }
}
