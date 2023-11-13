using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TestTask.Application.CommonFileBl.Command.CommonCreateFiles;
using TestTask.Application.FileEntityBL.Command.CreateDisposableFileLink;
using TestTask.Application.FileEntityBL.Queries.GetFileById;
using TestTask.Application.FileEntityBL.Queries.GetFileByLink;
using TestTask.Application.FileEntityBL.Queries.GetFileLoadPercent;
using TestTask.Application.FileEntityBL.Queries.GetLoadedFiles;

namespace TestTask.Controllers;

public class FileController : BaseFileController
{

    [Authorize]
    [HttpGet("{name}")]
    public async Task<IActionResult> LoadPercent(string name)
    {
        if (!User.Identity.IsAuthenticated)
        {
            Unauthorized();
        }

        var getPercentRequest = new GetFileLoadPercentQuery() { FileName = name, UserId = UserId };
        var percent = await Mediator.Send(getPercentRequest);

        return Json(percent);
    }
    
    [Authorize]
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        if (!User.Identity.IsAuthenticated)
        {
            Unauthorized();
        }

        var getLoadedFilesRequest = new GetLoadedFilesQuery() { UserId = UserId };
        var percent = await Mediator.Send(getLoadedFilesRequest);

        return Json(percent);
    }
    
    [Authorize]
    [HttpGet("linkId")]
    public async Task<IActionResult> GetFileByLink(Guid linkId)
    {
        if (!User.Identity.IsAuthenticated)
        {
            Unauthorized();
        }

        var getPathByLinkRequest = new GetFileByLinkWithFilesIdQuery() { LinkId = linkId };
        var path = await Mediator.Send(getPathByLinkRequest);

        return PhysicalFile(path.Path, "application/octet-stream");
    }

    [Authorize]
    [HttpGet("id")]
    public async Task<IActionResult> GetFileById(Guid id)
    {
        if (!User.Identity.IsAuthenticated)
        {
            Unauthorized();
        }

        var getPathByIdRequest = new GetFileByIdQuery() { Id = id, UserId = UserId };
        var path = await Mediator.Send(getPathByIdRequest);

        return PhysicalFile(path.Path, "application/octet-stream");
    }
    
    [Authorize]
    [HttpPost("id")]
    public async Task<IActionResult> CreateLink(Guid id)
    {
        if (!User.Identity.IsAuthenticated)
        {
            Unauthorized();
        }

        var createLinkCommand = new CreateDisposableFileLinkCommand() { FileId = id, UserId = UserId };
        var linkId = await Mediator.Send(createLinkCommand);

        return Json(linkId);
    }
    
    [Authorize]
    [HttpPost]
    [RequestSizeLimit((long)1e+10)]
    [RequestFormLimits(MultipartBodyLengthLimit = (long)1e+10)]
    public async Task<IActionResult> LoadFile(IFormFileCollection files)
    {
        if (!User.Identity.IsAuthenticated)
        {
            Unauthorized();
        }

        var loadRequest = new CommonCreateFileCommand() { FileCollection = files, UserId = UserId };
        var load = await Mediator.Send(loadRequest);

        return Json(load);
    }
}