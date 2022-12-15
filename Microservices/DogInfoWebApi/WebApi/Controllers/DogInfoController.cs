using Application.DogInfo.Commands.CreateDogInfo;
using Application.DogInfo.Commands.DeleteDogInfo;
using Application.DogInfo.Commands.UpdateDogInfo;
using Application.DogInfo.Queries.GetDogInfoById;
using Application.DogInfo.Queries.GetDogInfoByName;
using Application.DogInfo.Queries.GetDogInfoByResidence;
using Application.DogInfo.Queries.GetDogInfoList;
using Application.DogInfo.VMs;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebApi.DTOs.DogInfoDTOs;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class DogInfoController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly ILogger<DogInfoController> _logger;

        public DogInfoController(IMediator mediator, IMapper mapper, ILogger<DogInfoController> logger)
        {
            _mediator = mediator;
            _mapper = mapper;
            _logger = logger;
        }


        [HttpPost("CreateDogInfo")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> CreateDogInfo([FromBody] CreateDogInfoDTO createDogInfoDTO, CancellationToken cancellationToken)
        {
            var command = _mapper.Map<CreateDogInfoCommand>(createDogInfoDTO);
            await _mediator.Send(command, cancellationToken);

            return Ok();
        }


        [HttpDelete("DeleteDogInfo")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> DeleteDogInfo(int id, CancellationToken cancellationToken)
        {
            await _mediator.Send(new DeleteDogInfoCommand { Id = id }, cancellationToken);

            return Ok();
        }


        [HttpPut("UpdateDogInfo")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> UpdateDogInfo([FromBody] UpdateDogInfoDTO updateDogInfoDTO, CancellationToken cancellationToken)
        {
            var command = _mapper.Map<UpdateDogInfoCommand>(updateDogInfoDTO);
            await _mediator.Send(command, cancellationToken);

            return Ok();
        }


        [HttpGet("GetDogInfoList")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<DogInfoListVM>> GetDogInfoList(CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetDogInfoListQuery(), cancellationToken);

            return Ok(result);
        }


        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<DogInfoVM>> GetDogInfoById(int id, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetDogInfoByIdQuery { Id = id }, cancellationToken);

            return Ok(result);
        }


        [HttpGet("{name}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<DogInfoVM>> GetDogInfoByName(string name,  CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetDogInfoByNameQuery { Name = name }, cancellationToken);

            return Ok(result);
        }


        [HttpGet("{residence}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<DogInfoVM>> GetDogInfoByResidence(string residence, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetDogInfoByResidenceQuery { Residence = residence }, cancellationToken);

            return Ok(result);
        }
    }
}
