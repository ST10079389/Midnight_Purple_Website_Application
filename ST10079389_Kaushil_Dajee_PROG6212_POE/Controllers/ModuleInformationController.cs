using Microsoft.AspNetCore.Mvc;
using MidnightPurpleLibrary;
using ST10079389_Kaushil_Dajee_PROG6212_POE.Models;
namespace ST10079389_Kaushil_Dajee_PROG6212_POE.Controllers
{
    public class ModuleInformationController : Controller
    {
        private readonly MidnightPurpleWebsiteDbContext _context;
        Calculations calculations = new Calculations();
        Keys keys = new Keys();
        public ModuleInformationController(MidnightPurpleWebsiteDbContext context)
        {
            _context = context;
        }
        // GET: ModuleInformation
        public ActionResult Index()
        {
            if (Request.Cookies.TryGetValue("UserID", out string IDString) && int.TryParse(IDString, out int UserID))//takes in the cookie sends it as a string then converts it to an int to be used when neccessarry
            {
                // Fetch the modules for the specific UserId
                var userData = _context.ModuleInformations
                .Where(info => info.UserId == UserID)//ensuring that it shows only date where the users id is the same as the person who logged in
                .ToList();
                return View(userData);//it should always show the user which logged in their data
            }
                return View();
        }
        // GET: ModuleInformation/Details/5
        public async Task<IActionResult> Details(int? id)
        {       
            return RedirectToAction("Create", "Notifications");//this redirects them to create a notification if the user wants to enter an alarm for themselves
        }
        // GET: ModuleInformation/Create
        public IActionResult Create()
        {
            return View();//takes you to the view where you can enter a modules information
        }
        // POST: ModuleInformation/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ModuleId,ModuleName,ModuleCode,NumberOfCredits,ClassHours,SelfStudyHoursPerWeek,UserId")] ModuleInformation moduleInformation)
        {//takes in all of that information and checks to ensure none of it is null otherwise the user must enter it again
            if(moduleInformation.ModuleName == null || moduleInformation.ModuleCode == null || moduleInformation.NumberOfCredits == null || moduleInformation.ClassHours == null)
            {
                ModelState.AddModelError(string.Empty, "Error, please enter the respected fields");//i throw a basic error just to notify the user to enter in the correct information
                return View(moduleInformation);
            }
            else if (ModelState.IsValid)
            {
                if (Request.Cookies.TryGetValue("UserID", out string IDString) && int.TryParse(IDString, out int userId))
                {
                    if (!keys.checkDates(userId))
                    {//first it ensures that the user has entered in the semesters information to calculate the self study hours per week
                        ModelState.AddModelError(string.Empty, "Please enter your Semesters Dates");
                        return View(moduleInformation);
                    }
                    moduleInformation.UserId = userId;//i set the user id to whoever logged in
                    int numberOfWeeks = calculations.GetWeeks(moduleInformation.UserId);//i get the number of weeks for that user
                    moduleInformation.SelfStudyHoursPerWeek = calculations.CalculateSelfStudy(moduleInformation.NumberOfCredits, numberOfWeeks, moduleInformation.ClassHours);//i calculate the number of self study hours per week as the user does not have to type this field in
                    if (!calculations.NegativeHours(moduleInformation.SelfStudyHoursPerWeek))
                    {//advice from part 1 i have included error handling for negative hours
                        ModelState.AddModelError(string.Empty, "Error, you cannot have negative hours for self studying hours per week");//if the value is negative i throw an error to warn the user
                        return View(moduleInformation);
                    }
                        _context.Add(moduleInformation);
                        await _context.SaveChangesAsync();//i save the module if it meets all the neccassry conditions
                        return RedirectToAction(nameof(Index));//then it takes them to the index page which shows all the information of all of that users modules they have entered
                }
            }
            return View(moduleInformation);
        }
    }
}
