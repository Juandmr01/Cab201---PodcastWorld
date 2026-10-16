/// <summary>
/// Manages listening records, feedback, and episode statistics.
/// </summary>
public class ListeningRepository : IListeningService
{
    private readonly List<ListeningRecord> ListeningRecords = new List<ListeningRecord>();
    private readonly List<Feedback> Feedbacks = new List<Feedback>();

    /// <summary>
    /// Returns the listening record for a user and episode, or null if none exists.
    /// </summary>
    public ListeningRecord GetListeningRecord(User user, Episode episode)
    {
        return ListeningRecords.FirstOrDefault(r => r.Listener == user && r.Episode == episode);
    }

    /// <summary>
    /// Records a listening session and returns the updated record.
    /// </summary>
    public ListeningRecord ListenToEpisode(User user, Podcast podcast, Episode episode, int minutesListened, DateTime listenDate)
    {
        ListeningRecord record = GetListeningRecord(user, episode);
        if (record == null)
        {
            record = new ListeningRecord(user, podcast, episode);
            ListeningRecords.Add(record);
        }
        record.UpdateProgress(minutesListened, listenDate);
        return record;
    }

    /// <summary>
    /// Adds feedback from a user for an episode.
    /// </summary>
    public void AddFeedback(User user, Episode episode, bool thumbsUp, string textFeedback)
    {
        Feedbacks.Add(new Feedback(user, episode, thumbsUp, textFeedback));
    }

    /// <summary>
    /// Returns all feedback entries for an episode.
    /// </summary>
    public List<Feedback> GetFeedbackForEpisode(Episode episode)
    {
        return Feedbacks.Where(f => f.Episode == episode).ToList();
    }

    /// <summary>
    /// Returns the number of thumbs-up ratings for an episode.
    /// </summary>
    public int GetThumbsUpCount(Episode episode)
    {
        return Feedbacks.Count(f => f.Episode == episode && f.ThumbsUp);
    }

    /// <summary>
    /// Returns the number of listeners who started an episode.
    /// </summary>
    public int GetListenersStarted(Episode episode)
    {
        return ListeningRecords.Count(r => r.Episode == episode);
    }

    /// <summary>
    /// Returns the number of listeners who completed an episode.
    /// </summary>
    public int GetListenersCompleted(Episode episode)
    {
        return ListeningRecords.Count(r => r.Episode == episode && r.Completed);
    }

    /// <summary>
    /// Returns the number of episodes a user has started listening to.
    /// </summary>
    public int GetEpisodesStarted(User user)
    {
        return ListeningRecords.Count(r => r.Listener == user);
    }

    /// <summary>
    /// Returns the number of episodes a user has completed.
    /// </summary>
    public int GetEpisodesCompleted(User user)
    {
        return ListeningRecords.Count(r => r.Listener == user && r.Completed);
    }

    /// <summary>
    /// Returns the total minutes a user has listened across all episodes.
    /// </summary>
    public int GetTotalMinutesListened(User user)
    {
        return ListeningRecords.Where(r => r.Listener == user).Sum(r => r.MinutesListened);
    }

    /// <summary>
    /// Returns the number of advertisements a user has heard.
    /// </summary>
    public int GetAdvertisementsListened(User user)
    {
        return GetTotalMinutesListened(user) / 30;
    }
}
