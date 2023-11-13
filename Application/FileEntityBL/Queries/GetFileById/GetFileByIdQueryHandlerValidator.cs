using FluentValidation;

namespace TestTask.Application.FileEntityBL.Queries.GetFileById
{
    public class GetFileByLinkWithFilesIdQueryHandlerValidator: AbstractValidator<GetFileByLink.GetFileByLinkWithFilesIdQuery>
    {
        public GetFileByLinkWithFilesIdQueryHandlerValidator()
        {
            RuleFor(fileCommand => fileCommand.LinkId).NotEmpty();
        }
    }
}
