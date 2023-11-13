using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TestTask.Application.CommonFileBl.Command.CommonCreateFiles;
using TestTask.Application.FileEntityBL.Command.CreateDisposableFileLink;
using TestTask.Application.FileEntityBL.Queries.GetFileById;
using TestTask.Application.FileEntityBL.Queries.GetFileByLink;
using TestTask.Application.FileEntityBL.Queries.GetFileLoadPercent;
using TestTask.Application.FileEntityBL.Queries.GetLoadedFiles;
using TestTask.Application.FileGroups.Queries.GetFileGroupByLink;
using TestTask.Application.FileGroups.Queries.GetFileGroupLoadPercent;
using TestTask.Application.FileGroups.Queries.GetLoadedFileGroupList;

namespace TestTask.Controllers;

public class FileGroupController : BaseFileController
{

    [Authorize]
    [HttpGet("{id}")]
    public async Task<IActionResult> LoadPercent(Guid id)
    {
        if (!User.Identity.IsAuthenticated)
        {
            Unauthorized();
        }

        var getPercentRequest = new GetFileGroupLoadPercentQuery() { GroupId = id, UserId = UserId };
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

        var getLoadedFilesGroupsRequest = new GetFileGroupListQuery() { UserId = UserId };
        var percent = await Mediator.Send(getLoadedFilesGroupsRequest);

        return Json(percent);
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