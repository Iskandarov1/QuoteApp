using App.Domain.Core.Primitives;

namespace App.Api.Contracts
{

    public class ApiErrorResponse
    {
        public ApiErrorResponse(IReadOnlyCollection<Error> errors)
        {
            Errors = errors;

        }
        public IReadOnlyCollection<Error> Errors { get; }


    }
}