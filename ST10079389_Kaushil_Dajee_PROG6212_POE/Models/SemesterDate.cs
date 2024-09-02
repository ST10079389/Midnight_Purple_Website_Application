using System.ComponentModel.DataAnnotations;
namespace ST10079389_Kaushil_Dajee_PROG6212_POE.Models;
public partial class SemesterDate
{//all the neccassry varaibles created when i scaffollded the database
    public int SemesterDatesId { get; set; }
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]//added this so it can only save the date no need for time
    public DateTime? SemesterStart { get; set; }
    public int? NumberOfWeeks { get; set; }
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]//added this so it can only save the date no need for time
    public DateTime? SemesterEnd { get; set; }
    public int? UserId { get; set; }
    public virtual User? User { get; set; }
}
