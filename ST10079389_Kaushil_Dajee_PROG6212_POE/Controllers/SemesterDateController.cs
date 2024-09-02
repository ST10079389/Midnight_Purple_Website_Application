using Microsoft.AspNetCore.Mvc;
using MidnightPurpleLibrary;
using ST10079389_Kaushil_Dajee_PROG6212_POE.Models;
namespace ST10079389_Kaushil_Dajee_PROG6212_POE.Controllers
{
    public class SemesterDateController : Controller
    {
        private readonly MidnightPurpleWebsiteDbContext _context;
        Calculations calculations = new Calculations();
        Keys keys = new Keys();
        public SemesterDateController(MidnightPurpleWebsiteDbContext context)
        {
            _context = context;
        }
        // GET: SemesterDate
        public ActionResult Index()
        {
            if (Request.Cookies.TryGetValue("UserID", out string userIdString) && int.TryParse(userIdString, out int UserID))//Takes in the cookie and passes it as a string then an int
            {
                //displays the semester which has the same user id to prevent it from showing other users other peoples semesters
                var userData = _context.SemesterDates
                .Where(info => info.UserId == UserID)
                .ToList();
                return View(userData);
            }
                return View();
        }
        // GET: SemesterDate/Create
        public IActionResult Create()
        {
            return View();//just shows the basic create page
        }
        // POST: SemesterDate/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SemesterDatesId,SemesterStart,NumberOfWeeks,SemesterEnd,UserId")] SemesterDate semesterDate)
        {
            if (semesterDate.SemesterStart == null || semesterDate.NumberOfWeeks == null)
            {
                ModelState.AddModelError(string.Empty, "Invalid, please enter neccessary fields");
                return View();//ensure that none of the fields are left as null otherwise an error will be shown
            }

            if (ModelState.IsValid)
            {
                if (Request.Cookies.TryGetValue("UserID", out string userIdString) && int.TryParse(userIdString, out int UserID))//takes in the cookie makes a string then an int
                {
                    if (keys.checkDates(UserID))
                    {
                        ModelState.AddModelError(string.Empty, "Error, you have already inserted your semester date");//checks to see that the user does not have a semester date already
                        return View(semesterDate);
                    }
                    else
                    {
                        semesterDate.UserId = UserID;//setting the user id 
                        semesterDate.SemesterEnd = calculations.EndSemester(semesterDate.SemesterStart, semesterDate.NumberOfWeeks);//calculating the semters end date 
                        _context.Add(semesterDate);
                        await _context.SaveChangesAsync();//saving it to the database
                        return RedirectToAction(nameof(Index));//redirecting them to the index page
                    }                   
                }
            }
            return View();
        }
    }
}
