namespace Core.Database.Extra;

// TODO: Refactor
public enum UserStatus
{
    Undefined = -1,
    Ok = 0,
    EmailConfirmationNeeded = 1,
    Banned = 2
}