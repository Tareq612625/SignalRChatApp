using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using SignalRChatApp.Hubs;
using SignalRChatApp.Interface;
using SignalRChatApp.Models;
using SignalRChatApp.Repository;

namespace SignalRChatApp.Controllers
{
    public class ChatController : Controller
    {
        private readonly SessionRepo _sessionManager;
        private readonly IUser _user;
        private readonly IChatMessage _chatMessage;
        private readonly IHubContext<ChatHub> _hubContext;
        public ChatController(IUser user, IChatMessage chatMessage, IServiceProvider serviceProvider)
        {
            _user = user;
            _chatMessage = chatMessage;
            _sessionManager = serviceProvider.GetRequiredService<SessionRepo>();
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Message()
        {
            var userData=_sessionManager.GetSessionModelValue<SignalRChatApp.Models.User>("_UserInfo");
            ViewBag.UserInfo = userData;       
            var t = await _chatMessage.getUserChattedList(userData.Email);
            ViewBag.UserList = t;
            return View();
        }
        public async  Task<IActionResult> PartialViewAction(string userEmail)
        {
            var userData = _sessionManager.GetSessionModelValue<SignalRChatApp.Models.User>("_UserInfo");
            var user = await _chatMessage.GetSenderReceiverChat(userEmail,userData.Email);
            
            ViewBag.UserInfo = userData;
            ViewBag.SelectedUser = userEmail;
            return PartialView("_ChatArea", user);
        }
        //[HttpPost] 
        //public async Task<IActionResult> SendMessage(string sender, string receiver, string msgText)
        //{
        //    ChatMessage obj = new ChatMessage
        //    {
        //        Sender = sender,
        //        Receiver = receiver,
        //        Message = msgText,
        //        Timestamp = DateTime.Now,
        //        IsRead = false
        //    };

        //    try
        //    {
        //        await _chatMessage.SaveAsync(obj); 
        //        return Ok("Message sent successfully"); 
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, "Error sending message: " + ex.Message);
        //    }
        //}
        [HttpPost]
        public async Task<IActionResult> SendMessage(string sender, string receiver, string msgText)
        {
            ChatMessage obj = new ChatMessage
            {
                Sender = sender,
                Receiver = receiver,
                Message = msgText,
                Timestamp = DateTime.Now,
                IsRead = false
            };
            try
            {
                await _chatMessage.SaveAsync(obj);
                // Broadcast the message to all clients
                //await _hubContext.Clients.All.SendAsync("ReceiveMessage", sender, msgText);
                return Ok("Message sent successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error sending message: " + ex.Message);
            }
        }
    }
}
