using App.Api.Contracts;
using App.Domain.Core.Primitives;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace App.Api.Controller
{
    
    [Route("api")]
    public class ApiController : ControllerBase
    {
        protected ApiController(IMediator mediator) => Mediator = mediator;

        protected IMediator Mediator { get; }
        
        
        
        protected IActionResult BadRequest(Error error) => BadRequest(new ApiErrorResponse(new[] { error }));


        protected new IActionResult Ok(object value) => base.Ok(value);

        protected new NotFoundResult NotFound() => base.NotFound();
    }
}