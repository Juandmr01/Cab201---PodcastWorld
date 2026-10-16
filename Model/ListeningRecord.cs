/// <summary>
/// Tracks a user's listening progress for a specific episode,
/// including total minutes listened, last listen date, and completion status.
/// </summary>
public class ListeningRecord
{
    /// <summary>The user who is listening.</summary>
    public User Listener { get; private set; }

    /// <summary>The podcast containing the episode.</summary>
    public Podcast Podcast { get; private set; }

    /// <summary>The episode being listened to.</summary>
    public Episode Episode { get; private set; }

    /// <summary>Total minutes the user has listened so far.</summary>
    public int MinutesListened { get; private set; }

    /// <summary>The date of the user's most recent listening session.</summary>
    public DateTime LastListenDate { get; private set; }

    /// <summary>Whether the user has finished the entire episode.</summary>
    public bool Completed { get; private set; }

    /// <summary>
    /// Creates a new listening record with zero minutes and not completed.
    /// </summary>
    public ListeningRecord(User listener, Podcast podcast, Episode episode)
    {
        Listener = listener;
        Podcast = podcast;
        Episode = episode;
        MinutesListened = 0;
        Completed = false;
    }

    /// <summary>
    /// Records additional listening time and marks the episode complete
    /// if the total reaches or exceeds the episode length.
    /// </summary>
    public void UpdateProgress(int additionalMinutes, DateTime listenDate)
    {
        MinutesListened += additionalMinutes;
        LastListenDate = listenDate;
        if (MinutesListened >= Episode.EpisodeLength)
        {
            Completed = true;
        }
    }
}
