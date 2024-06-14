//using Dapper;
using MediatR;
using MediatR.Pipeline;
using Microsoft.EntityFrameworkCore;
using StargateAPI.Business.Data;
using StargateAPI.Controllers;
using System.Net;

namespace StargateAPI.Business.Commands
{
    public class CreateAstronautDuty : IRequest<CreateAstronautDutyResult>
    {
        public required string Name { get; set; }

        public required string Rank { get; set; }

        public required string DutyTitle { get; set; }

        public DateTime DutyStartDate { get; set; }
    }

    public class CreateAstronautDutyPreProcessor : IRequestPreProcessor<CreateAstronautDuty>
    {
        private readonly StargateContext _context;
        private readonly ILogRecord _logger;
        public CreateAstronautDutyPreProcessor(StargateContext context,ILogRecord log)
        {
            _context = context;
            _logger = log;
        }

        public Task Process(CreateAstronautDuty request, CancellationToken cancellationToken)
        {
            _logger.CreateLogRecord(@$"validating  Name:{request.Name}  rank:{request.Rank}  
                                        DutyTile:{request.DutyTitle} DutyStart:{request.DutyStartDate}","info");
            // checking to make sure data is not null for required fields
            if (request.Name.Trim().Length == 0
                || request.Rank.Trim().Length == 0
                || request.DutyTitle.Trim().Length == 0
                || request.DutyStartDate.Date == new DateTime()) {
                _logger.CreateLogRecord(@$"Bad Data in request for CreateAstronautDuty Name:{request.Name} " +
                    "                    rank:{request.Rank}  DutyTile:{request.DutyTitle} DutyStart:{request.DutyStartDate}","Error");
                throw new BadHttpRequestException("Bad Request Blank data dectected");
            }

            var person = _context.Person.AsNoTracking().FirstOrDefault(z => z.Name == request.Name);

            if (person is null) {
                _logger.CreateLogRecord(@$"Bad Request Person not found {request.Name} ", "Error");
                throw new BadHttpRequestException("Bad Request Person not found"); 
            }

            var verifyNoPreviousDuty = _context.AstronautDuties.FirstOrDefault(z => z.DutyTitle == request.DutyTitle 
                                                                                && z.DutyStartDate.Date == request.DutyStartDate.Date);

            if (verifyNoPreviousDuty is not null) {
                _logger.CreateLogRecord($@"Bad Data in request for CreateAstronautDuty Name:{request.Name}  
                            rank:{request.Rank}  DutyTile:{request.DutyTitle} DutyStart:{request.DutyStartDate}", "Error");
                throw new BadHttpRequestException("Bad Request  Same Duty entered twice"); 
            }

            return Task.CompletedTask;
        }
    }

    public class CreateAstronautDutyHandler : IRequestHandler<CreateAstronautDuty, CreateAstronautDutyResult>
    {
        private readonly StargateContext _context;
        private readonly ILogRecord _logger;
        public CreateAstronautDutyHandler(StargateContext context, ILogRecord log)
        {
            _context = context;
            _logger = log;
        }
        public async Task<CreateAstronautDutyResult> Handle(CreateAstronautDuty request, CancellationToken cancellationToken)
        {
            _logger.CreateLogRecord(@$"Handleing  Name:{request.Name}  rank:{request.Rank}  DutyTile:{request.DutyTitle} DutyStart:{request.DutyStartDate}", "Error");

            // var query = $"SELECT * FROM [Person] WHERE \'{request.Name}\' = Name";

            var person =  _context.Person.Where(x=>x.Name== request.Name).FirstOrDefault();

            if ( person == null)
            {
                _logger.CreateLogRecord(@$"Bad Request Person not found {request.Name} ", "Error");

                throw new BadHttpRequestException("Bad Request");
            }
            //query = $"SELECT * FROM [AstronautDetail] WHERE {person.Id} = PersonId";

            var astronautDetail = _context.AstronautDetails.Where(x => x.Id == person.Id).FirstOrDefault();

            if (astronautDetail == null)
            {
                astronautDetail = new AstronautDetail();
                astronautDetail.PersonId = person.Id;
                astronautDetail.CurrentDutyTitle = request.DutyTitle;
                astronautDetail.CurrentRank = request.Rank;
                astronautDetail.CareerStartDate = request.DutyStartDate.Date;
                if (request.DutyTitle == "RETIRED")
                {
                    astronautDetail.CareerEndDate = request.DutyStartDate.Date;
                }

                 _context.AstronautDetails.Add(astronautDetail);

            }
            else
            {
                astronautDetail.CurrentDutyTitle = request.DutyTitle;
                astronautDetail.CurrentRank = request.Rank;
                if (request.DutyTitle == "RETIRED")
                {
                    astronautDetail.CareerEndDate = request.DutyStartDate.AddDays(-1).Date;
                }
                _context.AstronautDetails.Update(astronautDetail);
            }

           // query = $"SELECT * FROM [AstronautDuty] WHERE {person.Id} = PersonId Order By DutyStartDate Desc";

            var previousAstronautDuty = _context.AstronautDuties
                .Where(x=>x.PersonId==astronautDetail.Id 
                        && x.DutyEndDate == null
                
                ).FirstOrDefault();

            if (previousAstronautDuty != null)
            {
                previousAstronautDuty.DutyEndDate = request.DutyStartDate.AddDays(-1).Date;
                _context.AstronautDuties.Update(previousAstronautDuty);
            }

            var newAstronautDuty = new AstronautDuty()
            {
                PersonId = person.Id,
                Rank = request.Rank,
                DutyTitle = request.DutyTitle,
                DutyStartDate = request.DutyStartDate.Date,
                DutyEndDate = null
            };

            await _context.AstronautDuties.AddAsync(newAstronautDuty);

            await _context.SaveChangesAsync();
            _logger.CreateLogRecord(@$"Done adding duty for {request.Name}  ", "info");

            return new CreateAstronautDutyResult()
            {
                Id = newAstronautDuty.Id
            };
        }
    }

    public class CreateAstronautDutyResult : BaseResponse
    {
        public int? Id { get; set; }

    }
}
