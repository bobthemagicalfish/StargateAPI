using MediatR;
using Microsoft.EntityFrameworkCore;
using StargateAPI.Business.Data;

namespace StargateAPI.Business.Commands
{
    public class CreateLogRecord : ICreateLogRecord
    {
        private readonly StargateContext _context;
        public CreateLogRecord(StargateContext context)
        {
            _context = context;
        }
        void ICreateLogRecord.CreateLogRecord(string message, string typeofmessage)
        {
            _context.Logging.Add(new Logging() 
            { Date = DateTime.Now,
            Message=message,
            TypeOfMessage=typeofmessage});
            _context.SaveChanges();
        }
    }
}
