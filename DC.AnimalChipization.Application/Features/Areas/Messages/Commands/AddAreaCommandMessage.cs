using DC.AnimalChipization.Application.Common.Immutable;
using DC.AnimalChipization.Application.Features.Areas.DataTransfer;
using DC.AnimalChipization.Application.Identity.Attributes;
using MediatR;

namespace DC.AnimalChipization.Application.Features.Areas.Messages.Commands;

[Authorize(Roles = new []{Roles.Admin})]
public class AddAreaCommandMessage : IRequest<AreaDto>
{
    public string Name { get; set; }
    public List<AreaPointDto> AreaPoints { get; set; }
}