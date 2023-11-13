using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace TestTask.Controllers;

[Route("api/[controller]/[action]")]
public abstract class BaseAppController: Controller
{
    internal Guid UserId => !User.Identity.IsAuthenticated
        ? Guid.Empty
        : Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
}