namespace Backend.Domain.MessageHandlers;

public static class MessageHandler
{
    // Errors
    public const string AuthenticationError = "Invalid user and/or password";
    public const string PasswordNotEquals = "The current password does not match";


    public const string UserAlreadyRegistered = "User already exists";
    public const string GenericError = "Se ha producido un error. Por favor contacte al Administrador";
    public const string UserNotFound = "User not found";
    public const string ProfileNotFound = "Profile not found";
    public const string PermissionNotFound = "Permission not found";
    public const string UserFound = "User already exists";
    public const string EmailFound = "Email already exists";

    public const string ErrorEmailFormat = "Email is not valid";

    //Data Users
    public const string ProfileAlreadyExit = "Profile already exists";

    // User Manager
    public const string ManagerUsernameNotEmpty = "Username is required";
    public const string ManagerFirstnameNotEmpty = "Names is required";
    
    //Registers
    public const string CatalogNotFound = "Catalog not found";
    public const string CatalogValueNotFound = "Catalog Value not found";
    public const string FormTemplateNotFound = "Form Template not found";
    public const string ItemSectionNotFound = "Item Section not found";
    public const string ItemNotFound = "Item not found";
    public const string InspectionNotFound = "Inspection not found";
    public const string InspectionResultNotFound = "Inspection Result not found";
    
        //Zoho
    public const string ZohoErrorGeneratedToken = "Error generating token";
    public const string ZohoTokenNotFound = "Zoho Token not found";
    public const string ZohoExternalUserErrorCreating = "Error creating External User";
    public const string ZohoTemplateErrorCreating = "Error creating Template TC";
    public const string ZohoInspectionErrorCreating = "Error creating Inspection TC";
    public const string ZohoInspectionErrorUpdate = "Error updating Inspection TC";
    public const string ZohoTemplateErrorUpdate = "Error updating Template TC";
    
    //Storage
    public const string FileNotExistMsg = "File with File Name {0} does     not exist.";
    public const string FileSizeGreaterThanLimitMsg = "File size is greater than the allowed limit: {0}";
    public const string ErrorGeneratingReport = "Error generating report";
    
}