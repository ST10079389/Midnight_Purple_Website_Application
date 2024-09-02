using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MidnightPurpleLibrary;
using ST10079389_Kaushil_Dajee_PROG6212_POE.Models;
namespace ST10079389_Kaushil_Dajee_PROG6212_POE.Controllers
{
    public class StudyRecordsController : Controller
    {
        private readonly MidnightPurpleWebsiteDbContext _context;
        Calculations calculations = new Calculations(); 
        public StudyRecordsController(MidnightPurpleWebsiteDbContext context)
        {
            _context = context;
        }
        // GET: StudyRecords
        public ActionResult Index()
        {
            if (Request.Cookies.TryGetValue("UserID", out string userIdString) && int.TryParse(userIdString, out int UserID))
            {
                // Fetch the modules for the specific UserId
                var userData = _context.StudyRecords
                .Include(info => info.Module)
                .Where(info => info.UserId == UserID)
                .ToList();
                return View(userData);//displays the index page with the correct information showing
            }
            return View();    
        }
        // GET: StudyRecords/Details/5
        public ActionResult Details(DateTime? startDate)
        {//this method shows the view progress i found it easier to take the already existing methods when i scaffolded and just edit them and the pages to do whatever i wanted
            if (!startDate.HasValue)
            {
                startDate = new DateTime(DateTime.Now.Year,1,1);//i set it to the first day of the year as i believe no one would enter anything on the first day
                return View(DisplayInformation(startDate));
            }
            else//this one displays all the information when the user selects a date
            {
                return View(DisplayInformation(startDate));//this displays all the information if the user selects a date
            }  
        }
        private HoursViewModel DisplayInformation(DateTime? startDate)//takes in a start date 
        {//i use this to help me return the view with the approriate data before this i had to many issues with if statements but with this its much easier to call and it displays the correct information if the user selects no date then nothing will be shown
            HoursViewModel hoursViewModel = new HoursViewModel();
            if (Request.Cookies.TryGetValue("UserID", out string userIdString) && int.TryParse(userIdString, out int UserID))
            {
                DateTime endDate = startDate.Value.AddDays(7);//it is used to make a range between dates
                hoursViewModel.studyRecords = _context.StudyRecords//i am setting it to the hoursViewModel which i created so i can easily access both the modules semesters self study hours per week and still get the total hours studied for that module otherwise i can't access both with just its own model so i created a new one
                .Include(info => info.Module)//similar to the if statement but it checks between dates
                .Where(info => info.UserId == UserID && info.StudyDates >= startDate && info.StudyDates <= endDate)//this is the condition so it will check in between those days
                .GroupBy(info => info.ModuleId)
                .Select(group => group.First())
                .ToList();
                var moduleTotalHours = _context.StudyRecords
                .Where(info => info.UserId == UserID && info.StudyDates >= startDate && info.StudyDates <= endDate)//same here gets all the information between those 7 days
                .GroupBy(info => info.ModuleId)
                .Select(group => new
                {
                    ModuleId = group.Key,//groups it with its module id as the key
                    TotalHoursStudied = group.Sum(info => info.HoursStudied)//adds the total hours studied
                })
                .ToList();
               
                ViewBag.ModuleTotalHours = moduleTotalHours;//then it is displayed onto the page
                return hoursViewModel;
            }
            return new HoursViewModel();
        }
        private void PopulateModuleName()
        {//this method is used to populate the view bag for module name otherwise the names will disapper if it encounters an error such as a missing field 
            if (Request.Cookies.TryGetValue("UserID", out string userIdString) && int.TryParse(userIdString, out int UserID))
            {
                var modulesForUser = _context.ModuleInformations
               .Where(m => m.UserId == UserID)
               .ToList();//the user is able to select the module based on its name instead of the id or typing it in
                ViewData["ModuleId"] = new SelectList(modulesForUser, "ModuleId", "ModuleName");//this allows for the user to see the modules name instead of the id
            }
        }
        // GET: StudyRecords/Create
        public IActionResult Create()
        {
            PopulateModuleName();
            return View();
        }
        // POST: StudyRecords/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StudyRecordId,StudyDates,HoursStudied,UserId,ModuleId")] StudyRecord studyRecord)
        {
            if (studyRecord.StudyDates == null || studyRecord.HoursStudied == null)
            {//always have error handling to prevent any of the fields to be saved as null
                ModelState.AddModelError(string.Empty, "Invalid, please enter neccessary fields");
                PopulateModuleName() ;  
                return View();
            }
            else if (ModelState.IsValid)
            {
                if (Request.Cookies.TryGetValue("UserID", out string userIdString) && int.TryParse(userIdString, out int UserID))
                {
                    studyRecord.UserId = UserID;//i set the user id to whomever logged in
                    _context.Add(studyRecord);
                    await _context.SaveChangesAsync();//it is saved to the database
                    return RedirectToAction(nameof(Index));//takes you to the index page
                }
            }
            return View(studyRecord);
        }
    }
}
