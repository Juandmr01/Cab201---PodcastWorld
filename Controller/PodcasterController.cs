/// <summary>
/// Handles the menu loop and actions for podcasters.
/// </summary>
class PodcasterController : BaseController
{
    private const int CreateEpisode = 3;
    private const int ViewEpisodes = 4;
    private const int PodcasterStatistics = 5;
    private const int PodcasterViewFeedback = 6;
    private const int PodcasterLogOut = 7;

    /// <summary>
    /// Initialises the podcaster controller with the required service dependencies.
    /// </summary>
    public PodcasterController(IAppMenu appMenu, IPodcastService podcastService, IListeningService listeningService)
        : base(appMenu, podcastService, listeningService) { }

    /// <summary>
    /// Runs the podcaster menu loop until the user logs out.
    /// </summary>
    public void Run(Podcaster podcasterUser)
    {
        appMenu.DisplayWelcomeBack(podcasterUser.Name);
        int choice;
        do
        {
            choice = appMenu.DisplayPodcasterMenu();
            if (choice == SeeDetails)
                HandleSeeDetails(podcasterUser);
            else if (choice == CreateEpisode)
                HandleCreateEpisode(podcasterUser);
            else if (choice == ViewEpisodes)
                HandleViewEpisodes(podcasterUser);
            else if (choice == PodcasterStatistics)
                HandleStatistics(podcasterUser);
            else if (choice == PodcasterViewFeedback)
                HandleViewFeedback(podcasterUser);
            else if (choice != PodcasterLogOut && choice != ChangePassword)
                appMenu.DisplayRetryError(MessageBox.InvalidMenuChoice);
        } while (choice != PodcasterLogOut);
    }

    /// <summary>
    /// Displays the podcaster's personal and podcast details.
    /// </summary>
    private void HandleSeeDetails(Podcaster podcasterUser)
    {
        appMenu.DisplayPodcasterDetails(podcasterUser);
    }

    /// <summary>
    /// Collects and validates episode details, then registers the new episode.
    /// </summary>
    private void HandleCreateEpisode(Podcaster podcasterUser)
    {
        string episodeName = GetValidatedInput(
            () => appMenu.GetEpisodeName(),
            a => Episode.IsValidEpisodeName(a),
            () => appMenu.DisplayRetryError(MessageBox.InvalidEpisodeName)
        );

        string episodeDescription = GetValidatedInput(
            () => appMenu.GetPodcastDescription(),
            a => Episode.IsValidDescription(a),
            () => appMenu.DisplayRetryError(MessageBox.InvalidPodcastDescription)
        );

        DateTime? episodeRecorded = GetValidatedInput(
            () => appMenu.GetEpisodeRecordedDate(),
            a => Episode.IsValidRecordedDate(a),
            () => appMenu.DisplayRetryError(MessageBox.InvalidDate)
        );

        int episodeLength = GetValidatedInput(
            () => appMenu.GetEpisodeLength(),
            a => Episode.IsValidEpisodeLength(a),
            () => appMenu.DisplayRetryError(MessageBox.OutOfRange)
        );

        string premiumInput = GetValidatedInput(
            () => appMenu.GetPremiumEpisodeInput(),
            a => bool.TryParse(a, out _),
            () => appMenu.DisplayRetryError(MessageBox.InvalidBoolean)
        );
        bool premiumEpisode = bool.Parse(premiumInput);

        Podcast podcast = podcasterUser.Podcasts[0];
        podcastService.RegisterEpisode(podcast, episodeName, episodeDescription,
            episodeRecorded, episodeLength, premiumEpisode);
    }

    /// <summary>
    /// Displays the podcaster's episode list.
    /// </summary>
    private void HandleViewEpisodes(Podcaster podcasterUser)
    {
        IReadOnlyList<Episode> episodes = podcasterUser.Podcasts[0].Episodes;
        if (episodes.Count == 0)
            appMenu.DisplayErrorMessage(MessageBox.NoEpisodesPodcaster);
        else
            appMenu.DisplayEpisodeList(episodes);
    }

    /// <summary>
    /// Displays listening statistics for the podcaster's episodes.
    /// </summary>
    private void HandleStatistics(Podcaster podcasterUser)
    {
        IReadOnlyList<Episode> episodes = podcasterUser.Podcasts[0].Episodes;
        if (episodes.Count == 0)
        {
            appMenu.DisplayErrorMessage(MessageBox.NoEpisodesPodcaster);
        }
        else
        {
            List<int> started = episodes.Select(e => listeningService.GetListenersStarted(e)).ToList();
            List<int> completed = episodes.Select(e => listeningService.GetListenersCompleted(e)).ToList();
            appMenu.DisplayStatistics(episodes, started, completed);
        }
    }

    /// <summary>
    /// Displays feedback summaries and allows viewing individual feedback texts.
    /// </summary>
    private void HandleViewFeedback(Podcaster podcasterUser)
    {
        IReadOnlyList<Episode> episodes = podcasterUser.Podcasts[0].Episodes;
        if (episodes.Count == 0)
        {
            appMenu.DisplayErrorMessage(MessageBox.NoEpisodesPodcaster);
        }
        else
        {
            List<int> thumbsUp = episodes.Select(e => listeningService.GetThumbsUpCount(e)).ToList();
            appMenu.DisplayFeedbackSummary(episodes, thumbsUp);
            int episodeChoice = appMenu.GetFeedbackViewMenuChoice();
            if (episodeChoice > 0 && episodeChoice <= episodes.Count)
            {
                List<Feedback> feedbacks = listeningService.GetFeedbackForEpisode(episodes[episodeChoice - 1]);
                List<string> texts = feedbacks.Select(f => f.TextFeedback).ToList();
                appMenu.DisplayFeedbackTexts(texts);
            }
        }
    }
}
