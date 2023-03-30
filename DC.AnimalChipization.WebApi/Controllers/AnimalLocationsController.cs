using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using DC.AnimalChipization.Application.Features.AnimalLocations.Messages.Commands;
using DC.AnimalChipization.Application.Features.AnimalLocations.Messages.Queries;
using DC.AnimalChipization.WebApi.ViewModels.AnimalLocations;
using DC.AnimalChipization.WebApi.ViewModels.AnimalLocations.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DC.AnimalChipization.WebApi.Controllers;

[ApiController]
public class AnimalLocationsController : ControllerBase
{
    #region [ControllerConstants]

    public const string ControllerName = "AnimalLocations";

    public const string ActionSearchName = "Search";
    public const string ActionCreateName = "Create";
    public const string ActionUpdateName = "Update";
    public const string ActionDeleteName = "Delete";

    #endregion

    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    public AnimalLocationsController(IMapper mapper, IMediator mediator)
    {
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    [HttpGet("/animals/{animalId:long}/locations")]
    [ActionName(ActionSearchName)]
    public async Task<ActionResult<List<AnimalLocationViewModel>>> SearchAsync(long animalId, [FromQuery] SearchAnimalLocationRequest request)
    {
        var message = _mapper.Map<SearchAnimalLocationQueryMessage>(request);
        message.AnimalId = animalId;

        var response = await _mediator.Send(message);
        var viewModels = _mapper.Map<List<AnimalLocationViewModel>>(response);

        return Ok(viewModels);
    }

    [HttpPost("animals/{animalId:long}/locations/{pointId:long}")]
    [ActionName(ActionCreateName)]
    public async Task<ActionResult<AnimalLocationViewModel>> CreateAsync(long animalId, long pointId)
    {
        var message = new AddAnimalLocationCommandMessage
        {
            AnimalId = animalId,
            PointId = pointId
        };

        var animalLocationDto = await _mediator.Send(message);
        var animalViewModel = _mapper.Map<AnimalLocationViewModel>(animalLocationDto);

        return CreatedAtAction
        (
            actionName: ActionSearchName,
            controllerName: ControllerName,
            routeValues: new { animalId },
            value: animalViewModel
        );
    }

    [HttpPut("/animals/{animalId:long}/locations")]
    [ActionName(ActionUpdateName)]
    public async Task<ActionResult<AnimalLocationViewModel>> UpdateAsync(long animalId, [FromBody] UpdateAnimalLocationRequest request)
    {
        var message = _mapper.Map<UpdateAnimalLocationCommandMessage>(request);
        message.AnimalId = animalId;

        var response = await _mediator.Send(message);
        var viewModels = _mapper.Map<AnimalLocationViewModel>(response);

        return Ok(viewModels);
    }

    [HttpDelete("/animals/{animalId:long}/locations/{visitedPointId}")]
    [ActionName(ActionDeleteName)]
    public async Task<ActionResult> DeleteAsync(long animalId, long visitedPointId)
    {
        var message = new DeleteAnimalLocationCommandMessage
        {
            AnimalId = animalId,
            VisitedPointId = visitedPointId
        };

        await _mediator.Send(message);

        return Ok();
    }
}