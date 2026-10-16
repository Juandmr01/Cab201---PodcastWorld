/// <summary>
/// Defines the contract for listening records, feedback, and episode statistics.
/// </summary>
public interface IListeningService
{
    /// <summary>Returns the listening record for a user and episode, or null if none.</summary>
    ListeningRecord GetListeningRecord(User user, Episode episode);

    /// <summary>Records a listening session and returns the updated record.</summary>
    ListeningRecord ListenToEpisode(User user, Podcast podcast, Episode episode, int minutesListened, DateTime listenDate);

    /// <summary>Adds feedback from a user for an episode.</summary>
    void AddFeedback(User user, Episode episode, bool thumbsUp, string textFeedback);

    /// <summary>Returns all feedback entries for an episode.</summary>
    List<Feedback> GetFeedbackForEpisode(Episode episode);

    /// <summary>Returns the number of thumbs-up ratings for an episode.</summary>
    int GetThumbsUpCount(Episode episode);

    /// <summary>Returns the number of listeners who started an episode.</summary>
    int GetListenersStarted(Episode episode);

    /// <summary>Returns the number of listeners who completed an episode.</summary>
    int GetListenersCompleted(Episode episode);

    /// <summary>Returns the number of episodes a user has started listening to.</summary>
    int GetEpisodesStarted(User user);

    /// <summary>Returns the number of episodes a user has completed.</summary>
    int GetEpisodesCompleted(User user);

    /// <summary>Returns the total minutes a user has listened across all episodes.</summary>
    int GetTotalMinutesListened(User user);

    /// <summary>Returns the number of advertisements a user has heard.</summary>
    int GetAdvertisementsListened(User user);
}
