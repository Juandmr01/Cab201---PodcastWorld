public interface IAppMenu
{
    // Header
    void DisplayHeader();

    // Menus
    int DisplayMenuOptions();
    int DisplayRegisterOptions();
    void DisplayLoginMenu();
    int DisplayRegularListenerMenuOptions();
    int DisplayPremiumListenerMenuOptions();
    int DisplayPodcasterMenu();
    int DisplayPodcastGenre();

    // Input prompts
    string GetName();
    int GetAge();
    string GetMobileNumber();
    string GetEmail();
    string GetPassword();
    string GetNewPassword();
    string GetChangedPassword();
    string GetCurrentPassword();
    DateTime? GetRegistrationDate();
    int GetPayId();
    string GetPodcastName();
    string GetPodcastDescription();
    string GetEpisodeName();
    DateTime? GetEpisodeRecordedDate();
    int GetEpisodeLength();
    string GetPremiumEpisodeInput();
    int GetGenreChoiceReprompt(int genreCount);
    int GetPodcastMenuChoice();
    int GetSubscribeMenuChoice();
    int GetUnsubscribeMenuChoice();
    int GetEpisodeMenuChoice();
    DateTime? GetListenDate();
    int GetListenMinutes();
    int GetFeedbackEpisodeMenuChoice();
    bool GetThumbsUp();
    string GetFeedbackText();
    int GetFeedbackViewMenuChoice();

    // Generic error display
    void DisplayRetryError(string message);
    void DisplayErrorMessage(string message);

    // Status and confirmation messages
    void DisplayPremiumRegisteration();
    void DisplayRegularRegisteration();
    void DisplayPodcasterRegistration();
    void DisplayCongratulationsMessage(string name);
    void DisplayCongratulationsMessagePremium(string name);
    void DisplayCongratsPD(string name);
    void DisplayPasswordUpdateSuccess();
    void DisplayExit();

    // Welcome and account details
    void DisplayWelcomeBack(string name);
    void DisplayUserDetails(User user, int episodesStarted, int episodesCompleted, int totalMinutes, int advertisements);
    void DisplayPremiumUserDetails(PremiumListener premiumListener, int episodesStarted, int episodesCompleted, int totalMinutes);
    void DisplayPodcasterDetails(Podcaster podcaster);

    // Podcast and episode display
    void DisplayPodcastList(string header, List<Podcast> podcasts);
    void DisplayEpisodeList(IReadOnlyList<Episode> episodes);
    void DisplayEpisodeListFull(IReadOnlyList<Episode> episodes);
    void DisplayPodcastDetails(Podcast podcast);

    // Subscription display
    void DisplaySubscriptedHeader();
    void DisplayNotSubscribed();
    void DisplayAvailableHeader();
    void DisplaySubscribedToAll();
    void DisplaySubscribeSuccess(string podcastTitle);
    void DisplayUnsubscribeSuccess(string podcastTitle);

    // Listening display
    void DisplayEpisodeFinished(string userName, string episodeName);
    void DisplayListeningProgress(string podcastName, string episodeName, int totalMinutes, DateTime lastDate, int remaining);
    void DisplayListeningProgressTotal(string podcastName, string episodeName, int totalMinutes, DateTime lastDate, int remaining);

    // Feedback display
    void DisplayStatistics(IReadOnlyList<Episode> episodes, List<int> listenersStarted, List<int> listenersCompleted);
    void DisplayFeedbackSummary(IReadOnlyList<Episode> episodes, List<int> thumbsUpCounts);
    void DisplayFeedbackTexts(List<string> feedbackTexts);
}
