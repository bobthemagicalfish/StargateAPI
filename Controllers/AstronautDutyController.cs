using MediatR;
using Microsoft.AspNetCore.Mvc;
using StargateAPI.Business.Commands;
using StargateAPI.Business.Queries;
using System.Net;

namespace StargateAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AstronautDutyController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogRecord _logger;
        public AstronautDutyController(IMediator mediator, ILogRecord logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet("{name}")]
        public async Task<IActionResult> GetAstronautDutiesByName(string name)
        {
            _logger.CreateLogRecord($"STarting GetAstronautDutiesByName for{name} ", "Log");
            try
            {
                var result = await _mediator.Send(new GetAstronautDutiesByName()
                {
                    Name = name
                });
                _logger.CreateLogRecord($"end GetAstronautDutiesByName for{name} ", "Log");
                return this.GetResponse(result);
            }
            catch (Exception ex)
            {
                _logger.CreateLogRecord($"error for GetAstronautDutiesByName for{name} {ex.Message} ", "Error");
                return this.GetResponse(new BaseResponse()
                {
                    Message = ex.Message,
                    Success = false,
                    ResponseCode = (int)HttpStatusCode.InternalServerError
                });
            }            
        }

        [HttpPost("")]
        public async Task<IActionResult> CreateAstronautDuty([FromBody] CreateAstronautDuty request)
        {
                var result = await _mediator.Send(request);
                return this.GetResponse(result);           
        }
    }
}