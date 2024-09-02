using System.ComponentModel.DataAnnotations;
namespace ST10079389_Kaushil_Dajee_PROG6212_POE.Models;
public partial class StudyRecord
{//all the neccessary variables created when i scffolded from the database for the user to save their study session
    public int StudyRecordId { get; set; }
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    public DateTime? StudyDates { get; set; }
    public int? HoursStudied { get; set; }
    public int? UserId { get; set; }
    public int? ModuleId { get; set; }
    public virtual ModuleInformation? Module { get; set; }
    public virtual User? User { get; set; }
}
