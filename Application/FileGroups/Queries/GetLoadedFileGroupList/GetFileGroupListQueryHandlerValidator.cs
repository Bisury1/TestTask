using FluentValidation;

namespace TestTask.Application.FileGroups.Queries.GetLoadedFileGroupList
{
    public class GetFileGroupListQueryHandlerValidator: AbstractValidator<GetFileGroupListQuery>
    {
        public GetFileGroupListQueryHandlerValidator()
        {
            RuleFor(note => note.UserId).NotEmpty().NotEqual(Guid.Empty);
        }
    }
}
