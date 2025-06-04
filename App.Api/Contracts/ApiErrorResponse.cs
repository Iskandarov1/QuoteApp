using App.Domain.Core.Primitives;

namespace App.Api.Contracts
{

    public class ApiErrorResponse(IReadOnlyCollection<Error> errors)
    {
        public IReadOnlyCollection<Error> Errors { get; } = errors;
    }
}