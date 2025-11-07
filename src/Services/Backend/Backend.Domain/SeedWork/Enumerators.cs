namespace Backend.Domain.SeedWork;

public static class PermissionTypes
{
    public static string Global => nameof(Global).ToLowerInvariant();
    public static string Scoped => nameof(Scoped).ToLowerInvariant();

    public static List<string> All => new() { Global, Scoped };
}

public static class StatusEnum
{
    public static string Active => nameof(Active).ToLowerInvariant();
    public static string Inactive => nameof(Inactive).ToLowerInvariant();
    public static string Deleted => nameof(Deleted).ToLowerInvariant();

    public static List<string> All => new() { Active, Inactive, Deleted };
}

public static class UserState
{
    public static string Active => nameof(Active).ToLowerInvariant();
    public static string Inactive => nameof(Inactive).ToLowerInvariant();
    public static string Deleted => nameof(Deleted).ToLowerInvariant();

    public static List<string> All => new() { Active, Inactive, Deleted };
}

public static class ProfileStatus
{
    public static string Active => nameof(Active).ToLowerInvariant();
    public static string Deleted => nameof(Deleted).ToLowerInvariant();
    public static List<string> All => new() { Active, Deleted };
}

public static class UserTypes
{
    public static string Manager => nameof(Manager).ToLowerInvariant();
    public static string Member => nameof(Member).ToLowerInvariant();
    public static List<string> All => new() { Manager, Member };
}

public static class InspectionStatusEnum
{
    public static string Created => nameof(Created).ToLowerInvariant();
    public static string InAction => nameof(InAction).ToLowerInvariant();
    public static string Completed => nameof(Completed).ToLowerInvariant();
    public static string Approved => nameof(Approved).ToLowerInvariant();
    public static string Rejected => nameof(Rejected).ToLowerInvariant();
    public static string Deleted => nameof(Deleted).ToLowerInvariant();
    public static List<string> All => new() { Created, InAction, Completed, Approved, Rejected, Deleted };
}

public static class DataTypeEnum
{
    public static string TypeText => nameof(TypeText).ToLowerInvariant();
    public static string TypeNumber => nameof(TypeNumber).ToLowerInvariant();
    public static string TypeDecision => nameof(TypeDecision).ToLowerInvariant();
    public static string TypeList => nameof(TypeList).ToLowerInvariant();
    public static List<string> All => new() { TypeText, TypeNumber, TypeDecision, TypeList };
}

