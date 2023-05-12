using DC.AnimalChipization.Application.Features.Areas.DataTransfer;
using DC.AnimalChipization.Application.Identity.Attributes;
using MediatR;

namespace DC.AnimalChipization.Application.Features.Areas.Messages.Queries;

[Authorize]
public class GetAreaByIdQueryMessage : IRequest<AreaDto>
{
    public long Id { get; set; }
}