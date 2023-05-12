using DC.AnimalChipization.Application.Features.Areas.DataTransfer;
using DC.AnimalChipization.Application.Identity.Attributes;
using MediatR;

namespace DC.AnimalChipization.Application.Features.Areas.Messages.Queries;

[Authorize]
public class GetAreaAnalyticsQueryMessage : IRequest<AreaAnalyticResultDto>
{
    public long AreaId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}