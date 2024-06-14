using MediatR;
using MediatR.Pipeline;
using Microsoft.EntityFrameworkCore;
using StargateAPI.Business.Data;
using StargateAPI.Controllers;

namespace StargateAPI.Business.Commands
{
    public class CreatePerson : IRequest<CreatePersonResult>
    {
        public required string Name { get; set; } = string.Empty;
    }

    public class CreatePersonPreProcessor : IRequestPreProcessor<CreatePerson>
    {
        private readonly StargateContext _context;
        private readonly ILogRecord _logger;
        public CreatePersonPreProcessor(StargateContext context, ILogRecord logger)
        {
            _context = context;
            _logger = logger;
        }
        public Task Process(CreatePerson request, CancellationToken cancellationToken)
        {
            if (request.Name.Trim() == string.Empty)
            {
                _logger.CreateLogRecord("Error", "Bad Request Person Blank");
                throw new BadHttpRequestException("Bad Request Person Blank");
            }
            var person = _context.Person.AsNoTracking().FirstOrDefault(z => z.Name == request.Name);

            if (person is not null)
            {
                _logger.CreateLogRecord("Error", "Bad Request Person Blank");
                throw new BadHttpRequestException("Bad Request Person Already exisit");

            }
            return Task.CompletedTask;
        }
    }

    public class CreatePersonHandler : IRequestHandler<CreatePerson, CreatePersonResult>
    {
        private readonly StargateContext _context;
        private readonly ILogRecord _logger;
        public CreatePersonHandler(StargateContext context, ILogRecord logger)
        {
            _logger = logger;
            _context = context;
        }
        public async Task<CreatePersonResult> Handle(CreatePerson request, CancellationToken cancellationToken)
        {

            var newPerson = new Person()
            {
                Name = request.Name
            };

            await _context.Person.AddAsync(newPerson);

            await _context.SaveChangesAsync();
            _logger.CreateLogRecord("Added new person " + request.Name, "Info");
            return new CreatePersonResult()
            {
                Id = newPerson.Id
            };

        }
    }

    public class CreatePersonResult : BaseResponse
    {
        public int Id { get; set; }
    }
}
