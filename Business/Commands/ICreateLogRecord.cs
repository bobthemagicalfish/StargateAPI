namespace StargateAPI.Business.Commands
{
    public interface ILogRecord
    {
        void CreateLogRecord(string message,string typeofmessage);
    }
}
