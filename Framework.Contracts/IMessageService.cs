namespace Framework.Contracts
{
    public interface IMessageService
    {
        Task SendMessageAsync(object message);
    }
}