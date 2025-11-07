using FluentValidation;

namespace Shared.Application.Utils;

public static class ProcessResponseUtils
{
    public static object GetMessageError(Exception exception)
    {
        List<string> errors = new();

        switch (exception.InnerException)
        {
            case null:
                return errors;
            case ValidationException validationException:

                var messages = from validationErrors in validationException.Errors
                    group validationErrors.ToString() by validationErrors.PropertyName
                    into g
                    select new { g.Key, Errors = g.ToList() };

                return messages.ToDictionary(message => message.Key, message => message.Errors);

            default:
                errors.Add(exception.InnerException.Message);
                break;
        }

        return errors;
    }

    public static string GenerateMsg(string msg, params object[] args)
    {
        return string.Format(msg, args);
    }
}