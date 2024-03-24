using Microsoft.AspNetCore.Mvc;
using SignalRChatApp.Interface;
using SignalRChatApp.Models;
using SignalRChatApp.Repository;

namespace SignalRChatApp.Controllers
{
    public class UserController : Controller
    {
        private readonly IUser _user;
        private readonly SessionRepo _sessionManager;

        public UserController(IUser user, IServiceProvider serviceProvider)
        {
            _user = user;
            _sessionManager = serviceProvider.GetRequiredService<SessionRepo>();
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(User user)
        {
            try
            {
                var data = await _user.GetByEmailAsync(user.Email);
                if (data != null) {
                    _sessionManager.SetSessionModeltValue("_UserInfo", data);
                    ViewBag.Message = "Login Successful";
                    return RedirectToAction("Message","Chat");
                }
                else
                {
                    ViewBag.Message = "User email is incorrect";
                    return View(user);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.ToString();
                return View(user);
            }
        }
        [HttpGet]
        public IActionResult Registration()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Registration(User user)
        {
            try
            {
                _user.SaveAsync(user);
                return RedirectToAction("Login", "User");

            }
            catch (Exception ex)
            {
                return View(user);
            }
        }
        public IActionResult Logout()
        {
            _sessionManager.RemoveAllSessionVlaue();
            return RedirectToAction("User", "Login");
        }
    }
}
