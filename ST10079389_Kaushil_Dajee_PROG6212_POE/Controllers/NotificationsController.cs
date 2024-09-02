using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ST10079389_Kaushil_Dajee_PROG6212_POE.Models;
namespace ST10079389_Kaushil_Dajee_PROG6212_POE.Controllers
{
    public class NotificationsController : Controller
    {
        private readonly MidnightPurpleWebsiteDbContext _context;
        public NotificationsController(MidnightPurpleWebsiteDbContext context)
        {
            _context = context;
        }
        // GET: Notifications
        public ActionResult Index()
        {
            if (Request.Cookies.TryGetValue("UserID", out string IDString) && int.TryParse(IDString, out int UserID))//takes in the cookie sends it as a string then converts it to an int to be used when neccessarry
            {
                // Fetch the modules for the specific UserId
                var userData = _context.Notification
                .Where(info => info.UserId == UserID)//ensuring that it shows only date where the users id is the same as the person who logged in
                .Include(info => info.Module)//without this my modules will not show so i have to include it like to this so it can be displayed on the index page 
                .ToList();
                return View(userData);//it should always show the user which logged in their data
            }
            return View();
        }

        // GET: Notifications/Details/5
        public async Task<IActionResult> Details(int? id)
        {//this method was generated when i scaffolded the model
            if (id == null || _context.Notification == null)
            {
                return NotFound();
            }
            var notification = await _context.Notification
                .Include(n => n.Module)
                .Include(n => n.User)
                .FirstOrDefaultAsync(m => m.NotificationID == id);//it displays the neccarry information about that notificatio 
            if (notification == null)
            {
                return NotFound();
            }
            return View(notification);
        }
        // GET: Notifications/Create
        private void PopulateModuleName()
        {//this method is used to populate the viewbag otherwise if an error occurs then it will display nothing when selecting the modules name
            if (Request.Cookies.TryGetValue("UserID", out string IDString) && int.TryParse(IDString, out int UserID))//takes in the cookie converts it to a string then an int
            {
                var modulesForUser = _context.ModuleInformations
               .Where(name => name.UserId == UserID)//is then used so it can only display that users notifications and none of the other users
               .ToList();
                ViewData["ModuleId"] = new SelectList(modulesForUser, "ModuleId", "ModuleName");
            }
        }
        public IActionResult Create()
        {
            PopulateModuleName();
            return View();
        }
        // POST: Notifications/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NotificationID,NotificationDate,UserId,ModuleId")] Notification notification)
        {
            if(notification.NotificationDate == null)
            {
                ModelState.AddModelError(string.Empty, "Error, please enter the respected fields");//i throw a basic error just to notify the user to enter in the correct information
                PopulateModuleName();
                return View();
            }
            else if (ModelState.IsValid)
            {
                if (Request.Cookies.TryGetValue("UserID", out string IDString) && int.TryParse(IDString, out int UserID))//takes in the cookie and passes it as a string which is converted to an int
                {
                    notification.UserId = UserID;   
                    _context.Add(notification);
                    await _context.SaveChangesAsync();//the notification is saved successfully
                    return RedirectToAction(nameof(Index));//the user is then taken to the index page 
                }
            }
            return View();
        }
        // GET: Notifications/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {//another method that was generated when scaffolded
            if (id == null || _context.Notification == null)
            {
                return NotFound();
            }
            var notification = await _context.Notification
                .Include(n => n.Module)
                .Include(n => n.User)
                .FirstOrDefaultAsync(m => m.NotificationID == id);//does display the correct notification with its correct information
            if (notification == null)
            {
                return NotFound();
            }
            return View(notification);
        }
        // POST: Notifications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Notification == null)
            {
                return Problem("Entity set 'MidnightPurpleWebsiteDbContext.Notification'  is null.");
            }
            var notification = await _context.Notification.FindAsync(id);
            if (notification != null)
            {
                _context.Notification.Remove(notification);//does remove the notification rom the database
            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));//user is then redirected to the index page 
        }
        private bool NotificationExists(int id)
        {
          return (_context.Notification?.Any(e => e.NotificationID == id)).GetValueOrDefault();
        }
        
    }
}
