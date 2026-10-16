/// <summary>
/// Abstract base class for all controllers, providing shared dependencies,
/// validation helpers, and the common episode listening flow.
/// </summary>
abstract class BaseController
{
    /// <summary>Menu choice constant for viewing personal details.</summary>
    protected const int SeeDetails = 1;

    /// <summary>Menu choice constant for changing password.</summary>
    protected const int ChangePassword = 2;

    /// <summary>The view layer for displaying menus and messages.</summary>
    protected readonly IAppMenu appMenu;

    /// <summary>Service for podcast operations.</summary>
    protected readonly IPodcastService podcastService;

    /// <summary>Service for listening and feedback operations.</summary>
    protected readonly IListeningService listeningService;

    /// <summary>
    /// Initialises the controller with the required service dependencies.
    /// </summary>
    protected BaseController(IAppMenu appMenu, IPodcastService podcastService, IListeningService listeningService)
    {
        this.appMenu = appMenu;
        this.podcastService = podcastService;
        this.listeningService = listeningService;
    }

    /// <summary>
    /// Repeatedly prompts for input until a valid value is provided.
    /// </summary>
    protected T GetValidatedInput<T>(Func<T> getValue, Func<T, bool> isValid, Action displayError)
    {
        T value = getValue();
        while (!isValid(value))
        {
            displayError();
            value = getValue();
        }
        return value;
    }

    /// <summary>
    /// Handles the full listen-to-episode flow: episode selection,
    /// date and minutes input, and recording the listening session.
    /// </summary>
    protected bool HandleListenFlow(User user, Podcast selectedPodcast, bool checkPremium)
    {
        if (selectedPodcast.Episodes.Count == 0)
        {
            appMenu.DisplayErrorMessage(MessageBox.NoEpisodesListener);
            return false;
        }

        appMenu.DisplayPodcastDetails(selectedPodcast);
        appMenu.DisplayEpisodeListFull(selectedPodcast.Episodes);

        int episodeChoice = GetEpisodeSelection(selectedPodcast.Episodes.Count);
        if (episodeChoice == 0) return false;

        Episode selectedEpisode = selectedPodcast.Episodes[episodeChoice - 1];

        ListeningRecord record = listeningService.GetListeningRecord(user, selectedEpisode);
        if (record != null && record.Completed)
        {
            appMenu.DisplayErrorMessage(MessageBox.AlreadyFinished);
            return false;
        }

        if (checkPremium && selectedEpisode.PremmiumEpisode && user is not PremiumListener)
        {
            appMenu.DisplayErrorMessage(MessageBox.PremiumOnly);
            return false;
        }

        int alreadyListened = record != null ? record.MinutesListened : 0;
        int remaining = selectedEpisode.EpisodeLength - alreadyListened;

        if (record != null)
        {
            appMenu.DisplayListeningProgress(selectedPodcast.Title, selectedEpisode.Name,
                record.MinutesListened, record.LastListenDate, remaining);
        }

        DateTime requiredDate = record != null ? record.LastListenDate : selectedEpisode.RecordedDate.Value;
        DateTime listenDate = GetValidatedListenDate(requiredDate);
        int minutesListened = GetValidatedListenMinutes(remaining);

        RecordAndDisplayListening(user, selectedPodcast, selectedEpisode, minutesListened, listenDate);
        return true;
    }

    /// <summary>
    /// Prompts for an episode number, validating the selection is in range.
    /// Returns 0 if the user cancels.
    /// </summary>
    private int GetEpisodeSelection(int episodeCount)
    {
        int episodeChoice;
        do
        {
            episodeChoice = appMenu.GetEpisodeMenuChoice();
            if (episodeChoice == 0) break;
            if (episodeChoice < 0 || episodeChoice > episodeCount)
                appMenu.DisplayRetryError(MessageBox.OutOfRange);
        } while (episodeChoice < 0 || episodeChoice > episodeCount);
        return episodeChoice;
    }

    /// <summary>
    /// Prompts for a listen date, ensuring it is valid and after the required date.
    /// </summary>
    private DateTime GetValidatedListenDate(DateTime requiredDate)
    {
        DateTime? listenDate;
        do
        {
            listenDate = appMenu.GetListenDate();
            if (listenDate == null)
            {
                appMenu.DisplayRetryError(MessageBox.InvalidDate);
            }
            else if (listenDate.Value.Date < requiredDate.Date)
            {
                appMenu.DisplayRetryError($"Supplied date must be after {requiredDate.ToString("d/M/yyyy")}");
                listenDate = null;
            }
        } while (listenDate == null);
        return listenDate.Value;
    }

    /// <summary>
    /// Prompts for minutes listened, ensuring the value is between 1 and the remaining time.
    /// </summary>
    private int GetValidatedListenMinutes(int remaining)
    {
        int minutesListened;
        do
        {
            minutesListened = appMenu.GetListenMinutes();
            if (minutesListened < 1 || minutesListened > remaining)
                appMenu.DisplayRetryError(MessageBox.OutOfRange);
        } while (minutesListened < 1 || minutesListened > remaining);
        return minutesListened;
    }

    /// <summary>
    /// Records the listening session and displays the outcome.
    /// </summary>
    private void RecordAndDisplayListening(User user, Podcast selectedPodcast, Episode selectedEpisode, int minutesListened, DateTime listenDate)
    {
        ListeningRecord updatedRecord = listeningService.ListenToEpisode(
            user, selectedPodcast, selectedEpisode, minutesListened, listenDate);

        if (updatedRecord.Completed)
        {
            appMenu.DisplayEpisodeFinished(user.Name, selectedEpisode.Name);
        }
        else
        {
            int newRemaining = selectedEpisode.EpisodeLength - updatedRecord.MinutesListened;
            appMenu.DisplayListeningProgressTotal(selectedPodcast.Title, selectedEpisode.Name,
                updatedRecord.MinutesListened, listenDate, newRemaining);
        }
    }
}
