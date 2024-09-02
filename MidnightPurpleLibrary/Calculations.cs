using System.Data.SqlClient;
namespace MidnightPurpleLibrary
{
    public class Calculations
    {
        public DateTime? EndSemester(DateTime? semesterStart, int? numberOfWeeks)
        {//takes in the semester start date and number of weeks to calculate the semesters ending date
            if (semesterStart.HasValue && numberOfWeeks.HasValue)
            {
                DateTime semesterEnd = semesterStart.Value.AddDays(7 * numberOfWeeks.Value);
                return semesterEnd;
            }
            else
            {
                // Handle the case where semesterStart or numberOfWeeks is null
                return null;
            }
        }
        public int GetWeeks(int? UserID)
        {
            //method used to query the number of weeks the user enter this is why i use the user id as a parameter
            string sqlConnection = "Data Source=.;Initial Catalog=Midnight_Purple_WebsiteDB;Integrated Security=True";
            int numberOfWeeks;
            using (SqlConnection connection = new SqlConnection(sqlConnection))
            {
                connection.Open();
                string query = "SELECT NumberOfWeeks FROM SemesterDates WHERE UserID = @UserID";//the paramter is used for this query as i need the number of weeks to calculated the self study hours per week
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserID", UserID);//parameter used and checks it
                    numberOfWeeks = Convert.ToInt32(command.ExecuteScalar());//gets the jumber of weeks
                }
            }
            return numberOfWeeks;//then i return the number of weeks
        }
        public int? CalculateSelfStudy(int? numberOfCredits, int? numberOfWeeks, int? classHours)
        {//used to calculate the self study hours per week
            if (numberOfCredits.HasValue && numberOfWeeks.HasValue && classHours.HasValue)
            {
                int selfStudyHoursWeek = (int)((numberOfCredits.Value * 10) / numberOfWeeks.Value) - classHours.Value;//checks if none of the values are null and then returns the value
                return selfStudyHoursWeek;
            }
            else
            {
                // Handle the case where numberOfCredits, numberOfWeeks, or classHours is null
                return null; // Or return another value that indicates an error or an undefined result
            }
        }

        public bool NegativeHours(int? selfStudyHoursWeek)
        {//I have followed your advice from Part 1 and added a negative hours error handling exception to be called if the user has negative hours for self study
            if (selfStudyHoursWeek < 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}

