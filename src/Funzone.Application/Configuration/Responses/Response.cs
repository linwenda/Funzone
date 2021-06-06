using System.Collections.Generic;
using System.Linq;
using Funzone.Domain.SeedWork;

namespace Funzone.Application.Configuration.Responses
{
    public class Response<T> : IResponse
    {
        private Response(T data)
        {
            Data = data;
        }

        private Response(ResponseCode code)
        {
            Code = code;
        }

        public ResponseCode Code { get; } = ResponseCode.Succeeded;
        public IEnumerable<string> Messages { get; private set; } = new List<string>();
        public T Data { get; }

        public static Response<T> Success(T value, params string[] succeededMessages)
        {
            return new Response<T>(value)
            {
                Messages = succeededMessages
            };
        }

        public static Response<T> Error(ResponseCode errorCode, params string[] errorMessages)
        {
            return new Response<T>(errorCode)
            {
                Messages = errorMessages
            };
        }

        public static Response<T> NotFound(string message)
        {
            return new Response<T>(ResponseCode.NotFound)
            {
                Messages = new []{message}
            };
        }

        public static Response<T> Invalid(IDictionary<string, string[]> errors)
        {
            return new Response<T>(ResponseCode.Invalid)
            {
                Messages = errors.Select(e => $"{string.Join(',', e.Value)}")
            };
        }
    }
}