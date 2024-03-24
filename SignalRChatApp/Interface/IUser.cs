using SignalRChatApp.Models;

namespace SignalRChatApp.Interface
{
    public interface IUser
    {
        Task SaveAsync(User user);
        Task UpdateAsync(User user);
        Task<IEnumerable<User>> SelectAllAsync();
        Task<User> GetByIdAsync(int Id);
        Task<User> GetByEmailAsync(string email);
    }
}
