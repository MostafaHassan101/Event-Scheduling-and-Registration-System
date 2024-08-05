using MediatR;
using Microsoft.AspNetCore.Mvc;
using API.ExceptionFilter;

namespace API.Controllers;
[ApiController]
[ApiExceptionFilter]
[Route("api/[controller]")]
public abstract class ApiControllerBase : ControllerBase
{
    private ISender _mediator;

    protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<ISender>();
}
