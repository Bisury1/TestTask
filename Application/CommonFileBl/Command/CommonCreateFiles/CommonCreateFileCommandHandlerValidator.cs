using FluentValidation;

namespace TestTask.Application.CommonFileBl.Command.CommonCreateFiles
{
    public class CommonCreateFileCommandHandlerValidator: AbstractValidator<CommonCreateFileCommand>
    {
        public CommonCreateFileCommandHandlerValidator()
        {

            RuleFor(createFileGroupCommand =>
                createFileGroupCommand.FileCollection).NotEmpty();
            RuleFor(createNoteCommand =>
                createNoteCommand.UserId).NotEqual(Guid.Empty);
        }
    }
}
