using AssetManagement.Entities.DTOs.Responses;
using AssetManagement.Entities.Enums;

namespace AssetManagement.API.Constants;

public static class MessageConstants
{
    public const string NotFoundAttendance = "The attendance record for checkout could not be found.";

    public const string InvalidAttendanceRequest = "The attendance request is invalid.";

    public static string UnexpectedError =>
        "We apologize, but an unforeseen error prevented us from completing this. Please try again later.";

    public static string NotFoundResource =>
        "The requested resource could not be found.";

    public static string CheckInEmailSubject =>
        "Drop-off confirmed for";

    public static string CheckOutEmailSubject =>
        "Pick-up confirmed for";

    public static string ServiceEmailAddress =>
        "ssrs@visipixel.com";

    public static string Success(RecordTypeEnum recordTypeEnum) =>
        recordTypeEnum switch
        {
            RecordTypeEnum.LogIn => "Login Successful",
            RecordTypeEnum.LogOut => "Login Successful",
            RecordTypeEnum.Save => "Record Added Successful",
            RecordTypeEnum.Edit => "Record Updated Successful",
            RecordTypeEnum.Delete => "Record Deleted Successful",
            RecordTypeEnum.GetAll => "Get record Successful",
            RecordTypeEnum.GetAllByDate => "Get record by date Successful",
            _ => "Request processed successfully."
        };

    public static class ApiResponseFactory
    {
        public static ApiActionResponse<T> Success<T>(T data, string message = "Success")
            => new ApiActionResponse<T> { Message = message, Data = data };

        public static ApiActionResponse<T> Created<T>(T data, string message = "Record Added Successful")
            => new ApiActionResponse<T> { Message = message, Data = data };

        public static ApiActionResponse<T> Conflict<T>(string message)
            => new ApiActionResponse<T> { Message = message, Data = default };
    }
}
