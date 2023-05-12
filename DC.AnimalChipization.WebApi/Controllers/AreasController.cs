using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using DC.AnimalChipization.Application.Features.Areas.Messages.Queries;
using DC.AnimalChipization.WebApi.ViewModels.Areas;
using DC.AnimalChipization.Application.Features.Areas.Messages.Commands;
using DC.AnimalChipization.WebApi.ViewModels.Areas.Requests;

namespace DC.AnimalChipization.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AreasController : ControllerBase
    {
        #region [ControllerConstants]

        public const string ControllerName = "Areas";

        public const string ActionGetByIdName = "GetById";
        public const string ActionAnalyticsName = "Analytics";
        public const string ActionCreateName = "Create";
        public const string ActionUpdateName = "Update";
        public const string ActionDeleteName = "Delete";
        #endregion

        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public AreasController(IMapper mapper, IMediator mediator)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpGet("{areaId:long}")]
        [ActionName(ActionGetByIdName)]
        public async Task<ActionResult<AreaViewModel>> GetByIdAsync(int areaId)
        {
            var message = new GetAreaByIdQueryMessage { Id = areaId };
            var areaDto = await _mediator.Send(message);
            var viewModel = _mapper.Map<AreaViewModel>(areaDto);

            return Ok(viewModel);
        }

        [HttpGet("{areaId:long}/analytics")]
        [ActionName(ActionAnalyticsName)]
        public async Task<ActionResult<AreaAnalyticResultViewModel>> GetAnalyticsAsync(long areaId, [FromQuery] AreaAnalyticRequest request)
        {
            var message = _mapper.Map<GetAreaAnalyticsQueryMessage>(request);
            message.AreaId = areaId;
            var response = await _mediator.Send(message);
            var viewModel = _mapper.Map<AreaAnalyticResultViewModel>(response);

            return Ok(viewModel);
        }

        [HttpPost]
        [ActionName(ActionCreateName)]
        public async Task<ActionResult<AreaViewModel>> CreateAsync([FromBody] CreateAreaRequest request)
        {
            var message = _mapper.Map<AddAreaCommandMessage>(request);
            var response = await _mediator.Send(message);
            var viewModel = _mapper.Map<AreaViewModel>(response);

            return CreatedAtAction
            (
                actionName: ActionGetByIdName,
                controllerName: ControllerName,
                routeValues: new { areaId = viewModel.Id },
                viewModel
            );
        }

        [HttpPut("{areaId:long}")]
        [ActionName(ActionUpdateName)]
        public async Task<ActionResult<AreaViewModel>> UpdateAsync(long areaId, [FromBody] UpdateAreaRequest request)
        {
            var message = _mapper.Map<UpdateAreaCommandMessage>(request);
            message.AreaId = areaId;
            var response = await _mediator.Send(message);
            var viewModel = _mapper.Map<AreaViewModel>(response);

            return Ok(viewModel);
        }

        [HttpDelete("{areaId:long}")]
        [ActionName(ActionDeleteName)]
        public async Task<ActionResult> DeleteAsync(long areaId)
        {
            var message = new DeleteAreaCommandMessage { AreaId = areaId };
            await _mediator.Send(message);

            return Ok();
        }
    }
}
