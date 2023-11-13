using MediatR;

namespace TestTask.Controllers;

public class BaseFileController: BaseAppController
{
    private IMediator _mediator;
    protected IMediator Mediator =>
        _mediator ??= HttpContext.RequestServices.GetService<IMediator>();
}