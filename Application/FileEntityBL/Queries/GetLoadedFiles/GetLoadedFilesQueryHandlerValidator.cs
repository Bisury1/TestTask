using FluentValidation;

namespace TestTask.Application.FileEntityBL.Queries.GetLoadedFiles
{
    public class GetLoadedFilesQueryHandlerValidator: AbstractValidator<GetFileLoadPercent.GetFileLoadPercentQuery>
    {
        public GetLoadedFilesQueryHandlerValidator()
        {
            RuleFor(fileCommand => fileCommand.UserId).NotEmpty();
            RuleFor(fileLoadCommand => fileLoadCommand.FileName).NotEmpty().MaximumLength(300);
        }
    }
}
