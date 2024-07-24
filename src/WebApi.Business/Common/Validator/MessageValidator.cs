namespace WebApi.Business.Common.Validator;

public static class MessageValidator
{
    public static string EmailExist => "There is already a registered user with this email";
    public static string RefreshTokenDenegate => "You can't request a new token yet";
    public static string RefreshTokenError => "The token is corrupt or does not exist";
    public static string RefreshTokenRevoked => "Refresh token revoked";
    public static string RefreshTokenExpired => "Refresh token expired";
    public static string RefreshTokenDoesntExist => "Refresh token doesn't exist";
    public static string UserDoestnExist => "User doest'n exist";
    public static string ErrorCredentials => "User / Password incorrect";
 
}