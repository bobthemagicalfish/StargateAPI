namespace StargateAPI.Business.Commands
{
    public interface ICreateLogRecord
    {
        void CreateLogRecord(string message,string typeofmessage);
    }
}
