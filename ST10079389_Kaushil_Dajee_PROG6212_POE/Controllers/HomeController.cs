using Microsoft.AspNetCore.Mvc;
using MidnightPurpleLibrary;
using ST10079389_Kaushil_Dajee_PROG6212_POE.Models;
using System.Diagnostics;
namespace ST10079389_Kaushil_Dajee_PROG6212_POE.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        Keys keys = new Keys(); 
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        public IActionResult Index()
        {
            if (Request.Cookies.TryGetValue("UserID", out string IDString) && int.TryParse(IDString, out int userID))
            {
                if (keys.NotifyUser(userID) == null)
                {
                    ViewBag.Message = null;//i set the message as null so it meets the condition in my home page html where if it is null it wont show any alert
                }
                else
                {
                    ViewBag.Message = keys.NotifyUser(userID);//notifies the user what they must study today if they did set a notification to study
                }
            }
            return View();
        }

        public IActionResult Privacy()
        {
            return View();//basic classes that were created when scaffolded
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });//shpws an error if there is
        }
    }
}