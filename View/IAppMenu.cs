/// <summary>
/// Defines the contract for the application's user interface,
/// including menus, input prompts, and display methods.
/// </summary>
public interface IAppMenu
{
    /// <summary>Displays the application header banner.</summary>
    void DisplayHeader();

    /// <summary>Displays the main menu and returns the user's choice.</summary>
    int DisplayMenuOptions();

    /// <summary>Displays the registration type menu and returns the user's choice.</summary>
    int DisplayRegisterOptions();

    /// <summary>Displays the login menu header.</summary>
    void DisplayLoginMenu();

    /// <summary>Displays the regular listener menu and returns the user's choice.</summary>
    int DisplayRegularListenerMenuOptions();

    /// <summary>Displays the premium listener menu and returns the user's choice.</summary>
    int DisplayPremiumListenerMenuOptions();

    /// <summary>Displays the podcaster menu and returns the user's choice.</summary>
    int DisplayPodcasterMenu();

    /// <summary>Displays the podcast genre selection menu and returns the user's choice.</summary>
    int DisplayPodcastGenre();

    /// <summary>Prompts the user for their name and returns it.</summary>
    string GetName();

    /// <summary>Prompts the user for their age and returns it.</summary>
    int GetAge();

    /// <summary>Prompts the user for their mobile number and returns it.</summary>
    string GetMobileNumber();

    /// <summary>Prompts the user for their email address and returns it.</summary>
    string GetEmail();

    /// <summary>Prompts the user for a new password (with requirements) and returns it.</summary>
    string GetPassword();

    /// <summary>Prompts the user for their password (without requirements) and returns it.</summary>
    string GetNewPassword();

    /// <summary>Prompts the user for a new replacement password and returns it.</summary>
    string GetChangedPassword();

    /// <summary>Prompts the user for their current password and returns it.</summary>
    string GetCurrentPassword();

    /// <summary>Prompts the user for a registration date and returns it.</summary>
    DateTime? GetRegistrationDate();

    /// <summary>Prompts the user for their pay ID and returns it.</summary>
    int GetPayId();

    /// <summary>Prompts the user for a podcast name and returns it.</summary>
    string GetPodcastName();

    /// <summary>Prompts the user for a podcast description and returns it.</summary>
    string GetPodcastDescription();

    /// <summary>Prompts the user for an episode name and returns it.</summary>
    string GetEpisodeName();

    /// <summary>Prompts the user for an episode recorded date and returns it.</summary>
    DateTime? GetEpisodeRecordedDate();

    /// <summary>Prompts the user for an episode length in minutes and returns it.</summary>
    int GetEpisodeLength();

    /// <summary>Prompts the user for whether the episode is premium-only and returns the input.</summary>
    string GetPremiumEpisodeInput();

    /// <summary>Re-prompts the user for a genre choice and returns it.</summary>
    int GetGenreChoiceReprompt(int genreCount);

    /// <summary>Prompts the user to select a podcast by number and returns the choice.</summary>
    int GetPodcastMenuChoice();

    /// <summary>Prompts the user to select a podcast to subscribe to and returns the choice.</summary>
    int GetSubscribeMenuChoice();

    /// <summary>Prompts the user to select a podcast to unsubscribe from and returns the choice.</summary>
    int GetUnsubscribeMenuChoice();

    /// <summary>Prompts the user to select an episode to listen to and returns the choice.</summary>
    int GetEpisodeMenuChoice();

    /// <summary>Prompts the user for the date they listened and returns it.</summary>
    DateTime? GetListenDate();

    /// <summary>Prompts the user for how many minutes they listened and returns it.</summary>
    int GetListenMinutes();

    /// <summary>Prompts the user to select an episode for feedback and returns the choice.</summary>
    int GetFeedbackEpisodeMenuChoice();

    /// <summary>Prompts the user for a thumbs-up or thumbs-down rating and returns it.</summary>
    bool GetThumbsUp();

    /// <summary>Prompts the user for text feedback and returns it.</summary>
    string GetFeedbackText();

    /// <summary>Prompts the user to select an episode to view feedback for and returns the choice.</summary>
    int GetFeedbackViewMenuChoice();

    /// <summary>Displays an error message followed by "Please try again".</summary>
    void DisplayRetryError(string message);

    /// <summary>Displays an error message without a retry prompt.</summary>
    void DisplayErrorMessage(string message);

    /// <summary>Displays the premium registration header.</summary>
    void DisplayPremiumRegisteration();

    /// <summary>Displays the regular listener registration header.</summary>
    void DisplayRegularRegisteration();

    /// <summary>Displays the podcaster registration header.</summary>
    void DisplayPodcasterRegistration();

    /// <summary>Displays a congratulations message for regular listener registration.</summary>
    void DisplayCongratulationsMessage(string name);

    /// <summary>Displays a congratulations message for premium listener registration.</summary>
    void DisplayCongratulationsMessagePremium(string name);

    /// <summary>Displays a congratulations message for podcaster registration.</summary>
    void DisplayCongratsPD(string name);

    /// <summary>Displays a message confirming the password was updated.</summary>
    void DisplayPasswordUpdateSuccess();

    /// <summary>Displays the application exit message.</summary>
    void DisplayExit();

    /// <summary>Displays a welcome-back greeting for the logged-in user.</summary>
    void DisplayWelcomeBack(string name);

    /// <summary>Displays a regular listener's personal details and listening statistics.</summary>
    void DisplayUserDetails(User user, int episodesStarted, int episodesCompleted, int totalMinutes, int advertisements);

    /// <summary>Displays a premium listener's personal details and listening statistics.</summary>
    void DisplayPremiumUserDetails(PremiumListener premiumListener, int episodesStarted, int episodesCompleted, int totalMinutes);

    /// <summary>Displays a podcaster's personal and podcast details.</summary>
    void DisplayPodcasterDetails(Podcaster podcaster);

    /// <summary>Displays a numbered list of podcasts with a header.</summary>
    void DisplayPodcastList(string header, List<Podcast> podcasts);

    /// <summary>Displays a podcaster's episode list in short format.</summary>
    void DisplayEpisodeList(IReadOnlyList<Episode> episodes);

    /// <summary>Displays an episode list with full details including descriptions.</summary>
    void DisplayEpisodeListFull(IReadOnlyList<Episode> episodes);

    /// <summary>Displays detailed information about a single podcast.</summary>
    void DisplayPodcastDetails(Podcast podcast);

    /// <summary>Displays the subscribed podcasts section header.</summary>
    void DisplaySubscriptedHeader();

    /// <summary>Displays a message indicating the user has no subscriptions.</summary>
    void DisplayNotSubscribed();

    /// <summary>Displays the available podcasts section header.</summary>
    void DisplayAvailableHeader();

    /// <summary>Displays a message indicating the user is subscribed to all podcasts.</summary>
    void DisplaySubscribedToAll();

    /// <summary>Displays a confirmation message after subscribing to a podcast.</summary>
    void DisplaySubscribeSuccess(string podcastTitle);

    /// <summary>Displays a confirmation message after unsubscribing from a podcast.</summary>
    void DisplayUnsubscribeSuccess(string podcastTitle);

    /// <summary>Displays a message indicating the user finished an episode.</summary>
    void DisplayEpisodeFinished(string userName, string episodeName);

    /// <summary>Displays the user's listening progress for an episode.</summary>
    void DisplayListeningProgress(string podcastName, string episodeName, int totalMinutes, DateTime lastDate, int remaining);

    /// <summary>Displays the user's total listening progress for an episode.</summary>
    void DisplayListeningProgressTotal(string podcastName, string episodeName, int totalMinutes, DateTime lastDate, int remaining);

    /// <summary>Displays episode listening statistics for a podcaster.</summary>
    void DisplayStatistics(IReadOnlyList<Episode> episodes, List<int> listenersStarted, List<int> listenersCompleted);

    /// <summary>Displays a feedback summary with thumbs-up counts per episode.</summary>
    void DisplayFeedbackSummary(IReadOnlyList<Episode> episodes, List<int> thumbsUpCounts);

    /// <summary>Displays a list of text feedback entries.</summary>
    void DisplayFeedbackTexts(List<string> feedbackTexts);
}
