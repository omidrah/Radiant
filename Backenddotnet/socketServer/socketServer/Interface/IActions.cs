namespace socketServer.Interface
{
    public interface IActions
    {
        Task LogErrorAsync(Exception exception, string business, string ip = null);
    }
}