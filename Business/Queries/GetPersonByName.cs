using Dapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using StargateAPI.Business.Data;
using StargateAPI.Business.Dtos;
using StargateAPI.Controllers;

namespace StargateAPI.Business.Queries
{
    public class GetPersonByName : IRequest<GetPersonByNameResult>
    {
        public required string Name { get; set; } = string.Empty;
    }

    public class GetPersonByNameHandler : IRequestHandler<GetPersonByName, GetPersonByNameResult>
    {
        private readonly StargateContext _context;
        public GetPersonByNameHandler(StargateContext context)
        {
            _context = context;
        }

        public async Task<GetPersonByNameResult> Handle(GetPersonByName request, CancellationToken cancellationToken)
        {
        

           // var query = $"SELECT a.Id as PersonId, a.Name, b.CurrentRank, b.CurrentDutyTitle, b.CareerStartDate, b.CareerEndDate FROM [Person] a LEFT JOIN [AstronautDetail] b on b.PersonId = a.Id WHERE '{request.Name}' = a.Name";
            var result = new GetPersonByNameResult();

            var personQuery = from a in _context.Person
                              join b in _context.AstronautDetails on a.Id equals b.PersonId into details
                              from b in details.DefaultIfEmpty() 
                              where a.Name == request.Name 
                              select new PersonAstronaut
                              {
                                  PersonId = a.Id,
                                  Name = a.Name,
                                  CurrentRank = b.CurrentRank,
                                  CurrentDutyTitle = b.CurrentDutyTitle,
                                  CareerStartDate = b.CareerStartDate,
                                  CareerEndDate = b.CareerEndDate
                              };

   
            result.Person = await personQuery.FirstOrDefaultAsync(cancellationToken);
          
            return result;
        }
    }

    public class GetPersonByNameResult : BaseResponse
    {
        public PersonAstronaut? Person { get; set; }
    }
}
