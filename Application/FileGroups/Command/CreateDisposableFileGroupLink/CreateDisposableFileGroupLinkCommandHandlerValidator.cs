using FluentValidation;

namespace TestTask.Application.FileGroups.Command.CreateDisposableFileGroupLink
{
    public class CreateDisposableFileGroupLinkCommandHandlerValidator: AbstractValidator<CreateDisposableFileGroupLinkCommand>
    {
        public CreateDisposableFileGroupLinkCommandHandlerValidator()
        {

            RuleFor(createFileGroupCommand =>
                createFileGroupCommand.FileId).NotEmpty();
            RuleFor(createNoteCommand =>
                createNoteCommand.UserId).NotEqual(Guid.Empty);
        }
    }
}
