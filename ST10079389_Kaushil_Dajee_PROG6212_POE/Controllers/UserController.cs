using Microsoft.AspNetCore.Mvc;
using MidnightPurpleLibrary;
using ST10079389_Kaushil_Dajee_PROG6212_POE.Models;
namespace ST10079389_Kaushil_Dajee_PROG6212_POE.Controllers
{
    public class UserController : Controller
    {
        private readonly MidnightPurpleWebsiteDbContext _context;
        Keys keys = new Keys();
        Encrypt encrypt = new Encrypt();
        public UserController(MidnightPurpleWebsiteDbContext context)
        {
            _context = context;
        }
        // GET: User
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index([Bind("Name,Password")] User profile)
        {
            if (profile.Name == null || profile.Password == null)
            {
                ModelState.AddModelError(string.Empty, "Invalid, Please enter your username or password.");//ensure none of the respected fields are blank
                return View(profile);
            }
            var user = _context.Users
           .FirstOrDefault(x => x.Name == profile.Name && x.Password == encrypt.Hash_Password(profile.Password));//checks to see if the credentials are correct

            if (user != null)
            {
                string username = profile.Name;
                string password = encrypt.Hash_Password(profile.Password);
                int UserID = keys.GetID(username, password);//i use it get the userid so i can set it as a cookie so anywhere in the application i can access it for when the user wants to login
                Response.Cookies.Append("UserID", UserID.ToString());//i use cookies because it allows me to easily access it anywhere on the websites application compared to a viewbag which only saves it temporarily for one page but with cookies it saves it throughout the web application
                return RedirectToAction("Index", "Home");//takes them to the home page
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid, Please enter the correct username or password.");//if they have the wrong credentials it throws a warning
                return View(profile);
            }
        }
        // GET: User/Create
        public IActionResult Create()
        {
            return View();
        }
        // POST: User/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserId,Name,Password")] User user)
        {

            if (user.Name == null || user.Password == null)
            {
                ModelState.AddModelError(string.Empty, "Invalid, Username or Password cannot be null.");//ensure none of the fields are left blank
                return View(user);
            }

            if (ModelState.IsValid)
            {
                user.Password = encrypt.Hash_Password(user.Password);//password is hashed for security reasons
                _context.Add(user);
                await _context.SaveChangesAsync();//it is saved to the database
                int UserID = user.UserId;//i get the user id with ease
                Response.Cookies.Append("UserID", UserID.ToString());//i use cookies because it allows me to easily access it anywhere on the websites application compared to a viewbag which only saves it temporarily for one page but with cookies it saves it throughout the web application
                return RedirectToAction("Index", "Home");//takes them to the home page
            }
            return View(user);
        }
    }
}
