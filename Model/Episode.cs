/// <summary>
/// Represents a single episode within a podcast, including its name,
/// description, recorded date, length, and premium status.
/// </summary>
public class Episode
{
    /// <summary>The episode's title.</summary>
    public string Name { get; set; }

    /// <summary>A short description of the episode's content.</summary>
    public string EpisodeDescription { get; set; }

    /// <summary>The date the episode was recorded.</summary>
    public DateTime? RecordedDate { get; set; }

    /// <summary>Length of the episode in minutes (1–300).</summary>
    public int EpisodeLength { get; set; }

    /// <summary>Whether this episode is restricted to premium listeners.</summary>
    public bool PremmiumEpisode { get; set; } = false;

    /// <summary>
    /// Creates a new episode with the specified details.
    /// </summary>
    public Episode(string episodeName, string podcastDescription, DateTime? episodeRecorded, int episodeLength, bool premiumEpisode)
    {
        Name = episodeName;
        EpisodeDescription = podcastDescription;
        RecordedDate = episodeRecorded;
        EpisodeLength = episodeLength;
        PremmiumEpisode = premiumEpisode;
    }

    /// <summary>
    /// Validates that the episode name is at least 3 characters and contains
    /// only letters, spaces, hyphens, apostrophes, or periods.
    /// </summary>
    public static bool IsValidEpisodeName(string name)
    {
        if (string.IsNullOrWhiteSpace(name) || name.Length < 3)
        {
            return false;
        }
        return name.All(c => char.IsLetter(c) || c == ' ' || c == '-' || c == '\'' || c == '.');
    }

    /// <summary>
    /// Validates that the episode length is between 1 and 300 minutes.
    /// </summary>
    public static bool IsValidEpisodeLength(int length)
    {
        return length >= 1 && length <= 300;
    }

    /// <summary>
    /// Validates that a recorded date has been provided.
    /// </summary>
    public static bool IsValidRecordedDate(DateTime? date)
    {
        return date != null;
    }

    /// <summary>
    /// Validates that the description is at least 3 characters.
    /// </summary>
    public static bool IsValidDescription(string description)
    {
        return !string.IsNullOrWhiteSpace(description) && description.Length >= 3;
    }
}
