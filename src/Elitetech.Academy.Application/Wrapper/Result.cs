namespace Elitetech.Academy.Application.Wrapper
{
    public class Result
    {
        public dynamic Data { get; set; }
        public ResultStatus Status { get; set; }
        public bool IsSuccess => Status == ResultStatus.Ok && !Errors.Any();
        public string SuccessMessage { get; private set; } = string.Empty;
        public IEnumerable<string> Errors { get; set; } = Enumerable.Empty<string>();

        
        private Result(dynamic data)
        {
            Data = data;
        }

        private Result(ResultStatus status)
        {
            Status = status;
        }

        public static Result Success<T>(T data)
        {
            return new Result(data);
        }

        public static Result Success<T>(T data, string successMessage)
        {
            return new Result(data)
            {
                SuccessMessage = successMessage
            };
        }

        public static Result Error(params string[] errorMessages)
        {
            return new Result(ResultStatus.Exception)
            {
                Errors = errorMessages
            };
        }

        public static Result NotFound()
        {
            return new Result(ResultStatus.NotFound);
        }

    }
}
