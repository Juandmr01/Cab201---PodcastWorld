/// <summary>
/// Centralised string constants for all user-facing messages,
/// eliminating duplication across the codebase.
/// </summary>
public static class MessageBox
{
    /// <summary>Error shown when the supplied name is invalid.</summary>
    public const string InvalidName = "Supplied name is invalid";

    /// <summary>Error shown when the supplied age is invalid.</summary>
    public const string InvalidAge = "Supplied age is invalid";

    /// <summary>Error shown when the supplied mobile number is invalid.</summary>
    public const string InvalidMobileNumber = "Supplied mobile number is invalid";

    /// <summary>Error shown when the menu choice is out of range.</summary>
    public const string InvalidMenuChoice = "Supplied menu input is out of range";

    /// <summary>Error shown when the supplied email is invalid.</summary>
    public const string InvalidEmail = "Supplied email is invalid";

    /// <summary>Error shown when the supplied password is invalid.</summary>
    public const string InvalidPassword = "Supplied password is invalid";

    /// <summary>Error shown when the entered password does not match.</summary>
    public const string PasswordNotMatch = "Entered password does not match existing password";

    /// <summary>Error shown when the new password is the same as the old one.</summary>
    public const string SamePassword = "New password is the same as the existing password";

    /// <summary>Error shown when the supplied pay ID is invalid.</summary>
    public const string InvalidPayId = "Supplied pay ID is invalid";

    /// <summary>Error shown when the supplied episode name is invalid.</summary>
    public const string InvalidEpisodeName = "Supplied episode's name is invalid";

    /// <summary>Error shown when the supplied podcast name is invalid.</summary>
    public const string InvalidPodcastName = "Supplied podcast name is invalid";

    /// <summary>Error shown when the supplied podcast description is invalid.</summary>
    public const string InvalidPodcastDescription = "Supplied podcast description is invalid";

    /// <summary>Error shown when the supplied date is invalid.</summary>
    public const string InvalidDate = "Supplied date is invalid";

    /// <summary>Error shown when a numeric value is out of range.</summary>
    public const string OutOfRange = "Supplied value is out of range";

    /// <summary>Error shown when the supplied value is not a boolean.</summary>
    public const string InvalidBoolean = "Supplied value must be a boolean";

    /// <summary>Error shown when the email is already registered.</summary>
    public const string EmailAlreadyRegistered = "Email already registered";

    /// <summary>The decorative border used in the application header.</summary>
    public const string HeaderBorder = "=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+==";

    /// <summary>Prompt asking the user to choose from the menu.</summary>
    public const string MenuPrompt = "Please make a choice from the menu below.";

    /// <summary>Prompt asking for a choice between 1 and 3.</summary>
    public const string ChoiceBetween1And3 = "Please enter a choice between 1 and 3:";

    /// <summary>Prompt asking the user to enter their password.</summary>
    public const string EnterPassword = "Please enter your password:";

    /// <summary>Header displayed above user details.</summary>
    public const string DetailsHeader = "Your details.";

    /// <summary>Menu option text for viewing personal details.</summary>
    public const string MenuSeeDetails = "1. See my details.";

    /// <summary>Menu option text for changing password.</summary>
    public const string MenuChangePassword = "2. Change my password.";

    /// <summary>Menu option text for viewing all podcasts.</summary>
    public const string MenuViewAllPodcasts = "3. View all podcasts.";

    /// <summary>Menu option text for listening to a podcast episode.</summary>
    public const string MenuListen = "4. Listen to a podcast episode.";

    /// <summary>Error shown when no users are registered.</summary>
    public const string NoUsersRegistered = "There are no people registered";

    /// <summary>Error shown when the email is not registered.</summary>
    public const string EmailNotRegistered = "Email is not registered";

    /// <summary>Error shown when no podcasts exist.</summary>
    public const string NoPodcasts = "There are no podcasts";

    /// <summary>Error shown to listeners when a podcast has no episodes.</summary>
    public const string NoEpisodesListener = "The podcaster has not recorded any episodes";

    /// <summary>Error shown to podcasters when they have no episodes.</summary>
    public const string NoEpisodesPodcaster = "You have not recorded any episodes";

    /// <summary>Error shown when the user has already finished an episode.</summary>
    public const string AlreadyFinished = "You have already finished this episode and cannot listen to it again";

    /// <summary>Error shown when a regular listener tries to access a premium episode.</summary>
    public const string PremiumOnly = "This episode can only be view by premium listeners";

    /// <summary>Error shown when the user is not subscribed to any podcasts.</summary>
    public const string NotSubscribed = "You are not subscribed to any podcasts";
}
