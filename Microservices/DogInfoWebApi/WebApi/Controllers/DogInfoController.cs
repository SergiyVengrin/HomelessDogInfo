using Application.DogInfo.Commands;
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
        public async Task<IActionResult> CreateDogInfo([FromBody] CreateDogInfoDTO createDogInfoDTO, CancellationToken cancellationToken)
        {
            var command = _mapper.Map<CreateDogInfoCommand>(createDogInfoDTO);
            await _mediator.Send(command, cancellationToken);

            return Ok();
        }
    }
}
