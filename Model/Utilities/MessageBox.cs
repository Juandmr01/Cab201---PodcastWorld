public static class MessageBox
{
    // Retry errors (shown with "Please try again")
    public const string InvalidName = "Supplied name is invalid";
    public const string InvalidAge = "Supplied age is invalid";
    public const string InvalidMobileNumber = "Supplied mobile number is invalid";
    public const string InvalidMenuChoice = "Supplied menu input is out of range";
    public const string InvalidEmail = "Supplied email is invalid";
    public const string InvalidPassword = "Supplied password is invalid";
    public const string PasswordNotMatch = "Entered password does not match existing password";
    public const string SamePassword = "New password is the same as the existing password";
    public const string InvalidPayId = "Supplied pay ID is invalid";
    public const string InvalidEpisodeName = "Supplied episode's name is invalid";
    public const string InvalidPodcastName = "Supplied podcast name is invalid";
    public const string InvalidPodcastDescription = "Supplied podcast description is invalid";
    public const string InvalidDate = "Supplied date is invalid";
    public const string OutOfRange = "Supplied value is out of range";
    public const string InvalidBoolean = "Supplied value must be a boolean";
    public const string EmailAlreadyRegistered = "Email already registered";

    // Shared UI text (reused across more than one screen)
    public const string HeaderBorder = "=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+==";
    public const string MenuPrompt = "Please make a choice from the menu below.";
    public const string ChoiceBetween1And3 = "Please enter a choice between 1 and 3:";
    public const string EnterPassword = "Please enter your password:";
    public const string DetailsHeader = "Your details.";
    public const string MenuSeeDetails = "1. See my details.";
    public const string MenuChangePassword = "2. Change my password.";
    public const string MenuViewAllPodcasts = "3. View all podcasts.";
    public const string MenuListen = "4. Listen to a podcast episode.";

    // Terminal errors (shown without "Please try again")
    public const string NoUsersRegistered = "There are no people registered";
    public const string EmailNotRegistered = "Email is not registered";
    public const string NoPodcasts = "There are no podcasts";
    public const string NoEpisodesListener = "The podcaster has not recorded any episodes";
    public const string NoEpisodesPodcaster = "You have not recorded any episodes";
    public const string AlreadyFinished = "You have already finished this episode and cannot listen to it again";
    public const string PremiumOnly = "This episode can only be view by premium listeners";
    public const string NotSubscribed = "You are not subscribed to any podcasts";
}
