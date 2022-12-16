using Application.Image.Commands.CreateImage;
using Application.Image.Commands.DeleteImage;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebApi.DTOs.ImageDTOs;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ImageController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public ImageController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost("CreateImage")]
        public async Task<ActionResult<int>> CreateImage([FromForm] CreateImageDTO createImageDTO, CancellationToken cancellationToken)
        {
            var command = _mapper.Map<CreateImageCommand>(createImageDTO);
            var id = await _mediator.Send(command, cancellationToken);

            return Ok(id);
        }


        [HttpDelete("DeleteImage")]
        public async Task<ActionResult> DeleteImage(int id, CancellationToken cancellationToken)
        {
            await _mediator.Send(new DeleteImageCommand { Id= id }, cancellationToken);

            return Ok();
        }
    }
}
