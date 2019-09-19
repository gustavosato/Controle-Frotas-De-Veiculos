using System;
using Lean.Test.Cloud.Domain.Entities.ApplicationSystems;

namespace Lean.Test.Cloud.Domain
{
    public class Result
    {
        public bool IsSuccess { get; }
        public int Code { get; }
        public string Info { get; set; }
        public string Error { get; }
        public bool IsFailure => !IsSuccess;

        protected Result(bool isSuccess, int code, string info, string error)
        {
            if (IsSuccess && error != string.Empty)
                throw new InvalidOperationException();

            if (!isSuccess && error == string.Empty)
                throw new InvalidOperationException();

            IsSuccess = isSuccess;
            Code = code;
            Info = info;
            Error = error;
        }

        internal static Result<ApplicationSystem> Ok(int v1, string v2, object applicationSystems)
        {
            throw new NotImplementedException();
        }

        public static Result Fail(int code, string info, string errorMessage)
        {
            return new Result(false, code, info, errorMessage);
        }

        public static Result<T> Fail<T>(int code, string info, string errorMessage)
        {
            return new Result<T>(default(T), false, code, info, errorMessage);
        }

        public static Result Ok()
        {
            return new Result(true, -1, string.Empty, string.Empty);
        }

        public static Result<T> Ok<T>(int code, string info, T value)
        {
            return new Result<T>(value, true, code, info, string.Empty);
        }

        public static Result Combine(params Result[] results)
        {
            foreach (Result result in results)
            {
                if (result.IsFailure)
                    return result;
            }

            return Ok();
        }
    }

    public class Result<T> : Result
    {
        private readonly T _value;
        private readonly int _code;
        private readonly string _info;

        public T Value
        {
            get
            {
                if (!IsSuccess)
                    throw new InvalidOperationException();

                return _value;
            }
        }

        protected internal Result(T value, bool isSuccess, int code, string info, string error)
            : base(isSuccess, code, info, error)
        {
            _value = value;
            _code = code;
            _info = info;
        }
    }
}
