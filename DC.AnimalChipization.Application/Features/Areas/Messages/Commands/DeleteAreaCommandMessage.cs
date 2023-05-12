using DC.AnimalChipization.Application.Common.Immutable;
using DC.AnimalChipization.Application.Identity.Attributes;
using MediatR;

namespace DC.AnimalChipization.Application.Features.Areas.Messages.Commands;

[Authorize(Roles = new[] { Roles.Admin })]
public class DeleteAreaCommandMessage : IRequest
{
    public long AreaId { get; set; }
}