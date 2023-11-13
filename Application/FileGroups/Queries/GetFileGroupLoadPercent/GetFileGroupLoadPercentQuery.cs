using MediatR;

namespace TestTask.Application.FileGroups.Queries.GetFileGroupLoadPercent;

public class GetFileGroupLoadPercentQuery: IRequest<double>
{
    public Guid UserId { get; set; }
    public Guid GroupId { get; set; }
}