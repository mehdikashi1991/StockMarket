using Framework.Contracts;

namespace MessageNserviceBus
{
    public class MessageSender : IMessageService
    {
        private readonly IMessageSession message;

        public MessageSender(IMessageSession message)
        {
            this.message = message;
        }
        public async Task SendMessageAsync(object obj)
        {
            await message.Send(obj);
        }
    }
}