namespace Shared.Domain.Models;

public static class EntityResponseUtils
{
    public static string GenerateMsg(string msg, params object[] args)
    {
        return string.Format(msg, args);
    }
}