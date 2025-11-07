using System.Security.Cryptography;
using Shared.Domain.Interfaces.Services;

namespace Shared.Infrastructure.Security;

public class MfaCodeGenerator : IMfaCodeGenerator
{
    public string GetMfaCode()
    {
        var element = RandomNumberGenerator.GetInt32(0, 999999);
        var elementStr = element.ToString("D6");
        return elementStr;
    }
}