using MediatR;

namespace TestTask.Application.FileEntityBL.Queries.GetFileLoadPercent;

public class GetFileLoadPercentQuery: IRequest<double>
{
    public Guid UserId { get; set; }
    public string FileName { get; set; } = null!;
}