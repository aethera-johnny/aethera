namespace AuthService.Common
{
    public class Result<T>
    {
        public bool IsSuccess { get; }
        public T Value { get; }
        public string Error { get; }

        public bool IsFailure => !IsSuccess;

        protected Result(T value)
        {
            IsSuccess = true;
            Value = value;
            Error = null;
        }

        protected Result(string error)
        {
            IsSuccess = false;
            Value = default;
            Error = error;
        }

        public static Result<T> Success(T value) => new Result<T>(value);
        public static Result<T> Failure(string error) => new Result<T>(error);
    }
}
