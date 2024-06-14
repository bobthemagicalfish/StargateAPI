using MediatR;
using Microsoft.EntityFrameworkCore;
using StargateAPI.Business.Data;

namespace StargateAPI.Business.Commands
{
    public class CreateLogRecord : ILogRecord
    {
        private readonly StargateContext _context;
        public CreateLogRecord(StargateContext context)
        {
            _context = context;
        }
        void ILogRecord.CreateLogRecord(string message, string typeofmessage)
        {
            _context.Logging.Add(new Logging() 
            { Date = DateTime.Now,
            Message=message,
            TypeOfMessage=typeofmessage});
            _context.SaveChanges();
        }
    }
}
