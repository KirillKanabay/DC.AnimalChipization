using DC.AnimalChipization.Application.Common.Immutable;
using DC.AnimalChipization.Application.Identity.Attributes;

namespace DC.AnimalChipization.Application.Features.Areas.Messages.Commands;

[Authorize(Roles = new[] { Roles.Admin })]
public class UpdateAreaCommandMessage : ImportAreaCommandMessageBase
{
    public long AreaId { get; set; }
}