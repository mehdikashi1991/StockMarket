using Domain;

namespace Application.Contract
{
    public interface ICancellOrderCommandHandler : ICommandHandler<long>
    {
    }
}