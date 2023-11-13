using FluentValidation;

namespace TestTask.Application.FileGroups.Queries.GetFileGroupByLink
{
    public class GetFileGroupByLinkWithFilesIdQueryHandlerValidator: AbstractValidator<GetFileGroupByLinkWithFilesIdQuery>
    {
        public GetFileGroupByLinkWithFilesIdQueryHandlerValidator()
        {
            RuleFor(fileCommand => fileCommand.LinkId).NotEmpty();
        }
    }
}
