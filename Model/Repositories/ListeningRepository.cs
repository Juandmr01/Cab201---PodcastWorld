/// <summary>
/// Manages listening records, feedback, and episode statistics.
/// </summary>
public class ListeningRepository : IListeningService
{
    private readonly List<ListeningRecord> ListeningRecords = new List<ListeningRecord>();
    private readonly List<Feedback> Feedbacks = new List<Feedback>();

    public ListeningRecord GetListeningRecord(User user, Episode episode)
    {
        return ListeningRecords.FirstOrDefault(r => r.Listener == user && r.Episode == episode);
    }

    public ListeningRecord ListenToEpisode(User user, Podcast podcast, Episode episode, int minutesListened, DateTime listenDate)
    {
        ListeningRecord record = GetListeningRecord(user, episode);
        if (record == null)
        {
            record = new ListeningRecord(user, podcast, episode);
            ListeningRecords.Add(record);
        }
        record.MinutesListened += minutesListened;
        record.LastListenDate = listenDate;
        if (record.MinutesListened >= episode.EpisodeLength)
        {
            record.Completed = true;
        }
        return record;
    }

    public void AddFeedback(User user, Episode episode, bool thumbsUp, string textFeedback)
    {
        Feedbacks.Add(new Feedback(user, episode, thumbsUp, textFeedback));
    }

    public List<Feedback> GetFeedbackForEpisode(Episode episode)
    {
        return Feedbacks.Where(f => f.Episode == episode).ToList();
    }

    public int GetThumbsUpCount(Episode episode)
    {
        return Feedbacks.Count(f => f.Episode == episode && f.ThumbsUp);
    }

    public int GetListenersStarted(Episode episode)
    {
        return ListeningRecords.Count(r => r.Episode == episode);
    }

    public int GetListenersCompleted(Episode episode)
    {
        return ListeningRecords.Count(r => r.Episode == episode && r.Completed);
    }

    public int GetEpisodesStarted(User user)
    {
        return ListeningRecords.Count(r => r.Listener == user);
    }

    public int GetEpisodesCompleted(User user)
    {
        return ListeningRecords.Count(r => r.Listener == user && r.Completed);
    }

    public int GetTotalMinutesListened(User user)
    {
        return ListeningRecords.Where(r => r.Listener == user).Sum(r => r.MinutesListened);
    }

    public int GetAdvertisementsListened(User user)
    {
        return GetTotalMinutesListened(user) / 30;
    }
}
