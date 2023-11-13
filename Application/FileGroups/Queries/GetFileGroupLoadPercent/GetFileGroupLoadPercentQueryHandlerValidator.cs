using FluentValidation;
using TestTask.Application.FileEntityBL.Queries.GetFileLoadPercent;

namespace TestTask.Application.FileGroups.Queries.GetFileGroupLoadPercent
{
    public class GetFileGroupLoadPercentQueryHandlerValidator: AbstractValidator<GetFileGroupLoadPercentQuery>
    {
        public GetFileGroupLoadPercentQueryHandlerValidator()
        {
            RuleFor(fileCommand => fileCommand.UserId).NotEmpty();
            RuleFor(fileLoadCommand => fileLoadCommand.GroupId).NotEmpty();
        }
    }
}
