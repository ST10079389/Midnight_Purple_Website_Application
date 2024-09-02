using System.ComponentModel.DataAnnotations;
namespace ST10079389_Kaushil_Dajee_PROG6212_POE.Models;
public partial class Notification
{//all the neccassy variables created when i scaffolded the database
    public int NotificationID { get; set; }
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]//added this so it can save only date
    public DateTime? NotificationDate { get; set; }
    public int? UserId { get; set; }
    public int? ModuleId { get; set; }
    public virtual ModuleInformation? Module { get; set; }
    public virtual User? User { get; set; }
}
