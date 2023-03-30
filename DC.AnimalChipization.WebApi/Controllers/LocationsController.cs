using System;
using System.Threading.Tasks;
using AutoMapper;
using DC.AnimalChipization.Application.Features.Locations.Messages.Commands;
using DC.AnimalChipization.Application.Features.Locations.Messages.Queries;
using DC.AnimalChipization.WebApi.ViewModels.Locations;
using DC.AnimalChipization.WebApi.ViewModels.Locations.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DC.AnimalChipization.WebApi.Controllers;

[ApiController]
[Route("[Controller]")]
public class LocationsController : ControllerBase
{
    #region [ControllerConstants]

    public const string ControllerName = "Locations";

    public const string ActionGetByIdName = "GetById";
    public const string ActionCreateName = "Create";
    public const string ActionUpdateName = "Update";
    public const string ActionDeleteName = "Delete";

    #endregion

    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    public LocationsController(IMapper mapper, IMediator mediator)
    {
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    [HttpGet("{pointId:long}")]
    [ActionName(ActionGetByIdName)]
    public async Task<ActionResult<LocationViewModel>> GetByIdAsync(long pointId)
    {
        var message = new GetLocationByIdQueryMessage { PointId = pointId };
        var locationDto = await _mediator.Send(message);
        var viewModel = _mapper.Map<LocationViewModel>(locationDto);

        return Ok(viewModel);
    }

    [HttpPost]
    [ActionName(ActionCreateName)]
    public async Task<ActionResult<LocationViewModel>> CreateAsync([FromBody] CreateLocationRequest request)
    {
        if (request == null)
        {
            return BadRequest();
        }

        var message = _mapper.Map<AddLocationCommandMessage>(request);
        var locationDto = await _mediator.Send(message);
        var viewModel = _mapper.Map<LocationViewModel>(locationDto);

        return CreatedAtAction
        (
            actionName: ActionGetByIdName,
            controllerName: ControllerName,
            routeValues: new { pointId = viewModel.Id },
            value: viewModel
        );
    }

    [HttpPut("{pointId:long}")]
    [ActionName(ActionUpdateName)]
    public async Task<ActionResult<LocationViewModel>> UpdateAsync(long pointId, [FromBody] UpdateLocationRequest request)
    {
        if (request == null)
        {
            return BadRequest();
        }

        var message = _mapper.Map<UpdateLocationCommandMessage>(request);
        message.PointId = pointId;
        var locationDto = await _mediator.Send(message);
        var viewModel = _mapper.Map<LocationViewModel>(locationDto);

        return Ok(viewModel);
    }

    [HttpDelete("{pointId:long}")]
    [ActionName(ActionDeleteName)]
    public async Task<ActionResult> DeleteAsync(long pointId)
    {
        var message = new DeleteLocationCommandMessage { PointId = pointId };
        await _mediator.Send(message);

        return Ok();
    }
}