using System;

namespace RtlAPI.Helper
{
    public class ServiceResult<T>
    {
        public ServiceResultType ResultType { get; private set; }

        public Exception Exception { get; private set; }

        public T Data { get; set; }

        public ServiceResult(T data)
        {
            ResultType = ServiceResultType.Success;
            Data = data;
        }

        public ServiceResult(Exception exception)
        {
            ResultType = ServiceResultType.Fail;
            Exception = exception;
        }
    }

    public enum ServiceResultType
    {
        Fail = 0,

        Success = 1
    }
}