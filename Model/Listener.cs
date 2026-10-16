/// <summary>
/// Represents a regular (non-premium) listener in the PodCastWorld system.
/// Regular listeners see advertisements and cannot access premium-only episodes.
/// </summary>
public class Listener : User
{
    /// <summary>
    /// Creates a new regular listener with the specified personal details.
    /// </summary>
    public Listener(string name, int age, string email, string mobileNumber, string password)
        : base(name, age, email, mobileNumber, password)
    {
    }
}
