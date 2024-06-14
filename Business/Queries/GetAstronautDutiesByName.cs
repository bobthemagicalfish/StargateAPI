using Dapper;
using MediatR;
using StargateAPI.Business.Commands;
using StargateAPI.Business.Data;
using StargateAPI.Business.Dtos;
using StargateAPI.Controllers;

namespace StargateAPI.Business.Queries
{
    public class GetAstronautDutiesByName : IRequest<GetAstronautDutiesByNameResult>
    {
        public string Name { get; set; } = string.Empty;
    }

    public class GetAstronautDutiesByNameHandler : IRequestHandler<GetAstronautDutiesByName, GetAstronautDutiesByNameResult>
    {
        private readonly StargateContext _context;
        private readonly ILogRecord _logger;
        public GetAstronautDutiesByNameHandler(StargateContext context, ILogRecord logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<GetAstronautDutiesByNameResult> Handle(GetAstronautDutiesByName request, CancellationToken cancellationToken)
        {
            var result = new GetAstronautDutiesByNameResult();

            if (request.Name == null || request.Name.Trim().Length == 0)
            {

                _logger.CreateLogRecord("Blank name on GetAstronautDutiesByName request", "warn");
                return result;

            }
            

            // Using LINQ to perform a left join and select data
            var personQuery = from a in _context.Person
                              join b in _context.AstronautDetails on a.Id equals b.PersonId into details
                              from b in details.DefaultIfEmpty() // Handling the left join
                              where a.Name == request.Name       // Safe parameterized query
                              select new PersonAstronaut
                              {
                                  PersonId = a.Id,
                                  Name = a.Name,
                                  CurrentRank = b.CurrentRank,
                                  CurrentDutyTitle = b.CurrentDutyTitle,
                                  CareerStartDate = b.CareerStartDate,
                                  CareerEndDate = b.CareerEndDate
                              };
            personQuery.OrderBy(x => x.CareerEndDate);
            // Execute the query and get the first or default result asynchronously
            var person =  personQuery.FirstOrDefault();

            if (person != null)
            {
                result.Person = person;
                // Querying duties only if person is found
                var dutiesQuery = from d in _context.AstronautDuties
                                  where d.PersonId == person.PersonId
                                  orderby d.DutyStartDate descending
                                  select d;

                // Execute the query and convert the result to a list asynchronously
                var duties =  dutiesQuery.ToList();

                result.AstronautDuties = duties;
            }
            else
            {
                _logger.CreateLogRecord($"person not found GetAstronautDutiesByName request {request.Name} ", "warn");
                return result;
            }

            return result;

        }
    }

    public class GetAstronautDutiesByNameResult : BaseResponse
    {
        public PersonAstronaut Person { get; set; }
        public List<AstronautDuty> AstronautDuties { get; set; } = new List<AstronautDuty>();
    }
}
