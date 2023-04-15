using System.Security.Cryptography.X509Certificates;
using DC.AnimalChipization.Application.Features.Areas.DataTransfer;
using DC.AnimalChipization.Application.Features.Areas.Messages.Commands;
using DC.AnimalChipization.Application.Features.Areas.Validators.Helpers;
using FluentValidation;

namespace DC.AnimalChipization.Application.Features.Areas.Validators;

public class AddAreaCommandMessageValidator : ImportAreaMessageValidatorBase<AddAreaCommandMessage>
{
}