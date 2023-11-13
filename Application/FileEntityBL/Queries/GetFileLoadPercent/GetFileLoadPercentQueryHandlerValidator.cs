using FluentValidation;

namespace TestTask.Application.FileEntityBL.Queries.GetFileLoadPercent
{
    public class GetFileLoadPercentQueryHandlerValidator: AbstractValidator<GetFileLoadPercentQuery>
    {
        public GetFileLoadPercentQueryHandlerValidator()
        {
            RuleFor(fileCommand => fileCommand.UserId).NotEmpty();
            RuleFor(fileLoadCommand => fileLoadCommand.FileName).NotEmpty().MaximumLength(300);
        }
    }
}
