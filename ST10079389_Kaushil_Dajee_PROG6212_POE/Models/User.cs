namespace ST10079389_Kaushil_Dajee_PROG6212_POE.Models;
public partial class User
{//all the neccessary variables created when i scaffolded this from the database for users to login and register and account
    public int UserId { get; set; }
    public string? Name { get; set; }
    public string? Password { get; set; }
    public virtual ICollection<ModuleInformation> ModuleInformations { get; set; } = new List<ModuleInformation>();
    public virtual ICollection<SemesterDate> SemesterDates { get; set; } = new List<SemesterDate>();
    public virtual ICollection<StudyRecord> StudyRecords { get; set; } = new List<StudyRecord>();
}
