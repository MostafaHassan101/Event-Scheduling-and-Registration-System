using System.Text.RegularExpressions;

namespace EventSystem.Application.Common.Models;

public class AppConstants
{
    public const int Success = 200;
    public const int NoContent = 204;
    public const int BadRequest = 400;
    public const int UnAuthorized = 401;
    public const int NotFound = 404;
    public const int InternalServerError = 500;


    public const string AuthResponse = "AuthResponse";
    public const string Result = "result";
    public const string Failed = "failed";
    public const string Message = "message";
    public const string SuccessMessage = "success";
    public const string Error = "errors";

    public const string Metas = "metas";
    public const string Model = "model";
    public const string Code = "Code";


    /// regex pattern
    public static Regex PasswordPattern = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[a-zA-Z\d]{8,}$");


}