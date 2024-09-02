namespace ST10079389_Kaushil_Dajee_PROG6212_POE.Models
{
    public partial class HoursViewModel//this model was created so i could display the correct information for the study records when they wanted to see how many hours were left
    {
        public IEnumerable<ModuleInformation> moduleInformation { get; set; }
        public IEnumerable<StudyRecord> studyRecords { get; set; }
    }
}
