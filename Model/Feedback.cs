/// <summary>
/// Represents a listener's feedback on an episode, consisting of
/// a thumbs-up/down rating and a text comment.
/// </summary>
public class Feedback
{
    /// <summary>The listener who provided the feedback.</summary>
    public User Listener { get; private set; }

    /// <summary>The episode the feedback is about.</summary>
    public Episode Episode { get; private set; }

    /// <summary>True for a thumbs-up rating, false for thumbs-down.</summary>
    public bool ThumbsUp { get; private set; }

    /// <summary>The listener's written feedback text.</summary>
    public string TextFeedback { get; private set; }

    /// <summary>
    /// Creates a new feedback entry for an episode.
    /// </summary>
    public Feedback(User listener, Episode episode, bool thumbsUp, string textFeedback)
    {
        Listener = listener;
        Episode = episode;
        ThumbsUp = thumbsUp;
        TextFeedback = textFeedback;
    }
}
