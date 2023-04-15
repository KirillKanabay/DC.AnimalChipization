using DC.AnimalChipization.Application.Features.Areas.DataTransfer;
using MediatR;

namespace DC.AnimalChipization.Application.Features.Areas.Messages.Commands;

public abstract class ImportAreaCommandMessageBase : IRequest<AreaDto>
{
    public string Name { get; set; }
    public List<AreaPointDto> AreaPoints { get; set; }
}