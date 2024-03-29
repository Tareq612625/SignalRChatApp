using SignalRChatApp.Interface;
using SignalRChatApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SignalRChatApp.Models.ViewModel;

namespace SignalRChatApp.Repository
{
    public class ChatMessageRepo : IChatMessage
    {
        private readonly AppDbContext _dbContext;

        public ChatMessageRepo(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<ChatMessage>> GetSenderReceiverChat(string email, string appUserEmail)
        {
            return await _dbContext.ChatMessage
        .Where(c => (c.Sender == email && c.Receiver == appUserEmail) || (c.Receiver == email && c.Sender == appUserEmail))
        .ToListAsync();
        }

        public async Task<ChatMessage> GetByIdAsync(int id)
        {
            return await _dbContext.ChatMessage.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task SaveAsync(ChatMessage chatMessage)
        {
            _dbContext.Add(chatMessage);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<ChatMessage>> SelectAllAsync()
        {
            return await _dbContext.ChatMessage.ToListAsync();
        }

        public async Task UpdateAsync(ChatMessage chatMessage)
        {
            _dbContext.Update(chatMessage);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<VwChatted>> getUserChattedList(string userEmail)
        {
            var chattedUsers = (from u in _dbContext.User
                                join cu in (
                                    from m in _dbContext.ChatMessage
                                    where m.Receiver == userEmail || m.Sender == userEmail
                                    select m.Sender == userEmail ? m.Receiver : m.Sender
                                ) on u.Email equals cu
                                where u.Email != userEmail
                                select new VwChatted
                                {
                                    Email = u.Email,
                                    UserName = u.FirstName + " " + u.LastName
                                }).Distinct().ToListAsync();
            return await chattedUsers;
        }
    }
}
