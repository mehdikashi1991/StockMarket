namespace Domain
{
    internal interface ICommand
    {
        Task<object?> Execute();
    }
}
