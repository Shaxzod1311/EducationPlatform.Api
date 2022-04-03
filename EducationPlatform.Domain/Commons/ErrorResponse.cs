namespace EducationPlatform.Domain.Commons
{
    public class ErrorResponse
    {
        public ErrorResponse(int? code = null, string mesage = null)
        {
            Code = code;
            Message = mesage;
        }

        public int? Code { get; set; }
        public string Message { get; set; }

    }
}