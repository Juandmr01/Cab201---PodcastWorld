public interface IUserService
{
    User RegisterUser(string name, int age, string email, string mobileNumber, string password, int userType);
    User RegisterPremiumUser(string name, int age, string email, string mobileNumber, string password, DateTime registrationDate, int payId);
    User RegisterPodcaster(string name, int age, string email, string mobileNumber, string password, string podcastName, string podcastDescription, Genre podcastGenre);
    User AuthenticateUser(string email, string password);
    User ChangePassword(string email, string oldPassword, string newUserPassword);
    bool CheckUserExistence();
    bool CheckEmailExistence(string email);
    User GetUserByEmail(string email);
}
