using System.Reflection;

namespace Shared.Domain.SeedWork;

public abstract class Enumeration : IComparable
{
    public int CompareTo(object other)
    {
        return Id.CompareTo(((Enumeration)other).Id);
    }

    public override string ToString()
    {
        return Name;
    }

    public static IEnumerable<T> GetAll<T>() where T : Enumeration
    {
        var fields = typeof(T).GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly);

        return fields.Select(f => f.GetValue(null)).Cast<T>();
    }

    public override bool Equals(object? obj)
    {
        if (obj is not Enumeration otherValue)
            return false;

        var typeMatches = GetType() == obj.GetType();
        var valueMatches = Id.Equals(otherValue.Id);

        return typeMatches && valueMatches;
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }

    public static int AbsoluteDifference(Enumeration firstValue, Enumeration secondValue)
    {
        var absoluteDifference = Math.Abs(firstValue.Id - secondValue.Id);
        return absoluteDifference;
    }

    public static T FromValue<T>(int value) where T : Enumeration
    {
        var matchingItem = Parse<T, int>(value, "value", item => item.Id == value);
        return matchingItem;
    }

    public static T FromDisplayName<T>(string displayName) where T : Enumeration
    {
        var matchingItem = Parse<T, string>(displayName, "display name", item => item.Name == displayName);
        return matchingItem;
    }

    private static T Parse<T, TK>(TK value, string description, Func<T, bool> predicate) where T : Enumeration
    {
        var matchingItem = GetAll<T>().FirstOrDefault(predicate);

        if (matchingItem == null)
            throw new InvalidOperationException($"'{value}' is not a valid {description} in {typeof(T)}");

        return matchingItem;
    }

    #region Constructor & properties

    public string Name { get; }

    public int Id { get; }

    protected Enumeration(int id, string name)
    {
        Id = id;
        Name = name;
    }

    #endregion
}

public static class JwtClaimTypes
{
    /// <summary>Unique Identifier for the End-User at the Issuer.</summary>
    public const string Subject = "sub";

    /// <summary>End-User's full name in displayable form including all name parts, possibly including titles and suffixes, ordered according to the End-User's locale and preferences.</summary>
    public const string Name = "name";

    /// <summary>Given name(s) or first name(s) of the End-User. Note that in some cultures, people can have multiple given names; all can be present, with the names being separated by space characters.</summary>
    public const string GivenName = "given_name";

    /// <summary>Surname(s) or last name(s) of the End-User. Note that in some cultures, people can have multiple family names or no family name; all can be present, with the names being separated by space characters.</summary>
    public const string FamilyName = "family_name";

    /// <summary>End-User's preferred e-mail address. Its value MUST conform to the RFC 5322 [RFC5322] addr-spec syntax. The relying party MUST NOT rely upon this value being unique</summary>
    public const string Email = "email";
}

public static class IntegrationEventStatus
{
    public static string NotPublished = nameof(NotPublished);
    public static string InProgress = nameof(InProgress);
    public static string Published = nameof(Published);
    public static string Failed = nameof(Failed);
    public static string Completed = nameof(Completed);
}

public static class NotificationTypes
{
    public static string Events => nameof(Events).ToLowerInvariant();
    public static string Programs => nameof(Programs).ToLowerInvariant();
    public static string Notifications => nameof(Notifications).ToLowerInvariant();
    public static List<string> All => new() { Events, Programs, Notifications };
}

public static class NotificationStates
{
    public static string Pending => nameof(Pending).ToLowerInvariant();
    public static string Sent => nameof(Sent).ToLowerInvariant();
    public static string Error => nameof(Error).ToLowerInvariant();

    public static List<string> All => new() { Pending, Sent, Error };
}