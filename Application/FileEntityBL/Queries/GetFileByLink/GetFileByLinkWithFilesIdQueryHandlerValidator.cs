using FluentValidation;

namespace TestTask.Application.FileEntityBL.Queries.GetFileByLink
{
    public class GetFileByLinkWithFilesIdQueryHandlerValidator: AbstractValidator<GetFileByLinkWithFilesIdQuery>
    {
        public GetFileByLinkWithFilesIdQueryHandlerValidator()
        {
            RuleFor(fileCommand => fileCommand.LinkId).NotEmpty();
        }
    }
}
