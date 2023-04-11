using AutoMapper;
using DC.AnimalChipization.Application.Features.Accounts.Messages.Queries;
using DC.AnimalChipization.WebApi.ViewModels.Accounts;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using DC.AnimalChipization.Application.Features.Areas.Messages.Queries;
using DC.AnimalChipization.WebApi.ViewModels.Areas;

namespace DC.AnimalChipization.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AreasController : ControllerBase
    {
        #region [ControllerConstants]

        public const string ControllerName = "Accounts";

        public const string ActionGetByIdName = "GetById";
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

        [HttpGet("{areaId:int}")]
        [ActionName(ActionGetByIdName)]
        public async Task<ActionResult<AccountViewModel>> GetByIdAsync(int areaId)
        {
            var message = new GetAreaByIdQueryMessage { Id = areaId };
            var areaDto = await _mediator.Send(message);
            var viewModel = _mapper.Map<AreaViewModel>(areaDto);

            return Ok(viewModel);
        }
    }
}
