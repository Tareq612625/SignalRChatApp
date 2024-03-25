using SignalRChatApp.Models;
using SignalRChatApp.Models.ViewModel;

namespace SignalRChatApp.Interface
{
    public interface IChatMessage
    {
        Task SaveAsync(ChatMessage user);
        Task UpdateAsync(ChatMessage user);
        Task<IEnumerable<ChatMessage>> SelectAllAsync();
        Task<ChatMessage> GetByIdAsync(int Id);
        Task<IEnumerable<ChatMessage>> GetSenderReceiverChat(string email);
        Task<IEnumerable<VwChatted>> getUserChattedList(string userEmail);
    }
}
