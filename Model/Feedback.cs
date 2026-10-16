/// <summary>
/// Represents a listener's feedback on an episode, consisting of
/// a thumbs-up/down rating and a text comment.
/// </summary>
public class Feedback
{
    public User Listener { get; set; }
    public Episode Episode { get; set; }
    public bool ThumbsUp { get; set; }
    public string TextFeedback { get; set; }

    public Feedback(User listener, Episode episode, bool thumbsUp, string textFeedback)
    {
        Listener = listener;
        Episode = episode;
        ThumbsUp = thumbsUp;
        TextFeedback = textFeedback;
    }
}
