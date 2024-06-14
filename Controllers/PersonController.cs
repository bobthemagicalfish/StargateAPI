using MediatR;
using Microsoft.AspNetCore.Mvc;
using StargateAPI.Business.Commands;
using StargateAPI.Business.Queries;
using System.Net;
using System.Xml.Linq;

namespace StargateAPI.Controllers
{
   
    [ApiController]
    [Route("[controller]")]
    public class PersonController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogRecord _logger;
        public PersonController(IMediator mediator, ILogRecord logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet("")]
        public async Task<IActionResult> GetPeople()
        {
            try
            {
                _logger.CreateLogRecord($"STarting GetPeople ", "Log");

                var result = await _mediator.Send(new GetPeople()
                {

                });
                _logger.CreateLogRecord($"ending GetPeople ", "Log");
                return this.GetResponse(result);
            }
            catch (Exception ex)
            {
                _logger.CreateLogRecord($"Error GetPeople {ex.Message}", "Error");
                return this.GetResponse(new BaseResponse()
                {
                    Message = ex.Message,
                    Success = false,
                    ResponseCode = (int)HttpStatusCode.InternalServerError
                });
            }
        }

        [HttpGet("{name}")]
        public async Task<IActionResult> GetPersonByName(string name)
        {
            try
            {
                _logger.CreateLogRecord($"STarting GetPersonByName ", "Log");
                var result = await _mediator.Send(new GetPersonByName()
                {
                    Name = name
                });
                _logger.CreateLogRecord($"ending GetPersonByName ", "Log");
                return this.GetResponse(result);
            }
            catch (Exception ex)
            {
                _logger.CreateLogRecord($"Error GetPersonByName {ex.Message}", "Error");
                return this.GetResponse(new BaseResponse()
                {
                    Message = ex.Message,
                    Success = false,
                    ResponseCode = (int)HttpStatusCode.InternalServerError
                });
            }
        }

        [HttpPost("")]
        public async Task<IActionResult> CreatePerson([FromBody] string name)
        {
            try
            {
                _logger.CreateLogRecord($"STarting CreatePerson ", "Log");
                var result = await _mediator.Send(new CreatePerson()
                {
                    Name = name
                });
                _logger.CreateLogRecord($"ending CreatePerson ", "Log");
                return this.GetResponse(result);
            }
            catch (Exception ex)
            {
                _logger.CreateLogRecord($"Error CreatePerson {ex.Message}", "Error");
                return this.GetResponse(new BaseResponse()
                {
                    Message = ex.Message,
                    Success = false,
                    ResponseCode = (int)HttpStatusCode.InternalServerError
                });
            }

        }
    }
}