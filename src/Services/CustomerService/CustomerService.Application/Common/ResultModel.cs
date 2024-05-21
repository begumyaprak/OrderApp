namespace CustomerService.Application.Common;

public class ResultModel
{
    public bool Success { get; set; }
    public string ErrorMessage { get; set; }

    public static ResultModel Fail(string errorMessage)
    {
        return new ResultModel { Success = false, ErrorMessage = errorMessage };
    }

    public static ResultModel Ok()
    {
        return new ResultModel { Success = true };
    }
}