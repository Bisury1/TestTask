using FluentValidation;

namespace TestTask.Application.FileEntityBL.Command.CreateDisposableFileLink
{
    public class CreateDisposableFileLinkCommandHandlerValidator: AbstractValidator<CreateDisposableFileLinkCommand>
    {
        public CreateDisposableFileLinkCommandHandlerValidator()
        {

            RuleFor(createFileGroupCommand =>
                createFileGroupCommand.FileId).NotEmpty();
            RuleFor(createNoteCommand =>
                createNoteCommand.UserId).NotEqual(Guid.Empty);
        }
    }
}
