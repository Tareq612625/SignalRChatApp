using Microsoft.AspNetCore.Mvc;
using SignalRChatApp.Interface;
using SignalRChatApp.Repository;

namespace SignalRChatApp.Controllers
{
    public class ChatController : Controller
    {
        private readonly SessionRepo _sessionManager;
        private readonly IUser _user;
        private readonly IChatMessage _chatMessage;
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
    }
}
