namespace OrderService.Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));

        Task<bool> SetEntityasync(CancellationToken cancellationToken = default(CancellationToken));
    }
}