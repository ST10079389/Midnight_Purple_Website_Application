using System.Data;
using System.Data.SqlClient;
namespace MidnightPurpleLibrary;
public class Keys
{
    public int GetID(string username, string password)
    {//i have to pass userID everywhere so that only the one user can see their own information no one elses
        string sqlConnection = "Data Source=.;Initial Catalog=Midnight_Purple_WebsiteDB;Integrated Security=True";
        int userID;
        using (SqlConnection connection = new SqlConnection(sqlConnection))
        {
            connection.Open();
            string query = "SELECT UserID FROM Users WHERE Name = @Username AND Password = @Password";//query used to get the users id
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Username", username);
                command.Parameters.AddWithValue("@Password", password);//parameters to check it
                userID = Convert.ToInt32(command.ExecuteScalar());
            }
        }
        return userID;//this is used so when any user enters their information for a module, a notification or a study session the user id is saved to basically ensure its for only that user
    }
    public string NotifyUser(int? UserID)
    {//this method is used to notify the user whenever they must study today or not
        string sqlConnection = "Data Source=.;Initial Catalog=Midnight_Purple_WebsiteDB;Integrated Security=True";
        string message = "";
        DateTime currentDate = DateTime.Now.Date;
        SqlConnection connection = new SqlConnection(sqlConnection);
        string query = "SELECT NotificationDate, ModuleInformation.Module_Name AS [NAME] FROM Notification Join ModuleInformation ON Notification.ModuleID = ModuleInformation.ModuleID WHERE Notification.UserID = @UserID AND NotificationDate = @NotificationDate";//query used so we can notify the coreect user if they set a notification
        connection.Open();
        SqlCommand command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("@UserID", UserID);
        command.Parameters.AddWithValue("@NotificationDate", currentDate);//takes in the userid, and todays date as a parameter so when the user logs in it shows their notification on the day
        SqlDataReader reader = command.ExecuteReader();
        if (reader.HasRows)
        {
            while (reader.Read())
            {
                string NotificationDate = reader["NotificationDate"].ToString();
                string[] date = NotificationDate.Split(' ');
                string dateOnly = date[0];
                //finds all the modules so the user can select the module instead of typing it with ease
                string Name = reader["NAME"].ToString();
                message = $"You have to study on the {dateOnly} for {Name}";//returns ths message if their is a notification          
            }
        }
        else
        {
            message = null;//else null the reason i set it as null is so that the alert thinks the message is null so it does not display anything
        }
        reader.Close();
        connection.Close();
        return message;
    }
    public bool checkDates(int UserID)
    {//this method is important as it ensures the user doesnt enter multiple semester dates
        string sqlConnection = "Data Source=.;Initial Catalog=Midnight_Purple_WebsiteDB;Integrated Security=True";
        bool valid = false;
        using (SqlConnection connection = new SqlConnection(sqlConnection))
        {
            connection.Open();
            string query = "SELECT SemesterDatesID FROM SemesterDates WHERE UserID = @UserID";//a query to check that the user has not entered multiple semester dates
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@UserID", UserID);//my parameters always take the userID
                SqlDataAdapter sda = new SqlDataAdapter(command);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    valid = true;
                }
                else
                {
                    valid = false;
                }
            }
        }
        return valid;
    }
}
