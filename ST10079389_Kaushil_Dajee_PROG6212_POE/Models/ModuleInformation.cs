namespace ST10079389_Kaushil_Dajee_PROG6212_POE.Models;
public partial class ModuleInformation
{
    //all the neccessary variables to save a module
    public int ModuleId { get; set; }
    public string? ModuleName { get; set; }
    public string? ModuleCode { get; set; }
    public int? NumberOfCredits { get; set; }
    public int? ClassHours { get; set; }
    public int? SelfStudyHoursPerWeek { get; set; }
    public int? UserId { get; set; }
    public virtual ICollection<StudyRecord> StudyRecords { get; set; } = new List<StudyRecord>();
    public virtual User? User { get; set; }
}
